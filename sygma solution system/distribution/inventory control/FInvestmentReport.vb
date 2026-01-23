Imports npgsql
Imports master_new.ModFunction

Public Class FInvestmentReport
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryAdjusmentPlusReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Receive Number", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Effective Date", "riu_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Remarks", "riu_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty Adjustment", "riud_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "UM", "riud_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM Conversion", "riud_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Real", "riud_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Lot Number", "riud_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost", "riud_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "User Create", "riu_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "riu_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "User Update", "riu_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "riu_upd_date", DevExpress.Utils.HorzAlignment.Center)
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
                            .SQL = "SELECT  " _
                                & "  riu_oid, " _
                                & "  riu_dom_id, " _
                                & "  riu_en_id, " _
                                & "  en_desc, " _
                                & "  riu_add_by, " _
                                & "  riu_add_date, " _
                                & "  riu_upd_by, " _
                                & "  riu_upd_date, " _
                                & "  riu_type2, " _
                                & "  riu_date, " _
                                & "  riu_type, " _
                                & "  riu_remarks, " _
                                & "  riu_dt " _
                                & "  riud_oid, " _
                                & "  riud_riu_oid, " _
                                & "  riud_pt_id, " _
                                & "  pt_id, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  pt_ls, " _
                                & "  riud_qty, " _
                                & "  riud_um, " _
                                & "  code_name as riud_um_name, " _
                                & "  riud_um_conv, " _
                                & "  riud_qty_real, " _
                                & "  riud_si_id, " _
                                & "  si_desc, " _
                                & "  riud_loc_id, " _
                                & "  loc_desc, " _
                                & "  riud_lot_serial, " _
                                & "  riud_cost, " _
                                & "  riud_ac_id, " _
                                & "  ac_code, " _
                                & "  ac_name, " _
                                & "  riud_sb_id, " _
                                & "  sb_desc, " _
                                & "  riud_cc_id, " _
                                & "  cc_desc, " _
                                & "  riud_dt " _
                                & "FROM  " _
                                & "  public.riu_mstr " _
                                & "  INNER JOIN public.riud_det ON (public.riud_det.riud_riu_oid = public.riu_mstr.riu_oid) " _
                                & "  INNER JOIN public.pt_mstr ON (public.riud_det.riud_pt_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.riud_det.riud_um = public.code_mstr.code_id) " _
                                & "  INNER JOIN public.loc_mstr ON (public.riud_det.riud_loc_id = public.loc_mstr.loc_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.riud_det.riud_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.ac_mstr ON (public.riud_det.riud_ac_id = public.ac_mstr.ac_id) " _
                                & "  INNER JOIN public.sb_mstr ON (public.riud_det.riud_sb_id = public.sb_mstr.sb_id) " _
                                & "  INNER JOIN public.cc_mstr ON (public.riud_det.riud_cc_id = public.cc_mstr.cc_id) " _
                                & "  inner join en_mstr on en_id = riu_en_id " _
                                & "  where riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & "  and riu_mstr.riu_type ~~* 'P'" _
                                & "  and riu_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
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
End Class
