Public Class FDRCRMemoKonsiyasiPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FDRCRMemoKonsiyasiPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FTransferSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FTransferSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "select  " _
            & "so_oid, " _
            & "so_code, " _
            & "pt_id, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "pt_desc2, " _
            & "sod_qty, " _
            & "sod_price, " _
            & "sod_disc, " _
            & "taxr_rate, " _
            & "sod_qty * (sod_price - (sod_price * sod_disc)) * (taxr_rate / 100) as sod_ppn, " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "so_credit_term, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "um_master.code_name as um_name, " _
            & "ptsfr_code, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_phone_1, " _
            & "cmaddr_phone_2, " _
            & "so_terbilang, " _
            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "from sod_det " _
            & "inner join so_mstr on so_oid = sod_so_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = sod_tax_class " _
            & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = sod_tax_class " _
            & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
            & "inner join ptnr_mstr on ptnr_id = so_ptnr_id_bill " _
            & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "inner join cu_mstr on cu_id = so_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
            & "inner join ptsfr_mstr on ptsfr_so_oid = so_oid " _
            & "inner join cmaddr_mstr on cmaddr_en_id = so_en_id " _
            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = so_oid " _
            & "  where tax_type_mstr.code_name = 'PPN'" _
            & "  and ptsfr_code >= '" + be_first.Text + "'" _
            & "  and ptsfr_code <= '" + be_to.Text + "'" _
            & "  order by ptsfr_code, sod_seq "



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoiceKonsiyasiPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
