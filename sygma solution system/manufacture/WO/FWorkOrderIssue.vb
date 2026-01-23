Imports npgsql
Imports master_new.ModFunction

Public Class FWorkOrderIssue

    Public dt_bantu As DataTable
    Public dt_wod_temp As New DataTable
    Dim dr As DataRow
    Public dr_print As DataRow
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _wo_oid_mstr As String
    Public _wo_id As String
    Public _wocid_en_id As String
    Dim _wo_oid_show As String
    Public ds_edit, ds_serial As DataSet
    Public ds_show As DataSet
    Public ds_show_det As DataSet
    Dim ds_wod_temp As DataSet
    Public dt_print As New DataTable
    Dim ds_warehouse As DataSet
    Dim dt_warehouse As DataTable
    Public _pjc_id As Integer

    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _wod_related_oid As String = ""
    Dim _conf_value As String

    Private Sub FWorkOrderIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        woci_date.DateTime = Now
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        _conf_value = func_coll.get_conf_file("wo_issue_to_wip")

        If _conf_value = "1" Then
            lci_work_center.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lci_work_center.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Public Overrides Sub format_grid()
        _conf_value = func_coll.get_conf_file("wo_issue_to_wip")

        add_column(gv_master, "woci_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Issue Code", "woci_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "woci_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO Ref.", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Request", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Unplan", "woci_unplan", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "User Create", "woci_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Date Create", "woci_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Update", "woci_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Date Update", "woci_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        'woci_unplan
        If _conf_value = "1" Then
            add_column_copy(gv_master, "Work Center To", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        End If
        add_column_copy(gv_master, "Route Desc.", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Order", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Qty Alc. Issued", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Qty Pick Issued", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "wocid_oid", False)
        add_column(gv_detail, "wocid_woci_oid", False)
        add_column(gv_detail, "wocid_wod_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc1_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM", "code_name_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Operation", "wocid_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Issued", "wod_qty_issued", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "wocid_pt_subs_id", False)


        add_column_copy(gv_detail, "Subt. Code", "pt_code_subs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Subt. Desc1", "pt_desc1_subs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Subt. Desc2", "pt_desc2_subs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Subt", "code_name_subs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "wocid_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Complement", "wocid_qty_comp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Standard Cost", "wocid_cost_std", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Current Cost", "wocid_cost_current", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Current Cost", "wocid_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Type", "wocid_loc_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location / Work Center from", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Project from", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        If _conf_value = "1" Then
            add_column_copy(gv_detail, "Work Center To", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        End If
        add_column_copy(gv_detail, "Lot Serial", "wocid_lot_serial", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_serial, "wocid_woci_oid", False)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "wocids_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "wocids_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "wod_oid", False)
        add_column(gv_edit, "wod_pt_bom_id", False)
        add_column(gv_edit, "wocid_pbd_oid", False)
        add_column_copy(gv_edit, "Part Number", "pt_code_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Description1", "pt_desc1_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Description2", "pt_desc2_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "UM", "code_name_wod", DevExpress.Utils.HorzAlignment.Default)
        ' add_column_copy(gv_edit, "Operation", "wocid_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Qty Open", "wocid_qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Issued", "wocid_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "wocid_pt_subs_id", False)
        'add_column(gv_edit, "Subt. Code", "pt_code_subs", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_edit, "Subt. Desc1", "pt_desc1_subs", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_edit, "Subt. Desc2", "pt_desc2_subs", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_edit, "Subt. UM", "code_name_subs", DevExpress.Utils.HorzAlignment.Default)

        'add_column_edit(gv_edit, "Qty Complement", "wocid_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Cost", "wocid_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "Current Cost", "wocid_cost_current", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "wocid_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "wocid_loc_id", False)
        add_column(gv_edit, "wocid_loc_type", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_edit, "wocid_pjc_id", False)
        'add_column(gv_edit, "Project from", "pjc_code", DevExpress.Utils.HorzAlignment.Default)

        'If _conf_value = "1" Then
        '    add_column(gv_edit, "wocid_wc_id", False)
        '    add_column(gv_edit, "Work Center To", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'End If
        add_column_edit(gv_edit, "Lot Serial", "wocid_lot_serial", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_serial, "wocids_oid", False)
        add_column(gv_serial, "wocids_wocid_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "wocids_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "wocids_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "wocids_si_id", False)
        add_column(gv_serial, "wocids_loc_id", False)
    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.woci_mstr.woci_oid, " _
                    & "  public.woci_mstr.woci_dom_id, " _
                    & "  public.woci_mstr.woci_en_id, " _
                    & "  public.woci_mstr.woci_code, " _
                    & "  public.woci_mstr.woci_wo_id, " _
                    & "  public.woci_mstr.woci_op, " _
                    & "  public.woci_mstr.woci_date, " _
                    & "  public.woci_mstr.woci_issue_alloc, " _
                    & "  public.woci_mstr.woci_issue_picked,coalesce(woci_unplan,'N') as woci_unplan, " _
                    & "  public.woci_mstr.woci_remarks, " _
                    & "  public.woci_mstr.woci_dt,woci_si_id,si_desc, " _
                    & "  public.wo_mstr.wo_code, " _
                    & "  public.wo_mstr.wo_pt_id, " _
                    & "  public.wo_mstr.wo_qty_ord, " _
                    & "  public.wo_mstr.wo_qty_rjc, " _
                    & "  public.wo_mstr.wo_ro_id, " _
                    & "  public.wo_mstr.wo_insheet_pct, " _
                    & "  public.wo_mstr.wo_rel_date, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.ro_mstr.ro_id, " _
                    & "  public.ro_mstr.ro_desc,woci_wc_id,wc_desc,woci_pb_oid,pb_code " _
                    & "FROM " _
                    & "  public.woci_mstr " _
                    & "  INNER JOIN public.wo_mstr ON (public.woci_mstr.woci_wo_id = public.wo_mstr.wo_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id)" _
                    & "  INNER JOIN public.en_mstr ON (public.en_mstr.en_id = public.woci_mstr.woci_en_id)" _
                    & "  INNER JOIN public.si_mstr ON (woci_mstr.woci_si_id = public.si_mstr.si_id)" _
                    & "  LEFT OUTER JOIN public.wc_mstr ON (woci_mstr.woci_wc_id = public.wc_mstr.wc_id)" _
                    & "  LEFT OUTER JOIN public.pb_mstr ON (woci_mstr.woci_pb_oid = pb_oid)" _
                    & "  WHERE woci_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and woci_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and wo_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        woci_en_id.Properties.DataSource = dt_bantu
        woci_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        woci_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        woci_en_id.ItemIndex = 0
    End Sub


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
            & "   wocid_det.wocid_oid, " _
            & "   wocid_det.wocid_woci_oid, " _
            & "   wocid_det.wocid_wod_oid, " _
            & "   wocid_det.wocid_seq, " _
            & "   wocid_det.wocid_op, " _
            & "   wocid_det.wocid_qty, wocid_det.wocid_qty_comp, " _
            & "   wocid_det.wocid_substitute, " _
            & "   wocid_det.wocid_pt_subs_id, " _
            & "   wocid_det.wocid_si_id, " _
            & "   wocid_det.wocid_loc_id, " _
            & "   wocid_det.wocid_lot_serial, " _
            & "   wocid_det.wocid_dt, " _
            & "   woci_mstr.woci_oid, " _
            & "   wod_det.wod_qty_req, " _
            & "   wod_det.wod_pt_bom_id, " _
            & "   wod_det.wod_comp, " _
            & "   wod_det.wod_qty_issued, " _
            & "   pt_mstr_wod.pt_code as pt_code_wod, " _
            & "   pt_mstr_wod.pt_desc1 as pt_desc1_wod, " _
            & "   pt_mstr_wod.pt_desc2 as pt_desc2_wod, " _
            & "   code_mstr_wod.code_name as code_name_wod, " _
            & "   pt_mstr_subs.pt_code as pt_code_subs, " _
            & "   pt_mstr_subs.pt_desc1 as pt_desc1_subs, " _
            & "   pt_mstr_subs.pt_desc2 as pt_desc2_subs, " _
            & "   code_mstr_subs.code_name as code_name_subs, " _
            & "   si_mstr.si_desc, " _
            & "   wocid_cost " _
            & "   " _
            & "      " _
            & "FROM " _
            & "   wocid_det " _
            & "  INNER JOIN  woci_mstr ON ( wocid_det.wocid_woci_oid =  woci_mstr.woci_oid) " _
            & "  INNER JOIN  wod_det ON ( wocid_det.wocid_wod_oid =  wod_det.wod_oid) " _
            & "  INNER JOIN  pt_mstr pt_mstr_wod ON ( wod_det.wod_pt_bom_id =  pt_mstr_wod.pt_id) " _
            & "  LEFT OUTER JOIN  pt_mstr pt_mstr_subs ON ( wocid_det.wocid_pt_subs_id =  pt_mstr_subs.pt_id) " _
            & "  INNER JOIN  code_mstr code_mstr_wod ON ( pt_mstr_wod.pt_um =  code_mstr_wod.code_id) " _
            & "  LEFT OUTER JOIN  code_mstr code_mstr_subs ON ( pt_mstr_subs.pt_um =  code_mstr_subs.code_id) " _
            & "  INNER JOIN  si_mstr ON ( wocid_det.wocid_si_id =  si_mstr.si_id) " _
            & "  where woci_mstr.woci_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and woci_mstr.woci_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " union all " _
            & "SELECT  " _
            & "   wocid_det.wocid_oid, " _
            & "   wocid_det.wocid_woci_oid, " _
            & "   wocid_det.wocid_wod_oid, " _
            & "   wocid_det.wocid_seq, " _
            & "   wocid_det.wocid_op, " _
            & "   wocid_det.wocid_qty, wocid_det.wocid_qty_comp, " _
            & "   wocid_det.wocid_substitute, " _
            & "   wocid_det.wocid_pt_subs_id, " _
            & "   wocid_det.wocid_si_id, " _
            & "   wocid_det.wocid_loc_id, " _
            & "   wocid_det.wocid_lot_serial, " _
            & "   wocid_det.wocid_dt, " _
            & "   woci_mstr.woci_oid, " _
            & "   0.0 as wod_qty_req, " _
            & "   wocid_det.wocid_pt_subs_id wod_pt_bom_id, " _
            & "   '' wod_comp, " _
            & "   0.0 wod_qty_issued, " _
            & "   pt_mstr_wod.pt_code as pt_code_wod, " _
            & "   pt_mstr_wod.pt_desc1 as pt_desc1_wod, " _
            & "   pt_mstr_wod.pt_desc2 as pt_desc2_wod, " _
            & "   code_mstr_wod.code_name as code_name_wod, " _
            & "   '' as pt_code_subs, " _
            & "   '' as pt_desc1_subs, " _
            & "   '' as pt_desc2_subs, " _
            & "   '' as code_name_subs, " _
            & "   si_mstr.si_desc, " _
            & "   wocid_cost " _
            & "   " _
            & "      " _
            & "FROM " _
            & "   wocid_det " _
            & "  INNER JOIN  woci_mstr ON ( wocid_det.wocid_woci_oid =  woci_mstr.woci_oid) " _
            & "  INNER  JOIN  pt_mstr pt_mstr_wod ON ( wocid_det.wocid_pt_subs_id =  pt_mstr_wod.pt_id) " _
            & "  INNER JOIN  code_mstr code_mstr_wod ON ( pt_mstr_wod.pt_um =  code_mstr_wod.code_id) " _
            & "  INNER JOIN  si_mstr ON ( wocid_det.wocid_si_id =  si_mstr.si_id) " _
            & "  where woci_mstr.woci_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and woci_mstr.woci_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  order by pt_code_wod "

        load_data_detail(sql, gc_detail, "detail")

        '& "  INNER JOIN  loc_mstr ON ( wocid_det.wocid_loc_id =  loc_mstr.loc_id)" _
        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  public.wocids_serial.wocids_oid, " _
            & "  public.wocids_serial.wocids_wocid_oid, " _
            & "  public.wocids_serial.wocids_seq, " _
            & "  public.wocids_serial.wocids_qty, " _
            & "  public.wocids_serial.wocids_si_id, " _
            & "  public.wocids_serial.wocids_loc_id, " _
            & "  public.wocids_serial.wocids_lot_serial, " _
            & "  public.wocids_serial.wocids_dt, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.loc_mstr.loc_desc, " _
            & "  public.woci_mstr.woci_oid " _
            & "FROM " _
            & "  public.wocids_serial " _
            & "  INNER JOIN public.wocid_det ON (public.wocids_serial.wocids_wocid_oid = public.wocid_det.wocid_oid) " _
            & "  INNER JOIN public.woci_mstr ON (public.wocid_det.wocid_woci_oid = public.woci_mstr.woci_oid) " _
            & "  INNER JOIN public.loc_mstr ON (public.wocids_serial.wocids_loc_id = public.loc_mstr.loc_id) " _
            & "  INNER JOIN public.si_mstr ON (public.wocids_serial.wocids_si_id = public.si_mstr.si_id)" _
            & "  where woci_mstr.woci_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and woci_mstr.woci_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_serial, "detail_serial")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("wocid_woci_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wocid_woci_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("woci_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_serial.Columns("woci_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[woci_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("woci_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub


#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        wo_code.Focus()
        wo_code.Text = ""
        wo_en_id.Text = ""
        wo_si_id.Text = ""
        pt_code.Text = ""
        wo_qty_ord.Text = ""
        wo_insheet_pct.Text = ""
        wo_ord_date.Text = ""
        wo_due_date.Text = ""
        wo_ro_id.Text = ""
        wo_remarks.Text = ""
        woci_en_id.ItemIndex = 0
        woci_wc_id.ItemIndex = 0

        'ant 5 nov 2011
        te_serial_code.Text = ""
        te_serial_from.Text = ""
        te_serial_to.Text = ""
        '----------------------------

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "   wocid_det.wocid_oid, " _
                            & "   wocid_det.wocid_woci_oid, " _
                            & "   wocid_det.wocid_wod_oid, " _
                            & "   wocid_det.wocid_seq, " _
                            & "   wocid_det.wocid_op,0.0 as wocid_qty_open, " _
                            & "   wocid_det.wocid_qty, wocid_det.wocid_qty_comp, " _
                            & "   wocid_det.wocid_substitute, " _
                            & "   wocid_det.wocid_pt_subs_id, " _
                            & "   wocid_det.wocid_si_id, " _
                            & "   wocid_det.wocid_loc_id, " _
                            & "   wocid_det.wocid_lot_serial, " _
                            & "   wocid_det.wocid_dt, " _
                            & "   woci_mstr.woci_oid,wocid_cost, " _
                            & "   wod_det.wod_qty_req, " _
                            & "   wod_det.wod_pt_bom_id, " _
                            & "   wod_det.wod_comp, " _
                            & "   wod_det.wod_qty_issued,wocid_pbd_oid, " _
                            & "    " _
                            & "   pt_mstr_wod.pt_id as pt_id_wod, " _
                            & "   pt_mstr_wod.pt_code as pt_code_wod, " _
                            & "   pt_mstr_wod.pt_desc1 as pt_desc1_wod, " _
                            & "   pt_mstr_wod.pt_desc2 as pt_desc2_wod, " _
                            & "   pt_mstr_wod.pt_type as pt_type_wod, " _
                            & "   pt_mstr_wod.pt_ls as pt_ls_wod, " _
                            & "   pt_mstr_wod.pt_cost as pt_cost_wod, " _
                            & "   code_mstr_wod.code_name as code_name_wod, " _
                            & "   pt_mstr_subs.pt_code as pt_code_subs, " _
                            & "   pt_mstr_subs.pt_desc1 as pt_desc1_subs, " _
                            & "   pt_mstr_subs.pt_desc2 as pt_desc2_subs, " _
                            & "   pt_mstr_subs.pt_type as pt_type_subs, " _
                            & "   pt_mstr_subs.pt_ls as pt_ls_subs, " _
                            & "   pt_mstr_subs.pt_cost as pt_cost_subs, " _
                            & "   code_mstr_subs.code_name as code_name_subs, " _
                            & "   si_mstr.si_desc, " _
                            & "   loc_mstr.loc_desc,wc_desc,wocid_wc_id " _
                            & " FROM " _
                            & "   wocid_det " _
                            & "  INNER JOIN  woci_mstr ON ( wocid_det.wocid_woci_oid =  woci_mstr.woci_oid) " _
                            & "  INNER JOIN  wod_det ON ( wocid_det.wocid_wod_oid =  wod_det.wod_oid) " _
                            & "  INNER JOIN  pt_mstr pt_mstr_wod ON ( wod_det.wod_pt_bom_id = pt_mstr_wod.pt_id) " _
                            & "  LEFT OUTER JOIN  pt_mstr pt_mstr_subs ON ( wocid_det.wocid_pt_subs_id = pt_mstr_subs.pt_id) " _
                            & "  INNER JOIN  code_mstr code_mstr_wod ON (pt_mstr_wod.pt_um = code_mstr_wod.code_id) " _
                            & "  LEFT OUTER JOIN  code_mstr code_mstr_subs ON (pt_mstr_subs.pt_um = code_mstr_subs.code_id) " _
                             & "  LEFT OUTER JOIN  wc_mstr ON (wc_id = wocid_wc_id) " _
                            & "  INNER JOIN  si_mstr ON ( wocid_det.wocid_si_id =  si_mstr.si_id) " _
                            & "  INNER JOIN  loc_mstr ON ( wocid_det.wocid_loc_id =  loc_mstr.loc_id)" _
                            & "  where wocid_seq = -3213 "

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "wocid_det")

                    gc_edit.DataSource = ds_edit.Tables("wocid_det")
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
                        & "  wocids_oid, " _
                        & "  wocids_wocid_oid, " _
                        & "  wocids_qty, " _
                        & "  wocids_si_id, " _
                        & "  wocids_loc_id, " _
                        & "  wocids_lot_serial " _
                        & "FROM  " _
                        & "  public.wocids_serial " _
                        & " where wocids_si_id = -99"

                    .InitializeCommand()
                    .FillDataSet(ds_serial, "serial")
                    gc_serial.DataSource = ds_serial.Tables(0)
                    gv_serial.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        ds_edit.AcceptChanges()
        Dim i, j As Integer
        Dim _qty, _qty_ttl_serial As Double

        _qty = 0.0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _qty = _qty + ds_edit.Tables(0).Rows(i).Item("wocid_qty")
        Next

        If _qty = 0.0 Then
            MessageBox.Show("Data Detail Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If IsDBNull(pt_code.EditValue) Then
            MessageBox.Show("Data cannot null...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai qty lebih dari 1
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("wocid_pt_subs_id").ToString) Then
            If ds_edit.Tables(0).Rows(i).Item("pt_ls_wod").ToString.ToUpper = "S" Then
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("wod_oid").ToString = ds_serial.Tables(0).Rows(j).Item("wods_wod_oid").ToString Then
                        If ds_serial.Tables(0).Rows(j).Item("wods_qty") > 1 Then
                            MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code_wod") + " Have Wrong Serial Qty Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            BindingContext(ds_edit.Tables(0)).Position = i
                            Return False
                        End If
                    End If
                Next
            End If
            'Else
            '    If ds_edit.Tables(0).Rows(i).Item("pt_ls_subs").ToString.ToUpper = "S" Then
            '        For j = 0 To ds_serial.Tables(0).Rows.Count - 1
            '            If ds_edit.Tables(0).Rows(i).Item("wod_oid") = ds_serial.Tables(0).Rows(j).Item("wods_wod_oid") Then
            '                If ds_serial.Tables(0).Rows(j).Item("wods_qty") > 1 Then
            '                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code_subs") + " Have Wrong Serial Qty Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '                    BindingContext(ds_edit.Tables(0)).Position = i
            '                    Return False
            '                End If
            '            End If
            '        Next
            '    End If
            'End If

        Next
        '***********************************************************************


        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("wocid_pt_subs_id").ToString) Then
            If ds_edit.Tables(0).Rows(i).Item("pt_ls_wod").ToString.ToUpper = "S" Then
                _qty = ds_edit.Tables(0).Rows(i).Item("wocid_qty")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("wocid_oid").ToString = ds_serial.Tables(0).Rows(j).Item("wocids_wocid_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + 1
                    End If
                Next
                If _qty <> _qty_ttl_serial Then
                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code_wod") + " Have Wrong Serial Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
            'Else
            'If ds_edit.Tables(0).Rows(i).Item("pt_ls_subs").ToString.ToUpper = "S" Then
            '    _qty = ds_edit.Tables(0).Rows(i).Item("wocid_qty")
            '    _qty_ttl_serial = 0
            '    For j = 0 To ds_serial.Tables(0).Rows.Count - 1
            '        If ds_edit.Tables(0).Rows(i).Item("wocid_oid") = ds_serial.Tables(0).Rows(j).Item("wocids_wocid_oid") Then
            '            _qty_ttl_serial = _qty_ttl_serial + 1
            '        End If
            '    Next
            '    If _qty <> _qty_ttl_serial Then
            '        MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_cod_subse") + " Have Wrong Serial Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        BindingContext(ds_edit.Tables(0)).Position = i
            '        Return False
            '    End If
            'End If
            'End If
        Next
        '***********************************************************************

        '***********************************************************************
        'Mencari apakah receive barang yang Lot mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("wocid_pt_subs_id").ToString) Then
            If ds_edit.Tables(0).Rows(i).Item("pt_ls_wod").ToString.ToUpper = "L" Then
                _qty = ds_edit.Tables(0).Rows(i).Item("wocid_qty")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("wocid_oid").ToString = ds_serial.Tables(0).Rows(j).Item("wocids_wocid_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("wocids_qty")
                    End If
                Next
                If _qty <> _qty_ttl_serial Then
                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code_wod") + " Have Wrong Lot Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
            'Else
            'If ds_edit.Tables(0).Rows(i).Item("pt_ls_subs").ToString.ToUpper = "L" Then
            '    _qty = ds_edit.Tables(0).Rows(i).Item("wocid_qty")
            '    _qty_ttl_serial = 0
            '    For j = 0 To ds_serial.Tables(0).Rows.Count - 1
            '        If ds_edit.Tables(0).Rows(i).Item("wocid_oid") = ds_serial.Tables(0).Rows(j).Item("wocids_wocid_oid") Then
            '            _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("wocids_qty")
            '        End If
            '    Next
            '    If _qty <> _qty_ttl_serial Then
            '        MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code_subs") + " Have Wrong Lot Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        BindingContext(ds_edit.Tables(0)).Position = i
            '        Return False
            '    End If
            'End If
            'End If
        Next
        '***********************************************************************

        'If _conf_value = "1" Then
        '    With ds_edit.Tables(0)
        '        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '            If SetString(.Rows(i).Item("wocid_wc_id")) = "" Then
        '                Box("Work Center detail can't null")
        '                Return False
        '            End If
        '        Next
        '    End With
        'End If

        '================= cek inv untuk change allocation =================
        Dim sSQL As String
        Dim dt As New DataTable


        'For i = 0 To ds_edit.Tables("wocid_det").Rows.Count - 1
        '    With ds_edit.Tables("wocid_det").Rows(i)

        '        If SetNumber(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pjc_id")) <> SetNumber(wo_so_oid.Tag) Then

        '            If func_coll.cek_inv(woci_en_id.EditValue, woci_si_id.EditValue, ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_id"), _
        '                                ds_edit.Tables("wocid_det").Rows(i).Item("wod_pt_bom_id"), ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pjc_id"), "''") = False Then

        '                sSQL = "INSERT INTO  " _
        '                    & "  public.invc_mstr " _
        '                    & "( " _
        '                    & "  invc_oid, " _
        '                    & "  invc_dom_id, " _
        '                    & "  invc_en_id, " _
        '                    & "  invc_si_id, " _
        '                    & "  invc_loc_id, " _
        '                    & "  invc_pt_id,invc_pjc_id, " _
        '                    & "  invc_qty, " _
        '                    & "  invc_serial " _
        '                    & ")  " _
        '                    & "VALUES ( " _
        '                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                    & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
        '                    & SetInteger(woci_en_id.EditValue) & ",  " _
        '                    & SetInteger(woci_si_id.EditValue) & ",  " _
        '                    & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_id")) & ",  " _
        '                    & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wod_pt_bom_id")) & ",  " _
        '                    & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pjc_id")) & ",  " _
        '                    & SetDbl(0) & ",  " _
        '                    & SetSetring("") & "  " _
        '                    & ")"

        '                master_new.PGSqlConn.DbRun(sSQL)

        '            End If

        '        End If


        '    End With
        'Next

        Return before_save
    End Function

    Private Function cek_qty_gudang(ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_value As Double) As Boolean ' jika ada return true
        Try
            Dim sSQL As String

            sSQL = "select coalesce(invc_qty,0) as invc_qty from invc_mstr where invc_en_id=" & par_en_id _
                & " AND invc_si_id=" & par_si_id _
                & " AND invc_loc_id=" & par_loc_id _
                & " AND invc_pt_id=" & par_pt_id

            Dim dt As New DataTable

            dt = master_new.PGSqlConn.GetTableData(sSQL)

            For Each dr As DataRow In dt.Rows
                If dr(0) >= par_value Then
                    Return True
                    Exit Function
                Else
                    Return False
                    Exit Function
                End If
            Next

            Return False
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Public Overrides Function edit_data() As Boolean
        MsgBox("Edit data not available...", MsgBoxStyle.Critical, "Edit disabled..")
        edit_data = False

    End Function
    '================================================================================
    Public Overrides Function edit()
        edit = False
        Return edit
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = False
        MsgBox("Delete is not available...", MsgBoxStyle.Critical, "Delete disabled...")
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = False
        MsgBox("Delete is not available...", MsgBoxStyle.Critical, "Delete disabled...")
        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

    Private Sub wo_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_code.ButtonClick
        ds_edit.Tables("wocid_det").Clear()

        Dim frm As New FWOSearch()
        frm.set_win(Me)
        frm.type_form = True
        frm._en_id = woci_en_id.EditValue
        frm._si_id = woci_si_id.EditValue
        frm.ShowDialog()


    End Sub

    Public Overrides Function insert() As Boolean
        insert = True
        Dim i, x As Integer
        Dim _woci_oid As Guid
        _woci_oid = Guid.NewGuid
        Dim ssqls As New ArrayList
        Dim _serial, _cost_methode, _pt_code, _woci_code As String
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _qty As Double
        Dim i_2, j As Integer

        Dim _total As Double = 0.0

        _cost_methode = ""
        _woci_code = func_coll.get_transaction_number("WI", woci_en_id.GetColumnValue("en_code"), "woci_mstr", "woci_code", func_coll.get_tanggal_sistem)
        x = 0

        ds_edit.AcceptChanges()
        '===untuk cia ===
        Dim sSQL As String
        Dim dt As New DataTable
        Dim _oid_mstr, _ciad_invc_oid As String
        _oid_mstr = Guid.NewGuid.ToString

        Dim _cia_code As String
        Dim _tran_id As Integer

        Dim _pjc_id_from, _pjc_id_to As Integer
        Dim _wo_issue_to_wip As String = func_coll.get_conf_file("wo_issue_to_wip")
        Dim _wc_id As String = ""

        '_cia_code = func_coll.get_transaction_number("CI", woci_en_id.GetColumnValue("en_code"), "cia_mstr", "cia_code", master_new.PGSqlConn.CekTanggal)
        _tran_id = func_coll.get_id_tran_mstr("iss-prv")

        If _wo_issue_to_wip = "0" Then
            _wc_id = "null"
        Else
            _wc_id = woci_wc_id.EditValue
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
                                                & "  public.woci_mstr " _
                                                & "( " _
                                                & "  woci_oid, " _
                                                & "  woci_dom_id, " _
                                                & "  woci_en_id, " _
                                                & "  woci_code, " _
                                                & "  woci_wo_id, " _
                                                & "  woci_date, " _
                                                & "  woci_remarks,woci_unplan, " _
                                                & "  woci_dt,woci_si_id,woci_pb_oid,woci_wc_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_woci_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(woci_en_id.EditValue) & ",  " _
                                                & SetSetring(_woci_code) & ",  " _
                                                & SetInteger(_wo_id.ToString) & ",  " _
                                                & SetDate(woci_date.DateTime) & "," _
                                                & SetSetring(woci_remarks.Text) & ",  " _
                                                & SetBitYN(woci_unplan.EditValue) & ", " _
                                                & "current_timestamp,  " _
                                                & SetInteger(woci_si_id.EditValue) & ",  " _
                                                & SetSetring(woci_pb_oid.Tag) & ",  " _
                                                & SetInteger(_wc_id) _
                                                & ");"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("wocid_det").Rows.Count - 1
                            If ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty") <> 0 Then

                                'If _wo_issue_to_wip = "0" Then
                                '    _wc_id = "null"
                                'Else
                                '    _wc_id = ds_edit.Tables("wocid_det").Rows(i).Item("wocid_wc_id")
                                'End If

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wocid_det " _
                                                & "( " _
                                                & "  wocid_oid, " _
                                                & "  wocid_woci_oid, " _
                                                & "  wocid_wod_oid, " _
                                                & "  wocid_seq, " _
                                                & "  wocid_op, " _
                                                & "  wocid_qty,wocid_qty_comp, " _
                                                & "  wocid_substitute, " _
                                                & "  wocid_pt_subs_id, " _
                                                & "  wocid_si_id, " _
                                                & "  wocid_loc_id, " _
                                                & "  wocid_wc_id,wocid_pbd_oid, " _
                                                & "   " _
                                                & "  wocid_lot_serial,wocid_cost,   " _
                                                & "  wocid_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_woci_oid.ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_wod_oid").ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_op")) & ",  " _
                                                & SetDbl(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty")) & ",  " _
                                                & SetDbl(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty_comp")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_substitute")) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pt_subs_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_id")) & ",  " _
                                                & SetInteger(_wc_id) & ",  " _
                                                & SetSetringDB(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pbd_oid")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_lot_serial")) & ",  " _
                                                & SetDbl(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_cost")) & ",  " _
                                                & " current_timestamp " _
                                                & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                & "  public.wod_det " _
                                                & " SET " _
                                                & "  wod_qty_issued = coalesce(wod_qty_issued,0) + (" & SetDbl(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty")) & ")  " _
                                                & " WHERE wod_oid = " & SetSetring(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_wod_oid").ToString)
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pbd_oid").ToString <> "" Then
                                    'update karena ada hubungan dengan pb_mstr (inventory request)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update pbd_det set pbd_qty_riud = coalesce(pbd_qty_riud,0) +" + SetDbl(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty")) + ", " _
                                                         & " pbd_qty_completed = coalesce(pbd_qty_completed,0) + " + SetDbl(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty")) _
                                                         & " where pbd_oid  = " + SetSetring(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pbd_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update pb_mstr set pb_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & ", pb_status = 'C' " + _
                                                         " where coalesce((select count(pbd_pb_oid) as jml From pbd_det " + _
                                                         " where pbd_qty <> coalesce(pbd_qty_riud,0) " + _
                                                         " and pbd_pb_oid = " & SetSetring(woci_pb_oid.Tag) & " " & _
                                                         " group by pbd_pb_oid),0) = 0 " & _
                                                         " and pb_oid = " & SetSetring(woci_pb_oid.Tag) & " "

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If


                                'proses ke change allocation 
                                'If func_coll.get_conf_file("wo_comp_issue_change_project_allocation") = "1" Then
                                '    _si_id = woci_si_id.EditValue
                                '    _en_id = woci_en_id.EditValue
                                '    _loc_id = ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_id")
                                '    _pt_id = ds_edit.Tables("wocid_det").Rows(i).Item("wod_pt_bom_id")
                                '    _pjc_id_from = SetNumber(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_pjc_id"))
                                '    _pjc_id_to = SetNumber(wo_so_oid.Tag)
                                '    _qty = ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty")
                                '    _cost = ds_edit.Tables("wocid_det").Rows(i).Item("wocid_cost_std")
                                '    _pt_code = ds_edit.Tables("wocid_det").Rows(i).Item("pt_code_wod")
                                '    _serial = "''"

                                '    If _pjc_id_from <> _pjc_id_to Then
                                '        _ciad_invc_oid = func_coll.get_invc_oid(_en_id, _si_id, _loc_id, _pt_id, _pjc_id_from, "''")

                                '        If x = 0 Then
                                '            'insert master ciad

                                '            .Command.CommandType = CommandType.Text
                                '            .Command.CommandText = "INSERT INTO  " _
                                '                            & "  public.cia_mstr " _
                                '                            & "( " _
                                '                            & "  cia_oid, " _
                                '                            & "  cia_dom_id, " _
                                '                            & "  cia_en_id, " _
                                '                            & "  cia_add_by, " _
                                '                            & "  cia_add_date, " _
                                '                            & "  cia_dt, " _
                                '                            & "  cia_si_id, " _
                                '                            & "  cia_code, " _
                                '                            & "  cia_date, " _
                                '                            & "  cia_remark,cia_type " _
                                '                            & ")  " _
                                '                            & "VALUES ( " _
                                '                            & SetSetring(_oid_mstr) & ",  " _
                                '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                '                            & SetInteger(woci_en_id.EditValue) & ",  " _
                                '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                '                            & "current_timestamp" & ",  " _
                                '                            & "current_timestamp" & ",  " _
                                '                            & SetInteger(woci_si_id.EditValue) & ",  " _
                                '                            & SetSetring(_cia_code) & ",  " _
                                '                            & SetDate(master_new.PGSqlConn.CekTanggal) & ",  " _
                                '                            & SetSetring("") & " ,'W' " _
                                '                            & ") "

                                '            .Command.ExecuteNonQuery()
                                '            .Command.Parameters.Clear()

                                '        End If

                                '        .Command.CommandType = CommandType.Text
                                '        .Command.CommandText = "INSERT INTO  " _
                                '                        & "  public.ciad_det " _
                                '                        & "( " _
                                '                        & "  ciad_oid, " _
                                '                        & "  ciad_cia_oid, " _
                                '                        & "  ciad_dt, " _
                                '                        & "  ciad_seq, " _
                                '                        & "  ciad_invc_oid, " _
                                '                        & "  ciad_qty, " _
                                '                        & "  ciad_cost, " _
                                '                        & "  ciad_from_pjc_id, " _
                                '                        & "  ciad_to_pjc_id, " _
                                '                        & "  ciad_remarks " _
                                '                        & ")  " _
                                '                        & "VALUES ( " _
                                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                '                        & SetSetring(_oid_mstr) & ",  " _
                                '                        & "current_timestamp" & ",  " _
                                '                        & SetInteger(x) & ",  " _
                                '                        & SetSetring(_ciad_invc_oid) & ",  " _
                                '                        & SetDec(_qty) & ",  " _
                                '                        & SetDec(_cost) & ",  " _
                                '                        & SetInteger(_pjc_id_from) & ",  " _
                                '                        & SetInteger(_pjc_id_to) & ",  " _
                                '                        & SetSetring("") & "  " _
                                '                        & ")"

                                '        .Command.ExecuteNonQuery()
                                '        .Command.Parameters.Clear()

                                '        If func_coll.boq_trans_id(_pjc_id_from) <> "I" Then
                                '            Box("BOQ not yet aproved")
                                '            sqlTran.Rollback()
                                '            Return False
                                '        End If

                                '        If func_coll.boq_trans_id(_pjc_id_to) <> "I" Then
                                '            Box("BOQ not yet aproved")
                                '            sqlTran.Rollback()
                                '            Return False
                                '        End If


                                '        .Command.CommandType = CommandType.Text
                                '        .Command.CommandText = "update boqs_stand set boqs_qty_relocation=coalesce(boqs_qty_relocation,0) - " _
                                '                    & SetDbl(_qty) & ",boqs_qty=coalesce(boqs_qty,0) - " _
                                '                    & SetDbl(_qty) & ",boqs_dt=current_timestamp,boqs_upd_by=" _
                                '                    & SetSetring(master_new.ClsVar.sNama) & ",boqs_upd_date=current_timestamp where boqs_oid = (SELECT  " _
                                '                    & "  d.boqs_oid " _
                                '                    & "FROM " _
                                '                    & "  public.boq_mstr a " _
                                '                    & "  INNER JOIN public.boqs_stand d ON (a.boq_oid = d.boqs_boq_oid) " _
                                '                    & "  INNER JOIN public.prj_mstr b ON (a.boq_sopj_oid = b.prj_oid) " _
                                '                    & "  INNER JOIN public.pjc_mstr c ON (b.prj_code = c.pjc_code) " _
                                '                    & "WHERE " _
                                '                    & "  c.pjc_id = " & _pjc_id_to & " AND  " _
                                '                    & "  d.boqs_pt_id = " & _pt_id & " AND  " _
                                '                    & "  d.boqs_is_manual = 'N')"


                                '        .Command.ExecuteNonQuery()
                                '        .Command.Parameters.Clear()

                                '        .Command.CommandType = CommandType.Text
                                '        .Command.CommandText = "update boqs_stand set boqs_qty_relocation=coalesce(boqs_qty_relocation,0) + " _
                                '                    & SetDbl(_qty) & ",boqs_qty=coalesce(boqs_qty,0) + " _
                                '                    & SetDbl(_qty) & ",boqs_dt=current_timestamp,boqs_upd_by=" _
                                '                    & SetSetring(master_new.ClsVar.sNama) & ",boqs_upd_date=current_timestamp where boqs_oid = (SELECT  " _
                                '                    & "  d.boqs_oid " _
                                '                    & "FROM " _
                                '                    & "  public.boq_mstr a " _
                                '                    & "  INNER JOIN public.boqs_stand d ON (a.boq_oid = d.boqs_boq_oid) " _
                                '                    & "  INNER JOIN public.prj_mstr b ON (a.boq_sopj_oid = b.prj_oid) " _
                                '                    & "  INNER JOIN public.pjc_mstr c ON (b.prj_code = c.pjc_code) " _
                                '                    & "WHERE " _
                                '                    & "  c.pjc_id = " & _pjc_id_from & " AND  " _
                                '                    & "  d.boqs_pt_id = " & _pt_id & " And " _
                                '                    & "  d.boqs_is_manual = 'N')"


                                '        .Command.ExecuteNonQuery()
                                '        .Command.Parameters.Clear()

                                '        ''============ update budget ===========
                                '        'If func_coll.bdgt_trans_id(_pjc_id_from) <> "I" Then
                                '        '    Box("Budget not yet aproved")
                                '        '    sqlTran.Rollback()
                                '        '    Return False
                                '        'End If

                                '        'If func_coll.bdgt_trans_id(_pjc_id_to) <> "I" Then
                                '        '    Box("Budget not yet aproved")
                                '        '    sqlTran.Rollback()
                                '        '    Return False
                                '        'End If


                                '        'Dim _date As Date
                                '        'Dim _ammount As Double
                                '        'Dim _cc_id, _ac_id, _pjc_id As Integer


                                '        'sSQL = "SELECT  " _
                                '        '        & "  z.pla_ac_id " _
                                '        '        & "FROM " _
                                '        '        & "  public.pt_mstr x " _
                                '        '        & "  INNER JOIN public.pl_mstr y ON (x.pt_pl_id = y.pl_id) " _
                                '        '        & "  INNER JOIN public.pla_mstr z ON (y.pl_id = z.pla_pl_id) " _
                                '        '        & "WHERE " _
                                '        '        & "  z.pla_code = 'INV_ACCT' AND  " _
                                '        '        & "  x.pt_id = " & _pt_id

                                '        'dt = master_new.PGSqlConn.GetTableData(sSQL)

                                '        '_ammount = _qty * _cost
                                '        '_cc_id = 0
                                '        '_ac_id = dt.Rows(0).Item(0)
                                '        '_pjc_id = _pjc_id_from
                                '        '_date = func_coll.get_tanggal_sistem()

                                '        'If func_coll.update_budget_dr_wf(ssqls, objinsert, _ac_id _
                                '        '                             , _ammount, _cc_id, _pjc_id, _date, "UPDATERELOKASI") = False Then
                                '        '    sqlTran.Rollback()
                                '        '    Return False
                                '        'End If


                                '        '_pjc_id = _pjc_id_to
                                '        '_ammount = _qty * _cost * -1

                                '        'If func_coll.update_budget_dr_wf(ssqls, objinsert, _ac_id _
                                '        '                            , _ammount, _cc_id, _pjc_id, _date, "UPDATERELOKASI") = False Then
                                '        '    sqlTran.Rollback()
                                '        '    Return False
                                '        'End If

                                '        'x += 1
                                '    End If
                            End If

                            'If func_coll.get_conf_file("wo_issue_to_wip") = "1" Then
                            '    If ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_type").ToString.ToUpper = "W" Then
                            '        If SetNumber(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty_comp")) <> 0 Then
                            '            Box("Qty Complement Only From Location")
                            '            sqlTran.Rollback()
                            '            Return False
                            '            Exit Try
                            '        End If

                            '        'jika dr workcenter maka kurangi inventory work center nya yang tanpa nilai wo (wo null)
                            '        If func_coll.update_inv_wip(objinsert, ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_id"), _
                            '           ds_edit.Tables("wocid_det").Rows(i).Item("pt_id_wod"), woci_en_id.EditValue, "", _
                            '           ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty") * -1) = False Then

                            '            sqlTran.Rollback()
                            '            Return False
                            '            Exit Try
                            '        End If
                            '    ElseIf ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_type").ToString.ToUpper = "L" Then
                            '        If SetNumber(ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty_comp")) <> 0 Then
                            '            If func_coll.update_inv_wip(objinsert, ds_edit.Tables("wocid_det").Rows(i).Item("wocid_wc_id"), _
                            '             ds_edit.Tables("wocid_det").Rows(i).Item("pt_id_wod"), woci_en_id.EditValue, "", _
                            '             ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty_comp")) = False Then

                            '                sqlTran.Rollback()
                            '                Return False
                            '                Exit Try
                            '            End If
                            '        End If
                            '    End If
                            'End If


                            'update boqs================================================
                            'krn kemungkinan bisa dobel maka update dr seq paling atas
                            'har 20110620


                            '    sSQL = "SELECT  " _
                            '        & "  d.pjc_id, " _
                            '        & "  a.boqs_oid, " _
                            '        & "  a.boqs_seq, " _
                            '        & "  a.boqs_pt_id, " _
                            '        & "  a.boqs_qty,coalesce(a.boqs_qty_wo,0) as boqs_qty_wo,a.boqs_qty - coalesce(a.boqs_qty_wo,0) as boqs_qty_os " _
                            '        & "FROM " _
                            '        & "  public.boqs_stand a " _
                            '        & "  INNER JOIN public.boq_mstr b ON (a.boqs_boq_oid = b.boq_oid) " _
                            '        & "  INNER JOIN public.prj_mstr c ON (b.boq_sopj_oid = c.prj_oid) " _
                            '        & "  INNER JOIN public.pjc_mstr d ON (c.prj_code = d.pjc_code) " _
                            '        & "WHERE " _
                            '        & "  d.pjc_id =(SELECT   x.wo_pjc_id FROM  public.wo_mstr x WHERE  x.wo_id = " & SetIntegerDB(_wo_id.ToString) & ") " _
                            '        & " AND  a.boqs_pt_id =" & SetInteger(ds_edit.Tables("wocid_det").Rows(i).Item("wod_pt_bom_id")) _
                            '        & " ORDER BY " _
                            '        & "  a.boqs_seq"


                            '    dt = master_new.PGSqlConn.GetTableData(sSQL)
                            '    Dim _qty_po, _qty_update As Double
                            '    _qty_po = ds_edit.Tables("wocid_det").Rows(i).Item("wocid_qty")
                            '    _qty_update = 0

                            '    For Each dr As DataRow In dt.Rows

                            '        If _qty_po <> 0 Then

                            '            If dr("boqs_qty_os") < _qty_po Then
                            '                _qty_update = dr("boqs_qty_os")
                            '            Else
                            '                _qty_update = _qty_po

                            '            End If

                            '            .Command.CommandType = CommandType.Text
                            '            .Command.CommandText = "update boqs_stand set boqs_qty_wo=coalesce(boqs_qty_wo,0) + " _
                            '                        & SetDbl(_qty_update) & " where boqs_oid = '" & dr("boqs_oid") & "'"
                            '            .Command.ExecuteNonQuery()
                            '            .Command.Parameters.Clear()

                            '            _qty_po = _qty_po - _qty_update
                            '        End If
                            '    Next
                            'End If
                        Next

                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wocids_serial " _
                                                & "( " _
                                                & "  wocids_oid, " _
                                                & "  wocids_wocid_oid, " _
                                                & "  wocids_qty, " _
                                                & "  wocids_si_id, " _
                                                & "  wocids_loc_id, " _
                                                & "  wocids_lot_serial " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("wocids_oid").ToString) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("wocids_wocid_oid").ToString) & ",  " _
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("wocids_qty")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("wocids_um")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("wocids_si_id")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("wocids_loc_id")) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("wocids_lot_serial")) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'If ds_edit.Tables("wocid_det").Rows(i).Item("wocid_loc_type").ToString.ToUpper = "L" Then
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For j = 0 To ds_edit.Tables(0).Rows.Count - 1

                            'If IsDBNull(ds_edit.Tables(0).Rows(j).Item("wocid_pt_subs_id")) Then
                            If ds_edit.Tables(0).Rows(j).Item("pt_type_wod").ToString.ToUpper = "I" Then
                                If ds_edit.Tables(0).Rows(j).Item("wocid_qty") <> 0 Then
                                    If ds_edit.Tables(0).Rows(j).Item("pt_ls_wod").ToString.ToUpper = "N" Then
                                        i_2 += 1
                                        _en_id = woci_en_id.EditValue
                                        _si_id = ds_edit.Tables(0).Rows(j).Item("wocid_si_id")
                                        _loc_id = ds_edit.Tables(0).Rows(j).Item("wocid_loc_id")
                                        _pt_id = ds_edit.Tables(0).Rows(j).Item("wod_pt_bom_id") 'wod_pt_bom_id
                                        _pjc_id = 0 ' ds_edit.Tables(0).Rows(j).Item("wocid_pjc_id")
                                        _pt_code = ds_edit.Tables(0).Rows(j).Item("pt_code_wod")
                                        _serial = "''"
                                        _qty = SetNumber(ds_edit.Tables(0).Rows(j).Item("wocid_qty")) '+ ds_edit.Tables(0).Rows(j).Item("wocid_qty_comp")
                                        'comment dulu by sys
                                        If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pjc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'Update History Inventory                                    
                                        _cost = SetNumber(ds_edit.Tables(0).Rows(j).Item("wocid_cost"))

                                        'comment dulu by sys
                                        If func_coll.update_invh_mstr(ssqls, objinsert, 1, i_2, _en_id, _woci_code, _woci_oid.ToString, _
                                             "WO Issue", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty * -1, _cost, 0, "", func_coll.get_tanggal_sistem) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    End If

                                End If
                            End If
                            'Else
                            'If ds_edit.Tables(0).Rows(j).Item("pt_type_subs").ToString.ToUpper = "I" Then
                            '    If ds_edit.Tables(0).Rows(j).Item("wocid_qty") <> 0 Then
                            '        If ds_edit.Tables(0).Rows(j).Item("pt_ls_subs").ToString.ToUpper = "N" Then
                            '            i_2 += 1
                            '            _en_id = wo_en_id.EditValue
                            '            _si_id = ds_edit.Tables(0).Rows(j).Item("wocid_si_id")
                            '            _loc_id = ds_edit.Tables(0).Rows(j).Item("wocid_loc_id")
                            '            _pjc_id = ds_edit.Tables(0).Rows(j).Item("wocid_pjc_id")
                            '            _pt_id = ds_edit.Tables(0).Rows(j).Item("pt_id_subs")
                            '            _pt_code = ds_edit.Tables(0).Rows(j).Item("pt_code_subs")
                            '            _serial = "''"
                            '            '_qty = ds_edit.Tables(0).Rows(j).Item("wocid_qty")
                            '            _qty = ds_edit.Tables(0).Rows(j).Item("wocid_qty") + ds_edit.Tables(0).Rows(j).Item("wocid_qty_comp")

                            '            'comment dulu by sys
                            '            If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pjc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                            '                sqlTran.Rollback()
                            '                insert = False
                            '                Exit Function
                            '            End If

                            '            'Update History Inventory                                    
                            '            _cost = ds_edit.Tables(0).Rows(j).Item("wocid_cost_std")

                            '            'comment dulu by sys
                            '            If func_coll.update_invh_mstr(ssqls, objinsert, 1, i_2, _en_id, _woci_code, _woci_oid.ToString, "WO Issue", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty * -1, _cost, 0, "", func_coll.get_tanggal_sistem) = False Then
                            '                sqlTran.Rollback()
                            '                insert = False
                            '                Exit Function
                            '            End If
                            '        End If
                            '    End If
                            'End If
                            'End If

                        Next
                       

                        '20111226 sys
                        'script masih salah...kayaknya belum update pt_id atau pt_subs_id di gv_serialnya...lalu qty harus total seperti yang script diatas..atau gimana..tolong cek lagi 
                        ' ''2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        ''For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                        ''    If IsDBNull(ds_edit.Tables("wocid_det").Rows(j).Item("wocid_pt_subs_id")) Then
                        ''        If ds_serial.Tables(0).Rows(j).Item("pt_type_wod").ToString.ToUpper = "I" Then
                        ''            If ds_serial.Tables(0).Rows(j).Item("wocids_qty") <> 0 Then
                        ''                i_2 += 1

                        ''                _en_id = woci_en_id.EditValue
                        ''                _si_id = ds_serial.Tables(0).Rows(j).Item("wocids_si_id")
                        ''                _loc_id = ds_serial.Tables(0).Rows(j).Item("wocids_loc_id")
                        ''                _pt_id = ds_serial.Tables(0).Rows(j).Item("pt_id_wod")
                        ''                _pt_code = ds_edit.Tables(0).Rows(j).Item("pt_code_wod")
                        ''                _serial = ds_serial.Tables(0).Rows(j).Item("wocids_lot_serial")
                        ''                _qty = ds_serial.Tables(0).Rows(j).Item("wocids_qty")

                        ''                'comment dulu by sys
                        ''                If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        ''                    sqlTran.Rollback()
                        ''                    insert = False
                        ''                    Exit Function
                        ''                End If

                        ''                'Update History Inventory
                        ''                'comment dulu by sys
                        ''                If func_coll.update_invh_mstr(ssqls, objinsert, 1, i_2, _en_id, _woci_code, _woci_oid.ToString, "WO Issue", "", _si_id, _loc_id, _pt_id, _qty * -1, _cost, 0, "", func_coll.get_tanggal_sistem) = False Then
                        ''                    sqlTran.Rollback()
                        ''                    insert = False
                        ''                    Exit Function
                        ''                End If
                        ''            End If
                        ''        End If
                        ''    Else
                        ''        If ds_serial.Tables(0).Rows(j).Item("pt_type_subs").ToString.ToUpper = "I" Then
                        ''            If ds_serial.Tables(0).Rows(j).Item("wocids_qty") <> 0 Then
                        ''                i_2 += 1

                        ''                _en_id = woci_en_id.EditValue
                        ''                _si_id = ds_serial.Tables(0).Rows(j).Item("wocids_si_id")
                        ''                _loc_id = ds_serial.Tables(0).Rows(j).Item("wocids_loc_id")
                        ''                _pt_id = ds_serial.Tables(0).Rows(j).Item("pt_id_subs")
                        ''                _pt_code = ds_edit.Tables(0).Rows(j).Item("pt_code_subs")
                        ''                _serial = ds_serial.Tables(0).Rows(j).Item("wocids_lot_serial")
                        ''                _qty = ds_serial.Tables(0).Rows(j).Item("wocids_qty")

                        ''                'comment dulu by sys
                        ''                If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        ''                    sqlTran.Rollback()
                        ''                    insert = False
                        ''                    Exit Function
                        ''                End If

                        ''                'Update History Inventory
                        ''                'comment dulu by sys
                        ''                If func_coll.update_invh_mstr(ssqls, objinsert, 1, i_2, _en_id, _woci_code, _woci_oid.ToString, "WO Issue", "", _si_id, _loc_id, _pt_id, _qty * -1, _cost, 0, "", func_coll.get_tanggal_sistem) = False Then
                        ''                    sqlTran.Rollback()
                        ''                    insert = False
                        ''                    Exit Function
                        ''                End If
                        ''            End If
                        ''        End If
                        ''    End If
                        ''Next

                        'belum dipake dulu....20111213 sys
                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For j = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If IsDBNull(ds_edit.Tables("wocid_det").Rows(j).Item("wocid_pt_subs_id")) Then
                        '        If ds_edit.Tables(0).Rows(j).Item("pt_type_wod").ToString.ToUpper = "I" Then
                        '            _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(j).Item("pt_id_wod").ToString.ToUpper)
                        '            _en_id = woci_en_id.EditValue
                        '            _si_id = ds_edit.Tables(0).Rows(j).Item("wocid_si_id")
                        '            _pt_id = ds_edit.Tables(0).Rows(j).Item("wod_pt_bom_id")
                        '            _qty = ds_edit.Tables(0).Rows(j).Item("wocid_qty")
                        '            _cost = ds_edit.Tables(0).Rows(j).Item("wod_cost")

                        '            If _cost_methode = "F" Or _cost_methode = "L" Then
                        '                If func_coll.update_invct_table_minus(objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '                    sqlTran.Rollback()
                        '                    insert = False
                        '                    Exit Function
                        '                End If
                        '            ElseIf _cost_methode = "A" Then
                        '                If func_coll.update_item_cost_avg(objinsert, "-", _en_id, _si_id, _pt_id, _qty, _cost) = False Then
                        '                    sqlTran.Rollback()
                        '                    insert = False
                        '                    Exit Function
                        '                End If
                        '            End If
                        '        End If
                        '    Else
                        '        If ds_edit.Tables(0).Rows(j).Item("pt_type_subs").ToString.ToUpper = "I" Then
                        '            _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(j).Item("pt_id_subs").ToString.ToUpper)
                        '            _en_id = woci_en_id.EditValue
                        '            _si_id = ds_edit.Tables(0).Rows(j).Item("wocid_si_id")
                        '            _pt_id = ds_edit.Tables(0).Rows(j).Item("pt_id_subs")
                        '            _qty = ds_edit.Tables(0).Rows(j).Item("wocid_qty")
                        '            _cost = ds_edit.Tables(0).Rows(j).Item("wod_cost")

                        '            If _cost_methode = "F" Or _cost_methode = "L" Then
                        '                If func_coll.update_invct_table_minus(objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '                    sqlTran.Rollback()
                        '                    insert = False
                        '                    Exit Function
                        '                End If
                        '            ElseIf _cost_methode = "A" Then
                        '                If func_coll.update_item_cost_avg(objinsert, "-", _en_id, _si_id, _pt_id, _qty, _cost) = False Then
                        '                    sqlTran.Rollback()
                        '                    insert = False
                        '                    Exit Function
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        'End If

                        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
                        If _create_jurnal = True Then
                            If insert_glt_det_wo_issue(ssqls, objinsert, ds_edit, _
                                                 woci_en_id.EditValue, woci_en_id.GetColumnValue("en_code"), _
                                                 _woci_oid.ToString, _woci_code, _
                                                 woci_date.DateTime, _
                                                 "WO", "ISS-WIP") = False Then

                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

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
                        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                        ds_edit.Tables("wocid_det").Clear()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        insert = False
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using

        Catch ex As Exception
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function
    Private Function insert_glt_det_wo_issue(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean
        insert_glt_det_wo_issue = True
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
                    If par_ds.Tables(0).Rows(i).Item("wocid_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id_wod"))
                        _cost = SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_qty")) * SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_cost"))

                        '********************** finish untuk yang debet
                        dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_DET_ACC")

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
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("Work Order Issue (Step 1)") & ",  " _
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
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If

                        dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")
                        'Insert Untuk Yang credit 
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
                                            & SetSetring("Work Order Issue (Step 1)") & ",  " _
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


                        'jurnal baris kedua
                        dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_HED_ACC")
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
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("Work Order Issue (Step 2)") & ",  " _
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
                                                          dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If

                        dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_DET_ACC")
                        'Insert Untuk Yang credit 
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
                                            & SetSetring("Work Order Issue (Step 2)") & ",  " _
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


                        'jurnal baris ketiga
                        dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
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
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("Work Order Issue (Step 3)") & ",  " _
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
                                                           dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If

                        dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_HED_ACC")
                        'Insert Untuk Yang credit 
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
                                            & SetSetring("Work Order Issue (Step 3)") & ",  " _
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
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function
    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _wocid_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _wocid_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "wc_desc" Then
            Dim frm As New FWCSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _wocid_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pjc_code" Then
            Dim frm As New FProjectSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _wocid_en_id
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "pt_code_subs" Then
            Dim frm As New FItemSubsSearch
            frm.set_win(Me)
            frm._pt_id = gv_edit.GetRowCellValue(_row, "wod_pt_bom_id").ToString
            frm._row = _row
            frm._en_id = _wocid_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub

    Private Sub XtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        'ant 5 nov 2011
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position
        If XtraTabControl1.SelectedTabPage.Name.ToString = "XtraTabPage2" Then
            '    'If IsDBNull(ds_edit.Tables("wocid_det").Rows(_row_edit).Item("wocid_pt_subs_id")) Then

            '    If (gv_edit.GetRowCellValue(_row_edit, "pt_ls_wod") = "S") Or (gv_edit.GetRowCellValue(_row_edit, "pt_ls_wod") = "L") Then
            '        gc_serial.EmbeddedNavigator.Buttons.Append.Visible = True
            '        gc_serial.EmbeddedNavigator.Buttons.Remove.Visible = True
            '        te_serial_code.Enabled = True
            '        te_serial_from.Enabled = True
            '        te_serial_to.Enabled = True
            '        btn_gen_serial.Enabled = True
            '    Else
            '        gc_serial.EmbeddedNavigator.Buttons.Append.Visible = False
            '        gc_serial.EmbeddedNavigator.Buttons.Remove.Visible = False
            '        te_serial_code.Enabled = False
            '        te_serial_from.Enabled = False
            '        te_serial_to.Enabled = False
            '        btn_gen_serial.Enabled = False
            '    End If
            'Else
            '    If (gv_edit.GetRowCellValue(_row_edit, "pt_ls_subs") = "S") Or (gv_edit.GetRowCellValue(_row_edit, "pt_ls_subs") = "L") Then
            '        gc_serial.EmbeddedNavigator.Buttons.Append.Visible = True
            '        gc_serial.EmbeddedNavigator.Buttons.Remove.Visible = True
            '        te_serial_code.Enabled = True
            '        te_serial_from.Enabled = True
            '        te_serial_to.Enabled = True
            '        btn_gen_serial.Enabled = True
            '    Else
            '        gc_serial.EmbeddedNavigator.Buttons.Append.Visible = False
            '        gc_serial.EmbeddedNavigator.Buttons.Remove.Visible = False
            '        te_serial_code.Enabled = False
            '        te_serial_from.Enabled = False
            '        te_serial_to.Enabled = False
            '        btn_gen_serial.Enabled = False
            '    End If

            'End If
        End If
        '-------------------------------------
    End Sub

    Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        With gv_serial
            .SetRowCellValue(e.RowHandle, "wocids_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "wocids_wocid_oid", ds_edit.Tables(0).Rows(_row_edit).Item("wocid_oid").ToString)
            .SetRowCellValue(e.RowHandle, "wocids_qty", 1)
            .SetRowCellValue(e.RowHandle, "wocids_si_id", ds_edit.Tables(0).Rows(_row_edit).Item("wocid_si_id"))
            .SetRowCellValue(e.RowHandle, "wocids_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("wocid_loc_id"))
            .SetRowCellValue(e.RowHandle, "pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_id"))
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub woci_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles woci_en_id.EditValueChanged
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_si_mstr(woci_en_id.EditValue))
            woci_si_id.Properties.DataSource = dt_bantu
            woci_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            woci_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            woci_si_id.ItemIndex = 0

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_wc_mstr(woci_en_id.EditValue))
            woci_wc_id.Properties.DataSource = dt_bantu
            woci_wc_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
            woci_wc_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
            woci_wc_id.ItemIndex = 0
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub btn_gen_serial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_gen_serial.Click
        Dim _dtrow As DataRow

        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        For _num_serial As Integer = te_serial_from.EditValue To te_serial_to.EditValue
            If cek_avail_serial(Trim(te_serial_code.EditValue) & _num_serial) = True Then
                MsgBox("Duplicate Serial Number : " & Trim(te_serial_code.EditValue) & _num_serial, MsgBoxStyle.Critical, "Duplicate")
                Exit Sub
            End If

            _dtrow = ds_serial.Tables(0).NewRow

            _dtrow("wocids_oid") = Guid.NewGuid.ToString
            _dtrow("wocids_wocid_oid") = ds_edit.Tables(0).Rows(_row_edit).Item("wocid_oid").ToString
            _dtrow("wocids_qty") = 1
            _dtrow("wocids_si_id") = ds_edit.Tables(0).Rows(_row_edit).Item("wocid_si_id")
            _dtrow("wocids_loc_id") = ds_edit.Tables(0).Rows(_row_edit).Item("wocid_loc_id")
            _dtrow("pt_id") = ds_edit.Tables(0).Rows(_row_edit).Item("pt_id")
            _dtrow("wocids_lot_serial") = Trim(te_serial_code.EditValue) & _num_serial

            ds_serial.Tables(0).Rows.Add(_dtrow)

        Next
        ds_serial.Tables(0).AcceptChanges()

    End Sub

    Private Function cek_avail_serial(ByVal _par_serial As String) As Boolean
        cek_avail_serial = False
        Try
            For i As Integer = 0 To ds_serial.Tables(0).Rows.Count - 1
                If _par_serial = ds_serial.Tables(0).Rows(i).Item("wocids_lot_serial").ToString Then
                    cek_avail_serial = True
                End If
            Next
        Catch
        End Try
        Return cek_avail_serial
    End Function

    Private Sub woci_pb_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles woci_pb_oid.ButtonClick
        Dim frm As New FInventoryRequestSearch()
        frm.set_win(Me)
        frm.type_form = True
        frm._en_id = woci_en_id.EditValue
        frm.par_wo_id = _wo_id
        frm._obj = woci_pb_oid
        frm.ShowDialog()
    End Sub
End Class