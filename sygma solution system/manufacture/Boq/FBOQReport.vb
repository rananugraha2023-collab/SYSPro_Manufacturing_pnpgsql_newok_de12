Imports npgsql
Imports master_new.ModFunction

Public Class FBOQReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FPurchaseOrderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "BOQ Number", "boq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Remark", "boq_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date", "boq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Status", "boq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PT Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PT Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty Open", "boqs_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Relocation", "boqs_qty_relocation", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty PR", "boqs_qty_pr", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty PO", "boqs_qty_po", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Receipt", "boqs_qty_receipt", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty WO", "boqs_qty_wo", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Return", "boqs_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Is Manual", "boqs_is_manual", DevExpress.Utils.HorzAlignment.Default)
       
        add_column_copy(gv_view1, "User Create", "boq_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "boq_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "User Update", "boq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "boq_upd_date", DevExpress.Utils.HorzAlignment.Center)
        
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
                                & "  c.en_desc, " _
                                & "  d.prj_code, " _
                                & "  a.boq_code, " _
                                & "  a.boq_date, " _
                                & "  a.boq_remark, " _
                                & "  a.boq_trans_id, " _
                                & "  e.tran_name, " _
                                & "  f.pt_code, " _
                                & "  f.pt_desc1, " _
                                & "  f.pt_desc2, " _
                                & "  b.boqs_qty, " _
                                & "  coalesce(b.boqs_qty_relocation,0) as boqs_qty_relocation, " _
                                & "  b.boqs_qty_pr, " _
                                & "  b.boqs_qty_po, " _
                                & "  b.boqs_qty_receipt, " _
                                & "  b.boqs_qty_wo, " _
                                & "  b.boqs_qty_return, " _
                                & "  b.boqs_is_manual, " _
                                & "  a.boq_add_by, " _
                                & "  a.boq_add_date, " _
                                & "  a.boq_upd_by, " _
                                & "  a.boq_upd_date " _
                                & "FROM " _
                                & "  public.boq_mstr a " _
                                & "  INNER JOIN public.boqs_stand b ON (a.boq_oid = b.boqs_boq_oid) " _
                                & "  INNER JOIN public.en_mstr c ON (a.boq_en_id = c.en_id) " _
                                & "  INNER JOIN public.prj_mstr d ON (a.boq_sopj_oid = d.prj_oid) " _
                                & "  INNER JOIN public.tran_mstr e ON (a.boq_tran_id = e.tran_id) " _
                                & "  INNER JOIN public.pt_mstr f ON (b.boqs_pt_id = f.pt_id) " _
                                & "WHERE " _
                                & "  a.boq_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
                                & " and boq_en_id in (select user_en_id from tconfuserentity " _
                                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & "ORDER BY " _
                                & "  a.boq_date, " _
                                & "  a.boq_code"


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
