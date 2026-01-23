Imports DevExpress.XtraEditors.Controls

Public Class FSEReportPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        init_le(le_entity, "en_mstr")
        'init_le(le_periode, "sales_report_type")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_periode_mstr())
        'le_periode.Properties.DataSource = dt_bantu
        'le_periode.Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        'le_periode.Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
        'le_periode.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr_se())
        With le_periode
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New LookUpColumnInfo("seperiode_code", "Code", 20))
                .Properties.Columns.Add(New LookUpColumnInfo("seperiode_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                .Properties.Columns.Add(New LookUpColumnInfo("seperiode_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                .Properties.Columns.Add(New LookUpColumnInfo("seperiode_bonus_gen", "Generate Bonus", 20))
                .Properties.Columns.Add(New LookUpColumnInfo("seperiode_payment_date", "Payment Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                .Properties.Columns.Add(New LookUpColumnInfo("seperiode_remarks", "Remarks", 20))
            End If

            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("seperiode_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("seperiode_code").ToString
            If dt_bantu.Rows.Count > 0 Then
                .EditValue = dt_bantu.Rows(0).Item(.Properties.ValueMember)
            End If

            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 12
            .Properties.PopupWidth = 600
            .ItemIndex = 0
        End With


        CBReportType.Text = "Rekap Reseller"

    End Sub


    Public Overrides Sub preview()


        Dim _en_id_all As String

        If CeChild.EditValue = True Then
            _en_id_all = get_en_id_child(le_entity.EditValue)
        Else
            _en_id_all = le_entity.EditValue
        End If

        Dim _sql As String

        If CBReportType.Text.ToUpper = "REKAP RESELLER" Then

            _sql = "SELECT  " _
                & "  a.segend_oid, " _
                & "  a.segend_segen_oid, " _
                & "  a.segend_ptnr_id, " _
                & "  a.segend_level, " _
                & "  a.segend_entity || ' ' || a.segend_nama as segend_nama_sort,a.segend_nama ,ptnr_code, " _
                & "  a.segend_active_sales,segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents, " _
                & "  a.segend_su_sales, " _
                & "  a.segend_pengali_komisi, " _
                & "  a.segend_komisi, " _
                & "  a.segend_su_group, " _
                & "  a.segend_percent_add_income, " _
                & "  a.segend_pengali_add_income, " _
                & "  a.segend_add_income, " _
                & "  a.segend_percent_coaching_income, " _
                & "  a.segend_pengali_coaching_income, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_entity, " _
                & "  a.segend_lvl_id,segend_komisi_bulanan,segend_komisi_telah_dibayar, " _
                & "  a.segend_parent, " _
                & "  a.segend_komisi_telah_dibayar, " _
                & "  a.segend_komisi_bulanan, " _
                & "  b.segen_remarks, " _
                & "  c.ptnr_bank as bank, " _
                & "  c.ptnr_no_rek as norek, " _
                & "  c.ptnr_rek_name " _
                & "FROM " _
                & "  public.segend_det a " _
                & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
                & "  LEFT OUTER JOIN public.ptnr_mstr c ON (a.segend_ptnr_id = c.ptnr_id) " _
                & "WHERE " _
                & "  b.segen_periode = '" + le_periode.EditValue + "' AND  " _
                & "  (segend_komisi_bulanan > 0 or  segend_total_income > 0) " _
                  & " and segend_en_id in (" & _en_id_all & ") " _
                & "ORDER BY " _
                & "  a.segend_nama"



            Dim rpt As New XRSEReport
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If


                .TreeList1.DataSource = ds.Tables(0)
                .XrLabelPeriode.Text = "PERIODE : " & Format(TxtStartDate.EditValue, "dd/MM/yyyy") & " - " & Format(TxtEndDate.EditValue, "dd/MM/yyyy")

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Personal Selling Report"
                'ps.Document.AutoFitToPagesWidth = 0
                'ps.Document.ScaleFactor = 0.8F
                '.PrintingSystem = ps
                printTool.ShowPreview()
                .TreeList1.ExpandAll()

            End With
        ElseIf CBReportType.Text.ToUpper = "PENGAJUAN FEE RESELLER" Then

            _sql = "SELECT  " _
                & "  a.segend_oid, " _
                & "  a.segend_segen_oid,ptnr_code, " _
                & "  a.segend_ptnr_id, " _
                & "  a.segend_level, " _
                & "  coalesce(c.ptnr_rek_name,a.segend_nama) as segend_nama, " _
                & "  a.segend_active_sales,segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents, " _
                & "  a.segend_su_sales, " _
                & "  a.segend_pengali_komisi, " _
                & "  a.segend_komisi, " _
                & "  a.segend_su_group, " _
                & "  a.segend_percent_add_income, " _
                & "  a.segend_pengali_add_income, " _
                & "  a.segend_add_income, " _
                & "  a.segend_percent_coaching_income, " _
                & "  a.segend_pengali_coaching_income, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_entity, " _
                & "  a.segend_lvl_id, " _
                & "  a.segend_parent, " _
                & "  a.segend_komisi_telah_dibayar, " _
                & "  a.segend_komisi_bulanan, " _
                & "  b.segen_remarks, " _
                & "  c.ptnr_bank as bank, " _
                & "  c.ptnr_no_rek as norek, " _
                & "  c.ptnr_rek_name " _
                & "FROM " _
                & "  public.segend_det a " _
                & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
                & "  LEFT OUTER JOIN public.ptnr_mstr c ON (a.segend_ptnr_id = c.ptnr_id) " _
                & "WHERE " _
                & "  b.segen_periode = '" + le_periode.EditValue + "' AND  " _
                & "  segend_total_income > 0  " _
                  & " and segend_en_id in (" & _en_id_all & ") " _
                & "ORDER BY " _
                & "  a.segend_nama"


            Dim rpt As New XRSEBankReport
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrLabelPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " - " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                .LblTglBayar.Text = "DIBAYARKAN TANGGAL " & Format(ds.Tables(0).Rows(0).Item("segend_payment_date"), "d")
                '.XrPivotGrid1.DataSource = ds
                '.XrPivotGrid1.DataMember = "Table"

                .DataSource = ds
                .DataMember = "Table1"


                ' .XrLabelPeriode.Text = "PERIODE : " & Format(TxtStartDate.EditValue, "dd/MM/yyyy") & " - " & Format(TxtEndDate.EditValue, "dd/MM/yyyy")

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Personal Selling Fee Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()
                '.TreeList1.ExpandAll()

            End With

        ElseIf CBReportType.Text.ToUpper = "SLIP RESELLER" Then
            _sql = "SELECT  " _
                & "  a.segend_oid, " _
                & "  a.segend_segen_oid, " _
                & "  a.segend_ptnr_id, " _
                & "  a.segend_level, " _
                & "  a.segend_nama, " _
                & "  a.segend_active_sales, " _
                & "  a.segend_su_sales, " _
                & "  a.segend_pengali_komisi, " _
                & "  a.segend_komisi, " _
                & "  a.segend_su_group,segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents, " _
                & "  a.segend_percent_add_income, " _
                & "  a.segend_pengali_add_income, " _
                & "  a.segend_add_income, " _
                & "  a.segend_percent_coaching_income, " _
                & "  a.segend_pengali_coaching_income, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_entity, " _
                & "  a.segend_lvl_id, " _
                & "  a.segend_parent, " _
                & "  a.segend_komisi_telah_dibayar, " _
                & "  a.segend_komisi_bulanan, " _
                & "  b.segen_remarks, " _
                & "  c.ptnr_bank as bank, " _
                & "  c.ptnr_no_rek as norek, " _
                & "  coalesce(c.ptnr_rek_name,segend_nama) as ptnr_rek_name,segen_periode " _
                & "FROM " _
                & "  public.segend_det a " _
                & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
                & "  LEFT OUTER JOIN public.ptnr_mstr c ON (a.segend_ptnr_id = c.ptnr_id) " _
                & "WHERE " _
                & "  b.segen_periode = '" + le_periode.EditValue + "' AND   " _
                & "  segend_total_income > 0  " _
                & " and segend_en_id in (" & _en_id_all & ") " _
                & " ORDER BY " _
                & "  a.segend_nama"

            Dim rpt As New XRSESlip
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub

                .XrLabelPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " - " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                '.XrPivotGrid1.DataSource = ds
                '.XrPivotGrid1.DataMember = "Table"
                .DataSource = ds
                .DataMember = "Table1"

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "SLIP"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With

        End If

    End Sub


    Private Sub le_periode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_periode.EditValueChanged
        Try
            TxtStartDate.DateTime = le_periode.GetColumnValue("seperiode_start_date")
            TxtEndDate.DateTime = le_periode.GetColumnValue("seperiode_end_date")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
