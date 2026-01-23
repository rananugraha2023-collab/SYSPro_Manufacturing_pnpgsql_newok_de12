Public Class FListRoutingPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FWOReceiptPrintSign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FRoutingSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FRoutingSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        Dim _conf_document_sign As Integer
        Dim _document_name As String = "List Routing"
        Dim _code_id As Integer = 0

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_id from code_mstr where code_field ~~* 'type_document' and code_code ~~* 'RO'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _code_id = .DataReader.Item("code_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        _conf_document_sign = func_coll.get_conf_file("document_sign")

        If _conf_document_sign = 1 Then
            'If ce_serial.Checked = True Then
            '    _sql = "SELECT  " _
            '        & "  e.en_desc, " _
            '        & "  a.wor_code, " _
            '        & "  a.wor_date, " _
            '        & "  a.wor_date_eff, " _
            '        & "  b.wo_code, " _
            '        & "  b.wo_remarks, " _
            '        & "  f.loc_code, " _
            '        & "  f.loc_desc, " _
            '        & "  g.si_code, " _
            '        & "  g.si_desc, " _
            '        & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
            '        & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
            '        & "  a.wor_remarks, " _
            '        & "  b.wo_remarks, " _
            '        & "  c.pt_code, " _
            '        & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
            '        & "  d.code_name AS unit_measure, " _
            '        & "  a.wor_close,wc_desc, " _
            '        & "  s.wors_qty,s.wors_lot_serial,um.code_desc as um_desc, " _
            '        & "  (SELECT coalesce(docappd_user_id,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
            '        & "          WHERE docappd_seq = 0 and code_id = " + _code_id.ToString + " limit 1) as sign1, " _
            '        & "  (SELECT coalesce(docappd_user_id,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
            '        & "          WHERE docappd_seq = 1 and code_id = " + _code_id.ToString + " limit 1) as sign2, " _
            '        & "  (SELECT coalesce(docappd_user_id,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
            '        & "          WHERE docappd_seq = 2 and code_id = " + _code_id.ToString + " limit 1) as sign3, " _
            '        & "  (SELECT coalesce(docappd_position,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
            '        & "          WHERE docappd_seq = 0 and code_id = " + _code_id.ToString + " limit 1) as post1, " _
            '        & "  (SELECT coalesce(docappd_position,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
            '        & "          WHERE docappd_seq = 1 and code_id = " + _code_id.ToString + " limit 1) as post2, " _
            '        & "  (SELECT coalesce(docappd_position,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
            '        & "          WHERE docappd_seq = 2 and code_id = " + _code_id.ToString + " limit 1) as post3 " _
            '        & "FROM " _
            '        & "  public.wor_mstr a " _
            '        & "  LEFT OUTER JOIN wors_serial s on s.wors_wor_oid = a.wor_oid " _
            '        & "  LEFT OUTER JOIN code_mstr um on um.code_id = s.wors_um " _
            '        & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
            '        & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
            '        & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
            '        & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
            '        & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
            '        & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
            '                     & "  LEFT OUTER JOIN public.wc_mstr ON (a.wor_wc_id = public.wc_mstr.wc_id)" _
            '        & "  where a.wor_code >= '" + be_first.Text + "'" _
            '        & "  and a.wor_code <= '" + be_to.Text + "'" _
            '        & " ORDER BY a.wor_code "

            '    Dim rpt As New rptWOReceipts_2
            '    Try
            '        With rpt
            '            Try
            '                Using objcb As New master_new.WDABasepgsql("", "")
            '                    With objcb
            '                        .SQL = _sql
            '                        .InitializeCommand()
            '                        .FillDataSet(ds_bantu, "data")
            '                    End With
            '                End Using
            '            Catch ex As Exception
            '                MessageBox.Show(ex.Message)
            '                Exit Sub
            '            End Try

            '            If ds_bantu.Tables(0).Rows.Count = 0 Then
            '                MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Exit Sub
            '            End If

            '            .DataSource = ds_bantu
            '            .DataMember = "data"
            '            printTool.ShowPreview()
            '        End With
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try
            'Else
            _sql = "SELECT  " _
                & "  a.ro_oid, " _
                & "  a.ro_dom_id, " _
                & "  a.ro_en_id, " _
                & "  b.en_desc, " _
                & "  a.ro_add_by, " _
                & "  a.ro_add_date, " _
                & "  a.ro_upd_by, " _
                & "  a.ro_upd_date, " _
                & "  a.ro_id, " _
                & "  a.ro_code, " _
                & "  a.ro_desc, " _
                & "  a.ro_active, " _
                & "  a.ro_dt, " _
                & "  a.ro_cs_id, " _
                & "  c.cs_name, " _
                & "  a.ro_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  d.pt_desc2, " _
                & "  a.ro_yield, " _
                & "  a.ro_insheet, " _
                & "  a.ro_total, " _
                & "  a.ro_is_default, " _
                & "  i.rod_oid, " _
                & "  i.rod_ro_oid, " _
                & "  i.rod_add_by, " _
                & "  i.rod_add_date, " _
                & "  i.rod_upd_by, " _
                & "  i.rod_upd_date, " _
                & "  i.rod_op, o.op_name, " _
                & "  i.rod_start_date, " _
                & "  i.rod_end_date, " _
                & "  i.rod_wc_id, " _
                & "  i.rod_desc, " _
                & "  i.rod_mch_op, " _
                & "  i.rod_tran_qty, " _
                & "  i.rod_queue, " _
                & "  i.rod_wait, " _
                & "  i.rod_move, " _
                & "  i.rod_run, " _
                & "  i.rod_setup, " _
                & "  i.rod_yield_pct, " _
                & "  i.rod_milestone, " _
                & "  i.rod_sub_lead, " _
                & "  i.rod_setup_men, " _
                & "  i.rod_men_mch, " _
                & "  i.rod_tool_code, " _
                & "  i.rod_ptnr_id, " _
                & "  i.rod_sub_cost, " _
                & "  i.rod_dt, " _
                & "  i.rod_lbr_ac_id, " _
                & "  i.rod_lbr_amount, " _
                & "  i.rod_bdn_ac_id, " _
                & "  i.rod_bdn_amount, " _
                & "  i.rod_sbc_ac_id, " _
                & "  i.rod_sbc_amount, " _
                & "  i.rod_seq, " _
                & "  j.wc_desc, " _
                & "  k.code_name, " _
                & "  l.ptnr_name,  " _
                & "  ac_lbr.ac_code as ac_code_lbr, ac_lbr.ac_name as ac_name_lbr, " _
                & " ac_bdn.ac_code as ac_code_bdn, ac_bdn.ac_name as ac_name_bdn, " _
                & " ac_sbc.ac_code as ac_code_sbc, ac_sbc.ac_name as ac_name_sbc,rod_lbr_ac_id,rod_bdn_ac_id,rod_sbc_ac_id,rod_lbr_amount,rod_bdn_amount,rod_sbc_amount, " _
                & "  (SELECT coalesce(docappd_user_id,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                & "          WHERE docappd_seq = 0 and code_id = " + _code_id.ToString + " limit 1) as sign1, " _
                & "  (SELECT coalesce(docappd_user_id,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                & "          WHERE docappd_seq = 1 and code_id = " + _code_id.ToString + " limit 1) as sign2, " _
                & "  (SELECT coalesce(docappd_user_id,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                & "          WHERE docappd_seq = 2 and code_id = " + _code_id.ToString + " limit 1) as sign3, " _
                & "  (SELECT coalesce(docappd_position,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                & "          WHERE docappd_seq = 0 and code_id = " + _code_id.ToString + " limit 1) as post1, " _
                & "  (SELECT coalesce(docappd_position,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                & "          WHERE docappd_seq = 1 and code_id = " + _code_id.ToString + " limit 1) as post2, " _
                & "  (SELECT coalesce(docappd_position,'') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                & "          WHERE docappd_seq = 2 and code_id = " + _code_id.ToString + " limit 1) as post3 " _
                & "FROM " _
                & "  public.en_mstr b " _
                & "  INNER JOIN public.ro_mstr a ON (b.en_id = a.ro_en_id) " _
                & "  LEFT OUTER JOIN public.cs_mstr c ON (c.cs_id = a.ro_cs_id) " _
                & "  LEFT OUTER JOIN public.pt_mstr d ON (a.ro_pt_id = d.pt_id) " _
                & "  INNER JOIN public.rod_det i ON (a.ro_oid = i.rod_ro_oid) " _
                & "  INNER JOIN wc_mstr j on (i.rod_wc_id= j.wc_id) " _
                & " LEFT OUTER JOIN code_mstr k on (i.rod_tool_code= k.code_id) " _
                & " LEFT OUTER JOIN ptnr_mstr l on (i.rod_ptnr_id= l.ptnr_id) " _
                & " LEFT OUTER JOIN ac_mstr  ac_lbr on (ac_lbr.ac_id= i.rod_lbr_ac_id) " _
                & " LEFT OUTER JOIN ac_mstr ac_bdn on (ac_bdn.ac_id= i.rod_bdn_ac_id) " _
                & " LEFT OUTER JOIN ac_mstr ac_sbc on (ac_sbc.ac_id= i.rod_sbc_ac_id) " _
                & "  LEFT OUTER JOIN op_mstr o  on (i.rod_op= o.op_code) " _
                & "  where a.ro_code >= '" + be_first.Text + "'" _
                & "  and a.ro_code <= '" + be_to.Text + "'" _
                & " ORDER BY i.rod_seq "

            Dim rpt As New XR_routing
            Try
                With rpt
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = _sql
                                .InitializeCommand()
                                .FillDataSet(ds_bantu, "data")
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Exit Sub
                    End Try

                    If ds_bantu.Tables(0).Rows.Count = 0 Then
                        MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    .DataSource = ds_bantu
                    .DataMember = "data"
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                    'printTool.PreviewForm.Text = "Personal Selling SDI Report"
                    printTool.ShowPreview()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            'End If

        Else
            'If ce_serial.Checked = True Then
            '    _sql = "SELECT  " _
            '        & "  e.en_desc, " _
            '        & "  a.wor_code, " _
            '        & "  a.wor_date, " _
            '        & "  a.wor_date_eff, " _
            '        & "  b.wo_code, " _
            '        & "  b.wo_remarks, " _
            '        & "  f.loc_code, " _
            '        & "  f.loc_desc, " _
            '        & "  g.si_code, " _
            '        & "  g.si_desc, " _
            '        & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
            '        & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
            '        & "  a.wor_remarks, " _
            '        & "  b.wo_remarks, " _
            '        & "  c.pt_code, " _
            '        & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
            '        & "  d.code_name AS unit_measure, " _
            '        & "  a.wor_close,wc_desc, " _
            '        & "  s.wors_qty,s.wors_lot_serial,um.code_desc as um_desc " _
            '        & "FROM " _
            '        & "  public.wor_mstr a " _
            '        & "  LEFT OUTER JOIN wors_serial s on s.wors_wor_oid = a.wor_oid " _
            '        & "  LEFT OUTER JOIN code_mstr um on um.code_id = s.wors_um " _
            '        & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
            '        & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
            '        & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
            '        & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
            '        & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
            '        & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
            '                     & "  LEFT OUTER JOIN public.wc_mstr ON (a.wor_wc_id = public.wc_mstr.wc_id)" _
            '        & "  where a.wor_code >= '" + be_first.Text + "'" _
            '        & "  and a.wor_code <= '" + be_to.Text + "'" _
            '        & " ORDER BY a.wor_code "

            '    Dim rpt As New rptWOReceipts_2
            '    Try
            '        With rpt
            '            Try
            '                Using objcb As New master_new.WDABasepgsql("", "")
            '                    With objcb
            '                        .SQL = _sql
            '                        .InitializeCommand()
            '                        .FillDataSet(ds_bantu, "data")
            '                    End With
            '                End Using
            '            Catch ex As Exception
            '                MessageBox.Show(ex.Message)
            '                Exit Sub
            '            End Try

            '            If ds_bantu.Tables(0).Rows.Count = 0 Then
            '                MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Exit Sub
            '            End If

            '            .DataSource = ds_bantu
            '            .DataMember = "data"
            '            printTool.ShowPreview()
            '        End With
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try
            'Else

            _sql = "SELECT  " _
                & "  a.ro_oid, " _
                & "  a.ro_dom_id, " _
                & "  a.ro_en_id, " _
                & "  b.en_desc, " _
                & "  a.ro_add_by, " _
                & "  a.ro_add_date, " _
                & "  a.ro_upd_by, " _
                & "  a.ro_upd_date, " _
                & "  a.ro_id, " _
                & "  a.ro_code, " _
                & "  a.ro_desc, " _
                & "  a.ro_active, " _
                & "  a.ro_dt, " _
                & "  a.ro_cs_id, " _
                & "  c.cs_name, " _
                & "  a.ro_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  d.pt_desc2, " _
                & "  a.ro_yield, " _
                & "  a.ro_total, " _
                & "  a.ro_is_default, " _
                & "  i.rod_oid, " _
                & "  i.rod_ro_oid, " _
                & "  i.rod_add_by, " _
                & "  i.rod_add_date, " _
                & "  i.rod_upd_by, " _
                & "  i.rod_upd_date, " _
                & "  i.rod_op, o.op_name, " _
                & "  i.rod_start_date, " _
                & "  i.rod_end_date, " _
                & "  i.rod_wc_id, " _
                & "  i.rod_desc, " _
                & "  i.rod_mch_op, " _
                & "  i.rod_tran_qty, " _
                & "  i.rod_queue, " _
                & "  i.rod_wait, " _
                & "  i.rod_move, " _
                & "  i.rod_run, " _
                & "  i.rod_setup, " _
                & "  i.rod_yield_pct, " _
                & "  i.rod_milestone, " _
                & "  i.rod_sub_lead, " _
                & "  i.rod_setup_men, " _
                & "  i.rod_men_mch, " _
                & "  i.rod_tool_code, " _
                & "  i.rod_ptnr_id, " _
                & "  i.rod_sub_cost, " _
                & "  i.rod_dt, " _
                & "  i.rod_lbr_ac_id, " _
                & "  i.rod_lbr_amount, " _
                & "  i.rod_bdn_ac_id, " _
                & "  i.rod_bdn_amount, " _
                & "  i.rod_sbc_ac_id, " _
                & "  i.rod_sbc_amount, " _
                & "  i.rod_seq, " _
                & "  j.wc_desc, " _
                & "  k.code_name, " _
                & "  l.ptnr_name,  " _
                & "  ac_lbr.ac_code as ac_code_lbr, ac_lbr.ac_name as ac_name_lbr, " _
                & " ac_bdn.ac_code as ac_code_bdn, ac_bdn.ac_name as ac_name_bdn, " _
                & " ac_sbc.ac_code as ac_code_sbc, ac_sbc.ac_name as ac_name_sbc,rod_lbr_ac_id,rod_bdn_ac_id,rod_sbc_ac_id,rod_lbr_amount,rod_bdn_amount,rod_sbc_amount " _
                & "FROM " _
                & "  public.en_mstr b " _
                & "  INNER JOIN public.ro_mstr a ON (b.en_id = a.ro_en_id) " _
                & "  LEFT OUTER JOIN public.cs_mstr c ON (c.cs_id = a.ro_cs_id) " _
                & "  LEFT OUTER JOIN public.pt_mstr d ON (a.ro_pt_id = d.pt_id) " _
                & "  INNER JOIN public.rod_det i ON (a.ro_oid = i.rod_ro_oid) " _
                & "  INNER JOIN wc_mstr j on (i.rod_wc_id= j.wc_id) " _
                & " LEFT OUTER JOIN code_mstr k on (i.rod_tool_code= k.code_id) " _
                & " LEFT OUTER JOIN ptnr_mstr l on (i.rod_ptnr_id= l.ptnr_id) " _
                & " LEFT OUTER JOIN ac_mstr  ac_lbr on (ac_lbr.ac_id= i.rod_lbr_ac_id) " _
                & " LEFT OUTER JOIN ac_mstr ac_bdn on (ac_bdn.ac_id= i.rod_bdn_ac_id) " _
                & " LEFT OUTER JOIN ac_mstr ac_sbc on (ac_sbc.ac_id= i.rod_sbc_ac_id) " _
                & "  LEFT OUTER JOIN op_mstr o  on (i.rod_op= o.op_code) " _
                & "  where a.ro_code >= '" + be_first.Text + "'" _
                & "  and a.ro_code <= '" + be_to.Text + "'" _
                & " ORDER BY i.rod_seq "

            'Dim rpt As New rptWOReceipts_1
            'Try
            '    With rpt
            '        Try
            '            Using objcb As New master_new.WDABasepgsql("", "")
            '                With objcb
            '                    .SQL = _sql
            '                    .InitializeCommand()
            '                    .FillDataSet(ds_bantu, "data")
            '                End With
            '            End Using
            '        Catch ex As Exception
            '            MessageBox.Show(ex.Message)
            '            Exit Sub
            '        End Try

            '        If ds_bantu.Tables(0).Rows.Count = 0 Then
            '            MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End If

            '        .DataSource = ds_bantu
            '        .DataMember = "data"
            '        printTool.ShowPreview()
            '    End With
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try
            'End If

        End If

    End Sub

    
End Class
