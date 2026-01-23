Imports Npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraGrid.Views.Base

Public Class FSalesProgramImportTarget
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim _sls_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList
    Public _geninstf_fsn_codes As String
    Dim _now As DateTime

    'Private Property inv_fsn_end_date As Object

    Private Sub FSalesProgramImportTarget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        'sls_years.EditValue = DateTime.Now
    End Sub

    Public Overrides Sub load_cb()
        init_le(sls_en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_years_mstr(sls_en_id.EditValue))
        sls_years.Properties.datasource = dt_bantu
        sls_years.Properties.displaymember = dt_bantu.Columns("code_name").ToString
        sls_years.Properties.valuemember = dt_bantu.Columns("code_id").ToString
        sls_years.itemindex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_qrtr_code_id())
        'sls_qrtr_code_id.Properties.DataSource = dt_bantu
        'sls_qrtr_code_id.Properties.DisplayMember = dt_bantu.Columns("qrtr_code_name").ToString
        'sls_qrtr_code_id.Properties.ValueMember = dt_bantu.Columns("qrtr_code_id").ToString
        'sls_qrtr_code_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "sls_oid", "sls_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Date", "sls_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Year", "sls_years", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Code", "sls_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "sls_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Desc", "sls_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Pricelist Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Pricelist Name", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "sls_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "sls_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Target Revenue", "sls_target_revenue", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Item Qty", "sls_target_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Simulated Disc", "sls_avg_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p2")
        add_column_copy(gv_master, "Simulated Mkt", "sls_bdgt_marketing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p2")
        add_column_copy(gv_master, "Simulated Incentive", "sls_bdgt_incentive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p2")
        add_column_copy(gv_master, "Cost", "sls_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Margin", "sls_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Gross Profit Margin", "sls_gpm", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p2")
        add_column_copy(gv_master, "Cost to Sale Ratio", "sls_ctsratio", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p2")
        add_column_copy(gv_master, "Remarks", "sls_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Approval Status", "sls_tran_id,", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "sls_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Stackable", "sls_stackable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Active", "sls_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "sls_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sls_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sls_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sls_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "slsprogrls_slsprg_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Desc", "pt_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Min Qty", "slsprogrls_min_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_detail, "Max Qty", "slsprogrls_max_q", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_detail, "Min Purch", "slsprogrls_min_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_detail, "Max Purch", "slsprogrls_max_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_detail, "Membership", "slsprogrls_member_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Unit", "slsprogrls_ui_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_copy(gv_detail, "Sales Unit Value", "slsprogrls_nilai_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_copy(gv_detail, "Lvl 0 Disc", "slsprogrls_disc_st0", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Lvl 0 inctv", "slsprogrls_insentif_st0", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_copy(gv_detail, "Lvl 1 Disc", "slsprogrls_disc_st1", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Lvl 1 inctv", "slsprogrls_insentif_st1", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_copy(gv_detail, "Lvl 2 Disc", "slsprogrls_disc_st2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Lvl 2 inctv", "slsprogrls_insentif_st2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_copy(gv_detail, "Yrl Ptnr Reward", "slsprogrls_reward_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Yrl Sls Reward", "slsprogrls_reward_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Prn Commision", "slsprogrls_komisi_parent", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column(gv_edit, "slsprogrls_oid", False)
        add_column(gv_edit, "slsprogrls_slsprg_oid", False)
        add_column_edit(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Desc", "pt_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Min Qty", "slsprogrls_min_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Max Qty", "slsprogrls_max_q", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Min Purch", "slsprogrls_min_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Max Purch", "slsprogrls_max_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Membership", "slsprogrls_member_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Sales Unit", "slsprogrls_nilai_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Sales Unit Value", "slsprogrls_nilai_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Lvl 0 Disc", "slsprogrls_disc_st0", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Lvl 0 inctv", "slsprogrls_insentif_st0", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Lvl 1 Disc", "slsprogrls_disc_st1", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Lvl 1 inctv", "slsprogrls_insentif_st1", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Lvl 2 Disc", "slsprogrls_disc_st2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Lvl 2 inctv", "slsprogrls_insentif_st2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")
        add_column_edit(gv_edit, "Yrl Ptnr Reward", "slsprogrls_reward_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Yrl Sls Reward", "slsprogrls_reward_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Prn Commision", "slsprogrls_komisi_parent", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                        & "  a.sls_oid, " _
                        & "  a.sls_id, " _
                        & "  a.sls_en_id, " _
                        & "  b.en_desc, " _
                        & "  a.sls_years, " _
                        & "  a.sls_code, " _
                        & "  a.sls_name, " _
                        & "  a.sls_desc, " _
                        & "  a.sls_pi_id, " _
                        & "  c.pi_code, " _
                        & "  c.pi_desc, " _
                        & "  a.sls_start_date, " _
                        & "  a.sls_end_date, " _
                        & "  a.sls_target_revenue, " _
                        & "  a.sls_target_qty, " _
                        & "  a.sls_avg_disc, " _
                        & "  a.sls_bdgt_marketing, " _
                        & "  a.sls_bdgt_incentive, " _
                        & "  a.sls_cost, " _
                        & "  a.sls_margin, " _
                        & "  a.sls_gpm, " _
                        & "  a.sls_ctsratio, " _
                        & "  a.sls_remarks, " _
                        & "  a.sls_tran_id, " _
                        & "  a.sls_status, " _
                        & "  a.sls_stackable, " _
                        & "  a.sls_active, " _
                        & "  a.sls_add_by, " _
                        & "  a.sls_add_date, " _
                        & "  a.sls_upd_by, " _
                        & "  a.sls_upd_date " _
                        & "FROM " _
                        & "  public.sls_program a " _
                        & "  INNER JOIN public.en_mstr b ON (a.sls_en_id = b.en_id) " _
                        & "  LEFT OUTER JOIN public.pi_mstr c ON (a.sls_pi_id = c.pi_id) " _
                        & "WHERE " _
                        & " a.sls_en_id in (select user_en_id from tconfuserentity " _
                        & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") "

        get_sequel += "   ORDER BY " _
                & "  a.sls_date"

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
                & "  a.slsprogrls_oid, " _
                & "  a.slsprogrls_id, " _
                & "  a.slsprogrls_slsprg_oid, " _
                & "  a.slsprogrls_pt_id, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  a.slsprogrls_min_qty, " _
                & "  a.slsprogrls_max_q, " _
                & "  a.slsprogrls_min_total, " _
                & "  a.slsprogrls_max_total, " _
                & "  a.slsprogrls_member_id, " _
                & "  d.ptnrg_code, " _
                & "  d.ptnrg_name, " _
                & "  d.ptnrg_desc, " _
                & "  a.slsprogrls_disc_st0, " _
                & "  a.slsprogrls_insentif_st0, " _
                & "  a.slsprogrls_disc_st1, " _
                & "  a.slsprogrls_insentif_st1, " _
                & "  a.slsprogrls_disc_st2, " _
                & "  a.slsprogrls_insentif_st2, " _
                & "  a.slsprogrls_ui_value, " _
                & "  a.slsprogrls_nilai_unit, " _
                & "  a.slsprogrls_reward_mitra, " _
                & "  a.slsprogrls_reward_sales, " _
                & "  a.slsprogrls_komisi_parent, " _
                & "  a.slsprogrls_stackable_disc " _
                & "FROM " _
                & "  public.sls_program_rules a " _
                & "  LEFT OUTER JOIN public.sls_program b ON (a.slsprogrls_slsprg_oid = b.sls_oid) " _
                & "  LEFT OUTER JOIN public.pt_mstr c ON (a.slsprogrls_id = c.pt_id) " _
                & "  LEFT OUTER JOIN public.ptnrg_grp d ON (a.slsprogrls_member_id = d.ptnrg_id) " _
                & "WHERE " _
                & "  a.slsprogrls_slsprg_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.slsprogrls_id"


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
        sls_en_id.EditValue = ""

        'sls_years.DateTime = CekTanggal()

        sls_remarks.EditValue = ""
        sls_en_id.Focus()

        'sls_multilpier.Text = "'1'"
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

        'sls_code.Tag = ""
        'sls_code.Text = ""

        sls_name.Tag = ""
        sls_name.Text = ""

        sls_desc.Tag = ""
        sls_desc.Text = ""

        sls_total_cost.Tag = ""
        sls_total_cost.Text = ""

        'sls_multilpier.Text = "1.0"
        'sls_multilpier.EditValue = "1.0"

        sls_target_revenue.Text = "0.0"
        sls_target_revenue.EditValue = "0.0"

        sls_target_qty.Text = "0.0"
        sls_target_qty.EditValue = "0.0"

        sls_margin.Text = "0.0"
        sls_margin.EditValue = "0.0"

        sls_total_cost.Text = "0.0"
        sls_total_cost.EditValue = "0.0"

        'sls_bdgt_marketing.Text = "0.0"
        'sls_bdgt_marketing.EditValue = "0.0"

        'sls_bdgt_incentive.Text = "0.0"
        'sls_bdgt_incentive.EditValue = "0.0"

        

        sls_years.EditValue = ""
        sls_years.Text = ""
        sls_years.Tag = ""

        'sls_roi.Text = ""
        'sls_roi.Tag = ""

        ''sls_amount_tgt.Text = ""
        ''sls_insentif.Text = ""
        'sls_achieved_qty.Text = ""
        sls_remarks.Text = ""
        'sls_qrtr_periode_id.Tag = ""
        'sls_qrtr_periode_id.Text = ""

        sls_start_date.DateTime = CekTanggal()
        sls_end_date.DateTime = CekTanggal()
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
                        & "  a.slsprogrls_oid, " _
                        & "  a.slsprogrls_id, " _
                        & "  a.slsprogrls_slsprg_oid, " _
                        & "  a.slsprogrls_pt_id, " _
                        & "  a.slsprogrls_min_qty, " _
                        & "  a.slsprogrls_max_q, " _
                        & "  a.slsprogrls_min_total, " _
                        & "  a.slsprogrls_max_total, " _
                        & "  a.slsprogrls_member_id, " _
                        & "  a.slsprogrls_disc_st0, " _
                        & "  a.slsprogrls_insentif_st0, " _
                        & "  a.slsprogrls_disc_st1, " _
                        & "  a.slsprogrls_insentif_st1, " _
                        & "  a.slsprogrls_disc_st2, " _
                        & "  a.slsprogrls_insentif_st2, " _
                        & "  a.slsprogrls_nilai_unit, " _
                        & "  a.slsprogrls_reward_mitra, " _
                        & "  a.slsprogrls_reward_sales, " _
                        & "  a.slsprogrls_komisi_parent, " _
                        & "  a.slsprogrls_stackable_disc " _
                        & "FROM " _
                        & "  public.sls_program_rules a " _
                        & "WHERE " _
                        & "  a.slsprogrls_oid IS NULL"

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

        Dim _sls_oid As Guid
        _sls_oid = Guid.NewGuid
        Dim _total As Double

        sSQLs.Clear()

        Dim _code As String
        '_code = func_coll.get_transaction_number("PLA", sls_en_id.GetColumnValue("en_code"), "sls_mstr", "sls_code")

        _code = func_coll.get_transaction_number("SPR", sls_en_id.GetColumnValue("en_code"), "so_mstr", "so_code")
        sSQLs.Clear()

        '_code = GetNewNumberYM("sls_mstr", "sls_code", 5, "SPR" & sls_en_id.GetColumnValue("en_code") _
        '                             & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        'Dim _ro_id As Integer
        '_ro_id = SetInteger(func_coll.GetID("fcs_mstr", fcs_en_id.GetColumnValue("en_code"), "ro_id", "ro_en_id", fcs_en_id.EditValue.ToString))
        'Dim sls_qrtr_years As Integer = Convert.ToDateTime(sls_qrtr_years.EditValue).Year

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
                                        & "    public.sls_program " _
                                        & " ( " _
                                        & "    sls_oid, " _
                                        & "    sls_en_id, " _
                                        & "    sls_years, " _
                                        & "    sls_code, " _
                                        & "    sls_name, " _
                                        & "    sls_desc, " _
                                        & "    sls_pi_id, " _
                                        & "    sls_start_date, " _
                                        & "    sls_end_date, " _
                                        & "    sls_target_revenue, " _
                                        & "    sls_target_qty, " _
                                        & "    sls_avg_disc, " _
                                        & "    sls_bdgt_marketing, " _
                                        & "    sls_bdgt_incentive, " _
                                        & "    sls_cost, " _
                                        & "    sls_margin, " _
                                        & "    sls_gpm, " _
                                        & "    sls_ctsratio, " _
                                        & "    sls_remarks, " _
                                        & "    sls_tran_id, " _
                                        & "    sls_status, " _
                                        & "    sls_stackable, " _
                                        & "    sls_active, " _
                                        & "    sls_add_by, " _
                                        & "    sls_add_date " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_sls_oid) & ",  " _
                                        & SetInteger(sls_en_id.EditValue) & ",  " _
                                        & SetInteger(sls_years.EditValue) & ",  " _
                                        & SetSetring(_code) & ",  " _
                                        & SetSetring(sls_name.Text) & ",  " _
                                        & SetSetring(sls_desc.Text) & ",  " _
                                        & SetInteger(sls_pi_id.Tag) & ",  " _
                                        & SetDate(sls_start_date.DateTime) & ",  " _
                                        & SetDate(sls_end_date.EditValue) & ",  " _
                                        & SetDec(sls_target_revenue.EditValue) & ",  " _
                                        & SetDec(sls_target_qty.EditValue) & ",  " _
                                        & SetDec(sls_avg_disc.EditValue) & ",  " _
                                        & SetDec(sls_bdgt_marketing.EditValue) & ",  " _
                                        & SetDec(sls_bdgt_incentive.EditValue) & ",  " _
                                        & SetDec(sls_total_cost.EditValue) & ",  " _
                                        & SetDec(sls_margin.EditValue) & ",  " _
                                        & SetDec(sls_gpm.EditValue) & ",  " _
                                        & SetDec(sls_ctsratio.EditValue) & ",  " _
                                        & SetSetring(sls_remarks.Text) & ",  " _
                                        & SetSetring(sls_tran_id.EditValue) & ",  " _
                                        & SetSetring(sls_status.EditValue) & ",  " _
                                        & IIf(sls_stackable.Checked, "TRUE", "FALSE") & ",  " _
                                        & SetBitYN(sls_active.Checked) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                        & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sls_program_rules " _
                                            & "( " _
                                            & " slsprogrls_oid, " _
                                            & " slsprogrls_slsprg_oid, " _
                                            & " slsprogrls_pt_id, " _
                                            & " slsprogrls_min_qty, " _
                                            & " slsprogrls_max_q, " _
                                            & " slsprogrls_min_total, " _
                                            & " slsprogrls_max_total, " _
                                            & " slsprogrls_member_id, " _
                                            & " slsprogrls_disc_st0, " _
                                            & " slsprogrls_insentif_st0, " _
                                            & " slsprogrls_disc_st1, " _
                                            & " slsprogrls_insentif_st1, " _
                                            & " slsprogrls_disc_st2, " _
                                            & " slsprogrls_insentif_st2, " _
                                            & " slsprogrls_nilai_unit, " _
                                            & " slsprogrls_reward_mitra, " _
                                            & " slsprogrls_reward_sales, " _
                                            & " slsprogrls_komisi_parent " _
                                            & " ) " _
                                            & "VALUES ( " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("slsprogrls_oid")) & ",  " _
                                            & SetSetring(_sls_oid.ToString) & ",  " _
                                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("slsprogrls_pt_id")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_min_qty")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_max_q")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_min_total")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_max_total")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_disc_st0")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_insentif_st0")) & ",  " _
                                            & SetDec(ds_edit.Tables(0).Rows(i).Item("slsprogrls_disc_st1")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_insentif_st2")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_disc_st2")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_insentif_st2")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_nilai_unit")) & ",  " _
                                            & SetDec(ds_edit.Tables(0).Rows(i).Item("slsprogrls_disc_st1")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_reward_mitra")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_reward_sales")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("slsprogrls_komisi_parent")) & ",  " _
                                            & ")"
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
                        after_success()
                        set_row(Trim(_sls_oid.ToString), "sls_oid")
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

        If SetString(sls_bdgt_incentive.EditValue) = "" Then
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
            sls_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _sls_oid_mstr = .Item("sls_oid").ToString
                sls_en_id.EditValue = .Item("sls_en_id")
                'sls_date.EditValue = .Item("sls_date")

                sls_years.Tag = SetString(.Item("sls_qrtr_years"))
                sls_years.Text = SetString(.Item("sls_qrtr_years"))

                '_sls_code = .Item("sls_oid.Tosting").ToString

                'sls_event_id.EditValue = .Item("sls_sales_id")

                sls_years.EditValue = .Item("sls_qrtr_years")

                'sls_event_id.Tag = SetString(.Item("sls_sales_id"))
                'sls_event_id.Text = SetString(.Item("ptnr_name"))

                'sls_code.Text = SetString(.Item("sls_emp_nik"))

                sls_name.Tag = SetString(.Item("sls_emp_pos_id"))
                sls_name.Text = SetString(.Item("emp_pos_name"))

                sls_desc.Tag = SetString(.Item("sls_emp_cat_id"))
                sls_desc.Text = SetString(.Item("emp_cat_name"))

                sls_total_cost.Tag = SetString(.Item("sls_emp_lvl_id"))
                sls_total_cost.Text = SetString(.Item("emp_lvl_name"))

                sls_margin.Tag = SetString(.Item("sls_emp_parent_id"))
                sls_margin.Text = SetString(.Item("sales_parent"))

                sls_target_qty.Text = SetString(.Item("sls_amount_tgt"))
                sls_target_revenue.Text = SetString(.Item("sls_insentif"))
                sls_bdgt_incentive.Text = SetString(.Item("sls_amount_total"))

                'sls_qrtr_code_id.EditValue = .Item("sls_qrtr_code_id")

                'sls_ce_special.EditValue = SetBitYNB(.Item("sls_ce_special"))
                'sls_ce_final.EditValue = SetBitYNB(.Item("sls_ce_final"))
                'sls_ce_special.EditValue = SetBitYNB(.Item("sls_ce_special"))
            End With

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                    & "  a.slsprogrls_oid, " _
                                    & "  a.slsprogrls_sls_oid, " _
                                    & "  a.slsprogrls_seq, " _
                                    & "  a.slsprogrls_year, " _
                                    & "  a.slsprogrls_qrtr_id, " _
                                    & "  b.qrtr_name, " _
                                    & "  a.slsprogrls_qrtr_start_date, " _
                                    & "  a.slsprogrls_qrtr_end_date, " _
                                    & "  a.slsprogrls_amount, " _
                                    & "  a.slsprogrls_netto_amount, " _
                                    & "  a.slsprogrls_netto_amount as netto_amount_old, " _
                                    & "  a.slsprogrls_spc, " _
                                    & "  a.slsprogrls_parent " _
                                    & "FROM " _
                                    & "  public.sls_program_rules a " _
                                    & "  INNER JOIN public.qrtr_mstr b ON (a.slsprogrls_qrtr_id = b.qrtr_id) " _
                                    & "WHERE " _
                                    & "  a.slsprogrls_sls_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString & "' " _
                                    & "ORDER BY " _
                                    & "  a.slsprogrls_seq"

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
        'Dim i As Integer
        'Dim ds_bantu As New DataSet
        ''Dim oldNettoAmount As Double
        'Dim dt_pt As New DataTable
        'Dim _difference As Double
        '_difference = 0

        'edit = True
        'sSQLs.Clear()

        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran

        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "UPDATE  " _
        '                            & "  public.sls_mstr   " _
        '                            & "SET  " _
        '                            & "  sls_en_id = " & SetInteger(sls_en_id.EditValue) & ",  " _
        '                            & "  sls_remarks = " & SetSetring(sls_remarks.EditValue) & ",  " _
        '                            & "  sls_amount_total = " & SetDec(sls_achieved_qty.EditValue) & ",  " _
        '                            & "  sls_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                            & "  sls_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
        '                            & "  sls_qrtr_years = " & SetInteger(sls_years.Tag) & ",  " _
        '                            & "  sls_qrtr_code_id = " & SetInteger(sls_qrtr_code_id.Tag) & ",  " _
        '                            '& "  sls_ce_special = " & SetBitYN(sls_ce_special.EditValue) & ",  " _
        '                '& "  sls_ce_final = " & SetBitYN(sls_ce_final.EditValue) & "  " _
        '                            & "WHERE  " _
        '                            & "  sls_oid = " & SetSetring(_sls_oid_mstr) & " "

        '                sSQLs.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "delete from slsprogrls_det where slsprogrls_sls_oid = '" + _sls_oid_mstr + "'"
        '                sSQLs.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()
        '                '******************************************************

        '                'Insert dan update data detail
        '                For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1

        '                    ' Ambil nilai netto baru dari dataset
        '                    Dim newNettoAmount As Double = SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_netto_amount"))

        '                    ' Ambil nilai netto lama dari dataset
        '                    Dim oldNettoAmount As Double = SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("netto_amount_old"))

        '                    _difference = _difference + (newNettoAmount.ToString - oldNettoAmount.ToString)

        '                    .Command.CommandType = CommandType.Text
        '                    .Command.CommandText = "INSERT INTO  " _
        '                                        & "  public.slsprogrls_det " _
        '                                        & "( " _
        '                                        & "  slsprogrls_oid, " _
        '                                        & "  slsprogrls_sls_oid, " _
        '                                        & "  slsprogrls_seq, " _
        '                                        & "  slsprogrls_year, " _
        '                                        & "  slsprogrls_qrtr_id, " _
        '                                        & "  slsprogrls_qrtr_start_date, " _
        '                                        & "  slsprogrls_qrtr_end_date, " _
        '                                        & "  slsprogrls_amount, " _
        '                                        & "  slsprogrls_netto_amount, " _
        '                                        & "  slsprogrls_spc, " _
        '                                        & "  slsprogrls_parent " _
        '                                        & ")  " _
        '                                        & "VALUES ( " _
        '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                        & SetSetring(_sls_oid_mstr.ToString) & ",  " _
        '                                        & SetInteger(i) & ", " _
        '                                        & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_year")) & ",  " _
        '                                        & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_id")) & ",  " _
        '                                        & SetDate(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_start_date")) & ",  " _
        '                                        & SetDate(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_end_date")) & ",  " _
        '                                        & SetDbl(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_amount")) & ",  " _
        '                                        & SetDbl(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_netto_amount")) & ",  " _
        '                                        & SetSetring(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_spc")) & ",  " _
        '                                        & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_parent")) & "  " _
        '                                        & ")"
        '                    sSQLs.Add(.Command.CommandText)
        '                    .Command.ExecuteNonQuery()
        '                    .Command.Parameters.Clear()

        '                    If SetString(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_parent")) <> "" Then
        '                        Dim _parent_sales_Id As String = SetString(ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_parent"))

        '                        'Dim updateParentSQL As String = "UPDATE slsprogrls_det " _
        '                        '                                & "SET slsprogrls_netto_amount = " _
        '                        '                                & "  ( " _
        '                        '                                & "    SELECT DISTINCT pd.slsprogrls_netto_amount " _
        '                        '                                & "    FROM public.slsprogrls_det pd " _
        '                        '                                & "      INNER JOIN public.sls_mstr pm ON pd.slsprogrls_sls_oid = pm.sls_oid " _
        '                        '                                & "      INNER JOIN public.ptnr_mstr pt ON pm.sls_sales_id = pt.ptnr_id " _
        '                        '                                & "    WHERE pm.sls_sales_id = '" & _parent_sales_Id & "'" _
        '                        '                                & "      and pd.slsprogrls_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_year")) & "' " _
        '                        '                                & "      and pd.slsprogrls_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_id")) & "' " _
        '                        '                                & "    LIMIT 1 " _
        '                        '                                & ") + " & _difference & " " _
        '                        '                                & "WHERE slsprogrls_sls_oid = " _
        '                        '                                & "  ( " _
        '                        '                                & "    SELECT DISTINCT pd.slsprogrls_sls_oid " _
        '                        '                                & "    FROM public.slsprogrls_det " _
        '                        '                                & "      INNER JOIN public.sls_mstr ON (public.slsprogrls_det.slsprogrls_sls_oid = public.sls_mstr.sls_oid) " _
        '                        '                                & "    WHERE sls_sales_id = '" & _parent_sales_Id & "'" _
        '                        '                                & ") " _
        '                        '                                & "    and slsprogrls_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_year")) & "' " _
        '                        '                                & "    and slsprogrls_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_id")) & "' "

        '                        Dim updateParentSQL As String = "UPDATE public.slsprogrls_det " _
        '                                                        & "SET slsprogrls_netto_amount = ( " _
        '                                                        & "    SELECT pd.slsprogrls_netto_amount + '" & SetDec(_difference) & "' " _
        '                                                        & "    FROM public.slsprogrls_det pd " _
        '                                                        & "    INNER JOIN public.sls_mstr pm ON pd.slsprogrls_sls_oid = pm.sls_oid " _
        '                                                        & "    INNER JOIN public.ptnr_mstr pt ON pm.sls_sales_id = pt.ptnr_id " _
        '                                                        & "    WHERE pm.sls_sales_id = '" & _parent_sales_Id & "'" _
        '                                                        & "      AND pd.slsprogrls_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_year")) & "' " _
        '                                                        & "      AND pd.slsprogrls_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_id")) & "' " _
        '                                                        & "    LIMIT 1 " _
        '                                                        & ") " _
        '                                                        & "WHERE slsprogrls_oid = ( " _
        '                                                        & "    SELECT DISTINCT pd.slsprogrls_oid " _
        '                                                        & "    FROM public.slsprogrls_det pd " _
        '                                                        & "    INNER JOIN public.sls_mstr pm ON pd.slsprogrls_sls_oid = pm.sls_oid " _
        '                                                        & "    WHERE pm.sls_sales_id = '" & _parent_sales_Id & "'" _
        '                                                        & "AND slsprogrls_year = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_year")) & "' " _
        '                                                        & "AND slsprogrls_qrtr_id = '" & (ds_edit.Tables("detail_upd").Rows(i).Item("slsprogrls_qrtr_id")) & "' " _
        '                                                        & "    LIMIT 1 " _
        '                                                        & ") "


        '                        .Command.CommandType = CommandType.Text
        '                        .Command.CommandText = updateParentSQL
        '                        sSQLs.Add(updateParentSQL)
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()
        '                    End If

        '                Next

        '                If master_new.PGSqlConn.status_sync = True Then
        '                    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
        '                        .Command.CommandType = CommandType.Text
        '                        .Command.CommandText = Data
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()
        '                    Next
        '                End If

        '                sqlTran.Commit()
        '                after_success()
        '                set_row(Trim(_sls_oid_mstr.ToString), "sls_oid")
        '                dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        '                edit = True
        '            Catch ex As NpgsqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    edit = False
        '    MessageBox.Show(ex.Message)
        'End Try
        'Return edit
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
                            .Command.CommandText = "delete from sls_mstr where sls_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString + "'"
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

    'Private Sub gv_edit_disc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_disc.KeyDown
    '    If e.Control And e.KeyCode = Keys.I Then
    '        gv_edit.AddNewRow()
    '    ElseIf e.Control And e.KeyCode = Keys.D Then
    '        gv_edit.DeleteSelectedRows()
    '    End If
    'End Sub

    'Private Sub gv_edit_disc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_disc.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        browse_data()
    '    End If
    'End Sub

    'Private Sub gv_edit_disc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_disc.DoubleClick
    '    browse_data()
    'End Sub

    'Private Sub gv_edit_reward_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_reward.KeyDown
    '    If e.Control And e.KeyCode = Keys.I Then
    '        gv_edit.AddNewRow()
    '    ElseIf e.Control And e.KeyCode = Keys.D Then
    '        gv_edit.DeleteSelectedRows()
    '    End If
    'End Sub

    'Private Sub gv_edit_reward_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_reward.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        browse_data()
    '    End If
    'End Sub

    'Private Sub gv_edit_reward_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_reward.DoubleClick
    '    browse_data()
    'End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _rod_en_id As Integer = sls_en_id.EditValue

        If _col = "qrtr_name" Then
            Dim frm As New FPeriodeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _rod_en_id
            frm._code = sls_years.Text
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub sls_pi_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sls_pi_id.ButtonClick
        Dim frm As New FPriceListSearch
        frm.set_win(Me)
        frm._obj = sls_pi_id
        frm._en_id = sls_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    'Private Sub sls_pi_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Try

    '        Dim frm As New FPriceListSearch
    '        frm.set_win(Me)
    '        frm._obj = sls_pi_id
    '        frm._en_id = sls_en_id.EditValue
    '        frm.type_form = True
    '        frm.ShowDialog()

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
    '    'Try
    '    '    If ds.Tables(0).Rows.Count = 0 Then
    '    '        Exit Sub
    '    '    End If

    '    '    Dim sql As String = ""

    '    '    Try
    '    '        ds.Tables("detail").Clear()
    '    '    Catch ex As Exception
    '    '    End Try

    '    '    sql = "SELECT  " _
    '    '        & "  a.plansd_oid, " _
    '    '        & "  a.plansd_sls_oid, " _
    '    '        & "  a.plansd_ptnr_id, " _
    '    '        & "  a.plansd_amount, " _
    '    '        & "  a.plansd_seq, " _
    '    '        & "  b.ptnr_code, " _
    '    '        & "  b.ptnr_name " _
    '    '        & "FROM " _
    '    '        & "  public.plansd_det a " _
    '    '        & "  INNER JOIN public.ptnr_mstr b ON (a.plansd_ptnr_id = b.ptnr_id) " _
    '    '        & "WHERE " _
    '    '        & "  a.plansd_sls_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString & "' " _
    '    '        & "ORDER BY " _
    '    '        & "  a.plansd_seq"

    '    '    load_data_detail(sql, gc_detail_cust, "detail")

    '    '    Try
    '    '        ds.Tables("detail_pt").Clear()
    '    '    Catch ex As Exception
    '    '    End Try

    '    '    sql = "SELECT  " _
    '    '        & "  a.plansptd_oid, " _
    '    '        & "  a.plansptd_sls_oid, " _
    '    '        & "  a.plansptd_pt_id, " _
    '    '        & "  b.pt_code, " _
    '    '        & "  b.pt_desc1, " _
    '    '        & "  b.pt_desc2, " _
    '    '        & "  c.code_name AS um_desc, " _
    '    '        & "  a.plansptd_amount " _
    '    '        & "FROM " _
    '    '        & "  public.plansptd_det a " _
    '    '        & "  INNER JOIN public.pt_mstr b ON (a.plansptd_pt_id = b.pt_id) " _
    '    '        & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
    '    '        & "WHERE " _
    '    '        & "  a.plansptd_sls_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString & "' " _
    '    '        & "ORDER BY " _
    '    '        & "  b.pt_desc1"

    '    '    load_data_detail(sql, gc_detail_pt, "detail_pt")

    '    '    Try
    '    '        ds.Tables("detail_target").Clear()
    '    '    Catch ex As Exception
    '    '    End Try

    '    '    sql = "SELECT  " _
    '    '                & "  a.slsprogrls_qrtr_id, " _
    '    '                & "  b.qrtr_name, " _
    '    '                & "  a.slsprogrls_amount, " _
    '    '                & "  a.slsprogrls_spc, " _
    '    '                & "  a.slsprogrls_year, " _
    '    '                & "  a.slsprogrls_qrtr_start_date, " _
    '    '                & "  a.slsprogrls_qrtr_end_date, " _
    '    '                & "  a.slsprogrls_netto_amount, " _
    '    '                & "  a.slsprogrls_oid, " _
    '    '                & "  a.slsprogrls_sls_oid, " _
    '    '                & "  a.slsprogrls_seq, " _
    '    '                & "  a.slsprogrls_parent " _
    '    '                & "FROM public.slsprogrls_det a " _
    '    '                & "  INNER JOIN public.qrtr_mstr b ON (a.slsprogrls_qrtr_id = b.qrtr_id) " _
    '    '                & "WHERE " _
    '    '                & "  a.slsprogrls_sls_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString & "' " _
    '    '                & "ORDER BY " _
    '    '                & "  a.slsprogrls_seq"


    '    '    load_data_detail(sql, gc_detail, "detail")

    '    'Catch ex As Exception
    '    '    Pesan(Err)
    '    'End Try
    'End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub
    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "slsprogrls_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "slsprogrls_year", sls_years.Text)
            .SetRowCellValue(e.RowHandle, "slsprogrls_amount", 0)
            .SetRowCellValue(e.RowHandle, "slsprogrls_netto_amount", 0)
            .SetRowCellValue(e.RowHandle, "slsprogrls_spc", "N")
            .SetRowCellValue(e.RowHandle, "slsprogrls_parent", sls_years.EditValue)
            .BestFitColumns()
        End With
    End Sub

    Private Sub BtGenMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenMonth.Click
        'Try
        '    If sls_roi.EditValue <> "" Then
        '        Dim sSQL As String
        '        sSQL = "SELECT " _
        '            & "  b.qrtr_name, " _
        '            & "  b.qrtr_code_id, " _
        '            & "  b.qrtr_code_name, " _
        '            & "  b.qrtr_id, " _
        '            & "  b.qrtr_code, " _
        '            & "  b.qrtr_year, " _
        '            & "  b.qrtr_start_date, " _
        '            & "  b.qrtr_end_date " _
        '            & "FROM public.qrtr_mstr b " _
        '            & "WHERE b.qrtr_year = " & SetSetring(sls_years.Text) & " " _
        '            & "AND b.qrtr_code_id = " & SetInteger(sls_qrtr_code_id.EditValue) & " " _
        '            & "order by qrtr_id"

        '        Dim dt_fs As New DataTable
        '        dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

        '        ds_edit.Tables(0).Rows.Clear()

        '        For Each dr As DataRow In dt_fs.Rows
        '            ' Pastikan baris tidak diduplikasi
        '            Dim existingRows() As DataRow = ds_edit.Tables(0).Select("slsprogrls_qrtr_id = '" & dr("qrtr_id") & "' AND slsprogrls_parent = '" & sls_roi.Tag & "'")
        '            If existingRows.Length = 0 Then
        '                Dim _row As DataRow = ds_edit.Tables(0).NewRow()
        '                _row("slsprogrls_oid") = Guid.NewGuid.ToString
        '                _row("slsprogrls_year") = dr("qrtr_year")
        '                _row("slsprogrls_qrtr_id") = dr("qrtr_id")
        '                _row("qrtr_name") = dr("qrtr_name")
        '                _row("slsprogrls_qrtr_start_date") = dr("qrtr_start_date")
        '                _row("slsprogrls_qrtr_end_date") = dr("qrtr_end_date")
        '                _row("slsprogrls_spc") = "N"
        '                _row("slsprogrls_amount") = 0.0 ' Default nilai awal
        '                _row("slsprogrls_netto_amount") = 0.0 ' Default nilai awal
        '                _row("slsprogrls_parent") = sls_roi.Tag
        '                ds_edit.Tables(0).Rows.Add(_row)
        '            End If
        '        Next
        '    Else
        '        Dim sSQL As String
        '        sSQL = "SELECT " _
        '            & "  b.qrtr_name, " _
        '            & "  b.qrtr_code_id, " _
        '            & "  b.qrtr_code_name, " _
        '            & "  b.qrtr_id, " _
        '            & "  b.qrtr_code, " _
        '            & "  b.qrtr_year, " _
        '            & "  b.qrtr_start_date, " _
        '            & "  b.qrtr_end_date " _
        '            & "FROM public.qrtr_mstr b " _
        '            & "WHERE b.qrtr_year = " & SetSetring(sls_years.Text) & " " _
        '            & "AND b.qrtr_code_id = " & SetInteger(sls_qrtr_code_id.EditValue) & " " _
        '            & "order by qrtr_id"

        '        Dim dt_fs As New DataTable
        '        dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

        '        ds_edit.Tables(0).Rows.Clear()

        '        For Each dr As DataRow In dt_fs.Rows
        '            ' Pastikan baris tidak diduplikasi
        '            Dim existingRows() As DataRow = ds_edit.Tables(0).Select("slsprogrls_qrtr_id = '" & dr("qrtr_id") & "' AND slsprogrls_parent = '" & sls_roi.Tag & "'")
        '            If existingRows.Length = 0 Then
        '                Dim _row As DataRow = ds_edit.Tables(0).NewRow()
        '                _row("slsprogrls_oid") = Guid.NewGuid.ToString
        '                _row("slsprogrls_year") = dr("qrtr_year")
        '                _row("slsprogrls_qrtr_id") = dr("qrtr_id")
        '                _row("qrtr_name") = dr("qrtr_name")
        '                _row("slsprogrls_qrtr_start_date") = dr("qrtr_start_date")
        '                _row("slsprogrls_qrtr_end_date") = dr("qrtr_end_date")
        '                _row("slsprogrls_spc") = "N"
        '                _row("slsprogrls_amount") = 0.0 ' Default nilai awal
        '                _row("slsprogrls_netto_amount") = 0.0 ' Default nilai awal
        '                ds_edit.Tables(0).Rows.Add(_row)
        '            End If
        '        Next
        '    End If

        '    ds_edit.AcceptChanges()
        '    gv_edit.BestFitColumns()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        'Try
        '    Dim totalNettoAmount As Double = 0.0 ' Variabel untuk menyimpan total netto
        '    Dim qrtr_id As Integer = 0 ' Initialize qrtr_id

        '    ' SQL query to get qrtr_id
        '    Dim sSQL As String
        '    sSQL = "SELECT b.qrtr_id " _
        '         & "FROM public.qrtr_mstr b " _
        '         & "WHERE b.qrtr_year = " & SetSetring(sls_years.Text) & " " _
        '         & "AND b.qrtr_code_id = " & SetInteger(sls_qrtr_code_id.EditValue)

        '    Dim dt_fs As New DataTable
        '    dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

        '    ' Check if the query returned any rows
        '    If dt_fs.Rows.Count > 0 Then
        '        qrtr_id = Convert.ToInt32(dt_fs.Rows(0)("qrtr_id")) ' Get the qrtr_id
        '    Else
        '        MsgBox("No data found for the specified year and code ID.")
        '        Return ' Exit the method if no data is found
        '    End If


        '    'If sls_emp_pos_id.Tag = 1 Then '1 id untuk CEO
        '    '' Jika sls_emp_parent_id kosong
        '    'For Each _row As DataRow In ds_edit.Tables(0).Rows

        '    '    Dim personalAmount As Double = If(IsDBNull(_row("slsprogrls_amount")), 0.0, Convert.ToDouble(_row("slsprogrls_amount")))

        '    '    ' SQL query to get summaryNettoAmount for this parent and qrtr_id
        '    '    Dim sSQLData As String
        '    '    sSQLData = "SELECT SUM(a.slsprogrls_netto_amount) AS summaryNettoAmount " _
        '    '             & "FROM public.slsprogrls_det a " _
        '    '             & "WHERE a.slsprogrls_year = " & SetSetring(sls_qrtr_years.Text) & " " _
        '    '             & "AND a.slsprogrls_qrtr_id = " & _row("slsprogrls_qrtr_id").ToString() & " "

        '    '    Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

        '    '    ' Initialize summaryNettoAmount
        '    '    Dim summaryNettoAmount As Double = 0.0

        '    '    ' Check if the query returned any rows
        '    '    If dt_summary.Rows.Count > 0 Then
        '    '        summaryNettoAmount = If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
        '    '    End If

        '    '    ' Hitung Netto sebagai Personal + Summary Netto
        '    '    Dim nettoAmount As Double = personalAmount + summaryNettoAmount

        '    '    ' Update nilai Netto ke baris
        '    '    _row("slsprogrls_netto_amount") = nettoAmount

        '    '    ' Tambahkan nilai Netto ke total
        '    '    totalNettoAmount += nettoAmount
        '    '    'End If
        '    'Next

        '    'ElseIf sls_emp_pos_id.Tag = 3 Then '3 id untuk NSM
        '    '' Jika sls_emp_parent_id kosong
        '    'For Each _row As DataRow In ds_edit.Tables(0).Rows

        '    '    Dim personalAmount As Double = If(IsDBNull(_row("slsprogrls_amount")), 0.0, Convert.ToDouble(_row("slsprogrls_amount")))

        '    '    ' SQL query to get summaryNettoAmount for this parent and qrtr_id
        '    '    Dim sSQLData As String
        '    '    sSQLData = "SELECT SUM(a.slsprogrls_netto_amount) AS summaryNettoAmount " _
        '    '             & "FROM public.slsprogrls_det a " _
        '    '             & "WHERE a.slsprogrls_year = " & SetSetring(sls_qrtr_years.Text) & " " _
        '    '             & "AND a.slsprogrls_qrtr_id = " & _row("slsprogrls_qrtr_id").ToString() & " "

        '    '    Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

        '    '    ' Initialize summaryNettoAmount
        '    '    Dim summaryNettoAmount As Double = 0.0

        '    '    ' Check if the query returned any rows
        '    '    If dt_summary.Rows.Count > 0 Then
        '    '        summaryNettoAmount = If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
        '    '    End If

        '    '    ' Hitung Netto sebagai Personal + Summary Netto
        '    '    Dim nettoAmount As Double = personalAmount + summaryNettoAmount

        '    '    ' Update nilai Netto ke baris
        '    '    _row("slsprogrls_netto_amount") = nettoAmount

        '    '    ' Tambahkan nilai Netto ke total
        '    '    totalNettoAmount += nettoAmount
        '    '    'End If
        '    'Next

        '    If sls_roi.EditValue <> "" Then
        '        ' Jika sls_emp_parent_id tidak kosong
        '        For Each _row As DataRow In ds_edit.Tables(0).Rows
        '            ' Pastikan baris valid berdasarkan filter
        '            If _row("slsprogrls_year").ToString() = sls_years.Text AndAlso
        '               _row("slsprogrls_parent").ToString() = sls_roi.Tag.ToString() Then

        '                ' Ambil nilai Personal dan hitung Netto
        '                Dim personalAmount As Double = If(IsDBNull(_row("slsprogrls_amount")), 0.0, Convert.ToDouble(_row("slsprogrls_amount")))
        '                Dim nettoAmount As Double = personalAmount * 1 ' Contoh: Netto = 100% dari Personal

        '                ' Update nilai Netto ke baris
        '                _row("slsprogrls_netto_amount") = nettoAmount

        '                ' Tambahkan nilai Netto ke total
        '                totalNettoAmount += nettoAmount
        '            End If
        '        Next
        '    Else
        '        ' Jika sls_emp_parent_id kosong
        '        For Each _row As DataRow In ds_edit.Tables(0).Rows

        '            Dim personalAmount As Double = If(IsDBNull(_row("slsprogrls_amount")), 0.0, Convert.ToDouble(_row("slsprogrls_amount")))

        '            ' SQL query to get summaryNettoAmount for this parent and qrtr_id
        '            Dim sSQLData As String
        '            sSQLData = "SELECT SUM(a.slsprogrls_netto_amount) AS summaryNettoAmount " _
        '                     & "FROM public.slsprogrls_det a " _
        '                     & "WHERE a.slsprogrls_year = " & SetSetring(sls_years.Text) & " " _
        '                     & "AND a.slsprogrls_qrtr_id = " & _row("slsprogrls_qrtr_id").ToString() & " " _
        '                     & "AND a.slsprogrls_parent = " & SetSetring(sls_event_id.Tag.ToString)

        '            Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

        '            ' Initialize summaryNettoAmount
        '            Dim summaryNettoAmount As Double = 0.0

        '            ' Check if the query returned any rows
        '            If dt_summary.Rows.Count > 0 Then
        '                summaryNettoAmount = If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
        '            End If

        '            ' Hitung Netto sebagai Personal + Summary Netto
        '            Dim nettoAmount As Double = personalAmount + summaryNettoAmount

        '            ' Update nilai Netto ke baris
        '            _row("slsprogrls_netto_amount") = nettoAmount

        '            ' Tambahkan nilai Netto ke total
        '            totalNettoAmount += nettoAmount
        '            'End If
        '        Next
        '    End If

        '    ' Masukkan total Netto ke kolom Total Target
        '    sls_achieved_qty.EditValue = totalNettoAmount

        '    ' Simpan perubahan ke dataset dan refresh grid
        '    ds_edit.AcceptChanges()
        '    gv_edit.RefreshData()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Function GetSummaryNettoAmount(year As String, qrtrId As Integer, parentId As String) As Double
        Dim sSQLData As String
        sSQLData = "SELECT SUM(a.slsprogrls_amount) AS summaryNettoAmount " _
                 & "FROM public.sls_program_rules a " _
                 & "WHERE a.slsprogrls_year = " & SetSetring(year) & " " _
                 & "AND a.slsprogrls_qrtr_id = " & qrtrId & " " _
                 & "AND a.slsprogrls_parent = " & SetSetring(parentId)

        Dim dt_summary As DataTable = master_new.PGSqlConn.GetTableData(sSQLData)

        If dt_summary.Rows.Count > 0 Then
            Return If(IsDBNull(dt_summary.Rows(0)("summaryNettoAmount")), 0.0, Convert.ToDouble(dt_summary.Rows(0)("summaryNettoAmount")))
        End If
        Return 0.0
    End Function

    Private Sub sls_ce_special_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    ' Periksa apakah checkbox Is Special dicentang
        '    'Dim isSpecial As Boolean = sls_ce_special.Checked

        '    ' Perbarui kolom slsprogrls_spc untuk semua baris
        '    For Each _row As DataRow In ds_edit.Tables(0).Rows
        '        If _row.RowState <> DataRowState.Deleted Then
        '            If isSpecial Then
        '                _row("slsprogrls_spc") = "Y"
        '            Else
        '                _row("slsprogrls_spc") = "N"
        '            End If
        '        End If
        '    Next

        '    ' Refresh grid view untuk menampilkan perubahan
        '    gv_edit.RefreshData()
        'Catch ex As Exception
        '    MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        'End Try
    End Sub

    'Private Sub file_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Try
    '        Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")
    '        If _file = "" Then Exit Sub

    '        file_excel.EditValue = _file
    '        Using ds As DataSet = master_new.excelconn.ImportExcel(_file)
    '            ds_edit.Tables(0).Rows.Clear()
    '            ds_edit.AcceptChanges()
    '            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

    '            Dim _row As Integer = 0
    '            For Each dr As DataRow In ds.Tables(0).Rows
    '                Dim ssql As String = "SELECT  distinct " _
    '                               & "  en_id, " _
    '                               & "  en_desc, " _
    '                               & "  si_desc, " _
    '                               & "  pt_id, " _
    '                               & "  pt_code, " _
    '                               & "  pt_desc1, " _
    '                               & "  pt_desc2, " _
    '                               & "  pt_cost, " _
    '                               & "  invct_cost, " _
    '                               & "  pt_price, " _
    '                               & "  pt_type, " _
    '                               & "  pt_um, " _
    '                               & "  pt_pl_id, " _
    '                               & "  pt_ls, " _
    '                               & "  pt_loc_id, " _
    '                               & "  loc_desc, " _
    '                               & "  pt_taxable, " _
    '                               & "  pt_tax_inc, " _
    '                               & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
    '                               & "  tax_class_mstr.code_name as tax_class_name, " _
    '                               & "  pt_ppn_type, " _
    '                               & "  um_mstr.code_name as um_name " _
    '                               & "FROM  " _
    '                               & "  public.pt_mstr" _
    '                               & " inner join en_mstr on en_id = pt_en_id " _
    '                               & " inner join loc_mstr on loc_id = pt_loc_id " _
    '                               & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
    '                               & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
    '                               & " inner join invct_table on invct_pt_id = pt_id " _
    '                               & " inner join si_mstr on si_id = invct_si_id " _
    '                               & " where pt_code ='" & dr("pt_code") & "' "

    '                Using dt_temp As DataTable = master_new.PGSqlConn.GetTableData(ssql)

    '                    For Each dr_temp As DataRow In dt_temp.Rows
    '                        gv_edit.AddNewRow()
    '                        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
    '                        gv_edit.SetRowCellValue(_row, "pt_id", dr_temp("pt_id"))
    '                        gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
    '                        gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
    '                        gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
    '                        gv_edit.SetRowCellValue(_row, "ptsfrd_um", dr_temp("pt_um"))
    '                        gv_edit.SetRowCellValue(_row, "ptsfrd_qty_open", dr("qty"))
    '                        gv_edit.SetRowCellValue(_row, "ptsfrd_qty", dr("qty"))
    '                        gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", dr_temp("um_name"))
    '                        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

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

    Private Sub CalculateValues()
        Dim _initial_target_value As Double = SetNumber(sls_target_revenue.Tag)
        Dim _initial_qty_value As Double = SetNumber(sls_target_qty.Tag)
        Dim _initial_cost_value As Double = SetNumber(sls_total_cost.Tag)
        Dim _initial_bdgt_incentive As Double = SetNumber(sls_bdgt_incentive.EditValue)
        Dim _initial_bdgt_marketing As Double = SetNumber(sls_bdgt_marketing.EditValue)
        Dim _initial_max_disc As Double = SetNumber(sls_avg_disc.EditValue)
        Dim _sls_target_revenue As Double = SetNumber(sls_target_revenue.EditValue)

        ' Hitung nilai yang diperbarui
        Dim _qty_value_net As Double = Math.Ceiling(_initial_qty_value * (_sls_target_revenue / _initial_target_value))
        Dim _cost_value_net As Double = SetNumber(_initial_cost_value * (_sls_target_revenue / _initial_target_value)) +
                                        (_sls_target_revenue * _initial_bdgt_marketing) +
                                        (_sls_target_revenue * _initial_bdgt_incentive) +
                                        (_sls_target_revenue * _initial_max_disc)
        Dim _margin_value_net As Double = SetNumber(_sls_target_revenue - _cost_value_net)
        Dim _gpm_value_net As Double = SetNumber(((_sls_target_revenue - _cost_value_net) / _sls_target_revenue) * 100) / 100
        Dim _ctsr_value_net As Double = SetNumber((_cost_value_net / _sls_target_revenue) * 100) / 100

        ' Set nilai ke kontrol UI
        sls_target_qty.EditValue = _qty_value_net
        sls_total_cost.EditValue = _cost_value_net
        sls_margin.EditValue = _margin_value_net
        sls_gpm.EditValue = _gpm_value_net
        sls_ctsratio.EditValue = _ctsr_value_net
    End Sub

    Private Sub sls_target_value_valueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sls_target_revenue.EditValueChanged
        CalculateValues()
    End Sub

    Private Sub sls_bdgt_incentive_valueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sls_bdgt_incentive.EditValueChanged
        CalculateValues()
    End Sub

    Private Sub sls_bdgt_marketing_valueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sls_bdgt_marketing.EditValueChanged
        CalculateValues()
    End Sub

    Private Sub sls_max_disc_valueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sls_avg_disc.EditValueChanged
        CalculateValues()
    End Sub

End Class