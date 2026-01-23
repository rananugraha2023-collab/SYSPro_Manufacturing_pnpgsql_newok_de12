Imports npgsql
Imports master_new.ModFunction

Public Class FInventoryReceiptsReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryReceiptsReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_detail_receipt, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Receipt Code", "riu_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Receipt Date", "riud_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Receipt By", "riu_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Pt Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Qty Receipt", "riud_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_receipt, "UM", "riud_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Remarks", "riu_remarks", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.riud_det.riud_dt, " _
                            & "  public.riu_mstr.riu_type2, " _
                            & "  public.riu_mstr.riu_add_by, " _
                            & "  public.riud_det.riud_pt_id, " _
                            & "  public.pt_mstr.pt_desc1, public.pt_mstr.pt_code, " _
                            & "  public.riud_det.riud_qty, " _
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
                            & "  and riu_mstr.riu_type ~~* 'R'" _
                            & "  and riu_en_id in (select user_en_id from tconfuserentity " _
                            & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  order by  public.riud_det.riud_dt ASC "

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
