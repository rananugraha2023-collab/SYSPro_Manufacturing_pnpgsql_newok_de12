Imports npgsql
Imports master_new.ModFunction

Public Class FWorkOrderIssueReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FPurchaseReceiptReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Issue Code", "woci_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date", "woci_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "WO Ref.", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Inventory Request", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Route Desc", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty Order", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Remarks", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Part Number", "pt_code_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc1_wod", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM", "code_name_wod", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty", "wocid_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Type", "wocid_loc_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Location / Work Center from", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Project from", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Work Center To", "wc_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        '.SQL = "SELECT  " _
                        '    & "public.rcv_mstr.rcv_oid, " _
                        '    & "public.rcv_mstr.rcv_dom_id, " _
                        '    & "public.rcv_mstr.rcv_en_id, " _
                        '    & "public.en_mstr.en_desc, " _
                        '    & "public.rcv_mstr.rcv_add_by, " _
                        '    & "public.rcv_mstr.rcv_add_date, " _
                        '    & "public.rcv_mstr.rcv_upd_by, " _
                        '    & "public.rcv_mstr.rcv_upd_date, " _
                        '    & "public.rcv_mstr.rcv_code, " _
                        '    & "public.rcv_mstr.rcv_date, " _
                        '    & "public.rcv_mstr.rcv_eff_date, " _
                        '    & "public.rcv_mstr.rcv_po_oid, " _
                        '    & "public.rcv_mstr.rcv_packing_slip, " _
                        '    & "public.cu_mstr.cu_name, " _
                        '    & "public.rcv_mstr.rcv_exc_rate, " _
                        '    & "public.rcv_mstr.rcv_dt, " _
                        '    & "public.po_mstr.po_code, " _
                        '    & "public.ptnr_mstr.ptnr_name, " _
                        '    & "public.rcvd_det.rcvd_oid, " _
                        '    & "public.rcvd_det.rcvd_rcv_oid, " _
                        '    & "public.rcvd_det.rcvd_pod_oid, " _
                        '    & "public.pt_mstr.pt_id, " _
                        '    & "public.pt_mstr.pt_code, " _
                        '    & "public.pt_mstr.pt_desc1, " _
                        '    & "public.pt_mstr.pt_desc2, " _
                        '    & "public.pt_mstr.pt_type, " _
                        '    & "public.rcvd_det.rcvd_qty, " _
                        '    & "public.rcvd_det.rcvd_qty_inv, " _
                        '    & "public.rcvd_det.rcvd_um, " _
                        '    & "um_master.code_name as rcvd_um_name, " _
                        '    & "public.rcvd_det.rcvd_packing_qty, " _
                        '    & "public.rcvd_det.rcvd_um_conv, " _
                        '    & "public.rcvd_det.rcvd_qty_real, " _
                        '    & "public.rcvd_det.rcvd_si_id, " _
                        '    & "public.rcvd_det.rcvd_loc_id, " _
                        '    & "public.rcvd_det.rcvd_lot_serial, " _
                        '    & "public.rcvd_det.rcvd_supp_lot, " _
                        '    & "public.rcvd_det.rcvd_dt, " _
                        '    & "public.si_mstr.si_desc, " _
                        '    & "public.loc_mstr.loc_desc, " _
                        '    & "pod_pt_desc1, " _
                        '    & "pod_pt_desc2, " _
                        '    & "ap_invoice, " _
                        '    & "ap_eff_date, " _
                        '    & "ap_type, pod_cost, " _
                        '    & "pod_cost * rcvd_qty as pod_cost_ext , " _
                        '    & "ap_type_master.code_name as type_name, " _
                        '    & "tax_class_master.code_name as tax_class_name " _
                        '    & "FROM " _
                        '    & "public.rcv_mstr " _
                        '    & "INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                        '    & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                        '    & "INNER JOIN public.en_mstr ON (public.rcv_mstr.rcv_en_id = public.en_mstr.en_id) " _
                        '    & "INNER JOIN public.cu_mstr ON (public.rcv_mstr.rcv_cu_id = public.cu_mstr.cu_id) " _
                        '    & "INNER JOIN public.rcvd_det ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
                        '    & "INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                        '    & "INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                        '    & "INNER JOIN public.code_mstr um_master ON (public.rcvd_det.rcvd_um = um_master.code_id) " _
                        '    & "INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
                        '    & "INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id)   " _
                        '    & "inner join public.code_mstr tax_class_master on tax_class_master.code_id = pod_tax_class " _
                        '    & "left outer join apr_rcv on apr_rcvd_oid = rcvd_oid " _
                        '    & "left outer join ap_mstr on ap_oid = apr_ap_oid " _
                        '    & "left outer join code_mstr ap_type_master on ap_type_master.code_id = ap_type" _
                        '    & " where rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        '    & " and rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        '    & " and rcv_is_receive ~~* 'Y'" _
                        '    & " and rcv_en_id in (select user_en_id from tconfuserentity " _
                        '                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                        .SQL = "SELECT  " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.woci_mstr.woci_oid, " _
                            & "  public.woci_mstr.woci_dom_id, " _
                            & "  public.woci_mstr.woci_en_id, " _
                            & "  public.woci_mstr.woci_code, " _
                            & "  public.woci_mstr.woci_wo_id, " _
                            & "  public.woci_mstr.woci_op, " _
                            & "  public.woci_mstr.woci_date, " _
                            & "  public.woci_mstr.woci_issue_alloc, " _
                            & "  public.woci_mstr.woci_issue_picked, " _
                            & "  public.woci_mstr.woci_remarks, " _
                            & "  public.woci_mstr.woci_dt,woci_si_id,si_desc, " _
                            & "  public.wo_mstr.wo_code, " _
                            & "  public.wo_mstr.wo_pt_id, " _
                            & "  public.wo_mstr.wo_qty_ord, " _
                            & "  public.wo_mstr.wo_qty_rjc, " _
                            & "  public.wo_mstr.wo_ro_id, " _
                            & "  public.wo_mstr.wo_insheet_pct, " _
                            & "  public.wo_mstr.wo_rel_date, " _
                            & "  public.ro_mstr.ro_id, " _
                            & "  public.ro_mstr.ro_desc,woci_wc_id,wc_desc,woci_pb_oid,pb_code, " _
               & "   wocid_det.wocid_oid, " _
                    & "   wocid_det.wocid_woci_oid, " _
                    & "   wocid_det.wocid_wod_oid, " _
                    & "   wocid_det.wocid_seq, " _
                    & "   wocid_det.wocid_op, " _
                    & "   wocid_det.wocid_qty, wocid_det.wocid_qty_comp, " _
                    & "   wocid_det.wocid_substitute, " _
                    & "   wocid_det.wocid_pt_subs_id, " _
                    & "   wocid_det.wocid_si_id, " _
                    & "   wocid_det.wocid_loc_id, " _
                    & "   wocid_det.wocid_lot_serial, " _
                    & "   wocid_det.wocid_dt, " _
            & "   pt_mstr_wod.pt_code as pt_code_wod, " _
                    & "   pt_mstr_wod.pt_desc1 as pt_desc1_wod, " _
                    & "   pt_mstr_wod.pt_desc2 as pt_desc2_wod, " _
                    & "   code_mstr_wod.code_name as code_name_wod " _
                            & "FROM " _
                            & "  public.woci_mstr " _
                            & "  INNER JOIN public.wo_mstr ON (public.woci_mstr.woci_wo_id = public.wo_mstr.wo_id) " _
                            & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id)" _
                            & "  INNER JOIN public.en_mstr ON (public.en_mstr.en_id = public.woci_mstr.woci_en_id)" _
                            & "  INNER JOIN public.si_mstr ON (woci_mstr.woci_si_id = public.si_mstr.si_id)" _
             & "  INNER JOIN public.wocid_det ON (woci_mstr.woci_oid = public.wocid_det.wocid_woci_oid)" _
             & "  INNER JOIN  wod_det ON ( wocid_det.wocid_wod_oid =  wod_det.wod_oid) " _
             & "  INNER JOIN  pt_mstr pt_mstr_wod ON ( wod_det.wod_pt_bom_id =  pt_mstr_wod.pt_id) " _
             & "  INNER JOIN  code_mstr code_mstr_wod ON ( pt_mstr_wod.pt_um =  code_mstr_wod.code_id) " _
                            & "  LEFT OUTER JOIN public.wc_mstr ON (woci_mstr.woci_wc_id = public.wc_mstr.wc_id)" _
                            & "  LEFT OUTER JOIN public.pb_mstr ON (woci_mstr.woci_pb_oid = pb_oid)" _
                            & "  WHERE woci_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and woci_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & "  and wo_en_id in (select user_en_id from tconfuserentity " _
                                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                            & " union all " _
                            & "SELECT  " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.woci_mstr.woci_oid, " _
                            & "  public.woci_mstr.woci_dom_id, " _
                            & "  public.woci_mstr.woci_en_id, " _
                            & "  public.woci_mstr.woci_code, " _
                            & "  public.woci_mstr.woci_wo_id, " _
                            & "  public.woci_mstr.woci_op, " _
                            & "  public.woci_mstr.woci_date, " _
                            & "  public.woci_mstr.woci_issue_alloc, " _
                            & "  public.woci_mstr.woci_issue_picked, " _
                            & "  public.woci_mstr.woci_remarks, " _
                            & "  public.woci_mstr.woci_dt,woci_si_id,si_desc, " _
                            & "  public.wo_mstr.wo_code, " _
                            & "  public.wo_mstr.wo_pt_id, " _
                            & "  public.wo_mstr.wo_qty_ord, " _
                            & "  public.wo_mstr.wo_qty_rjc, " _
                            & "  public.wo_mstr.wo_ro_id, " _
                            & "  public.wo_mstr.wo_insheet_pct, " _
                            & "  public.wo_mstr.wo_rel_date, " _
                            & "  public.ro_mstr.ro_id, " _
                            & "  public.ro_mstr.ro_desc,woci_wc_id,wc_desc,woci_pb_oid,pb_code, " _
               & "   wocid_det.wocid_oid, " _
                    & "   wocid_det.wocid_woci_oid, " _
                    & "   wocid_det.wocid_wod_oid, " _
                    & "   wocid_det.wocid_seq, " _
                    & "   wocid_det.wocid_op, " _
                    & "   wocid_det.wocid_qty, wocid_det.wocid_qty_comp, " _
                    & "   wocid_det.wocid_substitute, " _
                    & "   wocid_det.wocid_pt_subs_id, " _
                    & "   wocid_det.wocid_si_id, " _
                    & "   wocid_det.wocid_loc_id, " _
                    & "   wocid_det.wocid_lot_serial, " _
                    & "   wocid_det.wocid_dt, " _
            & "   pt_mstr_wod.pt_code as pt_code_wod, " _
                    & "   pt_mstr_wod.pt_desc1 as pt_desc1_wod, " _
                    & "   pt_mstr_wod.pt_desc2 as pt_desc2_wod, " _
                    & "   code_mstr_wod.code_name as code_name_wod " _
                            & "FROM " _
                            & "  public.woci_mstr " _
                            & "  INNER JOIN public.wo_mstr ON (public.woci_mstr.woci_wo_id = public.wo_mstr.wo_id) " _
                            & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id)" _
                            & "  INNER JOIN public.en_mstr ON (public.en_mstr.en_id = public.woci_mstr.woci_en_id)" _
                            & "  INNER JOIN public.si_mstr ON (woci_mstr.woci_si_id = public.si_mstr.si_id)" _
             & "  INNER JOIN public.wocid_det ON (woci_mstr.woci_oid = public.wocid_det.wocid_woci_oid)" _
             & "  INNER JOIN  pt_mstr pt_mstr_wod ON ( wocid_pt_subs_id =  pt_mstr_wod.pt_id) " _
             & "  INNER JOIN  code_mstr code_mstr_wod ON ( pt_mstr_wod.pt_um =  code_mstr_wod.code_id) " _
                            & "  LEFT OUTER JOIN public.wc_mstr ON (woci_mstr.woci_wc_id = public.wc_mstr.wc_id)" _
                            & "  LEFT OUTER JOIN public.pb_mstr ON (woci_mstr.woci_pb_oid = pb_oid)" _
                            & "  WHERE woci_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and woci_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & "  and wo_en_id in (select user_en_id from tconfuserentity " _
                                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "



                        '& "  INNER JOIN  wod_det ON ( wocid_det.wocid_wod_oid =  wod_det.wod_oid) " _


                        .InitializeCommand()
                        .FillDataSet(ds, "view1")
                        gc_view1.DataSource = ds.Tables("view1")

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
End Class
