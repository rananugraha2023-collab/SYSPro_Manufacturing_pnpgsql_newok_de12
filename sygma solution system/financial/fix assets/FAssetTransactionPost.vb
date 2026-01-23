Imports npgsql
Imports master_new.ModFunction

Public Class FAssetTransactionPost
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr As String
    Dim ds_select As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim ds_bantu As New DataSet
    Dim ds_depr As New DataSet

    Dim _part_number, _serial, _fabk_code, _assbk_oid As String
    Dim _year As Integer

    Private Sub FUpdateDepresiasiNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        _now = func_coll.get_now
        date_of_depr.EditValue = Today()
    End Sub

    Public Overrides Sub find()
        RetrieveData()
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "##", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Asset Code", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Barcode", "ass_barcode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "ass_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Service Date", "ass_service_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Cost", "ass_service_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Salvage Cost", "ass_salvage_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Basis Cost", "ass_basis_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Depresiasi Date", "ass_depr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Depresiasi/Month", "depr_per_month", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Depresiasi Akumulasi", "depr_acumulasi", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Private Sub RetrieveData()
        ds_select = New DataSet
        Try
            Using objcon As New master_new.WDABasepgsql("", "")
                With objcon
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.StoredProcedure
                        .Command.CommandText = "temp_ass_trans_post_del"
                        .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
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

        'Rev By Hendrik 2011-02-02
        Dim _ass_id As Integer = 0
        Dim ds_bantu As New DataSet
        Dim _month_of_depr, _yer_of_depr As String
        Dim _date_of_depr As Date
        _month_of_depr = Month(date_of_depr.EditValue).ToString
        _yer_of_depr = Year(date_of_depr.EditValue).ToString
        _date_of_depr = CDate("01/" + _month_of_depr + "/" + _yer_of_depr)

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  ass_oid,false as ceklist, " _
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
                        & "  coalesce(ass_salvage_cost,0) as ass_salvage_cost, " _
                        & "  ass_basis_cost, " _
                        & "  coalesce(ass_depr_acum,0) as ass_depr_acum,0 as ass_depr_amount, " _
                        & "  ass_depr_date, " _
                        & "  ass_disp_amount, " _
                        & "  ass_disp_date,pl_fa_depr,famt_oid,assbk_oid,assbk_exp_life, " _
                        & "  coalesce(assbk_per_year,0) as assbk_per_year,coalesce(assbk_per_month,0) as assbk_per_month, " _
                        & "  famt_method " _
                        & "FROM  " _
                        & "  public.ass_mstr  " _
                        & "  inner join pt_mstr on pt_id = ass_pt_id " _
                        & "  inner join pl_mstr on pl_id = pt_pl_id " _
                        & "  inner join assbk_mstr on assbk_ass_oid = ass_oid " _
                        & "  inner join fabk_mstr on fabk_id = assbk_fabk_id and fabk_posted = 'Y' " _
                        & "  inner join famt_mstr on famt_id = assbk_famt_id " _
                        & "  where pl_fa_depr = 'Y' and ass_class = 'A' and ass_qty_del = 0 " _
                        & "  and ass_service_date <= " + SetDate(date_of_depr.DateTime) _
                        & "  and ass_confirm ~~* 'y' " _
                        & "  and ass_service_cost > ass_depr_acum " _
                        & "  and to_date(('01'|| '/' ||  cast(coalesce(assbk_per_month,0) as text) || '/' || cast(coalesce(assbk_per_year,0) as text)),'dd/MM/yyyy') < '" + _date_of_depr + "' " _
                        & "  order by pt_code,ass_code asc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                    '& "  and ass_code in ('A-11-02-A21-0001','A-11-02-A21-0002') " _
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Rev By Hendrik 2011-02-02
        For Each _dr As DataRow In ds_bantu.Tables("bantu").Rows
            _part_number = _dr("pt_code")
            _ass_id = _dr("ass_id")
            If _dr("famt_method") = "T" Then
                view_custom_table(_dr("ass_id"), _dr("famt_oid"), _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                      _dr("ass_service_cost"), _dr("ass_salvage_cost"), _dr("ass_depr_acum"))
            ElseIf _dr("famt_method") = "D" Then
                view_double_declining(_dr("ass_id"), _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                       _dr("ass_service_cost"), _dr("ass_salvage_cost"), _dr("ass_depr_acum"))
            ElseIf _dr("famt_method") = "B" Then
                view_double_declining2(_dr("ass_id"), _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                       _dr("ass_service_cost"), _dr("ass_salvage_cost"), _dr("ass_depr_acum"))
            ElseIf _dr("famt_method") = "S" Then
                view_stright_line(_dr("ass_id"), _dr("ass_service_date"), _dr("assbk_exp_life"), _
                                      _dr("ass_service_cost"), _dr("ass_salvage_cost"), _dr("ass_depr_acum"))
            End If
        Next

        'Rev By Hendrik 2011-02-02
        ds_select = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "temp.ass_id, " _
                            & "tahun, " _
                            & "bulan, " _
                            & "tanggal, " _
                            & "depr_per_month, " _
                            & "depr_acumulasi, " _
                            & "usr_id, " _
                            & "ass_oid,false as ceklist, " _
                            & "ass_dom_id,   " _
                            & "ass_en_id,   " _
                            & "ass_add_by,   " _
                            & "ass_add_date,   " _
                            & "ass_upd_by,   " _
                            & "ass_upd_date,   " _
                            & "ass.ass_id as ass_id,   " _
                            & "ass_pt_id, pt_code,  " _
                            & "ass_code,   " _
                            & "ass_barcode,   " _
                            & "ass_desc,   " _
                            & "ass_class,   " _
                            & "ass_ref_req,   " _
                            & "ass_ref_po,   " _
                            & "ass_ref_rcpt,   " _
                            & "ass_ref_rcpt_oid,   " _
                            & "ass_ref_inv,   " _
                            & "ass_model,   " _
                            & "ass_qty,   " _
                            & "ass_qty_assgn,   " _
                            & "ass_qty_del,   " _
                            & "ass_sn,   " _
                            & "ass_service_date,   " _
                            & "ass_gar_date,   " _
                            & "ass_line,  " _
                            & "ass_ptnr_id,   " _
                            & "ass_st_purc,   " _
                            & "ass_lic_type,   " _
                            & "ass_service_cost,  " _
                            & "ass_emp_id,  " _
                            & "ass_emp_dept,  " _
                            & "ass_emp_rg,   " _
                            & "ass_confirm,  " _
                            & "ass_its_id,   " _
                            & "ass_dt,   " _
                            & "ass_salvage_cost,  " _
                            & "ass_basis_cost,   " _
                            & "ass_depr_acum,   " _
                            & "ass_depr_date,   " _
                            & "ass_disp_amount,   " _
                            & "ass_disp_date,pl_fa_depr,famt_oid,assbk_oid,assbk_exp_life,   " _
                            & "coalesce(assbk_per_year,0) as assbk_per_year,coalesce(assbk_per_month,0) as assbk_per_month,   " _
                            & "famt_method   " _
                            & "FROM  " _
                            & "  public.temp_ass_trans_post temp " _
                            & "  inner join ass_mstr ass on ass.ass_id =  temp.ass_id " _
                            & "  inner join pt_mstr on pt_id = ass.ass_pt_id " _
                            & "  inner join pl_mstr on pl_id = pt_pl_id " _
                            & "  inner join assbk_mstr on assbk_ass_oid = ass.ass_oid " _
                            & "  inner join fabk_mstr on fabk_id = assbk_fabk_id and fabk_posted = 'Y' " _
                            & "  inner join famt_mstr on famt_id = assbk_famt_id " _
                            & "  where pl_fa_depr = 'Y' and ass_class = 'A'  " _
                            & "  and ass_qty_del = 0 " _
                            & "  and usr_id = " + master_new.ClsVar.sUserID.ToString _
                            & "  order by pt_code,ass_code asc "
                    .InitializeCommand()
                    .FillDataSet(ds_select, "select")
                    gc_master.DataSource = ds_select.Tables(0)
                    gv_master.BestFitColumns()
                    '& "  and (tahun = " + SetInteger(Year(date_of_depr.DateTime.Date)) _
                    '        & "  and bulan = " + SetInteger(Month(date_of_depr.DateTime.Date)) + ") " _
                    '        & "  and to_date(('01'|| '/' ||  cast(coalesce(assbk_per_month,0) as text) || '/' || cast(coalesce(assbk_per_year,0) as text)),'DD/MM/yyyy') < '" + _date_of_depr + "' " _
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Rev By Hendrik 2011-02-02
    Private Sub SelectAfterPosted()
        ds_select = New DataSet
        Dim _month_of_depr, _yer_of_depr As String
        Dim _date_of_depr As Date
        _month_of_depr = Month(date_of_depr.EditValue).ToString
        _yer_of_depr = Year(date_of_depr.EditValue).ToString
        _date_of_depr = CDate("01/" + _month_of_depr + "/" + _yer_of_depr)

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "temp.ass_id, " _
                            & "tahun, " _
                            & "bulan, " _
                            & "tanggal, " _
                            & "depr_per_month, " _
                            & "depr_acumulasi, " _
                            & "usr_id, " _
                            & "ass_oid,false as ceklist, " _
                            & "ass_dom_id,   " _
                            & "ass_en_id,   " _
                            & "ass_add_by,   " _
                            & "ass_add_date,   " _
                            & "ass_upd_by,   " _
                            & "ass_upd_date,   " _
                            & "ass.ass_id as ass_id,   " _
                            & "ass_pt_id, pt_code,  " _
                            & "ass_code,   " _
                            & "ass_barcode,   " _
                            & "ass_desc,   " _
                            & "ass_class,   " _
                            & "ass_ref_req,   " _
                            & "ass_ref_po,   " _
                            & "ass_ref_rcpt,   " _
                            & "ass_ref_rcpt_oid,   " _
                            & "ass_ref_inv,   " _
                            & "ass_model,   " _
                            & "ass_qty,   " _
                            & "ass_qty_assgn,   " _
                            & "ass_qty_del,   " _
                            & "ass_sn,   " _
                            & "ass_service_date,   " _
                            & "ass_gar_date,   " _
                            & "ass_line,  " _
                            & "ass_ptnr_id,   " _
                            & "ass_st_purc,   " _
                            & "ass_lic_type,   " _
                            & "ass_service_cost,  " _
                            & "ass_emp_id,  " _
                            & "ass_emp_dept,  " _
                            & "ass_emp_rg,   " _
                            & "ass_confirm,  " _
                            & "ass_its_id,   " _
                            & "ass_dt,   " _
                            & "ass_salvage_cost,  " _
                            & "ass_basis_cost,   " _
                            & "ass_depr_acum,   " _
                            & "ass_depr_date,   " _
                            & "ass_disp_amount,   " _
                            & "ass_disp_date,pl_fa_depr,famt_oid,assbk_oid,assbk_exp_life,   " _
                            & "coalesce(assbk_per_year,0) as assbk_per_year,coalesce(assbk_per_month,0) as assbk_per_month,   " _
                            & "famt_method   " _
                            & "FROM  " _
                            & "  public.temp_ass_trans_post temp " _
                            & "  inner join ass_mstr ass on ass.ass_id =  temp.ass_id " _
                            & "  inner join pt_mstr on pt_id = ass.ass_pt_id " _
                            & "  inner join pl_mstr on pl_id = pt_pl_id " _
                            & "  inner join assbk_mstr on assbk_ass_oid = ass.ass_oid " _
                            & "  inner join fabk_mstr on fabk_id = assbk_fabk_id and fabk_posted = 'Y' " _
                            & "  inner join famt_mstr on famt_id = assbk_famt_id " _
                            & "  where pl_fa_depr = 'Y' and ass_class = 'A' and ass_qty_del = 0 " _
                            & "  and ass_service_date <= " + SetDate(date_of_depr.DateTime) _
                            & "  and to_date(('01'|| '/' ||  cast(coalesce(assbk_per_month,0) as text) || '/' || cast(coalesce(assbk_per_year,0) as text)),'dd/MM/yyyy') < '" + _date_of_depr + "' " _
                            & "  and (tahun = " + SetInteger(Year(date_of_depr.DateTime.Date)) _
                            & "  and bulan = " + SetInteger(Month(date_of_depr.DateTime.Date)) + ") " _
                            & "  order by pt_code,ass_code asc "
                    .InitializeCommand()
                    .FillDataSet(ds_select, "select")
                    gc_master.DataSource = ds_select.Tables(0)
                    gv_master.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Rev By Hendrik 2011-02-02
    Public Sub view_custom_table(ByVal par_ass_id As Integer, ByVal par_famt_oid As String, _
                                     ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                     ByVal par_cost As Double, _
                                     ByVal par_salv_cost As Double, ByVal par_last_depr_acum As Double)

        Dim _end_date_life As Date
        _end_date_life = par_start_date.AddYears(par_exp_life)


        Dim ds_bantu As New DataSet
        Dim _percent_depr As Double
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
                            & "  where famt_oid = " + SetSetring(par_famt_oid.ToString()) _
                            & "  and famtd_year = " + Year(date_of_depr.EditValue)
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "req")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim i As Integer = 0
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If Month(date_of_depr.EditValue) = 1 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_1")
            ElseIf Month(date_of_depr.EditValue) = 2 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_2")
            ElseIf Month(date_of_depr.EditValue) = 3 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_3")
            ElseIf Month(date_of_depr.EditValue) = 4 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_4")
            ElseIf Month(date_of_depr.EditValue) = 5 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_5")
            ElseIf Month(date_of_depr.EditValue) = 6 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_6")
            ElseIf Month(date_of_depr.EditValue) = 7 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_7")
            ElseIf Month(date_of_depr.EditValue) = 8 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_8")
            ElseIf Month(date_of_depr.EditValue) = 9 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_9")
            ElseIf Month(date_of_depr.EditValue) = 10 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_10")
            ElseIf Month(date_of_depr.EditValue) = 11 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_11")
            ElseIf Month(date_of_depr.EditValue) = 12 Then
                _percent_depr = ds_bantu.Tables(0).Rows(i).Item("famtd_per_12")
            End If
        Next

        Dim _tahun, _bulan As Integer
        Dim _nilai_asset, _depr_per_month, _depr_acum As Double
        _tahun = SetInteger(Year(date_of_depr.EditValue))
        _bulan = SetInteger(Month(date_of_depr.EditValue))
        _nilai_asset = par_cost - par_salv_cost
        _depr_per_month = _nilai_asset * (_percent_depr)
        _depr_acum = par_last_depr_acum + _depr_per_month

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.StoredProcedure
                        .Command.CommandText = "temp_ass_trans_post_insert"
                        .AddParameter("arg_ass_id", par_ass_id)
                        .AddParameter("arg_tahun", _tahun)
                        .AddParameter("arg_bulan", _bulan)
                        .AddParameter("arg_tanggal", date_of_depr.EditValue)
                        .AddParameter("arg_depr_per_month", _depr_per_month)
                        .AddParameter("arg_depr_acumulasi", _depr_acum)
                        .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
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

        'Dim _cost_after_salv, _nilai_buku, _depr_acum As Double
        '_cost_after_salv = par_cost - par_salv_cost
        '_nilai_buku = _cost_after_salv

        'Dim _start_month As Integer
        '_start_month = par_month

        'Dim _bulan_ke As Integer = 0
        'Dim _end_date_life As Date
        'Dim _end_month_life As Integer
        '_end_date_life = par_start_date.AddYears(par_exp_life)

        'Dim _bulan_date As Date
        'Dim _tahun, _bulan As Integer
        '_tahun = Year(par_start_date)
        '_bulan_date = par_start_date

        'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
        '    _tahun = Year(par_start_date.AddYears(i))

        '    Dim _per_1, _per_2, _per_3, _per_4, _per_5, _per_6, _per_7, _per_8, _per_9, _per_10, _per_11, _per_12 As Double
        '    _per_1 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_1")
        '    _per_2 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_2")
        '    _per_3 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_3")
        '    _per_4 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_4")
        '    _per_5 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_5")
        '    _per_6 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_6")
        '    _per_7 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_7")
        '    _per_8 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_8")
        '    _per_9 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_9")
        '    _per_10 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_10")
        '    _per_11 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_11")
        '    _per_12 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_12")

        '    If i = 0 Then
        '        _start_month = _start_month
        '    Else
        '        _start_month = 1
        '    End If

        '    If i = par_exp_life Then
        '        _end_month_life = Month(_end_date_life) - 1
        '    Else
        '        _end_month_life = 12
        '    End If

        '    _bulan = Month(_bulan_date)
        '    Try
        '        Using objinsert As New master_new.WDABasepgsql("", "")
        '            With objinsert
        '                .Connection.Open()
        '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
        '                Try
        '                    .Command = .Connection.CreateCommand
        '                    .Command.Transaction = sqlTran

        '                    Dim a As Integer
        '                    For a = _start_month To _end_month_life
        '                        .Command.CommandType = CommandType.StoredProcedure
        '                        .Command.CommandText = "temp_ass_trans_post_insert"
        '                        .AddParameter("arg_ass_id", par_ass_id)
        '                        .AddParameter("arg_tahun", _tahun)
        '                        .AddParameter("arg_bulan", _bulan)
        '                        .AddParameter("arg_tanggal", _bulan_date)
        '                        If a = 1 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_1))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_1))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_1))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 2 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_2))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_2))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_2))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 3 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_3))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_3))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_3))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 4 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_4))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_4))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_4))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 5 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_5))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_5))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_5))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 6 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_6))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_6))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_6))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 7 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_7))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_7))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_7))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 8 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_8))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_8))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_8))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 9 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_9))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_9))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_9))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 10 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_10))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_10))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_10))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 11 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_11))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_11))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_11))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 12 Then
        '                            _depr_acum = _depr_acum + (_cost_after_salv * (_per_12))
        '                            _nilai_buku = _nilai_buku - (_cost_after_salv * (_per_12))
        '                            If _nilai_buku < 0 Then
        '                                _nilai_buku = 0
        '                            End If
        '                            .AddParameter("arg_depr_per_month", _cost_after_salv * (_per_12))
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        End If

        '                        If _nilai_buku <= 0 Then
        '                            sqlTran.Commit()
        '                            Exit Sub
        '                        End If

        '                        _bulan_date = _bulan_date.AddMonths(1)
        '                        _bulan = Month(_bulan_date)
        '                    Next

        '                    sqlTran.Commit()
        '                Catch ex As Exception
        '                    sqlTran.Rollback()
        '                    MessageBox.Show(ex.Message)
        '                End Try

        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'Next

    End Sub

    'Rev By Hendrik 2011-02-02
    Public Sub view_double_declining(ByVal par_ass_id As Integer, _
                                     ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                     ByVal par_cost As Double, _
                                     ByVal par_salv_cost As Double, ByVal par_last_depr_acum As Double)

        Dim _end_date_life As Date
        _end_date_life = par_start_date.AddYears(par_exp_life)

        Dim _tahun, _bulan As Integer
        Dim _depr_per_month, _depr_acum As Double
        _tahun = SetInteger(Year(date_of_depr.EditValue))
        _bulan = SetInteger(Month(date_of_depr.EditValue))
        _depr_per_month = ((par_cost - par_salv_cost) * 0.5) / 12
        _depr_acum = par_last_depr_acum + _depr_per_month

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.StoredProcedure
                        .Command.CommandText = "temp_ass_trans_post_insert"
                        .AddParameter("arg_ass_id", par_ass_id)
                        .AddParameter("arg_tahun", _tahun)
                        .AddParameter("arg_bulan", _bulan)
                        .AddParameter("arg_tanggal", date_of_depr.EditValue)
                        .AddParameter("arg_depr_per_month", _depr_per_month)
                        .AddParameter("arg_depr_acumulasi", _depr_acum)
                        .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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


        'Dim i As Integer
        'Dim _cost_after_salv, _depr_per_month, _depr_acum, _nilai_buku As Double
        '_cost_after_salv = par_cost - par_salv_cost
        '_nilai_buku = _cost_after_salv

        'Dim _start_month As Integer
        '_start_month = par_month

        'Dim _bulan_date As Date
        'Dim _bulan_ke As Integer = 0
        'Dim _end_date_life As Date
        'Dim _end_month_life As Integer
        '_end_date_life = par_start_date.AddYears(par_exp_life)
        '_bulan_date = par_start_date

        'Dim _tahun, _bulan As Integer
        'For i = 0 To par_exp_life
        '    _tahun = Year(par_start_date.AddYears(i))
        '    _depr_per_month = (_nilai_buku * 0.5) / 12

        '    If i = 0 Then
        '        _start_month = _start_month
        '    Else
        '        _start_month = 1
        '    End If

        '    If i = par_exp_life Then
        '        _end_month_life = Month(_end_date_life) - 1
        '        _depr_per_month = _nilai_buku / _end_month_life
        '    Else
        '        _end_month_life = 12
        '    End If

        '    _bulan = Month(_bulan_date)
        '    Try
        '        Using objinsert As New master_new.WDABasepgsql("", "")
        '            With objinsert
        '                .Connection.Open()
        '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
        '                Try
        '                    .Command = .Connection.CreateCommand
        '                    .Command.Transaction = sqlTran

        '                    Dim a As Integer
        '                    For a = _start_month To _end_month_life
        '                        .Command.CommandType = CommandType.StoredProcedure
        '                        .Command.CommandText = "temp_ass_trans_post_insert"
        '                        .AddParameter("arg_ass_id", par_ass_id)
        '                        .AddParameter("arg_tahun", _tahun)
        '                        .AddParameter("arg_bulan", _bulan)
        '                        .AddParameter("arg_tanggal", _bulan_date)
        '                        If a = 1 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 2 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 3 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 4 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 5 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 6 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 7 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 8 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 9 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 10 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 11 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 12 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        End If

        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()

        '                        _bulan_date = _bulan_date.AddMonths(1)
        '                        _bulan = Month(_bulan_date)

        '                        'rev By Hendrik 2011-02-02
        '                        If a = SetInteger(Month(Today())) And i = SetInteger(Year(Today())) Then
        '                            sqlTran.Commit()
        '                            Exit Sub
        '                        End If

        '                    Next

        '                    sqlTran.Commit()
        '                Catch ex As Exception
        '                    sqlTran.Rollback()
        '                    MessageBox.Show(ex.Message)
        '                End Try

        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'Next

    End Sub

    'Rev By Hendrik 2011-02-02
    Public Sub view_double_declining2(ByVal par_ass_id As Integer, _
                                     ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                     ByVal par_cost As Double, _
                                     ByVal par_salv_cost As Double, ByVal par_last_depr_acum As Double)

        Dim _end_date_life As Date
        _end_date_life = par_start_date.AddYears(par_exp_life)

        Dim _tarif As Double = 0
        Dim _depr_per_month, _depr_acum, _nilai_asset, _nilai_buku As Double
        Dim _tahun, _bulan As Integer

        _tahun = SetInteger(Year(date_of_depr.EditValue))
        _bulan = SetInteger(Month(date_of_depr.EditValue))

        _nilai_asset = par_cost - par_salv_cost
        _tarif = ((100 / 100) / par_exp_life) * 2
        _nilai_buku = _nilai_asset - par_last_depr_acum
        _depr_per_month = (_nilai_buku * _tarif) / 12
        _depr_acum = par_last_depr_acum + _depr_per_month

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.StoredProcedure
                        .Command.CommandText = "temp_ass_trans_post_insert"
                        .AddParameter("arg_ass_id", par_ass_id)
                        .AddParameter("arg_tahun", _tahun)
                        .AddParameter("arg_bulan", _bulan)
                        .AddParameter("arg_tanggal", date_of_depr.EditValue)
                        .AddParameter("arg_depr_per_month", _depr_per_month)
                        .AddParameter("arg_depr_acumulasi", _depr_acum)
                        .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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

        'Dim i As Integer
        'Dim _cost_after_salv, _depr_per_month, _depr_acum, _nilai_buku As Double
        '_cost_after_salv = par_cost - par_salv_cost
        '_nilai_buku = _cost_after_salv

        'Dim _start_month As Integer
        '_start_month = par_month

        'Dim _bulan_ke As Integer = 0
        'Dim _end_date_life As Date
        'Dim _end_month_life As Integer
        '_end_date_life = par_start_date.AddYears(par_exp_life)

        'Dim _tarif As Double = 0
        '_tarif = ((100 / 100) / par_exp_life) * 2

        'Dim _bulan_date As Date
        'Dim _tahun, _bulan As Integer

        '_bulan_date = par_start_date
        '_tahun = Year(_bulan_date)

        'For i = 0 To par_exp_life
        '    If i = 0 Then
        '        _start_month = _start_month
        '    Else
        '        _start_month = 1
        '    End If

        '    If i = par_exp_life Then
        '        _end_month_life = Month(_end_date_life) - 1
        '    Else
        '        _end_month_life = 12
        '    End If

        '    _tahun = Year(_bulan_date)
        '    _bulan = Month(_bulan_date)
        '    Try
        '        Using objinsert As New master_new.WDABasepgsql("", "")
        '            With objinsert
        '                .Connection.Open()
        '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
        '                Try
        '                    .Command = .Connection.CreateCommand
        '                    .Command.Transaction = sqlTran

        '                    _bulan_ke = 0
        '                    Dim a As Integer
        '                    For a = _start_month To _end_month_life
        '                        _depr_per_month = (_nilai_buku * _tarif) / 12
        '                        .Command.CommandType = CommandType.StoredProcedure
        '                        .Command.CommandText = "temp_ass_trans_post_insert"
        '                        .AddParameter("arg_ass_id", par_ass_id)
        '                        .AddParameter("arg_tahun", _tahun)
        '                        .AddParameter("arg_bulan", _bulan)
        '                        .AddParameter("arg_tanggal", _bulan_date)
        '                        If a = 1 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 2 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 3 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 4 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 5 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 6 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 7 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 8 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 9 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 10 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 11 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 12 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        End If
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()

        '                        _bulan_date = _bulan_date.AddMonths(1)
        '                        _bulan = Month(_bulan_date)
        '                    Next

        '                    sqlTran.Commit()
        '                Catch ex As Exception
        '                    sqlTran.Rollback()
        '                    MessageBox.Show(ex.Message)
        '                End Try

        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'Next

    End Sub

    'Rev By Hendrik 2011-02-02
    Public Sub view_stright_line(ByVal par_ass_id As Integer, _
                                 ByVal par_start_date As Date, ByVal par_exp_life As Integer, _
                                 ByVal par_cost As Double, _
                                 ByVal par_salv_cost As Double, ByVal par_last_depr_acum As Double)

        Dim _end_date_life As Date
        _end_date_life = par_start_date.AddYears(par_exp_life)

        Dim _depr_per_month, _depr_acum, _nilai_asset As Double
        Dim _tahun, _bulan As Integer
        _tahun = SetInteger(Year(date_of_depr.EditValue))
        _bulan = SetInteger(Month(date_of_depr.EditValue))
        _nilai_asset = par_cost - par_salv_cost
        _depr_per_month = _nilai_asset / (par_exp_life * 12)
        _depr_acum = par_last_depr_acum + _depr_per_month

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.StoredProcedure
                        .Command.CommandText = "temp_ass_trans_post_insert"
                        .AddParameter("arg_ass_id", par_ass_id)
                        .AddParameter("arg_tahun", _tahun)
                        .AddParameter("arg_bulan", _bulan)
                        .AddParameter("arg_tanggal", date_of_depr.EditValue)
                        .AddParameter("arg_depr_per_month", _depr_per_month)
                        .AddParameter("arg_depr_acumulasi", _depr_acum)
                        .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)

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

        'Dim ds_bantu As New DataSet
        'Dim i As Integer
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

        'Dim _cost_after_salv, _depr_per_month, _depr_acum, _nilai_buku As Double
        '_cost_after_salv = par_cost - par_salv_cost
        '_nilai_buku = _cost_after_salv

        'Dim _start_month As Integer
        '_start_month = par_month

        'Dim _bulan_date As Date
        'Dim _bulan_ke As Integer = 0
        'Dim _end_date_life As Date
        'Dim _end_month_life As Integer
        '_end_date_life = par_start_date.AddYears(par_exp_life)
        '_depr_per_month = _cost_after_salv / (par_exp_life * 12)
        '_bulan_date = par_start_date

        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran

        '                Dim _tahun, _bulan As Integer
        '                _tahun = Year(par_start_date)

        '                For i = 0 To par_exp_life
        '                    _tahun = Year(_bulan_date.AddYears(i))

        '                    If i = 0 Then
        '                        _start_month = _start_month
        '                    Else
        '                        _start_month = 1
        '                    End If

        '                    If i = par_exp_life Then
        '                        _end_month_life = Month(_end_date_life) - 1
        '                    Else
        '                        _end_month_life = 12
        '                    End If

        '                    _bulan = Month(_bulan_date)
        '                    Dim a As Integer
        '                    For a = _start_month To _end_month_life
        '                        .Command.CommandType = CommandType.StoredProcedure
        '                        .Command.CommandText = "temp_ass_trans_post_insert"
        '                        .AddParameter("arg_ass_id", par_ass_id)
        '                        .AddParameter("arg_tahun", _tahun)
        '                        .AddParameter("arg_bulan", _bulan)
        '                        .AddParameter("arg_tanggal", _bulan_date)
        '                        If a = 1 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 2 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 3 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 4 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 5 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 6 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 7 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 8 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 9 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 10 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 11 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        ElseIf a = 12 Then
        '                            _depr_acum = _depr_acum + _depr_per_month
        '                            _nilai_buku = _nilai_buku - _depr_per_month
        '                            .AddParameter("arg_depr_per_month", _depr_per_month)
        '                            .AddParameter("arg_depr_acumulasi", _depr_acum)
        '                            .AddParameter("arg_usr_id", master_new.ClsVar.sUserID)
        '                        End If

        '                        _bulan_date = _bulan_date.AddMonths(1)
        '                        _bulan = Month(_bulan_date)
        '                    Next
        '                Next

        '                sqlTran.Commit()
        '            Catch ex As Exception
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try

        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Public Function UpdateDepresiasi(ByVal par_obj As Object, ByVal par_acum_depr As Double, ByVal par_date_of_depr As Date, _
                                     ByVal par_ass_oid As String, ByVal par_batas_tahun As Integer, ByVal par_batas_bulan As Integer, _
                                     ByVal par_assbk_oid As String) As Boolean

        UpdateDepresiasi = True
        Dim ssqls As New ArrayList

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.ass_mstr   " _
                                    & "SET  " _
                                    & "  ass_depr_acum = " & SetDbl(par_acum_depr) & ",  " _
                                    & "  ass_depr_date = " & SetDate(par_date_of_depr) & "  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  ass_oid = " & SetSetring(par_ass_oid) & "  " _
                                    & ";"
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.assbk_mstr   " _
                                    & "SET  " _
                                    & "  assbk_depr_acum = " & SetDbl(par_acum_depr) & ",  " _
                                    & "  assbk_per_year = " & SetInteger(par_batas_tahun) & ",  " _
                                    & "  assbk_per_month = " & SetInteger(par_batas_bulan) & "  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  assbk_oid = " & SetSetring(par_assbk_oid) & "  " _
                                    & ";"
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Private Sub btn_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_update.Click
        If MessageBox.Show("Yakin Data Akan Di Depresiasikan,,?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        Dim _batas_tahun, _batas_bulan As Integer
                        _batas_tahun = Year(date_of_depr.EditValue)
                        _batas_bulan = Month(date_of_depr.EditValue)

                        For Each _dr As DataRow In ds_select.Tables("select").Rows
                            If _dr("ceklist") = True Then
                                Dim _ass_oid As String = ""
                                Dim _assbk_oid As String = ""
                                Dim _amount_depr As Double = 0
                                _amount_depr = _dr("depr_acumulasi")
                                _ass_oid = _dr("ass_oid").ToString
                                _assbk_oid = _dr("assbk_oid").ToString

                                If UpdateDepresiasi(objinsert, _amount_depr, date_of_depr.EditValue, _ass_oid.ToString(), _batas_tahun, _
                                                        _batas_bulan, _assbk_oid.ToString()) = False Then
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If
                            End If
                        Next

                        sqlTran.Commit()
                        MessageBox.Show("Update Depresiasi Succesfull,,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        SelectAfterPosted()
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

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("This Form Can't To Insert Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("This Form Can't To Edit Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("This Form Can't To Delete Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function


    Private Sub ce_select_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_select.EditValueChanged
        If ce_select.Checked = True Then
            For Each _dr As DataRow In ds_select.Tables(0).Rows
                _dr("ceklist") = True
            Next
        Else
            For Each _dr As DataRow In ds_select.Tables(0).Rows
                _dr("ceklist") = False
            Next
        End If
    End Sub
End Class
