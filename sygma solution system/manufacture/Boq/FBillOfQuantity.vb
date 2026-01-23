Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FBillOfQuantity

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim ds_edit As DataSet
    Dim ds_update_related As DataSet
    Dim _conf_value As String
    Dim _now As Date
    Dim _boq_code As String
    Dim _boq_oid_mstr As String
    Dim dt_detail As New DataTable
    Dim dt_stand As New DataTable
    Dim dt_edit As New DataTable
    Dim dt_stand_edit As New DataTable

  
    Private Sub FRequisition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_bill_off_quantity")
        form_first_load()
        _now = func_coll.get_tanggal_sistem
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        If _conf_value = "0" Then
            xtc_detail.TabPages(2).PageVisible = False
            'xtc_detail.TabPages(4).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(2).PageVisible = True
            'xtc_detail.TabPages(4).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        boq_en_id.Properties.DataSource = dt_bantu
        boq_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        boq_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        boq_en_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            boq_tran_id.Properties.DataSource = dt_bantu
            boq_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            boq_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            boq_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            boq_tran_id.Properties.DataSource = dt_bantu
            boq_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            boq_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            boq_tran_id.ItemIndex = 0
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "boq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "boq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "boq_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "boq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "boq_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "boq_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "boq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "boq_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "boqd_oid", False)
        add_column(gv_detail, "boqd_boq_oid", False)
        add_column(gv_detail, "sopjd_um", False)
        add_column_copy(gv_detail, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Pt Desc 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Plan", "boqd_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty", "boqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty WO", "boqd_qty_wo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty WO Receive", "boqd_qty_wor", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Unit Measure", "code_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_stand, "boqs_oid", False)
        add_column(gv_stand, "boqs_boq_oid", False)
        add_column_copy(gv_stand, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_stand, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_stand, "Pt Desc 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_stand, "Qty Plan", "boqs_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty", "boqs_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty Relocation", "boqs_qty_relocation", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty PR", "boqs_qty_pr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty PO", "boqs_qty_po", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty Receive", "boqs_qty_receipt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty Return", "boqs_qty_return", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Qty WO", "boqs_qty_wo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_stand, "Unit Measure", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_stand, "Is Manual", "boqs_is_manual", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_stand, "Indirect", "boqs_indirect", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_stand, "Pt Code Parent", "boqs_pt_code_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand, "Pt Desc 1 Parent", "boqs_pt_desc1_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand, "Pt Desc 2 Parent", "boqs_pt_desc2_parent", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "boqd_oid", False)
        add_column(gv_edit, "boqd_boq_oid", False)
        add_column(gv_edit, "boqd_sopjd_oid", False)
        add_column(gv_edit, "boqd_um", False)
        add_column(gv_edit, "pt_pm_code", False)
        add_column(gv_edit, "boqd_pt_id", False)
        add_column_copy(gv_edit, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Pt Desc 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        If _conf_value = "0" Then
            add_column_copy(gv_edit, "Qty", "boqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        Else
            add_column_copy(gv_edit, "Qty", "boqd_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If


        add_column_copy(gv_edit, "Unit Measure", "code_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_stand_edit, "boqs_oid", False)
        add_column(gv_stand_edit, "boqs_boq_oid", False)
        add_column(gv_stand_edit, "boqs_pt_id", False)
        add_column(gv_stand_edit, "pt_um", False)
        add_column(gv_stand_edit, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Pt Desc 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        If _conf_value = "0" Then
            add_column_edit(gv_stand_edit, "Qty", "boqs_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        Else
            add_column_edit(gv_stand_edit, "Qty", "boqs_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If


        add_column(gv_stand_edit, "Unit Measure", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Is Manual", "boqs_is_manual", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Indirect", "psd_indirect", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Pt Code Parent", "pt_code_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Pt Desc 1 Parent", "pt_desc1_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_stand_edit, "Pt Desc 2 Parent", "pt_desc2_parent", DevExpress.Utils.HorzAlignment.Default)

        'psd_indirect

        '--------------------------

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "boq_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "boq_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Code", "boq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date", "boq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Remark", "boq_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Transaction", "tran_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "boq_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "boq_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "User Update", "boq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "boq_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_email, "Part Number Standar", "pt_code_boqs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1 Standar", "pt_desc1_boqs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2 Standar", "pt_desc2_boqs", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty Standar", "boqs_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Unit Measure Standar", "unit_measure_boqs", DevExpress.Utils.HorzAlignment.Default)


    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.boq_oid, " _
                & "  a.boq_dom_id, " _
                & "  a.boq_en_id, " _
                & "  b.en_desc, " _
                & "  a.boq_add_by, " _
                & "  a.boq_add_date, " _
                & "  a.boq_upd_by, " _
                & "  a.boq_upd_date, " _
                & "  a.boq_dt, " _
                & "  a.boq_sopj_oid, " _
                & "  c.so_code, " _
                & "  a.boq_code, " _
                & "  a.boq_date, " _
                & "  a.boq_remark, " _
                & "  d.tran_name, " _
                & "  a.boq_trans_id,boq_tran_id " _
                & "FROM " _
                & "  public.boq_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.boq_en_id = b.en_id) " _
                & "  INNER JOIN public.so_mstr c ON (a.boq_sopj_oid = c.so_oid) " _
                & "  LEFT OUTER JOIN public.tran_mstr d ON (a.boq_tran_id = d.tran_id) " _
                & "WHERE " _
                & "  a.boq_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
                & " and boq_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "ORDER BY " _
                & "  a.boq_code"

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
            & "  a.boqd_oid, " _
            & "  a.boqd_seq, " _
            & "  a.boqd_boq_oid, " _
            & "  a.boqd_sopjd_oid, " _
            & "  a.boqd_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.boqd_qty_plan, " _
            & "  a.boqd_qty, " _
            & "  a.boqd_um, " _
            & "  c.code_code, " _
            & "  a.boqd_qty_wo, " _
            & "  a.boqd_qty_wor " _
            & "FROM " _
            & "  public.boqd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.boqd_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (a.boqd_um = c.code_id) " _
            & "  INNER JOIN public.boq_mstr d ON (a.boqd_boq_oid = d.boq_oid) " _
            & "WHERE " _
            & "  d.boq_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
            & " and d.boq_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.boqd_seq"


        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("stand").Clear()
        Catch ex As Exception
        End Try


        sql = "SELECT  " _
            & "  a.boqs_oid, " _
            & "  a.boqs_boq_oid, " _
            & "  a.boqs_pt_id, " _
            & "  b.pt_code,boqs_pt_id_parent,boqs_pt_code_parent,boqs_pt_desc1_parent,boqs_pt_desc2_parent,boqs_indirect," _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  b.pt_um, " _
            & "  c.code_code, " _
            & "  a.boqs_qty_plan, " _
            & "  a.boqs_qty, " _
            & "  coalesce(a.boqs_qty_relocation,0) as boqs_qty_relocation, " _
            & "  a.boqs_qty_pr, " _
            & "  a.boqs_qty_po, " _
            & "  a.boqs_qty_receipt,boqs_qty_return, " _
            & "  a.boqs_qty_wo, " _
            & "  a.boqs_is_manual " _
            & "FROM " _
            & "  public.boqs_stand a " _
            & "  INNER JOIN public.boq_mstr d ON (a.boqs_boq_oid = d.boq_oid) " _
            & "  INNER JOIN public.pt_mstr b ON (a.boqs_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
            & "WHERE " _
            & "  d.boq_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
            & " and d.boq_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.boqs_seq"


        load_data_detail(sql, gc_stand, "stand")

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
                  " inner join boq_mstr on boq_code = wf_ref_code " + _
                 " where boq_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and boq_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and boq_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " " _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  a.boq_en_id, " _
                    & "  c.en_desc, " _
                    & "  a.boq_add_by, " _
                    & "  a.boq_add_date, " _
                    & "  a.boq_upd_by, " _
                    & "  a.boq_upd_date, " _
                    & "  d.prj_code, " _
                    & "  a.boq_code, " _
                    & "  a.boq_remark, " _
                    & "  b.tran_desc, " _
                    & "  i.pt_code AS pt_code_boqs, " _
                    & "  i.pt_desc1 AS pt_desc1_boqs, " _
                    & "  i.pt_desc2 AS pt_desc2_boqs, " _
                    & "  j.code_code AS unit_measure_boqs, " _
                    & "  h.boqs_pt_id, " _
                    & "  h.boqs_qty, " _
                    & "  h.boqs_seq, " _
                    & "  a.boq_oid, " _
                    & "  a.boq_date " _
                    & "FROM " _
                    & "  public.boq_mstr a " _
                    & "  INNER JOIN public.prj_mstr d ON (a.boq_sopj_oid = d.prj_oid) " _
                    & "  INNER JOIN public.en_mstr c ON (a.boq_en_id = c.en_id) " _
                    & "  INNER JOIN public.tran_mstr b ON (a.boq_tran_id = b.tran_id) " _
                    & "  INNER JOIN public.boqs_stand h ON (a.boq_oid = h.boqs_boq_oid) " _
                    & "  INNER JOIN public.pt_mstr i ON (i.pt_id = h.boqs_pt_id) " _
                    & "  INNER JOIN public.code_mstr j ON (i.pt_um = j.code_id) " _
                    & " where boq_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and boq_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and boq_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " order by boq_code, boqs_seq "


            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select boq_oid, boq_code, boq_trans_id, false as status from boq_mstr " _
                & " where boq_trans_id ~~* 'd' and boq_add_by ~~* '" + master_new.ClsVar.sNama + "'"

            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("boqd_boq_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[boqd_boq_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_stand.Columns("boqs_boq_oid").FilterInfo = _
                       New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[boqs_boq_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_oid").ToString & "'")
            gv_stand.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("boq_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[boq_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_oid").ToString & "'")
                gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"
    Public Overrides Sub load_cb_en()
       
    End Sub

    Private Sub req_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles boq_en_id.EditValueChanged
        load_cb_en()
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        boq_en_id.Focus()
        boq_en_id.ItemIndex = 0
        boq_date.DateTime = _now
        boq_remark.Text = ""
        boq_tran_id.ItemIndex = 0
        boq_sopj_oid.Text = ""
        boq_sopj_oid.Tag = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        gc_edit.DataSource = Nothing
        gc_stand_edit.DataSource = Nothing
        dt_stand.Clear()
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_stand_edit.UpdateCurrentRow()
        gc_stand_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

        If gv_edit.RowCount = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If gv_stand_edit.RowCount = 0 Then
            MessageBox.Show("Data Material Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If cek_duplikat_pt_id(gv_stand_edit, "boqs_pt_id", "boqs_is_manual") = False Then
            before_save = False
        End If


        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _oid_mstr As String
        _oid_mstr = Guid.NewGuid.ToString

        Dim _boq_code As String
        Dim _boq_tran_id As Integer
        Dim _boq_tran_status As String
        Dim ds_bantu As New DataSet

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(boq_tran_id.EditValue)
        End If
        '=============================================================================

        _boq_tran_id = boq_tran_id.EditValue
        _boq_tran_status = "D" 'Lansung Default Ke Draft

        _boq_code = func_coll.get_transaction_number("BQ", boq_en_id.GetColumnValue("en_code"), "boq_mstr", "boq_code", boq_date.DateTime)

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
                                            & "  public.boq_mstr " _
                                            & "( " _
                                            & "  boq_oid, " _
                                            & "  boq_dom_id, " _
                                            & "  boq_en_id, " _
                                            & "  boq_add_by, " _
                                            & "  boq_add_date, " _
                                            & "  boq_dt, " _
                                            & "  boq_sopj_oid, " _
                                            & "  boq_code, " _
                                            & "  boq_date, " _
                                            & "  boq_remark, " _
                                            & "  boq_trans_id, " _
                                            & "  boq_tran_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_oid_mstr) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(boq_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(boq_sopj_oid.Tag.ToString) & ",  " _
                                            & SetSetring(_boq_code) & ",  " _
                                            & SetDate(boq_date.DateTime) & ",  " _
                                            & SetSetring(boq_remark.Text) & ",  " _
                                            & SetSetring(_boq_tran_status) & ",  " _
                                            & SetInteger(boq_tran_id.EditValue) & "  " _
                                            & ")"


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i As Integer = 0 To gv_edit.RowCount - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.boqd_det " _
                                            & "( " _
                                            & "  boqd_oid, " _
                                            & "  boqd_add_by, " _
                                            & "  boqd_add_date, " _
                                            & "  boqd_dt, " _
                                            & "  boqd_seq, " _
                                            & "  boqd_boq_oid, " _
                                            & "  boqd_sopjd_oid, " _
                                            & "  boqd_pt_id, " _
                                            & "  boqd_qty_plan, " _
                                            & "  boqd_qty, " _
                                            & "  boqd_um, " _
                                            & "  boqd_qty_wo, " _
                                            & "  boqd_qty_wor " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetSetring(_oid_mstr) & ",  " _
                                            & SetSetring(gv_edit.GetRowCellValue(i, "boqd_sopjd_oid")) & ",  " _
                                            & SetInteger(gv_edit.GetRowCellValue(i, "boqd_pt_id")) & ",  " _
                                            & SetDbl(gv_edit.GetRowCellValue(i, "boqd_qty_plan")) & ",  " _
                                            & SetDbl(gv_edit.GetRowCellValue(i, "boqd_qty")) & ",  " _
                                            & SetInteger(gv_edit.GetRowCellValue(i, "boqd_um")) & ",  " _
                                            & SetDbl(0) & ", " _
                                            & SetDbl(0) & " " _
                                            & ")"


                          

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next
                       
                        For i As Integer = 0 To gv_stand_edit.RowCount - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.boqs_stand " _
                                            & "( " _
                                            & "  boqs_oid, " _
                                            & "  boqs_add_by, " _
                                            & "  boqs_add_date, " _
                                            & "  boqs_dt, " _
                                            & "  boqs_boq_oid, " _
                                            & "  boqs_seq, " _
                                            & "  boqs_pt_id, " _
                                            & "  boqs_qty_plan, " _
                                            & "  boqs_qty, " _
                                            & "  boqs_qty_pr, " _
                                            & "  boqs_qty_po, " _
                                            & "  boqs_qty_receipt, " _
                                            & "  boqs_qty_wo,boqs_indirect,boqs_pt_id_parent,boqs_pt_code_parent,boqs_pt_desc1_parent,boqs_pt_desc2_parent,  " _
                                            & "  boqs_is_manual " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_oid_mstr) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(gv_stand_edit.GetRowCellValue(i, "boqs_pt_id")) & ",  " _
                                            & SetDbl(gv_stand_edit.GetRowCellValue(i, "boqs_qty_plan")) & ",  " _
                                            & SetDbl(gv_stand_edit.GetRowCellValue(i, "boqs_qty")) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
                                              & SetSetring(gv_stand_edit.GetRowCellValue(i, "psd_indirect")) & ",  " _
                                                & SetInteger(gv_stand_edit.GetRowCellValue(i, "pt_id_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "pt_code_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "pt_desc1_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "pt_desc2_parent")) & ",  " _
                                            & SetSetring(gv_stand_edit.GetRowCellValue(i, "boqs_is_manual")) & "  " _
                                            & ")"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next
                        

                        If _conf_value = "1" Then
                            For j As Integer = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                        & SetInteger(boq_en_id.EditValue) & ",  " _
                                                        & SetSetring(boq_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_oid_mstr) & ",  " _
                                                        & SetSetring(_boq_code) & ",  " _
                                                        & SetSetring("Bill Of Quantity") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        set_row(_oid_mstr.ToString, "boq_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Function edit_data() As Boolean
        Dim func_coll As New function_collection
        Try

      
            If _conf_value = "0" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            ElseIf _conf_value = "1" Then
                If Not (ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_trans_id") = "D" Or ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_trans_id") = "I") Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        If MyBase.edit_data = True Then


            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                boq_en_id.Focus()
                _boq_oid_mstr = .Item("boq_oid").ToString
                boq_en_id.EditValue = .Item("boq_en_id")
                boq_date.DateTime = .Item("boq_date")
                boq_remark.Text = SetString(.Item("boq_remark"))
                boq_tran_id.EditValue = .Item("boq_tran_id")
                boq_sopj_oid.Text = master_new.PGSqlConn.GetRowInfo("select prj_code from prj_mstr where prj_oid=" & SetSetring(.Item("boq_sopj_oid")))(0)
                boq_sopj_oid.Tag = .Item("boq_sopj_oid").ToString
                _boq_code = .Item("boq_code")
            End With

            gc_stand_edit.EmbeddedNavigator.Buttons.Append.Visible = True
            gc_stand_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
            gc_stand_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
            gv_stand_edit.OptionsBehavior.Editable = True
            boq_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            boq_tran_id.Enabled = True
            ds_edit = New DataSet
            ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  a.boqd_oid, " _
                                & "  a.boqd_seq, " _
                                & "  a.boqd_boq_oid, " _
                                & "  a.boqd_sopjd_oid, " _
                                & "  a.boqd_pt_id, " _
                                & "  b.pt_code, " _
                                & "  b.pt_desc1, " _
                                & "  b.pt_desc2, "

                        If ds.Tables(0).Rows(row).Item("boq_trans_id") = "I" Then
                            .SQL += "  a.boqd_qty as boqd_qty_plan, "
                        Else
                            .SQL += "  a.boqd_qty_plan, "
                        End If

                        .SQL += "  a.boqd_qty, " _
                                & "  a.boqd_um, " _
                                & "  c.code_code, " _
                                & "  a.boqd_qty_wor " _
                                & "FROM " _
                                & "  public.boqd_det a " _
                                & "  INNER JOIN public.pt_mstr b ON (a.boqd_pt_id = b.pt_id) " _
                                & "  INNER JOIN public.code_mstr c ON (a.boqd_um = c.code_id) " _
                                & "  INNER JOIN public.boq_mstr d ON (a.boqd_boq_oid = d.boq_oid) " _
                                & "WHERE " _
                                & "  d.boq_oid = '" & ds.Tables(0).Rows(row).Item("boq_oid").ToString & "' " _
                                & "ORDER BY " _
                                & "  a.boqd_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        '-----------------------------------
                        gc_edit.DataSource = ds_edit.Tables("detail")
                        gv_edit.BestFitColumns()

                        .SQL = "SELECT  " _
                                & "  a.boqs_oid, " _
                                & "  a.boqs_boq_oid, " _
                                & "  a.boqs_pt_id, " _
                                & "  b.pt_code, " _
                                & "  b.pt_desc1, " _
                                & "  b.pt_desc2, " _
                                & "  b.pt_um, " _
                                & "  c.code_code, "

                        If ds.Tables(0).Rows(row).Item("boq_trans_id") = "I" Then
                            .SQL += "  a.boqs_qty as boqs_qty_plan, "
                        Else
                            .SQL += "  a.boqs_qty_plan, "
                        End If

                        .SQL += "  a.boqs_qty, " _
                                & "  a.boqs_qty_pr, " _
                                & "  a.boqs_qty_po, " _
                                & "  a.boqs_qty_receipt, " _
                                & "  a.boqs_qty_wo, " _
                                & "  a.boqs_is_manual " _
                                & "FROM " _
                                & "  public.boqs_stand a " _
                                & "  INNER JOIN public.boq_mstr d ON (a.boqs_boq_oid = d.boq_oid) " _
                                & "  INNER JOIN public.pt_mstr b ON (a.boqs_pt_id = b.pt_id) " _
                                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                                & "WHERE " _
                                & "  d.boq_oid = '" & ds.Tables(0).Rows(row).Item("boq_oid").ToString & "' " _
                                & "ORDER BY " _
                                & "  a.boqs_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "stand")
                        '-----------------------------------
                        gc_stand_edit.DataSource = ds_edit.Tables("stand")
                        gv_stand_edit.BestFitColumns()

                    End With
                    dt_detail = ds_edit.Tables("detail").Copy
                    dt_stand = ds_edit.Tables("stand").Copy
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function
    Function find_pt_id(ByVal par_dt As DataTable, ByVal par_kolom As String, ByVal par_nilai As Double) As Boolean
        Try
            For Each dr As DataRow In par_dt.Rows
                If dr(par_kolom) = par_nilai Then
                    Return True
                    Exit Function
                End If
            Next

            Return False
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function
    Function find_pt_id(ByVal par_dt As DataTable, ByVal par_kolom As String, ByVal par_nilai As Double, ByVal par_kolom2 As String, ByVal par_nilai2 As String) As Boolean
        Try
            For Each dr As DataRow In par_dt.Rows
                If dr(par_kolom) = par_nilai And dr(par_kolom2) = par_nilai2 Then
                    Return True
                    Exit Function
                End If
            Next

            Return False
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function
    Public Overrides Function edit()
        edit = True
        Dim i, j As Integer
        Dim _boq_trn_id As Integer
        Dim _boq_trn_status As String
        Dim ds_bantu As New DataSet
        '=============================================================================

        _boq_trn_id = boq_tran_id.EditValue
        _boq_trn_status = "D" 'set default langsung ke D

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(boq_tran_id.EditValue)
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update boq_mstr set boq_trans_id='D' WHERE boq_oid='" + _boq_oid_mstr.ToString + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To gv_edit.RowCount - 1
                            If find_pt_id(dt_detail, "boqd_pt_id", gv_edit.GetRowCellValue(i, "boqd_pt_id")) = True Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.boqd_det   " _
                                                    & "SET  " _
                                                    & "  boqd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  boqd_upd_date = current_timestamp,  " _
                                                    & "  boqd_qty_plan = " & SetDbl(gv_edit.GetRowCellValue(i, "boqd_qty_plan")) & ",  " _
                                                    & "  boqd_qty = 0 " _
                                                    & "WHERE  " _
                                                    & "   boqd_pt_id = " & gv_edit.GetRowCellValue(i, "boqd_pt_id") & " and boqd_boq_oid='" & _boq_oid_mstr & "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()


                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.boqd_det " _
                                                & "( " _
                                                & "  boqd_oid, " _
                                                & "  boqd_add_by, " _
                                                & "  boqd_add_date, " _
                                                & "  boqd_dt, " _
                                                & "  boqd_seq, " _
                                                & "  boqd_boq_oid, " _
                                                & "  boqd_sopjd_oid, " _
                                                & "  boqd_pt_id, " _
                                                & "  boqd_qty_plan, " _
                                                & "  boqd_qty, " _
                                                & "  boqd_um, " _
                                                & "  boqd_qty_wor " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(_boq_oid_mstr) & ",  " _
                                                & SetSetring(gv_edit.GetRowCellValue(i, "boqd_sopjd_oid")) & ",  " _
                                                & SetInteger(gv_edit.GetRowCellValue(i, "boqd_pt_id")) & ",  " _
                                                & SetDbl(gv_edit.GetRowCellValue(i, "boqd_qty_plan")) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetInteger(gv_edit.GetRowCellValue(i, "boqd_um")) & ",  " _
                                                & SetDbl(0) & "  " _
                                                & ")"


                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        For i = 0 To gv_stand_edit.RowCount - 1

                            If find_pt_id(dt_stand_edit, "boqs_pt_id", gv_stand_edit.GetRowCellValue(i, "boqs_pt_id"), "boqs_is_manual", gv_stand_edit.GetRowCellValue(i, "boqs_is_manual")) = True Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.boqs_stand   " _
                                                    & "SET  " _
                                                    & "  boqs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  boqs_upd_date = current_timestamp,  " _
                                                    & "  boqs_qty_plan = " & SetDbl(gv_stand_edit.GetRowCellValue(i, "boqs_qty_plan")) & ",  " _
                                                    & "  boqs_qty = 0 " _
                                                    & "WHERE  " _
                                                    & "   boqs_pt_id = " & gv_stand_edit.GetRowCellValue(i, "boqs_pt_id") & " and boqs_boq_oid='" & _boq_oid_mstr & "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.boqs_stand " _
                                                & "( " _
                                                & "  boqs_oid, " _
                                                & "  boqs_add_by, " _
                                                & "  boqs_add_date, " _
                                                & "  boqs_dt, " _
                                                & "  boqs_boq_oid, " _
                                                & "  boqs_seq, " _
                                                & "  boqs_pt_id, " _
                                                & "  boqs_qty_plan, " _
                                                & "  boqs_qty, " _
                                                & "  boqs_qty_pr, " _
                                                & "  boqs_qty_po, " _
                                                & "  boqs_qty_receipt, " _
                                                & "  boqs_qty_wo,boqs_indirect,boqs_pt_id_parent,boqs_pt_code_parent,boqs_pt_desc1_parent,boqs_pt_desc2_parent, " _
                                                & "  boqs_is_manual " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetSetring(_boq_oid_mstr) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetInteger(gv_stand_edit.GetRowCellValue(i, "boqs_pt_id")) & ",  " _
                                                & SetDbl(gv_stand_edit.GetRowCellValue(i, "boqs_qty_plan")) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "psd_indirect")) & ",  " _
                                                & SetInteger(gv_stand_edit.GetRowCellValue(i, "pt_id_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "pt_code_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "pt_desc1_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "pt_desc2_parent")) & ",  " _
                                                & SetSetring(gv_stand_edit.GetRowCellValue(i, "boqs_is_manual")) & "  " _
                                                & ")"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            'If gv_stand_edit.GetRowCellValue(i, "boqs_oid") Is System.DBNull.Value Then

                            'End If
                        Next


                        If _conf_value = "1" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _boq_oid_mstr.ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                        & SetInteger(boq_en_id.EditValue) & ",  " _
                                                        & SetSetring(boq_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_boq_oid_mstr) & ",  " _
                                                        & SetSetring(_boq_code) & ",  " _
                                                        & SetSetring("Bill Of Quantity") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_boq_oid_mstr, "boq_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        edit = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function
    Public Function cek_duplikat_pt_id(ByVal par_gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_kolom As String, ByVal par_kolom2 As String) As Boolean 'output false jika ada
        Try

            Dim _pt_id As Integer = 0
            Dim _cek As Integer = 0
            For i As Integer = 0 To par_gv.RowCount - 1
                _cek = 0
                _pt_id = par_gv.GetRowCellValue(i, par_kolom)
                For j As Integer = 0 To par_gv.RowCount - 1
                    If _pt_id = par_gv.GetRowCellValue(j, par_kolom) And par_gv.GetRowCellValue(j, par_kolom2) = "Y" Then
                        _cek = _cek + 1
                    End If
                Next
                If _cek >= 2 Then
                    Dim pt_desc As String = master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", _pt_id)
                    Dim pt_code As String = master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_code", "pt_id", _pt_id)
                    Box("Part Number : " & pt_code & " " & pt_desc & " double")
                    Return False
                    Exit Function
                End If
            Next
            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function
    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Return before_delete
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

        If _conf_value = "0" Then
            'MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Exit Function
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_trans_id") <> "D" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
                Exit Function
            End If
        End If

        '----------------------------------------

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        'Dim i As Integer

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

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

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from boq_mstr where boq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_oid").ToString & "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                           
                            '----------------------------------

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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

#Region "gv_edit"
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
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_en_id")

        'If _col = "req_code_relation" Then
        '    'ant 13 maret 2011
        '    Dim frm As New FReqBusPlanSearch
        '    '------------------------------
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _pod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "en_desc" Then
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
        'Dim _reqd_qty, _reqd_qty_real, _reqd_um_conv, _reqd_qty_cost, _reqd_cost, _reqd_disc, _reqd_qty_processed As Double
        Dim _reqd_qty, _reqd_um_conv, _reqd_cost, _reqd_disc As Double
        _reqd_um_conv = 1
        _reqd_qty = 1
        _reqd_cost = 0
        _reqd_disc = 0

        'If e.Column.Name = "reqd_qty" Then
        '    '********* Cek Qty Processed
        '    Try
        '        _reqd_qty_processed = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty_processed"))
        '    Catch ex As Exception
        '    End Try

        '    If e.Value < _reqd_qty_processed Then
        '        MessageBox.Show("Qty Requistion Can't Lower Than Qty Processed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        gv_edit.CancelUpdateCurrentRow()
        '        Exit Sub
        '    End If
        '    '********************************

        '    Try
        '        _reqd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_um_conv"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _reqd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_cost"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _reqd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_disc"))
        '    Catch ex As Exception
        '    End Try

        '    _reqd_qty_real = e.Value * _reqd_um_conv
        '    _reqd_qty_cost = (e.Value * _reqd_cost) - (e.Value * _reqd_cost * _reqd_disc)

        '    gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_real", _reqd_qty_real)
        '    gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)
        'ElseIf e.Column.Name = "reqd_cost" Then
        '    Try
        '        _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _reqd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_disc")))
        '    Catch ex As Exception
        '    End Try

        '    _reqd_qty_cost = (e.Value * _reqd_qty) - (e.Value * _reqd_qty * _reqd_disc)
        '    gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)
        'ElseIf e.Column.Name = "reqd_disc" Then
        '    Try
        '        _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _reqd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_cost")))
        '    Catch ex As Exception
        '    End Try

        '    _reqd_qty_cost = (_reqd_cost * _reqd_qty) - (_reqd_cost * _reqd_qty * e.Value)
        '    gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)
        'ElseIf e.Column.Name = "reqd_um_conv" Then
        '    Try
        '        _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
        '    Catch ex As Exception
        '    End Try

        '    _reqd_qty_real = e.Value * _reqd_qty

        '    gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_real", _reqd_qty_real)
        'End If
    End Sub


    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            '.SetRowCellValue(e.RowHandle, "reqd_en_id", boq_en_id.EditValue)
            '.SetRowCellValue(e.RowHandle, "en_desc", boq_en_id.GetColumnValue("en_desc"))
            '.SetRowCellValue(e.RowHandle, "reqd_ptnr_id", req_ptnr_id.GetColumnValue("ptnr_id"))
            '.SetRowCellValue(e.RowHandle, "ptnr_name", req_ptnr_id.GetColumnValue("ptnr_name"))
            '.SetRowCellValue(e.RowHandle, "reqd_si_id", req_si_id.EditValue)
            '.SetRowCellValue(e.RowHandle, "si_desc", req_si_id.GetColumnValue("si_desc"))
            '.SetRowCellValue(e.RowHandle, "reqd_end_user", Trim(req_end_user.Text))
            '.SetRowCellValue(e.RowHandle, "reqd_qty", 0)
            '.SetRowCellValue(e.RowHandle, "reqd_cost", 0)
            '.SetRowCellValue(e.RowHandle, "reqd_disc", 0)
            '.SetRowCellValue(e.RowHandle, "reqd_need_date", req_need_date.DateTime)
            '.SetRowCellValue(e.RowHandle, "reqd_due_date", req_due_date.DateTime)
            '.SetRowCellValue(e.RowHandle, "reqd_um_conv", 1)
            '.SetRowCellValue(e.RowHandle, "reqd_qty_real", 0)
            '.SetRowCellValue(e.RowHandle, "reqd_qty_cost", 0)
            '.BestFitColumns()
        End With
    End Sub
#End Region



    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_oid").ToString
        _colom = "boq_trans_id"
        _table = "boq_mstr"
        _criteria = "boq_code"
        _initial = "boq"
        _type = "bq"
        _title = "Bill Of Quantity"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub
    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                  ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
       
        Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String

        If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        _pby_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
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
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '============================================================================
                        If get_status_wf(par_code.ToString()) = 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'" + _
                                                   " and wf_seq = 0"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()



                        ElseIf get_status_wf(par_code.ToString()) > 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set " + _
                                                   " wf_iscurrent = 'Y', " + _
                                                   " wf_wfs_id = '0', " + _
                                                   " wf_desc = '', " + _
                                                   " wf_date_to = null, " + _
                                                   " wf_aprv_user = '', " + _
                                                   " wf_aprv_date = null " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'" + _
                                                   " and wf_wfs_id = '4' "
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '============================================================================

                        sqlTran.Commit()

                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        filename = "c:\syspro\" + par_code + ".xls"
                        ExportTo(par_gv, New ExportXlsProvider(filename))

                        If user_wf_email <> "" Then
                            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        Else
                            'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_oid").ToString
        _colom = "boq_trans_id"
        _table = "boq_mstr"
        _criteria = "boq_code"
        _initial = "boq"
        _type = "bq"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)


    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where " + par_criteria + " = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran



                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where " + par_criteria + " = '" + par_code + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_code = '" + par_code + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                        
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    '-------------------------------------------------------------

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("boq_code")
        _type = "bq"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Bill Of Quantity"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = "True" Then

                Try
                    'gv_email.Columns("boq_oid").FilterInfo = _
                    'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[boq_oid] = '" & ds.Tables("smart").Rows(i).Item("boq_oid").ToString & "'")
                    'gv_email.BestFitColumns()

                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("boq_code"), 0)
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
                                .Command.CommandText = "update boq_mstr set boq_trans_id = '" + _trans_id + "'," + _
                                               " req_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " req_upd_date = current_timestamp " + _
                                               " where req_oid = '" + ds.Tables("smart").Rows(i).Item("boq_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("boq_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("boq_code"), "bq")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("boq_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Bill Of Quantity", ds.Tables("smart").Rows(i).Item("boq_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
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

    Private Sub boq_sopj_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles boq_sopj_oid.ButtonClick
        Try
            'Dim frm As New FSalesOrderProjectSearch
            Dim frm As New FProjectSearch
            frm.set_win(Me)
            frm._en_id = boq_en_id.EditValue
            frm.ShowDialog()
            frm.type_form = True

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_stand_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_stand_edit.CellValueChanged
        Try
            If e.Column.Name = "boqs_qty_plan" Then
                If Not gv_stand_edit.GetRowCellValue(e.RowHandle, "boqs_oid") Is System.DBNull.Value Then
                    gv_stand_edit.CancelUpdateCurrentRow()
                    Box("Data can't edited")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
        
    End Sub
    Private Sub gv_stand_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_stand_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_stand_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_stand_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data_stand()
        End If
    End Sub

    Private Sub gv_stand_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_stand_edit.DoubleClick
        browse_data_stand()
    End Sub

    Private Sub browse_data_stand()
        gc_stand_edit.EmbeddedNavigator.Buttons.DoClick(gc_stand_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables("stand").AcceptChanges()

        Dim _col As String = gv_stand_edit.FocusedColumn.Name
        Dim _row As Integer = gv_stand_edit.FocusedRowHandle

        If Not gv_stand_edit.GetRowCellValue(_row, "boqs_oid") Is System.DBNull.Value Then
            Box("Data Can't edited")
            Exit Sub
        End If
        If _col = "pt_code" Or _col = "pt_desc1" Then

            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = boq_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
       
    End Sub

    
    Private Sub gv_stand_edit_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_stand_edit.SelectionChanged
        Try
            Dim _nilai As Object
            _nilai = gv_stand_edit.GetFocusedRowCellValue("boqs_oid").ToString
            If _nilai Is System.DBNull.Value Then
                gc_stand_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
            Else
                gc_stand_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_stand_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_stand_edit.Click
        gv_stand_edit_SelectionChanged(Nothing, Nothing)
    End Sub

    Private Sub btRetriveDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRetriveDetail.Click
        Try
            Dim ssql As String
            ssql = "SELECT  " _
                & "  a.sod_pt_id as boqd_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.sod_um as boqd_um, " _
                & "  a.sod_qty as boqd_qty_plan,0.0 as boqd_qty, " _
                & "  c.code_code,a.sod_oid as boqd_sopjd_oid,b.pt_pm_code " _
                & "FROM " _
                & "  public.sod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.sod_pt_id = b.pt_id) " _
                & "  INNER JOIN public.ps_mstr d ON (d.ps_pt_bom_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (a.sod_um = c.code_id) " _
                 & "  INNER JOIN public.so_mstr e ON (e.so_oid = a.sod_so_oid) " _
                & "WHERE " _
                & "  a.sod_so_oid = '" & boq_sopj_oid.Tag & "' " _
                & "  and upper(e.so_trans_id) <> 'X' " _
                & "  and pt_its_id = 1 "

            If func_coll.get_conf_file("wf_prod_structure") = "1" Then
                ssql = ssql & "  and ps_trans_id = 'I' "
            End If

            ssql = ssql & " ORDER by sod_seq "

            'Dim dt As New DataTable
            dt_edit = master_new.PGSqlConn.GetTableData(ssql)


            If _conf_value = "0" Then
                For i As Integer = 0 To dt_edit.Rows.Count - 1
                    With dt_edit.Rows(i)
                        .Item("boqd_qty") = .Item("boqd_qty_plan")
                        .Item("boqd_qty_plan") = 0
                    End With
                Next
                dt_edit.AcceptChanges()
            End If


            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()

        Catch ex As Exception
            Pesan(Err)
        End Try
      
    End Sub

    Private Sub btRetriveStandar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRetriveStandar.Click
        Try
            Dim ssql As String = ""

            'If dt_stand Is Nothing Then
            '    Box("Please retrieve detail first")
            '    Exit Sub
            'End If

            dt_stand_edit = Nothing

            For i As Integer = 0 To gv_edit.RowCount - 1
                With dt_edit.Rows(i)


                    If .Item("pt_pm_code").ToString.ToUpper <> "P" Then

                        ssql = "select psd_pt_bom_id as boqs_pt_id, pt_code, pt_desc1, pt_desc2,code_name as code_code," _
                            & " pt_um,psd_indirect,cast('N' as CHAR) as boqs_is_manual, sum(psd_qty) as boqs_qty_plan,0.0 as boqs_qty, " _
                            & " null as pt_id_parent, '' as pt_code_parent,'' as pt_desc1_parent, '' as pt_desc2_parent  from ( select * from public.get_ps_first(" & .Item("boqd_pt_id") _
                            & ",1) where  variable_4=0) as temp  group by psd_pt_bom_id, pt_code, pt_desc1, pt_desc2, " _
                            & " code_name , pt_um , psd_indirect "

                        If dt_stand_edit Is Nothing Then
                            dt_stand_edit = master_new.PGSqlConn.GetTableData(ssql)
                            ssql = "SELECT  " _
                                & "  a.ro_yield " _
                                & "FROM " _
                                & "  public.ro_mstr a " _
                                & "WHERE " _
                                & "  a.ro_pt_id = " & .Item("boqd_pt_id")

                            Dim _yield As Double
                            Dim _qty_prod As Double
                            Dim dt_yield As New DataTable

                            _yield = 0
                            _qty_prod = 0

                            dt_yield = master_new.PGSqlConn.GetTableData(ssql)
                            For Each dr_yield As DataRow In dt_yield.Rows
                                _yield = dr_yield("ro_yield")
                            Next

                            If _conf_value = "0" Then
                                _qty_prod = Math.Round(.Item("boqd_qty") / _yield, 0)
                            Else
                                _qty_prod = Math.Round(.Item("boqd_qty_plan") / _yield, 0)
                            End If


                            For Each dr As DataRow In dt_stand_edit.Rows
                                dr("pt_id_parent") = .Item("boqd_pt_id")
                                dr("pt_code_parent") = .Item("pt_code")
                                dr("pt_desc1_parent") = .Item("pt_desc1")
                                dr("pt_desc2_parent") = .Item("pt_desc2")
                                If dr("psd_indirect") = "N" Then
                                    dr("boqs_qty_plan") = dr("boqs_qty_plan") * _qty_prod
                                End If

                            Next
                            dt_stand_edit.AcceptChanges()
                        Else
                            Dim dt_temp As New DataTable
                            dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                            ssql = "SELECT  " _
                               & "  a.ro_yield " _
                               & "FROM " _
                               & "  public.ro_mstr a " _
                               & "WHERE " _
                               & "  a.ro_pt_id = " & .Item("boqd_pt_id")

                            Dim _yield As Double
                            Dim _qty_prod As Double
                            Dim dt_yield As New DataTable

                            _yield = 0
                            _qty_prod = 0

                            dt_yield = master_new.PGSqlConn.GetTableData(ssql)
                            For Each dr_yield As DataRow In dt_yield.Rows
                                _yield = dr_yield("ro_yield")
                            Next

                            If _conf_value = "0" Then
                                _qty_prod = Math.Round(.Item("boqd_qty") / _yield, 0)
                            Else
                                _qty_prod = Math.Round(.Item("boqd_qty_plan") / _yield, 0)
                            End If


                            For Each dr_temp As DataRow In dt_temp.Rows
                                Dim dr_new As DataRow
                                dr_new = dt_stand_edit.NewRow

                                dr_new("boqs_pt_id") = dr_temp("boqs_pt_id")
                                dr_new("pt_code") = dr_temp("pt_code")
                                dr_new("pt_desc1") = dr_temp("pt_desc1")
                                dr_new("pt_desc2") = dr_temp("pt_desc2")
                                dr_new("code_code") = dr_temp("code_code")
                                dr_new("pt_um") = dr_temp("pt_um")
                                dr_new("psd_indirect") = dr_temp("psd_indirect")
                                dr_new("boqs_is_manual") = dr_temp("boqs_is_manual")
                                If dr_temp("psd_indirect") = "N" Then
                                    dr_new("boqs_qty_plan") = dr_temp("boqs_qty_plan") * _qty_prod
                                End If
                                dr_new("pt_id_parent") = .Item("boqd_pt_id")
                                dr_new("pt_code_parent") = .Item("pt_code")
                                dr_new("pt_desc1_parent") = .Item("pt_desc1")
                                dr_new("pt_desc2_parent") = .Item("pt_desc2")
                                dt_stand_edit.Rows.Add(dr_new)

                            Next
                            dt_stand_edit.AcceptChanges()
                        End If
                    Else
                        ssql = "select " & .Item("boqd_pt_id") & " as boqs_pt_id,'" & .Item("pt_code") _
                            & "' as pt_code,'" & .Item("pt_desc1") & "' as pt_desc1,'" & .Item("pt_desc2") & "' as pt_desc2,'" _
                            & .Item("code_code") & "' as code_code," & .Item("boqd_um") & " as pt_um,'N' as psd_indirect," _
                            & " cast('N' as CHAR) as boqs_is_manual, " _
                            & .Item("boqd_qty_plan") & " as boqs_qty_plan"

                    End If


                End With
            Next

            If _conf_value = "0" Then
                For i As Integer = 0 To dt_stand_edit.Rows.Count - 1
                    With dt_stand_edit.Rows(i)
                        .Item("boqs_qty") = .Item("boqs_qty_plan")
                        .Item("boqs_qty_plan") = 0
                    End With
                Next
                dt_stand_edit.AcceptChanges()
            End If
           

            'ssql = "SELECT boqs_pt_id,pt_code,pt_desc1,pt_desc2,code_code,boqs_is_manual,psd_indirect,sum(boqs_qty_plan) as boqs_qty_plan from (" & ssql _
            '    & ") as temp group by boqs_pt_id,pt_code,pt_desc1,pt_desc2,psd_indirect,code_code,boqs_is_manual"

            'Dim dt As New DataTable


            'Dim _dtrow As DataRow
            'For Each dr As DataRow In dt_stand.Rows
            '    If dr("boqs_is_manual") = "Y" Then
            '        _dtrow = dt.NewRow

            '        _dtrow("boqs_pt_id") = dr("boqs_pt_id")
            '        _dtrow("pt_code") = dr("pt_code")
            '        _dtrow("pt_desc1") = dr("pt_desc1")
            '        _dtrow("pt_desc2") = dr("pt_desc2")
            '        _dtrow("code_code") = dr("code_code")
            '        _dtrow("boqs_is_manual") = dr("boqs_is_manual")
            '        _dtrow("boqs_qty_plan") = dr("boqs_qty_plan")
            '        dt.Rows.Add(_dtrow)
            '        dt.AcceptChanges()

            '    End If
            'Next
           
            gc_stand_edit.DataSource = dt_stand_edit
            gv_stand_edit.BestFitColumns()

            gc_stand_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            gc_stand_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
            gv_stand_edit.OptionsBehavior.Editable = False
        Catch ex As Exception
            If Err.Number = 91 Then
                Box("Please retrieve detail first")
                Exit Sub
            Else
                Pesan(Err)
            End If

        End Try
    End Sub
End Class
