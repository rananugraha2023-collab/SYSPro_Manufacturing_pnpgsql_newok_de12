Imports master_new.ModFunction
Imports npgsql

Public Class FInvIssuePrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FInvIssuePrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FInvRiuSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FInvRiuSearch()
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
        _type = 16
        _table = "riu_mstr"
        _initial = "riu"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        insert_tranaprvd_det_local(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  riu_mstr.riu_oid, " _
            & "  riu_mstr.riu_dom_id, " _
            & "  riu_mstr.riu_en_id, " _
            & "  riu_mstr.riu_add_by, " _
            & "  riu_mstr.riu_add_date, " _
            & "  riu_mstr.riu_upd_by, " _
            & "  riu_mstr.riu_upd_date, " _
            & "  riu_mstr.riu_type2, " _
            & "  riu_mstr.riu_date, " _
            & "  riu_mstr.riu_type, " _
            & "  riu_mstr.riu_remarks, " _
            & "  riu_mstr.riu_dt, " _
            & "  riu_mstr.riu_ref_so_code, " _
            & "  riu_mstr.riu_ref_so_oid, " _
            & "  riu_mstr.riu_ref_pb_oid, " _
            & "  riu_mstr.riu_ref_pb_code, " _
            & "  riud_det.riud_oid, " _
            & "  riud_det.riud_riu_oid, " _
            & "  riud_det.riud_pt_id, " _
            & "  riud_det.riud_qty * -1 as riud_qty, " _
            & "  riud_det.riud_um, " _
            & "  riud_det.riud_um_conv, " _
            & "  riud_det.riud_qty_real * -1 as riud_qty_real, " _
            & "  riud_det.riud_si_id, " _
            & "  riud_det.riud_loc_id, " _
            & "  riud_det.riud_lot_serial, " _
            & "  riud_det.riud_cost, " _
            & "  riud_det.riud_ac_id, " _
            & "  riud_det.riud_sb_id, " _
            & "  riud_det.riud_cc_id, " _
            & "  riud_det.riud_dt, " _
            & "  riud_det.riud_sod_oid, " _
            & "  riud_det.riud_pbd_oid, " _
            & "  loc_mstr.loc_desc, " _
            & "  si_mstr.si_desc, " _
            & "  pt_mstr.pt_code, " _
            & "  pt_mstr.pt_desc1, " _
            & "  pt_mstr.pt_desc2, " _
            & "  ltrim(coalesce(pt_desc1,'') || '' || coalesce(pt_desc2,'')) as pt_descriptions, " _
            & "  code_mstr.code_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM " _
            & "  riu_mstr " _
            & "  INNER JOIN riud_det ON (riu_mstr.riu_oid = riud_det.riud_riu_oid) " _
            & "  INNER JOIN si_mstr ON (riud_det.riud_si_id = si_mstr.si_id) " _
            & "  INNER JOIN loc_mstr ON (riud_det.riud_loc_id = loc_mstr.loc_id) " _
            & "  INNER JOIN pt_mstr ON (riud_det.riud_pt_id = pt_mstr.pt_id) " _
            & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id)" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = riu_oid  " _
            & "  where riu_type2 >= '" + be_first.Text + "'" _
            & "  and riu_type2 <= '" + be_to.Text + "'" _
            & " and riu_type ~~* 'I'" _
            & "  order by riu_type2 "



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvIssue"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub

    Private Function insert_tranaprvd_det_local(ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_table As String, ByVal par_initial As String, ByVal par_tran_code_awal As String, ByVal par_tran_code_akhir As String, ByVal par_date As Date) As Boolean
        insert_tranaprvd_det_local = True

        Dim i As Integer
        Dim dt_bantu As DataTable = New DataTable
        Dim dt_data As DataTable = New DataTable

        dt_bantu = (func_data.load_list_aprvd_dok(par_en_id, par_type, par_date))
        dt_data = (load_list_aprvd_data_local(par_en_id, par_tran_code_awal, par_tran_code_akhir, par_initial, par_table))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To dt_data.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from tranaprvd_dok where tranaprvd_tran_oid = '" + dt_data.Rows(i).Item("data_oid").ToString + "'"
                            'par_ssqls.Add(.Command.CommandText) gak perlu karena tidak penting..untuk disinkronisasikan
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tranaprvd_dok " _
                                                & "( " _
                                                & "  tranaprvd_oid, " _
                                                & "  tranaprvd_dom_id, " _
                                                & "  tranaprvd_en_id, " _
                                                & "  tranaprvd_add_by, " _
                                                & "  tranaprvd_add_date, " _
                                                & "  tranaprvd_dt, " _
                                                & "  tranaprvd_tran_oid, " _
                                                & "  tranaprvd_tran_code, " _
                                                & "  tranaprvd_name_1, " _
                                                & "  tranaprvd_pos_1, " _
                                                & "  tranaprvd_name_2, " _
                                                & "  tranaprvd_pos_2, " _
                                                & "  tranaprvd_name_3, " _
                                                & "  tranaprvd_pos_3, " _
                                                & "  tranaprvd_name_4, " _
                                                & "  tranaprvd_pos_4 " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(par_en_id) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & SetSetring(dt_data.Rows(i).Item("data_oid")) & ",  " _
                                                & SetSetring(dt_data.Rows(i).Item("data_code")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_1")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_1")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_2")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_2")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_3")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_3")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_4")) & ",  " _
                                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_4")) & "  " _
                                                & ")"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next
                        sqlTran.Commit()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert_tranaprvd_det_local = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            insert_tranaprvd_det_local = False
        End Try
    End Function

    Private Function load_list_aprvd_data_local(ByVal par_en_id As Integer, ByVal par_awal As String, ByVal par_akhir As String, ByVal par_initial As String, ByVal par_table As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & par_initial & "_oid as data_oid, " _
                        & par_initial & "_type2 as data_code " _
                        & "FROM  " _
                        & "  public." + par_table + " where " _
                        & par_initial & "_type2 >= '" + par_awal + "' and " _
                        & par_initial & "_type2 <= '" + par_akhir + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
End Class
