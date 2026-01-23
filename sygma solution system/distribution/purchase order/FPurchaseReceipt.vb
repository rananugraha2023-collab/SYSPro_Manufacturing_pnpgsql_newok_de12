Imports npgsql
Imports master_new.ModFunction
Imports System.Net
Imports System.Threading
Imports System.IO
Imports MessagingToolkit.QRCode

Public Class FPurchaseReceipt
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _po_oid As String
    Public ds_edit, ds_serial, ds_qrcode As DataSet
    Dim _now As DateTime
    Dim _conf_budget As String
    Public _rcv_oid, _rcv_freff_oids, _rcv_treff_oids As String
    Dim _count As Integer = 0
    Dim QR_code As New MessagingToolkit.QRCode.Codec.QRCodeEncoder

#Region "SettingAwal"
    Private Sub FPurchaseReceipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_master, "Supplier ID", "ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Receive Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "rcv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Effective Date", "rcv_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Packing Slip", "rcv_packing_slip", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Collie", "rcv_cly_end", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "rcv_exc_rate", DevExpress.Utils.HorzAlignment.far, DevExpress.Utils.FormatType.Numeric, "n")
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
        add_column_copy(gv_detail, "Qty Receipt", "rcvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice", "rcvd_qty_inv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "rcvd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "rcvd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "rcvd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Packing", "rcvd_packing_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Supplier Lot Number", "rcvd_supp_lot", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Serial Number", "rcvds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Serial", "rcvds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

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
        add_column(gv_edit, "pt_type", True)
        add_column(gv_edit, "", False)
        add_column(gv_edit, "rcvd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "rcvd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Receipt", "rcvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "rcvd_um", False)
        add_column(gv_edit, "UM", "rcvd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "UM Conversion", "rcvd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "rcvd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Packing", "rcvd_packing_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Supplier Lot Number", "rcvd_supp_lot", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_memo", False)

        add_column(gv_serial, "rcvds_oid", False)
        add_column(gv_serial, "rcvds_rcvd_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "rcvds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "rcvds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "rcvds_um_conv", False)
        add_column(gv_serial, "rcvds_um", False)
        add_column(gv_serial, "rcvds_si_id", False)
        add_column(gv_serial, "rcvds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_type", True)
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
                    & "  public.rcv_mstr.rcv_eff_date, " _
                    & "  public.rcv_mstr.rcv_po_oid, " _
                    & "  public.rcv_mstr.rcv_packing_slip, " _
                    & "  public.rcv_mstr.rcv_cly_end, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  public.rcv_mstr.rcv_exc_rate, " _
                    & "  public.rcv_mstr.rcv_dt, " _
                    & "  public.po_mstr.po_code, " _
                    & "  public.ptnr_mstr.ptnr_name,public.ptnr_mstr.ptnr_id " _
                    & "FROM " _
                    & "  public.rcv_mstr " _
                    & "  INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                    & "  INNER JOIN public.en_mstr ON (public.rcv_mstr.rcv_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.cu_mstr ON (public.rcv_mstr.rcv_cu_id = public.cu_mstr.cu_id)" _
                    & " where rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and rcv_is_receive ~~* 'Y'" _
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
            & "  public.pt_mstr.pt_type, " _
            & "  public.rcvd_det.rcvd_qty, " _
            & "  public.rcvd_det.rcvd_qty_inv, " _
            & "  public.rcvd_det.rcvd_um, " _
            & "  public.code_mstr.code_name as rcvd_um_name, " _
            & "  public.rcvd_det.rcvd_packing_qty, " _
            & "  public.rcvd_det.rcvd_um_conv, " _
            & "  public.rcvd_det.rcvd_qty_real, " _
            & "  public.rcvd_det.rcvd_si_id, " _
            & "  public.rcvd_det.rcvd_loc_id, " _
            & "  public.rcvd_det.rcvd_lot_serial, " _
            & "  public.rcvd_det.rcvd_supp_lot, " _
            & "  public.rcvd_det.rcvd_dt, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.loc_mstr.loc_desc, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2 " _
            & "FROM " _
            & "  public.rcvd_det " _
            & "  INNER JOIN public.rcv_mstr ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
            & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.rcvd_det.rcvd_um = public.code_mstr.code_id) " _
            & "  INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
            & "  INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id) " _
            & "  where rcv_mstr.rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and rcv_mstr.rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and rcv_mstr.rcv_is_receive ~~* 'Y'"

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
            & "  rcvds_qty, " _
            & "  rcvds_um, " _
            & "  code_name, " _
            & "  rcvds_si_id, " _
            & "  si_desc, " _
            & "  rcvds_loc_id, " _
            & "  loc_desc, " _
            & "  rcvds_lot_serial, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
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
            & "  and rcv_mstr.rcv_is_receive ~~* 'Y'"

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
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()

        rcv_en_id.Focus()
        rcv_en_id.ItemIndex = 0
        rcv_eff_date.DateTime = _now
        'rcv_packing_slip.Text = ""

        rcv_cu_id.Enabled = False
        rcv_exc_rate.Enabled = False
        rcv_cu_id.ItemIndex = 0

        rcv_ce_qrcode.EditValue = False

        rcv_packing_slip.Text = ""
        rcv_cly_end.Text = ""

        rcv_freff_oid.Text = ""
        _rcv_freff_oids = ""

        rcv_treff_oid.Text = ""
        _rcv_treff_oids = ""

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
                        & "  public.pt_mstr.pt_ls, " _
                        & "  public.pt_mstr.pt_type, " _
                        & "  public.rcvd_det.rcvd_qty, 0.0 as qty_open, " _
                        & "  public.rcvd_det.rcvd_um, " _
                        & "  public.code_mstr.code_name as rcvd_um_name, " _
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
                        & "  pod_pt_desc1, " _
                        & "  pod_pt_desc2, " _
                        & "  pod_pt_id, pod_cost, pod_disc, pod_cc_id,'' as po_date, pod_um_conv, " _
                        & "  public.loc_mstr.loc_desc, '' as pod_memo " _
                        & " FROM " _
                        & "  public.rcvd_det " _
                        & "  INNER JOIN public.rcv_mstr ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
                        & "  INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.rcvd_det.rcvd_um = public.code_mstr.code_id) " _
                        & "  INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
                        & "  INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.rcvds_serial ON (public.rcvd_det.rcvd_oid = public.rcvds_serial.rcvds_rcvd_oid)" _
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
                        & "  rcvds_dt, -1 as pt_id, '' as pt_type " _
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
            Return False
        End If

        If rcv_exc_rate.EditValue = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
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
                If ds_edit.Tables(0).Rows(i).Item("loc_desc") = "-" Then
                    MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
                If SetNumber(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) > SetNumber(ds_edit.Tables(0).Rows(i).Item("qty_open")) Then
                    MessageBox.Show("Qty process higher than qty open..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If

                Dim ssql As String
                ssql = "SELECT loc_id " _
                          & "FROM " _
                          & "  public.loc_mstr a " _
                          & "WHERE " _
                          & "  a.loc_id=" & ds_edit.Tables(0).Rows(i).Item("rcvd_loc_id") & " AND  " _
                          & "  a.loc_en_id=" & rcv_en_id.EditValue

                If master_new.PGSqlConn.CekRowSelect(ssql) = 0 Then
                    MessageBox.Show("Location error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Public Overrides Function insert() As Boolean
        _conf_budget = func_coll.get_conf_file("budget_base")

        Dim _rcv_oid As Guid
        _rcv_oid = Guid.NewGuid

        Dim _rcv_code, _qrcd_code, _serial, _cost_methode As String
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        _rcv_code = func_coll.get_transaction_number("RC", rcv_en_id.GetColumnValue("en_code"), "rcv_mstr", "rcv_code")

        '_qrcd_code = func_coll.get_transaction_number("QR", rcv_en_id.GetColumnValue("en_code"), "qrcode_table", "qrcd_code")

        _tran_id = func_coll.get_id_tran_mstr("rct_po")
        If _tran_id = -1 Then
            MessageBox.Show("Receipt PO In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                                            & "  rcv_po_oid, " _
                                            & "  rcv_packing_slip, " _
                                            & "  rcv_cly_end, " _
                                            & "  rcv_cu_id, " _
                                            & "  rcv_exc_rate, " _
                                            & "  rcv_is_receive, " _
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
                                            & SetSetring(rcv_packing_slip.Text) & ",  " _
                                            & SetSetring(rcv_cly_end.EditValue) & ",  " _
                                            & SetInteger(rcv_cu_id.EditValue) & ",  " _
                                            & SetDbl(rcv_exc_rate.EditValue) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _po_date As Date
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                                _po_date = ds_edit.Tables(0).Rows(i).Item("po_date")

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
                                                    & "  rcvd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("rcvd_oid").ToString) & ",  " _
                                                    & SetSetring(_rcv_oid.ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_packing_qty")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("rcvd_loc_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("rcvd_supp_lot")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update pod_det
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_receive = coalesce(pod_qty_receive,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) _
                                                     & " where pod_oid = '" + ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_status = 'C'" _
                                                     & " where pod_oid = '" + ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString + "'" _
                                                     & " and pod_qty = pod_qty_receive "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update qrcode_mstr
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = " update qrcode_mstr set qrcode_reff_oid = '" + _rcv_oid.ToString + "', " _
                                                       & " qrcode_used_date = " + SetDateNTime(master_new.PGSqlConn.CekTanggal) + ", " _
                                                       & " qrcode_reff_code = '" + _rcv_code.ToString + "', " _
                                                       & " qrcode_active = 'Y'" _
                                                       & " where qrcode_name between '" + rcv_freff_oid.EditValue.ToString + "' and '" + rcv_treff_oid.EditValue.ToString + "'" _
                                                       & "  and qrcode_active = 'N'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Untuk update qrcode_mstr apabile semua line sudah terpenuhi qty_receivenya...
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update qrcode_mstr set qrcode_reff_oid = " & SetDateNTime00(rcv_eff_date.DateTime) & "" + _
                                '                       " where coalesce((select count(pod_po_oid) as jml From pod_det " + _
                                '                       " where pod_qty <> coalesce(pod_qty_receive,0) " + _
                                '                       " and pod_po_oid = '" + _po_oid.ToString + "'" + _
                                '                       " group by pod_po_oid),0) = 0 " + _
                                '                       " and po_oid = '" + _po_oid.ToString + "'"

                                'ssqls.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()


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
                                    .Command.CommandText = "update boqs_stand set boqs_qty_receipt=coalesce(boqs_qty_receipt,0) + " _
                                       & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If

                                '================================================
                            End If
                        Next

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
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("rcvds_qty")) & ",  " _
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

                        'Next

                        'Untuk Update data qrcodde
                        'For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.qrcd_table " _
                        '                        & "( " _
                        '                        & "  qrcd_oid, " _
                        '                        & "  qrcd_code, " _
                        '                        & "  qrcd_seq, " _
                        '                        & "  qrcd_reff_oid, " _
                        '                        & "  qrcd_reff_code, " _
                        '                        & "  qrcd_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(ds_qrcode.Tables(0).Rows(i).Item("qrcd_oid").ToString) & ",  " _
                        '                        & SetSetring(_qrcd_code) & ",  " _
                        '                        & SetSetring(_rcv_oid.ToString) & ",  " _
                        '                        & SetSetring(ds_qrcode.Tables(0).Rows(i).Item("qrcd_reff_oid").ToString) & ",  " _
                        '                        & SetSetring(ds_qrcode.Tables(0).Rows(i).Item("_rcv_code")) & ",  " _
                        '                        & SetSetring(ds_qrcode.Tables(0).Rows(i).Item("rcvds_lot_serial")) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ");"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        'Untuk update po_close_date apabile semua line sudah terpenuhi qty_receivenya...
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update po_mstr set po_close_date = " & SetDateNTime00(rcv_eff_date.DateTime) & "" + _
                                               " where coalesce((select count(pod_po_oid) as jml From pod_det " + _
                                               " where pod_qty <> coalesce(pod_qty_receive,0) " + _
                                               " and pod_po_oid = '" + _po_oid.ToString + "'" + _
                                               " group by pod_po_oid),0) = 0 " + _
                                               " and po_oid = '" + _po_oid.ToString + "'"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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
                                        _serial = "''"
                                        _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
                                        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'Update History Inventory                                    
                                        _cost = func_coll.get_cost_pod_det(ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString)
                                        _cost = _cost / ds_edit.Tables(0).Rows(i).Item("rcvd_um_conv")
                                        _cost = _cost * rcv_exc_rate.EditValue

                                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _rcv_code, _rcv_oid.ToString, "PO Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", rcv_eff_date.DateTime) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    Else
                                        If ask(ds_edit.Tables(0).Rows(i).Item("pt_code") & " " & ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1") & " tidak akan masuk ke stok karena Lot/Serial", "Konfirmasi ...", MessageBoxDefaultButton.Button1) = False Then
                                            Return False
                                            Exit Function
                                        Else
                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = insert_log("Insert PO Receipt tidak masuk ke stok karena Lot/Serial " & _rcv_code)
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
                                        End If
                                    End If
                                Else
                                    If ask(ds_edit.Tables(0).Rows(i).Item("pt_code") & " " & ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1") & " tidak akan masuk ke stok karena QTY Real < 1", "Konfirmasi ...", MessageBoxDefaultButton.Button1) = False Then
                                        Return False
                                        Exit Function
                                    Else
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = insert_log("Insert PO Receipt tidak masuk ke stok karena QTY Real < 1" & _rcv_code)
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                End If
                            Else
                                If ask(ds_edit.Tables(0).Rows(i).Item("pt_code") & " " & ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1") & " tidak akan masuk ke stok karena tipenya bukan Inventory.", "Konfirmasi ...", MessageBoxDefaultButton.Button1) = False Then

                                    Return False
                                    Exit Function
                                Else
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = insert_log("Insert PO Receipt tidak masuk ke stok karena tipenya bukan Inventory" & _rcv_code)
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
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
                                    _serial = ds_serial.Tables(0).Rows(i).Item("rcvds_lot_serial")
                                    _qty = ds_serial.Tables(0).Rows(i).Item("rcvds_qty")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory
                                    _cost = func_coll.get_cost_pod_det(ds_serial.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString)
                                    _cost = _cost / ds_serial.Tables(0).Rows(i).Item("rcvds_um_conv")
                                    _cost = _cost * rcv_exc_rate.EditValue

                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)

                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _rcv_code, _rcv_oid.ToString, "PO Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", rcv_eff_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If

                            End If
                        Next

                        '3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Or ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "A" _
                            Or ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "E" Then
                                _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                                _en_id = rcv_en_id.EditValue
                                _si_id = ds_edit.Tables(0).Rows(i).Item("rcvd_si_id")
                                _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
                                _cost = func_coll.get_cost_pod_det(ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString)
                                _cost = _cost / ds_edit.Tables(0).Rows(i).Item("rcvd_um_conv")
                                _cost = _cost * rcv_exc_rate.EditValue
                                If _cost_methode = "F" Or _cost_methode = "L" Then
                                    MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                                    Return False
                                    'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                                    '    sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If
                                ElseIf _cost_methode = "A" Then
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If func_coll.insert_glt_det_ic(ssqls, objinsert, ds_edit, _
                                                 rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                                                 _rcv_oid.ToString, _rcv_code, _
                                                 rcv_eff_date.DateTime, _
                                                 rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                                                 "IC", "IC-RPO") = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        If _conf_budget = "1" Then
                            If func_coll.update_budget_po_receipt(ssqls, objinsert, ds_edit, rcv_en_id.EditValue, _po_date) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert PO Receipt " & _rcv_code)
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

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, rcv_en_id.EditValue, 5, _rcv_oid.ToString, _rcv_code, _now) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

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
#End Region

#Region "gv_edit"
    Private Sub rcv_po_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcv_po_oid.ButtonClick
        Dim frm As New FPurchaseOrderSearch()
        frm.set_win(Me)
        frm._en_id = rcv_en_id.EditValue
        frm._date = rcv_eff_date.DateTime
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub rcv_freff_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcv_freff_oid.ButtonClick
        Dim frm As New FQRCodeSearch()
        frm.set_win(Me)
        frm._en_id = rcv_en_id.EditValue
        frm._date = rcv_eff_date.DateTime
        frm._obj = rcv_cly_end.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub rcv_treff_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcv_treff_oid.ButtonClick
        Dim frm As New FQRCodeSearchTo()
        frm.set_win(Me)
        frm._en_id = rcv_en_id.EditValue
        frm._date = rcv_eff_date.DateTime
        frm._obj = rcv_cly_end.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _rcvd_um_conv As Double = 1
        Dim _rcvd_qty As Double = 1
        Dim _rcvd_qty_real As Double

        If e.Column.Name = "rcvd_qty" Then
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
            Dim frm As New FLocationSearch()
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
            'gv_serial.BestFitColumns()

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
            .SetRowCellValue(e.RowHandle, "pt_type", ds_edit.Tables(0).Rows(_row_edit).Item("pt_type"))
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
#End Region


    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_en_id")
        _type = 5
        _table = "rcv_mstr"
        _initial = "rcv"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
        _rcv_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_oid").ToString

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)
        Dim ds_bantu As New DataSet
        Dim _sql As String

        If rcv_ce_qrcode.EditValue = True Then
            Try
                Dim ssql As String
                _sql = "SELECT  " _
                    & "  public.qrcode_mstr.qrcode_oid, " _
                    & "  public.qrcode_mstr.qrcode_id, " _
                    & "  public.qrcode_mstr.qrcode_en_id, " _
                    & "  public.qrcode_mstr.qrcode_code, " _
                    & "  public.qrcode_mstr.qrcode_name, " _
                    & "  public.qrcode_mstr.qrcode_remarks, " _
                    & "  public.qrcode_mstr.qrcode_active, " _
                    & "  public.en_mstr.en_desc " _
                    & "FROM " _
                    & "  public.qrcode_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.qrcode_mstr.qrcode_en_id = public.en_mstr.en_id)" _
                    & " Where " _
                    & " qrcode_reff_oid = '" + _rcv_oid.ToString + "'" _
                    & "ORDER BY " _
                    & "  public.qrcode_mstr.qrcode_name"

                Dim frm As New frmPrintDialog
                frm._ssql = _sql
                frm._report = "rptLabelQRCode2"
                frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
                frm.ShowDialog()
            Catch ex As Exception

            End Try
        Else
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
                        & "    rcv_cly_end, " _
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
                        & "    rcvd_qty, " _
                        & "    rcvd_um, " _
                        & "    um_master.code_name as um_name, " _
                        & "    rcvd_loc_id, " _
                        & "    loc_desc, " _
                        & "    rcvd_rea_code_id, " _
                        & "    rea_code_mstr.code_name rea_code_name, " _
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
                        & "    where rcv_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code") + "'"

            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRPurchaseReceiptPrint"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
            frm.ShowDialog()

        End If
        '_sql = "SELECT  " _
        '    & "    rcv_oid, " _
        '    & "    rcv_dom_id, " _
        '    & "    rcv_en_id, " _
        '    & "    rcv_add_by, " _
        '    & "    rcv_add_date, " _
        '    & "    rcv_upd_by, " _
        '    & "    rcv_upd_date, " _
        '    & "    rcv_code, " _
        '    & "    rcv_date, " _
        '    & "    rcv_eff_date, " _
        '    & "    rcv_po_oid, " _
        '    & "    rcv_packing_slip, " _
        '    & "    rcv_cly_end, " _
        '    & "    rcv_dt, " _
        '    & "    rcv_is_receive, " _
        '    & "    rcv_ret_replace, " _
        '    & "    rcv_cu_id, " _
        '    & "    rcv_exc_rate, " _
        '    & "    rcvd_pod_oid, " _
        '    & "    po_code, " _
        '    & "    ptnr_name, " _
        '    & "    ptnra_line_1, " _
        '    & "    ptnra_line_2, " _
        '    & "    ptnra_line_3, " _
        '    & "    pt_code, " _
        '    & "    pod_pt_desc1 as pt_desc1, " _
        '    & "    pod_pt_desc2 as pt_desc2, " _
        '    & "    rcvd_qty, " _
        '    & "    rcvd_um, " _
        '    & "    um_master.code_name as um_name, " _
        '    & "    rcvd_loc_id, " _
        '    & "    loc_desc, " _
        '    & "    rcvd_rea_code_id, " _
        '    & "    rea_code_mstr.code_name rea_code_name, " _
        '    & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        '    & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        '    & "  FROM  " _
        '    & "    rcv_mstr " _
        '    & "    inner join rcvd_det on rcvd_rcv_oid = rcv_oid " _
        '    & "    inner join cu_mstr on cu_id = rcv_cu_id " _
        '    & "    inner join pod_det on pod_oid = rcvd_pod_oid " _
        '    & "    inner join pt_mstr on pt_id = pod_pt_id " _
        '    & "    inner join code_mstr um_master on um_master.code_id = rcvd_um " _
        '    & "    inner join loc_mstr on loc_id = rcvd_loc_id " _
        '    & "    left outer join code_mstr rea_code_mstr on rea_code_mstr.code_id = rcvd_rea_code_id " _
        '    & "    left outer join tranaprvd_dok on tranaprvd_tran_oid = rcv_oid  " _
        '    & "    inner join po_mstr on po_oid = pod_po_oid " _
        '    & "    inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
        '    & "    left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
        '    & "    where rcv_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code") + "'"

        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRPurchaseReceiptPrint"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
        'frm.ShowDialog()


    End Sub

    Shared listener As HttpListener
    Private listenThread1 As Thread

    Delegate Sub SetTextCallback(ByVal newString As String)

    Private Sub BtStartBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtStartBarcode.Click
        Try
            If BtStartBarcode.Text = "Start Barcode Scanner" Then
                listener = New HttpListener()
                listener.Prefixes.Add("http://*:8000/")
                'listener.Prefixes.Add("http://127.0.0.1:8000/")
                listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous
                listener.Start()
                Me.listenThread1 = New Thread(New ParameterizedThreadStart(AddressOf startlistener))
                listenThread1.Start()
                BtStartBarcode.Text = "Stop.."

                Dim GetIPv4Address As String = ""

                Dim hostName As String = Dns.GetHostName
                Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(hostName)
                Dim i As Integer = 0
                qrcode1.Image = Nothing
                qrcode2.Image = Nothing
                For Each ipheal As System.Net.IPAddress In iphe.AddressList
                    If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                        GetIPv4Address = IIf(GetIPv4Address.Length > 0, GetIPv4Address & ", ", "") & "http://" & ipheal.ToString() & ":8000"
                        If i = 0 Then
                            Try
                                'qrcode1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
                                qrcode1.Image = QR_code.Encode("http://" & ipheal.ToString() & ":8000")
                            Catch ex As Exception

                            End Try
                        ElseIf i = 1 Then
                            Try
                                qrcode2.Image = QR_code.Encode("http://" & ipheal.ToString() & ":8000")
                            Catch ex As Exception

                            End Try
                        End If
                        i = i + 1
                    End If
                Next

                'LabelControl3.Text = GetIPv4Address
            Else

                listener.Stop()
                listenThread1.Abort()
                BtStartBarcode.Text = "Start Barcode Scanner"
                'LabelControl3.Text = ""
            End If
            

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub startlistener(ByVal s As Object)
        While True
            ProcessRequest()
        End While
    End Sub

    Private Sub ProcessRequest()
        Dim result = listener.BeginGetContext(AddressOf ListenerCallback, listener)
        result.AsyncWaitHandle.WaitOne()
    End Sub

    Private Sub ListenerCallback(ByVal result As IAsyncResult)
        Try
            Dim context = listener.EndGetContext(result)
            Thread.Sleep(1000)
            Dim data_text = New StreamReader(context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd()

            Dim cleaned_data = data_text
            'TextBox1.Text = cleaned_data

            SetText(cleaned_data)

            context.Response.StatusCode = 200
            context.Response.StatusDescription = "OK"
            Dim responseString As String = "{""success"":true}"
            Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
            context.Response.ContentLength64 = buffer.Length
            Dim output As System.IO.Stream = context.Response.OutputStream
            output.Write(buffer, 0, buffer.Length)
            context.Response.Close()

            _count = 0
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub SetText(ByVal newString As String)
        Try
            
            If Me.rcv_po_oid.InvokeRequired Then
                Dim d As New SetTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {newString})

            Else
                Me.rcv_po_oid.Text = newString
            End If

            Dim sSQL As String
            sSQL = "SELECT  " _
                & "  public.po_mstr.po_oid, " _
                & "  public.po_mstr.po_en_id, " _
                & "  public.po_mstr.po_code, " _
                & "  public.po_mstr.po_date, " _
                & "  public.po_mstr.po_ptnr_id, " _
                & "  public.po_mstr.po_cmaddr_id, " _
                & "  public.po_mstr.po_date, " _
                & "  public.po_mstr.po_si_id, " _
                & "  public.po_mstr.po_close_date, " _
                & "  public.po_mstr.po_cu_id, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, " _
                & "  public.cmaddr_mstr.cmaddr_name, en_desc " _
                & "FROM " _
                & "  public.po_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id)" _
                & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id)" _
                & "  where po_code = " + SetSetring(newString) _
                & "  "

            Dim dt_po As New DataTable
            dt_po = master_new.PGSqlConn.GetTableData(sSQL)
            If _count > 0 Then
                Exit Sub
            End If
            Dim ds_po As New DataSet
            For Each dr_po As DataRow In dt_po.Rows
                _po_oid = dr_po("po_oid").ToString
                rcv_en_id.EditValue = dr_po("po_en_id")
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                                & "  pod_oid, " _
                                & "  pod_en_id, pod_cc_id," _
                                & "  en_desc, " _
                                & "  pod_po_oid, " _
                                & "  pod_si_id, " _
                                & "  si_desc, " _
                                & "  pod_pt_id, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  pod_pt_desc1, " _
                                & "  pod_pt_desc2, " _
                                & "  pt_ls, " _
                                & "  pt_type, " _
                                & "  pod_qty, pod_cost,pod_disc," _
                                & "  pod_qty_receive, " _
                                & "  (pod_qty - coalesce(pod_qty_receive,0)) as pod_qty_open, " _
                                & "  pod_um, " _
                                & "  coalesce(pod_memo,'') as pod_memo, " _
                                & "  po_cu_id, " _
                                & "  code_name as pod_um_name, " _
                                & "  pt_loc_id, " _
                                & "  loc_desc, " _
                                & "  pod_um_conv, " _
                                & "  pod_qty_real, po_date " _
                                & "FROM  " _
                                & "  public.pod_det  " _
                                & "  inner join public.po_mstr on public.pod_det.pod_po_oid = public.po_mstr.po_oid " _
                                & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                                & "  inner join public.loc_mstr on public.pt_mstr.pt_loc_id = public.loc_mstr.loc_id " _
                                & "  inner join public.en_mstr on public.pod_det.pod_en_id = public.en_mstr.en_id " _
                                & "  inner join public.si_mstr on public.pod_det.pod_si_id = public.si_mstr.si_id " _
                                & "  inner join public.code_mstr on public.pod_det.pod_um = public.code_mstr.code_id " _
                                & "  where (pod_qty - coalesce(pod_qty_receive,0)) > 0 " _
                                & "  and pod_po_oid = '" + _po_oid + "'"
                            .InitializeCommand()
                            .FillDataSet(ds_po, "pod_det")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                ds_edit.Tables(0).Clear()
                Dim _exc_rate As Double
                Dim i As Integer
                Dim _date As Date

                If ds_po.Tables(0).Rows.Count > 0 Then
                    rcv_cu_id.EditValue = ds_po.Tables(0).Rows(0).Item("po_cu_id")
                    If ds_po.Tables(0).Rows(0).Item("po_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                        _exc_rate = func_data.get_exchange_rate(ds_po.Tables(0).Rows(0).Item("po_cu_id"), _date)
                        If _exc_rate = 1 Then
                            rcv_exc_rate.EditValue = 0
                        Else
                            rcv_exc_rate.EditValue = _exc_rate
                        End If

                        'fobject.rcv_exc_rate.Enabled = True
                    Else
                        rcv_exc_rate.EditValue = 1
                        'fobject.rcv_exc_rate.Enabled = False
                    End If
                End If


                Dim _dtrow As DataRow
                For i = 0 To ds_po.Tables(0).Rows.Count - 1
                    _dtrow = ds_edit.Tables(0).NewRow
                    _dtrow("rcvd_oid") = Guid.NewGuid.ToString
                    _dtrow("rcvd_pod_oid") = ds_po.Tables(0).Rows(i).Item("pod_oid").ToString
                    _dtrow("pod_pt_id") = ds_po.Tables(0).Rows(i).Item("pod_pt_id")
                    _dtrow("pt_id") = ds_po.Tables(0).Rows(i).Item("pod_pt_id")
                    _dtrow("pt_code") = ds_po.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("pod_pt_desc1") = ds_po.Tables(0).Rows(i).Item("pod_pt_desc1")
                    _dtrow("pod_pt_desc2") = ds_po.Tables(0).Rows(i).Item("pod_pt_desc2")
                    _dtrow("pt_ls") = ds_po.Tables(0).Rows(i).Item("pt_ls")
                    _dtrow("pt_type") = ds_po.Tables(0).Rows(i).Item("pt_type")
                    _dtrow("rcvd_si_id") = ds_po.Tables(0).Rows(i).Item("pod_si_id")
                    _dtrow("si_desc") = ds_po.Tables(0).Rows(i).Item("si_desc")
                    _dtrow("rcvd_loc_id") = ds_po.Tables(0).Rows(i).Item("pt_loc_id")
                    _dtrow("loc_desc") = ds_po.Tables(0).Rows(i).Item("loc_desc")
                    _dtrow("qty_open") = ds_po.Tables(0).Rows(i).Item("pod_qty_open")
                    _dtrow("rcvd_qty") = ds_po.Tables(0).Rows(i).Item("pod_qty_open")
                    _dtrow("rcvd_um") = ds_po.Tables(0).Rows(i).Item("pod_um")
                    _dtrow("rcvd_um_name") = ds_po.Tables(0).Rows(i).Item("pod_um_name")
                    _dtrow("rcvd_um_conv") = ds_po.Tables(0).Rows(i).Item("pod_um_conv")
                    _dtrow("rcvd_qty_real") = CDbl(ds_po.Tables(0).Rows(i).Item("pod_qty_open")) * (ds_po.Tables(0).Rows(i).Item("pod_um_conv"))
                    _dtrow("rcvd_packing_qty") = 0
                    _dtrow("pod_memo") = ds_po.Tables(0).Rows(i).Item("pod_memo")
                    _dtrow("po_date") = ds_po.Tables(0).Rows(i).Item("po_date")
                    _dtrow("pod_cost") = ds_po.Tables(0).Rows(i).Item("pod_cost")
                    _dtrow("pod_disc") = ds_po.Tables(0).Rows(i).Item("pod_disc")
                    _dtrow("pod_cc_id") = ds_po.Tables(0).Rows(i).Item("pod_cc_id")

                    ds_edit.Tables(0).Rows.Add(_dtrow)
                Next
                ds_edit.Tables(0).AcceptChanges()

                gv_edit.BestFitColumns()
            Next
            _count = _count + 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

   
    Private Sub BtPrintQRcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '    Try
        '        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
        '            'If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "I" Then
        '            '    MessageBox.Show("Can't Edit Data That Has Been Release", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            '    Exit Sub
        '            'End If
        '            MessageBox.Show("No access to this menu..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End If

        '        If MessageBox.Show("Are you sure to reset ?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
        '            Exit Sub
        '        End If

        '        Dim ssqls As New ArrayList

        '        Dim ssql As String

        '        ssql = "UPDATE public.po_mstr set po_trans_id='D' where po_oid='" _
        '                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid") & "'"

        '        ssqls.Add(ssql)

        '        ssqls.Add(insert_log("Reset Purchase Order " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")))

        '        Dim ds_bantu As New DataSet
        '        If _conf_value = "1" Then
        '            ds_bantu = func_data.load_aprv_mstr(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_tran_id"))
        '        End If

        '        If _conf_value = "1" Then
        '            ssql = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid") + "'"
        '            ssqls.Add(ssql)

        '            Dim j As Integer
        '            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
        '                ssql = "INSERT INTO  " _
        '                        & "  public.wf_mstr " _
        '                        & "( " _
        '                        & "  wf_oid, " _
        '                        & "  wf_dom_id, " _
        '                        & "  wf_en_id, " _
        '                        & "  wf_tran_id, " _
        '                        & "  wf_ref_oid, " _
        '                        & "  wf_ref_code, " _
        '                        & "  wf_ref_desc, " _
        '                        & "  wf_seq, " _
        '                        & "  wf_user_id, " _
        '                        & "  wf_wfs_id, " _
        '                        & "  wf_iscurrent, " _
        '                        & "  wf_dt " _
        '                        & ")  " _
        '                        & "VALUES ( " _
        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                        & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_en_id")) & ",  " _
        '                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_tran_id")) & ",  " _
        '                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid")) & ",  " _
        '                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")) & ",  " _
        '                        & SetSetring("Purchase Order") & ",  " _
        '                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
        '                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
        '                        & SetInteger(0) & ",  " _
        '                        & SetSetring("N") & ",  " _
        '                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
        '                        & ")"
        '                ssqls.Add(ssql)

        '            Next
        '        End If


        '        If master_new.PGSqlConn.status_sync = True Then
        '            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

        '                Exit Sub
        '            End If
        '            ssqls.Clear()
        '        Else
        '            If DbRunTran(ssqls, "") = False Then

        '                Exit Sub
        '            End If
        '            ssqls.Clear()
        '        End If

        '        Box("Reload success")
        '        after_success()
        '        set_row(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid"), "po_oid")

        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
    End Sub


End Class
