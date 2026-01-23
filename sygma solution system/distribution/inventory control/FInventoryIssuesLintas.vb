Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports Newtonsoft.Json.Linq

Public Class FInventoryIssuesLintas
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime
    Dim _ref_pb_inventory_issue_par As String
    Public _riu_ref_pb_oid As String
    Public _en_id_reff As Integer = -1
    Public _en_desc_reff As String
    Dim mf As New master_new.ModFunction

    Private Sub FInventoryIssues_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        _ref_pb_inventory_issue_par = func_coll.get_conf_file("ref_pb_inventory_issue")

        If _ref_pb_inventory_issue_par = "0" Then
            ref_pb_inventory_request.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            gv_master.Columns("riu_ref_pb_code").Visible = False
            gv_edit.Columns("qty_open").Visible = False
        Else
            ref_pb_inventory_request.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            gv_master.Columns("riu_ref_pb_code").Visible = True
            gv_edit.Columns("qty_open").Visible = True
        End If
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        riu_en_id.Properties.DataSource = dt_bantu
        riu_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        riu_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        riu_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Receive Number", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "riu_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Inventory Req. Number", "riu_ref_pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "riu_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "riu_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "riu_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "riu_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "riu_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "riud_riu_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Issue", "riud_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "riud_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "riud_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "riud_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Lot Number", "riud_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "riud_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_serial, "riud_riu_oid", False)
        add_column_copy(gv_detail_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "riuds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "riuds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "riud_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "riud_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "", False)
        add_column(gv_edit, "riud_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Issues", "riud_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "riud_um", False)
        add_column(gv_edit, "UM", "riud_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "riud_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "riud_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Lot Number", "riud_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost", "riud_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "riud_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "riud_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "riud_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "riud_pbd_oid", False)

        add_column(gv_serial, "riuds_oid", False)
        add_column(gv_serial, "riuds_riud_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "riuds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "riuds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "riuds_um", False)
        add_column(gv_serial, "riuds_si_id", False)
        add_column(gv_serial, "riuds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_code", False)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  riu_oid, " _
                    & "  riu_dom_id, " _
                    & "  riu_en_id, " _
                    & "  en_desc, " _
                    & "  riu_add_by, " _
                    & "  riu_add_date, " _
                    & "  riu_upd_by, " _
                    & "  riu_upd_date, " _
                    & "  riu_type2, " _
                    & "  riu_date, " _
                    & "  riu_type, " _
                    & "  riu_ref_pb_code, " _
                    & "  riu_ref_pb_oid, " _
                    & "  riu_remarks, " _
                    & "  riu_dt,riu_is_sample " _
                    & "FROM  " _
                    & "  public.riu_mstr " _
                    & " inner join en_mstr on en_id = riu_en_id" _
                    & " where riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and riu_type ~~* 'I' and riu_is_sample='A' " _
                    & " and riu_en_id in (select user_en_id from tconfuserentity " _
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
            & "  riud_oid, " _
            & "  riud_riu_oid, " _
            & "  riud_pt_id, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_ls, " _
            & "  riud_qty * -1 as riud_qty, " _
            & "  riud_um, " _
            & "  code_name as riud_um_name, " _
            & "  riud_um_conv, " _
            & "  riud_qty_real * -1 as riud_qty_real, " _
            & "  riud_si_id, " _
            & "  si_desc, " _
            & "  riud_loc_id, " _
            & "  loc_desc, " _
            & "  riud_lot_serial, " _
            & "  riud_cost, " _
            & "  riud_ac_id, " _
            & "  ac_code, " _
            & "  ac_name, " _
            & "  riud_sb_id, " _
            & "  sb_desc, " _
            & "  riud_cc_id, " _
            & "  cc_desc, " _
            & "  riud_dt " _
            & "FROM  " _
            & "  public.riud_det" _
            & "  INNER JOIN public.riu_mstr ON (public.riud_det.riud_riu_oid = public.riu_mstr.riu_oid) " _
            & "  INNER JOIN public.pt_mstr ON (public.riud_det.riud_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.riud_det.riud_um = public.code_mstr.code_id) " _
            & "  INNER JOIN public.loc_mstr ON (public.riud_det.riud_loc_id = public.loc_mstr.loc_id) " _
            & "  INNER JOIN public.si_mstr ON (public.riud_det.riud_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.ac_mstr ON (public.riud_det.riud_ac_id = public.ac_mstr.ac_id) " _
            & "  INNER JOIN public.sb_mstr ON (public.riud_det.riud_sb_id = public.sb_mstr.sb_id) " _
            & "  INNER JOIN public.cc_mstr ON (public.riud_det.riud_cc_id = public.cc_mstr.cc_id) " _
            & "  where riu_mstr.riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and riu_mstr.riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and riu_mstr.riu_type ~~* 'I'"

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  riuds_oid, " _
            & "  riud_riu_oid, " _
            & "  riuds_riud_oid, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  riuds_qty * -1 as riuds_qty , " _
            & "  riuds_si_id, " _
            & "  riuds_loc_id, " _
            & "  riuds_lot_serial, " _
            & "  riuds_dt, " _
            & "  riuds_um " _
            & "FROM  " _
            & "  public.riuds_serial" _
            & "  inner join riud_det on riud_oid = riuds_riud_oid " _
            & "  inner join riu_mstr on riu_oid = riud_riu_oid " _
            & "  inner join pt_mstr on pt_id = riud_pt_id " _
            & "  inner join si_mstr on si_id = riuds_si_id " _
            & "  inner join loc_mstr on loc_id = riuds_loc_id " _
            & "  inner join code_mstr on code_id = riuds_um" _
            & "  where riu_mstr.riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and riu_mstr.riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and riu_mstr.riu_type ~~* 'I'"

        load_data_detail(sql, gc_detail_serial, "detail_serial")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("riud_riu_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("riud_riu_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_serial.Columns("riud_riu_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("riud_riu_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        riu_en_id.Focus()
        riu_en_id.ItemIndex = 0
        riu_date.DateTime = _now
        riu_remarks.Text = ""
        riu_ref_pb_code.Text = ""
        _riu_ref_pb_oid = ""
        riu_ref_pb_code.Enabled = True
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
                            & "  riud_oid, " _
                            & "  riud_riu_oid, " _
                            & "  riud_pt_id, " _
                            & "  pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  riud_qty, " _
                            & "  0 as qty_open, " _
                            & "  riud_um, " _
                            & "  code_name as riud_um_name , " _
                            & "  riud_um_conv, " _
                            & "  riud_qty_real, " _
                            & "  riud_si_id, " _
                            & "  si_desc, " _
                            & "  riud_loc_id, " _
                            & "  loc_desc, " _
                            & "  riud_lot_serial, " _
                            & "  riud_cost, " _
                            & "  riud_ac_id, " _
                            & "  ac_code, " _
                            & "  ac_name, " _
                            & "  riud_sb_id, " _
                            & "  sb_desc, " _
                            & "  riud_cc_id, " _
                            & "  cc_desc, " _
                            & "  riud_pbd_oid, " _
                            & "  riud_dt " _
                            & "FROM  " _
                            & "  public.riud_det" _
                            & "  INNER JOIN public.riu_mstr ON (public.riud_det.riud_riu_oid = public.riu_mstr.riu_oid) " _
                            & "  INNER JOIN public.pt_mstr ON (public.riud_det.riud_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.riud_det.riud_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.loc_mstr ON (public.riud_det.riud_loc_id = public.loc_mstr.loc_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.riud_det.riud_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.ac_mstr ON (public.riud_det.riud_ac_id = public.ac_mstr.ac_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.riud_det.riud_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.riud_det.riud_cc_id = public.cc_mstr.cc_id) " _
                            & "  where riud_ac_id = -99"
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
                        & "  riuds_oid, " _
                        & "  riud_riu_oid, " _
                        & "  riuds_riud_oid, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  riuds_qty, " _
                        & "  0 as riuds_cost, " _
                        & "  riuds_si_id, " _
                        & "  riuds_loc_id, " _
                        & "  riuds_lot_serial, " _
                        & "  riuds_dt, " _
                        & "  riuds_um " _
                        & "FROM  " _
                        & "  public.riuds_serial" _
                        & "  inner join riud_det on riud_oid = riuds_riud_oid " _
                        & "  inner join riu_mstr on riu_oid = riud_riu_oid " _
                        & "  inner join pt_mstr on pt_id = riud_pt_id " _
                        & "  inner join si_mstr on si_id = riuds_si_id " _
                        & "  inner join loc_mstr on loc_id = riuds_loc_id " _
                        & "  inner join code_mstr on code_id = riuds_um" _
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

        Dim _date As Date = riu_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(riu_en_id.EditValue, "gcald_ic", _date)

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

        Dim i, j As Integer
        Dim _qty, _qty_ttl_serial As Double

        '*********************
        'Cek part number, Location, site , account
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pt_id")) = True Then
                MessageBox.Show("Part Number Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(i).Item("riud_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(i).Item("riud_si_id")) = True Then
                MessageBox.Show("Site Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(i).Item("riud_ac_id")) = True Then
                MessageBox.Show("Account Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            ElseIf ds_edit.Tables(0).Rows(i).Item("riud_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
        '*********************

        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai qty lebih dari 1
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("riud_oid").ToString = ds_serial.Tables(0).Rows(j).Item("riuds_riud_oid").ToString Then
                        If ds_serial.Tables(0).Rows(j).Item("riuds_qty") > 1 Then
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
                _qty = ds_edit.Tables(0).Rows(i).Item("riud_qty_real")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("riud_oid").ToString = ds_serial.Tables(0).Rows(j).Item("riuds_riud_oid").ToString Then
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
                _qty = ds_edit.Tables(0).Rows(i).Item("riud_qty_real")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("riud_oid").ToString = ds_serial.Tables(0).Rows(j).Item("riuds_riud_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("riuds_qty")
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

        ''cek inventory allocation nya
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If func_coll.cek_inventory_allocation(riu_en_id.EditValue, ds_edit.Tables(0).Rows(i).Item("riud_si_id"), _
        '                                        ds_edit.Tables(0).Rows(i).Item("riud_loc_id"), ds_edit.Tables(0).Rows(i).Item("pt_id"), _
        '                                        ds_edit.Tables(0).Rows(i).Item("pt_code"), ds_edit.Tables(0).Rows(i).Item("riud_qty"), ds_edit.Tables(0).Rows(i).Item("riud_lot_serial")) = False Then
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _riud_um_conv As Double = 1
        Dim _riud_qty As Double = 1
        Dim _riud_qty_real As Double
        Dim _qty_open As Double = 0

        If e.Column.Name = "riud_qty" Then

            Try
                _qty_open = (gv_edit.GetRowCellValue(e.RowHandle, "qty_open"))
            Catch ex As Exception
            End Try

            'edit by har 20110505
            If _ref_pb_inventory_issue_par <> "0" Then
                If e.Value > _qty_open Then
                    MessageBox.Show("Qty Issue Can't Higher Than Qty Open..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gv_edit.CancelUpdateCurrentRow()
                    Exit Sub
                End If
            End If

            '********************************

            Try
                _riud_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "riud_um_conv"))
            Catch ex As Exception
            End Try

            _riud_qty_real = e.Value * _riud_um_conv
            gv_edit.SetRowCellValue(e.RowHandle, "riud_qty_real", _riud_qty_real)
        ElseIf e.Column.Name = "riud_um_conv" Then
            Try
                _riud_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "riud_qty")))
            Catch ex As Exception
            End Try

            _riud_qty_real = e.Value * _riud_qty
            gv_edit.SetRowCellValue(e.RowHandle, "riud_qty_real", _riud_qty_real)
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
        gv_edit.UpdateCurrentRow()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        'If _col = "pt_code" Then
        '    Dim frm As New FPartNumberSearch()
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = riu_en_id.EditValue
        '    frm._si_id = gv_edit.GetRowCellValue(_row, "riud_si_id")
        '    frm._tran_oid = _riu_ref_pb_oid
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "si_desc" Then
        '    Dim frm As New FSiteSearch()
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = riu_en_id.EditValue
        '    frm.type_form = True
        '    frm.ShowDialog()
        'Else
        If _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = riu_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_code" Then
            Dim frm As New FAccountSearch()

            Dim _filter As String

            _filter = " and ac_id in (SELECT  " _
              & "  enacc_ac_id " _
              & "FROM  " _
              & "  public.enacc_mstr  " _
              & "Where enacc_en_id=" & SetInteger(riu_en_id.EditValue) & " and enacc_code='inventory_issue_AJB_acc') "

            If limit_account(riu_en_id.EditValue) = True Then
                frm._obj = _filter
            Else
                frm._obj = ""
            End If

            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch()
            Dim _filter As String

            _filter = " and ac_id in (SELECT  " _
              & "  enacc_ac_id " _
              & "FROM  " _
              & "  public.enacc_mstr  " _
              & "Where enacc_en_id=" & SetInteger(riu_en_id.EditValue) & " and enacc_code='inventory_issue_AJB_acc') "

            If limit_account(riu_en_id.EditValue) = True Then
                frm._obj = _filter
            Else
                frm._obj = ""
            End If

            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            gv_serial.Columns("riuds_riud_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("riuds_riud_oid='" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("riud_oid").ToString & "'")
            gv_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        If IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("riud_loc_id")) = True Then
            MessageBox.Show("Location Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        With gv_serial
            .SetRowCellValue(e.RowHandle, "riuds_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "riuds_riud_oid", ds_edit.Tables(0).Rows(_row_edit).Item("riud_oid").ToString)
            .SetRowCellValue(e.RowHandle, "riuds_qty", 1)
            .SetRowCellValue(e.RowHandle, "riuds_um", ds_edit.Tables(0).Rows(_row_edit).Item("riud_um"))
            .SetRowCellValue(e.RowHandle, "riuds_si_id", ds_edit.Tables(0).Rows(_row_edit).Item("riud_si_id"))
            .SetRowCellValue(e.RowHandle, "riuds_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("riud_loc_id"))
            .SetRowCellValue(e.RowHandle, "pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_id"))
            .SetRowCellValue(e.RowHandle, "pt_code", ds_edit.Tables(0).Rows(_row_edit).Item("pt_code"))
            .SetRowCellValue(e.RowHandle, "riuds_cost", ds_edit.Tables(0).Rows(_row_edit).Item("riud_cost"))
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow

        With gv_edit
            .SetRowCellValue(e.RowHandle, "riud_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "riud_qty", 0)
            .SetRowCellValue(e.RowHandle, "riud_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "riud_qty_real", 0)
            .SetRowCellValue(e.RowHandle, "riud_cost", 0)
            .SetRowCellValue(e.RowHandle, "riud_sb_id", 0)
            .SetRowCellValue(e.RowHandle, "sb_desc", "-")
            .SetRowCellValue(e.RowHandle, "riud_cc_id", 0)
            .SetRowCellValue(e.RowHandle, "cc_desc", "-")
            Dim ssql As String
            Dim _ac_so As Integer = -1
            Dim _ac_code, _ac_name As String
            _ac_code = ""
            _ac_name = ""

            ssql = "SELECT  " _
                & "  a.enacc_ac_id,ac_code,ac_name " _
                & "FROM " _
                & "  public.enacc_mstr a " _
                & " inner join ac_mstr b on (enacc_ac_id=ac_id) " _
                & "WHERE " _
                & "  a.enacc_code='promo_account' AND  " _
                & "  a.enacc_en_id=" & SetInteger(riu_en_id.EditValue)

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSql)

            For Each dr As DataRow In dt.Rows
                _ac_so = dr(0)
                _ac_code = dr(1)
                _ac_name = dr(2)
            Next

            If _ac_so = -1 And riu_en_id.EditValue <> 0 Then
                Box("Please setting account default for Promo for this entity")
            Else
                .SetRowCellValue(e.RowHandle, "riu_ac_id", _ac_so)
                .SetRowCellValue(e.RowHandle, "ac_code", _ac_code)
                .SetRowCellValue(e.RowHandle, "ac_name", _ac_name)
            End If


            .BestFitColumns()
        End With

        'riu_ac_id,ac_code,ac_name
    End Sub

    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub

#Region "DML"
    Public Overrides Function insert() As Boolean
        Dim _riu_oid As Guid = Guid.NewGuid

        Dim _serial, _pt_code As String
        'Dim _cost_methode As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        _tran_id = func_coll.get_id_tran_mstr("iss-unp")
        If _tran_id = -1 Then
            MessageBox.Show("Inventory Issue In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
        End If

        Dim _riu_type2 As String
        _riu_type2 = func_coll.get_transaction_number("IU", riu_en_id.GetColumnValue("en_code"), "riu_mstr", "riu_type2")




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
                                            & "  public.riu_mstr " _
                                            & "( " _
                                            & "  riu_oid, " _
                                            & "  riu_dom_id, " _
                                            & "  riu_en_id, " _
                                            & "  riu_add_by, " _
                                            & "  riu_add_date, " _
                                            & "  riu_type2, " _
                                            & "  riu_date, " _
                                            & "  riu_type, " _
                                            & "  riu_remarks, " _
                                            & "  riu_ref_pb_code, " _
                                            & "  riu_ref_pb_oid,riu_is_sample, " _
                                            & "  riu_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_riu_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(riu_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_riu_type2) & ",  " _
                                            & SetDate(riu_date.DateTime) & ",  " _
                                            & SetSetring("I") & ",  " _
                                            & SetSetring(riu_remarks.Text) & ",  " _
                                            & SetSetring(riu_ref_pb_code.Text) & ",  " _
                                            & SetSetring(_riu_ref_pb_oid.ToString) & ",'A',  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"

                        


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("riud_qty") > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.riud_det " _
                                                    & "( " _
                                                    & "  riud_oid, " _
                                                    & "  riud_riu_oid, " _
                                                    & "  riud_pt_id, " _
                                                    & "  riud_qty, " _
                                                    & "  riud_um, " _
                                                    & "  riud_um_conv, " _
                                                    & "  riud_qty_real, " _
                                                    & "  riud_si_id, " _
                                                    & "  riud_loc_id, " _
                                                    & "  riud_lot_serial, " _
                                                    & "  riud_cost, " _
                                                    & "  riud_ac_id, " _
                                                    & "  riud_sb_id, " _
                                                    & "  riud_cc_id, riud_pbd_oid, " _
                                                    & "  riud_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("riud_oid").ToString) & ",  " _
                                                    & SetSetring(_riu_oid.ToString) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pt_id")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("riud_qty") * -1) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("riud_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("riud_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("riud_qty_real") * -1) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("riud_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("riud_loc_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("riud_lot_serial")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("riud_cost")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("riud_ac_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("riud_sb_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("riud_cc_id")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("riud_pbd_oid").ToString) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If ds_edit.Tables(0).Rows(i).Item("riud_pbd_oid").ToString <> "" Then
                                    'update karena ada hubungan dengan pb_mstr (inventory request)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update pbd_det set pbd_qty_riud = coalesce(pbd_qty_riud,0) +" + SetDbl(ds_edit.Tables(0).Rows(i).Item("riud_qty")) + ", " _
                                                         & " pbd_qty_completed = coalesce(pbd_qty_completed,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("riud_qty")) _
                                                         & " where pbd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("riud_pbd_oid").ToString)

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update pb_mstr set pb_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & ", pb_status = 'C' " + _
                                                         " where coalesce((select count(pbd_pb_oid) as jml From pbd_det " + _
                                                         " where pbd_qty <> coalesce(pbd_qty_riud,0) " + _
                                                         " and pbd_pb_oid = " & SetSetring(_riu_ref_pb_oid.ToString) & " " & _
                                                         " group by pbd_pb_oid),0) = 0 " & _
                                                         " and pb_oid = " & SetSetring(_riu_ref_pb_oid.ToString) & " "

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            End If
                        Next

                        'Untuk Update data serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.riuds_serial " _
                                                & "( " _
                                                & "  riuds_oid, " _
                                                & "  riuds_riud_oid, " _
                                                & "  riuds_qty, " _
                                                & "  riuds_si_id, " _
                                                & "  riuds_loc_id, " _
                                                & "  riuds_lot_serial, " _
                                                & "  riuds_dt, " _
                                                & "  riuds_um " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("riuds_oid").ToString) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("riuds_riud_oid").ToString) & ",  " _
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("riuds_qty")) * -1 & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("riuds_si_id")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("riuds_loc_id")) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("riuds_lot_serial")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("riuds_um")) & "  " _
                                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("riud_qty") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = riu_en_id.EditValue
                                    _si_id = ds_edit.Tables(0).Rows(i).Item("riud_si_id")
                                    _loc_id = ds_edit.Tables(0).Rows(i).Item("riud_loc_id")
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    '_qty = ds_edit.Tables(0).Rows(i).Item("riud_qty")
                                    _qty = ds_edit.Tables(0).Rows(i).Item("riud_qty_real")
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _qty = _qty * -1
                                    _cost = ds_edit.Tables(0).Rows(i).Item("riud_cost") / ds_edit.Tables(0).Rows(i).Item("riud_um_conv")
                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, Trim(_riu_type2), _riu_oid.ToString, "Inventory Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", riu_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            If ds_serial.Tables(0).Rows(i).Item("riuds_qty") > 0 Then
                                i_2 += 1

                                _en_id = riu_en_id.EditValue
                                _si_id = ds_serial.Tables(0).Rows(i).Item("riuds_si_id")
                                _loc_id = ds_serial.Tables(0).Rows(i).Item("riuds_loc_id")
                                _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                                _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                                _serial = ds_serial.Tables(0).Rows(i).Item("riuds_lot_serial")
                                _qty = ds_serial.Tables(0).Rows(i).Item("riuds_qty")
                                If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                'Update History Inventory
                                _cost = ds_serial.Tables(0).Rows(i).Item("riuds_cost") / ds_serial.Tables(0).Rows(i).Item("riuds_um_conv")
                                _qty = _qty * -1
                                _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, Trim(_riu_type2), _riu_oid.ToString, "Inventory Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", riu_date.DateTime) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If
                            End If
                        Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '    _en_id = riu_en_id.EditValue
                        '    _si_id = ds_edit.Tables(0).Rows(i).Item("riud_si_id")
                        '    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '    _qty = ds_edit.Tables(0).Rows(i).Item("riud_qty")
                        '    _cost = ds_edit.Tables(0).Rows(i).Item("riud_cost")
                        '    If _cost_methode = "F" Or _cost_methode = "L" Then
                        '        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '        Return False
                        '        'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '        '    sqlTran.Rollback()
                        '        '    insert = False
                        '        '    Exit Function
                        '        'End If
                        '    ElseIf _cost_methode = "A" Then
                        '        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '            sqlTran.Rollback()
                        '            insert = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next
                        'MessageBox.Show(ds_edit.Tables(0).Rows(0).Item("riud_qty_real").ToString)
                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If insert_glt_det_inv_iss(ssqls, objinsert, ds_edit, _
                                                 riu_en_id.EditValue, riu_en_id.GetColumnValue("en_code"), _
                                                 _riu_oid.ToString, _riu_type2, _
                                                 riu_date.DateTime, _
                                                 "IC", "ISS-UNP") = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Inv Issue " & _riu_type2)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        'Dim ssql1 As String = "select * from pb_mstr where pb_code=" & SetSetring(riu_ref_pb_code.Text)
                        'Dim _riu_ref_pb_code As String = ""

                        'Dim dt_reff As New DataTable
                        'dt_reff = GetTableData(ssql1)

                        'For Each dr_reff As DataRow In dt_reff.Rows
                        '    _riu_ref_pb_code = dr_reff("pb_reff_code")
                        'Next

                        Dim jsonstring As String

                        jsonstring = "{""pb_inv_issue_code"":""" & _riu_type2 _
                           & """,""pb_code"":""" & riu_ref_pb_code.Text & """}"

                        jsonstring = jsonstring.Replace(" ", "%20")

                        'Dim url As String = func_coll.get_http_server_api() & "php56/json/curl_api_sdi.php?url=" _
                        '& "https://10.10.10.148:5001/api/inventory_request/" & riu_ref_pb_code.Text & "/UpdateInventoryRequest" & "&json_string=" & jsonstring



                        Dim _app As String = ""
                        If func_coll.get_conf_file("syspro_approval_code") = "DUTA" Then
                            _app = "SDI"
                        Else
                            _app = "DUTA"
                        End If
                        Dim url As String = func_coll.get_http_server_api() & "php56/json/curl_api_sdi.php?app=" _
                            & _app & "&path=inventory_request/" & riu_ref_pb_code.Text & "/UpdateInventoryRequest&json_string=" & jsonstring


                        Dim result As String
                        result = mf.run_get_to_api(url & "")
                        Dim _pb_code_reff As String = ""

                        If Not SetString(result).Contains("\""success\"":true") Then
                            Dim result2 As JObject = JObject.Parse(result.ToString)

                            'If SetString(result2.GetValue("message").ToString()) <> "" Then
                            '    Dim _data As JArray = JArray.Parse(result2.GetValue("data").ToString())

                            '    For Each item As JObject In _data
                            '        _pb_code_reff = item.GetValue("pb_code").ToString()
                            '    Next
                            'End If
                            MsgBox(result2.GetValue("message").ToString())


                            sqlTran.Rollback()
                            insert = False
                            Exit Function

                        End If


                        If riu_ref_pb_code.EditValue <> "" And func_coll.get_conf_file("transfer_issue_limit_entity_shipment") = "1" Then

                            Dim _telegram_id As String = ""
                            Dim ssql As String
                            ssql = "select * from tconfuser x where x.pin='LOG' and x.userid in " _
                                & "(select z.userid from tconfuserentity z  where  z.user_en_id = " & SetInteger(_en_id_reff) & ")"

                            Dim dt_telegram As New DataTable
                            dt_telegram = GetTableData(ssql)

                            For Each dr_tele As DataRow In dt_telegram.Rows
                                _telegram_id = dr_tele("user_id_telegram")
                            Next

                            Dim _alamat_api As String = "" & func_coll.get_http_server_api() & "php72/api_iot/kirim_id.php?id=" & _telegram_id _
                            & "&pesan=" & "Mohon diproses Inventory receipt : " & SetStringJson(riu_remarks.EditValue) & " entitas " _
                            & _en_desc_reff & " diproses dengan Inventory Issue : " & _riu_type2 & " dari entitas " & riu_en_id.GetColumnValue("en_desc")

                            Dim result2 As String
                            result2 = mf.run_get_to_api(_alamat_api & "")

                            If _telegram_id <> "" Then
                                If SetString(result2).Contains("success") Then
                                    '_MakeReport("Data empty")

                                Else
                                    Box("Gagal notif telegram")
                                    Exit Function
                                End If
                            End If

                        End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE riu_mstr set riu_status_reject_web='Y' where riu_oid=" & SetSetring(_riu_oid.ToString())
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
                        set_row(_riu_oid.ToString, "riu_oid")
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

    Private Function insert_glt_det_inv_iss(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean
        insert_glt_det_inv_iss = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("riud_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id"))
                        dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                        _cost = par_ds.Tables(0).Rows(i).Item("riud_qty") * par_ds.Tables(0).Rows(i).Item("riud_cost")

                        'Insert Untuk Yang credit Dulu
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
                                            & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                            & SetDbl(1) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring("Inventory Issue") & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If
                        '********************** finish untuk yang debet

                        'Insert Untuk Debet nya....
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
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                            & SetDbl(1) & ",  " _
                                            & SetInteger(_seq + 1) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("riud_ac_id")) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("riud_sb_id")) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("riud_cc_id")) & ",  " _
                                            & SetSetringDB("Inventory Issue") & ",  " _
                                            & SetDblDB(_cost) & ",  " _
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

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         par_ds.Tables(0).Rows(i).Item("riud_ac_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("riud_sb_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("riud_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function
#End Region

    Private Sub riu_ref_pb_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riu_ref_pb_code.ButtonClick
        Dim frm As New FInventoryRequestSearch
        frm.set_win(Me)
        frm._obj = riu_ref_pb_code
        frm._en_id = riu_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_en_id")
        _type = 16
        _table = "riu_mstr"
        _initial = "riu"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_type2")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_type2")

        insert_tranaprvd_det_local(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  riu_mstr.riu_oid, " _
            & "  riu_mstr.riu_dom_id, " _
            & "  riu_mstr.riu_en_id, " _
            & "  riu_mstr.riu_add_by, " _
            & "  riu_mstr.riu_add_date, " _
            & "  riu_mstr.riu_upd_by, " _
            & "  riu_mstr.riu_upd_date, " _
            & "  riu_mstr.riu_type2, " _
            & "  riu_mstr.riu_date, " _
            & "  riu_mstr.riu_type, " _
            & "  riu_mstr.riu_remarks, " _
            & "  riu_mstr.riu_dt, " _
            & "  riu_mstr.riu_ref_so_code, " _
            & "  riu_mstr.riu_ref_so_oid, " _
            & "  riu_mstr.riu_ref_pb_oid, " _
            & "  riu_mstr.riu_ref_pb_code, " _
            & "  riud_det.riud_oid, " _
            & "  riud_det.riud_riu_oid, " _
            & "  riud_det.riud_pt_id, " _
            & "  riud_det.riud_qty * -1 as riud_qty, " _
            & "  riud_det.riud_um, " _
            & "  riud_det.riud_um_conv, " _
            & "  riud_det.riud_qty_real * -1 as riud_qty_real, " _
            & "  riud_det.riud_si_id, " _
            & "  riud_det.riud_loc_id, " _
            & "  riud_det.riud_lot_serial, " _
            & "  riud_det.riud_cost, " _
            & "  riud_det.riud_ac_id, " _
            & "  riud_det.riud_sb_id, " _
            & "  riud_det.riud_cc_id, " _
            & "  riud_det.riud_dt, " _
            & "  riud_det.riud_sod_oid, " _
            & "  riud_det.riud_pbd_oid, " _
            & "  loc_mstr.loc_desc, " _
            & "  si_mstr.si_desc, " _
            & "  pt_mstr.pt_code, " _
            & "  pt_mstr.pt_desc1, " _
            & "  pt_mstr.pt_desc2, " _
            & "  ltrim(coalesce(pt_desc1,'') || '' || coalesce(pt_desc2,'')) as pt_descriptions, " _
            & "  code_mstr.code_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM " _
            & "  riu_mstr " _
            & "  INNER JOIN riud_det ON (riu_mstr.riu_oid = riud_det.riud_riu_oid) " _
            & "  INNER JOIN si_mstr ON (riud_det.riud_si_id = si_mstr.si_id) " _
            & "  INNER JOIN loc_mstr ON (riud_det.riud_loc_id = loc_mstr.loc_id) " _
            & "  INNER JOIN pt_mstr ON (riud_det.riud_pt_id = pt_mstr.pt_id) " _
            & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id)" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = riu_oid  " _
            & "  where riu_type2 ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_type2") + "'" _
            & " and riu_type ~~* 'I'" _
            & "  order by riu_type2 "

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvIssue"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("riu_type2")
        frm.ShowDialog()

    End Sub

    Private Function insert_tranaprvd_det_local(ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_table As String, ByVal par_initial As String, ByVal par_tran_code_awal As String, ByVal par_tran_code_akhir As String, ByVal par_date As Date) As Boolean
        insert_tranaprvd_det_local = True

        Dim i As Integer
        Dim dt_bantu As DataTable = New DataTable
        Dim dt_data As DataTable = New DataTable

        dt_bantu = (func_data.load_list_aprvd_dok(par_en_id, par_type, par_date))
        dt_data = (load_list_aprvd_data_local(par_en_id, par_tran_code_awal, par_tran_code_akhir, par_initial, par_table))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To dt_data.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from tranaprvd_dok where tranaprvd_tran_oid = '" + dt_data.Rows(i).Item("data_oid").ToString + "'"
                            'par_ssqls.Add(.Command.CommandText) gak perlu karena tidak penting..untuk disinkronisasikan
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tranaprvd_dok " _
                                                & "( " _
                                                & "  tranaprvd_oid, " _
                                                & "  tranaprvd_dom_id, " _
                                                & "  tranaprvd_en_id, " _
                                                & "  tranaprvd_add_by, " _
                                                & "  tranaprvd_add_date, " _
                                                & "  tranaprvd_dt, " _
                                                & "  tranaprvd_tran_oid, " _
                                                & "  tranaprvd_tran_code, " _
                                                & "  tranaprvd_name_1, " _
                                                & "  tranaprvd_pos_1, " _
                                                & "  tranaprvd_name_2, " _
                                                & "  tranaprvd_pos_2, " _
                                                & "  tranaprvd_name_3, " _
                                                & "  tranaprvd_pos_3, " _
                                                & "  tranaprvd_name_4, " _
                                                & "  tranaprvd_pos_4 " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(par_en_id) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & SetSetring(dt_data.Rows(i).Item("data_oid").ToString) & ",  " _
                                                & SetSetring(dt_data.Rows(i).Item("data_code")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_1")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_1")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_2")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_2")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_3")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_3")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_4")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_4")) & "  " _
                                                & ")"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next
                        sqlTran.Commit()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function load_list_aprvd_data_local(ByVal par_en_id As Integer, ByVal par_awal As String, ByVal par_akhir As String, ByVal par_initial As String, ByVal par_table As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & par_initial & "_oid as data_oid, " _
                        & par_initial & "_type2 as data_code " _
                        & "FROM  " _
                        & "  public." + par_table + " where " _
                        & par_initial & "_type2 >= '" + par_awal + "' and " _
                        & par_initial & "_type2 <= '" + par_akhir + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            gv_edit.UpdateCurrentRow()
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "si_desc" Then

                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("si_desc") = gv_edit.GetRowCellValue(_row, "si_desc")
                    ds_edit.Tables(0).Rows(i).Item("riud_si_id") = gv_edit.GetRowCellValue(_row, "riud_si_id")
                Next

            ElseIf _col = "loc_desc" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
                    ds_edit.Tables(0).Rows(i).Item("riud_loc_id") = gv_edit.GetRowCellValue(_row, "riud_loc_id")
                Next
            ElseIf _col = "ac_code" Or _col = "ac_name" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("riud_ac_id") = gv_edit.GetRowCellValue(_row, "riud_ac_id")
                    ds_edit.Tables(0).Rows(i).Item("ac_code") = gv_edit.GetRowCellValue(_row, "ac_code")
                    ds_edit.Tables(0).Rows(i).Item("ac_name") = gv_edit.GetRowCellValue(_row, "ac_name")
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
                & "  a.riu_type2 as code, " _
                & "  a.riu_date, " _
                & "  a.riu_type, " _
                & "  a.riu_remarks, " _
                & "  a.riu_ref_so_code, " _
                & "  a.riu_ref_so_oid, " _
                & "  a.riu_ref_pb_oid, " _
                & "  a.riu_ref_pb_code, " _
                & "  b.en_desc as entity, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  d.pt_desc2, " _
                & "  e.code_name as unit_measure, " _
                & "  c.riud_qty * -1 as qty, " _
                & "  f.si_desc as site, " _
                & "  g.loc_desc as location " _
                & "FROM " _
                & "  public.riu_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.riu_en_id = b.en_id) " _
                & "  INNER JOIN public.riud_det c ON (a.riu_oid = c.riud_riu_oid) " _
                & "  INNER JOIN public.pt_mstr d ON (c.riud_pt_id = d.pt_id) " _
                & "  INNER JOIN public.code_mstr e ON (c.riud_um = e.code_id) " _
                & "  INNER JOIN public.si_mstr f ON (c.riud_si_id = f.si_id) " _
                & "  INNER JOIN public.loc_mstr g ON (c.riud_loc_id = g.loc_id) " _
                & " where riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and riu_type ~~* 'I'" _
                & " and riu_en_id in (select user_en_id from tconfuserentity " _
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
