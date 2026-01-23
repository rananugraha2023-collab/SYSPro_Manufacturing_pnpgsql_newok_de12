Public Class FSalesByMonthPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        init_le(le_entity, "en_mstr")
        init_le(le_report_type, "sales_report_type")

        TxtStartDate.DateTime = Now
        TxtEndDate.DateTime = Now
    End Sub


    Public Overrides Sub preview()


        Dim _en_id_all As String

        If CeChild.EditValue = True Then
            _en_id_all = get_en_id_child(le_entity.EditValue)
        Else

            _en_id_all = le_entity.EditValue

        End If

        Dim _sql As String

        If le_report_type.EditValue = "1" Then

            '_sql = "SELECT   soshipd_qty,soship_date, en_desc,      case  when sod_tax_inc ~~* 'N' " _
            '        & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
            '        & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
            '        & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
            '        & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
            '        & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
            '        & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
            '        & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
            '        & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
            '        & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
            '        & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
            '        & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
            '        & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
            '        & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
            '        & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
            '        & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
            '        & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
            '        & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
            '        & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
            '        & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
            '        & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
            '        & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
            '        & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
            '        & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
            '        & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
            '        & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
            '        & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
            '        & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
            '        & "soship_date between " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " and " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " and so_en_id in " _
            '        & "(" & _en_id_all & ") "

            _sql = "SELECT   soshipd_qty,soship_date, en_desc,   case upper(sod_tax_inc) " _
                    & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                    & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                    & "  end as sod_price_ori_aft_disc_aft_tax_ext         " _
                    & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                    & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                    & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                    & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                    & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                    & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                    & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                    & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                    & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                    & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                    & "soship_date between " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " and " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " and so_en_id in " _
                    & "(" & _en_id_all & ") "



            Dim rpt As New XRSalesByMonth
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrPivotGrid1.DataSource = ds
                .XrPivotGrid1.DataMember = "Table"
                .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
               
                .DataSource = Nothing
                '.DataMember = "Table1"

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Sales Order Shipment Summary Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
        ElseIf le_report_type.EditValue = "2" Then
            
            _sql = "SELECT  " _
                    & "  b.en_desc, " _
                    & "  a.ptnr_add_date, " _
                    & "  1 AS ptnr_value " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.ptnr_en_id = b.en_id) " _
                    & "WHERE " _
                    & "  a.ptnr_add_date BETWEEN " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " and " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " AND  " _
                    & "  a.ptnr_en_id IN (" & _en_id_all & ") and ptnr_is_cust='Y'"

            Dim rpt As New XRCustomerByMonth
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrPivotGrid1.DataSource = ds
                .XrPivotGrid1.DataMember = "Table"
                .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                .DataSource = Nothing
                '.DataSource = ds
                '.DataMember = "Table1"

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Customer Growth Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
        ElseIf le_report_type.EditValue = "3" Then
            _sql = "SELECT coalesce(pt_desc1,'') || ' ' || coalesce(pt_desc2,'') as pt_desc ,   soshipd_qty * -1 as soshipd_qty,soship_date, en_desc         " _
                   & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                   & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                   & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                   & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                   & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                   & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                   & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                   & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                   & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                   & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                   & "soship_date between " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " and " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " and so_en_id in " _
                   & "(" & _en_id_all & ") "

            Dim rpt As New XRProductByMonth
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrPivotGrid1.DataSource = ds
                .XrPivotGrid1.DataMember = "Table"
                .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                .DataSource = Nothing
                '.DataSource = ds
                '.DataMember = "Table1"

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Product Summary Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
        ElseIf le_report_type.EditValue = "4" Then
            _sql = "SELECT x.code_name as payment_type ,   soshipd_qty * -1 as soshipd_qty,soship_date, en_desc         " _
                   & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                   & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                   & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                   & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                   & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                   & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                   & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                   & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                   & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                   & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                   & "soship_date between " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " and " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " and so_en_id in " _
                   & "(" & _en_id_all & ") "

            Dim rpt As New XRPaymentTypeByMonth
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrPivotGrid1.DataSource = ds
                .XrPivotGrid1.DataMember = "Table"
                .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                .DataSource = Nothing
                '.DataSource = ds
                '.DataMember = "Table1"

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Payment Type Summary Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
        ElseIf le_report_type.EditValue = "5" Then
            _sql = "SELECT coalesce(pt_desc1,'') || ' ' || coalesce(pt_desc2,'') as pt_desc ,   soshipd_qty * -1 as soshipd_qty,soship_date, en_desc         " _
                   & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                   & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                   & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                   & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                   & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                   & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                   & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                   & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                   & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                   & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                   & "soship_date between " & master_new.ModFunction.SetDate(TxtStartDate.DateTime) & " and " & master_new.ModFunction.SetDate(TxtEndDate.DateTime) & " and so_en_id in " _
                   & "(" & _en_id_all & ") "

            Dim rpt As New XRProductByMonthByEntity
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong")
                    Exit Sub
                End If

                ' Exit Sub
                .XrPivotGrid1.DataSource = ds
                .XrPivotGrid1.DataMember = "Table"
                .XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
                .DataSource = Nothing
                '.DataSource = ds
                '.DataMember = "Table1"

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Product Summary Report By Entity"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
        End If

    End Sub

   
End Class
