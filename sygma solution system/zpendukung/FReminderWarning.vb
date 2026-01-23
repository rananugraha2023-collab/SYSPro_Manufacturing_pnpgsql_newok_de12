Imports master_new.ModFunction
Public Class FReminderWarning

    Private Sub FReminder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        load_data_many(True)
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_wf, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_wf, "Sales Order Number", "so_code", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_wf, "Sales Order Date", "so_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "D")
        add_column_copy(gv_wf, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Near)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================

            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        '.SQL = "SELECT DISTINCT " _
                        '        & "  a.soship_code, " _
                        '        & "  a.soship_date, " _
                        '        & "  b.so_code, " _
                        '        & "  c.ptnr_name, " _
                        '        & "  b.so_date, " _
                        '        & "  b.so_en_id,en_desc " _
                        '        & "FROM " _
                        '        & "  public.soship_mstr a " _
                        '        & "  INNER JOIN public.so_mstr b ON (a.soship_so_oid = b.so_oid) " _
                        '        & "  INNER JOIN public.soshipd_det d ON (a.soship_oid = d.soshipd_soship_oid) " _
                        '        & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                        '        & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                        '        & "WHERE " _
                        '        & "  d.soshipd_close_line = 'N' AND  " _
                        '        & "  d.soshipd_qty *-1 > " _
                        '        & "  coalesce(d.soshipd_qty_inv, 0) " _
                        '        & "  and soship_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -3, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                        '        & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') " _
                        '        & "  order by soship_date"

                        .SQL = "SELECT distinct " _
                            & "  b.so_code, " _
                            & "  c.ptnr_name, " _
                            & "  b.so_date,en_desc " _
                            & "FROM " _
                            & "  public.so_mstr b " _
                            & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                            & "  INNER JOIN public.sod_det a ON (b.so_oid = a.sod_so_oid) " _
                            & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                              & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            & "WHERE " _
                            & " coalesce(sod_qty_shipment,0) > coalesce(sod_qty_invoice,0) " _
                            & " and so_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -18, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                            & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') and pay_type.code_usr_1 <> '0'" _
                            & "  order by so_date desc"


                        .InitializeCommand()
                        .FillDataSet(ds, "wf")
                        gc_wf.DataSource = ds.Tables("wf")

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
