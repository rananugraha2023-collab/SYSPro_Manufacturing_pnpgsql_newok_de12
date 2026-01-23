Imports npgsql
Imports master_new.ModFunction

Public Class FSOARFPReportNoCost
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ds_detail As DataSet

    Private Sub FSOARFPReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        AddHandler gv_view1.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_view1.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Pay Type", "pay_type_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Sold To", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Category", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sales", "sales_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        'sales_name
        add_column_copy(gv_view1, "Shipment / Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Shipment / Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Is Shipment", "soship_is_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Reason Code", "reason_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Class", "tax_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Total Sales", "sales_ttl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Discount Value", "disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "FP Price", "price_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "FP Disc. Value", "disc_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Netto", "dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        'add_column_copy(gv_view1, "Total Cost", "total_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Gross Profit", "gross_profit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_view1, "Product Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Vat Paid", "ppn_bayar", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Vat Free", "ppn_bebas", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Sales Unit Total", "sod_sales_unit_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_view1, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then

                            '.SQL = "SELECT    " _
                            '   & "  en_desc, " _
                            '   & "  so_code, " _
                            '   & "  so_date, " _
                            '   & "  ptnr_mstr.ptnr_name, " _
                            '   & "  ptnr_mstr.ptnr_code, " _
                            '   & "  public.ptnr_mstr.ptnr_ptnrg_id, " _
                            '   & "  public.ptnr_mstr.ptnr_ptnrc_id, " _
                            '   & "  public.ptnrg_grp.ptnrg_code, " _
                            '   & "  public.ptnrg_grp.ptnrg_name, " _
                            '   & "  public.ptnrc_cat.ptnrc_code, " _
                            '   & "  public.ptnrc_cat.ptnrc_name, " _
                            '   & "  so_trans_rmks, " _
                            '   & "  soship_code, " _
                            '   & "  soship_date, " _
                            '   & "  si_desc, " _
                            '   & "  soship_is_shipment, " _
                            '   & "  soshipd_seq, " _
                            '   & "  cu_name, " _
                            '   & "  so_exc_rate, " _
                            '   & "  pt_code,sod_cost, " _
                            '   & "  pt_desc1, " _
                            '   & "  pt_desc2, " _
                            '   & "  sod_taxable, " _
                            '   & "  sod_tax_inc, " _
                            '   & " sod_sales_unit, " _
                            '   & " (sod_sales_unit * (-1) * soshipd_qty) as sod_sales_unit_total, " _
                            '   & "  tax_mstr.code_name as tax_name, " _
                            '   & "  soshipd_qty * -1 as soshipd_qty, " _
                            '   & "  sod_price, " _
                            '   & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
                            '   & "  sod_disc,  soshipd_qty * -1 * sod_cost  as total_cost, " _
                            '   & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then soshipd_qty * -1 * sod_price " _
                            '   & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                            '   & "  end as price_fp, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                            '   & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc " _
                            '   & "  end as disc_fp, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                            '   & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                            '   & "  end as dpp, " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                            '   & "  when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                            '   & "  end as gross_profit, " _
                            '   & "   " _
                            '   & "  pl_desc,  " _
                            '   & "   " _
                            '   & "  case pl_code " _
                            '   & "  when '990000000001' then " _
                            '   & "  					case upper(sod_tax_inc) " _
                            '   & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                            '   & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.1 " _
                            '   & "                    end " _
                            '   & "  end as ppn_bayar, " _
                            '   & "   " _
                            '   & "  case pl_code " _
                            '   & "  when '990000000002' then " _
                            '   & "  					case upper(sod_tax_inc) " _
                            '   & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                            '   & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                            '   & "                    end " _
                            '   & "  end as ppn_bebas, " _
                            '   & "               " _
                            '   & "  um_mstr.code_name as um_name, " _
                            '   & "  loc_desc, " _
                            '   & "  reason_mstr.code_name as reason_name, " _
                            '   & "  ar_code, " _
                            '   & "  ar_date, " _
                            '   & "  ti_code, " _
                            '   & "sales_mstr.ptnr_name as sales_name, " _
                            '   & "  ti_date,pay_type.code_name as pay_type_desc,pi_desc " _
                            '   & "FROM  " _
                            '   & "  public.soship_mstr " _
                            '   & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                            '   & "  inner join so_mstr on so_oid = soship_so_oid " _
                            '   & "  inner join pi_mstr on so_pi_id = pi_id " _
                            '   & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                            '   & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                            '   & "  inner join en_mstr on en_id = soship_en_id " _
                            '   & "  inner join si_mstr on si_id = soship_si_id " _
                            '   & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                            '   & "  INNER JOIN public.ptnrg_grp ON (public.ptnr_mstr.ptnr_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                            '   & "  INNER JOIN public.ptnrc_cat ON (public.ptnr_mstr.ptnr_ptnrc_id = public.ptnrc_cat.ptnrc_id)" _
                            '   & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            '   & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                            '   & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                            '   & "  inner join cu_mstr on cu_id = so_cu_id " _
                            '   & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                            '   & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            '   & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                            '   & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                            '   & "  left outer join ar_mstr on ar_oid = ars_ar_oid " _
                            '   & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                            '   & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                            '   & "  inner join pl_mstr on pl_id = pt_pl_id " _
                            '   & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            '   & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            '   & "  and pay_type.code_usr_1 <> '0' and left(en_desc,3) <> 'CMD' " _
                            '   & "  and so_en_id in (select user_en_id from tconfuserentity " _
                            '   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            '   & " union all " _
                            '   & " SELECT    " _
                            '   & "  en_desc, " _
                            '   & "  so_code, " _
                            '   & "  so_date, " _
                            '   & "  ptnr_mstr.ptnr_name, " _
                            '   & "  ptnr_mstr.ptnr_code, " _
                            '   & "  public.ptnr_mstr.ptnr_ptnrg_id, " _
                            '   & "  public.ptnr_mstr.ptnr_ptnrc_id, " _
                            '   & "  public.ptnrg_grp.ptnrg_code, " _
                            '   & "  public.ptnrg_grp.ptnrg_name, " _
                            '   & "  public.ptnrc_cat.ptnrc_code, " _
                            '   & "  public.ptnrc_cat.ptnrc_name, " _
                            '   & "  so_trans_rmks, " _
                            '   & "  soship_code, " _
                            '   & "  soship_date, " _
                            '   & "  si_desc, " _
                            '   & "  soship_is_shipment, " _
                            '   & "  soshipd_seq, " _
                            '   & "  cu_name, " _
                            '   & "  so_exc_rate, " _
                            '   & "  pt_code,sod_cost, " _
                            '   & "  pt_desc1, " _
                            '   & "  pt_desc2, " _
                            '   & "  sod_taxable, " _
                            '   & "  sod_tax_inc, " _
                            '   & " sod_sales_unit, " _
                            '   & " (sod_sales_unit * (-1) * soshipd_qty) as sod_sales_unit_total,  " _
                            '   & "  tax_mstr.code_name as tax_name, " _
                            '   & "  soshipd_qty * -1 as soshipd_qty, " _
                            '   & "  sod_price, " _
                            '   & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
                            '   & "  sod_disc,  soshipd_qty * -1 * sod_cost  as total_cost, " _
                            '   & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then soshipd_qty * -1 * sod_price " _
                            '   & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                            '   & "  end as price_fp, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                            '   & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc " _
                            '   & "  end as disc_fp, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                            '   & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                            '   & "  end as dpp, " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                            '   & "  when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                            '   & "  end as gross_profit, " _
                            '   & "   " _
                            '   & "  pl_desc,  " _
                            '   & "   " _
                            '   & "  case pl_code " _
                            '   & "  when '990000000001' then " _
                            '   & "  					case upper(sod_tax_inc) " _
                            '   & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                            '   & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.1 " _
                            '   & "                    end " _
                            '   & "  end as ppn_bayar, " _
                            '   & "   " _
                            '   & "  case pl_code " _
                            '   & "  when '990000000002' then " _
                            '   & "  					case upper(sod_tax_inc) " _
                            '   & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                            '   & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                            '   & "                    end " _
                            '   & "  end as ppn_bebas, " _
                            '   & "               " _
                            '   & "  um_mstr.code_name as um_name, " _
                            '   & "  loc_desc, " _
                            '   & "  reason_mstr.code_name as reason_name, " _
                            '   & "  null as ar_code, " _
                            '   & "  null as ar_date, " _
                            '   & "  ti_code, " _
                            '   & "sales_mstr.ptnr_name as sales_name, " _
                            '   & "  ti_date ,pay_type.code_name as pay_type_desc,pi_desc " _
                            '   & "FROM  " _
                            '   & "  public.soship_mstr " _
                            '   & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                            '   & "  inner join so_mstr on so_oid = soship_so_oid " _
                            '   & "  inner join pi_mstr on so_pi_id = pi_id " _
                            '   & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                            '   & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                            '   & "  inner join en_mstr on en_id = soship_en_id " _
                            '   & "  inner join si_mstr on si_id = soship_si_id " _
                            '   & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                            '   & "  INNER JOIN public.ptnrg_grp ON (public.ptnr_mstr.ptnr_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                            '   & "  INNER JOIN public.ptnrc_cat ON (public.ptnr_mstr.ptnr_ptnrc_id = public.ptnrc_cat.ptnrc_id)" _
                            '   & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            '   & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                            '   & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                            '   & "  inner join cu_mstr on cu_id = so_cu_id " _
                            '   & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                            '   & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            '   & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                            '   & "  left outer join tis_soship on tis_soship_oid = soship_oid " _
                            '   & "  left outer join ti_mstr on ti_oid = tis_ti_oid " _
                            '   & "  inner join pl_mstr on pl_id = pt_pl_id " _
                            '   & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            '   & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            '   & "  and pay_type.code_usr_1 = '0' " _
                            '   & "  and so_en_id in (select user_en_id from tconfuserentity " _
                            '   & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                            '   & " union all " _
                            '   & " SELECT    " _
                            '   & "  en_desc, " _
                            '   & "  so_code, " _
                            '   & "  so_date, " _
                            '   & "  ptnr_mstr.ptnr_name, " _
                            '   & "  ptnr_mstr.ptnr_code, " _
                            '   & "  public.ptnr_mstr.ptnr_ptnrg_id, " _
                            '   & "  public.ptnr_mstr.ptnr_ptnrc_id, " _
                            '   & "  public.ptnrg_grp.ptnrg_code, " _
                            '   & "  public.ptnrg_grp.ptnrg_name, " _
                            '   & "  public.ptnrc_cat.ptnrc_code, " _
                            '   & "  public.ptnrc_cat.ptnrc_name, " _
                            '   & "  so_trans_rmks, " _
                            '   & "  soship_code, " _
                            '   & "  soship_date, " _
                            '   & "  si_desc, " _
                            '   & "  soship_is_shipment, " _
                            '   & "  soshipd_seq, " _
                            '   & "  cu_name, " _
                            '   & "  so_exc_rate, " _
                            '   & "  pt_code,sod_cost, " _
                            '   & "  pt_desc1, " _
                            '   & "  pt_desc2, " _
                            '   & "  sod_taxable, " _
                            '   & "  sod_tax_inc, " _
                            '   & " sod_sales_unit, " _
                            '   & " (sod_sales_unit * (-1) * soshipd_qty) as sod_sales_unit_total, " _
                            '   & "  tax_mstr.code_name as tax_name, " _
                            '   & "  soshipd_qty * -1 as soshipd_qty, " _
                            '   & "  sod_price, " _
                            '   & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
                            '   & "  sod_disc, soshipd_qty * -1 * sod_cost  as total_cost, " _
                            '   & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then soshipd_qty * -1 * sod_price " _
                            '   & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                            '   & "  end as price_fp, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                            '   & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc " _
                            '   & "  end as disc_fp, " _
                            '   & "   " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                            '   & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                            '   & "  end as dpp, " _
                            '   & "  case upper(sod_tax_inc) " _
                            '   & "  when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                            '   & "  when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                            '   & "  end as gross_profit, " _
                            '   & "   " _
                            '   & "  pl_desc,  " _
                            '   & "   " _
                            '   & "  case pl_code " _
                            '   & "  when '990000000001' then " _
                            '   & "  					case upper(sod_tax_inc) " _
                            '   & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                            '   & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.1 " _
                            '   & "                    end " _
                            '   & "  end as ppn_bayar, " _
                            '   & "   " _
                            '   & "  case pl_code " _
                            '   & "  when '990000000002' then " _
                            '   & "  					case upper(sod_tax_inc) " _
                            '   & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                            '   & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                            '   & "                    end " _
                            '   & "  end as ppn_bebas, " _
                            '   & "               " _
                            '   & "  um_mstr.code_name as um_name, " _
                            '   & "  loc_desc, " _
                            '   & "  reason_mstr.code_name as reason_name, " _
                            '   & "  null as ar_code, " _
                            '   & "  null as ar_date, " _
                            '   & "  ti_code, " _
                            '   & "sales_mstr.ptnr_name as sales_name, " _
                            '   & "  ti_date,pay_type.code_name as pay_type_desc,pi_desc " _
                            '   & "FROM  " _
                            '   & "  public.soship_mstr " _
                            '   & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                            '   & "  inner join so_mstr on so_oid = soship_so_oid " _
                            '   & "  inner join pi_mstr on so_pi_id = pi_id " _
                            '   & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                            '   & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                            '   & "  inner join en_mstr on en_id = soship_en_id " _
                            '   & "  inner join si_mstr on si_id = soship_si_id " _
                            '   & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                            '   & "  INNER JOIN public.ptnrg_grp ON (public.ptnr_mstr.ptnr_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                            '   & "  INNER JOIN public.ptnrc_cat ON (public.ptnr_mstr.ptnr_ptnrc_id = public.ptnrc_cat.ptnrc_id)" _
                            '   & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            '   & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                            '   & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                            '   & "  inner join cu_mstr on cu_id = so_cu_id " _
                            '   & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                            '   & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            '   & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                            '   & "  left outer join tis_soship on tis_soship_oid = soship_oid " _
                            '   & "  left outer join ti_mstr on ti_oid = tis_ti_oid " _
                            '   & "  inner join pl_mstr on pl_id = pt_pl_id " _
                            '   & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            '   & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            '   & "  and pay_type.code_usr_1 <> '0' and  left(en_desc,3) = 'CMD' " _
                            '   & "  and so_en_id in (select user_en_id from tconfuserentity " _
                            '   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                            .SQL = "SELECT DISTINCT* " _
                                & "FROM ( " _
                                & "        SELECT si_desc, " _
                                & "         en_desc, " _
                                & "         so_date, " _
                                & "         so_code, " _
                                & "         soship_date, " _
                                & "         soship_code, " _
                                & "         ar_date, " _
                                & "         ar_code, " _
                                & "         ptnr_mstr.ptnr_code, " _
                                & "         ptnr_mstr.ptnr_name, " _
                                & "         sales_mstr.ptnr_code AS sales_code, " _
                                & "         sales_mstr.ptnr_name AS sales_name, " _
                                & "         pi_desc, " _
                                & "         pt_code, " _
                                & "         pt_desc1, " _
                                & "         pt_desc2, " _
                                & "         um_mstr.code_name AS um_name, " _
                                & "         loc_desc, " _
                                & "         soshipd_qty * - 1 AS soshipd_qty, " _
                                & "         sod_cost, " _
                                & "         CASE " _
                                & "           WHEN sod_price <> 0 THEN (sod_cost / sod_price) " _
                                & "           ELSE 0 " _
                                & "         END AS pct_cost, " _
                                & "         soshipd_qty * - 1 * sod_price AS sales_ttl, " _
                                & "         soshipd_qty * - 1 * sod_price / NULLIF(SUM(soshipd_qty * - 1 * sod_price) OVER(), " _
                                & "           0) AS grss_sls_contr, " _
                                & "         sod_price, " _
                                & "         sod_disc, " _
                                & "         soshipd_qty * - 1 * sod_price * sod_disc AS disc_value, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price " _
                                & "           WHEN sod_tax_inc = 'Y' THEN soshipd_qty * - 1 * sod_price * 100.0 / 110.0 " _
                                & "         END AS price_fp, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "           WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) * sod_disc " _
                                & "         END AS disc_fp, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "           WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "         END AS dpp, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "           WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "         END / NULLIF(SUM(CASE " _
                                & "                            WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "                            WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / " _
                                & "                              110.0 * sod_disc) " _
                                & "                          END) OVER(), " _
                                & "           0) AS nett_sls_contr, " _
                                & "         SUM(CASE " _
                                & "               WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "               WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "             END) as JML, " _
                                & "         soshipd_qty * - 1 * sod_cost AS total_cost, " _
                                & "         CASE " _
                                & "           WHEN sod_price <> 0 THEN soshipd_qty * - 1 * sod_cost / NULLIF(CASE " _
                                & "                                                                            WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * " _
                                & "                                                                              - 1 * sod_price * sod_disc " _
                                & "                                                                            WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0 " _
                                & "                                                                              ) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "                                                                          END, " _
                                & "           0) " _
                                & "           ELSE 0 " _
                                & "         END AS ttl_cost_nett, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN (soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc) - soshipd_qty * - 1 * sod_cost " _
                                & "           WHEN sod_tax_inc = 'Y' THEN ((soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc)) - " _
                                & "             soshipd_qty * - 1 * sod_cost " _
                                & "         END AS gross_profit, " _
                                & "         CASE " _
                                & "           WHEN sod_price <> 0 THEN (CASE UPPER(sod_tax_inc) " _
                                & "                                       WHEN 'N' THEN (soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc) - soshipd_qty * - 1 * " _
                                & "                                         sod_cost " _
                                & "                                       WHEN 'Y' THEN ((soshipd_qty * - 1 * sod_price * 100.0 / 110.0) - soshipd_qty * - 1 * sod_price * 100.0 / 110.0 " _
                                & "                                         * sod_disc) - soshipd_qty * - 1 * sod_cost " _
                                & "                                     END) / NULLIF(soshipd_qty * - 1 * sod_price, " _
                                & "           0) " _
                                & "           ELSE 0 " _
                                & "         END AS netto_gross_profit, " _
                                & "         sod_sales_unit, " _
                                & "         sod_sales_unit * - 1 * soshipd_qty AS sod_sales_unit_total, " _
                                & "         pl_desc, " _
                                & "         soship_is_shipment, " _
                                & "         soshipd_seq, " _
                                & "         cu_name, " _
                                & "         so_exc_rate, " _
                                & "         sod_taxable, " _
                                & "         sod_tax_inc, " _
                                & "         tax_mstr.code_name AS tax_name, " _
                                & "         CASE " _
                                & "           WHEN pl_code = '990000000001' THEN CASE sod_tax_inc " _
                                & "                                                WHEN 'N' THEN (soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc) * 0.1 " _
                                & "                                                WHEN 'Y' THEN ((soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 " _
                                & "                                                  / 110.0 * sod_disc)) * 0.1 " _
                                & "                                              END " _
                                & "         END AS ppn_bayar, " _
                                & "         reason_mstr.code_name AS reason_name, " _
                                & "         ti_code, " _
                                & "         ptnrg_name, " _
                                & "         ti_date, " _
                                & "         pay_type.code_name AS pay_type_desc, " _
                                & "         CASE " _
                                & "           WHEN ptnr_mstr.ptnr_is_ps = 'Y' THEN 'PS' " _
                                & "           ELSE 'NON PS' " _
                                & "         END AS ps_status " _
                                & "       FROM public.soship_mstr " _
                                & "         INNER JOIN soshipd_det ON soshipd_soship_oid = soship_oid " _
                                & "         INNER JOIN so_mstr ON so_oid = soship_so_oid " _
                                & "         LEFT OUTER JOIN pi_mstr ON so_pi_id = pi_id " _
                                & "         INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
                                & "         INNER JOIN ptnr_mstr sales_mstr ON sales_mstr.ptnr_id = so_sales_person " _
                                & "         INNER JOIN en_mstr ON en_id = soship_en_id " _
                                & "         INNER JOIN si_mstr ON si_id = soship_si_id " _
                                & "         INNER JOIN ptnr_mstr ON ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                                & "         INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
                                & "         INNER JOIN code_mstr AS um_mstr ON um_mstr.code_id = soshipd_um " _
                                & "         INNER JOIN loc_mstr ON loc_id = soshipd_loc_id " _
                                & "         INNER JOIN cu_mstr ON cu_id = so_cu_id " _
                                & "         INNER JOIN code_mstr AS tax_mstr ON tax_mstr.code_id = sod_tax_class " _
                                & "         INNER JOIN code_mstr pay_type ON pay_type.code_id = so_pay_type " _
                                & "         LEFT OUTER JOIN code_mstr AS reason_mstr ON reason_mstr.code_id = soshipd_rea_code_id " _
                                & "         LEFT OUTER JOIN ars_ship ON ars_soshipd_oid = soshipd_oid " _
                                & "         LEFT OUTER JOIN ar_mstr ON ar_oid = ars_ar_oid " _
                                & "         LEFT OUTER JOIN tia_ar ON tia_ar_oid = ar_oid " _
                                & "         LEFT OUTER JOIN ti_mstr ON ti_oid = tia_ti_oid " _
                                & "         LEFT OUTER JOIN ptnrg_grp ON ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id " _
                                & "         INNER JOIN pl_mstr ON pl_id = pt_pl_id " _
                                & "         LEFT OUTER JOIN public.arpayd_det ON (public.ars_ship.ars_ar_oid = public.arpayd_det.arpayd_ar_oid)  " _
                                & "         LEFT OUTER JOIN public.arpay_payment ON (public.arpayd_det.arpayd_arpay_oid = public.arpay_payment.arpay_oid)  " _
                                & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & "  and so_en_id in (select user_en_id from tconfuserentity " _
                                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                & "GROUP BY si_mstr.si_desc, " _
                                & "  en_desc, " _
                                & "  so_date, " _
                                & "  so_code, " _
                                & "  soship_date, " _
                                & "  soship_code, " _
                                & "  ar_date, " _
                                & "  ar_code, " _
                                & "  ptnr_mstr.ptnr_code, " _
                                & "  ptnr_mstr.ptnr_name, " _
                                & "  sales_mstr.ptnr_code, " _
                                & "  sales_mstr.ptnr_name, " _
                                & "  pi_desc, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  um_mstr.code_name, " _
                                & "  loc_desc, " _
                                & "  sod_cost, " _
                                & "  sod_price, " _
                                & "  sod_disc, " _
                                & "  sod_tax_inc, " _
                                & "  sod_sales_unit, " _
                                & "  pl_desc, " _
                                & "  soship_is_shipment, " _
                                & "  soshipd_seq, " _
                                & "  cu_name, " _
                                & "  so_exc_rate, " _
                                & "  sod_taxable, " _
                                & "  tax_mstr.code_name, " _
                                & "  reason_mstr.code_name, " _
                                & "  ti_code, " _
                                & "  ptnrg_name, " _
                                & "  ti_date, " _
                                & "  pay_type.code_name, " _
                                & "  ptnr_mstr.ptnr_is_ps, " _
                                & "  soshipd_qty, " _
                                & "  pl_code " _
                                & "  ) as shipment_data"

                            .InitializeCommand()
                            .FillDataSet(ds, "view1")

                            gc_view1.DataSource = ds.Tables("view1")
                        End If

                        bestfit_column()
                        'load_data_grid_detail()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
