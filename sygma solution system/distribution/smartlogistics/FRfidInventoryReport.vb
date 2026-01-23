Imports npgsql
Imports master_new.ModFunction

Public Class FRfidInventoryReport
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryAdjusmentPlusReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Ds_req1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_req1.DataTable1)
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Location Desc", "loc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Location Sub ID", "losc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location Sub Name", "locs_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location Sub Remarks", "locs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location Sub Active", "locs_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "RFID/QR Code", "invcd_rfid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "QR Code", "invcd_qrcode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "invcd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Color", "color_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "RFID/QR Code Remarks", "invcd_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "User Create", "invcd_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "invcd_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "User Update", "invcd_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "invcd_upd_date", DevExpress.Utils.HorzAlignment.Center)
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
                                & "  a.locs_en_id, " _
                                & "  b.en_desc, " _
                                & "  a.locs_loc_id, " _
                                & "  c.loc_code, " _
                                & "  c.loc_desc, " _
                                & "  a.losc_id, " _
                                & "  a.locs_name, " _
                                & "  a.locs_remarks, " _
                                & "  a.locs_active, " _
                                & "  a.locs_add_date, " _
                                & "  a.locs_add_by, " _
                                & "  a.locs_upd_date, " _
                                & "  a.locs_upd_by, " _
                                & "  d.invcd_oid, " _
                                & "  d.invcd_en_id, " _
                                & "  d.invcd_pt_id, " _
                                & "  e.pt_code, " _
                                & "  e.pt_desc1, " _
                                & "  d.invcd_qty, " _
                                & "  d.invcd_rfid, " _
                                & "  d.invcd_locs_id, " _
                                & "  d.invcd_color_code, " _
                                & "  f.color_name, " _
                                & "  d.invcd_remarks, " _
                                & "  d.invcd_add_date, " _
                                & "  d.invcd_add_by, " _
                                & "  d.invcd_upd_date, " _
                                & "  d.invcd_upd_by " _
                                & "FROM " _
                                & "  public.invcd_det d " _
                                & "  INNER JOIN public.en_mstr b ON (b.en_id = d.invcd_en_id) " _
                                & "  INNER JOIN public.locs_mstr a ON (a.losc_id = d.invcd_locs_id) " _
                                & "  INNER JOIN public.pt_mstr e ON (d.invcd_pt_id = e.pt_id) " _
                                & "  INNER JOIN public.loc_mstr c ON (a.locs_loc_id = c.loc_id) " _
                                & "  LEFT OUTER JOIN public.color_mstr f ON (d.invcd_color_code = f.color_code) " _
                                & "  where  invcd_en_id in (select user_en_id from tconfuserentity " _
                                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                & " ORDER BY " _
                                & "  pt_desc1 "

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
