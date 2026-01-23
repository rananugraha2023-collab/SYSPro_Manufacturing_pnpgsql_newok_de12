Imports master_new.PGSqlConn

Public Class FSalesOrderFakturPenjualanPrint

    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderFakturPenjualanPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        If func_coll.get_conf_file("syspro_approval_code") = "DUTA" Or func_coll.get_conf_file("syspro_approval_code") = "SDI" Then
            Dim ssql As String = "select en_id ,en_code,en_desc  from en_mstr where en_id>0 and en_active='Y' " _
                    & "union  " _
                    & "select -1 as en_id,'' as en_code, '-' as en_desc " _
                    & "order by en_code"

            Dim dt2 As New DataTable
            dt2 = GetTableData(ssql)
            With le_entity
                .Properties.DataSource = dt2
                .Properties.DisplayMember = dt2.Columns("en_desc").ToString
                .Properties.ValueMember = dt2.Columns("en_id").ToString
                .EditValue = dt2.Rows(0).Item("en_id")
                .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                .Properties.BestFit()
                .Properties.DropDownRows = 14
                .Properties.PopupWidth = 300
                '.Properties.TextEditStyle = TextEditStyles.Standard
            End With

        Else
            init_le(le_entity, "en_mstr")
        End If



    End Sub

    


    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        If ce_name.EditValue = True Then
            Dim frm As New FSalesOrderSearch()
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = be_first
            frm._ptnr_id = be_sold_to.Tag
            If ce_iscash.EditValue = True Then
                frm._interval = 0
            Else
                frm._interval = 1
            End If

            frm.type_form = True
            frm.ShowDialog()
        Else
            Dim frm As New FSalesOrderSearchNon()
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = be_first
            frm._ptnr_id = be_sold_to.Tag
            If ce_iscash.EditValue = True Then
                frm._interval = 0
            Else
                frm._interval = 1
            End If

            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        If ce_name.EditValue = True Then
            Dim frm As New FSalesOrderSearch()
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = be_to
            frm._ptnr_id = be_sold_to.Tag

            If ce_iscash.EditValue = True Then
                frm._interval = 0
            Else
                frm._interval = 1
            End If

            frm.type_form = True
            frm.ShowDialog()
        Else
            Dim frm As New FSalesOrderSearchNon()
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = be_to

            If ce_iscash.EditValue = True Then
                frm._interval = 0
            Else
                frm._interval = 1
            End If

            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub be_sold_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_sold_to.ButtonClick
        Dim frm As New FPartnerSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        ''frm._objk = be_sold_to.Tag

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
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim _be_sold_to As String
        Dim func_coll As New function_collection


        _en_id = le_entity.EditValue
        _type = 13
        _table = "so_mstr"
        _initial = "so"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)
        _be_sold_to = be_sold_to.EditValue

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        If ce_is_package.EditValue = True Then
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
                & "  so_price as sod_price, " _
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
                & "  ptnr_name, " _
                & "  ptnra_line_1, " _
                & "  REPLACE(ptnra_line_2,'bound','') as ptnra_line_2,   REPLACE(coalesce(ptnra_line_3,''),'bound','') || coalesce(ptnra_phone_1,'') as ptnra_line_3,   " _
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
                & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM  " _
                & "  sod_det " _
                & "  inner join so_mstr on so_oid = sod_so_oid " _
                & "  inner join cmaddr_mstr on cmaddr_en_id = so_en_id " _
                & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                & "  inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                & "  inner join cu_mstr on cu_id = so_cu_id " _
                & "  inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
                & "  inner join pt_mstr on pt_id = so_pt_id " _
                & "  inner join bk_mstr on bk_id = so_bk_id " _
                & "  inner join code_mstr um_master on um_master.code_id = sod_um " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = so_oid " _
                & "WHERE " _
                & " so_mstr.so_code >= '" + be_first.Text + "' " _
                & " and so_mstr.so_code <= '" + be_to.Text + "' "

        Else

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
                & "   sod_price, " _
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
                & "  ptnr_name as so_ptnr_sold, " _
                & "  ptnr_name, " _
                & "  ptnra_line_1, " _
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
                & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM  " _
                & "  sod_det " _
                & "  inner join so_mstr on so_oid = sod_so_oid " _
                & "  inner join cmaddr_mstr on cmaddr_en_id = so_en_id " _
                & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                & "  inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                & "  inner join cu_mstr on cu_id = so_cu_id " _
                & "  inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                & "  inner join bk_mstr on bk_id = so_bk_id " _
                & "  inner join code_mstr um_master on um_master.code_id = sod_um " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = so_oid " _
                & "WHERE " _
                & " so_mstr.so_code >= '" + be_first.Text + "'" _
                & " and so_mstr.so_code <= '" + be_to.Text + "'"
        End If


        If ce_iscash.EditValue = True Then
            _sql = _sql + " and coalesce(so_interval,-1) = 0 "
        Else
            _sql = _sql + " and coalesce(so_interval,-1) > 0 "
        End If

        If ce_is_package.EditValue = True Then
            _sql = _sql + " order by so_mstr.so_code limit 1 "
        Else
            _sql = _sql + " order by so_mstr.so_code "
        End If

        If CheckEditNew.EditValue = True Then
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
                & "   sod_price, " _
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
                & "   ptnra_line_1 , " _
                & "   REPLACE(ptnra_line_2,'bound','') as ptnra_line_2,   REPLACE(coalesce(ptnra_line_3,''),'bound','') || coalesce(ptnra_phone_1,'') as ptnra_line_3,  " _
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
                & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM  " _
                & "  sod_det " _
                & "  inner join so_mstr on so_oid = sod_so_oid " _
                & "  inner join cmaddr_mstr on cmaddr_en_id = so_en_id " _
                & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                & "  inner join ptnra_addr on ptnra_id = so_ptnra_id " _
                & "  inner join cu_mstr on cu_id = so_cu_id " _
                & "  inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                & "  inner join bk_mstr on bk_id = so_bk_id " _
                & "  inner join code_mstr um_master on um_master.code_id = sod_um " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = so_oid " _
                & "WHERE " _
                & "so_mstr.so_code >= '" + be_first.Text + "' " _
                & "and so_mstr.so_code <= '" + be_to.Text + "' "

            _sql = _sql + "order by so_mstr.so_code "
        End If

        If ce_iscash.EditValue = True Then

            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRSOCash"
            frm._remarks = be_first.Text & " >> " & be_to.Text
            frm.ShowDialog()

        Else
            If ce_is_package.EditValue = True Then
                Dim frm As New frmPrintDialog
                frm._ssql = _sql
                frm._report = "XRSOCashTemporaryPackage"
                frm._remarks = be_first.Text & " >> " & be_to.Text
                frm.ShowDialog()
            Else
                Dim frm As New frmPrintDialog
                frm._ssql = _sql
                frm._report = "XRSOTemporary"
                frm._remarks = be_first.Text & " >> " & be_to.Text
                frm.ShowDialog()
            End If
           
        End If

    End Sub

    'Public Overrides Sub insert_data_awal()

    '    le_entity.Focus()
    '    le_entity.ItemIndex = 0
    '    ce_name.EditValue = False

    '    be_sold_to.Text = ""
    '    be_sold_to.Tag = ""

    'End Sub
End Class
