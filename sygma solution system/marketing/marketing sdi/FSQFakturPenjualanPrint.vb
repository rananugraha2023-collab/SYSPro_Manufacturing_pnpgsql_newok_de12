Public Class FSQFakturPenjualanPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderCashPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FSalesQuotationSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first

        'If ce_iscash.EditValue = True Then
        '    frm._interval = 0
        'Else
        frm._interval = 1
        'End If

        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FSalesQuotationSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to

        'If ce_iscash.EditValue = True Then
        '    frm._interval = 0
        'Else
        frm._interval = 1
        'End If

        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = le_entity.EditValue
        _type = 13
        _table = "sq_mstr"
        _initial = "sq"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        If ce_is_package.EditValue = True Then
            _sql = "SELECT  " _
               & "sqd_sq_oid, " _
               & "  um_master.code_name AS um_name, sq_dropship_address," _
               & "  sq_total + sq_total_ppn AS sq_total_aft_tax, " _
               & "  coalesce(tranaprvd_name_1, '') AS tranaprvd_name_1, " _
               & "  coalesce(tranaprvd_name_2, '') AS tranaprvd_name_2, " _
               & "  coalesce(tranaprvd_name_3, '') AS tranaprvd_name_3, " _
               & "  coalesce(tranaprvd_name_4, '') AS tranaprvd_name_4, " _
               & "  a.sq_code, " _
               & "  a.sq_date, " _
               & "  a.sq_terbilang, " _
               & "  a.sq_total, " _
               & "  a.sq_total_ppn, " _
               & "  a.sq_total_pph, " _
               & "  b.sqd_oid, " _
               & "  b.sqd_dom_id, " _
               & "  b.sqd_en_id, " _
               & "  b.sqd_add_by, " _
               & "  b.sqd_add_date, " _
               & "  b.sqd_upd_by, " _
               & "  b.sqd_upd_date, " _
               & "  b.sqd_seq, " _
               & "  b.sqd_is_additional_charge, " _
               & "  b.sqd_si_id, " _
               & "  b.sqd_pt_id, " _
               & "  b.sqd_rmks, " _
               & "  b.sqd_qty, " _
               & "  b.sqd_qty_allocated, " _
               & "  b.sqd_qty_picked, " _
               & "  b.sqd_qty_shipment, " _
               & "  b.sqd_qty_pending_inv, " _
               & "  b.sqd_qty_invoice, " _
               & "  b.sqd_um, " _
               & "  b.sqd_cost, " _
               & "  sq_price as sqd_price, " _
               & "  b.sqd_disc, " _
               & "  b.sqd_sales_ac_id, " _
               & "  b.sqd_sales_sb_id, " _
               & "  b.sqd_sales_cc_id, " _
               & "  b.sqd_disc_ac_id, " _
               & "  b.sqd_um_conv, " _
               & "  b.sqd_qty_real, " _
               & "  b.sqd_taxable, " _
               & "  b.sqd_tax_inc, " _
               & "  b.sqd_tax_class, " _
               & "  b.sqd_status, " _
               & "  b.sqd_dt, " _
               & "  b.sqd_payment, " _
               & "  b.sqd_dp, " _
               & "  b.sqd_sales_unit, " _
               & "  b.sqd_loc_id, " _
               & "  b.sqd_serial, " _
               & "  b.sqd_qty_return, " _
               & "  b.sqd_ppn_type, " _
               & "  public.cmaddr_mstr.cmaddr_code, " _
               & "  public.cmaddr_mstr.cmaddr_name, " _
               & "  public.cmaddr_mstr.cmaddr_line_1, " _
               & "  public.cmaddr_mstr.cmaddr_line_2, " _
               & "  public.cmaddr_mstr.cmaddr_line_3, " _
               & "  public.cmaddr_mstr.cmaddr_phone_1, " _
               & "  public.cmaddr_mstr.cmaddr_phone_2, " _
               & "  public.ptnr_mstr.ptnr_name, " _
               & "  public.ptnra_addr.ptnra_line_1, " _
               & "  public.ptnra_addr.ptnra_line_2, " _
               & "  public.ptnra_addr.ptnra_line_3, " _
               & "  public.ptnra_addr.ptnra_zip, " _
               & "  public.cu_mstr.cu_name, " _
               & "  public.pt_mstr.pt_code, " _
               & "  public.pt_mstr.pt_desc1, " _
               & "  public.pt_mstr.pt_desc2, " _
               & "  tranaprvd_dok.tranaprvd_pos_1, " _
               & "  tranaprvd_dok.tranaprvd_pos_2, " _
               & "  tranaprvd_dok.tranaprvd_pos_3, " _
               & "  tranaprvd_dok.tranaprvd_pos_4 " _
               & "FROM " _
               & "  sqd_det b " _
               & "  INNER JOIN public.sq_mstr a ON (a.sq_oid = b.sqd_sq_oid) " _
               & "  INNER JOIN public.cmaddr_mstr ON (a.sq_en_id = public.cmaddr_mstr.cmaddr_en_id) " _
               & "  INNER JOIN public.ptnr_mstr ON (a.sq_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
               & "  INNER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
               & "  INNER JOIN public.cu_mstr ON (a.sq_cu_id = public.cu_mstr.cu_id) " _
               & "  INNER JOIN public.pt_mstr ON (a.sq_pt_id = public.pt_mstr.pt_id) " _
               & "  INNER JOIN public.code_mstr um_master ON (pt_mstr.pt_um = um_master.code_id )  " _
               & "  left outer join tranaprvd_dok on (tranaprvd_tran_oid = a.sq_oid) " _
               & "WHERE " _
               & "a.sq_code >= '" + be_first.Text + "' " _
               & "and a.sq_code <= '" + be_to.Text + "' "


            If ce_is_package.EditValue = True Then
                _sql = _sql + "order by sq_code limit 1"
            Else
                _sql = _sql + "order by sq_code "
            End If



            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRSQFakturSementaraPackage"
            frm._remarks = be_first.Text & " >> " & be_to.Text
            frm.ShowDialog()
        Else
            If CeShipaddress.EditValue = True Then
                _sql = "SELECT  " _
               & " " _
               & "  coalesce(sq_dropship_address,'') as sq_dropship_address, " _
               & "  sq_total + sq_total_ppn AS sq_total_aft_tax, " _
               & "  coalesce(tranaprvd_name_1, '') AS tranaprvd_name_1, " _
               & "  coalesce(tranaprvd_name_2, '') AS tranaprvd_name_2, " _
               & "  coalesce(tranaprvd_name_3, '') AS tranaprvd_name_3, " _
               & "  coalesce(tranaprvd_name_4, '') AS tranaprvd_name_4, " _
               & "  a.sq_code, " _
               & "  a.sq_date, " _
               & "  a.sq_terbilang, " _
               & "  a.sq_total, " _
               & "  a.sq_total_ppn, " _
               & "  a.sq_total_pph, " _
               & "  public.cmaddr_mstr.cmaddr_code, " _
               & "  public.cmaddr_mstr.cmaddr_name, " _
               & "  public.cmaddr_mstr.cmaddr_line_1, " _
               & "  public.cmaddr_mstr.cmaddr_line_2, " _
               & "  public.cmaddr_mstr.cmaddr_line_3, " _
               & "  public.cmaddr_mstr.cmaddr_phone_1, " _
               & "  public.cmaddr_mstr.cmaddr_phone_2, " _
               & "  public.ptnr_mstr.ptnr_name, " _
               & "  public.ptnra_addr.ptnra_line_1, " _
               & "  public.ptnra_addr.ptnra_line_2, " _
               & "  public.ptnra_addr.ptnra_line_3, " _
               & "  public.ptnra_addr.ptnra_zip, " _
               & "  public.cu_mstr.cu_name, chr(10) as line,ptnra_phone_1,ptnra_phone_2, " _
                & "  tranaprvd_dok.tranaprvd_pos_1, " _
               & "  tranaprvd_dok.tranaprvd_pos_2, " _
               & "  tranaprvd_dok.tranaprvd_pos_3, " _
               & "  tranaprvd_dok.tranaprvd_pos_4 " _
               & "FROM " _
               & "   public.sq_mstr a  " _
               & "  INNER JOIN public.cmaddr_mstr ON (a.sq_en_id = public.cmaddr_mstr.cmaddr_en_id) " _
               & "  INNER JOIN public.ptnr_mstr ON (a.sq_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
               & "  INNER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
               & "  INNER JOIN public.cu_mstr ON (a.sq_cu_id = public.cu_mstr.cu_id) " _
               & "  left outer join tranaprvd_dok on (tranaprvd_tran_oid = a.sq_oid) " _
               & "WHERE " _
               & "a.sq_code >= '" + be_first.Text + "' " _
               & "and a.sq_code <= '" + be_to.Text + "' "


                _sql = _sql + "order by a.sq_code"


                Dim frm As New frmPrintDialog
                frm._ssql = _sql
                frm._report = "XRShipAddressSQ"
                frm._remarks = be_first.Text & " >> " & be_to.Text
                frm.ShowDialog()
            Else
                _sql = "SELECT  " _
               & "sqd_sq_oid, " _
               & "  um_master.code_name AS um_name,sq_dropship_address, " _
               & "  sq_total + sq_total_ppn AS sq_total_aft_tax, " _
               & "  coalesce(tranaprvd_name_1, '') AS tranaprvd_name_1, " _
               & "  coalesce(tranaprvd_name_2, '') AS tranaprvd_name_2, " _
               & "  coalesce(tranaprvd_name_3, '') AS tranaprvd_name_3, " _
               & "  coalesce(tranaprvd_name_4, '') AS tranaprvd_name_4, " _
               & "  a.sq_code, " _
               & "  a.sq_date, " _
               & "  a.sq_terbilang, " _
               & "  a.sq_total, " _
               & "  a.sq_total_ppn, " _
               & "  a.sq_total_pph, " _
               & "  b.sqd_oid, " _
               & "  b.sqd_dom_id, " _
               & "  b.sqd_en_id, " _
               & "  b.sqd_add_by, " _
               & "  b.sqd_add_date, " _
               & "  b.sqd_upd_by, " _
               & "  b.sqd_upd_date, " _
               & "  b.sqd_seq, " _
               & "  b.sqd_is_additional_charge, " _
               & "  b.sqd_si_id, " _
               & "  b.sqd_pt_id, " _
               & "  b.sqd_rmks, " _
               & "  b.sqd_qty, " _
               & "  b.sqd_qty_allocated, " _
               & "  b.sqd_qty_picked, " _
               & "  b.sqd_qty_shipment, " _
               & "  b.sqd_qty_pending_inv, " _
               & "  b.sqd_qty_invoice, " _
               & "  b.sqd_um, " _
               & "  b.sqd_cost, " _
               & "  b.sqd_price, " _
               & "  b.sqd_disc, " _
               & "  b.sqd_sales_ac_id, " _
               & "  b.sqd_sales_sb_id, " _
               & "  b.sqd_sales_cc_id, " _
               & "  b.sqd_disc_ac_id, " _
               & "  b.sqd_um_conv, " _
               & "  b.sqd_qty_real, " _
               & "  b.sqd_taxable, " _
               & "  b.sqd_tax_inc, " _
               & "  b.sqd_tax_class, " _
               & "  b.sqd_status, " _
               & "  b.sqd_dt, " _
               & "  b.sqd_payment, " _
               & "  b.sqd_dp, " _
               & "  b.sqd_sales_unit, " _
               & "  b.sqd_loc_id, " _
               & "  b.sqd_serial, " _
               & "  b.sqd_qty_return, " _
               & "  b.sqd_ppn_type, " _
               & "  public.cmaddr_mstr.cmaddr_code, " _
               & "  public.cmaddr_mstr.cmaddr_name, " _
               & "  public.cmaddr_mstr.cmaddr_line_1, " _
               & "  public.cmaddr_mstr.cmaddr_line_2, " _
               & "  public.cmaddr_mstr.cmaddr_line_3, " _
               & "  public.cmaddr_mstr.cmaddr_phone_1, " _
               & "  public.cmaddr_mstr.cmaddr_phone_2, " _
               & "  public.ptnr_mstr.ptnr_name, " _
               & "  public.ptnra_addr.ptnra_line_1, " _
               & "  public.ptnra_addr.ptnra_line_2, " _
               & "  public.ptnra_addr.ptnra_line_3, " _
               & "  public.ptnra_addr.ptnra_zip, " _
               & "  public.cu_mstr.cu_name, " _
               & "  public.pt_mstr.pt_code, " _
               & "  public.pt_mstr.pt_desc1, " _
               & "  public.pt_mstr.pt_desc2, " _
               & "  tranaprvd_dok.tranaprvd_pos_1, " _
               & "  tranaprvd_dok.tranaprvd_pos_2, " _
               & "  tranaprvd_dok.tranaprvd_pos_3, " _
               & "  tranaprvd_dok.tranaprvd_pos_4 " _
               & "FROM " _
               & "  sqd_det b " _
               & "  INNER JOIN public.sq_mstr a ON (a.sq_oid = b.sqd_sq_oid) " _
               & "  INNER JOIN public.cmaddr_mstr ON (a.sq_en_id = public.cmaddr_mstr.cmaddr_en_id) " _
               & "  INNER JOIN public.ptnr_mstr ON (a.sq_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
               & "  INNER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
               & "  INNER JOIN public.cu_mstr ON (a.sq_cu_id = public.cu_mstr.cu_id) " _
               & "  INNER JOIN public.pt_mstr ON (b.sqd_pt_id = public.pt_mstr.pt_id) " _
               & "  INNER JOIN public.code_mstr um_master ON (pt_mstr.pt_um = um_master.code_id )  " _
               & "  left outer join tranaprvd_dok on (tranaprvd_tran_oid = a.sq_oid) " _
               & "WHERE " _
               & "a.sq_code >= '" + be_first.Text + "' " _
               & "and a.sq_code <= '" + be_to.Text + "' "


                _sql = _sql + "order by a.sq_code"


                Dim frm As New frmPrintDialog
                frm._ssql = _sql
                frm._report = "XRSQFakturSementara"
                frm._remarks = be_first.Text & " >> " & be_to.Text
                frm.ShowDialog()
            End If
            
        End If
       


    End Sub
End Class
