Imports npgsql
Imports master_new.ModFunction

Public Class FSalesOrderShipment
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _so_oid As String
    Public ds_edit, ds_serial As DataSet
    Public _is_consignment As Boolean
    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_master, "Remarks", "soship_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "soship_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "soship_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "soship_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "soship_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "soshipd_soship_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Shipment", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice", "soshipd_qty_inv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "soshipd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Serial Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)

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
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "soshipd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "soshipd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_edit, "soshipd_loc_id", False)
        add_column(gv_edit, "sod_invc_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "_qty_allocated_awal", False)
        add_column(gv_edit, "Qty Allocated", "soshipd_qty_allocated", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Shipment", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "soshipd_um", False)
        add_column(gv_edit, "UM", "soshipd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Supplier Lot Number", "soshipd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_cost", False)
        add_column(gv_edit, "so_cu_id", False)
        add_column(gv_edit, "so_exc_rate", False)
        'add_column(gv_edit, "soshipd_rea_code_id", False)
        'add_column(gv_edit, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_serial, "soshipds_oid", False)
        add_column(gv_serial, "soshipds_soshipd_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "soshipds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "soshipds_si_id", False)
        add_column(gv_serial, "soshipds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_code", False)
        add_column(gv_serial, "pt_type", False)
        add_column(gv_serial, "soshipd_sod_oid", False)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  soship_oid, " _
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
                    & "  cu_mstr.cu_name,soship_remarks, " _
                    & "  soship_exc_rate, " _
                    & "  ptnr_name as ptnr_name_sold, " _
                    & "  en_desc, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.soship_mstr " _
                    & "  inner join so_mstr on so_oid = soship_so_oid " _
                    & "  inner join en_mstr on en_id = soship_en_id " _
                    & "  inner join si_mstr on si_id = soship_si_id " _
                    & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold" _
                    & "  left outer join public.cu_mstr ON (public.soship_mstr.soship_cu_id = public.cu_mstr.cu_id)" _
                    & " where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and soship_is_shipment ~~* 'Y'" _
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
            & "  soshipd_seq, " _
            & "  soshipd_qty * -1 as soshipd_qty, " _
            & "  soshipd_um, " _
            & "  soshipd_um_conv, " _
            & "  soshipd_cancel_bo, " _
            & "  soshipd_qty_real * -1 as soshipd_qty_real, " _
            & "  soshipd_qty_inv, " _
            & "  soshipd_qty_allocated, " _
            & "  soshipd_si_id,soshipd_en_id, " _
            & "  soshipd_loc_id, " _
            & "  soshipd_lot_serial, " _
            & "  soshipd_rea_code_id, " _
            & "  soshipd_dt, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_ls, " _
            & "  si_desc, " _
            & "  loc_desc, " _
            & "  um_mstr.code_name as soshipd_um_name,soshipd_en_id,en_desc " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id " _
            & "  inner join si_mstr on si_id = soshipd_si_id " _
            & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
            & "  inner join code_mstr um_mstr on um_mstr.code_id = soshipd_um" _
            & "  left outer join en_mstr on soshipd_en_id = en_id " _
            & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and soship_is_shipment ~~* 'Y'"

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  soshipds_oid, " _
            & "  soshipds_soshipd_oid, " _
            & "  soshipds_seq, " _
            & "  soshipds_qty * -1 as soshipds_qty,  " _
            & "  soshipds_qty_real * -1 soshipds_qty_real, " _
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
            & "  and soship_is_shipment ~~* 'Y'"

        load_data_detail(sql, gc_detail_serial, "detail_serial")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("soshipd_soship_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soshipd_soship_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString & "'")
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
        soship_remarks.EditValue = ""

        soship_ce_lbl.EditValue = False

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        _is_consignment = False
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
                        & "  soshipd_qty_allocated as _qty_allocated_awal, " _
                        & "  soshipd_qty_allocated, " _
                        & "  soshipd_um, " _
                        & "  soshipd_um_conv, " _
                        & "  soshipd_cancel_bo, " _
                        & "  soshipd_qty_real, " _
                        & "  soshipd_si_id,soshipd_en_id,en_desc, " _
                        & "  soshipd_loc_id, " _
                        & "  soshipd_lot_serial, " _
                        & "  soshipd_rea_code_id, " _
                        & "  soshipd_dt, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_ls, " _
                        & "  pt_type, " _
                        & "  so_cu_id, " _
                        & "  so_exc_rate, " _
                        & "  sod_cost, " _
                        & "  si_desc, " _
                        & "  loc_desc, sod_invc_loc_id, " _
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
                        & "  left outer join en_mstr on soshipd_en_id = en_id " _
                        & "  where soshipd_oid is null"
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
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")) = True Then
                    MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next

        '*********************
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
        '        If SetNumber(ds_edit.Tables(0).Rows(i).Item("qty_open")) < SetNumber(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) = True Then
        '            MessageBox.Show("Qty Shipment higher than open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            BindingContext(ds_edit.Tables(0)).Position = i
        '            Return False
        '        End If
        '    End If
        'Next

        ''*********************

        ''Cek Reason Code
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
        '        If IsDBNull(ds_edit.Tables(0).Rows(i).Item("soshipd_rea_code_id")) = True Then
        '            MessageBox.Show("Reason Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            BindingContext(ds_edit.Tables(0)).Position = i
        '            Return False
        '        End If
        '    End If
        'Next
        ''*********************

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

        '*********************
        'Cek Location apakah sama antara locasi ambil barang dengan lokasi yang telah dialokasikan
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_allocated") > 0 Then
                If ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id") <> ds_edit.Tables(0).Rows(i).Item("sod_invc_loc_id") Then
                    MessageBox.Show("Shipment Location Different With Allocation Location...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '*********************

        'cek ketersediaan qty_open untuk parsial

        Dim _qty_selisih, _qty_open As Integer

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            _qty_selisih = SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) - SetDblDB(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_allocated"))

            If _qty_selisih > 0 Then
                _qty_open = -1
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .Connection.Open()
                            .Command = .Connection.CreateCommand
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "SELECT  " _
                                                 & "  coalesce(public.invc_mstr.invc_qty,0) - coalesce(public.invc_mstr.invc_qty_alloc,0) as invc_qty_open " _
                                                 & "FROM  " _
                                                 & "  public.invc_mstr " _
                                                 & " where invc_oid = (select sod_invc_oid from sod_det where sod_oid = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString) + ")"
                            .InitializeCommand()
                            .DataReader = .Command.ExecuteReader
                            While .DataReader.Read
                                _qty_open = .DataReader.Item("invc_qty_open")
                            End While
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try

                If _qty_open > 0 Then
                    If _qty_selisih > _qty_open Then
                        MsgBox(ds_edit.Tables(0).Rows(i).Item("pt_code") + " Qty Which Allocated Is More Than Availability on Allocated Location.....", MsgBoxStyle.Critical, "Unable to Save..")
                        before_save = False
                    End If
                End If
                
            End If
        Next

        'apabila so nya konsinyasi dan apabila di setingannya di set error, maka
        'ketika terjadi shipment so konsinyasi harus ambil dari gudang yang typenya konsinyasi jg, kalau tidak maka akan terjadi error
        If _is_consignment = True Then
            Dim _conf_value, _type As String

            _conf_value = func_coll.get_conf_file("err_shipment_consigment")
            _type = ""

            If _conf_value = "1" Then
                For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .Connection.Open()
                                .Command = .Connection.CreateCommand
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "select coalesce(code_usr_1,'N') as code_usr_1 from loc_mstr inner join code_mstr on code_id = loc_type " _
                                                     & " where loc_id = " + ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id").ToString
                                .InitializeCommand()
                                .DataReader = .Command.ExecuteReader
                                While .DataReader.Read
                                    _type = .DataReader.Item("code_usr_1")
                                End While
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try

                    If _type.ToUpper <> "Y" Then
                        MessageBox.Show("Can't Shipment From Location That Are Not Consignment Location...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                Next
            End If
        End If

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
            Try
                _soshipd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "soshipd_um_conv"))
            Catch ex As Exception
            End Try

            _soshipd_qty_real = e.Value * _soshipd_um_conv
            gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _soshipd_qty_real)

            If e.Value > gv_edit.GetRowCellValue(e.RowHandle, "qty_open") Then
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty", gv_edit.GetRowCellValue(e.RowHandle, "qty_open"))
            End If

            'sesuaikan qty yg dikirim dengan yg dialokasikan
            If e.Value <= gv_edit.GetRowCellValue(e.RowHandle, "_qty_allocated_awal") Then
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_allocated", e.Value)
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_allocated", gv_edit.GetRowCellValue(e.RowHandle, "_qty_allocated_awal"))
            End If

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
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = True Then
                If SetString(gv_edit.GetRowCellValue(_row, "soshipd_en_id")) = "" Then
                    frm._en_id = soship_en_id.EditValue
                Else
                    frm._en_id = gv_edit.GetRowCellValue(_row, "soshipd_en_id")
                End If
            Else
                frm._en_id = soship_en_id.EditValue
            End If

            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "en_desc" Then
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                Exit Sub
            End If
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            'frm._en_id = soship_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            gv_serial.Columns("soshipds_soshipd_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soshipds_soshipd_oid='" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("soshipd_oid").ToString & "'")
            gv_serial.BestFitColumns()
        Catch ex As Exception
        End Try

        If gv_edit.GetRowCellValue(e.FocusedRowHandle, "pt_ls") = "N" Then
            gc_serial.EmbeddedNavigator.Buttons.Append.Visible = False
            gc_serial.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_serial.EmbeddedNavigator.Buttons.Append.Visible = True
            gc_serial.EmbeddedNavigator.Buttons.Remove.Visible = False
        End If

        'Try
        '    gv_serial.Columns("soshipds_soshipd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("soshipd_oid"))
        '    gv_serial.BestFitColumns()
        'Catch ex As Exception
        'End Try
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
            .SetRowCellValue(e.RowHandle, "soshipds_soshipd_oid", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_oid").ToString)
            .SetRowCellValue(e.RowHandle, "soshipd_sod_oid", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_sod_oid").ToString)
            .SetRowCellValue(e.RowHandle, "soshipds_qty", 1)
            .SetRowCellValue(e.RowHandle, "soshipds_si_id", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_si_id"))
            .SetRowCellValue(e.RowHandle, "soshipds_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("soshipd_loc_id"))
            .SetRowCellValue(e.RowHandle, "pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_id"))
            .SetRowCellValue(e.RowHandle, "pt_code", ds_edit.Tables(0).Rows(_row_edit).Item("pt_code"))
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _soship_oid As Guid
        _soship_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _soship_code, _serial, _pt_code As String
        'Dim _cost_methode As String
        Dim _en_id, _en_id_detail, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim _en_desc_detail As String = ""
        Dim ds_bantu_ry As New DataSet
        'Dim _soshipd_qty_real As Double
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        _soship_code = func_coll.get_transaction_number("SS", soship_en_id.GetColumnValue("en_code"), "soship_mstr", "soship_code")

        _tran_id = func_coll.get_id_tran_mstr("iss-so")
        If _tran_id = -1 Then
            MessageBox.Show("Sales Order Shipment In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                                            & "  soship_is_shipment,soship_remarks, " _
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
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(soship_remarks.EditValue) & ",  " _
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
                                                    & "  soshipd_qty_allocated,soshipd_en_id, " _
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
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty") * -1) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_um_conv")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") * -1) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_allocated")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_en_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("soshipd_lot_serial")) & ",  " _
                                                    & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("soshipd_rea_code_id")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update sod_det
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sod_det set sod_qty_shipment = coalesce(sod_qty_shipment,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty").ToString) _
                                                    & " , sod_sales_unit_total = coalesce(sod_sales_unit,0) * " + SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty").ToString) & " " _
                                                    & " , sod_commision_total = coalesce(sod_commision,0) * " + SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty").ToString) & " " _
                                                     & " where sod_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update invc_mstr set invc_qty_alloc = coalesce(invc_qty_alloc,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_allocated")) _
                                                     & " where invc_oid = (select sod_invc_oid from sod_det where sod_oid in (" + SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString) + "))"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            End If
                        Next

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
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("soshipds_qty")) * -1 & ",  " _
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
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
                                    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                        i_2 += 1

                                        _en_desc_detail = soship_en_id.Text
                                        _en_id = soship_en_id.EditValue
                                        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = True Then
                                            If SetString(ds_edit.Tables(0).Rows(i).Item("soshipd_en_id")) <> "" Then
                                                _en_id = ds_edit.Tables(0).Rows(i).Item("soshipd_en_id")
                                                'master_new.PGSqlConn.GetRowInfo("select en_desc from en_mstr where en_id=" & _en_id)(0).ToString
                                            End If
                                        End If
                                        _si_id = ds_edit.Tables(0).Rows(i).Item("soshipd_si_id")
                                        _loc_id = ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")
                                        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                        _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                        _serial = "''"
                                        _qty = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")
                                        If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'Update History Inventory                        
                                        _qty = _qty * -1
                                        _cost = func_coll.get_cost_sod_det(ds_edit.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString)
                                        _cost = _cost * soship_exc_rate.EditValue

                                        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "SO Shipment (" & _en_desc_detail & ")", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", soship_date.DateTime) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
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
                                    _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                                    _serial = ds_serial.Tables(0).Rows(i).Item("soshipds_lot_serial")
                                    _qty = ds_serial.Tables(0).Rows(i).Item("soshipds_qty")
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory
                                    _cost = func_coll.get_cost_sod_det(ds_serial.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString)
                                    _qty = _qty * -1
                                    _cost = _cost * soship_exc_rate.EditValue

                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "SO Shipment", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", soship_date.DateTime) = False Then
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
                        '            'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '            '    sqlTran.Rollback()
                        '            '    insert = False
                        '            '    Exit Function
                        '            'End If
                        '        ElseIf _cost_methode = "A" Then
                        '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '                sqlTran.Rollback()
                        '                insert = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next

                        'sqlTran.Rollback()

                        'Untuk update status so apabile semua line sudah terpenuhi qtynya...
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update so_mstr set so_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & ", so_trans_id = 'C' " + _
                                               " where coalesce((select count(sod_so_oid) as jml From sod_det " + _
                                               " where sod_qty <> coalesce(sod_qty_shipment,0) " + _
                                               " and sod_so_oid = '" + _so_oid.ToString + "'" + _
                                               " group by sod_so_oid),0) = 0 " + _
                                               " and so_oid = '" + _so_oid.ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        ' TIdak jadi karena sudah ada di menu khusus untuk penghitungan royalti
                        ' ''Update Ke Table Royalti 'soshipd_qty
                        ''For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        ''    _soshipd_qty_real = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")

                        ''    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
                        ''        Try
                        ''            Using objcb As New master_new.WDABasepgsql("", "")
                        ''                With objcb
                        ''                    .SQL = "select royt_oid, royt_pt_id, royt_seq, royt_qty_royalti - royt_qty_so as royt_qty_open " + _
                        ''                       " from royt_table " + _
                        ''                       " where royt_qty_royalti > royt_qty_so " + _
                        ''                       " and royt_pt_id  = " + ds_edit.Tables(0).Rows(i).Item("pt_id").ToString + _
                        ''                       " order by royt_seq "
                        ''                    .InitializeCommand()
                        ''                    .FillDataSet(ds_bantu_ry, "royalti")
                        ''                End With
                        ''            End Using
                        ''        Catch ex As Exception
                        ''            MessageBox.Show(ex.Message)
                        ''        End Try

                        ''        For j = 0 To ds_bantu_ry.Tables(0).Rows.Count - 1
                        ''            If _soshipd_qty_real > ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open") Then
                        ''                .Command.CommandType = CommandType.Text
                        ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open").ToString _
                        ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                        ''                ssqls.Add(.Command.CommandText)
                        ''                .Command.ExecuteNonQuery()
                        ''                .Command.Parameters.Clear()

                        ''                _soshipd_qty_real = _soshipd_qty_real - ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open")
                        ''            Else
                        ''                .Command.CommandType = CommandType.Text
                        ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + _soshipd_qty_real.ToString _
                        ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
                        ''                ssqls.Add(.Command.CommandText)
                        ''                .Command.ExecuteNonQuery()
                        ''                .Command.Parameters.Clear()
                        ''                Exit For 'karena nilai _shosipd_qty_real sudah habis...
                        ''            End If
                        ''        Next
                        ''    End If
                        ''Next
                        ' ''**********************************************************

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If glt_det_so_shipment(ssqls, objinsert, ds_edit, _soship_oid.ToString, _soship_code) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, soship_en_id.EditValue, 11, _soship_oid.ToString, _soship_code, _now.Date) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SO Shipment " & _soship_code)
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

    Private Function glt_det_so_shipment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, ByVal par_soship_oid As String, ByVal par_soship_code As String) As Boolean
        glt_det_so_shipment = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _date As Date = soship_date.DateTime
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number("IC", soship_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id"))
                        _cost = func_coll.get_cost_sod_det(par_ds.Tables(0).Rows(i).Item("soshipd_sod_oid").ToString) * par_ds.Tables(0).Rows(i).Item("soshipd_qty")

                        dt_bantu = New DataTable
                        dt_bantu = (func_coll.get_prodline_account(_pl_id, "SL_CMACC"))

                        'Insert Untuk Yang Debet

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
                                            & SetDbl(soship_exc_rate.EditValue) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("SO Shipment") & ",  " _
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
                                                         soship_exc_rate.EditValue, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If


                        dt_bantu = New DataTable
                        dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))

                        'Insert Untuk Yang Debet Dulu
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
                                            & SetDbl(soship_exc_rate.EditValue) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring("SO Shipment") & ",  " _
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
                                                         soship_exc_rate.EditValue, _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If
                        '********************** finish untuk yang credit
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_en_id")
        _type = 11
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
            & "    soshipd_qty * -1 as soshipd_qty, " _
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
            & "    cmaddr_line_3, so_delivery_receipt_number, so_delivery_courier, " _
            & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 ,so_ref_po_code " _
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
            & "    and soship_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString + "' "

        If soship_ce_lbl.Checked = True Then
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            'frm._report = "XRPackingSheetPrint"
            frm._report = "XRSalesOrderShipmentLabelPrint"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
            frm.ShowDialog()
        Else
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRSalesOrderShipmentPrint"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
            frm.ShowDialog()
        End If

    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            gv_edit.UpdateCurrentRow()
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "si_desc" Then

                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("si_desc") = gv_edit.GetRowCellValue(_row, "si_desc")
                    ds_edit.Tables(0).Rows(i).Item("soshipd_si_id") = gv_edit.GetRowCellValue(_row, "soshipd_si_id")
                Next

            ElseIf _col = "loc_desc" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
                    ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id") = gv_edit.GetRowCellValue(_row, "soshipd_loc_id")
                Next


            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                & "  soship_oid, " _
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
                & "  soship_is_shipment,soship_remarks, " _
                & "  soship_dt, " _
                & "  so_code, " _
                & "  ptnr_name as ptnr_name_sold, " _
                & "  en_desc, " _
                & "  si_desc, " _
                & "  soshipd_oid, " _
                & "  soshipd_soship_oid, " _
                & "  soshipd_sod_oid, " _
                & "  soshipd_seq, " _
                & "  soshipd_qty * -1 as soshipd_qty, " _
                & "  soshipd_um, " _
                & "  soshipd_um_conv, " _
                & "  soshipd_cancel_bo, " _
                & "  soshipd_qty_real * -1 as soshipd_qty_real, " _
                & "  soshipd_qty_inv, " _
                & "  soshipd_qty_allocated, " _
                & "  soshipd_si_id, " _
                & "  soshipd_loc_id, " _
                & "  soshipd_lot_serial, " _
                & "  soshipd_rea_code_id, " _
                & "  soshipd_dt, " _
                & "  pt_id, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  pt_ls, " _
                & "  si_desc, " _
                & "  loc_desc, " _
                & "  um_mstr.code_name as soshipd_um_name " _
                & "FROM  " _
                & "  public.soship_mstr " _
                & "  inner join so_mstr on so_oid = soship_so_oid " _
                & "  inner join en_mstr on en_id = soship_en_id " _
                & "  inner join si_mstr on si_id = soship_si_id " _
                & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold" _
                & "  inner join soshipd_det on soship_oid = soshipd_soship_oid" _
                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                & "  inner join code_mstr um_mstr on um_mstr.code_id = soshipd_um" _
                & " where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and soship_is_shipment ~~* 'Y'" _
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

End Class
