Public Class FRequisitionPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FRequisitionPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FRequisitionSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FRequisitionSearch()
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
        _type = 1
        _table = "req_mstr"
        _initial = "req"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)


        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  req_oid, " _
            & "  req_dom_id, " _
            & "  req_en_id, " _
            & "  req_upd_date, " _
            & "  req_upd_by, " _
            & "  req_add_date, " _
            & "  req_add_by, " _
            & "  req_code, " _
            & "  req_ptnr_id, " _
            & "  req_cmaddr_id, " _
            & "  req_date, " _
            & "  req_need_date, " _
            & "  req_due_date, " _
            & "  req_requested, " _
            & "  req_end_user, " _
            & "  req_rmks, " _
            & "  req_sb_id, " _
            & "  req_cc_id, " _
            & "  req_si_id, " _
            & "  req_type, " _
            & "  req_pjc_id, " _
            & "  req_close_date, " _
            & "  req_total, " _
            & "  req_tran_id, " _
            & "  req_trans_id, " _
            & "  req_trans_rmks, " _
            & "  req_current_route, " _
            & "  req_next_route, " _
            & "  req_dt, " _
            & "  reqd_ptnr_id, " _
            & "  reqd_pt_id, " _
            & "  reqd_rmks, " _
            & "  reqd_end_user, " _
            & "  reqd_qty, " _
            & "  reqd_um, " _
            & "  reqd_cost, " _
            & "  reqd_disc, " _
            & "  reqd_need_date, " _
            & "  reqd_due_date, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  ptnr_name, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_master.code_name as um_name , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  req_mstr " _
            & "  inner join reqd_det on reqd_req_oid = req_oid " _
            & "  left outer join cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
            & "  left outer join ptnr_mstr on ptnr_id = reqd_ptnr_id " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = reqd_um" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = req_oid  " _
            & "  where req_code >= '" + be_first.Text + "'" _
            & "  and req_code <= '" + be_to.Text + "'" _
            & "  order by req_code"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        If func_coll.get_conf_file("wf_requisition") = "1" Then
            frm._report = "XRRequisitionApproval"
        Else
            frm._report = "XRRequisition"
        End If

        'frm._report = "XRRequisition"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
