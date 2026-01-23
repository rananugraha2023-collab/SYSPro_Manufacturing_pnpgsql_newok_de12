Public Class FInventoryRequestPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data


    Private Sub FInventoryRequestPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")

    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FInventoryRequestSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FInventoryRequestSearch()
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
        _type = 10
        _table = "pb_mstr"
        _initial = "pb"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT " _
        & "pb_mstr.pb_oid, " _
        & "pb_mstr.pb_dom_id,  " _
        & "pb_mstr.pb_en_id,  " _
        & "pb_mstr.pb_add_by,  " _
        & "pb_mstr.pb_add_date, " _
        & "pb_mstr.pb_upd_by,  " _
        & "pb_mstr.pb_upd_date,  " _
        & "pb_mstr.pb_date,  " _
        & "pb_mstr.pb_due_date, " _
        & "pb_mstr.pb_requested,  " _
        & "pb_mstr.pb_end_user,  " _
        & "pb_mstr.pb_rmks, " _
        & "pb_mstr.pb_status,  " _
        & "pb_mstr.pb_close_date,  " _
        & "pb_mstr.pb_dt,  " _
        & "pb_mstr.pb_code, " _
        & "pbd_det.pbd_oid,  " _
        & "pbd_det.pbd_dom_id,  " _
        & "pbd_det.pbd_en_id,  " _
        & "pbd_det.pbd_add_by,  " _
        & "pbd_det.pbd_add_date,  " _
        & "pbd_det.pbd_upd_by,  " _
        & "pbd_det.pbd_upd_date,  " _
        & "pbd_det.pbd_pb_oid,  " _
        & "pbd_det.pbd_seq,  " _
        & "pbd_det.pbd_pt_id,  " _
        & "pbd_det.pbd_rmks, " _
        & "pbd_det.pbd_end_user,  " _
        & "pbd_det.pbd_qty,  " _
        & "pbd_det.pbd_qty_processed, " _
        & "pbd_det.pbd_qty_completed,  " _
        & "pbd_det.pbd_um,  " _
        & "pbd_det.pbd_due_date,  " _
        & "pbd_det.pbd_status,  " _
        & "pbd_det.pbd_dt,  " _
        & "pt_mstr.pt_code,  " _
        & "pt_mstr.pt_desc1,  " _
        & "pt_mstr.pt_desc2,  " _
        & "en_mstr.en_id,  " _
        & "en_mstr.en_desc, " _
        & "cmaddr_en_id, " _
        & "cmaddr_mstr.cmaddr_name,  " _
        & "cmaddr_mstr.cmaddr_line_1, " _
        & "cmaddr_mstr.cmaddr_line_2,  " _
        & "cmaddr_mstr.cmaddr_line_3, " _
        & "code_mstr.code_id,  " _
        & "code_mstr.code_code, " _
        & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        & "FROM pb_mstr " _
        & "inner join pbd_det on pb_mstr.pb_oid = pbd_det.pbd_pb_oid " _
        & "inner join en_mstr on pb_mstr.pb_en_id = en_mstr.en_id " _
        & "inner join pt_mstr on pbd_det.pbd_pt_id = pt_mstr.pt_id " _
        & "inner join code_mstr on pbd_det.pbd_um = code_mstr.code_id  " _
        & "inner join cmaddr_mstr on pb_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
        & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
        & "WHERE " _
        & "pb_mstr.pb_code >= '" + be_first.Text + "'" _
        & "and pb_mstr.pb_code <= '" + be_to.Text + "'" _
        & "order by pb_mstr.pb_code"



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInventoryReqPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()


    End Sub
End Class
