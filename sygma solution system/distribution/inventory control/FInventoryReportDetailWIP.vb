Public Class FInventoryReportDetailWIP
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub FInventoryReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pr_entity.Properties.DataSource = dt_bantu
        pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pr_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_inv, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inv, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inv, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inv, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_inv, "WO Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inv, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inv, "Qty On Hand", "invw_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_inv, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)

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
                            & "  a.invw_oid, " _
                            & "  a.invw_en_id, " _
                            & "  d.en_desc, " _
                            & "  a.invw_wo_oid, " _
                            & "  e.wo_code, " _
                            & "  a.invw_wc_id, " _
                            & "  c.wc_desc, " _
                            & "  a.invw_pt_id, " _
                            & "  b.pt_code, " _
                            & "  b.pt_desc1, " _
                            & "  b.pt_desc2, " _
                            & "  f.code_code AS um_desc, " _
                            & "  a.invw_qty " _
                            & "FROM " _
                            & "  public.invw_wip a " _
                            & "  INNER JOIN public.pt_mstr b ON (a.invw_pt_id = b.pt_id) " _
                            & "  INNER JOIN public.wc_mstr c ON (a.invw_wc_id = c.wc_id) " _
                            & "  INNER JOIN public.en_mstr d ON (a.invw_en_id = d.en_id) " _
                            & "  INNER JOIN public.wo_mstr e ON (a.invw_wo_oid = e.wo_oid) " _
                            & "  INNER JOIN public.code_mstr f ON (b.pt_um = f.code_id) " _
                            & " WHERE a.invw_en_id=" & pr_entity.EditValue _
                            & " ORDER BY " _
                            & "  d.en_desc, " _
                            & "    b.pt_code"

                        .InitializeCommand()
                        .FillDataSet(ds, "inv_wip")
                        gc_inv.DataSource = ds.Tables("inv_wip")

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
