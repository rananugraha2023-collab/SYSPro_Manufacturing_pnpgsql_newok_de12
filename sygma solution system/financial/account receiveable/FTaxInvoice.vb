Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FTaxInvoice
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _ti_oid_mstr As String
    Public ds_edit_soship, ds_edit_ar, ds_edit_pt As DataSet
    Public _ti_gnt_oid As String
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FTaxInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        xtc_detail.SelectedTabPageIndex = 0

        xtc_detail.TabPages(4).PageVisible = False
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ti_en_id.Properties.DataSource = dt_bantu
        ti_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ti_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ti_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer())
        ti_ptnr_id.Properties.DataSource = dt_bantu
        ti_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        ti_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        ti_ptnr_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            ti_tran_id.Properties.DataSource = dt_bantu
            ti_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            ti_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            ti_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            ti_tran_id.Properties.DataSource = dt_bantu
            ti_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            ti_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            ti_tran_id.ItemIndex = 0
        End If

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_fp_status())
        ti_status.Properties.DataSource = dt_bantu
        ti_status.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        ti_status.Properties.ValueMember = dt_bantu.Columns("value").ToString
        ti_status.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        ti_cu_id.Properties.DataSource = dt_bantu
        ti_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        ti_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        ti_cu_id.ItemIndex = 0
    End Sub

    Private Sub ti_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ti_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ti_en_id.EditValue, "fp_sign_user"))
        ti_sign_id.Properties.DataSource = dt_bantu
        ti_sign_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ti_sign_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ti_sign_id.ItemIndex = 0
    End Sub

    Private Sub ti_ptnr_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ti_ptnr_id.EditValueChanged
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_name as address_type, (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, ptnra_oid " + _
                           " from ptnra_addr inner join code_mstr on code_id = ptnra_addr_type " + _
                           " where ptnra_ptnr_oid = (select ptnr_oid from ptnr_mstr where ptnr_id = " + ti_ptnr_id.EditValue.ToString + ")"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "address")
                    ti_ptnr_addr_oid.Properties.DataSource = ds_bantu.Tables("address")
                    ti_ptnr_addr_oid.Properties.DisplayMember = ds_bantu.Tables("address").Columns("address").ToString
                    ti_ptnr_addr_oid.Properties.ValueMember = ds_bantu.Tables("address").Columns("ptnra_oid").ToString
                    ti_ptnr_addr_oid.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Revisi", "ti_rev", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Status", "ti_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Invoice Replacement", "ti_code_pengganti", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Signature User", "sign_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Type", "ti_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unstrikeout", "ti_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Approval Status", "ti_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ti_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ti_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ti_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ti_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_soship, "tis_ti_oid", False)
        add_column_copy(gv_detail_soship, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_soship, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_soship, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_soship, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_soship, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_soship, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_ar, "tia_ti_oid", False)
        add_column_copy(gv_detail_ar, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "AR Code", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "AR Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_ar, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_pt, "tip_ti_oid", False)
        add_column_copy(gv_detail_pt, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Taxable", "tip_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Tax Include", "tip_tax_include", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Exc. Rate", "tip_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Qty", "tip_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "SO Price", "tip_so_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "SO Discount(%)", "tip_so_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_pt, "SO Discount Amount", "tip_so_disc_value", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Tax Price", "tip_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Tax Discount", "tip_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Tax Rate", "tip_tax_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_pt, "Tax Amount", "tip_ppn", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Tax Total", "tip_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit_ar, "tia_oid", False)
        add_column(gv_edit_ar, "tia_en_id", False)
        add_column(gv_edit_ar, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_ar, "tia_ar_oid", False)
        add_column(gv_edit_ar, "AR Code", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_ar, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_ar, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_soship, "tis_oid", False)
        add_column(gv_edit_soship, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_soship, "tis_soship_oid", False)
        add_column(gv_edit_soship, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_soship, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_soship, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_soship, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_soship, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_pt, "tip_oid", False)
        add_column(gv_edit_pt, "pt_id", False)
        add_column(gv_edit_pt, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Taxable", "tip_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Tax Include", "tip_tax_include", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_pt, "Exc. Rate", "tip_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_pt, "Qty", "tip_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pt, "SO Price", "tip_so_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pt, "SO Discount(%)", "tip_so_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit_pt, "SO Discount Amount", "tip_so_disc_value", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pt, "Tax Price", "tip_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pt, "Tax Discount", "tip_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pt, "Tax Rate", "tip_tax_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit_pt, "Tax Amount", "tip_ppn", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pt, "Tax Total", "tip_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_wf, "wf_ref_code", False)
        add_column(gv_wf, "wf_ref_oid", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "ti_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Revisi", "ti_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Signature User", "sign_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Tax Type", "ti_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Unstrikeout", "ti_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Approval Status", "ti_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "User Create", "ti_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Date Create", "ti_add_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ti_mstr.ti_oid, " _
                    & "  ti_mstr.ti_dom_id, " _
                    & "  ti_mstr.ti_en_id, " _
                    & "  en_desc, " _
                    & "  ti_mstr.ti_add_by, " _
                    & "  ti_mstr.ti_add_date, " _
                    & "  ti_mstr.ti_upd_by, " _
                    & "  ti_mstr.ti_upd_date, " _
                    & "  ti_mstr.ti_dt, " _
                    & "  ti_mstr.ti_code, " _
                    & "  ti_mstr.ti_date, " _
                    & "  ti_mstr.ti_sign_id, " _
                    & "  code_name as sign_name, " _
                    & "  ti_mstr.ti_status, " _
                    & "  CASE WHEN ti_mstr.ti_status = '0' " _
                    & "      THEN 'Normal' " _
                    & "      ELSE 'Penggantian' " _
                    & "  END AS ti_status_desc, " _
                    & "  ti_mstr.ti_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
                    & "  ptnr_nppkp, " _
                    & "  ti_mstr.ti_cu_id, " _
                    & "  cu_name, " _
                    & "  ti_mstr.ti_customer_type, " _
                    & "  ti_mstr.ti_area, " _
                    & "  ti_mstr.ti_ppn_type, " _
                    & "  ti_mstr.ti_ptnr_addr_oid, " _
                    & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                    & "  ti_mstr.ti_tran_id, " _
                    & "  tran_name, " _
                    & "  ti_mstr.ti_trans_id, " _
                    & "  ti_mstr.ti_rev, " _
                    & "  ti_mstr.ti_unstrikeout, " _
                    & "  ti_mstr.ti_ti_oid, " _
                    & "  tm.ti_code as ti_code_pengganti " _
                    & "FROM  " _
                    & "  public.ti_mstr " _
                    & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
                    & "  left outer join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
                    & "  inner join code_mstr on code_id = ti_mstr.ti_sign_id " _
                    & "  inner join tran_mstr on tran_id = ti_mstr.ti_tran_id " _
                    & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
                    & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
                    & "  where ti_mstr.ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and ti_mstr.ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " order by ti_mstr.ti_code, ti_mstr.ti_rev"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail_ar").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  tia_oid, " _
            & "  tia_ti_oid, " _
            & "  tia_seq, " _
            & "  tia_ar_oid, " _
            & "  en_desc, " _
            & "  ar_code, " _
            & "  ar_eff_date, " _
            & "  ptnr_name " _
            & "FROM  " _
            & "  public.tia_ar  " _
            & "  inner join ti_mstr on ti_oid = tia_ti_oid " _
            & "  inner join ar_mstr on ar_oid = tia_ar_oid " _
            & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "  inner join en_mstr on en_id = ar_en_id " _
            & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_ar, "detail_ar")

        Try
            ds.Tables("detail_shipment").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  tis_oid, " _
            & "  tis_ti_oid, " _
            & "  tis_seq, " _
            & "  tis_soship_oid, " _
            & "  so_code, " _
            & "  so_date, " _
            & "  soship_code, " _
            & "  soship_date, " _
            & "  en_desc, " _
            & "  ptnr_name " _
            & "FROM  " _
            & "  public.tis_soship  " _
            & "  inner join ti_mstr on ti_oid = tis_ti_oid " _
            & "  inner join soship_mstr on soship_oid = tis_soship_oid " _
            & "  inner join so_mstr on so_oid = soship_so_oid " _
            & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_bill " _
            & "  inner join en_mstr on en_id = so_en_id " _
            & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_soship, "detail_shipment")

        Try
            ds.Tables("detail_item").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  tip_oid, " _
            & "  tip_ti_oid, " _
            & "  tip_seq, " _
            & "  tip_pt_id, " _
            & "  tip_taxable, " _
            & "  tip_tax_include, " _
            & "  tip_tax_class, " _
            & "  code_name as tax_class_name, " _
            & "  tip_exc_rate, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  tip_qty, " _
            & "  tip_so_price, " _
            & "  tip_so_disc, " _
            & "  tip_so_disc_value, " _
            & "  tip_price, " _
            & "  tip_disc, " _
            & "  tip_tax_rate, " _
            & "  tip_ppn, " _
            & "  tip_pph, " _
            & "  tip_total, " _
            & "  tip_soshipd_oid, " _
            & "  so_code, " _
            & "  soship_code " _
            & "FROM  " _
            & "  public.tip_pt  " _
            & "  inner join ti_mstr on ti_oid = tip_ti_oid " _
            & "  inner join pt_mstr on pt_Id = tip_pt_id " _
            & "  inner join code_mstr on code_id = tip_tax_class " _
            & "  inner join soshipd_det on soshipd_oid = tip_soshipd_oid " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join so_mstr on so_oid = soship_so_oid " _
            & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_pt, "detail_item")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join ti_mstr on ti_oid = wf_ref_oid " _
                & " where ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and ti_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and ti_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  ti_oid, " _
                    & "  ti_dom_id, " _
                    & "  ti_en_id, " _
                    & "  en_desc, " _
                    & "  ti_add_by, " _
                    & "  ti_add_date, " _
                    & "  ti_upd_by, " _
                    & "  ti_upd_date, " _
                    & "  ti_dt, " _
                    & "  ti_code, " _
                    & "  ti_date, " _
                    & "  ti_sign_id, " _
                    & "  code_name as sign_name, " _
                    & "  ti_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
                    & "  ptnr_nppkp, " _
                    & "  ti_status, " _
                    & "  ti_customer_type, " _
                    & "  ti_area, " _
                    & "  ti_ppn_type, " _
                    & "  ti_ptnr_addr_oid, " _
                    & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                    & "  ti_tran_id, " _
                    & "  tran_name, " _
                    & "  ti_trans_id, " _
                    & "  ti_rev, " _
                    & "  ti_unstrikeout, " _
                    & "  ti_ti_oid " _
                    & "FROM  " _
                    & "  public.ti_mstr " _
                    & "  inner join en_mstr on en_id = ti_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = ti_ptnr_id " _
                    & "  inner join ptnra_addr on ptnra_oid = ti_ptnr_addr_oid " _
                    & "  inner join code_mstr on code_id = ti_sign_id " _
                    & "  inner join tran_mstr on tran_id = ti_tran_id " _
                    & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_soship.Columns("tis_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tis_ti_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString & "'")
            gv_detail_soship.BestFitColumns()

            gv_detail_ar.Columns("tia_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tia_ti_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString & "'")
            gv_detail_ar.BestFitColumns()

            gv_detail_pt.Columns("tip_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tip_ti_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString & "'")
            gv_detail_pt.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("ti_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ti_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString & "'")
                gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        ti_en_id.Focus()
        ti_en_id.ItemIndex = 0
        ti_date.DateTime = _now
        ti_sign_id.ItemIndex = 0
        ti_ptnr_id.ItemIndex = 0
        ti_customer_type.Text = "01"
        ti_area.Text = "000"
        ti_code.Text = ""
        ti_cu_id.ItemIndex = 0
        ti_tran_id.ItemIndex = 0
        ti_unstrikeout.SelectedIndex = 0
        ti_ti_code.Text = ""
        _ti_gnt_oid = ""

        ti_code.Enabled = True
        ti_ppn_type.Enabled = True
        ti_ptnr_id.Enabled = True
        ti_cu_id.Enabled = True
        ti_status.Enabled = True
        ti_ti_code.Enabled = True

        ti_ti_code.Enabled = False
        gc_edit_ar.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_soship.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_soship.EmbeddedNavigator.Buttons.Remove.Visible = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_ar = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  tia_oid, " _
                        & "  tia_ti_oid, " _
                        & "  tia_seq, " _
                        & "  tia_ar_oid, " _
                        & "  ar_code, " _
                        & "  ar_date, " _
                        & "  en_desc, " _
                        & "  ptnr_name " _
                        & "FROM  " _
                        & "  public.tia_ar  " _
                        & "  inner join ti_mstr on ti_oid = tia_ti_oid " _
                        & "  inner join ar_mstr on ar_oid = tia_ar_oid " _
                        & "  inner join en_mstr on en_id = ar_en_id " _
                        & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                        & " where tia_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_ar, "ar")
                    gc_edit_ar.DataSource = ds_edit_ar.Tables(0)
                    gv_edit_ar.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_soship = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  tis_oid, " _
                        & "  tis_ti_oid, " _
                        & "  tis_seq, " _
                        & "  tis_soship_oid, " _
                        & "  so_code, " _
                        & "  so_date, " _
                        & "  soship_code, " _
                        & "  soship_date, " _
                        & "  en_desc, " _
                        & "  ptnr_name " _
                        & "FROM  " _
                        & "  public.tis_soship  " _
                        & "  inner join ti_mstr on ti_oid = tis_ti_oid " _
                        & "  inner join soship_mstr on soship_oid = tis_soship_oid " _
                        & "  inner join so_mstr on so_oid = soship_so_oid " _
                        & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_bill " _
                        & "  inner join en_mstr on en_id = so_en_id " _
                        & "  where so_code ~~* 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_soship, "so")
                    gc_edit_soship.DataSource = ds_edit_soship.Tables(0)
                    gv_edit_soship.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_pt = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  tip_oid, " _
                        & "  tip_ti_oid, " _
                        & "  tip_seq, " _
                        & "  tip_exc_rate, " _
                        & "  tip_pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  tip_taxable, " _
                        & "  tip_tax_include, " _
                        & "  tip_tax_class, " _
                        & "  code_name as tax_class_name, " _
                        & "  tip_qty, " _
                        & "  tip_so_price, " _
                        & "  tip_so_disc, " _
                        & "  tip_so_disc_value, " _
                        & "  tip_price, " _
                        & "  tip_disc, " _
                        & "  tip_tax_rate, " _
                        & "  tip_ppn, " _
                        & "  tip_pph, " _
                        & "  tip_total, " _
                        & "  tip_soshipd_oid, " _
                        & "  so_code, " _
                        & "  soship_code " _
                        & "FROM  " _
                        & "  public.tip_pt  " _
                        & "  inner join ti_mstr on ti_oid = tip_ti_oid " _
                        & "  inner join code_mstr on code_id = tip_tax_class " _
                        & "  inner join pt_mstr on pt_id = tip_pt_id " _
                        & "  inner join soshipd_det on soshipd_oid = tip_soshipd_oid " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join so_mstr on so_oid = soship_so_oid " _
                        & " where tip_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_pt, "pt")
                    gc_edit_pt.DataSource = ds_edit_pt.Tables(0)
                    gv_edit_pt.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub ti_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ti_code.ButtonClick
        If Trim(ti_customer_type.Text) = "" Then
            MessageBox.Show("Customer Type Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf Trim(ti_area.Text) = "" Then
            MessageBox.Show("Area Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim _tanggal As Date
        Dim _tahun, _ti_code_full, _cmaddr_code_cabang, _ti_code, _ti_status As String
        Dim _no_urut As Integer
        _tanggal = func_coll.get_tanggal_sistem()
        _tahun = _tanggal.Year.ToString.Substring(2, 2)
        _ti_code_full = ""
        _ti_code = ""
        _ti_status = "0"
        _cmaddr_code_cabang = Trim(ti_area.Text)

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "Select COALESCE(MAX(CAST(SUBSTRING(ti_code,12,100) AS INTEGER)),0) + 1 as no_urut " _
                         & " from ti_mstr " _
                         & " where SUBSTRING(ti_code,9,2) = '" + _tahun + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    _no_urut = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        _ti_code = Trim(ti_customer_type.Text) + _ti_status + "." + _cmaddr_code_cabang + "-"
        If Len(_no_urut.ToString) = 1 Then
            _ti_code_full = _ti_code + _tahun + "." + "0000000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 2 Then
            _ti_code_full = _ti_code + _tahun + "." + "000000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 3 Then
            _ti_code_full = _ti_code + _tahun + "." + "00000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 4 Then
            _ti_code_full = _ti_code + _tahun + "." + "0000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 5 Then
            _ti_code_full = _ti_code + _tahun + "." + "000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 6 Then
            _ti_code_full = _ti_code + _tahun + "." + "00" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 7 Then
            _ti_code_full = _ti_code + _tahun + "." + "0" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 8 Then
            _ti_code_full = _ti_code + _tahun + "." + _no_urut.ToString.ToString
        End If

        ti_code.Text = _ti_code_full
    End Sub

    Private Sub gv_edit_shipment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_soship.DoubleClick
        browse_shipment()
    End Sub

    Private Sub browse_shipment()
        Dim _col As String = gv_edit_soship.FocusedColumn.Name
        Dim _row As Integer = gv_edit_soship.FocusedRowHandle

        If _col = "en_desc" Or _col = "so_code" Or _col = "soship_code" Then
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._ptnr_id = ti_ptnr_id.EditValue
            frm._cu_id = ti_cu_id.EditValue
            frm._ppn_type = IIf(ti_ppn_type.EditValue = True, "E", "A")
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_ar_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_ar.DoubleClick
        browse_ar()
    End Sub

    Private Sub browse_ar()
        Dim _col As String = gv_edit_ar.FocusedColumn.Name
        Dim _row As Integer = gv_edit_ar.FocusedRowHandle

        If _col = "en_desc" Or _col = "ar_code" Then
            Dim frm As New FInvoiceSearch
            frm.set_win(Me)
            frm._row = _row
            frm._ptnr_id = ti_ptnr_id.EditValue
            frm._cu_id = ti_cu_id.EditValue
            frm._ppn_type = IIf(ti_ppn_type.EditValue = True, "E", "A")
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_so_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_soship.InitNewRow
        If ds_edit_ar.Tables(0).Rows.Count > 0 Then
            MessageBox.Show("Please Click Retrieve Button For Add List SO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gv_edit_soship.CancelUpdateCurrentRow()
        End If

        gc_edit_ar.EmbeddedNavigator.Buttons.Append.Visible = False
        ti_ppn_type.Enabled = False
        ti_ptnr_id.Enabled = False
        ti_cu_id.Enabled = False
    End Sub

    Private Sub gv_edit_ar_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_ar.InitNewRow
        'gc_edit_soship.EmbeddedNavigator.Buttons.Append.Visible = False
        ti_ppn_type.Enabled = False
        ti_ptnr_id.Enabled = False
        ti_cu_id.Enabled = False
    End Sub

    Private Sub sb_retrieve_soship_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_soship.Click
        ti_ppn_type.Enabled = False

        Dim _ar_code As String = ""
        Dim i As Integer

        If ds_edit_ar.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit_ar.Tables(0).Rows.Count - 1
            _ar_code = _ar_code + "'" + ds_edit_ar.Tables(0).Rows(i).Item("ar_code") + "',"
        Next

        _ar_code = _ar_code.Substring(0, _ar_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT " _
                        & "  soship_oid, " _
                        & "  en_id, " _
                        & "  so_code, " _
                        & "  so_date, " _
                        & "  soship_code, " _
                        & "  soship_date, " _
                        & "  ar_bill_to, " _
                        & "  ptnr_name, " _
                        & "  en_desc " _
                        & "FROM  " _
                        & "  public.ar_mstr " _
                        & "  inner join ars_ship on ars_ar_oid = ar_oid " _
                        & "  inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join so_mstr on so_oid = soship_so_oid " _
                        & "  inner join en_mstr on en_id = ar_en_id " _
                        & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                        & "  where ar_code in (" + _ar_code + ")"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_soship.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_soship.Tables(0).NewRow
            _dtrow("tis_oid") = Guid.NewGuid.ToString
            _dtrow("en_desc") = ds_bantu.Tables(0).Rows(i).Item("en_desc")
            _dtrow("tis_soship_oid") = ds_bantu.Tables(0).Rows(i).Item("soship_oid").ToString
            _dtrow("so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
            _dtrow("so_date") = ds_bantu.Tables(0).Rows(i).Item("so_date")
            _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
            _dtrow("soship_date") = ds_bantu.Tables(0).Rows(i).Item("soship_date")
            _dtrow("ptnr_name") = ds_bantu.Tables(0).Rows(i).Item("ptnr_name")
            ds_edit_soship.Tables(0).Rows.Add(_dtrow)
        Next

        ds_edit_soship.Tables(0).AcceptChanges()
        gv_edit_soship.BestFitColumns()

        gc_edit_ar.EmbeddedNavigator.Buttons.Append.Visible = False
    End Sub

    Private Sub sb_retrieve_pt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_pt.Click
        If ds_edit_ar.Tables(0).Rows.Count > 0 Then
            retrieve_from_ar()
        Else
            retrieve_from_soship()
        End If

        gc_edit_ar.EmbeddedNavigator.Buttons.Append.Visible = False
        gc_edit_soship.EmbeddedNavigator.Buttons.Append.Visible = False
        gc_edit_soship.EmbeddedNavigator.Buttons.Remove.Visible = False
    End Sub

    Private Sub retrieve_from_ar()
        Dim _tax_free_rate As Double = func_coll.get_conf_file("tax_free_rate")
        Dim _after_tax As Double = 0.0
        Dim _total As Double = 0.0
        Dim _tax_rate As Double = 0.0
        Dim _tax_amount As Double = 0.0
        Dim _tax_amount_disc As Double = 0.0
        Dim _ar_code As String = ""
        Dim _soship_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_ar.Tables(0).Rows.Count - 1
            _ar_code = _ar_code + "'" + ds_edit_ar.Tables(0).Rows(i).Item("ar_code") + "',"
        Next

        _ar_code = _ar_code.Substring(0, _ar_code.Length - 1)

        ds_edit_soship.AcceptChanges()

        If ds_edit_soship.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit_soship.Tables(0).Rows.Count - 1
            _soship_code = _soship_code + "'" + ds_edit_soship.Tables(0).Rows(i).Item("soship_code") + "',"
        Next

        _soship_code = _soship_code.Substring(0, _soship_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT " _
                        & "  soshipd_oid, " _
                        & "  ar_exc_rate, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  so_code, " _
                        & "  soship_code, " _
                        & "  ars_taxable, " _
                        & "  ars_tax_inc, " _
                        & "  ars_tax_class_id, " _
                        & "  ars_invoice, " _
                        & "  ars_so_price, " _
                        & "  sod_disc, " _
                        & "  ars_so_disc_value, " _
                        & "  taxr_rate / 100 as taxr_rate, " _
                        & "  ((ars_so_price - ars_so_disc_value) * (taxr_rate) / 100) as after_tax, " _
                        & "  ((ars_so_price - ars_so_disc_value) * (taxr_rate) / 100) * ars_invoice as total, " _
                        & "  ars_tax_class_id, code_name as tax_class_name " _
                        & "FROM  " _
                        & "  public.ar_mstr " _
                        & "  inner join ars_ship on ars_ar_oid = ar_oid " _
                        & "  inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                        & "  inner join code_mstr on code_id = ars_tax_class_id " _
                        & "  inner join taxr_mstr on taxr_tax_class = ars_tax_class_id " _
                        & "  where ar_code in (" + _ar_code + ")" _
                        & "  AND soship_code in (" + _soship_code + ")" _
                        & "  and taxr_tax_type = (select code_id from code_mstr where code_field ~~* 'taxtype_mstr' and code_code ~~* 'ppn') "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ds_edit_pt.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_pt.Tables(0).NewRow
            _dtrow("tip_oid") = Guid.NewGuid.ToString
            _dtrow("tip_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid").ToString
            _dtrow("so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
            _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
            _dtrow("tip_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
            _dtrow("tip_pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
            _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
            _dtrow("tip_taxable") = ds_bantu.Tables(0).Rows(i).Item("ars_taxable")
            _dtrow("tip_tax_include") = ds_bantu.Tables(0).Rows(i).Item("ars_tax_inc")
            _dtrow("tip_tax_class") = ds_bantu.Tables(0).Rows(i).Item("ars_tax_class_id")
            _dtrow("tax_class_name") = ds_bantu.Tables(0).Rows(i).Item("tax_class_name")
            _dtrow("tip_qty") = ds_bantu.Tables(0).Rows(i).Item("ars_invoice")
            _dtrow("tip_so_price") = ds_bantu.Tables(0).Rows(i).Item("ars_so_price")
            _dtrow("tip_so_disc") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
            _dtrow("tip_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value")

            If ti_ppn_type.EditValue = False Then
                _tax_rate = ds_bantu.Tables(0).Rows(i).Item("taxr_rate")
            Else
                _tax_rate = _tax_free_rate
            End If

            _dtrow("tip_tax_rate") = _tax_rate

            If ds_bantu.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                If ds_bantu.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    'disini hanya line ppn saja

                    _tax_amount = _tax_rate * (ds_bantu.Tables(0).Rows(i).Item("ars_so_price") / (1 + _tax_rate))
                    _dtrow("tip_price") = ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - _tax_amount

                    _tax_amount_disc = _tax_rate * (ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value") / (1 + _tax_rate))
                    _dtrow("tip_disc") = ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value") - _tax_amount_disc

                    _dtrow("tip_ppn") = ((ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - _tax_amount) - (ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value") - _tax_amount_disc)) * _tax_rate
                    _after_tax = ((ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - _tax_amount) - (ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value") - _tax_amount_disc)) * _tax_rate

                    _total = (((ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - _tax_amount) - (ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value") - _tax_amount_disc)) + _after_tax) * ds_bantu.Tables(0).Rows(i).Item("ars_invoice")
                    _dtrow("tip_total") = _total * ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                Else
                    _dtrow("tip_price") = ds_bantu.Tables(0).Rows(i).Item("ars_so_price")
                    _dtrow("tip_disc") = ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value")

                    If ti_ppn_type.EditValue = False Then
                        _dtrow("tip_ppn") = ds_bantu.Tables(0).Rows(i).Item("after_tax")
                        _after_tax = ds_bantu.Tables(0).Rows(i).Item("after_tax")
                    Else
                        _dtrow("tip_ppn") = (ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value")) * _tax_rate
                        _after_tax = (ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value")) * _tax_rate
                    End If

                    _total = (ds_bantu.Tables(0).Rows(i).Item("ars_so_price") - ds_bantu.Tables(0).Rows(i).Item("ars_so_disc_value") + _after_tax) * ds_bantu.Tables(0).Rows(i).Item("ars_invoice")
                    _dtrow("tip_total") = _total * ds_bantu.Tables(0).Rows(i).Item("ar_exc_rate")
                End If
            End If

            ds_edit_pt.Tables(0).Rows.Add(_dtrow)
        Next

        ds_edit_pt.Tables(0).AcceptChanges()
        gv_edit_pt.BestFitColumns()
    End Sub

    Private Sub retrieve_from_soship()
        Dim _tax_free_rate As Double = func_coll.get_conf_file("tax_free_rate")
        Dim _after_tax As Double = 0.0
        Dim _total As Double = 0.0
        Dim _soship_code As String = ""
        Dim i As Integer

        Dim _tax_rate As Double = 0.0
        Dim _tax_amount As Double = 0.0
        Dim _tax_amount_disc As Double = 0.0

        ds_edit_soship.AcceptChanges()

        If ds_edit_soship.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit_soship.Tables(0).Rows.Count - 1
            _soship_code = _soship_code + "'" + ds_edit_soship.Tables(0).Rows(i).Item("soship_code") + "',"
        Next

        _soship_code = _soship_code.Substring(0, _soship_code.Length - 1)
        'Exit Sub
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT " _
                        & "  soshipd_oid, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  so_code, " _
                        & "  soship_code, " _
                        & "  sod_qty_shipment, " _
                        & "  sod_price, sod_disc, " _
                        & "  coalesce(soship_exc_rate,1) as soship_exc_rate, " _
                        & "  sod_disc * sod_price as sod_disc_amount, " _
                        & "  taxr_rate / 100 as taxr_rate, " _
                        & "  ((sod_price - (sod_disc * sod_price)) * (taxr_rate) / 100) as after_tax, " _
                        & "  sod_tax_class, sod_taxable, sod_tax_inc, code_name as tax_class_name " _
                        & "FROM  " _
                        & "  public.soship_mstr " _
                        & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                        & "  inner join taxr_mstr on taxr_tax_class = sod_tax_class " _
                        & "  inner join code_mstr on code_id = sod_tax_class " _
                        & "  where soship_code in (" + _soship_code + ")" _
                        & "  and taxr_tax_type = (select code_id from code_mstr where code_field ~~* 'taxtype_mstr' and code_code ~~* 'ppn') "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ds_edit_pt.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_pt.Tables(0).NewRow
            _dtrow("tip_oid") = Guid.NewGuid.ToString
            _dtrow("so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
            _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
            _dtrow("tip_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid").ToString
            _dtrow("tip_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("soship_exc_rate")
            _dtrow("tip_pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
            _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")

            _dtrow("tip_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
            _dtrow("tip_tax_include") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
            _dtrow("tip_tax_class") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
            _dtrow("tax_class_name") = ds_bantu.Tables(0).Rows(i).Item("tax_class_name")

            _dtrow("tip_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_shipment")
            _dtrow("tip_so_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
            _dtrow("tip_so_disc") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
            _dtrow("tip_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")

            If ti_ppn_type.EditValue = False Then
                _tax_rate = ds_bantu.Tables(0).Rows(i).Item("taxr_rate")
            Else
                _tax_rate = _tax_free_rate
            End If

            _dtrow("tip_tax_rate") = _tax_rate

            If ds_bantu.Tables(0).Rows(i).Item("sod_taxable").ToString.ToUpper = "Y" Then
                If ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    'disini hanya line ppn saja
                    'MessageBox.Show(ds_bantu.Tables(0).Rows(i).Item("sod_price"), "")
                    _tax_amount = _tax_rate * (ds_bantu.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate))
                    _dtrow("tip_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price") - _tax_amount

                    _tax_amount_disc = _tax_rate * (ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") / (1 + _tax_rate))
                    _dtrow("tip_disc") = ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") - _tax_amount_disc

                    _dtrow("tip_ppn") = ((ds_bantu.Tables(0).Rows(i).Item("sod_price") - _tax_amount) - (ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") - _tax_amount_disc)) * _tax_rate
                    _after_tax = ((ds_bantu.Tables(0).Rows(i).Item("sod_price") - _tax_amount) - (ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") - _tax_amount_disc)) * _tax_rate

                    _total = (((ds_bantu.Tables(0).Rows(i).Item("sod_price") - _tax_amount) - (ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") - _tax_amount_disc)) + _after_tax) * ds_bantu.Tables(0).Rows(i).Item("sod_qty_shipment")
                    _dtrow("tip_total") = _total * ds_bantu.Tables(0).Rows(i).Item("soship_exc_rate")
                Else
                    _dtrow("tip_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
                    _dtrow("tip_disc") = ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")

                    If ti_ppn_type.EditValue = False Then
                        _dtrow("tip_ppn") = ds_bantu.Tables(0).Rows(i).Item("after_tax")
                        _after_tax = ds_bantu.Tables(0).Rows(i).Item("after_tax")
                    Else
                        _dtrow("tip_ppn") = (ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")) * _tax_rate
                        _after_tax = (ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")) * _tax_rate
                    End If

                    _total = (ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") + _after_tax) * ds_bantu.Tables(0).Rows(i).Item("sod_qty_shipment")
                    _dtrow("tip_total") = _total * ds_bantu.Tables(0).Rows(i).Item("soship_exc_rate")
                End If
            End If

            '_dtrow("tip_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_shipment")
            '_dtrow("tip_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
            '_dtrow("tip_disc") = ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")

            'If ti_ppn_type.EditValue = False Then
            '    _dtrow("tip_tax_rate") = ds_bantu.Tables(0).Rows(i).Item("taxr_rate")
            '    _dtrow("tip_ppn") = ds_bantu.Tables(0).Rows(i).Item("after_tax")
            '    _after_tax = ds_bantu.Tables(0).Rows(i).Item("after_tax")
            'Else
            '    _dtrow("tip_tax_rate") = _tax_free_rate
            '    _dtrow("tip_ppn") = (ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")) * _tax_free_rate
            '    _after_tax = (ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount")) * _tax_free_rate
            'End If

            '_total = (ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc_amount") + _after_tax) * ds_bantu.Tables(0).Rows(i).Item("sod_qty_shipment")
            '_dtrow("tip_total") = _total * ds_bantu.Tables(0).Rows(i).Item("soship_exc_rate")
            ds_edit_pt.Tables(0).Rows.Add(_dtrow)
        Next

        ds_edit_pt.Tables(0).AcceptChanges()
        gv_edit_pt.BestFitColumns()
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        ds_edit_ar.AcceptChanges()
        ds_edit_soship.AcceptChanges()
        ds_edit_pt.AcceptChanges()

        If Trim(ti_code.Text) = "" Then
            MessageBox.Show("Tax Invoice Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit_soship.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Shipment Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit_pt.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Item Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Private Function get_rev() As Integer
        get_rev = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(ti_code) as jml from ti_mstr " + _
                                           " where ti_code ~~* " + SetSetring(ti_code.Text) + _
                                           " and ti_trans_id = 'X' "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_rev = .DataReader.Item("jml")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Return get_rev
    End Function

    Public Overrides Function insert() As Boolean
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")

        Dim _ti_oid As Guid
        _ti_oid = Guid.NewGuid

        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _ti_trans_id, _ti_so_cash, _ref_ar, _ref_so As String
        Dim ds_bantu As New DataSet

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(ti_tran_id.EditValue)
            _ti_trans_id = "D"
        Else
            _ti_trans_id = "I"
        End If

        If _ti_gnt_oid = "" Then
            _ti_gnt_oid = "null"
        Else
            _ti_gnt_oid = "'" & _ti_gnt_oid & "'"
        End If

        If ds_edit_ar.Tables(0).Rows.Count = 0 Then
            _ti_so_cash = "Y"
        Else : _ti_so_cash = "N"
        End If

        ds_edit_pt.AcceptChanges()

        _ref_ar = ""
        _ref_so = ""

        If ds_edit_ar.Tables(0).Rows.Count = 1 Then
            _ref_ar = ds_edit_ar.Tables(0).Rows(0).Item("ar_code")
        End If

        _ref_so = ds_edit_soship.Tables(0).Rows(0).Item("so_code")
        For i = 0 To ds_edit_soship.Tables(0).Rows.Count - 1
            If _ref_so <> ds_edit_soship.Tables(0).Rows(i).Item("so_code") Then
                _ref_so = ""
                Exit For
            End If
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
                                            & "  public.ti_mstr " _
                                            & "( " _
                                            & "  ti_oid, " _
                                            & "  ti_dom_id, " _
                                            & "  ti_en_id, " _
                                            & "  ti_add_by, " _
                                            & "  ti_add_date, " _
                                            & "  ti_dt, " _
                                            & "  ti_code, " _
                                            & "  ti_date, " _
                                            & "  ti_sign_id, " _
                                            & "  ti_ptnr_id, " _
                                            & "  ti_cu_id, " _
                                            & "  ti_status, " _
                                            & "  ti_customer_type, " _
                                            & "  ti_area, " _
                                            & "  ti_ppn_type, " _
                                            & "  ti_ptnr_addr_oid, " _
                                            & "  ti_tran_id, " _
                                            & "  ti_trans_id, " _
                                            & "  ti_rev, " _
                                            & "  ti_unstrikeout, " _
                                            & "  ti_so_cash, " _
                                            & "  ti_ti_oid, " _
                                            & "  ti_ref_ar, " _
                                            & "  ti_ref_so " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ti_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ti_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetSetring(ti_code.Text) & ",  " _
                                            & SetDate(ti_date.DateTime) & ",  " _
                                            & SetInteger(ti_sign_id.EditValue) & ",  " _
                                            & SetInteger(ti_ptnr_id.EditValue) & ",  " _
                                            & SetInteger(ti_cu_id.EditValue) & ",  " _
                                            & SetSetring(ti_status.EditValue) & ",  " _
                                            & SetSetring(ti_customer_type.Text) & ",  " _
                                            & SetSetring(ti_area.Text) & ",  " _
                                            & SetSetring(IIf(ti_ppn_type.EditValue = True, "E", "A")) & ",  " _
                                            & SetSetring(ti_ptnr_addr_oid.EditValue) & ",  " _
                                            & SetInteger(ti_tran_id.EditValue) & ",  " _
                                            & SetSetring(_ti_trans_id) & ",  " _
                                            & SetInteger(get_rev) & ",  " _
                                            & SetSetring(ti_unstrikeout.Text) & ", " _
                                            & SetSetring(_ti_so_cash) & ", " _
                                            & _ti_gnt_oid.ToString & ",  " _
                                            & SetSetring(_ref_ar) & ", " _
                                            & SetSetring(_ref_so) & " " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Insert Data List AR
                        For i = 0 To ds_edit_ar.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tia_ar " _
                                                & "( " _
                                                & "  tia_oid, " _
                                                & "  tia_ti_oid, " _
                                                & "  tia_seq, " _
                                                & "  tia_ar_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_ar.Tables(0).Rows(i).Item("tia_oid")) & ",  " _
                                                & SetSetring(_ti_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_ar.Tables(0).Rows(i).Item("tia_ar_oid")) _
                                                & ");"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List Shipment
                        For i = 0 To ds_edit_soship.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tis_soship " _
                                                & "( " _
                                                & "  tis_oid, " _
                                                & "  tis_ti_oid, " _
                                                & "  tis_seq, " _
                                                & "  tis_soship_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_soship.Tables(0).Rows(i).Item("tis_oid")) & ",  " _
                                                & SetSetring(_ti_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_soship.Tables(0).Rows(i).Item("tis_soship_oid")) _
                                                & ");"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List Item
                        For i = 0 To ds_edit_pt.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tip_pt " _
                                                & "( " _
                                                & "  tip_oid, " _
                                                & "  tip_ti_oid, " _
                                                & "  tip_seq, " _
                                                & "  tip_exc_rate, " _
                                                & "  tip_pt_id, " _
                                                & "  tip_tax_class, " _
                                                & "  tip_taxable, " _
                                                & "  tip_tax_include, " _
                                                & "  tip_qty, " _
                                                & "  tip_so_price, " _
                                                & "  tip_so_disc, " _
                                                & "  tip_so_disc_value, " _
                                                & "  tip_price, " _
                                                & "  tip_ppn, " _
                                                & "  tip_total, " _
                                                & "  tip_disc, " _
                                                & "  tip_soshipd_oid, " _
                                                & "  tip_tax_rate " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_oid")) & ",  " _
                                                & SetSetring(_ti_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_exc_rate")) & ",  " _
                                                & SetInteger(ds_edit_pt.Tables(0).Rows(i).Item("tip_pt_id")) & ",  " _
                                                & SetInteger(ds_edit_pt.Tables(0).Rows(i).Item("tip_tax_class")) & ",  " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_taxable")) & ",  " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_tax_include")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_qty")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_so_price")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_so_disc")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_so_disc_value")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_price")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_ppn")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_total")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_disc")) & ",  " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_soshipd_oid")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_tax_rate")) _
                                                & ");"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If _conf_value = "1" Then
                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wf_mstr " _
                                                & "( " _
                                                & "  wf_oid, " _
                                                & "  wf_dom_id, " _
                                                & "  wf_en_id, " _
                                                & "  wf_tran_id, " _
                                                & "  wf_ref_oid, " _
                                                & "  wf_ref_code, " _
                                                & "  wf_ref_desc, " _
                                                & "  wf_seq, " _
                                                & "  wf_user_id, " _
                                                & "  wf_wfs_id, " _
                                                & "  wf_iscurrent, " _
                                                & "  wf_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(ti_en_id.EditValue) & ",  " _
                                                & SetSetring(ti_tran_id.EditValue) & ",  " _
                                                & SetSetring(_ti_oid.ToString) & ",  " _
                                                & SetSetring(ti_code.Text) & ",  " _
                                                & SetSetring("Tax Invoice") & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Tax Invoice " & ti_code.Text)
                        ssqls.Add(.Command.CommandText)
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
                        after_success()
                        set_row(_ti_oid.ToString, "ti_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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

    Public Overrides Function edit_data() As Boolean
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")

        'If _conf_value = "0" Then
        '    Return False
        'End If

        Dim _trans_id As String
        _trans_id = mf.get_transaction_status_by_oid("ti_trans_id", "ti_mstr", "ti_oid", ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString)

        If _conf_value = "1" Then
            If _trans_id.ToString.ToUpper <> "D" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ti_oid_mstr = SetString(.Item("ti_oid"))
                ti_en_id.EditValue = .Item("ti_en_id")
                ti_code.Text = SetString(.Item("ti_code"))
                ti_date.DateTime = .Item("ti_date")
                ti_sign_id.EditValue = .Item("ti_sign_id")
                ti_ptnr_id.EditValue = .Item("ti_ptnr_id")
                ti_cu_id.EditValue = .Item("ti_cu_id")
                ti_ptnr_addr_oid.EditValue = .Item("ti_ptnr_addr_oid").ToString
                ti_customer_type.Text = .Item("ti_customer_type")
                ti_area.Text = .Item("ti_area")
                If .Item("ti_ppn_type") = "E" Then
                    ti_ppn_type.EditValue = True
                Else
                    ti_ppn_type.EditValue = False
                End If

                ti_status.EditValue = CInt(.Item("ti_status"))

                If IsDBNull(.Item("ti_ti_oid")) = False Then
                    _ti_gnt_oid = Trim(.Item("ti_ti_oid").ToString)
                    ti_ti_code.Text = Trim(.Item("ti_code_pengganti"))
                End If

                ti_unstrikeout.EditValue = .Item("ti_unstrikeout")
                ti_tran_id.EditValue = .Item("ti_tran_id")
            End With

            ti_code.Enabled = False
            ti_ppn_type.Enabled = False
            ti_ptnr_id.Enabled = False
            ti_cu_id.Enabled = False
            ti_status.Enabled = False
            ti_ti_code.Enabled = False

            ti_en_id.Focus()

            ds_edit_ar = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tia_oid, " _
                            & "  tia_ti_oid, " _
                            & "  tia_seq, " _
                            & "  tia_ar_oid, " _
                            & "  ar_code, " _
                            & "  ar_date, " _
                            & "  en_desc, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.tia_ar  " _
                            & "  inner join ti_mstr on ti_oid = tia_ti_oid " _
                            & "  inner join ar_mstr on ar_oid = tia_ar_oid " _
                            & "  inner join en_mstr on en_id = ar_en_id " _
                            & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                            & " where tia_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                        .InitializeCommand()
                        .FillDataSet(ds_edit_ar, "ar")
                        gc_edit_ar.DataSource = ds_edit_ar.Tables(0)
                        gv_edit_ar.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_soship = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tis_oid, " _
                            & "  tis_ti_oid, " _
                            & "  tis_seq, " _
                            & "  tis_soship_oid, " _
                            & "  so_code, " _
                            & "  so_date, " _
                            & "  soship_code, " _
                            & "  soship_date, " _
                            & "  en_desc, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.tis_soship  " _
                            & "  inner join ti_mstr on ti_oid = tis_ti_oid " _
                            & "  inner join soship_mstr on soship_oid = tis_soship_oid " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_bill " _
                            & "  inner join en_mstr on en_id = so_en_id " _
                            & "  where tis_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                        .InitializeCommand()
                        .FillDataSet(ds_edit_soship, "so")
                        gc_edit_soship.DataSource = ds_edit_soship.Tables(0)
                        gv_edit_soship.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_pt = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tip_oid, " _
                            & "  tip_ti_oid, " _
                            & "  tip_seq, " _
                            & "  tip_exc_rate, " _
                            & "  tip_pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  tip_taxable, " _
                            & "  tip_tax_include, " _
                            & "  tip_tax_class, " _
                            & "  code_name as tax_class_name, " _
                            & "  tip_qty, " _
                            & "  tip_so_price, " _
                            & "  tip_so_disc, " _
                            & "  tip_so_disc_value, " _
                            & "  tip_price, " _
                            & "  tip_disc, " _
                            & "  tip_tax_rate, " _
                            & "  tip_ppn, " _
                            & "  tip_pph, " _
                            & "  tip_total, " _
                            & "  tip_soshipd_oid, " _
                            & "  so_code, " _
                            & "  soship_code " _
                            & "FROM  " _
                            & "  public.tip_pt  " _
                            & "  inner join ti_mstr on ti_oid = tip_ti_oid " _
                            & "  inner join pt_mstr on pt_Id = tip_pt_id " _
                            & "  inner join code_mstr on code_id = tip_tax_class " _
                            & "  inner join soshipd_det on soshipd_oid = tip_soshipd_oid " _
                            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & " where tip_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                        .InitializeCommand()
                        .FillDataSet(ds_edit_pt, "pt")
                        gc_edit_pt.DataSource = ds_edit_pt.Tables(0)
                        gv_edit_pt.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If

        tcg_header.SelectedTabPageIndex = 0
        tcg_detail.SelectedTabPageIndex = 0
    End Function

    Public Overrides Function edit()
        edit = True
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")

        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _ti_trans_id, _ti_so_cash, _ref_ar, _ref_so As String

        Dim _ti_tran_id As Integer
        _ti_tran_id = ti_tran_id.EditValue

        Dim ds_bantu As New DataSet
        If _conf_value = "1" Then
            _ti_trans_id = "D"
            ds_bantu = func_data.load_aprv_mstr(ti_tran_id.EditValue)
        Else
            _ti_trans_id = "I"
        End If

        If ds_edit_ar.Tables(0).Rows.Count = 0 Then
            _ti_so_cash = "Y"
        Else : _ti_so_cash = "N"
        End If

        _ref_ar = ""
        _ref_so = ""

        If ds_edit_ar.Tables(0).Rows.Count = 1 Then
            _ref_ar = ds_edit_ar.Tables(0).Rows(0).Item("ar_code")
        End If

        _ref_so = ds_edit_soship.Tables(0).Rows(0).Item("so_code")
        For i = 0 To ds_edit_soship.Tables(0).Rows.Count - 1
            If _ref_so <> ds_edit_soship.Tables(0).Rows(i).Item("so_code") Then
                _ref_so = ""
                Exit For
            End If
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.ti_mstr   " _
                                            & "SET  " _
                                            & "  ti_en_id = " & SetInteger(ti_en_id.EditValue) & ",  " _
                                            & "  ti_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ti_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ti_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ti_date = " & SetDate(ti_date.DateTime) & ",  " _
                                            & "  ti_sign_id = " & SetInteger(ti_sign_id.EditValue) & ",  " _
                                            & "  ti_customer_type = " & SetSetring(ti_customer_type.Text) & ",  " _
                                            & "  ti_area = " & SetSetring(ti_area.Text) & ",  " _
                                            & "  ti_ptnr_addr_oid = " & SetSetring(ti_ptnr_addr_oid.EditValue) & ",  " _
                                            & "  ti_tran_id = " & SetInteger(ti_tran_id.EditValue) & ",  " _
                                            & "  ti_trans_id = " & SetSetring(_ti_trans_id) & ",  " _
                                            & "  ti_so_cash = " & SetSetring(_ti_so_cash) & ",  " _
                                            & "  ti_ref_ar = " & SetSetring(_ref_ar) & ",  " _
                                            & "  ti_ref_so = " & SetSetring(_ref_so) & ",  " _
                                            & "  ti_unstrikeout = " & SetSetring(ti_unstrikeout.Text) & " " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ti_oid = " & SetSetring(_ti_oid_mstr) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from tia_ar where tia_ti_oid = '" + _ti_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from tis_soship where tis_ti_oid = '" + _ti_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from tip_pt where tip_ti_oid = '" + _ti_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Insert Data List AR
                        For i = 0 To ds_edit_ar.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tia_ar " _
                                                & "( " _
                                                & "  tia_oid, " _
                                                & "  tia_ti_oid, " _
                                                & "  tia_seq, " _
                                                & "  tia_ar_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_ar.Tables(0).Rows(i).Item("tia_oid")) & ",  " _
                                                & SetSetring(_ti_oid_mstr.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_ar.Tables(0).Rows(i).Item("tia_ar_oid")) _
                                                & ");"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List Shipment
                        For i = 0 To ds_edit_soship.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tis_soship " _
                                                & "( " _
                                                & "  tis_oid, " _
                                                & "  tis_ti_oid, " _
                                                & "  tis_seq, " _
                                                & "  tis_soship_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_soship.Tables(0).Rows(i).Item("tis_oid")) & ",  " _
                                                & SetSetring(_ti_oid_mstr.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_soship.Tables(0).Rows(i).Item("tis_soship_oid")) _
                                                & ");"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List Item
                        For i = 0 To ds_edit_pt.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tip_pt " _
                                                & "( " _
                                                & "  tip_oid, " _
                                                & "  tip_ti_oid, " _
                                                & "  tip_seq, " _
                                                & "  tip_exc_rate, " _
                                                & "  tip_pt_id, " _
                                                & "  tip_tax_class, " _
                                                & "  tip_taxable, " _
                                                & "  tip_tax_include, " _
                                                & "  tip_qty, " _
                                                & "  tip_so_price, " _
                                                & "  tip_so_disc, " _
                                                & "  tip_so_disc_value, " _
                                                & "  tip_price, " _
                                                & "  tip_ppn, " _
                                                & "  tip_total, " _
                                                & "  tip_disc, " _
                                                & "  tip_soshipd_oid, " _
                                                & "  tip_tax_rate " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_oid")) & ",  " _
                                                & SetSetring(_ti_oid_mstr.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_exc_rate")) & ",  " _
                                                & SetInteger(ds_edit_pt.Tables(0).Rows(i).Item("tip_pt_id")) & ",  " _
                                                & SetInteger(ds_edit_pt.Tables(0).Rows(i).Item("tip_tax_class")) & ",  " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_taxable")) & ",  " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_tax_include")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_qty")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_so_price")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_so_disc")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_so_disc_value")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_price")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_ppn")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_total")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_disc")) & ",  " _
                                                & SetSetring(ds_edit_pt.Tables(0).Rows(i).Item("tip_soshipd_oid")) & ",  " _
                                                & SetDbl(ds_edit_pt.Tables(0).Rows(i).Item("tip_tax_rate")) _
                                                & ");"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If _conf_value = "1" Then
                            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_tran_id") <> ti_tran_id.EditValue Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _ti_oid_mstr + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                            & "  public.wf_mstr " _
                                                            & "( " _
                                                            & "  wf_oid, " _
                                                            & "  wf_dom_id, " _
                                                            & "  wf_en_id, " _
                                                            & "  wf_tran_id, " _
                                                            & "  wf_ref_oid, " _
                                                            & "  wf_ref_code, " _
                                                            & "  wf_ref_desc, " _
                                                            & "  wf_seq, " _
                                                            & "  wf_user_id, " _
                                                            & "  wf_wfs_id, " _
                                                            & "  wf_iscurrent, " _
                                                            & "  wf_dt " _
                                                            & ")  " _
                                                            & "VALUES ( " _
                                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                            & SetInteger(ti_en_id.EditValue) & ",  " _
                                                            & SetInteger(ti_tran_id.EditValue) & ",  " _
                                                            & SetSetring(_ti_oid_mstr.ToString) & ",  " _
                                                            & SetSetring(ti_code.Text) & ",  " _
                                                            & SetSetring("Tax Invoice") & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                            & SetInteger(0) & ",  " _
                                                            & SetSetring("N") & ",  " _
                                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                            & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Tax Invoice" & ti_code.Text)
                        ssqls.Add(.Command.CommandText)
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
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_ti_oid_mstr, "ti_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
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
        Dim ssqls As New ArrayList

        row = BindingContext(ds.Tables(0)).Position
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If _conf_value = "1" Then
            Dim _trans_id As String
            _trans_id = mf.get_transaction_status_by_oid("ti_trans_id", "ti_mstr", "ti_oid", ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString)

            If _trans_id.ToString.ToUpper <> "D" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        'Dim i As Integer

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ti_mstr where ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
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

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString
        _colom = "ti_trans_id"
        _table = "ti_mstr"
        _criteria = "ti_code"
        _initial = "ti"
        _type = "ti"
        _title = "Tax Invoice"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)

        Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _ti_code As String
        Dim _dt_ar, _dt_soship As DataTable
        Dim ssqls As New ArrayList

        If mf.get_transaction_status_by_oid(par_colom, par_table, "ti_oid", par_oid) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        _ti_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '============================================================================
                        If get_status_wf(par_code.ToString()) = 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                   " where wf_ref_oid = '" + par_oid + "'" + _
                                                   " and wf_seq = 0"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_oid = '" + par_oid + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        ElseIf get_status_wf(par_code.ToString()) > 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set " + _
                                                   " wf_iscurrent = 'Y', " + _
                                                   " wf_wfs_id = '0', " + _
                                                   " wf_desc = '', " + _
                                                   " wf_date_to = null, " + _
                                                   " wf_aprv_user = '', " + _
                                                   " wf_aprv_date = null " + _
                                                   " where wf_ref_oid = '" + par_oid + "'" + _
                                                   " and wf_wfs_id = '4' "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        '---------------------------------------------------------------------------------------------
                        'Update ar dan soship
                        _dt_ar = New DataTable
                        _dt_ar = get_ar(par_oid.ToString)

                        If _dt_ar Is Nothing Then
                            MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            sqlTran.Rollback()
                            Exit Sub
                        End If

                        For Each _dr As DataRow In _dt_ar.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ar_mstr set ar_ti_in_use = 'Y' " + _
                                                   " where ar_oid = " + SetSetring(_dr("tia_ar_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        _dt_soship = New DataTable
                        _dt_soship = get_soship(par_oid.ToString)
                        If _dt_soship.Rows.Count = 0 Then
                            MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            sqlTran.Rollback()
                            Exit Sub
                        End If

                        For Each _dr As DataRow In _dt_soship.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update soship_mstr set soship_ti_in_use = 'Y' " + _
                                                   " where soship_oid = " + SetSetring(_dr("tis_soship_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next
                        '---------------------------------------------------------------------------------------------

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        filename = "c:\syspro\" + par_code + ".xls"
                        ExportTo(par_gv, New ExportXlsProvider(filename))

                        If user_wf_email <> "" Then
                            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        Else
                            'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString
        _colom = "ti_trans_id"
        _table = "ti_mstr"
        _criteria = "ti_code"
        _initial = "ti"
        _type = "ti"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""
        Dim _dt_ar, _dt_soship As DataTable
        Dim ssqls As New ArrayList

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where ti_oid = '" + par_oid + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "X" Then
            MessageBox.Show("Can't Cancel For Cancel Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where ti_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '---------------------------------------------------------------------------------------------
                        'Update ar dan soship
                        _dt_ar = New DataTable
                        _dt_ar = get_ar(par_oid.ToString)
                        If _dt_ar.Rows.Count = 0 Then
                            MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            sqlTran.Rollback()
                            Exit Sub
                        End If

                        For Each _dr As DataRow In _dt_ar.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ar_mstr set ar_ti_in_use = 'N' " + _
                                                   " where ar_oid = " + SetSetring(_dr("tia_ar_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        _dt_soship = New DataTable
                        _dt_soship = get_soship(par_oid.ToString)
                        If _dt_soship.Rows.Count = 0 Then
                            MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            sqlTran.Rollback()
                            Exit Sub
                        End If

                        For Each _dr As DataRow In _dt_soship.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update soship_mstr set soship_ti_in_use = 'N' " + _
                                                   " where soship_oid = " + SetSetring(_dr("tis_soship_oid"))
                            ssqls.Add(.Command.CommandText)
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
                        '---------------------------------------------------------------------------------------------

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_code")
        _type = "ti"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Tax Invoice"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Private Function get_ar(ByVal par_oid As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            dt_bantu = Nothing
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tia_oid, " _
                            & "  tia_ar_oid " _
                            & "FROM  " _
                            & "  public.tia_ar  " _
                            & " where tia_ti_oid = " + SetSetring(par_oid).ToString
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "bantu")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Private Function get_soship(ByVal par_oid As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tis_oid, " _
                            & "  tis_soship_oid " _
                            & "FROM  " _
                            & "  public.tis_soship  " _
                            & "  where tis_ti_oid = " + SetSetring(par_oid).ToString
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "bantu")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Private Sub ti_status_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ti_status.EditValueChanged
        If ti_status.EditValue = 1 Then
            ti_ti_code.Enabled = True
        ElseIf ti_status.EditValue = 0 Then
            ti_ti_code.Enabled = False
            ti_ti_code.Text = ""
        End If
        _ti_gnt_oid = ""
    End Sub

    Private Sub ti_ti_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ti_ti_code.ButtonClick
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = ti_en_id.EditValue
        frm._ptnr_id = ti_ptnr_id.EditValue
        frm._obj = ti_ti_code
        frm._ppn_type = IIf(ti_ppn_type.EditValue = True, "E", "A")
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Function set_sql_detail_gabungan() As String
        set_sql_detail_gabungan = "SELECT  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name as sign_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
            & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code as ti_code_pengganti,  " _
            & "  tm.ti_date as ti_date_pengganti, " _
            & "  tip_seq, " _
            & "  tip_pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  trim(pt_desc1 || ' ' || pt_desc2) as pt_desc, " _
            & "  tip_qty, " _
            & "  tip_price, " _
            & "  tip_ppn, " _
            & "  tip_total, " _
            & "  tip_disc, " _
            & "  tip_qty * tip_price as qty_price, " _
            & "  tip_qty * tip_price as qty_price_usd, " _
            & "  tip_qty * tip_disc as qty_disc, " _
            & "  tip_qty * tip_disc as qty_disc_usd, " _
            & "  tip_qty * tip_ppn as qty_ppn, " _
            & "  tip_qty * tip_ppn as qty_ppn_usd, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc_usd, " _
            & "  tip_tax_rate, " _
            & "  (select count(tia_oid) as jml from tia_ar where tia_ti_oid = ti_mstr.ti_oid) as jml_ar " _
            & "FROM  " _
            & "  ti_mstr " _
            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
            & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
            & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
            & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
            & "  inner join pt_mstr on pt_id = tip_pt_id" _
            & "  where ti_mstr.ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'" _
            & "  order by ti_mstr.ti_code, tip_seq "

        Return set_sql_detail_gabungan
    End Function

    Private Function set_sql_detail_single() As String
        set_sql_detail_single = "SELECT  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name as sign_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
            & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code as ti_code_pengganti,  " _
            & "  tm.ti_date as ti_date_pengganti, " _
            & "  tip_seq, " _
            & "  tip_pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  trim(pt_desc1 || ' ' || pt_desc2) as pt_desc, " _
            & "  tip_qty, " _
            & "  tip_price, " _
            & "  tip_ppn, " _
            & "  tip_total, " _
            & "  tip_disc, " _
            & "  tip_qty * tip_price as qty_price, " _
            & "  tip_qty * tip_price as qty_price_usd, " _
            & "  tip_qty * tip_disc as qty_disc, " _
            & "  tip_qty * tip_disc as qty_disc_usd, " _
            & "  tip_qty * tip_ppn as qty_ppn, " _
            & "  tip_qty * tip_ppn as qty_ppn_usd, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc_usd, " _
            & "  tip_tax_rate, " _
            & "  (select count(tia_oid) as jml from tia_ar where tia_ti_oid = ti_mstr.ti_oid) as jml_ar, " _
            & "  (select count(tis_oid) as jml from tis_soship where tis_ti_oid = ti_mstr.ti_oid) as jml_soship, " _
            & "  'Reference : ' || coalesce(ti_mstr.ti_ref_ar,ti_mstr.ti_ref_so) as ar_code, " _
            & "  term_mstr.code_name as top_name, " _
            & "  tip_exc_rate " _
            & "FROM  " _
            & "  ti_mstr " _
            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
            & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
            & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
            & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
            & "  inner join pt_mstr on pt_id = tip_pt_id" _
            & "  left outer join tia_ar on tia_ti_oid = ti_mstr.ti_oid " _
            & "  left outer join ar_mstr on ar_oid = tia_ar_oid " _
            & "  left outer join code_mstr term_mstr on term_mstr.code_id = ar_credit_term " _
            & "  where ti_mstr.ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'" _
            & "  order by ti_mstr.ti_code, tip_seq "

        Return set_sql_detail_single
    End Function

    Private Function set_sql_group() As String
        set_sql_group = "SELECT  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  case ti_mstr.ti_ppn_type " _
            & "  when 'E' then 'Penjualan Buku' " _
            & "  when 'A' then 'PPN Dibayar' " _
            & "  end as pt_desc1, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name as sign_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
            & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code as ti_code_pengganti,  " _
            & "  tm.ti_date as ti_date_pengganti, " _
            & "  sum(tip_qty * tip_price) as qty_price, " _
            & "  sum(tip_qty * tip_price) as qty_price_usd, " _
            & "  sum(tip_qty * tip_disc) as qty_disc, " _
            & "  sum(tip_qty * tip_disc) as qty_disc_usd, " _
            & "  sum(tip_qty * tip_ppn) as qty_ppn, " _
            & "  sum(tip_qty * tip_ppn) as qty_ppn_usd, " _
            & "   sum((tip_qty * tip_price) - (tip_qty * tip_disc)) as price_kurang_disc, " _
            & "  sum((tip_qty * tip_price) - (tip_qty * tip_disc)) as price_kurang_disc_usd " _
            & "FROM  " _
            & "  ti_mstr " _
            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
            & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
            & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
            & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
            & "  inner join pt_mstr on pt_id = tip_pt_id" _
            & "  where ti_mstr.ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'" _
            & "  group by  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  ptnr_npwp, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code,  " _
            & "  tm.ti_date "

        Return set_sql_group
    End Function

    Private Function get_ar_count() As Integer
        get_ar_count = 0

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select count(tia_oid) as jml from tia_ar where tia_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "tia")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_ar_count = ds_bantu.Tables(0).Rows(0).Item("jml")

        Return get_ar_count
    End Function

    Private Function get_soship_count() As Integer
        get_soship_count = 0

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select count(tis_oid) as jml from tis_soship where tis_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "tis")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_soship_count = ds_bantu.Tables(0).Rows(0).Item("jml")

        Return get_soship_count
    End Function

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String
        Dim _ar_count As Integer = get_ar_count()
        Dim _soship_count As Integer = get_soship_count()

        If _ar_count = 0 And _soship_count = 0 Then
            MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ce_show_detail.Checked = True Then
            If _ar_count > 1 Then
                _sql = set_sql_detail_gabungan()
            Else : _sql = set_sql_detail_single()
            End If
        Else
            _sql = set_sql_group()
        End If

        'set_sql_detail_single ini agar bisa tampil no invoice, top, kurs di faktur pajak print out

        Dim rpt As Object = Nothing

        rpt = New XRTaxInvoice

        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

   
End Class
