Imports master_new.ModFunction

Public Class FSalesOrderSearchCut
    Public _row As Integer
    Public _en_id As Integer
    Public _cut_first As Date
    Public _cut_last As Date
    Public _ptnr_id As Integer
    Public _indent As String
    Public _cu_id As Integer
    Public _obj As Object
    Public _so_code, _ppn_type As String
    Public _interval As Integer
    Public _loc_id As Integer
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now, _then As DateTime
    Dim _conf_value As String
    Public _date As Date

    Private Sub FSalesOrderSearchCut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_sales_order")
        form_first_load()
        _now = func_coll.get_now_curr
        _then = func_coll.get_then
        pr_txttglawal.DateTime = _then
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
        add_column(gv_master, "SQ Code", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Order Code", "sq_ajb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Date", "so_add_date", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "G")
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
            'ElseIf fobject.name = FSalesOrderPrint.Name Then
            '    add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
            '    add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            '    add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
            '    add_column(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
            '    add_column(gv_master, "Customer", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)

            'add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
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
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id_shipment = public.en_mstr.en_id)" _
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
                    & "  where so_trans_id <> 'X' and   so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString _
                    & "  and so_cons ~~* 'Y' "
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


        ElseIf fobject.name = "FSalesOrderPrint" Or fobject.name = "FSalesOrderPrintEnShipment" Or fobject.name = "FSalesOrderPrintSE" Or fobject.name = "FInventoryReceipts" Or fobject.name = "FSalesOrderFakturPenjualanPrintES" Then

            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_en_id_shipment, " _
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
                    & "  sq_ajb_code, " _
                    & "  sq_code, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  left outer join sq_mstr ON sq_oid = so_sq_ref_oid " _
                    & "  where so_trans_id <> 'X' and   " _
                    & "  so_add_date >= " + SetDateNTime991(_cut_first.ToString) + " " _
                    & "  and so_add_date <= " + SetDateNTime991(_cut_last.ToString) + " " _
                    & "  and so_en_id_shipment = " + _en_id.ToString + " "

            If _indent.ToString = True Then
                'Dim _sql As String
                get_sequel = get_sequel + "and so_pay_type = '991246' "
                get_sequel = get_sequel + "order by so_mstr.so_code ASC "
            Else
                'Dim _sql As String
                get_sequel = get_sequel + "and so_pay_type <> '991246' "
                get_sequel = get_sequel + "order by so_mstr.so_code ASC "
            End If
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
                    & "  and so_en_id = " + _en_id.ToString

            If _interval = 0 Then
                get_sequel = get_sequel + "and coalesce(so_interval,-1) = 0"
            Else
                get_sequel = get_sequel + "and coalesce(so_interval,-1) > 0"
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
                   & "  where so_trans_id <> 'X' and  so_add_date >= " + SetDate(pr_txttglawal.DateTime) _
                   & "  and so_add_date <= " + SetDate(pr_txttglakhir.DateTime) _
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

        Dim ds_bantu As New DataSet
        Dim i As Integer
        Dim qty_alloc, qty_alloc_awal As Integer
        Dim _exc_rate As Double = 0

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
        ElseIf fobject.name = "FSalesOrderPrintSE" Or fobject.name = FSalesOrderCashPrint.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        ElseIf fobject.name = "FSalesOrderPrintEnShipment" Or fobject.name = FSalesOrderCashPrint.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        ElseIf fobject.name = "FSalesOrderFakturPenjualanPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        ElseIf fobject.name = "FSalesOrderFakturPenjualanPrintES" Then
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
            fobject.gv_edit_soship.SetRowCellValue(_row, "tis_soship_oid", ds.Tables(0).Rows(_row_gv).Item("soship_oid"))
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
                            fobject.gv_edit_soship.SetRowCellValue(_row, "tis_soship_oid", ds.Tables(0).Rows(i).Item("soship_oid"))
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
                            fobject.gv_edit_soship.SetRowCellValue(_row_pos, "tis_soship_oid", ds.Tables(0).Rows(i).Item("soship_oid"))
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
