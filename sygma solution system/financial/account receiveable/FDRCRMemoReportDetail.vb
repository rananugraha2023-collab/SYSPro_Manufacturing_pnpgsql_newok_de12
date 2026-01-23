Imports npgsql
Imports master_new.ModFunction

Public Class FDRCRMemoReportDetail
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FDRCRMemoReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
     
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Effective Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Exc. Rate", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Due Date", "ar_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "Expected Date", "ar_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Taxable", "ar_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Tax Include", "ar_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Type", "ar_type_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Prepayment", "ar_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Prepayment IDR", "ar_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Account Code Prepayment", "ar_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Account Name Prepayment", "ar_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Remarks", "ar_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Total", "ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Total Payment", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Outstanding Payment", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Total IDR", "ar_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Total Payment IDR", "ar_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Outstanding Payment IDR", "ar_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Status", "ar_status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view1, "arso_ar_oid", False)
        add_column(gv_view1, "sokp_ar_oid", False)
        add_column(gv_view1, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Seq", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Payment Amount", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Payment Date", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Status", "sokp_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "User Create", "ar_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "ar_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "User Update", "ar_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "ar_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

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
                                & "  public.ar_mstr.ar_oid, " _
                                & "  public.ar_mstr.ar_dom_id, " _
                                & "  public.ar_mstr.ar_en_id, " _
                                & "  public.ar_mstr.ar_add_by, " _
                                & "  public.ar_mstr.ar_add_date, " _
                                & "  public.ar_mstr.ar_upd_by, " _
                                & "  public.ar_mstr.ar_upd_date, " _
                                & "  public.ar_mstr.ar_code, " _
                                & "  public.ar_mstr.ar_date, " _
                                & "  public.ar_mstr.ar_bill_to, " _
                                & "  public.ar_mstr.ar_cu_id, " _
                                & "  public.ar_mstr.ar_exc_rate, " _
                                & "  public.ar_mstr.ar_credit_term, " _
                                & "  public.ar_mstr.ar_eff_date, " _
                                & "  public.ar_mstr.ar_disc_date, " _
                                & "  public.ar_mstr.ar_expt_date, " _
                                & "  public.ar_mstr.ar_ac_id, " _
                                & "  public.ar_mstr.ar_sb_id, " _
                                & "  public.ar_mstr.ar_cc_id, " _
                                & "  public.ar_mstr.ar_type, " _
                                & "  public.ar_mstr.ar_taxable, " _
                                & "  public.ar_mstr.ar_tax_inc, " _
                                & "  public.ar_mstr.ar_tax_class_id, " _
                                & "  public.ar_mstr.ar_pay_prepaid, " _
                                & "  public.ar_mstr.ar_pay_prepaid * ar_exc_rate as ar_pay_prepaid_idr, " _
                                & "  public.ar_mstr.ar_ac_prepaid, " _
                                & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                                & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                                & "  public.ar_mstr.ar_amount, " _
                                & "  public.ar_mstr.ar_pay_amount, " _
                                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                                & "  public.ar_mstr.ar_amount * ar_exc_rate as ar_amount_idr, " _
                                & "  public.ar_mstr.ar_pay_amount * ar_exc_rate as ar_pay_amount_idr, " _
                                & "  (public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount) * ar_exc_rate as ar_outstanding_idr, " _
                                & "  public.ar_mstr.ar_remarks, " _
                                & "  public.ar_mstr.ar_status, " _
                                & "  public.ar_mstr.ar_dt, " _
                                & "  public.ar_mstr.ar_due_date, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  public.cu_mstr.cu_name, " _
                                & "  credit_term_mstr.code_name as credit_terms_name, " _
                                & "  public.ac_mstr.ac_code, " _
                                & "  public.ac_mstr.ac_name, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.cc_mstr.cc_desc, " _
                                & "  public.ar_mstr.ar_bk_id, " _
                                & "  public.bk_mstr.bk_name, " _
                                & "  ar_type.code_name as ar_type_name, " _
                                & "  taxclass_mstr.code_name as taxclass_name, " _
                                & "  sokp_oid, " _
                                & "  arso_ar_oid, " _
                                & "  arso_so_code, " _
                                & "  sokp_so_oid, " _
                                & "  sokp_seq, " _
                                & "  sokp_amount, " _
                                & "  sokp_due_date, " _
                                & "  sokp_amount_pay, " _
                                & "  sokp_description " _
                                & "FROM " _
                                & "  public.ar_mstr " _
                                & "  inner join public.arso_so on ar_mstr.ar_oid = arso_ar_oid " _
                                & "  inner join public.sokp_piutang on arso_so_oid = sokp_so_oid" _
                                & "  LEFT OUTER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                                & "  LEFT OUTER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                                & "  LEFT OUTER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                                & "  LEFT OUTER JOIN public.code_mstr credit_term_mstr ON (public.ar_mstr.ar_credit_term = credit_term_mstr.code_id) " _
                                & "  LEFT OUTER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                                & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ar_mstr.ar_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                                & "  LEFT OUTER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                                & "  LEFT OUTER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                                & "  LEFT OUTER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                                & "  LEFT OUTER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = bk_mstr.bk_id) " _
                                & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.ar_mstr.ar_tax_class_id = taxclass_mstr.code_id) " _
                                & " where ar_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & " and ar_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
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
