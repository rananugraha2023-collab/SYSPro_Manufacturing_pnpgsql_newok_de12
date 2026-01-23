Imports npgsql
Imports master_new.ModFunction

Public Class FARPSDueDateReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Sales", "sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "AR Amount", "ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "AR Pay Amount", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "AR Outstanding", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

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
                                & "  distinct public.ar_mstr.ar_code, ar_date,x.ptnr_name,y.ptnr_name as sales, ar_amount,ar_pay_amount,ar_amount-ar_pay_amount as ar_outstanding, en_desc " _
                                & "FROM " _
                                & "  public.ar_mstr " _
                                & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
                                & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
                                & "  INNER JOIN public.en_mstr ON (ar_en_id = en_id) " _
                                & "  INNER JOIN public.ptnr_mstr x ON (public.ar_mstr.ar_bill_to = x.ptnr_id) " _
                                & "    INNER JOIN public.ptnr_mstr y ON (so_sales_person = y.ptnr_id) " _
                                & "WHERE " _
                                & "  public.ar_mstr.ar_status  is null and  x.ptnr_is_ps='Y'  " _
                                & " and ar_date > '21/08/2017' and    datediff(CURRENT_DATE,ar_date)>3 "

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
