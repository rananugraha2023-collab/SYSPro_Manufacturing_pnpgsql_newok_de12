Public Class FSalesOrderReturnPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderReturnPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FSalesOrderReturnSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FSalesOrderReturnSearch
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
        _type = 12
        _table = "soship_mstr"
        _initial = "soship"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)


        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "    soship_oid, " _
            & "    soship_dom_id, " _
            & "    soship_en_id, " _
            & "    soship_add_by, " _
            & "    soship_add_date, " _
            & "    soship_upd_by, " _
            & "    soship_upd_date, " _
            & "    soship_code, " _
            & "    soship_date, " _
            & "    soship_so_oid, " _
            & "    soship_si_id, " _
            & "    soship_is_shipment, " _
            & "    soship_dt, " _
            & "    soshipd_qty, " _
            & "    soshipd_um, " _
            & "    soshipd_um_conv, " _
            & "    soshipd_cancel_bo, " _
            & "    soshipd_qty_real, " _
            & "    soshipd_si_id, " _
            & "    soshipd_loc_id, " _
            & "    soshipd_lot_serial, " _
            & "    soshipd_rea_code_id, " _
            & "    soshipd_dt, " _
            & "    soshipd_qty_inv, " _
            & "    soshipd_close_line, " _
            & "    so_code,  " _
            & "    so_date, " _
            & "    ptnr_name, " _
            & "    ptnra_line_1, " _
            & "    ptnra_line_2, " _
            & "    ptnra_line_3, " _
            & "    credit_term_mstr.code_name as credit_term_name, " _
            & "    cu_name, " _
            & "    pt_code, " _
            & "    pt_desc1, " _
            & "    pt_desc2, " _
            & "    um_master.code_name as um_name, " _
            & "    cmaddr_name, " _
            & "    cmaddr_line_1, " _
            & "    cmaddr_line_2, " _
            & "    cmaddr_line_3, " _
            & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "  FROM  " _
            & "    soship_mstr " _
            & "    inner join soshipd_det on soshipd_soship_oid = soship_oid " _
            & "    inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "    inner join so_mstr on so_oid = sod_so_oid " _
            & "    inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
            & "    left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "    inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = so_credit_term " _
            & "    inner join cu_mstr on cu_id = so_cu_id " _
            & "    inner join pt_mstr on pt_id = sod_pt_id  " _
            & "    inner join code_mstr um_master on um_master.code_id = soshipd_um " _
            & "    inner join cmaddr_mstr on cmaddr_en_id = soship_en_id " _
            & "    inner join code_mstr ptnr_type on ptnr_type.code_id = ptnra_addr_type " _
            & "    left outer join tranaprvd_dok on tranaprvd_tran_oid = soship_oid " _
            & "    where ptnr_type.code_name ~~* 'bill to' " _
            & "    and soship_code >= '" + be_first.Text + "'" _
            & "    and soship_code <= '" + be_to.Text + "'" _
            & "    order by soship_code, soshipd_seq "



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRSalesOrderReturPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
