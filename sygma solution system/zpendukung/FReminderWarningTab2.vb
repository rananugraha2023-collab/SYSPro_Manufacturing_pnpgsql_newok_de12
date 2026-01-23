Imports master_new.ModFunction
Public Class FReminderWarningTab2

    Private Sub FReminderWarningTab2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        load_data_many(True)
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_wf, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_wf, "Sales Order Number", "so_code", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_wf, "Sales Order Date", "so_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "D")
        add_column_copy(gv_wf, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Near)

        add_column_copy(gv_ir, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_ir, "IR Number", "pb_code", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_ir, "IR Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_ir, "Partner", "pb_requested", DevExpress.Utils.HorzAlignment.Near)

        add_column_copy(gv_vc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "PO Date", "po_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_vc, "Vendor", "ptnr_name", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "Down Payment", "podtp_down_pay", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "DP Date", "dp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_vc, "Credit Term", "code_name", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "TDB", "tbd", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "Rcv Number", "rcv_code", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "Rcv Date", "rcv_eff_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_vc, "AP Number", "ap_code", DevExpress.Utils.HorzAlignment.Near)
        'add_column_copy(gv_vc, "AP Date", "ap_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "D")
        add_column_copy(gv_vc, "Voucher Code", "ap_code", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_vc, "Voucher Date", "ap_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_vc, "Due Date by DP", "duedated_by_dp", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_vc, "Due Date by rcv", "duedated_by_rcv", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_vc, "Payment Date", "pb_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "D")
        'add_column_copy(gv_vc, "Due Date", "pb_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "D")
        add_column_copy(gv_vc, "Over Due", "overdue", DevExpress.Utils.HorzAlignment.Near)

    End Sub

    'Public Overrides Sub load_data_many(ByVal arg As Boolean)
    '    Cursor = Cursors.WaitCursor
    '    If arg <> False Then
    '        ================================================================
    '        Try
    '            ds = New DataSet
    '            Using objload As New master_new.WDABasepgsql("", "")
    '                With objload
    '                    If xtc_master.SelectedTabPageIndex = 0 Then
    '                        load_detail(objload)
    '                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
    '                        load_ir(objload)
    '                    ElseIf xtc_master.SelectedTabPageIndex = 2 Then
    '                        load_sales_person(objload)
    '                    ElseIf xtc_master.SelectedTabPageIndex = 3 Then
    '                        load_sales_by_so(objload)
    '                    ElseIf xtc_master.SelectedTabPageIndex = 4 Then
    '                        load_cash_credit(objload)
    '                    End If
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If
    '    Cursor = Cursors.Arrow
    'End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================

            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        '.SQL = "SELECT DISTINCT " _
                        '        & "  a.soship_code, " _
                        '        & "  a.soship_date, " _
                        '        & "  b.so_code, " _
                        '        & "  c.ptnr_name, " _
                        '        & "  b.so_date, " _
                        '        & "  b.so_en_id,en_desc " _
                        '        & "FROM " _
                        '        & "  public.soship_mstr a " _
                        '        & "  INNER JOIN public.so_mstr b ON (a.soship_so_oid = b.so_oid) " _
                        '        & "  INNER JOIN public.soshipd_det d ON (a.soship_oid = d.soshipd_soship_oid) " _
                        '        & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                        '        & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                        '        & "WHERE " _
                        '        & "  d.soshipd_close_line = 'N' AND  " _
                        '        & "  d.soshipd_qty *-1 > " _
                        '        & "  coalesce(d.soshipd_qty_inv, 0) " _
                        '        & "  and soship_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -3, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                        '        & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') " _
                        '        & "  order by soship_date"

                        .SQL = "SELECT distinct " _
                            & "  b.so_code, " _
                            & "  c.ptnr_name, " _
                            & "  b.so_date,en_desc " _
                            & "FROM " _
                            & "  public.so_mstr b " _
                            & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                            & "  INNER JOIN public.sod_det a ON (b.so_oid = a.sod_so_oid) " _
                            & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                              & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            & "WHERE " _
                            & " coalesce(sod_qty_shipment,0) > coalesce(sod_qty_invoice,0) " _
                            & " and so_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -18, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                            & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') and pay_type.code_usr_1 <> '0'" _
                            & "  order by so_date desc"


                        .InitializeCommand()
                        .FillDataSet(ds, "wf")
                        gc_wf.DataSource = ds.Tables("wf")

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        '.SQL = "SELECT DISTINCT " _
                        '        & "  a.soship_code, " _
                        '        & "  a.soship_date, " _
                        '        & "  b.so_code, " _
                        '        & "  c.ptnr_name, " _
                        '        & "  b.so_date, " _
                        '        & "  b.so_en_id,en_desc " _
                        '        & "FROM " _
                        '        & "  public.soship_mstr a " _
                        '        & "  INNER JOIN public.so_mstr b ON (a.soship_so_oid = b.so_oid) " _
                        '        & "  INNER JOIN public.soshipd_det d ON (a.soship_oid = d.soshipd_soship_oid) " _
                        '        & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                        '        & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                        '        & "WHERE " _
                        '        & "  d.soshipd_close_line = 'N' AND  " _
                        '        & "  d.soshipd_qty *-1 > " _
                        '        & "  coalesce(d.soshipd_qty_inv, 0) " _
                        '        & "  and soship_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -3, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                        '        & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') " _
                        '        & "  order by soship_date"

                        .SQL = "SELECT DISTINCT  " _
                            & "  public.pb_mstr.pb_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pb_mstr.pb_code, " _
                            & "  public.pb_mstr.pb_requested, " _
                            & "  public.pb_mstr.pb_date " _
                            & "FROM " _
                            & "  public.pb_mstr " _
                            & "  INNER JOIN public.pbd_det ON (public.pb_mstr.pb_oid = public.pbd_det.pbd_pb_oid) " _
                            & "  INNER JOIN public.en_mstr ON (public.pb_mstr.pb_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                            & "WHERE " _
                            & "  pb_status IS NULL " _
                            & " and pb_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -18, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                            & "  and pb_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') " _
                            & "ORDER BY " _
                            & "  public.pb_mstr.pb_en_id, " _
                            & "  public.pb_mstr.pb_date"

                        .InitializeCommand()
                        .FillDataSet(ds, "ir")
                        gc_ir.DataSource = ds.Tables("ir")

                        bestfit_column()
                        ConditionsAdjustment()

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        '.SQL = "SELECT DISTINCT " _
                        '        & "  a.soship_code, " _
                        '        & "  a.soship_date, " _
                        '        & "  b.so_code, " _
                        '        & "  c.ptnr_name, " _
                        '        & "  b.so_date, " _
                        '        & "  b.so_en_id,en_desc " _
                        '        & "FROM " _
                        '        & "  public.soship_mstr a " _
                        '        & "  INNER JOIN public.so_mstr b ON (a.soship_so_oid = b.so_oid) " _
                        '        & "  INNER JOIN public.soshipd_det d ON (a.soship_oid = d.soshipd_soship_oid) " _
                        '        & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                        '        & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                        '        & "WHERE " _
                        '        & "  d.soshipd_close_line = 'N' AND  " _
                        '        & "  d.soshipd_qty *-1 > " _
                        '        & "  coalesce(d.soshipd_qty_inv, 0) " _
                        '        & "  and soship_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -3, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                        '        & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') " _
                        '        & "  order by soship_date"

                        .SQL = "SELECT DISTINCT public.po_mstr.po_en_id, " _
                            & "public.en_mstr.en_desc, " _
                            & "public.po_mstr.po_code, " _
                            & "public.po_mstr.po_date, " _
                            & "public.po_mstr.po_ptnr_id, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "public.pod_term_payment.podtp_down_pay, " _
                            & "CASE WHEN public.pod_term_payment.podtp_down_pay = 'Y' THEN (public.pod_term_payment.podtp_date) ELSE NULL END AS dp_date, " _
                            & "public.po_mstr.po_credit_term, " _
                            & "public.code_mstr.code_name, " _
                            & "coalesce(public.code_mstr.code_usr_1) AS tbd, " _
                            & "public.rcv_mstr.rcv_code, " _
                            & "public.rcv_mstr.rcv_eff_date, " _
                            & "public.ap_mstr.ap_code, " _
                            & "public.ap_mstr.ap_date, " _
                            & "(public.rcv_mstr.rcv_eff_date +(public.code_mstr.code_usr_1::integer)) AS duedated_by_rcv, " _
                            & "CASE WHEN public.pod_term_payment.podtp_down_pay = 'Y' THEN (public.pod_term_payment.podtp_date +(public.code_mstr.code_usr_1::integer)) END AS duedated_by_dp, " _
                            & "public.appayd_det.appayd_ap_oid, " _
                            & "public.appayd_det.appayd_ap_ref, " _
                            & "CURRENT_DATE -(public.rcv_mstr.rcv_eff_date +(public.code_mstr.code_usr_1::integer)) AS overdue, " _
                            & "public.pod_term_payment.podtp_date " _
                            & "FROM public.po_mstr " _
                            & "LEFT OUTER JOIN public.rcv_mstr ON (public.po_mstr.po_oid = public.rcv_mstr.rcv_po_oid) " _
                            & "LEFT OUTER JOIN public.app_po ON (public.po_mstr.po_oid = public.app_po.app_po_oid) " _
                            & "LEFT OUTER JOIN public.ap_mstr ON (public.app_po.app_ap_oid = public.ap_mstr.ap_oid) " _
                            & "INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id) " _
                            & "INNER JOIN public.code_mstr ON (public.po_mstr.po_credit_term = public.code_mstr.code_id) " _
                            & "LEFT OUTER JOIN public.appayd_det ON (public.ap_mstr.ap_oid = public.appayd_det.appayd_ap_oid) " _
                            & "INNER JOIN public.pod_term_payment ON (public.po_mstr.po_oid = public.pod_term_payment.podtp_po_oid) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "WHERE po_date >= '01/01/2022' AND " _
                            & "public.appayd_det.appayd_ap_oid IS NULL AND " _
                            & "(public.rcv_mstr.rcv_eff_date +(public.code_mstr.code_usr_1::integer)) - INTERVAL '7 days' <= CURRENT_DATE AND " _
                            & "public.code_mstr.code_name <> 'CASH' " _
                            & "ORDER BY public.po_mstr.po_date "

                        .InitializeCommand()
                        .FillDataSet(ds, "ap")
                        gc_vc.DataSource = ds.Tables("ap")

                        bestfit_column()
                        ConditionsAdjustment()

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    'Private Sub load_detail(ByVal par_obj As Object)
    '    With par_obj
    '        .SQL = "SELECT distinct " _
    '            & "  b.so_code, " _
    '            & "  c.ptnr_name, " _
    '            & "  b.so_date,en_desc " _
    '            & "FROM " _
    '            & "  public.so_mstr b " _
    '            & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
    '            & "  INNER JOIN public.sod_det a ON (b.so_oid = a.sod_so_oid) " _
    '            & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
    '              & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
    '            & "WHERE " _
    '            & " coalesce(sod_qty_shipment,0) > coalesce(sod_qty_invoice,0) " _
    '            & " and so_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -18, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
    '            & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') and pay_type.code_usr_1 <> '0'" _
    '            & "  order by so_date desc"


    '        .InitializeCommand()
    '        .FillDataSet(ds, "wf")
    '        gc_wf.DataSource = ds.Tables("wf")

    '        bestfit_column()
    '        ConditionsAdjustment()
    '    End With
    'End Sub

    'Private Sub load_ir(ByVal par_obj As Object)
    '    With par_obj
    '        .SQL = "SELECT distinct " _
    '            & "  b.so_code, " _
    '            & "  c.ptnr_name, " _
    '            & "  b.so_date,en_desc " _
    '            & "FROM " _
    '            & "  public.so_mstr b " _
    '            & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
    '            & "  INNER JOIN public.sod_det a ON (b.so_oid = a.sod_so_oid) " _
    '            & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
    '              & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
    '            & "WHERE " _
    '            & " coalesce(sod_qty_shipment,0) > coalesce(sod_qty_invoice,0) " _
    '            & " and so_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -18, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
    '            & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') and pay_type.code_usr_1 <> '0'" _
    '            & "  order by so_date desc"


    '        .InitializeCommand()
    '        .FillDataSet(ds, "ir")
    '        gc_ir.DataSource = ds.Tables("ir")

    '        bestfit_column()
    '        ConditionsAdjustment()
    '    End With
    'End Sub
End Class
