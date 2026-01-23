Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FTransferIssuesReturn
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial As DataSet
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _so_oid, _pb_oid As String
    Dim _conf_value As String

    Private Sub FTransferIssues_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_requisition")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        ptsfr_loc_git.Enabled = False

        If _conf_value = "0" Then
            xtc_detail.TabPages(2).PageVisible = False
            xtc_detail.TabPages(4).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(2).PageVisible = True
            xtc_detail.TabPages(4).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'ptsfr_en_id.Properties.DataSource = dt_bantu
        'ptsfr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'ptsfr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'ptsfr_en_id.ItemIndex = 0
        init_le(ptsfr_en_id, "en_mstr")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran_global())
        'ptsfr_en_to_id.Properties.DataSource = dt_bantu
        'ptsfr_en_to_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'ptsfr_en_to_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'ptsfr_en_to_id.ItemIndex = 0

        init_le(ptsfr_en_to_id, "en_mstr")

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

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr(ptsfr_en_id.EditValue))
        'ptsfr_loc_id.Properties.DataSource = dt_bantu
        'ptsfr_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'ptsfr_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'ptsfr_loc_id.ItemIndex = 0

        init_le(ptsfr_loc_id, "loc_mstr", ptsfr_en_id.EditValue)

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr(ptsfr_en_id.EditValue))
        'ptsfr_loc_git.Properties.DataSource = dt_bantu
        'ptsfr_loc_git.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'ptsfr_loc_git.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'ptsfr_loc_git.ItemIndex = 0

        init_le(ptsfr_loc_git, "loc_mstr", ptsfr_en_id.EditValue)

        Dim ssql As String

        ssql = "select loc_id from loc_mstr where loc_en_id=" & SetInteger(ptsfr_en_id.EditValue) & " and loc_git='Y'"
        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)

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

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr(ptsfr_en_to_id.EditValue))
        'ptsfr_loc_to_id.Properties.DataSource = dt_bantu
        'ptsfr_loc_to_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'ptsfr_loc_to_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'ptsfr_loc_to_id.ItemIndex = 0

        init_le(ptsfr_loc_to_id, "loc_mstr", ptsfr_en_to_id.EditValue)
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transfer Issue Return Number", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Return Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ptsfr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "ptsfr_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Request Number", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptsfr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptsfr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptsfr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptsfr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "ptsfrd_ptsfr_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Kirim", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Receipt", "ptsfrd_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Return", "ptsfrd_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "ptsfrd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Lot Number", "ptsfrd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost", "ptsfrd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_serial, "ptsfrd_ptsfr_oid", False)
        add_column_copy(gv_detail_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "ptsfrds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "ptsfrds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_serial, "Qty Serial Receipt", "ptsfrds_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_serial, "Qty Serial Return", "ptsfrds_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "ptsfrd_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "ptsfrd_qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Return", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "ptsfrd_um", False)
        add_column(gv_edit, "UM", "ptsfrd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Lot Number", "ptsfrd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "ptsfrd_cost", False)
        add_column(gv_edit, "pbd_oid", False)

        add_column(gv_serial, "ptsfrds_oid", False)
        add_column(gv_serial, "ptsfrds_ptsfrd_oid", False)
        add_column_edit(gv_serial, "Lot/Serial Number", "ptsfrds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty", "ptsfrds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "ptsfrds_si_id", False)
        add_column(gv_serial, "ptsfrds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_code", False)

        add_column(gv_wf, "wf_ref_oid", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "ptsfr_oid", False)
        add_column(gv_email, "ptsfrd_ptsfr_oid", False)
        add_column_copy(gv_email, "Transfer Issue Number", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Transfer Issue Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "ptsfr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Status", "ptsfr_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "ptsfr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "ptsfr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "ptsfr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "ptsfr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty Kirim", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Qty Receipt", "ptsfrd_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Qty Return", "ptsfrd_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "ptsfrd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Lot Number", "ptsfrd_lot_serial", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ptsfr_oid, " _
                    & "  ptsfr_dom_id, " _
                    & "  ptsfr_add_by, " _
                    & "  ptsfr_add_date, " _
                    & "  ptsfr_upd_by, " _
                    & "  ptsfr_upd_date, " _
                    & "  ptsfr_code, " _
                    & "  ptsfr_date, " _
                    & "  ptsfr_receive_date, " _
                    & "  ptsfr_en_id, " _
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
                    & "  ptsfr_trans_id, " _
                    & "  so_code, " _
                    & "  pb_code,sq_code,ptsfr_sq_oid,ptsfr_pb_oid, " _
                    & "  ptsfr_dt " _
                    & "FROM  " _
                    & "  public.ptsfr_mstr " _
                    & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = ptsfr_en_id " _
                    & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = ptsfr_si_id " _
                    & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = ptsfr_loc_id " _
                    & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = ptsfr_loc_git " _
                    & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = ptsfr_en_to_id " _
                    & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = ptsfr_si_to_id " _
                    & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = ptsfr_loc_to_id " _
                    & "  left outer join so_mstr on so_oid = ptsfr_so_oid " _
                    & "  left outer join pb_mstr on pb_oid = ptsfr_pb_oid " _
                    & "  left outer join sq_mstr on sq_oid = ptsfr_sq_oid " _
                    & "  where coalesce(ptsfr_is_transfer,'Y')='N' and ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and ptsfr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  ptsfrd_oid, " _
            & "  ptsfrd_ptsfr_oid, " _
            & "  ptsfrd_seq, " _
            & "  ptsfrd_pt_id, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_ls, " _
            & "  ptsfrd_qty, " _
            & "  ptsfrd_qty_receive, " _
            & "  ptsfrd_qty - ptsfrd_qty_receive as ptsfrd_qty_return, " _
            & "  ptsfrd_um, " _
            & "  code_name as ptsfrd_um_name, " _
            & "  ptsfrd_lot_serial, " _
            & "  ptsfrd_cost, " _
            & "  ptsfrd_dt, " _
            & "  ptsfrd_pbd_oid,ptsfrd_sod_oid,ptsfrd_sqd_oid " _
            & "FROM  " _
            & "  public.ptsfrd_det" _
            & "  INNER JOIN public.ptsfr_mstr ON (public.ptsfrd_det.ptsfrd_ptsfr_oid = public.ptsfr_mstr.ptsfr_oid) " _
            & "  INNER JOIN public.pt_mstr ON (public.ptsfrd_det.ptsfrd_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.ptsfrd_det.ptsfrd_um = public.code_mstr.code_id) " _
            & "  where ptsfr_mstr.ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ptsfr_mstr.ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""
        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  ptsfrds_oid, " _
            & "  ptsfrds_ptsfrd_oid, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  ptsfrds_qty, " _
            & "  ptsfrds_qty_receive, " _
            & "  ptsfrds_qty - ptsfrds_qty_receive as ptsfrds_qty_return, " _
            & "  ptsfrds_si_id, " _
            & "  ptsfrds_loc_id, " _
            & "  ptsfrds_lot_serial, " _
            & "  ptsfrds_dt " _
            & "FROM  " _
            & "  public.ptsfrds_serial" _
            & "  inner join ptsfrd_det on ptsfrd_oid = ptsfrds_ptsfrd_oid " _
            & "  inner join ptsfr_mstr on ptsfr_oid = ptsfrd_ptsfr_oid " _
            & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
            & "  inner join si_mstr on si_id = ptsfrds_si_id " _
            & "  inner join loc_mstr on loc_id = ptsfrds_loc_id " _
            & "  where ptsfr_mstr.ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ptsfr_mstr.ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail_serial, "detail_serial")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join ptsfr_mstr on ptsfr_code = wf_ref_code " + _
                  " inner join ptsfrd_det dt on dt.ptsfrd_ptsfr_oid = ptsfr_oid " _
                & " where ptsfr_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and ptsfr_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
               & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                        & "  ptsfr_oid, " _
                        & "  ptsfr_dom_id, " _
                        & "  ptsfr_add_by, " _
                        & "  ptsfr_add_date, " _
                        & "  ptsfr_upd_by, " _
                        & "  ptsfr_upd_date, " _
                        & "  ptsfr_code, " _
                        & "  ptsfr_date, " _
                        & "  ptsfr_receive_date, " _
                        & "  ptsfr_en_id, " _
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
                        & "  ptsfr_trans_id, " _
                        & "  ptsfr_dt,   " _
                        & "  ptsfrd_oid, " _
                        & "  ptsfrd_ptsfr_oid, " _
                        & "  ptsfrd_seq, " _
                        & "  ptsfrd_pt_id, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_ls, " _
                        & "  ptsfrd_qty, " _
                        & "  ptsfrd_qty_receive, " _
                        & "  ptsfrd_qty - ptsfrd_qty_receive as ptsfrd_qty_return, " _
                        & "  ptsfrd_um, " _
                        & "  code_name as ptsfrd_um_name, " _
                        & "  ptsfrd_lot_serial, " _
                        & "  ptsfrd_cost, " _
                        & "  ptsfrd_dt " _
                        & "FROM  " _
                        & "  public.ptsfr_mstr " _
                        & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = ptsfr_en_id " _
                        & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = ptsfr_si_id " _
                        & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = ptsfr_loc_id " _
                        & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = ptsfr_loc_git " _
                        & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = ptsfr_en_to_id " _
                        & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = ptsfr_si_to_id " _
                        & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = ptsfr_loc_to_id " _
                        & "  INNER JOIN public.ptsfrd_det ON (public.ptsfrd_det.ptsfrd_ptsfr_oid = public.ptsfr_mstr.ptsfr_oid) " _
                        & "  INNER JOIN public.pt_mstr ON (public.ptsfrd_det.ptsfrd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.ptsfrd_det.ptsfrd_um = public.code_mstr.code_id) " _
                        & " where ptsfr_date >= " + SetDate(pr_txttglawal.DateTime) _
                        & " and ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        & " and ptsfr_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select ptsfr_oid, ptsfr_code, ptsfr_trans_id, false as status from ptsfr_mstr " _
                & " where ptsfr_trans_id ~~* 'd' "
            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("ptsfrd_ptsfr_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ptsfrd_ptsfr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_serial.Columns("ptsfrd_ptsfr_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ptsfrd_ptsfr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()

            'gv_detail.Columns("ptsfrd_ptsfr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid"))
            'gv_detail.BestFitColumns()

            'gv_detail_serial.Columns("ptsfrd_ptsfr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid"))
            'gv_detail_serial.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("ptsfr_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ptsfr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_wf.Columns("wf_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("ptsfr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid"))
                'gv_email.BestFitColumns()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        ptsfr_en_id.ItemIndex = 0
        ptsfr_en_to_id.ItemIndex = 0
        ptsfr_date.DateTime = _now
        ptsfr_remarks.Text = ""
        'ptsfr_en_id.Focus()
        ptsfr_date.Focus()
        ptsfr_sq_oid.Text = ""
        ptsfr_sq_oid.Enabled = True
        ptsfr_pb_oid.Text = ""
        ptsfr_pb_oid.Enabled = True
        ptsfr_tran_id.ItemIndex = 0

        gc_edit.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        _so_oid = ""
        _pb_oid = ""
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  ptsfrd_oid, " _
                            & "  ptsfrd_ptsfr_oid, " _
                            & "  ptsfrd_seq, " _
                            & "  ptsfrd_pt_id, " _
                            & "  pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  ptsfrd_qty, " _
                            & "  ptsfrd_qty_receive,0.0 as ptsfrd_qty_open,ptsfrd_sqd_oid, " _
                            & "  ptsfrd_um, " _
                            & "  code_name as ptsfrd_um_name, " _
                            & "  ptsfrd_lot_serial, " _
                            & "  ptsfrd_cost, " _
                            & "  ptsfrd_pbd_oid,ptsfrd_sod_oid  " _
                            & "FROM  " _
                            & "  public.ptsfrd_det" _
                            & "  INNER JOIN public.ptsfr_mstr ON (public.ptsfrd_det.ptsfrd_ptsfr_oid = public.ptsfr_mstr.ptsfr_oid) " _
                            & "  INNER JOIN public.pt_mstr ON (public.ptsfrd_det.ptsfrd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.ptsfrd_det.ptsfrd_um = public.code_mstr.code_id) " _
                            & "  where ptsfrd_pt_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_serial = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  ptsfrds_oid, " _
                        & "  ptsfrds_ptsfrd_oid, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  ptsfrds_qty, " _
                        & "  ptsfrds_qty_receive, " _
                        & "  ptsfrds_si_id, " _
                        & "  ptsfrds_loc_id, " _
                        & "  ptsfrds_lot_serial, " _
                        & "  ptsfrds_dt " _
                        & "FROM  " _
                        & "  public.ptsfrds_serial" _
                        & "  inner join ptsfrd_det on ptsfrd_oid = ptsfrds_ptsfrd_oid " _
                        & "  inner join ptsfr_mstr on ptsfr_oid = ptsfrd_ptsfr_oid " _
                        & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
                        & "  inner join si_mstr on si_id = ptsfrds_si_id " _
                        & "  inner join loc_mstr on loc_id = ptsfrds_loc_id " _
                        & " where pt_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_serial, "serial")
                    gc_serial.DataSource = ds_serial.Tables(0)
                    gv_serial.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim _ptsfr_trans_id As String = ""
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(ptsfr_trans_id,'') as ptsfr_trans_id from ptsfr_mstr where ptsfr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ptsfr_trans_id = .DataReader("ptsfr_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _ptsfr_trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Delete Closed Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim ssql As String
        ssql = "select count(*) as jml from ptsfrd_det where coalesce(ptsfrd_qty_receive,0) > 0 and ptsfrd_ptsfr_oid = '" _
                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'"

        If SetNumber(master_new.PGSqlConn.GetRowInfo(ssql)(0)) > 0 Then
            MessageBox.Show("Can't delete already receive transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

    End Function

    Public Overrides Function delete_data() As Boolean
        Dim ssqls As New ArrayList
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            Dim _serial, _pt_code, _ptsfr_code, _ptsfr_oid As String
            'Dim _cost_methode As String
            Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id, _qty, _row As Integer
            Dim _cost, _cost_avg As Double
            Dim i, i_2 As Integer

            row = BindingContext(ds.Tables(0)).Position
            _row = row

            _ptsfr_oid = ds.Tables(0).Rows(_row).Item("ptsfr_oid").ToString
            _ptsfr_code = ds.Tables(0).Rows(_row).Item("ptsfr_code")

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            '*********************************************************************************
                            'Proses Pengurangan untuk lokasi git
                            'Update Table Inventory Dan Cost Inventory Dan History Inventory
                            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                            i_2 = 0
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("ptsfrd_ptsfr_oid").ToString = _ptsfr_oid Then
                                    If ds.Tables("detail").Rows(i).Item("ptsfrd_qty") > 0 Then
                                        If ds.Tables("detail").Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                            i_2 += 1

                                            _en_id = ds.Tables(0).Rows(_row).Item("ptsfr_en_id")
                                            _si_id = ds.Tables(0).Rows(_row).Item("ptsfr_si_id")
                                            _loc_id = ds.Tables(0).Rows(_row).Item("ptsfr_loc_git")
                                            _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                                            _pt_code = ds.Tables("detail").Rows(i).Item("pt_code")
                                            _serial = "''"
                                            _qty = ds.Tables("detail").Rows(i).Item("ptsfrd_qty")
                                            If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If

                                            'Update History Inventory                                    
                                            _qty = _qty * -1
                                            _cost = ds.Tables("detail").Rows(i).Item("ptsfrd_cost")
                                            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                            '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                            For i = 0 To ds.Tables("detail_serial").Rows.Count - 1
                                If ds.Tables("detail_serial").Rows(i).Item("ptsfrd_ptsfr_oid").ToString = _ptsfr_oid Then
                                    If ds.Tables("detail_serial").Rows(i).Item("ptsfrds_qty") > 0 Then
                                        i_2 += 1

                                        _en_id = ds.Tables(0).Rows(_row).Item("ptsfr_en_id")
                                        _si_id = ds.Tables(0).Rows(_row).Item("ptsfr_si_id")
                                        _loc_id = ds.Tables(0).Rows(_row).Item("ptsfr_loc_git")
                                        _pt_id = ds.Tables("detail_serial").Rows(i).Item("pt_id")
                                        _pt_code = ds.Tables("detail_serial").Rows(i).Item("pt_code")
                                        _serial = ds.Tables("detail_serial").Rows(i).Item("ptsfrds_lot_serial")
                                        _qty = ds.Tables("detail_serial").Rows(i).Item("ptsfrds_qty")

                                        If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            delete_data = False
                                            Exit Function
                                        End If

                                        'Update History Inventory
                                        _cost = ds.Tables("detail_serial").Rows(i).Item("ptsfrds_cost")
                                        _cost = _cost * -1
                                        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                            sqlTran.Rollback()
                                            delete_data = False
                                            Exit Function
                                        End If
                                    End If
                                End If
                            Next

                            ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                            'For i = 0 To ds.Tables("detail").Rows.Count - 1
                            '    If ds.Tables("detail").Rows(i).Item("ptsfrd_ptsfr_oid") = _ptsfr_oid Then
                            '        _cost_methode = func_coll.get_pt_cost_method(ds.Tables("detail").Rows(i).Item("pt_id").ToString.ToUpper)
                            '        _en_id = ds.Tables(0).Rows(_row).Item("ptsfr_en_id")
                            '        _si_id = ds.Tables(0).Rows(_row).Item("ptsfr_si_id")
                            '        _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                            '        _qty = ds.Tables("detail").Rows(i).Item("ptsfrd_qty")
                            '        _cost = ds.Tables("detail").Rows(i).Item("ptsfrd_cost")
                            '        If _cost_methode = "F" Or _cost_methode = "L" Then
                            '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                            '            Return False
                            '            'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                            '            '    sqlTran.Rollback()
                            '            '    delete_data = False
                            '            '    Exit Function
                            '            'End If
                            '        ElseIf _cost_methode = "A" Then
                            '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                            '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                            '                sqlTran.Rollback()
                            '                delete_data = False
                            '                Exit Function
                            '            End If
                            '        End If
                            '    End If
                            'Next
                            '*****************************************************************************************

                            '*********************************************************************************
                            'Proses penambahan (+) untuk lokasi asal
                            'Update Table Inventory Dan Cost Inventory Dan History Inventory
                            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                            i_2 = 0
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("ptsfrd_ptsfr_oid").ToString = _ptsfr_oid Then
                                    If ds.Tables("detail").Rows(i).Item("ptsfrd_qty") > 0 Then
                                        If ds.Tables("detail").Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                            i_2 += 1

                                            _en_id = ds.Tables(0).Rows(_row).Item("ptsfr_en_id")
                                            _si_id = ds.Tables(0).Rows(_row).Item("ptsfr_si_id")
                                            _loc_id = ds.Tables(0).Rows(_row).Item("ptsfr_loc_id")
                                            _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                                            _pt_code = ds.Tables("detail").Rows(i).Item("pt_code")
                                            _serial = "''"
                                            _qty = ds.Tables("detail").Rows(i).Item("ptsfrd_qty")
                                            If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If

                                            'Update History Inventory                                    
                                            _cost = ds.Tables("detail").Rows(i).Item("ptsfrd_cost")
                                            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                            '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                            For i = 0 To ds.Tables("detail_serial").Rows.Count - 1
                                If ds.Tables("detail_serial").Rows(i).Item("ptsfrd_ptsfr_oid").ToString = _ptsfr_oid Then
                                    If ds.Tables("detail_serial").Rows(i).Item("ptsfrds_qty") > 0 Then
                                        i_2 += 1

                                        _en_id = ds.Tables(0).Rows(_row).Item("ptsfr_en_id")
                                        _si_id = ds.Tables(0).Rows(_row).Item("ptsfr_si_id")
                                        _loc_id = ds.Tables(0).Rows(_row).Item("ptsfr_loc_id")
                                        _pt_id = ds.Tables("detail_serial").Rows(i).Item("pt_id")
                                        _pt_code = ds.Tables("detail_serial").Rows(i).Item("pt_code")
                                        _serial = ds.Tables("detail_serial").Rows(i).Item("ptsfrds_lot_serial")
                                        _qty = ds.Tables("detail_serial").Rows(i).Item("ptsfrds_qty")
                                        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            delete_data = False
                                            Exit Function
                                        End If

                                        'Update History Inventory
                                        _cost = ds.Tables("detail_serial").Rows(i).Item("ptsfrds_cost")
                                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                            sqlTran.Rollback()
                                            delete_data = False
                                            Exit Function
                                        End If
                                    End If
                                End If
                            Next

                            ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                            'For i = 0 To ds.Tables("detail").Rows.Count - 1
                            '    If ds.Tables("detail").Rows(i).Item("ptsfrd_ptsfr_oid") = _ptsfr_oid Then
                            '        _cost_methode = func_coll.get_pt_cost_method(ds.Tables("detail").Rows(i).Item("pt_id").ToString.ToUpper)
                            '        _en_id = ds.Tables(0).Rows(_row).Item("ptsfr_en_id")
                            '        _si_id = ds.Tables(0).Rows(_row).Item("ptsfr_si_id")
                            '        _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                            '        _qty = ds.Tables("detail").Rows(i).Item("ptsfrd_qty")
                            '        _cost = ds.Tables("detail").Rows(i).Item("ptsfrd_cost")
                            '        If _cost_methode = "F" Or _cost_methode = "L" Then
                            '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                            '            Return False
                            '            'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                            '            '    sqlTran.Rollback()
                            '            '    delete_data = False
                            '            '    Exit Function
                            '            'End If
                            '        ElseIf _cost_methode = "A" Then
                            '            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                            '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                            '                sqlTran.Rollback()
                            '                delete_data = False
                            '                Exit Function
                            '            End If
                            '        End If
                            '    End If
                            'Next
                            '*****************************************************************************************

                            '********** update ke table pbd_det apabila ada..
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("ptsfrd_ptsfr_oid").ToString = _ptsfr_oid Then
                                    If SetString(ds.Tables("detail").Rows(i).Item("ptsfrd_pbd_oid").ToString) <> "" Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "UPDATE  " _
                                                            & "  public.pbd_det   " _
                                                            & "SET  " _
                                                            & "  pbd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                            & "  pbd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                            & "  pbd_qty_processed = coalesce(pbd_qty_processed,0) + " & SetDbl(ds.Tables("detail").Rows(i).Item("ptsfrd_qty")) & ",  " _
                                                            & "  pbd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                            & "  " _
                                                            & "WHERE  " _
                                                            & "  pbd_oid = " & SetSetring(ds.Tables("detail").Rows(i).Item("ptsfrd_pbd_oid").ToString) & " "
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                    If SetString(ds.Tables("detail").Rows(i).Item("ptsfrd_sqd_oid")) <> "" Then

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "UPDATE  " _
                                                            & "  public.sqd_det   " _
                                                            & "SET  " _
                                                            & "  sqd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                            & "  sqd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                            & "  sqd_qty_transfer = coalesce(sqd_qty_transfer,0) + " & SetDbl(ds.Tables("detail").Rows(i).Item("ptsfrd_qty")) & ",  " _
                                                            & "  sqd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                            & "  " _
                                                            & "WHERE  " _
                                                            & "  sqd_oid = " & SetSetring(ds.Tables("detail").Rows(i).Item("ptsfrd_sqd_oid").ToString) & " "
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                    End If
                                End If
                            Next

                            If Trim(ds.Tables(0).Rows(_row).Item("ptsfr_sq_oid").ToString) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sq_mstr set sq_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & ", sq_trans_id = 'C' " + _
                                                     " where coalesce((select count(sqd_sq_oid) as jml From sqd_det " + _
                                                     " where sqd_qty <> coalesce(sqd_qty_transfer,0) " + _
                                                     " and sqd_sq_oid = '" + ds.Tables(0).Rows(_row).Item("ptsfr_sq_oid").ToString + "'" + _
                                                     " group by sqd_sq_oid),0) = 0 " + _
                                                     " and sq_oid = '" + ds.Tables(0).Rows(_row).Item("ptsfr_sq_oid").ToString + "'"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            If Trim(ds.Tables(0).Rows(_row).Item("ptsfr_pb_oid").ToString) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pb_mstr set pb_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & ", pb_status = 'C' " + _
                                                     " where coalesce((select count(pbd_sq_oid) as jml From pbd_det " + _
                                                     " where pbd_qty <> coalesce(pbd_qty_processed,0) " + _
                                                     " and pbd_pb_oid = '" + ds.Tables(0).Rows(_row).Item("ptsfr_pb_oid").ToString + "'" + _
                                                     " group by pbd_pb_oid),0) = 0 " + _
                                                     " and pb_oid = '" + ds.Tables(0).Rows(_row).Item("ptsfr_pb_oid").ToString + "'"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If



                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ptsfr_mstr where ptsfr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Transfer Issue Return" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        gv_edit.UpdateCurrentRow()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = ptsfr_en_id.EditValue
            frm._si_id = ptsfr_si_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            gv_serial.Columns("ptsfrds_ptsfrd_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("ptsfrd_oid").ToString)
            gv_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "ptsfrd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "ptsfrd_qty", 0)
            .SetRowCellValue(e.RowHandle, "ptsfrd_cost", 0)
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        If ptsfr_loc_id.EditValue = 0 Then
            MessageBox.Show("Location Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        With gv_serial
            .SetRowCellValue(e.RowHandle, "ptsfrds_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "ptsfrds_ptsfrd_oid", ds_edit.Tables(0).Rows(_row_edit).Item("ptsfrd_oid").ToString)
            .SetRowCellValue(e.RowHandle, "ptsfrds_qty", 1)
            .SetRowCellValue(e.RowHandle, "ptsfrds_si_id", ptsfr_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "ptsfrds_loc_id", ptsfr_loc_id.EditValue)
            .SetRowCellValue(e.RowHandle, "pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_id"))
            .SetRowCellValue(e.RowHandle, "pt_code", ds_edit.Tables(0).Rows(_row_edit).Item("pt_code"))
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        gv_serial.UpdateCurrentRow()

        ds_edit.AcceptChanges()
        ds_serial.AcceptChanges()

        Dim _date As Date = ptsfr_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(ptsfr_en_to_id.EditValue, "gcald_ic", _date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i, j As Integer
        Dim _qty, _qty_ttl_serial As Double

        '*********************
        'Cek part number, Location, site , account
        If ptsfr_loc_id.EditValue = 0 Then
            MessageBox.Show("Location From Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ptsfr_loc_id.Focus()
            Return False
        ElseIf ptsfr_loc_git.EditValue = 0 Then
            MessageBox.Show("Location GIT Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ptsfr_loc_git.Focus()
            Return False
        ElseIf ptsfr_loc_to_id.EditValue = 0 Then
            MessageBox.Show("Location To Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ptsfr_loc_to_id.Focus()
            Return False
        ElseIf ptsfr_si_id.EditValue = 0 Then
            MessageBox.Show("Site From Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ptsfr_si_id.Focus()
            Return False
        ElseIf ptsfr_si_to_id.EditValue = 0 Then
            MessageBox.Show("Site To Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ptsfr_si_to_id.Focus()
            Return False
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pt_id")) = True Then
                MessageBox.Show("Part Number Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next
        '*********************

        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai qty lebih dari 1
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("ptsfrd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("ptsfrds_ptsfrd_oid").ToString Then
                        If ds_serial.Tables(0).Rows(j).Item("ptsfrds_qty") > 1 Then
                            MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Qty Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            BindingContext(ds_edit.Tables(0)).Position = i
                            Return False
                        End If
                    End If
                Next
            End If
        Next
        '***********************************************************************

        '***********************************************************************
        'Mencari apakah receive barang yang Serial mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
                _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("ptsfrd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("ptsfrds_ptsfrd_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + 1
                    End If
                Next
                If _qty <> _qty_ttl_serial Then
                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '***********************************************************************

        '***********************************************************************
        'Mencari apakah receive barang yang Lot mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "L" Then
                _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")
                _qty_ttl_serial = 0
                For j = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("ptsfrd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("ptsfrds_ptsfrd_oid").ToString Then
                        _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("ptsfrds_qty")
                    End If
                Next
                If _qty <> _qty_ttl_serial Then
                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Lot Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '***********************************************************************

        'cek inventory allocation nya
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If func_coll.cek_inventory_allocation(ptsfr_en_id.EditValue, ptsfr_si_id.EditValue, _
        '                                        ptsfr_loc_id.EditValue, ds_edit.Tables(0).Rows(i).Item("pt_id"), _
        '                                        ds_edit.Tables(0).Rows(i).Item("pt_code"), ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty"), ds_edit.Tables(0).Rows(i).Item("ptsfrd_lot_serial")) = False Then
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ptsfr_oid As Guid = Guid.NewGuid

        Dim _serial, _pt_code, _ptsfr_code As String
        'Dim _cost_methode As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList
        Dim _ptsfr_trn_satatus As String
        Dim _ptsfr_trn_id As Integer
        Dim ds_bantu As New DataSet

        _ptsfr_code = func_coll.get_transaction_number("TR", ptsfr_en_id.GetColumnValue("en_code"), "ptsfr_mstr", "ptsfr_code")

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(ptsfr_tran_id.EditValue)
        End If

        _ptsfr_trn_id = ptsfr_tran_id.EditValue
        _ptsfr_trn_satatus = "D" 'Lansung Default Ke Draft

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
                                            & "  public.ptsfr_mstr " _
                                            & "( " _
                                            & "  ptsfr_oid, " _
                                            & "  ptsfr_dom_id, " _
                                            & "  ptsfr_en_id, " _
                                            & "  ptsfr_add_by, " _
                                            & "  ptsfr_add_date, " _
                                            & "  ptsfr_en_to_id, " _
                                            & "  ptsfr_code, " _
                                            & "  ptsfr_date, " _
                                            & "  ptsfr_si_id, " _
                                            & "  ptsfr_loc_id, " _
                                            & "  ptsfr_loc_git, " _
                                            & "  ptsfr_remarks, " _
                                            & "  ptsfr_dt, " _
                                            & "  ptsfr_loc_to_id, " _
                                            & "  ptsfr_tran_id, " _
                                            & "  ptsfr_trans_id, " _
                                            & "  ptsfr_si_to_id, " _
                                            & "  ptsfr_so_oid,ptsfr_is_transfer,ptsfr_sq_oid, " _
                                            & "  ptsfr_pb_oid " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptsfr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ptsfr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(ptsfr_en_to_id.EditValue) & ",  " _
                                            & SetSetring(_ptsfr_code) & ",  " _
                                            & SetDate(ptsfr_date.DateTime) & ",  " _
                                            & SetInteger(ptsfr_si_id.EditValue) & ",  " _
                                            & SetInteger(ptsfr_loc_id.EditValue) & ",  " _
                                            & SetSetring(ptsfr_loc_git.EditValue) & ",  " _
                                            & SetSetring(ptsfr_remarks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(ptsfr_loc_to_id.EditValue) & ",  " _
                                            & SetInteger(ptsfr_tran_id.EditValue) & ",  " _
                                            & SetSetring(_ptsfr_trn_satatus) & ",  " _
                                            & SetInteger(ptsfr_si_to_id.EditValue) & ",  " _
                                            & SetSetring(_so_oid) & ",'N',  " _
                                            & SetSetring(ptsfr_sq_oid.Tag) & ",  " _
                                            & SetSetring(_pb_oid) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") > 0 Then
                                'If Trim(ptsfr_pb_oid.Text) = "" Then
                                '    _ptsfrd_pbd_oid = "null"
                                'Else
                                '    _ptsfrd_pbd_oid = SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pbd_oid").ToString)
                                'End If


                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.ptsfrd_det " _
                                                    & "( " _
                                                    & "  ptsfrd_oid, " _
                                                    & "  ptsfrd_ptsfr_oid, " _
                                                    & "  ptsfrd_seq, " _
                                                    & "  ptsfrd_pt_id, " _
                                                    & "  ptsfrd_qty, " _
                                                    & "  ptsfrd_um, " _
                                                    & "  ptsfrd_lot_serial, " _
                                                    & "  ptsfrd_cost, " _
                                                    & "  ptsfrd_dt, " _
                                                    & "  ptsfrd_pbd_oid,ptsfrd_sqd_oid " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_oid").ToString) & ",  " _
                                                    & SetSetring(_ptsfr_oid.ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pt_id")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("ptsfrd_um")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("ptsfrd_lot_serial")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pbd_oid").ToString) & ", " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_sqd_oid").ToString) & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If Trim(ptsfr_pb_oid.Text) <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE  " _
                                                        & "  public.pbd_det   " _
                                                        & "SET  " _
                                                        & "  pbd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "  pbd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                        & "  pbd_qty_processed = coalesce(pbd_qty_processed,0) - " & SetDbl(ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")) & ",  " _
                                                        & "  pbd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                        & "  " _
                                                        & "WHERE  " _
                                                        & "  pbd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pbd_oid").ToString) & " "
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                                If ptsfr_sq_oid.Text <> "" Then

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE  " _
                                                        & "  public.sqd_det   " _
                                                        & "SET  " _
                                                        & "  sqd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "  sqd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                        & "  sqd_qty_transfer = coalesce(sqd_qty_transfer,0) - " & SetDbl(ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")) & ",  " _
                                                        & "  sqd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                        & "  " _
                                                        & "WHERE  " _
                                                        & "  sqd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_sqd_oid").ToString) & " "
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If
                            End If
                        Next

                        If ptsfr_sq_oid.Text <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update sq_mstr set sq_close_date = null, sq_trans_id = 'D' " + _
                                                 " where  sq_oid = " + SetSetring(ptsfr_sq_oid.Tag) + ""
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        If ptsfr_pb_oid.Text <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update pb_mstr set pb_close_date = null, pb_status = 'D' " + _
                                                   " where  pb_oid = " + SetSetring(_pb_oid) + ""
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        'Untuk Update data serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptsfrds_serial " _
                                                & "( " _
                                                & "  ptsfrds_oid, " _
                                                & "  ptsfrds_ptsfrd_oid, " _
                                                & "  ptsfrds_qty, " _
                                                & "  ptsfrds_si_id, " _
                                                & "  ptsfrds_loc_id, " _
                                                & "  ptsfrds_lot_serial, " _
                                                & "  ptsfrds_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("ptsfrds_oid").ToString) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("ptsfrds_ptsfrd_oid").ToString) & ",  " _
                                                & SetDbl(ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("ptsfrds_si_id")) & ",  " _
                                                & SetInteger(ds_serial.Tables(0).Rows(i).Item("ptsfrds_loc_id")) & ",  " _
                                                & SetSetring(ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        '*********************************************************************************
                        'Proses Pengurangan untuk lokasi asal
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = ptsfr_en_id.EditValue
                                    _si_id = ptsfr_si_id.EditValue
                                    _loc_id = ptsfr_loc_id.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _qty = _qty * -1
                                    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty") > 0 Then
                                i_2 += 1

                                _en_id = ptsfr_en_id.EditValue
                                _si_id = ptsfr_si_id.EditValue
                                _loc_id = ptsfr_loc_id.EditValue
                                _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                                _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                                _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                                _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty")
                                If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                'Update History Inventory
                                _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                                _cost = _cost * -1
                                _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If
                            End If
                        Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '    _en_id = ptsfr_en_id.EditValue
                        '    _si_id = ptsfr_si_id.EditValue
                        '    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")
                        '    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                        '    If _cost_methode = "F" Or _cost_methode = "L" Then
                        '        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '        Return False
                        '        'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '        '    sqlTran.Rollback()
                        '        '    insert = False
                        '        '    Exit Function
                        '        'End If
                        '    ElseIf _cost_methode = "A" Then
                        '        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '            sqlTran.Rollback()
                        '            insert = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        '*********************************************************************************
                        'Proses penambahan (+) untuk lokasi git
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = ptsfr_en_id.EditValue
                                    _si_id = ptsfr_si_id.EditValue
                                    _loc_id = ptsfr_loc_git.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty") > 0 Then
                                i_2 += 1

                                _en_id = ptsfr_en_id.EditValue
                                _si_id = ptsfr_si_id.EditValue
                                _loc_id = ptsfr_loc_git.EditValue
                                _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                                _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                                _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                                _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty")
                                If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                'Update History Inventory
                                _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                                _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_date.DateTime) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If
                            End If
                        Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '    _en_id = ptsfr_en_id.EditValue
                        '    _si_id = ptsfr_si_id.EditValue
                        '    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty")
                        '    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                        '    If _cost_methode = "F" Or _cost_methode = "L" Then
                        '        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '        Return False
                        '        'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '        '    'If func_coll.update_invct_table_minus(objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '        '    sqlTran.Rollback()
                        '        '    insert = False
                        '        '    Exit Function
                        '        'End If
                        '    ElseIf _cost_methode = "A" Then
                        '        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '            sqlTran.Rollback()
                        '            insert = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************
                        If _conf_value = "1" Then
                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(ptsfr_en_id.EditValue) & ",  " _
                                                        & SetSetring(ptsfr_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_ptsfr_oid.ToString) & ",  " _
                                                        & SetSetring(_ptsfr_code) & ",  " _
                                                        & SetSetring("Inventory Transfer Issue") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If



                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                        '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                        '                         _rcv_oid.ToString, _rcv_code, _
                        '                         func_coll.get_tanggal_sistem, _
                        '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                        '                         "IC", "IC-RPO") = False Then
                        '    sqlTran.Rollback()
                        '    insert = False
                        '    Exit Function
                        'End If

                        'If Trim(_so_oid.ToString) <> "" Then
                        '    If func_coll.insert_tranaprvd_det(ssqls, objinsert, ptsfr_en_id.EditValue, 14, _so_oid.ToString, ptsfr_so_oid.Text, _now.Date) = False Then
                        '        'sqlTran.Rollback()
                        '        'insert = False
                        '        'Exit Function
                        '    End If
                        'Else
                        '    If func_coll.insert_tranaprvd_det(ssqls, objinsert, ptsfr_en_id.EditValue, 8, _ptsfr_oid.ToString, _ptsfr_code, _now.Date) = False Then
                        '        'sqlTran.Rollback()
                        '        'insert = False
                        '        'Exit Function
                        '    End If
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Transfer Issue Return" & _ptsfr_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(_ptsfr_oid.ToString, "ptsfr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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

    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub

    Private Sub ptsfr_so_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptsfr_sq_oid.ButtonClick
        'Dim frm As New FSalesOrderSearch
        'frm.set_win(Me)
        'frm._en_id = ptsfr_en_id.EditValue
        'frm._loc_id = ptsfr_loc_id.EditValue
        'frm.type_form = True
        'frm.ShowDialog()

        Dim frm As New FSalesQuotationSearch
        frm.set_win(Me)
        frm._en_id = ptsfr_en_id.EditValue
        frm._obj = ptsfr_sq_oid
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub ptsfr_pb_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptsfr_pb_oid.ButtonClick
        Dim frm As New FInventoryRequestSearch
        frm.set_win(Me)
        frm._en_id = ptsfr_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
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

        'Dim ds_bantu As New DataSet
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
            & "  to_cmaddr_mstr.cmaddr_name as to_cmaddr_name, " _
            & "  to_cmaddr_mstr.cmaddr_line_1 as to_cmaddr_line_1, " _
            & "  to_cmaddr_mstr.cmaddr_line_2 as to_cmaddr_line_2, " _
            & "  to_cmaddr_mstr.cmaddr_line_3 as to_cmaddr_line_3, " _
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
            & "  left outer join cmaddr_mstr to_cmaddr_mstr on to_cmaddr_mstr.cmaddr_en_id = ptsfr_en_to_id " _
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
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString
        _colom = "ptsfr_trans_id"
        _table = "ptsfr_mstr"
        _criteria = "ptsfr_code"
        _initial = "ptsfr"
        _type = "ti"
        _title = "Inventory Transfer Issue"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString
        _colom = "ptsfr_trans_id"
        _table = "ptsfr_mstr"
        _criteria = "ptsfr_code"
        _initial = "ptsfr"
        _type = "ti"
        '_title = "Inventory Transfer Issue"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub reminder_mail()
        Dim _code, _type, _user, _title As String

        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        _type = "ti"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Inventory Transfer Issue"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu, _title As String
        Dim i As Integer

        _title = "Inventory Transfer Issue"

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("ptsfr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("ptsfr_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("ptsfr_code"), 0)
                user_wf_email = mf.get_email_address(user_wf)

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update ptsfr_mstr set ptsfr_trans_id = '" + _trans_id + "'," + _
                                               " ptsfr_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " ptsfr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where ptsfr_oid = '" + ds.Tables("smart").Rows(i).Item("ptsfr_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("ptsfr_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("ptsfr_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("ptsfr_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email(_title, ds.Tables("smart").Rows(i).Item("ptsfr_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

                            Catch ex As nPgSqlException
                                sqlTran.Rollback()
                                MessageBox.Show(ex.Message)
                            End Try
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
