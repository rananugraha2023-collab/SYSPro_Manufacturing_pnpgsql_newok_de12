Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports MessagingToolkit.QRCode


Public Class XR_WO
    Public _pjc_code As String
    Dim QR_code As New MessagingToolkit.QRCode.Codec.QRCodeEncoder

    Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
       
    End Sub

    Private Sub XR_WO_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
        Catch ex As Exception

        End Try

        Try
            XrPictureBox1.Image = QR_code.Encode(GetCurrentColumnValue("wo_code"))
        Catch ex As Exception

        End Try

        Dim sSQL As String
        Try
            'sSQL = "SELECT  " _
            '        & "  a.ro_oid, " _
            '        & "  a.ro_dom_id, " _
            '        & "  a.ro_en_id, " _
            '        & "  b.en_desc, " _
            '        & "  a.ro_add_by, " _
            '        & "  a.ro_add_date, " _
            '        & "  a.ro_upd_by, " _
            '        & "  a.ro_upd_date, " _
            '        & "  a.ro_id, " _
            '        & "  a.ro_code, " _
            '        & "  a.ro_desc, " _
            '        & "  a.ro_active, " _
            '        & "  a.ro_dt, " _
            '        & "  a.ro_cs_id, " _
            '        & "  c.cs_name, " _
            '        & "  a.ro_pt_id, " _
            '        & "  d.pt_code, " _
            '        & "  d.pt_desc1, " _
            '        & "  d.pt_desc2, " _
            '        & "  a.ro_yield, " _
            '        & "  a.ro_total, " _
            '        & "  a.ro_is_default, " _
            '        & "  i.rod_oid, " _
            '        & "  i.rod_ro_oid, " _
            '        & "  i.rod_add_by, " _
            '        & "  i.rod_add_date, " _
            '        & "  i.rod_upd_by, " _
            '        & "  i.rod_upd_date, " _
            '        & "  i.rod_op, " _
            '        & "  o.op_name, " _
            '        & "  i.rod_start_date, " _
            '        & "  i.rod_end_date, " _
            '        & "  i.rod_wc_id, " _
            '        & "  i.rod_desc, " _
            '        & "  i.rod_mch_op, " _
            '        & "  i.rod_tran_qty, " _
            '        & "  i.rod_queue, " _
            '        & "  i.rod_wait, " _
            '        & "  i.rod_move, " _
            '        & "  i.rod_run, " _
            '        & "  i.rod_setup, " _
            '        & "  i.rod_yield_pct, " _
            '        & "  i.rod_milestone, " _
            '        & "  i.rod_sub_lead, " _
            '        & "  i.rod_setup_men, " _
            '        & "  i.rod_men_mch, " _
            '        & "  i.rod_tool_code, " _
            '        & "  i.rod_ptnr_id, " _
            '        & "  i.rod_sub_cost, " _
            '        & "  i.rod_dt, " _
            '        & "  i.rod_lbr_ac_id, " _
            '        & "  i.rod_lbr_amount, " _
            '        & "  i.rod_bdn_ac_id, " _
            '        & "  i.rod_bdn_amount, " _
            '        & "  i.rod_sbc_ac_id, " _
            '        & "  i.rod_sbc_amount, " _
            '        & "  i.rod_seq, " _
            '        & "  j.wc_desc, " _
            '        & "  k.code_name, " _
            '        & "  l.ptnr_name, " _
            '        & "  ac_lbr.ac_code AS ac_code_lbr, " _
            '        & "  ac_lbr.ac_name AS ac_name_lbr, " _
            '        & "  ac_bdn.ac_code AS ac_code_bdn, " _
            '        & "  ac_bdn.ac_name AS ac_name_bdn, " _
            '        & "  ac_sbc.ac_code AS ac_code_sbc, " _
            '        & "  ac_sbc.ac_name AS ac_name_sbc, " _
            '        & "  e.wo_qty, " _
            '        & "  e.wo_ref_rework, " _
            '        & "  e.wo_bom_id, " _
            '        & "  e.wo_qty_ord, " _
            '        & "  e.wo_qty_comp, " _
            '        & "  e.wo_qty_rjc " _
            '        & "FROM " _
            '        & "  public.en_mstr b " _
            '        & "  INNER JOIN public.ro_mstr a ON (b.en_id = a.ro_en_id) " _
            '        & "  LEFT OUTER JOIN public.cs_mstr c ON (c.cs_id = a.ro_cs_id) " _
            '        & "  LEFT OUTER JOIN public.pt_mstr d ON (a.ro_pt_id = d.pt_id) " _
            '        & "  INNER JOIN public.rod_det i ON (a.ro_oid = i.rod_ro_oid) " _
            '        & "  INNER JOIN wc_mstr j ON (i.rod_wc_id = j.wc_id) " _
            '        & "  LEFT OUTER JOIN code_mstr k ON (i.rod_tool_code = k.code_id) " _
            '        & "  LEFT OUTER JOIN ptnr_mstr l ON (i.rod_ptnr_id = l.ptnr_id) " _
            '        & "  LEFT OUTER JOIN ac_mstr ac_lbr ON (ac_lbr.ac_id = i.rod_lbr_ac_id) " _
            '        & "  LEFT OUTER JOIN ac_mstr ac_bdn ON (ac_bdn.ac_id = i.rod_bdn_ac_id) " _
            '        & "  LEFT OUTER JOIN ac_mstr ac_sbc ON (ac_sbc.ac_id = i.rod_sbc_ac_id) " _
            '        & "  LEFT OUTER JOIN op_mstr o ON (i.rod_op = o.op_code) " _
            '        & "  INNER JOIN public.wo_mstr e ON (a.ro_id = e.wo_ro_id) " _
            '        & "WHERE " _
            '        & "  e.wo_code = '" & _pjc_code & "'   "

            sSQL = "SELECT  " _
               & "  a.wodr_uid, " _
               & "  a.wodr_wo_oid, " _
               & "  a.wodr_op, " _
               & "  b.op_name, " _
               & "  a.wodr_start_date, " _
               & "  a.wodr_end_date, " _
               & "  a.wodr_desc, " _
               & "  a.wodr_wc_id, " _
               & "  c.wc_desc, " _
               & "  a.wodr_yield_pct, " _
               & "  a.wodr_seq, " _
               & "  a.wodr_qty_in, " _
               & "  a.wodr_qty_complete, " _
               & "  a.wodr_qty_reject,coalesce(wodr_qty_in,0) - coalesce(wodr_qty_complete,0) -  coalesce(wodr_qty_reject,0) as  wodr_qty_outstanding, " _
               & "  a.wodr_qty_out,wodr_run,wodr_setup,wodr_down, wodr_qty_conversion " _
               & "FROM " _
               & "  public.wodr_routing a " _
               & "  left outer JOIN public.op_mstr b ON (a.wodr_op = b.op_code) " _
               & "  left outer JOIN public.wc_mstr c ON (a.wodr_wc_id = c.wc_id) " _
               & "WHERE " _
               & "  a.wodr_wo_oid = '" & GetCurrentColumnValue("wo_oid").ToString & "' " _
               & " ORDER By wodr_seq"



            'Dim detail As XR_sub_routing = TryCast(XrSubreport1.ReportSource, XR_sub_routing)
            'detail.DataSource = GetTableData(sSQL)
            Dim ds_bantu As New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = sSQL
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "data")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            'If ds_bantu.Tables(0).Rows.Count = 0 Then
            '    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If

            Dim detail As New XR_sub_routing
            detail.DataSource = ds_bantu
            detail.DataMember = "data"

            XrSubreport1.ReportSource = detail

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class