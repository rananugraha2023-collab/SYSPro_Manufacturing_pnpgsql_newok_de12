Imports master_new.ModFunction
Imports npgsql

Public Class FAssetReport
    Dim _ds_report As New DataSet
    Dim _is_insert As Boolean
    Dim dt_bantu As New DataTable
    Public _bdgt_oid, _trans_id, _active As String
    Public _cc_id, _en_id, _year As Integer
    Dim ds_bantu As New DataSet
    Dim _part_number, _serial, _fabk_code, _assbk_oid As String
    Dim _ass_id As Integer
    Dim ds_depr As New DataSet
    Dim _ds_select As New DataSet
    Dim _famt_method As String

    Dim _start_date As Date

    Private Sub FAssetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _is_insert = True
        pr_date.EditValue = Today()
    End Sub

    Public Overrides Sub load_data_many(ByVal par As Boolean)
        GenerateReport()

        Dim _filter_date As Date

        'Rev By Hendrik 2011-02-07
        '_filter_date = (pr_date.DateTime.Date).AddMonths(-1)
        _filter_date = pr_date.DateTime.Date

        _ds_report = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  assrep_ass_id, " _
                        & "  pt_code, " _
                        & "  ass_code,ass_service_date, " _
                        & "  ass_basis_cost - ass_salvage_cost as basis_cost, " _
                        & "  assrep_depr_today, " _
                        & "  assrep_tahun, " _
                        & "  assrep_bulan, " _
                        & "  assrep_bulan_ke, " _
                        & "  assrep_depresiasi, " _
                        & "  assrep_depr_acum, " _
                        & "  assrep_nilai_buku, " _
                        & "  assrep_usr_id,assrep_date " _
                        & "FROM  " _
                        & "  public.assrep_report " _
                        & "  inner join ass_mstr on ass_id = assrep_ass_id " _
                        & "  inner join pt_mstr on pt_id =  ass_pt_id " _
                        & "  where assrep_usr_id = " + SetInteger(master_new.ClsVar.sUserID) _
                        & "  and assrep_date >= " + SetDate(_filter_date.Date) _
                        & " order by assrep_date asc "
                    '& "  and ass_code = 'A-11-01-A21-0001' " _
                    .InitializeCommand()
                    .FillDataSet(_ds_report, "report")
                    pgc_detail.DataSource = _ds_report
                    pgc_detail.DataMember = "report"
                    pgc_detail.BestFit()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub GenerateReport()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM assrep_report " _
                                               & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    Catch ex As nPgSqlException
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        _ds_select = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  ass_oid, " _
                        & "  ass_dom_id, " _
                        & "  ass_en_id, " _
                        & "  ass_add_by, " _
                        & "  ass_add_date, " _
                        & "  ass_upd_by, " _
                        & "  ass_upd_date, " _
                        & "  ass_id, " _
                        & "  ass_pt_id, pt_code," _
                        & "  ass_code, " _
                        & "  ass_barcode, " _
                        & "  ass_desc, " _
                        & "  ass_class, " _
                        & "  ass_ref_req, " _
                        & "  ass_ref_po, " _
                        & "  ass_ref_rcpt, " _
                        & "  ass_ref_rcpt_oid, " _
                        & "  ass_ref_inv, " _
                        & "  ass_model, " _
                        & "  ass_qty, " _
                        & "  ass_qty_assgn, " _
                        & "  ass_qty_del, " _
                        & "  ass_sn, " _
                        & "  ass_service_date, " _
                        & "  ass_gar_date, " _
                        & "  ass_line, " _
                        & "  ass_ptnr_id, " _
                        & "  ass_st_purc, " _
                        & "  ass_lic_type, " _
                        & "  ass_service_cost, " _
                        & "  ass_emp_id, " _
                        & "  ass_emp_dept, " _
                        & "  ass_emp_rg, " _
                        & "  ass_confirm, " _
                        & "  ass_its_id, " _
                        & "  ass_dt, " _
                        & "  ass_salvage_cost, " _
                        & "  ass_basis_cost - ass_salvage_cost as basis_cost, " _
                        & "  ass_service_cost, " _
                        & "  ass_depr_acum,0 as ass_depr_amount, " _
                        & "  ass_depr_date, " _
                        & "  ass_disp_amount, " _
                        & "  ass_disp_date,pl_fa_depr,famt_oid,assbk_oid,assbk_exp_life, " _
                        & "  assbk_per_year,assbk_per_month, " _
                        & "  famt_method " _
                        & "FROM  " _
                        & "  public.ass_mstr  " _
                        & "  inner join pt_mstr on pt_id = ass_pt_id " _
                        & "  inner join pl_mstr on pl_id = pt_pl_id " _
                        & "  inner join assbk_mstr on assbk_ass_oid = ass_oid " _
                        & "  inner join fabk_mstr on fabk_id = assbk_fabk_id and fabk_posted = 'Y' " _
                        & "  inner join famt_mstr on famt_id = assbk_famt_id " _
                        & "  where pl_fa_depr = 'Y' and ass_class = 'A' and ass_qty_del = 0 " _
                        & "  order by pt_code,ass_code asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_select, "select")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For Each _dr As DataRow In _ds_select.Tables(0).Rows
            _part_number = _dr("pt_code")
            _ass_id = _dr("ass_id")
            If _dr("famt_method") = "T" Then
                view_custom_table(_dr("ass_id"), _dr("famt_oid").ToString, _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                      Month(_dr("ass_service_date")), _dr("ass_service_cost"), _dr("ass_salvage_cost"))
            ElseIf _dr("famt_method") = "D" Then
                view_double_declining(_dr("ass_id"), _dr("famt_oid").ToString, _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                      Month(_dr("ass_service_date")), _dr("ass_service_cost"), _dr("ass_salvage_cost"))
            ElseIf _dr("famt_method") = "B" Then
                view_double_declining2(_dr("ass_id"), _dr("famt_oid").ToString, _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                      Month(_dr("ass_service_date")), _dr("ass_service_cost"), _dr("ass_salvage_cost"))
            ElseIf _dr("famt_method") = "S" Then
                view_stright_line(_dr("ass_id"), _dr("famt_oid").ToString, _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                      Month(_dr("ass_service_date")), _dr("ass_service_cost"), _dr("ass_salvage_cost"))
            End If
        Next

    End Sub

    Public Sub view_custom_table(ByVal par_ass_id As Integer, ByVal par_famt_oid As String, _
                                     ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                     ByVal par_month As Integer, ByVal par_cost As Double, _
                                     ByVal par_salv_cost As Double)
        Dim ds_bantu As New DataSet
        Dim i As Integer
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
                            & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
                            & "  where famt_oid = " + SetSetring(par_famt_oid.ToString())
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "req")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim _cost_after_salv, _nilai_buku, _depr_acum As Double
        _cost_after_salv = par_cost - par_salv_cost
        _nilai_buku = _cost_after_salv

        Dim _start_month As Integer
        _start_month = par_month

        Dim _bulan_ke As Integer = 0
        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        _end_date_life = par_start_date.AddYears(par_exp_life)

        Dim _bulan_date As Date
        Dim _tahun, _bulan As Integer
        _tahun = Year(par_start_date)
        _bulan_date = par_start_date

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '_tahun = _tahun + i
            _tahun = Year(par_start_date.AddYears(i))

            Dim _per_1, _per_2, _per_3, _per_4, _per_5, _per_6, _per_7, _per_8, _per_9, _per_10, _per_11, _per_12 As Double
            _per_1 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_1")
            _per_2 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_2")
            _per_3 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_3")
            _per_4 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_4")
            _per_5 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_5")
            _per_6 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_6")
            _per_7 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_7")
            _per_8 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_8")
            _per_9 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_9")
            _per_10 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_10")
            _per_11 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_11")
            _per_12 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_12")

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = par_exp_life Then
                _end_month_life = Month(_end_date_life) - 1
            Else
                _end_month_life = 12
            End If

            '_bulan = _start_month - 1
            _bulan = Month(_bulan_date)
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            _bulan_ke = 0
                            Dim a As Integer
                            For a = _start_month To _end_month_life
                                '_bulan = _bulan + 1
                                '_bulan_date = _bulan_date
                                _bulan_ke = _bulan_ke + 1
                                .Command.CommandType = CommandType.StoredProcedure
                                .Command.CommandText = "assrep_report_insert"
                                .AddParameter("arg_assrep_ass_id", par_ass_id)
                                .AddParameter("arg_assrep_depr_today", 0)
                                .AddParameter("arg_assrep_tahun", _tahun)
                                .AddParameter("arg_assrep_bulan", _bulan)
                                .AddParameter("arg_assrep_date", _bulan_date)
                                .AddParameter("arg_assrep_bulan_ke", _bulan_ke)
                                If a = 1 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_1))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_1))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_1))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 2 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_2))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_2))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_2))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 3 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_3))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_3))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_3))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 4 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_4))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_4))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_4))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 5 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_5))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_5))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_5))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 6 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_6))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_6))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_6))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 7 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_7))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_7))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_7))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 8 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_8))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_8))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_8))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 9 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_9))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_9))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_9))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 10 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_10))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_10))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_10))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 11 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_11))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_11))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_11))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 12 Then
                                    _depr_acum = _depr_acum + (_cost_after_salv * (_per_12))
                                    _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_12))
                                    If _nilai_buku < 0 Then
                                        _nilai_buku = 0
                                    End If
                                    .AddParameter("arg_assrep_depresiasi", _cost_after_salv * (_per_12))
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                End If

                                If _nilai_buku <= 0 Then
                                    sqlTran.Commit()
                                    Exit Sub
                                End If

                                'If _tahun = SetInteger(Year(Today())) And _bulan = SetInteger(Month(Today())) Then
                                '    sqlTran.Commit()
                                '    Exit Sub
                                'End If

                                _bulan_date = _bulan_date.AddMonths(1)
                                _bulan = Month(_bulan_date)
                            Next

                            sqlTran.Commit()
                        Catch ex As Exception
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Next

    End Sub

    Public Sub view_double_declining(ByVal par_ass_id As Integer, ByVal par_famt_oid As String, _
                                     ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                     ByVal par_month As Integer, ByVal par_cost As Double, _
                                     ByVal par_salv_cost As Double)
        'Rev By Hendrik 2011-02-07
        'Dim ds_bantu As New DataSet
        Dim i As Integer

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT  " _
        '                    & "  famtd_oid, " _
        '                    & "  famtd_famt_oid, " _
        '                    & "  famtd_add_by, " _
        '                    & "  famtd_add_date, " _
        '                    & "  famtd_upd_by, " _
        '                    & "  famtd_upd_date, " _
        '                    & "  famtd_year, " _
        '                    & "  famtd_per_1, " _
        '                    & "  famtd_per_2, " _
        '                    & "  famtd_per_3, " _
        '                    & "  famtd_per_4, " _
        '                    & "  famtd_per_5, " _
        '                    & "  famtd_per_6, " _
        '                    & "  famtd_per_7, " _
        '                    & "  famtd_per_8, " _
        '                    & "  famtd_per_9, " _
        '                    & "  famtd_per_10, " _
        '                    & "  famtd_per_11, " _
        '                    & "  famtd_per_12, " _
        '                    & "  famtd_dt " _
        '                    & "FROM  " _
        '                    & "  public.famtd_det " _
        '                    & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
        '                    & "  where famt_oid = " + SetSetring(par_famt_oid.ToString())
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "req")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        Dim _cost_after_salv, _depr_per_month, _depr_acum, _nilai_buku As Double
        _cost_after_salv = par_cost - par_salv_cost
        _nilai_buku = _cost_after_salv

        Dim _start_month As Integer
        _start_month = par_month

        Dim _bulan_date As Date
        Dim _bulan_ke As Integer = 0
        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        _end_date_life = par_start_date.AddYears(par_exp_life)
        _bulan_date = par_start_date

        Dim _tahun, _bulan As Integer
        For i = 0 To par_exp_life
            _tahun = Year(par_start_date.AddYears(i))
            _depr_per_month = (_nilai_buku * 0.5) / 12

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = par_exp_life Then
                _end_month_life = Month(_end_date_life) - 1
                _depr_per_month = _nilai_buku / _end_month_life
            Else
                _end_month_life = 12
            End If

            '_bulan = _start_month - 1
            _bulan = Month(_bulan_date)
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            _bulan_ke = 0
                            Dim a As Integer
                            For a = _start_month To _end_month_life
                                '_bulan = _bulan + 1
                                '_bulan_date = _bulan_date
                                _bulan_ke = _bulan_ke + 1
                                .Command.CommandType = CommandType.StoredProcedure
                                .Command.CommandText = "assrep_report_insert"
                                .AddParameter("arg_assrep_ass_id", par_ass_id)
                                .AddParameter("arg_assrep_depr_today", 0)
                                .AddParameter("arg_assrep_tahun", _tahun)
                                .AddParameter("arg_assrep_bulan", _bulan)
                                .AddParameter("assrep_date", _bulan_date)
                                .AddParameter("arg_assrep_bulan_ke", _bulan_ke)
                                If a = 1 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 2 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 3 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 4 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 5 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 6 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 7 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 8 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 9 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 10 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 11 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 12 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                End If

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'If _tahun = SetInteger(Year(Today())) And _bulan = SetInteger(Month(Today())) Then
                                '    sqlTran.Commit()
                                '    Exit Sub
                                'End If

                                _bulan_date = _bulan_date.AddMonths(1)
                                _bulan = Month(_bulan_date)

                            Next

                            sqlTran.Commit()
                        Catch ex As Exception
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Next

    End Sub

    Public Sub view_double_declining2(ByVal par_ass_id As Integer, ByVal par_famt_oid As String, _
                                     ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                     ByVal par_month As Integer, ByVal par_cost As Double, _
                                     ByVal par_salv_cost As Double)
        'Rev By Hendrik 2011-02-07
        'Dim ds_bantu As New DataSet
        Dim i As Integer

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT  " _
        '                    & "  famtd_oid, " _
        '                    & "  famtd_famt_oid, " _
        '                    & "  famtd_add_by, " _
        '                    & "  famtd_add_date, " _
        '                    & "  famtd_upd_by, " _
        '                    & "  famtd_upd_date, " _
        '                    & "  famtd_year, " _
        '                    & "  famtd_per_1, " _
        '                    & "  famtd_per_2, " _
        '                    & "  famtd_per_3, " _
        '                    & "  famtd_per_4, " _
        '                    & "  famtd_per_5, " _
        '                    & "  famtd_per_6, " _
        '                    & "  famtd_per_7, " _
        '                    & "  famtd_per_8, " _
        '                    & "  famtd_per_9, " _
        '                    & "  famtd_per_10, " _
        '                    & "  famtd_per_11, " _
        '                    & "  famtd_per_12, " _
        '                    & "  famtd_dt " _
        '                    & "FROM  " _
        '                    & "  public.famtd_det " _
        '                    & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
        '                    & "  where famt_oid = " + SetSetring(par_famt_oid.ToString())
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "req")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        Dim _cost_after_salv, _depr_per_month, _depr_acum, _nilai_buku As Double
        _cost_after_salv = par_cost - par_salv_cost
        _nilai_buku = _cost_after_salv

        Dim _start_month As Integer
        _start_month = par_month

        Dim _bulan_ke As Integer = 0
        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        _end_date_life = par_start_date.AddYears(par_exp_life)

        Dim _tarif As Double = 0
        _tarif = ((100 / 100) / par_exp_life) * 2

        Dim _bulan_date As Date
        Dim _tahun, _bulan As Integer
        '_tahun = Year(par_start_date)
        _bulan_date = par_start_date
        _tahun = Year(_bulan_date)

        For i = 0 To par_exp_life
            '_tahun = _tahun + i
            '_tahun = Year(_bulan_date.AddYears(i))

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = par_exp_life Then
                _end_month_life = Month(_end_date_life) - 1
            Else
                _end_month_life = 12
            End If

            '_bulan = _start_month - 1
            _tahun = Year(_bulan_date)
            _bulan = Month(_bulan_date)
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            _bulan_ke = 0
                            Dim a As Integer
                            For a = _start_month To _end_month_life
                                _depr_per_month = (_nilai_buku * _tarif) / 12

                                '_bulan = _bulan + 1
                                '_bulan_date = _bulan_date
                                _bulan_ke = _bulan_ke + 1
                                .Command.CommandType = CommandType.StoredProcedure
                                .Command.CommandText = "assrep_report_insert"
                                .AddParameter("arg_assrep_ass_id", par_ass_id)
                                .AddParameter("arg_assrep_depr_today", 0)
                                .AddParameter("arg_assrep_tahun", _tahun)
                                .AddParameter("arg_assrep_bulan", _bulan)
                                .AddParameter("assrep_date", _bulan_date)
                                .AddParameter("arg_assrep_bulan_ke", _bulan_ke)
                                If a = 1 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 2 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 3 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 4 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 5 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 6 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 7 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 8 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 9 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 10 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 11 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 12 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                End If

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'If _tahun = SetInteger(Year(Today())) And _bulan = SetInteger(Month(Today())) Then
                                '    sqlTran.Commit()
                                '    Exit Sub
                                'End If

                                _bulan_date = _bulan_date.AddMonths(1)
                                _bulan = Month(_bulan_date)
                            Next

                            sqlTran.Commit()
                        Catch ex As Exception
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Next

    End Sub

    Public Sub view_stright_line(ByVal par_ass_id As Integer, ByVal par_famt_oid As String, _
                                 ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                 ByVal par_month As Integer, ByVal par_cost As Double, _
                                 ByVal par_salv_cost As Double)
        'Rev By Hendrik 2011-02-07
        'Dim ds_bantu As New DataSet
        Dim i As Integer
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT  " _
        '                    & "  famtd_oid, " _
        '                    & "  famtd_famt_oid, " _
        '                    & "  famtd_add_by, " _
        '                    & "  famtd_add_date, " _
        '                    & "  famtd_upd_by, " _
        '                    & "  famtd_upd_date, " _
        '                    & "  famtd_year, " _
        '                    & "  famtd_per_1, " _
        '                    & "  famtd_per_2, " _
        '                    & "  famtd_per_3, " _
        '                    & "  famtd_per_4, " _
        '                    & "  famtd_per_5, " _
        '                    & "  famtd_per_6, " _
        '                    & "  famtd_per_7, " _
        '                    & "  famtd_per_8, " _
        '                    & "  famtd_per_9, " _
        '                    & "  famtd_per_10, " _
        '                    & "  famtd_per_11, " _
        '                    & "  famtd_per_12, " _
        '                    & "  famtd_dt " _
        '                    & "FROM  " _
        '                    & "  public.famtd_det " _
        '                    & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
        '                    & "  where famt_oid = " + SetSetring(par_famt_oid.ToString())
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "req")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        Dim _cost_after_salv, _depr_per_month, _depr_acum, _nilai_buku As Double
        _cost_after_salv = par_cost - par_salv_cost
        _nilai_buku = _cost_after_salv

        Dim _start_month As Integer
        _start_month = par_month

        Dim _bulan_date As Date
        Dim _bulan_ke As Integer = 0
        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        _end_date_life = par_start_date.AddYears(par_exp_life)
        _depr_per_month = _cost_after_salv / (par_exp_life * 12)
        _bulan_date = par_start_date

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        Dim _tahun, _bulan As Integer
                        _tahun = Year(par_start_date)

                        For i = 0 To par_exp_life
                            '_tahun = _tahun + i
                            _tahun = Year(_bulan_date.AddYears(i))

                            If i = 0 Then
                                _start_month = _start_month
                            Else
                                _start_month = 1
                            End If

                            If i = par_exp_life Then
                                _end_month_life = Month(_end_date_life) - 1
                            Else
                                _end_month_life = 12
                            End If

                            '_bulan = _start_month - 1
                            _bulan = Month(_bulan_date)

                            _bulan_ke = 0
                            Dim a As Integer
                            For a = _start_month To _end_month_life
                                '_bulan = _bulan + 1
                                '_bulan_date = _bulan_date
                                _bulan_ke = _bulan_ke + 1
                                .Command.CommandType = CommandType.StoredProcedure
                                .Command.CommandText = "assrep_report_insert"
                                .AddParameter("arg_assrep_ass_id", par_ass_id)
                                .AddParameter("arg_assrep_depr_today", 0)
                                .AddParameter("arg_assrep_tahun", _tahun)
                                .AddParameter("arg_assrep_bulan", _bulan)
                                .AddParameter("assrep_date", _bulan_date)
                                .AddParameter("arg_assrep_bulan_ke", _bulan_ke)
                                If a = 1 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 2 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 3 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 4 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 5 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 6 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 7 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 8 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 9 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 10 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 11 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                ElseIf a = 12 Then
                                    _depr_acum = _depr_acum + _depr_per_month
                                    _nilai_buku = _nilai_buku - _depr_per_month
                                    .AddParameter("arg_assrep_depresiasi", _depr_per_month)
                                    .AddParameter("arg_assrep_depr_acum", _depr_acum)
                                    .AddParameter("arg_assrep_nilai_buku", _nilai_buku)
                                    .AddParameter("arg_assrep_usr_id", master_new.ClsVar.sUserID)
                                End If

                                'If _tahun = SetInteger(Year(Today())) And _bulan = SetInteger(Month(Today())) Then
                                '    sqlTran.Commit()
                                '    Exit Sub
                                'End If

                                _bulan_date = _bulan_date.AddMonths(1)
                                _bulan = Month(_bulan_date)
                            Next
                        Next

                        sqlTran.Commit()
                    Catch ex As Exception
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
