Imports master_new.ModFunction

Public Class FARReportByTop
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub ARReportByTop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
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
        pgc_master.DataSource = Nothing
        pgc_master.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "data_ar")
            pgc_master.DataSource = ds.Tables("data_ar")
            pgc_master.BestFit()
        End With
    End Sub

    Private Function set_sql() As String
        If RBFilterCU.Checked Then
            If CBPaid.Checked Then
                set_sql = "SELECT  en_desc, " _
                  & "  b.ptnr_name, " _
                  & "  c.code_name AS ar_type_name, " _
                  & "  sum(coalesce(a.ar_amount,0)) as ar_amount, " _
                  & "  sum(coalesce(a.ar_pay_amount,0)) as ar_pay_amount, " _
                  & "  sum(coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0)) as ar_outstanding " _
                  & " " _
                  & "FROM " _
                  & "  public.ar_mstr a " _
                  & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                  & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                  & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                  & "WHERE " _
                  & "  lower(a.ar_status) = 'c' and a.ar_cu_id=" & SetInteger(cu_id.EditValue) _
                  & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            Else
                set_sql = "SELECT en_desc, " _
                  & "  b.ptnr_name, " _
                  & "  c.code_name AS ar_type_name, " _
                  & "  sum(coalesce(a.ar_amount,0)) as ar_amount, " _
                  & "  sum(coalesce(a.ar_pay_amount,0)) as ar_pay_amount, " _
                  & "  sum(coalesce(a.ar_amount,0)-coalesce(a.ar_pay_amount,0)) as ar_outstanding " _
                  & " " _
                  & "FROM " _
                  & "  public.ar_mstr a " _
                  & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                  & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                  & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                  & "WHERE " _
                  & "  (lower(a.ar_status) <> 'c' OR  " _
                  & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) _
                  & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            End If
            set_sql += " group by en_desc, ar_type_name, ptnr_name order by ar_amount desc limit " & SetInteger(CBoTop.Text)
        Else
            If CBPaid.Checked Then
                set_sql = "SELECT  en_desc, " _
                  & "  b.ptnr_name, " _
                  & "  c.code_name AS ar_type_name, " _
                  & "  sum(coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) AS ar_amount, " _
                  & "  sum(f_get_ar_payment(a.ar_oid)) AS ar_pay_amount, " _
                  & "  sum((coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - (f_get_ar_payment(a.ar_oid))) AS ar_outstanding " _
                  & "FROM " _
                  & "  public.ar_mstr a " _
                  & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                  & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                  & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                  & "WHERE " _
                  & "  lower(a.ar_status) = 'c' " _
                  & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            Else
                set_sql = "SELECT  en_desc, " _
                  & "  b.ptnr_name, " _
                  & "  c.code_name AS ar_type_name, " _
                  & "  sum(coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) AS ar_amount, " _
                  & "  sum(f_get_ar_payment(a.ar_oid)) AS ar_pay_amount, " _
                  & "  sum((coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - (f_get_ar_payment(a.ar_oid))) AS ar_outstanding " _
                  & "FROM " _
                  & "  public.ar_mstr a " _
                  & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
                  & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
                  & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
                  & "WHERE " _
                  & "  (lower(a.ar_status) <> 'c' OR  " _
                  & "  a.ar_status IS NULL) " _
                  & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            End If
            set_sql += " group by en_desc, ar_type_name,ptnr_name order by ar_amount desc limit " & SetInteger(CBoTop.Text)
        End If

        Return set_sql
    End Function
End Class
