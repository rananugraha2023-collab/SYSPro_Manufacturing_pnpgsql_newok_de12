Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports System.Net

Public Class FWORelease
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _pt_id As Integer
    Public _oid_master As String
    Public _pt_code As String

    Private Sub FInventoryReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub
    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_detail, "wod_oid", False)
        'add_column(gv_detail, "wod_wo_oid", False)
        'add_column(gv_detail, "wod_pt_bom_id", False)
        'add_column_copy(gv_detail, "Component", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "PT/BOM Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Qty Per", "wod_qty_per", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Alloc", "wod_qty_alloc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Picked", "wod_qty_picked", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Issued", "wod_qty_issued", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Cost", "wod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub
    Public Overrides Sub preview()
        Dim ssql As String
        Dim sSQLs As New ArrayList
        Dim _pt_bom_cost As Double = 0

        ssql = "SELECT  psd_pt_bom_id,psd_comp,   " _
                & "psd_desc, psdstrname, psd_start_date, " _
                & "psd_end_date, psd_scrp_pct,par_pt_phantom,sum(psd_qty) as psd_qty,'' as psd_loc_desc,0 as psd_loc_qty,'' as psd_loc_serial " _
                & " from public.get_all_simulated('" & _pt_code & "',  " _
                & SetNumber(wo_qty_remaining.EditValue) & ", " & "'Y'" & "," _
                & SetNumber(le_entity.EditValue) & ",'Y', " & SetDate(CekTanggal) & ") " _
                & "Group by  psd_pt_bom_id,psd_comp,psd_desc, psdstrname, psd_start_date,psd_end_date, psd_scrp_pct,par_pt_phantom"

        Dim dt As New DataTable
        dt = GetTableData(ssql)

        ssql = "UPDATE  " _
           & "  public.wo_mstr   " _
           & "SET  " _
           & "  wo_status = 'R' ," _
           & "  wo_rel_date = " & SetDate(CekTanggal) _
           & " WHERE  " _
           & "  wo_code = '" & wo_code.EditValue.ToString & "'  "

        sSQLs.Add(ssql)

        ssql = "select * from wod_det where wod_wo_oid='" & _oid_master & "'"
        If CekRowSelect(ssql) = 0 Then

            For Each dr As DataRow In dt.Rows
                _pt_bom_cost = GetRowInfo("select coalesce(pt_cost,0) * " & SetDbl(wo_qty_remaining.EditValue) _
                                          & " as pt_cost from pt_mstr where pt_id=" & dr("psd_pt_bom_id"))(0)

                ssql = "INSERT INTO  " _
                        & "  public.wod_det " _
                        & "( " _
                        & "  wod_oid, " _
                        & "  wod_wo_oid, " _
                        & "  wod_use_bom, " _
                        & "  wod_pt_bom_id, " _
                        & "  wod_comp, " _
                        & "  wod_op, " _
                        & "  wod_qty_per, " _
                        & "  wod_qty_req, " _
                        & "  wod_qty_alloc, " _
                        & "  wod_qty_picked, " _
                        & "  wod_qty_issued, " _
                        & "  wod_cost, " _
                        & "  wod_dt " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_oid_master) & ",  " _
                        & SetSetring("N") & ",  " _
                        & SetInteger(dr("psd_pt_bom_id")) & ",  " _
                        & SetSetring(dr("psd_comp")) & ",  " _
                        & SetDbl(0) & ",  " _
                        & SetDbl(dr("psd_qty") / wo_qty_remaining.EditValue) & ",  " _
                        & SetDbl(dr("psd_qty")) & ",  " _
                        & " 0,  " _
                        & " 0,  " _
                        & " 0,  " _
                        & SetDbl(_pt_bom_cost) & ",  " _
                        & SetDate(CekTanggal) _
                        & ")"

                sSQLs.Add(ssql)
            Next
        End If

        DbRunTran(sSQLs)
        'Exit Sub
        ' show_detail()
        print_pick_list()
    End Sub

    Private Sub show_detail()
        Try
            Dim sSQL As String

            sSQL = "SELECT  " _
                & "  a.wod_oid, " _
                & "  a.wod_wo_oid, " _
                & "  a.wod_pt_bom_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  a.wod_op, " _
                & "  a.wod_qty_per, " _
                & "  a.wod_qty_req, " _
                & "  a.wod_qty_alloc, " _
                & "  a.wod_qty_picked, " _
                & "  a.wod_qty_issued, " _
                & "  a.wod_cost " _
                & "FROM " _
                & "  public.wod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                & " WHERE wod_wo_oid='" & _oid_master & "' " _
                & "ORDER BY " _
                & "  a.wod_op"

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            'gc_detail.DataSource = dt
            'gv_detail.BestFitColumns()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub print_pick_list()
        Try
            Dim sSQL As String
            sSQL = "SELECT  " _
                & "  a.wod_oid, " _
                & "  a.wod_wo_oid, " _
                & "  a.wod_pt_bom_id, " _
                & "  b.pt_code AS wod_pt_code, " _
                & "  trim(b.pt_desc1 || b.pt_desc2) AS wod_pt_desc1, " _
                & "  a.wod_op, " _
                & "  a.wod_qty_per, " _
                & "  a.wod_qty_req, " _
                & "  a.wod_qty_alloc, " _
                & "  a.wod_qty_picked, " _
                & "  a.wod_qty_issued, " _
                & "  a.wod_cost, " _
                & "  coalesce(a.wod_qty_req, 0) - coalesce(a.wod_qty_issued, 0) AS wod_qty_remaining,'' as wod_loc_desc,0 as wod_loc_qty,'' as wod_loc_serial, " _
                & "  c.wo_oid, " _
                & "  c.wo_code, " _
                & "  c.wo_pt_id, " _
                & "  d.pt_code AS wo_pt_code, " _
                & "  d.pt_desc1 AS wo_pt_desc1, " _
                & "  c.wo_rel_date, " _
                & "  c.wo_due_date, " _
                & "  e.ro_code, " _
                & "  e.ro_desc, " _
                & "  c.wo_status, " _
                & "  c.wo_remarks, " _
                & "  c.wo_qty_comp, " _
                & "  c.wo_qty_ord, " _
                & "  coalesce(wo_qty_ord, 0) - coalesce(wo_qty_comp, 0) AS wo_qty_remaining " _
                & "FROM " _
                & "  public.wod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                & "  INNER JOIN public.wo_mstr c ON (a.wod_wo_oid = c.wo_oid) " _
                & "  INNER JOIN public.pt_mstr d ON (c.wo_pt_id = d.pt_id) " _
                & "  INNER JOIN public.ro_mstr e ON (c.wo_ro_id = e.ro_id) " _
                & "WHERE " _
                & "  c.wo_oid = '" & _oid_master & "'"

            Dim dt As New DataTable
            dt = GetTableData(sSQL)


            Dim dt_temp As New DataTable

            dt_temp = dt.Copy
            dt_temp.Rows.Clear()

            Dim _dtrow As DataRow
            For Each dr As DataRow In dt.Rows
                If ce_zero.EditValue = True And dr("wod_qty_remaining") = 0 Then
                    _dtrow = dt_temp.NewRow


                    _dtrow("wod_oid") = dr("wod_oid").ToString
                    _dtrow("wod_wo_oid") = dr("wod_wo_oid").ToString
                    _dtrow("wod_pt_bom_id") = dr("wod_pt_bom_id")
                    _dtrow("wod_pt_code") = dr("wod_pt_code")
                    _dtrow("wod_pt_desc1") = dr("wod_pt_desc1")
                    _dtrow("wod_op") = dr("wod_op")
                    _dtrow("wod_qty_per") = dr("wod_qty_per")
                    _dtrow("wod_qty_req") = dr("wod_qty_req")
                    _dtrow("wod_qty_alloc") = dr("wod_qty_alloc")
                    _dtrow("wod_qty_picked") = dr("wod_qty_picked")
                    _dtrow("wod_qty_issued") = dr("wod_qty_issued")
                    _dtrow("wod_cost") = dr("wod_cost")
                    _dtrow("wod_qty_remaining") = dr("wod_qty_remaining")
                    _dtrow("wo_oid") = dr("wo_oid").ToString
                    _dtrow("wo_code") = dr("wo_code")
                    _dtrow("wo_pt_id") = dr("wo_pt_id")
                    _dtrow("wo_pt_code") = dr("wo_pt_code")
                    _dtrow("wo_pt_desc1") = dr("wo_pt_desc1")
                    _dtrow("wo_rel_date") = dr("wo_rel_date")
                    _dtrow("wo_due_date") = dr("wo_due_date")
                    _dtrow("ro_code") = dr("ro_code")
                    _dtrow("ro_desc") = dr("ro_desc")
                    _dtrow("wo_status") = dr("wo_status")
                    _dtrow("wo_remarks") = dr("wo_remarks")
                    _dtrow("wo_qty_comp") = dr("wo_qty_comp")
                    _dtrow("wo_qty_ord") = dr("wo_qty_ord")
                    _dtrow("wo_qty_remaining") = dr("wo_qty_remaining")

                    _dtrow("wod_loc_desc") = "Not Available"
                    _dtrow("wod_loc_qty") = dr("wod_qty_remaining")
                    _dtrow("wod_loc_serial") = ""

                    dt_temp.Rows.Add(_dtrow)
                    dt_temp.AcceptChanges()

                Else
                    If dr("wod_qty_remaining") > 0 Then


                        sSQL = "SELECT  " _
                           & "  y.loc_desc,x.invc_qty,x.invc_serial " _
                           & "FROM " _
                           & "  public.invc_mstr x " _
                           & "  INNER JOIN public.loc_mstr y ON (x.invc_loc_id = y.loc_id) " _
                           & "  INNER JOIN public.is_mstr z ON (y.loc_is_id = z.is_id)  " _
                           & "   WHERE " _
                           & "  z.is_avail = 'Y' AND x.invc_qty > 0 AND " _
                           & "  x.invc_en_id = " & SetInteger(le_entity.EditValue) & " AND  " _
                           & "  x.invc_pt_id = " & dr("wod_pt_bom_id") & " order by x.invc_qty, x.invc_serial"

                        Dim dt_loc As New DataTable
                        dt_loc = GetTableData(sSQL)
                        Dim _qty_temp As Double = 0
                        Dim _qty_all As Double = dr("wod_qty_remaining")
                        If dt_loc.Rows.Count > 0 Then


                            For Each dr_loc As DataRow In dt_loc.Rows

                                If _qty_temp < _qty_all Then
                                    _dtrow = dt_temp.NewRow

                                    '==========================
                                    _dtrow("wod_oid") = dr("wod_oid").ToString
                                    _dtrow("wod_wo_oid") = dr("wod_wo_oid").ToString
                                    _dtrow("wod_pt_bom_id") = dr("wod_pt_bom_id")
                                    _dtrow("wod_pt_code") = dr("wod_pt_code")
                                    _dtrow("wod_pt_desc1") = dr("wod_pt_desc1")
                                    _dtrow("wod_op") = dr("wod_op")
                                    _dtrow("wod_qty_per") = dr("wod_qty_per")
                                    _dtrow("wod_qty_req") = dr("wod_qty_req")
                                    _dtrow("wod_qty_alloc") = dr("wod_qty_alloc")
                                    _dtrow("wod_qty_picked") = dr("wod_qty_picked")
                                    _dtrow("wod_qty_issued") = dr("wod_qty_issued")
                                    _dtrow("wod_cost") = dr("wod_cost")
                                    _dtrow("wod_qty_remaining") = dr("wod_qty_remaining")
                                    _dtrow("wo_oid") = dr("wo_oid")
                                    _dtrow("wo_code") = dr("wo_code")
                                    _dtrow("wo_pt_id") = dr("wo_pt_id")
                                    _dtrow("wo_pt_code") = dr("wo_pt_code")
                                    _dtrow("wo_pt_desc1") = dr("wo_pt_desc1")
                                    _dtrow("wo_rel_date") = dr("wo_rel_date")
                                    _dtrow("wo_due_date") = dr("wo_due_date")
                                    _dtrow("ro_code") = dr("ro_code")
                                    _dtrow("ro_desc") = dr("ro_desc")
                                    _dtrow("wo_status") = dr("wo_status")
                                    _dtrow("wo_remarks") = dr("wo_remarks")
                                    _dtrow("wo_qty_comp") = dr("wo_qty_comp")
                                    _dtrow("wo_qty_ord") = dr("wo_qty_ord")
                                    _dtrow("wo_qty_remaining") = dr("wo_qty_remaining")

                                    _dtrow("wod_loc_desc") = dr_loc("loc_desc")

                                    If (_qty_all - _qty_temp) < dr_loc("invc_qty") Then
                                        _dtrow("wod_loc_qty") = _qty_all - _qty_temp
                                    Else
                                        _dtrow("wod_loc_qty") = dr_loc("invc_qty")
                                    End If
                                    _dtrow("wod_loc_serial") = dr_loc("invc_serial")

                                    '==========================



                                    _qty_temp = _qty_temp + dr_loc("invc_qty")
                                    dt_temp.Rows.Add(_dtrow)
                                    dt_temp.AcceptChanges()

                                Else
                                    Exit For
                                End If
                            Next
                            If _qty_temp < _qty_all Then
                                _dtrow = dt_temp.NewRow

                                _dtrow("wod_oid") = dr("wod_oid").ToString
                                _dtrow("wod_wo_oid") = dr("wod_wo_oid").ToString
                                _dtrow("wod_pt_bom_id") = dr("wod_pt_bom_id")
                                _dtrow("wod_pt_code") = dr("wod_pt_code")
                                _dtrow("wod_pt_desc1") = dr("wod_pt_desc1")
                                _dtrow("wod_op") = dr("wod_op")
                                _dtrow("wod_qty_per") = dr("wod_qty_per")
                                _dtrow("wod_qty_req") = dr("wod_qty_req")
                                _dtrow("wod_qty_alloc") = dr("wod_qty_alloc")
                                _dtrow("wod_qty_picked") = dr("wod_qty_picked")
                                _dtrow("wod_qty_issued") = dr("wod_qty_issued")
                                _dtrow("wod_cost") = dr("wod_cost")
                                _dtrow("wod_qty_remaining") = dr("wod_qty_remaining")
                                _dtrow("wo_oid") = dr("wo_oid").ToString
                                _dtrow("wo_code") = dr("wo_code")
                                _dtrow("wo_pt_id") = dr("wo_pt_id")
                                _dtrow("wo_pt_code") = dr("wo_pt_code")
                                _dtrow("wo_pt_desc1") = dr("wo_pt_desc1")
                                _dtrow("wo_rel_date") = dr("wo_rel_date")
                                _dtrow("wo_due_date") = dr("wo_due_date")
                                _dtrow("ro_code") = dr("ro_code")
                                _dtrow("ro_desc") = dr("ro_desc")
                                _dtrow("wo_status") = dr("wo_status")
                                _dtrow("wo_remarks") = dr("wo_remarks")
                                _dtrow("wo_qty_comp") = dr("wo_qty_comp")
                                _dtrow("wo_qty_ord") = dr("wo_qty_ord")
                                _dtrow("wo_qty_remaining") = dr("wo_qty_remaining")

                                _dtrow("wod_loc_desc") = "Not Available"
                                _dtrow("wod_loc_qty") = _qty_all - _qty_temp
                                _dtrow("wod_loc_serial") = ""

                                dt_temp.Rows.Add(_dtrow)
                                dt_temp.AcceptChanges()

                            End If

                        Else

                            _dtrow = dt_temp.NewRow


                            _dtrow("wod_oid") = dr("wod_oid").ToString
                            _dtrow("wod_wo_oid") = dr("wod_wo_oid").ToString
                            _dtrow("wod_pt_bom_id") = dr("wod_pt_bom_id")
                            _dtrow("wod_pt_code") = dr("wod_pt_code")
                            _dtrow("wod_pt_desc1") = dr("wod_pt_desc1")
                            _dtrow("wod_op") = dr("wod_op")
                            _dtrow("wod_qty_per") = dr("wod_qty_per")
                            _dtrow("wod_qty_req") = dr("wod_qty_req")
                            _dtrow("wod_qty_alloc") = dr("wod_qty_alloc")
                            _dtrow("wod_qty_picked") = dr("wod_qty_picked")
                            _dtrow("wod_qty_issued") = dr("wod_qty_issued")
                            _dtrow("wod_cost") = dr("wod_cost")
                            _dtrow("wod_qty_remaining") = dr("wod_qty_remaining")
                            _dtrow("wo_oid") = dr("wo_oid").ToString
                            _dtrow("wo_code") = dr("wo_code")
                            _dtrow("wo_pt_id") = dr("wo_pt_id")
                            _dtrow("wo_pt_code") = dr("wo_pt_code")
                            _dtrow("wo_pt_desc1") = dr("wo_pt_desc1")
                            _dtrow("wo_rel_date") = dr("wo_rel_date")
                            _dtrow("wo_due_date") = dr("wo_due_date")
                            _dtrow("ro_code") = dr("ro_code")
                            _dtrow("ro_desc") = dr("ro_desc")
                            _dtrow("wo_status") = dr("wo_status")
                            _dtrow("wo_remarks") = dr("wo_remarks")
                            _dtrow("wo_qty_comp") = dr("wo_qty_comp")
                            _dtrow("wo_qty_ord") = dr("wo_qty_ord")
                            _dtrow("wo_qty_remaining") = dr("wo_qty_remaining")

                            _dtrow("wod_loc_desc") = "Not Available"
                            _dtrow("wod_loc_qty") = dr("wod_qty_remaining")
                            _dtrow("wod_loc_serial") = ""

                            dt_temp.Rows.Add(_dtrow)
                            dt_temp.AcceptChanges()

                        End If

                    End If
                End If

            Next

            Dim rpt As New rptWOPickList
            Try
                With rpt

                    If dt_temp.Rows.Count = 0 Then
                        MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    .DataSource = dt_temp
                    .DataMember = "Table1"
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                    'printTool.PreviewForm.Text = "Personal Selling SDI Report"
                    printTool.ShowPreview()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub wo_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_code.ButtonClick

        'SimpleButton1_Click(Nothing, Nothing)

        Dim frm As New FWOSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _user As String
        Dim _host As String
        Dim _ip As String
        Dim _ip_public As String
        Dim _company As String

        Try
            _user = SystemInformation.UserName
            _host = System.Net.Dns.GetHostName
            _ip = IPAddresses(_host)
            _company = ""

            Dim ssql As String
            ssql = "SELECT  " _
                & "  max(a.cmaddr_name) as company " _
                & "FROM " _
                & "  public.cmaddr_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.cmaddr_en_id = b.en_id) " _
                & "WHERE " _
                & "  b.en_active = 'Y'"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                _company = SetString(dr(0))
            Next


            Dim ip As New WebClient
            _ip_public = ip.DownloadString("http://192.168.1.150:81/cekip.php")

            Dim sURL As String
            sURL = "http://localhost:81/info/upgrade_info.php?"

            sURL = sURL & "id=" & Guid.NewGuid.ToString & "&"
            sURL = sURL & "company_address=" & _company & "&"
            sURL = sURL & "ip_local=" & _ip & "&"
            sURL = sURL & "ip_public=" & _ip_public & "&"
            sURL = sURL & "computer_name=" & _host & "&"
            sURL = sURL & "user_name=" & _user

            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(sURL)
            Dim response As WebResponse = wrGETURL.GetResponse()

            Box("success")
        Catch ex As Exception
            Pesan(Err)
            'Return ""
        End Try
    End Sub
End Class
