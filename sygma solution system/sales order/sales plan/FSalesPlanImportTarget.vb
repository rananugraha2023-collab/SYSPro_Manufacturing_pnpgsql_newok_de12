Imports Npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraGrid.Views.Base

Public Class FSalesPlanImportTarget
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim _plans_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList
    Public _geninstf_fsn_codes As String
    Dim _now As DateTime

    Private Property inv_fsn_end_date As Object

    Private Sub FSalesPlanImportTargetBck_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        plans_date.EditValue = DateTime.Now
    End Sub

    Public Overrides Sub load_cb()
        init_le(plans_en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_years_mstr(plans_en_id.EditValue))
        plans_qrtr_years.Properties.DataSource = dt_bantu
        plans_qrtr_years.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        plans_qrtr_years.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        plans_qrtr_years.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_qrtr_code_id())
        plans_qrtr_code_id.Properties.DataSource = dt_bantu
        plans_qrtr_code_id.Properties.DisplayMember = dt_bantu.Columns("qrtr_code_name").ToString
        plans_qrtr_code_id.Properties.ValueMember = dt_bantu.Columns("qrtr_code_id").ToString
        plans_qrtr_code_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "plans_oid", "plans_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "plans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Date", "plans_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Years", "plans_qrtr_years", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "plans_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Min Target", "plans_amount_tgt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Standar Insentif", "plans_insentif", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Amount Total", "plans_amount_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "User Create", "plans_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "plans_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "plans_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "plans_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail, "Year", "planstgtd_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Month", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Start", "planstgtd_qrtr_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail, "End", "planstgtd_qrtr_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail, "Personal", "planstgtd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_detail, "Netto", "planstgtd_netto_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_detail, "is Special", "planstgtd_spc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "planstgtd_oid", False)
        add_column(gv_edit, "planstgtd_plans_oid", False)
        add_column(gv_edit, "planstgtd_qrtr_id", False)
        add_column(gv_edit, "Year", "planstgtd_year", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Month", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Start", "planstgtd_qrtr_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "End", "planstgtd_qrtr_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_edit(gv_edit, "Personal", "planstgtd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column(gv_edit, "Netto", "planstgtd_netto_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Special", "planstgtd_spc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "planstgtd_parent", False)

        add_column_copy(gv_edit_cust, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_cust, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_cust, "Amount/Qty", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit_cust, "plansd_ptnr_id", False)
        add_column(gv_edit_cust, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_cust, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cust, "Amount/Qty", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                        & "  a.plans_oid, " _
                        & "  a.plans_en_id, " _
                        & "  b.en_desc, " _
                        & "  a.plans_code, " _
                        & "  a.plans_date, " _
                        & "  a.plans_sales_id, " _
                        & "  c.ptnr_code, " _
                        & "  c.ptnr_name, " _
                        & "  a.plans_qrtr_years, " _
                        & "  a.plans_amount_tgt, " _
                        & "  a.plans_insentif, " _
                        & "  a.plans_amount_total, " _
                        & "  a.plans_remarks, " _
                        & "  a.plans_add_by, " _
                        & "  a.plans_add_date, " _
                        & "  a.plans_upd_by, " _
                        & "  a.plans_upd_date, " _
                        & "  a.plans_emp_nik, " _
                        & "  a.plans_emp_pos_id, " _
                        & "  e.emp_pos_name, " _
                        & "  a.plans_emp_cat_id, " _
                        & "  f.emp_cat_name, " _
                        & "  a.plans_emp_lvl_id, " _
                        & "  g.emp_lvl_name, " _
                        & "  a.plans_emp_parent_id, " _
                        & "  parent_id.ptnr_name as sales_parent, " _
                        & "  a.plans_qrtr_code_id, " _
                        & "  a.plans_periode, " _
                        & "  a.plans_ce_final, " _
                        & "  a.plans_ce_special, " _
                        & "  a.plans_end_dt, " _
                        & "  a.plans_start_dt " _
                        & "FROM public.plans_mstr a " _
                        & "  INNER JOIN public.en_mstr b ON (a.plans_en_id = b.en_id) " _
                        & "  LEFT OUTER JOIN public.ptnr_mstr c ON (a.plans_sales_id = c.ptnr_id) " _
                        & "  LEFT OUTER JOIN public.psperiode_mstr d ON (a.plans_periode = d.periode_code) " _
                        & "  INNER JOIN public.sls_emp_pos e ON (a.plans_emp_pos_id = e.emp_pos_id) " _
                        & "  INNER JOIN public.sls_emp_cat f ON (a.plans_emp_cat_id = f.emp_cat_id) " _
                        & "  INNER JOIN public.sls_emp_lvl g ON (a.plans_emp_lvl_id = g.emp_lvl_id) " _
                        & "  LEFT OUTER JOIN ptnr_mstr parent_id on parent_id.ptnr_id = plans_emp_parent_id " _
                        & "WHERE " _
                        & " a.plans_en_id in (select user_en_id from tconfuserentity " _
                        & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") "

        get_sequel += "   ORDER BY " _
                & "  a.plans_code"

        Return get_sequel
    End Function

    'Public Overrides Sub load_data_grid_detail()

    '    If ds.Tables.Count = 0 Then
    '        Exit Sub
    '    End If

    '    If ds.Tables(0).Rows.Count = 0 Then
    '        Exit Sub
    '    End If


    '    Dim sql As String

    '    Try
    '        ds.Tables("detail").Clear()
    '    Catch ex As Exception
    '    End Try

    '    sql = "SELECT a.planstgtd_oid, " _
    '                    & "  a.planstgtd_plans_oid, " _
    '                    & "  a.planstgtd_seq, " _
    '                    & "  a.planstgtd_qrtr_id, " _
    '                    & "  b.qrtr_name, " _
    '                    & "  a.planstgtd_amount, " _
    '                    & "  a.planstgtd_spc " _
    '                    & "FROM public.planstgtd_det a " _
    '                    & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
    '                    & "WHERE " _
    '                    & "  a.planstgtd_plans_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
    '                    & "ORDER BY " _
    '                    & "  a.planstgtd_seq"

    '    load_data_detail(sql, gc_detail, "detail")
    '    gv_detail.BestFitColumns()
    'End Sub

    Public Overrides Sub relation_detail()
        Try
            'load_data_grid_detail()
            'gv_detail.Columns("rod_ro_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rod_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        plans_en_id.EditValue = ""
        plans_sales_id.Tag = ""
        plans_sales_id.EditValue = ""

        plans_date.DateTime = CekTanggal()

        plans_remarks.EditValue = ""
        plans_en_id.Focus()
        'plans_qrtr_id.ItemIndex = 0

        'plans_year.DateTime = Now
        'plans_year.Enabled = True

        plans_ce_special.EditValue = False
        plans_ce_final.EditValue = False

        'plans_periode.Text = ""
        '_plans_periodes = ""

        Dim ssql As String
        ssql = "select ptnr_id,ptnr_is_bm,ptnr_name from ptnr_mstr where ptnr_user_name = (select usernama from tconfuser where userid= " _
                                           & master_new.ClsVar.sUserID & ")"
        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)

        Dim _ptnr_id, _ptnr_is_bm, _ptnr_name As String
        _ptnr_id = ""
        _ptnr_is_bm = ""
        _ptnr_name = ""
        For Each dr As DataRow In dt.Rows
            _ptnr_id = dr(0).ToString
            _ptnr_is_bm = SetString(dr(1))
            _ptnr_name = SetString(dr(2))
        Next

        plans_emp_nik.Tag = ""
        plans_emp_nik.Text = ""

        plans_emp_pos_id.Tag = ""
        plans_emp_pos_id.Text = ""

        plans_emp_cat_id.Tag = ""
        plans_emp_cat_id.Text = ""

        plans_emp_lvl_id.Tag = ""
        plans_emp_lvl_id.Text = ""

        plans_amount_tgt.Text = ""
        plans_insentif.Text = ""

        plans_qrtr_years.EditValue = ""
        plans_qrtr_years.Text = ""
        plans_qrtr_years.Tag = ""

        plans_emp_parent_id.Text = ""
        plans_emp_parent_id.Tag = ""

        'plans_amount_tgt.Text = ""
        'plans_insentif.Text = ""
        plans_amount_total.Text = ""
        plans_remarks.Text = ""
        'plans_qrtr_periode_id.Tag = ""
        'plans_qrtr_periode_id.Text = ""

        'Try
        '    tcg_header.SelectedTabPageIndex = 0
        'Catch ex As Exception
        'End Try
    End Sub
    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                                    & "  a.planstgtd_oid, " _
                                    & "  a.planstgtd_plans_oid, " _
                                    & "  a.planstgtd_seq, " _
                                    & "  a.planstgtd_year, " _
                                    & "  a.planstgtd_qrtr_id, " _
                                    & "  b.qrtr_name, " _
                                    & "  a.planstgtd_qrtr_start_date, " _
                                    & "  a.planstgtd_qrtr_end_date, " _
                                    & "  a.planstgtd_amount, " _
                                    & "  a.planstgtd_netto_amount, " _
                                    & "  a.planstgtd_spc, " _
                                    & "  a.planstgtd_parent " _
                                    & "FROM " _
                                    & "  public.planstgtd_det a " _
                                    & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
                                    & "WHERE " _
                                    & "  a.planstgtd_plans_oid IS NULL"

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

    Public Overrides Function insert() As Boolean

        Dim i As Integer

        Dim _plans_oid As Guid
        _plans_oid = Guid.NewGuid
        Dim _total As Double

        sSQLs.Clear()

        Dim _code As String
        '_code = func_coll.get_transaction_number("PLA", plans_en_id.GetColumnValue("en_code"), "plans_mstr", "plans_code")

        _code = GetNewNumberYM("plans_mstr", "plans_code", 5, "PLA" & plans_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        'Dim _ro_id As Integer
        '_ro_id = SetInteger(func_coll.GetID("fcs_mstr", fcs_en_id.GetColumnValue("en_code"), "ro_id", "ro_en_id", fcs_en_id.EditValue.ToString))
        'Dim plans_qrtr_years As Integer = Convert.ToDateTime(plans_qrtr_years.EditValue).Year

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                        & "  public.plans_mstr " _
                                        & "( " _
                                        & "  plans_oid, " _
                                        & "  plans_dom_id, " _
                                        & "  plans_code, " _
                                        & "  plans_en_id, " _
                                        & "  plans_date, " _
                                        & "  plans_qrtr_years, " _
                                        & "  plans_sales_id, " _
                                        & "  plans_emp_nik, " _
                                        & "  plans_emp_pos_id, " _
                                        & "  plans_emp_cat_id, " _
                                        & "  plans_emp_lvl_id, " _
                                        & "  plans_emp_parent_id, " _
                                        & "  plans_amount_tgt, " _
                                        & "  plans_insentif, " _
                                        & "  plans_amount_total, " _
                                        & "  plans_remarks, " _
                                        & "  plans_tran_id, " _
                                        & "  plans_ce_special, " _
                                        & "  plans_ce_final, " _
                                        & "  plans_add_by, " _
                                        & "  plans_add_date " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_plans_oid) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetSetring(_code) & ",  " _
                                        & SetInteger(plans_en_id.EditValue) & ",  " _
                                        & SetDateNTime00(plans_date.EditValue) & ",  " _
                                        & SetInteger(plans_qrtr_years.Text) & ",  " _
                                        & SetInteger(plans_sales_id.Tag) & ",  " _
                                        & SetSetring(plans_emp_nik.Text) & ",  " _
                                        & SetInteger(plans_emp_pos_id.Tag) & ",  " _
                                        & SetInteger(plans_emp_cat_id.Tag) & ",  " _
                                        & SetInteger(plans_emp_lvl_id.Tag) & ",  " _
                                        & SetInteger(plans_emp_parent_id.Tag) & ",  " _
                                        & SetDec(plans_amount_tgt.EditValue) & ",  " _
                                        & SetDec(plans_insentif.EditValue) & ",  " _
                                        & SetDec(plans_amount_total.EditValue) & ",  " _
                                        & SetSetring(plans_remarks.EditValue) & ",  " _
                                        & SetSetring(plans_tran_id.EditValue) & ",  " _
                                        & SetBitYN(plans_ce_special.Checked) & ",  " _
                                        & SetBitYN(plans_ce_final.Checked) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                        & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.planstgtd_det " _
                                        & "( " _
                                        & "  planstgtd_oid, " _
                                        & "  planstgtd_plans_oid, " _
                                        & "  planstgtd_seq, " _
                                        & "  planstgtd_year, " _
                                        & "  planstgtd_qrtr_id, " _
                                        & "  planstgtd_qrtr_start_date, " _
                                        & "  planstgtd_qrtr_end_date, " _
                                        & "  planstgtd_amount, " _
                                        & "  planstgtd_netto_amount, " _
                                        & "  planstgtd_spc, " _
                                        & "  planstgtd_parent " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_plans_oid.ToString) & ",  " _
                                        & SetInteger(i) & ", " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_year")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_qrtr_id")) & ",  " _
                                        & SetDate(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_qrtr_start_date")) & ",  " _
                                        & SetDate(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_qrtr_end_date")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_amount")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_netto_amount")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_spc")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("planstgtd_parent")) & "  " _
                                        & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_plans_oid.ToString), "plans_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As NpgsqlException
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If SetString(plans_amount_total.EditValue) = "" Then
            Box("Total Cant Empty")
            Return False
            Exit Function
        End If
        '*********************
        'Cek UM
        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_wc_id")) = True Then
        '        MessageBox.Show("Workstation Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next
        '*********************

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_tool_code")) = True Then
        '        MessageBox.Show("Tool Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_ptnr_id")) = True Then
        '        MessageBox.Show("Partner Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_milestone")) = True Then
        '        MessageBox.Show("Milestone Can't Empty.. Fill with (Y/N)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        'MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Return False
        If MyBase.edit_data = True Then
            plans_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _plans_oid_mstr = .Item("plans_oid").ToString
                plans_en_id.EditValue = .Item("plans_en_id")
                plans_date.EditValue = .Item("plans_date")

                plans_qrtr_years.Tag = SetString(.Item("plans_qrtr_years"))
                plans_qrtr_years.Text = SetString(.Item("plans_qrtr_years"))

                '_plans_code = .Item("plans_oid.Tosting").ToString

                plans_sales_id.EditValue = .Item("plans_sales_id")

                plans_qrtr_years.EditValue = .Item("plans_qrtr_years")

                plans_sales_id.Tag = SetString(.Item("plans_sales_id"))
                plans_sales_id.Text = SetString(.Item("ptnr_name"))

                plans_emp_nik.Text = SetString(.Item("plans_emp_nik"))

                plans_emp_pos_id.Tag = SetString(.Item("plans_emp_pos_id"))
                plans_emp_pos_id.Text = SetString(.Item("emp_pos_name"))

                plans_emp_cat_id.Tag = SetString(.Item("plans_emp_cat_id"))
                plans_emp_cat_id.Text = SetString(.Item("emp_cat_name"))

                plans_emp_lvl_id.Tag = SetString(.Item("plans_emp_lvl_id"))
                plans_emp_lvl_id.Text = SetString(.Item("emp_lvl_name"))

                plans_emp_parent_id.Tag = SetString(.Item("plans_emp_parent_id"))
                plans_emp_parent_id.Text = SetString(.Item("sales_parent"))

                plans_amount_tgt.Text = SetString(.Item("plans_amount_tgt"))
                plans_insentif.Text = SetString(.Item("plans_insentif"))
                plans_amount_total.Text = SetString(.Item("plans_amount_total"))

                plans_qrtr_code_id.EditValue = .Item("plans_qrtr_code_id")

                'plans_ce_special.EditValue = SetBitYNB(.Item("plans_ce_special"))
                plans_ce_final.EditValue = SetBitYNB(.Item("plans_ce_final"))
                'plans_ce_special.EditValue = SetBitYNB(.Item("plans_ce_special"))
            End With

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                    & "  a.planstgtd_oid, " _
                                    & "  a.planstgtd_plans_oid, " _
                                    & "  a.planstgtd_seq, " _
                                    & "  a.planstgtd_year, " _
                                    & "  a.planstgtd_qrtr_id, " _
                                    & "  b.qrtr_name, " _
                                    & "  a.planstgtd_qrtr_start_date, " _
                                    & "  a.planstgtd_qrtr_end_date, " _
                                    & "  a.planstgtd_amount, " _
                                    & "  a.planstgtd_netto_amount, " _
                                    & "  a.planstgtd_netto_amount as netto_amount_old, " _
                                    & "  a.planstgtd_spc, " _
                                    & "  a.planstgtd_parent " _
                                    & "FROM " _
                                    & "  public.planstgtd_det a " _
                                    & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
                                    & "WHERE " _
                                    & "  a.planstgtd_plans_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                                    & "ORDER BY " _
                                    & "  a.planstgtd_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail_upd")

                        gc_edit.DataSource = ds_edit.Tables("detail_upd")
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
        Dim i As Integer
        Dim ds_bantu As New DataSet
        'Dim oldNettoAmount As Double
        Dim dt_pt As New DataTable
        Dim _difference As Double
        _difference = 0

        edit = True
        sSQLs.Clear()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                    & "  public.plans_mstr   " _
                                    & "SET  " _
                                    & "  plans_en_id = " & SetInteger(plans_en_id.EditValue) & ",  " _
                                    & "  plans_remarks = " & SetSetring(plans_remarks.EditValue) & ",  " _
                                    & "  plans_amount_total = " & SetDec(plans_amount_total.EditValue) & ",  " _
                                    & "  plans_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  plans_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  plans_qrtr_years = " & SetInteger(plans_qrtr_years.Tag) & ",  " _
                                    & "  plans_qrtr_code_id = " & SetInteger(plans_qrtr_code_id.Tag) & ",  " _
                                    & "  plans_ce_special = " & SetBitYN(plans_ce_special.EditValue) & ",  " _
                                    & "  plans_ce_final = " & SetBitYN(plans_ce_final.EditValue) & "  " _
                                    & "WHERE  " _
                                    & "  plans_oid = " & SetSetring(_plans_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from planstgtd_det where planstgtd_plans_oid = '" + _plans_oid_mstr + "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1

                            ' Ambil nilai netto baru dari dataset
                            Dim newNettoAmount As Double = SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_netto_amount"))

                            ' Ambil nilai netto lama dari dataset
                            Dim oldNettoAmount As Double = SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("netto_amount_old"))

                            _difference = _difference + (newNettoAmount.ToString - oldNettoAmount.ToString)

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.planstgtd_det " _
                                                & "( " _
                                                & "  planstgtd_oid, " _
                                                & "  planstgtd_plans_oid, " _
                                                & "  planstgtd_seq, " _
                                                & "  planstgtd_year, " _
                                                & "  planstgtd_qrtr_id, " _
                                                & "  planstgtd_qrtr_start_date, " _
                                                & "  planstgtd_qrtr_end_date, " _
                                                & "  planstgtd_amount, " _
                                                & "  planstgtd_netto_amount, " _
                                                & "  planstgtd_spc, " _
                                                & "  planstgtd_parent " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_plans_oid_mstr.ToString) & ",  " _
                                                & SetInteger(i) & ", " _
                                                & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_year")) & ",  " _
                                                & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_id")) & ",  " _
                                                & SetDate(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_start_date")) & ",  " _
                                                & SetDate(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_end_date")) & ",  " _
                                                & SetDbl(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_netto_amount")) & ",  " _
                                                & SetSetring(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_spc")) & ",  " _
                                                & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_parent")) & "  " _
                                                & ")"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If SetString(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_parent")) <> "" Then
                                Dim _parent_sales_Id As String = SetString(ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_parent"))

                                'Dim updateParentSQL As String = "UPDATE planstgtd_det " _
                                '                                & "SET planstgtd_netto_amount = " _
                                '                                & "  ( " _
                                '                                & "    SELECT DISTINCT pd.planstgtd_netto_amount " _
                                '                                & "    FROM public.planstgtd_det pd " _
                                '                                & "      INNER JOIN public.plans_mstr pm ON pd.planstgtd_plans_oid = pm.plans_oid " _
                                '                                & "      INNER JOIN public.ptnr_mstr pt ON pm.plans_sales_id = pt.ptnr_id " _
                                '                                & "    WHERE pm.plans_sales_id = '" & _parent_sales_Id & "'" _
                                '                                & "      and pd.planstgtd_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_year")) & "' " _
                                '                                & "      and pd.planstgtd_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_id")) & "' " _
                                '                                & "    LIMIT 1 " _
                                '                                & ") + " & _difference & " " _
                                '                                & "WHERE planstgtd_plans_oid = " _
                                '                                & "  ( " _
                                '                                & "    SELECT DISTINCT pd.planstgtd_plans_oid " _
                                '                                & "    FROM public.planstgtd_det " _
                                '                                & "      INNER JOIN public.plans_mstr ON (public.planstgtd_det.planstgtd_plans_oid = public.plans_mstr.plans_oid) " _
                                '                                & "    WHERE plans_sales_id = '" & _parent_sales_Id & "'" _
                                '                                & ") " _
                                '                                & "    and planstgtd_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_year")) & "' " _
                                '                                & "    and planstgtd_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_id")) & "' "

                                Dim updateParentSQL As String = "UPDATE public.planstgtd_det " _
                                                                & "SET planstgtd_netto_amount = ( " _
                                                                & "    SELECT pd.planstgtd_netto_amount + '" & SetDec(_difference) & "' " _
                                                                & "    FROM public.planstgtd_det pd " _
                                                                & "    INNER JOIN public.plans_mstr pm ON pd.planstgtd_plans_oid = pm.plans_oid " _
                                                                & "    INNER JOIN public.ptnr_mstr pt ON pm.plans_sales_id = pt.ptnr_id " _
                                                                & "    WHERE pm.plans_sales_id = '" & _parent_sales_Id & "'" _
                                                                & "      AND pd.planstgtd_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_year")) & "' " _
                                                                & "      AND pd.planstgtd_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_id")) & "' " _
                                                                & "    LIMIT 1 " _
                                                                & ") " _
                                                                & "WHERE planstgtd_oid = ( " _
                                                                & "    SELECT DISTINCT pd.planstgtd_oid " _
                                                                & "    FROM public.planstgtd_det pd " _
                                                                & "    INNER JOIN public.plans_mstr pm ON pd.planstgtd_plans_oid = pm.plans_oid " _
                                                                & "    WHERE pm.plans_sales_id = '" & _parent_sales_Id & "'" _
                                                                & "AND planstgtd_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_year")) & "' " _
                                                                & "AND planstgtd_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("planstgtd_qrtr_id")) & "' " _
                                                                & "    LIMIT 1 " _
                                                                & ") "


                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = updateParentSQL
                                sSQLs.Add(updateParentSQL)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_plans_oid_mstr.ToString), "plans_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As NpgsqlException
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
            sSQLs.Clear()

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from plans_mstr where plans_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If
                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As NpgsqlException
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged

        Try
            'If e.Column.Name = "fcsd_01_amount" Or e.Column.Name = "fcsd_02_amount" Or e.Column.Name = "fcsd_03_amount" Then
            '    Dim _buffer_persen As Double = 0
            '    Dim _buffer As Double = 0
            '    If gv_edit.GetRowCellValue(e.RowHandle, "gr_code") = "TOP10" Then
            '        _buffer_persen = 0.5
            '    Else
            '        _buffer_persen = 0.2
            '    End If
            '    Dim _total As Double
            '    _total = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_01_amount")) + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_02_amount")) _
            '    + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_03_amount"))

            '    _buffer = _total / 3 * _buffer_persen
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_total_amount", _total)
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_buffer_amount", _buffer)

            'End If
            'gv_edit.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _rod_en_id As Integer = plans_en_id.EditValue

        If _col = "qrtr_name" Then
            Dim frm As New FPeriodeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _rod_en_id
            frm._code = plans_qrtr_years.Text
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub plans_sales_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles plans_sales_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = plans_sales_id
            frm._en_id = plans_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  a.plansd_oid, " _
                & "  a.plansd_plans_oid, " _
                & "  a.plansd_ptnr_id, " _
                & "  a.plansd_amount, " _
                & "  a.plansd_seq, " _
                & "  b.ptnr_code, " _
                & "  b.ptnr_name " _
                & "FROM " _
                & "  public.plansd_det a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.plansd_ptnr_id = b.ptnr_id) " _
                & "WHERE " _
                & "  a.plansd_plans_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.plansd_seq"

            load_data_detail(sql, gc_detail_cust, "detail")

            Try
                ds.Tables("detail_pt").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  a.plansptd_oid, " _
                & "  a.plansptd_plans_oid, " _
                & "  a.plansptd_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  c.code_name AS um_desc, " _
                & "  a.plansptd_amount " _
                & "FROM " _
                & "  public.plansptd_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.plansptd_pt_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "WHERE " _
                & "  a.plansptd_plans_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                & "ORDER BY " _
                & "  b.pt_desc1"

            load_data_detail(sql, gc_detail_pt, "detail_pt")

            Try
                ds.Tables("detail_target").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                        & "  a.planstgtd_qrtr_id, " _
                        & "  b.qrtr_name, " _
                        & "  a.planstgtd_amount, " _
                        & "  a.planstgtd_spc, " _
                        & "  a.planstgtd_year, " _
                        & "  a.planstgtd_qrtr_start_date, " _
                        & "  a.planstgtd_qrtr_end_date, " _
                        & "  a.planstgtd_netto_amount, " _
                        & "  a.planstgtd_oid, " _
                        & "  a.planstgtd_plans_oid, " _
                        & "  a.planstgtd_seq, " _
                        & "  a.planstgtd_parent " _
                        & "FROM public.planstgtd_det a " _
                        & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
                        & "WHERE " _
                        & "  a.planstgtd_plans_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                        & "ORDER BY " _
                        & "  a.planstgtd_seq"


            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "planstgtd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "planstgtd_year", plans_qrtr_years.Text)
            .SetRowCellValue(e.RowHandle, "planstgtd_amount", 0)
            .SetRowCellValue(e.RowHandle, "planstgtd_netto_amount", 0)
            .SetRowCellValue(e.RowHandle, "planstgtd_spc", "N")
            .SetRowCellValue(e.RowHandle, "planstgtd_parent", plans_qrtr_years.EditValue)
            .BestFitColumns()
        End With
    End Sub

    Private Sub BtGenMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenMonth.Click
        Try
            If plans_emp_parent_id.EditValue <> "" Then
                Dim sSQL As String
                sSQL = "SELECT " _
                    & "  b.qrtr_name, " _
                    & "  b.qrtr_code_id, " _
                    & "  b.qrtr_code_name, " _
                    & "  b.qrtr_id, " _
                    & "  b.qrtr_code, " _
                    & "  b.qrtr_year, " _
                    & "  b.qrtr_start_date, " _
                    & "  b.qrtr_end_date " _
                    & "FROM public.qrtr_mstr b " _
                    & "WHERE b.qrtr_year = " & SetSetring(plans_qrtr_years.Text) & " " _
                    & "AND b.qrtr_code_id = " & SetInteger(plans_qrtr_code_id.EditValue) & " " _
                    & "order by qrtr_id"

                Dim dt_fs As New DataTable
                dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

                ds_edit.Tables(0).Rows.Clear()

                For Each dr As DataRow In dt_fs.Rows
                    ' Pastikan baris tidak diduplikasi
                    Dim existingRows() As DataRow = ds_edit.Tables(0).Select("planstgtd_qrtr_id = '" & dr("qrtr_id") & "' AND planstgtd_parent = '" & plans_emp_parent_id.Tag & "'")
                    If existingRows.Length = 0 Then
                        Dim _row As DataRow = ds_edit.Tables(0).NewRow()
                        _row("planstgtd_oid") = Guid.NewGuid.ToString
                        _row("planstgtd_year") = dr("qrtr_year")
                        _row("planstgtd_qrtr_id") = dr("qrtr_id")
                        _row("qrtr_name") = dr("qrtr_name")
                        _row("planstgtd_qrtr_start_date") = dr("qrtr_start_date")
                        _row("planstgtd_qrtr_end_date") = dr("qrtr_end_date")
                        _row("planstgtd_spc") = "N"
                        _row("planstgtd_amount") = 0.0 ' Default nilai awal
                        _row("planstgtd_netto_amount") = 0.0 ' Default nilai awal
                        _row("planstgtd_parent") = plans_emp_parent_id.Tag
                        ds_edit.Tables(0).Rows.Add(_row)
                    End If
                Next
            Else
                Dim sSQL As String
                sSQL = "SELECT " _
                    & "  b.qrtr_name, " _
                    & "  b.qrtr_code_id, " _
                    & "  b.qrtr_code_name, " _
                    & "  b.qrtr_id, " _
                    & "  b.qrtr_code, " _
                    & "  b.qrtr_year, " _
                    & "  b.qrtr_start_date, " _
                    & "  b.qrtr_end_date " _
                    & "FROM public.qrtr_mstr b " _
                    & "WHERE b.qrtr_year = " & SetSetring(plans_qrtr_years.Text) & " " _
                    & "AND b.qrtr_code_id = " & SetInteger(plans_qrtr_code_id.EditValue) & " " _
                    & "order by qrtr_id"

                Dim dt_fs As New DataTable
                dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

                ds_edit.Tables(0).Rows.Clear()

                For Each dr As DataRow In dt_fs.Rows
                    ' Pastikan baris tidak diduplikasi
                    Dim existingRows() As DataRow = ds_edit.Tables(0).Select("planstgtd_qrtr_id = '" & dr("qrtr_id") & "' AND planstgtd_parent = '" & plans_emp_parent_id.Tag & "'")
                    If existingRows.Length = 0 Then
                        Dim _row As DataRow = ds_edit.Tables(0).NewRow()
                        _row("planstgtd_oid") = Guid.NewGuid.ToString
                        _row("planstgtd_year") = dr("qrtr_year")
                        _row("planstgtd_qrtr_id") = dr("qrtr_id")
                        _row("qrtr_name") = dr("qrtr_name")
                        _row("planstgtd_qrtr_start_date") = dr("qrtr_start_date")
                        _row("planstgtd_qrtr_end_date") = dr("qrtr_end_date")
                        _row("planstgtd_spc") = "N"
                        _row("planstgtd_amount") = 0.0 ' Default nilai awal
                        _row("planstgtd_netto_amount") = 0.0 ' Default nilai awal
                        ds_edit.Tables(0).Rows.Add(_row)
                    End If
                Next
            End If

            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try
            Dim totalNettoAmount As Double = 0.0 ' Variabel untuk menyimpan total netto
            Dim qrtr_id As Integer = 0 ' Initialize qrtr_id

            ' SQL query to get qrtr_id
            Dim sSQL As String
            sSQL = "SELECT b.qrtr_id " _
                 & "FROM public.qrtr_mstr b " _
                 & "WHERE b.qrtr_year = " & SetSetring(plans_qrtr_years.Text) & " " _
                 & "AND b.qrtr_code_id = " & SetInteger(plans_qrtr_code_id.EditValue)

            Dim dt_fs As New DataTable
            dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

            ' Check if the query returned any rows
            If dt_fs.Rows.Count > 0 Then
                qrtr_id = Convert.ToInt32(dt_fs.Rows(0)("qrtr_id")) ' Get the qrtr_id
            Else
                MsgBox("No data found for the specified year and code ID.")
                Return ' Exit the method if no data is found
            End If


            'If plans_emp_pos_id.Tag = 1 Then '1 id untuk CEO
            '' Jika plans_emp_parent_id kosong
            'For Each _row As DataRow In ds_edit.Tables(0).Rows

            '    Dim personalAmount As Double = If(IsDBNull(_row("planstgtd_amount")), 0.0, Convert.ToDouble(_row("planstgtd_amount")))

            '    ' SQL query to get summaryNettoAmount for this parent and qrtr_id
            '    Dim sSQLData As String
            '    sSQLData = "SELECT SUM(a.planstgtd_netto_amount) AS summaryNettoAmount " _
            '             & "FROM public.planstgtd_det a " _
            '             & "WHERE a.planstgtd_year = " & SetSetring(plans_qrtr_years.Text) & " " _
            '             & "AND a.planstgtd_qrtr_id = " & _row("planstgtd_qrtr_id").ToString() & " "

            '    Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

            '    ' Initialize summaryNettoAmount
            '    Dim summaryNettoAmount As Double = 0.0

            '    ' Check if the query returned any rows
            '    If dt_summary.Rows.Count > 0 Then
            '        summaryNettoAmount = If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
            '    End If

            '    ' Hitung Netto sebagai Personal + Summary Netto
            '    Dim nettoAmount As Double = personalAmount + summaryNettoAmount

            '    ' Update nilai Netto ke baris
            '    _row("planstgtd_netto_amount") = nettoAmount

            '    ' Tambahkan nilai Netto ke total
            '    totalNettoAmount += nettoAmount
            '    'End If
            'Next

            'ElseIf plans_emp_pos_id.Tag = 3 Then '3 id untuk NSM
            '' Jika plans_emp_parent_id kosong
            'For Each _row As DataRow In ds_edit.Tables(0).Rows

            '    Dim personalAmount As Double = If(IsDBNull(_row("planstgtd_amount")), 0.0, Convert.ToDouble(_row("planstgtd_amount")))

            '    ' SQL query to get summaryNettoAmount for this parent and qrtr_id
            '    Dim sSQLData As String
            '    sSQLData = "SELECT SUM(a.planstgtd_netto_amount) AS summaryNettoAmount " _
            '             & "FROM public.planstgtd_det a " _
            '             & "WHERE a.planstgtd_year = " & SetSetring(plans_qrtr_years.Text) & " " _
            '             & "AND a.planstgtd_qrtr_id = " & _row("planstgtd_qrtr_id").ToString() & " "

            '    Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

            '    ' Initialize summaryNettoAmount
            '    Dim summaryNettoAmount As Double = 0.0

            '    ' Check if the query returned any rows
            '    If dt_summary.Rows.Count > 0 Then
            '        summaryNettoAmount = If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
            '    End If

            '    ' Hitung Netto sebagai Personal + Summary Netto
            '    Dim nettoAmount As Double = personalAmount + summaryNettoAmount

            '    ' Update nilai Netto ke baris
            '    _row("planstgtd_netto_amount") = nettoAmount

            '    ' Tambahkan nilai Netto ke total
            '    totalNettoAmount += nettoAmount
            '    'End If
            'Next

            If plans_emp_parent_id.EditValue <> "" Then
                ' Jika plans_emp_parent_id tidak kosong
                For Each _row As DataRow In ds_edit.Tables(0).Rows
                    ' Pastikan baris valid berdasarkan filter
                    If _row("planstgtd_year").ToString() = plans_qrtr_years.Text AndAlso
                       _row("planstgtd_parent").ToString() = plans_emp_parent_id.Tag.ToString() Then

                        ' Ambil nilai Personal dan hitung Netto
                        Dim personalAmount As Double = If(IsDBNull(_row("planstgtd_amount")), 0.0, Convert.ToDouble(_row("planstgtd_amount")))
                        Dim nettoAmount As Double = personalAmount * 1 ' Contoh: Netto = 100% dari Personal

                        ' Update nilai Netto ke baris
                        _row("planstgtd_netto_amount") = nettoAmount

                        ' Tambahkan nilai Netto ke total
                        totalNettoAmount += nettoAmount
                    End If
                Next
            Else
                ' Jika plans_emp_parent_id kosong
                For Each _row As DataRow In ds_edit.Tables(0).Rows

                    Dim personalAmount As Double = If(IsDBNull(_row("planstgtd_amount")), 0.0, Convert.ToDouble(_row("planstgtd_amount")))

                    ' SQL query to get summaryNettoAmount for this parent and qrtr_id
                    Dim sSQLData As String
                    sSQLData = "SELECT SUM(a.planstgtd_netto_amount) AS summaryNettoAmount " _
                             & "FROM public.planstgtd_det a " _
                             & "WHERE a.planstgtd_year = " & SetSetring(plans_qrtr_years.Text) & " " _
                             & "AND a.planstgtd_qrtr_id = " & _row("planstgtd_qrtr_id").ToString() & " " _
                             & "AND a.planstgtd_parent = " & SetSetring(plans_sales_id.Tag.ToString)

                    Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

                    ' Initialize summaryNettoAmount
                    Dim summaryNettoAmount As Double = 0.0

                    ' Check if the query returned any rows
                    If dt_summary.Rows.Count > 0 Then
                        summaryNettoAmount = If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
                    End If

                    ' Hitung Netto sebagai Personal + Summary Netto
                    Dim nettoAmount As Double = personalAmount + summaryNettoAmount

                    ' Update nilai Netto ke baris
                    _row("planstgtd_netto_amount") = nettoAmount

                    ' Tambahkan nilai Netto ke total
                    totalNettoAmount += nettoAmount
                    'End If
                Next
            End If

            ' Masukkan total Netto ke kolom Total Target
            plans_amount_total.EditValue = totalNettoAmount

            ' Simpan perubahan ke dataset dan refresh grid
            ds_edit.AcceptChanges()
            gv_edit.RefreshData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetSummaryNettoAmount(year As String, qrtrId As Integer, parentId As String) As Double
        Dim sSQLData As String
        sSQLData = "SELECT SUM(a.planstgtd_amount) AS summaryNettoAmount " _
                 & "FROM public.planstgtd_det a " _
                 & "WHERE a.planstgtd_year = " & SetSetring(year) & " " _
                 & "AND a.planstgtd_qrtr_id = " & qrtrId & " " _
                 & "AND a.planstgtd_parent = " & SetSetring(parentId)

        Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

        If dt_summary.Rows.Count > 0 Then
            Return If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
        End If
        Return 0.0
    End Function

    Private Sub plans_ce_special_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ' Periksa apakah checkbox Is Special dicentang
            Dim isSpecial As Boolean = plans_ce_special.Checked

            ' Perbarui kolom planstgtd_spc untuk semua baris
            For Each _row As DataRow In ds_edit.Tables(0).Rows
                If _row.RowState <> DataRowState.Deleted Then
                    If isSpecial Then
                        _row("planstgtd_spc") = "Y"
                    Else
                        _row("planstgtd_spc") = "N"
                    End If
                End If
            Next

            ' Refresh grid view untuk menampilkan perubahan
            gv_edit.RefreshData()
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub file_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_excel.ButtonClick
        Try
            Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")
            If _file = "" Then Exit Sub

            file_excel.EditValue = _file
            Using ds As DataSet = master_new.excelconn.ImportExcel(_file)
                ds_edit.Tables(0).Rows.Clear()
                ds_edit.AcceptChanges()
                gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                Dim _row As Integer = 0
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim ssql As String = "SELECT  distinct " _
                                   & "  en_id, " _
                                   & "  en_desc, " _
                                   & "  si_desc, " _
                                   & "  pt_id, " _
                                   & "  pt_code, " _
                                   & "  pt_desc1, " _
                                   & "  pt_desc2, " _
                                   & "  pt_cost, " _
                                   & "  invct_cost, " _
                                   & "  pt_price, " _
                                   & "  pt_type, " _
                                   & "  pt_um, " _
                                   & "  pt_pl_id, " _
                                   & "  pt_ls, " _
                                   & "  pt_loc_id, " _
                                   & "  loc_desc, " _
                                   & "  pt_taxable, " _
                                   & "  pt_tax_inc, " _
                                   & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                                   & "  tax_class_mstr.code_name as tax_class_name, " _
                                   & "  pt_ppn_type, " _
                                   & "  um_mstr.code_name as um_name " _
                                   & "FROM  " _
                                   & "  public.pt_mstr" _
                                   & " inner join en_mstr on en_id = pt_en_id " _
                                   & " inner join loc_mstr on loc_id = pt_loc_id " _
                                   & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                                   & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                                   & " inner join invct_table on invct_pt_id = pt_id " _
                                   & " inner join si_mstr on si_id = invct_si_id " _
                                   & " where pt_code ='" & dr("pt_code") & "' "

                    Using dt_temp As DataTable = master_new.PGSqlConn.GetTableData(ssql)

                        For Each dr_temp As DataRow In dt_temp.Rows
                            gv_edit.AddNewRow()
                            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
                            gv_edit.SetRowCellValue(_row, "pt_id", dr_temp("pt_id"))
                            gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                            gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                            gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                            gv_edit.SetRowCellValue(_row, "ptsfrd_um", dr_temp("pt_um"))
                            gv_edit.SetRowCellValue(_row, "ptsfrd_qty_open", dr("qty"))
                            gv_edit.SetRowCellValue(_row, "ptsfrd_qty", dr("qty"))
                            gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", dr_temp("um_name"))
                            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                            _row += 1
                            'System.Windows.Forms.Application.DoEvents()
                            If _row Mod 10 = 0 Then System.Windows.Forms.Application.DoEvents()
                        Next
                    End Using
                Next
            End Using
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class