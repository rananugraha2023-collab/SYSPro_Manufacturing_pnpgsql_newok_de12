Public Class FDRCRMemoPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FDRCRMemoPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FDRCRMemoSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FDRCRMemoSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = le_entity.EditValue
        _type = 13
        _table = "ar_mstr"
        _initial = "ar"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        If CheckEditNew.EditValue = False Then
            _sql = "select  " _
            & "ar_oid, " _
            & "ar_code, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "ar_date, " _
            & "ar_eff_date, " _
            & "ar_amount, " _
            & "ar_pay_amount, " _
            & "ar_due_date, " _
            & "ar_expt_date, " _
            & "ar_exc_rate, " _
            & "ar_remarks, " _
            & "ar_status, " _
            & "ar_type, " _
            & "ar_credit_term, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "so_code, " _
            & "um_master.code_name as um_name , " _
            & "pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "sod_disc, sod_tax_inc,  " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name,  " _
            & "taxr_rate,  " _
            & "ars_invoice,ars_so_disc_value,  " _
            & "ars_invoice_price, " _
            & "ars_invoice_price + (sod_price * sod_disc) as ars_invoice_price2, " _
            & "case ars_taxable when 'Y' then (ars_invoice * (ars_invoice_price * taxr_rate / 100))  else 0 end as ars_invoice_price_ppn, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
            & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
            & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
            & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
            & "bk_name,bk_code, " _
            & "ac_name, " _
            & "ar_terbilang, " _
            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "from ars_ship " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid  " _
            & "inner join ar_mstr on ar_oid = ars_ar_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join so_mstr on so_oid = sod_so_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = ars_tax_class_id " _
            & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = ars_tax_class_id " _
            & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "  inner join ( " _
            & "  select distinct on (ptnra_ptnr_oid) " _
            & "    ptnra_ptnr_oid, " _
            & "    ptnra_line_1, " _
            & "    ptnra_line_2, " _
            & "    ptnra_line_3, " _
            & "    ptnra_zip " _
            & "  from ptnra_addr " _
            & "  order by ptnra_ptnr_oid, ptnra_add_date desc " _
            & ") ptnra_addr on ptnra_addr.ptnra_ptnr_oid = ptnr_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join bk_mstr on bk_id = ar_bk_id " _
            & "inner join ac_mstr on ac_id = bk_ac_id " _
            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
            & "where tax_type_mstr.code_name = 'PPN' " _
            & "and ar_code >= '" + be_first.Text + "' " _
            & "and ar_code <= '" + be_to.Text + "' " _
            & "order by ar_code "
        Else
            _sql = "select  " _
            & "ar_oid, " _
            & "ar_code, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "ar_date, " _
            & "ar_eff_date, " _
            & "ar_amount, " _
            & "ar_pay_amount, " _
            & "ar_due_date, " _
            & "ar_expt_date, " _
            & "ar_exc_rate, " _
            & "ar_remarks, " _
            & "ar_status, " _
            & "ar_type, " _
            & "ar_credit_term, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "so_code, " _
            & "um_master.code_name as um_name , " _
            & "pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "sod_disc, sod_tax_inc,  " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name,  " _
            & "taxr_rate,  " _
            & "ars_invoice,ars_so_disc_value,  " _
            & "ars_invoice_price, " _
            & "ars_invoice_price + (sod_price * sod_disc) as ars_invoice_price2, " _
            & "case ars_taxable when 'Y' then (ars_invoice * (ars_invoice_price * taxr_rate / 100))  else 0 end as ars_invoice_price_ppn, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
            & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
            & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
            & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
            & "bk_name,bk_code, " _
            & "ac_name, " _
            & "ar_terbilang, " _
            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "from ars_ship " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid  " _
            & "inner join ar_mstr on ar_oid = ars_ar_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join so_mstr on so_oid = sod_so_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = ars_tax_class_id " _
            & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = ars_tax_class_id " _
            & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "  inner join ( " _
            & "  select distinct on (ptnra_ptnr_oid) " _
            & "    ptnra_ptnr_oid, " _
            & "    ptnra_line_1, " _
            & "    ptnra_line_2, " _
            & "    ptnra_line_3, " _
            & "    ptnra_zip " _
            & "  from ptnra_addr " _
            & "  order by ptnra_ptnr_oid, ptnra_add_date desc " _
            & ") ptnra_addr on ptnra_addr.ptnra_ptnr_oid = ptnr_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join bk_mstr on bk_id = ar_bk_id " _
            & "inner join ac_mstr on ac_id = bk_ac_id " _
            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
            & "where tax_type_mstr.code_name = 'PPN' " _
            & "and ar_code >= '" + be_first.Text + "' " _
            & "and ar_code <= '" + be_to.Text + "' " _
            & "order by ar_code "
        End If
        


       

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoice"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()


    End Sub
End Class
