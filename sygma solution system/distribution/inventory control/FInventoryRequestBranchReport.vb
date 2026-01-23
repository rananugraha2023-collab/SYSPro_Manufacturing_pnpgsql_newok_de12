Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FInventoryRequestBranchReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pb_oid_mstr As String
    Dim mf As New master_new.ModFunction
    Public ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pbd_related_oid As String = ""
    Dim _conf_value As String
    Dim _now As Date

    Private Sub FInventorypbuest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_cash_out_ar1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_cash_out_ar1.DataTable1)
        ' _conf_value = func_coll.get_conf_file("wf_inventory_request")
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

        'xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pb_en_id.Properties.DataSource = dt_bantu
        pb_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pb_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pb_en_id.ItemIndex = 0

        'If _conf_value = "0" Then
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_tran_mstr())
        pb_tran_id.Properties.DataSource = dt_bantu
        pb_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        pb_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        pb_tran_id.ItemIndex = 0
        'Else
        '    dt_bantu = New DataTable
        '    dt_bantu = (func_data.load_transaction())
        '    pb_tran_id.Properties.DataSource = dt_bantu
        '    pb_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        '    pb_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
        '    pb_tran_id.ItemIndex = 0
        'End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "pb_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "pbt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_master, "pbd_pb_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Process", "pbd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Outstanding", "pbd_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Qty Issue Process", "pbd_qty_riud", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Status Detail", "pbd_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")




    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.pb2_mstr.pb_upd_date, " _
                    & "  public.pb2_mstr.pb_upd_by, " _
                    & "  public.pb2_mstr.pb_add_date, " _
                    & "  public.pb2_mstr.pb_add_by, " _
                    & "  public.pb2_mstr.pb_code, " _
                    & "  public.pb2_mstr.pb_date, " _
                    & "  public.pb2_mstr.pb_due_date, " _
                    & "  public.pb2_mstr.pb_requested, " _
                    & "  public.pb2_mstr.pb_end_user, " _
                    & "  public.pb2_mstr.pb_rmks, " _
                    & "  public.pb2_mstr.pb_status, " _
                    & "  public.pb2_mstr.pb_tran_id, " _
                    & "  public.pb2_mstr.pb_trans_id, " _
                    & "  public.pb2_mstr.pb_close_date, " _
                    & "  public.pb2_mstr.pb_dt, " _
                    & "  public.pb2d_det.pbd_oid, " _
                    & "  public.pb2d_det.pbd_dom_id, " _
                    & "  public.pb2d_det.pbd_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pb2d_det.pbd_si_id, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.pb2d_det.pbd_add_by, " _
                    & "  public.pb2d_det.pbd_add_date, " _
                    & "  public.pb2d_det.pbd_upd_by, " _
                    & "  public.pb2d_det.pbd_upd_date, " _
                    & "  public.pb2d_det.pbd_pb_oid, " _
                    & "  public.pb2d_det.pbd_seq, " _
                    & "  public.pb2d_det.pbd_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pb2d_det.pbd_rmks, " _
                    & "  public.pb2d_det.pbd_end_user, " _
                    & "  public.pb2d_det.pbd_due_date, " _
                    & "  public.pb2d_det.pbd_qty, " _
                    & "  public.pb2d_det.pbd_qty_processed,public.pb2d_det.pbd_qty- coalesce(public.pb2d_det.pbd_qty_processed,0) as  pbd_qty_outstanding, " _
                    & "  public.pb2d_det.pbd_qty_completed, " _
                    & "  public.pb2d_det.pbd_qty_riud, " _
                    & "  public.pb2d_det.pbd_um, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.pb2d_det.pbd_status, " _
                    & "  public.pb2d_det.pbd_dt,pb_pbt_code,pbt_desc " _
                    & "  FROM " _
                    & "  public.pb2d_det " _
                    & "  LEFT OUTER JOIN public.si_mstr ON (public.pb2d_det.pbd_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.pb2d_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.pb2d_det.pbd_um = public.code_mstr.code_id) " _
                    & "  INNER JOIN public.pb2_mstr ON (public.pb2_mstr.pb_oid = public.pb2d_det.pbd_pb_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.pb2_mstr.pb_en_id = public.en_mstr.en_id) " _
                    & "  LEFT OUTER JOIN public.pbt_type ON (public.pb2_mstr.pb_pbt_code = public.pbt_type.pbt_code) " _
                    & "  where  " _
                    & "  pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " and pb_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "



        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        load_detail()

    End Sub

    Public Overrides Sub relation_detail()
        'Try
        '    gv_detail.Columns("pbd_pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pbd_pb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'")
        '    gv_detail.BestFitColumns()

        '    If _conf_value = "1" Then
        '        gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code").ToString & "'")
        '        gv_wf.BestFitColumns()

        '        gv_email.Columns("pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'")
        '        gv_email.BestFitColumns()
        '    End If

        'Catch ex As Exception
        'End Try
    End Sub
    Public Sub load_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String

        'Try
        '    ds.Tables("detail").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  public.pb2d_det.pbd_oid, " _
        '    & "  public.pb2d_det.pbd_dom_id, " _
        '    & "  public.pb2d_det.pbd_en_id, " _
        '    & "  public.en_mstr.en_desc, " _
        '    & "  public.pb2d_det.pbd_si_id, " _
        '    & "  public.si_mstr.si_desc, " _
        '    & "  public.pb2d_det.pbd_add_by, " _
        '    & "  public.pb2d_det.pbd_add_date, " _
        '    & "  public.pb2d_det.pbd_upd_by, " _
        '    & "  public.pb2d_det.pbd_upd_date, " _
        '    & "  public.pb2d_det.pbd_pb_oid, " _
        '    & "  public.pb2d_det.pbd_seq, " _
        '    & "  public.pb2d_det.pbd_pt_id, " _
        '    & "  public.pt_mstr.pt_code, " _
        '    & "  public.pt_mstr.pt_desc1, " _
        '    & "  public.pt_mstr.pt_desc2, " _
        '    & "  public.pb2d_det.pbd_rmks, " _
        '    & "  public.pb2d_det.pbd_end_user, " _
        '    & "  public.pb2d_det.pbd_due_date, " _
        '    & "  public.pb2d_det.pbd_qty, " _
        '    & "  public.pb2d_det.pbd_qty_processed, " _
        '    & "  public.pb2d_det.pbd_qty_completed, " _
        '    & "  public.pb2d_det.pbd_qty_riud, " _
        '    & "  public.pb2d_det.pbd_um, " _
        '    & "  public.code_mstr.code_name, " _
        '    & "  public.pb2d_det.pbd_status, " _
        '    & "  public.pb2d_det.pbd_dt " _
        '    & "  FROM " _
        '    & "  public.pb2d_det " _
        '    & "  LEFT OUTER JOIN public.si_mstr ON (public.pb2d_det.pbd_si_id = public.si_mstr.si_id) " _
        '    & "  INNER JOIN public.en_mstr ON (public.pb2d_det.pbd_en_id = public.en_mstr.en_id) " _
        '    & "  INNER JOIN public.pt_mstr ON (public.pb2d_det.pbd_pt_id = public.pt_mstr.pt_id) " _
        '    & "  INNER JOIN public.code_mstr ON (public.pb2d_det.pbd_um = public.code_mstr.code_id) " _
        '    & "  where pb2d_det.pbd_pb_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"


        'load_data_detail(sql, gc_detail, "detail")

        'If _conf_value = "1" Then
        '    Try
        '        ds.Tables("wf").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
        '          " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
        '          " wf_iscurrent, wf_seq " + _
        '          " from wf_mstr w " + _
        '          " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
        '          " inner join pb2_mstr on pb_code = wf_ref_code " + _
        '          " inner join pb2d_det dt on dt.pbd_pb_oid = pb_oid " _
        '        & " where pb2_mstr.pb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'" _
        '        & " order by wf_ref_code, wf_seq "
        '    load_data_detail(sql, gc_wf, "wf")
        '    gv_wf.BestFitColumns()

        '    Try
        '        ds.Tables("email").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "SELECT  " _
        '       & "  public.en_mstr.en_desc, " _
        '        & "  public.pb2_mstr.pb_oid, " _
        '        & "  public.pb2_mstr.pb_dom_id, " _
        '        & "  public.pb2_mstr.pb_en_id, " _
        '        & "  public.pb2_mstr.pb_upd_date, " _
        '        & "  public.pb2_mstr.pb_upd_by, " _
        '        & "  public.pb2_mstr.pb_add_date, " _
        '        & "  public.pb2_mstr.pb_add_by, " _
        '        & "  public.pb2_mstr.pb_code, " _
        '        & "  public.pb2_mstr.pb_date, " _
        '        & "  public.pb2_mstr.pb_due_date, " _
        '        & "  public.pb2_mstr.pb_requested, " _
        '        & "  public.pb2_mstr.pb_end_user, " _
        '        & "  public.pb2_mstr.pb_rmks, " _
        '        & "  public.pb2_mstr.pb_status, " _
        '        & "  public.pb2_mstr.pb_close_date, " _
        '        & "  public.pb2_mstr.pb_dt, " _
        '        & "  public.pb2d_det.pbd_oid, " _
        '        & "  public.pb2d_det.pbd_dom_id, " _
        '        & "  public.pb2d_det.pbd_en_id, " _
        '        & "  public.en_mstr.en_desc, " _
        '        & "  public.pb2d_det.pbd_add_by, " _
        '        & "  public.pb2d_det.pbd_add_date, " _
        '        & "  public.pb2d_det.pbd_upd_by, " _
        '        & "  public.pb2d_det.pbd_upd_date, " _
        '        & "  public.pb2d_det.pbd_pb_oid, " _
        '        & "  public.pb2d_det.pbd_seq, " _
        '        & "  public.pb2d_det.pbd_pt_id, " _
        '        & "  public.pt_mstr.pt_code, " _
        '        & "  public.pt_mstr.pt_desc1, " _
        '        & "  public.pt_mstr.pt_desc2, " _
        '        & "  public.pb2d_det.pbd_rmks, " _
        '        & "  public.pb2d_det.pbd_end_user, " _
        '        & "  public.pb2d_det.pbd_due_date, " _
        '        & "  public.pb2d_det.pbd_qty, " _
        '        & "  public.pb2d_det.pbd_qty_processed, " _
        '        & "  public.pb2d_det.pbd_qty_completed, " _
        '        & "  public.pb2d_det.pbd_um, " _
        '        & "  public.code_mstr.code_name, " _
        '        & "  public.pb2d_det.pbd_status, " _
        '        & "  public.pb2d_det.pbd_dt " _
        '        & "FROM " _
        '        & "  public.pb2_mstr " _
        '        & " inner join pb2d_det on pbd_pb_oid = pb_oid " _
        '        & " inner join en_mstr on en_id = pb_en_id  " _
        '        & "  INNER JOIN public.pt_mstr ON (public.pb2d_det.pbd_pt_id = public.pt_mstr.pt_id) " _
        '        & "  INNER JOIN public.code_mstr ON (public.pb2d_det.pbd_um = public.code_mstr.code_id) " _
        '        & " where pb2_mstr.pb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"

        '    load_data_detail(sql, gc_email, "email")
        '    gv_email.BestFitColumns()

        '    Try
        '        ds.Tables("smart").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "select pb_oid, pb_code, pb_trans_id, false as status from pb2_mstr " _
        '        & " where pb_trans_id ~~* 'd' "

        '    load_data_detail(sql, gc_smart, "smart")
        'End If
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
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        'MyBase.insert_data()

        'ds_edit = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT  " _
        '                & "  public.pb2d_det.pbd_oid, " _
        '                & "  public.pb2d_det.pbd_dom_id, " _
        '                & "  public.pb2d_det.pbd_en_id, " _
        '                & "  public.en_mstr.en_desc, " _
        '                & "  public.pb2d_det.pbd_si_id, " _
        '                & "  public.si_mstr.si_desc, " _
        '                & "  public.pb2d_det.pbd_add_by, " _
        '                & "  public.pb2d_det.pbd_add_date, " _
        '                & "  public.pb2d_det.pbd_upd_by, " _
        '                & "  public.pb2d_det.pbd_upd_date, " _
        '                & "  public.pb2d_det.pbd_pb_oid, " _
        '                & "  public.pb2d_det.pbd_seq, " _
        '                & "  public.pb2d_det.pbd_pt_id, " _
        '                & "  public.pt_mstr.pt_code, " _
        '                & "  public.pt_mstr.pt_desc1, " _
        '                & "  public.pt_mstr.pt_desc2, " _
        '                & "  public.pb2d_det.pbd_rmks, " _
        '                & "  public.pb2d_det.pbd_end_user, " _
        '                & "  public.pb2d_det.pbd_qty, " _
        '                & "  public.pb2d_det.pbd_qty_processed, " _
        '                & "  public.pb2d_det.pbd_qty_completed, " _
        '                & "  public.pb2d_det.pbd_um, " _
        '                & "  public.code_mstr.code_name, " _
        '                & "  public.pb2d_det.pbd_due_date, " _
        '                & "  public.pb2d_det.pbd_status, " _
        '                & "  public.pb2d_det.pbd_dt " _
        '                & "FROM " _
        '                & "  public.pb2d_det " _
        '                & "  LEFT OUTER JOIN public.si_mstr ON (public.pb2d_det.pbd_si_id = public.si_mstr.si_id) " _
        '                & "  INNER JOIN public.pt_mstr ON (public.pb2d_det.pbd_pt_id = public.pt_mstr.pt_id) " _
        '                & "  INNER JOIN public.code_mstr ON (public.pb2d_det.pbd_um = public.code_mstr.code_id) " _
        '                & "  INNER JOIN public.en_mstr ON (public.pb2d_det.pbd_en_id = public.en_mstr.en_id) " _
        '                & " where public.pb2d_det.pbd_oid is null"

        '            .InitializeCommand()
        '            .FillDataSet(ds_edit, "detail")
        '            gc_edit.DataSource = ds_edit.Tables(0)
        '            gv_edit.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Function

    Public Overrides Function before_save() As Boolean
        'before_save = True
        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()
        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("pbd_qty") = 0 Then
        '        MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'Next

        'Return before_save
    End Function

    Public Overrides Function insert() As Boolean

    End Function

    Public Overrides Function edit_data() As Boolean

    End Function


    Public Overrides Function before_delete() As Boolean

    End Function

    Public Overrides Function delete_data() As Boolean

    End Function

    Public Overrides Function cancel_data() As Boolean

    End Function
