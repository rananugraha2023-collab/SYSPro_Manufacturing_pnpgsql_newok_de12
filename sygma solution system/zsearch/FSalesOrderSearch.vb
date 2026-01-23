Imports master_new.ModFunction

Public Class FSalesOrderSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ar_credit_term_id As Integer
    Public _en_id_shipment As Integer
    Public _ptnr_id, _so_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Public _objk As Object
    Public _so_code, _ppn_type, _ar_code As String
    Public _interval As Integer
    Public _loc_id
    Public _eff_date As Date
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Dim _conf_value As String
    Public _date As Date
    Public dt_bants As DataTable
    'Public ds_edit_so, ds_edit_shipments, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet


    Private Sub FSalesOrderSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_sales_order")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()

        If fobject.name = FTaxInvoice.Name Then
            sb_fill.Visible = True
            gv_master.Columns("status").Visible = True
            Me.Text = "Sales Order Shipment Search"
        Else
            sb_fill.Visible = False
            gv_master.Columns("status").Visible = False
            Me.Text = "Sales Order Search"
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        If fobject.name = "FTransferIssuesReturn" Then
            add_column(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number Name", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code Syslog", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = FSalesOrderShipment.Name Then
            add_column(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number Name", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code Syslog", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
            add_column(gv_master, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
            add_column(gv_master, "Disc", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "P")
        ElseIf fobject.name = FTaxInvoice.Name Then
            add_column(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = FWorkOrder.Name Then
            add_column(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number Name", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = FARPaymentDetail30052023.Name Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Date", "arso_dt", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = FARPayment20240802.Name Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Date", "arso_dt", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        End If

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FSalesOrderShipment" Then


            get_sequel = "SELECT  " _
                    & "  a.so_oid, " _
                    & "  a.so_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.so_ptnr_id_sold , " _
                    & "  d.ptnr_name as ptnr_name_sold, " _
                    & "  c.si_desc, " _
                    & "  a.so_code, " _
                    & "  a.so_date,f.pt_code,  f.pt_desc1,  f.pt_desc2,  e.sod_qty,e.sod_price,e.sod_disc " _
                    & "FROM " _
                    & "  public.so_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.so_en_id = b.en_id) " _
                    & "  INNER JOIN public.si_mstr c ON (a.so_si_id = c.si_id) " _
                    & "  INNER JOIN public.ptnr_mstr d ON (a.so_ptnr_id_sold = d.ptnr_id) " _
                    & "  INNER JOIN public.sod_det e ON (a.so_oid = e.sod_so_oid) " _
                    & "  INNER JOIN public.pt_mstr f ON (e.sod_pt_id = f.pt_id) " _
                    & "WHERE so_trans_id <> 'X' and (a.so_date between " & SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and  " & SetDate(pr_txttglakhir.DateTime.Date) _
                    & ")  and a.so_en_id = " & _en_id & " AND  " _
                    & " coalesce(so_close_date,'01/01/1999') = '01/01/1999' "

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and so_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by so_code"

        ElseIf fobject.name = FWorkOrder.Name Then

            get_sequel = "SELECT  " _
               & "  a.so_oid, " _
               & "  a.so_en_id, " _
               & "  b.en_desc, " _
               & "  a.so_ptnr_id_sold , " _
               & "  d.ptnr_name as ptnr_name_sold, " _
               & "  c.si_desc, " _
               & "  a.so_code, " _
               & "  a.so_date,f.pt_code,  f.pt_desc1,  f.pt_desc2,  e.sod_qty,e.sod_price,e.sod_disc " _
               & "FROM " _
               & "  public.so_mstr a " _
               & "  INNER JOIN public.en_mstr b ON (a.so_en_id = b.en_id) " _
               & "  INNER JOIN public.si_mstr c ON (a.so_si_id = c.si_id) " _
               & "  INNER JOIN public.ptnr_mstr d ON (a.so_ptnr_id_sold = d.ptnr_id) " _
               & "  INNER JOIN public.sod_det e ON (a.so_oid = e.sod_so_oid) " _
               & "  INNER JOIN public.pt_mstr f ON (e.sod_pt_id = f.pt_id) " _
               & "WHERE so_trans_id <> 'X' and (a.so_date between " & SetDate(pr_txttglawal.DateTime.Date) _
               & "  and  " & SetDate(pr_txttglakhir.DateTime.Date) _
               & ")  and a.so_en_id = " & _en_id & " AND  " _
               & " coalesce(sod_wo_status,'N') = 'N' "

            get_sequel = get_sequel + " order by so_code"

        ElseIf fobject.name = "FSalesOrderReturn" Then
            get_sequel = "SELECT distinct " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "   INNER JOIN public.sod_det  ON (so_oid = sod_so_oid) " _
                    & "  where so_trans_id <> 'X' and  so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString _
                    & "  and coalesce(sod_qty_shipment,0) > coalesce(sod_qty_invoice,0) " _
                    & " GROUP BY " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_name_sold, " _
                    & "  si_desc "

            'by sys 20110416
            'line terakhir agar apabila masih terdapat data di d/c memo maka so return tidak bisa dilakukan

        ElseIf fobject.name = "FDRCRMemo" Then
            If _obj.name = "be_so_code" Then
                get_sequel = "SELECT  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  where so_trans_id <> 'X' and  so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_en_id = " + _en_id.ToString

            ElseIf _obj.name = "gv_edit_so" Then

                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If

        ElseIf fobject.name = "FDPackingSheetPrintOut" Then
            If _obj.name = "gv_edit_so" Then
                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_qty_invoice > '0' "
            End If

        ElseIf fobject.name = "FDPackingSheetPrintOutSO" Or fobject.name = "FDPackingSheetPrintOutbySO" Then
            If _obj.name = "gv_edit_so" Then
                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id = 'D' " _
                & "  and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_sold = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' "
            End If

        ElseIf fobject.name = "FDPickingListSO" Then
            If _obj.name = "gv_edit_so" Then
                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_qty_invoice > '0' "
            End If

        ElseIf fobject.name = "FDPackingSheetNew" Then
            If _obj.name = "be_so_code" Then
                get_sequel = "SELECT  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  where so_trans_id <> 'X' and  so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_en_id = " + _en_id.ToString

            ElseIf _obj.name = "gv_edit_so" Then

                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id, " _
                & "  public.so_mstr.so_ptnr_id_bill, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and ptnr_id = " & _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If

        ElseIf fobject.name = "FDRCRMemoDetail" Then
            If _obj.name = "be_so_code" Then
                get_sequel = "SELECT  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  where so_trans_id <> 'X' and  so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_en_id = " + _en_id.ToString

            ElseIf _obj.name = "gv_edit_so" Then

                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                   & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If
        ElseIf fobject.name = "FDRCRMemo20240802" Then
            If _obj.name = "ar_so_ref_code" Then
                get_sequel = "SELECT DISTINCT  " _
                & "public.so_mstr.so_oid, " _
                & "public.so_mstr.so_en_id, " _
                & "public.so_mstr.so_code, " _
                & "public.so_mstr.so_ptnr_id_sold, " _
                & "public.so_mstr.so_date, " _
                & "public.so_mstr.so_si_id, " _
                & "public.so_mstr.so_close_date, " _
                & "public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "public.si_mstr.si_desc, " _
                & "public.en_mstr.en_desc, " _
                & "public.so_mstr.so_cu_id, " _
                & "public.cu_mstr.cu_name, " _
                & "public.so_mstr.so_exc_rate, " _
                & "public.so_mstr.so_credit_term, " _
                & "credit_term.code_name as credit_term_name, " _
                & "public.so_mstr.so_bk_id, " _
                & "public.bk_mstr.bk_name, " _
                & "public.so_mstr.so_ar_ac_id, " _
                & "public.so_mstr.so_ar_sb_id, " _
                & "public.so_mstr.so_ar_cc_id, " _
                & "ac_mstr_ar.ac_code, " _
                & "ac_mstr_ar.ac_name, " _
                & "sb_mstr_ar.sb_desc, " _
                & "public.so_mstr.so_taxable, " _
                & "public.so_mstr.so_tax_inc, " _
                & "public.so_mstr.so_tax_class, " _
                & "public.so_mstr.so_ppn_type, " _
                & "tax_class.code_name as tax_class_name, " _
                & "public.so_mstr.so_dp, " _
                & "public.so_mstr.so_trans_rmks " _
                & "FROM public.so_mstr " _
                & "INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                & "INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id) " _
                & "INNER JOIN public.cu_mstr ON (public.so_mstr.so_cu_id = public.cu_mstr.cu_id) " _
                & "INNER JOIN public.bk_mstr ON (public.so_mstr.so_bk_id = public.bk_mstr.bk_id) " _
                & "INNER JOIN public.code_mstr tax_class on tax_class.code_id = so_tax_class " _
                & "INNER JOIN code_mstr credit_term on credit_term.code_id = so_credit_term " _
                & "INNER JOIN ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = so_ar_ac_id " _
                & "INNER JOIN sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = so_ar_sb_id " _
                & "INNER JOIN cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = so_ar_cc_id" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and public.code_mstr.code_usr_1 <> '0' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) "

            ElseIf _obj.name = "gv_edit_so" Then
                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If


        ElseIf fobject.name = "FDRCRMemoSDI26052023" Or fobject.name = "FDRCRMemoDetail20231028" Then
            If _obj.name = "ar_so_ref_code" Then
                get_sequel = "SELECT DISTINCT  " _
                & "public.so_mstr.so_oid, " _
                & "public.so_mstr.so_en_id, " _
                & "public.so_mstr.so_code, " _
                & "public.so_mstr.so_ptnr_id_sold, " _
                & "public.so_mstr.so_date, " _
                & "public.so_mstr.so_si_id, " _
                & "public.so_mstr.so_close_date, " _
                & "public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "public.si_mstr.si_desc, " _
                & "public.en_mstr.en_desc, " _
                & "public.so_mstr.so_cu_id, " _
                & "public.cu_mstr.cu_name, " _
                & "public.so_mstr.so_exc_rate, " _
                & "public.so_mstr.so_credit_term, " _
                & "credit_term.code_name as credit_term_name, " _
                & "public.so_mstr.so_bk_id, " _
                & "public.bk_mstr.bk_name, " _
                & "public.so_mstr.so_ar_ac_id, " _
                & "public.so_mstr.so_ar_sb_id, " _
                & "public.so_mstr.so_ar_cc_id, " _
                & "ac_mstr_ar.ac_code, " _
                & "ac_mstr_ar.ac_name, " _
                & "sb_mstr_ar.sb_desc, " _
                & "public.so_mstr.so_taxable, " _
                & "public.so_mstr.so_tax_inc, " _
                & "public.so_mstr.so_tax_class, " _
                & "public.so_mstr.so_ppn_type, " _
                & "tax_class.code_name as tax_class_name, " _
                & "public.so_mstr.so_dp, " _
                & "public.so_mstr.so_trans_rmks " _
                & "FROM public.so_mstr " _
                & "INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                & "INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id) " _
                & "INNER JOIN public.cu_mstr ON (public.so_mstr.so_cu_id = public.cu_mstr.cu_id) " _
                & "INNER JOIN public.bk_mstr ON (public.so_mstr.so_bk_id = public.bk_mstr.bk_id) " _
                & "INNER JOIN public.code_mstr tax_class on tax_class.code_id = so_tax_class " _
                & "INNER JOIN code_mstr credit_term on credit_term.code_id = so_credit_term " _
                & "INNER JOIN ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = so_ar_ac_id " _
                & "INNER JOIN sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = so_ar_sb_id " _
                & "INNER JOIN cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = so_ar_cc_id" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and public.code_mstr.code_usr_1 <> '0' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) "

            ElseIf _obj.name = "gv_edit_so" Then
                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If

        ElseIf fobject.name = "FARPaymentDetail30052023" Then
            If _obj.name = "arpay_so_id" Then
                get_sequel = "SELECT DISTINCT  " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.arso_so.arso_so_oid, " _
                & "  public.arso_so.arso_so_code, " _
                & "  public.arso_so.arso_dt, " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_bk_id, " _
                & "  public.bk_mstr.bk_name, " _
                & "  public.ar_mstr.ar_date, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  ac_mstr_ar.ac_code, " _
                & "  ac_mstr_ar.ac_name, " _
                & "  sb_mstr_ar.sb_desc, " _
                & "  public.cashi_in.cashi_oid, " _
                & "  public.cashi_in.cashi_oid," _
                & "  public.cashi_in.cashi_code," _
                & "  public.cashi_in.cashi_amount, " _
                & "  public.ar_mstr.ar_remarks " _
                & "FROM " _
                & "  public.arso_so " _
                & "  INNER JOIN public.ar_mstr ON (public.arso_so.arso_ar_oid = public.ar_mstr.ar_oid) " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  LEFT OUTER JOIN public.cashi_in ON (public.arso_so.arso_so_oid = public.cashi_in.cashi_so_oid) " _
                & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = public.bk_mstr.bk_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id)" _
                & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = ar_ac_id " _
                & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = ar_sb_id " _
                & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = ar_cc_id " _
                & "  where arso_dt >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and arso_dt <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and ar_en_id = " + _en_id.ToString _
                & "  and ar_amount > coalesce(ar_pay_amount,0) "

            ElseIf _obj.name = "gv_edit_so" Then

                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If
            '-------

        ElseIf fobject.name = "FARPayment20240802" Then
            If _obj.name = "arpay_so_code" Then
                get_sequel = "SELECT DISTINCT  " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.arso_so.arso_so_oid, " _
                & "  public.arso_so.arso_so_code, " _
                & "  public.arso_so.arso_dt, " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_bk_id, " _
                & "  public.bk_mstr.bk_name, " _
                & "  public.ar_mstr.ar_date, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  ac_mstr_ar.ac_code, " _
                & "  ac_mstr_ar.ac_name, " _
                & "  sb_mstr_ar.sb_desc, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  public.cashi_in.cashi_oid, " _
                & "  public.cashi_in.cashi_oid," _
                & "  public.cashi_in.cashi_code," _
                & "  public.cashi_in.cashi_amount, " _
                & "  public.ar_mstr.ar_remarks " _
                & "FROM " _
                & "  public.arso_so " _
                & "  INNER JOIN public.ar_mstr ON (public.arso_so.arso_ar_oid = public.ar_mstr.ar_oid) " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  LEFT OUTER JOIN public.cashi_in ON (public.arso_so.arso_so_oid = public.cashi_in.cashi_so_oid) " _
                & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = public.bk_mstr.bk_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id)" _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = ar_ac_id " _
                & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = ar_sb_id " _
                & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = ar_cc_id " _
                & "  where arso_dt >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and arso_dt <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and ar_en_id = " + _en_id.ToString _
                & "  and ar_amount > coalesce(ar_pay_amount,0) "

            ElseIf _obj.name = "gv_edit_so" Then

                get_sequel = "SELECT  distinct " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and code_usr_1 <> '0' " _
                & "  and sod_ppn_type ~~* '" & _ppn_type & "' " _
                & "  and sod_qty_shipment > coalesce(sod_qty_invoice,0) " _
                & "  group by  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, en_desc "

                'by sys 20110416 ditambahkan ini agar so yang sudah di d/c memo tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            ElseIf _obj.name = "par_so" Then

                get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_close_date, " _
                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                    & "  public.si_mstr.si_desc, en_desc " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                    & "  and code_usr_1 <> '0' "
            End If
            '---------

        ElseIf fobject.name = "FTransferIssues" Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_trans_id <> 'D' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString

        ElseIf fobject.name = "FTransferIssuesReturn" Then
            get_sequel = "SELECT  " _
                    & "  a.so_oid, " _
                    & "  a.so_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.so_ptnr_id_sold , " _
                    & "  d.ptnr_name as ptnr_name_sold, " _
                    & "  c.si_desc, " _
                    & "  a.so_code, " _
                    & "  a.so_date,f.pt_code,  f.pt_desc1,  f.pt_desc2,  e.sod_qty " _
                    & "FROM " _
                    & "  public.so_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.so_en_id = b.en_id) " _
                    & "  INNER JOIN public.si_mstr c ON (a.so_si_id = c.si_id) " _
                    & "  INNER JOIN public.ptnr_mstr d ON (a.so_ptnr_id_sold = d.ptnr_id) " _
                    & "   INNER JOIN public.sod_det e ON (a.so_oid = e.sod_so_oid) " _
                    & "   INNER JOIN public.pt_mstr f ON (e.sod_pt_id = f.pt_id) " _
                    & "WHERE so_trans_id <> 'X' and   (a.so_date between " & SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and  " & SetDate(pr_txttglakhir.DateTime.Date) _
                    & ")  and a.so_en_id = " & _en_id & " AND  " _
                    & "  a.so_cons = 'Y' AND  " _
                    & "  a.so_oid IN (SELECT a.ptsfr_so_oid FROM public.ptsfr_mstr a WHERE a.ptsfr_loc_to_id = " & _loc_id _
                    & " and a.ptsfr_so_oid is not null) order by a.so_date,a.so_code,f.pt_desc1"


        ElseIf fobject.name = "FSalesOrderPrint" Or fobject.name = "FInventoryReceipts" Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString

        ElseIf fobject.name = FInventoryReceiptsLintas.Name Then
            get_sequel = "SELECT  distinct " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join sod_det on sod_so_oid = so_oid " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where  (sod_qty - coalesce(sod_qty_ir,0))>0   and so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString 'and so_inv_receipt='READY'

        ElseIf fobject.name = "FSalesOrderCashPrint" Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join code_mstr on code_id = so_pay_type " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_trans_id <> 'X' and code_usr_1='0' and  so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString
            'so_pay_type
        ElseIf fobject.name = "FSalesOrderFakturPenjualanPrint" Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString _
                    & "  and so_ptnr_id_sold = " + _ptnr_id.ToString

            If _interval = 0 Then
                get_sequel = get_sequel + " and coalesce(so_interval,-1) = 0"
            Else
                get_sequel = get_sequel + " and coalesce(so_interval,-1) > 0"
            End If
            'ElseIf fobject.name = FTaxInvoice.Name Then
            '    get_sequel = "SELECT  false as status, " _
            '            & "  soship_oid, " _
            '            & "  so_en_id, " _
            '            & "  so_code, " _
            '            & "  so_ptnr_id_bill, " _
            '            & "  ptnr_name as ptnr_name_sold, " _
            '            & "  so_date, " _
            '            & "  en_desc, " _
            '            & "  soship_code, " _
            '            & "  soship_date " _
            '            & "FROM  " _
            '            & "  public.so_mstr " _
            '            & "  inner join en_mstr on en_id = so_en_id " _
            '            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
            '            & "  inner join soship_mstr on soship_so_oid = so_oid " _
            '            & "  inner join code_mstr on code_id = so_pay_type " _
            '            & "  where so_trans_id <> 'X' and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            '            & "  and coalesce(so_ppn_type,'N') ~~* " + SetSetring(_ppn_type) _
            '            & "  and coalesce(soship_ti_in_use,'N') ~~* 'N'" _
            '            & "  and code_usr_1 = '0' " _
            '            & "  and so_ptnr_id_bill in (select " + _ptnr_id.ToString + " as ptnr_id union " _
            '                                 & " select tipgd_ptnr_id from tipg_group " _
            '                                 & " inner join tipgd_det on tipgd_tipg_oid = tipg_oid " _
            '                                 & " where tipg_ptnr_id =  " + _ptnr_id.ToString + ")"
            'End If
            'perbaikan untuk penambahan fungsi pencarian so sdi pada saat pembuatan faktur pajak

            'ElseIf fobject.name = "FSalesOrderFakturPenjualanPrint" Then
            '    get_sequel = "SELECT  " _
            '            & "  so_oid, " _
            '            & "  so_dom_id, " _
            '            & "  so_en_id, " _
            '            & "  so_add_by, " _
            '            & "  so_add_date, " _
            '            & "  so_upd_by, " _
            '            & "  so_upd_date, " _
            '            & "  so_code, " _
            '            & "  so_ptnr_id_sold, " _
            '            & "  so_date, " _
            '            & "  so_si_id, " _
            '            & "  en_desc, " _
            '            & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
            '            & "  si_desc " _
            '            & "FROM  " _
            '            & "  public.so_mstr " _
            '            & "  inner join en_mstr on en_id = so_en_id " _
            '            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
            '            & "  inner join si_mstr on si_id = so_si_id " _
            '            & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            '            & "  and so_en_id = " + _en_id.ToString

            '    If _interval = 0 Then
            '        get_sequel = get_sequel + " and coalesce(so_interval,-1) = 0"
            '    Else
            '        get_sequel = get_sequel + " and coalesce(so_interval,-1) > 0"
            '    End If

        ElseIf fobject.name = FTaxInvoice.Name Then
            get_sequel = "SELECT  false as status, " _
                    & "  soship_oid, " _
                    & "  so_en_id, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_bill, " _
                    & "  ptnr_name as ptnr_name_sold, " _
                    & "  so_date, " _
                    & "  en_desc, " _
                    & "  soship_code, " _
                    & "  soship_date " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join soship_mstr on soship_so_oid = so_oid " _
                    & "  inner join code_mstr on code_id = so_pay_type " _
                    & "  where so_trans_id <> 'X' and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and coalesce(so_ppn_type,'N') ~~* " + SetSetring(_ppn_type) _
                & "  and coalesce(soship_ti_in_use,'N') ~~* 'N'" _
                & "  and code_usr_1 = '0' " _
                & "  and so_type not in ('D') " _
                & "  and so_ptnr_id_bill in (select " + _ptnr_id.ToString + " as ptnr_id union " _
                & " select tipgd_ptnr_id from tipg_group " _
                & " inner join tipgd_det on tipgd_tipg_oid = tipg_oid " _
                & " where tipg_ptnr_id =  " + _ptnr_id.ToString + ")" _
                & "  union " _
                & " SELECT  false as status, " _
                & "  soship_oid, " _
                & "  so_en_id, " _
                & "  so_code, " _
                & "  so_ptnr_id_bill, " _
                & "  ptnr_name as ptnr_name_sold, " _
                & "  so_date, " _
                & "  en_desc, " _
                & "  soship_code, " _
                & "  soship_date " _
                & "FROM  " _
                & "  public.so_mstr " _
                & "  inner join en_mstr on en_id = so_en_id " _
                & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                & "  inner join soship_mstr on soship_so_oid = so_oid " _
                & "  inner join code_mstr on code_id = so_pay_type " _
                & "  where so_trans_id <> 'X' and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and coalesce(so_ppn_type,'N') ~~* " + SetSetring(_ppn_type) _
                & "  and coalesce(soship_ti_in_use,'N') ~~* 'N'" _
                & "  and so_type in ('D') " _
                & "  and so_ptnr_id_bill in (select " + _ptnr_id.ToString + " as ptnr_id union " _
                & " select tipgd_ptnr_id from tipg_group " _
                & " inner join tipgd_det on tipgd_tipg_oid = tipg_oid " _
                & " where tipg_ptnr_id =  " + _ptnr_id.ToString + ")"

        ElseIf fobject.name = FCashIn.Name Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString _
                    & "  and so_ptnr_id_sold = " + _ptnr_id.ToString _
                    & " and coalesce(so_indent,'') ='' and so_manufacture='Y' "

        ElseIf fobject.name = "FSalesOrder" Then
            get_sequel = "SELECT  " _
                   & "  so_oid, " _
                   & "  so_dom_id, " _
                   & "  so_en_id, " _
                   & "  so_add_by, " _
                   & "  so_add_date, " _
                   & "  so_upd_by, " _
                   & "  so_upd_date, " _
                   & "  so_code, " _
                   & "  so_ptnr_id_sold, " _
                   & "  so_date, " _
                   & "  so_si_id, " _
                   & "  en_desc, " _
                   & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                   & "  si_desc " _
                   & "FROM  " _
                   & "  public.so_mstr " _
                   & "  inner join en_mstr on en_id = so_en_id " _
                   & "  inner join code_mstr on code_id = so_pay_type " _
                   & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                   & "  inner join si_mstr on si_id = so_si_id " _
                   & "  where so_trans_id <> 'X'  and  so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                       & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                       & "  and so_en_id = " + _en_id.ToString
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

        Dim ds_bantu, ds_bant, ds_ban, ds_sod_piutang As New DataSet
        Dim i, j As Integer
        Dim qty_alloc, qty_alloc_awal As Integer
        Dim _exc_rate As Double = 0

        Dim _exr_cu_rate_1 As Double = 0
        If _cu_id = master_new.ClsVar.ibase_cur_id Then
            _exr_cu_rate_1 = 1
        Else
            '_exr_cu_rate_1 = func_data.get_exchange_rate(ds.Tables(0).Rows(_row_gv).Item("ap_cu_id").ToString)
            _exr_cu_rate_1 = func_data.get_exchange_rate(_cu_id)
        End If

        If fobject.name = "FSalesOrderShipment" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString
            fobject.soship_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_shipment,0) as sod_qty_open, " _
                            & "  coalesce(sod_qty_allocated,0) as sod_qty_allocated, " _
                            & "  sod_invc_loc_id, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  so_cu_id, " _
                            & "  so_cons, " _
                            & "  so_exc_rate, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = sod_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_shipment,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()
            fobject._is_consignment = IIf(ds_bantu.Tables(0).Rows(0).Item("so_cons") = "Y", True, False)

            If ds_bantu.Tables(0).Rows.Count > 0 Then
                fobject.soship_cu_id.editvalue = ds_bantu.Tables(0).Rows(0).Item("so_cu_id")
                If ds_bantu.Tables(0).Rows(0).Item("so_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                    _exc_rate = func_data.get_exchange_rate(ds_bantu.Tables(0).Rows(0).Item("so_cu_id"), _date)
                    If _exc_rate = 1 Then
                        fobject.soship_exc_rate.EditValue = 0
                    Else
                        fobject.soship_exc_rate.EditValue = _exc_rate
                    End If

                    'fobject.rcv_exc_rate.Enabled = True
                Else
                    fobject.soship_exc_rate.EditValue = 1
                    'fobject.rcv_exc_rate.Enabled = False
                End If
            End If

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                _dtrow("soshipd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid").ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("sod_loc_id")
                _dtrow("sod_invc_loc_id") = ds_bantu.Tables(0).Rows(i).Item("sod_invc_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")

                'cek qty allocated yg sudah dikirim
                '__________________________________________________________________________

                Dim ds_allocated_ship As New DataSet
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                                & "  sum(soshipd_qty_allocated) as tot_alloc_ship " _
                                & "FROM  " _
                                & "  public.soshipd_det " _
                                & "  where soshipd_sod_oid = " + SetSetring(ds_bantu.Tables(0).Rows(i).Item("sod_oid"))

                            .InitializeCommand()
                            .FillDataSet(ds_allocated_ship, "allocated_ship")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                '____________________________________________________________
                '_qty_allocated_awal sebagai nilai awal allocated (maximal allocated ship qty)
                ' dikurangi (ditambah karena nilainya minus) allocated_qty yg sudah terkirim

                If ds_allocated_ship.Tables("allocated_ship").Rows.Count > 0 Then
                    If IsDBNull(ds_allocated_ship.Tables("allocated_ship").Rows(0).Item("tot_alloc_ship")) = False Then
                        qty_alloc = ds_bantu.Tables(0).Rows(i).Item("sod_qty_allocated") - (ds_allocated_ship.Tables("allocated_ship").Rows(0).Item("tot_alloc_ship"))
                        _dtrow("soshipd_qty_allocated") = qty_alloc
                        qty_alloc_awal = ds_bantu.Tables(0).Rows(i).Item("sod_qty_allocated") - (ds_allocated_ship.Tables("allocated_ship").Rows(0).Item("tot_alloc_ship"))
                        _dtrow("_qty_allocated_awal") = qty_alloc_awal
                    Else
                        _dtrow("soshipd_qty_allocated") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_allocated")
                        _dtrow("_qty_allocated_awal") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_allocated")
                    End If
                Else
                    _dtrow("soshipd_qty_allocated") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_allocated")
                    _dtrow("_qty_allocated_awal") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_allocated")
                End If

                '____________________________________________________________

                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))
                _dtrow("so_cu_id") = ds_bantu.Tables(0).Rows(i).Item("so_cu_id")
                _dtrow("so_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("so_exc_rate")
                _dtrow("sod_cost") = ds_bantu.Tables(0).Rows(i).Item("sod_cost")

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FSalesOrderReturn" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString
            fobject.soship_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, coalesce(sod_qty_shipment,0) - coalesce(sod_qty_invoice,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  so_cu_id, " _
                            & "  so_exc_rate, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc, " _
                            & "  pay_type.code_usr_1 as pay_type_interval " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            & "  where (sod_qty_shipment - coalesce(sod_qty_return,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            If ds_bantu.Tables(0).Rows.Count > 0 Then
                fobject.soship_cu_id.editvalue = ds_bantu.Tables(0).Rows(0).Item("so_cu_id")
                If ds_bantu.Tables(0).Rows(0).Item("so_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                    _exc_rate = func_data.get_exchange_rate(ds_bantu.Tables(0).Rows(0).Item("so_cu_id"), _date)
                    If _exc_rate = 1 Then
                        fobject.soship_exc_rate.EditValue = 0
                    Else
                        fobject.soship_exc_rate.EditValue = _exc_rate
                    End If

                    'fobject.rcv_exc_rate.Enabled = True
                Else
                    fobject.soship_exc_rate.EditValue = 1
                    'fobject.rcv_exc_rate.Enabled = False
                End If
            End If

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                _dtrow("soshipd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid").ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))
                _dtrow("so_cu_id") = ds_bantu.Tables(0).Rows(i).Item("so_cu_id")
                _dtrow("so_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("so_exc_rate")
                _dtrow("sod_cost") = ds_bantu.Tables(0).Rows(i).Item("sod_cost")

                'by sys 20110412
                _dtrow("sod_pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("sod_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
                _dtrow("sod_tax_class") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
                _dtrow("sod_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
                _dtrow("sod_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
                _dtrow("sod_disc") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                _dtrow("pay_type_interval") = ds_bantu.Tables(0).Rows(i).Item("pay_type_interval")
                '_dtrow("soshipd_close_line") = "N"
                '-------------------------------------------------------------------

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FDRCRMemo" Or fobject.name = "FDRCRMemoDetail" Then
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()
                'ElseIf _obj.name = "be_so_code" Then
                '    _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")

                '    Try
                '        Using objcb As New master_new.WDABasepgsql("", "")
                '            With objcb
                '                .SQL = "SELECT  " _
                '                    & "  so_oid, " _
                '                    & "  so_dom_id, " _
                '                    & "  so_en_id, " _
                '                    & "  so_code, " _
                '                    & "  so_ptnr_id_sold, " _
                '                    & "  so_ptnr_id_bill, " _
                '                    & "  so_date, " _
                '                    & "  so_credit_term, " _
                '                    & "  so_taxable, " _
                '                    & "  so_tax_class, " _
                '                    & "  so_si_id, " _
                '                    & "  so_type, " _
                '                    & "  so_sales_person, " _
                '                    & "  so_pi_id, " _
                '                    & "  so_pay_type, " _
                '                    & "  so_pay_method, " _
                '                    & "  so_ar_ac_id, " _
                '                    & "  so_ar_sb_id, " _
                '                    & "  so_ar_cc_id, " _
                '                    & "  so_dp, " _
                '                    & "  so_disc_header, " _
                '                    & "  so_total, " _
                '                    & "  so_print_count, " _
                '                    & "  so_payment_date, " _
                '                    & "  so_close_date, " _
                '                    & "  so_tran_id, " _
                '                    & "  so_trans_id, " _
                '                    & "  so_trans_rmks, " _
                '                    & "  so_current_route, " _
                '                    & "  so_next_route, " _
                '                    & "  so_dt, " _
                '                    & "  so_cu_id, " _
                '                    & "  so_bk_id, " _
                '                    & "  so_total_ppn, " _
                '                    & "  so_total_pph, " _
                '                    & "  so_payment, " _
                '                    & "  so_exc_rate, " _
                '                    & "  so_tax_inc, " _
                '                    & "  so_cons, " _
                '                    & "  so_terbilang " _
                '                    & " FROM  " _
                '                    & "  public.so_mstr " _
                '                    & "  where so_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code") + "'" _
                '                    & "  and so_en_id = " + _en_id.ToString
                '                .InitializeCommand()
                '                .FillDataSet(ds_bantu, "so")
                '            End With
                '        End Using
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message)
                '    End Try

                '    fobject.ar_bill_to.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_ptnr_id_bill")
                '    fobject.ar_cu_id.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_cu_id")
                '    fobject.ar_eff_date.datetime = func_coll.get_now()
                '    fobject.ar_credit_term.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_credit_term")
                '    fobject.ar_bk_id.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_bk_id")
                '    fobject.ar_ac_id.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_ar_ac_id")
                '    fobject.ar_taxable.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_taxable")
                '    fobject.ar_tax_inc.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_tax_inc")
                '    fobject.ar_tax_class_id.editvalue = ds_bantu.Tables("so").Rows(0).Item("so_tax_class")
            ElseIf _obj.name = "par_so" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            End If

            'ElseIf fobject.name = "FDRCRMemoSDI26052023" Then
            '    If _obj.name = "ar_so_ref_code" Then
            '        _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            '        fobject._ar_bill_to_id = ds.Tables(0).Rows(_row_gv).Item("so_ptnr_id_sold")
            '        fobject.ar_bill_to.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold")
            '        'fobject._ar_credit_term_id = ds.Tables(0).Rows(_row_gv).Item("so_credit_term")
            '        'fobject.ar_credit_term.text = ds.Tables(0).Rows(_row_gv).Item("credit_term_name")
            '        fobject._ar_bk_ids = ds.Tables(0).Rows(_row_gv).Item("so_bk_id")
            '        fobject.ar_bk_id.text = ds.Tables(0).Rows(_row_gv).Item("bk_name")
            '        fobject._ar_ac_ids = ds.Tables(0).Rows(_row_gv).Item("so_ar_ac_id")
            '        fobject.ar_ac_id.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

            '        If ds.Tables(0).Rows(_row_gv).Item("so_taxable") = "Y" Then
            '            fobject.ar_taxable.Checked = True

            '        End If

            '        If ds.Tables(0).Rows(_row_gv).Item("so_tax_inc") = "Y" Then
            '            fobject.ar_tax_inc.Checked = True

            '        End If

            '        fobject._ar_tax_class_ids = ds.Tables(0).Rows(_row_gv).Item("so_tax_class")
            '        'fobject.ar_tax_class_id.text = ds.Tables(0).Rows(_row_gv).Item("tax_class_name")
            '        'fobject._ar_ppn_type_id = ds.Tables(0).Rows(_row_gv).Item("so_ppn_type")
            '        'fobject.ar_pay_prepaid.text = ds.Tables(0).Rows(_row_gv).Item("so_dp")

            '        Try
            '            Using objcb As New master_new.WDABasepgsql("", "")
            '                With objcb
            '                    .SQL = "SELECT  distinct " _
            '                    & "  public.so_mstr.so_oid, " _
            '                    & "  public.so_mstr.so_en_id, " _
            '                    & "  public.so_mstr.so_code, " _
            '                    & "  public.so_mstr.so_ptnr_id_sold, " _
            '                    & "  public.so_mstr.so_date, " _
            '                    & "  public.so_mstr.so_si_id, " _
            '                    & "  public.so_mstr.so_close_date, " _
            '                    & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
            '                    & "  public.si_mstr.si_desc, en_desc " _
            '                    & "FROM " _
            '                    & "  public.so_mstr " _
            '                    & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
            '                    & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
            '                    & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
            '                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
            '                    & "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
            '                    & "  where public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'"

            '                    .InitializeCommand()
            '                    .FillDataSet(ds_bantu, "ard_det")
            '                End With
            '            End Using
            '        Catch ex As Exception
            '            MessageBox.Show(ex.Message)
            '        End Try


            '        fobject.ds_edit_so.tables(0).clear()

            '        Dim _dtrow As DataRow
            '        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '            _dtrow = fobject.ds_edit_so.Tables(0).NewRow
            '            'fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
            '            _dtrow("arso_oid") = Guid.NewGuid.ToString
            '            _dtrow("arso_so_oid") = ds_bantu.Tables(0).Rows(i).Item("so_oid")
            '            _dtrow("arso_so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
            '            '_dtrow("arso_so_date") = ds_bantu.Tables(0).Rows(i).Item("so_date")
            '            fobject.ds_edit_so.Tables(0).Rows.Add(_dtrow)

            '        Next
            '        fobject.ds_edit_so.Tables(0).AcceptChanges()
            '        fobject.gv_edit_so.BestFitColumns()

            '        'input so shipment detail


            '        Try
            '            Using objcb As New master_new.WDABasepgsql("", "")
            '                With objcb
            '                    .SQL = "SELECT  " _
            '                        & "  soshipd_oid, True as ceklist, " _
            '                        & "  soshipd_soship_oid, " _
            '                        & "  soshipd_sod_oid, " _
            '                        & "  soship_code, " _
            '                        & "  soship_date, " _
            '                        & "  pt_id, " _
            '                        & "  pt_code, " _
            '                        & "  pt_desc1, " _
            '                        & "  pt_desc2, " _
            '                        & "  sod_price,coalesce(sod_qty_shipment,0) as qty_shipment,  " _
            '                        & "  (sod_price * coalesce(sod_disc,0)) as sod_disc,sod_ppn_type,  " _
            '                        & "  sod_taxable, " _
            '                        & "  sod_tax_inc, " _
            '                        & "  sod_tax_class, " _
            '                        & "  code_name as sod_tax_class_name, " _
            '                        & "  ((soshipd_qty * -1) - coalesce(soshipd_qty_inv,0))  as qty_open " _
            '                        & "FROM  " _
            '                        & "  public.soshipd_det " _
            '                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            '                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            '                        & "  inner join so_mstr on so_oid = sod_so_oid " _
            '                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
            '                        & "  inner join code_mstr on code_id = sod_tax_class " _
            '                        & "  where coalesce(soshipd_close_line,'N') = 'N' " _
            '                        & "  and so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
            '                        & "  order by soship_date "
            '                    .InitializeCommand()
            '                    .FillDataSet(ds_bant, "soship_mstr")
            '                End With
            '            End Using
            '        Catch ex As Exception
            '            MessageBox.Show(ex.Message)
            '        End Try

            '        ds_edit_shipments.Tables(0).Clear()

            '        Dim _dtrows As DataRow
            '        For i = 0 To ds_bant.Tables(0).Rows.Count - 1
            '            'If ds_bant.Tables(0).Rows(i).Item("qty_open") <> 0 Then
            '            _dtrows = ds_edit_shipments.Tables(0).NewRow
            '            _dtrows("ars_oid") = Guid.NewGuid.ToString
            '            _dtrows("ceklist") = ds_bant.Tables(0).Rows(i).Item("ceklist")
            '            _dtrows("ars_soshipd_oid") = ds_bant.Tables(0).Rows(i).Item("soshipd_oid")
            '            _dtrows("soship_code") = ds_bant.Tables(0).Rows(i).Item("soship_code")
            '            _dtrows("pt_id") = ds_bant.Tables(0).Rows(i).Item("pt_id")
            '            _dtrows("pt_code") = ds_bant.Tables(0).Rows(i).Item("pt_code")
            '            _dtrows("pt_desc1") = ds_bant.Tables(0).Rows(i).Item("pt_desc1")
            '            _dtrows("pt_desc2") = ds_bant.Tables(0).Rows(i).Item("pt_desc2")
            '            _dtrows("ars_taxable") = ds_bant.Tables(0).Rows(i).Item("sod_taxable")
            '            _dtrows("ars_tax_class_id") = ds_bant.Tables(0).Rows(i).Item("sod_tax_class")
            '            _dtrows("taxclass_name") = ds_bant.Tables(0).Rows(i).Item("sod_tax_class_name")
            '            _dtrows("ars_tax_inc") = ds_bant.Tables(0).Rows(i).Item("sod_tax_inc")
            '            _dtrows("ars_open") = ds_bant.Tables(0).Rows(i).Item("qty_open")
            '            _dtrows("ars_shipment") = ds_bant.Tables(0).Rows(i).Item("qty_shipment")
            '            _dtrows("ars_invoice") = ds_bant.Tables(0).Rows(i).Item("qty_open")
            '            _dtrows("ars_so_price") = ds_bant.Tables(0).Rows(i).Item("sod_price")
            '            _dtrows("ars_so_disc_value") = ds_bant.Tables(0).Rows(i).Item("sod_disc")
            '            _dtrows("ars_invoice_price") = ds_bant.Tables(0).Rows(i).Item("sod_price") - ds_bant.Tables(0).Rows(i).Item("sod_disc")
            '            _dtrows("tot_inv_price") = _dtrows("ars_invoice_price") * _dtrows("ars_invoice")
            '            _dtrows("ars_close_line") = "N"
            '            ds_edit_shipments.Tables(0).Rows.Add(_dtrows)
            '            'End If
            '        Next

            '        Dim ssql As String
            '        ssql = "SELECT  " _
            '            & "  soship_date " _
            '            & "FROM  " _
            '            & "  public.soshipd_det " _
            '            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            '            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            '            & "  inner join so_mstr on so_oid = sod_so_oid " _
            '            & "  inner join pt_mstr on pt_id = sod_pt_id " _
            '            & "  inner join code_mstr on code_id = sod_tax_class " _
            '            & "  where coalesce(soshipd_close_line,'N') = 'N' " _
            '            & "  and so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
            '            & "  and soship_is_shipment='Y'   " _
            '            & "  order by soship_date desc"

            '        Dim dt As New DataTable
            '        dt = master_new.PGSqlConn.GetTableData(ssql)


            '        _eff_date = dt.Rows(0).Item("soship_date")
            '        '(i) disini pasti line yang terakhir

            '        fobject.ds_edit_shipment.Tables(0).AcceptChanges()
            '        'fobject.gv_edit_shipment.BestFitColumns()

            '    ElseIf _obj.name = "gv_edit_so" Then
            '        fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
            '        fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid"))
            '        fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
            '        fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
            '        fobject.gv_edit_so.BestFitColumns()

            '    ElseIf _obj.name = "par_so" Then
            '        _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            '    End If

        ElseIf fobject.name = "FDRCRMemoDetail20231028" Then
            If _obj.name = "ar_so_ref_code" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
                fobject._ar_bill_to_id = ds.Tables(0).Rows(_row_gv).Item("so_ptnr_id_sold")
                fobject.ar_bill_to.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold")
                'fobject._ar_credit_term_id = ds.Tables(0).Rows(_row_gv).Item("so_credit_term")
                'fobject.ar_credit_term.text = ds.Tables(0).Rows(_row_gv).Item("credit_term_name")
                fobject._ar_bk_ids = ds.Tables(0).Rows(_row_gv).Item("so_bk_id")
                fobject.ar_bk_id.text = ds.Tables(0).Rows(_row_gv).Item("bk_name")
                fobject._ar_ac_ids = ds.Tables(0).Rows(_row_gv).Item("so_ar_ac_id")
                fobject.ar_ac_id.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

                'If ds.Tables(0).Rows(_row_gv).Item("so_taxable") = "Y" Then
                '    fobject.ar_taxable.Checked = True

                'End If

                'If ds.Tables(0).Rows(_row_gv).Item("so_tax_inc") = "Y" Then
                '    fobject.ar_tax_inc.Checked = True

                'End If

                'fobject._ar_tax_class_ids = ds.Tables(0).Rows(_row_gv).Item("so_tax_class")
                'fobject.ar_tax_class_id.text = ds.Tables(0).Rows(_row_gv).Item("tax_class_name")
                'fobject._ar_ppn_type_id = ds.Tables(0).Rows(_row_gv).Item("so_ppn_type")
                'fobject.ar_pay_prepaid.text = ds.Tables(0).Rows(_row_gv).Item("so_dp")

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            '.SQL = "SELECT  distinct " _
                            '& "  public.so_mstr.so_oid, " _
                            '& "  public.so_mstr.so_en_id, " _
                            '& "  public.so_mstr.so_code, " _
                            '& "  public.so_mstr.so_ptnr_id_sold, " _
                            '& "  public.so_mstr.so_date, " _
                            '& "  public.so_mstr.so_si_id, " _
                            '& "  public.so_mstr.so_close_date, " _
                            '& "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                            '& "  public.si_mstr.si_desc, en_desc " _
                            '& "FROM " _
                            '& "  public.so_mstr " _
                            '& "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                            '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                            '& "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                            '& "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                            '& "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                            '& "  where public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'"

                            .SQL = "SELECT DISTINCT  " _
                                & "  public.so_mstr.so_oid, " _
                                & "  public.so_mstr.so_en_id, " _
                                & "  public.so_mstr.so_code, " _
                                & "  public.so_mstr.so_ptnr_id_sold, " _
                                & "  public.so_mstr.so_date, " _
                                & "  public.so_mstr.so_si_id, " _
                                & "  public.so_mstr.so_close_date, " _
                                & "  public.so_mstr.so_pay_type, " _
                                & "  pay_type.code_name as pay_type_name," _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.soshipd_det.soshipd_oid, " _
                                & "  True as ceklist, " _
                                & "  public.soshipd_det.soshipd_soship_oid, " _
                                & "  public.soshipd_det.soshipd_sod_oid, " _
                                & "  public.soship_mstr.soship_code, " _
                                & "  public.pt_mstr.pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.sod_det.sod_ppn_type, " _
                                & "  public.sod_det.sod_price, " _
                                & "  (sod_price * coalesce(sod_disc, 0)) AS sod_disc, " _
                                & "  public.sod_det.sod_taxable, " _
                                & "  public.sod_det.sod_tax_inc, " _
                                & "  public.sod_det.sod_tax_class, " _
                                & "  public.code_mstr.code_name AS sod_tax_class_name, " _
                                & "  (soshipd_qty - coalesce(soshipd_qty_inv, 0)) *-1 AS qty_open " _
                                & "FROM " _
                                & "  public.so_mstr " _
                                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.sod_det.sod_tax_class = public.code_mstr.code_id) " _
                                & "  INNER JOIN public.soshipd_det ON (public.sod_det.sod_oid = public.soshipd_det.soshipd_sod_oid) " _
                                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                                & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id)" _
                                & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                                & "  and public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
                                & "  and sod_ppn_type=" & SetSetring(_ppn_type) & " "

                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "ard_det")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


                fobject.ds_edit_so.tables(0).clear()

                Dim _dtrow As DataRow
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit_so.Tables(0).NewRow
                    'fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                    _dtrow("arso_oid") = Guid.NewGuid.ToString
                    _dtrow("arso_so_oid") = ds_bantu.Tables(0).Rows(i).Item("so_oid").ToString
                    _dtrow("arso_so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
                    '_dtrow("arso_so_date") = ds_bantu.Tables(0).Rows(i).Item("so_date")
                    fobject.ds_edit_so.Tables(0).Rows.Add(_dtrow)

                Next
                fobject.ds_edit_so.Tables(0).AcceptChanges()
                fobject.gv_edit_so.BestFitColumns()

                Dim ssql As String
                ssql = "SELECT  " _
                    & "  soship_date " _
                    & "FROM  " _
                    & "  public.soshipd_det " _
                    & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                    & "  inner join so_mstr on so_oid = sod_so_oid " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join code_mstr on code_id = sod_tax_class " _
                    & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                    & "  and so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
                    & "  and soship_is_shipment='Y'   " _
                    & "  order by soship_date desc"

                Dim dt As New DataTable
                dt = master_new.PGSqlConn.GetTableData(ssql)


                _eff_date = dt.Rows(0).Item("soship_date")
                '(i) disini pasti line yang terakhir

                fobject.ds_edit_shipment.Tables(0).AcceptChanges()

                fobject.ds_edit_shipment.Tables(0).Clear()

                'Dim _dtrows As DataRow
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit_shipment.Tables(0).NewRow
                    _dtrow("ars_oid") = Guid.NewGuid.ToString
                    _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
                    _dtrow("ars_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid").ToString
                    _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
                    _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                    _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                    _dtrow("sod_ppn_type") = ds_bantu.Tables(0).Rows(i).Item("sod_ppn_type")
                    _dtrow("ars_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
                    _dtrow("ars_tax_class_id") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
                    _dtrow("taxclass_name") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class_name")
                    _dtrow("ars_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
                    _dtrow("ars_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("ars_invoice") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("ars_so_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
                    _dtrow("ars_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                    _dtrow("ars_invoice_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                    _dtrow("ars_close_line") = "Y"
                    fobject.ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
                Next

                ''input so shipment detail

                'If fobject.ds_edit_so.Tables.Count = 0 Then
                '    Exit Sub
                'ElseIf fobject.ds_edit_so.Tables(0).Rows.Count = 0 Then
                '    Exit Sub
                'End If
                'Dim _so_code As String = ""
                ''Dim i As Integer

                'For i = 0 To fobject.ds_edit_so.Tables(0).Rows.Count - 1
                '    _so_code = _so_code + "'" + ds_bantu.Tables(0).Rows(i).Item("so_code") + "',"
                'Next

                '_so_code = _so_code.Substring(0, _so_code.Length - 1)

                'Try
                '    Using objcb As New master_new.WDABasepgsql("", "")
                '        With objcb
                '            .SQL = "SELECT  " _
                '                & "  soshipd_oid, True as ceklist, " _
                '                & "  soshipd_soship_oid, " _
                '                & "  soshipd_sod_oid, " _
                '                & "  soship_code, " _
                '                & "  pt_id, " _
                '                & "  pt_code, " _
                '                & "  pt_desc1, " _
                '                & "  pt_desc2, " _
                '                & "  sod_ppn_type, " _
                '                & "  sod_price, " _
                '                & "  (sod_price * coalesce(sod_disc,0)) as sod_disc, " _
                '                & "  sod_taxable, " _
                '                & "  sod_tax_inc, " _
                '                & "  sod_tax_class, " _
                '                & "  code_name as sod_tax_class_name, " _
                '                & "  (soshipd_qty - coalesce(soshipd_qty_inv,0)) * -1 as qty_open " _
                '                & "FROM  " _
                '                & "  public.soshipd_det " _
                '                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                '                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                '                & "  inner join so_mstr on so_oid = sod_so_oid " _
                '                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                '                & "  inner join code_mstr on code_id = sod_tax_class " _
                '                & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                '                & "  and public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
                '                & "  and sod_ppn_type=" & SetSetring(_ppn_type) & " " _
                '                & "  order by soship_date "
                '            .InitializeCommand()
                '            .FillDataSet(ds_ban, "soship_mstr")
                '        End With
                '    End Using
                'Catch ex As Exception
                '    MessageBox.Show(ex.Message)
                'End Try

                'fobject.ds_edit_shipment.Tables(0).Clear()

                'Dim _dtrows As DataRow
                'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                '    _dtrows = fobject.ds_edit_shipment.Tables(0).NewRow
                '    _dtrows("ars_oid") = Guid.NewGuid.ToString
                '    _dtrows("ceklist") = ds_ban.Tables(0).Rows(i).Item("ceklist")
                '    _dtrows("ars_soshipd_oid") = ds_ban.Tables(0).Rows(i).Item("soshipd_oid")
                '    _dtrows("soship_code") = ds_ban.Tables(0).Rows(i).Item("soship_code")
                '    _dtrows("pt_id") = ds_ban.Tables(0).Rows(i).Item("pt_id")
                '    _dtrows("pt_code") = ds_ban.Tables(0).Rows(i).Item("pt_code")
                '    _dtrows("pt_desc1") = ds_ban.Tables(0).Rows(i).Item("pt_desc1")
                '    _dtrows("pt_desc2") = ds_ban.Tables(0).Rows(i).Item("pt_desc2")
                '    _dtrows("sod_ppn_type") = ds_ban.Tables(0).Rows(i).Item("sod_ppn_type")
                '    _dtrows("ars_taxable") = ds_ban.Tables(0).Rows(i).Item("sod_taxable")
                '    _dtrows("ars_tax_class_id") = ds_ban.Tables(0).Rows(i).Item("sod_tax_class")
                '    _dtrows("taxclass_name") = ds_ban.Tables(0).Rows(i).Item("sod_tax_class_name")
                '    _dtrows("ars_tax_inc") = ds_ban.Tables(0).Rows(i).Item("sod_tax_inc")
                '    _dtrows("ars_open") = ds_ban.Tables(0).Rows(i).Item("qty_open")
                '    _dtrows("ars_invoice") = ds_ban.Tables(0).Rows(i).Item("qty_open")
                '    _dtrows("ars_so_price") = ds_ban.Tables(0).Rows(i).Item("sod_price")
                '    _dtrows("ars_so_disc_value") = ds_ban.Tables(0).Rows(i).Item("sod_disc")
                '    _dtrows("ars_invoice_price") = ds_ban.Tables(0).Rows(i).Item("sod_price") - ds_ban.Tables(0).Rows(i).Item("sod_disc")
                '    _dtrows("ars_close_line") = "Y"
                '    fobject.ds_edit_shipment.Tables(0).Rows.Add(_dtrows)
                'Next

                fobject.ds_edit_shipment.Tables(0).AcceptChanges()
                fobject.gv_edit_shipment.BestFitColumns()


                'fobject.gv_edit_shipment.BestFitColumns()

                'Dim i, j As Integer

                'fobject.gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = False
                'fobject.gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = False
                'fobject.gv_edit_dist.Columns("taxclass_name").Visible = False
                'fobject.gv_edit_dist.Columns("ard_tax_inc").Visible = False
                'fobject.gv_edit_dist.Columns("ard_taxable").OptionsColumn.AllowEdit = False
                'fobject.gv_edit_dist.Columns("ard_tax_inc").OptionsColumn.AllowEdit = False
                'fobject.gv_edit_dist.Columns("ard_remarks").OptionsColumn.AllowEdit = False
                'fobject.gv_edit_dist.Columns("ard_amount").OptionsColumn.AllowEdit = False

                fobject.ds_edit_dist.Tables(0).Clear()
                Dim _search As Boolean = False
                Dim _dtrowns As DataRow
                Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double

                'Dim dt_bants As New DataSet

                _invoice_price = 0
                _line_tr_pph = 0
                _line_tr_ppn = 0
                _tax_rate = 0

                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                            'Mencari prodline account untuk masing2 line receipt
                            dt_bants = New DataTable
                            dt_bants = (func_data.get_prodline_account_ar(ds_bantu.Tables(0).Rows(i).Item("pt_id")))
                            For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                'If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")) And _
                                '(fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_tax_class_id") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) Then
                                If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")) Then
                                    _search = True
                                    Exit For
                                End If
                            Next

                            If _search = True Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    'disini hanya line ppn saja
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                                    _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                    _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                                Else
                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                    (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                     fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price"))
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If
                            Else
                                _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")
                                _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")

                                _dtrowns("ard_sb_id") = 0
                                _dtrowns("sb_desc") = "-"
                                _dtrowns("ard_cc_id") = 0
                                _dtrowns("cc_desc") = "-"
                                _dtrowns("ard_taxable") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                                _dtrowns("ard_tax_class_id") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                                _dtrowns("taxclass_name") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                                _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    'disini hanya dicari ppn nya saja
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                                    _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                    _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                    _dtrowns("ard_amount") = _invoice_price
                                Else
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                                    _dtrowns("ard_amount") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                                End If


                                _dtrowns("ard_tax_distribution") = "Y"

                                fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        End If
                    End If
                Next

                'Untuk PPN dan PPH
                Dim _ppn, _pph As Double
                _search = False
                _ppn = 0
                _pph = 0

                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" And fobject.ds_edit_shipment.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                            'Mencari taxrate account ar untuk masing2 line receipt
                            dt_bants = New DataTable
                            dt_bants = (func_data.get_taxrate_ar_account(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                            '1. PPN
                            For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                    _search = True
                                    Exit For
                                End If
                            Next
                            'Exit Sub
                            If _search = True Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    '_sod_cost = fobject.ds_edit.Tables(0).Rows(i).Item("sod_cost") - (_tax_rate * (fobject.ds_edit.Tables(0).Rows(i).Item("sod_cost") / (1 + _tax_rate)))
                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                    _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                Else
                                    _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                End If

                                fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            Else
                                _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")
                                _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")

                                _dtrowns("ard_sb_id") = 0
                                _dtrowns("sb_desc") = "-"
                                _dtrowns("ard_cc_id") = 0
                                _dtrowns("cc_desc") = "-"
                                _dtrowns("ard_taxable") = "N"
                                _dtrowns("ard_tax_class_id") = DBNull.Value
                                _dtrowns("taxclass_name") = DBNull.Value
                                _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                    _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                Else
                                    _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                End If

                                _dtrowns("ard_amount") = _ppn
                                _dtrowns("ard_tax_distribution") = "Y"
                                fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            End If

                            '1. PPH
                            For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                    _search = True
                                    Exit For
                                End If
                            Next

                            If _search = True Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    '_pph = (dt_bants.Rows(1).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(1).Item("taxr_rate") / 100))
                                    '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                Else
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(1).Item("taxr_rate") / 100)
                                End If

                                fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            Else
                                _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                _dtrowns("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")
                                _dtrowns("ac_code") = dt_bants.Rows(1).Item("ac_code")

                                _dtrowns("ard_sb_id") = 0
                                _dtrowns("sb_desc") = "-"
                                _dtrowns("ard_cc_id") = 0
                                _dtrowns("cc_desc") = "-"
                                _dtrowns("ard_taxable") = "N"
                                _dtrowns("ard_tax_class_id") = DBNull.Value
                                _dtrowns("taxclass_name") = DBNull.Value
                                _dtrowns("ard_remarks") = dt_bants.Rows(1).Item("ac_name")
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    '_pph = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                    '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                Else
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(1).Item("taxr_rate") / 100)
                                End If

                                _dtrowns("ard_amount") = _pph
                                _dtrowns("ard_tax_distribution") = "Y"
                                fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        End If
                    End If
                Next

                'Ini untuk ar discount
                _search = False
                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                            If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                                'Mencari prodline account untuk masing2 line receipt
                                dt_bants = New DataTable
                                dt_bants = (func_data.get_prodline_account_ar_discount(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                                For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                    If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")) Then
                                        _search = True
                                        Exit For
                                    End If
                                Next

                                If _search = True Then
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        'disini hanya line ppn saja
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
                                        _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                        _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                        fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

                                        fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                        (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                         _invoice_price)
                                        fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                    End If
                                Else
                                    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                    _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")
                                    _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")
                                    _dtrowns("ac_name") = dt_bants.Rows(0).Item("ac_name")

                                    _dtrowns("ard_sb_id") = 0
                                    _dtrowns("sb_desc") = "-"
                                    _dtrowns("ard_cc_id") = 0
                                    _dtrowns("cc_desc") = "-"
                                    _dtrowns("ard_taxable") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                                    _dtrowns("ard_tax_class_id") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                                    _dtrowns("taxclass_name") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                                    _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        'disini hanya dicari ppn nya saja
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'agar mengurangi
                                        _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                        _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                        _dtrowns("ard_amount") = _invoice_price
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1
                                        _dtrowns("ard_amount") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                                    End If


                                    _dtrowns("ard_tax_distribution") = "Y"

                                    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If
                            End If
                        End If
                    End If
                Next

                'Untuk PPN dan PPH yang ar discount
                _search = False
                _ppn = 0
                _pph = 0

                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                            If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                                'Mencari taxrate account ap untuk masing2 line receipt
                                dt_bants = New DataTable
                                dt_bants = (func_data.get_taxrate_ar_account(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                                '1. PPN
                                For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                    If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                        _search = True
                                        Exit For
                                    End If
                                Next

                                If _search = True Then
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                        _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                        _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                    End If

                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                Else
                                    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                    _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")
                                    _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")
                                    _dtrowns("ac_name") = dt_bants.Rows(0).Item("ac_name")

                                    _dtrowns("ard_sb_id") = 0
                                    _dtrowns("sb_desc") = "-"
                                    _dtrowns("ard_cc_id") = 0
                                    _dtrowns("cc_desc") = "-"
                                    _dtrowns("ard_taxable") = "N"
                                    _dtrowns("ard_tax_class_id") = DBNull.Value
                                    _dtrowns("taxclass_name") = DBNull.Value
                                    _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                        _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                        _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                    End If

                                    _dtrowns("ard_amount") = _ppn
                                    _dtrowns("ard_tax_distribution") = "Y"
                                    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If

                                '1. PPH
                                For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                    If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                        _search = True
                                        Exit For
                                    End If
                                Next

                                If _search = True Then
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        '_pph = (dt_bants.Rows(1).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(1).Item("taxr_rate") / 100))
                                        '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                        _pph = (_invoice_price - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                        _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                        _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(1).Item("taxr_rate") / 100)
                                    End If

                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                Else
                                    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                    _dtrowns("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")
                                    _dtrowns("ac_code") = dt_bants.Rows(1).Item("ac_code")
                                    _dtrowns("ac_name") = dt_bants.Rows(1).Item("ac_name")

                                    _dtrowns("ard_sb_id") = 0
                                    _dtrowns("sb_desc") = "-"
                                    _dtrowns("ard_cc_id") = 0
                                    _dtrowns("cc_desc") = "-"
                                    _dtrowns("ard_taxable") = "N"
                                    _dtrowns("ard_tax_class_id") = DBNull.Value
                                    _dtrowns("taxclass_name") = DBNull.Value
                                    _dtrowns("ard_remarks") = dt_bants.Rows(1).Item("ac_name")
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        '_pph = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                        '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                        _pph = (_invoice_price - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                        _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                        _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                                    End If

                                    _dtrowns("ard_amount") = _pph
                                    _dtrowns("ard_tax_distribution") = "Y"
                                    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If
                            End If
                        End If
                    End If
                Next
                '**************************************************

                ''Insert ke ds untuk yang prepayment kalau ada
                'If ar_pay_prepaid.EditValue < 0 Then
                '    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                '    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                '    _dtrowns("ard_ac_id") = ar_ac_prepaid.EditValue
                '    _dtrowns("ac_code") = ar_ac_prepaid.GetColumnValue("ac_code")

                '    _dtrowns("ard_sb_id") = 0
                '    _dtrowns("sb_desc") = "-"
                '    _dtrowns("ard_cc_id") = 0
                '    _dtrowns("cc_desc") = "-"
                '    _dtrowns("ard_taxable") = "N"
                '    _dtrowns("ard_tax_class_id") = DBNull.Value
                '    _dtrowns("taxclass_name") = DBNull.Value
                '    _dtrowns("ard_remarks") = ar_ac_prepaid.GetColumnValue("ac_name")
                '    _dtrowns("ard_amount") = ar_pay_prepaid.EditValue
                '    _dtrowns("ard_tax_distribution") = "Y"
                '    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                '    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                '    '**********************************************************
                'End If

                For i = fobject.ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
                    If fobject.ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
                        fobject.ds_edit_dist.Tables(0).Rows(i).Delete()
                    End If
                Next
                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                fobject.gv_edit_dist.BestFitColumns()

                'detail piutang
                Dim x, y, z As Integer
                Dim _total_dp_all, _total_dp_item, _total_payment_all, _total_payment_item As Double
                Dim _tot_qty As Integer = 0
                Dim _dtrows As DataRow

                _total_dp_all = 0
                _total_payment_all = 0
                fobject.ds_edit_piutang.Tables(0).Clear()

                For x = 0 To fobject.ds_edit_so.Tables(0).Rows.Count - 1
                    'Dim ds_sod_piutang = New DataSet
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "SELECT  " _
                                    & "  public.so_mstr.so_oid, " _
                                    & "  public.so_mstr.so_code, " _
                                    & "  public.so_mstr.so_payment_date, " _
                                    & "  public.so_mstr.so_pay_type, " _
                                    & "  public.sod_det.sod_qty, " _
                                    & "  public.sod_det.sod_oid, " _
                                    & "  public.sod_det.sod_so_oid, " _
                                    & "  public.sod_det.sod_seq, " _
                                    & "  public.sod_det.sod_pt_id, " _
                                    & "  public.sod_det.sod_qty_allocated, " _
                                    & "  public.sod_det.sod_qty_picked, " _
                                    & "  public.sod_det.sod_qty_shipment, " _
                                    & "  public.sod_det.sod_qty_pending_inv, " _
                                    & "  public.sod_det.sod_qty_invoice, " _
                                    & "  public.sod_det.sod_cost, " _
                                    & "  public.sod_det.sod_price, " _
                                    & "  public.sod_det.sod_disc, " _
                                    & "  public.sod_det.sod_qty_real, " _
                                    & "  public.sod_det.sod_status, " _
                                    & "  public.sod_det.sod_dt, " _
                                    & "  public.sod_det.sod_payment, " _
                                    & "  public.sod_det.sod_dp, " _
                                    & "  public.sod_det.sod_sales_unit, " _
                                    & "  public.sod_det.sod_serial, " _
                                    & "  public.sod_det.sod_qty_return, " _
                                    & "  public.sod_det.sod_qty_ir " _
                                    & "FROM " _
                                    & "  public.sod_det " _
                                    & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid)" _
                                    & "  where so_code = " & SetSetring(fobject.ds_edit_so.Tables(0).Rows(x).Item("arso_so_code").ToString)
                                .InitializeCommand()
                                .FillDataSet(ds_sod_piutang, "list_sod")
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    For y = 0 To ds_sod_piutang.Tables(0).Rows.Count - 1
                        _total_dp_item = 0
                        _total_payment_item = 0
                        For z = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                            If ds_sod_piutang.Tables(0).Rows(y).Item("sod_pt_id").ToString = fobject.ds_edit_shipment.Tables(0).Rows(z).Item("pt_id") Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice") < 0 Then
                                    _total_dp_item = _total_dp_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_dp") * -1.0)
                                    _total_payment_item = _total_payment_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_payment") * -1.0) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)

                                Else
                                    _total_dp_item = _total_dp_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_dp").ToString) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)
                                    _total_payment_item = _total_payment_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_payment")) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)

                                End If
                            End If
                        Next
                        _total_dp_all = _total_dp_all + _total_dp_item
                        _total_payment_all = _total_payment_all + _total_payment_item
                    Next

                    Dim interval As Integer

                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .Connection.Open()
                                .Command = .Connection.CreateCommand
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "select code_usr_1 From code_mstr " + _
                                                       " where code_field = 'payment_type' " + _
                                                       " and code_id = " + ds_sod_piutang.Tables(0).Rows(0).Item("so_pay_type").ToString
                                .InitializeCommand()
                                .DataReader = .Command.ExecuteReader
                                While .DataReader.Read
                                    _interval = .DataReader("code_usr_1").ToString
                                End While
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try


                    Dim _date As Date = ds_sod_piutang.Tables(0).Rows(0).Item("so_payment_date").ToString

                    _dtrow = fobject.ds_edit_piutang.Tables(0).NewRow
                    _dtrow("sokp_oid") = Guid.NewGuid.ToString

                    _dtrow("sokp_so_oid") = ds_sod_piutang.Tables(0).Rows(0).Item("so_oid").ToString
                    _dtrow("sokp_seq") = 0

                    _dtrow("sokp_amount") = _total_dp_all
                    _dtrow("sokp_due_date") = _date
                    _dtrow("sokp_amount_pay") = 0
                    _dtrow("sokp_description") = "-"

                    fobject.ds_edit_piutang.Tables(0).Rows.Add(_dtrow)
                    fobject.ds_edit_piutang.Tables(0).AcceptChanges()

                    Dim _total_piutang As Double

                    _total_piutang = _total_dp_all
                    For i = 0 To _interval - 1
                        _date = _date.AddMonths(1)

                        _dtrow = fobject.ds_edit_piutang.Tables(0).NewRow
                        _dtrow("sokp_oid") = Guid.NewGuid.ToString

                        _dtrow("sokp_so_oid") = ds_sod_piutang.Tables(0).Rows(0).Item("so_oid").ToString
                        _dtrow("sokp_seq") = i + 1

                        _dtrow("sokp_amount") = _total_payment_all
                        _total_piutang += _total_payment_all

                        _dtrow("sokp_due_date") = _date
                        _dtrow("sokp_amount_pay") = 0
                        _dtrow("sokp_description") = "-"

                        fobject.ds_edit_piutang.Tables(0).Rows.Add(_dtrow)
                        fobject.ds_edit_piutang.Tables(0).AcceptChanges()

                    Next
                    fobject.gv_edit_piutang.BestFitColumns()
                Next

            ElseIf _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()

            ElseIf _obj.name = "par_so" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            End If




            '====

        ElseIf fobject.name = "FDRCRMemo20240802" Then
            If _obj.name = "ar_so_ref_code" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
                fobject._ar_bill_to_id = ds.Tables(0).Rows(_row_gv).Item("so_ptnr_id_sold")
                fobject.ar_bill_to.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold")
                fobject._ar_credit_term_id = ds.Tables(0).Rows(_row_gv).Item("so_credit_term")
                fobject.ar_credit_term.text = ds.Tables(0).Rows(_row_gv).Item("credit_term_name")
                fobject._ar_bk_ids = ds.Tables(0).Rows(_row_gv).Item("so_bk_id")
                fobject.ar_bk_id.text = ds.Tables(0).Rows(_row_gv).Item("bk_name")
                fobject._ar_ac_ids = ds.Tables(0).Rows(_row_gv).Item("so_ar_ac_id")
                fobject.ar_ac_id.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

                'If ds.Tables(0).Rows(_row_gv).Item("so_taxable") = "Y" Then
                '    fobject.ar_taxable.Checked = True

                'End If

                'If ds.Tables(0).Rows(_row_gv).Item("so_tax_inc") = "Y" Then
                '    fobject.ar_tax_inc.Checked = True

                'End If

                'fobject._ar_tax_class_ids = ds.Tables(0).Rows(_row_gv).Item("so_tax_class")
                'fobject.ar_tax_class_id.text = ds.Tables(0).Rows(_row_gv).Item("tax_class_name")
                'fobject._ar_ppn_type_id = ds.Tables(0).Rows(_row_gv).Item("so_ppn_type")
                'fobject.ar_pay_prepaid.text = ds.Tables(0).Rows(_row_gv).Item("so_dp")

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            '.SQL = "SELECT  distinct " _
                            '& "  public.so_mstr.so_oid, " _
                            '& "  public.so_mstr.so_en_id, " _
                            '& "  public.so_mstr.so_code, " _
                            '& "  public.so_mstr.so_ptnr_id_sold, " _
                            '& "  public.so_mstr.so_date, " _
                            '& "  public.so_mstr.so_si_id, " _
                            '& "  public.so_mstr.so_close_date, " _
                            '& "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                            '& "  public.si_mstr.si_desc, en_desc " _
                            '& "FROM " _
                            '& "  public.so_mstr " _
                            '& "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                            '& "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                            '& "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                            '& "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                            '& "  INNER JOIN public.code_mstr ON (public.so_mstr.so_pay_type = public.code_mstr.code_id)" _
                            '& "  where public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'"

                            .SQL = "SELECT DISTINCT  " _
                                & "  public.so_mstr.so_oid, " _
                                & "  public.so_mstr.so_en_id, " _
                                & "  public.so_mstr.so_code, " _
                                & "  public.so_mstr.so_ptnr_id_sold, " _
                                & "  public.so_mstr.so_date, " _
                                & "  public.so_mstr.so_si_id, " _
                                & "  public.so_mstr.so_close_date, " _
                                & "  public.so_mstr.so_pay_type, " _
                                & "  pay_type.code_name as pay_type_name," _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.soshipd_det.soshipd_oid, " _
                                & "  True as ceklist, " _
                                & "  public.soshipd_det.soshipd_soship_oid, " _
                                & "  public.soshipd_det.soshipd_sod_oid, " _
                                & "  public.soship_mstr.soship_code, " _
                                & "  public.pt_mstr.pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.sod_det.sod_ppn_type, " _
                                & "  public.sod_det.sod_price, " _
                                & "  (sod_price * coalesce(sod_disc, 0)) AS sod_disc, " _
                                & "  public.sod_det.sod_taxable, " _
                                & "  public.sod_det.sod_tax_inc, " _
                                & "  public.sod_det.sod_tax_class, " _
                                & "  public.code_mstr.code_name AS sod_tax_class_name, " _
                                & "  (soshipd_qty - coalesce(soshipd_qty_inv, 0)) *-1 AS qty_open " _
                                & "FROM " _
                                & "  public.so_mstr " _
                                & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.sod_det.sod_tax_class = public.code_mstr.code_id) " _
                                & "  INNER JOIN public.soshipd_det ON (public.sod_det.sod_oid = public.soshipd_det.soshipd_sod_oid) " _
                                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                                & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id)" _
                                & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                                            & "  and public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
                                            & "  and sod_ppn_type=" & SetSetring(_ppn_type) & " "


                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "ard_det")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


                fobject.ds_edit_so.tables(0).clear()

                Dim _dtrow As DataRow
                'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit_so.Tables(0).NewRow
                'fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                _dtrow("arso_oid") = Guid.NewGuid.ToString
                _dtrow("arso_so_oid") = ds_bantu.Tables(0).Rows(i).Item("so_oid").ToString
                _dtrow("arso_so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
                '_dtrow("arso_so_date") = ds_bantu.Tables(0).Rows(i).Item("so_date")
                fobject.ds_edit_so.Tables(0).Rows.Add(_dtrow)

                'Next
                fobject.ds_edit_so.Tables(0).AcceptChanges()
                fobject.gv_edit_so.BestFitColumns()

                Dim ssql As String
                ssql = "SELECT  " _
                    & "  soship_date " _
                    & "FROM  " _
                    & "  public.soshipd_det " _
                    & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                    & "  inner join so_mstr on so_oid = sod_so_oid " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join code_mstr on code_id = sod_tax_class " _
                    & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                    & "  and so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
                    & "  and soship_is_shipment='Y'   " _
                    & "  order by soship_date desc"

                Dim dt As New DataTable
                dt = master_new.PGSqlConn.GetTableData(ssql)


                _eff_date = dt.Rows(0).Item("soship_date")
                '(i) disini pasti line yang terakhir

                fobject.ds_edit_shipment.Tables(0).AcceptChanges()

                fobject.ds_edit_shipment.Tables(0).Clear()

                'Dim _dtrows As DataRow
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit_shipment.Tables(0).NewRow
                    _dtrow("ars_oid") = Guid.NewGuid.ToString
                    _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
                    _dtrow("ars_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid").ToString
                    _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
                    _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                    _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                    _dtrow("sod_ppn_type") = ds_bantu.Tables(0).Rows(i).Item("sod_ppn_type")
                    _dtrow("ars_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
                    _dtrow("ars_tax_class_id") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
                    _dtrow("taxclass_name") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class_name")
                    _dtrow("ars_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
                    _dtrow("ars_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("ars_invoice") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("ars_so_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
                    _dtrow("ars_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                    _dtrow("ars_invoice_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                    _dtrow("ars_close_line") = "Y"
                    fobject.ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
                Next

                ''input so shipment detail

                'If fobject.ds_edit_so.Tables.Count = 0 Then
                '    Exit Sub
                'ElseIf fobject.ds_edit_so.Tables(0).Rows.Count = 0 Then
                '    Exit Sub
                'End If
                'Dim _so_code As String = ""
                ''Dim i As Integer

                'For i = 0 To fobject.ds_edit_so.Tables(0).Rows.Count - 1
                '    _so_code = _so_code + "'" + ds_bantu.Tables(0).Rows(i).Item("so_code") + "',"
                'Next

                '_so_code = _so_code.Substring(0, _so_code.Length - 1)

                'Try
                '    Using objcb As New master_new.WDABasepgsql("", "")
                '        With objcb
                '            .SQL = "SELECT  " _
                '                & "  soshipd_oid, True as ceklist, " _
                '                & "  soshipd_soship_oid, " _
                '                & "  soshipd_sod_oid, " _
                '                & "  soship_code, " _
                '                & "  pt_id, " _
                '                & "  pt_code, " _
                '                & "  pt_desc1, " _
                '                & "  pt_desc2, " _
                '                & "  sod_ppn_type, " _
                '                & "  sod_price, " _
                '                & "  (sod_price * coalesce(sod_disc,0)) as sod_disc, " _
                '                & "  sod_taxable, " _
                '                & "  sod_tax_inc, " _
                '                & "  sod_tax_class, " _
                '                & "  code_name as sod_tax_class_name, " _
                '                & "  (soshipd_qty - coalesce(soshipd_qty_inv,0)) * -1 as qty_open " _
                '                & "FROM  " _
                '                & "  public.soshipd_det " _
                '                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                '                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                '                & "  inner join so_mstr on so_oid = sod_so_oid " _
                '                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                '                & "  inner join code_mstr on code_id = sod_tax_class " _
                '                & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                '                & "  and public.so_mstr.so_code = '" + ds.Tables(0).Rows(_row_gv).Item("so_code") + "'" _
                '                & "  and sod_ppn_type=" & SetSetring(_ppn_type) & " " _
                '                & "  order by soship_date "
                '            .InitializeCommand()
                '            .FillDataSet(ds_ban, "soship_mstr")
                '        End With
                '    End Using
                'Catch ex As Exception
                '    MessageBox.Show(ex.Message)
                'End Try

                'fobject.ds_edit_shipment.Tables(0).Clear()

                'Dim _dtrows As DataRow
                'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                '    _dtrows = fobject.ds_edit_shipment.Tables(0).NewRow
                '    _dtrows("ars_oid") = Guid.NewGuid.ToString
                '    _dtrows("ceklist") = ds_ban.Tables(0).Rows(i).Item("ceklist")
                '    _dtrows("ars_soshipd_oid") = ds_ban.Tables(0).Rows(i).Item("soshipd_oid")
                '    _dtrows("soship_code") = ds_ban.Tables(0).Rows(i).Item("soship_code")
                '    _dtrows("pt_id") = ds_ban.Tables(0).Rows(i).Item("pt_id")
                '    _dtrows("pt_code") = ds_ban.Tables(0).Rows(i).Item("pt_code")
                '    _dtrows("pt_desc1") = ds_ban.Tables(0).Rows(i).Item("pt_desc1")
                '    _dtrows("pt_desc2") = ds_ban.Tables(0).Rows(i).Item("pt_desc2")
                '    _dtrows("sod_ppn_type") = ds_ban.Tables(0).Rows(i).Item("sod_ppn_type")
                '    _dtrows("ars_taxable") = ds_ban.Tables(0).Rows(i).Item("sod_taxable")
                '    _dtrows("ars_tax_class_id") = ds_ban.Tables(0).Rows(i).Item("sod_tax_class")
                '    _dtrows("taxclass_name") = ds_ban.Tables(0).Rows(i).Item("sod_tax_class_name")
                '    _dtrows("ars_tax_inc") = ds_ban.Tables(0).Rows(i).Item("sod_tax_inc")
                '    _dtrows("ars_open") = ds_ban.Tables(0).Rows(i).Item("qty_open")
                '    _dtrows("ars_invoice") = ds_ban.Tables(0).Rows(i).Item("qty_open")
                '    _dtrows("ars_so_price") = ds_ban.Tables(0).Rows(i).Item("sod_price")
                '    _dtrows("ars_so_disc_value") = ds_ban.Tables(0).Rows(i).Item("sod_disc")
                '    _dtrows("ars_invoice_price") = ds_ban.Tables(0).Rows(i).Item("sod_price") - ds_ban.Tables(0).Rows(i).Item("sod_disc")
                '    _dtrows("ars_close_line") = "Y"
                '    fobject.ds_edit_shipment.Tables(0).Rows.Add(_dtrows)
                'Next

                fobject.ds_edit_shipment.Tables(0).AcceptChanges()
                fobject.gv_edit_shipment.BestFitColumns()


                'fobject.gv_edit_shipment.BestFitColumns()

                'Dim i, j As Integer

                'fobject.gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = False
                'fobject.gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = False
                'fobject.gv_edit_dist.Columns("taxclass_name").Visible = False
                'fobject.gv_edit_dist.Columns("ard_tax_inc").Visible = False
                'fobject.gv_edit_dist.Columns("ard_taxable").OptionsColumn.AllowEdit = False
                'fobject.gv_edit_dist.Columns("ard_tax_inc").OptionsColumn.AllowEdit = False
                'fobject.gv_edit_dist.Columns("ard_remarks").OptionsColumn.AllowEdit = False
                'fobject.gv_edit_dist.Columns("ard_amount").OptionsColumn.AllowEdit = False

                fobject.ds_edit_dist.Tables(0).Clear()
                Dim _search As Boolean = False
                Dim _dtrowns As DataRow
                Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double

                'Dim dt_bants As New DataSet

                _invoice_price = 0
                _line_tr_pph = 0
                _line_tr_ppn = 0
                _tax_rate = 0

                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                            'Mencari prodline account untuk masing2 line receipt
                            dt_bants = New DataTable
                            dt_bants = (func_data.get_prodline_account_ar(ds_bantu.Tables(0).Rows(i).Item("pt_id")))
                            For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                'If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")) And _
                                '(fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_tax_class_id") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) Then
                                If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")) Then
                                    _search = True
                                    Exit For
                                End If
                            Next

                            If _search = True Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    'disini hanya line ppn saja
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                                    _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                    _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                                Else
                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                    (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                     fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price"))
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If
                            Else
                                _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")
                                _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")

                                _dtrowns("ard_sb_id") = 0
                                _dtrowns("sb_desc") = "-"
                                _dtrowns("ard_cc_id") = 0
                                _dtrowns("cc_desc") = "-"
                                _dtrowns("ard_taxable") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                                _dtrowns("ard_tax_class_id") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                                _dtrowns("taxclass_name") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                                _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    'disini hanya dicari ppn nya saja
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                                    _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                    _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                    _dtrowns("ard_amount") = _invoice_price
                                Else
                                    _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                                    _dtrowns("ard_amount") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                                End If


                                _dtrowns("ard_tax_distribution") = "Y"

                                fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        End If
                    End If
                Next

                'Untuk PPN dan PPH
                Dim _ppn, _pph As Double
                _search = False
                _ppn = 0
                _pph = 0

                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" And fobject.ds_edit_shipment.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                            'Mencari taxrate account ar untuk masing2 line receipt
                            dt_bants = New DataTable
                            dt_bants = (func_data.get_taxrate_ar_account(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                            '1. PPN
                            For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                    _search = True
                                    Exit For
                                End If
                            Next
                            'Exit Sub
                            If _search = True Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    '_sod_cost = fobject.ds_edit.Tables(0).Rows(i).Item("sod_cost") - (_tax_rate * (fobject.ds_edit.Tables(0).Rows(i).Item("sod_cost") / (1 + _tax_rate)))
                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                    _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                Else
                                    _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                End If

                                fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            Else
                                _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")
                                _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")

                                _dtrowns("ard_sb_id") = 0
                                _dtrowns("sb_desc") = "-"
                                _dtrowns("ard_cc_id") = 0
                                _dtrowns("cc_desc") = "-"
                                _dtrowns("ard_taxable") = "N"
                                _dtrowns("ard_tax_class_id") = DBNull.Value
                                _dtrowns("taxclass_name") = DBNull.Value
                                _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                    _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                Else
                                    _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                End If

                                _dtrowns("ard_amount") = _ppn
                                _dtrowns("ard_tax_distribution") = "Y"
                                fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            End If

                            '1. PPH
                            For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                    _search = True
                                    Exit For
                                End If
                            Next

                            If _search = True Then
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                    'Item Price - Tax Amount = Taxable Base                            
                                    '100.00 - 9.09 = 90.91 
                                    '_pph = (dt_bants.Rows(1).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(1).Item("taxr_rate") / 100))
                                    '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                Else
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(1).Item("taxr_rate") / 100)
                                End If

                                fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            Else
                                _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                _dtrowns("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")
                                _dtrowns("ac_code") = dt_bants.Rows(1).Item("ac_code")

                                _dtrowns("ard_sb_id") = 0
                                _dtrowns("sb_desc") = "-"
                                _dtrowns("ard_cc_id") = 0
                                _dtrowns("cc_desc") = "-"
                                _dtrowns("ard_taxable") = "N"
                                _dtrowns("ard_tax_class_id") = DBNull.Value
                                _dtrowns("taxclass_name") = DBNull.Value
                                _dtrowns("ard_remarks") = dt_bants.Rows(1).Item("ac_name")
                                If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                    '_pph = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                    '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                    _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                Else
                                    _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bants.Rows(1).Item("taxr_rate") / 100)
                                End If

                                _dtrowns("ard_amount") = _pph
                                _dtrowns("ard_tax_distribution") = "Y"
                                fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        End If
                    End If
                Next

                'Ini untuk ar discount
                _search = False
                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                            If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                                'Mencari prodline account untuk masing2 line receipt
                                dt_bants = New DataTable
                                dt_bants = (func_data.get_prodline_account_ar_discount(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                                For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                    If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")) Then
                                        _search = True
                                        Exit For
                                    End If
                                Next

                                If _search = True Then
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        'disini hanya line ppn saja
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
                                        _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                        _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                        fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

                                        fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                        (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                         _invoice_price)
                                        fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                    End If
                                Else
                                    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                    _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("ac_id")
                                    _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")
                                    _dtrowns("ac_name") = dt_bants.Rows(0).Item("ac_name")

                                    _dtrowns("ard_sb_id") = 0
                                    _dtrowns("sb_desc") = "-"
                                    _dtrowns("ard_cc_id") = 0
                                    _dtrowns("cc_desc") = "-"
                                    _dtrowns("ard_taxable") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                                    _dtrowns("ard_tax_class_id") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                                    _dtrowns("taxclass_name") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                                    _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        'disini hanya dicari ppn nya saja
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'agar mengurangi
                                        _tax_rate = func_coll.get_ppn(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                        _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                        _dtrowns("ard_amount") = _invoice_price
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1
                                        _dtrowns("ard_amount") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                                    End If


                                    _dtrowns("ard_tax_distribution") = "Y"

                                    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If
                            End If
                        End If
                    End If
                Next

                'Untuk PPN dan PPH yang ar discount
                _search = False
                _ppn = 0
                _pph = 0

                For i = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                        If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                            If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                                'Mencari taxrate account ap untuk masing2 line receipt
                                dt_bants = New DataTable
                                dt_bants = (func_data.get_taxrate_ar_account(fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                                '1. PPN
                                For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                    If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                        _search = True
                                        Exit For
                                    End If
                                Next

                                If _search = True Then
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                        _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                        _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                    End If

                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                Else
                                    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                    _dtrowns("ard_ac_id") = dt_bants.Rows(0).Item("taxr_ac_sales_id")
                                    _dtrowns("ac_code") = dt_bants.Rows(0).Item("ac_code")
                                    _dtrowns("ac_name") = dt_bants.Rows(0).Item("ac_name")

                                    _dtrowns("ard_sb_id") = 0
                                    _dtrowns("sb_desc") = "-"
                                    _dtrowns("ard_cc_id") = 0
                                    _dtrowns("cc_desc") = "-"
                                    _dtrowns("ard_taxable") = "N"
                                    _dtrowns("ard_tax_class_id") = DBNull.Value
                                    _dtrowns("taxclass_name") = DBNull.Value
                                    _dtrowns("ard_remarks") = dt_bants.Rows(0).Item("ac_name")

                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100))
                                        _ppn = _ppn * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                        _ppn = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(0).Item("taxr_rate") / 100)
                                    End If

                                    _dtrowns("ard_amount") = _ppn
                                    _dtrowns("ard_tax_distribution") = "Y"
                                    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If

                                '1. PPH
                                For j = 0 To fobject.ds_edit_dist.Tables(0).Rows.Count - 1
                                    If (fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                        _search = True
                                        Exit For
                                    End If
                                Next

                                If _search = True Then
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                        'Item Price - Tax Amount = Taxable Base                            
                                        '100.00 - 9.09 = 90.91 
                                        '_pph = (dt_bants.Rows(1).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(1).Item("taxr_rate") / 100))
                                        '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                        _pph = (_invoice_price - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                        _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                        _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(1).Item("taxr_rate") / 100)
                                    End If

                                    fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = fobject.ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                Else
                                    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                                    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                                    _dtrowns("ard_ac_id") = dt_bants.Rows(1).Item("taxr_ac_sales_id")
                                    _dtrowns("ac_code") = dt_bants.Rows(1).Item("ac_code")
                                    _dtrowns("ac_name") = dt_bants.Rows(1).Item("ac_name")

                                    _dtrowns("ard_sb_id") = 0
                                    _dtrowns("sb_desc") = "-"
                                    _dtrowns("ard_cc_id") = 0
                                    _dtrowns("cc_desc") = "-"
                                    _dtrowns("ard_taxable") = "N"
                                    _dtrowns("ard_tax_class_id") = DBNull.Value
                                    _dtrowns("taxclass_name") = DBNull.Value
                                    _dtrowns("ard_remarks") = dt_bants.Rows(1).Item("ac_name")
                                    If fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                        '_pph = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                        '_pph = _pph * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

                                        _ppn = (dt_bants.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bants.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                        _pph = (_invoice_price - _ppn) * fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                        _pph = _pph * (dt_bants.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                                    Else
                                        _invoice_price = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                        _pph = (fobject.ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bants.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                                    End If

                                    _dtrowns("ard_amount") = _pph
                                    _dtrowns("ard_tax_distribution") = "Y"
                                    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                                    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                                End If
                            End If
                        End If
                    End If
                Next
                '**************************************************

                ''Insert ke ds untuk yang prepayment kalau ada
                'If ar_pay_prepaid.EditValue < 0 Then
                '    _dtrowns = fobject.ds_edit_dist.Tables(0).NewRow
                '    _dtrowns("ard_oid") = Guid.NewGuid.ToString

                '    _dtrowns("ard_ac_id") = ar_ac_prepaid.EditValue
                '    _dtrowns("ac_code") = ar_ac_prepaid.GetColumnValue("ac_code")

                '    _dtrowns("ard_sb_id") = 0
                '    _dtrowns("sb_desc") = "-"
                '    _dtrowns("ard_cc_id") = 0
                '    _dtrowns("cc_desc") = "-"
                '    _dtrowns("ard_taxable") = "N"
                '    _dtrowns("ard_tax_class_id") = DBNull.Value
                '    _dtrowns("taxclass_name") = DBNull.Value
                '    _dtrowns("ard_remarks") = ar_ac_prepaid.GetColumnValue("ac_name")
                '    _dtrowns("ard_amount") = ar_pay_prepaid.EditValue
                '    _dtrowns("ard_tax_distribution") = "Y"
                '    fobject.ds_edit_dist.Tables(0).Rows.Add(_dtrowns)
                '    fobject.ds_edit_dist.Tables(0).AcceptChanges()
                '    '**********************************************************
                'End If

                For i = fobject.ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
                    If fobject.ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
                        fobject.ds_edit_dist.Tables(0).Rows(i).Delete()
                    End If
                Next
                fobject.ds_edit_dist.Tables(0).AcceptChanges()
                fobject.gv_edit_dist.BestFitColumns()

                'detail piutang
                '    Dim x, y, z As Integer
                '    Dim _total_dp_all, _total_dp_item, _total_payment_all, _total_payment_item As Double
                '    Dim _tot_qty As Integer = 0
                '    Dim _dtrows As DataRow

                '    _total_dp_all = 0
                '    _total_payment_all = 0

                '    fobject.ds_edit_piutang.Tables(0).Clear()

                '    For x = 0 To fobject.ds_edit_so.Tables(0).Rows.Count - 1
                '        'Dim ds_sod_piutang = New DataSet
                '        Try
                '            Using objcb As New master_new.WDABasepgsql("", "")
                '                With objcb
                '                    .SQL = "SELECT  " _
                '                        & "  public.so_mstr.so_oid, " _
                '                        & "  public.so_mstr.so_code, " _
                '                        & "  public.so_mstr.so_payment_date, " _
                '                        & "  public.so_mstr.so_pay_type, " _
                '                        & "  public.sod_det.sod_qty, " _
                '                        & "  public.sod_det.sod_oid, " _
                '                        & "  public.sod_det.sod_so_oid, " _
                '                        & "  public.sod_det.sod_seq, " _
                '                        & "  public.sod_det.sod_pt_id, " _
                '                        & "  public.sod_det.sod_qty_allocated, " _
                '                        & "  public.sod_det.sod_qty_picked, " _
                '                        & "  public.sod_det.sod_qty_shipment, " _
                '                        & "  public.sod_det.sod_qty_pending_inv, " _
                '                        & "  public.sod_det.sod_qty_invoice, " _
                '                        & "  public.sod_det.sod_cost, " _
                '                        & "  public.sod_det.sod_price, " _
                '                        & "  public.sod_det.sod_disc, " _
                '                        & "  public.sod_det.sod_qty_real, " _
                '                        & "  public.sod_det.sod_status, " _
                '                        & "  public.sod_det.sod_dt, " _
                '                        & "  public.sod_det.sod_payment, " _
                '                        & "  public.sod_det.sod_dp, " _
                '                        & "  public.sod_det.sod_sales_unit, " _
                '                        & "  public.sod_det.sod_serial, " _
                '                        & "  public.sod_det.sod_qty_return, " _
                '                        & "  public.sod_det.sod_qty_ir " _
                '                        & "FROM " _
                '                        & "  public.sod_det " _
                '                        & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid)" _
                '                        & "  where so_code = " & SetSetring(fobject.ds_edit_so.Tables(0).Rows(x).Item("arso_so_code").ToString)
                '                    .InitializeCommand()
                '                    .FillDataSet(ds_sod_piutang, "list_sod")
                '                End With
                '            End Using
                '        Catch ex As Exception
                '            MessageBox.Show(ex.Message)
                '        End Try

                '        For y = 0 To ds_sod_piutang.Tables(0).Rows.Count - 1
                '            _total_dp_item = 0
                '            _total_payment_item = 0
                '            For z = 0 To fobject.ds_edit_shipment.Tables(0).Rows.Count - 1
                '                If ds_sod_piutang.Tables(0).Rows(y).Item("sod_pt_id").ToString = fobject.ds_edit_shipment.Tables(0).Rows(z).Item("pt_id") Then
                '                    If fobject.ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice") < 0 Then
                '                        _total_dp_item = _total_dp_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_dp") * -1.0)
                '                        _total_payment_item = _total_payment_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_payment") * -1.0) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)

                '                    Else
                '                        _total_dp_item = _total_dp_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_dp").ToString) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)
                '                        _total_payment_item = _total_payment_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_payment")) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)

                '                    End If
                '                End If
                '            Next
                '            _total_dp_all = _total_dp_all + _total_dp_item
                '            _total_payment_all = _total_payment_all + _total_payment_item
                '        Next

                '        Dim interval As Integer

                '        Try
                '            Using objcb As New master_new.WDABasepgsql("", "")
                '                With objcb
                '                    .Connection.Open()
                '                    .Command = .Connection.CreateCommand
                '                    .Command.CommandType = CommandType.Text
                '                    .Command.CommandText = "select code_usr_1 From code_mstr " + _
                '                                           " where code_field = 'payment_type' " + _
                '                                           " and code_id = " + ds_sod_piutang.Tables(0).Rows(0).Item("so_pay_type").ToString
                '                    .InitializeCommand()
                '                    .DataReader = .Command.ExecuteReader
                '                    While .DataReader.Read
                '                        _interval = .DataReader("code_usr_1").ToString
                '                    End While
                '                End With
                '            End Using
                '        Catch ex As Exception
                '            MessageBox.Show(ex.Message)
                '        End Try


                '        Dim _date As Date = ds_sod_piutang.Tables(0).Rows(0).Item("so_payment_date").ToString

                '        _dtrow = fobject.ds_edit_piutang.Tables(0).NewRow
                '        _dtrow("sokp_oid") = Guid.NewGuid.ToString

                '        _dtrow("sokp_so_oid") = ds_sod_piutang.Tables(0).Rows(0).Item("so_oid").ToString
                '        _dtrow("sokp_seq") = 0

                '        _dtrow("sokp_amount") = _total_dp_all
                '        _dtrow("sokp_due_date") = _date
                '        _dtrow("sokp_amount_pay") = 0
                '        _dtrow("sokp_description") = "-"

                '        fobject.ds_edit_piutang.Tables(0).Rows.Add(_dtrow)
                '        fobject.ds_edit_piutang.Tables(0).AcceptChanges()

                '        Dim _total_piutang As Double

                '        _total_piutang = _total_dp_all
                '        For i = 0 To _interval - 1
                '            _date = _date.AddMonths(1)

                '            _dtrow = fobject.ds_edit_piutang.Tables(0).NewRow
                '            _dtrow("sokp_oid") = Guid.NewGuid.ToString

                '            _dtrow("sokp_so_oid") = ds_sod_piutang.Tables(0).Rows(0).Item("so_oid").ToString
                '            _dtrow("sokp_seq") = i + 1

                '            _dtrow("sokp_amount") = _total_payment_all
                '            _total_piutang += _total_payment_all

                '            _dtrow("sokp_due_date") = _date
                '            _dtrow("sokp_amount_pay") = 0
                '            _dtrow("sokp_description") = "-"

                '            fobject.ds_edit_piutang.Tables(0).Rows.Add(_dtrow)
                '            fobject.ds_edit_piutang.Tables(0).AcceptChanges()

                '        Next
                '        fobject.gv_edit_piutang.BestFitColumns()
                '    Next

                'ElseIf _obj.name = "gv_edit_so" Then
                '    fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                '    fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                '    fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                '    fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                '    fobject.gv_edit_so.BestFitColumns()

                'ElseIf _obj.name = "par_so" Then
                '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            End If
            '---------- fdcrmemmo20240802 

            'ElseIf fobject.name = "FARPayment20240802" Then
            '    If _obj.name = "arpay_so_code" Then
            '        _obj.text = ds.Tables(0).Rows(_row_gv).Item("arso_so_code")
            '        fobject._arpay_bill_to_id = ds.Tables(0).Rows(_row_gv).Item("ar_bill_to")
            '        fobject.arpay_bill_to.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            '        fobject._arpay_bk_ids = ds.Tables(0).Rows(_row_gv).Item("ar_bk_id")
            '        fobject.arpay_bk_id.text = ds.Tables(0).Rows(_row_gv).Item("bk_name")
            '        fobject._arpay_ar_ac_ids = ds.Tables(0).Rows(_row_gv).Item("ar_ac_id")
            '        fobject.arpay_ar_ac_id.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

            '        Try
            '            Using objcb As New master_new.WDABasepgsql("", "")
            '                With objcb

            '                    .SQL = "SELECT  " _
            '                        & "  public.ar_mstr.ar_oid, " _
            '                        & "  public.ar_mstr.ar_en_id, " _
            '                        & "  public.ar_mstr.ar_code, " _
            '                        & "  public.ar_mstr.ar_date,ar_eff_date, " _
            '                        & "  public.ar_mstr.ar_bill_to, " _
            '                        & "  public.ar_mstr.ar_cu_id, " _
            '                        & "  public.ar_mstr.ar_type, " _
            '                        & "  public.ar_mstr.ar_amount, " _
            '                        & "  public.ar_mstr.ar_pay_amount, " _
            '                        & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
            '                        & "  public.ar_mstr.ar_status, " _
            '                        & "  public.ar_mstr.ar_due_date, " _
            '                        & "  public.en_mstr.en_desc, " _
            '                        & "  public.ptnr_mstr.ptnr_name, " _
            '                        & "  public.cu_mstr.cu_name, " _
            '                        & "  public.ar_mstr.ar_ac_id, " _
            '                        & "  public.ac_mstr.ac_code, " _
            '                        & "  public.ac_mstr.ac_name, " _
            '                        & "  public.ar_mstr.ar_sb_id, " _
            '                        & "  public.sb_mstr.sb_desc, " _
            '                        & "  public.ar_mstr.ar_cc_id, " _
            '                        & "  public.cc_mstr.cc_desc, ar_exc_rate, " _
            '                        & "  public.arso_so.arso_so_code " _
            '                        & "FROM " _
            '                        & "  public.ar_mstr " _
            '                        & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
            '                        & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
            '                        & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
            '                        & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
            '                        & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
            '                        & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
            '                        & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
            '                        & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
            '                        & " where coalesce(ar_status,'') = '' " _
            '                        & " and ar_en_id = " + _en_id.ToString _
            '                        & " and arso_so_code = '" + ds.Tables(0).Rows(_row_gv).Item("arso_so_code") + "'"

            '                    .InitializeCommand()
            '                    .FillDataSet(ds_bantu, "arpayd_det")
            '                End With
            '            End Using
            '        Catch ex As Exception
            '            MessageBox.Show(ex.Message)
            '        End Try

            '        fobject.ds_edit.tables(0).clear()

            '        'Dim _dtrow As DataRow

            '        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_ref", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_code"))
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_oid", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_oid").ToString)
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ac_id", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_ac_id"))
            '            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds_bantu.Tables(0).Rows(_row_gv).Item("ac_code"))
            '            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds_bantu.Tables(0).Rows(_row_gv).Item("ac_name"))
            '            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds_bantu.Tables(0).Rows(_row_gv).Item("ac_name"))
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_sb_id", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_sb_id"))
            '            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(_row_gv).Item("sb_desc"))
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cc_id", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_cc_id"))
            '            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(_row_gv).Item("cc_desc"))

            '            'If ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
            '            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
            '            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
            '            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
            '            'Else
            '            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
            '            'End If

            '            If ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") = _cu_id Then
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
            '                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            '            ElseIf ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id And _
            '               _cu_id <> master_new.ClsVar.ibase_cur_id Then
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
            '                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            '            ElseIf ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
            '                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            '            Else
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
            '                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            '            End If
            '            fobject.gv_edit.SetRowCellValue(_row, "ar_cu_id", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_cu_id"))
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", 0)
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_exp_amount", 0)
            '            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cur_amount", ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '            fobject.gv_edit.BestFitColumns()

            '        Next

            '        fobject.ds_edit.Tables(0).AcceptChanges()
            '        fobject.gv_edit.BestFitColumns()

            '    End If
            '---------- END OF FARPayment20240802 


        ElseIf fobject.name = "FARPaymentDetail30052023" Then
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()

            ElseIf _obj.name = "par_so" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")

            ElseIf _obj.name = "arpay_so_id" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("arso_so_code")
                fobject._arpay_bill_to_id = ds.Tables(0).Rows(_row_gv).Item("ar_bill_to")
                fobject.arpay_bill_to.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
                fobject.arpay_ar_code.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")

                fobject._arpay_bk_ids = ds.Tables(0).Rows(_row_gv).Item("ar_bk_id")
                fobject.arpay_bk_id.text = ds.Tables(0).Rows(_row_gv).Item("bk_name")

                'fobject.arpay_cashi_oid.Tag = ds.Tables(0).Rows(_row_gv).Item("cashi_oid")
                'fobject.arpay_cashi_oid.Text = ds.Tables(0).Rows(_row_gv).Item("cashi_code")
                'fobject.arpay_cashi_amount.EditValue = ds.Tables(0).Rows(_row_gv).Item("cashi_amount")



                fobject._arpay_ar_ac_ids = ds.Tables(0).Rows(_row_gv).Item("ar_ac_id")
                fobject.arpay_ar_ac_id.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                            & "  public.ar_mstr.ar_oid, " _
                            & "  public.ar_mstr.ar_en_id, " _
                            & "  public.ar_mstr.ar_code, " _
                            & "  public.ar_mstr.ar_date, " _
                            & "  public.ar_mstr.ar_bill_to, " _
                            & "  public.ar_mstr.ar_cu_id, " _
                            & "  public.ar_mstr.ar_type, " _
                            & "  public.ar_mstr.ar_amount, " _
                            & "  public.ar_mstr.ar_pay_amount, " _
                            & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                            & "  public.ar_mstr.ar_status, " _
                            & "  public.ar_mstr.ar_due_date, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.cu_mstr.cu_name, " _
                            & "  public.ar_mstr.ar_ac_id, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.ar_mstr.ar_sb_id, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.ar_mstr.ar_cc_id, " _
                            & "  public.cc_mstr.cc_desc, ar_exc_rate, " _
                            & "  sokp_oid, " _
                            & "  arso_ar_oid, " _
                            & "  arso_so_code, " _
                            & "  sokp_oid, " _
                            & "  sokp_so_oid, " _
                            & "  sokp_seq, " _
                            & "  sokp_amount, " _
                            & "  sokp_due_date, " _
                            & "  sokp_amount_pay, " _
                            & "  sokp_amount - sokp_amount_pay as sokp_amount_open, " _
                            & "  sokp_description " _
                            & "FROM " _
                            & "  public.ar_mstr " _
                            & "  inner join public.arso_so on ar_mstr.ar_oid = arso_ar_oid " _
                            & "  inner join public.sokp_piutang on arso_so_oid = sokp_so_oid" _
                            & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                            & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                            & " where coalesce(ar_status,'') = '' " _
                            & " and ar_en_id = " + _en_id.ToString _
                            & " and ar_code = '" + ds.Tables(0).Rows(_row_gv).Item("ar_code") + "'" _
                            & " and coalesce(sokp_status,'') = '' "
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "arpayd_det")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                fobject.ds_edit.tables(0).clear()

                Dim _dtrow As DataRow
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("arpayd_oid") = Guid.NewGuid.ToString
                    _dtrow("arpayd_ar_ref") = ds_bantu.Tables(0).Rows(i).Item("ar_code")
                    _dtrow("arpayd_ar_oid") = ds_bantu.Tables(0).Rows(i).Item("ar_oid").ToString
                    _dtrow("sokp_oid") = ds_bantu.Tables(0).Rows(i).Item("sokp_oid").ToString
                    _dtrow("arpayd_ac_id") = ds_bantu.Tables(0).Rows(i).Item("ar_ac_id")
                    _dtrow("arso_so_code") = ds_bantu.Tables(0).Rows(i).Item("arso_so_code")
                    _dtrow("sokp_seq") = ds_bantu.Tables(0).Rows(i).Item("sokp_seq")
                    _dtrow("ac_code") = ds_bantu.Tables(0).Rows(i).Item("ac_code")
                    _dtrow("ac_name") = ds_bantu.Tables(0).Rows(i).Item("ac_name")
                    _dtrow("ac_name") = ds_bantu.Tables(0).Rows(i).Item("ac_name")
                    _dtrow("arpayd_sb_id") = ds_bantu.Tables(0).Rows(i).Item("ar_sb_id")
                    _dtrow("sb_desc") = ds_bantu.Tables(0).Rows(i).Item("sb_desc")
                    _dtrow("arpayd_cc_id") = ds_bantu.Tables(0).Rows(i).Item("ar_cc_id")
                    _dtrow("cc_desc") = ds_bantu.Tables(0).Rows(i).Item("cc_desc")

                    If ds_bantu.Tables(0).Rows(i).Item("ar_cu_id") = _cu_id Then
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")
                        _dtrow("arpayd_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                        'fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                        '_dtrow("arpayd_exc_rate", _exr_cu_rate_1)
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id And _
                       _cu_id <> master_new.ClsVar.ibase_cur_id Then
                        '_dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding")
                        '_dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding")
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")
                        _dtrow("arpayd_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                        'fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                        '_dtrow("arpayd_exc_rate", _exr_cu_rate_1)
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                        '_dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding") * _exr_cu_rate_1)
                        '_dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding") * _exr_cu_rate_1)
                        fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open") * _exr_cu_rate_1)
                        fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open") * _exr_cu_rate_1)
                        '_dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open") * _exr_cu_rate_1)
                        '_dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open") * _exr_cu_rate_1)
                        fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                        '_dtrow("arpayd_exc_rate", _exr_cu_rate_1)
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                    Else
                        '_dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding")
                        '_dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding")
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")
                        fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
                        '_dtrow("arpayd_exc_rate", 1)
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                    End If

                    _dtrow("ar_cu_id") = ds_bantu.Tables(0).Rows(i).Item("ar_cu_id")
                    '_dtrow("arpayd_disc_amount") = fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", 0)
                    '_dtrowns("taxclass_name") = fobject.ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                    'fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", ds_bantu.Tables(0).Rows(i).Item("arpayd_disc_amount") = _exr_cu_rate_1)
                    fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", 0)
                    fobject.gv_edit.SetRowCellValue(_row, "arpayd_exp_amount", 0)
                    '_dtrow("arpayd_disc_amount", 0)
                    '_dtrow("arpayd_exp_amount", 0)
                    _dtrow("arpayd_cur_amount") = ds_bantu.Tables(0).Rows(i).Item("sokp_amount_open")

                    '_dtrow("arpayd_cur_amount") = ds_bantu.Tables(0).Rows(i).Item("ar_outstanding")
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)

                Next
                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gv_edit.BestFitColumns()

            End If

        ElseIf fobject.name = "FARPayment20240802" Then
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()

            ElseIf _obj.name = "par_so" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")

            ElseIf _obj.name = "arpay_so_code" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("arso_so_code")
                fobject._arpay_bill_to_id = ds.Tables(0).Rows(_row_gv).Item("ar_bill_to")
                fobject.arpay_bill_to.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
                'fobject.arpay_ar_code.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")

                fobject._arpay_bk_ids = ds.Tables(0).Rows(_row_gv).Item("ar_bk_id")
                fobject.arpay_bk_id.text = ds.Tables(0).Rows(_row_gv).Item("bk_name")

                'fobject.arpay_cashi_oid.Tag = ds.Tables(0).Rows(_row_gv).Item("cashi_oid")
                'fobject.arpay_cashi_oid.Text = ds.Tables(0).Rows(_row_gv).Item("cashi_code")
                'fobject.arpay_cashi_amount.EditValue = ds.Tables(0).Rows(_row_gv).Item("cashi_amount")



                fobject._arpay_ar_ac_ids = ds.Tables(0).Rows(_row_gv).Item("ar_ac_id")
                fobject.arpay_ar_ac_id.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                            & "  public.ar_mstr.ar_oid, " _
                            & "  public.ar_mstr.ar_en_id, " _
                            & "  public.ar_mstr.ar_code, " _
                            & "  public.ar_mstr.ar_date, " _
                            & "  public.ar_mstr.ar_bill_to, " _
                            & "  public.ar_mstr.ar_cu_id, " _
                            & "  public.ar_mstr.ar_type, " _
                            & "  public.ar_mstr.ar_amount, " _
                            & "  public.ar_mstr.ar_pay_amount, " _
                            & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                            & "  public.ar_mstr.ar_status, " _
                            & "  public.ar_mstr.ar_due_date, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.cu_mstr.cu_name, " _
                            & "  public.ar_mstr.ar_ac_id, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.ar_mstr.ar_sb_id, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.ar_mstr.ar_cc_id, " _
                            & "  public.cc_mstr.cc_desc, ar_exc_rate, " _
                            & "  arso_ar_oid, " _
                            & "  arso_so_code " _
                            & "FROM " _
                            & "  public.ar_mstr " _
                            & "  inner join public.arso_so on ar_mstr.ar_oid = arso_ar_oid " _
                            & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                            & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                            & " where coalesce(ar_status,'') = '' " _
                            & " and ar_en_id = " + _en_id.ToString _
                            & " and ar_code = '" + ds.Tables(0).Rows(_row_gv).Item("ar_code") + "'"

                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "arpayd_det")

                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                fobject.ds_edit.tables(0).clear()

                Dim _dtrow As DataRow
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("arpayd_oid") = Guid.NewGuid.ToString
                    _dtrow("arpayd_ar_ref") = ds_bantu.Tables(0).Rows(i).Item("ar_code")
                    _dtrow("arpayd_ar_oid") = ds_bantu.Tables(0).Rows(i).Item("ar_oid").ToString
                    _dtrow("arpayd_ac_id") = ds_bantu.Tables(0).Rows(i).Item("ar_ac_id")
                    '_dtrow("arso_so_code") = ds_bantu.Tables(0).Rows(i).Item("arso_so_code")
                    _dtrow("ac_code") = ds_bantu.Tables(0).Rows(i).Item("ac_code")
                    _dtrow("ac_name") = ds_bantu.Tables(0).Rows(i).Item("ac_name")
                    _dtrow("ac_name") = ds_bantu.Tables(0).Rows(i).Item("ac_name")
                    _dtrow("arpayd_sb_id") = ds_bantu.Tables(0).Rows(i).Item("ar_sb_id")
                    _dtrow("sb_desc") = ds_bantu.Tables(0).Rows(i).Item("sb_desc")
                    _dtrow("arpayd_cc_id") = ds_bantu.Tables(0).Rows(i).Item("ar_cc_id")
                    _dtrow("cc_desc") = ds_bantu.Tables(0).Rows(i).Item("cc_desc")

                    If ds_bantu.Tables(0).Rows(i).Item("ar_cu_id") = _cu_id Then
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")
                        _dtrow("arpayd_exc_rate") = _exr_cu_rate_1
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate")
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id And _
                       _cu_id <> master_new.ClsVar.ibase_cur_id Then
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")
                        _dtrow("arpayd_exc_rate") = _exr_cu_rate_1
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate")
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1
                        _dtrow("arpayd_exc_rate") = _exr_cu_rate_1
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate")
                    Else
                        _dtrow("arpayd_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")
                        _dtrow("arpayd_cash_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")
                        _dtrow("arpayd_exc_rate") = 1
                        _dtrow("ar_exc_rate") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_exc_rate")
                    End If
                    _dtrow("ar_cu_id") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_cu_id")
                    _dtrow("arpayd_disc_amount") = 0
                    _dtrow("arpayd_exp_amount") = 0
                    _dtrow("arpayd_cur_amount") = ds_bantu.Tables(0).Rows(_row_gv).Item("ar_outstanding")

                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                Next

                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gv_edit.BestFitColumns()

            End If

            '---- END OF FARPAYMENT 2804


        ElseIf fobject.name = "FDPackingSheetPrintOut" Or fobject.name = "FDPackingSheetPrintOutSO" Or fobject.name = "FDPackingSheetPrintOutbySO" Or fobject.name = "FDPcsPrintNew" Then
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()

            ElseIf _obj.name = "par_so" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            End If

        ElseIf fobject.name = "FDPickingListSO" Then
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "picklsso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "picklsso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "picklsso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "picklsso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()

            ElseIf _obj.name = "par_so" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            End If

        ElseIf fobject.name = "FDPackingSheetNew" Then
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "dopd_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "dopd_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "dopd_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                'fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
                fobject.gv_edit_so.BestFitColumns()
            End If

        ElseIf fobject.name = "FTransferIssues" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString
            fobject.ptsfr_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_shipment,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  pt_cost, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_shipment,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("ptsfrd_oid") = Guid.NewGuid.ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty")
                _dtrow("ptsfrd_qty_receive") = 0
                _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("sod_cost")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            'fobject.ptsfr_so_oid.enabled = False
            'fobject.ptsfr_pb_oid.enabled = False
            fobject.ptsfr_pb_oid.text = ""
            fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False

        ElseIf fobject.name = "FTransferIssuesReturn" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString
            fobject.ptsfr_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_shipment,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  pt_cost, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("ptsfrd_oid") = Guid.NewGuid.ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty")
                _dtrow("ptsfrd_qty_receive") = 0
                _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("sod_cost")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            'fobject.ptsfr_so_oid.enabled = False
            'fobject.ptsfr_pb_oid.enabled = False
            fobject.ptsfr_pb_oid.text = ""
            fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        ElseIf fobject.name = "FSalesOrderPrint" Or fobject.name = FSalesOrderCashPrint.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        ElseIf fobject.name = "FSalesOrderFakturPenjualanPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        ElseIf fobject.name = "FInventoryReceipts" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            fobject.riu_ref_so_code.enabled = False
            fobject._riu_ref_so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString

        ElseIf fobject.name = FInventoryReceiptsLintas.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            fobject.riu_ref_so_code.enabled = False
            fobject._riu_ref_so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString

        ElseIf fobject.name = FCashIn.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString

        ElseIf fobject.name = FWorkOrder.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString


        ElseIf fobject.name = FSalesOrder.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString

        ElseIf fobject.name = FTaxInvoice.Name Then
            fobject.gv_edit_soship.SetRowCellValue(_row, "tis_oid", Guid.NewGuid.ToString)
            fobject.gv_edit_soship.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            fobject.gv_edit_soship.SetRowCellValue(_row, "tis_soship_oid", ds.Tables(0).Rows(_row_gv).Item("soship_oid").ToString)
            fobject.gv_edit_soship.SetRowCellValue(_row, "so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
            fobject.gv_edit_soship.SetRowCellValue(_row, "so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
            fobject.gv_edit_soship.SetRowCellValue(_row, "soship_code", ds.Tables(0).Rows(_row_gv).Item("soship_code"))
            fobject.gv_edit_soship.SetRowCellValue(_row, "soship_date", ds.Tables(0).Rows(_row_gv).Item("soship_date"))
            fobject.gv_edit_soship.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold"))
            fobject.gv_edit_soship.BestFitColumns()
        End If
    End Sub

    Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("status") = True Then
                    If fobject.name = FTaxInvoice.Name Then
                        If jml = 0 Then
                            fobject.gv_edit_soship.SetRowCellValue(_row, "tis_oid", Guid.NewGuid.ToString)
                            fobject.gv_edit_soship.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit_soship.SetRowCellValue(_row, "tis_soship_oid", ds.Tables(0).Rows(i).Item("soship_oid").ToString)
                            fobject.gv_edit_soship.SetRowCellValue(_row, "so_code", ds.Tables(0).Rows(i).Item("so_code"))
                            fobject.gv_edit_soship.SetRowCellValue(_row, "so_date", ds.Tables(0).Rows(i).Item("so_date"))
                            fobject.gv_edit_soship.SetRowCellValue(_row, "soship_code", ds.Tables(0).Rows(i).Item("soship_code"))
                            fobject.gv_edit_soship.SetRowCellValue(_row, "soship_date", ds.Tables(0).Rows(i).Item("soship_date"))
                            fobject.gv_edit_soship.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name_sold"))

                            jml = jml + 1
                        Else
                            fobject.gv_edit_soship.AddNewRow()
                            _row_pos = fobject.gv_edit_soship.FocusedRowHandle()

                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "tis_oid", Guid.NewGuid.ToString)
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "tis_soship_oid", ds.Tables(0).Rows(i).Item("soship_oid").ToString)
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "so_code", ds.Tables(0).Rows(i).Item("so_code"))
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "so_date", ds.Tables(0).Rows(i).Item("so_date"))
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "soship_code", ds.Tables(0).Rows(i).Item("soship_code"))
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "soship_date", ds.Tables(0).Rows(i).Item("soship_date"))
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name_sold"))
                        End If

                        fobject.gv_edit_soship.BestFitColumns()
                    End If
                End If
            Next
        Catch
        End Try

        Me.Close()
    End Sub
End Class
