Imports master_new.ModFunction

Public Class FPickListPrint
    Dim dt_bantu As DataTable
    Dim func_data As New function_data

    Private Sub FTaxInvoicePrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_cu_mstr())
        'le_cur.Properties.DataSource = dt_bantu
        'le_cur.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        'le_cur.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        'le_cur.ItemIndex = 0
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FWORoutingSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
       
    End Sub

    Private Sub le_cur_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        be_first.Text = ""
        'be_to.Text = ""
    End Sub

    Private Function set_sql_detail_gabungan() As String
        'set_sql_detail_gabungan = "SELECT  " _
        '    & "  ti_mstr.ti_oid, " _
        '    & "  ti_mstr.ti_dom_id, " _
        '    & "  ti_mstr.ti_en_id, " _
        '    & "  ti_mstr.ti_add_by, " _
        '    & "  ti_mstr.ti_add_date, " _
        '    & "  ti_mstr.ti_upd_by, " _
        '    & "  ti_mstr.ti_upd_date, " _
        '    & "  ti_mstr.ti_dt, " _
        '    & "  ti_mstr.ti_code, " _
        '    & "  ti_mstr.ti_date, " _
        '    & "  ti_mstr.ti_sign_id, " _
        '    & "  ti_mstr.ti_ptnr_id, " _
        '    & "  ti_mstr.ti_status, " _
        '    & "  ti_mstr.ti_customer_type, " _
        '    & "  ti_mstr.ti_area, " _
        '    & "  ti_mstr.ti_ppn_type, " _
        '    & "  ti_mstr.ti_ptnr_addr_oid, " _
        '    & "  ti_mstr.ti_tran_id, " _
        '    & "  ti_mstr.ti_trans_id, " _
        '    & "  ti_mstr.ti_rev, " _
        '    & "  ti_mstr.ti_unstrikeout, " _
        '    & "  ti_mstr.ti_ti_oid, " _
        '    & "  cmaddr_name,  " _
        '    & "  cmaddr_npwp, " _
        '    & "  cmaddr_tax_line_1, " _
        '    & "  cmaddr_tax_line_2, " _
        '    & "  cmaddr_tax_line_3, " _
        '    & "  sign_mstr.code_name as sign_name, " _
        '    & "  ptnr_name, " _
        '    & "  ptnra_line_1, " _
        '    & "  ptnra_line_2, " _
        '    & "  ptnra_line_3, " _
        '    & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
        '    & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
        '    & "  ti_mstr.ti_cu_id, " _
        '    & "  cu_code, " _
        '    & "  tm.ti_code as ti_code_pengganti,  " _
        '    & "  tm.ti_date as ti_date_pengganti, " _
        '    & "  tip_seq, " _
        '    & "  tip_pt_id, " _
        '    & "  pt_code, " _
        '    & "  pt_desc1, " _
        '    & "  pt_desc2, " _
        '    & "  trim(pt_desc1 || ' ' || pt_desc2) as pt_desc, " _
        '    & "  tip_qty, " _
        '    & "  tip_price, " _
        '    & "  tip_ppn, " _
        '    & "  tip_total, " _
        '    & "  tip_disc, " _
        '    & "  tip_qty * tip_price as qty_price, " _
        '    & "  tip_qty * tip_price as qty_price_usd, " _
        '    & "  tip_qty * tip_disc as qty_disc, " _
        '    & "  tip_qty * tip_disc as qty_disc_usd, " _
        '    & "  tip_qty * tip_ppn as qty_ppn, " _
        '    & "  tip_qty * tip_ppn as qty_ppn_usd, " _
        '    & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc, " _
        '    & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc_usd, " _
        '    & "  'Reference : ' || coalesce(ti_mstr.ti_ref_ar,ti_mstr.ti_ref_so) as ar_code, " _
        '    & "  tip_tax_rate, " _
        '    & "  99 as jml_ar " _
        '    & "FROM  " _
        '    & "  ti_mstr " _
        '    & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
        '    & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
        '    & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
        '    & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
        '    & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
        '    & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
        '    & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
        '    & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
        '    & "  inner join pt_mstr on pt_id = tip_pt_id" _
        '    & "  where ti_mstr.ti_code >= '" + be_first.Text + "'" _
        '    & "  and ti_mstr.ti_code <= '" + be_to.Text + "'" _
        '    & "  and coalesce(ti_mstr.ti_trans_id,'I') = 'I' and ti_mstr.ti_sign_id=" & le_sign.EditValue _
        '    & "  and ti_mstr.ti_so_cash ~~* " + SetSetring(IIf(ce_so_cash.Checked = True, "Y", "N")) _
        '    & "  order by ti_mstr.ti_code, tip_seq "

        Return set_sql_detail_gabungan
    End Function

    Private Function set_sql_group() As String
        'set_sql_group = "SELECT  " _
        '    & "  ti_mstr.ti_oid, " _
        '    & "  ti_mstr.ti_dom_id, " _
        '    & "  ti_mstr.ti_en_id, " _
        '    & "  ti_mstr.ti_add_by, " _
        '    & "  ti_mstr.ti_add_date, " _
        '    & "  ti_mstr.ti_upd_by, " _
        '    & "  ti_mstr.ti_upd_date, " _
        '    & "  ti_mstr.ti_dt, " _
        '    & "  ti_mstr.ti_code, " _
        '    & "  ti_mstr.ti_date, " _
        '    & "  ti_mstr.ti_sign_id, " _
        '    & "  ti_mstr.ti_ptnr_id, " _
        '    & "  ti_mstr.ti_status, " _
        '    & "  ti_mstr.ti_customer_type, " _
        '    & "  ti_mstr.ti_area, " _
        '    & "  ti_mstr.ti_ppn_type, " _
        '    & "  case ti_mstr.ti_ppn_type " _
        '    & "  when 'E' then 'Penjualan Buku' " _
        '    & "  when 'A' then 'PPN Dibayar' " _
        '    & "  end as pt_desc1, " _
        '    & "  ti_mstr.ti_ptnr_addr_oid, " _
        '    & "  ti_mstr.ti_tran_id, " _
        '    & "  ti_mstr.ti_trans_id, " _
        '    & "  ti_mstr.ti_rev, " _
        '    & "  ti_mstr.ti_unstrikeout, " _
        '    & "  ti_mstr.ti_ti_oid, " _
        '    & "  cmaddr_name,  " _
        '    & "  cmaddr_npwp, " _
        '    & "  cmaddr_tax_line_1, " _
        '    & "  cmaddr_tax_line_2, " _
        '    & "  cmaddr_tax_line_3, " _
        '    & "  sign_mstr.code_name as sign_name, " _
        '    & "  ptnr_name, " _
        '    & "  ptnra_line_1, " _
        '    & "  ptnra_line_2, " _
        '    & "  ptnra_line_3, " _
        '    & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
        '    & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
        '    & "  ti_mstr.ti_cu_id, " _
        '    & "  cu_code, " _
        '    & "  tm.ti_code as ti_code_pengganti,  " _
        '    & "  tm.ti_date as ti_date_pengganti, " _
        '    & "  sum(tip_qty * tip_price) as qty_price, " _
        '    & "  sum(tip_qty * tip_price) as qty_price_usd, " _
        '    & "  sum(tip_qty * tip_disc) as qty_disc, " _
        '    & "  sum(tip_qty * tip_disc) as qty_disc_usd, " _
        '    & "  sum(tip_qty * tip_ppn) as qty_ppn, " _
        '    & "  sum(tip_qty * tip_ppn) as qty_ppn_usd, " _
        '    & "   sum((tip_qty * tip_price) - (tip_qty * tip_disc)) as price_kurang_disc, " _
        '    & "  'Reference : ' || coalesce(ti_mstr.ti_ref_ar,ti_mstr.ti_ref_so) as ar_code, " _
        '    & "  sum((tip_qty * tip_price) - (tip_qty * tip_disc)) as price_kurang_disc_usd " _
        '    & "FROM  " _
        '    & "  ti_mstr " _
        '    & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
        '    & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
        '    & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
        '    & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
        '    & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
        '    & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
        '    & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
        '    & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
        '    & "  inner join pt_mstr on pt_id = tip_pt_id" _
        '    & "  where ti_mstr.ti_code >= '" + be_first.Text + "'" _
        '    & "  and ti_mstr.ti_code <= '" + be_to.Text + "'" _
        '    & "  and ti_mstr.ti_so_cash ~~* " + SetSetring(IIf(ce_so_cash.Checked = True, "Y", "N")) _
        '    & "  and coalesce(ti_mstr.ti_trans_id,'I') = 'I' and ti_mstr.ti_sign_id=" & le_sign.EditValue _
        '    & "  group by  " _
        '    & "  ti_mstr.ti_oid, " _
        '    & "  ti_mstr.ti_dom_id, " _
        '    & "  ti_mstr.ti_en_id, " _
        '    & "  ti_mstr.ti_add_by, " _
        '    & "  ti_mstr.ti_add_date, " _
        '    & "  ti_mstr.ti_upd_by, " _
        '    & "  ti_mstr.ti_upd_date, " _
        '    & "  ti_mstr.ti_dt, " _
        '    & "  ti_mstr.ti_code, " _
        '    & "  ti_mstr.ti_date, " _
        '    & "  ti_mstr.ti_sign_id, " _
        '    & "  ti_mstr.ti_ptnr_id, " _
        '    & "  ti_mstr.ti_status, " _
        '    & "  ti_mstr.ti_customer_type, " _
        '    & "  ti_mstr.ti_area, " _
        '    & "  ti_mstr.ti_ppn_type, " _
        '    & "  ti_mstr.ti_ptnr_addr_oid, " _
        '    & "  ti_mstr.ti_tran_id, " _
        '    & "  ti_mstr.ti_trans_id, " _
        '    & "  ti_mstr.ti_rev, " _
        '    & "  ti_mstr.ti_unstrikeout, " _
        '    & "  ti_mstr.ti_ti_oid, " _
        '    & "  cmaddr_name,  " _
        '    & "  cmaddr_npwp, " _
        '    & "  cmaddr_tax_line_1, " _
        '    & "  cmaddr_tax_line_2, " _
        '    & "  cmaddr_tax_line_3, " _
        '    & "  sign_mstr.code_name, " _
        '    & "  ptnr_name, " _
        '    & "  ptnra_line_1, " _
        '    & "  ptnra_line_2, " _
        '    & "  ptnra_line_3, " _
        '    & "  ptnr_npwp, " _
        '    & "  ti_mstr.ti_cu_id, " _
        '    & "  cu_code, " _
        '    & "  tm.ti_code,  " _
        '    & "  tm.ti_date " _
        '    & "  order by ti_mstr.ti_code "

        Return set_sql_group
    End Function

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        'If ce_show_detail.Checked = True Then
        '    _sql = set_sql_detail_gabungan()
        'Else
        '    _sql = set_sql_group()
        'End If

        Dim rpt As Object = Nothing

        rpt = New XRTaxInvoice
        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ce_ppn_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        be_first.Text = ""
        ' be_to.Text = ""
    End Sub

    Private Sub ce_so_cash_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        be_first.Text = ""
        ' be_to.Text = ""
    End Sub

    Private Sub ti_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_code_mstr(le_entity.EditValue, "fp_sign_user"))
        'le_sign.Properties.DataSource = dt_bantu
        'le_sign.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'le_sign.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'le_sign.ItemIndex = 0

    End Sub
End Class