#End Region

#Region "gv_edit"
    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        'If e.Control And e.KeyCode = Keys.I Then
        '    gv_edit.AddNewRow()
        'ElseIf e.Control And e.KeyCode = Keys.D Then
        '    gv_edit.DeleteSelectedRows()
        'End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        'If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        '    browse_data()
        'End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        'Dim _col As String = gv_edit.FocusedColumn.Name
        'Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "pbd_en_id")

        'If _col = "en_desc" Then
        '    Dim frm As New FEntitySearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "si_desc" Then
        '    Dim frm As New FSiteSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _pod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "pt_code" Then
        '    Dim frm As New FPartNumberSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _pod_en_id
        '    frm._si_id = gv_edit.GetRowCellValue(_row, "pbd_si_id")
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "code_name" Then
        '    Dim frm As New FUMSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _pod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        'Dim _pbd_qty, _pbd_um_conv, _pbd_cost, _pbd_disc, _pbd_qty_processed As Double
        '_pbd_um_conv = 1
        '_pbd_qty = 1
        '_pbd_cost = 0
        '_pbd_disc = 0

        'If e.Column.Name = "pbd_qty" Then
        '    '********* Cek Qty Processed
        '    Try
        '        _pbd_qty_processed = (gv_edit.GetRowCellValue(e.RowHandle, "pbd_qty_processed"))
        '    Catch ex As Exception
        '    End Try

        '    If e.Value < _pbd_qty_processed Then
        '        MessageBox.Show("Qty Request Can't Lower Than Qty Processed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        gv_edit.CancelUpdateCurrentRow()
        '        Exit Sub
        '    End If
        '    '*******************************            
        'End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'With gv_edit
        '    .SetRowCellValue(e.RowHandle, "pbd_en_id", pb_en_id.EditValue)
        '    .SetRowCellValue(e.RowHandle, "en_desc", pb_en_id.GetColumnValue("en_desc"))
        '    .SetRowCellValue(e.RowHandle, "pbd_end_user", Trim(pb_end_user.Text))
        '    .SetRowCellValue(e.RowHandle, "pbd_qty", 0)
        '    .SetRowCellValue(e.RowHandle, "pbd_due_date", pb_due_date.DateTime)
        '    .BestFitColumns()
        'End With
    End Sub
