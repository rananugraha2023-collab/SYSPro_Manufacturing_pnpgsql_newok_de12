Imports Npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraGrid.Views.Base

Public Class FBonusInsentifGeneratorSDQ
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList
    Public _geninstf_fsn_codes As String
    Dim _now As DateTime

    Private Property inv_fsn_end_date As Object


    Private Sub FBonusInsentifGenerator_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()


        'plans_qrtr_years.EditValue = DateTime.Now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        geninstf_en_id.Properties.DataSource = dt_bantu
        geninstf_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        geninstf_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        geninstf_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_qrtr_code_id())
        'geninstf_qrtr_code_id.Properties.DataSource = dt_bantu
        'geninstf_qrtr_code_id.Properties.DisplayMember = dt_bantu.Columns("qrtr_code_name").ToString
        'geninstf_qrtr_code_id.Properties.ValueMember = dt_bantu.Columns("qrtr_code_id").ToString
        'geninstf_qrtr_code_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Domain", "dom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "All Child", "geninstf_all_child", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "INS Number", "geninstf_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "FSN Number", "genfsn_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "FSN M/Q", "qrtr_name_fsn", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "FSN Period", "qrtr_periode_name_fsn", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Period", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Emp Category", "geninstf_qrtr_id", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Sales Start Date", "geninstf_qrtr_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Sales End Date", "geninstf_qrtr_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Year", "geninstf_qrtr_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "geninstf_remarks", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "geninstf_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "geninstf_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "geninstf_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "geninstf_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_detail, "geninstf_geninstf_oid", False)
        'add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Group", "ptgroup_code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Methode", "pt_methode_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Total Qty", "genfsnd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Clasification", "genfsnd_fsn_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "genfsnd_geninstf_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Category", "emp_cat_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Target", "geninstfd_planstgtd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Target", "geninstfd_plans_instf", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sales Achievement", "geninstfd_achieve", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Achievement (%)", "geninstfd_pct_achieve", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_edit, "Achievement (%)", "soshipd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Is Special", "geninstfd_planstgtd_spc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Insentive", "geninstfd_instf", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Tax (PPH 21)", "geninstfd_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Total", "geninstfd_payout", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column(gv_edit, "genfsnd_geninstf_oid", False)
        add_column(gv_edit, "geninstfd_en_id", False)
        add_column_copy(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "geninstfd_ptnr_id", False)
        add_column_copy(gv_edit, "Sales Code", "sales_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Name", "sales_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Category", "sales_cat", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Target", "geninstfd_planstgtd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "Target", "geninstfd_plans_instf", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Sales Achievement", "geninstfd_achieve", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Achievement (%)", "geninstfd_pct_achieve", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_edit, "Achievement (%)", "soshipd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_edit, "Is Special", "geninstfd_planstgtd_spc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_edit, "Insentive", "geninstfd_instf", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Tax (PPH 21)", "geninstfd_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Total", "geninstfd_payout", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        '_row("geninstfd_si_id") = dr("si_id")
        '_row("geninstfd_en_id") = dr("en_id")
        '_row("geninstfd_ptnr_id") = dr("sales_id")
        '_row("sales_code") = dr("sales_code")
        '_row("sales_name") = dr("sales_name")
        '_row("sales_cat") = dr("sales_cat")
        '_row("geninstfd_qrtr_id") = dr("sales_id")
        '_row("geninstfd_planstgtd_amount") = dr("planstgtd_amount")
        '_row("geninstfd_plans_instf") = dr("plans_insentif")
        '_row("geninstfd_planstgtd_spc") = dr("planstgtd_spc")
        '_row("geninstfd_achieve") = dr("sales_achievement")
        '_row("geninstfd_soshipd_qty_id") = dr("soshipd_qty")
        '_row("geninstfd_ttl_sls") = dr("total_sales")
        '_row("geninstfd_ttl_disc") = dr("total_discount")
        '_row("geninstfd_ttl_prc_fp") = dr("total_price_fp")
        '_row("geninstfd_ttl_disc_fp") = dr("total_disc_fp")
        '_row("geninstfd_ttl_disc_fp") = dr("total_bobot")
        '_row("geninstfd_ttl_disc_fp") = dr("total_cat")
        '_row("geninstfd_ttl_disc_fp") = dr("avg_bobot")
        '_row("geninstfd_pct_achieve") = dr("peres")
        '_row("geninstfd_instf") = dr("final_insentif")
        '_row("geninstfd_tax") = dr("pph_21")
        '_row("geninstfd_payout") = dr("payout")
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.geninstf_oid, " _
                & "  a.geninstf_dom_id, " _
                & "  e.dom_desc, " _
                & "  a.geninstf_en_id, " _
                & "  b.en_desc, " _
                & "  a.geninstf_all_child, " _
                & "  a.geninstf_code, " _
                & "  a.geninstf_fsn_oid, " _
                & "  c.genfsn_code, " _
                & "  a.geninstf_qrtr_code, " _
                & "  qrtr_code_fsn.qrtr_code_name as qrtr_name_fsn, " _
                & "  a.geninstf_qrtr_periode, " _
                & "  qrtr_periode_fsn.qrtr_name as qrtr_periode_name_fsn, " _
                & "  a.geninstf_qrtr_id, " _
                & "  d.qrtr_name, " _
                & "  a.geninstf_qrtr_start_date, " _
                & "  a.geninstf_qrtr_end_date, " _
                & "  a.geninstf_qrtr_year, " _
                & "  a.geninstf_remark, " _
                & "  a.geninstf_trans_id, " _
                & "  a.geninstf_dt, " _
                & "  a.geninstf_generate, " _
                & "  a.geninstf_gen_by, " _
                & "  a.geninstf_gen_date, " _
                & "  a.geninstf_remarks, " _
                & "  a.geninstf_add_by, " _
                & "  a.geninstf_add_date, " _
                & "  a.geninstf_upd_date, " _
                & "  a.geninstf_dom_id " _
                & "FROM public.geninstf_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.geninstf_en_id = b.en_id) " _
                & "  INNER JOIN public.genfsn_mstr c ON (a.geninstf_fsn_oid = c.genfsn_oid) " _
                & "  INNER JOIN public.qrtr_mstr d ON (a.geninstf_qrtr_id = d.qrtr_id) " _
                & "  INNER JOIN public.dom_mstr e ON (a.geninstf_dom_id = e.dom_id) " _
                & "  INNER JOIN qrtr_mstr qrtr_code_fsn on qrtr_code_fsn.qrtr_id = geninstf_qrtr_code " _
                & "  INNER JOIN qrtr_mstr qrtr_periode_fsn on qrtr_periode_fsn.qrtr_id = geninstf_qrtr_periode " _
                & "   where geninstf_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " order by geninstf_code"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.geninstfd_oid, " _
            & "  a.geninstfd_geninstf_oid, " _
            & "  a.geninstfd_en_id, " _
            & "  b.en_desc, " _
            & "  a.geninstfd_ptnr_id, " _
            & "  c.ptnr_code, " _
            & "  c.ptnr_name, " _
            & "  a.geninstfd_sls_cat_id, " _
            & "  d.emp_cat_name, " _
            & "  a.geninstfd_qrtr_id, " _
            & "  e.qrtr_name, " _
            & "  a.geninstfd_year, " _
            & "  a.geninstfd_planstgtd_amount, " _
            & "  a.geninstfd_planstgtd_spc, " _
            & "  a.geninstfd_achieve, " _
            & "  a.geninstfd_pct_achieve, " _
            & "  a.geninstfd_instf, " _
            & "  a.geninstfd_tax, " _
            & "  a.geninstfd_payout, " _
            & "  a.geninstfd_seq " _
            & "FROM " _
            & "  public.geninstfd_det a " _
            & "  INNER JOIN public.en_mstr b ON (a.geninstfd_en_id = b.en_id) " _
            & "  INNER JOIN public.ptnr_mstr c ON (a.geninstfd_ptnr_id = c.ptnr_id) " _
            & "  INNER JOIN public.sls_emp_cat d ON (a.geninstfd_sls_cat_id = d.emp_cat_id) " _
            & "  INNER JOIN public.qrtr_mstr e ON (a.geninstfd_qrtr_id = e.qrtr_id) " _
            & "WHERE " _
            & "  a.geninstfd_geninstf_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("geninstf_oid").ToString & "' " _
            & "ORDER BY " _
            & "  geninstfd_seq"

        load_data_detail(sql, gc_detail, "detail")
        gv_detail.BestFitColumns()
    End Sub

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
        geninstf_en_id.ItemIndex = 0
        'geninstf_qrtr_id.ItemIndex = 0

        geninstf_remarks.Text = ""
        geninstf_en_id.Focus()
        geninstf_all_child.EditValue = True

        geninstf_ptnr_id.Tag = ""
        geninstf_ptnr_id.Text = ""
        geninstf_sales.EditValue = False
        '_geninstf_fsn_ids = ""

        '_geninstf_qrtr_ids = -1
        'geninstf_qrtr_id.Text = ""

        geninstf_fsn_oid.Text = ""
        geninstf_fsn_oid.Tag = ""

        geninstf_qrtr_code.Tag = ""
        geninstf_qrtr_code.Text = ""

        geninstf_qrtr_periode.Tag = ""
        geninstf_qrtr_periode.Text = ""

        geninstf_qrtr_id.Tag = ""
        geninstf_qrtr_id.Text = ""

        geninstf_generate.EditValue = False
        geninstf_gen_date.DateTime = _now

        'geninstf_start_dt.EditValue = ""
        'geninstf_end_dt.EditValue = ""
        'geninstf_year.EditValue = ""

        Try
            XtraTabControl1.SelectedTabPageIndex = 0
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
                    & "  a.geninstfd_oid, " _
                    & "  a.geninstfd_geninstf_oid, " _
                    & "  a.geninstfd_genfsn_oid, " _
                    & "  a.geninstfd_seq, " _
                    & "  a.geninstfd_si_id, " _
                    & "  a.geninstfd_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.geninstfd_ptnr_id, " _
                    & "  e.ptnr_code as sales_code, " _
                    & "  e.ptnr_name as sales_name, " _
                    & "  e.ptnr_sls_emp_cat as sales_name, " _
                    & "  a.geninstfd_soshipd_qty, " _
                    & "  a.geninstfd_sls_cat_id, " _
                    & "  c.emp_cat_name as sales_cat, " _
                    & "  a.geninstfd_year, " _
                    & "  a.geninstfd_qrtr_id, " _
                    & "  d.qrtr_name, " _
                    & "  a.geninstfd_planstgtd_amount, " _
                    & "  a.geninstfd_plans_instf, " _
                    & "  a.geninstfd_planstgtd_spc, " _
                    & "  a.geninstfd_achieve, " _
                    & "  a.geninstfd_pct_achieve, " _
                    & "  a.geninstfd_ttl_sls, " _
                    & "  a.geninstfd_ttl_disc, " _
                    & "  a.geninstfd_ttl_prc_fp, " _
                    & "  a.geninstfd_ttl_disc_fp, " _
                    & "  a.geninstfd_instf, " _
                    & "  a.geninstfd_ttl_bbt, " _
                    & "  a.geninstfd_ttl_cat, " _
                    & "  a.geninstfd_avg_cat, " _
                    & "  a.geninstfd_tax, " _
                    & "  a.geninstfd_payout " _
                    & "FROM " _
                    & "  public.geninstfd_det a " _
                    & "  INNER JOIN public.en_mstr b ON (a.geninstfd_en_id = b.en_id) " _
                    & "  INNER JOIN public.sls_emp_cat c ON (a.geninstfd_sls_cat_id = c.emp_cat_id) " _
                    & "  INNER JOIN public.qrtr_mstr d ON (a.geninstfd_qrtr_id = d.qrtr_id) " _
                    & "  INNER JOIN public.ptnr_mstr e ON (a.geninstfd_ptnr_id = e.ptnr_id) " _
                    & "WHERE " _
                    & "  a.geninstfd_geninstf_oid IS NULL"

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

        Dim _geninstf_oid As Guid
        _geninstf_oid = Guid.NewGuid

        sSQLs.Clear()

        Dim _code As String
        _code = func_coll.get_transaction_number("INS", geninstf_en_id.GetColumnValue("en_code"), "geninstf_mstr", "geninstf_code")

        'Dim _ro_id As Integer
        '_ro_id = SetInteger(func_coll.GetID("fcs_mstr", fcs_en_id.GetColumnValue("en_code"), "ro_id", "ro_en_id", fcs_en_id.EditValue.ToString))
        Dim _geninstf_year As Integer = Convert.ToDateTime(geninstf_qrtr_year.EditValue).Year

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO public.geninstf_mstr " _
                                            & "( " _
                                            & "  geninstf_oid, " _
                                            & "  geninstf_dom_id, " _
                                            & "  geninstf_en_id, " _
                                            & "  geninstf_all_child, " _
                                            & "  geninstf_code, " _
                                            & "  geninstf_fsn_oid, " _
                                            & "  geninstf_qrtr_code, " _
                                            & "  geninstf_qrtr_periode, " _
                                            & "  geninstf_qrtr_id, " _
                                            & "  geninstf_qrtr_start_date, " _
                                            & "  geninstf_qrtr_end_date, " _
                                            & "  geninstf_qrtr_year, " _
                                            & "  geninstf_dt, " _
                                            & "  geninstf_remarks, " _
                                            & "  geninstf_add_by, " _
                                            & "  geninstf_add_date " _
                                            & ") " _
                                            & "VALUES ( " _
                                            & SetSetring(_geninstf_oid.ToString) & ", " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ", " _
                                            & SetInteger(geninstf_en_id.EditValue) & ", " _
                                            & SetBitYN(geninstf_all_child.EditValue) & ", " _
                                            & SetSetring(_code) & ", " _
                                            & SetSetring(geninstf_fsn_oid.Tag) & ", " _
                                            & SetSetring(geninstf_qrtr_code.Tag) & ", " _
                                            & SetSetring(geninstf_qrtr_periode.Tag) & ", " _
                                            & SetInteger(geninstf_qrtr_id.Tag) & ", " _
                                            & SetDate(geninstf_qrtr_start_date.DateTime) & ", " _
                                            & SetDate(geninstf_qrtr_end_date.DateTime) & ", " _
                                            & SetInteger(_geninstf_year) & ", " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                                            & SetSetring(geninstf_remarks.EditValue) & ", " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                            & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.geninstfd_det " _
                                        & "( " _
                                        & "  geninstfd_oid, " _
                                        & "  geninstfd_geninstf_oid, " _
                                        & "  geninstfd_genfsn_oid, " _
                                        & "  geninstfd_seq, " _
                                        & "  geninstfd_si_id, " _
                                        & "  geninstfd_en_id, " _
                                        & "  geninstfd_ptnr_id, " _
                                        & "  geninstfd_year, " _
                                        & "  geninstfd_sls_cat_id, " _
                                        & "  geninstfd_qrtr_id, " _
                                        & "  geninstfd_planstgtd_amount, " _
                                        & "  geninstfd_plans_instf, " _
                                        & "  geninstfd_soshipd_qty, " _
                                        & "  geninstfd_planstgtd_spc, " _
                                        & "  geninstfd_achieve, " _
                                        & "  geninstfd_pct_achieve, " _
                                        & "  geninstfd_ttl_sls, " _
                                        & "  geninstfd_ttl_disc, " _
                                        & "  geninstfd_ttl_prc_fp, " _
                                        & "  geninstfd_ttl_disc_fp, " _
                                        & "  geninstfd_instf, " _
                                        & "  geninstfd_ttl_bbt, " _
                                        & "  geninstfd_ttl_cat, " _
                                        & "  geninstfd_avg_cat, " _
                                        & "  geninstfd_tax, " _
                                        & "  geninstfd_payout " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_geninstf_oid.ToString) & ",  " _
                                        & SetSetring(geninstf_fsn_oid.Tag) & ",  " _
                                        & SetInteger(i) & ", " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_si_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_en_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ptnr_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_year")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_sls_cat_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_qrtr_id")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_planstgtd_amount")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_plans_instf")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_soshipd_qty")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_planstgtd_spc")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_achieve")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_pct_achieve")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ttl_sls")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ttl_disc")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ttl_prc_fp")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ttl_disc_fp")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_instf")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ttl_bbt")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_ttl_cat")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_avg_cat")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_tax")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("geninstfd_payout")) & "  " _
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
                        set_row(Trim(_geninstf_oid.ToString), "geninstf_oid")
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

        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        If SetString(geninstf_qrtr_year.EditValue) = "" Then
            Box("Year can't empt")
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
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False
        'If MyBase.edit_data = True Then
        '    genpr_en_id.Focus()

        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _ro_oid_mstr = .Item("fcs_oid")
        '        genpr_en_id.EditValue = .Item("fcs_en_id")
        '        genpr_qrtr_id.EditValue = .Item("fcs_qrtr_id")
        '        genpr_remarks.EditValue = .Item("fcs_remarks")
        '        genpr_year.EditValue = .Item("fcs_year")
        '    End With

        '    ds_edit = New DataSet
        '    'ds_update_related = New DataSet
        '    Try
        '        Using objcb As New master_new.WDABasepgsql("", "")
        '            With objcb
        '                .SQL = "SELECT  " _
        '                    & "  a.fcsd_oid, " _
        '                    & "  a.fcsd_fcs_oid, " _
        '                    & "  a.fcsd_pt_id, " _
        '                    & "  b.pt_code, " _
        '                    & "  b.pt_desc1, " _
        '                    & "  b.pt_desc2, " _
        '                    & "  d.gr_name, " _
        '                    & "  a.fcsd_01_amount, " _
        '                    & "  a.fcsd_02_amount, " _
        '                    & "  a.fcsd_03_amount, " _
        '                    & "  a.fcsd_total_amount, " _
        '                    & "  a.fcsd_buffer_amount, " _
        '                    & "  a.fcsd_seq " _
        '                    & "FROM " _
        '                    & "  public.fcsd_det a " _
        '                    & "  INNER JOIN public.pt_mstr b ON (a.fcsd_pt_id = b.pt_id) " _
        '                    & "  INNER JOIN public.ptgr_mstr c ON (b.pt_id = c.ptgr_pt_id) " _
        '                    & "  INNER JOIN public.gr_mstr d ON (c.ptgr_gr_id = d.gr_id) " _
        '                    & "WHERE " _
        '                    & "  a.fcsd_fcs_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fcs_oid").ToString & "' " _
        '                    & "ORDER BY " _
        '                    & "  a.fcsd_seq"


        '                .InitializeCommand()
        '                .FillDataSet(ds_edit, "detail_upd")

        '                gc_edit.DataSource = ds_edit.Tables("detail_upd")
        '                gv_edit.BestFitColumns()
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try

        '    edit_data = True
        'End If
    End Function

    Public Overrides Function edit()
        Dim i As Integer

        edit = True
        sSQLs.Clear()

        Try
            'Using objinsert As New master_new.WDABasepgsql("", "")
            '    With objinsert
            '        .Connection.Open()
            '        Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
            '        Try
            '            .Command = .Connection.CreateCommand
            '            .Command.Transaction = sqlTran

            '            .Command.CommandType = CommandType.Text
            '            .Command.CommandText = "UPDATE  " _
            '                        & "  public.fcs_mstr   " _
            '                        & "SET  " _
            '                        & "  fcs_en_id = " & SetInteger(geninstf_en_id.EditValue) & ",  " _
            '                        & "  fcs_remarks = " & SetSetring(geninstf_remarks.EditValue) & ",  " _
            '                        & "  fcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '                        & "  fcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
            '                        & "  fcs_qrtr_id = " & SetInteger(geninstf_qrtr_id.EditValue) & ",  " _
            '                        & "  fcs_year = " & SetInteger(geninstf_qrtr_year.EditValue) & "  " _
            '                        & "WHERE  " _
            '                        & "  fcs_oid = " & SetSetring(_ro_oid_mstr) & " "

            '            sSQLs.Add(.Command.CommandText)
            '            .Command.ExecuteNonQuery()
            '            .Command.Parameters.Clear()

            '            .Command.CommandType = CommandType.Text
            '            .Command.CommandText = "delete from fcsd_det where fcsd_fcs_oid = '" + _ro_oid_mstr + "'"
            '            sSQLs.Add(.Command.CommandText)
            '            .Command.ExecuteNonQuery()
            '            .Command.Parameters.Clear()
            '            '******************************************************

            '            'Insert dan update data detail
            '            For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1
            '                .Command.CommandType = CommandType.Text
            '                .Command.CommandText = "INSERT INTO  " _
            '                                & "  public.fcsd_det " _
            '                                & "( " _
            '                                & "  fcsd_oid, " _
            '                                & "  fcsd_fcs_oid, " _
            '                                & "  fcsd_pt_id, " _
            '                                & "  fcsd_01_amount, " _
            '                                & "  fcsd_02_amount, " _
            '                                & "  fcsd_03_amount, " _
            '                                & "  fcsd_total_amount, " _
            '                                & "  fcsd_buffer_amount, " _
            '                                & "  fcsd_seq " _
            '                                & ")  " _
            '                                & "VALUES ( " _
            '                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '                                & SetSetring(_ro_oid_mstr.ToString) & ",  " _
            '                                & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_pt_id")) & ",  " _
            '                                & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_01_amount"))) & ",  " _
            '                                & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_02_amount"))) & ",  " _
            '                                & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_03_amount"))) & ",  " _
            '                                & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_total_amount"))) & ",  " _
            '                                & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_buffer_amount"))) & ",  " _
            '                                & SetInteger(i) & "  " _
            '                                & ")"
            '                sSQLs.Add(.Command.CommandText)
            '                .Command.ExecuteNonQuery()
            '                .Command.Parameters.Clear()
            '            Next

            '            If master_new.PGSqlConn.status_sync = True Then
            '                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
            '                    .Command.CommandType = CommandType.Text
            '                    .Command.CommandText = Data
            '                    .Command.ExecuteNonQuery()
            '                    .Command.Parameters.Clear()
            '                Next
            '            End If

            '            sqlTran.Commit()
            '            after_success()
            '            set_row(Trim(_ro_oid_mstr.ToString), "fcs_oid")
            '            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            '            edit = True
            '        Catch ex As NpgsqlException
            '            sqlTran.Rollback()
            '            MessageBox.Show(ex.Message)
            '        End Try
            '    End With
            'End Using
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
                            .Command.CommandText = "delete from geninstf_mstr where geninstf_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("geninstf_oid").ToString + "'"
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
        Dim _rod_en_id As Integer = geninstf_en_id.EditValue

        'If _col = "wc_desc" Then
        '    Dim frm As New FWCSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "code_name" Then
        '    Dim frm As New FToolSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "ptnr_name" Then
        '    Dim frm As New FPartnerSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub

    'Private Sub geninstf_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles geninstf_ptnr_id.ButtonClick
    '    Dim frm As New FPSalesPersonSearch
    '    frm.set_win(Me)
    '    frm._en_id = geninstf_en_id.EditValue
    '    'frm._obj = geninstf_periode_id
    '    'frm._code = geninstf_qrtr_code_id.EditValue
    '    'frm._pil = 1
    '    frm.type_form = True
    '    frm.ShowDialog()
    '    BtGen.PerformClick()
    'End Sub

    Private Sub geninstf_fsn_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles geninstf_fsn_oid.ButtonClick
        Dim frm As New FFSNItemSearch
        frm.set_win(Me)
        frm._en_id = geninstf_en_id.EditValue
        frm._obj = geninstf_fsn_oid
        'frm._code = geninstf_periode_id
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

    Private Sub plans_periode_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles geninstf_qrtr_id.ButtonClick
        Dim frm As New FPeriodeSearch
        frm.set_win(Me)
        'frm._en_id = geninstf_en_id.EditValue
        frm._obj = geninstf_qrtr_code.EditValue
        frm._code = geninstf_qrtr_periode.Tag
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub


    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try
            If SetString(geninstf_qrtr_year.EditValue) = "" Then
                Box("Year can't be empty")
                Exit Sub
            End If

            Dim _en_id_all As String

            If geninstf_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(geninstf_en_id.EditValue)
            Else
                _en_id_all = geninstf_en_id.EditValue
            End If

            Dim sSQL As String
            sSQL = "WITH filtered_data AS ( " _
                & "    SELECT " _
                & "        si_desc, " _
                & "        en_id, " _
                & "        en_desc, " _
                & "        sales_mstr.ptnr_id AS sales_id, " _
                & "        sales_mstr.ptnr_code AS sales_code, " _
                & "        sales_mstr.ptnr_name AS sales_name, " _
                & "        sales_mstr.ptnr_sls_emp_cat AS sales_cat_id, " _
                & "        emp_cat_name AS sales_cat, " _
                & "        plans_sales_id, " _
                & "        plans_qrtr_years, " _
                & "        planstgtd_qrtr_id, " _
                & "        planstgtd_amount, " _
                & "        plans_insentif, " _
                & "        planstgtd_spc, " _
                & "        soshipd_qty, " _
                & "        soshipd_qty * -1 AS qty_neg, " _
                & "        soshipd_qty * -1 * sod_price AS sales_ttl, " _
                & "        soshipd_qty * -1 * sod_price * sod_disc AS disc_value, " _
                & "        sod_price, " _
                & "        sod_disc, " _
                & "        sod_tax_inc, " _
                & "        sod_cost, " _
                & "        genfsnd_fsn_name, " _
                & "        CASE " _
                & "            WHEN sod_tax_inc = 'N' THEN soshipd_qty * -1 * sod_price " _
                & "            WHEN sod_tax_inc = 'Y' THEN soshipd_qty * -1 * sod_price * 100.0 / 110.0 " _
                & "        END AS price_fp, " _
                & "        CASE " _
                & "            WHEN sod_tax_inc = 'N' THEN soshipd_qty * -1 * sod_price * sod_disc " _
                & "            WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * -1 * sod_price * 100.0 / 110.0) * sod_disc " _
                & "        END AS disc_fp " _
                & "    FROM public.soship_mstr " _
                & "    INNER JOIN soshipd_det ON soshipd_soship_oid = soship_oid " _
                & "    INNER JOIN so_mstr ON so_oid = soship_so_oid " _
                & "    INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
                & "    INNER JOIN ptnr_mstr sales_mstr ON sales_mstr.ptnr_id = so_sales_person " _
                & "    INNER JOIN en_mstr ON en_id = soship_en_id " _
                & "    INNER JOIN si_mstr ON si_id = soship_si_id " _
                & "    INNER JOIN ptnr_mstr ON ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                & "    INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
                & "    INNER JOIN code_mstr AS tax_mstr ON tax_mstr.code_id = sod_tax_class " _
                & "    INNER JOIN code_mstr pay_type ON pay_type.code_id = so_pay_type " _
                & "    INNER JOIN sls_emp_cat ON emp_cat_id = sales_mstr.ptnr_sls_emp_cat " _
                & "    LEFT JOIN ars_ship ON ars_soshipd_oid = soshipd_oid " _
                & "    LEFT JOIN ar_mstr ON ar_oid = ars_ar_oid " _
                & "    LEFT JOIN tia_ar ON tia_ar_oid = ar_oid " _
                & "    LEFT JOIN ti_mstr ON ti_oid = tia_ti_oid " _
                & "    INNER JOIN plans_mstr ON plans_sales_id = sales_mstr.ptnr_id " _
                & "    INNER JOIN planstgtd_det ON planstgtd_plans_oid = plans_oid " _
                & "    INNER JOIN public.genfsnd_det gd ON pt_id = gd.genfsnd_pt_id " _
                & "    INNER JOIN public.genfsn_mstr gm ON gd.genfsnd_genfsn_oid = gm.genfsn_oid " _
                & " WHERE soship_date >= " + SetDate(geninstf_qrtr_start_date.DateTime.Date) _
                & " AND soship_date <= " + SetDate(geninstf_qrtr_end_date.DateTime.Date) _
                & " AND planstgtd_qrtr_id = " & SetInteger(geninstf_qrtr_id.Tag) & " " _
                & " AND genfsnd_genfsn_oid = " & SetSetring(geninstf_fsn_oid.Tag) & " " _
                & " AND so_en_id IN ( " _
                & "    SELECT user_en_id " _
                & "    FROM tconfuserentity " _
                & "    WHERE userid = " + master_new.ClsVar.sUserID.ToString + ")), " _
                & "aggregated_data AS ( " _
                & "    SELECT " _
                & "        si_desc, " _
                & "        en_id, " _
                & "        en_desc, " _
                & "        sales_id, " _
                & "        sales_code, " _
                & "        sales_name, " _
                & "        sales_cat_id, " _
                & "        sales_cat, " _
                & "        plans_sales_id, " _
                & "        plans_qrtr_years, " _
                & "        planstgtd_qrtr_id, " _
                & "        planstgtd_amount, " _
                & "        plans_insentif, " _
                & "        planstgtd_spc, " _
                & "        SUM(CASE " _
                & "            WHEN sod_tax_inc = 'N' THEN qty_neg * sod_price - qty_neg * sod_price * sod_disc " _
                & "            WHEN sod_tax_inc = 'Y' THEN (qty_neg * sod_price * 100.0 / 110.0) - (qty_neg * sod_price * 100.0 / 110.0 * sod_disc) " _
                & "        END) AS sales_achievement, " _
                & "        SUM(qty_neg) AS soshipd_qty, " _
                & "        SUM(sales_ttl) AS total_sales, " _
                & "        SUM(disc_value) AS total_discount, " _
                & "        SUM(price_fp) AS total_price_fp, " _
                & "        SUM(disc_fp) AS total_disc_fp, " _
                & "        SUM(CASE " _
                & "            WHEN sod_disc > 0.4 AND genfsnd_fsn_name = 'Fast Moving' THEN 1 * 0.50 " _
                & "            WHEN sod_disc BETWEEN 0.36 AND 0.40 AND genfsnd_fsn_name = 'Fast Moving' THEN 1 * 0.75 " _
                & "            WHEN sod_disc BETWEEN 0.26 AND 0.35 AND genfsnd_fsn_name = 'Fast Moving' THEN 1 * 1 " _
                & "            WHEN sod_disc < 0.25 AND genfsnd_fsn_name = 'Fast Moving' THEN 1 * 1.25 " _
                & "            WHEN sod_disc > 0.4 AND genfsnd_fsn_name = 'Moving' THEN 1 * 0.75 " _
                & "            WHEN sod_disc BETWEEN 0.36 AND 0.40 AND genfsnd_fsn_name = 'Moving' THEN 1 * 1 " _
                & "            WHEN sod_disc BETWEEN 0.26 AND 0.35 AND genfsnd_fsn_name = 'Moving' THEN 1 * 1.25 " _
                & "            WHEN sod_disc < 0.25 AND genfsnd_fsn_name = 'Moving' THEN 1 * 1.25 " _
                & "            WHEN sod_disc > 0.4 AND genfsnd_fsn_name = 'Slow Moving' THEN 1 * 1 " _
                & "            WHEN sod_disc BETWEEN 0.36 AND 0.40 AND genfsnd_fsn_name = 'Slow Moving' THEN 1 * 1.25 " _
                & "            WHEN sod_disc BETWEEN 0.26 AND 0.35 AND genfsnd_fsn_name = 'Slow Moving' THEN 1 * 1.50 " _
                & "            WHEN sod_disc <= 0.25 AND genfsnd_fsn_name = 'Slow Moving' THEN 1 * 1.75 " _
                & "            WHEN sod_disc > 0.4 AND genfsnd_fsn_name = 'Death Stock' THEN 1 * 1.25 " _
                & "            WHEN sod_disc BETWEEN 0.36 AND 0.40 AND genfsnd_fsn_name = 'Death Stock' THEN 1 * 1.50 " _
                & "            WHEN sod_disc BETWEEN 0.26 AND 0.35 AND genfsnd_fsn_name = 'Death Stock' THEN 1 * 1.75 " _
                & "            WHEN sod_disc < 0.25 AND genfsnd_fsn_name = 'Death Stock' THEN 1 * 2 " _
                & "            ELSE 0 " _
                & "        END) as total_bobot, " _
                & "        SUM(CASE " _
                & "            WHEN genfsnd_fsn_name = 'Fast Moving' THEN 1 " _
                & "            WHEN genfsnd_fsn_name = 'Moving' THEN 1 " _
                & "            WHEN genfsnd_fsn_name = 'Slow Moving' THEN 1 " _
                & "            WHEN genfsnd_fsn_name = 'Death Stock' THEN 1 " _
                & "            ELSE 0 " _
                & "        END) as total_cat " _
                & "    FROM filtered_data " _
                & "    GROUP BY " _
                & "        si_desc, " _
                & "        sales_id, " _
                & "        sales_code, " _
                & "        sales_name, " _
                & "        sales_cat_id, " _
                & "        sales_cat, " _
                & "        en_desc, " _
                & "        plans_sales_id, " _
                & "        plans_qrtr_years, " _
                & "        planstgtd_qrtr_id, " _
                & "        planstgtd_amount, " _
                & "        plans_insentif, " _
                & "        planstgtd_spc, " _
                & "        en_id " _
                & "), " _
                & "final_data AS ( " _
                & "    SELECT *, " _
                & "        total_bobot / NULLIF(total_cat, 0) AS avg_bobot, " _
                & "        ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) AS peres, " _
                & "        CASE  " _
                & "            WHEN planstgtd_spc = 'Y' THEN  " _
                & "                CASE  " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) < 0.79 THEN 0 " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 0.8 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 0.89 THEN " _
                & "                        ROUND(plans_insentif * (total_bobot / NULLIF(total_cat, 0)), 2) * (ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) - 0.2) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 0.9 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 0.99 THEN " _
                & "                        ROUND(plans_insentif * (total_bobot / NULLIF(total_cat, 0)), 2) * (ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) + 0) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 1 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 1.249 THEN " _
                & "                        ROUND(plans_insentif * (total_bobot / NULLIF(total_cat, 0)), 2) * (ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) + 0.1) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 1.25 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 1.499 THEN " _
                & "                        ROUND(plans_insentif * (total_bobot / NULLIF(total_cat, 0)), 2) * (ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) + 0.1) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 1.5 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 1.699 THEN " _
                & "                        ROUND(plans_insentif * (total_bobot / NULLIF(total_cat, 0)), 2) * (ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) + 0.2) " _
                & "                END " _
                & "            WHEN planstgtd_spc = 'N' THEN  " _
                & "                CASE  " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 0.8 THEN ROUND(plans_insentif * (NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0)), 2) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 0.8 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 0.89 THEN ROUND(plans_insentif * (NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0)), 2) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 0.9 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 0.99 THEN ROUND(plans_insentif * (NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0)), 2) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 1 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 1.249 THEN ROUND(plans_insentif * (NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0)), 2) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 1.25 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 1.499 THEN ROUND(plans_insentif * (NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0)), 2) " _
                & "                    WHEN ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) >= 1.5 AND ROUND(NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0), 2) <= 1.699 THEN ROUND(plans_insentif * (NULLIF(sales_achievement, 0) / NULLIF(planstgtd_amount, 0)), 2) " _
                & "                END " _
                & "            ELSE 0 " _
                & "        END AS final_insentif " _
                & "    FROM aggregated_data " _
                & "), " _
                & "pph_data AS ( " _
                & "    SELECT *, " _
                & "           final_insentif * 0.025 AS pph_21, " _
                & "           final_insentif - (final_insentif * 0.025) AS payout " _
                & "    FROM final_data " _
                & ") " _
                & "SELECT DISTINCT * " _
                & "FROM pph_data " _
                & "ORDER BY en_id;"

            Dim dt_fs As New DataTable
            dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

            ds_edit.Tables(0).Rows.Clear()

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                '_row("geninstfd_si_id") = dr("si_id")
                _row("geninstfd_en_id") = dr("en_id")
                _row("en_desc") = dr("en_desc")
                _row("geninstfd_ptnr_id") = dr("sales_id")
                _row("sales_code") = dr("sales_code")
                _row("sales_name") = dr("sales_name")
                _row("geninstfd_sls_cat_id") = dr("sales_cat_id")
                _row("sales_cat") = dr("sales_cat")
                _row("geninstfd_year") = dr("plans_qrtr_years")
                _row("geninstfd_qrtr_id") = dr("planstgtd_qrtr_id")
                _row("geninstfd_planstgtd_amount") = dr("planstgtd_amount")
                _row("geninstfd_plans_instf") = dr("plans_insentif")
                _row("geninstfd_planstgtd_spc") = dr("planstgtd_spc")
                _row("geninstfd_achieve") = dr("sales_achievement")
                _row("geninstfd_soshipd_qty") = dr("soshipd_qty")
                _row("geninstfd_ttl_sls") = dr("total_sales")
                _row("geninstfd_ttl_disc") = dr("total_discount")
                _row("geninstfd_ttl_prc_fp") = dr("total_price_fp")
                _row("geninstfd_ttl_disc_fp") = dr("total_disc_fp")
                _row("geninstfd_ttl_bbt") = dr("total_bobot")
                _row("geninstfd_ttl_cat") = dr("total_cat")
                _row("geninstfd_avg_cat") = dr("avg_bobot")
                _row("geninstfd_pct_achieve") = dr("peres")
                _row("geninstfd_instf") = dr("final_insentif")
                _row("geninstfd_tax") = dr("pph_21")
                _row("geninstfd_payout") = dr("payout")

                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub GridView1_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs)
    '    Dim view As GridView = CType(sender, GridView)

    '    If e.Column.FieldName = "Clasification" Then
    '        Dim cellValue As Object = view.GetRowCellValue(e.RowHandle, e.Column)

    '        If cellValue IsNot Nothing AndAlso cellValue.ToString() = "High" Then
    '            e.Appearance.BackColor = Color.Red
    '            e.Appearance.ForeColor = Color.White
    '        End If
    '    End If
    'End Sub

End Class
