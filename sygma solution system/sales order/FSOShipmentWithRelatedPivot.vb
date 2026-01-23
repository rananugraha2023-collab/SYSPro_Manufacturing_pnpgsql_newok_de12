Imports master_new.ModFunction

Public Class FSOShipmentWithRelatedPivot
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARReportByAging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_general_ledger_print1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_general_ledger_print1.DataTable1)
        form_first_load()
        format_pivot()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal.Date
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal.Date
    End Sub

    Public Overrides Sub load_cb()
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
                            load_ar(objload)
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
    Private Sub format_pivot()
        add_column_pivot(pgc_ar, "Entity", "en_desc", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Sold To", "ptnr_name", DevExpress.XtraPivotGrid.PivotArea.RowArea)

        add_column_pivot(pgc_ar, "SO Number", "so_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Pay Type", "pay_type_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Effective Date", "so_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea)

        add_column_pivot(pgc_ar, "Sales Program", "sls_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Sales ID", "ptnr_id", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Sales Code", "ptnr_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Sales", "sales_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "PS Status", "ps_status", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Customer Group", "ptnrg_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Price List", "pi_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Shipment / Return Number", "soship_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Shipment Year", "soship_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear, "soship_date_year")
        add_column_pivot(pgc_ar, "Shipment Month", "soship_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth, "soship_date_month")
        add_column_pivot(pgc_ar, "Shipment Date", "soship_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")

        add_column_pivot(pgc_ar, "Is Shipment", "soship_is_shipment", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Currency", "cu_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Exchange Rate", "so_exc_rate", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Description2", "pt_desc2", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "UM", "um_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Location", "loc_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Reason Code", "reason_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Taxable", "sod_taxable", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Tax Include", "sod_tax_inc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Tax Class", "tax_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Qty", "soshipd_qty", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Cost", "sod_cost", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Total Sales", "sales_ttl", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Discount Value", "disc_value", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "FP Price", "price_fp", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "FP Disc. Value", "disc_fp", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Netto", "dpp", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Sales Unit", "sod_sales_unit", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")


    End Sub
    Private Sub load_ar(ByVal par_obj As Object)
        pgc_ar.DataSource = Nothing
        pgc_ar.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "data_ar")
            pgc_ar.DataSource = ds.Tables("data_ar")
            pgc_ar.BestFit()
        End With
    End Sub
    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_ar.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
    Private Function set_sql() As String

        set_sql = "SELECT    " _
                  & "  en_desc,sls_name, " _
                  & "  so_code, " _
                  & "  so_date, " _
                  & "  ptnr_mstr.ptnr_name, " _
                  & "  sales_mstr.ptnr_code,sales_mstr.ptnr_id, " _
                  & "  soship_code, " _
                  & "  soship_date, " _
                  & "  si_desc, " _
                  & "  soship_is_shipment, " _
                  & "  soshipd_seq, " _
                  & "  cu_name, " _
                  & "  so_exc_rate, " _
                  & "  pt_code,sod_cost, " _
                  & "  pt_desc1, " _
                  & "  pt_desc2, " _
                  & "  sod_taxable, " _
                  & "  sod_tax_inc, " _
                   & " sod_sales_unit, " _
                  & " (sod_sales_unit * (-1) * soshipd_qty) as sod_sales_unit_total, " _
                  & "  tax_mstr.code_name as tax_name, " _
                  & "  soshipd_qty * -1 as soshipd_qty, " _
                  & "  sod_price, " _
                  & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
                  & "  sod_disc,  soshipd_qty * -1 * sod_cost  as total_cost, " _
                  & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                  & "   " _
                  & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then soshipd_qty * -1 * sod_price " _
                  & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                  & "  end as price_fp, " _
                  & "   " _
                  & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                  & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc " _
                  & "  end as disc_fp, " _
                  & "   " _
                  & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                  & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                  & "  end as dpp, " _
                      & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                  & "  when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                  & "  end as gross_profit, " _
                  & "   " _
                  & "  pl_desc,  " _
                  & "   " _
                  & "  case pl_code " _
                  & "  when '990000000001' then " _
                  & "  					case upper(sod_tax_inc) " _
                  & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                  & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.11 " _
                  & "                    end " _
                  & "  end as ppn_bayar, " _
                  & "   " _
                  & "  case pl_code " _
                  & "  when '990000000002' then " _
                  & "  					case upper(sod_tax_inc) " _
                  & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                  & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                  & "                    end " _
                  & "  end as ppn_bebas, " _
                  & "               " _
                  & "  um_mstr.code_name as um_name, " _
                  & "  loc_desc, " _
                  & "  reason_mstr.code_name as reason_name, " _
                  & "  ar_code, " _
                  & "  ar_date, " _
                  & "  ti_code,ptnrg_name, " _
                  & "sales_mstr.ptnr_name as sales_name, " _
                  & "  ti_date,pay_type.code_name as pay_type_desc,pi_desc, case when ptnr_mstr.ptnr_is_ps='Y' then 'PS' else 'NON PS' end as ps_status  " _
                  & "FROM  " _
                  & "  public.soship_mstr " _
                  & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                  & "  inner join so_mstr on so_oid = soship_so_oid " _
                  & "  inner join pi_mstr on so_pi_id = pi_id " _
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
                  & "  left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id " _
                  & "  inner join pl_mstr on pl_id = pt_pl_id " _
                  & "  left outer join sls_program on so_sales_program = sls_code " _
                  & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                  & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                  & "  and pay_type.code_usr_1 <> '0'  " _
                  & "  and so_en_id in (select user_en_id from tconfuserentity " _
                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                  & " union all " _
                  & " SELECT    " _
                  & "  en_desc,sls_name, " _
                  & "  so_code, " _
                  & "  so_date, " _
                  & "  ptnr_mstr.ptnr_name, " _
                   & "  sales_mstr.ptnr_code,sales_mstr.ptnr_id, " _
                  & "  soship_code, " _
                  & "  soship_date, " _
                  & "  si_desc, " _
                  & "  soship_is_shipment, " _
                  & "  soshipd_seq, " _
                  & "  cu_name, " _
                  & "  so_exc_rate, " _
                  & "  pt_code,sod_cost, " _
                  & "  pt_desc1, " _
                  & "  pt_desc2, " _
                  & "  sod_taxable, " _
                  & "  sod_tax_inc, " _
                  & " sod_sales_unit, " _
                  & " (sod_sales_unit * (-1) * soshipd_qty) as sod_sales_unit_total,  " _
                  & "  tax_mstr.code_name as tax_name, " _
                  & "  soshipd_qty * -1 as soshipd_qty, " _
                  & "  sod_price, " _
                  & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
                  & "  sod_disc,  soshipd_qty * -1 * sod_cost  as total_cost, " _
                  & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                  & "   " _
                  & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then soshipd_qty * -1 * sod_price " _
                  & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                  & "  end as price_fp, " _
                  & "   " _
                  & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                  & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc " _
                  & "  end as disc_fp, " _
                  & "   " _
                  & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                  & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                  & "  end as dpp, " _
                   & "  case upper(sod_tax_inc) " _
                  & "  when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                  & "  when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))-(soshipd_qty * -1 * sod_cost) " _
                  & "  end as gross_profit, " _
                  & "   " _
                  & "  pl_desc,  " _
                  & "   " _
                  & "  case pl_code " _
                  & "  when '990000000001' then " _
                  & "  					case upper(sod_tax_inc) " _
                  & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                  & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.11 " _
                  & "                    end " _
                  & "  end as ppn_bayar, " _
                  & "   " _
                  & "  case pl_code " _
                  & "  when '990000000002' then " _
                  & "  					case upper(sod_tax_inc) " _
                  & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                  & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                  & "                    end " _
                  & "  end as ppn_bebas, " _
                  & "               " _
                  & "  um_mstr.code_name as um_name, " _
                  & "  loc_desc, " _
                  & "  reason_mstr.code_name as reason_name, " _
                  & "  null as ar_code, " _
                  & "  null as ar_date, " _
                  & "  ti_code,ptnrg_name, " _
                   & "sales_mstr.ptnr_name as sales_name, " _
                  & "  ti_date ,pay_type.code_name as pay_type_desc,pi_desc, case when ptnr_mstr.ptnr_is_ps='Y' then 'PS' else 'NON PS' end as ps_status " _
                  & "FROM  " _
                  & "  public.soship_mstr " _
                  & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                  & "  inner join so_mstr on so_oid = soship_so_oid " _
                   & "  inner join pi_mstr on so_pi_id = pi_id " _
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
                  & "  left outer join tis_soship on tis_soship_oid = soship_oid " _
                  & "  left outer join ti_mstr on ti_oid = tis_ti_oid " _
                  & "  left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id " _
                  & "  inner join pl_mstr on pl_id = pt_pl_id " _
                  & "  left outer join sls_program on so_sales_program = sls_code " _
                  & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                  & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                  & "  and pay_type.code_usr_1 = '0' " _
                  & "  and so_en_id in (select user_en_id from tconfuserentity " _
                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "
                  
        Return set_sql
    End Function

   

    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                               ByVal par_area As DevExpress.XtraPivotGrid.PivotArea)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption
    
    End Sub

    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                               ByVal par_area As DevExpress.XtraPivotGrid.PivotArea, ByVal par_group_interval As DevExpress.XtraPivotGrid.PivotGroupInterval, ByVal par_column_name As String)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Name = par_column_name
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption
        pvg_field.GroupInterval = par_group_interval


    End Sub
    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                               ByVal par_area As DevExpress.XtraPivotGrid.PivotArea, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption
        pvg_field.CellFormat.FormatType = formatType
        pvg_field.CellFormat.FormatString = formatString
        pvg_field.GrandTotalCellFormat.FormatType = formatType
        pvg_field.GrandTotalCellFormat.FormatString = formatString
        pvg_field.ValueFormat.FormatType = formatType
        pvg_field.ValueFormat.FormatString = formatString
        pvg_field.TotalCellFormat.FormatType = formatType
        pvg_field.TotalCellFormat.FormatString = formatString
        pvg_field.TotalValueFormat.FormatType = formatType
        pvg_field.TotalValueFormat.FormatString = formatString

    End Sub
End Class
