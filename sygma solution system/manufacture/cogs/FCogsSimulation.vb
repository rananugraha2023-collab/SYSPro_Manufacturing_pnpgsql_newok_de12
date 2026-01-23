Imports npgsql
Imports master_new.ModFunction

Public Class FCogsSimulation
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ar_oid_mstr As String
    Public dt_raw, dt_raw_nonmat, dt_indirect_mat, dt_indirect_nonmat As New DataTable
    Dim _now As Date
    Dim sSql As String
    Dim dt_temp_ac As New DataTable

    Private Sub FCogsSimulation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        AddHandler gv_master.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_master.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        cogsc_en_id.Properties.DataSource = dt_bantu
        cogsc_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cogsc_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cogsc_en_id.ItemIndex = 0

    End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cogsc_en_id.EditValueChanged
        init_le(cogsc_cs_id, "cost_set", cogsc_en_id.EditValue)
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Simulation Code", "cogsc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "cogsc_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remark", "cogsc_remarks", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Cost Set", "cs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Material Total", "cogsc_mtl_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Service Total", "cogsc_srv_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total", "cogsc_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "cogsc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cogsc_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "cogsc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cogsc_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail_item, "cogsci_cogsc_oid", False)
        add_column(gv_detail_item, "cogsci_pt_id", False)
        add_column_copy(gv_detail_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Qty", "cogsci_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_item, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "PM Code", "pt_pm_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_route, "cogscr_cogsc_oid", False)
        add_column(gv_detail_route, "cogscr_pt_id", False)
        add_column_copy(gv_detail_route, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_route, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_route, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_route, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_route, "Routing", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_route, "Indirect Amount", "cogscr_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_raw, "cogscr_cogsc_oid", False)
        add_column(gv_detail_raw, "cogscr_pt_id", False)
        add_column_copy(gv_detail_raw, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_raw, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_raw, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_raw, "Qty", "cogscr_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_raw, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_raw, "Product Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail_raw, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_raw, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail_raw, "Cost", "cogscr_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_raw, "Ext. Cost", "cogscr_cost_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_acc, "cogsca_cogsc_oid", False)
        add_column(gv_detail_acc, "cogsca_ac_id", False)
        add_column_copy(gv_detail_acc, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_acc, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_acc, "Amount", "cogsca_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_acc, "Type", "cogsca_type", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_dir_mat, "psd_pt_bom_id", False)
        add_column_copy(gv_edit_dir_mat, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_mat, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_mat, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_mat, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_mat, "Unit Measure", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_mat, "Cost", "cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_mat, "Ext Cost", "ex_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit_dir_nonmat, "Routing", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_nonmat, "Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_nonmat, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_dir_nonmat, "Run Time", "rod_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_nonmat, "Ratio Product Structure", "ps_ratio", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_nonmat, "Labor Amount", "labor_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_nonmat, "Machine Amount", "mach_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_nonmat, "Setup Amount", "setup_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_dir_nonmat, "Total Amount", "total_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit_indir_mat, "Routing Code", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_mat, "Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_mat, "Type", "rodit_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_mat, "Remarks", "rodi_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_indir_mat, "Qty", "rodi_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_indir_mat, "Amount", "rodi_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_indir_mat, "Total", "rodi_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit_indir_nonmat, "Routing Code", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_nonmat, "Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_nonmat, "Type", "rodit_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_nonmat, "Remarks", "rodi_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_indir_nonmat, "Qty", "rodi_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_indir_nonmat, "Amount", "rodi_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_indir_nonmat, "Total", "rodi_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  b.en_desc, " _
                & "  a.cogsc_oid, " _
                & "  a.cogsc_code, " _
                & "  a.cogsc_en_id, " _
                & "  a.cogsc_date, " _
                & "  a.cogsc_remarks, " _
                & "  a.cogsc_cs_id, " _
                & "  c.cs_desc, " _
                & "  a.cogsc_mtl_total, " _
                & "  a.cogsc_srv_total, " _
                & "  a.cogsc_total, " _
                & "  a.cogsc_add_by, " _
                & "  a.cogsc_add_date, " _
                & "  a.cogsc_upd_by, " _
                & "  a.cogsc_upd_date " _
                & "FROM " _
                & "  public.cogsc_calc a " _
                & "  INNER JOIN public.en_mstr b ON (a.cogsc_en_id = b.en_id) " _
                & "  INNER JOIN public.cs_mstr c ON (a.cogsc_cs_id = c.cs_id) " _
                & "WHERE " _
                & "  a.cogsc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
                & " and cogsc_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "ORDER BY " _
                & "  a.cogsc_code"


        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail_item").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.cogsci_oid, " _
            & "  a.cogsci_cogsc_oid, " _
            & "  a.cogsci_seq, " _
            & "  a.cogsci_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  b.pt_um,pt_pm_code, " _
            & "  c.code_name as um_desc, " _
            & "  a.cogsci_qty " _
            & "FROM " _
            & "  public.cogsci_item a " _
            & "  INNER JOIN public.pt_mstr b ON (a.cogsci_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
            & "  INNER JOIN public.cogsc_calc d ON (a.cogsci_cogsc_oid = d.cogsc_oid) " _
            & "WHERE " _
            & "  d.cogsc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
            & " and d.cogsc_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.cogsci_seq"

        load_data_detail(sql, gc_detail_item, "detail_item")

        Try
            ds.Tables("detail_route").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.cogscr_oid, " _
            & "  a.cogscr_cogsc_oid, " _
            & "  a.cogscr_seq, " _
            & "  a.cogscr_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.cogscr_ro_id, " _
            & "  c.ro_code,d.code_name as um_desc,cogscr_qty " _
            & "FROM " _
            & "  public.cogscr_route a " _
            & "  INNER JOIN public.pt_mstr b ON (a.cogscr_pt_id = b.pt_id) " _
            & "  INNER JOIN public.ro_mstr c ON (a.cogscr_ro_id = c.ro_id) " _
            & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
            & "  INNER JOIN public.cogsc_calc e ON (a.cogscr_cogsc_oid = e.cogsc_oid) " _
            & "WHERE " _
            & "  e.cogsc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
            & " and e.cogsc_en_id in (select user_en_id from tconfuserentity " _
                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.cogscr_seq"


        load_data_detail(sql, gc_detail_route, "detail_route")

        Try
            ds.Tables("detail_raw").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  d.code_code, " _
            & "  a.cogscr_oid, " _
            & "  a.cogscr_cogsc_oid, " _
            & "  a.cogscr_seq, " _
            & "  a.cogscr_pt_id, " _
            & "  a.cogscr_qty, " _
            & "  a.cogscr_cost,d.code_name as um_desc, pl_desc, " _
            & "  a.cogscr_cost * a.cogscr_qty as cogscr_cost_ext,f.ac_code,f.ac_name " _
            & "FROM " _
            & "  public.pt_mstr b " _
            & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
            & "  INNER JOIN public.cogscr_raw a ON (b.pt_id = a.cogscr_pt_id) " _
            & "  INNER JOIN public.cogsc_calc c ON (a.cogscr_cogsc_oid = c.cogsc_oid) " _
            & "  INNER JOIN public.pl_mstr e ON (b.pt_pl_id = e.pl_id) " _
            & "  LEFT OUTER JOIN public.ac_mstr f ON (a.cogscr_ac_id = f.ac_id) " _
            & "WHERE " _
            & "  c.cogsc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
            & " and c.cogsc_en_id in (select user_en_id from tconfuserentity " _
                          & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.cogscr_seq"

        load_data_detail(sql, gc_detail_raw, "detail_raw")


        Try
            ds.Tables("detail_acc").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.cogsca_cogsc_oid, " _
            & "  a.cogsca_seq, " _
            & "  a.cogsca_ac_id, " _
            & "  b.ac_code, " _
            & "  b.ac_name, " _
            & "  a.cogsca_amount, " _
            & "  a.cogsca_oid,cogsca_type " _
            & "FROM " _
            & "  public.cogsca_acct a " _
            & "  INNER JOIN public.ac_mstr b ON (a.cogsca_ac_id = b.ac_id) " _
            & "  INNER JOIN public.cogsc_calc c ON (a.cogsca_cogsc_oid = c.cogsc_oid) " _
            & "WHERE " _
            & "  c.cogsc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
            & " and c.cogsc_en_id in (select user_en_id from tconfuserentity " _
                          & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.cogsca_seq"

        load_data_detail(sql, gc_detail_acc, "detail_acc")


    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_item.Columns("cogsci_cogsc_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cogsci_cogsc_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cogsc_oid").ToString & "'")
            gv_detail_item.BestFitColumns()

            gv_detail_raw.Columns("cogscr_cogsc_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cogscr_cogsc_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cogsc_oid").ToString & "'")
            gv_detail_raw.BestFitColumns()

            gv_detail_route.Columns("cogscr_cogsc_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cogscr_cogsc_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cogsc_oid").ToString & "'")
            gv_detail_route.BestFitColumns()

            gv_detail_acc.Columns("cogsca_cogsc_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cogsca_cogsc_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cogsc_oid").ToString & "'")
            gv_detail_route.BestFitColumns()


        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        cogsc_en_id.ItemIndex = 0
        cogsc_direct_non_material_ttl.EditValue = 0
        cogsc_cs_id.ItemIndex = 0
        cogsc_date.DateTime = Now
        cogsc_direct_material_ttl.EditValue = 0
        cogsc_direct_non_material_ttl.EditValue = 0
        cogsc_total.EditValue = 0
        cogsc_remarks.Text = ""


        gc_edit_dir_nonmat.EmbeddedNavigator.Buttons.Append.Visible = False
        gc_edit_dir_nonmat.EmbeddedNavigator.Buttons.Remove.Visible = False
        gc_edit_indir_mat.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_indir_mat.EmbeddedNavigator.Buttons.Remove.Visible = True
        gc_edit_indir_nonmat.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_indir_nonmat.EmbeddedNavigator.Buttons.Remove.Visible = True

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        'ds_edit_item = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb

        '            .SQL = "SELECT  " _
        '                & "  a.cogsci_oid, " _
        '                & "  a.cogsci_cogsc_oid, " _
        '                & "  a.cogsci_seq, " _
        '                & "  a.cogsci_pt_id, " _
        '                & "  b.pt_code, " _
        '                & "  b.pt_desc1, " _
        '                & "  b.pt_desc2,b.pt_pm_code, " _
        '                & "  b.pt_um, " _
        '                & "  c.code_name as um_desc, " _
        '                & "  a.cogsci_qty" _
        '                & " FROM " _
        '                & "  public.cogsci_item a " _
        '                & "  INNER JOIN public.pt_mstr b ON (a.cogsci_pt_id = b.pt_id) " _
        '                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
        '                & "  INNER JOIN public.cogsc_calc d ON (a.cogsci_cogsc_oid = d.cogsc_oid) " _
        '                & " WHERE cogsci_oid is null "

        '            .InitializeCommand()
        '            .FillDataSet(ds_edit_item, "list_item")
        '            gc_edit_dir_mat.DataSource = ds_edit_item.Tables(0)
        '            gv_edit_dir_mat.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        'ds_edit_route = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb


        '            .SQL = "SELECT  " _
        '                & "  a.cogscr_oid, " _
        '                & "  a.cogscr_cogsc_oid, " _
        '                & "  a.cogscr_seq, " _
        '                & "  a.cogscr_pt_id, " _
        '                & "  b.pt_code, " _
        '                & "  b.pt_desc1, " _
        '                & "  b.pt_desc2, " _
        '                & "  a.cogscr_ro_id, " _
        '                & "  c.ro_code,d.code_name as um_desc,0.0 as qty " _
        '                & "FROM " _
        '                & "  public.cogscr_route a " _
        '                & "  INNER JOIN public.pt_mstr b ON (a.cogscr_pt_id = b.pt_id) " _
        '                & "  INNER JOIN public.ro_mstr c ON (a.cogscr_ro_id = c.ro_id) " _
        '                & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
        '                & "WHERE " _
        '                & "  a.cogscr_oid IS NULL"

        '            .InitializeCommand()
        '            .FillDataSet(ds_edit_route, "list_route")
        '            gc_edit_dir_nonmat.DataSource = ds_edit_route.Tables(0)
        '            gv_edit_dir_nonmat.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        'ds_edit_raw = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb

        '            .SQL = "SELECT  " _
        '                & "  b.pt_code, " _
        '                & "  b.pt_desc1, " _
        '                & "  b.pt_desc2, " _
        '                & "  d.code_code, " _
        '                & "  a.cogscr_oid, " _
        '                & "  a.cogscr_cogsc_oid, " _
        '                & "  a.cogscr_seq, " _
        '                & "  a.cogscr_pt_id,a.cogscr_ac_id, " _
        '                & "  a.cogscr_qty, " _
        '                & "  a.cogscr_cost,d.code_name as um_desc, pl_desc,'' as ac_code,'' as ac_name , " _
        '                & "  0.0 as cogscr_cost_ext " _
        '                & "FROM " _
        '                & "  public.pt_mstr b " _
        '                & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
        '                & "  INNER JOIN public.cogscr_raw a ON (b.pt_id = a.cogscr_pt_id) " _
        '                & "  INNER JOIN public.pl_mstr e ON (b.pt_pl_id = e.pl_id) " _
        '                & "WHERE " _
        '                & "  a.cogscr_oid IS NULL"

        '            .InitializeCommand()
        '            .FillDataSet(ds_edit_raw, "list_raw")
        '            gc_edit_indir_mat.DataSource = ds_edit_raw.Tables(0)
        '            gv_edit_indir_mat.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        'ds_edit_acc = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb

        '            .SQL = "SELECT  " _
        '                & "  a.cogsca_cogsc_oid, " _
        '                & "  a.cogsca_seq, " _
        '                & "  a.cogsca_ac_id, " _
        '                & "  b.ac_code, " _
        '                & "  b.ac_name, " _
        '                & "  a.cogsca_amount, " _
        '                & "  a.cogsca_oid,'' as type " _
        '                & "FROM " _
        '                & "  public.cogsca_acct a " _
        '                & "  INNER JOIN public.ac_mstr b ON (a.cogsca_ac_id = b.ac_id) " _
        '                & "WHERE " _
        '                & "  a.cogsca_oid IS NULL"

        '            .InitializeCommand()
        '            .FillDataSet(ds_edit_acc, "list_raw")
        '            gc_edit_indir_nonmat.DataSource = ds_edit_acc.Tables(0)
        '            gv_edit_indir_nonmat.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try


    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit_dir_mat.UpdateCurrentRow()
        'ds_edit_item.AcceptChanges()
        gv_edit_indir_mat.UpdateCurrentRow()
        'ds_edit_raw.AcceptChanges()
        gv_edit_dir_nonmat.UpdateCurrentRow()
        'ds_edit_route.AcceptChanges()
        gv_edit_indir_nonmat.UpdateCurrentRow()
        'ds_edit_acc.AcceptChanges()

        'If ds_edit_item.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Item Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        'If ds_edit_route.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Routing Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        'If ds_edit_raw.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Material Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        'If ds_edit_acc.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Account Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _oid_mstr As String
        _oid_mstr = Guid.NewGuid.ToString

        Dim _code As String
        Dim i As Integer
        _code = func_coll.get_transaction_number("CG", cogsc_en_id.GetColumnValue("en_code"), "cogsc_calc", "cogsc_code", cogsc_date.DateTime)

        'For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
        '    _ar_amount = _ar_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        'Next

        calc_header()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text

                        sSql = "INSERT INTO  " _
                            & "  public.cogsc_calc " _
                            & "( " _
                            & "  cogsc_oid, " _
                            & "  cogsc_dom_id, " _
                            & "  cogsc_en_id, " _
                            & "  cogsc_add_by, " _
                            & "  cogsc_add_date, " _
                            & "  cogsc_dt, " _
                            & "  cogsc_code, " _
                            & "  cogsc_date, " _
                            & "  cogsc_remarks, " _
                            & "  cogsc_cs_id, " _
                            & "  cogsc_mtl_total, " _
                            & "  cogsc_srv_total, " _
                            & "  cogsc_total " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(_oid_mstr) & ",  " _
                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                            & SetSetring(cogsc_en_id.EditValue) & ",  " _
                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "current_timestamp" & ",  " _
                            & "current_timestamp" & ",  " _
                            & SetSetring(_code) & ",  " _
                            & SetDate(cogsc_date.DateTime) & ",  " _
                            & SetSetring(cogsc_remarks.Text) & ",  " _
                            & SetInteger(cogsc_cs_id.EditValue) & ",  " _
                            & SetDec(cogsc_direct_material_ttl.EditValue) & ",  " _
                            & SetDec(cogsc_direct_non_material_ttl.EditValue) & ",  " _
                            & SetDec(cogsc_total.EditValue) & "  " _
                            & ")"

                        .Command.CommandText = sSql
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'i = 0
                        'For Each dr As DataRow In dt_raw.Rows
                        '    .Command.CommandType = CommandType.Text

                        '    sSql = "INSERT INTO  " _
                        '        & "  public.cogsci_item " _
                        '        & "( " _
                        '        & "  cogsci_oid, " _
                        '        & "  cogsci_cogsc_oid, " _
                        '        & "  cogsci_seq, " _
                        '        & "  cogsci_pt_id, " _
                        '        & "  cogsci_qty " _
                        '        & ")  " _
                        '        & "VALUES ( " _
                        '        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '        & SetSetring(_oid_mstr) & ",  " _
                        '        & SetInteger(i) & ",  " _
                        '        & SetInteger(dr("psd_pt_bom_id")) & ",  " _
                        '        & SetDec(dr("psd_qty")) & "  " _
                        '        & ")"

                        '    .Command.CommandText = sSql
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        '    i += 1
                        'Next

                        'i = 0
                        'For Each dr As DataRow In ds_edit_route.Tables(0).Rows
                        '    .Command.CommandType = CommandType.Text

                        '    sSql = "INSERT INTO  " _
                        '        & "  public.cogscr_route " _
                        '        & "( " _
                        '        & "  cogscr_oid, " _
                        '        & "  cogscr_cogsc_oid, " _
                        '        & "  cogscr_seq, " _
                        '        & "  cogscr_pt_id, " _
                        '        & "  cogscr_ro_id,cogscr_qty " _
                        '        & ")  " _
                        '        & "VALUES ( " _
                        '        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '        & SetSetring(_oid_mstr) & ",  " _
                        '        & SetInteger(i) & ",  " _
                        '        & SetInteger(dr("cogscr_pt_id")) & ",  " _
                        '        & SetInteger(dr("cogscr_ro_id")) & ",  " _
                        '        & SetInteger(dr("qty")) & "  " _
                        '        & ")"

                        '    .Command.CommandText = sSql
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        '    i += 1
                        'Next

                        'i = 0
                        'For Each dr As DataRow In ds_edit_raw.Tables(0).Rows
                        '    .Command.CommandType = CommandType.Text

                        '    sSql = "INSERT INTO  " _
                        '        & "  public.cogscr_raw " _
                        '        & "( " _
                        '        & "  cogscr_oid, " _
                        '        & "  cogscr_cogsc_oid, " _
                        '        & "  cogscr_seq, " _
                        '        & "  cogscr_pt_id, " _
                        '        & "  cogscr_qty, " _
                        '        & "  cogscr_cost,cogscr_ac_id " _
                        '        & ")  " _
                        '        & "VALUES ( " _
                        '        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '        & SetSetring(_oid_mstr) & ",  " _
                        '        & SetInteger(i) & ",  " _
                        '        & SetInteger(dr("cogscr_pt_id")) & ",  " _
                        '        & SetDec(dr("cogscr_qty")) & ",  " _
                        '        & SetDec(dr("cogscr_cost")) & ",  " _
                        '        & SetInteger(dr("cogscr_ac_id")) & "  " _
                        '        & ")"

                        '    .Command.CommandText = sSql
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        '    i += 1
                        'Next

                        'i = 0
                        'For Each dr As DataRow In ds_edit_acc.Tables(0).Rows
                        '    .Command.CommandType = CommandType.Text

                        '    sSql = "INSERT INTO  " _
                        '        & "  public.cogsca_acct " _
                        '        & "( " _
                        '        & "  cogsca_oid, " _
                        '        & "  cogsca_cogsc_oid, " _
                        '        & "  cogsca_seq, " _
                        '        & "  cogsca_ac_id, " _
                        '        & "  cogsca_amount,cogsca_type " _
                        '        & ")  " _
                        '        & "VALUES ( " _
                        '        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '        & SetSetring(_oid_mstr) & ",  " _
                        '        & SetInteger(i) & ",  " _
                        '        & SetDec(dr("cogsca_ac_id")) & ",  " _
                        '        & SetDec(dr("cogsca_amount")) & ",  " _
                        '        & SetSetring(dr("type")) & "  " _
                        '        & ")"

                        '    .Command.CommandText = sSql
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        '    i += 1
                        'Next

                        sqlTran.Commit()
                        after_success()
                        set_row(_oid_mstr.ToString, "cogsc_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Harus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from cogsc_calc where cogsc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cogsc_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Sub preview()

        Try
            Dim _sql As String

            _sql = "SELECT  " _
                & "  a.cogsc_code,cogsc_oid, " _
                & "  a.cogsc_date, " _
                & "  e.en_desc, " _
                & "  a.cogsc_remarks, " _
                & "  f.cs_name, " _
                & "  a.cogsc_mtl_total, " _
                & "  a.cogsc_srv_total, " _
                & "  a.cogsc_total, " _
                & "  b.cogsci_seq, " _
                & "  b.cogsci_pt_id, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  c.pt_desc2, " _
                & "  d.code_name as unit_measure, " _
                & "  b.cogsci_qty " _
                & "FROM " _
                & "  public.cogsc_calc a " _
                & "  INNER JOIN public.cogsci_item b ON (a.cogsc_oid = b.cogsci_cogsc_oid) " _
                & "  INNER JOIN public.pt_mstr c ON (b.cogsci_pt_id = c.pt_id) " _
                & "  INNER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "  INNER JOIN public.en_mstr e ON (a.cogsc_en_id = e.en_id) " _
                & "  INNER JOIN public.cs_mstr f ON (a.cogsc_cs_id = f.cs_id) " _
                & "WHERE " _
                & "cogsc_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cogsc_code") + "'"


            'Dim rpt As New XRCogsSimulation

            'With rpt
            '    Dim ds As New DataSet

            '    ds = master_new.PGSqlConn.ReportDataset(_sql)

            '    If ds.Tables(0).Rows.Count = 0 Then
            '        MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", _
            '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    End If

            '    .DataSource = ds
            '    .DataMember = "Table"
            '    printTool.ShowPreview()
            'End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub gv_edit_so_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_dir_mat.DoubleClick
        browse_so()
    End Sub

    Private Sub gv_edit_so_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_dir_mat.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_dir_mat.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_dir_mat.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_so_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_dir_mat.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_so()
        End If
    End Sub

    Private Sub browse_so()
        Dim _col As String = gv_edit_dir_mat.FocusedColumn.Name
        Dim _row As Integer = gv_edit_dir_mat.FocusedRowHandle


        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = cogsc_en_id.EditValue
            frm._obj = gv_edit_dir_mat
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub sb_retrieve_raw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

     

    End Sub


    Private Sub sb_retrieve_route_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub


    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_dir_nonmat.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_dir_nonmat.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_dir_nonmat.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_dir_nonmat.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_dir_nonmat.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit_dir_nonmat.FocusedColumn.Name
        Dim _row As Integer = gv_edit_dir_nonmat.FocusedRowHandle
        Dim _pt_id As Integer = gv_edit_dir_nonmat.GetRowCellValue(_row, "cogscr_pt_id")

        'If _col = "ro_code" Then
        '    Dim frm As New FRoutingSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = cogsc_en_id.EditValue
        '    frm._pt_id = _pt_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub

    Private Sub sb_retrieve_acc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
      
    End Sub

    Function group_account(ByVal par_ac_id As Integer, ByVal par_ac_code As String, ByVal par_ac_name As String, _
                           ByVal par_ac_amount As Double, ByVal par_opsi As String, ByVal par_type As String) As Boolean
        Try
            If par_opsi = "clear" Then
                sSql = "select 0 as ac_id,'' as ac_code,'' as ac_name,0.0 as ac_amount,'' as type"
                dt_temp_ac = master_new.PGSqlConn.GetTableData(sSql)
                dt_temp_ac.Rows.Clear()
                Return True
                Exit Function
            End If
            Dim _dtrow As DataRow
            Dim _status_duplicate As Boolean = False
            For Each dr As DataRow In dt_temp_ac.Rows
                If dr("ac_id") = par_ac_id And SetString(dr("type")) = SetString(par_type) Then
                    _status_duplicate = True
                    dr("ac_amount") = dr("ac_amount") + par_ac_amount
                    Exit For
                End If
            Next
            dt_temp_ac.AcceptChanges()

            If _status_duplicate = False Then
                _dtrow = dt_temp_ac.NewRow
                _dtrow("ac_id") = par_ac_id
                _dtrow("ac_code") = par_ac_code
                _dtrow("ac_name") = par_ac_name
                _dtrow("ac_amount") = SetNumber(par_ac_amount)
                _dtrow("type") = par_type
                dt_temp_ac.Rows.Add(_dtrow)
            End If
            dt_temp_ac.AcceptChanges()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Sub gv_edit_raw_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_indir_mat.CellValueChanged
        'If e.Column.Name = "cogscr_cost" Then
        '    Dim _qty, _cost, _cost_ext As Double
        '    _qty = 0
        '    _cost = 0
        '    _cost_ext = 0
        '    Try
        '        _cost = e.Value
        '        _qty = SetNumber(gv_edit_indir_mat.GetRowCellValue(e.RowHandle, "cogscr_qty"))
        '        _cost_ext = _cost * _qty

        '        gv_edit_indir_mat.SetRowCellValue(e.RowHandle, "cogscr_cost_ext", _cost_ext)

        '        calc_raw()
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'ElseIf e.Column.Name = "cogscr_qty" Then
        '    Dim _qty, _cost, _cost_ext As Double
        '    _qty = 0
        '    _cost = 0
        '    _cost_ext = 0
        '    Try
        '        _cost = SetNumber(gv_edit_indir_mat.GetRowCellValue(e.RowHandle, "cogscr_cost"))
        '        _qty = e.Value
        '        _cost_ext = _cost * _qty

        '        gv_edit_indir_mat.SetRowCellValue(e.RowHandle, "cogscr_cost_ext", _cost_ext)

        '        calc_raw()
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'End If
    End Sub

    Private Sub calc_header()
        Try
            'gv_edit_indir_nonmat.UpdateCurrentRow()
            'ds_edit_acc.AcceptChanges()

            'Dim _material, _service As Double
            '_material = 0
            '_service = 0
            'For Each dr As DataRow In ds_edit_acc.Tables(0).Rows
            '    If SetString(dr("type")) = "M" Then
            '        _material += SetNumber(dr("cogsca_amount"))
            '    Else
            '        _service += SetNumber(dr("cogsca_amount"))
            '    End If
            'Next

            'cogsc_direct_non_material_ttl.EditValue = _service
            'cogsc_direct_material_ttl.EditValue = _material
            'cogsc_total.EditValue = _material + _service


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub calc_raw()
        Try
            'gv_edit_indir_mat.UpdateCurrentRow()
            'ds_edit_raw.AcceptChanges()

            'Dim _total As Double = 0
            'For Each dr As DataRow In ds_edit_raw.Tables(0).Rows
            '    _total += dr("cogscr_cost_ext")
            'Next

            'cogsc_direct_material_ttl.EditValue = _total
            'cogsc_total.EditValue = _total + cogsc_direct_non_material_ttl.EditValue

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_edit_acc_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_indir_nonmat.CellValueChanged
        calc_header()
    End Sub

    Private Sub gv_edit_acc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_indir_nonmat.DoubleClick
        browse_data_acc()
    End Sub

    Private Sub gv_edit_acc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_indir_nonmat.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_indir_nonmat.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_indir_nonmat.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_acc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_indir_nonmat.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub browse_data_acc()
        'gv_edit_indir_nonmat.UpdateCurrentRow()

        'Dim _col As String = gv_edit_indir_nonmat.FocusedColumn.Name
        'Dim _row As Integer = gv_edit_indir_nonmat.FocusedRowHandle

        'If _col = "ac_code" Then
        '    Dim frm As New FAccountSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub

    Private Sub gv_edit_acc_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_indir_nonmat.RowCountChanged
        calc_header()
    End Sub

    Private Sub gv_edit_raw_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_indir_mat.DoubleClick
        browse_data_raw()
    End Sub

    Private Sub browse_data_raw()
        gv_edit_indir_mat.UpdateCurrentRow()

        Dim _col As String = gv_edit_indir_mat.FocusedColumn.Name
        Dim _row As Integer = gv_edit_indir_mat.FocusedRowHandle

        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._obj = gv_edit_indir_mat
            frm._en_id = cogsc_en_id.EditValue
            'frm._cs_type = cogsc_cs_id.GetColumnValue("cs_type")
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

   

    Public Function ImportFromExcel(ByVal PrmPathExcelFile As String) As DataSet
        ImportFromExcel = Nothing
        Dim MyConnection As System.Data.OleDb.OleDbConnection = Nothing

        Try
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & "data source='" _
                                                                 & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [sheet1$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "routing")

            DtSet = New System.Data.DataSet

            MyCommand.Fill(DtSet)
            MyConnection.Close()

            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
        End Try
    End Function

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenerate.Click
        Try
            If SetNumber(cogsc_qty.EditValue) = 0 Then
                Box("Qty can't empty")
                Exit Sub
            End If
            Dim _direct_mat_ttl, _indirect_mat_ttl, _direct_nonmat_ttl, _indirect_nonmat_ttl As Double
            _direct_mat_ttl = 0
            _indirect_mat_ttl = 0
            _direct_nonmat_ttl = 0
            _indirect_nonmat_ttl = 0


            Dim sSQL As String = ""
            sSQL = "select psd_pt_bom_id, pt_code, pt_desc1, pt_desc2, code_name, sum(psd_qty) as psd_qty, " _
                & " 0.0 as cost,0.0 as ex_cost from ( select * from public.get_ps_first(" & cogsc_pt_id.Tag _
                & ",1) where psd_indirect='N' and variable_4=0) as temp  group by psd_pt_bom_id, pt_code, pt_desc1, pt_desc2, code_name "

            dt_raw = master_new.PGSqlConn.GetTableData(sSQL)

            Dim dt_cost As New DataTable
            For Each dr As DataRow In dt_raw.Rows
                dr("psd_qty") = dr("psd_qty") * cogsc_qty_prod.EditValue

                'sSQL = "SELECT  pt_code,sct_pt_id, sct_cs_id,cs_name, sct_total" _
                '       & " FROM pt_mstr " _
                '       & " left outer join sct_mstr on sct_pt_id=pt_id " _
                '       & " left outer join cs_mstr on cs_id=sct_cs_id " _
                '       & "  where cs_name ~~* " & "'standard'" & " and sct_pt_id = " & dr("psd_pt_bom_id")

                sSQL = "SELECT  " _
                    & " max( a.invct_cost ) as jml " _
                    & "FROM " _
                    & "  public.invct_table a " _
                    & "WHERE " _
                    & "  a.invct_pt_id = " & dr("psd_pt_bom_id") & "   "


                dt_cost = master_new.PGSqlConn.GetTableData(sSQL)
                If dt_cost.Rows.Count > 0 Then
                    dr("cost") = dt_cost.Rows(0).Item("jml")
                End If
                dr("ex_cost") = dr("cost") * dr("psd_qty")
                _direct_mat_ttl += dr("ex_cost")
            Next
            dt_raw.AcceptChanges()

            gc_edit_dir_mat.DataSource = dt_raw
            gv_edit_dir_mat.BestFitColumns()

            sSQL = "SELECT  " _
                & "  a.ro_code, " _
                & "  a.ro_desc, " _
                & "  b.rod_run, " _
                & "  d.wc_men_mch, " _
                & "  d.wc_lbr_rate,d.wc_men_mch* d.wc_lbr_rate * b.rod_run as labor_amount, " _
                & "  d.wc_mch_op, " _
                & "  d.wc_mch_bdn_rate,d.wc_mch_op* d.wc_mch_bdn_rate * b.rod_run as mach_amount, " _
                & "  d.wc_setup_men, " _
                & "  d.wc_setup_rate, " _
                & "  c.ps_ratio,d.wc_setup_men * d.wc_setup_rate * c.ps_ratio as setup_amount, " _
                & " (d.wc_men_mch* d.wc_lbr_rate * coalesce(b.rod_run,0)) + (d.wc_mch_op* d.wc_mch_bdn_rate * coalesce(b.rod_run,0)) + (d.wc_setup_men * d.wc_setup_rate * c.ps_ratio) as total_amount , " _
                & "  b.rod_op, " _
                & "  d.wc_desc,ps_ratio " _
                & "FROM " _
                & "  public.ro_mstr a " _
                & "  inner join  public.get_ps_first(" & cogsc_pt_id.Tag & ", 1) f on (a.ro_pt_id=f.psd_pt_bom_id)  " _
                & "  INNER JOIN public.rod_det b ON (a.ro_oid = b.rod_ro_oid) " _
                & "  INNER JOIN public.wc_mstr d ON (b.rod_wc_id = d.wc_id) " _
                & "  INNER JOIN public.ps_mstr c ON (a.ro_pt_id = c.ps_pt_bom_id) " _
                & "  INNER JOIN public.op_mstr e ON (b.rod_op = e.op_code) " _
                & "WHERE " _
                & "    variable_4>0 "

            dt_raw_nonmat = master_new.PGSqlConn.GetTableData(sSQL)
            gc_edit_dir_nonmat.DataSource = dt_raw_nonmat
            gv_edit_dir_nonmat.BestFitColumns()

            For Each dr As DataRow In dt_raw_nonmat.Rows
                _direct_nonmat_ttl += dr("total_amount")
            Next

            sSQL = "select  a.ro_code,  a.ro_desc,  b.rodi_total, b.rodi_remarks,  b.rodi_qty,  b.rodi_amount,  c.rodit_name from public.get_ps_first(" & cogsc_pt_id.Tag _
                & ",1) e inner join  public.ro_mstr a on (a.ro_pt_id=e.psd_pt_bom_id)  " _
                & " INNER JOIN public.rodi_indirect b ON (a.ro_oid = b.rodi_ro_oid) " _
                & " INNER JOIN public.rodi_type_mstr c ON (b.rodi_type = c.rodit_code) " _
                & " where  variable_4>0 and c.rodit_is_material = 'Y'"

            dt_indirect_mat = master_new.PGSqlConn.GetTableData(sSQL)

            gc_edit_indir_mat.DataSource = dt_indirect_mat
            gv_edit_indir_mat.BestFitColumns()

            For Each dr As DataRow In dt_indirect_mat.Rows
                _indirect_mat_ttl += dr("rodi_total")
            Next

            sSQL = "select  a.ro_code,  a.ro_desc,  b.rodi_total, b.rodi_remarks,  b.rodi_qty,  b.rodi_amount,  c.rodit_name from public.get_ps_first(" & cogsc_pt_id.Tag _
                & ",1) e inner join  public.ro_mstr a on (a.ro_pt_id=e.psd_pt_bom_id)  " _
                & " INNER JOIN public.rodi_indirect b ON (a.ro_oid = b.rodi_ro_oid) " _
                & " INNER JOIN public.rodi_type_mstr c ON (b.rodi_type = c.rodit_code) " _
                & " where  variable_4>0 and c.rodit_is_material = 'N'"

            'Dim dt_indirect_nonmat As New DataTable
            dt_indirect_nonmat = master_new.PGSqlConn.GetTableData(sSQL)

            gc_edit_indir_nonmat.DataSource = dt_indirect_nonmat
            gv_edit_indir_nonmat.BestFitColumns()

            For Each dr As DataRow In dt_indirect_nonmat.Rows
                _indirect_nonmat_ttl += dr("rodi_total")
            Next
            cogsc_direct_material_ttl.EditValue = _direct_mat_ttl
            cogsc_direct_non_material_ttl.EditValue = _direct_nonmat_ttl
            cogsc_indirect_material_ttl.EditValue = _indirect_mat_ttl
            cogsc_indirect_non_material_ttl.EditValue = _indirect_nonmat_ttl
            cogsc_total.EditValue = (_direct_mat_ttl + _direct_nonmat_ttl + _indirect_mat_ttl + _indirect_nonmat_ttl)
            cogsc_unit.EditValue = (_direct_mat_ttl + _direct_nonmat_ttl + _indirect_mat_ttl + _indirect_nonmat_ttl) / cogsc_qty.EditValue

            Box("Generate Success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub cogsc_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cogsc_pt_id.ButtonClick
        Dim frm As New FProdStrucSearch()
        frm.set_win(Me)
        frm._obj = cogsc_pt_id
        frm._en_id = cogsc_en_id.EditValue
        frm._prj_code = ""
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub cogsc_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cogsc_qty.EditValueChanged
        Try
            cogsc_qty_prod.EditValue = Math.Round(cogsc_qty.EditValue / cogsc_yield.EditValue, 0)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
