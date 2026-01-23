Public Class FPSReportPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        init_le(le_entity, "en_mstr")
        'init_le(le_periode, "sales_report_type")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr())
        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
        le_periode.ItemIndex = 0

        'TxtStartDate.DateTime = Now
        'TxtEndDate.DateTime = Now

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
                & "  a.psgend_oid, " _
                & "  a.psgend_psgen_oid, " _
                & "  a.psgend_sales_amount, " _
                & "  a.psgend_ptnr_id as ptnr_id, " _
                & "  a.psgend_lvl_id, " _
                & "  a.psgend_seq, " _
                & "  a.psgend_ps_vol, " _
                & "  a.psgend_pt_vol, " _
                & "  a.psgend_network_bonus, " _
                & "  a.psgend_commision, " _
                & "  a.psgend_network_child, " _
                & "  a.psgend_child_level, " _
                & "  a.psgend_level_status, " _
                & "  a.psgend_lvl_id_old, " _
                & "  a.psgend_thp_total, " _
                & "  b.psgen_code, " _
                & "  d.en_desc, " _
                & "  b.psgen_date, " _
                & "  b.psgen_remarks, " _
                & "  b.psgen_all_child, " _
                & "  b.psgen_periode_code, " _
                & "  c.lvl_name,ptnr_name, " _
                & "  e.periode_start_date, " _
                & "  e.periode_end_date,ptnr_parent " _
                & "FROM " _
                & "  public.psgend_det a " _
                & "  INNER JOIN public.psgen_mstr b ON (a.psgend_psgen_oid = b.psgen_oid) " _
                & "  INNER JOIN public.pslvl_mstr c ON (a.psgend_lvl_id = c.lvl_id) " _
                & "  INNER JOIN public.en_mstr d ON (b.psgen_en_id = d.en_id) " _
                & "  INNER JOIN public.psperiode_mstr e ON (b.psgen_periode_code = e.periode_code) " _
                 & "  INNER JOIN public.ptnr_mstr f ON (a.psgend_ptnr_id = f.ptnr_id) " _
                & "WHERE " _
                & "  b.psgen_new_mode = 'Y' AND  " _
                & "  b.psgen_periode_code = '" + le_periode.EditValue + "' AND  " _
                & "  b.psgen_en_id in (" + _en_id_all + ")"


            Dim rpt As New XRPSReportNew
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                '.XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                '.XrPivotGrid1.DataSource = ds
                '.XrPivotGrid1.DataMember = "Table"
                '.DataSource = ds
                '.DataMember = "Table1"

                .TreeList1.DataSource = ds.Tables(0)
                .XrLabelPeriode.Text = "PERIODE : " & Format(TxtStartDate.EditValue, "dd/MM/yyyy") & " - " & Format(TxtEndDate.EditValue, "dd/MM/yyyy")

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                printTool.PreviewForm.Text = "Personal Selling Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()
                .TreeList1.ExpandAll()

            End With
        ElseIf CBReportType.Text.ToUpper = "PENGAJUAN FEE RESELLER" Then


            _sql = "SELECT  " _
              & "  a.psgend_oid, " _
              & "  a.psgend_psgen_oid, " _
              & "  a.psgend_sales_amount, " _
              & "  a.psgend_ptnr_id as ptnr_id, " _
              & "  a.psgend_lvl_id, " _
              & "  a.psgend_seq, " _
              & "  a.psgend_ps_vol, " _
              & "  a.psgend_pt_vol, " _
              & "  a.psgend_network_bonus, " _
              & "  a.psgend_commision, " _
              & "  a.psgend_network_child, " _
              & "  a.psgend_child_level, " _
              & "  a.psgend_level_status, " _
              & "  a.psgend_lvl_id_old, " _
              & "  a.psgend_thp_total, " _
              & "  b.psgen_code, " _
              & "  d.en_desc, " _
              & "  b.psgen_date, " _
              & "  b.psgen_remarks, " _
              & "  b.psgen_all_child, " _
              & "  b.psgen_periode_code, " _
              & "  c.lvl_name,ptnr_name, " _
              & "  e.periode_start_date, " _
              & "  e.periode_end_date,ptnr_parent " _
              & "FROM " _
              & "  public.psgend_det a " _
              & "  INNER JOIN public.psgen_mstr b ON (a.psgend_psgen_oid = b.psgen_oid) " _
              & "  INNER JOIN public.pslvl_mstr c ON (a.psgend_lvl_id = c.lvl_id) " _
              & "  INNER JOIN public.en_mstr d ON (b.psgen_en_id = d.en_id) " _
              & "  INNER JOIN public.psperiode_mstr e ON (b.psgen_periode_code = e.periode_code) " _
               & "  INNER JOIN public.ptnr_mstr f ON (a.psgend_ptnr_id = f.ptnr_id) " _
              & "WHERE " _
              & "  b.psgen_new_mode = 'Y' AND  " _
              & "  b.psgen_periode_code = '" + le_periode.EditValue + "' AND  " _
              & "  b.psgen_en_id in (" + _en_id_all + ") and psgend_thp_total <> 0"

            Dim rpt As New XRPSReportNew
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                '.XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                '.XrPivotGrid1.DataSource = ds
                '.XrPivotGrid1.DataMember = "Table"
                '.DataSource = ds
                '.DataMember = "Table1"

                .TreeList1.DataSource = ds.Tables(0)
                .XrLabelPeriode.Text = "PERIODE : " & Format(TxtStartDate.EditValue, "dd/MM/yyyy") & " - " & Format(TxtEndDate.EditValue, "dd/MM/yyyy")

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                printTool.PreviewForm.Text = "Personal Selling Fee Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()
                .TreeList1.ExpandAll()

            End With

        ElseIf CBReportType.Text.ToUpper = "SLIP RESELLER" Then
            _sql = ""

            Dim rpt As New XRProductByMonth
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                .XrPivotGrid1.DataSource = ds
                .XrPivotGrid1.DataMember = "Table"
                '.DataSource = ds
                '.DataMember = "Table1"

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                printTool.PreviewForm.Text = "Product Summary Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
      
        End If

    End Sub


    Private Sub le_periode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_periode.EditValueChanged
        Try
            TxtStartDate.DateTime = le_periode.GetColumnValue("periode_start_date")
            TxtEndDate.DateTime = le_periode.GetColumnValue("periode_end_date")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
