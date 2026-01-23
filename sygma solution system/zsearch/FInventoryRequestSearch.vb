Imports master_new.ModFunction

Public Class FInventoryRequestSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _en_id_shipment As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Dim _conf_value As String
    Dim _conf_limit_date As String
    Public par_wo_id As String

    Private Sub FInventoryRequestSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
        _conf_value = func_coll.get_conf_file("wf_inventory_request")
        _conf_limit_date = ""
        _conf_limit_date = func_coll.get_conf_file("inventory_request_limit_date")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Inventory Request Number", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Inventory Request Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        If fobject.name = FWorkOrderIssue.Name Then
            '
            add_column(gv_master, "Unplan", "pb_is_unplan", DevExpress.Utils.HorzAlignment.Default)

            add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If
        add_column(gv_master, "Requested By", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Status", "pb_status", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FTransferIssues" Or fobject.name = FTransferIssuesMultiLoc.Name Then
            get_sequel = "SELECT distinct " _
                        & "  pb_oid, " _
                        & "  pb_dom_id, " _
                        & "  pb_en_id, " _
                        & "  pb_date, " _
                        & "  pb_due_date, " _
                        & "  pb_requested, " _
                        & "  pb_end_user, " _
                        & "  pb_rmks, " _
                        & "  pb_status, " _
                        & "  pb_close_date, " _
                        & "  pb_dt, " _
                        & "  pb_code, " _
                        & "  en_desc,pb_pbt_code " _
                        & "FROM  " _
                        & "  public.pb_mstr " _
                        & "  inner join en_mstr on en_id = pb_en_id " _
                        & "  inner join pbd_det on pbd_pb_oid = pb_oid " _
                        & " where pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and pb_en_id = " + _en_id.ToString _
                        & "  and (pb_status = '' or pb_status is null) and coalesce(pbd_qty,0)-coalesce(pbd_qty_processed,0) > 0 "

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and pb_trans_id ~~* 'I' "
            End If

            If _conf_limit_date = "1" Then
                get_sequel = get_sequel + " and to_char(pb_date,'YYYYMM') = to_char(current_date,'YYYYMM') "
            End If

            If func_coll.get_conf_file("transfer_issue_limit_entity_shipment") = "1" Then
                get_sequel = get_sequel & " and pb_en_id_shipment=" & _en_id_shipment
            End If

            get_sequel = get_sequel & " order by pb_date,pb_code"

        ElseIf fobject.name = "FInventoryRequestPrint" Or fobject.name = "FInventoryIssues" Or fobject.name = FInventoryIssuesHadiah.Name Then
            get_sequel = "SELECT  " _
                        & "  pb_oid, " _
                        & "  pb_dom_id, " _
                        & "  pb_en_id, " _
                        & "  pb_date, " _
                        & "  pb_due_date, " _
                        & "  pb_requested, " _
                        & "  pb_end_user, " _
                        & "  pb_rmks, " _
                        & "  pb_status, " _
                        & "  pb_close_date, " _
                        & "  pb_dt, " _
                        & "  pb_code, " _
                        & "  en_desc " _
                        & "FROM  " _
                        & "  public.pb_mstr " _
                        & "  inner join en_mstr on en_id = pb_en_id " _
                        & " where pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and pb_en_id = " + _en_id.ToString _
                        & "  and (pb_status = '' or pb_status is null) "

        ElseIf fobject.name = FInventoryIssuesLintas.Name Then
            get_sequel = "SELECT  " _
                        & "  pb_oid, " _
                        & "  pb_dom_id, " _
                        & "  pb_en_id, " _
                        & "  pb_date, " _
                        & "  pb_due_date, " _
                        & "  pb_requested, " _
                        & "  pb_end_user, " _
                        & "  pb_rmks, " _
                        & "  pb_status, " _
                        & "  pb_close_date, " _
                        & "  pb_dt, " _
                        & "  pb_code, " _
                        & "  en_desc " _
                        & "FROM  " _
                        & "  public.pb_mstr " _
                        & "  inner join en_mstr on en_id = pb_en_id " _
                        & " where pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and pb_en_id_shipment = " + _en_id.ToString _
                        & "  and (pb_status = '' or pb_status is null)  order by pb_code"


        ElseIf fobject.name = FInventoryReceiptsLintas.Name Then

            get_sequel = "SELECT  " _
                        & "  pb_oid, " _
                        & "  pb_dom_id, " _
                        & "  pb_en_id, " _
                        & "  pb_date, " _
                        & "  pb_due_date, " _
                        & "  pb_requested, " _
                        & "  pb_end_user, " _
                        & "  pb_rmks, " _
                        & "  pb_status, " _
                        & "  pb_close_date, " _
                        & "  pb_dt, " _
                        & "  pb_code, " _
                        & "  en_desc " _
                        & "FROM  " _
                        & "  public.pb_mstr " _
                        & "  inner join en_mstr on en_id = pb_en_id " _
                        & " where pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and pb_en_id = " + _en_id.ToString _
                        & "  and pb_status = 'X' and pb_status_inv_issue='Y' and pb_inv_issue_code is not null and pb_status_inv_receipt='N' order by pb_code"

        ElseIf fobject.name = "FWorkOrderIssue" Then
            get_sequel = "SELECT  " _
                      & "  pb_oid, " _
                      & "  pb_dom_id, " _
                      & "  pb_en_id, " _
                      & "  pb_date, " _
                      & "  pb_due_date, " _
                      & "  pb_requested, " _
                      & "  pb_end_user, " _
                      & "  pb_rmks, " _
                      & "  pb_status, " _
                      & "  pb_close_date, " _
                      & "  pb_dt, " _
                      & "  pb_code, " _
                      & "  public.pt_mstr.pt_code, " _
                      & "  public.pt_mstr.pt_desc1, " _
                      & "  public.pbd_det.pbd_qty,pb_is_unplan, " _
                      & "  en_desc " _
                      & "FROM  " _
                      & "  public.pb_mstr " _
                      & "  inner join en_mstr on en_id = pb_en_id " _
                      & "  inner join pbd_det on pbd_pb_oid = pb_oid " _
                       & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                      & " where pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                      & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                      & "  and pb_en_id = " + _en_id.ToString _
                      & "  and pb_wo_oid in (select wo_oid from wo_mstr where wo_id= '" & par_wo_id & "') and coalesce(pbd_qty,0)-coalesce(pbd_qty_riud,0) > 0"

        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        Dim ds_bantu As New DataSet
        Dim i As Integer

        If fobject.name = "FTransferIssues" Or fobject.name = FTransferIssuesMultiLoc.Name Then
            'fobject._pb_oid = ds.Tables(0).Rows(_row_gv).Item("pb_oid")
            'fobject.ptsfr_pb_oid.text = ds.Tables(0).Rows(_row_gv).Item("pb_code")
            'fobject.ptsfr_pb_oid.tag = ds.Tables(0).Rows(_row_gv).Item("pb_oid")

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pbd_oid, " _
                            & "  pbd_dom_id, " _
                            & "  pbd_en_id, " _
                            & "  pbd_add_by, " _
                            & "  pbd_add_date, " _
                            & "  pbd_upd_by, " _
                            & "  pbd_upd_date, " _
                            & "  pbd_pb_oid, " _
                            & "  pbd_seq, " _
                            & "  pbd_pt_id,pb_pbt_code, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_cost, " _
                            & "  invct_cost, " _
                            & "  pbd_rmks, " _
                            & "  pbd_end_user, " _
                            & "  pbd_qty, " _
                            & "  pbd_qty_processed, " _
                            & "  pbd_qty_completed, " _
                            & "  pbd_qty - coalesce(pbd_qty_processed,0) as qty_open, " _
                            & "  pbd_um, " _
                            & "  um_master.code_name as um_name, " _
                            & "  pbd_due_date, " _
                            & "  pbd_status, " _
                            & "  pbd_dt,pb_code,pb_oid ,ircd_qty_approve,pb_rmks " _
                            & "FROM  " _
                            & "  public.pbd_det " _
                            & "  inner join pb_mstr on pb_oid = pbd_pb_oid " _
                            & "  inner join pt_mstr on pt_id = pbd_pt_id " _
                            & "  inner join code_mstr um_master on um_master.code_id = pbd_um" _
                            & "  inner join invct_table on invct_pt_id = pbd_pt_id " _
                            & "  inner join si_mstr on si_id = invct_si_id " _
                            & "  left outer join ircd_det on ircd_pbd_oid = pbd_oid " _
                            & "  where (pbd_qty - coalesce(pbd_qty_processed,0)) > 0 " _
                            & "  and pbd_pb_oid = '" + ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pbd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                If i = 0 Then
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_oid", Guid.NewGuid.ToString)
                    fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds_bantu.Tables(0).Rows(i).Item("pt_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds_bantu.Tables(0).Rows(i).Item("pt_desc1"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds_bantu.Tables(0).Rows(i).Item("pt_desc2"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds_bantu.Tables(0).Rows(i).Item("pt_ls"))


                    If SetString(ds_bantu.Tables(0).Rows(i).Item("pb_pbt_code")) = "REQCBG" And func_coll.get_conf_file("irc_on") = "1" Then
                        fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty_open", SetNumber(ds_bantu.Tables(0).Rows(i).Item("ircd_qty_approve")))
                        fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty", SetNumber(ds_bantu.Tables(0).Rows(i).Item("ircd_qty_approve")))
                    Else
                        fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty_open", ds_bantu.Tables(0).Rows(i).Item("qty_open"))
                        fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty", ds_bantu.Tables(0).Rows(i).Item("qty_open"))
                    End If

                    'fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty_open", ds_bantu.Tables(0).Rows(i).Item("qty_open"))
                    'fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty", ds_bantu.Tables(0).Rows(i).Item("qty_open"))


                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_qty_receive", 0)
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um", ds_bantu.Tables(0).Rows(i).Item("pbd_um"))
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", ds_bantu.Tables(0).Rows(i).Item("um_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_cost", ds_bantu.Tables(0).Rows(i).Item("invct_cost"))
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_pbd_oid", ds_bantu.Tables(0).Rows(i).Item("pbd_oid").ToString)
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_pb_oid", ds_bantu.Tables(0).Rows(i).Item("pb_oid").ToString)
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_pb_code", ds_bantu.Tables(0).Rows(i).Item("pb_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_remarks", ds_bantu.Tables(0).Rows(i).Item("pb_rmks"))
                    'ptsfrd_remarks

                Else
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("ptsfrd_oid") = Guid.NewGuid.ToString
                    _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                    _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                    _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                    _dtrow("ptsfrd_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")

                    If SetString(ds_bantu.Tables(0).Rows(i).Item("pb_pbt_code")) = "REQCBG" And func_coll.get_conf_file("irc_on") = "1" Then
                        _dtrow("ptsfrd_qty_open") = SetNumber(ds_bantu.Tables(0).Rows(i).Item("ircd_qty_approve"))
                        _dtrow("ptsfrd_qty") = SetNumber(ds_bantu.Tables(0).Rows(i).Item("ircd_qty_approve"))
                    Else
                        _dtrow("ptsfrd_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                        _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    End If


                    _dtrow("ptsfrd_qty_receive") = 0
                    _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("pbd_um")
                    _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                    _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
                    _dtrow("ptsfrd_pbd_oid") = SetStringNull(ds_bantu.Tables(0).Rows(i).Item("pbd_oid"))

                    _dtrow("ptsfrd_pb_oid") = ds_bantu.Tables(0).Rows(i).Item("pb_oid").ToString
                    _dtrow("ptsfrd_pb_code") = ds_bantu.Tables(0).Rows(i).Item("pb_code")
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                End If

            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            fobject.ptsfr_pb_oid.enabled = False
            fobject.ptsfr_sq_oid.enabled = False
            fobject.ptsfr_sq_oid.text = ""
            'fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            'fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False

        ElseIf fobject.name = "FInventoryRequestPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")

        ElseIf fobject.name = FInventoryReceiptsLintas.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pb_code")
            fobject.riu_ref_pb_code.enabled = False
            fobject._riu_ref_pb_oid = ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString
            fobject.riu_remarks.editvalue = ds.Tables(0).Rows(_row_gv).Item("pb_rmks")


            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pbd_oid, " _
                            & "  pbd_dom_id, " _
                            & "  pbd_en_id, " _
                            & "  pbd_add_by, " _
                            & "  pbd_add_date, " _
                            & "  pbd_upd_by, " _
                            & "  pbd_upd_date, " _
                            & "  pbd_pb_oid, " _
                            & "  pbd_seq, " _
                            & "  pbd_pt_id, " _
                            & "  pt_code,pt_pl_id, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_cost, " _
                            & "  invct_cost, " _
                            & "  pbd_rmks, " _
                            & "  pbd_end_user, " _
                            & "  pbd_qty, " _
                            & "  pbd_qty_processed, " _
                            & "  pbd_qty_completed, " _
                            & "  pbd_qty - coalesce(pbd_qty_riud,0) as qty_open, " _
                            & "  pbd_um, pt_um, " _
                            & "  um_master.code_name as um_name, " _
                            & "  pbd_due_date, " _
                            & "  pbd_status, " _
                            & "  pbd_si_id, " _
                            & "  si_desc, " _
                            & "  pbd_dt " _
                            & "FROM  " _
                            & "  public.pbd_det " _
                            & "  inner join pb_mstr on pb_oid = pbd_pb_oid " _
                            & "  inner join pt_mstr on pt_id = pbd_pt_id " _
                            & "  inner join code_mstr um_master on um_master.code_id = pbd_um" _
                            & "  inner join invct_table on invct_pt_id = pbd_pt_id " _
                            & "  inner join si_mstr on si_id = invct_si_id " _
                            & "  where (pbd_qty - coalesce(pbd_qty_riud,0)) > 0 " _
                            & "  and pbd_pb_oid = '" + ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pbd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("riud_oid") = Guid.NewGuid.ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("riud_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '_dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '_dtrow("riud_um_conv") = 1
                _dtrow("riud_um") = ds_bantu.Tables(0).Rows(i).Item("pbd_um")
                _dtrow("riud_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                '_dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
                '_dtrow("riud_pbd_oid") = ds_bantu.Tables(0).Rows(i).Item("pbd_oid")
                '_dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("riud_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("riud_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("riud_si_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")

                If fobject.name = FInventoryReceiptsLintas.Name Then
                    'Dim ssql As String
                    Dim _ac_so As Integer = -1
                    Dim _ac_code, _ac_name As String
                    _ac_code = ""
                    _ac_name = ""
                    Dim ssql As String
                    ssql = "SELECT  " _
                        & "  a.enacc_ac_id,ac_code,ac_name " _
                        & "FROM " _
                        & "  public.enacc_mstr a " _
                        & " inner join ac_mstr b on (enacc_ac_id=ac_id)" _
                        & "WHERE " _
                        & "  a.enacc_code='inventory_receipt_AJB_acc' AND  " _
                        & "  a.enacc_en_id=" & SetInteger(_en_id)

                    Dim dt As New DataTable
                    dt = master_new.PGSqlConn.GetTableData(ssql)

                    For Each dr As DataRow In dt.Rows
                        _ac_so = dr(0)
                        _ac_code = dr(1)
                        _ac_name = dr(2)
                    Next

                    If _ac_so = -1 And _en_id <> 0 Then
                        Box("Please setting account default for Promo for this entity")
                    Else
                        _dtrow("riud_ac_id") = _ac_so
                        _dtrow("ac_code") = _ac_code
                        _dtrow("ac_name") = _ac_name
                    End If
                Else
                    Dim dt_bantu As New DataTable

                    If limit_account(_en_id) = True Then

                        'Dim _filter As String

                        '_filter = " and ac_id in (SELECT  " _
                        '  & "  enacc_ac_id " _
                        '  & "FROM  " _
                        '  & "  public.enacc_mstr  " _
                        '  & "Where enacc_en_id=" & SetInteger(_en_id) & " and enacc_code='inv_issue_account') "


                        'dt_bantu = (func_data.load_ac_mstr(_filter))

                        'If dt_bantu.Rows.Count = 0 Then
                        '    Box("Inventory Issue account setting is empty")
                        '    Exit Sub
                        'End If

                        '_dtrow("riud_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        '_dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                        '_dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                    Else
                        dt_bantu = (func_coll.get_prodline_account(ds_bantu.Tables(0).Rows(i).Item("pt_pl_id"), "WO_COP-"))
                        If dt_bantu Is Nothing Then
                            Box("WO_COP- for " & ds_bantu.Tables(0).Rows(i).Item("pt_code") & " " & ds_bantu.Tables(0).Rows(i).Item("pt_desc1") & " is empty")
                            Exit Sub
                        End If

                        _dtrow("riud_ac_id") = dt_bantu.Rows(0).Item("pla_ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                        _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")


                    End If
                End If

                'sys 20110922
                If ds_bantu.Tables(0).Rows(i).Item("pbd_um") = ds_bantu.Tables(0).Rows(i).Item("pt_um") Then
                    _dtrow("riud_um_conv") = 1
                    _dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
                Else
                    Dim _um_conv As Double = 1
                    Try
                        Using objcek As New master_new.WDABasepgsql("", "")
                            With objcek
                                .Connection.Open()
                                .Command = .Connection.CreateCommand
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "select um_conv from um_mstr " + _
                                       " where um_pt_id = " + ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id").ToString + _
                                       " and um_pt_um = " + ds_bantu.Tables(0).Rows(i).Item("pbd_um").ToString + _
                                       " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id").ToString + ") "
                                .InitializeCommand()
                                .DataReader = .Command.ExecuteReader
                                While .DataReader.Read
                                    _um_conv = .DataReader("um_conv")
                                End While
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Exit Sub
                    End Try

                    _dtrow("riud_um_conv") = _um_conv
                    _dtrow("riud_qty_real") = _um_conv * ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("riud_cost") = _um_conv * ds_bantu.Tables(0).Rows(i).Item("invct_cost")



                End If
                '--

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            'fobject.ptsfr_pb_oid.enabled = False
            'fobject.ptsfr_so_oid.enabled = False
            'fobject.ptsfr_so_oid.text = ""
            fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True

        ElseIf fobject.name = "FInventoryIssues" Or fobject.name = FInventoryIssuesHadiah.Name Or fobject.name = FInventoryIssuesLintas.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pb_code")
            fobject.riu_ref_pb_code.enabled = False
            fobject._riu_ref_pb_oid = ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString

            If fobject.name = FInventoryIssuesLintas.Name Then
                fobject.riu_remarks.editvalue = ds.Tables(0).Rows(_row_gv).Item("pb_rmks")
                fobject._en_id_reff = ds.Tables(0).Rows(_row_gv).Item("pb_en_id")
                fobject._en_desc_reff = ds.Tables(0).Rows(_row_gv).Item("en_desc")
            End If
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pbd_oid, " _
                            & "  pbd_dom_id, " _
                            & "  pbd_en_id, " _
                            & "  pbd_add_by, " _
                            & "  pbd_add_date, " _
                            & "  pbd_upd_by, " _
                            & "  pbd_upd_date, " _
                            & "  pbd_pb_oid, " _
                            & "  pbd_seq, " _
                            & "  pbd_pt_id, " _
                            & "  pt_code,pt_pl_id, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_cost, " _
                            & "  invct_cost, " _
                            & "  pbd_rmks, " _
                            & "  pbd_end_user, " _
                            & "  pbd_qty, " _
                            & "  pbd_qty_processed, " _
                            & "  pbd_qty_completed, " _
                            & "  pbd_qty - coalesce(pbd_qty_riud,0) as qty_open, " _
                            & "  pbd_um, pt_um, " _
                            & "  um_master.code_name as um_name, " _
                            & "  pbd_due_date, " _
                            & "  pbd_status, " _
                            & "  pbd_si_id, " _
                            & "  si_desc, " _
                            & "  pbd_dt " _
                            & "FROM  " _
                            & "  public.pbd_det " _
                            & "  inner join pb_mstr on pb_oid = pbd_pb_oid " _
                            & "  inner join pt_mstr on pt_id = pbd_pt_id " _
                            & "  inner join code_mstr um_master on um_master.code_id = pbd_um" _
                            & "  inner join invct_table on invct_pt_id = pbd_pt_id " _
                            & "  inner join si_mstr on si_id = invct_si_id " _
                            & "  where (pbd_qty - coalesce(pbd_qty_riud,0)) > 0 " _
                            & "  and pbd_pb_oid = '" + ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pbd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("riud_oid") = Guid.NewGuid.ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("riud_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '_dtrow("riud_um_conv") = 1
                _dtrow("riud_um") = ds_bantu.Tables(0).Rows(i).Item("pbd_um")
                _dtrow("riud_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                '_dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
                _dtrow("riud_pbd_oid") = ds_bantu.Tables(0).Rows(i).Item("pbd_oid").ToString
                '_dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("riud_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("riud_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("riud_si_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")

                If fobject.name = FInventoryIssuesLintas.Name Then
                    'Dim ssql As String
                    Dim _ac_so As Integer = -1
                    Dim _ac_code, _ac_name As String
                    _ac_code = ""
                    _ac_name = ""
                    Dim ssql As String
                    ssql = "SELECT  " _
                        & "  a.enacc_ac_id,ac_code,ac_name " _
                        & "FROM " _
                        & "  public.enacc_mstr a " _
                        & " inner join ac_mstr b on (enacc_ac_id=ac_id)" _
                        & "WHERE " _
                        & "  a.enacc_code='inventory_issue_AJB_acc' AND  " _
                        & "  a.enacc_en_id=" & SetInteger(_en_id)

                    Dim dt As New DataTable
                    dt = master_new.PGSqlConn.GetTableData(ssql)

                    For Each dr As DataRow In dt.Rows
                        _ac_so = dr(0)
                        _ac_code = dr(1)
                        _ac_name = dr(2)
                    Next

                    If _ac_so = -1 And _en_id <> 0 Then
                        Box("Please setting account default for Promo for this entity")
                    Else
                        _dtrow("riud_ac_id") = _ac_so
                        _dtrow("ac_code") = _ac_code
                        _dtrow("ac_name") = _ac_name
                    End If
                Else
                    Dim dt_bantu As New DataTable

                    If limit_account(_en_id) = True Then

                        'Dim _filter As String

                        '_filter = " and ac_id in (SELECT  " _
                        '  & "  enacc_ac_id " _
                        '  & "FROM  " _
                        '  & "  public.enacc_mstr  " _
                        '  & "Where enacc_en_id=" & SetInteger(_en_id) & " and enacc_code='inv_issue_account') "


                        'dt_bantu = (func_data.load_ac_mstr(_filter))

                        'If dt_bantu.Rows.Count = 0 Then
                        '    Box("Inventory Issue account setting is empty")
                        '    Exit Sub
                        'End If

                        '_dtrow("riud_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        '_dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                        '_dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                    Else
                        dt_bantu = (func_coll.get_prodline_account(ds_bantu.Tables(0).Rows(i).Item("pt_pl_id"), "WO_COP-"))
                        If dt_bantu Is Nothing Then
                            Box("WO_COP- for " & ds_bantu.Tables(0).Rows(i).Item("pt_code") & " " & ds_bantu.Tables(0).Rows(i).Item("pt_desc1") & " is empty")
                            Exit Sub
                        End If

                        _dtrow("riud_ac_id") = dt_bantu.Rows(0).Item("pla_ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                        _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")


                    End If
                End If

                'sys 20110922
                If ds_bantu.Tables(0).Rows(i).Item("pbd_um") = ds_bantu.Tables(0).Rows(i).Item("pt_um") Then
                    _dtrow("riud_um_conv") = 1
                    _dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
                Else
                    Dim _um_conv As Double = 1
                    Try
                        Using objcek As New master_new.WDABasepgsql("", "")
                            With objcek
                                .Connection.Open()
                                .Command = .Connection.CreateCommand
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "select um_conv from um_mstr " + _
                                       " where um_pt_id = " + ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id").ToString + _
                                       " and um_pt_um = " + ds_bantu.Tables(0).Rows(i).Item("pbd_um").ToString + _
                                       " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id").ToString + ") "
                                .InitializeCommand()
                                .DataReader = .Command.ExecuteReader
                                While .DataReader.Read
                                    _um_conv = .DataReader("um_conv")
                                End While
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Exit Sub
                    End Try

                    _dtrow("riud_um_conv") = _um_conv
                    _dtrow("riud_qty_real") = _um_conv * ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("riud_cost") = _um_conv * ds_bantu.Tables(0).Rows(i).Item("invct_cost")



                End If
                '--

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            'fobject.ptsfr_pb_oid.enabled = False
            'fobject.ptsfr_so_oid.enabled = False
            'fobject.ptsfr_so_oid.text = ""
            fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True

        ElseIf fobject.name = "FWorkOrderIssue" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pb_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString


            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        '.SQL = "SELECT  " _
                        '    & "  pbd_oid, " _
                        '    & "  pbd_dom_id, " _
                        '    & "  pbd_en_id, " _
                        '    & "  pbd_add_by, " _
                        '    & "  pbd_add_date, " _
                        '    & "  pbd_upd_by, " _
                        '    & "  pbd_upd_date, " _
                        '    & "  pbd_pb_oid, " _
                        '    & "  pbd_seq, " _
                        '    & "  pbd_pt_id, " _
                        '    & "  pt_code,pt_pl_id, " _
                        '    & "  pt_desc1, " _
                        '    & "  pt_desc2, " _
                        '    & "  pt_ls, " _
                        '    & "  pt_cost, " _
                        '    & "  invct_cost, " _
                        '    & "  pbd_rmks, " _
                        '    & "  pbd_end_user, " _
                        '    & "  pbd_qty, " _
                        '    & "  pbd_qty_processed, " _
                        '    & "  pbd_qty_completed, " _
                        '    & "  pbd_qty - coalesce(pbd_qty_riud,0) as qty_open, " _
                        '    & "  pbd_um, pt_um, " _
                        '    & "  um_master.code_name as um_name, " _
                        '    & "  pbd_due_date, " _
                        '    & "  pbd_status, " _
                        '    & "  pbd_si_id, " _
                        '    & "  si_desc, " _
                        '    & "  pbd_dt " _
                        '    & "FROM  " _
                        '    & "  public.pbd_det " _
                        '    & "  inner join pb_mstr on pb_oid = pbd_pb_oid " _
                        '    & "  inner join pt_mstr on pt_id = pbd_pt_id " _
                        '    & "  inner join code_mstr um_master on um_master.code_id = pbd_um" _
                        '    & "  inner join invct_table on invct_pt_id = pbd_pt_id " _
                        '    & "  inner join si_mstr on si_id = invct_si_id " _
                        '    & "  where (pbd_qty - coalesce(pbd_qty_riud,0)) > 0 " _
                        '    & "  and pbd_pb_oid = '" + ds.Tables(0).Rows(_row_gv).Item("pb_oid") + "'"



                        .SQL = "SELECT  " _
                        & "  public.pbd_det.pbd_oid, " _
                        & "  public.pbd_det.pbd_dom_id, " _
                        & "  public.pbd_det.pbd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.pbd_det.pbd_si_id, " _
                        & "  public.si_mstr.si_desc, " _
                        & "  public.pbd_det.pbd_add_by, " _
                        & "  public.pbd_det.pbd_add_date, " _
                        & "  public.pbd_det.pbd_upd_by, " _
                        & "  public.pbd_det.pbd_upd_date, " _
                        & "  public.pbd_det.pbd_pb_oid, " _
                        & "  public.pbd_det.pbd_seq, " _
                        & "  public.pbd_det.pbd_pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2,pt_type,pt_ls,pt_cost, " _
                        & "  public.pbd_det.pbd_rmks, " _
                        & "  public.pbd_det.pbd_end_user, " _
                        & "  public.pbd_det.pbd_due_date, " _
                        & "  public.pbd_det.pbd_qty - coalesce(pbd_qty_riud,0) as pbd_qty, " _
                        & "  public.pbd_det.pbd_qty_processed, " _
                        & "  public.pbd_det.pbd_qty_completed, " _
                        & "  public.pbd_det.pbd_qty_riud, " _
                        & "  public.pbd_det.pbd_um, " _
                        & "  public.code_mstr.code_name, " _
                        & "  public.pbd_det.pbd_status, " _
                        & "  public.pbd_det.pbd_dt " _
                        & "  FROM " _
                        & "  public.pbd_det " _
                        & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                        & "  where pbd_det.pbd_pb_oid = '" _
                        & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pbd_det")
                    End With
                End Using

                For Each dr As DataRow In fobject.ds_edit.tables(0).rows
                    dr("wocid_qty") = 0.0
                Next

                If ds.Tables(0).Rows(_row_gv).Item("pb_is_unplan") = "Y" Then
                    fobject.ds_edit.tables(0).Rows.Clear()
                    fobject.woci_unplan.editvalue = True

                    Dim _dtrow As DataRow
                    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                        _dtrow = fobject.ds_edit.Tables("wocid_det").NewRow

                        _dtrow("wocid_wod_oid") = System.DBNull.Value
                        _dtrow("wod_pt_bom_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                        _dtrow("pt_id_wod") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                        _dtrow("wocid_pt_subs_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                        _dtrow("pt_code_wod") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                        _dtrow("pt_desc1_wod") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                        _dtrow("pt_desc2_wod") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                        _dtrow("code_name_wod") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                        _dtrow("pt_type_wod") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                        _dtrow("pt_ls_wod") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                        _dtrow("pt_cost_wod") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                        ' _dtrow("wocid_op") = ds_show.Tables("wod_det").Rows(i).Item("wod_op")
                        _dtrow("wod_qty_req") = 0.0
                        _dtrow("wod_comp") = System.DBNull.Value 'ds_show.Tables("wod_det").Rows(i).Item("wod_comp")
                        '_dtrow("wocid_qty") = 0.0 'ds_show.Tables("wod_det").Rows(i).Item("wod_qty_open")
                        _dtrow("wocid_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                        ' _dtrow("wocid_qty") = ds_show.Tables("wod_det").Rows(i).Item("wod_qty_open")
                        _dtrow("wocid_qty_open") = 0.0 'ds_show.Tables("wod_det").Rows(i).Item("wod_qty_open")

                        _dtrow("wocid_qty") = ds_bantu.Tables(0).Rows(i).Item("pbd_qty")
                        _dtrow("wocid_pbd_oid") = ds_bantu.Tables(0).Rows(i).Item("pbd_oid").ToString

                        'dr("wocid_qty") = ds_bantu.Tables(0).Rows(i).Item("pbd_qty")
                        'dr("wocid_pbd_oid") = ds_bantu.Tables(0).Rows(i).Item("pbd_oid")

                        _dtrow("wocid_si_id") = fobject.woci_si_id.EditValue
                        _dtrow("si_desc") = fobject.wo_si_id.EditValue
                        _dtrow("wocid_loc_id") = DBNull.Value 'ds_show.Tables("wod_det").Rows(i).Item("pt_loc_id")
                        _dtrow("loc_desc") = DBNull.Value ' ds_show.Tables("wod_det").Rows(0).Item("loc_desc")
                        _dtrow("wocid_wc_id") = DBNull.Value ' fobject.woci_wc_id.editvalue
                        _dtrow("wc_desc") = DBNull.Value ' fobject.woci_wc_id.text
                        fobject.ds_edit.Tables("wocid_det").Rows.Add(_dtrow)

                    Next

                    fobject.ds_edit.Tables("wocid_det").AcceptChanges()
                    fobject.gv_edit.BestFitColumns()

                Else
                    fobject.woci_unplan.editvalue = False

                    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                        For Each dr As DataRow In fobject.ds_edit.tables(0).rows
                            If dr("wod_pt_bom_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id") Then
                                dr("wocid_qty") = ds_bantu.Tables(0).Rows(i).Item("pbd_qty")
                                dr("wocid_pbd_oid") = ds_bantu.Tables(0).Rows(i).Item("pbd_oid").ToString
                            End If
                        Next
                    Next
                End If
                
                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gv_edit.BestFitColumns()
                'For i = 0 To fobject.ds_edit.tables(0).rows

                'Next
                'fobject.ds_edit.Tables(0).AcceptChanges()
                'fobject.gv_edit.BestFitColumns()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub
End Class
