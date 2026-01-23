Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FProjectSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _filter As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = FBillOfQuantity.Name Or fobject.name = FBoQtoPR.Name Then
            add_column(gv_master, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Order Date", "so_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
            add_column(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Order Date", "prj_date_ord", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
            add_column(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "prj_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If
    End Sub

    Public Overrides Function get_sequel() As String

       
        If fobject.name = FBillOfQuantity.Name Then
            get_sequel = "SELECT  " _
                        & "  so_oid, " _
                        & "  so_code, " _
                        & "  ptnr_name, " _
                        & "  so_date " _
                        & "FROM  " _
                        & "  public.so_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                        & " where so_en_id = " + SetInteger(_en_id) _
                        & " and so_code ~~* '%" + Trim(te_search.Text) + "%' and so_manufacture='Y' and so_boq_status='N' " _
                        & " order by so_code  "
        ElseIf fobject.name = FBoQtoPR.Name Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id,boq_oid,pjc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & "  inner join boq_mstr on prj_oid = boq_sopj_oid " _
                         & "  inner join pjc_mstr on prj_code = pjc_code " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_code ~~* '%" + Trim(te_search.Text) + "%' " _
                        & " and boq_trans_id='I' " _
                        & "order by prj_code asc "
        Else
            get_sequel = "SELECT  " _
                & "  a.prj_code, " _
                & "  a.prj_date_ord, " _
                & "  c.ptnr_name, " _
                & "  a.prj_ptnr_id, " _
                & "  a.prj_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  a.prj_qty, " _
                & "  a.prj_bom_id, " _
                & "  d.bom_code, " _
                & "  d.bom_desc, " _
                & "  a.prj_ro_id, " _
                & "  e.ro_code, " _
                & "  e.ro_desc,a.prj_oid,a.prj_remarks " _
                & "FROM " _
                & "  public.prj_mstr a " _
                & "  INNER JOIN public.pt_mstr b ON (a.prj_pt_id = b.pt_id) " _
                & "  INNER JOIN public.ptnr_mstr c ON (a.prj_ptnr_id = c.ptnr_id) " _
                & "  INNER JOIN public.bom_mstr d ON (a.prj_bom_id = d.bom_id) " _
                & "  INNER JOIN public.ro_mstr e ON (a.prj_ro_id = e.ro_id) " _
                & " Where a.prj_remarks ~~* '%" + Trim(te_search.Text) + "%' or a.prj_code ~~* '%" + Trim(te_search.Text) + "%' "


            If _filter <> "" Then
                get_sequel += _filter
            End If

            get_sequel += "  order by a.prj_code"
        End If

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

    Public Overrides Sub fill_data()
        Dim sSQL As String
        Try
            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position

            If fobject.name = "FWorkOrder" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("prj_remarks")
                fobject.__wo_prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString
                fobject.wo_pt_id.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.__wo_pt_id = ds.Tables(0).Rows(_row_gv).Item("prj_pt_id")
                fobject.wo_bom_id.text = ds.Tables(0).Rows(_row_gv).Item("bom_desc")
                fobject.__wo_bom_id = ds.Tables(0).Rows(_row_gv).Item("prj_bom_id")
                fobject.__wo_ro_id = ds.Tables(0).Rows(_row_gv).Item("prj_ro_id")
                fobject.wo_ro_id.text = ds.Tables(0).Rows(_row_gv).Item("ro_desc")
                fobject.wo_qty_ord.editvalue = ds.Tables(0).Rows(_row_gv).Item("prj_qty")

            ElseIf fobject.name = FCostElementRealProject.Name Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("prj_remarks")
                fobject.__prjdr_prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString

                sSQL = "SELECT  " _
                   & "  a.prjd_oid, " _
                   & "  a.prjd_prj_oid, " _
                   & "  a.prjd_seq, " _
                   & "  a.prjd_cse_id, " _
                   & "  b.cse_code, " _
                   & "  b.cse_desc, " _
                   & "  a.prjd_cost_est, " _
                   & "  a.prjd_cost_rea, " _
                   & "  a.prjd_var, " _
                   & "  a.prjd_remarks " _
                   & "FROM " _
                   & "  public.prjd_det a " _
                   & "  INNER JOIN public.cse_mstr b ON (a.prjd_cse_id = b.cse_id) " _
                   & "WHERE " _
                   & "  a.prjd_prj_oid='" & ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString & "' " _
                   & "ORDER BY " _
                   & "  a.prjd_seq"


                Dim dt As New DataTable
                dt = GetTableData(sSQL)


                fobject.ds_edit.Tables(0).Clear()
                For Each dr As DataRow In dt.Rows
                    Dim _dtrow As DataRow
                    _dtrow = fobject.ds_edit.Tables(0).NewRow

                    _dtrow("prjdrd_prjd_oid") = dr("prjd_oid").ToString
                    _dtrow("cse_code") = dr("cse_code")
                    _dtrow("cse_desc") = dr("cse_desc")
                    _dtrow("prjd_remarks") = dr("prjd_remarks")
                    _dtrow("prjdrd_cost") = SetNumber(dr("prjd_cost_est"))

                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                    fobject.ds_edit.Tables(0).AcceptChanges()

                    fobject.gv_edit.BestFitColumns()
                Next
            ElseIf fobject.name = FBillOfQuantity.Name Then
                fobject.boq_sopj_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
                fobject.boq_sopj_oid.tag = ds.Tables(0).Rows(_row_gv).Item("so_oid").ToString
            ElseIf fobject.name = FBoQtoPR.Name Then

                fobject.par_project.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
                fobject.par_project.tag = ds.Tables(0).Rows(_row_gv).Item("pjc_id")
                fobject._boq_oid = ds.Tables(0).Rows(_row_gv).Item("boq_oid").ToString

                'Dim sSql As String

                sSQL = "SELECT  " _
                    & "  false as status,a.boqs_oid, " _
                    & "  a.boqs_boq_oid, " _
                    & "  a.boqs_pt_id, " _
                    & "  b.pt_code, " _
                    & "  b.pt_desc1, " _
                    & "  b.pt_desc2, " _
                    & "  b.pt_um, " _
                    & "  c.code_code, " _
                    & "  a.boqs_qty_plan, " _
                    & "  coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0) as qty_open , coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0) as qty_generate, " _
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
                    & "  a.boqs_boq_oid ='" & ds.Tables(0).Rows(_row_gv).Item("boq_oid").ToString & "' " _
                    & " and d.boq_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " and coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0)>0 " _
                    & "ORDER BY " _
                    & "  a.boqs_seq"

                fobject.gc_outstanding.datasource = master_new.PGSqlConn.GetTableData(sSql)
                fobject.gv_outstanding.BestFitColumns()


                sSql = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.req_mstr.req_oid, " _
                    & "  public.req_mstr.req_dom_id, " _
                    & "  public.req_mstr.req_en_id, " _
                    & "  public.req_mstr.req_upd_date, " _
                    & "  public.req_mstr.req_upd_by, " _
                    & "  public.req_mstr.req_add_date, " _
                    & "  public.req_mstr.req_add_by, " _
                    & "  public.req_mstr.req_code, " _
                    & "  public.req_mstr.req_ptnr_id, " _
                    & "  public.req_mstr.req_cmaddr_id, " _
                    & "  public.req_mstr.req_date, " _
                    & "  public.req_mstr.req_need_date, " _
                    & "  public.req_mstr.req_due_date, " _
                    & "  public.req_mstr.req_requested, " _
                    & "  public.req_mstr.req_end_user, " _
                    & "  public.req_mstr.req_rmks, " _
                    & "  public.req_mstr.req_sb_id, " _
                    & "  public.req_mstr.req_cc_id, " _
                    & "  public.req_mstr.req_si_id, " _
                    & "  public.req_mstr.req_type, " _
                    & "  public.req_mstr.req_pjc_id, " _
                    & "  public.req_mstr.req_close_date, " _
                    & "  public.req_mstr.req_total, " _
                    & "  public.req_mstr.req_tran_id, " _
                    & "  public.req_mstr.req_trans_id, " _
                    & "  public.req_mstr.req_trans_rmks, " _
                    & "  public.req_mstr.req_current_route, " _
                    & "  public.req_mstr.req_next_route, " _
                    & "  public.req_mstr.req_dt, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  tran_name, " _
                    & "  public.pjc_mstr.pjc_code, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.sb_mstr.sb_desc, " _
                    & "  public.cc_mstr.cc_desc " _
                    & "FROM " _
                    & "  public.req_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                    & " where req_pjc_id= " & ds.Tables(0).Rows(_row_gv).Item("pjc_id") _
                    & " and req_en_id in (select user_en_id from tconfuserentity " _
                           & " where userid = " & master_new.ClsVar.sUserID.ToString & ") " _
                    & " Order by req_code"

                Dim dt As New DataTable
                dt = master_new.PGSqlConn.GetTableData(sSql)

                fobject.gc_master.datasource = dt
                fobject.gv_master.BestFitColumns()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
