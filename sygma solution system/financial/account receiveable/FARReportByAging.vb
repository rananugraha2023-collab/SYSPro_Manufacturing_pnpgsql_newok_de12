Imports master_new.ModFunction

Public Class FARReportByAging
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARReportByAging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub load_cb()
        init_le(en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        cu_id.Properties.DataSource = dt_bantu
        cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        cu_id.ItemIndex = 0
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
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "data_ar")
            pgc_ar.DataSource = ds.Tables("data_ar")
            pgc_ar.BestFit()
        End With
    End Sub

    Private Function set_sql() As String
        set_sql = ""
        If RBFilterCU.Checked Then
            If CBOutstandingType.Text = "Before Outstanding Date" Then
                set_sql = "SELECT en_desc, " _
                   & "  b.ptnr_name,ar_code,ar_date, " _
                   & "  c.code_name AS ar_type_name, " _
                   & "  coalesce(a.ar_amount,0) as ar_amount, " _
                   & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
                   & "  coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
                   & " " _
                   & "FROM " _
                   & "  public.ar_mstr a " _
                   & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                   & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                   & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                   & "WHERE " _
                   & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                   & "  a.ar_status IS NULL ) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_due_date<=" _
                   & SetDateNTime00(outstanding_date.Text)

               


            ElseIf CBOutstandingType.Text = "After Outstanding Date" Then
                set_sql = "SELECT  en_desc," _
                   & "  b.ptnr_name,ar_code,ar_date, " _
                   & "  c.code_name AS ar_type_name, " _
                   & "  coalesce(a.ar_amount,0) as ar_amount, " _
                   & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
                   & "  coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
                   & " " _
                   & "FROM " _
                   & "  public.ar_mstr a " _
                   & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                   & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                   & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                   & "WHERE " _
                   & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                   & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_due_date>=" _
                   & SetDateNTime00(outstanding_date.Text)



            ElseIf CBOutstandingType.Text = "All Data" Then

                set_sql = "SELECT en_desc, " _
                    & "  b.ptnr_name,ar_code,ar_date, " _
                    & "  c.code_name AS ar_type_name, " _
                    & "  coalesce(a.ar_amount,0) as ar_amount, " _
                    & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
                    & "  coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
                    & " " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                    & "WHERE " _
                    & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue)



            ElseIf CBOutstandingType.Text = "Between Date" Then
                If Pr_Type.Text = "Due Date" Then
                    set_sql = "SELECT  en_desc," _
                       & "  b.ptnr_name,ar_code,ar_date, " _
                       & "  c.code_name AS ar_type_name, " _
                       & "  coalesce(a.ar_amount,0) as ar_amount, " _
                       & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
                       & "  coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
                       & " " _
                       & "FROM " _
                       & "  public.ar_mstr a " _
                       & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                       & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                       & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                       & "WHERE " _
                       & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                       & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_due_date between  " _
                       & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)


                Else
                    set_sql = "SELECT en_desc, " _
                       & "  b.ptnr_name,ar_code,ar_date, " _
                       & "  c.code_name AS ar_type_name, " _
                       & "  coalesce(a.ar_amount,0) as ar_amount, " _
                       & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
                       & "  coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_date as ar_due_date" _
                       & " " _
                       & "FROM " _
                       & "  public.ar_mstr a " _
                       & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                       & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                       & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                       & "WHERE " _
                       & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                       & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_date between  " _
                       & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)


                End If

            End If

        Else
            If CBOutstandingType.Text = "Before Outstanding Date" Then
                set_sql = "SELECT  en_desc," _
                    & "  b.ptnr_name,ar_code,ar_date, " _
                    & "  c.code_name AS ar_type_name, " _
                    & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                    & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                    & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                    & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                    & "WHERE " _
                    & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL) and a.ar_due_date<=" _
                    & SetDateNTime00(outstanding_date.Text)



            ElseIf CBOutstandingType.Text = "After Outstanding Date" Then
                set_sql = "SELECT en_desc, " _
                    & "  b.ptnr_name,ar_code,ar_date, " _
                    & "  c.code_name AS ar_type_name, " _
                   & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                    & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                    & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                    & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                    & "WHERE " _
                    & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL) and a.ar_due_date>=" _
                    & SetDateNTime00(outstanding_date.Text)



            ElseIf CBOutstandingType.Text = "All Data" Then
                set_sql = "SELECT en_desc, " _
                    & "  b.ptnr_name,ar_code,ar_date, " _
                    & "  c.code_name AS ar_type_name, " _
                    & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                    & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                    & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                    & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                    & "WHERE " _
                    & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL) "

            

            ElseIf CBOutstandingType.Text = "Between Date" Then
                If Pr_Type.Text = "Due Date" Then
                    set_sql = "SELECT en_desc, " _
                       & "  b.ptnr_name,ar_code,ar_date, " _
                       & "  c.code_name AS ar_type_name, " _
                       & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                       & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                       & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                       & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                       & " " _
                       & "FROM " _
                       & "  public.ar_mstr a " _
                       & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                       & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                       & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                       & "WHERE " _
                       & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                       & "  a.ar_status IS NULL)  and a.ar_due_date between  " _
                       & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)

                   

                Else
                    set_sql = "SELECT en_desc, " _
                       & "  b.ptnr_name,ar_code,ar_date, " _
                       & "  c.code_name AS ar_type_name, " _
                       & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                       & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                       & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                       & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_date as ar_due_date " _
                       & " " _
                       & "FROM " _
                       & "  public.ar_mstr a " _
                       & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                       & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                       & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                       & "WHERE " _
                       & "  coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and (lower(a.ar_status) <> 'c' OR  " _
                       & "  a.ar_status IS NULL)   and a.ar_date between  " _
                       & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)

                  

                End If
            End If
        End If

        If CeAllEntity.EditValue = False Then
            set_sql = set_sql & " and ar_en_id = " + en_id.EditValue.ToString
        Else
            set_sql = set_sql & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + " ) "
        End If

        Return set_sql
    End Function
End Class
