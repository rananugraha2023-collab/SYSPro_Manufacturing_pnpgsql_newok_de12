Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FWorkCenterOutstandingChartPrint
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
        'de_first.EditValue = CekTanggal()
        'de_end.EditValue = CekTanggal()

    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        'Dim frm As New FInventoryRequestSearch()
        'frm.set_win(Me)
        'frm._en_id = le_entity.EditValue
        'frm._obj = be_first
        'frm.type_form = True
        'frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        'Dim frm As New FInventoryRequestSearch()
        'frm.set_win(Me)
        'frm._en_id = le_entity.EditValue
        'frm._obj = be_to
        'frm.type_form = True
        'frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String
        'Dim func_coll As New function_collection

        '_en_id = le_entity.EditValue
        '_type = 10
        '_table = "pb_mstr"
        '_initial = "pb"
        '_code_awal = Trim(be_first.Text)
        '_code_akhir = Trim(be_to.Text)

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        'Dim ds_bantu As New DataSet
        'Dim _sql As String

        '_sql = "SELECT  " _
        '        & "  a.lbrf_oid,lbrf_code,lbrf_person, " _
        '        & "  a.lbrf_dom_id, " _
        '        & "  a.lbrf_en_id, " _
        '        & "  b.en_desc, " _
        '        & "  a.lbrf_wodr_uid, " _
        '        & "  d.wo_code,wodr_wc_id, " _
        '        & "  dpt_code || '_' || dpt_desc as wc_desc, mch_name,dpt_code, " _
        '        & "  e.code_name as reason_name, " _
        '        & "  j.code_name as shift_name, " _
        '        & "  a.lbrf_qty_complete + coalesce(a.lbrf_qty_reject,0)  as lbrf_qty_complete, " _
        '        & "  a.lbrf_qty_reject, " _
        '        & "  a.lbrf_date, " _
        '        & "  a.lbrf_start_setup, " _
        '        & "  a.lbrf_stop_setup, " _
        '        & "  a.lbrf_elapsed_setup, " _
        '        & "  a.lbrf_start_run, " _
        '        & "  a.lbrf_stop_run, " _
        '        & "  a.lbrf_elapsed_run, " _
        '        & "  a.lbrf_down_start, " _
        '        & "  a.lbrf_down_stop, " _
        '        & "  a.lbrf_elapsed_down,lbrf_qty_conversion, " _
        '        & "  a.lbrf_down_reason_id, " _
        '        & "  h.qc_desc as qc_desc_in, " _
        '        & "  g.qc_desc as qc_desc_out, " _
        '        & "  a.lbrf_qc_out_reason_id,pt_desc1, " _
        '        & "  a.lbrf_qc_in_reason_id, " _
        '        & "  a.lbrf_remarks,lbrf_add_by,lbrf_add_date,lbrf_upd_by,lbrf_upd_date,lbrf_activity_type_id,lbrfa_desc  " _
        '        & " FROM " _
        '        & "  public.lbrf_mstr a " _
        '        & "  INNER JOIN public.en_mstr b ON (a.lbrf_en_id = b.en_id) " _
        '        & "  INNER JOIN public.wodr_routing c ON (a.lbrf_wodr_uid = c.wodr_uid) " _
        '        & "  INNER JOIN public.wo_mstr d ON (c.wodr_wo_oid = d.wo_oid) " _
        '         & "  INNER JOIN public.pt_mstr k ON (d.wo_pt_id = k.pt_id) " _
        '        & "  LEFT outer JOIN public.code_mstr e ON (a.lbrf_down_reason_id = e.code_id) " _
        '        & "  LEFT outer JOIN public.wc_mstr f ON (c.wodr_wc_id = f.wc_id) " _
        '        & "  LEFT outer JOIN public.qc_reason_mstr g ON  (a.lbrf_qc_out_reason_id = g.qc_id) " _
        '        & "  LEFT outer JOIN public.qc_reason_mstr h ON (a.lbrf_qc_in_reason_id = h.qc_id) " _
        '        & "  LEFT outer JOIN public.lbrfa_activity i ON (a.lbrf_activity_type_id = i.lbrfa_id) " _
        '        & "  LEFT outer JOIN public.code_mstr j ON (a.lbrf_shift_id = j.code_id) " _
        '        & "   LEFT outer JOIN public.dpt_mstr ON (f.wc_dpt_id = public.dpt_mstr.dpt_id) " _
        '        & "  LEFT outer JOIN public.mch_mstr l ON (a.lbrf_mch_id = l.mch_id) " _
        '        & " WHERE " _
        '        & "  a.lbrf_date BETWEEN " & SetDate(de_first.DateTime) & " AND " & SetDate(de_end.DateTime) & " " _
        '        & " and lbrf_en_id IN (select user_en_id from tconfuserentity " _
        '                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
        '        & " ORDER BY " _
        '        & "  a.lbrf_date"



        'Dim dt_temp As New DataTable

        'dt_temp = GetTableData(_sql)

        'Dim rpt As New XR_LabourfeedbackSummary
        'Try
        '    With rpt

        '        If dt_temp.Rows.Count = 0 Then
        '            MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            Exit Sub
        '        End If

        '        .XrPeriode.Text = de_first.DateTime.Date.ToString("dd/MM/yyyy") & " to " & de_end.DateTime.Date.ToString("dd/MM/yyyy")
        '        .XrPivotGrid1.DataSource = dt_temp
        '        '.DataSource = dt_temp
        '        '.DataMember = "Table1"
        '        printTool.ShowPreview()
        '    End With
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        SimpleButton1_Click(Nothing, Nothing)

    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Try
            Dim ds_bantu As New DataSet
            Dim _sql As String

            _sql = "SELECT  " _
             & " dpt_code || '_' || dpt_desc as ngroup ,dpt_id, " _
             & " sum(coalesce(dpt_lbr_cap,0)) as nvalue  " _
             & "FROM " _
             & "  public.dpt_mstr  " _
             & "  group by ngroup,dpt_id "


            Dim dt_standard As New DataTable
            Dim dt_standard_book As New DataTable
            Dim dt_wip_quran As New DataTable
            Dim dt_wip_book As New DataTable

            dt_standard = GetTableData(_sql)


            _sql = "SELECT  " _
              & " dpt_code || '_' || dpt_desc as ngroup ,dpt_id, " _
              & " sum(coalesce(dpt_lbr_cap_book,0)*-1) as nvalue " _
              & "FROM " _
              & "  public.dpt_mstr  " _
              & "  group by ngroup,dpt_id "

            dt_standard_book = GetTableData(_sql)


            _sql = "SELECT  " _
             & " dpt_code || '_' || dpt_desc as ngroup , dpt_id," _
             & " sum((coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0))) as nvalue " _
             & "FROM " _
             & "  public.wo_mstr  " _
             & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
             & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
             & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
             & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
             & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
             & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
              & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
             & " Where pt_prj.pt_group=99280 and wo_status <> 'X' and not (wc_desc ~~* '%velt%') group by ngroup,dpt_id "

            dt_wip_quran = GetTableData(_sql)


            For Each dr As DataRow In dt_wip_quran.Rows
                If dr("nvalue") = 0.0 Then
                    dr.Delete()
                End If
            Next
            dt_wip_quran.AcceptChanges()


            Dim _found As Boolean = False
            For Each dr As DataRow In dt_standard.Rows
                _found = False

                For Each dr2 As DataRow In dt_wip_quran.Rows
                    If dr("ngroup") = dr2("ngroup") Then
                        _found = True
                    End If
                Next
                If _found = False Then
                    dr.Delete()
                End If
            Next
            dt_standard.AcceptChanges()


            ' Dim dt_temp3 As New DataTable
            _sql = "SELECT  " _
                & " dpt_code || '_' || dpt_desc as ngroup ,dpt_id, " _
                & " sum((coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0))*-1) as nvalue " _
                & "FROM " _
                & "  public.wo_mstr  " _
                & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
                & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
                & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
                & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
                & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
                & " Where   pt_prj.pt_group=9910426 and wo_status <> 'X' and not (wc_desc ~~* '%velt%') group by ngroup,dpt_id"

            dt_wip_book = GetTableData(_sql)

            For Each dr As DataRow In dt_wip_book.Rows
                If dr("nvalue") = 0.0 Then
                    dr.Delete()
                End If
            Next
            dt_wip_book.AcceptChanges()

            'Dim _found As Boolean = False
            For Each dr As DataRow In dt_standard_book.Rows
                _found = False

                For Each dr2 As DataRow In dt_wip_book.Rows
                    If dr("ngroup") = dr2("ngroup") Then
                        _found = True
                    End If
                Next
                If _found = False Then
                    dr.Delete()
                End If
            Next
            dt_standard_book.AcceptChanges()

            Dim _dpt_id As String = ""

            For Each dr As DataRow In dt_wip_quran.Rows
                If dr("nvalue") = 0.0 Then
                    'dr.Delete()
                Else
                    _dpt_id = _dpt_id & dr("dpt_id") & ","
                End If

            Next

            'dt_wip_quran.AcceptChanges()


            If _dpt_id.Length > 0 Then
                _dpt_id = Microsoft.VisualBasic.Left(_dpt_id, _dpt_id.Length - 1)
            Else
                _dpt_id = 0
            End If




            Dim _dpt_id2 As String = ""

            For Each dr As DataRow In dt_wip_book.Rows
                If dr("nvalue") = 0.0 Then
                    'dr.Delete()
                Else
                    _dpt_id2 = _dpt_id2 & dr("dpt_id") & ","
                End If

            Next

            If _dpt_id2.Length > 0 Then
                _dpt_id2 = Microsoft.VisualBasic.Left(_dpt_id2, _dpt_id2.Length - 1)
            Else
                _dpt_id2 = 0
            End If


            Dim dt_pivot As New DataTable

            _sql = "SELECT  " _
                & " dpt_code || '_' || dpt_desc as ngroup , 0.0 as nvalue_standard, " _
                & " sum((coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0))) as nvalue_wip,0.0 as nvalue_standard_book,0.0 as nvalue_wip_book " _
                & "FROM " _
                & "  public.wo_mstr  " _
                & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
                & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
                & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
                & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
                & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
                & " Where  pt_prj.pt_group=99280 and wo_status <> 'X' and not (wc_desc ~~* '%velt%') group by ngroup " _
                & " union all SELECT  " _
                & " dpt_code || '_' || dpt_desc as ngroup , " _
                & " sum(coalesce(dpt_lbr_cap,0)) as nvalue_standard,0.0 as nvalue_wip,0.0 as nvalue_standard_book,0.0 as nvalue_wip_book " _
                & "FROM " _
                & "  public.dpt_mstr where dpt_id in (" & _dpt_id & ") group by ngroup " _
                 & " union all SELECT  " _
                & " dpt_code || '_' || dpt_desc as ngroup , " _
                & " 0.0 as nvalue_standard,0.0 as nvalue_wip,sum(coalesce(dpt_lbr_cap_book,0)) as nvalue_standard_book,0.0 as nvalue_wip_book " _
                & "FROM " _
                & "  public.dpt_mstr where dpt_id in (" & _dpt_id2 & ") group by ngroup " _
                & " union all SELECT  " _
                & " dpt_code || '_' || dpt_desc as ngroup , 0.0 as nvalue_standard, " _
                & " 0.0 as nvalue_wip,0.0 as nvalue_standard_book, sum((coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0))) as nvalue_wip_book " _
                & "FROM " _
                & "  public.wo_mstr  " _
                & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
                & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
                & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
                & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
                & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
                & " Where  pt_prj.pt_group=9910426 and wo_status <> 'X' and not (wc_desc ~~* '%velt%') group by ngroup "

            dt_pivot = GetTableData(_sql)


            'wo_ord_date between " & SetDate(de_first.DateTime.Date) _
            '    & " and " & SetDate(de_end.DateTime.Date) & " and

            '"SELECT  " _
            '& " dpt_code || '_' || dpt_desc as ngroup , " _
            '& " sum(coalesce(dpt_lbr_cap,0)) as nvalue_standard,0.0 as nvalue_wip " _
            '& "FROM " _
            '& "  public.dpt_mstr group by ngroup " _
            '& "  union all "




            Dim rpt As New XRWIPChart
            Try
                With rpt

                    If dt_standard.Rows.Count = 0 Then
                        MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    .XrPeriode.Text = "" 'de_first.DateTime.Date.ToString("dd/MM/yyyy") & " to " & de_end.DateTime.Date.ToString("dd/MM/yyyy")
                    '.XrPivotGrid1.DataSource = dt_temp

                    .XrChart1.Series(0).DataSource = dt_standard
                    .XrChart1.Series(1).DataSource = dt_wip_quran
                    .XrChart1.Series(2).DataSource = dt_standard_book
                    .XrChart1.Series(3).DataSource = dt_wip_book

                    .XrChart1.Series(0).Name = "Standard Al-Quran"
                    .XrChart1.Series(1).Name = "WIP Al-Quran"
                    .XrChart1.Series(2).Name = "Standard Book"
                    .XrChart1.Series(3).Name = "WIP Book"
                    .XrPivotGrid1.DataSource = dt_pivot

                    '.DataSource = dt_temp
                    '.DataMember = "Table1"
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                    'printTool.PreviewForm.Text = "Personal Selling SDI Report"
                    printTool.ShowPreview()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtShowDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtShowDetail.Click
        Try
            Dim ssql As String
            ssql = "SELECT  " _
                & " wo_code, wodr_seq, dpt_code || '_' || dpt_desc as ngroup ,wc_desc , wodr_routing.wodr_qty_in, wodr_qty_complete,wodr_qty_reject , " _
                & " (coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0)) as nvalue_wip,0.0 as nvalue_wip_book " _
                & "FROM " _
                & "  public.wo_mstr  " _
                & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
                & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
                & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
                & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
                & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
                & " Where  pt_prj.pt_group=99280 and wo_status <> 'X' and not (wc_desc ~~* '%velt%')  " _
                & " union all SELECT  " _
                & " wo_code, wodr_seq, dpt_code || '_' || dpt_desc as ngroup ,wc_desc , wodr_routing.wodr_qty_in, wodr_qty_complete,wodr_qty_reject , " _
                & " 0.0 as nvalue_wip,(coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0)) as nvalue_wip_book " _
                & "FROM " _
                & "  public.wo_mstr  " _
                & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
                & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
                & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
                & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
                & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
                & " Where  pt_prj.pt_group=9910426 and wo_status <> 'X' and not (wc_desc ~~* '%velt%') order by ngroup "

            Dim frm As New frmExport
            'Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

            With frm
                .Text = "Work Center Outstanding Detail"
                add_column_copy(.gv_export, "WO Number", "wo_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Sequence", "wodr_seq", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Group", "ngroup", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Qty IN", "wodr_qty_in", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Complete", "wodr_qty_complete", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Reject", "wodr_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty WIP Quran", "nvalue_wip", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty WIP Book", "nvalue_wip_book", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


                .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                .gv_export.BestFitColumns()
                .ShowDialog()
            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
