Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraCharts

Public Class XRSalesAllEntityByEntitySub
    Public par_first_date As Date
    Public par_last_date As Date
    Public par_entity As String
    Public par_with_child As Boolean


    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim ssql As String
        Try

            ssql = ""
            Dim _kode As String = ""
            Dim _filter As String = ""
            For i As Integer = 1 To 12
                _kode = par_last_date.Date.Year & Format(i, "00")
                _filter = Format(i, "00")

                'ssql = ssql + " select ptnr_name,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc, sales_mstr.ptnr_name,     case  when sod_tax_inc ~~* 'N' " _
                '   & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
                '   & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                '   & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                '   & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
                '   & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
                '   & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
                '   & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
                '   & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
                '   & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                '   & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                '   & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
                '   & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
                '   & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
                '   & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                '   & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                '   & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                '   & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                '   & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                '   & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                '   & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                '   & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                '   & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                '   & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                '   & "  soship_date between " & SetDateNTime00(par_first_date) & " and " & SetDateNTime00(par_last_date) & "   and to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                '   & "(" & GetCurrentColumnValue("en_id") & ")) as temp  group by ptnr_name, ngroup union "

                'ssql = ssql + " select ptnr_name,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc, sales_mstr.ptnr_name,  " _
                ' & " case upper(sod_tax_inc) " _
                '& "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                '& "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                '& "  end as sod_price_ori_aft_disc_aft_tax_ext " _
                '  & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                '  & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                '  & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                '  & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                '  & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                '  & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                '  & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                '  & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                '  & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                '  & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                '  & "  soship_date between " & SetDateNTime00(par_first_date) & " and " & SetDateNTime00(par_last_date) & "   and to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                '  & "(" & GetCurrentColumnValue("en_id") & ")) as temp  group by ptnr_name, ngroup union "

                ssql = ssql + " select ptnr_name,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc, sales_mstr.ptnr_name,  " _
                    & " case upper(sod_tax_inc) " _
                    & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                    & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                    & "  end as sod_price_ori_aft_disc_aft_tax_ext " _
                    & "FROM  " _
                    & "  public.soship_mstr " _
                    & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                    & "  inner join so_mstr on so_oid = soship_so_oid " _
                    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                    & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                    & "  inner join en_mstr on en_id = soship_en_id " _
                    & "  inner join si_mstr on si_id = soship_si_id " _
                    & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                    & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                    & "  inner join cu_mstr on cu_id = so_cu_id " _
                    & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                    & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                    & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                    & "  left outer join ar_mstr on ar_oid = ars_ar_oid " _
                    & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                    & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                    & "  inner join pl_mstr on pl_id = pt_pl_id " _
                    & "  where soship_date between " & SetDateNTime00(par_first_date) & " and " & SetDateNTime00(par_last_date) & "   and to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                    & "(" & GetCurrentColumnValue("en_id") & ")) as temp  group by ptnr_name, ngroup union "

            Next

            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 6)

            Dim dt_new As New DataTable
            dt_new = master_new.PGSqlConn.GetTableData(ssql)


            XrChart1.DataSource = dt_new
            XrChart1.SeriesDataMember = "ptnr_name"
            XrChart1.SeriesTemplate.ArgumentDataMember = "ngroup"
            XrChart1.SeriesTemplate.ValueScaleType = ScaleType.Numerical
            XrChart1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"nvalue"})
            XrChart1.SeriesTemplate.SeriesPointsSorting = SortingMode.Ascending


            XrPivotGrid1.DataSource = dt_new

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub ReportHeader_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles ReportHeader.BeforePrint
        Try
            Dim ssql As String

            Dim _en_id_all As String

            If par_with_child = True Then
                _en_id_all = get_en_id_child(par_entity)
            Else
                _en_id_all = par_entity
            End If

            sSQL = ""
            Dim _kode As String = ""
            Dim _filter As String = ""
            For i As Integer = 1 To 12
                _kode = par_last_date.Date.Year & Format(i, "00")
                _filter = Format(i, "00")

                'ssql = ssql + " select en_desc,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc,      case  when sod_tax_inc ~~* 'N' " _
                '   & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
                '   & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                '   & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                '   & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
                '   & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
                '   & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
                '   & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
                '   & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
                '   & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                '   & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                '   & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
                '   & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                '   & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
                '   & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
                '   & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                '   & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                '   & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                '   & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                '   & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                '   & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                '   & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                '   & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                '   & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                '   & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                '   & "  soship_date between " & SetDateNTime00(par_first_date) & " and " & SetDateNTime00(par_last_date) & "  and to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                '   & "(" & _en_id_all & ")) as temp  group by en_desc, ngroup union "

                'ssql = ssql + " select en_desc,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc,  " _
                '   & "  case upper(sod_tax_inc) " _
                '& "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                '& "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                '& "  end as sod_price_ori_aft_disc_aft_tax_ext " _
                '   & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                '   & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                '   & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                '   & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                '   & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                '   & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                '   & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                '   & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                '   & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                '   & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                '   & "  soship_date between " & SetDateNTime00(par_first_date) & " and " & SetDateNTime00(par_last_date) & "  and to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                '   & "(" & _en_id_all & ")) as temp  group by en_desc, ngroup union "

                ssql = ssql + " select en_desc,  ngroup , round(sum(sod_price_ori_aft_disc_aft_tax_ext/1000000),1) as nvalue from (SELECT  cast('" & _kode & "' as varchar)  as ngroup , soshipd_qty,soship_date, en_desc,  " _
                    & "  case upper(sod_tax_inc) " _
                    & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                    & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                    & "  end as sod_price_ori_aft_disc_aft_tax_ext " _
                    & "FROM  " _
                    & "  public.soship_mstr " _
                    & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                    & "  inner join so_mstr on so_oid = soship_so_oid " _
                    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                    & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                    & "  inner join en_mstr on en_id = soship_en_id " _
                    & "  inner join si_mstr on si_id = soship_si_id " _
                    & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                    & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                    & "  inner join cu_mstr on cu_id = so_cu_id " _
                    & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                    & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                    & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                    & "  left outer join ar_mstr on ar_oid = ars_ar_oid " _
                    & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                    & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                    & "  inner join pl_mstr on pl_id = pt_pl_id " _
                    & "  where soship_date between " & SetDateNTime00(par_first_date) & " and " & SetDateNTime00(par_last_date) & "  and to_char(soship_date,'yyyyMM')='" & _kode & "' and so_en_id in " _
                    & "(" & _en_id_all & ")) as temp  group by en_desc, ngroup union "




            Next

            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 6)

            Try

                Dim xrsub As XRSalesAllEntitySub = TryCast(XrSubreport1.ReportSource, XRSalesAllEntitySub)
                Dim ds As New DataSet
                ds = ReportDataset(ssql)

                With xrsub
                    .XrPeriode.Text = "Periode : " & Format(par_first_date, "dd/MM/yyyy") & " to " & Format(par_last_date, "dd/MM/yyyy")
                    .XrChart1.SeriesDataMember = "en_desc"
                    .XrChart1.SeriesTemplate.ArgumentDataMember = "ngroup"
                    .XrChart1.SeriesTemplate.ValueScaleType = ScaleType.Numerical
                    .XrChart1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"nvalue"})
                    .XrChart1.SeriesTemplate.SeriesPointsSorting = SortingMode.Ascending
                    .XrChart1.DataSource = ds.Tables(0)

                    .XrPivotGrid1.DataSource = ds.Tables(0)

                End With

            Catch ex As Exception
                Pesan(Err)
            End Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class