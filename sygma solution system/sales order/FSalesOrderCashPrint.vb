Imports master_new.ModFunction

Public Class FSalesOrderCashPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data


    Private Sub FSalesOrderPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FSalesOrderSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FSalesOrderSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String
        'Dim func_coll As New function_collection

        '_en_id = le_entity.EditValue
        '_type = 10
        '_table = "so_mstr"
        '_initial = "so"
        '_code_awal = Trim(be_first.Text)
        '_code_akhir = Trim(be_to.Text)

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        'Dim ds_bantu As New DataSet
        'Dim _sql As String

        '_sql = "SELECT  " _
        '    & "  so_mstr.so_code, " _
        '    & "  so_mstr.so_ptnr_id_sold, " _
        '    & "  so_mstr.so_date, " _
        '    & "  so_mstr.so_ptnr_id_bill, " _
        '    & "  so_mstr.so_credit_term, " _
        '    & "  so_mstr.so_pay_type, " _
        '    & "  so_mstr.so_pay_method, " _
        '    & "  sod_det.sod_pt_id, " _
        '    & "  sod_det.sod_qty, " _
        '    & "  sod_det.sod_so_oid, " _
        '    & "  sod_det.sod_um, " _
        '    & "  ptnr_mstr.ptnr_code, " _
        '    & "  ptnr_mstr.ptnr_name, " _
        '    & "  ptnr_mstr.ptnr_id, " _
        '    & "  ptnra_addr.ptnra_id, " _
        '    & "  ptnra_addr.ptnra_line, " _
        '    & "  ptnra_addr.ptnra_line_1, " _
        '    & "  ptnra_addr.ptnra_line_2, " _
        '    & "  ptnra_addr.ptnra_line_3, " _
        '    & "  pt_mstr.pt_id, " _
        '    & "  pt_mstr.pt_code, " _
        '    & "  pt_mstr.pt_desc1, " _
        '    & "  pt_mstr.pt_um, " _
        '    & "  code_mstr.code_id, " _
        '    & "  code_mstr.code_code, " _
        '    & "  code_mstr.code_field, " _
        '    & "  so_mstr.so_oid, " _
        '    & "  ptnra_addr.ptnra_oid, " _
        '    & "  ptnrac_cntc.ptnrac_oid, " _
        '    & "  ptnrac_cntc.ptnrac_contact_name, " _
        '    & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        '    & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        '    & "FROM " _
        '    & "  so_mstr " _
        '    & "  INNER JOIN sod_det ON (so_mstr.so_oid = sod_det.sod_so_oid) " _
        '    & "  INNER JOIN ptnr_mstr ON (so_mstr.so_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
        '    & "  INNER JOIN ptnra_addr ON (ptnra_addr.ptnra_ptnr_oid = ptnr_mstr.ptnr_oid) " _
        '    & "  INNER JOIN pt_mstr ON (sod_det.sod_pt_id = pt_mstr.pt_id) " _
        '    & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
        '    & "  LEFT OUTER JOIN ptnrac_cntc ON (ptnrac_cntc.addrc_ptnra_oid = ptnra_addr.ptnra_ptnr_oid) " _
        '    & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = so_oid " _
        '    & "WHERE " _
        '    & "so_mstr.so_code >= '" + be_first.Text + "'" _
        '    & "and so_mstr.so_code <= '" + be_to.Text + "'" _
        '    & "order by so_mstr.so_code"


        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRSO"
        'frm._remarks = be_first.Text & " >> " & be_to.Text
        'frm.ShowDialog()


        Dim _sql As String
        _sql = "SELECT  " _
            & "  so_code as arpay_code, " _
            & "  so_date as arpay_date,  so_date as arpay_eff_date, " _
            & "  c.ptnr_name, " _
            & "  d.ptnra_line_1, " _
            & "  d.ptnra_line_2, " _
            & "  d.ptnra_line_3, " _
            & "  so_terbilang as arpay_tbilang, " _
            & "  so_total as arpay_total_amount, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_phone_1, " _
            & "cmaddr_phone_2, " _
            & "  so_code as arpayd_ar_ref,en_city " _
            & " " _
            & "FROM " _
            & "  public.so_mstr a " _
            & "  INNER JOIN public.ptnr_mstr c ON (a.so_ptnr_id_bill = c.ptnr_id) " _
            & "  INNER JOIN public.ptnra_addr d ON (c.ptnr_oid = d.ptnra_ptnr_oid) " _
             & "inner join cmaddr_mstr on cmaddr_en_id = so_en_id " _
             & "inner join en_mstr on en_id = so_en_id " _
            & " WHERE so_code = " & SetSetring(be_first.Text)



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "rptKwitansiReguler"
        frm._remarks = be_first.Text
        frm.ShowDialog()


    End Sub
End Class
