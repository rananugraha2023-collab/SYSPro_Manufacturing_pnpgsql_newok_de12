Imports npgsql
Imports master_new.ModFunction

Public Class FPurchaseReturn
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _po_oid As String
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime
    Dim _conf_budget As String

    Private Sub FPurchaseReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        rcv_en_id.Properties.DataSource = dt_bantu
        rcv_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        rcv_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        rcv_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        rcv_cu_id.Properties.DataSource = dt_bantu
        rcv_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        rcv_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        rcv_cu_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Return Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "rcv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Effective Date", "rcv_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "rcv_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "rcv_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "rcv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "rcv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "rcv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "rcv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "rcvd_rcv_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Return", "rcvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice", "rcvd_qty_inv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "rcvd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "rcvd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "rcvd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Packing", "rcvd_packing_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Supplier Lot Number", "rcvd_supp_lot", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Serial Number", "rcvds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Serial", "rcvds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Is Close Line", "rcvd_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_serial, "rcvd_rcv_oid", False)
        add_column_copy(gv_detail_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "rcvds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "rcvds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "rcvd_oid", False)
        add_column(gv_edit, "rcvd_pod_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "", False)
        add_column(gv_edit, "rcvd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "rcvd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Return", "rcvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "rcvd_um", False)
        add_column(gv_edit, "UM", "rcvd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "UM Conversion", "rcvd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "rcvd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Packing", "rcvd_packing_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Supplier Lot Number", "rcvd_supp_lot", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "rcvd_rea_code_id", False)
        add_column(gv_edit, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Is Close Line", "rcvd_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_serial, "rcvds_oid", False)
        add_column(gv_serial, "rcvds_rcvd_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "rcvds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "rcvds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "rcvds_um", False)
        add_column(gv_serial, "rcvds_um_conv", True)
        add_column(gv_serial, "rcvds_si_id", False)
        add_column(gv_serial, "rcvds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_code", False)
        add_column(gv_serial, "pt_type", False)
        add_column(gv_serial, "rcvd_pod_oid", False)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.rcv_mstr.rcv_oid, " _
                    & "  public.rcv_mstr.rcv_dom_id, " _
                    & "  public.rcv_mstr.rcv_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.rcv_mstr.rcv_add_by, " _
                    & "  public.rcv_mstr.rcv_add_date, " _
                    & "  public.rcv_mstr.rcv_upd_by, " _
                    & "  public.rcv_mstr.rcv_upd_date, " _
                    & "  public.rcv_mstr.rcv_code, " _
                    & "  public.rcv_mstr.rcv_date, " _
                    & "  public.rcv_mstr.rcv_eff_date,rcv_remarks, " _
                    & "  public.rcv_mstr.rcv_po_oid, " _
                    & "  public.rcv_mstr.rcv_packing_slip, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  public.rcv_mstr.rcv_exc_rate, " _
                    & "  public.rcv_mstr.rcv_ret_replace, " _
                    & "  public.rcv_mstr.rcv_dt, " _
                    & "  public.po_mstr.po_code, " _
                    & "  public.ptnr_mstr.ptnr_name " _
                    & "FROM " _
                    & "  public.rcv_mstr " _
                    & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                    & "  INNER JOIN public.en_mstr ON (public.rcv_mstr.rcv_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.cu_mstr ON (public.rcv_mstr.rcv_cu_id = public.cu_mstr.cu_id)" _
                    & " where rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and rcv_is_receive ~~* 'N'" _
                    & " and rcv_en_id in (select user_en_id from tconfuserentity " _
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
            & "  public.rcvd_det.rcvd_oid, " _
            & "  public.rcvd_det.rcvd_rcv_oid, " _
            & "  public.rcvd_det.rcvd_pod_oid, " _
            & "  public.pt_mstr.pt_id, " _
            & "  public.pt_mstr.pt_code, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.pt_mstr.pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  public.rcvd_det.rcvd_qty * -1 as rcvd_qty, " _
            & "  public.rcvd_det.rcvd_qty_inv, " _
            & "  public.rcvd_det.rcvd_um, " _
            & "  um_mstr.code_name as rcvd_um_name, " _
            & "  public.rcvd_det.rcvd_packing_qty * -1 as rcvd_packing_qty , " _
            & "  public.rcvd_det.rcvd_um_conv, " _
            & "  public.rcvd_det.rcvd_qty_real * -1 as rcvd_qty_real, " _
            & "  public.rcvd_det.rcvd_si_id, " _
            & "  public.rcvd_det.rcvd_loc_id, " _
            & "  public.rcvd_det.rcvd_lot_serial, " _
            & "  public.rcvd_det.rcvd_supp_lot, " _
            & "  public.rcvd_det.rcvd_dt, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.loc_mstr.loc_desc, " _
            & "  public.rcvd_det.rcvd_rea_code_id, " _
            & "  public.rcvd_det.rcvd_close_line, " _
            & "  rea_code_mstr.code_name as rea_code_name " _
            & "FROM " _
            & "  public.rcvd_det " _
            & "  INNER JOIN public.rcv_mstr ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
            & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr um_mstr ON (public.rcvd_det.rcvd_um = um_mstr.code_id) " _
            & "  INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
            & "  INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.code_mstr rea_code_mstr ON (public.rcvd_det.rcvd_rea_code_id = rea_code_mstr.code_id) " _
            & "  where rcv_mstr.rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and rcv_mstr.rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and rcv_mstr.rcv_is_receive ~~* 'N'"

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  rcvds_oid, " _
            & "  rcvds_rcvd_oid, " _
            & "  rcvd_det.rcvd_rcv_oid, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  rcvds_qty * -1 as rcvds_qty , " _
            & "  rcvds_um, " _
            & "  code_name, " _
            & "  rcvds_si_id, " _
            & "  si_desc, " _
            & "  rcvds_loc_id, " _
            & "  loc_desc, " _
            & "  rcvds_lot_serial, " _
            & "  rcvds_dt " _
            & "FROM  " _
            & "  public.rcvds_serial " _
            & "  inner join rcvd_det on rcvd_oid = rcvds_rcvd_oid " _
            & "  inner join rcv_mstr on rcv_oid = rcvd_rcv_oid " _
            & "  inner join pod_det on pod_oid = rcvd_pod_oid " _
            & "  inner join pt_mstr on pt_id = pod_pt_id " _
            & "  inner join si_mstr on si_id = rcvds_si_id " _
            & "  inner join loc_mstr on loc_id = rcvds_loc_id " _
            & "  inner join code_mstr on code_id = rcvds_um" _
            & "  where rcv_mstr.rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and rcv_mstr.rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and rcv_mstr.rcv_is_receive ~~* 'N'"

        load_data_detail(sql, gc_detail_serial, "detail_serial")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("rcvd_rcv_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("rcvd_rcv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_serial.Columns("rcvd_rcv_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("rcvd_rcv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()

            'gv_detail.Columns("rcvd_rcv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_oid"))
            'gv_detail.BestFitColumns()

            'gv_detail_serial.Columns("rcvd_rcv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_oid"))
            'gv_detail_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        rcv_en_id.Focus()
        rcv_en_id.ItemIndex = 0
        rcv_eff_date.DateTime = _now
        rcv_po_oid.Text = ""
        rcv_cu_id.Enabled = False
        rcv_exc_rate.Enabled = False
        rcv_cu_id.ItemIndex = 0
        rcv_remarks.Text = ""

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
                        & "  public.rcvd_det.rcvd_oid, " _
                        & "  public.rcvd_det.rcvd_rcv_oid, " _
                        & "  public.rcvd_det.rcvd_pod_oid, " _
                        & "  public.pt_mstr.pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  pod_pt_desc1, " _
                        & "  pod_pt_desc2, " _
                        & "  public.pt_mstr.pt_ls, " _
                        & "  public.pt_mstr.pt_type, " _
                        & "  public.rcvd_det.rcvd_qty, 0.0 as qty_open, " _
                        & "  public.rcvd_det.rcvd_um, " _
                        & "  um_mstr.code_name as rcvd_um_name, " _
                        & "  public.rcvd_det.rcvd_packing_qty, " _
                        & "  public.rcvd_det.rcvd_um_conv, " _
                        & "  public.rcvd_det.rcvd_qty_real, " _
                        & "  public.rcvd_det.rcvd_si_id, " _
                        & "  public.rcvd_det.rcvd_loc_id, " _
                        & "  public.rcvd_det.rcvd_lot_serial, " _
                        & "  public.rcvd_det.rcvd_supp_lot, " _
                        & "  public.rcvd_det.rcvd_dt, " _
                        & "  public.rcvds_serial.rcvds_qty, " _
                        & "  public.rcvds_serial.rcvds_lot_serial, " _
                        & "  public.si_mstr.si_desc, " _
                        & "  public.loc_mstr.loc_desc, " _
                        & "  public.rcvd_det.rcvd_rea_code_id, " _
                        & "  public.rcvd_det.rcvd_close_line, " _
                        & "  pod_memo, " _
                        & "  rea_code_mstr.code_name as rea_code_name, " _
                        & "  '' as po_date, pod_cost, pod_cost, pod_disc, pod_cc_id, pod_po_oid " _
                        & " FROM " _
                        & "  public.rcvd_det " _
                        & "  INNER JOIN public.rcv_mstr ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
                        & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr um_mstr ON (public.rcvd_det.rcvd_um = um_mstr.code_id) " _
                        & "  INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
                        & "  INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.rcvds_serial ON (public.rcvd_det.rcvd_oid = public.rcvds_serial.rcvds_rcvd_oid)" _
                        & "  INNER JOIN public.code_mstr rea_code_mstr ON (public.rcvd_det.rcvd_rea_code_id = rea_code_mstr.code_id) " _
                        & "  where rcvd_det.rcvd_supp_lot >= 'asdfad'"
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
                        & "  rcvds_oid, " _
                        & "  rcvds_rcvd_oid, " _
                        & "  '' as rcvd_pod_oid, " _
                        & "  rcvds_qty, " _
                        & "  0 as rcvds_cost, " _
                        & "  1 as rcvds_um_conv, " _
                        & "  rcvds_um, " _
                        & "  rcvds_si_id, " _
                        & "  rcvds_loc_id, " _
                        & "  rcvds_lot_serial, " _
                        & "  rcvds_dt, -1 as pt_id, '' as pt_code, '' as pt_type " _
                        & "FROM  " _
                        & "  public.rcvds_serial " _
                        & " where rcvds_si_id = -99"
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

        Dim _date As Date = rcv_eff_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(rcv_en_id.EditValue, "gcald_ic", _date)

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
            If ds_edit.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rcvd_loc_id")) = True Then
                    MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '*********************

        '*********************
        'Cek Reason Code
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rcvd_rea_code_id")) = True Then
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
                    If ds_edit.Tables(0).Rows(i).Item("rcvd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("rcvds_rcvd_oid").ToString Then
                        If ds_serial.Tables(0).Rows(j).Item("rcvds_qty") > 1 Then
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
                _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("rcvd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("rcvds_rcvd_oid").ToString Then
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
                _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("rcvd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("rcvds_rcvd_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("rcvds_qty")
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
        Return before_save
    End Function

    Private Sub rcv_po_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcv_po_oid.ButtonClick
        Dim frm As New FPurchaseOrderSearch
        frm.set_win(Me)
        frm._en_id = rcv_en_id.EditValue
        frm._date = rcv_eff_date.DateTime
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _rcvd_qty, _rcvd_qty_real, _rcvd_um_conv As Double
        _rcvd_um_conv = 1
        _rcvd_qty = 1

        If e.Column.Name = "rcvd_qty" Then

            If SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "qty_open")) < SetNumber(e.Value) Then
                MessageBox.Show("Qty Invalid ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If

            Try
                _rcvd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "rcvd_um_conv"))
            Catch ex As Exception
            End Try

            _rcvd_qty_real = e.Value * _rcvd_um_conv
            gv_edit.SetRowCellValue(e.RowHandle, "rcvd_qty_real", _rcvd_qty_real)
        ElseIf e.Column.Name = "rcvd_um_conv" Then
            Try
                _rcvd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "rcvd_qty")))
            Catch ex As Exception
            End Try

            _rcvd_qty_real = e.Value * _rcvd_qty
            gv_edit.SetRowCellValue(e.RowHandle, "rcvd_qty_real", _rcvd_qty_real)
        End If
    End Sub

    Public Overrides Function insert() As Boolean
        _conf_budget = func_coll.get_conf_file("budget_base")

        Dim _rcv_oid As Guid
        _rcv_oid = Guid.NewGuid

        Dim _rcv_code, _serial, _pt_code As String
        'Dim _cost_methode As String
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        Dim _open_po As Boolean = False

        _rcv_code = func_coll.get_transaction_number("RT", rcv_en_id.GetColumnValue("en_code"), "rcv_mstr", "rcv_code")

        _tran_id = func_coll.get_id_tran_mstr("iss-prv")
        If _tran_id = -1 Then
            MessageBox.Show("Return PO In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                                            & "  public.rcv_mstr " _
                                            & "( " _
                                            & "  rcv_oid, " _
                                            & "  rcv_dom_id, " _
                                            & "  rcv_en_id, " _
                                            & "  rcv_add_by, " _
                                            & "  rcv_add_date, " _
                                            & "  rcv_code, " _
                                            & "  rcv_date, " _
                                            & "  rcv_eff_date, " _
                                            & "  rcv_po_oid,rcv_remarks, " _
                                            & "  rcv_packing_slip, " _
                                            & "  rcv_cu_id, " _
                                            & "  rcv_exc_rate, " _
                                            & "  rcv_is_receive, " _
                                            & "  rcv_ret_replace, " _
                                            & "  rcv_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_rcv_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(rcv_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_rcv_code) & ",  " _
                                            & SetDate(rcv_eff_date.DateTime) & ",  " _
                                            & SetDate(rcv_eff_date.DateTime) & ",  " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetSetring(rcv_remarks.Text) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetInteger(rcv_cu_id.EditValue) & ",  " _
                                            & SetDbl(rcv_exc_rate.EditValue) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.rcvd_det " _
                                                    & "( " _
                                                    & "  rcvd_oid, " _
                                                    & "  rcvd_rcv_oid, " _
                                                    & "  rcvd_pod_oid, " _
                                                    & "  rcvd_qty, " _
                                                    & "  rcvd_um, " _
                                                    & "  rcvd_packing_qty, " _
                                                    & "  rcvd_um_conv, " _
                                                    & "  rcvd_qty_real, " _
                                                    & "  rcvd_si_id, " _
                                                    & "  rcvd_loc_id, " _
                                                    & "  rcvd_supp_lot, " _
                                                    & "  rcvd_rea_code_id, " _
                                                    & "  rcvd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("rcvd_oid").ToString) & ",  " _
                                                    & SetSetring(_rcv_oid.ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty") * -1) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_packing_qty") * -1) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real") * -1) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_loc_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("rcvd_supp_lot")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_rea_code_id")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                             

                                ssqls.Add(.Command.CommandText)
                                
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update pod_det
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_return = coalesce(pod_qty_return,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("rcvd_qty").ToString) _
                                                     & " where pod_oid = '" + ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'sqlTran.Rollback()
                                'Exit Function
                               

                                'If ds_edit.Tables(0).Rows(i).Item("rcvd_close_line").ToString.ToUpper = "N" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_receive = coalesce(pod_qty_receive,0) - " + SetDec(ds_edit.Tables(0).Rows(i).Item("rcvd_qty").ToString) _
                                                     & " ,pod_status =  null " _
                                                     & " where pod_oid = '" + ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                _open_po = True 'agar po di open lagi dan muncul di search po ketika po receipt
                                'End If

                                'har 20110617
                                'update boqs================================================
                                Dim _boqs_oid As String
                                _boqs_oid = ""
                                Dim sSql As String

                                sSql = "SELECT  " _
                                  & "  b.reqd_boqs_oid " _
                                  & "FROM " _
                                  & "  public.pod_det a " _
                                  & "  INNER JOIN public.reqd_det b ON (a.pod_reqd_oid = b.reqd_oid) " _
                                  & "WHERE " _
                                  & " a.pod_oid='" & ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString & "'"

                                Dim dt As New DataTable
                                dt = master_new.PGSqlConn.GetTableData(sSql)

                                For Each dr As DataRow In dt.Rows
                                    _boqs_oid = SetString(dr(0))
                                Next

                                If _boqs_oid <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update boqs_stand set boqs_qty_return=coalesce(boqs_qty_return,0) + " _
                                       & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) & ", boqs_qty_receipt=coalesce(boqs_qty_receipt,0) - " _
                                       & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                                '================================================
                            End If
                        Next

                        'By sys 20110418
                        If _open_po = True Then
                            'Untuk update po_close_date jadi null lagi
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update po_mstr set po_close_date = null " + _
                                                   " where po_oid = '" + _po_oid.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '------------------

                        'Untuk Update data serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.rcvds_serial " _
                                                & "( " _
                                                & "  rcvds_oid, " _
                                                & "  rcvds_rcvd_oid, " _
                                                & "  rcvds_qty, " _
                                                & "  rcvds_um, " _
                                                & "  rcvds_si_id, " _
                                                & "  rcvds_loc_id, " _
                                                & "  rcvds_lot_serial, " _
                                                & "  rcvds_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("rcvds_oid").ToString) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("rcvds_rcvd_oid").ToString) & ",  " _
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("rcvds_qty") * -1) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("rcvds_um")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("rcvds_si_id")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("rcvds_loc_id")) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("rcvds_lot_serial")) & ",  " _
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
                                If ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real") > 0 Then
                                    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                        i_2 += 1

                                        _en_id = rcv_en_id.EditValue
                                        _si_id = ds_edit.Tables(0).Rows(i).Item("rcvd_si_id")
                                        _loc_id = ds_edit.Tables(0).Rows(i).Item("rcvd_loc_id")
                                        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                        _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                        _serial = "''"
                                        _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
                                        If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'Update History Inventory                        
                                        _qty = _qty * -1.0
                                        _cost = func_coll.get_cost_pod_det(ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString)
                                        _cost = _cost / ds_edit.Tables(0).Rows(i).Item("rcvd_um_conv")
                                        _cost = _cost * rcv_exc_rate.EditValue

                                        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _rcv_code, _rcv_oid.ToString, "PO Return", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", rcv_eff_date.DateTime) = False Then
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
                                If ds_serial.Tables(0).Rows(i).Item("rcvds_qty") > 0 Then
                                    i_2 += 1

                                    _en_id = rcv_en_id.EditValue
                                    _si_id = ds_serial.Tables(0).Rows(i).Item("rcvds_si_id")
                                    _loc_id = ds_serial.Tables(0).Rows(i).Item("rcvds_loc_id")
                                    _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                                    _serial = ds_serial.Tables(0).Rows(i).Item("rcvds_lot_serial")
                                    _qty = ds_serial.Tables(0).Rows(i).Item("rcvds_qty")
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory
                                    _cost = func_coll.get_cost_pod_det(ds_serial.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString)
                                    _cost = _cost / ds_serial.Tables(0).Rows(i).Item("rcvds_um_conv")
                                    _cost = _cost * rcv_exc_rate.EditValue
                                    _qty = _qty * -1.0

                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)

                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _rcv_code, _rcv_oid.ToString, "PO Return", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", rcv_eff_date.DateTime) = False Then
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
                        '        _en_id = rcv_en_id.EditValue
                        '        _si_id = ds_edit.Tables(0).Rows(i).Item("rcvd_si_id")
                        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '        _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
                        '        _cost = func_coll.get_cost_pod_det(ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid"))
                        '        _cost = _cost / ds_edit.Tables(0).Rows(i).Item("rcvd_um_conv")
                        '        _cost = _cost * rcv_exc_rate.EditValue

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

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If func_coll.insert_glt_det_ic(ssqls, objinsert, ds_edit, _
                                                 rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                                                 _rcv_oid.ToString, _rcv_code, _
                                                 rcv_eff_date.DateTime, _
                                                 rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                                                 "IC", "IC-PRS") = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        If _conf_budget = "1" Then
                            'Update budget realsisai
                            If func_coll.update_budget_po_return(ssqls, objinsert, ds_edit, rcv_en_id.EditValue) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, rcv_en_id.EditValue, 6, _rcv_oid.ToString, _rcv_code, _now) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert PO Return " & _rcv_code)
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
                        set_row(_rcv_oid.ToString, "rcv_oid")
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
    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.rcv_mstr.rcv_code, " _
                    & "  public.rcv_mstr.rcv_date, " _
                    & "  public.rcv_mstr.rcv_eff_date,rcv_remarks, " _
                    & "  public.rcv_mstr.rcv_packing_slip, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  public.rcv_mstr.rcv_exc_rate, " _
                    & "  public.rcv_mstr.rcv_ret_replace, " _
                    & "  public.rcv_mstr.rcv_dt, " _
                    & "  public.po_mstr.po_code, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  pod_pt_desc1, " _
                    & "  pod_pt_desc2, " _
                    & "  public.rcvd_det.rcvd_qty * -1 as rcvd_qty, " _
                    & "  public.rcvd_det.rcvd_qty_inv, " _
                    & "  um_mstr.code_name as rcvd_um_name, " _
                    & "  public.rcvd_det.rcvd_packing_qty * -1 as rcvd_packing_qty , " _
                    & "  public.rcvd_det.rcvd_um_conv, " _
                    & "  public.rcvd_det.rcvd_qty_real * -1 as rcvd_qty_real, " _
                    & "  public.rcvd_det.rcvd_supp_lot, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.rcvd_det.rcvd_close_line, " _
                    & "  rea_code_mstr.code_name as rea_code_name " _
                    & "FROM " _
                    & "  public.rcv_mstr " _
                    & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                    & "  INNER JOIN public.en_mstr ON (public.rcv_mstr.rcv_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.cu_mstr ON (public.rcv_mstr.rcv_cu_id = public.cu_mstr.cu_id)" _
                    & "  INNER JOIN public.rcvd_det ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
                    & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                    & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr um_mstr ON (public.rcvd_det.rcvd_um = um_mstr.code_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.code_mstr rea_code_mstr ON (public.rcvd_det.rcvd_rea_code_id = rea_code_mstr.code_id) " _
                    & " where rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and rcv_is_receive ~~* 'N'" _
                    & " and rcv_en_id in (select user_en_id from tconfuserentity " _
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
            frm._en_id = rcv_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "rea_code_name" Then
            Dim frm As New FReasonCodeReturnSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = rcv_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            'gv_serial.Columns("rcvds_rcvd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("rcvd_oid"))
            'gv_serial.BestFitColumns

            gv_serial.Columns("rcvds_rcvd_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("rcvds_rcvd_oid='" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("rcvd_oid").ToString & "'")
            gv_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        If IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_loc_id")) = True Then
            MessageBox.Show("Location Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        With gv_serial
            .SetRowCellValue(e.RowHandle, "rcvds_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "rcvds_rcvd_oid", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_oid").ToString)
            .SetRowCellValue(e.RowHandle, "rcvd_pod_oid", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_pod_oid").ToString)
            .SetRowCellValue(e.RowHandle, "rcvds_qty", 1)
            .SetRowCellValue(e.RowHandle, "rcvds_um", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_um"))
            .SetRowCellValue(e.RowHandle, "rcvds_um_conv", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_um_conv"))
            .SetRowCellValue(e.RowHandle, "rcvds_si_id", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_si_id"))
            .SetRowCellValue(e.RowHandle, "rcvds_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_loc_id"))
            .SetRowCellValue(e.RowHandle, "pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_id"))
            .SetRowCellValue(e.RowHandle, "pt_code", ds_edit.Tables(0).Rows(_row_edit).Item("pt_code"))
            .SetRowCellValue(e.RowHandle, "pt_type", ds_edit.Tables(0).Rows(_row_edit).Item("pt_type"))
            .BestFitColumns()
        End With
    End Sub

#Region "Overrides From Master"
    Public Overrides Function save_data() As Boolean
        save_data = validasi_save()

        If save_data = True And before_save() = True Then
            If insert_edit = True Then
                save_data = insert()
                If save_data = False Then ' Ini dirubah agar kalau terjadi error tidak langsung kembali ke form awal
                    Return False
                End If
            Else
                save_data = edit()
            End If
            Dim my As master_new.MasterMDI = CType(Me.ParentForm, master_new.MasterMDI)
            my.configurasi_menu("awal_transaksi")

            panel_visibility()
            xtp_edit.PageVisible = False
            xtp_data.PageVisible = True
            For Each ctrl In Me.Controls
                If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                    dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                    dp.Show()
                End If
            Next
        Else
            save_data = False
        End If

        Return save_data
    End Function

    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub
#End Region

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_en_id")
        _type = 6
        _table = "rcv_mstr"
        _initial = "rcv"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "    rcv_oid, " _
            & "    rcv_dom_id, " _
            & "    rcv_en_id, " _
            & "    rcv_add_by, " _
            & "    rcv_add_date, " _
            & "    rcv_upd_by, " _
            & "    rcv_upd_date, " _
            & "    rcv_code, " _
            & "    rcv_date, " _
            & "    rcv_eff_date, " _
            & "    rcv_po_oid, " _
            & "    rcv_packing_slip, " _
            & "    rcv_dt, " _
            & "    rcv_is_receive, " _
            & "    rcv_ret_replace, " _
            & "    rcv_cu_id, " _
            & "    rcv_exc_rate, " _
            & "    rcvd_pod_oid, " _
            & "    po_code, " _
            & "    ptnr_name, " _
            & "    ptnra_line_1, " _
            & "    ptnra_line_2, " _
            & "    ptnra_line_3, " _
            & "    pt_code, " _
            & "    pod_pt_desc1 as pt_desc1, " _
            & "    pod_pt_desc2 as pt_desc2, " _
            & "    rcvd_qty * -1 as rcvd_qty, " _
            & "    rcvd_um, " _
            & "    um_master.code_name as um_name, " _
            & "    rcvd_loc_id, " _
            & "    loc_desc, " _
            & "    rcvd_rea_code_id, " _
            & "    cmaddr_name, " _
            & "    cmaddr_line_1, " _
            & "    cmaddr_line_2, " _
            & "    cmaddr_line_3, " _
            & "    pod_cost, " _
            & "    pod_disc, " _
            & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "  FROM  " _
            & "    rcv_mstr " _
            & "    inner join rcvd_det on rcvd_rcv_oid = rcv_oid " _
            & "    inner join cu_mstr on cu_id = rcv_cu_id " _
            & "    inner join pod_det on pod_oid = rcvd_pod_oid " _
            & "    inner join pt_mstr on pt_id = pod_pt_id " _
            & "    inner join code_mstr um_master on um_master.code_id = rcvd_um " _
            & "    inner join loc_mstr on loc_id = rcvd_loc_id " _
            & "    left outer join code_mstr rea_code_mstr on rea_code_mstr.code_id = rcvd_rea_code_id " _
            & "    left outer join tranaprvd_dok on tranaprvd_tran_oid = rcv_oid  " _
            & "    inner join po_mstr on po_oid = pod_po_oid " _
            & "    inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
            & "    left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "    left outer join cmaddr_mstr on cmaddr_id = po_cmaddr_id " _
            & "    where rcv_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code") + "'"





        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRPurchaseReturnPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
        frm.ShowDialog()

    End Sub

    
  
   
End Class
