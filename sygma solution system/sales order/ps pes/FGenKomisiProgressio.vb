Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraEditors.Controls

Public Class FGenKomisiProgressio
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList

    Private Sub FRouting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal

    End Sub

    Public Overrides Sub load_cb()
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_en_mstr_mstr())
            segen_en_id.Properties.DataSource = dt_bantu
            segen_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            segen_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            segen_en_id.ItemIndex = 0

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_periode_mstr_se())
            With segen_periode
                If .Properties.Columns.VisibleCount = 0 Then
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_code", "Code", 20))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                    '.Properties.Columns.Add(New LookUpColumnInfo("seperiode_bonus_gen", "Generate Bonus", 20))
                    '.Properties.Columns.Add(New LookUpColumnInfo("seperiode_payment_date", "Payment Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_remarks", "Remarks", 20))
                End If

                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("seperiode_code").ToString
                .Properties.ValueMember = dt_bantu.Columns("seperiode_code").ToString
                If dt_bantu.Rows.Count > 0 Then
                    .EditValue = dt_bantu.Rows(0).Item(.Properties.ValueMember)
                    segen_start_date.EditValue = segen_periode.GetColumnValue("seperiode_start_date")
                    segen_end_date.EditValue = segen_periode.GetColumnValue("seperiode_end_date")
                    segen_remarks.EditValue = segen_periode.GetColumnValue("seperiode_remarks")
                End If

                .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                .Properties.BestFit()
                .Properties.DropDownRows = 12
                .Properties.PopupWidth = 600
                .ItemIndex = 0
            End With



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "All Child", "segen_all_child", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Code", "segen_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "segen_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "segen_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "seperiode_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End Date", "seperiode_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Commission", "segen_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bonus Recruiter", "segen_bonus_recruitment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "PPh21", "segen_pph21", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Commission Nett", "segen_commission_nett", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Total Commision", "segen_total_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "segen_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "segen_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "segen_add_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "segend_lvl_id", False)
        add_column_copy(gv_edit, "ID", "segend_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Name", "segend_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "NPWP", "segend_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "entity", "segend_entity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Start Periode", "segend_ptnr_start_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "ID Parent", "segend_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Name Parent", "segend_nama_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Gross Commission", "segend_komisi_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Recruiter Commission", "segend_komisi_recruiter", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Total Commission", "segend_komisi_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        'add_column_copy(gv_edit, "Point Total", "segend_poin_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "Multiplier Commission", "segend_poin_pengali", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "Point Recruiter", "segend_point_rekruter", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "Multiplier Recruiter", "segend_point_pengali_rekrut", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "50% Gross Commission", "segend_setengah_komisi_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "PTKP", "segend_ptkp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "PKP", "segend_pkp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit, "PPH21", "segend_pph_21", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_edit, "Nett Commission", "segend_komisi_netto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_edit_detail, "segendc_secommp_oid", "segendc_secommp_oid", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_edit_detail, "SO Number", "segendc_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Date", "segendc_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_edit_detail, "Start Date", "secommp_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_edit_detail, "End Date", "secommp_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_edit_detail, "ID", "segendc_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Customer Name", "segendc_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Sales ID", "segendc_ptnr_id_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Sales Name", "segendc_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Product", "segendc_produk", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Price", "segendc_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_detail, "Point", "segendc_point_id", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_detail, "Pen", "segendc_pen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_detail, "Payment", "segendc_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_detail, "Payment Seq", "segendc_payment_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_edit_detail, "Commission Base", "segendc_dasar_komisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_detail, "Commission Point", "segendc_point_cicilan_id", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit_detail, "Partner Periode", "segendc_ptnr_start_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "SO Periode", "segendc_so_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_detail, "Recruitment Point", "segendc_point_rekrutmen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_edit_calc, "ID", "segendp_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_edit_calc, "Periode", "segendp_secommp_oid", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_edit_calc, "Sales ID", "segendp_ptnr_id_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_calc, "Sales Name", "segendp_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_calc, "Start Date", "secommp_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_calc, "End Date", "secommp_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_calc, "Point", "segendp_point_id", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_calc, "Multiplier", "segendp_poin_pengali", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_calc, "Total", "segendp_komisi_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit_calc, "Parent Name", "segendp_nama_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_calc, "Point Recruitment", "segendp_point_rekrutmen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_calc, "Multiplier Recruitment", "segendp_poin_pengali_rekrutmen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_calc, "Total Recruitment", "segendp_komisi_rekrutmen_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column(gv_detail, "segend_lvl_id", False)
        add_column_copy(gv_detail, "ID", "segend_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Name", "segend_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "NPWP", "segend_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "entity", "segend_entity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Start Periode", "segend_ptnr_start_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "ID Parent", "segend_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Name Parent", "segend_nama_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Gross Commission", "segend_komisi_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Recruiter Commission", "segend_komisi_recruiter", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Total Commission", "segend_komisi_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column_copy(gv_detail_trx, "SO Number", "segendc_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Date", "segendc_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail_trx, "Start Date", "secommp_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail_trx, "End Date", "secommp_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail_trx, "ID", "segendc_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Customer Name", "segendc_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Sales ID", "segendc_ptnr_id_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Sales Name", "segendc_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Product", "segendc_produk", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Price", "segendc_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_trx, "Point", "segendc_point_id", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_trx, "Pen", "segendc_pen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_trx, "Payment", "segendc_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_trx, "Payment Seq", "segendc_payment_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_detail_trx, "Commission Base", "segendc_dasar_komisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_trx, "Commission Point", "segendc_point_cicilan_id", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_trx, "Partner Periode", "segendc_ptnr_start_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "SO Periode", "segendc_so_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_trx, "Recruitment Point", "segendc_point_rekrutmen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail_calc, "Sales ID", "segendp_ptnr_id_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_calc, "Sales Name", "segendp_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_calc, "Start Date", "secommp_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_calc, "End Date", "secommp_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_calc, "Point", "segendp_point_id", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_calc, "Multiplier", "segendp_poin_pengali", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_calc, "Total", "segendp_komisi_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_calc, "Parent Name", "segendp_nama_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_calc, "Point Recruitment", "segendp_point_rekrutmen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_calc, "Multiplier Recruitment", "segendp_poin_pengali_rekrutmen", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_calc, "Total Recruitment", "segendp_komisi_rekrutmen_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.segen_oid, " _
                & "  a.segen_dom_id, " _
                & "  a.segen_en_id, " _
                & "  b.en_desc, " _
                & "  a.segen_date, " _
                & "  a.segen_code, " _
                & "  a.segen_periode, " _
                & "  a.segen_total_income,segen_income,segen_add_income,segen_coaching_income, segen_all_child, " _
                & "  a.segen_remarks,segen_commision,segen_bonus_recruitment,segen_pph21,segen_commission_nett, " _
                & "  a.segen_add_by, " _
                & "  a.segen_add_date, " _
                & "  a.segen_total_su, " _
                & "  c.seperiode_start_date, " _
                & "  c.seperiode_end_date, " _
                & "  c.seperiode_bonus_gen " _
                & "FROM " _
                & "  public.segen_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.segen_en_id = b.en_id) " _
                & "  INNER JOIN public.seperiode_mstr c ON (a.segen_periode = c.seperiode_code) " _
                & "WHERE " _
                & "  a.segen_date BETWEEN " & SetDate(pr_txttglawal.Text) & " AND " & SetDate(pr_txttglakhir.Text) & "  " _
                  & " and segen_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "ORDER BY " _
                & "  a.segen_code"


        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
                & "  a.segend_oid, " _
                & "  a.segend_segen_oid, " _
                & "  a.segend_ptnr_id, " _
                & "  a.segend_level, " _
                & "  a.segend_nama, " _
                & "  a.segend_active_sales, " _
                & "  a.segend_su_sales, " _
                & "  a.segend_pengali_komisi, " _
                & "  a.segend_komisi, " _
                & "  a.segend_su_group, " _
                & "  a.segend_percent_add_income, " _
                & "  a.segend_pengali_add_income, " _
                & "  a.segend_add_income, " _
                & "  a.segend_percent_coaching_income, " _
                & "  a.segend_pengali_coaching_income, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_entity, " _
                & "  a.segend_lvl_id, " _
                & "  a.segend_parent,segend_komisi_telah_dibayar,segend_komisi_bulanan, " _
                & "  segend_poin_total, " _
                & "  segend_poin_pengali, " _
                & "  segend_komisi_bruto, " _
                & "  segend_pph_21, " _
                & "  segend_komisi_netto, " _
                & "  segend_npwp, " _
                & "  segend_setengah_komisi_bruto, " _
                & "  segend_ptkp, " _
                & "  segend_pkp, " _
                & "  segend_ptnr_code, " _
                & "  segend_point_rekruter, " _
                & "  segend_point_pengali_rekrut, " _
                & "  segend_komisi_recruiter_parent, " _
                & "  segend_komisi_recruiter, " _
                & "  segend_komisi_total, " _
                & "  segend_ptnr_start_periode, " _
                & "  segend_ptnr_id_parent, " _
                & "  segend_nama_parent, " _
                & "  segend_ptkp_status,segend_setengah_komisi_bruto,segend_ptnr_code " _
                & "FROM " _
                & "  public.segend_det a " _
                & "WHERE " _
                & "  a.segend_segen_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.segend_nama"


        load_data_detail(sql, gc_detail, "detail")
        gv_detail.BestFitColumns()

        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible



        Try
            ds.Tables("detail_trx").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
                & "  a.segendc_oid, " _
                & "  a.segendc_segend_oid, " _
                & "  a.segendc_ptnr_id, " _
                & "  a.segendc_ptnr_name, " _
                & "  a.segendc_sales, " _
                & "  a.segendc_price, " _
                & "  a.segendc_point_id, " _
                & "  a.segendc_point_db, " _
                & "  a.segendc_produk, " _
                & "  a.segendc_pen, " _
                & "  a.segendc_dasar_komisi, " _
                & "  a.segendc_sales_code, " _
                & "  a.segendc_ptnr_id_sales, " _
                & "  a.segendc_point_cicilan_id, " _
                & "  a.segendc_payment, " _
                & "  a.segendc_payment_seq,secommp_start_date,secommp_end_date, " _
                & "  a.segendc_segen_oid,segendc_date,segendc_secommp_oid,segendc_so_code,segendc_point_rekrutmen,segendc_ptnr_start_periode,segendc_so_periode " _
                & "FROM " _
                & "  public.segendc_customer a " _
                & "  left outer join secomm_periode on (a.segendc_secommp_oid=secommp_oid)   " _
                & "WHERE " _
                & "  a.segendc_segen_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.segendc_sales"

        load_data_detail(sql, gc_detail_trx, "detail_trx")
        gv_detail_trx.BestFitColumns()



        Try
            ds.Tables("detail_calc").Clear()
        Catch ex As Exception
        End Try
        sql = "SELECT  " _
                & "  a.segendp_oid, " _
                & "  a.segendp_segend_oid, " _
                & "  a.segendp_ptnr_id, " _
                & "  a.segendp_ptnr_name, " _
                & "  a.segendp_point_id, " _
                & "  a.segendp_poin_pengali, " _
                & "  a.segendp_komisi_bruto, " _
                & "  a.segendp_secommp_oid, " _
                & "  a.segendp_ptnr_id_sales, " _
                & "  a.segendp_sales_code, " _
                & "  a.segendp_sales, " _
                & "  a.segendp_point_rekrutmen, " _
                & "  a.segendp_poin_pengali_rekrutmen, " _
                & "  a.segendp_komisi_rekrutmen_bruto,secommp_start_date,secommp_end_date,  " _
                & "  a.segendp_nama_parent " _
                & "FROM " _
                & "  public.segendp_point a " _
                 & "  left outer join secomm_periode on (a.segendp_secommp_oid=secommp_oid)   " _
                & "WHERE " _
                & "  a.segendp_segen_oid  ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.segendp_ptnr_name"

        load_data_detail(sql, gc_detail_calc, "detail_calc")
        gv_detail_calc.BestFitColumns()

        Try
            'sql = "select ptnr_id,ptnr_parent,ptnr_name,lvl_name,psgend_sales_amount,psgend_sales_total from (select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
            '  & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
            '  & " where ptnr_id in " _
            '  & " ( select menu_id from get_all_child(" _
            '  & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("psgend_ptnr_id").ToString _
            '  & ")) or ptnr_id in (" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("psgend_ptnr_id").ToString & ")) as temp  " _
            '   & " left outer join public.psgend_det  ON (psgend_ptnr_id = ptnr_id) where psgend_psgen_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_oid").ToString & "'"

            'Dim dt_tree As New DataTable
            'dt_tree = master_new.PGSqlConn.GetTableData(sql)

            'TreeList1.DataSource = dt_tree
            'TreeList1.ExpandAll()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub relation_detail()
        Try
            'load_data_grid_detail()
            'gv_detail.Columns("rod_ro_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rod_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        segen_en_id.ItemIndex = 0
        segen_periode.ItemIndex = 0
        segen_en_id.Focus()
        segen_date.EditValue = master_new.PGSqlConn.CekTanggal
        segen_remarks.EditValue = ""

        Try
            XtraTabControl1.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "SELECT  " _
                        & "  a.segend_oid,segend_segen_oid, " _
                        & "  a.segend_ptnr_id, " _
                        & "  a.segend_nama,segend_en_id,segend_entity,segend_parent, " _
                        & "  a.segend_poin_total, " _
                        & "  a.segend_poin_pengali, " _
                        & "  a.segend_komisi_bruto,segend_ptnr_start_periode, " _
                        & "  a.segend_pph_21, " _
                        & "  a.segend_komisi_netto, " _
                        & "  a.segend_npwp, " _
                        & "  a.segend_setengah_komisi_bruto,segend_ptnr_id_parent,segend_nama_parent, segend_ptkp_status, " _
                        & "  a.segend_ptkp, " _
                        & "  a.segend_pkp,segend_ptnr_code,segend_point_rekruter,segend_point_pengali_rekrut,segend_komisi_recruiter_parent,segend_komisi_recruiter,segend_komisi_total " _
                        & "FROM " _
                        & "  public.segend_det a " _
                        & "WHERE " _
                        & "  a.segend_oid IS NULL"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")

                    .SQL = "SELECT  " _
                        & "  a.segendc_oid, " _
                        & "  a.segendc_segend_oid, " _
                        & "  a.segendc_ptnr_id, " _
                        & "  a.segendc_ptnr_name, " _
                        & "  a.segendc_sales_code,'' as segendp_nama_parent, " _
                        & "  a.segendc_sales, " _
                        & "  a.segendc_price, " _
                        & "  a.segendc_point_id, " _
                        & "  a.segendc_point_db, " _
                        & "  a.segendc_produk,segendc_so_code, " _
                        & "  a.segendc_pen,secommp_start_date, secommp_end_date,segendc_point_rekrutmen,segendc_ptnr_start_periode,segendc_so_periode, " _
                        & "  a.segendc_dasar_komisi,segendc_ptnr_id_sales,segendc_point_cicilan_id,segendc_payment,segendc_payment_seq,segendc_date,segendc_secommp_oid " _
                        & "FROM " _
                        & "  public.segendc_customer a " _
                        & " INNER JOIN public.secomm_periode b ON (b.secommp_oid = a.segendc_secommp_oid) " _
                        & "WHERE " _
                        & "  a.segendc_segend_oid IS NULL"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_detail")


                    .SQL = "SELECT  " _
                        & "  a.segendp_oid, " _
                        & "  a.segendp_segend_oid, " _
                        & "  a.segendp_ptnr_id, " _
                        & "  a.segendp_ptnr_name, " _
                        & "  a.segendp_point_id, " _
                        & "  a.segendp_poin_pengali, " _
                        & "  a.segendp_komisi_bruto, " _
                        & "  a.segendp_secommp_oid, " _
                        & "  a.segendp_ptnr_id_sales, " _
                        & "  a.segendp_sales_code, " _
                        & "  a.segendp_sales,segendp_nama_parent, " _
                        & "  b.secommp_start_date,segendp_point_rekrutmen,segendp_poin_pengali_rekrutmen,segendp_komisi_rekrutmen_bruto, " _
                        & "  b.secommp_end_date " _
                        & "FROM " _
                        & "  public.secomm_periode b " _
                        & "  INNER JOIN public.segendp_point a ON (b.secommp_oid = a.segendp_secommp_oid) " _
                        & "WHERE " _
                        & "  a.segendp_segend_oid is null " _
                        & "ORDER BY " _
                        & "  a.segendp_sales, " _
                        & "  a.segendp_ptnr_name"


                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_calc")

                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                    gc_edit_detail.DataSource = ds_edit.Tables(1)
                    gv_edit_detail.BestFitColumns()
                    gc_edit_calc.DataSource = ds_edit.Tables(2)
                    gv_edit_calc.BestFitColumns()

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function insert() As Boolean
        Dim i As Integer

        Dim _ro_oid As Guid
        _ro_oid = Guid.NewGuid

        sSQLs.Clear()

        Dim _code As String
        _code = func_coll.get_transaction_number("SE", segen_en_id.GetColumnValue("en_code"), "segen_mstr", "segen_code")
        Dim _segen_commision_bruto As Double
        Dim _segen_bonus_recruitment As Double = 0
        Dim _segen_pph21 As Double = 0
        Dim _segen_commission_nett As Double
        Dim _total As Double

        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
            _segen_commision_bruto += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_bruto"))
            _segen_bonus_recruitment += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_recruiter"))
            _segen_pph21 += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pph_21"))
            _segen_commission_nett += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_total"))
        Next
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                & "  public.segen_mstr " _
                                & "( " _
                                & "  segen_oid, " _
                                & "  segen_dom_id, " _
                                & "  segen_en_id, " _
                                & "  segen_date, " _
                                & "  segen_code, " _
                                & "  segen_periode, " _
                                & "   " _
                                & "  segen_remarks, " _
                                & "  segen_add_by, " _
                                & "  segen_add_date, " _
                                & "  segen_all_child, " _
                                & "  segen_commision, " _
                                & "  segen_bonus_recruitment,segen_pph21, " _
                                & "  segen_commission_nett " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_ro_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(segen_en_id.EditValue) & ",  " _
                                & SetDateNTime00(segen_date.EditValue) & ",  " _
                                & SetSetring(_code) & ",  " _
                                & SetSetring(segen_periode.EditValue) & ",  " _
                                & SetSetring(segen_remarks.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetBitYN(segen_all_child.EditValue) & ",  " _
                                & SetDec(_segen_commision_bruto) & ",  " _
                                & SetDec(_segen_bonus_recruitment) & ",  " _
                                & SetDec(_segen_pph21) & ",  " _
                                & SetDec(_segen_commission_nett) & "  " _
                                & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.segend_det " _
                                    & "( " _
                                    & "  segend_oid, " _
                                    & "  segend_segen_oid, " _
                                    & "  segend_ptnr_id, " _
                                    & "  segend_ptnr_id_parent, " _
                                    & "  segend_nama, " _
                                    & "  segend_nama_parent, " _
                                    & "  segend_poin_total, " _
                                    & "  segend_poin_pengali, " _
                                    & "  segend_komisi_bruto, " _
                                    & "  segend_ptnr_start_periode, " _
                                    & "  segend_pph_21, " _
                                    & "  segend_komisi_netto, " _
                                    & "  segend_npwp, " _
                                    & "  segend_setengah_komisi_bruto, " _
                                    & "  segend_ptkp, " _
                                    & "  segend_pkp, " _
                                    & "  segend_point_rekruter, " _
                                    & "  segend_point_pengali_rekrut,segend_komisi_recruiter,segend_komisi_total, " _
                                    & "  segend_entity, " _
                                    & "  segend_ptnr_code,segend_en_id, " _
                                    & "  segend_parent " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid.ToString) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_ptnr_id")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_ptnr_id_parent")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_nama")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_nama_parent")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_poin_total")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_poin_pengali")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_bruto")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_ptnr_start_periode")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pph_21")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_netto")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_npwp")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_setengah_komisi_bruto")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_ptkp")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pkp")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_point_rekruter")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_point_pengali_rekrut")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_recruiter")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_total")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_entity")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_ptnr_code")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_en_id")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_parent")) & "  " _
                                    & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        For i = 0 To ds_edit.Tables("insert_edit_detail").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.segendc_customer " _
                                    & "( " _
                                    & "  segendc_oid, " _
                                    & "  segendc_segend_oid,segendc_segen_oid, " _
                                    & "  segendc_ptnr_id, " _
                                    & "  segendc_ptnr_name, " _
                                    & "  segendc_sales, " _
                                    & "  segendc_price, " _
                                    & "  segendc_point_id, " _
                                    & "  segendc_point_db, " _
                                    & "  segendc_produk, " _
                                    & "  segendc_pen, " _
                                    & "  segendc_dasar_komisi, " _
                                    & "  segendc_sales_code, " _
                                    & "  segendc_ptnr_id_sales, " _
                                    & "  segendc_point_cicilan_id, " _
                                    & "  segendc_payment,segendc_date,segendc_secommp_oid,segendc_so_code,segendc_point_rekrutmen,segendc_ptnr_start_periode,segendc_so_periode, " _
                                    & "  segendc_payment_seq " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_oid")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_segend_oid")) & ",  " _
                                    & SetSetring(_ro_oid.ToString) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_ptnr_id")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_ptnr_name")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_sales")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_price")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_point_id")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_point_db")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_produk")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_pen")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_dasar_komisi")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_sales_code")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_ptnr_id_sales")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_point_cicilan_id")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_payment")) & ",  " _
                                    & SetDate(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_date")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_secommp_oid")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_so_code")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_point_rekrutmen")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_ptnr_start_periode")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_so_periode")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("segendc_payment_seq")) & "  " _
                                    & ")"


                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_calc").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.segendp_point " _
                                            & "( " _
                                            & "  segendp_oid, " _
                                            & "  segendp_segen_oid, " _
                                            & "  segendp_ptnr_id, " _
                                            & "  segendp_ptnr_name, " _
                                            & "  segendp_point_id, " _
                                            & "  segendp_poin_pengali, " _
                                            & "  segendp_komisi_bruto, " _
                                            & "  segendp_secommp_oid, " _
                                            & "  segendp_ptnr_id_sales, " _
                                            & "  segendp_sales_code, " _
                                            & "  segendp_sales, " _
                                            & "  segendp_point_rekrutmen, " _
                                            & "  segendp_poin_pengali_rekrutmen, " _
                                            & "  segendp_komisi_rekrutmen_bruto, " _
                                            & "  segendp_nama_parent " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid.ToString) & ",  " _
                                            & SetInteger(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_ptnr_id")) & ",  " _
                                            & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_ptnr_name")) & ",  " _
                                            & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_point_id")) & ",  " _
                                            & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_poin_pengali")) & ",  " _
                                            & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_komisi_bruto")) & ",  " _
                                            & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_secommp_oid")) & ",  " _
                                            & SetInteger(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_ptnr_id_sales")) & ",  " _
                                            & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_sales_code")) & ",  " _
                                            & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_sales")) & ",  " _
                                            & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_point_rekrutmen")) & ",  " _
                                            & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_poin_pengali_rekrutmen")) & ",  " _
                                            & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_komisi_rekrutmen_bruto")) & ",  " _
                                            & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("segendp_nama_parent")) & "  " _
                                            & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next
                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_ro_oid.ToString), "segen_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        'If SetString(genpr_year.EditValue) = "" Then
        '    Box("Year can't empt")
        '    Return False
        '    Exit Function
        'End If
        '*********************
        'Cek UM
        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_wc_id")) = True Then
        '        MessageBox.Show("Workstation Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next
        '*********************

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_tool_code")) = True Then
        '        MessageBox.Show("Tool Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_ptnr_id")) = True Then
        '        MessageBox.Show("Partner Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_milestone")) = True Then
        '        MessageBox.Show("Milestone Can't Empty.. Fill with (Y/N)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False

    End Function

    Public Overrides Function edit()
        Dim i As Integer

        edit = True
        sSQLs.Clear()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from fcsd_det where fcsd_fcs_oid = '" + _ro_oid_mstr + "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.fcsd_det " _
                                            & "( " _
                                            & "  fcsd_oid, " _
                                            & "  fcsd_fcs_oid, " _
                                            & "  fcsd_pt_id, " _
                                            & "  fcsd_01_amount, " _
                                            & "  fcsd_02_amount, " _
                                            & "  fcsd_03_amount, " _
                                            & "  fcsd_total_amount, " _
                                            & "  fcsd_buffer_amount, " _
                                            & "  fcsd_seq " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid_mstr.ToString) & ",  " _
                                            & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_pt_id")) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_01_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_02_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_03_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_total_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_buffer_amount"))) & ",  " _
                                            & SetInteger(i) & "  " _
                                            & ")"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_ro_oid_mstr.ToString), "fcs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If
            sSQLs.Clear()

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from segen_mstr where segen_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If
                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged

        Try
            'If e.Column.Name = "fcsd_01_amount" Or e.Column.Name = "fcsd_02_amount" Or e.Column.Name = "fcsd_03_amount" Then
            '    Dim _buffer_persen As Double = 0
            '    Dim _buffer As Double = 0
            '    If gv_edit.GetRowCellValue(e.RowHandle, "gr_code") = "TOP10" Then
            '        _buffer_persen = 0.5
            '    Else
            '        _buffer_persen = 0.2
            '    End If
            '    Dim _total As Double
            '    _total = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_01_amount")) + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_02_amount")) _
            '    + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_03_amount"))

            '    _buffer = _total / 3 * _buffer_persen
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_total_amount", _total)
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_buffer_amount", _buffer)

            'End If
            'gv_edit.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _rod_en_id As Integer = segen_en_id.EditValue

        'If _col = "wc_desc" Then
        '    Dim frm As New FWCSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "code_name" Then
        '    Dim frm As New FToolSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "ptnr_name" Then
        '    Dim frm As New FPartnerSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'Dim _now As DateTime
        '_now = func_coll.get_now
        'With gv_edit
        '    .SetRowCellValue(e.RowHandle, "rod_op", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_start_date", _now)
        '    .SetRowCellValue(e.RowHandle, "rod_end_date", _now)
        '    .SetRowCellValue(e.RowHandle, "rod_mch_op", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_tran_qty", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_queue", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_wait", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_move", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_run", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_setup", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_yield_pct", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_milestone", "N")
        '    .SetRowCellValue(e.RowHandle, "rod_sub_lead", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_setup_men", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_men_mch", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_sub_cost", 0)
        '    .BestFitColumns()
        'End With
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try

            Dim _en_id_all As String

            If segen_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(segen_en_id.EditValue)
            Else
                _en_id_all = segen_en_id.EditValue
            End If

            Dim tgl_awal, tgl_akhir As Date
            tgl_awal = segen_start_date.EditValue
            tgl_akhir = segen_end_date.EditValue

            Dim _pen_pt_code = func_coll.get_conf_file("pen_pt_code")
            Dim sSQL As String

            sSQL = "SELECT  " _
                & "  a.ptnr_id,a.ptnr_en_id,a.ptnr_start_periode, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name, " _
                & "  c.en_desc, " _
                & "  b.lvl_code, " _
                & "  a.ptnr_parent , d.ptnr_name as ptnr_name_parent,a.ptnr_lvl_id, a.ptnr_npwp  " _
                & "FROM " _
                & "  public.ptnr_mstr a " _
                & "  left outer JOIN public.pslvl_mstr b ON (a.ptnr_lvl_id = b.lvl_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id) " _
                & "  left outer JOIN public.ptnr_mstr d ON (a.ptnr_parent = d.ptnr_id) " _
                & "WHERE " _
                & "  a.ptnr_is_ps = 'Y' AND a.ptnr_en_id in (" & _en_id_all _
                & ") AND a.ptnr_active = 'Y'"


            Dim dt_fs As New DataTable
            dt_fs = GetTableData(sSQL)
            Dim dr_data As DataRow
            ds_edit.Tables(0).Rows.Clear()
            ds_edit.Tables(1).Rows.Clear()
            ds_edit.Tables(2).Rows.Clear()
            Dim dt_komisi As New DataTable
            Dim dt_comm_mstr As New DataTable
            Dim dt_add As New DataTable
            Dim dt_coach As New DataTable
            Dim dt_bayar As New DataTable
            Dim dt_periode As New DataTable

            sSQL = "select * from secomm_mstr where  secomm_type='C' order by secomm_min "
            dt_comm_mstr = GetTableData(sSQL)

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                _row("segend_oid") = Guid.NewGuid.ToString
                _row("segend_ptnr_id") = dr("ptnr_id")
                _row("segend_nama") = dr("ptnr_name")
                _row("segend_npwp") = dr("ptnr_npwp")
                '_row("segend_level") = dr("lvl_code")
                _row("segend_entity") = dr("en_desc")
                _row("segend_parent") = dr("ptnr_parent")
                _row("segend_nama_parent") = dr("ptnr_name_parent")
                _row("segend_en_id") = dr("ptnr_en_id")
                _row("segend_poin_total") = 0.0
                _row("segend_ptnr_start_periode") = dr("ptnr_start_periode")

                sSQL = "SELECT  " _
                    & "  b.arpayd_amount, " _
                    & "  c.sokp_seq,so_code,f.ptnr_start_periode as segendc_ptnr_start_periode, " _
                    & "  d.so_oid,so_date,(SELECT   max(x.seperiode_code) as so_periode FROM  public.seperiode_mstr x WHERE  x.seperiode_start_date <= so_date AND   x.seperiode_end_date >= so_date) as segendc_so_periode, " _
                    & "  e.ptnr_id as segendc_ptnr_id, " _
                    & "  e.ptnr_name as segendc_ptnr_name, " _
                    & "  f.ptnr_id as segendc_ptnr_id_sales, " _
                    & "  f.ptnr_code as segendc_sales_code, " _
                    & "  f.ptnr_name as segendc_sales, " _
                    & "  d.so_total as segendc_price, " _
                    & "  (select sum (sod_price * sod_qty) as jml from sod_det where sod_pt_id in (select pt_id from pt_mstr where pt_code='" & _pen_pt_code & "')  " _
                    & "  and sod_so_oid=d.so_oid) as segendc_pen, " _
                    & "(select sum(sod_sales_unit * sod_qty) as jml from sod_det where   " _
                    & "   sod_so_oid=d.so_oid) as poin,g.ptnr_name as segendp_nama_parent, " _
                    & "  array_to_string(array(select pt_desc1 from sod_det inner join  pt_mstr on (sod_pt_id=pt_id) where  sod_so_oid=d.so_oid), ';') as segendc_produk " _
                    & "FROM " _
                    & "  public.arpay_payment a " _
                    & "  INNER JOIN public.arpayd_det b ON (a.arpay_oid = b.arpayd_arpay_oid) " _
                    & "  INNER JOIN public.sokp_piutang c ON (b.arpayd_sokp_oid = c.sokp_oid) " _
                    & "  INNER JOIN public.so_mstr d ON (c.sokp_so_oid = d.so_oid) " _
                    & "  INNER JOIN public.ptnr_mstr e ON (d.so_ptnr_id_sold = e.ptnr_id) " _
                    & "  INNER JOIN public.ptnr_mstr f ON (d.so_sales_person = f.ptnr_id) " _
                    & "  left outer JOIN public.ptnr_mstr g ON (f.ptnr_parent = g.ptnr_id) " _
                    & "WHERE " _
                    & "  a.arpay_eff_date BETWEEN " & SetDate(tgl_awal) & " AND " & SetDate(tgl_akhir) & " AND  " _
                    & "  d.so_sales_person =" & SetInteger(dr("ptnr_id")) & " order by so_date"

                dt_komisi = GetTableData(sSQL)

                For Each dr_komisi As DataRow In dt_komisi.Rows
                    Dim _row2 As DataRow
                    _row2 = ds_edit.Tables(1).NewRow

                    _row2("segendc_oid") = Guid.NewGuid.ToString
                    _row2("segendc_date") = dr_komisi("so_date")


                    sSQL = "SELECT  " _
                      & "  b.secommp_oid, " _
                      & "  b.secommp_start_date, " _
                      & "  b.secommp_end_date " _
                      & "FROM " _
                      & "  public.secomm_periode b " _
                      & "WHERE " _
                      & "  b.secommp_start_date <= " & SetDate(_row2("segendc_date")) & " AND  " _
                      & "  b.secommp_end_date >= " & SetDate(_row2("segendc_date"))


                    dt_periode = GetTableData(sSQL)
                    For Each dr_periode As DataRow In dt_periode.Rows
                        _row2("segendc_secommp_oid") = dr_periode(0).ToString
                        _row2("secommp_start_date") = dr_periode(1)
                        _row2("secommp_end_date") = dr_periode(2)

                    Next

                    _row2("segendc_so_code") = dr_komisi("so_code")
                    _row2("segendc_segend_oid") = _row("segend_oid").ToString
                    _row2("segendc_ptnr_id") = dr_komisi("segendc_ptnr_id")
                    _row2("segendc_ptnr_name") = dr_komisi("segendc_ptnr_name")
                    _row2("segendc_ptnr_start_periode") = dr_komisi("segendc_ptnr_start_periode")
                    _row2("segendc_so_periode") = dr_komisi("segendc_so_periode")
                    _row2("segendc_ptnr_id_sales") = dr_komisi("segendc_ptnr_id_sales")
                    _row2("segendc_sales_code") = dr_komisi("segendc_sales_code")
                    _row2("segendc_sales") = dr_komisi("segendc_sales")
                    _row2("segendc_price") = dr_komisi("segendc_price")
                    _row2("segendc_point_id") = dr_komisi("poin")
                    '_row2("segendc_point_db") = dr_komisi("poin_db")
                    _row2("segendc_pen") = dr_komisi("segendc_pen")
                    _row2("segendc_produk") = dr_komisi("segendc_produk")
                    _row2("segendc_payment") = dr_komisi("arpayd_amount")
                    _row2("segendc_payment_seq") = dr_komisi("sokp_seq")
                    _row2("segendp_nama_parent") = dr_komisi("segendp_nama_parent")

                    If dr_komisi("sokp_seq") = 0 Then
                        _row2("segendc_dasar_komisi") = SetNumber(dr_komisi("arpayd_amount")) - SetNumber(dr_komisi("segendc_pen"))
                    Else
                        _row2("segendc_dasar_komisi") = SetNumber(dr_komisi("arpayd_amount"))
                    End If

                    _row2("segendc_point_cicilan_id") = SetNumber(_row2("segendc_dasar_komisi")) / (SetNumber(_row2("segendc_price")) - SetNumber(_row2("segendc_pen"))) * SetNumber(_row2("segendc_point_id"))

                    _row("segend_poin_total") += _row2("segendc_point_cicilan_id")

                    If SetString(dr_komisi("segendc_ptnr_start_periode")) >= "201602" Then
                        If cek_periode(SetString(dr_komisi("segendc_ptnr_start_periode"))) >= SetString(dr_komisi("segendc_so_periode")) Then
                            _row2("segendc_point_rekrutmen") = SetNumber(_row2("segendc_point_cicilan_id"))
                        End If

                    End If
                  
                    ds_edit.Tables(1).Rows.Add(_row2)
                    ds_edit.AcceptChanges()

                Next


                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
            gv_edit_detail.BestFitColumns()

            Dim view As DataView
            view = ds_edit.Tables(1).DefaultView
            view.Sort = "segendc_ptnr_id_sales ASC, segendc_secommp_oid ASC"

            Dim _segendp_ptnr_id_sales As String = ""
            Dim _segendc_secommp_oid As String = ""
            Dim _baris As Integer = -1
            Dim _ada As Boolean = False
            For Each rowView As DataRowView In view
                Dim row As DataRow = rowView.Row

             
                If _segendp_ptnr_id_sales <> row("segendc_ptnr_id_sales").ToString Or _segendc_secommp_oid <> row("segendc_secommp_oid").ToString Then

                    Dim _row2 As DataRow
                    _row2 = ds_edit.Tables(2).NewRow


                    _row2("segendp_secommp_oid") = row("segendc_secommp_oid")
                    _row2("secommp_start_date") = row("secommp_start_date")
                    _row2("secommp_end_date") = row("secommp_end_date")
                    _row2("segendp_ptnr_id_sales") = row("segendc_ptnr_id_sales")
                    _row2("segendp_sales_code") = row("segendc_sales_code")
                    _row2("segendp_sales") = row("segendc_sales")
                    _row2("segendp_point_id") = row("segendc_point_cicilan_id")
                    _row2("segendp_point_rekrutmen") = row("segendc_point_rekrutmen")
                    _row2("segendp_nama_parent") = row("segendp_nama_parent")

                    _segendp_ptnr_id_sales = row("segendc_ptnr_id_sales").ToString
                    _segendc_secommp_oid = row("segendc_secommp_oid").ToString

                    ds_edit.Tables(2).Rows.Add(_row2)
                    ds_edit.AcceptChanges()

                    _ada = True

                Else
                    ds_edit.Tables(2).Rows(_baris).Item("segendp_point_id") = SetNumber(ds_edit.Tables(2).Rows(_baris).Item("segendp_point_id")) + SetNumber(row("segendc_point_cicilan_id"))

                    ds_edit.Tables(2).Rows(_baris).Item("segendp_point_rekrutmen") = SetNumber(ds_edit.Tables(2).Rows(_baris).Item("segendp_point_rekrutmen")) + SetNumber(row("segendc_point_rekrutmen"))

                    ds_edit.AcceptChanges()
                End If
                If _ada = True Then
                    _baris += 1
                    _ada = False
                End If

            Next


          
            For Each dr As DataRow In ds_edit.Tables(2).Rows
                sSQL = "SELECT  " _
                   & "  a.secomm_oid, " _
                   & "  a.secomm_min, " _
                   & "  a.secomm_max, " _
                   & "  a.secomm_multiplier, " _
                   & "  a.secomm_type, " _
                   & "  a.secomm_secommp_oid " _
                   & "FROM " _
                   & "  public.secomm_mstr a " _
                   & "WHERE " _
                   & "  a.secomm_secommp_oid = '" & dr("segendp_secommp_oid").ToString & "' and  secomm_type='C' " _
                   & "ORDER BY " _
                   & "  a.secomm_min"

                dt_comm_mstr = GetTableData(sSQL)

                For Each dr_comm As DataRow In dt_comm_mstr.Rows
                    If SetNumber(dr("segendp_point_id")) >= SetNumber(dr_comm("secomm_min")) Then
                        If SetNumber(dr("segendp_point_id")) <= SetNumber(dr_comm("secomm_max")) Then
                            dr("segendp_poin_pengali") = dr_comm("secomm_multiplier")

                            Exit For
                        End If
                    End If
                Next

                sSQL = "SELECT  " _
                 & "  a.secomm_oid, " _
                 & "  a.secomm_min, " _
                 & "  a.secomm_max, " _
                 & "  a.secomm_multiplier, " _
                 & "  a.secomm_type, " _
                 & "  a.secomm_secommp_oid " _
                 & "FROM " _
                 & "  public.secomm_mstr a " _
                 & "WHERE " _
                 & "  a.secomm_secommp_oid = '" & dr("segendp_secommp_oid").ToString & "' and  secomm_type='R' " _
                 & "ORDER BY " _
                 & "  a.secomm_min"

                dt_comm_mstr = GetTableData(sSQL)

                For Each dr_comm As DataRow In dt_comm_mstr.Rows
                    If SetNumber(dr("segendp_point_rekrutmen")) >= SetNumber(dr_comm("secomm_min")) Then
                        If SetNumber(dr("segendp_point_rekrutmen")) <= SetNumber(dr_comm("secomm_max")) Then
                            dr("segendp_poin_pengali_rekrutmen") = dr_comm("secomm_multiplier")
                            Exit For
                        End If
                    End If
                Next


                dr("segendp_komisi_bruto") = SetNumber(dr("segendp_point_id")) * SetNumber(dr("segendp_poin_pengali"))
                dr("segendp_komisi_rekrutmen_bruto") = SetNumber(dr("segendp_point_rekrutmen")) * SetNumber(dr("segendp_poin_pengali_rekrutmen"))

               

            Next
            ds_edit.AcceptChanges()
            Dim dt_recruiter As New DataTable
            For Each dr As DataRow In ds_edit.Tables(0).Rows
                For Each dr_calc As DataRow In ds_edit.Tables(2).Rows
                    If dr("segend_ptnr_id") = dr_calc("segendp_ptnr_id_sales") Then
                        dr("segend_komisi_bruto") = SetNumber(dr("segend_komisi_bruto")) + SetNumber(dr_calc("segendp_komisi_bruto"))
                        'dr("segend_komisi_recruiter") = SetNumber(dr("segend_komisi_recruiter")) + SetNumber(dr_calc("segendp_komisi_rekrutmen_bruto"))
                    End If
                Next


                sSQL = "SELECT  " _
                        & "  a.ptnr_id,ptnr_start_periode " _
                        & "FROM " _
                        & "  public.ptnr_mstr a " _
                        & "WHERE " _
                        & "  a.ptnr_parent = " & SetInteger(dr("segend_ptnr_id")) & " AND  " _
                        & "  a.ptnr_is_ps = 'Y'  "

                dt_recruiter = GetTableData(sSQL)
                dr("segend_point_rekruter") = 0.0
                For Each dr_r As DataRow In dt_recruiter.Rows
                    'If segen_periode.EditValue <= cek_periode(dr_r("ptnr_start_periode")) Then

                    For Each dr_2 As DataRow In ds_edit.Tables(2).Rows
                        If dr_2("segendp_ptnr_id_sales") = dr_r("ptnr_id") Then
                            dr("segend_komisi_recruiter") = SetNumber(dr("segend_komisi_recruiter")) + SetNumber(dr_2("segendp_komisi_rekrutmen_bruto"))
                        End If
                    Next

                    'End If
                Next

                'dr("segend_komisi_recruiter") = 0.0 'SetNumber(dr("segend_point_pengali_rekrut")) * SetNumber(dr("segend_point_rekruter"))
                dr("segend_komisi_total") = SetNumber(dr("segend_komisi_recruiter")) + SetNumber(dr("segend_komisi_bruto"))

                dr("segend_setengah_komisi_bruto") = 0.5 * SetNumber(dr("segend_komisi_total"))

                If segen_periode.EditValue > SetString(dr("segend_ptnr_start_periode")) Then
                    If SetString(dr("segend_npwp")) = "" Then
                        dr("segend_ptkp") = CDbl(0)
                    Else
                        If SetString(dr("segend_ptnr_start_periode")) <> "" Then
                            dr("segend_ptkp") = CDbl(3000000)
                        Else
                            dr("segend_ptkp") = CDbl(0)
                        End If

                    End If

                Else
                    dr("segend_ptkp") = CDbl(0)
                End If

                dr("segend_pkp") = SetNumber(dr("segend_setengah_komisi_bruto")) - SetNumber(dr("segend_ptkp"))

                If dr("segend_pkp") < 0 Then
                    dr("segend_pkp") = 0
                End If
                If SetString(dr("segend_npwp")) = "" Then
                    dr("segend_pph_21") = 0.06 * SetNumber(dr("segend_pkp"))
                Else
                    dr("segend_pph_21") = 0.05 * SetNumber(dr("segend_pkp"))
                End If

                dr("segend_komisi_netto") = SetNumber(dr("segend_komisi_total")) - SetNumber(dr("segend_pph_21"))

            Next
            ds_edit.AcceptChanges()

            'Dim dt_recruiter As New DataTable

            'sSQL = "select * from secomm_mstr where  secomm_type='R' order by secomm_min "



            '    For Each dr_comm As DataRow In dt_comm_mstr.Rows
            '        If SetNumber(dr("segend_point_rekruter")) >= SetNumber(dr_comm("secomm_min")) Then
            '            If SetNumber(dr("segend_point_rekruter")) <= SetNumber(dr_comm("secomm_max")) Then
            '                dr("segend_point_pengali_rekrut") = dr_comm("secomm_multiplier")
            '                Exit For
            '            End If
            '        End If
            '    Next

            '    dr("segend_komisi_recruiter") = SetNumber(dr("segend_point_pengali_rekrut")) * SetNumber(dr("segend_point_rekruter"))
            '    dr("segend_komisi_total") = SetNumber(dr("segend_komisi_recruiter")) + SetNumber(dr("segend_komisi_bruto"))

            '    dr("segend_setengah_komisi_bruto") = 0.5 * SetNumber(dr("segend_komisi_total"))

            '    If segen_periode.EditValue > SetString(dr("segend_ptnr_start_periode")) Then
            '        If SetString(dr("segend_npwp")) = "" Then
            '            dr("segend_ptkp") = CDbl(0)
            '        Else
            '            If SetString(dr("segend_ptnr_start_periode")) <> "" Then
            '                dr("segend_ptkp") = CDbl(3000000)
            '            Else
            '                dr("segend_ptkp") = CDbl(0)
            '            End If

            '        End If

            '    Else
            '        dr("segend_ptkp") = CDbl(0)
            '    End If

            '    dr("segend_pkp") = SetNumber(dr("segend_setengah_komisi_bruto")) - SetNumber(dr("segend_ptkp"))

            '    If dr("segend_pkp") < 0 Then
            '        dr("segend_pkp") = 0
            '    End If
            '    If SetString(dr("segend_npwp")) = "" Then
            '        dr("segend_pph_21") = 0.06 * SetNumber(dr("segend_pkp"))
            '    Else
            '        dr("segend_pph_21") = 0.05 * SetNumber(dr("segend_pkp"))
            '    End If

            '    dr("segend_komisi_netto") = SetNumber(dr("segend_komisi_total")) - SetNumber(dr("segend_pph_21"))
            'Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
            gv_edit_calc.BestFitColumns()
            Box("Generate Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function cek_periode(ByVal pr_periode As String) As String
        Try
            Dim ssql As String
            Dim hasil As String = ""
            ssql = "select  seperiode_code  from seperiode_mstr where seperiode_code>=" & SetSetring(pr_periode) & " order by seperiode_code limit 3 "

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                hasil = dr(0)
            Next
            Return hasil
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub psgen_periode_code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles segen_periode.EditValueChanged
        Try
            segen_start_date.EditValue = segen_periode.GetColumnValue("seperiode_start_date")
            segen_end_date.EditValue = segen_periode.GetColumnValue("seperiode_end_date")
            segen_remarks.EditValue = segen_periode.GetColumnValue("seperiode_remarks")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_detail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_detail.Click
        Try
            gv_detail_SelectionChanged(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gv_detail_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_detail.SelectionChanged
        Try
            If ds.Tables("detail").Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String
            sql = "select ptnr_id,ptnr_parent,ptnr_name,  segend_poin_total, segend_poin_pengali,  segend_komisi_bruto,  segend_komisi_recruiter,    segend_komisi_total,  segend_pph_21,  segend_komisi_netto  from (select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
              & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
              & " where ptnr_id in " _
              & " ( select menu_id from get_all_child(" _
              & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("segend_ptnr_id").ToString _
              & ")) or ptnr_id in (" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("segend_ptnr_id").ToString & ")) as temp  " _
               & " left outer join public.segend_det  ON (segend_ptnr_id = ptnr_id) where segend_segen_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "'"

            Dim dt_tree As New DataTable
            dt_tree = master_new.PGSqlConn.GetTableData(sql)

            Try
                TreeList1.DataSource = dt_tree
                TreeList1.ExpandAll()
            Catch ex As Exception
            End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Try
            Dim _file As String = ""
            _file = AskSaveAsFile("Xls Files | *.xls")
            If _file = "" Then
                Box("Please select file name to export")
                Exit Sub
            End If

            TreeList1.ExportToXls(_file)
            OpenFile(_file)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub preview()

        Dim _sql As String

        _sql = "SELECT  " _
             & "  a.segend_oid, " _
             & "  a.segend_segen_oid, " _
             & "  a.segend_ptnr_id, " _
             & "  a.segend_level, " _
             & "  a.segend_nama, " _
             & "  a.segend_active_sales, " _
             & "  a.segend_su_sales, " _
             & "  a.segend_pengali_komisi, " _
             & "  a.segend_komisi, " _
             & "  a.segend_su_group, " _
             & "  a.segend_percent_add_income, " _
             & "  a.segend_pengali_add_income, " _
             & "  a.segend_add_income, " _
             & "  a.segend_percent_coaching_income, " _
             & "  a.segend_pengali_coaching_income, " _
             & "  a.segend_coaching_income, " _
             & "  a.segend_total_income, " _
             & "  a.segend_payment_date, " _
             & "  a.segend_entity, " _
             & "  a.segend_lvl_id, " _
             & "  a.segend_parent,segend_komisi_telah_dibayar,segend_komisi_bulanan, " _
             & "  segend_poin_total, " _
             & "  segend_poin_pengali, " _
             & "  segend_komisi_bruto, " _
             & "  segend_pph_21, " _
             & "  segend_komisi_netto, " _
             & "  segend_npwp, " _
             & "  segend_setengah_komisi_bruto, " _
             & "  segend_ptkp, " _
             & "  segend_pkp, " _
             & "  segend_ptnr_code, " _
             & "  segend_point_rekruter, " _
             & "  segend_point_pengali_rekrut, " _
             & "  segend_komisi_recruiter_parent, " _
             & "  segend_komisi_recruiter, " _
             & "  segend_komisi_total, " _
             & "  segend_ptnr_start_periode, " _
             & "  segend_ptnr_id_parent, " _
             & "  segend_nama_parent, " _
             & "  segend_ptkp_status " _
             & "FROM " _
             & "  public.segend_det a " _
             & "WHERE " _
             & "  a.segend_segen_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "' " _
             & "ORDER BY " _
             & "  a.segend_nama"

      

        Dim rpt As New XRSEProgressioReport
        With rpt
            Dim ds As New DataSet
            ds = master_new.PGSqlConn.ReportDataset(_sql)
            If ds.Tables(0).Rows.Count < 1 Then
                Box("Maaf data kosong")
                Exit Sub
            End If

            'ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

            'Dim dt As New DataTable
            'dt = GetTableData(ssql)

            'For Each dr As DataRow In dt.Rows
            '    .XrLabelTitle.Text = dr("dom_company")
            'Next

            'If Ce_Posting.EditValue = True Then
            '    .Posting_Option = True
            'Else
            '    .Posting_Option = False
            'End If

            '.periode = de_end.DateTime.ToString("dd MMMM yyyy")
            '.TreeList1.ExpandAll()
            .TreeList1.DataSource = ds.Tables(0)
            '.TreeList1.ExpandAll()

            '''''''.XrLabelPeriode.Text = "PERIODE : " & Format(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_start_date"), "dd/MM/yyyy") & " - " & Format(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_end_date"), "dd/MM/yyyy")

            '.DataSource = ds
            '.DataMember = "Table"
            '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
            '.Parameters("PPosisi").Value = posisi

             Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            printTool.PreviewForm.Text = "Personal Selling Progressio Report"
            '.PrintingSystem = ps
            printTool.ShowPreview()
            .TreeList1.ExpandAll()

        End With
    End Sub
End Class
