Imports npgsql
Imports master_new.ModFunction

Public Class FDownTimeReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Dim dt_bantu As DataTable
    Dim func_data As New function_data

    Private Sub FRequisitionReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pr_entity.Properties.DataSource = dt_bantu
        pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pr_entity.ItemIndex = 0

        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Work Order Code ", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Operation", "wr_op", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Purchase Order Code ", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date", "wrd_eff_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty ", "wrd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Setup Start", "wrd_start_setup", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "Setup Stop", "wrd_stop_setup", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "Setup Elapsed", "wrd_elapsed_setup", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Run Start", "wrd_start_run", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "Run Stop", "wrd_stop_run", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "Run Elapsed", "wrd_elapsed_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Down time", "wrd_down_time", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Down Time Reason", "down_reason_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Remarks", "wrd_remark", DevExpress.Utils.HorzAlignment.Default)
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
                            & "  a.wrd_oid, " _
                            & "  a.wrd_en_id, " _
                            & "  b.en_desc, " _
                            & "  a.wrd_wr_oid, " _
                            & "  c.wr_op, " _
                            & "  d.wo_code, " _
                            & "  a.wrd_eff_date, " _
                            & "  a.wrd_qty, " _
                            & "  a.wrd_start_setup, " _
                            & "  a.wrd_stop_setup, " _
                            & "  a.wrd_elapsed_setup, " _
                            & "  a.wrd_start_run, " _
                            & "  a.wrd_stop_run, " _
                            & "  a.wrd_elapsed_run, " _
                            & "  a.wrd_down_time, " _
                            & "  a.wrd_down_reason_id, " _
                            & "  e.code_name AS down_reason_desc, " _
                            & "  a.wrd_sub_cost, " _
                            & "  a.wrd_remark, " _
                            & "  a.wrd_add_by, " _
                            & "  a.wrd_add_date, " _
                            & "  a.wrd_upd_by, " _
                            & "  a.wrd_upd_date,f.po_code " _
                            & "FROM " _
                            & "  public.wrd_det a " _
                            & "  INNER JOIN public.en_mstr b ON (a.wrd_en_id = b.en_id) " _
                            & "  INNER JOIN public.wr_route c ON (a.wrd_wr_oid = c.wr_oid) " _
                            & "  INNER JOIN public.wo_mstr d ON (c.wr_wo_oid = d.wo_oid) " _
                            & "  LEFT OUTER JOIN public.code_mstr e ON (a.wrd_down_reason_id = e.code_id) " _
                            & "  LEFT OUTER JOIN public.po_mstr f ON (a.wrd_po_oid = f.po_oid) " _
                            & "WHERE " _
                            & "  a.wrd_eff_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
                            & " and wrd_en_id=" & pr_entity.EditValue _
                            & " and wrd_down_time <> 0 " _
                            & " ORDER BY " _
                            & "  a.wrd_eff_date"

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
