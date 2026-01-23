Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FWOReceipt
    Dim ssql As String
    Dim _wo_oid_master As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    'Public __wor_wo_id As String
    Public __qty_outstanding As Double
    'Public __pt_id As String

    Dim _conf_value As String
    Public ds_edit As DataSet
    Public dt_serial As DataTable
    Public _pt_um As String
    Public _pjc_id As Integer
    Public _cost As Double


    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

        '_conf_value = func_coll.get_conf_file("wo_issue_to_wip")

        'If _conf_value = "1" Then
        '    lci_work_center.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Else
        '    lci_work_center.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'End If

        'If func_coll.get_conf_file("lock_location_wo_receive") = "1" Then
        '    wor_loc_id.Enabled = False
        'End If
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        wor_en_id.Properties.DataSource = dt_bantu
        wor_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        wor_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        wor_en_id.ItemIndex = 0

    End Sub

    Private Sub wor_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wor_en_id.EditValueChanged
        'init_le(wor_loc_id, "loc_mstr", wor_en_id.EditValue)
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(wor_en_id.EditValue))
        wor_si_id.Properties.DataSource = dt_bantu
        wor_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        wor_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        wor_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(wor_en_id.EditValue))
        wor_loc_id.Properties.DataSource = dt_bantu
        wor_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        wor_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        wor_loc_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_wc_mstr(wor_en_id.EditValue))
        'wor_wc_id.Properties.DataSource = dt_bantu
        'wor_wc_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
        'wor_wc_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
        'wor_wc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        '_conf_value = func_coll.get_conf_file("wo_issue_to_wip")
        'master
        add_column(gv_master, "wor_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO. Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO. Receipt Code", "wor_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Date", "wor_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Effective Date", "wor_date_eff", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Cost", "wor_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Qty Complete", "wor_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Reject", "wor_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Qty Inspection", "wor_qty_qc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Unit Measure", "unit_measure", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'If _conf_value = "1" Then
        '    add_column_copy(gv_master, "Work Center From", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'End If

        add_column_copy(gv_master, "Remarks", "wor_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wor_close", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "wor_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wor_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "wor_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wor_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_detail_serial, "wors_oid", False)
        add_column_copy(gv_detail_serial, "Serial/Lot Number", "wors_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "wors_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_serial, "wors_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "wors_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "wors_qty", DevExpress.Utils.HorzAlignment.Default)
    End Sub


    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.wor_oid, " _
                & "  a.wor_en_id, " _
                & "  e.en_desc, " _
                & "  a.wor_add_by, " _
                & "  a.wor_add_date, " _
                & "  a.wor_upd_by, " _
                & "  a.wor_upd_date, " _
                & "  a.wor_code, " _
                & "  a.wor_date, " _
                & "  a.wor_date_eff, " _
                & "  a.wor_wo_id, " _
                & "  b.wo_code, " _
                & "  b.wo_remarks, " _
                & "  a.wor_loc_id, " _
                & "  f.loc_code, " _
                & "  f.loc_desc, " _
                & "  a.wor_si_id, " _
                & "  g.si_code, " _
                & "  g.si_desc, " _
                & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
                & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
                & "  a.wor_remarks, " _
                & "  b.wo_remarks, " _
                & "  c.pt_code, " _
                & "  c.pt_id, " _
                & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
                & "  c.pt_um, " _
                & "  d.code_name AS unit_measure, " _
                & "  wo_pjc_id, " _
                & "  a.wor_close, pjc_code,wor_cost,wo_cost,wo_pt_id,en_code " _
                & "FROM " _
                & "  public.wor_mstr a " _
                & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
                & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
                & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
                & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
                & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
                & "  LEFT OUTER JOIN public.pjc_mstr ON pjc_id = wo_pjc_id " _
                & " Where wor_date between " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " AND a.wor_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " ORDER BY wor_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        wor_en_id.ItemIndex = 0
        'wor_code.EditValue = ""
        wor_date.DateTime = CekTanggal()
        wor_date_eff.DateTime = CekTanggal()
        wor_si_id.ItemIndex = 0
        wor_loc_id.ItemIndex = 0
        wor_wo_id.EditValue = ""
        '__wor_wo_id = ""
        wor_wo_id.Tag = ""
        wor_remarks.EditValue = ""
        wor_qty_comp.EditValue = 0
        wor_qty_reject.EditValue = 0

        pt_code.Tag = ""
        pt_code.Text = ""
        pt_desc1.Text = ""
        pt_desc2.Text = ""
        unit_measure.Text = ""
        pt_ls.Text = ""
        'wor_wc_id.ItemIndex = 0

        ssql = "SELECT  " _
            & "  a.wors_oid, " _
            & "  a.wors_wor_oid, " _
            & "  a.wors_qty, " _
            & "  a.wors_um, " _
            & "  d.code_code, " _
            & "  a.wors_si_id, " _
            & "  b.si_code, " _
            & "  a.wors_lot_serial, " _
            & "  a.wors_loc_id, " _
            & "  c.loc_code, " _
            & "  a.wors_dt " _
            & "FROM " _
            & "  public.wors_serial a " _
            & "  INNER JOIN public.si_mstr b ON (a.wors_si_id = b.si_id) " _
            & "  INNER JOIN public.loc_mstr c ON (a.wors_loc_id = c.loc_id) " _
            & "  INNER JOIN public.code_mstr d ON (a.wors_um = d.code_id) " _
            & "WHERE " _
            & "  a.wors_wor_oid is null"

        dt_serial = GetTableData(ssql)
        gc_serial.DataSource = dt_serial
        gv_serial.BestFitColumns()

        Try

            tcg_header.SelectedTabPageIndex = 0
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _wor_code As String = ""
        Dim _wor_oid As String = Guid.NewGuid.ToString
        Dim ssqls As New ArrayList

        _wor_code = func_coll.get_transaction_number("WR", wor_en_id.GetColumnValue("en_code"), "wor_mstr", "wor_code", func_coll.get_tanggal_sistem)

        'wor_code.EditValue = _wor_code

        Dim _en_id, _si_id, _loc_id, _pt_id, _tran_id As Integer
        Dim _qty, _cost_avg As Double
        Dim _serial As String

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
                                    & "  public.wor_mstr " _
                                    & "( " _
                                    & "  wor_oid, " _
                                    & "  wor_dom_id, " _
                                    & "  wor_en_id, " _
                                    & "  wor_add_by, " _
                                    & "  wor_add_date, " _
                                    & "  wor_code, " _
                                    & "  wor_date, " _
                                    & "  wor_date_eff, " _
                                    & "  wor_wo_id, " _
                                    & "  wor_loc_id, " _
                                    & "  wor_si_id, " _
                                    & "  wor_qty_comp, " _
                                    & "  wor_qty_reject,wor_cost, " _
                                    & "  wor_remarks, " _
                                    & "  wor_dt " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(_wor_oid) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(wor_en_id.EditValue) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & SetDateNTime(CekTanggal) & ",  " _
                                    & SetSetring(_wor_code) & ",  " _
                                    & SetDate(wor_date.DateTime) & ",  " _
                                    & SetDate(wor_date_eff.DateTime) & ",  " _
                                    & SetInteger(wor_wo_id.Tag) & ",  " _
                                    & SetInteger(wor_loc_id.EditValue) & ",  " _
                                    & SetInteger(wor_si_id.EditValue) & ",  " _
                                    & SetDecDB(wor_qty_comp.EditValue) & ",  " _
                                    & SetDecDB(wor_qty_reject.EditValue) & ",  " _
                                    & SetDecDB(wor_cost.EditValue) & ",  " _
                                    & SetSetring(wor_remarks.Text) & ",  " _
                                    & SetDate(CekTanggal) & "  " _
                                    & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        ' ''update ke wip
                        ''If _conf_value = "1" Then
                        ''    Dim _wo_oid As String = GetRowInfo("select wo_oid from wo_mstr where wo_id=" & __wor_wo_id)(0).ToString
                        ''    If func_coll.update_inv_wip(objinsert, wor_wc_id.EditValue, _
                        ''            __pt_id, wor_en_id.EditValue, _wo_oid, _
                        ''            (wor_qty_comp.EditValue + wor_qty_reject.EditValue) * -1) = False Then

                        ''        sqlTran.Rollback()
                        ''        Return False
                        ''        Exit Try
                        ''    End If
                        ''End If


                        'update ke WO
                        ssql = "UPDATE  " _
                            & "  public.wo_mstr  " _
                            & "SET  " _
                            & "  wo_qty_comp = coalesce(wo_qty_comp,0) + " & SetDecDB(wor_qty_comp.EditValue) & ",  " _
                            & "  wo_qty_rjc = coalesce(wo_qty_rjc,0) + " & SetDecDB(wor_qty_reject.EditValue) & "  " _
                            & "WHERE  " _
                            & "  wo_id = " & SetInteger(wor_wo_id.Tag) & " "


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = ssql
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'cek apakah sudah full, kalo sudah maka close dengan otomatis
                        If __qty_outstanding <= (wor_qty_comp.EditValue + wor_qty_reject.EditValue) Then
                            ssql = "UPDATE  " _
                                 & "  public.wo_mstr  " _
                                 & "SET  " _
                                 & "  wo_status = 'C', " _
                                 & "  wo_date_close=" & SetDateNTime(CekTanggal) _
                                 & "WHERE  " _
                                 & "  wo_id = " & SetInteger(wor_wo_id.Tag) & " "

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If


                        For Each dr As DataRow In dt_serial.Rows
                            ssql = "INSERT INTO  " _
                                    & "  public.wors_serial " _
                                    & "( " _
                                    & "  wors_oid, " _
                                    & "  wors_wor_oid, " _
                                    & "  wors_qty, " _
                                    & "  wors_um, " _
                                    & "  wors_si_id, " _
                                    & "  wors_lot_serial, " _
                                    & "  wors_loc_id, " _
                                    & "  wors_dt " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_wor_oid) & ",  " _
                                    & SetDecDB(dr("wors_qty")) & ",  " _
                                    & SetInteger(_pt_um) & ",  " _
                                    & SetInteger(wor_si_id.EditValue) & ",  " _
                                    & SetSetring(dr("wors_lot_serial")) & ",  " _
                                    & SetInteger(wor_loc_id.EditValue) & ",  " _
                                    & "current_timestamp" & "  " _
                                    & ")"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        'update ke stok
                        If SetString(pt_ls.EditValue.ToString).ToUpper <> "S" Then


                            _en_id = wor_en_id.EditValue
                            _si_id = wor_si_id.EditValue
                            _loc_id = wor_loc_id.EditValue
                            _pt_id = pt_code.Tag
                            _serial = "''"
                            _qty = wor_qty_comp.EditValue

                            'If func_coll.update_invc_mstr_plus(ssqls, objinsert, wor_en_id.EditValue, wor_si_id.EditValue, _
                            '                                   wor_loc_id.EditValue, _pjc_id, pt_code.Tag, "''", _
                            '                                   wor_qty_comp.EditValue + wor_qty_reject.EditValue) = False Then
                            '    sqlTran.Rollback()
                            '    insert = False
                            '    Exit Function
                            'End If

                            If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                            _en_id = wor_en_id.EditValue
                            _si_id = wor_si_id.EditValue
                            _loc_id = wor_loc_id.EditValue
                            _pt_id = pt_code.Tag
                            _serial = "''"
                            _qty = wor_qty_comp.EditValue
                            _tran_id = func_coll.get_id_tran_mstr("rct-wo")

                            '20120101 ini sudah gak dipake lagi ...cost avg
                            _cost_avg = 0 'func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost) 

                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 0, _en_id, _wor_code, _wor_oid, _
                                                          "WO Receipt", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _
                                                          _cost, _cost_avg, "", wor_date.DateTime) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                        Else
                            Dim x As Integer = 0
                            For Each dr As DataRow In dt_serial.Rows
                                If func_coll.update_invc_mstr_plus(ssqls, objinsert, wor_en_id.EditValue, _
                                                                   wor_si_id.EditValue, wor_loc_id.EditValue, _pjc_id, _
                                                                   pt_code.Tag, dr("wors_lot_serial"), dr("wors_qty")) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                _en_id = wor_en_id.EditValue
                                _si_id = wor_si_id.EditValue
                                _loc_id = wor_loc_id.EditValue
                                _pt_id = pt_code.Tag
                                _serial = dr("wors_lot_serial")
                                _qty = dr("wors_qty")
                                _tran_id = func_coll.get_id_tran_mstr("rct-wo")

                                '20120101 ini sudah gak dipake lagi ...cost avg
                                _cost_avg = 0 'func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)

                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, x, _en_id, _wor_code, _wor_oid, _
                                                              "WO Receipt", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _
                                                              _cost, _cost_avg, _serial, func_coll.get_tanggal_sistem) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If
                                x += 1
                            Next
                        End If



                        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
                        If _create_jurnal = True Then
                            'If SetNumber(wor_cost.EditValue) > 0.0 Then
                            If insert_glt_det_wo_receipt(ssqls, objinsert, ds_edit, _
                                             wor_en_id.EditValue, wor_en_id.GetColumnValue("en_code"), _
                                             _wor_oid.ToString, _wor_code, _
                                             wor_date.DateTime, _
                                             "WR", "ISS-WIP") = False Then

                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                            'Else
                            '    MsgBox("Cost can't blank")
                            '    insert = False
                            'End If

                        End If

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        insert = True
                        sqlTran.Commit()

                        after_success()
                        set_row(_wor_oid, "wor_oid")
                        'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        show_detail()
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

    Public Sub show_detail()
        Try
            If ds.Tables.Count = 0 Then
                gc_detail_serial.DataSource = Nothing
                Exit Sub
            End If
            If ds.Tables(0).Rows.Count = 0 Then
                gc_detail_serial.DataSource = Nothing
                Exit Sub
            End If
            Dim sSQL As String

            sSQL = "SELECT  " _
                    & "  a.wors_oid, " _
                    & "  a.wors_wor_oid, " _
                    & "  a.wors_qty, " _
                    & "  a.wors_um, " _
                    & "  d.code_code, " _
                    & "  a.wors_si_id, " _
                    & "  b.si_code, " _
                    & "  a.wors_lot_serial, " _
                    & "  a.wors_loc_id, " _
                    & "  c.loc_code, " _
                    & "  a.wors_dt " _
                    & "FROM " _
                    & "  public.wors_serial a " _
                    & "  INNER JOIN public.si_mstr b ON (a.wors_si_id = b.si_id) " _
                    & "  INNER JOIN public.loc_mstr c ON (a.wors_loc_id = c.loc_id) " _
                    & "  INNER JOIN public.code_mstr d ON (a.wors_um = d.code_id) " _
                    & "WHERE " _
                    & "  a.wors_wor_oid=" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_oid"))

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            gc_detail_serial.DataSource = dt
            gv_detail_serial.BestFitColumns()

            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function edit()

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        edit = True
        Dim ssqls As New ArrayList
        Try
            'ssql = "UPDATE  " _
            '    & "  public.wo_mstr   " _
            '    & "SET  " _
            '    & "  wo_en_id = " & SetInteger(wor_en_id.EditValue) & ",  " _
            '    & "  wo_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '    & "  wo_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
            '    & "  wo_code = " & SetSetring(wor_code.Text) & ",  " _
            '    & "  wo_date = " & SetSetring(wor_date.Text) & ",  " _
            '    & "  wo_date_rel = " & SetSetring(wo_date_rel.Text) & ",  " _
            '    & "  wo_date_due = " & SetSetring(wor_date_eff.Text) & ",  " _
            '    & "  wo_type = " & SetSetring(wo_type.Text) & ",  " _
            '    & "  wo_qty_ord = " & SetSetring(wo_qty_ord.Text) & ",  " _
            '    & "  wo_qty_comp = " & SetSetring(wo_qty_comp.Text) & ",  " _
            '    & "  wo_qty_rjc = " & SetSetring(wo_qty_rjc.Text) & ",  " _
            '    & "  wo_pt_id = " & SetSetring(wo_pt_id.Text) & ",  " _
            '    & "  wo_si_id = " & SetSetring(wo_si_id.Text) & ",  " _
            '    & "  wo_bom_id = " & SetSetring(wo_bom_id.Text) & ",  " _
            '    & "  wo_ro_id = " & SetSetring(wo_ro_id.Text) & ",  " _
            '    & "  wo_remarks = " & SetSetring(wo_remarks.Text) & ",  " _
            '    & "  wo_status = " & SetSetring(wo_status.Text) & ",  " _
            '    & "  wo_use_bom = " & SetSetring(wo_use_bom.Text) & "  " _
            '    & "  " _
            '    & "WHERE  " _
            '    & "  wo_oid = " & SetSetring(_wo_oid_master) & " "

            ssqls.Add(ssql)

            'If master_new.PGSqlConn.status_sync = True Then
            '    DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
            '    ssqls.Clear()
            'Else
            '    DbRunTran(ssqls, "")
            '    ssqls.Clear()
            'End If

            after_success()
            set_row(Trim(_wo_oid_master.ToString), "wo_oid")
            edit = True

        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
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

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then

            Dim _en_id, _si_id, _loc_id, _pt_id, _tran_id As Integer
            Dim _cost, _qty, _cost_avg As Double
            Dim _serial, _wor_code As String

            row = BindingContext(ds.Tables(0)).Position

            'If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
            '    row = row - 1
            'ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
            '    row = 0
            'End If

            _wor_code = ds.Tables(0).Rows(row).Item("wor_code")

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            ssql = "delete from wor_mstr where wor_oid = '" + ds.Tables(0).Rows(row).Item("wor_oid").ToString + "'"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            ssql = "UPDATE  " _
                                & "  public.wo_mstr  " _
                                & "SET  " _
                                & "  wo_status = 'R', " _
                                & "  wo_date_close=null, " _
                                & "  wo_qty_comp = coalesce(wo_qty_comp,0) - " & SetDecDB(ds.Tables(0).Rows(row).Item("wor_qty_comp")) & ",  " _
                                & "  wo_qty_rjc = coalesce(wo_qty_rjc,0) - " & SetDecDB(ds.Tables(0).Rows(row).Item("wor_qty_reject")) & "  " _
                                & "WHERE  " _
                                & "  wo_id = " & ds.Tables(0).Rows(row).Item("wor_wo_id") & " "

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            ''update ke wip
                            'If _conf_value = "1" Then
                            '    Dim _wo_oid As String = GetRowInfo("select wo_oid from wo_mstr where wo_id=" & ds.Tables(0).Rows(row).Item("wor_wo_id"))(0).ToString

                            '    If func_coll.update_inv_wip(objinsert, ds.Tables(0).Rows(row).Item("wor_wc_id"), _
                            '             ds.Tables(0).Rows(row).Item("pt_id"), ds.Tables(0).Rows(row).Item("wor_en_id"), _wo_oid, _
                            '            ds.Tables(0).Rows(row).Item("wor_qty_comp") + ds.Tables(0).Rows(row).Item("wor_qty_reject")) = False Then

                            '        sqlTran.Rollback()
                            '        Return False
                            '        Exit Try
                            '    End If
                            'End If

                            ssql = "SELECT  " _
                              & "  a.wors_oid, " _
                              & "  a.wors_wor_oid, " _
                              & "  a.wors_qty, " _
                              & "  a.wors_um, " _
                              & "  d.code_code, " _
                              & "  a.wors_si_id, " _
                              & "  b.si_code, " _
                              & "  a.wors_lot_serial, " _
                              & "  a.wors_loc_id, " _
                              & "  c.loc_code, " _
                              & "  a.wors_dt " _
                              & "FROM " _
                              & "  public.wors_serial a " _
                              & "  INNER JOIN public.si_mstr b ON (a.wors_si_id = b.si_id) " _
                              & "  INNER JOIN public.loc_mstr c ON (a.wors_loc_id = c.loc_id) " _
                              & "  INNER JOIN public.code_mstr d ON (a.wors_um = d.code_id) " _
                              & "WHERE " _
                              & "  a.wors_wor_oid=" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_oid"))


                            dt_serial = GetTableData(ssql)

                            'update ke stok
                            If SetString(ds.Tables(0).Rows(row).Item("pt_ls")).ToUpper <> "S" Then
                                If func_coll.update_invc_mstr_minus(ssqls, objinsert, ds.Tables(0).Rows(row).Item("wor_en_id"), ds.Tables(0).Rows(row).Item("wor_si_id"), ds.Tables(0).Rows(row).Item("wor_loc_id"), 0, ds.Tables(0).Rows(row).Item("pt_id"), "", "''", ds.Tables(0).Rows(row).Item("wor_qty_comp") + ds.Tables(0).Rows(row).Item("wor_qty_reject")) = False Then
                                    sqlTran.Rollback()
                                    delete_data = False
                                    Exit Function
                                End If

                                _en_id = ds.Tables(0).Rows(row).Item("wor_en_id")
                                _si_id = ds.Tables(0).Rows(row).Item("wor_si_id")
                                _loc_id = ds.Tables(0).Rows(row).Item("wor_loc_id")
                                _pjc_id = 0 'ds.Tables(0).Rows(row).Item("wo_pjc_id")
                                _pt_id = ds.Tables(0).Rows(row).Item("pt_id")
                                _serial = "''"
                                _qty = ds.Tables(0).Rows(row).Item("wor_qty_comp") * -1
                                _tran_id = func_coll.get_id_tran_mstr("rct-wo")

                                ssql = "SELECT  " _
                                    & "  a.invct_cost " _
                                    & "FROM " _
                                    & "  public.invct_table a " _
                                    & "WHERE " _
                                    & "  a.invct_dom_id = " & master_new.ClsVar.sdom_id & " AND  " _
                                    & "  a.invct_pt_id = " & _pt_id & " And " _
                                    & "  a.invct_en_id =" & _en_id & " AND  " _
                                    & "  a.invct_si_id =" & _si_id

                                Dim dt_cost As New DataTable

                                For Each dr_cost As DataRow In dt_cost.Rows
                                    _cost = dr_cost("invct_cost")
                                Next

                                '20120101 _cost_avg dah gak dipake di history report inventory
                                _cost_avg = 0 'func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)

                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 0, _en_id, _wor_code, ds.Tables(0).Rows(row).Item("wor_oid").ToString, "WO Receipt Delete", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _cost, _cost_avg, "", func_coll.get_tanggal_sistem) = False Then
                                    sqlTran.Rollback()
                                    delete_data = False
                                    Exit Function
                                End If

                            Else
                                Dim x As Integer = 0
                                For Each dr As DataRow In dt_serial.Rows
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, ds.Tables(0).Rows(row).Item("wor_en_id"), ds.Tables(0).Rows(row).Item("wor_si_id"), ds.Tables(0).Rows(row).Item("wor_loc_id"), ds.Tables(0).Rows(row).Item("wo_pjc_id"), ds.Tables(0).Rows(row).Item("pt_id"), dr("wors_lot_serial"), dr("wors_lot_serial"), dr("wors_qty")) = False Then
                                        sqlTran.Rollback()
                                        delete_data = False
                                        Exit Function
                                    End If

                                    _en_id = ds.Tables(0).Rows(row).Item("wor_en_id")
                                    _si_id = ds.Tables(0).Rows(row).Item("wor_si_id")
                                    _loc_id = ds.Tables(0).Rows(row).Item("wor_loc_id")
                                    _pjc_id = ds.Tables(0).Rows(row).Item("wo_pjc_id")
                                    _pt_id = ds.Tables(0).Rows(row).Item("pt_id")
                                    _serial = dr("wors_lot_serial")
                                    _qty = dr("wors_qty") * -1
                                    _tran_id = func_coll.get_id_tran_mstr("rct-wo")

                                    ssql = "SELECT  " _
                                        & "  a.invct_cost " _
                                        & "FROM " _
                                        & "  public.invct_table a " _
                                        & "WHERE " _
                                        & "  a.invct_dom_id = " & master_new.ClsVar.sdom_id & " AND  " _
                                        & "  a.invct_pt_id = " & _pt_id & " And " _
                                        & "  a.invct_en_id =" & _en_id & " AND  " _
                                        & "  a.invct_si_id =" & _si_id

                                    Dim dt_cost As New DataTable

                                    For Each dr_cost As DataRow In dt_cost.Rows
                                        _cost = dr_cost("invct_cost")
                                    Next

                                    '20120101 _cost_avg dah gak dipake lagi
                                    _cost_avg = 0 'func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)

                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, x, _en_id, _wor_code, ds.Tables(0).Rows(row).Item("wor_oid").ToString, "WO Receipt Delete", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _cost, _cost_avg, _serial, func_coll.get_tanggal_sistem) = False Then
                                        sqlTran.Rollback()
                                        delete_data = False
                                        Exit Function
                                    End If
                                    x += 1
                                Next
                            End If


                            Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
                            If _create_jurnal = True Then
                                'If SetNumber(ds.Tables(0).Rows(row).Item("wor_en_id") wor_cost.EditValue) > 0.0 Then
                                If delete_glt_det_wo_receipt(ssqls, objinsert, ds_edit, _
                                                 ds.Tables(0).Rows(row).Item("wor_en_id"), ds.Tables(0).Rows(row).Item("en_code"), _
                                                 ds.Tables(0).Rows(row).Item("wor_oid").ToString, ds.Tables(0).Rows(row).Item("wor_code"), _
                                                 master_new.PGSqlConn.CekTanggal, _
                                                 "WD", "ISS-WIP") = False Then

                                    sqlTran.Rollback()


                                    delete_data = False
                                    Exit Function
                                End If
                                'Else
                                '    MsgBox("Cost can't blank")
                                '    insert = False
                                'End If
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
                            delete_data = True
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_serial.AcceptChanges()

        Dim _qty_total As Double = 0

        If pt_ls.EditValue.ToString.ToUpper = "S" Then
            For i As Integer = 0 To dt_serial.Rows.Count - 1
                If dt_serial.Rows(i).Item("wors_qty") > 1 Then
                    MessageBox.Show("Serial : " + dt_serial.Rows(i).Item("wors_lot_serial") + " Have Wrong Serial Qty Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                    Exit Function
                End If
                _qty_total += dt_serial.Rows(i).Item("wors_qty")
            Next

            If _qty_total <> (wor_qty_comp.EditValue + wor_qty_reject.EditValue) Then
                MessageBox.Show("Qty receive not equal with qty serial.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
                Exit Function
            End If
        End If

        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub wo_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wor_wo_id.ButtonClick
        Try

            Dim frm As New FWOSearch
            frm.set_win(Me)
            'frm._obj = wor_wo_id
            frm._en_id = wor_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        show_detail()
    End Sub

    Private Sub gv_master_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.GotFocus
        show_detail()
    End Sub


    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            show_detail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_gen_serial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''Dim _dtrow As DataRow
        ''For _num_serial As Integer = te_serial_from.EditValue To te_serial_to.EditValue
        ''    If cek_avail_serial(Trim(te_serial_code.EditValue) & _num_serial) = True Then
        ''        MsgBox("Duplicate Serial Number : " & Trim(te_serial_code.EditValue) & _num_serial, MsgBoxStyle.Critical, "Duplicate")
        ''        Exit Sub
        ''    End If

        ''    _dtrow = dt_serial.NewRow
        ''    _dtrow("wors_oid") = Guid.NewGuid.ToString
        ''    _dtrow("wors_lot_serial") = Trim(te_serial_code.EditValue) & _num_serial
        ''    _dtrow("wors_qty") = 1
        ''    dt_serial.Rows.Add(_dtrow)

        ''Next
        ''dt_serial.AcceptChanges()

    End Sub

    Private Function cek_avail_serial(ByVal _par_serial As String) As Boolean
        cek_avail_serial = False
        Try
            For i As Integer = 0 To dt_serial.Rows.Count - 1
                If _par_serial = dt_serial.Rows(i).Item("wors_lot_serial").ToString Then
                    cek_avail_serial = True
                End If
            Next
        Catch
        End Try
        Return cek_avail_serial
    End Function


    Private Function delete_glt_det_wo_receipt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                ByVal par_oid As String, ByVal par_trans_code As String, _
                                ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean

        delete_glt_det_wo_receipt = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        _cost = SetNumber(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_cost"))
        If _cost = 0.0 Then
            _cost = SetNumber(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_cost"))
            'ssql = "select wo_cost from wo_mstr where wo_id=" & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_wo_id"))
            'Dim dt_cost As New DataTable
            'dt_cost = GetTableData(ssql)
            'For Each dr_cost As DataRow In dt_cost.Rows
            '    _cost = dr_cost(0)
            'Next
        End If

        If _cost = 0.0 Then
            Return True
            Exit Function
        End If


        With par_obj
            Try

                If (ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_qty_comp") + _
                    ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_qty_reject")) > 0 Then

                    dt_bantu = New DataTable


                    _pl_id = func_coll.get_prodline(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_pt_id"))
                    _cost = SetNumber(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_qty_comp") + _
                                      ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wor_qty_reject")) * SetNumber(_cost)

                    '********************** finish untuk yang debet
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
                    'dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

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
                         & SetSetringDB("Work Order Receipt Delete") & ",  " _
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
                    'dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
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
                         & SetSetring("Work Order Receipt") & ",  " _
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


    End Function

    Private Function insert_glt_det_wo_receipt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                ByVal par_oid As String, ByVal par_trans_code As String, _
                                ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean

        insert_glt_det_wo_receipt = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        Dim _nilai_kalkulasi As Double = SetNumber(wor_cost.EditValue)
        'Return False

        If _nilai_kalkulasi <= 0 Then
            Return True
            Exit Function

        End If

        'For i = 0 To par_ds.Tables(0).Rows.Count - 1
        '    _seq = _seq + 1

        With par_obj
            Try

                If (wor_qty_comp.EditValue + wor_qty_reject.EditValue) > 0 Then

                    dt_bantu = New DataTable
                    '_pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id_wod"))
                    '_cost = SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_qty")) * SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_cost"))


                    _pl_id = func_coll.get_prodline(pt_code.Tag)
                    _cost = SetNumber(wor_qty_comp.EditValue + wor_qty_reject.EditValue) * SetNumber(wor_cost.EditValue)

                    '********************** finish untuk yang debet
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

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
                                        & SetSetringDB("Work Order Receipt") & ",  " _
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

                    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
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
                                        & SetSetring("Work Order Receipt") & ",  " _
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



                ''===================================================================================

                'If par_ds.Tables(0).Rows(i).Item("wocid_qty") > 0 Then
                '    dt_bantu = New DataTable
                '    _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id_wod"))
                '    _cost = SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_qty")) * SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_cost"))

                '    '********************** finish untuk yang debet
                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_DET_ACC")

                '    'Insert Untuk Debet nya....
                '    _seq = _seq + 1
                '    .Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq + 1) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetringDB("Work Order Issue (Step 1)") & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    .Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "D") = False Then

                '        Return False
                '        Exit Function
                '    End If

                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")
                '    'Insert Untuk Yang credit 
                '    .Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetring("Work Order Issue (Step 1)") & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    .Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "C") = False Then

                '        Return False
                '        Exit Function
                '    End If


                '    'jurnal baris kedua
                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_HED_ACC")
                '    'Insert Untuk Debet nya....
                '    _seq = _seq + 1
                '    .Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq + 1) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetringDB("Work Order Issue (Step 2)") & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    .Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                      dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "D") = False Then

                '        Return False
                '        Exit Function
                '    End If

                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_DET_ACC")
                '    'Insert Untuk Yang credit 
                '    .Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetring("Work Order Issue (Step 2)") & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    .Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                       dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "C") = False Then

                '        Return False
                '        Exit Function
                '    End If


                '    'jurnal baris ketiga
                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
                '    'Insert Untuk Debet nya....
                '    _seq = _seq + 1
                '    .Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq + 1) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetringDB("Work Order Issue (Step 3)") & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    .Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                       dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "D") = False Then

                '        Return False
                '        Exit Function
                '    End If

                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_HED_ACC")
                '    'Insert Untuk Yang credit 
                '    .Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetring("Work Order Issue (Step 3)") & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    .Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "C") = False Then

                '        Return False
                '        Exit Function
                '    End If
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
        'Next
    End Function
    
End Class
