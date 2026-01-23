Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FInventoryHistory
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _sql As String
    Public __pt_id As String

    Private Sub FInventoryReportDetailLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        date_start.DateTime = CekTanggal()
        date_end.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(pr_entity, "en_mstr")
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_loc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Date", "invh_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Description", "invh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Site ", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Transaction Code", "invh_trn_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "PT Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Part Number Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Part Number Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Qty", "invh_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Qty Proses", "invh_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Qty Old", "invh_qty_old", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "TimeStamp", "dt_timestamp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
      
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor

        Dim _en_id_all As String

        If cbChild.EditValue = True Then
            _en_id_all = get_en_id_child(pr_entity.EditValue)
        Else

            _en_id_all = pr_entity.EditValue

        End If

        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                            & "  a.invh_oid, " _
                            & "  b.en_desc, " _
                            & "  a.invh_date, " _
                            & "  a.invh_desc, " _
                            & "  c.si_desc, " _
                            & "  d.loc_desc, " _
                            & "  e.pt_code, " _
                            & "  e.pt_desc1, " _
                            & "  e.pt_desc2, " _
                            & "  coalesce(invh_qty_old,0) + coalesce(invh_qty,0) as invh_real, " _
                            & "  a.invh_qty, " _
                            & "  a.dt_timestamp,a.invh_trn_code,invh_qty_old " _
                            & "FROM " _
                            & "  public.invh_mstr a " _
                            & "  INNER JOIN public.en_mstr b ON (a.invh_en_id = b.en_id) " _
                            & "  INNER JOIN public.si_mstr c ON (a.invh_si_id = c.si_id) " _
                            & "  INNER JOIN public.loc_mstr d ON (a.invh_loc_id = d.loc_id) " _
                            & "  INNER JOIN public.pt_mstr e ON (a.invh_pt_id = e.pt_id) " _
                            & "WHERE invh_en_id in (" & _en_id_all & ") and dt_timestamp between " & SetDateNTime00(date_start.DateTime) _
                            & " and " & SetDateNTime99(date_end.DateTime)

                        If pt_id.Text <> "" Then
                            .SQL = .SQL & " and a.invh_pt_id=" & __pt_id
                        End If

                        If CBLoc.EditValue = True Then
                            .SQL = .SQL & " and a.invh_loc_id=" & par_loc.EditValue
                        End If

                        .SQL = .SQL & " order by pt_desc1, dt_timestamp desc"

                        .InitializeCommand()
                        .FillDataSet(ds, "inv_location")
                        gc_loc.DataSource = ds.Tables("inv_location")
                        gv_loc.BestFitColumns()

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id.ButtonClick
        Try
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._obj = pt_id
            frm._en_id = pr_entity.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub pr_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pr_entity.EditValueChanged
        Try
            init_le(par_loc, "loc_mstr", pr_entity.EditValue)
        Catch ex As Exception
        End Try
    End Sub
End Class
