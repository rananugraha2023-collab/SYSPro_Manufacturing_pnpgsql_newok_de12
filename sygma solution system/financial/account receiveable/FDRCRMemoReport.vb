Imports npgsql
Imports master_new.ModFunction

Public Class FDRCRMemoReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FDRCRMemo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "AR Eff. Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Is Personal Sales", "ptnr_is_ps", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "ar_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Shipment / Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipment / Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Shipment", "soship_is_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "ars_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "ars_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Invoice Qty", "ars_invoice", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Invoice Price", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount Value", "ars_so_disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "DPP", "dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Netto", "netto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Netto Ext.", "netto_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  en_desc, " _
                & "  ptnr_mstr.ptnr_name, " _
                & "  ar_code, " _
                & "  ar_date, " _
                & "  ar_eff_date, " _
                & "  cu_name, " _
                & "  term_mstr.code_name as term_name, " _
                & "  ar_amount, " _
                & "  ar_pay_amount, " _
                & "  ar_amount - ar_pay_amount as ar_outstanding, " _
                & "  ar_exc_rate, " _
                & "  ar_remarks, " _
                & "  so_code, " _
                & "  so_date, " _
                & "  soship_code, " _
                & "  soship_date,ptnr_mstr.ptnr_is_ps, " _
                & "  soship_is_shipment, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  ars_taxable, " _
                & "  ars_tax_inc, " _
                & "  tax_mstr.code_name as tax_name, " _
                & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                & "  sod_qty, " _
                & "  ars_invoice, " _
                & "  ars_invoice_price, " _
                & "  ars_so_disc_value, " _
                & "  ars_invoice_price - ars_so_disc_value as dpp, " _
                & "  case ars_tax_inc " _
                & "  when 'N' then (ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/100) as numeric(26,8))  " _
                & "  when 'Y' then (ars_invoice_price - ars_so_disc_value) *  cast((taxr_rate/110)  as numeric(26,8))  " _
                & "  end as ppn, " _
                & "  case ars_tax_inc " _
                & "  when 'N' then (ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/100) as numeric(26,8))) " _
                & "  when 'Y' then (ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/110)  as numeric(26,8))) " _
                & "  end as netto, " _
                & "  case ars_tax_inc " _
                & "  when 'N' then ars_invoice * ((ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/100) as numeric(26,8)))) " _
                & "  when 'Y' then ars_invoice * ((ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/110)  as numeric(26,8)))) " _
                & "  end as netto_ext " _
                & "FROM  " _
                & "  public.ar_mstr " _
                & "  inner join ars_ship on ars_ar_oid = ar_oid " _
                & "  inner join en_mstr on en_id = ar_en_id " _
                & "  inner join cu_mstr on cu_id = ar_cu_id " _
                & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                & "  inner join code_mstr term_mstr on term_mstr.code_id = ar_credit_term " _
                & "  inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                & "  inner join so_mstr on so_oid = sod_so_oid " _
                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                & "  inner join code_mstr tax_mstr on tax_mstr.code_id = ars_tax_class_id " _
                & "  inner join taxr_mstr on taxr_tax_class = ars_tax_class_id " _
                & "  inner join code_mstr ttype_mstr on ttype_mstr.code_id = taxr_tax_type " _
                & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = so_sales_person " _
                & "  where ttype_mstr.code_code ~~* 'PPN'" _
                & "  and ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and ar_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"


        Return get_sequel
    End Function

End Class
