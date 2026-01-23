Public Class FFakturPajakPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public Shared PageNum As String



    Private Sub FFakturPajakPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        le_cur.Properties.DataSource = dt_bantu
        le_cur.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        le_cur.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        le_cur.ItemIndex = 0

        ce_print_detail.Checked = True

        
        ce_page_number.Checked = True
        ce_page_number.Checked = False
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm._cu_id = le_cur.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm._cu_id = le_cur.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "true as print_comma, " _
            & "true as print_detail, " _
            & "fp_mstr.fp_oid, " _
            & "fp_mstr.fp_dom_id, " _
            & "fp_mstr.fp_en_id, " _
            & "fp_mstr.fp_add_by, " _
            & "fp_mstr.fp_add_date, " _
            & "fp_mstr.fp_upd_by, " _
            & "fp_mstr.fp_upd_date, " _
            & "fp_mstr.fp_code, " _
            & "fp_mstr.fp_pengali_tax, " _
            & "fp_mstr.fp_dt, " _
            & "fp_mstr.fp_date, " _
            & "fp_mstr.fp_sign, " _
            & "fp_mstr.fp_status, " _
            & "fp_mstr.fp_customer_type, " _
            & "fp_mstr.fp_area, " _
            & "fp_mstr.fp_ppn_type, " _
            & "fp_mstr.fp_ptnr_id, " _
            & "fp_mstr.fp_tax_inc, " _
            & "fp_mstr.fp_ar_oid , " _
            & "fp_mstr.fp_unstrikeout , " _
            & "fm.fp_code as fp_code_pengganti, " _
            & "fm.fp_date as fp_date_pengganti, " _
            & "fp_mstr.fp_trans_id, " _
            & "ar_code, " _
            & "ar_date,  " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_npwp, " _
            & "cmaddr_pkp_date, " _
            & "coalesce(ptnr_name_alt,ptnr_name) as ptnr_name, " _
            & "ptnr_npwp, " _
            & "ptnr_nppkp, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ar_cu_id, " _
            & "ars_invoice, " _
            & "ars_invoice_price, " _
            & "ars_tax_class_id, " _
            & "sod_disc, " _
            & "ar_exc_rate, " _
            & "ar_credit_term, " _
            & "cu_code, " _
            & "credit_terms_mstr.code_name as top_name, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "pt_desc2, " _
            & "sod_seq, " _
            & "(select conf_value from conf_file where conf_name = 'faktur_pajak_city') as faktur_pajak_city, " _
            & "ars_tax_inc, " _
            & "ars_invoice, " _
            & "ars_invoice_price, " _
            & "sod_disc, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (ars_invoice_price * ars_invoice) " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) " _
            & "END AS price_ext_idr,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (ars_invoice_price * ars_invoice) " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) " _
            & "END AS price_ext_usd,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ars_invoice * ars_invoice_price * sod_disc " _
            & "WHEN 'Y' THEN ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc) " _
            & "END AS disc_value_idr,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ars_invoice * ars_invoice_price * sod_disc " _
            & "WHEN 'Y' THEN ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc) " _
            & "END AS disc_value_usd,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc)) " _
            & "END AS dpp_value_idr, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc)) " _
            & "END AS dpp_value_usd, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate) * 0.1 " _
            & "WHEN 'Y' THEN ((ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc))) * 0.1 " _
            & "END AS ppn_idr, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate) * 0.1 " _
            & "WHEN 'Y' THEN ((ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc))) * 0.1 " _
            & "END AS ppn_usd " _
            & "FROM  " _
            & "fp_mstr " _
            & "inner join ar_mstr on ar_oid = fp_mstr.fp_ar_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "inner join ptnra_addr on ptnra_oid = fp_ptnr_addr_oid " _
            & "inner join ars_ship on ars_ar_oid = ar_oid " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join code_mstr credit_terms_mstr on credit_terms_mstr.code_id = ar_credit_term  " _
            & "left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "where ars_taxable ~~* 'Y' " _
            & "and fp_mstr.fp_code >= '" + be_first.Text + "' " _
            & "and fp_mstr.fp_code <= '" + be_to.Text + "' " _
            & "and coalesce(fp_mstr.fp_trans_id,'I') = 'I' " _
            & "order by fp_mstr.fp_code, sod_seq "

        Dim rpt As Object

        'If ce_blank.Checked = True Then
        rpt = New XRFakturPajakFormPlain
        'Else
        'rpt = New XRFakturPajakForm
        'End If

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

    Private Sub le_cur_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_cur.EditValueChanged
        be_first.Text = ""
        be_to.Text = ""
    End Sub

    
    Private Sub ce_page_number_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_page_number.CheckedChanged
        'Taufik / 24 Maret 2011  
        XRFakturPajakFormPlain.StatusForm = "2"
        'XRFakturPajakForm.StatusFormPajak = "2"
        If ce_page_number.Checked = True Then
            PageNum = "Y"
        Else
            PageNum = "N"
        End If
    End Sub

End Class
