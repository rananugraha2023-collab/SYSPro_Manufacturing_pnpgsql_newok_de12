Imports master_new.ModFunction

Public Class FPointDubaiReport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARReportByAging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CDate("01/01/" & Now.Year)
        pr_txttglakhir.DateTime = Now

        add_column_copy(gv_detail, "Sales", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Point", "point_dubai", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Sub load_cb()
        init_le(en_id, "en_mstr")
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
                            load_ar(objload)
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            With objload
                                .SQL = "SELECT  " _
                                    & "  sum(b.sod_qty * b.sod_commision) AS point_dubai, " _
                                    & "  c.ptnr_name " _
                                    & "FROM " _
                                    & "  public.so_mstr a " _
                                    & "  INNER JOIN public.sod_det b ON (a.so_oid = b.sod_so_oid) " _
                                    & "  INNER JOIN public.ptnr_mstr c ON (a.so_sales_person = c.ptnr_id) " _
                                    & "WHERE " _
                                    & "b.sod_commision is not null and " _
                                    & "  a.so_date BETWEEN " & SetDate(pr_txttglawal.EditValue) & " AND " & SetDate(pr_txttglakhir.EditValue) _
                                    & " and a.so_en_id=" & SetInteger(en_id.EditValue) & " and so_trans_id<>'X' group by ptnr_name order by point_dubai desc limit 3"

                              
                                .InitializeCommand()
                                .FillDataSet(ds, "data_detail")
                                gc_detail.DataSource = ds.Tables("data_detail")
                                gv_detail.BestFitColumns()
                            End With
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub load_ar(ByVal par_obj As Object)

        pgc_ar.DataSource = Nothing
        pgc_ar.DataMember = Nothing

        With par_obj
            .SQL = "SELECT  " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  b.sod_qty *  b.sod_commision as point_dubai, " _
                & "  a.so_date,ptnr_name as so_sales " _
                & "FROM " _
                & "  public.so_mstr a " _
                & "  INNER JOIN public.sod_det b ON (a.so_oid = b.sod_so_oid) " _
                & "  INNER JOIN public.pt_mstr c ON (b.sod_pt_id = c.pt_id) " _
                  & "  INNER JOIN public.ptnr_mstr d ON (a.so_sales_person = d.ptnr_id) " _
                & "WHERE " _
                & "  a.so_date BETWEEN " & SetDate(pr_txttglawal.EditValue) & " AND " & SetDate(pr_txttglakhir.EditValue) _
                & " and a.so_en_id=" & SetInteger(en_id.EditValue) & " and so_trans_id<>'X'"


            .InitializeCommand()
            .FillDataSet(ds, "data_ar")
            pgc_ar.DataSource = ds.Tables("data_ar")
            pgc_ar.BestFit()
        End With
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim _file As String = AskSaveAsFile("Excel Files | *.xls*")

        If xtc_master.SelectedTabPageIndex = 0 Then
            pgc_ar.OptionsPrint.MergeColumnFieldValues = False
            pgc_ar.OptionsPrint.MergeRowFieldValues = False
            pgc_ar.ExportToXls(_file & ".xls")
        Else
            gv_detail.ExportToXls(_file & ".xls")
        End If
        OpenFile(_file & ".xls")
       
        Box("Success")
    End Function
End Class
