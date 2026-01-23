Imports npgsql
Imports master_new.ModFunction

Public Class FCanvasingSalesPlanning

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _cnv_oid_mstr As String
    Dim _now As DateTime
    Public ds_edit_item, ds_edit_trans As DataSet

    Private Sub FCanvasingSalesPlanning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        AddHandler gv_edit_item.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_edit_item.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        cnv_en_id.Properties.DataSource = dt_bantu
        cnv_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cnv_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cnv_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_so_type())
        cnv_so_type.Properties.DataSource = dt_bantu
        cnv_so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        cnv_so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        cnv_so_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        cnv_cu_id.Properties.DataSource = dt_bantu
        cnv_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        cnv_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        cnv_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_area_mstr())
        cnv_region.Properties.DataSource = dt_bantu
        cnv_region.Properties.DisplayMember = dt_bantu.Columns("area_name").ToString
        cnv_region.Properties.ValueMember = dt_bantu.Columns("area_id").ToString
        cnv_region.ItemIndex = 0

    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", cnv_en_id.EditValue))
        cnv_ptnrg_id.Properties.DataSource = dt_bantu
        cnv_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        cnv_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        cnv_ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_promo_mstr(cnv_en_id.EditValue))
        cnv_promo_id.Properties.DataSource = dt_bantu
        cnv_promo_id.Properties.DisplayMember = dt_bantu.Columns("promo_desc").ToString
        cnv_promo_id.Properties.ValueMember = dt_bantu.Columns("promo_id").ToString
        cnv_promo_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales_program(cnv_en_id.EditValue))
        cnv_sales_program.Properties.DataSource = dt_bantu
        cnv_sales_program.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        cnv_sales_program.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        cnv_sales_program.ItemIndex = 0
    End Sub

    Private Sub cnv_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cnv_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "cnv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "cnv_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Type", "cnv_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Programe", "sales_program_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Start Date", "cnv_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "cnv_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "cnv_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cnv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cnv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cnv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cnv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_item, "cnvd_cnv_oid", False)
        add_column_copy(gv_detail_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Qty", "cnvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_item, "Unit Measure", "code_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_trans, "cnvd_cnv_oid", False)
        add_column(gv_edit_item, "cnvd_oid", False)
        add_column(gv_edit_item, "cnvd_pt_id", False)
        add_column(gv_edit_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_item, "cnvd_loc_id", False)
        'add_column(gv_edit_item, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_item, "Qty", "cnvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_item, "cnvd_um", False)
        add_column(gv_edit_item, "Unit Measure", "code_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_trans, "cnvd_cnv_oid", False)
        add_column_copy(gv_detail_trans, "Transfer Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trans, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trans, "Closing Date", "cnvdd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_trans, "Cash In", "cnvdd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_trans, "Transfer Return", "cnvdd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_detail_trans, "Interval", "cnvdd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail_trans, "Payment", "cnvdd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_trans, "Min. Qty", "cnvdd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_trans, "Sales Unit", "cnvdd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column(gv_edit_trans, "cnvdd_oid", False)
        'add_column(gv_edit_trans, "cnvdd_cnvd_oid", False)
        'add_column(gv_edit_trans, "cnvdd_payment_type", False)
        'add_column(gv_edit_trans, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit_trans, "Price", "cnvdd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_trans, "Discount", "cnvdd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_edit(gv_edit_trans, "Prepayment", "cnvdd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_edit(gv_edit_trans, "Interval", "cnvdd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_edit(gv_edit_trans, "Payment", "cnvdd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_trans, "Min. Qty", "cnvdd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_trans, "Sales Unit", "cnvdd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.cnv_mstr.cnv_oid, " _
                    & "  public.cnv_mstr.cnv_id, " _
                    & "  public.cnv_mstr.cnv_en_id, " _
                    & "  public.cnv_mstr.cnv_code, " _
                    & "  public.cnv_mstr.cnv_desc, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.cnv_mstr.cnv_date, " _
                    & "  public.cnv_mstr.cnv_sls_id, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.cnv_mstr.cnv_region, " _
                    & "  public.area_mstr.area_name, " _
                    & "  public.cnv_mstr.cnv_so_type, " _
                    & "  public.cnv_mstr.cnv_pi_id, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.cnv_mstr.cnv_ptnrg_id, " _
                    & "  public.ptnrg_grp.ptnrg_name, " _
                    & "  public.cnv_mstr.cnv_promo_id, " _
                    & "  public.promo_mstr.promo_desc, " _
                    & "  public.cnv_mstr.cnv_prg_id, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.cnv_mstr.cnv_loc_id, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.cnv_mstr.cnv_start_date, " _
                    & "  public.cnv_mstr.cnv_end_date, " _
                    & "  public.cnv_mstr.cnv_cap, " _
                    & "  public.cnv_mstr.cnv_cash, " _
                    & "  public.cnv_mstr.cnv_ttl, " _
                    & "  public.cnv_mstr.cnv_close_date, " _
                    & "  public.cnv_mstr.cnv_active, " _
                    & "  public.cnv_mstr.cnv_add_by, " _
                    & "  public.cnv_mstr.cnv_add_date, " _
                    & "  public.cnv_mstr.cnv_upd_by, " _
                    & "  public.cnv_mstr.cnv_upd_date " _
                    & "FROM " _
                    & "  public.cnv_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.cnv_mstr.cnv_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.cnv_mstr.cnv_sls_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.area_mstr ON (public.cnv_mstr.cnv_region = public.area_mstr.area_id) " _
                    & "  INNER JOIN public.pi_mstr ON (public.cnv_mstr.cnv_pi_id = public.pi_mstr.pi_id) " _
                    & "  LEFT OUTER JOIN public.ptnrg_grp ON (public.cnv_mstr.cnv_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                    & "  INNER JOIN public.promo_mstr ON (public.cnv_mstr.cnv_promo_id = public.promo_mstr.promo_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.cnv_mstr.cnv_prg_id = public.code_mstr.code_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.cnv_mstr.cnv_loc_id = public.loc_mstr.loc_id) " _
                    & "  where cnv_en_id in (select user_en_id from tconfuserentity " _
                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String = ""

        Try
            ds.Tables("detail_item").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  cnvd_oid, " _
            & "  cnvd_add_by, " _
            & "  cnvd_add_date, " _
            & "  cnvd_upd_date, " _
            & "  cnvd_upd_by, " _
            & "  cnvd_cnv_oid, " _
            & "  cnvd_pt_id, " _
            & "  pt_code, pt_desc1, pt_desc2, " _
            & "  cnvd_loc_id, loc_desc, " _
            & "  cnvd_um, code_desc, " _
            & "  cnvd_dt " _
            & "FROM  " _
            & "  public.cnvd_det " _
            & "  inner join pt_mstr on pt_id = cnvd_pt_id " _
            & "  inner join loc_mstr on loc_id = cnvd_loc_id " _
            & "  inner join code_mstr on code_id = cnvd_um "

        load_data_detail(sql, gc_detail_item, "detail_item")

        Try
            ds.Tables("detail_trans").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
        & "  public.cnvdd_det.cnvdd_oid, " _
        & "  public.cnvdd_det.cnvdd_cnv_oid, " _
        & "  public.cnvdd_det.cnvdd_pb_ref_oid, " _
        & "  public.cnvdd_det.cnvdd_pb_code, " _
        & "  public.cnvdd_det.cnvdd_pb_date, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_ref_oid, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_code, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_date, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_rec_date, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_rtr_oid, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_rtr_code, " _
        & "  public.cnvdd_det.cnvdd_ptsfr_rtr_date, " _
        & "  public.cnvdd_det.cnvdd_dt, " _
        & "  public.cnvdd_det.cnvdd_cnvd_oid " _
        & "FROM " _
        & "  public.cnvdd_det"

        load_data_detail(sql, gc_detail_trans, "detail_trans")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_item.Columns("cnvd_cnv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cnvd_cnv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid").ToString & "'")
            gv_detail_item.BestFitColumns()

            gv_detail_trans.Columns("cnvd_cnv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cnvd_cnv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid").ToString & "'")
            gv_detail_trans.BestFitColumns()
        Catch ex As Exception
        End Try

        'Try
        '    gv_edit_trans.Columns("cnvdd_cnvd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cnvdd_cnvd_oid='" & ds_edit_item.Tables(0).Rows(BindingContext(ds_edit_item.Tables(0)).Position).Item("cnvd_oid").ToString & "'")
        '    gv_edit_trans.BestFitColumns()
        'Catch ex As Exception
        'End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        cnv_en_id.Focus()
        cnv_en_id.ItemIndex = 0
        cnv_code.EditValue = False
        cnv_code.Text = ""
        cnv_desc.Text = ""
        cnv_sls_id.Text = ""
        cnv_sls_id.Tag = ""
        cnv_pi_id.Text = ""
        cnv_pi_id.Tag = ""
        cnv_loc_id.Text = ""
        cnv_loc_id.Tag = ""

        cnv_so_type.ItemIndex = 0
        cnv_promo_id.Text = ""
        cnv_cu_id.ItemIndex = 0
        cnv_sales_program.Text = ""
        cnv_date.DateTime = _now
        cnv_start_date.DateTime = _now
        cnv_end_date.DateTime = _now
        cnv_active.EditValue = True
        cnv_ptnrg_id.Text = ""
        'cnv_loc_id.ItemIndex = 0
        cnv_region.ItemIndex = 0

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_item = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  cnvd_oid, " _
                        & "  cnvd_add_by, " _
                        & "  cnvd_add_date, " _
                        & "  cnvd_upd_date, " _
                        & "  cnvd_upd_by, " _
                        & "  cnvd_cnv_oid, " _
                        & "  cnvd_pt_id, " _
                        & "  pt_code, pt_desc1, pt_desc2, " _
                        & "  cnvd_dt " _
                        & "FROM  " _
                        & "  public.cnvd_det " _
                        & " inner join pt_mstr on pt_id = cnvd_pt_id " _
                        & "  where cnvd_pt_id = -99 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit_item, "item")
                    gc_edit_item.DataSource = ds_edit_item.Tables(0)
                    gv_edit_item.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'ds_edit_trans = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT  " _
        '                & "  cnvdd_oid, " _
        '                & "  cnvdd_add_by, " _
        '                & "  cnvdd_add_date, " _
        '                & "  cnvdd_upd_date, " _
        '                & "  cnvdd_upd_by, " _
        '                & "  cnvdd_cnvd_oid, " _
        '                & "  cnvdd_payment_type, " _
        '                & "  code_name as payment_type_name, " _
        '                & "  cnvdd_price, " _
        '                & "  cnvdd_disc, " _
        '                & "  cnvdd_dp, " _
        '                & "  cnvdd_interval, " _
        '                & "  cnvdd_payment, " _
        '                & "  cnvdd_min_qty, " _
        '                & "  cnvdd_sales_unit, " _
        '                & "  cnvdd_dt " _
        '                & "FROM  " _
        '                & "  public.cnvdd_det " _
        '                & "  inner join public.code_mstr on code_id = cnvdd_payment_type " _
        '                & " where cnvdd_price = -99"
        '            .InitializeCommand()
        '            .FillDataSet(ds_edit_trans, "rule")
        '            gc_edit_trans.DataSource = ds_edit_trans.Tables(0)
        '            gv_edit_trans.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

    End Function

    Private Sub cvn_sls_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cnv_sls_id.ButtonClick

        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._obj = cnv_sls_id
        frm._en_id = cnv_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    Private Sub cnv_loc_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cnv_loc_id.ButtonClick

        Dim frm As New FLocationCanvasingSearch
        frm.set_win(Me)
        frm._obj = cnv_loc_id
        frm._sls_id = cnv_sls_id.Tag
        frm._en_id = cnv_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    Private Sub cnv_pi_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cnv_pi_id.ButtonClick

        Dim frm As New FPriceListCanvasingSearch()
        frm.set_win(Me)
        frm._en_id = cnv_en_id.EditValue
        frm._obj = cnv_pi_id
        frm._start = cnv_start_date.EditValue
        frm._end = cnv_end_date.EditValue
        frm._so_type = cnv_so_type.EditValue
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    Private Sub gv_edit_item_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_item.DoubleClick
        Dim _col As String = gv_edit_item.FocusedColumn.Name
        Dim _row As Integer = gv_edit_item.FocusedRowHandle

        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = cnv_en_id.EditValue
            frm._pi_id = cnv_pi_id.Tag
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_item_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_item.InitNewRow
        With gv_edit_item
            .SetRowCellValue(e.RowHandle, "cnvd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "cnvd_qty", 0)
        End With
    End Sub

    'Private Sub gv_edit_trans_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_trans.DoubleClick
    '    Dim _col As String = gv_edit_trans.FocusedColumn.Name
    '    Dim _row As Integer = gv_edit_trans.FocusedRowHandle

    '    If _col = "payment_type_name" Then
    '        Dim frm As New FPaymentTypeSearch
    '        frm.set_win(Me)
    '        frm._row = _row
    '        frm._en_id = cnv_en_id.EditValue
    '        frm.type_form = True
    '        frm.ShowDialog()
    '    End If
    'End Sub

    'Private Sub gv_edit_trans_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_trans.InitNewRow
    '    With gv_edit_trans
    '        .SetRowCellValue(e.RowHandle, "cnvdd_oid", Guid.NewGuid.ToString)
    '        .SetRowCellValue(e.RowHandle, "cnvdd_cnvd_oid", ds_edit_item.Tables(0).Rows(BindingContext(ds_edit_item.Tables(0)).Position).Item("cnvd_oid"))
    '        .SetRowCellValue(e.RowHandle, "cnvdd_price", 0)
    '        .SetRowCellValue(e.RowHandle, "cnvdd_disc", 0)
    '        .SetRowCellValue(e.RowHandle, "cnvdd_dp", 0)
    '    End With
    'End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit_item.UpdateCurrentRow()
        'gv_edit_trans.UpdateCurrentRow()

        ds_edit_item.AcceptChanges()
        ds_edit_trans.AcceptChanges()

        'Ini Diperbolehkan kosong...,kalau kosong artinya berlaku untuk semua barang
        'If ds_edit_item.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If ds_edit_trans.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'Dim i As Integer

        Return before_save
    End Function

    Public Sub gv_edit_item_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_item.CellValueChanged
        'Dim _riud_um_conv As Double = 1
        Dim _cnvd_qty As Double = 1
        'Dim _riud_qty_real As Double
        'Dim _qty_open As Double = 0

        If e.Column.Name = "cnvd_qty" Then

            Try
                _cnvd_qty = (gv_edit_item.GetRowCellValue(e.RowHandle, "cnvd_qty"))
            Catch ex As Exception
            End Try

            'edit by har 20110505
            '    If _ref_pb_inventory_issue_par <> "0" Then
            '        If e.Value > _qty_open Then
            '            MessageBox.Show("Qty Issue Can't Higher Than Qty Open..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '            gv_edit.CancelUpdateCurrentRow()
            '            Exit Sub
            '        End If
            '    End If

            '    '********************************

            '    Try
            '        _riud_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "riud_um_conv"))
            '    Catch ex As Exception
            '    End Try

            '    _riud_qty_real = e.Value * _riud_um_conv
            '    gv_edit.SetRowCellValue(e.RowHandle, "riud_qty_real", _riud_qty_real)
            'ElseIf e.Column.Name = "riud_um_conv" Then
            '    Try
            '        _riud_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "riud_qty")))
            '    Catch ex As Exception
            '    End Try

            '    _riud_qty_real = e.Value * _riud_qty
            '    gv_edit.SetRowCellValue(e.RowHandle, "riud_qty_real", _riud_qty_real)
        End If
    End Sub


    Public Overrides Function insert() As Boolean
        Dim _cnv_oid As Guid = Guid.NewGuid
        Dim _cnv_code As String
        Dim i As Integer
        Dim ssqls As New ArrayList

        _cnv_code = func_coll.get_transaction_number("CN", cnv_en_id.GetColumnValue("en_code"), "cnv_mstr", "cnv_code")
        ssqls.Clear()

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
                                            & "  public.cnv_mstr " _
                                            & "( " _
                                            & "  cnv_oid, " _
                                            & "  cnv_dom_id, " _
                                            & "  cnv_en_id, " _
                                            & "  cnv_add_by, " _
                                            & "  cnv_add_date, " _
                                            & "  cnv_id, " _
                                            & "  cnv_code, " _
                                            & "  cnv_desc, " _
                                            & "  cnv_sls_id, " _
                                            & "  cnv_region, " _
                                            & "  cnv_so_type, " _
                                            & "  cnv_pi_id, " _
                                            & "  cnv_ptnrg_id, " _
                                            & "  cnv_promo_id, " _
                                            & "  cnv_prg_id, " _
                                            & "  cnv_cu_id, " _
                                            & "  cnv_loc_id, " _
                                            & "  cnv_cap, " _
                                            & "  cnv_cash, " _
                                            & "  cnv_start_date, " _
                                            & "  cnv_end_date, " _
                                            & "  cnv_active, " _
                                            & "  cnv_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_cnv_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(cnv_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("cnv_mstr", cnv_en_id.GetColumnValue("en_code"), "cnv_id", "cnv_en_id", cnv_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(_cnv_code) & ",  " _
                                            & SetSetring(cnv_desc.Text) & ",  " _
                                            & SetInteger(cnv_sls_id.Tag) & ",  " _
                                            & SetSetring(cnv_region.EditValue) & ",  " _
                                            & SetSetring(cnv_so_type.EditValue) & ",  " _
                                            & SetInteger(cnv_pi_id.Tag) & ",  " _
                                            & SetInteger(cnv_ptnrg_id.Tag) & ",  " _
                                            & SetInteger(cnv_promo_id.Tag) & ",  " _
                                            & SetInteger(cnv_sales_program.Tag) & ",  " _
                                            & SetInteger(cnv_cu_id.EditValue) & ",  " _
                                            & SetInteger(cnv_loc_id.Tag) & ",  " _
                                            & SetSetring(cnv_cap.EditValue) & ",  " _
                                            & SetSetring(cnv_cash.EditValue) & ",  " _
                                            & SetDate(cnv_start_date.DateTime) & ",  " _
                                            & SetDate(cnv_end_date.DateTime) & ",  " _
                                            & SetBitYN(cnv_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.cnvd_det " _
                                                & "( " _
                                                & "  cnvd_oid, " _
                                                & "  cnvd_add_by, " _
                                                & "  cnvd_add_date, " _
                                                & "  cnvd_cnv_oid, " _
                                                & "  cnvd_pt_id, " _
                                                & "  cnvd_loc, " _
                                                & "  cnvd_qty, " _
                                                & "  cnvd_um, " _
                                                & "  cnvd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("cnvd_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_cnv_oid.ToString) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("cnvd_pt_id").ToString) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("cnvd_loc_id").ToString) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("cnvd_loc_id").ToString) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("cnvd_qty").ToString) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        ''Untuk Update data rule
                        'For i = 0 To ds_edit_trans.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.cnvdd_det " _
                        '                        & "( " _
                        '                        & "  cnvdd_oid, " _
                        '                        & "  cnvdd_add_by, " _
                        '                        & "  cnvdd_add_date, " _
                        '                        & "  cnvdd_cnvd_oid, " _
                        '                        & "  cnvdd_payment_type, " _
                        '                        & "  cnvdd_price, " _
                        '                        & "  cnvdd_disc, " _
                        '                        & "  cnvdd_dp, " _
                        '                        & "  cnvdd_interval, " _
                        '                        & "  cnvdd_payment, " _
                        '                        & "  cnvdd_min_qty, " _
                        '                        & "  cnvdd_sales_unit, " _
                        '                        & "  cnvdd_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_oid").ToString) & ",  " _
                        '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        '                        & SetSetring(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_cnvd_oid").ToString) & ",  " _
                        '                        & SetInteger(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_payment_type")) & ",  " _
                        '                        & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_price")) & ",  " _
                        '                        & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_disc")) & ",  " _
                        '                        & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_dp")) & ",  " _
                        '                        & SetDblDB(0) & ",  " _
                        '                        & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_payment")) & ",  " _
                        '                        & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_min_qty")) & ",  " _
                        '                        & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_sales_unit")) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ")"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(_cnv_oid.ToString, "cnv_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from cnv_mstr where cnv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If MyPGDll.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cnv_oid_mstr = .Item("cnv_oid").ToString
                cnv_en_id.EditValue = .Item("cnv_en_id")
                cnv_code.Text = .Item("cnv_code")
                cnv_desc.Text = SetString(.Item("cnv_desc"))
                cnv_so_type.EditValue = .Item("cnv_so_type")
                cnv_promo_id.EditValue = .Item("cnv_promo_id")
                cnv_cu_id.EditValue = .Item("cnv_cu_id")
                cnv_sales_program.EditValue = .Item("cnv_sales_program")
                cnv_start_date.DateTime = .Item("cnv_start_date")
                cnv_end_date.DateTime = .Item("cnv_end_date")
                cnv_active.EditValue = SetBitYNB(.Item("cnv_active"))
                cnv_ptnrg_id.EditValue = .Item("cnv_ptnrg_id")
            End With

            cnv_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_item = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  cnvd_oid, " _
                            & "  cnvd_add_by, " _
                            & "  cnvd_add_date, " _
                            & "  cnvd_upd_date, " _
                            & "  cnvd_upd_by, " _
                            & "  cnvd_cnv_oid, " _
                            & "  cnvd_pt_id, " _
                            & "  pt_code, pt_desc1, pt_desc2, " _
                            & "  cnvd_dt " _
                            & "FROM  " _
                            & "  public.cnvd_det " _
                            & " inner join pt_mstr on pt_id = cnvd_pt_id " _
                            & " inner join cnv_mstr on cnv_oid = cnvd_cnv_oid " _
                            & "  where cnv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid") + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_item, "cnvd_det")
                        gc_edit_item.DataSource = ds_edit_item.Tables(0)
                        gv_edit_item.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'ds_edit_trans = New DataSet
            'Try
            '    Using objcb As New master_new.WDABasepgsql("", "")
            '        With objcb
            '            .SQL = "SELECT  " _
            '                & "  cnvdd_oid, " _
            '                & "  cnvdd_add_by, " _
            '                & "  cnvdd_add_date, " _
            '                & "  cnvdd_upd_date, " _
            '                & "  cnvdd_upd_by, " _
            '                & "  cnvdd_cnvd_oid, " _
            '                & "  cnvdd_payment_type, " _
            '                & "  code_name as payment_type_name, " _
            '                & "  cnvdd_price, " _
            '                & "  cnvdd_disc, " _
            '                & "  cnvdd_dp, " _
            '                & "  cnvdd_interval, " _
            '                & "  cnvdd_payment, " _
            '                & "  cnvdd_min_qty, " _
            '                & "  cnvdd_sales_unit, " _
            '                & "  cnvdd_dt, pt_code, pt_desc1, pt_desc2 " _
            '                & "FROM  " _
            '                & "  public.cnvdd_det " _
            '                & "  inner join public.code_mstr on code_id = cnvdd_payment_type " _
            '                & "  inner join public.cnvd_det on cnvd_oid = cnvdd_cnvd_oid " _
            '                & "  inner join public.cnv_mstr on cnv_oid = cnvd_cnv_oid " _
            '                & "  inner join public.pt_mstr on pt_id = cnvd_pt_id " _
            '                & "  where cnv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid") + "'"

            '            .InitializeCommand()
            '            .FillDataSet(ds_edit_trans, "rule")
            '            gc_edit_trans.DataSource = ds_edit_trans.Tables(0)
            '            gv_edit_trans.BestFitColumns()
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim i As Integer
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
                                            & "  public.cnv_mstr   " _
                                            & "SET  " _
                                            & "  cnv_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  cnv_en_id = " & SetInteger(cnv_en_id.EditValue) & ",  " _
                                            & "  cnv_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  cnv_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  cnv_code = " & SetSetring(cnv_code.Text) & ",  " _
                                            & "  cnv_desc = " & SetSetring(cnv_desc.Text) & ",  " _
                                            & "  cnv_so_type = " & SetSetring(cnv_so_type.EditValue) & ",  " _
                                            & "  cnv_promo_id = " & SetInteger(cnv_promo_id.EditValue) & ",  " _
                                            & "  cnv_ptnrg_id = " & SetInteger(cnv_ptnrg_id.EditValue) & ",  " _
                                            & "  cnv_cu_id = " & SetInteger(cnv_cu_id.EditValue) & ",  " _
                                            & "  cnv_sales_program = " & SetInteger(cnv_sales_program.EditValue) & ",  " _
                                            & "  cnv_start_date = " & SetDate(cnv_start_date.DateTime) & ",  " _
                                            & "  cnv_end_date = " & SetDate(cnv_end_date.DateTime) & ",  " _
                                            & "  cnv_active = " & SetBitYN(cnv_active.EditValue) & ",  " _
                                            & "  cnv_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cnv_oid = " & SetSetring(_cnv_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cnvd_det where cnvd_cnv_oid = '" + _cnv_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.cnvd_det " _
                                                & "( " _
                                                & "  cnvd_oid, " _
                                                & "  cnvd_add_by, " _
                                                & "  cnvd_add_date, " _
                                                & "  cnvd_cnv_oid, " _
                                                & "  cnvd_pt_id, " _
                                                & "  cnvd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("cnvd_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_cnv_oid_mstr) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("cnvd_pt_id").ToString) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Update data rule
                        For i = 0 To ds_edit_trans.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.cnvdd_det " _
                                                & "( " _
                                                & "  cnvdd_oid, " _
                                                & "  cnvdd_add_by, " _
                                                & "  cnvdd_add_date, " _
                                                & "  cnvdd_cnvd_oid, " _
                                                & "  cnvdd_payment_type, " _
                                                & "  cnvdd_price, " _
                                                & "  cnvdd_disc, " _
                                                & "  cnvdd_dp, " _
                                                & "  cnvdd_interval, " _
                                                & "  cnvdd_payment, " _
                                                & "  cnvdd_min_qty, " _
                                                & "  cnvdd_sales_unit, " _
                                                & "  cnvdd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_cnvd_oid").ToString) & ",  " _
                                                & SetInteger(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_payment_type")) & ",  " _
                                                & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_price")) & ",  " _
                                                & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_disc")) & ",  " _
                                                & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_dp")) & ",  " _
                                                & SetDblDB(0) & ",  " _
                                                & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_payment")) & ",  " _
                                                & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_min_qty")) & ",  " _
                                                & SetDblDB(ds_edit_trans.Tables(0).Rows(i).Item("cnvdd_sales_unit")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_cnv_oid_mstr, "cnv_oid")
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

End Class

