Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FSOARFPPerFADReportDetail
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pb_oid_mstr As String
    Dim mf As New master_new.ModFunction
    Public ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pbd_related_oid As String = ""
    'Public _pwsr_ptnr_id As Integer
    Dim _conf_value As String
    Dim _now As Date

    Private Sub FSOARFPPerFADReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_wsr_request")
        form_first_load()
        '_now = func_coll.get_tanggal_sistem
        'pr_txttglawal.DateTime = _now
        'pr_txttglakhir.DateTime = _now

        'If _conf_value = "0" Then
        '    xtc_detail.TabPages(1).PageVisible = False
        '    xtc_detail.TabPages(3).PageVisible = False
        'ElseIf _conf_value = "1" Then
        '    xtc_detail.TabPages(1).PageVisible = True
        '    xtc_detail.TabPages(3).PageVisible = True
        'End If
        'xtc_detail.SelectedTabPageIndex = 0

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        AddHandler gv_master.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_master.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'pwsr_en_id.Properties.DataSource = dt_bantu
        'pwsr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'pwsr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'pwsr_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_all())
        'pwsr_en_id_shipment.Properties.DataSource = dt_bantu
        'pwsr_en_id_shipment.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'pwsr_en_id_shipment.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'pwsr_en_id_shipment.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_pb_type())
        'pwsr_rwd_code.Properties.DataSource = dt_bantu
        'pwsr_rwd_code.Properties.DisplayMember = dt_bantu.Columns("pbt_desc").ToString
        'pwsr_rwd_code.Properties.ValueMember = dt_bantu.Columns("pbt_code").ToString
        'pwsr_rwd_code.ItemIndex = 0


        'If _conf_value = "0" Then
        '    dt_bantu = New DataTable
        '    dt_bantu = (func_data.load_tran_mstr())
        '    pwsr_tran_id.Properties.DataSource = dt_bantu
        '    pwsr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        '    pwsr_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        '    pwsr_tran_id.ItemIndex = 0
        'Else
        '    dt_bantu = New DataTable
        '    dt_bantu = (func_data.load_transaction())
        '    pwsr_tran_id.Properties.DataSource = dt_bantu
        '    pwsr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        '    pwsr_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
        '    pwsr_tran_id.ItemIndex = 0
        'End If
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipment / Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Shipment / Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Shipment", "soship_is_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AY Date", "arpay_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "AY Number", "arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales", "sales_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "(%) Cost", "pct_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Sales", "sales_ttl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Total Bruto ", "total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "(%) Gross Sales Contribution", "grss_sls_contr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Discount Value", "disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "FP Price", "price_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "FP Disc. Value", "disc_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Netto", "dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "(%) Netto Sales Contribution", "nett_sls_contr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Total Cost", "total_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "(%)Total Cost", "ttl_cost_nett", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Gross Profit", "gross_profit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "(%) Gross Profit", "netto_gross_profit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Product Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reason Code", "reason_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Vat Paid", "ppn_bayar", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Vat Free", "ppn_bebas", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sales Unit Total", "sod_sales_unit_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sales Person", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Partner Name", "hppr_nama_costumer", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Type", "hppr_type_hpp", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Partnumber", "hppr_nama_produk", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_master, "Qty", "hppr_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Qty", "hppr_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Margin (%)", "hppr_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Insentif (%)", "hppr_insentif_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Reward (%)", "hppr_reward_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Fee (%)", "hppr_fee_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Price", "hppr_hasil_satuan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Total Price", "hppr_hasil_total_harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Print Date", "hppr_tgl_print", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "SPH Code", "hppr_number_kertas", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "hppr_oid", False)
        'add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Type", "hppr_type_hpp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Product Name", "hppr_nama_produk", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Ukuran", "hppr_ukuran", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sisi", "hppr_sisi_flatbed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Isian", "hppr_isian", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Isi Trans Process", "hppr_tambahansisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Endsheet", "hppr_costum_endsheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Cover", "hppr_isi_cover", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sisipan Trans Complete", "hppr_tambahan_sisipan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Tambahan Endsheet", "hppr_tambahan_endsheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Issue Process", "hppr_po_customer_relasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Margin", "hppr_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Insentif Sales", "hppr_insentif_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Reward Mitra", "hppr_reward_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Fee Mitra", "hppr_fee_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        
        get_sequel = "SELECT DISTINCT* " _
                                & "FROM ( " _
                                & "        SELECT si_desc, " _
                                & "         en_desc, " _
                                & "         so_date, " _
                                & "         so_code, " _
                                & "         soship_date, " _
                                & "         soship_code, " _
                                & "         ar_date, " _
                                & "         ar_code, " _
                                & "         arpay_date, " _
                                & "         arpay_code, " _
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
                                & "  arpay_date, " _
                                & "  arpay_code, " _
                                & "  pl_code " _
                                & "  ) as shipment_data"

        Return get_sequel
    End Function

    Private Sub load_data_to_detail()
        'Public Overrides Sub load_data_grid_detail()
        'load_detail()
        If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  public.hppr_mstr.hppr_oid, " _
            & "  public.hppr_mstr.hppr_type_hpp, " _
            & "  public.hppr_mstr.hppr_nama_produk, " _
            & "  public.hppr_mstr.hppr_ukuran, " _
            & "  public.hppr_mstr.hppr_isian, " _
            & "  public.hppr_mstr.hppr_sisi_flatbed, " _
            & "  public.hppr_mstr.hppr_tambahansisi, " _
            & "  public.hppr_mstr.hppr_costum_endsheet, " _
            & "  public.hppr_mstr.hppr_isi_cover, " _
            & "  public.hppr_mstr.hppr_tambahan_sisipan, " _
            & "  public.hppr_mstr.hppr_po_customer_relasi, " _
            & "  public.hppr_mstr.hppr_margin, " _
            & "  public.hppr_mstr.hppr_insentif_sales, " _
            & "  public.hppr_mstr.hppr_reward_mitra, " _
            & "  public.hppr_mstr.hppr_fee_mitra, " _
            & "  public.hppr_mstr.hppr_tambahan_endsheet " _
            & "FROM " _
            & "  public.hppr_mstr " _
            & "  where public.hppr_mstr.hppr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("hppr_oid").ToString & "'"

        load_data_detail(sql, gc_detail, "detail")

        
    End Sub

    Public Overrides Sub relation_detail()
        'Try
        '    gv_detail.Columns("pbd_pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pbd_pb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'")
        '    gv_detail.BestFitColumns()

        '    If _conf_value = "1" Then
        '        gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code").ToString & "'")
        '        gv_wf.BestFitColumns()

        '        gv_email.Columns("pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'")
        '        gv_email.BestFitColumns()
        '    End If

        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            load_data_to_detail()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Sub load_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "select  " _
            & "  public.hppr_mstr.hppr_oid, " _
            & "  public.hppr_mstr.hppr_pt_id, " _
            & "  public.hppr_mstr.hppr_nama_produk, " _
            & "  public.hppr_mstr.hppr_ukuran, " _
            & "  public.hppr_mstr.hppr_isian, " _
            & "  public.hppr_mstr.hppr_sisi_flatbed, " _
            & "  public.hppr_mstr.hppr_tambahansisi, " _
            & "  public.hppr_mstr.hppr_costum_endsheet, " _
            & "  public.hppr_mstr.hppr_isi_cover, " _
            & "  public.hppr_mstr.hppr_tambahan_sisipan, " _
            & "  public.hppr_mstr.hppr_po_customer_relasi, " _
            & "  public.hppr_mstr.hppr_margin, " _
            & "  public.hppr_mstr.hppr_insentif_sales, " _
            & "  public.hppr_mstr.hppr_reward_mitra, " _
            & "  public.hppr_mstr.hppr_fee_mitra, " _
            & "  public.hppr_mstr.hppr_tambahan_endsheet " _
            & "from " _
            & "  public.hppr_mstr " _
            & "  where public.hppr_mstr.hppr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("hppr_oid").ToString & "'"


        load_data_detail(sql, gc_detail, "detail")


    End Sub
#End Region

End Class
