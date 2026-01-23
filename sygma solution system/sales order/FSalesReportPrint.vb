Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraCharts

Public Class FSalesReportPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        init_le(le_entity, "en_mstr")
        first_date.EditValue = Now
        last_date.EditValue = Now

    End Sub


    Public Overrides Sub preview()

        Dim _en_id_all As String

        If ce_with_child.EditValue = True Then
            _en_id_all = get_en_id_child(le_entity.EditValue)
        Else
            _en_id_all = le_entity.EditValue
        End If

        Dim sSQL As String

        'sSQL = ""
        'Dim _kode As String = ""
        'Dim _filter As String = ""
        'For i As Integer = 1 To 12
        '    _kode = last_date.DateTime.Year & Format(i, "00")
        '    _filter = Format(i, "00")

        '    sSQL = sSQL + " select en_desc,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc,      case  when sod_tax_inc ~~* 'N' " _
        '       & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
        '       & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
        '       & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
        '       & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
        '       & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
        '       & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
        '       & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
        '       & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
        '       & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
        '       & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
        '       & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
        '       & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
        '       & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
        '       & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
        '       & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
        '       & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
        '       & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
        '       & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
        '       & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
        '       & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
        '       & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
        '       & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
        '       & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
        '       & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
        '       & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
        '       & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
        '       & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
        '       & " to_char(soship_date,'yyyyMM')='" & _kode & "' and soship_date between " & SetDateNTime00(first_date.DateTime.Date) & " and " & SetDateNTime00(last_date.DateTime.Date) & " and so_en_id in " _
        '       & "(" & _en_id_all & ")) as temp  group by en_desc, ngroup union "
        'Next

        'sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 6)

        'Dim dt_chart As New DataTable
        'dt_chart = GetTableData(sSQL)

        'Dim rpt As New XRSalesAllEntitySub
        'With rpt



        '    .XrPeriode.Text = "Periode : " & Format(first_date.DateTime.Date, "dd/MM/yyyy") & " to " & Format(last_date.DateTime.Date, "dd/MM/yyyy")
        '    .XrChart1.SeriesDataMember = "en_desc"
        '    .XrChart1.SeriesTemplate.ArgumentDataMember = "ngroup"
        '    .XrChart1.SeriesTemplate.ValueScaleType = ScaleType.Numerical
        '    .XrChart1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"nvalue"})
        '    .XrChart1.SeriesTemplate.SeriesPointsSorting = SortingMode.Ascending
        '    .XrChart1.DataSource = dt_chart

        '    .XrPivotGrid1.DataSource = dt_chart

        '     Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
        '    printTool.PreviewForm.Text = "Report"
        '    '.PrintingSystem = ps
        '    printTool.ShowPreview()

        'End With


        sSQL = "select * from en_mstr where en_id in (" & _en_id_all & ")"

        Dim dt_chart As New DataTable
        dt_chart = GetTableData(sSQL)

        Dim rpt As New XRSalesAllEntityByEntitySub
        With rpt



            .XrPeriode.Text = "Periode : " & Format(first_date.DateTime.Date, "dd/MM/yyyy") & " to " & Format(last_date.DateTime.Date, "dd/MM/yyyy")

            .par_first_date = first_date.DateTime.Date
            .par_last_date = last_date.DateTime.Date
            .par_entity = le_entity.EditValue
            .par_with_child = ce_with_child.EditValue
            .DataSource = dt_chart

             Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            printTool.PreviewForm.Text = "Report"
            '.PrintingSystem = ps
            printTool.ShowPreview()

        End With


    End Sub
End Class
