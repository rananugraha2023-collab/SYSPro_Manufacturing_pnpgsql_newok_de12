Imports master_new.ModFunction

Public Class FInventoryIssuesReport
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryIssuesReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        grid_detail()
    End Sub

    Private Sub grid_detail()
      
        add_column_copy(gv_detail_issued, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Issued Date", "riud_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Issued Code", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Issued By", "riu_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Pt Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Qty Issue", "riud_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "UM", "riud_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_issued, "Remarks", "riu_remarks", DevExpress.Utils.HorzAlignment.Default)
     
        add_column_copy(gv_detail_req, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Issued Date", "riud_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Issued Code", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Issued By", "riu_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Pt Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail_req, "Qty Issue", "riud_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "UM", "riud_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Remarks", "riu_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Requested Date", "pb_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Request Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Requested Date", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_req, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
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
                            load_detail_issued(objload)
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            load_detail_Inv_IR(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                            '    load_sales_person(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 3 Then
                            '    load_sales_by_so(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 4 Then
                            '    load_cash_credit(objload)
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub load_detail_issued(ByVal par_obj As Object)
        With par_obj
            .SQL = "SELECT " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.riud_det.riud_dt, " _
                    & "  public.riu_mstr.riu_type2, " _
                    & "  public.riu_mstr.riu_add_by, " _
                    & "  public.riud_det.riud_pt_id, " _
                    & "  public.pt_mstr.pt_desc1,public.pt_mstr.pt_code, " _
                    & "  public.riud_det.riud_qty *-1 AS riud_qty, " _
                    & "  code_name as riud_um_name, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.riu_mstr.riu_remarks " _
                    & "  FROM " _
                    & "  public.riud_det " _
                    & "  INNER JOIN public.riu_mstr ON (public.riud_det.riud_riu_oid = public.riu_mstr.riu_oid) " _
                    & "  INNER JOIN public.loc_mstr ON (public.riud_det.riud_loc_id = public.loc_mstr.loc_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.riud_det.riud_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.riud_det.riud_um = public.code_mstr.code_id) " _
                    & "  LEFT OUTER JOIN public.en_mstr ON (public.riu_mstr.riu_en_id = public.en_mstr.en_id)" _
                    & "  where riu_mstr.riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and riu_mstr.riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and riu_mstr.riu_type ~~* 'I'" _
                    & "  and riu_en_id in (select user_en_id from tconfuserentity " _
                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & "  order by  public.riud_det.riud_dt ASC "

            .InitializeCommand()
            .FillDataSet(ds, "detail_issued")
            gc_detail_issued.DataSource = ds.Tables("detail_issued")
            gv_detail_issued.BestFitColumns()
        End With
    End Sub

    Private Sub load_detail_Inv_IR(ByVal par_obj As Object)
        With par_obj
            .SQL = "SELECT " _
                & "  public.en_mstr.en_desc, " _
                & "  public.riud_det.riud_dt, " _
                & "  public.riu_mstr.riu_type2, " _
                & "  public.riu_mstr.riu_add_by, " _
                & "  public.riud_det.riud_pt_id, " _
                & "  public.pt_mstr.pt_desc1,public.pt_mstr.pt_code, " _
                & "  public.riud_det.riud_qty *-1 AS riud_qty, " _
                & "  code_name as riud_um_name, " _
                & "  public.loc_mstr.loc_desc, " _
                & "  public.riu_mstr.riu_remarks, " _
                & "  public.pb_mstr.pb_dt, " _
                & "  public.pb_mstr.pb_code, " _
                & "  public.pb_mstr.pb_requested, " _
                & "  public.pb_mstr.pb_end_user, " _
                & "  public.pb_mstr.pb_rmks " _
                & "  FROM " _
                & "  public.riud_det " _
                & "  INNER JOIN public.riu_mstr ON (public.riud_det.riud_riu_oid = public.riu_mstr.riu_oid) " _
                & "  INNER JOIN public.loc_mstr ON (public.riud_det.riud_loc_id = public.loc_mstr.loc_id) " _
                & "  INNER JOIN public.pt_mstr ON (public.riud_det.riud_pt_id = public.pt_mstr.pt_id) " _
                & "  INNER JOIN public.code_mstr ON (public.riud_det.riud_um = public.code_mstr.code_id) " _
                & "  INNER JOIN public.en_mstr ON (public.riu_mstr.riu_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.pb_mstr ON (public.riu_mstr.riu_ref_pb_oid = public.pb_mstr.pb_oid)" _
                & "  where riu_mstr.riu_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and riu_mstr.riu_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and riu_mstr.riu_type ~~* 'I'" _
                & "  and riu_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & "  order by  public.riud_det.riud_dt ASC "

            .InitializeCommand()
            .FillDataSet(ds, "load_detail_Inv_IR")
            gc_detail_req.DataSource = ds.Tables("load_detail_Inv_IR")
            gv_detail_req.BestFitColumns()
        End With
    End Sub

    Private Sub gc_detail_req_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_detail_req.Click

    End Sub

    Private Sub gc_detail_ar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_detail_issued.Click

    End Sub
End Class
