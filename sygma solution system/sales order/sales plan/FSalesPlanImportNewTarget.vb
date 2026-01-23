Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FSalesPlanImportNewTarget
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet
    Public ds_edit_target As DataSet
    Public _plans_periodes As String

    Private Sub FSalesPlanImportNewTarget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()

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

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_qrtr_code_id())
        'plans_qrtr_code_id.Properties.DataSource = dt_bantu
        'plans_qrtr_code_id.Properties.DisplayMember = dt_bantu.Columns("qrtr_code_name").ToString
        'plans_qrtr_code_id.Properties.ValueMember = dt_bantu.Columns("qrtr_code_id").ToString
        'plans_qrtr_code_id.ItemIndex = 0

        '    dt_bantu = New DataTable
        '    dt_bantu = (func_data.load_periode_mstr())
        '    plans_periode.Properties.DataSource = dt_bantu
        '    plans_periode.Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        '    plans_periode.Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
        '    plans_periode.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "plans_oid", "plans_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "plans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "plans_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Periode", "plans_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "plans_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount Total", "plans_amount_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "plans_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "plans_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "plans_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "plans_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail_target, "Month", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Target", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_target, "Amount", "planstgtd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_target, "is Special", "planstgtd_spc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Amount/Qty", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "plansd_ptnr_id", False)
        add_column(gv_edit, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Amount/Qty", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")

        'add_column(gv_edit_pt, "plansptd_pt_id", False)
        add_column(gv_edit_pt, "planstgtd_plans_oid", "planstgtd_plans_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "planstgtd_qrtr_id", "planstgtd_qrtr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Month", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_pt, "Partnumber Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_pt, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_pt, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit_pt, "Amount/Qty", "plansptd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")

        'add_column_copy(gv_detail_pt, "Partnumber Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_pt, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_pt, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_pt, "Amount/Qty", "plansptd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit_target, "planstgtd_plans_oid", False)
        add_column(gv_edit_target, "planstgtd_qrtr_id", False)
        'add_column(gv_edit_target, "Periode", "plans_spc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_target, "Month", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_target, "Year", "qrtr_year", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_target, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_target, "Amount", "planstgtd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column(gv_edit_target, "Special", "planstgtd_spc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.plans_oid, " _
                & "  a.plans_code, " _
                & "  a.plans_date, " _
                & "  a.plans_periode, " _
                & "  d.periode_start_date, " _
                & "  d.periode_end_date, " _
                & "  a.plans_sales_id, " _
                & "  c.ptnr_code, " _
                & "  c.ptnr_name, " _
                & "  a.plans_en_id, " _
                & "  b.en_desc, " _
                & "  a.plans_remarks, " _
                & "  a.plans_amount_total, " _
                & "  a.plans_add_by, " _
                & "  a.plans_add_date, " _
                & "  a.plans_upd_by, " _
                & "  a.plans_upd_date " _
                & "FROM " _
                & "  public.plans_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.plans_en_id = b.en_id) " _
                & "  left outer JOIN public.ptnr_mstr c ON (a.plans_sales_id = c.ptnr_id) " _
                & "  left outer JOIN public.psperiode_mstr d ON (a.plans_periode = d.periode_code) " _
                & "WHERE " _
                & " a.plans_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " and plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date)
        'Dim ssql As String
        'ssql = "select ptnr_id,ptnr_is_bm from ptnr_mstr where ptnr_user_name = (select usernama from tconfuser where userid= " _
        '                                   & master_new.ClsVar.sUserID & ")"
        'Dim dt As New DataTable
        'dt = master_new.PGSqlConn.GetTableData(ssql)

        'Dim _ptnr_id, _ptnr_is_bm As String
        '_ptnr_id = ""
        '_ptnr_is_bm = ""

        'For Each dr As DataRow In dt.Rows
        '    _ptnr_id = dr(0).ToString
        '    _ptnr_is_bm = SetString(dr(1))
        'Next

        'If _ptnr_is_bm = "" Or _ptnr_is_bm = "N" Then
        '    get_sequel += " and plans_sales_id=" & SetInteger(_ptnr_id)
        'End If

        get_sequel += "   ORDER BY " _
                & "  a.plans_code"

        Return get_sequel
    End Function

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

        plans_ptnr_nik.Tag = ""
        plans_ptnr_nik.Text = ""

        plans_emp_pos_id.Tag = ""
        plans_emp_pos_id.Text = ""

        plans_emp_cat_id.Tag = ""
        plans_emp_cat_id.Text = ""

        plans_emp_lvl_id.Tag = ""
        plans_emp_lvl_id.Text = ""

        plans_target.Text = ""
        plans_insentif.Text = ""

        plans_qrtr_years.EditValue = ""
        plans_qrtr_years.Text = ""
        plans_qrtr_years.Tag = ""

        plans_qrtr_periode_id.Tag = ""
        plans_qrtr_periode_id.Text = ""

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
                        & "  a.plansd_plans_oid IS NULL " _
                        & "ORDER BY " _
                        & "  a.plansd_seq"


                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()


                    .SQL = "SELECT  " _
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
                        & "  a.plansptd_plans_oid is null " _
                        & "ORDER BY " _
                        & "  b.pt_desc1"


                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit_pt")
                    gc_edit_pt.DataSource = ds_edit.Tables(1)
                    gv_edit_pt.BestFitColumns()

                    .SQL = "SELECT a.planstgtd_oid, " _
                        & "  a.planstgtd_plans_oid, " _
                        & "  a.planstgtd_seq, " _
                        & "  a.planstgtd_qrtr_id, " _
                        & "  b.qrtr_name, " _
                        & "  a.planstgtd_amount, " _
                        & "  a.planstgtd_spc " _
                        & "FROM public.planstgtd_det a " _
                        & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
                        & "WHERE " _
                        & "  a.planstgtd_plans_oid is null " _
                        & "ORDER BY " _
                        & "  a.planstgtd_seq"


                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit_target")
                    gc_edit_pt.DataSource = ds_edit.Tables(1)
                    gv_edit_pt.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean

        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _code As String
        Dim _total As Double

        _code = GetNewNumberYM("plans_mstr", "plans_code", 5, "PLA" & plans_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        'gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        'ds_edit_target.Tables(0).AcceptChanges()

        'Dim _plans_year As Integer = Convert.ToDateTime(plans_year.EditValue).Year

        Try
            _total = 0
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                _total = _total + SetNumber(ds_edit.Tables(0).Rows(i).Item("planstgtd_amount"))
            Next

            ssql = "INSERT INTO  " _
                & "  public.plans_mstr " _
                & "( " _
                & "  plans_oid, " _
                & "  plans_dom_id, " _
                & "  plans_code, " _
                & "  plans_en_id, " _
                & "  plans_date, " _
                & "  plans_periode, " _
                & "  plans_sales_id, " _
                & "  plans_amount_total, " _
                & "  plans_amount_tgt, " _
                & "  plans_insentif, " _
                & "  plans_remarks, " _
                & "  plans_add_by, " _
                & "  plans_add_date " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetInteger(plans_en_id.EditValue) & ",  " _
                & SetDateNTime00(plans_date.EditValue) & ",  " _
                & SetSetring(_plans_periodes) & ", " _
                & SetInteger(plans_sales_id.Tag) & ",  " _
                & SetDec(_total) & ",  " _
                & SetDec(plans_target.EditValue) & ",  " _
                & SetDec(plans_insentif.EditValue) & ",  " _
                & SetSetring(plans_remarks.EditValue) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                & ")"

            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.plansd_det " _
                        & "( " _
                        & "  plansd_oid, " _
                        & "  plansd_plans_oid, " _
                        & "  plansd_ptnr_id, " _
                        & "  plansd_amount, " _
                        & "  plansd_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetInteger(.Item("plansd_ptnr_id")) & ",  " _
                        & SetDec(.Item("plansd_amount")) & ",  " _
                        & SetInteger(i) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next

            For i = 0 To ds_edit.Tables("edit_pt").Rows.Count - 1
                With ds_edit.Tables("edit_pt").Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.plansptd_det " _
                        & "( " _
                        & "  plansptd_oid, " _
                        & "  plansptd_plans_oid, " _
                        & "  plansptd_pt_id, " _
                        & "  plansptd_amount " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetInteger(.Item("plansptd_pt_id")) & ",  " _
                        & SetDec(.Item("plansptd_amount")) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next

            For i = 0 To ds_edit.Tables("edit_target").Rows.Count - 1
                With ds_edit.Tables("edit_target").Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.plansptd_det " _
                        & "( " _
                        & "  planstgtd_oid, " _
                        & "  planstgtd_plans_oid, " _
                        & "  planstgtd_amount, " _
                        & "  planstgtd_qrtr_id, " _
                        & "  planstgtd_spc, " _
                        & "  planstgtd_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetDec(.Item("planstgtd_amount")) & ", " _
                        & SetInteger(.Item("planstgtd_qrtr_id")) & ",  " _
                        & SetSetring(.Item("planstgtd_spc")) & ",  " _
                        & SetInteger(i) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If

            after_success()
            set_row(_mstr_oid, "plans_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _mstr_oid = .Item("plans_oid").ToString
                plans_en_id.EditValue = .Item("plans_en_id")
                plans_date.DateTime = .Item("plans_date")
                plans_sales_id.Tag = .Item("plans_sales_id")
                plans_sales_id.EditValue = .Item("ptnr_name")
                plans_remarks.EditValue = .Item("plans_remarks")
            End With
            plans_en_id.Focus()

            'plans_year.Enabled = False

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "SELECT  " _
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

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "edit")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        .SQL = "SELECT  " _
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
                            & "  b.planstgtd_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "edit_pt")
                        gc_edit_pt.DataSource = ds_edit.Tables("edit_pt")
                        gv_edit_pt.BestFitColumns()
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
        Dim ssqls As New ArrayList
        Dim i As Integer
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        Dim _total As Double

                        _total = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _total = _total + SetNumber(ds_edit.Tables(0).Rows(i).Item("plansd_amount"))
                        Next

                        .Command.CommandText = "UPDATE  " _
                                & "  public.plans_mstr   " _
                                & "SET  " _
                                & "  plans_date = " & SetDateNTime00(plans_date.EditValue) & ",  " _
                                & "  plans_sales_id = " & SetInteger(plans_sales_id.Tag) & ",  " _
                                & "  plans_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  plans_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                & "  plans_amount_total = " & SetDec(_total) & ",  " _
                                & "  plans_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & "  plans_en_id = " & SetSetring(plans_en_id.EditValue) & ",  " _
                                & "  plans_remarks = " & SetSetring(plans_remarks.EditValue) & "  " _
                                & "WHERE  " _
                                & "  plans_oid = " & SetSetring(_mstr_oid) & " "


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from plansd_det " _
                                            & "WHERE  " _
                                            & "  plansd_plans_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from plansptd_det " _
                                            & "WHERE  " _
                                            & "  plansptd_plans_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)

                            ssql = "INSERT INTO  " _
                              & "  public.plansd_det " _
                              & "( " _
                              & "  plansd_oid, " _
                              & "  plansd_plans_oid, " _
                              & "  plansd_ptnr_id, " _
                              & "  plansd_amount, " _
                              & "  plansd_seq " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetSetring(_mstr_oid) & ",  " _
                              & SetInteger(ds_edit.Tables(0).Rows(i).Item("plansd_ptnr_id")) & ",  " _
                              & SetDec(ds_edit.Tables(0).Rows(i).Item("plansd_amount")) & ",  " _
                              & SetInteger(i) & "  " _
                              & ")"


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'End With
                        Next


                        For i = 0 To ds_edit.Tables("edit_pt").Rows.Count - 1

                            ssql = "INSERT INTO  " _
                                & "  public.plansptd_det " _
                                & "( " _
                                & "  plansptd_oid, " _
                                & "  plansptd_plans_oid, " _
                                & "  plansptd_pt_id, " _
                                & "  plansptd_amount " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetInteger(ds_edit.Tables("edit_pt").Rows(i).Item("plansptd_pt_id")) & ",  " _
                                & SetDec(ds_edit.Tables("edit_pt").Rows(i).Item("plansptd_amount")) & "  " _
                                & ")"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql

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

                        after_success()
                        set_row(_mstr_oid, "plans_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As NpgsqlException
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


    End Function
    Public Overrides Function delete_data() As Boolean
        delete_data = False

        gv_master_SelectionChanged(Nothing, Nothing)

        Dim sSQL As String
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

        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                    sSQL = "DELETE FROM  " _
                        & "  public.plans_mstr  " _
                        & "WHERE  " _
                        & "  plans_oid ='" & .Item("plans_oid").ToString & "'"

                    ssqls.Add(sSQL)


                End With

                If master_new.PGSqlConn.status_sync = True Then
                    If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                End If

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Overrides Function before_save() As Boolean
        before_save = True

        'If SetString(plans_periode.EditValue) = "" Then
        '    Box("Periode can't blank")
        '    Return False
        '    Exit Function
        'End If

        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    Box("Detail can't blank")
        '    Return False
        '    Exit Function
        'End If


        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(sender, Nothing)
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


            load_data_detail(sql, gc_detail, "detail")

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

            sql = "SELECT a.planstgtd_oid, " _
                        & "  a.planstgtd_plans_oid, " _
                        & "  a.planstgtd_seq, " _
                        & "  a.planstgtd_qrtr_id, " _
                        & "  b.qrtr_name, " _
                        & "  a.planstgtd_amount, " _
                        & "  a.planstgtd_spc " _
                        & "FROM public.planstgtd_det a " _
                        & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
                        & "WHERE " _
                        & "  a.planstgtd_plans_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plans_oid").ToString & "' " _
                        & "ORDER BY " _
                        & "  a.planstgtd_seq"


            load_data_detail(sql, gc_detail_target, "detail_target")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "ptnr_name" Or _col = "ptnr_code" Then


            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = gv_edit
            frm._row = _row
            frm._en_id = plans_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "pt_code" Or _col = "pt_desc1" Then

            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = plans_en_id.EditValue
            frm._si_id = gv_edit.GetRowCellValue(_row, "riud_si_id")
            frm.type_form = True
            frm.ShowDialog()


        End If
    End Sub

    Private Sub browse_data_pt()
        Dim _col As String = gv_edit_pt.FocusedColumn.Name
        Dim _row As Integer = gv_edit_pt.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "pt_code" Or _col = "pt_desc1" Then

            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = plans_en_id.EditValue
            'frm._si_id = gv_edit_pt.GetRowCellValue(_row, "riud_si_id")
            frm.type_form = True
            frm.ShowDialog()


        End If
    End Sub

    Private Sub browse_data_periode()
        Dim _col As String = gv_edit_pt.FocusedColumn.Name
        Dim _row As Integer = gv_edit_pt.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "pt_code" Or _col = "pt_desc1" Then

            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = plans_en_id.EditValue
            'frm._si_id = gv_edit_pt.GetRowCellValue(_row, "riud_si_id")
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

    'Private Sub plans_qrtr_periode_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Dim frm As New FPeriodeSearch
    '    frm.set_win(Me)
    '    'frm._en_id = genfsn_en_id.EditValue
    '    frm._obj = plans_qrtr_code_id
    '    frm._code = plans_qrtr_code_id.EditValue
    '    'frm._pil = 1
    '    frm.type_form = True
    '    frm.ShowDialog()
    '    'BtGen.PerformClick()
    'End Sub

    Private Sub plans_qrtr_periode_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles plans_qrtr_periode_id.ButtonClick
        Dim frm As New FPeriodeSearch
        frm.set_win(Me)
        'frm._en_id = geninstf_en_id.EditValue
        frm._obj = plans_qrtr_periode_id
        frm._code = plans_qrtr_years.Text
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
        gv_edit.BestFitColumns()
    End Sub

    Private Sub gv_edit_pt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_pt.DoubleClick
        browse_data_pt()
    End Sub

    Private Sub gv_edit_target_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        browse_data_periode()
    End Sub

    Private Sub gv_edit_pt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_pt.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data_pt()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_pt_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit_pt.RowUpdated
        gv_edit_pt.BestFitColumns()
    End Sub

    Private Sub gc_edit_target_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_target.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_target.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gc_edit_target_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs)
        gv_edit_target.BestFitColumns()
    End Sub

    Private Sub file_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_excel.ButtonClick
        Try
            Dim dt_temp As New DataTable
            Dim _row As Integer = 0

            Dim ssql As String
            Dim ds As New DataSet
            Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")

            If _file = "" Then
                Exit Sub
            End If

            file_excel.EditValue = _file
            ds = master_new.excelconn.ImportExcel(_file)

            ds_edit.Tables(0).Rows.Clear()
            ds_edit.AcceptChanges()

            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

            For Each dr As DataRow In ds.Tables(0).Rows
                'ssql = "SELECT  distinct " _
                '   & "  en_id, " _
                '   & "  en_desc, " _
                '   & "  si_desc, " _
                '   & "  pt_id, " _
                '   & "  pt_code, " _
                '   & "  pt_desc1, " _
                '   & "  pt_desc2, " _
                '   & "  pt_cost, " _
                '   & "  invct_cost, " _
                '   & "  pt_price, " _
                '   & "  pt_type, " _
                '   & "  pt_um, " _
                '   & "  pt_pl_id, " _
                '   & "  pt_ls, " _
                '   & "  pt_loc_id, " _
                '   & "  loc_desc, " _
                '   & "  pt_taxable, " _
                '   & "  pt_tax_inc, " _
                '   & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                '   & "  tax_class_mstr.code_name as tax_class_name, " _
                '   & "  pt_ppn_type, " _
                '   & "  um_mstr.code_name as um_name " _
                '   & "FROM  " _
                '   & "  public.pt_mstr" _
                '   & " inner join en_mstr on en_id = pt_en_id " _
                '   & " inner join loc_mstr on loc_id = pt_loc_id " _
                '   & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                '   & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                '   & " inner join invct_table on invct_pt_id = pt_id " _
                '   & " inner join si_mstr on si_id = invct_si_id " _
                '   & " where pt_code ='" & dr("pt_code") & "' "

                ssql = "SELECT  " _
                    & "  a.qrtr_oid, " _
                    & "  a.qrtr_id, " _
                    & "  a.qrtr_code, " _
                    & "  a.qrtr_name, " _
                    & "  a.qrtr_year, " _
                    & "  a.qrtr_start_date, " _
                    & "  a.qrtr_end_date, " _
                    & "  a.qrtr_code_id, " _
                    & "  a.qrtr_code_name, " _
                    & "  a.qrtr_add_by, " _
                    & "  a.qrtr_add_date, " _
                    & "  a.qrtr_upd_by, " _
                    & "  a.qrtr_upd_date " _
                    & "FROM " _
                    & "  public.qrtr_mstr a " _
                    & " where qrtr_name ='" & dr("month") & "' " _
                    & " and qrtr_year ='" & dr("year") & "' "

                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr_temp As DataRow In dt_temp.Rows

                    Dim ds_bantu As New DataSet

                    gv_edit_pt.AddNewRow()
                    gc_edit_pt.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    'gv_edit_pt.SetRowCellValue(_row, "plansptd_pt_id", dr_temp("pt_id"))
                    'gv_edit_pt.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                    'gv_edit_pt.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                    'gv_edit_pt.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                    'gv_edit_pt.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                    'gv_edit_pt.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                    'gv_edit_pt.SetRowCellValue(_row, "sqd_loc_id", dr_temp("pt_loc_id"))
                    'gv_edit_pt.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                    'gv_edit_pt.SetRowCellValue(_row, "sqd_um", dr_temp("pt_um"))
                    'gv_edit_pt.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                    'gv_edit_pt.SetRowCellValue(_row, "sqd_cost", dr_temp("invct_cost"))

                    'If _so_type <> "D" Then
                    '    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                    'End If

                    'gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                    'gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                    'gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                    'gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                    'gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                    'gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                    'gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                    'gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                    'gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                    'gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                    'gv_edit.SetRowCellValue(_row, "sqd_taxable", dr_temp("pt_taxable"))
                    'gv_edit.SetRowCellValue(_row, "sqd_tax_inc", dr_temp("pt_tax_inc"))
                    'gv_edit.SetRowCellValue(_row, "sqd_tax_class", dr_temp("pt_tax_class"))
                    'gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", dr_temp("tax_class_name"))

                    'gv_edit.SetRowCellValue(_row, "sqd_ppn_type", dr_temp("pt_ppn_type"))
                    'gv_edit_pt.SetRowCellValue(_row, "plansptd_qty", dr("qty"))
                    'gv_edit_pt.SetRowCellValue(_row, "plansptd_amount", dr("qty"))
                    'gv_edit.SetRowCellValue(_row, "sqd_disc", dr("disc"))

                    gv_edit_pt.SetRowCellValue(_row, "planstgtd_qrtr_id", dr_temp("qrtr_id"))
                    gv_edit_pt.SetRowCellValue(_row, "qrtr_name", dr_temp("qrtr_name"))
                    gv_edit_pt.SetRowCellValue(_row, "planstgtd_year", dr_temp("qrtr_year"))
                    'gv_edit_pt.SetRowCellValue(_row, "planstgtd_amount", dr_temp("amount"))
                    'gv_edit_pt.SetRowCellValue(_row, "planstgtd_spc", dr_temp("special"))

                    gc_edit_pt.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    _row = _row + 1
                    System.Windows.Forms.Application.DoEvents()
                    'Exit Sub
                Next
            Next
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub plans_import_EditValueChanged(sender As Object, e As EventArgs) Handles plans_tgt_import.EditValueChanged

    End Sub

    Private Sub plans_import_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles plans_tgt_import.ButtonClick
        Try
            Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")
            If _file = "" Then Exit Sub

            plans_tgt_import.EditValue = _file
            Using ds As DataSet = master_new.excelconn.ImportExcel(_file)
                ds_edit.Tables(0).Rows.Clear()
                ds_edit.AcceptChanges()
                gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit_target.EmbeddedNavigator.Buttons.EndEdit)

                Dim _row As Integer = 0
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim ssql As String = "SELECT " _
                                    & " a.planstgtd_oid, " _
                                    & "  a.planstgtd_plans_oid, " _
                                    & "  a.planstgtd_seq, " _
                                    & "  a.planstgtd_qrtr_id, " _
                                    & "  a.planstgtd_year, " _
                                    & "  b.qrtr_id, " _
                                    & "  b.qrtr_name, " _
                                    & "  b.qrtr_year, " _
                                    & "  a.planstgtd_amount, " _
                                    & "  a.planstgtd_spc " _
                                    & "FROM public.planstgtd_det a " _
                                    & "  INNER JOIN public.qrtr_mstr b ON (a.planstgtd_qrtr_id = b.qrtr_id) " _
                                    & "WHERE " _
                                    & "  b.qrtr_name = '" & dr("month") & "' " _
                                    & "  and b.qrtr_year = '" & dr("year") & "' "

                    Using dt_temp As DataTable = master_new.PGSqlConn.GetTableData(ssql)

                        For Each dr_temp As DataRow In dt_temp.Rows
                            gv_edit_target.AddNewRow()
                            gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit_target.EmbeddedNavigator.Buttons.EndEdit)

                            'gv_edit_target.SetRowCellValue(_row, "planstgtd_qrtr_id", dr_temp("planstgtd_qrtr_id"))
                            'gv_edit_target.SetRowCellValue(_row, "qrtr_name", dr_temp("month"))
                            'gv_edit_target.SetRowCellValue(_row, "planstgtd_year", dr_temp("year"))
                            'gv_edit_target.SetRowCellValue(_row, "planstgtd_amount", dr_temp("amount"))
                            'gv_edit_target.SetRowCellValue(_row, "planstgtd_spc", dr_temp("special"))

                            gv_edit_target.SetRowCellValue(_row, "planstgtd_qrtr_id", dr_temp("qrtr_id"))
                            gv_edit_target.SetRowCellValue(_row, "qrtr_name", dr_temp("qrtr_name"))
                            gv_edit_target.SetRowCellValue(_row, "planstgtd_year", dr_temp("qrtr_year"))
                            gv_edit_target.SetRowCellValue(_row, "planstgtd_amount", dr_temp("amount"))
                            gv_edit_target.SetRowCellValue(_row, "planstgtd_spc", dr_temp("special"))

                            gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit_target.EmbeddedNavigator.Buttons.EndEdit)

                            _row = _row + 1
                            System.Windows.Forms.Application.DoEvents()
                            'System.Windows.Forms.Application.DoEvents()
                            'If _row Mod 10 = 0 Then System.Windows.Forms.Application.DoEvents()
                        Next
                    End Using
                Next
            End Using
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    'Private Sub plans_tgt_import_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles plans_tgt_import.ButtonClick
    '    Try
    '        Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")
    '        If _file = "" Then Exit Sub

    '        file_excel.EditValue = _file
    '        Using ds As DataSet = master_new.excelconn.ImportExcel(_file)
    '            ds_edit.Tables(0).Rows.Clear()
    '            ds_edit.AcceptChanges()
    '            gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit_target.EmbeddedNavigator.Buttons.EndEdit)

    '            Dim _row As Integer = 0
    '            For Each dr As DataRow In ds.Tables(0).Rows
    '                'Dim ssql As String = "SELECT  distinct " _
    '                '               & "  en_id, " _
    '                '               & "  en_desc, " _
    '                '               & "  si_desc, " _
    '                '               & "  pt_id, " _
    '                '               & "  pt_code, " _
    '                '               & "  pt_desc1, " _
    '                '               & "  pt_desc2, " _
    '                '               & "  pt_cost, " _
    '                '               & "  invct_cost, " _
    '                '               & "  pt_price, " _
    '                '               & "  pt_type, " _
    '                '               & "  pt_um, " _
    '                '               & "  pt_pl_id, " _
    '                '               & "  pt_ls, " _
    '                '               & "  pt_loc_id, " _
    '                '               & "  loc_desc, " _
    '                '               & "  pt_taxable, " _
    '                '               & "  pt_tax_inc, " _
    '                '               & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
    '                '               & "  tax_class_mstr.code_name as tax_class_name, " _
    '                '               & "  pt_ppn_type, " _
    '                '               & "  um_mstr.code_name as um_name " _
    '                '               & "FROM  " _
    '                '               & "  public.pt_mstr" _
    '                '               & " inner join en_mstr on en_id = pt_en_id " _
    '                '               & " inner join loc_mstr on loc_id = pt_loc_id " _
    '                '               & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
    '                '               & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
    '                '               & " inner join invct_table on invct_pt_id = pt_id " _
    '                '               & " inner join si_mstr on si_id = invct_si_id " _
    '                '               & " where pt_code ='" & dr("pt_code") & "' "

    '                Dim ssql As String = "SELECT  " _
    '                                & "  a.qrtr_oid, " _
    '                                & "  a.qrtr_id, " _
    '                                & "  a.qrtr_code, " _
    '                                & "  a.qrtr_name, " _
    '                                & "  a.qrtr_year, " _
    '                                & "  a.qrtr_start_date, " _
    '                                & "  a.qrtr_end_date, " _
    '                                & "  a.qrtr_code_id, " _
    '                                & "  a.qrtr_code_name, " _
    '                                & "  a.qrtr_add_by, " _
    '                                & "  a.qrtr_add_date, " _
    '                                & "  a.qrtr_upd_by, " _
    '                                & "  a.qrtr_upd_date " _
    '                                & "FROM " _
    '                                & "  public.qrtr_mstr a " _
    '                                & " where qrtr_name ='" & dr("month") & "' " _
    '                                & " and qrtr_year ='" & dr("year") & "' "


    '                Using dt_temp As DataTable = master_new.PGSqlConn.GetTableData(ssql)

    '                    For Each dr_temp As DataRow In dt_temp.Rows
    '                        gv_edit_target.AddNewRow()
    '                        gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit_target.EmbeddedNavigator.Buttons.EndEdit)

    '                        gv_edit_target.SetRowCellValue(_row, "planstgtd_qrtr_id", dr_temp("qrtr_id"))
    '                        gv_edit_target.SetRowCellValue(_row, "qrtr_name", dr_temp("qrtr_name"))
    '                        gv_edit_target.SetRowCellValue(_row, "planstgtd_year", dr_temp("year"))
    '                        gv_edit_target.SetRowCellValue(_row, "planstgtd_amount", dr_temp("amount"))
    '                        gv_edit_target.SetRowCellValue(_row, "planstgtd_spc", dr_temp("special"))

    '                        gc_edit_target.EmbeddedNavigator.Buttons.DoClick(gc_edit_target.EmbeddedNavigator.Buttons.EndEdit)

    '                        _row += 1
    '                        'System.Windows.Forms.Application.DoEvents()
    '                        If _row Mod 10 = 0 Then System.Windows.Forms.Application.DoEvents()
    '                    Next
    '                End Using
    '            Next
    '        End Using
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub
End Class
