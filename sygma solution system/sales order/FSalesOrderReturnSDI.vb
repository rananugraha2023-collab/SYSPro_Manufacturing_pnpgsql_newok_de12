Imports npgsql
Imports master_new.ModFunction

Public Class FSalesOrderReturnSDI
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _so_oid As String
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime

    Private Sub FSalesOrderReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        soship_en_id.Properties.DataSource = dt_bantu
        soship_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        soship_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        soship_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        soship_cu_id.Properties.DataSource = dt_bantu
        soship_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        soship_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        soship_cu_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(soship_en_id.EditValue))
        soship_si_id.Properties.DataSource = dt_bantu
        soship_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        soship_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        soship_si_id.ItemIndex = 0
    End Sub

    Private Sub soship_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles soship_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "soship_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "soship_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "soship_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "soship_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "soship_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "soshipd_soship_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Return", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice", "soshipd_qty_inv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "soshipd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Serial Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Close Line", "soshipd_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_serial, "soshipd_soship_oid", False)
        add_column_copy(gv_detail_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "soshipds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "soshipd_oid", False)
        add_column(gv_edit, "soshipd_sod_oid", False)
        add_column(gv_edit, "soship_so_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "soshipd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "soshipd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Return", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "soshipd_um", False)
        add_column(gv_edit, "UM", "soshipd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Supplier Lot Number", "soshipd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "soshipd_rea_code_id", False)
        add_column(gv_edit, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Sales Unit Total", "sod_sales_unit_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sod_cost", False)
        add_column(gv_edit, "so_cu_id", False)
        add_column(gv_edit, "so_exc_rate", False)

        add_column(gv_serial, "soshipds_oid", False)
        add_column(gv_serial, "soshipds_soshipd_oid", False)
        add_column(gv_serial, "soship_so_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "soshipds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "soshipds_si_id", False)
        add_column(gv_serial, "soshipds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_type", False)
        add_column(gv_serial, "soshipd_sod_oid", False)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  soship_oid, " _
                    & "  so_oid, " _
                    & "  so_code, " _
                    & "  soship_dom_id, " _
                    & "  soship_en_id, " _
                    & "  soship_add_by, " _
                    & "  soship_add_date, " _
                    & "  soship_upd_by, " _
                    & "  soship_upd_date, " _
                    & "  soship_code, " _
                    & "  soship_date, " _
                    & "  soship_so_oid, " _
                    & "  soship_si_id, " _
                    & "  soship_is_shipment, " _
                    & "  soship_dt, " _
                    & "  so_code, " _
                    & "  cu_mstr.cu_name, " _
                    & "  soship_exc_rate, " _
                    & "  ptnr_name as ptnr_name_sold, " _
                    & "  en_desc, " _
                    & "  si_desc, code_usr_1 " _
                    & "FROM  " _
                    & "  public.soship_mstr " _
                    & "  inner join so_mstr on so_oid = soship_so_oid " _
                    & "  inner join en_mstr on en_id = soship_en_id " _
                    & "  inner join si_mstr on si_id = soship_si_id " _
                    & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold" _
                    & "  left outer join public.cu_mstr ON (public.soship_mstr.soship_cu_id = public.cu_mstr.cu_id)" _
                    & "  inner join code_mstr on code_id = so_pay_type " _
                    & " where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and soship_is_shipment ~~* 'N'" _
                    & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  soshipd_oid, " _
            & "  soshipd_soship_oid, " _
            & "  soshipd_sod_oid, " _
            & "  soship_so_oid, " _
            & "  soshipd_seq, " _
            & "  soshipd_qty, " _
            & "  soshipd_qty_inv, " _
            & "  soshipd_um, " _
            & "  soshipd_um_conv, " _
            & "  soshipd_cancel_bo, " _
            & "  soshipd_qty_real, " _
            & "  soshipd_si_id, " _
            & "  soshipd_loc_id, " _
            & "  soshipd_lot_serial, " _
            & "  soshipd_rea_code_id, " _
            & "  rea_code_mstr.code_name as rea_code_name, " _
            & "  soshipd_dt, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_ls, " _
            & "  si_desc, " _
            & "  loc_desc, soshipd_close_line, " _
            & "  um_mstr.code_name as soshipd_um_name " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id " _
            & "  inner join si_mstr on si_id = soshipd_si_id " _
            & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
            & "  inner join code_mstr um_mstr on um_mstr.code_id = soshipd_um" _
            & "  left outer join code_mstr rea_code_mstr on rea_code_mstr.code_id = soshipd_rea_code_id" _
            & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and soship_is_shipment ~~* 'N'"

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  soshipds_oid, " _
            & "  soshipds_soshipd_oid, " _
            & "  soship_so_oid, " _
            & "  soshipds_seq, " _
            & "  soshipds_qty,  " _
            & "  soshipds_qty_real, " _
            & "  soshipds_si_id, " _
            & "  soshipds_loc_id, " _
            & "  soshipds_lot_serial, " _
            & "  soshipds_dt, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  si_desc, " _
            & "  loc_desc " _
            & "FROM  " _
            & "  public.soshipds_serial " _
            & "  inner join soshipd_det on soshipd_oid = soshipds_soshipd_oid " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id  " _
            & "  inner join si_mstr on si_id = soshipds_si_id " _
            & "  inner join loc_mstr on loc_id = soshipds_loc_id" _
            & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and soship_is_shipment ~~* 'N'"

        load_data_detail(sql, gc_detail_serial, "detail_serial")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("soshipd_soship_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soshipd_soship_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString & "'")
            'gv_detail.Columns("soship_so_oid").FilterInfo = "0
            gv_detail.BestFitColumns()

            gv_detail_serial.Columns("soshipd_soship_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soshipd_soship_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        soship_en_id.Focus()
        soship_en_id.ItemIndex = 0
        soship_so_oid.Text = ""
        soship_date.DateTime = _now
        soship_si_id.ItemIndex = 0
        soship_exc_rate.Enabled = False
        soship_cu_id.ItemIndex = 0

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
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
                        & "  soshipd_oid, " _
                        & "  soshipd_soship_oid, " _
                        & "  soshipd_sod_oid, " _
                        & "  soshipd_seq, " _
                        & "  soshipd_qty, 0 as qty_open, " _
                        & "  soshipd_um, " _
                        & "  soshipd_um_conv, " _
                        & "  soshipd_cancel_bo, " _
                        & "  soshipd_qty_real, " _
                        & "  soshipd_si_id, " _
                        & "  soshipd_loc_id, " _
                        & "  soshipd_lot_serial, " _
                        & "  soshipd_rea_code_id, " _
                        & "  rea_code_mstr as rea_code_name, " _
                        & "  soshipd_dt, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_ls, " _
                        & "  pt_type, " _
                        & "  si_desc, " _
                        & "  loc_desc, " _
                        & "  so_cu_id, " _
                        & "  so_exc_rate, " _
                        & "  sod_pt_id, " _
                        & "  sod_cost, " _
                        & "  sod_taxable, " _
                        & "  sod_tax_class, " _
                        & "  sod_tax_inc, " _
                        & "  sod_price, " _
                        & "  sod_disc, " _
                        & "  sod_sales_unit, " _
                        & "  sod_sales_unit_total, " _
                        & "  soshipd_close_line, " _
                        & "  pay_type.code_usr_1 as pay_type_interval, " _
                        & "  um_mstr.code_name as soshipd_um_name " _
                        & "FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                        & "  inner join si_mstr on si_id = soshipd_si_id " _
                        & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = soshipd_um" _
                        & "  left outer join code_mstr rea_code_mstr on rea_code_mstr.code_id = soshipd_rea_code_id" _
                        & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                        & "  where soshipd_lot_serial >= 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_serial = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  soshipds_oid, " _
                        & "  soshipds_soshipd_oid, " _
                        & "  soshipds_seq, " _
                        & "  soshipds_qty, " _
                        & "  soshipds_qty_real, " _
                        & "  soshipds_si_id, " _
                        & "  soshipds_loc_id, " _
                        & "  '' as soshipd_sod_oid, " _
                        & "  soshipds_lot_serial, " _
                        & "  soshipds_dt, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_type, " _
                        & "  si_desc, " _
                        & "  loc_desc " _
                        & "FROM  " _
                        & "  public.soshipds_serial " _
                        & "  inner join soshipd_det on soshipd_oid = soshipds_soshipd_oid " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id  " _
                        & "  inner join si_mstr on si_id = soshipds_si_id " _
                        & "  inner join loc_mstr on loc_id = soshipds_loc_id" _
                        & " where pt_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_serial, "serial")
                    gc_serial.DataSource = ds_serial.Tables(0)
                    gv_serial.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        gv_serial.UpdateCurrentRow()

        ds_edit.AcceptChanges()
        ds_serial.AcceptChanges()

        If soship_date.DateTime.Month <> master_new.PGSqlConn.CekTanggal.Month Then
            Box("Returns are not allowed to pass through the current month")
            Return False
        End If

        Dim _date As Date = soship_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(soship_en_id.EditValue, "gcald_so", _date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim i, j As Integer
        Dim _qty, _qty_ttl_serial As Double

        '*********************
        'Cek Location
        Dim sSQL As String
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")) = True Then
                    MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                Else
                    sSQL = "SELECT loc_id " _
                        & "FROM " _
                        & "  public.loc_mstr a " _
                        & "WHERE " _
                        & "  a.loc_id=" & ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id") & " AND  " _
                        & "  a.loc_en_id=" & soship_en_id.EditValue

                    If master_new.PGSqlConn.CekRowSelect(sSQL) = 0 Then
                        MessageBox.Show("Location error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        BindingContext(ds_edit.Tables(0)).Position = i
                        Return False
                    End If
                End If

            End If
        Next
        '*********************

        '*********************
        'Cek Reason Code
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("soshipd_rea_code_id")) = True Then
                    MessageBox.Show("Reason Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '*********************

        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai qty lebih dari 1
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("soshipd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("soshipds_soshipd_oid").ToString Then
                        If ds_serial.Tables(0).Rows(j).Item("soshipds_qty") > 1 Then
                            MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Qty Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            BindingContext(ds_edit.Tables(0)).Position = i
                            Return False
                        End If
                    End If
                Next
            End If
        Next
        '***********************************************************************

        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
                _qty = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("soshipd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("soshipds_soshipd_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + 1
                    End If
                Next
                If _qty <> _qty_ttl_serial Then
                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '***********************************************************************

        '***********************************************************************
        'Mencari apakah receive barang yang Lot mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "L" Then
                _qty = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("soshipd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("soshipds_soshipd_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("soshipds_qty")
                    End If
                Next
                If _qty <> _qty_ttl_serial Then
                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Lot Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '***********************************************************************

        If soship_exc_rate.EditValue = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Private Sub soship_so_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles soship_so_oid.ButtonClick
        Dim frm As New FSalesOrderSearch
        frm.set_win(Me)
        frm._en_id = soship_en_id.EditValue
        frm._date = soship_date.DateTime
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _soshipd_qty, _soshipd_qty_real, _soshipd_um_conv As Double
        _soshipd_um_conv = 1
        _soshipd_qty = 1

        If e.Column.Name = "soshipd_qty" Then

            If SetNumber(e.Value) > SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "qty_open")) Then
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
            Try
                _soshipd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "soshipd_um_conv"))
            Catch ex As Exception
            End Try

            _soshipd_qty_real = e.Value * _soshipd_um_conv
            gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _soshipd_qty_real)
        ElseIf e.Column.Name = "soshipd_um_conv" Then
            Try
                _soshipd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "soshipd_qty")))
            Catch ex As Exception
            End Try

            _soshipd_qty_real = e.Value * _soshipd_qty
            gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _soshipd_qty_real)
        End If
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

        If _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = soship_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "rea_code_name" Then
            Dim frm As New FReasonCodeSOSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = soship_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            gv_serial.Columns("soshipds_soshipd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soshipds_soshipd_oid='" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("soshipd_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_serial.Columns("soshipds_soshipd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("soshipd_oid"))
            'gv_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        If IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_loc_id")) = True Then
            MessageBox.Show("Location Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        With gv_serial
            .SetRowCellValue(e.RowHandle, "soshipds_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "soshipds_soshipd_oid", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_oid"))
            .SetRowCellValue(e.RowHandle, "soshipd_sod_oid", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_sod_oid"))
            .SetRowCellValue(e.RowHandle, "soshipds_qty", 1)
            .SetRowCellValue(e.RowHandle, "soshipds_si_id", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_si_id"))
            .SetRowCellValue(e.RowHandle, "soshipds_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_loc_id"))
            .SetRowCellValue(e.RowHandle, "pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_id"))
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _soship_oid As Guid
        _soship_oid = Guid.NewGuid

        Dim _soship_code, _serial, _pt_code As String
        'Dim _cost_methode As String
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ds_bantu_ry As New DataSet
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        'Dim _soshipd_qty_real As Double

        Dim ssqls As New ArrayList

        _soship_code = func_coll.get_transaction_number("ST", soship_en_id.GetColumnValue("en_code"), "soship_mstr", "soship_code")

        _tran_id = func_coll.get_id_tran_mstr("rct-sor")
        If _tran_id = -1 Then
            MessageBox.Show("Sales Order Return In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.soship_mstr " _
                                            & "( " _
                                            & "  soship_oid, " _
                                            & "  soship_dom_id, " _
                                            & "  soship_en_id, " _
                                            & "  soship_add_by, " _
                                            & "  soship_add_date, " _
                                            & "  soship_code, " _
                                            & "  soship_date, " _
                                            & "  soship_so_oid, " _
                                            & "  soship_si_id, " _
                                            & "  soship_cu_id, " _
                                            & "  soship_exc_rate, " _
                                            & "  soship_is_shipment, " _
                                            & "  soship_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_soship_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(soship_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_soship_code) & ",  " _
                                            & SetDate(soship_date.DateTime) & ",  " _
                                            & SetSetring(_so_oid.ToString) & ",  " _
                                            & SetInteger(soship_si_id.EditValue) & ",  " _
                                            & SetInteger(soship_cu_id.EditValue) & ",  " _
                                            & SetDbl(soship_exc_rate.EditValue) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.soshipd_det " _
                                                    & "( " _
                                                    & "  soshipd_oid, " _
                                                    & "  soshipd_soship_oid, " _
                                                    & "  soshipd_sod_oid, " _
                                                    & "  soshipd_seq, " _
                                                    & "  soshipd_qty, " _
                                                    & "  soshipd_um, " _
                                                    & "  soshipd_um_conv, " _
                                                    & "  soshipd_qty_real, " _
                                                    & "  soshipd_si_id, " _
                                                    & "  soshipd_loc_id, " _
                                                    & "  soshipd_lot_serial, " _
                                                    & "  soshipd_rea_code_id, " _
                                                    & "  soshipd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_oid").ToString) & ",  " _
                                                    & SetSetring(_soship_oid.ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_um_conv")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("soshipd_lot_serial")) & ",  " _
                                                    & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("soshipd_rea_code_id")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                ''Update sod_det
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update sod_det set sod_qty_shipment = coalesce(sod_qty_shipment,0) - " + ds_edit.Tables(0).Rows(i).Item("soshipd_qty").ToString _
                                '                     & " where sod_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString + "'"

                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If
                        Next

                        'Update(sod_mstr)
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update so_mstr set so_close_date = null, so_trans_id = 'D' " _
                                                & " where so_oid = '" + _so_oid.ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Update data serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.soshipds_serial " _
                                                & "( " _
                                                & "  soshipds_oid, " _
                                                & "  soshipds_soshipd_oid, " _
                                                & "  soshipds_seq, " _
                                                & "  soshipds_qty, " _
                                                & "  soshipds_si_id, " _
                                                & "  soshipds_loc_id, " _
                                                & "  soshipds_lot_serial, " _
                                                & "  soshipds_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("soshipds_oid").ToString) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("soshipds_soshipd_oid").ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("soshipds_qty")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("soshipds_si_id")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("soshipds_loc_id")) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("soshipds_lot_serial")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        Dim rdc_su As String = ""

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
                                    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                        i_2 += 1

                                        _en_id = soship_en_id.EditValue
                                        _si_id = ds_edit.Tables(0).Rows(i).Item("soshipd_si_id")
                                        _loc_id = ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")
                                        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                        _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                        _serial = "''"
                                        _qty = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")
                                        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'Update History Inventory                        
                                        _cost = func_coll.get_cost_sod_det(ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString)
                                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "SO Return", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", soship_date.DateTime) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'cek SU dikurangi atau tidak
                                        If Check_SU.Checked = True Then
                                            rdc_su = " , sod_sales_unit_total = coalesce(sod_sales_unit_total,0) - " & SetDec(ds_edit.Tables(0).Rows(i).Item("sod_sales_unit_total").ToString)
                                        Else
                                            rdc_su = ""
                                        End If

                                        'Update(sod_det)
                                        'If ds_edit.Tables(0).Rows(i).Item("soshipd_close_line").ToString.ToUpper = "N" Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update sod_det set sod_qty_shipment = coalesce(sod_qty_shipment,0) - " + SetDec(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real").ToString) _
                                                                & rdc_su _
                                                             & " where sod_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString + "'"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                        'End If
                                    End If
                                End If
                            End If
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            If ds_serial.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                                If ds_serial.Tables(0).Rows(i).Item("soshipds_qty") > 0 Then
                                    i_2 += 1

                                    _en_id = soship_en_id.EditValue
                                    _si_id = ds_serial.Tables(0).Rows(i).Item("soshipds_si_id")
                                    _loc_id = ds_serial.Tables(0).Rows(i).Item("soshipds_loc_id")
                                    _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                                    _serial = ds_serial.Tables(0).Rows(i).Item("soshipds_lot_serial")
                                    _qty = ds_serial.Tables(0).Rows(i).Item("soshipds_qty")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory
                                    _cost = func_coll.get_cost_sod_det(ds_serial.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString)
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "SO Return", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", soship_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                        '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '        _en_id = soship_en_id.EditValue
                        '        _si_id = ds_edit.Tables(0).Rows(i).Item("soshipd_si_id")
                        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '        _qty = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")
                        '        _cost = func_coll.get_cost_sod_det(ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid"))

                        '        If _cost_methode = "F" Or _cost_methode = "L" Then
                        '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '            Return False
                        '            'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '            '    sqlTran.Rollback()
                        '            '    insert = False
                        '            '    Exit Function
                        '            'End If
                        '        ElseIf _cost_methode = "A" Then
                        '            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '                sqlTran.Rollback()
                        '                insert = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next

                        'sudah ada menu khusus untuk mengitung royalti....
                        ''Update Ke Table Royalti 'soshipd_qty
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _soshipd_qty_real = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")

                        '    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
                        '        Try
                        '            Using objcb As New master_new.WDABasepgsql("", "")
                        '                With objcb
                        '                    .SQL = "select royt_oid, royt_pt_id, royt_seq, royt_qty_so " + _
                        '                       " from royt_table " + _
                        '                       " where royt_qty_so > 0 " + _
                        '                       " and royt_pt_id  = " + ds_edit.Tables(0).Rows(i).Item("pt_id").ToString + _
                        '                       " order by royt_seq desc "
                        '                    .InitializeCommand()
                        '                    .FillDataSet(ds_bantu_ry, "royalti")
                        '                End With
                        '            End Using
                        '        Catch ex As Exception
                        '            MessageBox.Show(ex.Message)
                        '        End Try

                        '        For j = 0 To ds_bantu_ry.Tables(0).Rows.Count - 1
                        '            If _soshipd_qty_real > ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_so") Then
                        '                .Command.CommandType = CommandType.Text
                        '                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so - " + ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_so").ToString _
                        '                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                        '                ssqls.Add(.Command.CommandText)
                        '                .Command.ExecuteNonQuery()
                        '                .Command.Parameters.Clear()

                        '                _soshipd_qty_real = _soshipd_qty_real - ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_so")
                        '            Else
                        '                .Command.CommandType = CommandType.Text
                        '                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so - " + _soshipd_qty_real.ToString _
                        '                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                        '                ssqls.Add(.Command.CommandText)
                        '                .Command.ExecuteNonQuery()
                        '                .Command.Parameters.Clear()
                        '                Exit For 'karena nilai _shosipd_qty_real sudah habis...
                        '            End If
                        '        Next
                        '    End If
                        'Next
                        ''**********************************************************

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If glt_det_so_return(ssqls, objinsert, ds_edit, _soship_oid.ToString, _soship_code) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                            'by sys 20110412 kalau penjualan cash harus melakukan pengurangan kas juga 
                            'cukup ambil ds_edit yang posisi ke 0
                            If ds_edit.Tables(0).Rows(0).Item("pay_type_interval") = "0" Then
                                If jurnal_payment(ssqls, objinsert, _so_oid, soship_so_oid.Text, _soship_code) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update so_mstr set so_trans_id = 'X' " _
                                                        & " where so_oid = '" + _so_oid.ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                            '------------------
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, soship_en_id.EditValue, 12, _soship_oid.ToString, _soship_code, _now.Date) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SO Return " & _soship_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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
                        set_row(_soship_oid.ToString, "soship_oid")
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

    Private Function glt_det_so_return(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, ByVal par_soship_oid As String, ByVal par_soship_code As String) As Boolean
        glt_det_so_return = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _date As Date = soship_date.DateTime
        Dim _cost As Double
        Dim _seq As Integer = -1
        _glt_code = func_coll.get_transaction_number("IC", soship_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id"))
                        _cost = func_coll.get_cost_sod_det(par_ds.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString) * par_ds.Tables(0).Rows(i).Item("soshipd_qty")

                        dt_bantu = New DataTable
                        dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))

                        'Insert yang debit
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(soship_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("IC") & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("so_cu_id")) & ",  " _
                                            & SetDbl(par_ds.Tables(0).Rows(i).Item("so_exc_rate")) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring("SO Shipment") & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_soship_oid) & ",  " _
                                            & SetSetring(par_soship_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("IC-SOS") & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         soship_en_id.EditValue, par_ds.Tables(0).Rows(i).Item("so_cu_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("so_exc_rate"), _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If

                        dt_bantu = New DataTable
                        dt_bantu = (func_coll.get_prodline_account(_pl_id, "SL_CMACC"))

                        'Insert Untuk Yang Credit
                        _seq = _seq + 1
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(soship_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("IC") & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("so_cu_id")) & ",  " _
                                            & SetDbl(par_ds.Tables(0).Rows(i).Item("so_exc_rate")) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("SO Shipment") & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetSetring(par_soship_oid) & ",  " _
                                            & SetSetring(par_soship_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("IC-SOS") & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         soship_en_id.EditValue, par_ds.Tables(0).Rows(i).Item("so_cu_id"), _
                                                          par_ds.Tables(0).Rows(i).Item("so_exc_rate"), _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    End If

                    '********************** finish untuk yang credit
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function


    Private Function jurnal_payment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_so_oid As String, ByVal par_so_code As String, ByVal par_soship_code As String) As Boolean
        jurnal_payment = True
        'buat struktur dulu datasetnya...dengan data yang kosong
        Dim ds_edit_shipment As DataSet = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  ars_oid, True as ceklist, " _
                        & "  ars_ar_oid, " _
                        & "  ars_seq, " _
                        & "  ars_soshipd_oid, " _
                        & "  soship_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  ars_taxable, " _
                        & "  ars_tax_class_id, " _
                        & "  code_name as taxclass_name, " _
                        & "  ars_tax_inc, " _
                        & "  ars_open, " _
                        & "  ars_invoice, " _
                        & "  ars_so_price, " _
                        & "  ars_so_disc_value, " _
                        & "  ars_invoice_price, " _
                        & "  ars_close_line, " _
                        & "  ars_dt " _
                        & "FROM  " _
                        & "  public.ars_ship " _
                        & "  inner join public.soshipd_det on public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid " _
                        & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                        & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                        & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.code_mstr on public.ars_ship.ars_tax_class_id = public.code_mstr.code_id" _
                        & "  inner join public.ar_mstr on public.ars_ship.ars_ar_oid = public.ar_mstr.ar_oid" _
                        & " where ars_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ds_edit_dist As DataSet = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.ard_dist.ard_oid, " _
                        & "  public.ard_dist.ard_ar_oid, " _
                        & "  public.ard_dist.ard_tax_distribution, " _
                        & "  public.ard_dist.ard_taxable, " _
                        & "  public.ard_dist.ard_tax_inc, " _
                        & "  public.ard_dist.ard_tax_class_id, " _
                        & "  public.ard_dist.ard_ac_id, " _
                        & "  public.ard_dist.ard_sb_id, " _
                        & "  public.ard_dist.ard_cc_id, " _
                        & "  public.ard_dist.ard_amount, " _
                        & "  public.ard_dist.ard_remarks, " _
                        & "  public.ard_dist.ard_dt, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.sb_mstr.sb_desc, " _
                        & "  public.code_mstr.code_name as taxclass_name, " _
                        & "  public.cc_mstr.cc_desc " _
                        & "FROM " _
                        & "  public.ard_dist " _
                        & "  INNER JOIN public.ar_mstr ON (public.ard_dist.ard_ar_oid = public.ar_mstr.ar_oid) " _
                        & "  INNER JOIN public.ac_mstr ON (public.ard_dist.ard_ac_id = public.ac_mstr.ac_id) " _
                        & "  left outer join public.sb_mstr ON (public.ard_dist.ard_sb_id = public.sb_mstr.sb_id) " _
                        & "  left outer join public.cc_mstr ON (public.ard_dist.ard_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.code_mstr ON (public.ard_dist.ard_tax_class_id = public.code_mstr.code_id)" _
                        & " where ard_amount = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_dist, "dist")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        Dim i As Integer

        Dim _dtrow As DataRow
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_shipment.Tables(0).NewRow
            _dtrow("ars_oid") = Guid.NewGuid.ToString
            _dtrow("ceklist") = True
            _dtrow("ars_soshipd_oid") = _so_oid 'ini perantara saja
            _dtrow("soship_code") = soship_so_oid.Text 'ini perantara saja
            _dtrow("pt_id") = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
            _dtrow("pt_code") = ds_edit.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_edit.Tables(0).Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = ds_edit.Tables(0).Rows(i).Item("pt_desc2")
            _dtrow("ars_taxable") = ds_edit.Tables(0).Rows(i).Item("sod_taxable")
            _dtrow("ars_tax_class_id") = ds_edit.Tables(0).Rows(i).Item("sod_tax_class")
            _dtrow("taxclass_name") = "" 'ini perantara saja
            _dtrow("ars_tax_inc") = ds_edit.Tables(0).Rows(i).Item("sod_tax_inc")
            _dtrow("ars_open") = ds_edit.Tables(0).Rows(i).Item("soshipd_qty")
            _dtrow("ars_invoice") = ds_edit.Tables(0).Rows(i).Item("soshipd_qty")
            _dtrow("ars_so_price") = ds_edit.Tables(0).Rows(i).Item("sod_price")
            _dtrow("ars_so_disc_value") = ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc")
            '_dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sod_price") - (ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sod_price") '- (ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _dtrow("ars_close_line") = "Y"
            ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
        Next
        ds_edit_shipment.Tables(0).AcceptChanges()

        'pindah kan ke table distribution
        Dim j As Integer

        ds_edit_dist.Tables(0).Clear()
        Dim _search As Boolean = False
        Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double
        _invoice_price = 0
        _line_tr_pph = 0
        _line_tr_ppn = 0
        _tax_rate = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                    'Mencari prodline account untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_prodline_account_ar(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                    _search = False
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        'If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) And _
                        '(ds_edit_dist.Tables(0).Rows(j).Item("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) Then
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya line ppn saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                        Else
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                            (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                             ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price"))
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                        _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                        _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                        _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya dicari ppn nya saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            _dtrow("ard_amount") = _invoice_price
                        Else
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                        End If


                        _dtrow("ard_tax_distribution") = "Y"

                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH
        Dim _ppn, _pph As Double
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                    'Mencari taxrate account ar untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                    '1. PPN
                    _search = False
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next
                    'Exit Sub
                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_sod_cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_cost") / (1 + _tax_rate)))
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = "N"
                        _dtrow("ard_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _ppn
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If

                    '1. PPH
                    _search = False
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = "N"
                        _dtrow("ard_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _pph
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                End If
            End If
        Next

        'Ini untuk ar discount
        _search = False
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                        'Mencari prodline account untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_prodline_account_ar_discount(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                        _search = False
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya line ppn saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

                                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                 _invoice_price)
                                ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                            _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                            _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                            _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya dicari ppn nya saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                _dtrow("ard_amount") = _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1
                                _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                            End If


                            _dtrow("ard_tax_distribution") = "Y"

                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH yang ar discount
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                        'Mencari taxrate account ap untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                        '1. PPN
                        _search = False
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = "N"
                            _dtrow("ard_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            _dtrow("ard_amount") = _ppn
                            _dtrow("ard_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If

                        '1. PPH
                        _search = False
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = "N"
                            _dtrow("ard_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            End If

                            _dtrow("ard_amount") = _pph
                            _dtrow("ard_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next
        '**************************************************

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        Dim _bk_ac_id As Integer
        Dim _bk_ac_name As String = ""
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bk_ac_id, ac_name from bk_mstr inner join ac_mstr on ac_id = bk_ac_id where bk_id = (select so_bk_id from so_mstr where so_oid = '" + _so_oid.ToString + "')"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _bk_ac_id = .DataReader("bk_ac_id").ToString
                        _bk_ac_name = .DataReader("ac_name").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Dim _ard_total_amount As Double = 0

        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            _ard_total_amount = _ard_total_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        Next

        If insert_glt_det_ar(par_ssqls, par_obj, ds_edit_dist, _
                                                       soship_en_id.EditValue, soship_en_id.GetColumnValue("en_code"), _
                                                       par_so_oid.ToString, par_so_code, _
                                                       soship_date.DateTime, _
                                                       ds_edit.Tables(0).Rows(0).Item("so_cu_id"), ds_edit.Tables(0).Rows(0).Item("so_exc_rate"), _
                                                       "AY", "AR-PAY", _
                                                       _bk_ac_id, 0, 0, _
                                                       _bk_ac_name, _ard_total_amount) = False Then
            'untuk so_cu_id dan so_exc_rate ambil saja dari row yang 0
            Return False
        End If

    End Function

    Private Function insert_glt_det_ar(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_exc_rate As Double, _
                                   ByVal par_type As String, ByVal par_daybook As String, _
                                   ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
                                   ByVal par_cc_id As Integer, _
                                   ByVal par_desc As String, ByVal par_amount As Double) As Boolean

        'Return False
        'Exit Function
        insert_glt_det_ar = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        'Insert Untuk Yang Debet dan Credit, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            'If par_ds.Tables(0).Rows(i).Item("ard_amount") > 0 Then
            If par_ds.Tables(0).Rows(i).Item("ard_amount") < 0 Then
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount") * -1) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount") * -1, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
                'ElseIf par_ds.Tables(0).Rows(i).Item("ard_amount") < 0 Then
            ElseIf par_ds.Tables(0).Rows(i).Item("ard_amount") > 0 Then
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount"), "D") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        Next

        _seq = _seq + 1

        With par_obj
            Try
                'Insert untuk yang credit account bank nya berkurang
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.glt_det " _
                                    & "( " _
                                    & "  glt_oid, " _
                                    & "  glt_dom_id, " _
                                    & "  glt_en_id, " _
                                    & "  glt_add_by, " _
                                    & "  glt_add_date, " _
                                    & "  glt_code, " _
                                    & "  glt_date, " _
                                    & "  glt_type, " _
                                    & "  glt_cu_id, " _
                                    & "  glt_exc_rate, " _
                                    & "  glt_seq, " _
                                    & "  glt_ac_id, " _
                                    & "  glt_sb_id, " _
                                    & "  glt_cc_id, " _
                                    & "  glt_desc, " _
                                    & "  glt_debit, " _
                                    & "  glt_credit, " _
                                    & "  glt_ref_oid, " _
                                    & "  glt_ref_trans_code, " _
                                    & "  glt_posted, " _
                                    & "  glt_dt, " _
                                    & "  glt_daybook " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(par_en_id) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(_glt_code) & ",  " _
                                    & SetDate(par_date) & ",  " _
                                    & SetSetring(par_type) & ",  " _
                                    & SetInteger(par_cu_id) & ",  " _
                                    & SetDbl(par_exc_rate) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(par_ac_id) & ",  " _
                                    & SetIntegerDB(par_sb_id) & ",  " _
                                    & SetIntegerDB(par_cc_id) & ",  " _
                                    & SetSetringDB(par_desc) & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetDblDB(par_amount) & ",  " _
                                    & SetSetring(par_oid) & ",  " _
                                    & SetSetring(par_trans_code) & ",  " _
                                    & SetSetring("N") & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(par_daybook) & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                 par_ac_id, _
                                                 par_sb_id, _
                                                 par_cc_id, _
                                                 par_en_id, par_cu_id, _
                                                 par_exc_rate, par_amount, "C") = False Then

                    Return False
                    Exit Function
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function


    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_en_id")
        _type = 12
        _table = "soship_mstr"
        _initial = "soship"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "    soship_oid, " _
            & "    soship_dom_id, " _
            & "    soship_en_id, " _
            & "    soship_add_by, " _
            & "    soship_add_date, " _
            & "    soship_upd_by, " _
            & "    soship_upd_date, " _
            & "    soship_code, " _
            & "    soship_date, " _
            & "    soship_so_oid, " _
            & "    soship_si_id, " _
            & "    soship_is_shipment, " _
            & "    soship_dt, " _
            & "    soshipd_qty, " _
            & "    soshipd_um, " _
            & "    soshipd_um_conv, " _
            & "    soshipd_cancel_bo, " _
            & "    soshipd_qty_real, " _
            & "    soshipd_si_id, " _
            & "    soshipd_loc_id, " _
            & "    soshipd_lot_serial, " _
            & "    soshipd_rea_code_id, " _
            & "    soshipd_dt, " _
            & "    soshipd_qty_inv, " _
            & "    soshipd_close_line, " _
            & "    so_code,  " _
            & "    so_date, " _
            & "    ptnr_name, " _
            & "    ptnra_line_1, " _
            & "    ptnra_line_2, " _
            & "    ptnra_line_3, " _
            & "    credit_term_mstr.code_name as credit_term_name, " _
            & "    cu_name, " _
            & "    pt_code, " _
            & "    pt_desc1, " _
            & "    pt_desc2, " _
            & "    um_master.code_name as um_name, " _
            & "    cmaddr_name, " _
            & "    cmaddr_line_1, " _
            & "    cmaddr_line_2, " _
            & "    cmaddr_line_3, " _
            & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "  FROM  " _
            & "    soship_mstr " _
            & "    inner join soshipd_det on soshipd_soship_oid = soship_oid " _
            & "    inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "    inner join so_mstr on so_oid = sod_so_oid " _
            & "    inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
            & "    left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "    inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
            & "    inner join cu_mstr on cu_id = so_cu_id " _
            & "    inner join pt_mstr on pt_id = sod_pt_id  " _
            & "    inner join code_mstr um_master on um_master.code_id = soshipd_um " _
            & "    inner join cmaddr_mstr on cmaddr_en_id = soship_en_id " _
            & "    inner join code_mstr ptnr_type on ptnr_type.code_id = ptnra_addr_type " _
            & "    left outer join tranaprvd_dok on tranaprvd_tran_oid = soship_oid " _
            & "    where ptnr_type.code_name ~~* 'bill to' " _
            & "    and soship_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code") + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRSalesOrderReturPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
        frm.ShowDialog()

    End Sub

    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try

            ssql = "SELECT  " _
                & "    soship_oid, " _
                & "    soship_dom_id, " _
                & "    soship_en_id, " _
                & "    soship_add_by, " _
                & "    soship_add_date, " _
                & "    soship_upd_by, " _
                & "    soship_upd_date, " _
                & "    soship_code, " _
                & "    soship_date, " _
                & "    soship_so_oid, " _
                & "    soship_si_id, " _
                & "    soship_is_shipment, " _
                & "    soship_dt, " _
                & "    soshipd_qty, " _
                & "    soshipd_um, " _
                & "    soshipd_um_conv, " _
                & "    soshipd_cancel_bo, " _
                & "    soshipd_qty_real, " _
                & "    soshipd_si_id, " _
                & "    soshipd_loc_id, " _
                & "    soshipd_lot_serial, " _
                & "    soshipd_rea_code_id, " _
                & "    soshipd_dt, " _
                & "    soshipd_qty_inv, " _
                & "    soshipd_close_line, " _
                & "    so_code,  " _
                & "    so_date, " _
                & "    ptnr_name, " _
                & "    ptnra_line_1, " _
                & "    ptnra_line_2, " _
                & "    ptnra_line_3, " _
                & "    credit_term_mstr.code_name as credit_term_name, " _
                & "    cu_name, " _
                & "    pt_code, " _
                & "    pt_desc1, " _
                & "    pt_desc2, " _
                & "    um_master.code_name as um_name, " _
                & "    cmaddr_name, " _
                & "    cmaddr_line_1, " _
                & "    cmaddr_line_2, " _
                & "    cmaddr_line_3, " _
                & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "  FROM  " _
                & "    soship_mstr " _
                & "    inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                & "    inner join sod_det on sod_oid = soshipd_sod_oid " _
                & "    inner join so_mstr on so_oid = sod_so_oid " _
                & "    inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                & "    left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                & "    inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
                & "    inner join cu_mstr on cu_id = so_cu_id " _
                & "    inner join pt_mstr on pt_id = sod_pt_id  " _
                & "    inner join code_mstr um_master on um_master.code_id = soshipd_um " _
                & "    inner join cmaddr_mstr on cmaddr_en_id = soship_en_id " _
                & "    inner join code_mstr ptnr_type on ptnr_type.code_id = ptnra_addr_type " _
                & "    left outer join tranaprvd_dok on tranaprvd_tran_oid = soship_oid " _
                & " where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and soship_is_shipment ~~* 'N'" _
                & " and soship_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            gv_edit.UpdateCurrentRow()
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "rea_code_name" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("rea_code_name") = gv_edit.GetRowCellValue(_row, "rea_code_name")
                    ds_edit.Tables(0).Rows(i).Item("soshipd_rea_code_id") = gv_edit.GetRowCellValue(_row, "soshipd_rea_code_id")
                Next
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
