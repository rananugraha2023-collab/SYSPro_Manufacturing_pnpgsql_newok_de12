Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FUpdateStock
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Dim dt_lokal As New DataTable
    Dim ssql As String
    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        init_le(le_entity, "en_mstr")
        init_le(par_loc, "loc_mstr", le_entity.EditValue)
        add_column_copy(gv_loc, "PT ID", "invc_pt_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "PT Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "PT Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Qty Local", "invc_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_loc, "Qty Pusat", "qty_pusat", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub


    Public Overrides Sub preview()



    End Sub


    Private Sub pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try

        Catch ex As Exception
            'Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged
        Try
            init_le(par_loc, "loc_mstr", le_entity.EditValue)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtUpdate.Click
        Try
            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " proses data ini..?", "Proses", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim ssql As String
            Dim sSQLs As New ArrayList

            'ssql = "select * from invc_mstr where invc_en_id=4 and invc_loc_id=302"

            'Dim dt As New DataTable
            'dt = GetTableData(ssql)
            'Dim _hasil As String = ""

            For Each dr As DataRow In dt_lokal.Rows
                ssql = "update invc_mstr set invc_qty=" & SetDbl(dr("qty_pusat")) & " where invc_pt_id=" & SetInteger(dr("invc_pt_id")) & _
                " and invc_en_id=" & SetInteger(dr("invc_en_id")) & " and invc_loc_id=" & SetInteger(dr("invc_loc_id"))

                sSQLs.Add(ssql)

                '_hasil = _hasil & ssql & vbNewLine

            Next
            'Clipboard.SetText(_hasil)
            DbRunTran(sSQLs)
            Box("Update data success")


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtLoad.Click
        Try

            Dim sSQLs As New ArrayList

            ssql = "select   public.invc_mstr.invc_oid, " _
                    & " invc_en_id,invc_loc_id, " _
                    & "  public.invc_mstr.invc_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.invc_mstr.invc_qty, 0.0 as qty_pusat " _
                     & " from invc_mstr " _
                     & "  INNER JOIN public.pt_mstr ON (public.invc_mstr.invc_pt_id = public.pt_mstr.pt_id) " _
                     & " where invc_en_id=4 and invc_loc_id=302"


            dt_lokal = GetTableData(ssql)

            Dim dt_pusat As New DataTable
            dt_pusat = GetTableData(ssql, "SVR2")

            For Each dr As DataRow In dt_lokal.Rows
                For Each dr_pusat As DataRow In dt_pusat.Rows
                    If (dr("invc_pt_id") = dr_pusat("invc_pt_id")) Then
                        If (dr("invc_qty") = dr_pusat("invc_qty")) Then
                            dr.Delete()
                        Else
                            dr("qty_pusat") = dr_pusat("invc_qty")
                        End If
                        Exit For
                    End If
                Next
            Next

            dt_lokal.AcceptChanges()

            gc_loc.DataSource = dt_lokal
            gv_loc.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtTransfer.Click
        Try
            'ssql = "SELECT  " _
            '    & "  public.ptsfr_mstr.ptsfr_oid, " _
            '    & "  public.ptsfr_mstr.ptsfr_dom_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_en_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_add_by, " _
            '    & "  public.ptsfr_mstr.ptsfr_add_date, " _
            '    & "  public.ptsfr_mstr.ptsfr_upd_by, " _
            '    & "  public.ptsfr_mstr.ptsfr_upd_date, " _
            '    & "  public.ptsfr_mstr.ptsfr_en_to_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_code, " _
            '    & "  public.ptsfr_mstr.ptsfr_date, " _
            '    & "  public.ptsfr_mstr.ptsfr_receive_date, " _
            '    & "  public.ptsfr_mstr.ptsfr_si_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_loc_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_loc_git, " _
            '    & "  public.ptsfr_mstr.ptsfr_remarks, " _
            '    & "  public.ptsfr_mstr.ptsfr_trans_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_dt, " _
            '    & "  public.ptsfr_mstr.ptsfr_loc_to_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_si_to_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_pb_oid, " _
            '    & "  public.ptsfr_mstr.ptsfr_so_oid, " _
            '    & "  public.ptsfr_mstr.pt_tax_class, " _
            '    & "  public.ptsfr_mstr.ptsfr_tran_id, " _
            '    & "  public.ptsfr_mstr.ptsfr_sq_oid, " _
            '    & "  public.ptsfr_mstr.ptsfr_is_transfer " _
            '    & "FROM " _
            '    & "  public.ptsfr_mstr " _
            '    & "WHERE " _
            '    & " public.ptsfr_mstr.ptsfr_add_date >= '1/3/2020 0:00:00' " _
            '    & " and public.ptsfr_mstr.ptsfr_add_by = 'Saiful Heru'"

            'Dim dt_trf As New DataTable
            'dt_trf = GetTableData(ssql)

            'For Each dr As DataRow In dt_trf.Rows

            'Next

            Dim frm As New frmRunFromServer
            frm.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtSOShipment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSOShipment.Click

    End Sub
End Class
