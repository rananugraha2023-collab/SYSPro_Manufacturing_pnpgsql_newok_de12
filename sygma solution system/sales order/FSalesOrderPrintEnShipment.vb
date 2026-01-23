Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FSalesOrderPrintEnShipment
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Dim _now, _before, _then As DateTime


    Private Sub FSalesOrderPrintEnShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_all())
        le_entity_shipment.Properties.DataSource = dt_bantu
        le_entity_shipment.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity_shipment.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity_shipment.ItemIndex = 0

        _now = func_coll.get_now_curr
        _then = func_coll.get_then
        so_tglcutting_awal.DateTime = _then
        so_tglcutting_akhir.EditValue = _now

        ce_invoice.EditValue = True
        ce_indent.EditValue = False
        ce_prn_dt.EditValue = False
        ce_excel.EditValue = True


    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FSalesOrderSearchCut()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._cut_first = so_tglcutting_awal.EditValue
        frm._cut_last = so_tglcutting_akhir.EditValue
        frm._obj = be_first

        'If ce_iscash.EditValue = True Then
        '    frm._interval = 0
        'Else
        '    frm._interval = 1
        'End If

        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FSalesOrderSearchCut()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._cut_first = so_tglcutting_awal.EditValue
        frm._cut_last = so_tglcutting_akhir.EditValue
        frm._obj = be_to

        'If ce_iscash.EditValue = True Then
        '    frm._interval = 0
        'Else
        '    frm._interval = 1
        'End If

        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _type_inv, _table, _active, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = le_entity.EditValue
        _type = 10
        _type_inv = 13
        '_type_invd = 14
        _active = "'Y'"
        _table = "so_mstr"
        _initial = "so"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        '_code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        '_code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        'Dim ds_bantu As New DataSet
        'Dim _sql As String


        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

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
        '& "WHERE " _
        '& "so_mstr.so_code >= '" + be_first.Text + "'" _
        '& "and so_mstr.so_code <= '" + be_to.Text + "'" _
        '& "order by so_mstr.so_code"

        If ce_invoice.EditValue = True Then
            _sql = "SELECT  " _
                    & "  sod_oid,so_trans_rmks, " _
                    & "  sod_dom_id, " _
                    & "  sod_en_id, " _
                    & "  sod_add_by, " _
                    & "  sod_add_date, " _
                    & "  sod_upd_by, " _
                    & "  sod_upd_date, " _
                    & "  sod_so_oid, " _
                    & "  sod_seq, " _
                    & "  sod_is_additional_charge, " _
                    & "  sod_si_id, " _
                    & "  sod_pt_id,so_pt_id, " _
                    & "  sod_rmks, " _
                    & "  sod_qty, " _
                    & "  sod_qty_allocated, " _
                    & "  sod_qty_picked, " _
                    & "  sod_qty_shipment, " _
                    & "  sod_qty_pending_inv, " _
                    & "  sod_qty_invoice, " _
                    & "  sod_um, " _
                    & "  sod_cost, " _
                    & "  sod_price, " _
                    & "  sod_disc, " _
                    & "  sod_sales_ac_id, " _
                    & "  sod_sales_sb_id, " _
                    & "  sod_sales_cc_id, " _
                    & "  sod_disc_ac_id, " _
                    & "  sod_um_conv, " _
                    & "  sod_qty_real, " _
                    & "  sod_taxable, " _
                    & "  sod_tax_inc, " _
                    & "  sod_tax_class, " _
                    & "  sod_status, " _
                    & "  sod_dt, " _
                    & "  sod_payment, " _
                    & "  sod_dp, " _
                    & "  sod_sales_unit, " _
                    & "  sod_loc_id, " _
                    & "  sod_serial, " _
                    & "  sod_qty_return, " _
                    & "  sod_ppn_type, " _
                    & "  so_code, " _
                    & "  so_date, " _
                    & "  cmaddr_code, " _
                    & "  cmaddr_name, " _
                    & "  cmaddr_line_1, " _
                    & "  cmaddr_line_2, " _
                    & "  cmaddr_line_3, " _
                    & "  cmaddr_phone_1, " _
                    & "  cmaddr_phone_2, " _
                    & "  ptnr_name || ' / ' || coalesce(ptnra_receiver_name,'') as ptnr_name, " _
                    & "  ptnra_line_1 , " _
                    & "  REPLACE(ptnra_line_2,'bound','') as ptnra_line_2,   REPLACE(coalesce(ptnra_line_3,''),'bound','') || coalesce(ptnra_phone_1,'') as ptnra_line_3,  " _
                    & "  ptnra_zip, " _
                    & "  cu_name, " _
                    & "  credit_term_mstr.code_name as credit_term_name, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  so_terbilang, " _
                    & "  bk_name, " _
                    & "  bk_code, " _
                    & "  um_master.code_name as um_name, " _
                    & "  so_total, " _
                    & "  so_total_ppn, " _
                    & "  so_total_pph, " _
                    & "  so_total + so_total_ppn as so_total_aft_tax, " _
                    & "  coalesce(aprvd_name_1,'') as tranaprvd_name_1, coalesce(aprvd_name_2,'') as tranaprvd_name_2, coalesce(aprvd_name_3,'') as tranaprvd_name_3, coalesce(aprvd_name_4,'') as tranaprvd_name_4, " _
                    & "  coalesce(aprvd_pos_1,'') as tranaprvd_pos_1, coalesce(aprvd_pos_2,'') as tranaprvd_pos_2, coalesce(aprvd_pos_3,'') as tranaprvd_pos_3, coalesce(aprvd_pos_4,'') as tranaprvd_pos_4  " _
                    & "FROM  " _
                    & "  sod_det " _
                    & "  inner join so_mstr on so_oid = sod_so_oid " _
                    & "  inner join cmaddr_mstr on cmaddr_en_id = so_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                    & "INNER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
                    & "AND (public.so_mstr.so_ptnra_id = public.ptnra_addr.ptnra_id) " _
                    & "  inner join cu_mstr on cu_id = so_cu_id " _
                    & "  inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join bk_mstr on bk_id = so_bk_id " _
                    & "  inner join code_mstr um_master on um_master.code_id = sod_um " _
                    & "  left outer join aprvd_dok on aprvd_en_id = so_en_id_shipment "

        Else

            _sql = "SELECT  " _
                    & "so_en_id_shipment, " _
                    & "so_mstr.so_code, " _
                    & "so_mstr.so_ptnr_id_sold, " _
                    & "so_mstr.so_date, " _
                    & "so_mstr.so_ptnr_id_bill, " _
                    & "so_mstr.so_credit_term, " _
                    & "so_mstr.so_pay_type, " _
                    & "so_mstr.so_pay_method, " _
                    & "sod_det.sod_pt_id, " _
                    & "sod_det.sod_qty, " _
                    & "sod_det.sod_so_oid, " _
                    & "sod_det.sod_um, " _
                    & "ptnr_mstr.ptnr_code, " _
                    & "ptnr_mstr.ptnr_name, " _
                    & "ptnr_mstr.ptnr_id, " _
                    & "ptnra_addr.ptnra_id, " _
                    & "ptnra_addr.ptnra_line, " _
                    & "ptnra_addr.ptnra_line_1, " _
                    & "ptnra_addr.ptnra_line_2, " _
                    & "ptnra_addr.ptnra_line_3, " _
                    & "pt_mstr.pt_id, " _
                    & "pt_mstr.pt_code, " _
                    & "pt_mstr.pt_desc1, " _
                    & "pt_mstr.pt_um, " _
                    & "code_mstr.code_id, " _
                    & "code_mstr.code_code, " _
                    & "code_mstr.code_field, " _
                    & "so_mstr.so_oid, " _
                    & "cmaddr_code, " _
                    & "cmaddr_name, " _
                    & "cmaddr_line_1, " _
                    & "cmaddr_line_2, " _
                    & "cmaddr_line_3, " _
                    & "cmaddr_phone_1, " _
                    & "cmaddr_phone_2, " _
                    & "ptnr_name || ' / ' || coalesce(ptnra_receiver_name, '') as ptnr_name, " _
                    & "ptnra_line_1, " _
                    & "REPLACE(ptnra_line_2,'bound','') as ptnra_line_2,   REPLACE(coalesce(ptnra_line_3,''),'bound','') || coalesce(ptnra_phone_1,'') as ptnra_line_3,  " _
                    & "ptnra_zip, " _
                    & "coalesce(aprvd_name_1, '') as tranaprvd_name_1, " _
                    & "coalesce(aprvd_name_2, '') as tranaprvd_name_2, " _
                    & "coalesce(aprvd_name_3, '') as tranaprvd_name_3, " _
                    & "coalesce(aprvd_name_4, '') as tranaprvd_name_4, " _
                    & "coalesce(aprvd_pos_1,'') as tranaprvd_pos_1, " _
                    & "coalesce(aprvd_pos_2,'') as tranaprvd_pos_2, " _
                    & "coalesce(aprvd_pos_3,'') as tranaprvd_pos_3, " _
                    & "coalesce(aprvd_pos_4,'') as tranaprvd_pos_4 " _
                    & "FROM so_mstr " _
                    & "INNER JOIN sod_det ON (so_mstr.so_oid = sod_det.sod_so_oid) " _
                    & "INNER JOIN ptnr_mstr ON (so_mstr.so_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
                    & "INNER JOIN pt_mstr ON (sod_det.sod_pt_id = pt_mstr.pt_id) " _
                    & "INNER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
                    & "AND (public.so_mstr.so_ptnra_id = public.ptnra_addr.ptnra_id) " _
                    & "INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
                    & "INNER JOIN cmaddr_mstr on cmaddr_en_id = so_en_id " _
                    & "  left outer join aprvd_dok on aprvd_en_id = so_en_id_shipment "
        End If


        If ce_prn_dt.EditValue = True Then
            _sql = _sql + "WHERE "
            _sql = _sql + "  so_add_date >= " + SetDateNTime991(so_tglcutting_awal.DateTime) + " "
            _sql = _sql + "  and so_add_date <= " + SetDateNTime991(so_tglcutting_akhir.DateTime) + " "
            _sql = _sql + "  and so_en_id_shipment = " + le_entity.EditValue.ToString + " "
            _sql = _sql + "  and aprvd_type = " + _type + " "
            _sql = _sql + "  and ptnra_active = " + _active + " "
        End If

        'If ce Then
        '    _sql = _sql + "WHERE "
        '    _sql = _sql + "  so_mstr.so_code >= '" + be_first.Text + "'"
        '    _sql = _sql + "  and so_mstr.so_code <= '" + be_to.Text + "'"
        '    _sql = _sql + "  and so_add_date >= " + SetDateNTime991(so_tglcutting_awal.DateTime) + " "
        '    _sql = _sql + "  and so_add_date <= " + SetDateNTime991(so_tglcutting_akhir.DateTime) + " "
        '    _sql = _sql + "  and so_en_id_shipment = " + le_entity_shipment.EditValue.ToString + " "
        'End If

        'If ce_indent.EditValue = True Then
        '    'Dim _sql As String
        '    _sql = _sql + "and so_pay_type = '991246' "
        '    _sql = _sql + "order by so_mstr.so_code ASC "
        'Else
        '    'Dim _sql As String
        '    _sql = _sql + "and so_pay_type <> '991246' "
        '    _sql = _sql + "order by so_mstr.so_code ASC "

        'End If

        If ce_excel.Checked Then
            Dim v_i As String
            Dim v_a() As String
            Dim v_j As Integer

            v_i = TxtSONumber.Text
            v_a = v_i.Split(vbNewLine)
            Dim _filter As String = ""
            For v_j = 0 To v_a.GetUpperBound(0)
                If Len(v_a(v_j)) > 0 Then
                    Dim _str As String = v_a(v_j)
                    _filter = _filter & "'" & _str.Replace(vbNewLine, "").Replace(vbCr, "").Replace(vbCrLf, "").Replace(vbLf, "") & "',"
                End If
            Next
            If _filter.Length > 0 Then
                _filter = _filter.Substring(0, _filter.Length - 1)
            End If

            _sql = _sql & " WHERE so_mstr.so_code in (" & _filter & ")"
            _sql = _sql + "  and so_en_id_shipment = " + le_entity.EditValue.ToString + " "
            'If ce_invoice.EditValue = True Then
            '    _sql = _sql + "  and aprvd_type = " + _type_inv + " "
            'Else
            _sql = _sql + "  and aprvd_type = " + _type + " "
            _sql = _sql + "  and ptnra_active = " + _active + " "
            'End If

        End If

        If ce_invoice.EditValue = True Then
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRSOTemporary"
            frm._remarks = be_first.Text & " >> " & be_to.Text
            frm.ShowDialog()
        Else

            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRSO"
            frm._remarks = be_first.Text & " >> " & be_to.Text
            frm.ShowDialog()
        End If


    End Sub

End Class
