Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FTransferReceiptReport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial As DataSet
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _so_oid, _pb_oid As String
    Dim _conf_value As String
    Dim _mode As String



    Private Sub FTransferIssues_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_cash_out_ar1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_cash_out_ar1.DataTable1)
        '_conf_value = func_coll.get_conf_file("wf_requisition")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        'ptsfr_loc_git.Enabled = False

        'If _conf_value = "0" Then
        '    xtc_detail.TabPages(2).PageVisible = False
        '    xtc_detail.TabPages(4).PageVisible = False
        'ElseIf _conf_value = "1" Then
        '    xtc_detail.TabPages(2).PageVisible = True
        '    xtc_detail.TabPages(4).PageVisible = True
        'End If

        'xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ptsfr_en_id.Properties.DataSource = dt_bantu
        ptsfr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptsfr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptsfr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran_global())
        ptsfr_en_to_id.Properties.DataSource = dt_bantu
        ptsfr_en_to_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptsfr_en_to_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptsfr_en_to_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            ptsfr_tran_id.Properties.DataSource = dt_bantu
            ptsfr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            ptsfr_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            ptsfr_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            ptsfr_tran_id.Properties.DataSource = dt_bantu
            ptsfr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            ptsfr_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            ptsfr_tran_id.ItemIndex = 0
        End If
    End Sub

    Private Sub ptsfr_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ptsfr_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(ptsfr_en_id.EditValue))
        ptsfr_si_id.Properties.DataSource = dt_bantu
        ptsfr_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        ptsfr_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        ptsfr_si_id.ItemIndex = 0

        If _mode = "" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_loc_mstr(ptsfr_en_id.EditValue))
            ptsfr_loc_id.Properties.DataSource = dt_bantu
            ptsfr_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
            ptsfr_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
            ptsfr_loc_id.ItemIndex = 0
        End If


        init_le(ptsfr_loc_git, "loc_mstr", ptsfr_en_id.EditValue)

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr(ptsfr_en_id.EditValue))
        'ptsfr_loc_git.Properties.DataSource = dt_bantu
        'ptsfr_loc_git.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'ptsfr_loc_git.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        ''If dt_bantu.Rows.Count > 0 Then
        ''    ptsfr_loc_git.EditValue = dt_bantu.Rows(0).Item("loc_id")
        'End If

        'ptsfr_loc_git.ItemIndex = 0

        Dim ssql As String

        ssql = "select loc_id from loc_mstr where loc_en_id=" & SetInteger(ptsfr_en_id.EditValue) & " and loc_git='Y'"
        Dim dt As New DataTable
        dt = GetTableData(ssql)

        Dim _loc_id As Long = 0

        For Each dr As DataRow In dt.Rows
            _loc_id = dr(0)
        Next

        If _loc_id <> 0 Then
            ptsfr_loc_git.EditValue = _loc_id
        End If

    End Sub

    Private Sub ptsfr_en_to_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ptsfr_en_to_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(ptsfr_en_to_id.EditValue))
        ptsfr_si_to_id.Properties.DataSource = dt_bantu
        ptsfr_si_to_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        ptsfr_si_to_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        ptsfr_si_to_id.ItemIndex = 0

        If SetString(ptsfr_sq_oid.EditValue) = "" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_loc_mstr(ptsfr_en_to_id.EditValue))
            ptsfr_loc_to_id.Properties.DataSource = dt_bantu
            ptsfr_loc_to_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
            ptsfr_loc_to_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
            ptsfr_loc_to_id.ItemIndex = 0
        End If

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transfer Issue Number", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Transfer Receipt Date", "ptsfr_receive_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ptsfr_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "ptsfrd_ptsfr_oid", False)
        add_column_copy(gv_master, "Inv Req Number", "ptsfrd_pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Kirim", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Receipt", "ptsfrd_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Return", "ptsfrd_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "ptsfrd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Lot Number", "ptsfrd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost", "ptsfrd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Status", "ptsfr_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Request Number", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptsfr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptsfr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptsfr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptsfr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")



    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ptsfrd_oid,ptsfrd_pb_code,ptsfrd_pb_oid,ptsfr_receive_date, " _
                    & "  ptsfrd_ptsfr_oid, " _
                    & "  ptsfrd_seq, " _
                    & "  ptsfrd_pt_id, " _
                    & "  ptsfr_code, " _
                    & "  ptsfr_date, " _
                    & "  ptsfr_receive_date, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_ls, " _
                    & "  ptsfrd_qty, " _
                    & "  ptsfrd_qty_receive, " _
                    & "  coalesce(ptsfrd_qty,0) - coalesce(ptsfrd_qty_receive,0) as ptsfrd_qty_return, " _
                    & "  ptsfrd_um, " _
                    & "  code_name as ptsfrd_um_name, " _
                    & "  ptsfrd_lot_serial, " _
                    & "  ptsfrd_cost, " _
                    & "  ptsfrd_dt, " _
                    & "  ptsfrd_pbd_oid,ptsfrd_sqd_oid, " _
                    & "  en_mstr_from.en_desc as en_desc_from, " _
                    & "  ptsfr_si_id, " _
                    & "  si_mstr_from.si_desc as si_desc_from, " _
                    & "  ptsfr_loc_id, " _
                    & "  loc_mstr_from.loc_desc as loc_desc_from, " _
                    & "  ptsfr_loc_git, " _
                    & "  loc_mstr_git.loc_desc as loc_desc_git, " _
                    & "  ptsfr_en_to_id, " _
                    & "  en_mstr_to.en_desc as en_desc_to, " _
                    & "  ptsfr_si_to_id, " _
                    & "  si_mstr_to.si_desc as si_desc_to, " _
                    & "  ptsfr_loc_to_id, " _
                    & "  loc_mstr_to.loc_desc as loc_desc_to, " _
                    & "  ptsfr_remarks, " _
                    & "  ptsfr_add_by, " _
                    & "  ptsfr_add_date, " _
                    & "  ptsfr_upd_by, " _
                    & "  ptsfr_upd_date " _
                    & "FROM  " _
                    & "  public.ptsfrd_det" _
                    & "  INNER JOIN public.ptsfr_mstr ON (public.ptsfrd_det.ptsfrd_ptsfr_oid = public.ptsfr_mstr.ptsfr_oid) " _
                    & "  INNER JOIN public.pt_mstr ON (public.ptsfrd_det.ptsfrd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.ptsfrd_det.ptsfrd_um = public.code_mstr.code_id) " _
                    & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = ptsfr_en_id " _
                    & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = ptsfr_si_id " _
                    & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = ptsfr_loc_id " _
                    & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = ptsfr_loc_git " _
                    & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = ptsfr_en_to_id " _
                    & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = ptsfr_si_to_id " _
                    & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = ptsfr_loc_to_id " _
                    & "  where ptsfr_mstr.ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and ptsfr_mstr.ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
                    & " and ptsfr_en_to_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "


        If ce_qty_rcp.Checked = True Then

            get_sequel = get_sequel + " AND ptsfrd_qty_receive < ptsfrd_qty "

        End If



        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

    End Sub

    Public Overrides Sub relation_detail()

    End Sub

    Public Overrides Sub insert_data_awal()

    End Sub

    Public Overrides Function insert_data() As Boolean

    End Function

    Public Overrides Function before_delete() As Boolean


    End Function

    Public Overrides Function delete_data() As Boolean

    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        'MyBase.cancel_data()
        'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown

    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        'If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        '    browse_data()
        'End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        'browse_data()
    End Sub

    Private Sub browse_data()
        'gv_edit.UpdateCurrentRow()
        'Dim _col As String = gv_edit.FocusedColumn.Name
        'Dim _row As Integer = gv_edit.FocusedRowHandle

        'If _col = "pt_code" Then
        '    'Dim frm As New FPartNumberSearch()
        '    'frm.set_win(Me)
        '    'frm._row = _row
        '    'frm._en_id = ptsfr_en_id.EditValue
        '    'frm._si_id = ptsfr_si_id.EditValue
        '    'frm.type_form = True
        '    'frm.ShowDialog()
        'ElseIf _col = "ptsfrd_pb_code" Then
        '    Dim frm As New FInventoryRequestSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    'frm._obj = riu_ref_pb_code
        '    frm._en_id = ptsfr_en_to_id.EditValue
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Try
        '    gv_serial.Columns("ptsfrds_ptsfrd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("ptsfrd_oid").ToString)
        '    gv_serial.BestFitColumns()
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'With gv_edit
        '    .SetRowCellValue(e.RowHandle, "ptsfrd_oid", Guid.NewGuid.ToString)
        '    .SetRowCellValue(e.RowHandle, "ptsfrd_qty", 0)
        '    .SetRowCellValue(e.RowHandle, "ptsfrd_cost", 0)
        '    .BestFitColumns()
        'End With
    End Sub

    Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow

    End Sub

    Public Overrides Function before_save() As Boolean

    End Function

    Public Overrides Function insert() As Boolean

    End Function

    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown

    End Sub

    Private Sub ptsfr_so_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptsfr_sq_oid.ButtonClick

    End Sub

    Private Sub ptsfr_pb_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptsfr_pb_oid.ButtonClick

    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_en_id")
        _type = 8
        _table = "ptsfr_mstr"
        _initial = "ptsfr"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  ptsfr_oid, " _
            & "  ptsfr_dom_id, " _
            & "  ptsfr_en_id, " _
            & "  ptsfr_add_by, " _
            & "  ptsfr_add_date, " _
            & "  ptsfr_upd_by, " _
            & "  ptsfr_upd_date, " _
            & "  ptsfr_en_to_id, " _
            & "  ptsfr_code, " _
            & "  ptsfr_date, " _
            & "  ptsfr_receive_date, " _
            & "  ptsfr_si_id, " _
            & "  ptsfr_loc_id, " _
            & "  ptsfr_loc_git, " _
            & "  ptsfr_remarks, " _
            & "  ptsfr_trans_id, " _
            & "  ptsfr_dt, " _
            & "  ptsfr_loc_to_id, " _
            & "  ptsfr_si_to_id, " _
            & "  ptsfrd_pt_id, " _
            & "  ptsfrd_qty, " _
            & "  ptsfrd_qty_receive, " _
            & "  ptsfrd_um, " _
            & "  ptsfrd_lot_serial, " _
            & "  ptsfrd_cost, " _
            & "  from_cmaddr_mstr.cmaddr_name as from_cmaddr_name, " _
            & "  from_cmaddr_mstr.cmaddr_line_1 as from_cmaddr_line_1, " _
            & "  from_cmaddr_mstr.cmaddr_line_2 as from_cmaddr_line_2, " _
            & "  from_cmaddr_mstr.cmaddr_line_3 as from_cmaddr_line_3, " _
            & "  ptnr_mstr.ptnr_name as to_cmaddr_name, " _
            & "  ptnra_addr.ptnra_line_1 as to_cmaddr_line_1, " _
            & "  ptnra_addr.ptnra_line_2 as to_cmaddr_line_2, " _
            & "  ptnra_addr.ptnra_line_3 as to_cmaddr_line_3, " _
            & "  from_loc_mstr.loc_desc as from_loc_desc, " _
            & "  to_loc_mstr.loc_desc as to_loc_desc, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_master.code_name as um_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  ptsfr_mstr " _
            & "  inner join ptsfrd_det on ptsfrd_ptsfr_oid = ptsfr_oid " _
            & "  inner join loc_mstr from_loc_mstr on from_loc_mstr.loc_id = ptsfr_loc_id " _
            & "  inner join loc_mstr to_loc_mstr on to_loc_mstr.loc_id = ptsfr_loc_to_id " _
            & "  left outer join cmaddr_mstr from_cmaddr_mstr on from_cmaddr_mstr.cmaddr_en_id = ptsfr_en_id " _
              & "  LEFT OUTER JOIN public.ptnr_mstr ON (to_loc_mstr.loc_ptnr_id = public.ptnr_mstr.ptnr_id) " _
            & "  LEFT OUTER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
            & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = ptsfrd_um" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = ptsfr_oid " _
            & "  where ptsfr_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRTransferIssuesPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        frm.ShowDialog()

    End Sub

    Public Overrides Sub approve_line()

    End Sub

    Public Overrides Sub cancel_line()

    End Sub

    Public Overrides Sub reminder_mail()

    End Sub

    Public Overrides Sub smart_approve()

    End Sub


    Private Sub ExportToPameranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LblMode_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblMode.DoubleClick
        If _mode = "" Then
            _mode = "."
            LblMode.Text = "."
        Else
            _mode = ""
            LblMode.Text = "-"
        End If
    End Sub

    Private Sub BtPrintBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
