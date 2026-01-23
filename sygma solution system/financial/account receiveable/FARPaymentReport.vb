Imports npgsql
Imports master_new.ModFunction

Public Class FARPaymentReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Payment Number", "arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Payment Date", "arpay_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "AR Payment Eff. Date", "arpay_eff_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Bank / Kas", "bk_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "AR Payment Detail Amount", "arpayd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_view1, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "AR Eff. Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "AR Amount", "ar_amount", DevExpress.Utils.HorzAlignment.Far)
        add_column_copy(gv_view1, "AR Pay Amount", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far)
        add_column_copy(gv_view1, "AR Outstanding", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far)
        
        
        add_column_copy(gv_view1, "AR Payment Remarks", "arpay_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Payment User", "arpay_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Payment Date", "arpay_add_date", DevExpress.Utils.HorzAlignment.Center)
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
                                & "  en_desc, " _
                                & "  ptnr_name,  " _
                                & "  arpay_code, " _
                                & "  cu_name, " _
                                & "  bk_name, " _
                                & "  arpay_date, " _
                                & "  arpay_eff_date, " _
                                & "  arpayd_amount, " _
                                & "  arpayd_remarks, " _
                                & "  ar_code, " _
                                & "  ar_date, " _
                                & "  ar_eff_date, " _
                                & "  ar_amount, " _
                                & "  ar_pay_amount, " _
                                & "  ar_amount - coalesce(ar_pay_amount,0) as ar_outstanding, " _
                                & "  arpay_add_by, " _
                                & "  arpay_add_date " _
                                & "FROM  " _
                                & "  public.arpay_payment " _
                                & "  inner join en_mstr on en_id = arpay_en_id " _
                                & "  inner join ptnr_mstr on ptnr_id = arpay_bill_to " _
                                & "  inner join cu_mstr on cu_id = arpay_cu_id " _
                                & "  inner join bk_Mstr on bk_id = arpay_bk_id " _
                                & "  inner join arpayd_det on arpayd_arpay_oid = arpay_oid " _
                                & "  inner join ar_mstr on ar_oid = arpayd_ar_oid " _
                                & "  where arpay_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and arpay_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & "  and arpay_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & "  order by arpay_dt, ar_dt"
                                
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

    Private Sub LabelControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelControl1.Click

    End Sub
End Class
