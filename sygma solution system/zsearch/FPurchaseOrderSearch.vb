Imports master_new.ModFunction

Public Class FPurchaseOrderSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _date As Date
    Public _obj As Object
    Public _so_ptnr_id_sold_mstr
    Public _sq_ptnr_id_sold_mstr
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FPurchaseOrderSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PO Date", "po_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FPurchaseReceipt" Then
            get_sequel = "SELECT  " _
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
                & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and po_en_id = " + _en_id.ToString _
                & "  and coalesce(po_trans_id,'I') = 'I' " _
                & "  and coalesce(po_close_date,'01/01/1999') = '01/01/1999' order by po_code"
        ElseIf fobject.name = "FPurchaseReceiptCrossSelling" Then
            get_sequel = "SELECT  " _
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
                & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and po_is_internal = 'Y' " _
                & "  and coalesce(po_trans_id,'D') = 'D' " _
                & "  and coalesce(po_close_date,'01/01/1999') = '01/01/1999' order by po_code"
        ElseIf fobject.name = "FPurchaseReturn" Then
            get_sequel = "SELECT  " _
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
                & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and po_en_id = " + _en_id.ToString _
                & "  order by po_code "
        ElseIf fobject.name = "FVoucher" Then
            If _obj Is Nothing Then
                get_sequel = "SELECT distinct " _
                      & "  public.po_mstr.po_oid, " _
                      & "  public.po_mstr.po_en_id, " _
                      & "  public.po_mstr.po_code, " _
                      & "  public.po_mstr.po_date, " _
                      & "  public.po_mstr.po_ptnr_id, " _
                      & "  public.po_mstr.po_cmaddr_id, " _
                      & "  public.po_mstr.po_date, " _
                      & "  public.po_mstr.po_si_id, " _
                      & "  public.po_mstr.po_close_date, " _
                      & "  public.ptnr_mstr.ptnr_name, " _
                      & "  public.si_mstr.si_desc, " _
                      & "  public.cmaddr_mstr.cmaddr_name, en_desc " _
                      & "FROM " _
                      & "  public.po_mstr " _
                      & "  INNER JOIN public.pod_det ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                      & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                      & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                      & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id)" _
                      & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id)" _
                      & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                      & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                      & "  and po_ptnr_id = " + _ptnr_id.ToString _
                      & "  and po_en_id = " + _en_id.ToString _
                      & "  and po_cu_id = " + _cu_id.ToString _
                      & "  and po_status_cash='R' " _
                      & "  group by " _
                      & "  public.po_mstr.po_oid, " _
                      & "  public.po_mstr.po_en_id, " _
                      & "  public.po_mstr.po_code, " _
                      & "  public.po_mstr.po_date, " _
                      & "  public.po_mstr.po_ptnr_id, " _
                      & "  public.po_mstr.po_cmaddr_id, " _
                      & "  public.po_mstr.po_date, " _
                      & "  public.po_mstr.po_si_id, " _
                      & "  public.po_mstr.po_close_date, " _
                      & "  public.ptnr_mstr.ptnr_name, " _
                      & "  public.si_mstr.si_desc, " _
                      & "  public.cmaddr_mstr.cmaddr_name, en_desc " _
                      & "  order by po_code "

                '& "  and coalesce(pod_qty_invoice,0) = 0 " _
                'by sys 20110419 ditambahkan ini agar po yang sudah di voucher tidak bisa tampil lagi datanya
                'get_sequel = "SELECT  distinct " _
                '& "  INNER JOIN public.pod_det ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                '& "  group by  " _ sampai dengan kebawah 
                '------------------------------------

            Else
                If _obj.name = "par_po" Then
                    get_sequel = "SELECT  " _
                           & "  public.po_mstr.po_oid, " _
                           & "  public.po_mstr.po_en_id, " _
                           & "  public.po_mstr.po_code, " _
                           & "  public.po_mstr.po_date, " _
                           & "  public.po_mstr.po_ptnr_id, " _
                           & "  public.po_mstr.po_cmaddr_id, " _
                           & "  public.po_mstr.po_date, " _
                           & "  public.po_mstr.po_si_id, " _
                           & "  public.po_mstr.po_close_date, " _
                           & "  public.ptnr_mstr.ptnr_name, " _
                           & "  public.si_mstr.si_desc, " _
                           & "  public.cmaddr_mstr.cmaddr_name, en_desc " _
                           & "FROM " _
                           & "  public.po_mstr " _
                           & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                           & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                           & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id)" _
                           & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id)" _
                           & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                           & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                           & "  and po_ptnr_id = " + _ptnr_id.ToString _
                           & "  order by po_code "
                Else
                    get_sequel = "SELECT  " _
                           & "  public.po_mstr.po_oid, " _
                           & "  public.po_mstr.po_en_id, " _
                           & "  public.po_mstr.po_code, " _
                           & "  public.po_mstr.po_date, " _
                           & "  public.po_mstr.po_ptnr_id, " _
                           & "  public.po_mstr.po_cmaddr_id, " _
                           & "  public.po_mstr.po_date, " _
                           & "  public.po_mstr.po_si_id, " _
                           & "  public.po_mstr.po_close_date, " _
                           & "  public.ptnr_mstr.ptnr_name, " _
                           & "  public.si_mstr.si_desc, " _
                           & "  public.cmaddr_mstr.cmaddr_name, en_desc " _
                           & "FROM " _
                           & "  public.po_mstr " _
                           & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                           & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                           & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id)" _
                           & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id)" _
                           & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                           & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                           & "  and po_ptnr_id = " + _ptnr_id.ToString _
                           & "  and po_en_id = " + _en_id.ToString _
                           & "  and po_cu_id = " + _cu_id.ToString _
                           & "  order by po_code "
                End If
            End If

        ElseIf fobject.name = "FSalesOrder" Then

            get_sequel = "SELECT  " _
                & "  public.po_mstr.po_oid, " _
                & "  public.po_mstr.po_en_id, " _
                & "  public.po_mstr.po_code, " _
                & "  public.po_mstr.po_date, " _
                & "  public.po_mstr.po_ptnr_id, " _
                & "  public.po_mstr.po_cmaddr_id, " _
                & "  public.po_mstr.po_date, " _
                & "  public.po_mstr.po_si_id, " _
                & "  public.po_mstr.po_close_date, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.si_mstr.si_desc, " _
                & "  public.cmaddr_mstr.cmaddr_name, en_desc " _
                & "FROM " _
                & "  public.po_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id)" _
                & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id)" _
                & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and po_ptnr_id = " + _so_ptnr_id_sold_mstr.ToString _
                & "  order by po_code "

            'Ini minta dikomentarin dulu katanya 20100508
            '& "  and po_en_id in (select user_en_id from tconfuserentity " _
            '                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
        Else
            get_sequel = "SELECT  " _
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
                & "  where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and po_en_id = " + _en_id.ToString _
                & " order by po_code "
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
        Dim _exc_rate As Double
        _row_gv = BindingContext(ds.Tables(0)).Position

        Dim ds_bantu As New DataSet
        Dim i As Integer

        If fobject.name = "FPurchaseReceipt" Or fobject.name = "FPurchaseReceiptCrossSelling" Then
            fobject._po_oid = ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString
            fobject.rcv_po_oid.text = ds.Tables(0).Rows(_row_gv).Item("po_code")
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
                            & "  and pod_po_oid = '" + ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            If ds_bantu.Tables(0).Rows.Count > 0 Then
                fobject.rcv_cu_id.editvalue = ds_bantu.Tables(0).Rows(0).Item("po_cu_id")
                If ds_bantu.Tables(0).Rows(0).Item("po_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                    _exc_rate = func_data.get_exchange_rate(ds_bantu.Tables(0).Rows(0).Item("po_cu_id"), _date)
                    If _exc_rate = 1 Then
                        fobject.rcv_exc_rate.EditValue = 0
                    Else
                        fobject.rcv_exc_rate.EditValue = _exc_rate
                    End If

                    'fobject.rcv_exc_rate.Enabled = True
                Else
                    fobject.rcv_exc_rate.EditValue = 1
                    'fobject.rcv_exc_rate.Enabled = False
                End If
            End If


            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("rcvd_oid") = Guid.NewGuid.ToString
                _dtrow("rcvd_pod_oid") = ds_bantu.Tables(0).Rows(i).Item("pod_oid").ToString
                _dtrow("pod_pt_id") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_id")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pod_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_desc1")
                _dtrow("pod_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("rcvd_si_id") = ds_bantu.Tables(0).Rows(i).Item("pod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("rcvd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("pod_qty_open")
                _dtrow("rcvd_qty") = ds_bantu.Tables(0).Rows(i).Item("pod_qty_open")
                _dtrow("rcvd_um") = ds_bantu.Tables(0).Rows(i).Item("pod_um")
                _dtrow("rcvd_um_name") = ds_bantu.Tables(0).Rows(i).Item("pod_um_name")
                _dtrow("rcvd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("pod_um_conv")
                _dtrow("rcvd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("pod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("pod_um_conv"))
                _dtrow("rcvd_packing_qty") = 0
                _dtrow("pod_memo") = ds_bantu.Tables(0).Rows(i).Item("pod_memo")
                _dtrow("po_date") = ds_bantu.Tables(0).Rows(i).Item("po_date")
                _dtrow("pod_cost") = ds_bantu.Tables(0).Rows(i).Item("pod_cost")
                _dtrow("pod_disc") = ds_bantu.Tables(0).Rows(i).Item("pod_disc")
                _dtrow("pod_cc_id") = ds_bantu.Tables(0).Rows(i).Item("pod_cc_id")

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()

            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FPurchaseReturn" Then
            fobject._po_oid = ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString
            fobject.rcv_po_oid.text = ds.Tables(0).Rows(_row_gv).Item("po_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pod_oid, " _
                            & "  pod_en_id,pod_cc_id, " _
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
                            & "  coalesce(pod_memo,'') as pod_memo, " _
                            & "  pod_qty, " _
                            & "  pod_qty_receive, " _
                            & "  coalesce(pod_qty_receive ,0) - coalesce(pod_qty_invoice,0) as pod_qty_open, " _
                            & "  pod_um, " _
                            & "  po_cu_id, " _
                            & "  code_name as pod_um_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc, " _
                            & "  pod_um_conv, po_date, pod_cost, pod_disc, pod_cc_id, pod_po_oid, " _
                            & "  pod_qty_real " _
                            & "FROM  " _
                            & "  public.pod_det  " _
                            & "  inner join public.po_mstr on public.pod_det.pod_po_oid = public.po_mstr.po_oid " _
                            & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                            & "  inner join public.loc_mstr on public.pt_mstr.pt_loc_id = public.loc_mstr.loc_id " _
                            & "  inner join public.en_mstr on public.pod_det.pod_en_id = public.en_mstr.en_id " _
                            & "  inner join public.si_mstr on public.pod_det.pod_si_id = public.si_mstr.si_id " _
                            & "  inner join public.code_mstr on public.pod_det.pod_um = public.code_mstr.code_id " _
                            & "  where coalesce(pod_qty_receive,0) > coalesce(pod_qty_invoice,0) " _
                            & "  and pod_po_oid = '" + ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            If ds_bantu.Tables(0).Rows.Count > 0 Then
                fobject.rcv_cu_id.editvalue = ds_bantu.Tables(0).Rows(0).Item("po_cu_id")
                If ds_bantu.Tables(0).Rows(0).Item("po_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                    _exc_rate = func_data.get_exchange_rate(ds_bantu.Tables(0).Rows(0).Item("po_cu_id"), _date)
                    If _exc_rate = 1 Then
                        fobject.rcv_exc_rate.EditValue = 0
                    Else
                        fobject.rcv_exc_rate.EditValue = _exc_rate
                    End If

                    'fobject.rcv_exc_rate.Enabled = True
                Else
                    fobject.rcv_exc_rate.EditValue = 1
                    'fobject.rcv_exc_rate.Enabled = False
                End If
            End If

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("rcvd_oid") = Guid.NewGuid.ToString
                _dtrow("rcvd_pod_oid") = ds_bantu.Tables(0).Rows(i).Item("pod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pod_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_desc1")
                _dtrow("pod_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("rcvd_si_id") = ds_bantu.Tables(0).Rows(i).Item("pod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("rcvd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("pod_qty_open")
                _dtrow("rcvd_qty") = 0
                _dtrow("rcvd_um") = ds_bantu.Tables(0).Rows(i).Item("pod_um")
                _dtrow("rcvd_um_name") = ds_bantu.Tables(0).Rows(i).Item("pod_um_name")
                _dtrow("rcvd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("pod_um_conv")
                '_dtrow("rcvd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("pod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("pod_um_conv"))
                _dtrow("rcvd_qty_real") = 0
                _dtrow("rcvd_packing_qty") = 0
                _dtrow("pod_memo") = ds_bantu.Tables(0).Rows(i).Item("pod_memo")
                _dtrow("po_date") = ds_bantu.Tables(0).Rows(i).Item("po_date")
                _dtrow("pod_cost") = ds_bantu.Tables(0).Rows(i).Item("pod_cost")
                _dtrow("pod_disc") = ds_bantu.Tables(0).Rows(i).Item("pod_disc")
                _dtrow("pod_cc_id") = ds_bantu.Tables(0).Rows(i).Item("pod_cc_id")
                _dtrow("pod_po_oid") = ds_bantu.Tables(0).Rows(i).Item("pod_po_oid").ToString

                'by sys 20110416
                '_dtrow("rcvd_close_line") = "N"
                '------------------------

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FVoucher" Then
            If _obj Is Nothing Then
                fobject.gv_edit_po.SetRowCellValue(_row, "app_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_po.SetRowCellValue(_row, "app_po_oid", ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString)
                fobject.gv_edit_po.SetRowCellValue(_row, "po_code", ds.Tables(0).Rows(_row_gv).Item("po_code"))
                fobject.gv_edit_po.BestFitColumns()
            Else
                If _obj.name = "par_po" Then
                    _obj.text = ds.Tables(0).Rows(_row_gv).Item("po_code")
                    fobject._par_po_oid = ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString
                Else
                    fobject.gv_edit_po.SetRowCellValue(_row, "app_oid", Guid.NewGuid.ToString)
                    fobject.gv_edit_po.SetRowCellValue(_row, "app_po_oid", ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString)
                    fobject.gv_edit_po.SetRowCellValue(_row, "po_code", ds.Tables(0).Rows(_row_gv).Item("po_code"))
                    fobject.gv_edit_po.BestFitColumns()
                End If

            End If

        ElseIf fobject.name = "FPurchaseOrderPrint" Or fobject.name = "FPurchaseOrderPrintFilm" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("po_code")

        ElseIf fobject.name = "FSalesOrder" Then
            If _obj.name = "so_ref_po_code" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("po_code")
                'fobject.so_ref_po_code.enabled = False
                fobject._so_ref_po_oid = ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString
            Else
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("po_code")
                'fobject.so_ref_po_code2.enabled = False
                _obj.tag = ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString
            End If
        ElseIf fobject.name = FPurchaseOrder.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("po_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("po_oid").ToString
        End If
    End Sub
End Class
