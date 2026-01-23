Public Class FPurchaseReceiptPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FPurchaseReceiptPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FPurchaseReceiptSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        'frm._obk = rcv_cly_end
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FPurchaseReceiptSearch()
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
        _type = 5
        _table = "rcv_mstr"
        _initial = "rcv"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "    rcv_oid, " _
            & "    rcv_dom_id, " _
            & "    rcv_en_id, " _
            & "    rcv_add_by, " _
            & "    rcv_add_date, " _
            & "    rcv_upd_by, " _
            & "    rcv_upd_date, " _
            & "    rcv_code, " _
            & "    rcv_date, " _
            & "    rcv_eff_date, " _
            & "    rcv_po_oid, " _
            & "    rcv_packing_slip, " _
            & "    rcv_dt, " _
            & "    rcv_is_receive, " _
            & "    rcv_ret_replace, " _
            & "    rcv_cu_id, " _
            & "    rcv_exc_rate, " _
            & "    rcvd_pod_oid, " _
            & "    po_code, " _
            & "    ptnr_name, " _
            & "    ptnra_line_1, " _
            & "    ptnra_line_2, " _
            & "    ptnra_line_3, " _
            & "    pt_code, " _
            & "    pod_pt_desc1 as pt_desc1, " _
            & "    pod_pt_desc2 as pt_desc2, " _
            & "    rcvd_qty, " _
            & "    rcvd_um, " _
            & "    um_master.code_name as um_name, " _
            & "    rcvd_loc_id, " _
            & "    loc_desc, " _
            & "    rcvd_rea_code_id, " _
            & "    rea_code_mstr.code_name rea_code_name, " _
            & "    coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "    tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "  FROM  " _
            & "    rcv_mstr " _
            & "    inner join rcvd_det on rcvd_rcv_oid = rcv_oid " _
            & "    inner join cu_mstr on cu_id = rcv_cu_id " _
            & "    inner join pod_det on pod_oid = rcvd_pod_oid " _
            & "    inner join pt_mstr on pt_id = pod_pt_id " _
            & "    inner join code_mstr um_master on um_master.code_id = rcvd_um " _
            & "    inner join loc_mstr on loc_id = rcvd_loc_id " _
            & "    left outer join code_mstr rea_code_mstr on rea_code_mstr.code_id = rcvd_rea_code_id " _
            & "    left outer join tranaprvd_dok on tranaprvd_tran_oid = rcv_oid  " _
            & "    inner join po_mstr on po_oid = pod_po_oid " _
            & "    inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
            & "    left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "    where rcv_code >= '" + be_first.Text + "'" _
            & "    and rcv_code <= '" + be_to.Text + "'" _
            & "    order by rcv_code"

        '    Dim frm As New frmPrintDialog
        '    frm._ssql = _sql
        '    If ce_qrcode.EditValue = True Then
        '        frm._report = "XRPurchaseOrderPrintOutNoCost"
        '    Else
        '        frm._report = "XRPurchaseReceiptPrint"
        '        'End If
        '        'frm._report = "XRPurchaseOrderPrintOut"
        '    End If

        '    frm._remarks = be_first.Text & " >> " & be_to.Text
        '    frm.ShowDialog()


        'End Sub

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRPurchaseReceiptPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
