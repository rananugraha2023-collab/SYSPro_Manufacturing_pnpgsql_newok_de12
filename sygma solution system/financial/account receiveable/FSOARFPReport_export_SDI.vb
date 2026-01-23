Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports System.Math


Public Class FSOARFPReport_export_SDI
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ds_detail As DataSet

    Private Sub FSOARFPReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_cash_out_ar1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_cash_out_ar1.DataTable1)
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        AddHandler gv_view1.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_view1.ColumnFilterChanged, AddressOf relation_detail
    End Sub
    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "FK", "tmptax_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "KD_JENIS_TRANSAKSI", "tmptax_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "FG_PENGGANTI", "tmptax_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_view1, "NOMOR_FAKTUR", "tmptax_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "MASA_PAJAK", "tmptax_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "TAHUN_PAJAK", "tmptax_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "TANGGAL_FAKTUR", "tmptax_7", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "NPWP", "tmptax_8", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "NAMA", "tmptax_9", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "ALAMAT_LENGKAP", "tmptax_10", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "JUMLAH_DPP", "tmptax_11", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "JUMLAH_PPN", "tmptax_12", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "JUMLAH_PPNBM", "tmptax_13", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "ID_KETERANGAN_TAMBAHAN", "tmptax_14", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "FG_UANG_MUKA", "tmptax_15", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UANG_MUKA_DPP", "tmptax_16", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UANG_MUKA_PPN", "tmptax_17", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UANG_MUKA_PPNBM", "tmptax_18", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "REFERENSI", "tmptax_19", DevExpress.Utils.HorzAlignment.Default)


    End Sub
    Public Sub load_header_row()
        Using objload4 As New master_new.WDABasepgsql("", "")
            With objload4
                .SQL = "SELECT  " _
                        & "  tmptax_1, " _
                        & "  tmptax_2, " _
                        & "  tmptax_3, " _
                        & "  tmptax_4, " _
                        & "  tmptax_5, " _
                        & "  tmptax_6, " _
                        & "  tmptax_7, " _
                        & "  tmptax_8, " _
                        & "  tmptax_9, " _
                        & "  tmptax_10, " _
                        & "  tmptax_11, " _
                        & "  tmptax_12, " _
                        & "  tmptax_13, " _
                        & "  tmptax_14, " _
                        & "  tmptax_15, " _
                        & "  tmptax_16, " _
                        & "  tmptax_17, " _
                        & "  tmptax_18, " _
                        & "  tmptax_19, " _
                        & "  tmptax_20 " _
                        & " FROM  " _
                        & "  akses.tmp_tax " _
                        & " WHERE tmptax_20 <> '1' "


                .InitializeCommand()
                .FillDataSet(ds, "view1")

            End With
        End Using
    End Sub
    Function get_group_tax(ByVal _par_id As String) As String
        Try
            Dim _hasil As String = ""

            Dim sSQL As String = ""

            sSQL = "SELECT  " _
                & "  a.tipg_oid, " _
                & "  a.tipg_code, " _
                & "  a.tipg_desc, " _
                & "  a.tipg_ptnr_id, " _
                & "  c.ptnr_id, " _
                & "  c.ptnr_code, " _
                & "  c.ptnr_name " _
                & "FROM " _
                & "  public.tipg_group a " _
                & "  INNER JOIN public.tipgd_det b ON (a.tipg_oid = b.tipgd_tipg_oid) " _
                & "  INNER JOIN public.ptnr_mstr c ON (b.tipgd_ptnr_id = c.ptnr_id) " _
                & "WHERE " _
                & "  a.tipg_code = '" & _par_id & "' "

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            For Each dr As DataRow In dt.Rows
                _hasil = _hasil & "," & dr("ptnr_id").ToString
            Next
            _hasil = _hasil.Substring(2).ToString


            Return _hasil

        Catch ex As Exception
            Pesan(Err)
            Return ""
        End Try
    End Function
    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet

                If xtc_master.SelectedTabPageIndex = 0 Then

                    'Dim dtr As DataRow
                    'Dim dtrppn As DataRow
                    'Dim dtrppn_bbs As DataRow
                    'Dim qr_add As String = ""
                    Dim sSql As String = ""



                    Try

                        Dim dtmp2 As New DataTable


                        sSql = "SELECT    " _
                              & "  en_desc, " _
                              & "  so_code, " _
                              & "  so_date, " _
                              & "  ptnr_mstr.ptnr_name, COALESCE(ptnr_mstr.ptnr_npwp,'000000000000000') as ptnr_npwp, ptnr_mstr.ptnr_address_tax, " _
                              & "  ptnr_mstr.ptnr_code, " _
                              & "  soship_code, " _
                              & "  soship_date, " _
                              & "  si_desc, " _
                              & "  soship_is_shipment, " _
                              & "  soshipd_seq, " _
                              & "  cu_name, " _
                              & "  so_exc_rate, " _
                              & "  pt_code, sod_cost, " _
                              & "  pt_desc1, " _
                              & "  pt_desc2, " _
                              & "  sod_taxable, sod_tax_class, " _
                              & "  sod_tax_inc, sod_sales_unit * sod_qty as so_sales_unit, " _
                              & "  tax_mstr.code_name as tax_name, " _
                              & "  soshipd_qty * -1 as soshipd_qty, " _
                              & "  sod_price, " _
                              & " soshipd_qty * -1 * sod_price as sales_ttl, " _
                              & "  sod_disc, " _
                              & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                              & "   " _
                              & " case upper(sod_tax_inc) " _
                              & "  when 'N' then soshipd_qty * -1 * sod_price " _
                              & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                              & "  end as price_fp, " _
                              & "   " _
                              & "  case upper(sod_tax_inc) " _
                              & "  when 'N' then sod_price " _
                              & "  when 'Y' then sod_price * cast((100.0/110.0) as numeric(26,8)) " _
                              & "  end as price_fp1, " _
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
                              & "   " _
                              & "  pl_desc,  " _
                              & "   " _
                              & "  COALESCE(case pl_code " _
                              & "  when '990000000001' then " _
                              & "  					case upper(sod_tax_inc) " _
                              & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                              & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.11 " _
                              & "                    end " _
                              & "  end,0) as ppn_bayar, " _
                              & "   " _
                              & "  COALESCE(case pl_code " _
                              & "  when '990000000002' then " _
                              & "  					case upper(sod_tax_inc) " _
                              & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                              & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                              & "                    end " _
                              & "  end,0) as ppn_bebas, " _
                              & "               " _
                              & "  um_mstr.code_name as um_name, " _
                              & "  loc_desc, " _
                              & "  reason_mstr.code_name as reason_name, " _
                              & "  ar_code, " _
                              & "  ar_date, " _
                              & "  ti_code, " _
                              & "  public.f_cek_return (so_code,pt_code) as sreturn, " _
                              & "sales_mstr.ptnr_name as sales_name, cmaddr_name, cmaddr_npwp, cmaddr_tax_line_1, cmaddr_tax_line_2, " _
                              & "  ti_date,pay_type.code_name as pay_type_desc, pi_desc, so_pay_type, trunc(so_total) as so_total, so_total_ppn " _
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
                              & "  inner join pl_mstr on pl_id = pt_pl_id " _
                              & "  left outer join cmaddr_mstr on cmaddr_en_id = en_id " _
                              & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                              & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                              & "  AND soship_is_shipment ='Y' AND so_pay_type = 9941 " _
                              & "  and so_en_id in (select user_en_id from tconfuserentity " _
                              & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "

                        '& "  AND ((so_pay_type = 9942 AND ar_code <> '' )  OR so_pay_type = 9941 ) " _

                        '001 group gramedia
                        'If ce_gramed.Checked = True Then

                        '    sSql = sSql + " AND ptnr_mstr.ptnr_id  in (" & get_group_tax("001") & ") order by sod_tax_class, so_code, soship_date"
                        'Else
                        '    sSql = sSql + " AND ptnr_mstr.ptnr_id not in (" & get_group_tax("001") & ")   order by so_code, soship_date"
                        'End If

                        'SEG & SDI
                        sSql = sSql + " order by so_code, soship_date "


                        dtmp2 = GetTableData(sSql)

                        'Dim i = 1
                        'Dim so_blm As String = ""
                        Dim ar_blm As String = ""
                        'Dim txc_blm As String = ""
                        'Dim sls_ttl As String = ""
                        'Dim ppn_ttl As String = ""
                        Dim sls_ttl_h As Decimal = 0
                        Dim ppn_ttl_h As Decimal = 0
                        Dim l_dppt As Decimal = 0
                        Dim l_ppnt As Decimal = 0
                        'Dim sls_ttlg As Decimal = 0
                        'Dim ppn_ttlg As Decimal = 0
                        Dim sales_tot_det As String = ""
                        Dim sales_dis_det As String = ""
                        Dim id_ket As String = ""
                        Dim taxclas As String = ""
                        Dim vat As String = ""
                        Dim paytype_date As Date
                        Dim paytype_ref As String = ""
                        Dim addr As String = ""
                        Dim h As Integer = 0
                        Dim h1 As Integer = 0
                        Dim drarray() As DataRow

                        load_header_row()

                        For Each Dr2 As DataRow In dtmp2.Rows

                            'If Dr2("ar_code") <> "" Then

                            If Dr2("sreturn") > 0 Then

                                ''cek tax class
                                If Dr2("sod_tax_class") = "9950" Then

                                    taxclas = "01"
                                    id_ket = ""
                                    vat = CStr(Decimal.Floor(Dr2("ppn_bayar") * 100) / 100)
                                    'ppn_ttl_h = ppn_ttl_h + Dr2("ppn_bayar")
                                    '    'ppn bayar
                                    sales_tot_det = CStr(Decimal.Floor(Dr2("price_fp") * 100) / 100)
                                    sales_dis_det = CStr(Decimal.Floor(Dr2("disc_fp") * 100) / 100)

                                    'l_ppnt = Dr2("ppn_bayar") + l_ppnt
                                    'sls_ttl_h = sls_ttl_h + Dr2("price_fp")

                                    'sSql = "SELECT public.f_cek_sum_ppn_cash('" & Dr2("so_code") & "'," & SetDate(Dr2("soship_date")) & ") as ppn_total"
                                    'dtrppn = GetRowInfo(sSql)

                                    'ppn_ttl_h = dtrppn("ppn_total")

                                    'l_ppnt = dtrppn("ppn_total") + l_ppnt


                                    'sSql = "SELECT public.f_cek_sum_dpp_cash ('" & Dr2("so_code") & "'," & SetDate(Dr2("soship_date")) & ") as dpp_total"
                                    'dtr = GetRowInfo(sSql)
                                    'sls_ttl_h = dtr("dpp_total")
                                    'l_dppt = sls_ttl_h + l_dppt
                                    'l_dppt = Dr2("price_fp") + l_dppt

                                ElseIf Dr2("sod_tax_class") = "9949" Then
                                    taxclas = "08"
                                    id_ket = "1"
                                    vat = CStr(Decimal.Floor(Dr2("ppn_bebas") * 100) / 100)

                                    'sSql = "SELECT public.f_cek_sum_ppn_bebas_cash('" & Dr2("so_code") & "'," & SetDate(Dr2("soship_date")) & ") as ppn_total"
                                    'dtrppn_bbs = GetRowInfo(sSql)

                                    'ppn_ttl_h = dtrppn_bbs("ppn_total")

                                    'l_ppnt = dtrppn_bbs("ppn_total") + l_ppnt

                                    'ppn_ttl_h = ppn_ttl_h + Dr2("ppn_bebas")
                                    '    'ppn bebas
                                    sales_tot_det = CStr(Decimal.Floor(Dr2("sales_ttl") * 100) / 100)
                                    sales_dis_det = CStr(Decimal.Floor(Dr2("disc_value") * 100) / 100)



                                    'l_ppnt = Dr2("ppn_bebas") + l_ppnt
                                    'l_dppt = Dr2("dpp") + l_dppt
                                    'sls_ttl_h = sls_ttl_h + Dr2("dpp")

                                End If

                                'sSql = "SELECT public.f_cek_sum_dpp_cash ('" & Dr2("so_code") & "'," & SetDate(Dr2("soship_date")) & ") as dpp_total"
                                'dtr = GetRowInfo(sSql)
                                'sls_ttl_h = dtr("dpp_total")
                                'l_dppt = sls_ttl_h + l_dppt


                                drarray = dtmp2.Select("so_code='" & Dr2("so_code").ToString & "'", "so_code", DataViewRowState.CurrentRows)
                                sls_ttl_h = 0
                                ppn_ttl_h = 0
                                For x As Integer = 0 To (drarray.Length - 1)
                                    sls_ttl_h += Floor(drarray(x)("dpp")) 'IIf(drarray(x)("dpp") Is System.DBNull.Value, 0, drarray(x)("dpp"))
                                    ppn_ttl_h += IIf(drarray(x)("sod_tax_class") = "9950", drarray(x)("ppn_bayar"), drarray(x)("ppn_bebas"))
                                Next

                                l_dppt = sls_ttl_h + l_dppt
                                l_ppnt = ppn_ttl_h + l_ppnt


                                'paytype_date = CDate(Dr2("soship_date"))
                                paytype_date = CDate(Dr2("soship_date"))

                                If Dr2("ptnr_address_tax").ToString.Length = 0 Then
                                    addr = "-"
                                Else
                                    addr = Dr2("ptnr_address_tax").ToString
                                End If


                                If ce_gramed.Checked = True Then

                                    If Dr2("sod_tax_class") = "9950" Then
                                        If h = 0 Then
                                            drarray = dtmp2.Select("sod_tax_class='" & Dr2("sod_tax_class").ToString & "'", "sod_tax_class", DataViewRowState.CurrentRows)
                                            sls_ttl_h = 0
                                            ppn_ttl_h = 0
                                            For x As Integer = 0 To (drarray.Length - 1)
                                                sls_ttl_h += IIf(drarray(x)("dpp") Is System.DBNull.Value, 0, drarray(x)("dpp"))
                                                ppn_ttl_h += IIf(taxclas = "01", IIf(drarray(x)("ppn_bayar") Is System.DBNull.Value, 0, drarray(x)("ppn_bayar")), IIf(drarray(x)("ppn_bebas") Is System.DBNull.Value, 0, drarray(x)("ppn_bebas")))
                                            Next


                                            Dim NewRow5 As DataRow
                                            NewRow5 = ds.Tables("view1").NewRow

                                            NewRow5("tmptax_1") = "FK"
                                            NewRow5("tmptax_2") = taxclas
                                            NewRow5("tmptax_3") = "0"
                                            NewRow5("tmptax_4") = ""
                                            NewRow5("tmptax_5") = Format(paytype_date, "MM") * 1
                                            NewRow5("tmptax_6") = Format(paytype_date, "yyyy")
                                            NewRow5("tmptax_7") = Format(paytype_date, "dd/MM/yyyy")
                                            NewRow5("tmptax_8") = Replace(Replace(Dr2("ptnr_npwp"), ".", ""), "-", "")
                                            NewRow5("tmptax_9") = Dr2("ptnr_name")
                                            NewRow5("tmptax_10") = addr
                                            NewRow5("tmptax_11") = Decimal.Floor(sls_ttl_h) '& " + " & l_dppt
                                            NewRow5("tmptax_12") = Decimal.Floor(ppn_ttl_h) '& " + " & l_ppnt
                                            NewRow5("tmptax_13") = "0"
                                            NewRow5("tmptax_14") = id_ket
                                            NewRow5("tmptax_15") = "0"
                                            NewRow5("tmptax_16") = "0"
                                            NewRow5("tmptax_17") = "0"
                                            NewRow5("tmptax_18") = "0"
                                            NewRow5("tmptax_19") = paytype_ref
                                            ds.Tables("view1").Rows.Add(NewRow5)

                                            Dim NewRow6 As DataRow
                                            NewRow6 = ds.Tables("view1").NewRow

                                            NewRow6("tmptax_1") = "FAPR"
                                            'NewRow3("tmptax_2") = Dr2("cmaddr_npwp")
                                            NewRow6("tmptax_2") = Dr2("cmaddr_name")
                                            NewRow6("tmptax_3") = Dr2("cmaddr_tax_line_1") & " " & Dr2("cmaddr_tax_line_2")

                                            ds.Tables("view1").Rows.Add(NewRow6)

                                            ppn_ttl_h = 0
                                            sls_ttl_h = 0

                                            ar_blm = Dr2("so_code")

                                            h = 1
                                        End If
                                    ElseIf Dr2("sod_tax_class") = "9949" Then
                                        If h1 = 0 Then
                                            drarray = dtmp2.Select("sod_tax_class='" & Dr2("sod_tax_class").ToString & "'", "sod_tax_class", DataViewRowState.CurrentRows)
                                            sls_ttl_h = 0
                                            ppn_ttl_h = 0
                                            For x As Integer = 0 To (drarray.Length - 1)
                                                sls_ttl_h += IIf(drarray(x)("dpp") Is System.DBNull.Value, 0, drarray(x)("dpp"))
                                                ppn_ttl_h += IIf(taxclas = "01", IIf(drarray(x)("ppn_bayar") Is System.DBNull.Value, 0, drarray(x)("ppn_bayar")), IIf(drarray(x)("ppn_bebas") Is System.DBNull.Value, 0, drarray(x)("ppn_bebas")))
                                            Next

                                            Dim NewRow5 As DataRow
                                            NewRow5 = ds.Tables("view1").NewRow

                                            NewRow5("tmptax_1") = "FK"
                                            NewRow5("tmptax_2") = taxclas
                                            NewRow5("tmptax_3") = "0"
                                            NewRow5("tmptax_4") = ""
                                            NewRow5("tmptax_5") = Format(paytype_date, "MM") * 1
                                            NewRow5("tmptax_6") = Format(paytype_date, "yyyy")
                                            NewRow5("tmptax_7") = Format(paytype_date, "dd/MM/yyyy")
                                            NewRow5("tmptax_8") = Replace(Replace(Dr2("ptnr_npwp"), ".", ""), "-", "")
                                            NewRow5("tmptax_9") = Dr2("ptnr_name")
                                            NewRow5("tmptax_10") = addr
                                            NewRow5("tmptax_11") = Decimal.Floor(sls_ttl_h) '& " + " & l_dppt
                                            NewRow5("tmptax_12") = Decimal.Floor(ppn_ttl_h) '& " + " & l_ppnt
                                            NewRow5("tmptax_13") = "0"
                                            NewRow5("tmptax_14") = id_ket
                                            NewRow5("tmptax_15") = "0"
                                            NewRow5("tmptax_16") = "0"
                                            NewRow5("tmptax_17") = "0"
                                            NewRow5("tmptax_18") = "0"
                                            NewRow5("tmptax_19") = paytype_ref
                                            ds.Tables("view1").Rows.Add(NewRow5)

                                            Dim NewRow6 As DataRow
                                            NewRow6 = ds.Tables("view1").NewRow

                                            NewRow6("tmptax_1") = "FAPR"
                                            'NewRow3("tmptax_2") = Dr2("cmaddr_npwp")
                                            NewRow6("tmptax_2") = Dr2("cmaddr_name")
                                            NewRow6("tmptax_3") = Dr2("cmaddr_tax_line_1") & " " & Dr2("cmaddr_tax_line_2")

                                            ds.Tables("view1").Rows.Add(NewRow6)

                                            ppn_ttl_h = 0
                                            sls_ttl_h = 0

                                            ar_blm = Dr2("so_code")

                                            h1 = 1
                                        End If
                                    End If

                                Else

                                    'If Dr2("so_pay_type") <> "9941" Then
                                    paytype_ref = Dr2("so_code")


                                    If ar_blm <> Dr2("so_code").ToString Then

                                        Dim NewRow5 As DataRow
                                        NewRow5 = ds.Tables("view1").NewRow

                                        NewRow5("tmptax_1") = "FK"
                                        NewRow5("tmptax_2") = taxclas
                                        NewRow5("tmptax_3") = "0"
                                        NewRow5("tmptax_4") = ""
                                        NewRow5("tmptax_5") = Format(paytype_date, "MM") * 1
                                        NewRow5("tmptax_6") = Format(paytype_date, "yyyy")
                                        NewRow5("tmptax_7") = Format(paytype_date, "dd/MM/yyyy")
                                        NewRow5("tmptax_8") = Replace(Replace(Dr2("ptnr_npwp"), ".", ""), "-", "")
                                        NewRow5("tmptax_9") = Dr2("ptnr_name")
                                        NewRow5("tmptax_10") = addr
                                        NewRow5("tmptax_11") = Decimal.Floor(sls_ttl_h) '& " + " & l_dppt
                                        NewRow5("tmptax_12") = Decimal.Floor(ppn_ttl_h) '& " + " & l_ppnt
                                        NewRow5("tmptax_13") = "0"
                                        NewRow5("tmptax_14") = id_ket
                                        NewRow5("tmptax_15") = "0"
                                        NewRow5("tmptax_16") = "0"
                                        NewRow5("tmptax_17") = "0"
                                        NewRow5("tmptax_18") = "0"
                                        NewRow5("tmptax_19") = paytype_ref
                                        ds.Tables("view1").Rows.Add(NewRow5)

                                        Dim NewRow6 As DataRow
                                        NewRow6 = ds.Tables("view1").NewRow

                                        NewRow6("tmptax_1") = "FAPR"
                                        'NewRow3("tmptax_2") = Dr2("cmaddr_npwp")
                                        NewRow6("tmptax_2") = Dr2("cmaddr_name")
                                        NewRow6("tmptax_3") = Dr2("cmaddr_tax_line_1") & " " & Dr2("cmaddr_tax_line_2")

                                        ds.Tables("view1").Rows.Add(NewRow6)

                                        ppn_ttl_h = 0
                                        sls_ttl_h = 0

                                        ar_blm = Dr2("so_code")



                                    End If
                                End If
                                'End If

                                If CInt(Dr2("price_fp1")) > 0 Then



                                    Dim NewRow2 As DataRow
                                    NewRow2 = ds.Tables("view1").NewRow

                                    NewRow2("tmptax_1") = "OF" '& " > " & ar_blm & " <> " & Dr2("ar_code").ToString & " <> " & Dr2("so_code").ToString
                                    NewRow2("tmptax_2") = Dr2("pt_code")
                                    NewRow2("tmptax_3") = Dr2("pt_desc1")
                                    NewRow2("tmptax_4") = Replace(CStr(Decimal.Floor(Dr2("price_fp1") * 100) / 100), ",", ".")
                                    NewRow2("tmptax_5") = Dr2("soshipd_qty")
                                    NewRow2("tmptax_6") = Replace(sales_tot_det, ",", ".")
                                    NewRow2("tmptax_7") = Replace(sales_dis_det, ",", ".")
                                    NewRow2("tmptax_8") = Replace(CStr(Decimal.Floor(Dr2("dpp") * 100) / 100), ",", ".")
                                    NewRow2("tmptax_9") = Replace(vat, ",", ".")
                                    NewRow2("tmptax_10") = "0"
                                    NewRow2("tmptax_11") = "0.0"
                                    'NewRow2("tmptax_18") = Dr2("ar_code").ToString
                                    'NewRow2("tmptax_19") = Dr2("so_code").ToString


                                    ds.Tables("view1").Rows.Add(NewRow2)
                                End If



                                l_dpp.Text = FormatNumber(l_dppt, 2)
                                l_ppn.Text = FormatNumber(l_ppnt, 2)
                                'End If
                            End If
                        Next


                        sSql = "SELECT    " _
                              & "  en_desc, " _
                              & "  so_code, " _
                              & "  so_date, " _
                              & "  ptnr_mstr.ptnr_name, COALESCE(ptnr_mstr.ptnr_npwp,'000000000000000') as ptnr_npwp, ptnr_mstr.ptnr_address_tax, " _
                              & "  ptnr_mstr.ptnr_code, " _
                              & "  soship_code, " _
                              & "  soship_date, " _
                              & "  si_desc, " _
                              & "  soship_is_shipment, " _
                              & "  soshipd_seq, " _
                              & "  cu_name, " _
                              & "  so_exc_rate, " _
                              & "  pt_code, sod_cost, " _
                              & "  pt_desc1, " _
                              & "  pt_desc2, " _
                              & "  sod_taxable, sod_tax_class, " _
                              & "  sod_tax_inc, sod_sales_unit * sod_qty as so_sales_unit, " _
                              & "  tax_mstr.code_name as tax_name, " _
                              & "  soshipd_qty * -1 as soshipd_qty, " _
                              & "  sod_price, " _
                              & " soshipd_qty * -1 * sod_price as sales_ttl, " _
                              & "  sod_disc, " _
                              & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                              & "   " _
                              & " case upper(sod_tax_inc) " _
                              & "  when 'N' then soshipd_qty * -1 * sod_price " _
                              & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                              & "  end as price_fp, " _
                              & "   " _
                               & "   " _
                              & "  case upper(sod_tax_inc) " _
                              & "  when 'N' then sod_price " _
                              & "  when 'Y' then sod_price * cast((100.0/110.0) as numeric(26,8)) " _
                              & "  end as price_fp1, " _
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
                              & "   " _
                              & "  pl_desc,  " _
                              & "   " _
                              & "  COALESCE(case pl_code " _
                              & "  when '990000000001' then " _
                              & "  					case upper(sod_tax_inc) " _
                              & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                              & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.11 " _
                              & "                    end " _
                              & "  end,0) as ppn_bayar, " _
                              & "   " _
                              & "  COALESCE(case pl_code " _
                              & "  when '990000000002' then " _
                              & "  					case upper(sod_tax_inc) " _
                              & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                              & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                              & "                    end " _
                              & "  end,0) as ppn_bebas, " _
                              & "               " _
                              & "  um_mstr.code_name as um_name, " _
                              & "  loc_desc, " _
                              & "  reason_mstr.code_name as reason_name, " _
                              & "  ar_code, " _
                              & "  ar_date, " _
                              & "  ti_code, " _
                              & "  public.f_cek_return (so_code,pt_code) as sreturn, " _
                              & "sales_mstr.ptnr_name as sales_name, cmaddr_name, cmaddr_npwp, cmaddr_tax_line_1, cmaddr_tax_line_2, " _
                              & "  ti_date,pay_type.code_name as pay_type_desc, pi_desc, so_pay_type, trunc(so_total) as so_total, so_total_ppn " _
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
                              & "  inner join pl_mstr on pl_id = pt_pl_id " _
                              & "  left outer join cmaddr_mstr on cmaddr_en_id = en_id " _
                              & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                              & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                              & "  AND soship_is_shipment ='Y' AND so_pay_type <> 9941 " _
                              & "  and so_en_id in (select user_en_id from tconfuserentity " _
                              & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "

                        '& "  AND ((so_pay_type = 9942 AND ar_code <> '' )  OR so_pay_type = 9941 ) " _

                        '001 group gramedia
                        'If ce_gramed.Checked = True Then

                        '    sSql = sSql + " AND ptnr_mstr.ptnr_id  in (" & get_group_tax("001") & ") order by sod_tax_class, ar_code, soship_date "
                        'Else
                        '    sSql = sSql + " AND ptnr_mstr.ptnr_id not in (" & get_group_tax("001") & ") order by ar_code, soship_date "
                        'End If

                        'SEG & SDI
                        sSql = sSql + " order by ar_code, soship_date "


                        dtmp2 = GetTableData(sSql)


                        h = 0
                        h1 = 0

                        For Each Dr2 As DataRow In dtmp2.Rows



                            'If Dr2("ar_code") <> "" Then


                            If Dr2("sreturn") > 0 Then



                                ''cek tax class
                                If Dr2("sod_tax_class") = "9950" Then
                                    taxclas = "01"
                                    id_ket = ""
                                    vat = CStr(Decimal.Floor(Dr2("ppn_bayar") * 100) / 100)

                                    '    'ppn bayar
                                    sales_tot_det = CStr(Decimal.Floor(Dr2("price_fp") * 100) / 100)
                                    sales_dis_det = CStr(Decimal.Floor(Dr2("disc_fp") * 100) / 100)



                                    'l_dppt = Dr2("price_fp") + l_dppt
                                    'sls_ttl_h = sls_ttl_h + Dr2("price_fp")
                                    'ppn_ttl_h = ppn_ttl_h + Dr2("ppn_bayar")
                                ElseIf Dr2("sod_tax_class") = "9949" Then
                                    taxclas = "08"
                                    id_ket = "1"
                                    vat = CStr(Decimal.Floor(Dr2("ppn_bebas") * 100) / 100)


                                    '    'ppn bebas
                                    sales_tot_det = CStr(Decimal.Floor(Dr2("sales_ttl") * 100) / 100)

                                    sales_dis_det = CStr(Decimal.Floor(Dr2("disc_value") * 100) / 100)

                                    'l_ppnt = Dr2("ppn_bebas") + l_ppnt
                                    'l_dppt = Dr2("dpp") + l_dppt
                                    'sls_ttl_h = sls_ttl_h + Dr2("dpp")
                                    'ppn_ttl_h = ppn_ttl_h + Dr2("ppn_bebas")
                                End If


                                'SEG & SEA ar_date
                                'SDI soship_date
                                paytype_date = CDate(Dr2("soship_date"))
                                'paytype_date = CDate(Dr2("ar_date"))


                                If Dr2("ptnr_address_tax").ToString.Length = 0 Then
                                    addr = "-"
                                Else
                                    addr = Dr2("ptnr_address_tax").ToString
                                End If


                                'If Dr2("so_pay_type") <> "9941" Then
                                paytype_ref = Dr2("ar_code")

                                If ar_blm <> Dr2("ar_code").ToString Then

                                    'If (Dr2("sod_tax_class") = "9950") Then

                                    '    sSql = "SELECT public.f_cek_sum_ppn('" & Dr2("ar_code") & "'," & SetDate(Dr2("soship_date")) & ") as ppn_total"
                                    '    dtrppn = GetRowInfo(sSql)

                                    '            ppn_ttl_h = dtrppn("ppn_total")

                                    '            l_ppnt = dtrppn("ppn_total") + l_ppnt

                                    '    '        'ppn bayar

                                    'ElseIf (Dr2("sod_tax_class") = "9949") Then
                                    '            sSql = "SELECT public.f_cek_sum_ppn_bebas('" & Dr2("ar_code") & "'," & SetDate(Dr2("soship_date")) & ") as ppn_bebas_total"
                                    '    dtrppn_bbs = GetRowInfo(sSql)
                                    '    ppn_ttl_h = dtrppn_bbs("ppn_bebas_total")
                                    '            l_ppnt = dtrppn_bbs("ppn_bebas_total") + l_ppnt

                                    '    '        'ppn bebas


                                    'End If

                                    drarray = dtmp2.Select("ar_code='" & Dr2("ar_code").ToString & "'", "ar_code", DataViewRowState.CurrentRows)
                                    sls_ttl_h = 0
                                    ppn_ttl_h = 0
                                    For x As Integer = 0 To (drarray.Length - 1)
                                        sls_ttl_h += Floor(drarray(x)("dpp")) 'IIf(drarray(x)("dpp") Is System.DBNull.Value, 0, drarray(x)("dpp"))
                                        ppn_ttl_h += IIf(drarray(x)("sod_tax_class") = "9950", drarray(x)("ppn_bayar"), drarray(x)("ppn_bebas"))
                                    Next

                                    'sSql = "SELECT public.f_cek_sum_dpp ('" & Dr2("ar_code") & "'," & SetDate(Dr2("soship_date")) & ") as dpp_total"

                                    'dtr = GetRowInfo(sSql)
                                    'sls_ttl_h = dtr("dpp_total")
                                    l_dppt = sls_ttl_h + l_dppt
                                    l_ppnt = ppn_ttl_h + l_ppnt

                                    Dim NewRow As DataRow
                                    NewRow = ds.Tables("view1").NewRow

                                    If ce_gramed.Checked = True Then
                                        If Dr2("sod_tax_class") = "9950" Then
                                            If h = 0 Then

                                                drarray = dtmp2.Select("sod_tax_class=" & Dr2("sod_tax_class").ToString & " AND sreturn > 0", "sod_tax_class", DataViewRowState.CurrentRows)
                                                sls_ttl_h = 0
                                                ppn_ttl_h = 0
                                                For x As Integer = 0 To (drarray.Length - 1)
                                                    sls_ttl_h += Floor(drarray(x)("dpp")) 'IIf(drarray(x)("dpp") Is System.DBNull.Value, 0, drarray(x)("dpp"))
                                                    ppn_ttl_h += IIf(drarray(x)("sod_tax_class") = "9950", Floor(drarray(x)("ppn_bayar")), Floor(drarray(x)("ppn_bebas")))
                                                Next

                                                NewRow("tmptax_1") = "FK"
                                                NewRow("tmptax_2") = taxclas
                                                NewRow("tmptax_3") = "0"
                                                NewRow("tmptax_4") = ""
                                                NewRow("tmptax_5") = Format(paytype_date, "MM") * 1
                                                NewRow("tmptax_6") = Format(paytype_date, "yyyy")
                                                NewRow("tmptax_7") = Format(paytype_date, "dd/MM/yyyy")
                                                NewRow("tmptax_8") = Replace(Replace(Dr2("ptnr_npwp"), ".", ""), "-", "")
                                                NewRow("tmptax_9") = Dr2("ptnr_name")
                                                NewRow("tmptax_10") = addr
                                                NewRow("tmptax_11") = Decimal.Floor(sls_ttl_h)
                                                '& " + " & l_dppt
                                                NewRow("tmptax_12") = Decimal.Floor(ppn_ttl_h)
                                                '& " + " & l_ppnt
                                                NewRow("tmptax_13") = "0" '& " = " & l_dppt
                                                '"0"
                                                NewRow("tmptax_14") = id_ket '& " = " & l_ppnt 'id_ket
                                                NewRow("tmptax_15") = "0"
                                                NewRow("tmptax_16") = "0"
                                                NewRow("tmptax_17") = "0"
                                                NewRow("tmptax_18") = "0"
                                                NewRow("tmptax_19") = paytype_ref
                                                ds.Tables("view1").Rows.Add(NewRow)

                                                Dim NewRow3 As DataRow
                                                NewRow3 = ds.Tables("view1").NewRow

                                                NewRow3("tmptax_1") = "FAPR"
                                                'NewRow3("tmptax_2") = Dr2("cmaddr_npwp")
                                                NewRow3("tmptax_2") = Dr2("cmaddr_name")
                                                NewRow3("tmptax_3") = Dr2("cmaddr_tax_line_1") & " " & Dr2("cmaddr_tax_line_2")

                                                ds.Tables("view1").Rows.Add(NewRow3)

                                                'ppn_ttl_h = 0
                                                'sls_ttl_h = 0
                                                h = 1
                                                ar_blm = Dr2("ar_code")
                                            End If

                                        ElseIf Dr2("sod_tax_class") = "9949" Then
                                            If h1 = 0 Then

                                                drarray = dtmp2.Select("sod_tax_class=" & Dr2("sod_tax_class").ToString & " AND sreturn > 0 ", "sod_tax_class", DataViewRowState.CurrentRows)
                                                sls_ttl_h = 0
                                                ppn_ttl_h = 0
                                                For x As Integer = 0 To (drarray.Length - 1)
                                                    sls_ttl_h += Floor(drarray(x)("dpp")) 'IIf(drarray(x)("dpp") Is System.DBNull.Value, 0, drarray(x)("dpp"))
                                                    ppn_ttl_h += IIf(drarray(x)("sod_tax_class") = "9950", Floor(drarray(x)("ppn_bayar")), Floor(drarray(x)("ppn_bebas")))
                                                Next


                                                NewRow("tmptax_1") = "FK"
                                                NewRow("tmptax_2") = taxclas
                                                NewRow("tmptax_3") = "0"
                                                NewRow("tmptax_4") = ""
                                                NewRow("tmptax_5") = Format(paytype_date, "MM") * 1
                                                NewRow("tmptax_6") = Format(paytype_date, "yyyy")
                                                NewRow("tmptax_7") = Format(paytype_date, "dd/MM/yyyy")
                                                NewRow("tmptax_8") = Replace(Replace(Dr2("ptnr_npwp"), ".", ""), "-", "")
                                                NewRow("tmptax_9") = Dr2("ptnr_name")
                                                NewRow("tmptax_10") = addr
                                                NewRow("tmptax_11") = Decimal.Floor(sls_ttl_h)
                                                '& " + " & l_dppt
                                                NewRow("tmptax_12") = Decimal.Floor(ppn_ttl_h)
                                                '& " + " & l_ppnt
                                                NewRow("tmptax_13") = "0" '& " = " & l_dppt
                                                '"0"
                                                NewRow("tmptax_14") = id_ket '& " = " & l_ppnt 'id_ket
                                                NewRow("tmptax_15") = "0"
                                                NewRow("tmptax_16") = "0"
                                                NewRow("tmptax_17") = "0"
                                                NewRow("tmptax_18") = "0"
                                                NewRow("tmptax_19") = paytype_ref
                                                ds.Tables("view1").Rows.Add(NewRow)

                                                Dim NewRow3 As DataRow
                                                NewRow3 = ds.Tables("view1").NewRow

                                                NewRow3("tmptax_1") = "FAPR"
                                                'NewRow3("tmptax_2") = Dr2("cmaddr_npwp")
                                                NewRow3("tmptax_2") = Dr2("cmaddr_name")
                                                NewRow3("tmptax_3") = Dr2("cmaddr_tax_line_1") & " " & Dr2("cmaddr_tax_line_2")

                                                ds.Tables("view1").Rows.Add(NewRow3)

                                                'ppn_ttl_h = 0
                                                'sls_ttl_h = 0

                                                ar_blm = Dr2("ar_code")
                                                h1 = 1
                                            End If
                                        End If
                                    Else


                                        NewRow("tmptax_1") = "FK"
                                        NewRow("tmptax_2") = taxclas
                                        NewRow("tmptax_3") = "0"
                                        NewRow("tmptax_4") = ""
                                        NewRow("tmptax_5") = Format(paytype_date, "MM") * 1
                                        NewRow("tmptax_6") = Format(paytype_date, "yyyy")
                                        NewRow("tmptax_7") = Format(paytype_date, "dd/MM/yyyy")
                                        NewRow("tmptax_8") = Replace(Replace(Dr2("ptnr_npwp"), ".", ""), "-", "")
                                        NewRow("tmptax_9") = Dr2("ptnr_name")
                                        NewRow("tmptax_10") = addr
                                        NewRow("tmptax_11") = Decimal.Floor(sls_ttl_h)
                                        '& " + " & l_dppt
                                        NewRow("tmptax_12") = Decimal.Floor(ppn_ttl_h)
                                        '& " + " & l_ppnt
                                        NewRow("tmptax_13") = "0" '& " = " & l_dppt
                                        '"0"
                                        NewRow("tmptax_14") = id_ket '& " = " & l_ppnt 'id_ket
                                        NewRow("tmptax_15") = "0"
                                        NewRow("tmptax_16") = "0"
                                        NewRow("tmptax_17") = "0"
                                        NewRow("tmptax_18") = "0"
                                        NewRow("tmptax_19") = paytype_ref
                                        ds.Tables("view1").Rows.Add(NewRow)

                                        Dim NewRow3 As DataRow
                                        NewRow3 = ds.Tables("view1").NewRow

                                        NewRow3("tmptax_1") = "FAPR"
                                        'NewRow3("tmptax_2") = Dr2("cmaddr_npwp")
                                        NewRow3("tmptax_2") = Dr2("cmaddr_name")
                                        NewRow3("tmptax_3") = Dr2("cmaddr_tax_line_1") & " " & Dr2("cmaddr_tax_line_2")

                                        ds.Tables("view1").Rows.Add(NewRow3)

                                        'ppn_ttl_h = 0
                                        'sls_ttl_h = 0

                                        ar_blm = Dr2("ar_code")


                                    End If
                                End If

                                If CInt(Dr2("price_fp1")) > 0 Then


                                    Dim NewRow2 As DataRow
                                    NewRow2 = ds.Tables("view1").NewRow

                                    NewRow2("tmptax_1") = "OF" '& " > " & ar_blm & " <> " & Dr2("ar_code").ToString & " <> " & Dr2("so_code").ToString
                                    NewRow2("tmptax_2") = Dr2("pt_code")
                                    NewRow2("tmptax_3") = Dr2("pt_desc1")
                                    NewRow2("tmptax_4") = Replace(CStr(Decimal.Floor(Dr2("price_fp1") * 100) / 100), ",", ".")
                                    NewRow2("tmptax_5") = Dr2("soshipd_qty")
                                    NewRow2("tmptax_6") = Replace(sales_tot_det, ",", ".")
                                    NewRow2("tmptax_7") = Replace(sales_dis_det, ",", ".")
                                    NewRow2("tmptax_8") = Replace(CStr(Decimal.Floor(Dr2("dpp") * 100) / 100), ",", ".")
                                    NewRow2("tmptax_9") = Replace(vat, ",", ".")
                                    NewRow2("tmptax_10") = "0"
                                    NewRow2("tmptax_11") = "0.0"
                                    'NewRow2("tmptax_18") = Dr2("ar_code").ToString
                                    'NewRow2("tmptax_19") = Dr2("so_code").ToString


                                    ds.Tables("view1").Rows.Add(NewRow2)
                                End If

                                'so_blm = Dr2("so_code").ToString

                                'If (Dr2("so_pay_type") = "9941") Then
                                '    ar_blm = Dr2("so_code").ToString
                                'Else
                                '    ar_blm = Dr2("ar_code").ToString
                                'End If

                                ''ar_blm = Dr2("ar_code").ToString

                                'txc_blm = Dr2("sod_tax_class").ToString

                                l_dpp.Text = FormatNumber(l_dppt, 2)
                                l_ppn.Text = FormatNumber(l_ppnt, 2)
                                'End If

                            End If
                        Next


                    Catch ex As Exception
                        'MessageBox.Show("Message" & ex.ToString & "Failed to add new row to DataTable ")
                    End Try



                    gc_view1.DataSource = ds.Tables("view1")
                End If

                bestfit_column()
                'load_data_grid_detail()
                ConditionsAdjustment()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

End Class
