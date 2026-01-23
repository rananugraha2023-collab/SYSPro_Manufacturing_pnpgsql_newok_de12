Public Class FReqTransferIssuePrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FReqTransferIssuePrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FRequisitionTransferSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FRequisitionTransferSearch()
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
        _type = 3
        _table = "reqs_mstr"
        _initial = "reqs"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  reqs_oid, " _
            & "  reqs_dom_id, " _
            & "  reqs_en_id, " _
            & "  reqs_add_by, " _
            & "  reqs_add_date, " _
            & "  reqs_upd_by, " _
            & "  reqs_upd_date, " _
            & "  reqs_code, " _
            & "  reqs_date, " _
            & "  reqs_req_oid, " _
            & "  reqs_en_id_to, " _
            & "  reqs_loc_id_from, " _
            & "  reqs_loc_id_git, " _
            & "  reqs_loc_id_to, " _
            & "  reqs_trans_id, " _
            & "  reqs_receive_date, " _
            & "  reqs_remarks, " _
            & "  reqs_dt, " _
            & "  reqs_si_id, " _
            & "  reqs_si_to_id, " _
            & "  req_code, " _
            & "  en_mstr_from.en_desc as en_desc_from, " _
            & "  en_mstr_to.en_desc as en_desc_to, " _
            & "  loc_mstr_from.loc_desc as loc_desc_from, " _
            & "  loc_mstr_to.loc_desc as loc_desc_to, " _
            & "  cmaddr_mstr_from.cmaddr_name as cmaddr_name_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_1 as cmaddr_line_1_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_2 as cmaddr_line_2_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_3 as cmaddr_line_3_from, " _
            & "  cmaddr_mstr_to.cmaddr_name as cmaddr_name_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_1 as cmaddr_line_1_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_2 as cmaddr_line_2_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_3 as cmaddr_line_3_to, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  reqds_qty, " _
            & "  code_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  public.reqs_mstr " _
            & "  inner join req_mstr on req_oid = reqs_req_oid " _
            & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
            & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = reqs_en_id_to " _
            & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = reqs_loc_id_from " _
            & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = reqs_loc_id_to " _
            & "  inner join cmaddr_mstr cmaddr_mstr_from on cmaddr_mstr_from.cmaddr_en_id = reqs_en_id " _
            & "  inner join cmaddr_mstr cmaddr_mstr_to on cmaddr_mstr_to.cmaddr_en_id = reqs_en_id_to " _
            & "  inner join reqsd_det on reqsd_det.reqds_reqs_oid = reqs_oid " _
            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr on code_id = reqds_um " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = reqs_oid " _
            & "  where reqs_code >= '" + be_first.Text + "'" _
            & "  and reqs_code <= '" + be_to.Text + "'" _
            & "  order by reqs_code "



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRReqTransferIssue"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
