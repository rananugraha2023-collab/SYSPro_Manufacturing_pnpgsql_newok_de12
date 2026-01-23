Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FPesananSales
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pb_oid_mstr As String
    Dim mf As New master_new.ModFunction
    Public ds_edit As DataSet
    Dim status_insert As Boolean = True
    Public _pbd_related_oid As String = ""
    Dim _conf_value As String
    Dim _now As Date

    Private Sub FInventorypbuest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_inventory_request")
        form_first_load()
        _now = func_coll.get_tanggal_sistem
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        'If _conf_value = "0" Then
        '    xtc_detail.TabPages(1).PageVisible = False
        '    xtc_detail.TabPages(3).PageVisible = False
        'ElseIf _conf_value = "1" Then
        '    xtc_detail.TabPages(1).PageVisible = True
        '    xtc_detail.TabPages(3).PageVisible = True
        'End If
        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pb_en_id.Properties.DataSource = dt_bantu
        pb_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pb_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pb_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pb_type())
        pb_pbt_code.Properties.DataSource = dt_bantu
        pb_pbt_code.Properties.DisplayMember = dt_bantu.Columns("pbt_desc").ToString
        pb_pbt_code.Properties.ValueMember = dt_bantu.Columns("pbt_code").ToString
        pb_pbt_code.ItemIndex = 0


        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            pb_tran_id.Properties.DataSource = dt_bantu
            pb_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            pb_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            pb_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            pb_tran_id.Properties.DataSource = dt_bantu
            pb_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            pb_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            pb_tran_id.ItemIndex = 0
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pesan_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "pesan_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Sales Order Number", "pesan_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Old Customer", "pesan_status_customer_lama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Code", "pesan_kode_costumer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Name", "pesan_customer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address", "pesan_costumer_alamat", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone", "pesan_costumer_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "pesan_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pesan_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    

        add_column_copy(gv_detail, "Part Number", "pesan_kode_barang", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pesan_nama_barang", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "pesan_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Remarks", "pesan_ket", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_edit, "pbd_en_id", False)
        'add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "pbd_si_id", False)
        'add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "pbd_pt_id", False)
        'add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit, "pbd_qty_processed", False)
        'add_column(gv_edit, "pbd_um", False)
        'add_column(gv_edit, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)

        'add_column(gv_wf, "wf_ref_code", False)
        'add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        'add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        'add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_email, "pb_oid", False)
        'add_column(gv_email, "pbd_pb_oid", False)
        'add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_email, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_email, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_email, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_email, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_email, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_smart, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)

    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.pesan_oid,en_desc, " _
            & "  a.pesan_code, " _
            & "  a.pesan_date, " _
            & "  a.pesan_sales_id,  b.ptnr_code,  b.ptnr_name,  " _
            & "  a.pesan_en_id, " _
            & "  a.pesan_add_date, " _
            & "  a.pesan_status_customer_lama, " _
            & "  a.pesan_kode_costumer, " _
            & "  a.pesan_customer, " _
            & "  a.pesan_costumer_alamat, " _
            & "  a.pesan_costumer_hp, " _
            & "  a.pesan_kode_barang1, " _
            & "  a.pesan_nama_barang1, " _
            & "  a.pesan_qty1, " _
            & "  a.pesan_ket1, " _
            & "  a.pesan_kode_barang2, " _
            & "  a.pesan_nama_barang2, " _
            & "  a.pesan_qty2, " _
            & "  a.pesan_ket2, " _
            & "  a.pesan_kode_barang3, " _
            & "  a.pesan_nama_barang3, " _
            & "  a.pesan_qty3, " _
            & "  a.pesan_ket3, " _
            & "  a.pesan_kode_barang4, " _
            & "  a.pesan_nama_barang4, " _
            & "  a.pesan_qty4, " _
            & "  a.pesan_ket4, coalesce(pesan_status,'') as pesan_status,pesan_so_code " _
            & "FROM " _
            & "  public.pesanan a " _
            & "   INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "   INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & "  AND  " _
            & "  a.pesan_en_id IN (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.pesan_code"


        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        load_detail()
      
    End Sub

    Public Overrides Sub relation_detail()
    
    End Sub
    Public Sub load_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.pesan_oid, " _
            & "  a.pesan_kode_barang1 as pesan_kode_barang, " _
            & "  a.pesan_nama_barang1 as pesan_nama_barang, " _
            & "  a.pesan_qty1 as pesan_qty, " _
            & "  a.pesan_ket1 as pesan_ket " _
            & "FROM " _
            & "  public.pesanan a " _
            & "  INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "  INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pesan_oid").ToString & "' and  length(pesan_kode_barang1) > 0 " _
            & "union all " _
            & "SELECT  " _
            & "  a.pesan_oid, " _
            & "  a.pesan_kode_barang2 as pesan_kode_barang, " _
            & "  a.pesan_nama_barang2 as pesan_nama_barang, " _
            & "  a.pesan_qty2 as pesan_qty, " _
            & "  a.pesan_ket2 as pesan_ket " _
            & "FROM " _
            & "  public.pesanan a " _
            & "  INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "  INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pesan_oid").ToString & "' and length(pesan_kode_barang2) > 0 " _
            & "  union all " _
            & "SELECT  " _
            & "  a.pesan_oid, " _
            & "  a.pesan_kode_barang3  as pesan_kode_barang, " _
            & "  a.pesan_nama_barang3  as pesan_nama_barang, " _
            & "  a.pesan_qty3  as pesan_qty, " _
            & "  a.pesan_ket3  as pesan_ket " _
            & "FROM " _
            & "  public.pesanan a " _
            & "  INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "  INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pesan_oid").ToString & "' and length(pesan_kode_barang3) > 0 " _
            & "  union all " _
            & "SELECT  " _
            & "  a.pesan_oid, " _
            & "  a.pesan_kode_barang4  as pesan_kode_barang, " _
            & "  a.pesan_nama_barang4  as pesan_nama_barang, " _
            & "  a.pesan_qty4  as pesan_qty, " _
            & "  a.pesan_ket4  as pesan_ket " _
            & "FROM " _
            & "  public.pesanan a " _
            & "  INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "  INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pesan_oid").ToString & "' and length(pesan_kode_barang4) > 0 "

        load_data_detail(sql, gc_detail, "detail")

       
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        pb_en_id.Focus()
        pb_en_id.ItemIndex = 0
        pb_date.DateTime = Now
        pb_due_date.DateTime = Now
        pb_requested.Text = ""
        pb_end_user.Text = ""
        pb_rmks.Text = ""
        pb_tran_id.ItemIndex = 0
        pb_pbt_code.ItemIndex = 0
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pbd_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        insert = False
        
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean

        edit_data = False
    End Function

    Public Overrides Function edit()
        edit = False
        Return edit
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = False
      
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = False
      

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region


    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_en_id")
        _type = 7
        _table = "pb_mstr"
        _initial = "pb"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT " _
        & "pb_mstr.pb_oid, " _
        & "pb_mstr.pb_dom_id,  " _
        & "pb_mstr.pb_en_id,  " _
        & "pb_mstr.pb_add_by,  " _
        & "pb_mstr.pb_add_date, " _
        & "pb_mstr.pb_upd_by,  " _
        & "pb_mstr.pb_upd_date,  " _
        & "pb_mstr.pb_date,  " _
        & "pb_mstr.pb_due_date, " _
        & "pb_mstr.pb_requested,  " _
        & "pb_mstr.pb_end_user,  " _
        & "pb_mstr.pb_rmks, " _
        & "pb_mstr.pb_status,  " _
        & "pb_mstr.pb_close_date,  " _
        & "pb_mstr.pb_dt,  " _
        & "pb_mstr.pb_code, " _
        & "pbd_det.pbd_oid,  " _
        & "pbd_det.pbd_dom_id,  " _
        & "pbd_det.pbd_en_id,  " _
        & "pbd_det.pbd_add_by,  " _
        & "pbd_det.pbd_add_date,  " _
        & "pbd_det.pbd_upd_by,  " _
        & "pbd_det.pbd_upd_date,  " _
        & "pbd_det.pbd_pb_oid,  " _
        & "pbd_det.pbd_seq,  " _
        & "pbd_det.pbd_pt_id,  " _
        & "pbd_det.pbd_rmks, " _
        & "pbd_det.pbd_end_user,  " _
        & "pbd_det.pbd_qty,  " _
        & "pbd_det.pbd_qty_processed, " _
        & "pbd_det.pbd_qty_completed,  " _
        & "pbd_det.pbd_um,  " _
        & "pbd_det.pbd_due_date,  " _
        & "pbd_det.pbd_status,  " _
        & "pbd_det.pbd_dt,  " _
        & "pt_mstr.pt_code,  " _
        & "pt_mstr.pt_desc1,  " _
        & "pt_mstr.pt_desc2,  " _
        & "en_mstr.en_id,  " _
        & "en_mstr.en_desc, " _
        & "cmaddr_en_id, " _
        & "cmaddr_mstr.cmaddr_name,  " _
        & "cmaddr_mstr.cmaddr_line_1, " _
        & "cmaddr_mstr.cmaddr_line_2,  " _
        & "cmaddr_mstr.cmaddr_line_3, " _
        & "code_mstr.code_id,  " _
        & "code_mstr.code_code, " _
        & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        & "FROM pb_mstr " _
        & "inner join pbd_det on pb_mstr.pb_oid = pbd_det.pbd_pb_oid " _
        & "inner join en_mstr on pb_mstr.pb_en_id = en_mstr.en_id " _
        & "inner join pt_mstr on pbd_det.pbd_pt_id = pt_mstr.pt_id " _
        & "inner join code_mstr on pbd_det.pbd_um = code_mstr.code_id  " _
        & "inner join cmaddr_mstr on pb_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
        & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
        & "WHERE " _
        & "pb_mstr.pb_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code") + "' " _
        & "order by pb_mstr.pb_code"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInventoryReqPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        frm.ShowDialog()

    End Sub
    'Public Overrides Function export_data() As Boolean

    '    Dim ssql As String
    '    Try
    '        ssql = "SELECT " _
    '            & "pb_mstr.pb_oid, " _
    '            & "pb_mstr.pb_dom_id,  " _
    '            & "pb_mstr.pb_en_id,  " _
    '            & "pb_mstr.pb_add_by,  " _
    '            & "pb_mstr.pb_add_date, " _
    '            & "pb_mstr.pb_upd_by,  " _
    '            & "pb_mstr.pb_upd_date,  " _
    '            & "pb_mstr.pb_date,  " _
    '            & "pb_mstr.pb_due_date, " _
    '            & "pb_mstr.pb_requested,  " _
    '            & "pb_mstr.pb_end_user,  " _
    '            & "pb_mstr.pb_rmks, " _
    '            & "pb_mstr.pb_status,pbt_desc,  " _
    '            & "pb_mstr.pb_close_date,  " _
    '            & "pb_mstr.pb_dt,  " _
    '            & "pb_mstr.pb_code, " _
    '            & "pbd_det.pbd_oid,  " _
    '            & "pbd_det.pbd_dom_id,  " _
    '            & "pbd_det.pbd_en_id,  " _
    '            & "pbd_det.pbd_add_by,  " _
    '            & "pbd_det.pbd_add_date,  " _
    '            & "pbd_det.pbd_upd_by,  " _
    '            & "pbd_det.pbd_upd_date,  " _
    '            & "pbd_det.pbd_pb_oid,  " _
    '            & "pbd_det.pbd_seq,  " _
    '            & "pbd_det.pbd_pt_id,  " _
    '            & "pbd_det.pbd_rmks, " _
    '            & "pbd_det.pbd_end_user,  " _
    '            & "pbd_det.pbd_qty,  " _
    '            & "pbd_det.pbd_qty_processed, " _
    '            & "pbd_det.pbd_qty_completed,  " _
    '            & "pbd_det.pbd_um,  " _
    '            & "pbd_det.pbd_due_date,  " _
    '            & "pbd_det.pbd_status,  " _
    '            & "pbd_det.pbd_dt,  " _
    '            & "pt_mstr.pt_code,  " _
    '            & "pt_mstr.pt_desc1,  " _
    '            & "pt_mstr.pt_desc2,  " _
    '            & "en_mstr.en_id,  " _
    '            & "en_mstr.en_desc, " _
    '            & "cmaddr_en_id, " _
    '            & "cmaddr_mstr.cmaddr_name,  " _
    '            & "cmaddr_mstr.cmaddr_line_1, " _
    '            & "cmaddr_mstr.cmaddr_line_2,  " _
    '            & "cmaddr_mstr.cmaddr_line_3, " _
    '            & "code_mstr.code_id,  " _
    '            & "code_mstr.code_code, " _
    '            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
    '            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
    '            & "FROM pb_mstr " _
    '            & "inner join pbd_det on pb_mstr.pb_oid = pbd_det.pbd_pb_oid " _
    '            & "inner join en_mstr on pb_mstr.pb_en_id = en_mstr.en_id " _
    '            & "inner join pt_mstr on pbd_det.pbd_pt_id = pt_mstr.pt_id " _
    '            & "inner join code_mstr on pbd_det.pbd_um = code_mstr.code_id  " _
    '            & "inner join cmaddr_mstr on pb_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
    '            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
    '            & "  LEFT OUTER JOIN public.pbt_type ON (public.pb_mstr.pb_pbt_code = public.pbt_type.pbt_code) " _
    '            & "WHERE " _
    '            & "  pb_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " AND  " _
    '            & "  pb_en_id in (select user_en_id from tconfuserentity " _
    '            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
    '            & " order by pb_mstr.pb_code"


    '        Dim frm As New frmExport
    '        Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

    '        With frm


    '            add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
    '            add_column_copy(.gv_export, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
    '            add_column_copy(.gv_export, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Status", "pb_trans_id", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Type", "pbt_desc", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)
    '            add_column_copy(.gv_export, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    '            add_column_copy(.gv_export, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    '            add_column_copy(.gv_export, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    '            add_column_copy(.gv_export, "Qty Trans Process", "pbd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    '            add_column_copy(.gv_export, "Qty Trans Complete", "pbd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    '            add_column_copy(.gv_export, "Qty Issue Process", "pbd_qty_riud", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    '            add_column_copy(.gv_export, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
    '            add_column_copy(.gv_export, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
    '            add_column_copy(.gv_export, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)


    '            .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
    '            .gv_export.BestFitColumns()
    '            .gv_export.ExportToXls(_file)
    '        End With

    '        frm.Dispose()
    '        Box("Export data sucess")
    '        OpenFile(_file)


    '    Catch ex As Exception
    '        Pesan(Err)
    '        Return False
    '    End Try

    'End Function


    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_detail()
    End Sub

    
End Class
