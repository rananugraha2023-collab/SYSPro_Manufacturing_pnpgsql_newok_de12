Public Class FTransferReceiptsPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FTransferReceiptsPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = le_entity.EditValue
        _type = 9
        _table = "ptsfr_mstr"
        _initial = "ptsfr"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.update_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  ptsfr_oid, " _
            & "  ptsfr_dom_id, " _
            & "  ptsfr_en_id, " _
            & "  ptsfr_add_by, " _
            & "  ptsfr_add_date, " _
            & "  ptsfr_upd_by, " _
            & "  ptsfr_upd_date, " _
            & "  ptsfr_en_to_id, " _
            & "  ptsfr_code, " _
            & "  ptsfr_date, " _
            & "  ptsfr_receive_date, " _
            & "  ptsfr_si_id, " _
            & "  ptsfr_loc_id, " _
            & "  ptsfr_loc_git, " _
            & "  ptsfr_remarks, " _
            & "  ptsfr_trans_id, " _
            & "  ptsfr_dt, " _
            & "  ptsfr_loc_to_id, " _
            & "  ptsfr_si_to_id, " _
            & "  ptsfrd_pt_id, " _
            & "  ptsfrd_qty, " _
            & "  ptsfrd_qty_receive, " _
            & "  ptsfrd_um, " _
            & "  ptsfrd_lot_serial, " _
            & "  ptsfrd_cost, " _
            & "  from_cmaddr_mstr.cmaddr_name as from_cmaddr_name, " _
            & "  from_cmaddr_mstr.cmaddr_line_1 as from_cmaddr_line_1, " _
            & "  from_cmaddr_mstr.cmaddr_line_2 as from_cmaddr_line_2, " _
            & "  from_cmaddr_mstr.cmaddr_line_3 as from_cmaddr_line_3, " _
            & "  to_cmaddr_mstr.cmaddr_name as to_cmaddr_name, " _
            & "  to_cmaddr_mstr.cmaddr_line_1 as to_cmaddr_line_1, " _
            & "  to_cmaddr_mstr.cmaddr_line_2 as to_cmaddr_line_2, " _
            & "  to_cmaddr_mstr.cmaddr_line_3 as to_cmaddr_line_3, " _
            & "  from_loc_mstr.loc_desc as from_loc_desc, " _
            & "  to_loc_mstr.loc_desc as to_loc_desc, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_master.code_name as um_name, " _
            & "  coalesce(tranaprvd_name_5,'') as tranaprvd_name_5, coalesce(tranaprvd_name_6,'') as tranaprvd_name_6, coalesce(tranaprvd_name_7,'') as tranaprvd_name_7, coalesce(tranaprvd_name_8,'') as tranaprvd_name_8, " _
            & "  tranaprvd_pos_5, tranaprvd_pos_6, tranaprvd_pos_7, tranaprvd_pos_8 " _
            & "FROM  " _
            & "  ptsfr_mstr " _
            & "  inner join ptsfrd_det on ptsfrd_ptsfr_oid = ptsfr_oid " _
            & "  inner join loc_mstr from_loc_mstr on from_loc_mstr.loc_id = ptsfr_loc_id " _
            & "  inner join loc_mstr to_loc_mstr on to_loc_mstr.loc_id = ptsfr_loc_to_id " _
            & "  left outer join cmaddr_mstr from_cmaddr_mstr on from_cmaddr_mstr.cmaddr_id = ptsfr_en_id " _
            & "  left outer join cmaddr_mstr to_cmaddr_mstr on to_cmaddr_mstr.cmaddr_id = ptsfr_en_to_id " _
            & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = ptsfrd_um" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = ptsfr_oid " _
            & "  where ptsfr_code >= '" + be_first.Text + "'" _
            & "  and ptsfr_code <= '" + be_to.Text + "'" _
            & "  order by ptsfr_code "


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRTransferReceiptPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()


    End Sub
End Class
