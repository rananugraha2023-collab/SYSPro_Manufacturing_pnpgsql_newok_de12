Imports npgsql
Imports master_new.ModFunction

Public Class FPurchaseReturnReport
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
        add_column_copy(gv_view1, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Return Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date", "rcv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Effective Date", "rcv_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Packing Slip", "rcv_packing_slip", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Exchange Rate", "rcv_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty Return", "rcvd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Invoice", "rcvd_qty_inv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "PO Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "PO Cost Ext", "pod_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "UM", "rcvd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM Conversion", "rcvd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Real", "rcvd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Packing", "rcvd_packing_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Supplier Lot Number", "rcvd_supp_lot", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Serial Number", "rcvds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty Serial", "rcvds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AP Eff. Date", "ap_eff_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AP Type", "type_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "User Create", "rcv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "rcv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "User Update", "rcv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "rcv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                            & "public.rcv_mstr.rcv_oid, " _
                            & "public.rcv_mstr.rcv_dom_id, " _
                            & "public.rcv_mstr.rcv_en_id, " _
                            & "public.en_mstr.en_desc, " _
                            & "public.rcv_mstr.rcv_add_by, " _
                            & "public.rcv_mstr.rcv_add_date, " _
                            & "public.rcv_mstr.rcv_upd_by, " _
                            & "public.rcv_mstr.rcv_upd_date, " _
                            & "public.rcv_mstr.rcv_code, " _
                            & "public.rcv_mstr.rcv_date, " _
                            & "public.rcv_mstr.rcv_eff_date, " _
                            & "public.rcv_mstr.rcv_po_oid, " _
                            & "public.rcv_mstr.rcv_packing_slip, " _
                            & "public.cu_mstr.cu_name, " _
                            & "public.rcv_mstr.rcv_exc_rate, " _
                            & "public.rcv_mstr.rcv_dt, " _
                            & "public.po_mstr.po_code, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "public.rcvd_det.rcvd_oid, " _
                            & "public.rcvd_det.rcvd_rcv_oid, " _
                            & "public.rcvd_det.rcvd_pod_oid, " _
                            & "public.pt_mstr.pt_id, " _
                            & "public.pt_mstr.pt_code, " _
                            & "public.pt_mstr.pt_desc1, " _
                            & "public.pt_mstr.pt_desc2, " _
                            & "public.pt_mstr.pt_type, " _
                            & "public.rcvd_det.rcvd_qty, " _
                            & "public.rcvd_det.rcvd_qty_inv, " _
                            & "public.rcvd_det.rcvd_um, " _
                            & "um_master.code_name as rcvd_um_name, " _
                            & "public.rcvd_det.rcvd_packing_qty, " _
                            & "public.rcvd_det.rcvd_um_conv, " _
                            & "public.rcvd_det.rcvd_qty_real, " _
                            & "public.rcvd_det.rcvd_si_id, " _
                            & "public.rcvd_det.rcvd_loc_id, " _
                            & "public.rcvd_det.rcvd_lot_serial, " _
                            & "public.rcvd_det.rcvd_supp_lot, " _
                            & "public.rcvd_det.rcvd_dt, " _
                            & "public.si_mstr.si_desc, " _
                            & "public.loc_mstr.loc_desc, " _
                            & "pod_pt_desc1, " _
                            & "pod_pt_desc2, " _
                            & "ap_invoice, " _
                            & "ap_eff_date, " _
                            & "ap_type, pod_cost, " _
                            & "pod_cost * rcvd_qty as pod_cost_ext , " _
                            & "ap_type_master.code_name as type_name, " _
                            & "tax_class_master.code_name as tax_class_name " _
                            & "FROM " _
                            & "public.rcv_mstr " _
                            & "INNER JOIN public.po_mstr ON (public.rcv_mstr.rcv_po_oid = public.po_mstr.po_oid) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "INNER JOIN public.en_mstr ON (public.rcv_mstr.rcv_en_id = public.en_mstr.en_id) " _
                            & "INNER JOIN public.cu_mstr ON (public.rcv_mstr.rcv_cu_id = public.cu_mstr.cu_id) " _
                            & "INNER JOIN public.rcvd_det ON (public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid) " _
                            & "INNER JOIN public.pod_det ON (public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid) " _
                            & "INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                            & "INNER JOIN public.code_mstr um_master ON (public.rcvd_det.rcvd_um = um_master.code_id) " _
                            & "INNER JOIN public.loc_mstr ON (public.rcvd_det.rcvd_loc_id = public.loc_mstr.loc_id) " _
                            & "INNER JOIN public.si_mstr ON (public.rcvd_det.rcvd_si_id = public.si_mstr.si_id)   " _
                            & "inner join public.code_mstr tax_class_master on tax_class_master.code_id = pod_tax_class " _
                            & "left outer join apr_rcv on apr_rcvd_oid = rcvd_oid " _
                            & "left outer join ap_mstr on ap_oid = apr_ap_oid " _
                            & "left outer join code_mstr ap_type_master on ap_type_master.code_id = ap_type" _
                            & " where rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and rcv_is_receive ~~* 'N'" _
                            & " and rcv_en_id in (select user_en_id from tconfuserentity " _
                                              & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

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