#End Region

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Dim _pbd_qty_processed As Double = 0

        'Try
        '    _pbd_qty_processed = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "pbd_qty_processed")))
        'Catch ex As Exception
        'End Try

        'If _pbd_qty_processed <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        'Dim _pbd_qty_processed As Double = 0

        'Try
        '    _pbd_qty_processed = ((gv_edit.GetRowCellValue(0, "pbd_qty_processed")))
        'Catch ex As Exception
        'End Try

        'If _pbd_qty_processed <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
    End Sub

    Public Overrides Sub approve_line()
        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid")
        '_colom = "pb_trans_id"
        '_table = "pb2_mstr"
        '_criteria = "pb_code"
        '_initial = "pb"
        '_type = "ir"
        '_title = "Inventory Request"
        'approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                  ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)

    End Sub
    Public Function preview_export(ByVal par_filename As String) As Boolean
        Try
            Dim _sql As String

            Dim _en_id As Integer
            Dim _type, _table, _initial, _code_awal, _code_akhir As String

            _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_en_id")
            _type = 7
            _table = "pb2_mstr"
            _initial = "pb"
            _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
            _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")

            func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

            'Dim ds_bantu As New DataSet

            _sql = "SELECT " _
                & "pb2_mstr.pb_oid, " _
                & "pb2_mstr.pb_dom_id,  " _
                & "pb2_mstr.pb_en_id,  " _
                & "pb2_mstr.pb_add_by,  " _
                & "pb2_mstr.pb_add_date, " _
                & "pb2_mstr.pb_upd_by,  " _
                & "pb2_mstr.pb_upd_date,  " _
                & "pb2_mstr.pb_date,  " _
                & "pb2_mstr.pb_due_date, " _
                & "pb2_mstr.pb_requested,  " _
                & "pb2_mstr.pb_end_user,  " _
                & "pb2_mstr.pb_rmks, " _
                & "pb2_mstr.pb_status,  " _
                & "pb2_mstr.pb_close_date,  " _
                & "pb2_mstr.pb_dt,  " _
                & "pb2_mstr.pb_code, " _
                & "pb2d_det.pbd_oid,  " _
                & "pb2d_det.pbd_dom_id,  " _
                & "pb2d_det.pbd_en_id,  " _
                & "pb2d_det.pbd_add_by,  " _
                & "pb2d_det.pbd_add_date,  " _
                & "pb2d_det.pbd_upd_by,  " _
                & "pb2d_det.pbd_upd_date,  " _
                & "pb2d_det.pbd_pb_oid,  " _
                & "pb2d_det.pbd_seq,  " _
                & "pb2d_det.pbd_pt_id,  " _
                & "pb2d_det.pbd_rmks, " _
                & "pb2d_det.pbd_end_user,  " _
                & "pb2d_det.pbd_qty,  " _
                & "pb2d_det.pbd_qty_processed, " _
                & "pb2d_det.pbd_qty_completed,  " _
                & "pb2d_det.pbd_um,  " _
                & "pb2d_det.pbd_due_date,  " _
                & "pb2d_det.pbd_status,  " _
                & "pb2d_det.pbd_dt,  " _
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
                & "FROM pb2_mstr " _
                & "inner join pb2d_det on pb2_mstr.pb_oid = pb2d_det.pbd_pb_oid " _
                & "inner join en_mstr on pb2_mstr.pb_en_id = en_mstr.en_id " _
                & "inner join pt_mstr on pb2d_det.pbd_pt_id = pt_mstr.pt_id " _
                & "inner join code_mstr on pb2d_det.pbd_um = code_mstr.code_id  " _
                & "inner join cmaddr_mstr on pb2_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
                & "WHERE " _
                & "pb2_mstr.pb_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code") + "' " _
                & "order by pb2_mstr.pb_code"

            Dim rpt As New XRInventoryReqPrint
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong. Gagal email silahkan perbaiki data kemudian klik remainder email")
                    'Exit Function
                    Return False
                    Exit Function
                End If

                .DataSource = ds
                .DataMember = "Table"

                If IO.Directory.Exists(appbase() & "\export") = False Then
                    IO.Directory.CreateDirectory(appbase() & "\export")
                End If

                'export image
                Dim imageOptions As DevExpress.XtraPrinting.ImageExportOptions = rpt.ExportOptions.Image

                ' Set Image-specific export options.
                imageOptions.Resolution = 72
                imageOptions.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile
                imageOptions.Format = System.Drawing.Imaging.ImageFormat.Png


                .ExportToImage(par_filename, imageOptions)

            End With
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function


    Public Overrides Sub cancel_line()

    End Sub

    Public Overrides Sub reminder_mail()

    End Sub

    Public Overrides Sub smart_approve()

    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_en_id")
        _type = 7
        _table = "pb2_mstr"
        _initial = "pb"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT " _
        & "pb2_mstr.pb_oid, " _
        & "pb2_mstr.pb_dom_id,  " _
        & "pb2_mstr.pb_en_id,  " _
        & "pb2_mstr.pb_add_by,  " _
        & "pb2_mstr.pb_add_date, " _
        & "pb2_mstr.pb_upd_by,  " _
        & "pb2_mstr.pb_upd_date,  " _
        & "pb2_mstr.pb_date,  " _
        & "pb2_mstr.pb_due_date, " _
        & "pb2_mstr.pb_requested,  " _
        & "pb2_mstr.pb_end_user,  " _
        & "pb2_mstr.pb_rmks, " _
        & "pb2_mstr.pb_status,  " _
        & "pb2_mstr.pb_close_date,  " _
        & "pb2_mstr.pb_dt,  " _
        & "pb2_mstr.pb_code, " _
        & "pb2d_det.pbd_oid,  " _
        & "pb2d_det.pbd_dom_id,  " _
        & "pb2d_det.pbd_en_id,  " _
        & "pb2d_det.pbd_add_by,  " _
        & "pb2d_det.pbd_add_date,  " _
        & "pb2d_det.pbd_upd_by,  " _
        & "pb2d_det.pbd_upd_date,  " _
        & "pb2d_det.pbd_pb_oid,  " _
        & "pb2d_det.pbd_seq,  " _
        & "pb2d_det.pbd_pt_id,  " _
        & "pb2d_det.pbd_rmks, " _
        & "pb2d_det.pbd_end_user,  " _
        & "pb2d_det.pbd_qty,  " _
        & "pb2d_det.pbd_qty_processed, " _
        & "pb2d_det.pbd_qty_completed,  " _
        & "pb2d_det.pbd_um,  " _
        & "pb2d_det.pbd_due_date,  " _
        & "pb2d_det.pbd_status,  " _
        & "pb2d_det.pbd_dt,  " _
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
        & "FROM pb2_mstr " _
        & "inner join pb2d_det on pb2_mstr.pb_oid = pb2d_det.pbd_pb_oid " _
        & "inner join en_mstr on pb2_mstr.pb_en_id = en_mstr.en_id " _
        & "inner join pt_mstr on pb2d_det.pbd_pt_id = pt_mstr.pt_id " _
        & "inner join code_mstr on pb2d_det.pbd_um = code_mstr.code_id  " _
        & "inner join cmaddr_mstr on pb2_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
        & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
        & "WHERE " _
        & "pb2_mstr.pb_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code") + "' " _
        & "order by pb2_mstr.pb_code"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInventoryReqPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        frm.ShowDialog()

    End Sub
    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT " _
                & "pb2_mstr.pb_oid, " _
                & "pb2_mstr.pb_dom_id,  " _
                & "pb2_mstr.pb_en_id,  " _
                & "pb2_mstr.pb_add_by,  " _
                & "pb2_mstr.pb_add_date, " _
                & "pb2_mstr.pb_upd_by,  " _
                & "pb2_mstr.pb_upd_date,  " _
                & "pb2_mstr.pb_date,  " _
                & "pb2_mstr.pb_due_date, " _
                & "pb2_mstr.pb_requested,  " _
                & "pb2_mstr.pb_end_user,  " _
                & "pb2_mstr.pb_rmks, " _
                & "pb2_mstr.pb_status,  " _
                & "pb2_mstr.pb_close_date,  " _
                & "pb2_mstr.pb_dt,  " _
                & "pb2_mstr.pb_code, " _
                & "pb2d_det.pbd_oid,  " _
                & "pb2d_det.pbd_dom_id,  " _
                & "pb2d_det.pbd_en_id,  " _
                & "pb2d_det.pbd_add_by,  " _
                & "pb2d_det.pbd_add_date,  " _
                & "pb2d_det.pbd_upd_by,  " _
                & "pb2d_det.pbd_upd_date,  " _
                & "pb2d_det.pbd_pb_oid,  " _
                & "pb2d_det.pbd_seq,  " _
                & "pb2d_det.pbd_pt_id,  " _
                & "pb2d_det.pbd_rmks, " _
                & "pb2d_det.pbd_end_user,  " _
                & "pb2d_det.pbd_qty,  " _
                & "pb2d_det.pbd_qty_processed, " _
                & "pb2d_det.pbd_qty_completed,  " _
                & "pb2d_det.pbd_um,  " _
                & "pb2d_det.pbd_due_date,  " _
                & "pb2d_det.pbd_status,  " _
                & "pb2d_det.pbd_dt,  " _
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
                & "FROM pb2_mstr " _
                & "inner join pb2d_det on pb2_mstr.pb_oid = pb2d_det.pbd_pb_oid " _
                & "inner join en_mstr on pb2_mstr.pb_en_id = en_mstr.en_id " _
                & "inner join pt_mstr on pb2d_det.pbd_pt_id = pt_mstr.pt_id " _
                & "inner join code_mstr on pb2d_det.pbd_um = code_mstr.code_id  " _
                & "inner join cmaddr_mstr on pb2_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
                & "WHERE " _
                & "  pb_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " AND  " _
                & "  pb_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " order by pb2_mstr.pb_code"


            Dim frm As New frmExport
            Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

            With frm
                'add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "PR Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Entity", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Date", "req_date", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Duedate", "req_due_date", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)

                'add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Remark Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                'add_column_copy(.gv_export, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                'add_column_copy(.gv_export, "Qty Komplit", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                'add_column_copy(.gv_export, "UM", "code_code", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Status", "pb_trans_id", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
                add_column_copy(.gv_export, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

                add_column_copy(.gv_export, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Trans Process", "pbd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Trans Complete", "pbd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Issue Process", "pbd_qty_riud", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)


                .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                .gv_export.BestFitColumns()
                .gv_export.ExportToXls(_file)
            End With

            frm.Dispose()
            Box("Export data sucess")
            OpenFile(_file)


        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function


    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_detail()
    End Sub

    Private Sub BeCopySQ_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BeCopySQ.ButtonClick

    End Sub

    Private Sub UpdateEntityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateEntityToolStripMenuItem.Click
        Try
            For Each dr As DataRow In ds_edit.Tables(0).Rows
                dr("pbd_en_id") = pb_en_id.EditValue
                dr("en_desc") = pb_en_id.Text
            Next
            ds_edit.AcceptChanges()
            Box("Success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
