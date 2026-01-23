Imports master_new.ModFunction

Public Class FARPendingInvoice
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARPendingInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            set_sql = "SELECT  en_desc, " _
                & "  a.soshipd_qty * -1 as soshipd_qty, " _
                & "  coalesce(a.soshipd_qty_inv, 0) as soshipd_qty_inv, " _
                & "  (a.soshipd_qty - coalesce(a.soshipd_qty_inv, 0)) * -1 as qty_accrue, " _
                & "  ((a.soshipd_qty - coalesce(a.soshipd_qty_inv, 0)) *  (coalesce(b.sod_price,0) - (b.sod_price * b.sod_disc)) ) * -1  as ar_accrue, " _
                & "  d.soship_date, " _
                & "  b.sod_price, " _
                & "  c.so_exc_rate, " _
                & "  c.so_code, " _
                & "  c.so_cu_id, " _
                & "  pay_type.code_usr_1 as pay_type_interval, " _
                & "  e.ptnr_name " _
                & "FROM " _
                & "  public.soshipd_det a " _
                & "  INNER JOIN public.sod_det b ON (a.soshipd_sod_oid = b.sod_oid) " _
                & "  INNER JOIN public.so_mstr c ON (b.sod_so_oid = c.so_oid) " _
                & "  INNER JOIN public.soship_mstr d ON (a.soshipd_soship_oid = d.soship_oid) " _
                & "  INNER JOIN public.ptnr_mstr e ON (c.so_ptnr_id_sold = e.ptnr_id) " _
                & "  INNER JOIN public.en_mstr ON (en_id = so_en_id) " _
                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                & "WHERE " _
                & "  (a.soshipd_qty *-1) > coalesce(a.soshipd_qty_inv, 0) and c.so_cu_id = " & SetInteger(cu_id.EditValue) _
                & " and pay_type.code_usr_1 <> '0' " _
                & " and so_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

           
        Else
            set_sql = "SELECT en_desc, " _
                & "  a.soshipd_qty * -1 as soshipd_qty, " _
                & "  coalesce(a.soshipd_qty_inv, 0) as soshipd_qty_inv, " _
                & "  (a.soshipd_qty - coalesce(a.soshipd_qty_inv, 0)) * -1 as qty_accrue, " _
                & "  ((a.soshipd_qty - coalesce(a.soshipd_qty_inv, 0)) * (coalesce(b.sod_price,0) - (b.sod_price * b.sod_disc)) * coalesce(soship_exc_rate,1))* -1 as ar_accrue, " _
                & "  d.soship_date, " _
                & "  b.sod_price, " _
                & "  soship_exc_rate, " _
                & "  c.so_code, " _
                & "  c.so_cu_id, " _
                & "  pay_type.code_usr_1 as pay_type_interval, " _
                & "  e.ptnr_name " _
                & "FROM " _
                & "  public.soshipd_det a " _
                & "  INNER JOIN public.sod_det b ON (a.soshipd_sod_oid = b.sod_oid) " _
                & "  INNER JOIN public.so_mstr c ON (b.sod_so_oid = c.so_oid) " _
                & "  INNER JOIN public.soship_mstr d ON (a.soshipd_soship_oid = d.soship_oid) " _
                & "  INNER JOIN public.ptnr_mstr e ON (c.so_ptnr_id_sold = e.ptnr_id) " _
                & "  INNER JOIN public.en_mstr ON (en_id = so_en_id) " _
                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                & "WHERE " _
                & "  (a.soshipd_qty * -1) > coalesce(a.soshipd_qty_inv, 0)" _
                & " and pay_type.code_usr_1 <> '0' " _
                & " and so_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        End If

        Return set_sql
    End Function

    
End Class
