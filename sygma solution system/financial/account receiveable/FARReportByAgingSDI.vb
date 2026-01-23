Imports master_new.ModFunction

Public Class FARReportByAgingSDI
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARReportByAging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        RBFilterCU.Checked = True
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
        If RBFilterCU.Checked Then
            If CBOutstandingType.Text = "Before Outstanding Date" Then
                set_sql = "select ar_en_id, en_desc, " _
                        & "ptnr_name, " _
                        & "code_name as ar_type_name,    " _
                        & "sokp_amount as ar_amount, " _
                        & "coalesce(sokp_amount_pay,0) as ar_pay_amount, " _
                        & "sokp_amount - coalesce(sokp_amount_pay,0) as ar_outstanding, " _
                        & "sokp_due_date as ar_due_date " _
                        & "from public.ar_mstr " _
                        & "inner join ptnr_mstr ON ar_bill_to = ptnr_id " _
                        & "inner join code_mstr ON ar_type = code_id " _
                        & "inner join arso_so on arso_ar_oid = ar_oid " _
                        & "inner join sokp_piutang on sokp_so_oid = arso_so_oid " _
                        & "inner join en_mstr on en_id  = ar_en_id " _
                        & "WHERE " _
                        & " coalesce(ar_amount,0.0)-coalesce(ar_pay_amount,0.0) > 0.0 and  (lower(ar_status) <> 'c' OR  " _
                        & "  ar_status IS NULL) and ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and sokp_due_date<=" _
                        & SetDateNTime00(outstanding_date.Text) _
                        & " and ar_en_id in (select user_en_id from tconfuserentity " _
                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            ElseIf CBOutstandingType.Text = "After Outstanding Date" Then
                set_sql = "select ar_en_id, en_desc, " _
                        & "ptnr_name, " _
                        & "code_name as ar_type_name,    " _
                        & "sokp_amount as ar_amount, " _
                        & "coalesce(sokp_amount_pay,0) as ar_pay_amount, " _
                        & "sokp_amount - coalesce(sokp_amount_pay,0) as ar_outstanding, " _
                        & "sokp_due_date as ar_due_date " _
                        & "from public.ar_mstr " _
                        & "inner join ptnr_mstr ON ar_bill_to = ptnr_id " _
                        & "inner join code_mstr ON ar_type = code_id " _
                        & "inner join arso_so on arso_ar_oid = ar_oid " _
                        & "inner join sokp_piutang on sokp_so_oid = arso_so_oid " _
                        & "inner join en_mstr on en_id  = ar_en_id " _
                        & "WHERE " _
                        & " coalesce(ar_amount,0.0)-coalesce(ar_pay_amount,0.0) > 0.0 and  (lower(ar_status) <> 'c' OR  " _
                        & "  ar_status IS NULL) and ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and sokp_due_date>=" _
                        & SetDateNTime00(outstanding_date.Text) _
                        & " and ar_en_id in (select user_en_id from tconfuserentity " _
                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            Else
                set_sql = "select ar_en_id, en_desc, " _
                        & "ptnr_name, " _
                        & "code_name as ar_type_name,    " _
                        & "sokp_amount as ar_amount, " _
                        & "coalesce(sokp_amount_pay,0) as ar_pay_amount, " _
                        & "sokp_amount - coalesce(sokp_amount_pay,0) as ar_outstanding, " _
                        & "sokp_due_date as ar_due_date " _
                        & "from public.ar_mstr " _
                        & "inner join ptnr_mstr ON ar_bill_to = ptnr_id " _
                        & "inner join code_mstr ON ar_type = code_id " _
                        & "inner join arso_so on arso_ar_oid = ar_oid " _
                        & "inner join sokp_piutang on sokp_so_oid = arso_so_oid " _
                        & "inner join en_mstr on en_id  = ar_en_id " _
                        & "WHERE " _
                        & " coalesce(ar_amount,0.0)-coalesce(ar_pay_amount,0.0) > 0.0 and  (lower(ar_status) <> 'c' OR  " _
                        & "  ar_status IS NULL) and ar_cu_id=" & SetInteger(cu_id.EditValue) _
                        & " and ar_en_id in (select user_en_id from tconfuserentity " _
                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            End If

        Else
            If CBOutstandingType.Text = "Before Outstanding Date" Then
                set_sql = "SELECT  " _
                    & "  b.ptnr_name, " _
                    & "  c.code_name AS ar_type_name, " _
                    & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                    & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                    & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                    & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "WHERE " _
                    & " coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and  lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL and a.ar_due_date<=" _
                    & SetDateNTime00(outstanding_date.Text)
            ElseIf CBOutstandingType.Text = "After Outstanding Date" Then
                set_sql = "SELECT  " _
                    & "  b.ptnr_name, " _
                    & "  c.code_name AS ar_type_name, " _
                   & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                    & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                    & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                    & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "WHERE " _
                    & " coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and  lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL and a.ar_due_date>=" _
                    & SetDateNTime00(outstanding_date.Text)
            Else
                set_sql = "SELECT  " _
                    & "  b.ptnr_name, " _
                    & "  c.code_name AS ar_type_name, " _
                    & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
                    & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
                    & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
                    & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
                    & "FROM " _
                    & "  public.ar_mstr a " _
                    & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                    & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                    & "WHERE " _
                    & " coalesce(a.ar_amount,0.0)-coalesce(a.ar_pay_amount,0.0) > 0.0 and  lower(a.ar_status) <> 'c' OR  " _
                    & "  a.ar_status IS NULL "
            End If
        End If

        Return set_sql
    End Function
End Class
