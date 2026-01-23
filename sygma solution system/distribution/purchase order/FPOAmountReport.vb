Imports npgsql
Imports master_new.ModFunction

Public Class FPOAmountReport
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim _qry As String

    Private Sub FPurchaseOrderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_view1, "pod_pt_id", False)
        add_column_copy(gv_view1, "Partnumber Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Amount", "amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Amount Receive", "amount_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view2, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Order Date", "po_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_view2, "Suplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Amount", "pod_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view2, "Amount Receive", "amount_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            .SQL = "SELECT pod_pt_id,pt_code,pod_pt_desc1, sum(pod_qty_cost) as amount, sum (amount_receive) as amount_receive,'" & pr_txttglawal.DateTime.Date.ToString("dd/MM/yyyy") & " to " & pr_txttglakhir.DateTime.Date.ToString("dd/MM/yyyy") & "' as lbl_periode," & SetDateNTime00(pr_txttglawal.DateTime.Date) & " as tgl_awal," & SetDateNTime00(pr_txttglakhir.DateTime.Date) & " as tgl_akhir, '" & CeWithDetail.Checked.ToString & "' as status from (SELECT  " _
                            & "en_mstr_header.en_desc as en_desc_header, " _
                            & "public.po_mstr.po_oid, " _
                            & "public.po_mstr.po_dom_id, " _
                            & "public.po_mstr.po_en_id, " _
                            & "public.po_mstr.po_upd_date, " _
                            & "public.po_mstr.po_upd_by, " _
                            & "public.po_mstr.po_add_date, " _
                            & "public.po_mstr.po_add_by, " _
                            & "public.po_mstr.po_code, " _
                            & "public.po_mstr.po_ptnr_id, " _
                            & "public.po_mstr.po_cmaddr_id, " _
                            & "public.po_mstr.po_date, " _
                            & "public.po_mstr.po_need_date, " _
                            & "public.po_mstr.po_due_date, " _
                            & "public.po_mstr.po_rmks, " _
                            & "public.po_mstr.po_sb_id, " _
                            & "public.po_mstr.po_cc_id, " _
                            & "public.po_mstr.po_si_id, " _
                            & "public.po_mstr.po_pjc_id, " _
                            & "public.po_mstr.po_close_date, " _
                            & "public.po_mstr.po_total, " _
                            & "public.po_mstr.po_tran_id, " _
                            & "public.po_mstr.po_trans_id, " _
                            & "public.po_mstr.po_trans_rmks, " _
                            & "public.po_mstr.po_current_route, " _
                            & "public.po_mstr.po_next_route, " _
                            & "public.po_mstr.po_dt, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "cmaddr_name, " _
                            & "tran_name, " _
                            & "po_status_cash, " _
                            & "pjc_mstr_header.pjc_desc as pjc_desc_header, " _
                            & "si_mstr_header.si_desc as si_desc_header, " _
                            & "sb_mstr_header.sb_desc as sb_desc_header,  " _
                            & "cc_mstr_header.cc_desc as cc_desc_header, " _
                            & "po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                            & "po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                            & "po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                            & "po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext, " _
                            & "en_mstr_detail.en_desc as en_desc_detail, " _
                            & "pod_seq, " _
                            & "si_mstr_detail.si_desc as si_desc_detail,pod_pt_id, " _
                            & "pt_code, " _
                            & "pod_pt_desc1, " _
                            & "pod_pt_desc2, " _
                            & "pod_rmks, " _
                            & "pod_end_user, " _
                            & "pod_qty, " _
                            & "pod_qty_receive, " _
                            & "pod_qty_invoice, " _
                            & "pod_qty - coalesce(pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "pod_um, " _
                            & "um_master.code_name as um_name, " _
                            & "pod_cost, " _
                            & "pod_disc, " _
                            & "pod_sb_id, pod_cc_id, pod_pjc_id, " _
                            & "pod_need_date, " _
                            & "pod_due_date, " _
                            & "pod_um_conv, " _
                            & "pod_qty_real, " _
                            & "pod_pt_class, " _
                            & "pod_taxable, " _
                            & "pod_tax_inc, " _
                            & "pod_tax_class, " _
                            & "pod_status, " _
                            & "pod_qty_return, " _
                            & "pjc_mstr_detail.pjc_desc as pjc_desc_detail, " _
                            & "si_mstr_detail.si_desc as si_desc_detailr, " _
                            & "sb_mstr_detail.sb_desc as sb_desc_detail,  " _
                            & " taxclass_mstr_detail.code_name as pod_tax_class_name,req_code, " _
                            & "cc_mstr_detail.cc_desc as cc_desc_detail, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost , ((pod_qty_receive * pod_cost) - (pod_qty_receive * pod_cost * pod_disc)) as amount_receive " _
                            & "FROM " _
                            & "public.po_mstr " _
                            & "INNER JOIN public.en_mstr en_mstr_header ON (public.po_mstr.po_en_id = en_mstr_header.en_id) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "INNER JOIN public.sb_mstr sb_mstr_header ON (public.po_mstr.po_sb_id = sb_mstr_header.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_header ON (public.po_mstr.po_cc_id = cc_mstr_header .cc_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_header ON (public.po_mstr.po_si_id = si_mstr_header.si_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_header ON (public.po_mstr.po_pjc_id = pjc_mstr_header.pjc_id) " _
                            & "INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                            & "INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                            & "INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class   " _
                            & "inner join pod_det on pod_po_oid = po_oid " _
                            & "INNER JOIN public.en_mstr en_mstr_detail ON (pod_en_id = en_mstr_detail.en_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_detail ON (pod_si_id = si_mstr_detail.si_id) " _
                            & "inner join pt_mstr on pt_id = pod_pt_id " _
                            & "INNER JOIN public.sb_mstr sb_mstr_detail ON (pod_sb_id = sb_mstr_detail.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_detail ON (pod_cc_id = cc_mstr_detail .cc_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_detail ON (pod_pjc_id = pjc_mstr_detail.pjc_id) " _
                            & "INNER JOIN public.code_mstr as um_master ON um_master.code_id  = pod_um " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr_detail ON taxclass_mstr_detail.code_id  = pod_tax_class   " _
                            & "left outer join reqd_det on pod_reqd_oid = reqd_oid " _
                            & "left outer join req_mstr on reqd_req_oid = req_oid " _
                            & " where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")) as temp group by pod_pt_id,pt_code,pod_pt_desc1 order by amount desc "

                            _qry = .SQL

                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            .SQL = "SELECT  " _
                            & "en_mstr_header.en_desc as en_desc_header, " _
                            & "public.po_mstr.po_oid, " _
                            & "public.po_mstr.po_dom_id, " _
                            & "public.po_mstr.po_en_id, " _
                            & "public.po_mstr.po_upd_date, " _
                            & "public.po_mstr.po_upd_by, " _
                            & "public.po_mstr.po_add_date, " _
                            & "public.po_mstr.po_add_by, " _
                            & "public.po_mstr.po_code, " _
                            & "public.po_mstr.po_ptnr_id, " _
                            & "public.po_mstr.po_cmaddr_id, " _
                            & "public.po_mstr.po_date, " _
                            & "public.po_mstr.po_need_date, " _
                            & "public.po_mstr.po_due_date, " _
                            & "public.po_mstr.po_rmks, " _
                            & "public.po_mstr.po_sb_id, " _
                            & "public.po_mstr.po_cc_id, " _
                            & "public.po_mstr.po_si_id, " _
                            & "public.po_mstr.po_pjc_id, " _
                            & "public.po_mstr.po_close_date, " _
                            & "public.po_mstr.po_total, " _
                            & "public.po_mstr.po_tran_id, " _
                            & "public.po_mstr.po_trans_id, " _
                            & "public.po_mstr.po_trans_rmks, " _
                            & "public.po_mstr.po_current_route, " _
                            & "public.po_mstr.po_next_route, " _
                            & "public.po_mstr.po_dt, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "cmaddr_name, " _
                            & "tran_name, " _
                            & "po_status_cash, " _
                            & "pjc_mstr_header.pjc_desc as pjc_desc_header, " _
                            & "si_mstr_header.si_desc as si_desc_header, " _
                            & "sb_mstr_header.sb_desc as sb_desc_header,  " _
                            & "cc_mstr_header.cc_desc as cc_desc_header, " _
                            & "po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                            & "po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                            & "po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                            & "po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext, " _
                            & "en_mstr_detail.en_desc as en_desc_detail, " _
                            & "pod_seq, " _
                            & "si_mstr_detail.si_desc as si_desc_detail,pod_pt_id, " _
                            & "pt_code, " _
                            & "pod_pt_desc1, " _
                            & "pod_pt_desc2, " _
                            & "pod_rmks, " _
                            & "pod_end_user, " _
                            & "pod_qty, " _
                            & "pod_qty_receive, " _
                            & "pod_qty_invoice, " _
                            & "pod_qty - coalesce(pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "pod_um, " _
                            & "um_master.code_name as um_name, " _
                            & "pod_cost, " _
                            & "pod_disc, " _
                            & "pod_sb_id, pod_cc_id, pod_pjc_id, " _
                            & "pod_need_date, " _
                            & "pod_due_date, " _
                            & "pod_um_conv, " _
                            & "pod_qty_real, " _
                            & "pod_pt_class, " _
                            & "pod_taxable, " _
                            & "pod_tax_inc, " _
                            & "pod_tax_class, " _
                            & "pod_status, " _
                            & "pod_qty_return, " _
                            & "pjc_mstr_detail.pjc_desc as pjc_desc_detail, " _
                            & "si_mstr_detail.si_desc as si_desc_detailr, " _
                            & "sb_mstr_detail.sb_desc as sb_desc_detail,  " _
                            & " taxclass_mstr_detail.code_name as pod_tax_class_name,req_code, " _
                            & "cc_mstr_detail.cc_desc as cc_desc_detail, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost , ((pod_qty_receive * pod_cost) - (pod_qty_receive * pod_cost * pod_disc)) as amount_receive " _
                            & "FROM " _
                            & "public.po_mstr " _
                            & "INNER JOIN public.en_mstr en_mstr_header ON (public.po_mstr.po_en_id = en_mstr_header.en_id) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "INNER JOIN public.sb_mstr sb_mstr_header ON (public.po_mstr.po_sb_id = sb_mstr_header.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_header ON (public.po_mstr.po_cc_id = cc_mstr_header .cc_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_header ON (public.po_mstr.po_si_id = si_mstr_header.si_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_header ON (public.po_mstr.po_pjc_id = pjc_mstr_header.pjc_id) " _
                            & "INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                            & "INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                            & "INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class   " _
                            & "inner join pod_det on pod_po_oid = po_oid " _
                            & "INNER JOIN public.en_mstr en_mstr_detail ON (pod_en_id = en_mstr_detail.en_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_detail ON (pod_si_id = si_mstr_detail.si_id) " _
                            & "inner join pt_mstr on pt_id = pod_pt_id " _
                            & "INNER JOIN public.sb_mstr sb_mstr_detail ON (pod_sb_id = sb_mstr_detail.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_detail ON (pod_cc_id = cc_mstr_detail .cc_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_detail ON (pod_pjc_id = pjc_mstr_detail.pjc_id) " _
                            & "INNER JOIN public.code_mstr as um_master ON um_master.code_id  = pod_um " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr_detail ON taxclass_mstr_detail.code_id  = pod_tax_class   " _
                            & "left outer join reqd_det on pod_reqd_oid = reqd_oid " _
                            & "left outer join req_mstr on reqd_req_oid = req_oid " _
                            & " where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") and pod_pt_id=" & gv_view1.GetRowCellValue(gv_view1.FocusedRowHandle, "pod_pt_id")


                            .InitializeCommand()
                            .FillDataSet(ds, "view2")
                            gc_view2.DataSource = ds.Tables("view2")
                        End If

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

    Private Sub xtc_master_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtc_master.SelectedPageChanged
        If xtc_master.SelectedTabPageIndex = 1 Then
            load_data_many(True)
        End If

    End Sub

    Private Sub xtc_master_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xtc_master.TabIndexChanged
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        End If

    End Sub

    Public Overrides Sub preview()
        
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT pod_pt_id,pt_code,pod_pt_desc1, sum(pod_qty_cost) as amount, sum (amount_receive) as amount_receive,'" & pr_txttglawal.DateTime.Date.ToString("dd/MM/yyyy") & " to " & pr_txttglakhir.DateTime.Date.ToString("dd/MM/yyyy") & "' as lbl_periode," & SetDateNTime00(pr_txttglawal.DateTime.Date) & " as tgl_awal," & SetDateNTime00(pr_txttglakhir.DateTime.Date) & " as tgl_akhir, '" & CeWithDetail.Checked.ToString & "' as status from (SELECT  " _
                            & "en_mstr_header.en_desc as en_desc_header, " _
                            & "public.po_mstr.po_oid, " _
                            & "public.po_mstr.po_dom_id, " _
                            & "public.po_mstr.po_en_id, " _
                            & "public.po_mstr.po_upd_date, " _
                            & "public.po_mstr.po_upd_by, " _
                            & "public.po_mstr.po_add_date, " _
                            & "public.po_mstr.po_add_by, " _
                            & "public.po_mstr.po_code, " _
                            & "public.po_mstr.po_ptnr_id, " _
                            & "public.po_mstr.po_cmaddr_id, " _
                            & "public.po_mstr.po_date, " _
                            & "public.po_mstr.po_need_date, " _
                            & "public.po_mstr.po_due_date, " _
                            & "public.po_mstr.po_rmks, " _
                            & "public.po_mstr.po_sb_id, " _
                            & "public.po_mstr.po_cc_id, " _
                            & "public.po_mstr.po_si_id, " _
                            & "public.po_mstr.po_pjc_id, " _
                            & "public.po_mstr.po_close_date, " _
                            & "public.po_mstr.po_total, " _
                            & "public.po_mstr.po_tran_id, " _
                            & "public.po_mstr.po_trans_id, " _
                            & "public.po_mstr.po_trans_rmks, " _
                            & "public.po_mstr.po_current_route, " _
                            & "public.po_mstr.po_next_route, " _
                            & "public.po_mstr.po_dt, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "cmaddr_name, " _
                            & "tran_name, " _
                            & "po_status_cash, " _
                            & "pjc_mstr_header.pjc_desc as pjc_desc_header, " _
                            & "si_mstr_header.si_desc as si_desc_header, " _
                            & "sb_mstr_header.sb_desc as sb_desc_header,  " _
                            & "cc_mstr_header.cc_desc as cc_desc_header, " _
                            & "po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                            & "po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                            & "po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                            & "po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext, " _
                            & "en_mstr_detail.en_desc as en_desc_detail, " _
                            & "pod_seq, " _
                            & "si_mstr_detail.si_desc as si_desc_detail,pod_pt_id, " _
                            & "pt_code, " _
                            & "pod_pt_desc1, " _
                            & "pod_pt_desc2, " _
                            & "pod_rmks, " _
                            & "pod_end_user, " _
                            & "pod_qty, " _
                            & "pod_qty_receive, " _
                            & "pod_qty_invoice, " _
                            & "pod_qty - coalesce(pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "pod_um, " _
                            & "um_master.code_name as um_name, " _
                            & "pod_cost, " _
                            & "pod_disc, " _
                            & "pod_sb_id, pod_cc_id, pod_pjc_id, " _
                            & "pod_need_date, " _
                            & "pod_due_date, " _
                            & "pod_um_conv, " _
                            & "pod_qty_real, " _
                            & "pod_pt_class, " _
                            & "pod_taxable, " _
                            & "pod_tax_inc, " _
                            & "pod_tax_class, " _
                            & "pod_status, " _
                            & "pod_qty_return, " _
                            & "pjc_mstr_detail.pjc_desc as pjc_desc_detail, " _
                            & "si_mstr_detail.si_desc as si_desc_detailr, " _
                            & "sb_mstr_detail.sb_desc as sb_desc_detail,  " _
                            & " taxclass_mstr_detail.code_name as pod_tax_class_name,req_code, " _
                            & "cc_mstr_detail.cc_desc as cc_desc_detail, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost , ((pod_qty_receive * pod_cost) - (pod_qty_receive * pod_cost * pod_disc)) as amount_receive " _
                            & "FROM " _
                            & "public.po_mstr " _
                            & "INNER JOIN public.en_mstr en_mstr_header ON (public.po_mstr.po_en_id = en_mstr_header.en_id) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "INNER JOIN public.sb_mstr sb_mstr_header ON (public.po_mstr.po_sb_id = sb_mstr_header.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_header ON (public.po_mstr.po_cc_id = cc_mstr_header .cc_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_header ON (public.po_mstr.po_si_id = si_mstr_header.si_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_header ON (public.po_mstr.po_pjc_id = pjc_mstr_header.pjc_id) " _
                            & "INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                            & "INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                            & "INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class   " _
                            & "inner join pod_det on pod_po_oid = po_oid " _
                            & "INNER JOIN public.en_mstr en_mstr_detail ON (pod_en_id = en_mstr_detail.en_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_detail ON (pod_si_id = si_mstr_detail.si_id) " _
                            & "inner join pt_mstr on pt_id = pod_pt_id " _
                            & "INNER JOIN public.sb_mstr sb_mstr_detail ON (pod_sb_id = sb_mstr_detail.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_detail ON (pod_cc_id = cc_mstr_detail .cc_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_detail ON (pod_pjc_id = pjc_mstr_detail.pjc_id) " _
                            & "INNER JOIN public.code_mstr as um_master ON um_master.code_id  = pod_um " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr_detail ON taxclass_mstr_detail.code_id  = pod_tax_class   " _
                            & "left outer join reqd_det on pod_reqd_oid = reqd_oid " _
                            & "left outer join req_mstr on reqd_req_oid = req_oid " _
                            & " where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")) as temp group by pod_pt_id,pt_code,pod_pt_desc1 order by amount desc " & " limit " & TeLimitPrint.Text


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRPOAmountReport"
        frm._remarks = "Amount Report"
        frm.ShowDialog()


    End Sub
End Class
