Imports npgsql
Imports master_new.ModFunction
Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class FChartSalesYearVSOmsetByEntity
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _now = func_coll.get_now
        pr_txtyear.DateTime = _now
        chart_type.EditValue = "Bar Type"

        init_le(pr_entity, "en_mstr")
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        Dim ssql As String
        Try
            'Dim _en_id_all As String
            '_en_id_all = get_en_id_child(1)

            Dim _en_id_all As String

            If CBChild.EditValue = True Then
                _en_id_all = get_en_id_child(pr_entity.EditValue)
            Else
                _en_id_all = pr_entity.EditValue

            End If

            ssql = ""
            Dim _kode As String = ""
            Dim _filter As String = ""
            For i As Integer = 1 To 12
                _kode = pr_txtyear.Text & Format(i, "00")
                _filter = Format(i, "00")
                ssql = ssql + " select  ngroup, en_desc, sum(sod_price_ori_aft_disc_aft_tax_ext/1000000) as nvalue " _
                    & " from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, concat(en_desc,' - Actual') as en_desc,      case  when sod_tax_inc ~~* 'N' " _
                    & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
                    & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                    & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                    & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
                    & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
                    & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
                    & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
                    & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
                    & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                    & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                    & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
                    & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
                    & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
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
                    & " to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                    & "(" & _en_id_all & ")) as temp  group by ngroup, en_desc union " _
                    & "select   " _
                     & "ngroup , en_desc,  " _
                     & "sum(sod_price_ori_aft_disc_aft_tax_ext/1000000) as nvalue  " _
                     & "from (SELECT  " _
                     & "  to_char(a.omz_date,'yyyyMM') as ngroup, " _
                     & "  a.omz_en_id as soshipd_qty, " _
                     & "  concat(en_desc, ' - Target') as en_desc,  " _
                     & "  a.omz_target as sod_price_ori_aft_disc_aft_tax, " _
                     & "  a.omz_target as sod_price_ori_aft_disc_aft_tax_ext " _
                     & "FROM " _
                     & "  public.omz_mstr a " _
                     & "  INNER JOIN public.en_mstr b ON (a.omz_en_id = b.en_id) " _
                     & "WHERE " _
                     & "  a.omz_en_id IN (" & _en_id_all & ") " _
                     & "  AND to_char(a.omz_date,'yyyyMM')='" & _kode & "' ) as temp  group by ngroup, en_desc union "
            Next

            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 6)

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)

            ChartControl1.SeriesDataMember = "en_desc"
            ChartControl1.SeriesTemplate.ArgumentDataMember = "ngroup"
            ChartControl1.SeriesTemplate.ValueScaleType = ScaleType.Numerical
            ChartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"nvalue"})
            ChartControl1.SeriesTemplate.SeriesPointsSorting = SortingMode.Ascending
            ChartControl1.DataSource = dt

            For Each Series As Series In ChartControl1.Series
                If chart_type.EditValue.ToString.ToLower = "bar type" Then
                    Series.ChangeView(ViewType.Bar)
                Else
                    Series.ChangeView(ViewType.Line)
                End If
            Next
            pgc_master.DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub load_data_many(ByVal par As Boolean)
        Dim ssql As String
        Try
            Dim _en_id_all As String


            _en_id_all = get_en_id_child(1)


            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)
            ChartControl1.Series(0).DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub checkEditShowLabels_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkEditShowLabels.CheckedChanged
        For Each series As Series In ChartControl1.Series
            series.Label.Visible = Me.checkEditShowLabels.Checked
        Next series

    End Sub

    Public Overrides Function export_data() As Boolean
        Try
            Dim _file As String
            _file = AskSaveAsFile("Excel Files | *.xls")
            If _file.Length > 0 Then
                ChartControl1.ExportToXls(_file)
            End If
            Box("Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Function

    Private Sub chart_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chart_type.EditValueChanged
        Try

            For Each Series As Series In ChartControl1.Series
                If chart_type.EditValue.ToString.ToLower = "bar type" Then
                    Series.ChangeView(ViewType.Bar)
                Else
                    Series.ChangeView(ViewType.Line)
                End If

            Next
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
