Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports Newtonsoft.Json.Linq
Imports System.Net.WebClient
Imports System.Collections.Generic

Imports System.Text
Imports Newtonsoft.Json
Imports System.Net

Public Class FPartnerAll
    Dim ssql As String
    Dim _ptnr_oid As String
    Public dt_bantu As DataTable
    Dim mf As New master_new.ModFunction
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit_address, ds_edit_cp As DataSet
    Dim _ptnr_id_edit As Integer = 0
    Dim _conf_value As String = ""
    Dim _delete_rows As New ArrayList
    Public URL_ROOT As String = "http://192.168.1.140:5000/apisyspro/" '"http://192.168.1.140:5000/apisyspro/"

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        _conf_value = func_coll.get_conf_file("upload_reseler_to_web")


        de_ptnr_birthday.EditValue = Now.Date
        de_ptnr_bp_date.EditValue = Now.Date

        AddHandler gv_edit_address.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_edit_address.ColumnFilterChanged, AddressOf relation_detail

        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        init_le(par_entity, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_partner_type())
        par_type.Properties.DataSource = dt_bantu
        par_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        par_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        par_type.ItemIndex = 0

        _delete_rows.Clear()
        AddHandler gc_edit_address.EmbeddedNavigator.ButtonClick, AddressOf gc_edit_address_ButtonClick

    End Sub

    Private Sub gc_edit_address_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            'Box(gv_edit_address.GetFocusedRowCellValue("ptnra_line_1"))

            If MessageBox.Show("Do you want to delete the current row?", "Confirm deletion", _
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
                _delete_rows.Add(gv_edit_address.GetFocusedRowCellValue("ptnra_oid"))
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Public Overrides Sub load_cb()

        init_le(le_ptnr_sex, "Jenis_Kelamin")
        init_le(le_ptnr_goldarah, "gol_darah")
        init_le(le_ptnr_bp_type, "bp_type")
        init_le(le_ptnr_negara, "WNegara")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        ptnr_cu_id.Properties.DataSource = dt_bantu
        ptnr_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        ptnr_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        ptnr_cu_id.ItemIndex = 0

        init_le(sc_le_ptnr_en_id, "en_mstr")
        init_le(ptnr_ac_ar_id, "account")
        init_le(ptnr_ac_ap_id, "account")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr_se())
        ptnr_start_periode.Properties.DataSource = dt_bantu
        ptnr_start_periode.Properties.DisplayMember = dt_bantu.Columns("seperiode_code").ToString
        ptnr_start_periode.Properties.ValueMember = dt_bantu.Columns("seperiode_code").ToString
        ptnr_start_periode.ItemIndex = 0

    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", sc_le_ptnr_en_id.EditValue))
        sc_le_ptnr_ptnrg_id.Properties.DataSource = dt_bantu
        sc_le_ptnr_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        sc_le_ptnr_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        sc_le_ptnr_ptnrg_id.ItemIndex = 0

        'Penambahan fungsi untuk categori customer
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrc_cat", sc_le_ptnr_en_id.EditValue))
        sc_le_ptnr_ptnrc_id.Properties.DataSource = dt_bantu
        sc_le_ptnr_ptnrc_id.Properties.DisplayMember = dt_bantu.Columns("ptnrc_name").ToString
        sc_le_ptnr_ptnrc_id.Properties.ValueMember = dt_bantu.Columns("ptnrc_id").ToString
        sc_le_ptnr_ptnrc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_sb_ap_id.Properties.DataSource = dt_bantu
        ptnr_sb_ap_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ptnr_sb_ap_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ptnr_sb_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_sb_ar_id.Properties.DataSource = dt_bantu
        ptnr_sb_ar_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ptnr_sb_ar_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ptnr_sb_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_cc_ap_id.Properties.DataSource = dt_bantu
        ptnr_cc_ap_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ptnr_cc_ap_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ptnr_cc_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_cc_ar_id.Properties.DataSource = dt_bantu
        ptnr_cc_ar_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ptnr_cc_ar_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ptnr_cc_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sc_le_ptnr_en_id.EditValue, "fakturpajak_transactioncode"))
        ptnr_transaction_code_id.Properties.DataSource = dt_bantu
        ptnr_transaction_code_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ptnr_transaction_code_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ptnr_transaction_code_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_level())
        ptnr_sls_emp_lvl.Properties.DataSource = dt_bantu
        ptnr_sls_emp_lvl.Properties.DisplayMember = dt_bantu.Columns("lvl_name").ToString
        ptnr_sls_emp_lvl.Properties.ValueMember = dt_bantu.Columns("lvl_id").ToString
        ptnr_sls_emp_lvl.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sc_le_ptnr_en_id.EditValue, "position"))
        ptnr_lvl_mob_user.Properties.DataSource = dt_bantu
        ptnr_lvl_mob_user.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ptnr_lvl_mob_user.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ptnr_lvl_mob_user.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.ptnr_sls_emp_pos())
        'dt_bantu = (func_data.load_data("ptnr_sls_emp_pos", sc_le_ptnr_en_id.EditValue))
        ptnr_sls_emp_pos.Properties.DataSource = dt_bantu
        ptnr_sls_emp_pos.Properties.DisplayMember = dt_bantu.Columns("emp_pos_name").ToString
        ptnr_sls_emp_pos.Properties.ValueMember = dt_bantu.Columns("emp_pos_id").ToString
        ptnr_sls_emp_pos.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.ptnr_sls_emp_cat())
        'dt_bantu = (func_data.load_data("ptnr_sls_emp_pos", sc_le_ptnr_en_id.EditValue))
        ptnr_sls_emp_cat.Properties.DataSource = dt_bantu
        ptnr_sls_emp_cat.Properties.DisplayMember = dt_bantu.Columns("emp_cat_name").ToString
        ptnr_sls_emp_cat.Properties.ValueMember = dt_bantu.Columns("emp_cat_id").ToString
        ptnr_sls_emp_cat.ItemIndex = 0
        dt_bantu = New DataTable

        dt_bantu = (func_data.ptnr_sls_emp_lvl())
        'dt_bantu = (func_data.load_data("ptnr_sls_emp_pos", sc_le_ptnr_en_id.EditValue))
        ptnr_sls_emp_lvl.Properties.DataSource = dt_bantu
        ptnr_sls_emp_lvl.Properties.DisplayMember = dt_bantu.Columns("emp_lvl_name").ToString
        ptnr_sls_emp_lvl.Properties.ValueMember = dt_bantu.Columns("emp_lvl_id").ToString
        ptnr_sls_emp_lvl.ItemIndex = 0
    End Sub

    Private Sub sc_le_ptnr_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_ptnr_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Trans. Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Trans. Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID", "ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Old Name", "ptnr_kor_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID SLC Online", "ptnr_sales_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID Customer Online", "ptnr_customer_id", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Name", "ptnr_user_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NIK", "ptnr_nik", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IMEI", "ptnr_imei2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Is Branch Manager", "ptnr_is_bm", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "URL", "ptnr_url", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Email", "ptnr_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Name", "ptnr_name_alt", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Marital Status", "ptnr_marital_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount Dependents", "ptnr_amount_dependents", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Contact Tax", "ptnr_contact_tax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Tax", "ptnr_address_tax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ptnr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Customer", "ptnr_is_cust", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Vendor", "ptnr_is_vend", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Member", "ptnr_is_member", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Employee", "ptnr_is_emp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Writer", "ptnr_is_writer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Personal Sales", "ptnr_is_ps", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Internal Use Only", "ptnr_is_internal", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Is Volunteer", "ptnr_is_volunteer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is SBM", "ptnr_is_sbm", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Parent ID", "ptnr_parent", DevExpress.Utils.HorzAlignment.Default)
        '
        add_column_copy(gv_master, "Parent", "parent_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Level", "lvl_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Periode", "ptnr_start_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "ptnr_bank", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Number", "ptnr_no_rek", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ptnr_rek_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Birthday", "ptnr_birthday", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Birth City", "ptnr_birthcity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Kewarganegaraan", "ptnr_negara_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KTP", "ptnr_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Jenis Kelamin", "ptnr_sex_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Golongan Darah", "ptnr_goldarah_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Nama Ahli Waris", "ptnr_waris_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KTP Ahli Waris", "ptnr_waris_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "BP Date", "ptnr_bp_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "BP Type", "ptnr_bp_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "AR Account Code", "ac_code_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Account Name", "ac_name_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Sub Account", "sb_desc_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Cost Center", "cc_desc_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Code", "ac_code_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Name", "ac_name_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Sub Account", "sb_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Cost Center", "cc_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IMEI", "ptnr_imei", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Limit Credit", "ptnr_limit_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Prepayment Balance", "ptnr_prepaid_balance", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Is Active", "ptnr_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptnr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_address, "ptnra_ptnr_oid", False)
        add_column_copy(gv_detail_address, "Address Type", "address_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "ID", "ptnra_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Customer ID", "ptnra_customer_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Line1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Line2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Line3", "ptnra_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "City", "ptnra_city", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Phone1", "ptnra_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Phone2", "ptnra_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Fax1", "ptnra_fax_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Fax2", "ptnra_fax_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Zip", "ptnra_zip", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Comment", "ptnra_comment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Active", "ptnra_active", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_cp, "ptnra_ptnr_oid", False)
        add_column_copy(gv_detail_cp, "Contact Person", "ptnrac_contact_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cp, "Phone 1", "ptnrac_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cp, "Phone 2", "ptnrac_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cp, "Email", "ptnrac_email", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_address, "ptnra_oid", False)
        add_column(gv_edit_address, "ptnra_addr_type", False)
        add_column(gv_edit_address, "ID", "ptnra_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_address, "Customer ID", "ptnra_customer_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_address, "Address Type", "address_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Line1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Line2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Line3", "ptnra_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "City", "ptnra_city", DevExpress.Utils.HorzAlignment.Default, init_le_repo("city"))
        add_column_edit(gv_edit_address, "Phone1", "ptnra_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Phone2", "ptnra_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Fax1", "ptnra_fax_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Fax2", "ptnra_fax_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Zip", "ptnra_zip", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Comment", "ptnra_comment", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Active", "ptnra_active", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_cp, "ptnrac_oid", False)
        add_column(gv_edit_cp, "addrc_ptnra_oid", False)
        add_column(gv_edit_cp, "ptnrac_function", False)
        add_column(gv_edit_cp, "Function", "ptnrac_function_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Contact Person", "ptnrac_contact_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Phone 1", "ptnrac_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Phone 2", "ptnrac_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Email", "ptnrac_email", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                & "  trans.code_code, " _
                & "  trans.code_name, " _
                & "  a.ptnr_add_by, " _
                & "  a.ptnr_add_date, " _
                & "  a.ptnr_upd_by, " _
                & "  a.ptnr_upd_date, " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name,a.ptnr_kor_name,a.ptnr_address_tax,a.ptnr_contact_tax, " _
                & "  ptnrg_name, " _
                & "  ptnrc_name, " _
                & "  ptnr_name_alt, " _
                & "  a.ptnr_url, " _
                & "  a.ptnr_email, " _
                & "  a.ptnr_remarks, " _
                & "  a.ptnr_parent, " _
                & "  a.ptnr_is_cust, " _
                & "  a.ptnr_is_vend, " _
                & "  a.ptnr_is_internal, " _
                & "  a.ptnr_is_member, " _
                & "  a.ptnr_is_emp,coalesce(a.ptnr_is_ps,'N') as  ptnr_is_ps, " _
                & "  a.ptnr_is_writer, " _
                & "  a.ptnr_npwp, " _
                & "  a.ptnr_nppkp, " _
                & "  a.ptnr_active, " _
                & "  a.ptnr_ktp, " _
                & "  a.ptnr_sex, " _
                & "  a.ptnr_goldarah, " _
                & "  a.ptnr_birthcity, " _
                & "  a.ptnr_birthday, " _
                & "  a.ptnr_negara, " _
                & "  a.ptnr_bp_date, " _
                & "  a.ptnr_bp_type, " _
                & "  a.ptnr_waris_name, " _
                & "  a.ptnr_waris_ktp, " _
                & "  a.ptnr_lvl_mob_user, " _
                & "  a.ptnr_sls_emp_pos, " _
                & "  a.ptnr_sls_emp_cat, " _
                & "  a.ptnr_sls_emp_lvl, " _
                & "  c.en_desc, " _
                & "  b.dom_desc, " _
                & "  ac_mstr_ar.ac_code as ac_code_ar, " _
                & "  ac_mstr_ar.ac_name as ac_name_ar, " _
                & "  sb_mstr_ar.sb_desc as sb_desc_ar, " _
                & "  cc_mstr_ar.cc_desc as cc_desc_ar, " _
                & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                & "  ac_mstr_ap.ac_name as ac_name_ap, " _
                & "  sb_mstr_ap.sb_desc as sb_desc_ap, " _
                & "  cc_mstr_ap.cc_desc as cc_desc_ap, " _
                & "  ptnr_user_name,ptnr_is_bm, " _
                & "  cu_name,ptnr_start_periode, " _
                & "  ptnr_prepaid_balance, " _
                & "  a.ptnr_limit_credit, ptnr_lvl_id,ptnr_parent,lvl_name,(select x.ptnr_name from ptnr_mstr x where x.ptnr_id=a.ptnr_parent) as parent_name, " _
                & "  d.ptnra_line, " _
                & "  d.ptnra_line_1, " _
                & "  d.ptnra_line_2, " _
                & "  d.ptnra_line_3, " _
                & "  d.ptnra_phone_1, " _
                & "  d.ptnra_phone_2, " _
                & "  d.ptnra_fax_1, " _
                & "  d.ptnra_fax_2, " _
                & "  d.ptnra_zip, " _
                & "  d.ptnra_ptnr_oid, " _
                & "  d.ptnra_addr_type, " _
                & "  d.ptnra_comment, wn.code_name as ptnr_negara_name, " _
                & "  d.ptnra_active, sex.code_name as ptnr_sex_type, darah.code_name as ptnr_goldarah_name, bp.code_name as ptnr_bp_name " _
                & " FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.ptnr_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                & "  INNER JOIN public.ptnrg_grp ON ptnrg_id = ptnr_ptnrg_id " _
                & "  INNER JOIN public.ptnrc_cat ON ptnrc_id = ptnr_ptnrc_id " _
                & "  INNER JOIN public.cu_mstr ON cu_id = ptnr_cu_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ap ON ptnr_ac_ap_id = ac_mstr_ap.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ap ON ptnr_sb_ap_id = sb_mstr_ap.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ap ON ptnr_cc_ap_id = cc_mstr_ap.cc_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ar ON ptnr_ac_ar_id = ac_mstr_ar.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ar ON ptnr_sb_ar_id = sb_mstr_ar.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ar ON ptnr_cc_ar_id = cc_mstr_ar.cc_id " _
                & "  left outer join public.code_mstr trans ON code_id = ptnr_transaction_code_id " _
                & "  left outer join public.code_mstr sex ON sex.code_id = a.ptnr_sex " _
                & "  left outer join public.code_mstr darah ON darah.code_id = a.ptnr_goldarah " _
                & "  left outer join public.code_mstr bp ON bp.code_id = a.ptnr_bp_type " _
                & "  left outer join public.code_mstr wn ON wn.code_id = a.ptnr_negara " _
                & "  left outer join public.pslvl_mstr ON lvl_id = ptnr_lvl_id " _
                & "  INNER JOIN public.ptnra_addr d ON (ptnr_oid = ptnra_ptnr_oid) " _
                & " where 1+1=2 "

            If par_entity.Text = "-" Then
                ssql = ssql & " and ptnr_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            Else
                ssql = ssql & " and ptnr_en_id = " + par_entity.EditValue.ToString
            End If

            Dim sql_tambahan As String = ""

            If par_type.EditValue = "A" Then
                sql_tambahan = " "
            ElseIf par_type.EditValue = "V" Then
                ssql = ssql & " and ptnr_is_vend ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "C" Then
                ssql = ssql & " and ptnr_is_cust ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "M" Then
                ssql = ssql & " and ptnr_is_member ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "E" Then
                ssql = ssql & " and ptnr_is_emp ~~* 'Y' " _
                            & " order by ptnr_name"
            ElseIf par_type.EditValue = "W" Then
                ssql = ssql & " and ptnr_is_writer ~~* 'Y' " _
                             & " order by ptnr_name"
            End If
            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ptnr_oid, " _
                & "  a.ptnr_dom_id, " _
                & "  a.ptnr_en_id,ptnr_nik,ptnr_imei2,ptnr_sales_code,ptnr_customer_id, " _
                & "  a.ptnr_transaction_code_id,ptnr_marital_status,ptnr_amount_dependents, " _
                & "  trans.code_code, " _
                & "  trans.code_name, " _
                & "  a.ptnr_add_by, " _
                & "  a.ptnr_add_date, " _
                & "  a.ptnr_upd_by, " _
                & "  a.ptnr_upd_date, " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name,a.ptnr_kor_name,a.ptnr_address_tax,a.ptnr_contact_tax, " _
                & "  a.ptnr_ptnrg_id, " _
                & "  ptnrg_name, " _
                & "  ptnr_name_alt, " _
                & "  a.ptnr_ptnrc_id, " _
                & "  ptnrc_name, " _
                & "  a.ptnr_url, " _
                & "  a.ptnr_email, " _
                & "  a.ptnr_remarks, " _
                & "  a.ptnr_parent, " _
                & "  a.ptnr_is_cust, " _
                & "  a.ptnr_is_vend,ptnr_is_internal,ptnr_is_volunteer,ptnr_is_sbm, " _
                & "  a.ptnr_is_member, " _
                & "  a.ptnr_is_emp,coalesce(a.ptnr_is_ps,'N') as  ptnr_is_ps, " _
                & "  a.ptnr_is_writer, " _
                & "  a.ptnr_npwp, " _
                & "  a.ptnr_nppkp, " _
                & "  a.ptnr_active, " _
                & "  a.ptnr_dt, " _
                & "  a.ptnr_ktp, " _
                & "  a.ptnr_sex, " _
                & "  a.ptnr_goldarah, " _
                & "  a.ptnr_birthcity, " _
                & "  a.ptnr_birthday, " _
                & "  a.ptnr_negara, " _
                & "  a.ptnr_bp_date, " _
                & "  a.ptnr_bp_type, " _
                & "  a.ptnr_waris_name, wn.code_name as ptnr_negara_name, " _
                & "  a.ptnr_waris_ktp, sex.code_name as ptnr_sex_type, darah.code_name as ptnr_goldarah_name, bp.code_name as ptnr_bp_name, " _
                & "  c.en_desc, " _
                & "  b.dom_desc, " _
                & "  ac_mstr_ar.ac_code as ac_code_ar, " _
                & "  ac_mstr_ar.ac_name as ac_name_ar, " _
                & "  sb_mstr_ar.sb_desc as sb_desc_ar, " _
                & "  cc_mstr_ar.cc_desc as cc_desc_ar, " _
                & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                & "  ac_mstr_ap.ac_name as ac_name_ap, " _
                & "  sb_mstr_ap.sb_desc as sb_desc_ap, " _
                & "  cc_mstr_ap.cc_desc as cc_desc_ap, " _
                & "  a.ptnr_ac_ar_id, " _
                & "  a.ptnr_sb_ar_id, " _
                & "  a.ptnr_cc_ar_id, " _
                & "  a.ptnr_ac_ap_id, " _
                & "  a.ptnr_sb_ap_id, " _
                & "  a.ptnr_cc_ap_id, " _
                & "  a.ptnr_cu_id,ptnr_user_name,ptnr_is_bm, " _
                & "  cu_name,ptnr_start_periode,ptnr_imei, " _
                & "  ptnr_prepaid_balance,ptnr_bank,ptnr_no_rek,ptnr_rek_name, " _
                & "  a.ptnr_lvl_mob_user, " _
                & "  a.ptnr_sls_emp_pos, " _
                & "  a.ptnr_sls_emp_cat, " _
                & "  a.ptnr_sls_emp_lvl, " _
                & "  a.ptnr_limit_credit,ptnr_lvl_id,ptnr_parent,lvl_name,(select x.ptnr_name from ptnr_mstr x where x.ptnr_id=a.ptnr_parent) as parent_name " _
                & " FROM " _
                & "  public.ptnr_mstr a " _
                & "  left outer JOIN public.dom_mstr b ON (a.ptnr_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                & "  left outer  JOIN public.ptnrg_grp ON ptnrg_id = ptnr_ptnrg_id " _
                & "  left outer  JOIN public.ptnrc_cat ON ptnrc_id = ptnr_ptnrc_id " _
                & "  left outer  JOIN public.cu_mstr ON cu_id = ptnr_cu_id " _
                & "  left outer  JOIN public.ac_mstr ac_mstr_ap ON ptnr_ac_ap_id = ac_mstr_ap.ac_id " _
                & "  left outer  JOIN public.sb_mstr sb_mstr_ap ON ptnr_sb_ap_id = sb_mstr_ap.sb_id " _
                & "  left outer  JOIN public.cc_mstr cc_mstr_ap ON ptnr_cc_ap_id = cc_mstr_ap.cc_id " _
                & "  left outer  JOIN public.ac_mstr ac_mstr_ar ON ptnr_ac_ar_id = ac_mstr_ar.ac_id " _
                & "  left outer  JOIN public.sb_mstr sb_mstr_ar ON ptnr_sb_ar_id = sb_mstr_ar.sb_id " _
                & "  left outer  JOIN public.cc_mstr cc_mstr_ar ON ptnr_cc_ar_id = cc_mstr_ar.cc_id " _
                & "  left outer join public.code_mstr trans ON code_id = ptnr_transaction_code_id " _
                & "  left outer join public.code_mstr sex ON sex.code_id = a.ptnr_sex " _
                & "  left outer join public.code_mstr darah ON darah.code_id = a.ptnr_goldarah " _
                & "  left outer join public.code_mstr wn ON wn.code_id = a.ptnr_negara " _
                & "  left outer join public.code_mstr bp ON bp.code_id = a.ptnr_bp_type " _
                & "  left outer join public.pslvl_mstr ON lvl_id = ptnr_lvl_id " _
                & " where 1+1=2 "

        If par_entity.Text = "-" Then
            get_sequel = get_sequel & " and ptnr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Else
            get_sequel = get_sequel & " and ptnr_en_id = " + par_entity.EditValue.ToString
        End If

        Dim sql_tambahan As String = ""

        If par_type.EditValue = "A" Then
            sql_tambahan = " "
        ElseIf par_type.EditValue = "V" Then
            sql_tambahan = " and ptnr_is_vend ~~* 'Y' " _
                         & " order by ptnr_name"
        ElseIf par_type.EditValue = "C" Then
            sql_tambahan = " and ptnr_is_cust ~~* 'Y' " _
                         & " order by ptnr_name"
        ElseIf par_type.EditValue = "M" Then
            sql_tambahan = " and ptnr_is_member ~~* 'Y' " _
                         & " order by ptnr_name"
        ElseIf par_type.EditValue = "E" Then
            sql_tambahan = " and ptnr_is_emp ~~* 'Y' " _
                        & " order by ptnr_name"
        ElseIf par_type.EditValue = "W" Then
            sql_tambahan = " and ptnr_is_writer ~~* 'Y' " _
                         & " order by ptnr_name"
        End If

        get_sequel = get_sequel + sql_tambahan

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String = ""

        'Dim sql_tambahan As String = ""

        'If par_type.EditValue = "A" Then
        '    sql_tambahan = " "
        'ElseIf par_type.EditValue = "V" Then
        '    sql_tambahan = " and ptnr_is_vend ~~* 'Y' " _
        '                 & " and ptnr_is_cust ~~* 'N' " _
        '                 & " and ptnr_is_member ~~* 'N' " _
        '                 & " and ptnr_is_emp ~~* 'N' " _
        '                 & " and ptnr_is_writer ~~* 'N' " _
        '                 & " order by ptnr_name"
        'ElseIf par_type.EditValue = "C" Then
        '    sql_tambahan = " and ptnr_is_vend ~~* 'N' " _
        '                 & " and ptnr_is_cust ~~* 'Y' " _
        '                 & " and ptnr_is_member ~~* 'N' " _
        '                 & " and ptnr_is_emp ~~* 'N' " _
        '                 & " and ptnr_is_writer ~~* 'N' " _
        '                 & " order by ptnr_name"
        'ElseIf par_type.EditValue = "M" Then
        '    sql_tambahan = " and ptnr_is_vend ~~* 'N' " _
        '                 & " and ptnr_is_cust ~~* 'N' " _
        '                 & " and ptnr_is_member ~~* 'Y' " _
        '                 & " and ptnr_is_emp ~~* 'N' " _
        '                 & " and ptnr_is_writer ~~* 'N' " _
        '                 & " order by ptnr_name"
        'ElseIf par_type.EditValue = "E" Then
        '    sql_tambahan = " and ptnr_is_vend ~~* 'N' " _
        '                 & " and ptnr_is_cust ~~* 'N' " _
        '                 & " and ptnr_is_member ~~* 'N' " _
        '                 & " and ptnr_is_emp ~~* 'Y' " _
        '                 & " and ptnr_is_writer ~~* 'N' " _
        '                 & " order by ptnr_name"
        'ElseIf par_type.EditValue = "W" Then
        '    sql_tambahan = " and ptnr_is_vend ~~* 'N' " _
        '                 & " and ptnr_is_cust ~~* 'N' " _
        '                 & " and ptnr_is_member ~~* 'N' " _
        '                 & " and ptnr_is_emp ~~* 'N' " _
        '                 & " and ptnr_is_writer ~~* 'Y' " _
        '                 & " order by ptnr_name"
        'End If

        'Try
        '    ds.Tables("address").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  a.ptnra_oid, " _
        '    & "  a.ptnra_id, " _
        '    & "  a.ptnra_dom_id, " _
        '    & "  a.ptnra_en_id, " _
        '    & "  a.ptnra_add_by, " _
        '    & "  a.ptnra_add_date, " _
        '    & "  a.ptnra_upd_by, " _
        '    & "  a.ptnra_upd_date, " _
        '    & "  a.ptnra_line, " _
        '    & "  a.ptnra_line_1, " _
        '    & "  a.ptnra_line_2, " _
        '    & "  a.ptnra_line_3, " _
        '    & "  a.ptnra_phone_1, " _
        '    & "  a.ptnra_phone_2, " _
        '    & "  a.ptnra_fax_1, " _
        '    & "  a.ptnra_fax_2, " _
        '    & "  a.ptnra_zip, " _
        '    & "  a.ptnra_ptnr_oid, " _
        '    & "  a.ptnra_addr_type, " _
        '    & "  a.ptnra_comment, " _
        '    & "  a.ptnra_active, " _
        '    & "  a.ptnra_dt, " _
        '    & "  b.dom_desc, " _
        '    & "  c.en_desc, " _
        '    & "  code_name as address_type, " _
        '    & "  public.ptnr_mstr.ptnr_name " _
        '    & "FROM " _
        '    & "  public.ptnra_addr a " _
        '    & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
        '    & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
        '    & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
        '    & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
        '    & " where ptnra_en_id = " + par_entity.EditValue.ToString 

        'sql = sql + sql_tambahan
        'load_data_detail(sql, gc_detail_address, "address")

        'Try
        '    ds.Tables("contactperson").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '        & "  a.ptnrac_oid, " _
        '        & "  a.addrc_ptnra_oid, " _
        '        & "  a.ptnrac_add_by, " _
        '        & "  a.ptnrac_add_date, " _
        '        & "  a.ptnrac_seq, " _
        '        & "  a.ptnrac_function, " _
        '        & "  a.ptnrac_contact_name, " _
        '        & "  a.ptnrac_phone_1, " _
        '        & "  a.ptnrac_phone_2, " _
        '        & "  a.ptnrac_email, " _
        '        & "  a.ptnrac_dt, " _
        '        & "  b.ptnra_line, " _
        '        & "  code_name as ptnrac_function_name, " _
        '        & "  ptnra_ptnr_oid, " _
        '        & "  ptnr_name " _
        '        & "FROM " _
        '        & "  public.ptnrac_cntc a " _
        '        & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
        '        & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
        '        & "  Inner join public.code_mstr on code_id = ptnrac_function " _
        '        & " where ptnra_en_id = " + par_entity.EditValue.ToString

        'sql = sql + sql_tambahan
        'load_data_detail(sql, gc_detail_cp, "contactperson")
    End Sub

    Public Overrides Sub relation_detail()
        'Try
        '    gv_detail_address.Columns("ptnra_ptnr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid"))
        '    gv_detail_address.BestFitColumns()

        '    gv_detail_cp.Columns("ptnra_ptnr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid"))
        '    gv_detail_cp.BestFitColumns()
        'Catch ex As Exception
        'End Try
        Try

            'gv_edit_cp.Columns("addrc_ptnra_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("addrc_ptnra_oid='" & ds_edit_address.Tables(0).Rows(BindingContext(ds_edit_address.Tables(0)).Position).Item("ptnra_oid").ToString & "'")
            'gv_edit_cp.BestFitColumns()

            'gv_edit_cp.Columns("addrc_ptnra_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit_address.Tables(0).Rows(BindingContext(ds_edit_address.Tables(0)).Position).Item("ptnra_oid"))
            'gv_edit_cp.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        sc_le_ptnr_en_id.ItemIndex = 0
        ptnr_transaction_code_id.ItemIndex = 0
        sc_te_ptnr_name.Text = ""
        sc_te_ptnr_url.Text = ""
        ptnr_npwp.Text = ""
        ptnr_name_alt.Text = ""
        ptnr_nppkp.Text = ""
        sc_te_ptnr_remarks.Text = ""
        sc_le_ptnr_ptnrg_id.ItemIndex = 0 'par_entity
        sc_le_ptnr_ptnrc_id.ItemIndex = 0 'par_entity
        sc_ce_ptnr_active.EditValue = True
        sc_ce_ptnr_is_cust.EditValue = False
        sc_ce_ptnr_is_vend.EditValue = False
        sc_ce_ptnr_is_internal.EditValue = False
        ptnr_is_writer.EditValue = False
        ptnr_is_member.EditValue = False
        ptnr_is_emp.EditValue = False
        ptnr_cu_id.ItemIndex = 0
        ptnr_ac_ar_id.ItemIndex = 0
        ptnr_ac_ap_id.ItemIndex = 0
        ptnr_email.Text = ""
        sc_le_ptnr_en_id.Focus()
        ptnr_is_ps.EditValue = False
        ptnr_parent.Tag = ""
        ptnr_parent.EditValue = ""
        ptnr_sls_emp_lvl.EditValue = ""
        ptnr_user_name.EditValue = ""
        ptnr_is_bm.EditValue = False
        ptnr_imei.EditValue = ""
        ptnr_is_volunteer.EditValue = False
        ptnr_is_sbm.EditValue = False
        ptnr_nik.EditValue = ""
        ptnr_imei2.EditValue = ""

        'new add 20241028 by rans untuk kebutuhan limitasi user di mobile
        ptnr_lvl_mob_user.ItemIndex = 0

        de_ptnr_birthday.EditValue = Date.Now
        de_ptnr_bp_date.EditValue = Date.Now
        te_ptnr_ktp.Text = ""
        te_ptnr_waris_ktp.Text = ""
        te_ptnr_waris_name.Text = ""
        le_ptnr_birthcity.Text = ""
        le_ptnr_bp_type.EditValue = False
        le_ptnr_goldarah.EditValue = False
        le_ptnr_negara.EditValue = False
        le_ptnr_sex.EditValue = False

        ptnr_marital_status.EditValue = ""
        ptnr_amount_dependents.EditValue = ""

        ptnr_is_ps.Enabled = True
        ptnr_parent.Enabled = True
        btClear.Enabled = True
        sc_le_ptnr_en_id.Enabled = True


        ptnr_sales_code.Text = ""
        ptnr_customer_id.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        _delete_rows.Clear()
        ds_edit_address = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.ptnra_oid, " _
                        & "  a.ptnra_id, " _
                        & "  a.ptnra_dom_id, " _
                        & "  a.ptnra_en_id, " _
                        & "  a.ptnra_add_by, " _
                        & "  a.ptnra_add_date, " _
                        & "  a.ptnra_upd_by, " _
                        & "  a.ptnra_upd_date, " _
                        & "  a.ptnra_line,ptnra_city, " _
                        & "  a.ptnra_line_1, " _
                        & "  a.ptnra_line_2, " _
                        & "  a.ptnra_line_3, " _
                        & "  a.ptnra_phone_1, " _
                        & "  a.ptnra_phone_2, " _
                        & "  a.ptnra_fax_1, " _
                        & "  a.ptnra_fax_2, " _
                        & "  a.ptnra_zip, " _
                        & "  a.ptnra_ptnr_oid, " _
                        & "  a.ptnra_addr_type,ptnra_city, " _
                        & "  a.ptnra_comment, " _
                        & "  a.ptnra_active, " _
                        & "  a.ptnra_dt, " _
                        & "  b.dom_desc, " _
                        & "  c.en_desc, " _
                        & "  code_name as address_type, " _
                        & "  public.ptnr_mstr.ptnr_name " _
                        & "FROM " _
                        & "  public.ptnra_addr a " _
                        & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
                        & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
                        & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
                        & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
                        & " where ptnra_line = -99 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit_address, "address")
                    gc_edit_address.DataSource = ds_edit_address.Tables(0)
                    gv_edit_address.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_cp = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.ptnrac_oid, " _
                        & "  a.addrc_ptnra_oid, " _
                        & "  a.ptnrac_add_by, " _
                        & "  a.ptnrac_add_date, " _
                        & "  a.ptnrac_seq, " _
                        & "  a.ptnrac_function, " _
                        & "  a.ptnrac_contact_name, " _
                        & "  a.ptnrac_phone_1, " _
                        & "  a.ptnrac_phone_2, " _
                        & "  a.ptnrac_email, " _
                        & "  a.ptnrac_dt, " _
                        & "  b.ptnra_line, " _
                        & "  code_name as ptnrac_function_name, " _
                        & "  ptnra_ptnr_oid, " _
                        & "  ptnr_name " _
                        & "FROM " _
                        & "  public.ptnrac_cntc a " _
                        & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                        & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                        & "  Inner join public.code_mstr on code_id = ptnrac_function " _
                        & " where ptnrac_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_cp, "contactperson")
                    gc_edit_cp.DataSource = ds_edit_cp.Tables(0)
                    gv_edit_cp.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    'Private Function GetID_Local(ByVal par_en_code As String) As Integer
    '    Try
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text

    '                .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),3,100) as integer)),0) as max_col  from ptnr_mstr " + _
    '                                       " where substring(cast(ptnr_id as varchar),3,100) <> ''"
    '                .InitializeCommand()

    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read

    '                    GetID_Local = .DataReader("max_col") + 1
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    If par_en_code = "0" Then
    '        par_en_code = "99"
    '    End If

    '    GetID_Local = CInt(par_en_code + GetID_Local.ToString)

    '    Return GetID_Local
    'End Function

    Private Function GetID_Local(ByVal par_en_code As String) As Long
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),5,100) as integer)),0) as max_col  from ptnr_mstr " + _
                                           " where substring(cast(ptnr_id as varchar),5,100) <> '' and ptnr_en_id=(select en_id from en_mstr where en_code='" _
                                           & par_en_code & "') and substring(cast(ptnr_id as varchar),3,2)='" & master_new.ClsVar.sServerCode & "'"


                    .InitializeCommand()
                    'Clipboard.SetText(.Command.CommandText)
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CLng(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        'Clipboard.SetText(Clipboard.GetText & " >> " & GetID_Local)
        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid

        Dim _ptnr_code As String = ""
        Dim _ptnr_id, i As Long
        Dim ssqls As New ArrayList

        '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", sc_le_ptnr_en_id.EditValue.ToString))
        _ptnr_id = SetInteger(GetID_Local(sc_le_ptnr_en_id.GetColumnValue("en_code")))
        'Clipboard.SetText(_ptnr_id)
        If sc_ce_ptnr_is_cust.EditValue = True Then
            _ptnr_code = _ptnr_code + "CU"
        End If
        If sc_ce_ptnr_is_vend.EditValue = True Then
            _ptnr_code = _ptnr_code + "SP"
        End If
        If ptnr_is_member.EditValue = True Then
            _ptnr_code = _ptnr_code + "SL"
        End If
        If ptnr_is_emp.EditValue = True Then
            _ptnr_code = _ptnr_code + "EM"
        End If
        If ptnr_is_writer.EditValue = True Then
            _ptnr_code = _ptnr_code + "WR"
        End If

        'If Len(_ptnr_code) = 2 Then
        '    _ptnr_code = _ptnr_code + "00"
        'End If

        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(4, Len(_ptnr_id.ToString) - 4)

        'If Len(_ptnr_id_s) = 1 Then
        '    _ptnr_id_s = "000000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 2 Then
        '    _ptnr_id_s = "00000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 3 Then
        '    _ptnr_id_s = "0000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 4 Then
        '    _ptnr_id_s = "000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 5 Then
        '    _ptnr_id_s = "00" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 6 Then
        '    _ptnr_id_s = "0" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 7 Then
        '    _ptnr_id_s = _ptnr_id_s.ToString
        'End If

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + _ptnr_id_s.ToString
        End If

        Static Generator As System.Random = New System.Random()
        Dim _rnd As Integer = Generator.Next(1, 99)

        _ptnr_code = _ptnr_code + IIf(sc_le_ptnr_en_id.GetColumnValue("en_code") = 0, "99", sc_le_ptnr_en_id.GetColumnValue("en_code")) _
                + _ptnr_id_s.ToString + _rnd.ToString("00")

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
                                            & "  public.ptnr_mstr " _
                                            & "( " _
                                            & "  ptnr_oid, " _
                                            & "  ptnr_dom_id, " _
                                            & "  ptnr_en_id, " _
                                            & "  ptnr_add_by, " _
                                            & "  ptnr_add_date, " _
                                            & "  ptnr_id,ptnr_sales_code,ptnr_customer_id, " _
                                            & "  ptnr_code,ptnr_address_tax,ptnr_contact_tax,ptnr_nik,ptnr_imei2, " _
                                            & "  ptnr_name, " _
                                            & "  ptnr_name_alt, " _
                                            & "  ptnr_ptnrg_id, " _
                                            & "  ptnr_ptnrc_id, " _
                                            & "  ptnr_url, " _
                                            & "  ptnr_email, " _
                                            & "  ptnr_npwp, " _
                                            & "  ptnr_nppkp, " _
                                            & "  ptnr_remarks, " _
                                            & "  ptnr_is_cust, " _
                                            & "  ptnr_is_vend, " _
                                            & "  ptnr_is_internal, " _
                                            & "  ptnr_is_member, " _
                                            & "  ptnr_is_emp, " _
                                            & "  ptnr_is_writer, " _
                                            & "  ptnr_ac_ar_id, " _
                                            & "  ptnr_sb_ar_id, " _
                                            & "  ptnr_cc_ar_id, " _
                                            & "  ptnr_ac_ap_id, " _
                                            & "  ptnr_sb_ap_id, " _
                                            & "  ptnr_cc_ap_id,ptnr_imei,ptnr_lvl_mob_user, " _
                                            & "  ptnr_cu_id,ptnr_bank,ptnr_no_rek,ptnr_rek_name, " _
                                            & "  ptnr_limit_credit,ptnr_user_name,ptnr_is_bm, " _
                                            & "  ptnr_active,ptnr_is_volunteer,ptnr_is_sbm, " _
                                            & "  ptnr_transaction_code_id,ptnr_is_ps,ptnr_lvl_id,ptnr_parent,ptnr_start_periode, " _
                                            & "  ptnr_dt, " _
                                            & "  ptnr_ktp, " _
                                            & "  ptnr_sex, " _
                                            & "  ptnr_goldarah, " _
                                            & "  ptnr_birthcity, " _
                                            & "  ptnr_birthday, " _
                                            & "  ptnr_negara, " _
                                            & "  ptnr_bp_date, " _
                                            & "  ptnr_bp_type, " _
                                            & "  ptnr_waris_name,ptnr_marital_status,ptnr_amount_dependents, " _
                                            & "  ptnr_sls_emp_pos, " _
                                            & "  ptnr_sls_emp_cat, " _
                                            & "  ptnr_sls_emp_lvl, " _
                                            & "  ptnr_waris_ktp " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnr_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(sc_le_ptnr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & _ptnr_id & ",  " _
                                            & SetSetring(ptnr_sales_code.Text) & ",  " _
                                            & SetSetring(ptnr_customer_id.Text) & ",  " _
                                            & SetSetring(_ptnr_code) & ",  " _
                                            & SetSetring(ptnr_address_tax.Text) & ",  " _
                                            & SetSetring(ptnr_contact_tax.Text) & ",  " _
                                            & SetSetring(ptnr_nik.Text) & ",  " _
                                            & SetSetring(ptnr_imei2.Text) & ",  " _
                                            & SetSetring(sc_te_ptnr_name.Text) & ",  " _
                                            & SetSetring(ptnr_name_alt.Text) & ",  " _
                                            & sc_le_ptnr_ptnrg_id.EditValue & ",  " _
                                            & sc_le_ptnr_ptnrc_id.EditValue & ",  " _
                                            & SetSetring(sc_te_ptnr_url.Text) & ",  " _
                                            & SetSetring(ptnr_email.Text) & ",  " _
                                            & SetSetring(ptnr_npwp.Text) & ",  " _
                                            & SetSetring(ptnr_nppkp.Text) & ",  " _
                                            & SetSetring(sc_te_ptnr_remarks.Text) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_cust.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_vend.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_internal.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_member.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_emp.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_writer.EditValue) & ",  " _
                                            & SetInteger(ptnr_ac_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_sb_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cc_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_ac_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_sb_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cc_ap_id.EditValue) & ",  " _
                                            & SetSetring(ptnr_imei.EditValue) & ",  " _
                                            & SetInteger(ptnr_lvl_mob_user.EditValue) & ",  " _
                                            & SetInteger(ptnr_cu_id.EditValue) & ",  " _
                                            & SetSetring(ptnr_bank.EditValue) & ",  " _
                                            & SetSetring(ptnr_no_rek.EditValue) & ",  " _
                                            & SetSetring(ptnr_rek_name.EditValue) & ",  " _
                                            & SetDbl(ptnr_limit_credit.EditValue) & ",  " _
                                            & SetSetring(ptnr_user_name.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_bm.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_active.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_volunteer.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_sbm.EditValue) & ",  " _
                                            & SetInteger(ptnr_transaction_code_id.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_ps.EditValue) & ",  " _
                                            & SetInteger(ptnr_sls_emp_lvl.EditValue) & ",  " _
                                            & SetInteger(ptnr_parent.Tag) & ",  " _
                                            & SetSetring(ptnr_start_periode.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetSetring(te_ptnr_ktp.Text) & ",  " _
                                            & SetInteger(le_ptnr_sex.EditValue) & ",  " _
                                            & SetInteger(le_ptnr_goldarah.EditValue) & ",  " _
                                            & SetSetring(le_ptnr_birthcity.Text) & ",  " _
                                            & SetDate(de_ptnr_birthday.Text) & ",  " _
                                            & SetInteger(le_ptnr_negara.EditValue) & ",  " _
                                            & SetDate(de_ptnr_bp_date.Text) & ",  " _
                                            & SetInteger(le_ptnr_bp_type.EditValue) & ",  " _
                                            & SetSetring(te_ptnr_waris_name.Text) & ",  " _
                                            & SetSetring(ptnr_marital_status.EditValue) & ",  " _
                                            & SetInteger(ptnr_amount_dependents.EditValue) & ",  " _
                                            & SetInteger(ptnr_sls_emp_pos.EditValue) & ",  " _
                                            & SetInteger(ptnr_sls_emp_cat.EditValue) & ",  " _
                                            & SetInteger(ptnr_sls_emp_lvl.EditValue) & ",  " _
                                            & SetSetring(te_ptnr_waris_ktp.Text) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)

                        'Clipboard.SetText(Clipboard.GetText & " >> " & .Command.CommandText)

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnra_addr " _
                                                & "( " _
                                                & "  ptnra_oid, " _
                                                & "  ptnra_id, " _
                                                & "  ptnra_dom_id, " _
                                                & "  ptnra_en_id, " _
                                                & "  ptnra_add_by, " _
                                                & "  ptnra_add_date, " _
                                                & "  ptnra_line, " _
                                                & "  ptnra_line_1, " _
                                                & "  ptnra_line_2, " _
                                                & "  ptnra_line_3, " _
                                                & "  ptnra_phone_1, " _
                                                & "  ptnra_phone_2, " _
                                                & "  ptnra_fax_1, " _
                                                & "  ptnra_fax_2, " _
                                                & "  ptnra_zip,ptnra_city, " _
                                                & "  ptnra_ptnr_oid, " _
                                                & "  ptnra_addr_type, " _
                                                & "  ptnra_comment, " _
                                                & "  ptnra_active, " _
                                                & "  ptnra_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString) & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", sc_le_ptnr_en_id.EditValue.ToString)) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", _ptnr_oid.ToString)) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_2")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_3")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_2")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_1")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_2")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_zip")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_city")) & ",  " _
                                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                & SetInteger(ds_edit_address.Tables(0).Rows(i).Item("ptnra_addr_type")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_comment")) & ",  " _
                                                & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_active").ToString.ToUpper) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)

                            'Clipboard.SetText(Clipboard.GetText & " >> " & .Command.CommandText)

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")).Length < 10 Then
                                sqlTran.Rollback()
                                MessageBox.Show("Silahkan isi alamat, minimal kota dan propinsi, jangan - atau titik.")
                                insert = False
                                Return False
                                Exit Function
                            End If

                        Next

                        For i = 0 To ds_edit_cp.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnrac_cntc " _
                                                & "( " _
                                                & "  ptnrac_oid, " _
                                                & "  addrc_ptnra_oid, " _
                                                & "  ptnrac_add_by, " _
                                                & "  ptnrac_add_date, " _
                                                & "  ptnrac_seq, " _
                                                & "  ptnrac_function, " _
                                                & "  ptnrac_contact_name, " _
                                                & "  ptnrac_phone_1, " _
                                                & "  ptnrac_phone_2, " _
                                                & "  ptnrac_email, " _
                                                & "  ptnrac_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & SetInteger(GetNewID("ptnrac_cntc", "ptnrac_seq", "addrc_ptnra_oid", ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid").ToString)) & ",  " _
                                                & SetInteger(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_function")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_contact_name")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_1")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_2")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_email")) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            'Clipboard.SetText(Clipboard.GetText & " >> " & .Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Partner Complete " & _ptnr_code)
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
                        set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message & " partner id : " & _ptnr_id)
                        insert = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message & " partner id : " & _ptnr_id)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            sc_le_ptnr_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position
            _delete_rows.Clear()
            With ds.Tables(0).Rows(row)
                _ptnr_id_edit = .Item("ptnr_id")
                _ptnr_oid = .Item("ptnr_oid").ToString
                sc_le_ptnr_en_id.EditValue = .Item("ptnr_en_id")


                sc_le_ptnr_en_id.Enabled = False

                If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = True Then
                    'MessageBox.Show("Disable Authorization cancel...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Exit Function
                    sc_le_ptnr_en_id.Enabled = True
                End If

                sc_le_ptnr_ptnrg_id.EditValue = .Item("ptnr_ptnrg_id")
                sc_le_ptnr_ptnrc_id.EditValue = .Item("ptnr_ptnrc_id")
                sc_te_ptnr_name.Text = SetString(.Item("ptnr_name"))
                ptnr_user_name.EditValue = .Item("ptnr_user_name")
                ptnr_is_bm.EditValue = SetBitYNB(.Item("ptnr_is_bm"))
                ptnr_no_rek.EditValue = .Item("ptnr_no_rek")
                ptnr_rek_name.EditValue = .Item("ptnr_rek_name")
                ptnr_bank.EditValue = .Item("ptnr_bank")

                ptnr_nik.EditValue = .Item("ptnr_nik")
                ptnr_imei2.EditValue = .Item("ptnr_imei2")

                ptnr_name_alt.Text = SetString(.Item("ptnr_name_alt"))
                sc_te_ptnr_url.Text = SetString(.Item("ptnr_url"))
                ptnr_email.Text = SetString(.Item("ptnr_email"))
                ptnr_npwp.Text = SetString(.Item("ptnr_npwp"))
                ptnr_nppkp.Text = SetString(.Item("ptnr_nppkp"))
                ptnr_address_tax.Text = SetString(.Item("ptnr_address_tax"))
                ptnr_contact_tax.Text = SetString(.Item("ptnr_contact_tax"))
                sc_te_ptnr_remarks.Text = SetString(.Item("ptnr_remarks"))
                sc_ce_ptnr_active.EditValue = SetBitYNB(.Item("ptnr_active"))
                sc_ce_ptnr_is_cust.EditValue = SetBitYNB(.Item("ptnr_is_cust"))
                sc_ce_ptnr_is_vend.EditValue = SetBitYNB(.Item("ptnr_is_vend"))
                sc_ce_ptnr_is_internal.EditValue = SetBitYNB(.Item("ptnr_is_internal"))

                ptnr_is_volunteer.EditValue = SetBitYNB(.Item("ptnr_is_volunteer"))
                ptnr_is_sbm.EditValue = SetBitYNB(.Item("ptnr_is_sbm"))

                ptnr_is_member.EditValue = SetBitYNB(.Item("ptnr_is_member"))
                ptnr_is_ps.EditValue = SetBitYNB(.Item("ptnr_is_ps"))
                ptnr_is_emp.EditValue = SetBitYNB(.Item("ptnr_is_emp"))
                ptnr_is_writer.EditValue = SetBitYNB(.Item("ptnr_is_writer"))
                ptnr_ac_ar_id.EditValue = .Item("ptnr_ac_ar_id")
                ptnr_sb_ar_id.EditValue = .Item("ptnr_sb_ar_id")
                ptnr_cc_ar_id.EditValue = .Item("ptnr_cc_ar_id")
                ptnr_ac_ap_id.EditValue = .Item("ptnr_ac_ap_id")
                ptnr_sb_ap_id.EditValue = .Item("ptnr_sb_ap_id")
                ptnr_cc_ap_id.EditValue = .Item("ptnr_cc_ap_id")
                ptnr_cu_id.EditValue = .Item("ptnr_cu_id")
                ptnr_limit_credit.EditValue = .Item("ptnr_limit_credit")
                ptnr_is_ps.EditValue = SetBitYNB(.Item("ptnr_is_ps"))
                ptnr_sls_emp_lvl.EditValue = .Item("ptnr_lvl_id")
                ptnr_parent.Tag = .Item("ptnr_parent")
                ptnr_parent.EditValue = .Item("parent_name")
                ptnr_start_periode.EditValue = .Item("ptnr_start_periode")
                ptnr_imei.EditValue = .Item("ptnr_imei")
                ptnr_lvl_mob_user.EditValue = .Item("ptnr_lvl_mob_user")

                ptnr_sls_emp_pos.EditValue = .Item("ptnr_sls_emp_pos")
                ptnr_sls_emp_cat.EditValue = .Item("ptnr_sls_emp_cat")
                ptnr_sls_emp_lvl.EditValue = .Item("ptnr_sls_emp_lvl")

                te_ptnr_ktp.Text = SetString(.Item("ptnr_ktp"))
                le_ptnr_sex.EditValue = .Item("ptnr_sex")
                le_ptnr_goldarah.EditValue = .Item("ptnr_goldarah")
                le_ptnr_birthcity.Text = SetString(.Item("ptnr_birthcity"))
                de_ptnr_birthday.EditValue = SetString(.Item("ptnr_birthday"))
                le_ptnr_negara.EditValue = .Item("ptnr_negara")
                de_ptnr_bp_date.EditValue = SetString(.Item("ptnr_bp_date"))
                le_ptnr_bp_type.EditValue = .Item("ptnr_bp_type")
                te_ptnr_waris_name.Text = SetString(.Item("ptnr_waris_name"))
                te_ptnr_waris_ktp.Text = SetString(.Item("ptnr_waris_ktp"))

                ptnr_marital_status.EditValue = .Item("ptnr_marital_status")
                ptnr_amount_dependents.EditValue = .Item("ptnr_amount_dependents")

                ptnr_sales_code.EditValue = .Item("ptnr_sales_code")
                ptnr_customer_id.EditValue = .Item("ptnr_customer_id")

                If IsDBNull(.Item("ptnr_transaction_code_id")) = True Then
                    ptnr_transaction_code_id.ItemIndex = 0
                Else
                    ptnr_transaction_code_id.EditValue = .Item("ptnr_transaction_code_id")
                End If

                If func_coll.get_conf_file("limit_status_partner_edit") = "1" Then
                    If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                        ptnr_is_ps.Enabled = False
                        ptnr_parent.Enabled = False
                        btClear.Enabled = False
                    Else
                        ptnr_is_ps.Enabled = True
                        ptnr_parent.Enabled = True
                        btClear.Enabled = True
                    End If
                Else
                    ptnr_is_ps.Enabled = True
                    ptnr_parent.Enabled = True
                    btClear.Enabled = True
                End If
            End With

            ds_edit_address = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.ptnra_oid, " _
                            & "  a.ptnra_id, " _
                            & "  a.ptnra_dom_id, " _
                            & "  a.ptnra_en_id, " _
                            & "  a.ptnra_add_by, " _
                            & "  a.ptnra_add_date, " _
                            & "  a.ptnra_upd_by, " _
                            & "  a.ptnra_upd_date, " _
                            & "  a.ptnra_line, " _
                            & "  a.ptnra_line_1, " _
                            & "  a.ptnra_line_2, " _
                            & "  a.ptnra_line_3, " _
                            & "  a.ptnra_phone_1, " _
                            & "  a.ptnra_phone_2, " _
                            & "  a.ptnra_fax_1, " _
                            & "  a.ptnra_fax_2, " _
                            & "  a.ptnra_zip,ptnra_city, " _
                            & "  a.ptnra_ptnr_oid, " _
                            & "  a.ptnra_addr_type, " _
                            & "  a.ptnra_comment, " _
                            & "  a.ptnra_active, " _
                            & "  a.ptnra_dt, " _
                            & "  b.dom_desc, " _
                            & "  c.en_desc, " _
                            & "  code_name as address_type,ptnra_customer_id, " _
                            & "  public.ptnr_mstr.ptnr_name " _
                            & "FROM " _
                            & "  public.ptnra_addr a " _
                            & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
                            & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
                            & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
                            & " where ptnra_ptnr_oid = '" + _ptnr_oid + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_address, "address")
                        gc_edit_address.DataSource = ds_edit_address.Tables(0)
                        gv_edit_address.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_cp = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.ptnrac_oid, " _
                            & "  a.addrc_ptnra_oid, " _
                            & "  a.ptnrac_add_by, " _
                            & "  a.ptnrac_add_date, " _
                            & "  a.ptnrac_seq, " _
                            & "  a.ptnrac_function, " _
                            & "  a.ptnrac_contact_name, " _
                            & "  a.ptnrac_phone_1, " _
                            & "  a.ptnrac_phone_2, " _
                            & "  a.ptnrac_email, " _
                            & "  a.ptnrac_dt, " _
                            & "  b.ptnra_line, " _
                            & "  code_name as ptnrac_function_name, " _
                            & "  ptnra_ptnr_oid, " _
                            & "  ptnr_name " _
                            & "FROM " _
                            & "  public.ptnrac_cntc a " _
                            & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                            & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                            & "  Inner join public.code_mstr on code_id = ptnrac_function " _
                            & " where ptnr_oid = '" + _ptnr_oid + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit_cp, "contactperson")
                        gc_edit_cp.DataSource = ds_edit_cp.Tables(0)
                        gv_edit_cp.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim _ptnr_code As String = ""
        Dim _ptnr_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id")
        Dim i As Integer
        Dim ssqls As New ArrayList

        If sc_ce_ptnr_is_cust.EditValue = True Then
            _ptnr_code = _ptnr_code + "CU"
        End If
        If sc_ce_ptnr_is_vend.EditValue = True Then
            _ptnr_code = _ptnr_code + "SP"
        End If
        If ptnr_is_member.EditValue = True Then
            _ptnr_code = _ptnr_code + "SL"
        End If
        If ptnr_is_emp.EditValue = True Then
            _ptnr_code = _ptnr_code + "EM"
        End If
        If ptnr_is_writer.EditValue = True Then
            _ptnr_code = _ptnr_code + "WR"
        End If

        If Len(_ptnr_code) = 2 Then
            _ptnr_code = _ptnr_code + "00"
        End If

        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(2, Len(_ptnr_id.ToString) - 2)

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = "000000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = "00000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 6 Then
            _ptnr_id_s = "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 7 Then
            _ptnr_id_s = _ptnr_id_s.ToString
        End If

        If SetNumber(ptnr_parent.Tag) = SetNumber(_ptnr_id_edit) Then
            MessageBox.Show("Error parrent")
            Return False
            Exit Function
        End If

        'If _conf_value = "1" Then
        '    If ptnr_email.EditValue = "" Then
        '        MessageBox.Show("Email Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If

        '    If sc_te_ptnr_name.EditValue = "" Then
        '        MessageBox.Show("Name Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If

        'End If


        _ptnr_code = _ptnr_code + IIf(sc_le_ptnr_en_id.GetColumnValue("en_code") = 0, "99", sc_le_ptnr_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        '& "  ptnr_code = " & SetSetring(_ptnr_code) & ",  " _

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.ptnr_mstr  " _
                                                & "SET  " _
                                                & "  ptnr_en_id = " & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                & "  ptnr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ptnr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & "  ptnr_sales_code = " & SetSetring(ptnr_sales_code.Text) & ",  " _
                                                & "  ptnr_customer_id = " & SetSetring(ptnr_customer_id.Text) & ",  " _
                                                & "  ptnr_name = " & SetSetring(sc_te_ptnr_name.Text) & ",  " _
                                                & "  ptnr_kor_name = " & SetSetring(sc_te_ptnr_name.EditValue) & ",  " _
                                                & "  ptnr_user_name = " & SetSetring(ptnr_user_name.Text) & ",  " _
                                                & "  ptnr_is_bm = " & SetBitYN(ptnr_is_bm.EditValue) & ",  " _
                                                & "  ptnr_name_alt = " & SetSetring(ptnr_name_alt.Text) & ",  " _
                                                & "  ptnr_amount_dependents = " & SetInteger(ptnr_amount_dependents.EditValue) & ",  " _
                                                & "  ptnr_marital_status = " & SetSetring(ptnr_marital_status.EditValue) & ",  " _
                                                & "  ptnr_bank = " & SetSetring(ptnr_bank.Text) & ",  " _
                                                & "  ptnr_no_rek = " & SetSetring(ptnr_no_rek.Text) & ",  " _
                                                & "  ptnr_rek_name = " & SetSetring(ptnr_rek_name.Text) & ",  " _
                                                & "  ptnr_imei2 = " & SetSetring(ptnr_imei2.EditValue) & ",  " _
                                                & "  ptnr_nik = " & SetSetring(ptnr_nik.EditValue) & ",  " _
                                                & "  ptnr_ptnrg_id = " & SetInteger(sc_le_ptnr_ptnrg_id.EditValue) & ",  " _
                                                & "  ptnr_ptnrc_id = " & SetInteger(sc_le_ptnr_ptnrc_id.EditValue) & ",  " _
                                                & "  ptnr_url = " & SetSetring(sc_te_ptnr_url.Text) & ",ptnr_process='N',  " _
                                                & "  ptnr_email = " & SetSetring(ptnr_email.Text) & ",  " _
                                                & "  ptnr_npwp = " & SetSetring(ptnr_npwp.Text) & ",  " _
                                                & "  ptnr_address_tax = " & SetSetring(ptnr_address_tax.Text) & ",  " _
                                                & "  ptnr_contact_tax = " & SetSetring(ptnr_contact_tax.Text) & ",  " _
                                                & "  ptnr_nppkp = " & SetSetring(ptnr_nppkp.Text) & ",  " _
                                                & "  ptnr_transaction_code_id = " & SetInteger(ptnr_transaction_code_id.EditValue) & ",  " _
                                                & "  ptnr_remarks = " & SetSetring(sc_te_ptnr_remarks.Text) & ",  " _
                                                & "  ptnr_imei = " & SetSetring(ptnr_imei.Text) & ",  " _
                                                & "  ptnr_lvl_mob_user = " & SetInteger(ptnr_lvl_mob_user.EditValue) & ",  " _
                                                & "  ptnr_sls_emp_pos = " & SetInteger(ptnr_sls_emp_pos.EditValue) & ",  " _
                                                & "  ptnr_sls_emp_cat = " & SetInteger(ptnr_sls_emp_cat.EditValue) & ",  " _
                                                & "  ptnr_sls_emp_lvl = " & SetInteger(ptnr_sls_emp_lvl.EditValue) & ",  " _
                                                & "  ptnr_is_cust = " & SetBitYN(sc_ce_ptnr_is_cust.EditValue) & ",  " _
                                                & "  ptnr_is_vend = " & SetBitYN(sc_ce_ptnr_is_vend.EditValue) & ",  " _
                                                & "  ptnr_is_internal = " & SetBitYN(sc_ce_ptnr_is_internal.EditValue) & ",  " _
                                                & "  ptnr_is_member = " & SetBitYN(ptnr_is_member.EditValue) & ",  " _
                                                & "  ptnr_is_sbm = " & SetBitYN(ptnr_is_sbm.EditValue) & ",  " _
                                                & "  ptnr_is_volunteer = " & SetBitYN(ptnr_is_volunteer.EditValue) & ",  " _
                                                & "  ptnr_is_ps = " & SetBitYN(ptnr_is_ps.EditValue) & ",  " _
                                                & "  ptnr_is_emp = " & SetBitYN(ptnr_is_emp.EditValue) & ",  " _
                                                & "  ptnr_is_writer = " & SetBitYN(ptnr_is_writer.EditValue) & ",  " _
                                                & "  ptnr_active = " & SetBitYN(sc_ce_ptnr_active.EditValue) & ",  " _
                                                & "  ptnr_ac_ar_id = " & SetInteger(ptnr_ac_ar_id.EditValue) & ",  " _
                                                & "  ptnr_sb_ar_id = " & SetInteger(ptnr_sb_ar_id.EditValue) & ",  " _
                                                & "  ptnr_cc_ar_id = " & SetInteger(ptnr_cc_ar_id.EditValue) & ",  " _
                                                & "  ptnr_ac_ap_id = " & SetInteger(ptnr_ac_ap_id.EditValue) & ",  " _
                                                & "  ptnr_sb_ap_id = " & SetInteger(ptnr_sb_ap_id.EditValue) & ",  " _
                                                & "  ptnr_cc_ap_id = " & SetInteger(ptnr_cc_ap_id.EditValue) & ",  " _
                                                & "  ptnr_parent = " & SetInteger(ptnr_parent.Tag) & ",  " _
                                                & "  ptnr_lvl_id = " & SetInteger(ptnr_sls_emp_lvl.EditValue) & ",  " _
                                                & "  ptnr_cu_id = " & SetInteger(ptnr_cu_id.EditValue) & ",  " _
                                                & "  ptnr_ktp =  " & SetSetring(te_ptnr_ktp.Text) & ", " _
                                                & "  ptnr_sex =  " & SetInteger(le_ptnr_sex.EditValue) & ", " _
                                                & "  ptnr_goldarah = " & SetInteger(le_ptnr_goldarah.EditValue) & ",  " _
                                                & "  ptnr_birthcity = " & SetSetring(le_ptnr_birthcity.Text) & ",  " _
                                                & "  ptnr_birthday = " & SetDate(de_ptnr_birthday.Text) & ",  " _
                                                & "  ptnr_negara = " & SetInteger(le_ptnr_negara.EditValue) & ",  " _
                                                & "  ptnr_bp_date = " & SetDate(de_ptnr_bp_date.Text) & ",  " _
                                                & "  ptnr_bp_type = " & SetInteger(le_ptnr_bp_type.EditValue) & ",  " _
                                                & "  ptnr_waris_name = " & SetSetring(te_ptnr_waris_name.Text) & ",  " _
                                                & "  ptnr_waris_ktp = " & SetSetring(te_ptnr_waris_ktp.Text) & ", " _
                                                & "  ptnr_start_periode = " & SetSetring(ptnr_start_periode.EditValue) & ",  " _
                                                & "  ptnr_limit_credit = " & SetDbl(ptnr_limit_credit.EditValue) & ",  " _
                                                & "  ptnr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  ptnr_oid = '" & _ptnr_oid & "' "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        For Each _baris As String In _delete_rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ptnra_addr where ptnra_oid = '" + _baris + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next



                        For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1

                            If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_id")) = "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.ptnra_addr " _
                                                        & "( " _
                                                        & "  ptnra_oid, " _
                                                        & "  ptnra_id, " _
                                                        & "  ptnra_dom_id, " _
                                                        & "  ptnra_en_id, " _
                                                        & "  ptnra_add_by, " _
                                                        & "  ptnra_add_date, " _
                                                        & "  ptnra_line, " _
                                                        & "  ptnra_line_1, " _
                                                        & "  ptnra_line_2, " _
                                                        & "  ptnra_line_3, " _
                                                        & "  ptnra_phone_1, " _
                                                        & "  ptnra_phone_2, " _
                                                        & "  ptnra_fax_1, " _
                                                        & "  ptnra_fax_2, " _
                                                        & "  ptnra_zip,ptnra_city, " _
                                                        & "  ptnra_ptnr_oid, " _
                                                        & "  ptnra_addr_type, " _
                                                        & "  ptnra_comment, " _
                                                        & "  ptnra_active, " _
                                                        & "  ptnra_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString) & ",  " _
                                                        & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", sc_le_ptnr_en_id.EditValue.ToString)) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                        & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", _ptnr_oid.ToString)) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_2")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_3")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_2")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_1")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_2")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_zip")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_city")) & ",  " _
                                                        & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                        & SetInteger(ds_edit_address.Tables(0).Rows(i).Item("ptnra_addr_type")) & ",  " _
                                                        & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_comment")) & ",  " _
                                                        & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_active").ToString.ToUpper) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            Else


                                .Command.CommandText = "UPDATE  " _
                                                        & "  public.ptnra_addr  " _
                                                        & "SET  " _
                                                        & "  ptnra_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "  ptnra_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                        & "  ptnra_line_1 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) & ",  " _
                                                        & "  ptnra_line_2 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_2")) & ",  " _
                                                        & "  ptnra_line_3 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_3")) & ",  " _
                                                        & "  ptnra_phone_1 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) & ",  " _
                                                        & "  ptnra_phone_2 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_2")) & ",  " _
                                                        & "  ptnra_fax_1 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_1")) & ",  " _
                                                        & "  ptnra_fax_2 = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_2")) & ",  " _
                                                        & "  ptnra_zip = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_zip")) & ",  " _
                                                        & "  ptnra_addr_type = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_addr_type")) & ",  " _
                                                        & "  ptnra_comment = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_comment")) & ",  " _
                                                        & "  ptnra_active = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_active").ToString.ToUpper) & ",  " _
                                                        & "  ptnra_city = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_city")) & "  " _
                                                        & "WHERE  " _
                                                        & "  ptnra_oid = " & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString) & " "


                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()


                            End If



                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from ptnrac_cntc where addrc_ptnra_oid in (select ptnra_oid from ptnra_addr " + _
                                                                                                " where ptnra_ptnr_oid = '" + _ptnr_oid.ToString + "')"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_cp.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnrac_cntc " _
                                                & "( " _
                                                & "  ptnrac_oid, " _
                                                & "  addrc_ptnra_oid, " _
                                                & "  ptnrac_add_by, " _
                                                & "  ptnrac_add_date, " _
                                                & "  ptnrac_seq, " _
                                                & "  ptnrac_function, " _
                                                & "  ptnrac_contact_name, " _
                                                & "  ptnrac_phone_1, " _
                                                & "  ptnrac_phone_2, " _
                                                & "  ptnrac_email, " _
                                                & "  ptnrac_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & SetInteger(GetNewID("ptnrac_cntc", "ptnrac_seq", "addrc_ptnra_oid", ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid").ToString)) & ",  " _
                                                & SetInteger(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_function")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_contact_name")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_1")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_2")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_email")) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Partner Complete " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_code"))
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
                        set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        Dim ssqls As New ArrayList

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

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ptnr_mstr where ptnr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Partner Complete " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_code"))
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

    Private Sub gv_edit_address_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_address.DoubleClick
        Dim _col As String = gv_edit_address.FocusedColumn.Name
        Dim _row As Integer = gv_edit_address.FocusedRowHandle

        If _col = "address_type" Then
            Dim frm As New FAddressTypeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = sc_le_ptnr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_cp_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_cp.DoubleClick
        Dim _col As String = gv_edit_cp.FocusedColumn.Name
        Dim _row As Integer = gv_edit_cp.FocusedRowHandle

        If _col = "ptnrac_function_name" Then
            Dim frm As New FFunctionCPSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = sc_le_ptnr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_address_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_address.InitNewRow
        With gv_edit_address
            .SetRowCellValue(e.RowHandle, "ptnra_oid", Guid.NewGuid.ToString)
        End With
    End Sub

    Private Sub gv_edit_cp_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_cp.InitNewRow
        With gv_edit_cp
            .SetRowCellValue(e.RowHandle, "ptnrac_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "addrc_ptnra_oid", ds_edit_address.Tables(0).Rows(BindingContext(ds_edit_address.Tables(0)).Position).Item("ptnra_oid").ToString)
        End With
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit_address.UpdateCurrentRow()
        gv_edit_cp.UpdateCurrentRow()

        ds_edit_address.AcceptChanges()
        ds_edit_cp.AcceptChanges()

        Dim i, j As Integer
        i = 0
        j = 0
        Dim _ptnra_oid As String = ""
        Dim _status As Boolean = False

        If ds_edit_address.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Address Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1
            _ptnra_oid = ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString

            If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) = "" Then
                MessageBox.Show("Address Column Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) = "" Then
                MessageBox.Show("Phone Column Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            For j = 0 To ds_edit_cp.Tables(0).Rows.Count - 1
                If SetString(ds_edit_cp.Tables(0).Rows(j).Item("addrc_ptnra_oid")) = _ptnra_oid Then
                    _status = True
                    Exit For
                End If
            Next

            If _status = False Then
                MessageBox.Show("Contact Person Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If _conf_value = "1" Then
            If ptnr_is_ps.EditValue = True And sc_ce_ptnr_active.EditValue = True Then
                If ptnr_email.EditValue = "" Then
                    MessageBox.Show("Email Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

                If sc_te_ptnr_name.EditValue = "" Then
                    MessageBox.Show("Name Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        End If

        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(Nothing, Nothing)
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Dim sql_tambahan As String = ""

            If par_type.EditValue = "A" Then
                sql_tambahan = " "
            ElseIf par_type.EditValue = "V" Then
                sql_tambahan = " and ptnr_is_vend ~~* 'Y' " _
                            & " order by ptnr_name"
            ElseIf par_type.EditValue = "C" Then
                sql_tambahan = " and ptnr_is_cust ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "M" Then
                sql_tambahan = " and ptnr_is_member ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "E" Then
                sql_tambahan = " and ptnr_is_emp ~~* 'Y' " _
                            & " order by ptnr_name"
            ElseIf par_type.EditValue = "W" Then
                sql_tambahan = " and ptnr_is_writer ~~* 'Y' " _
                             & " order by ptnr_name"
            End If

            Try
                ds.Tables("address").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  a.ptnra_oid, " _
                & "  a.ptnra_id, " _
                & "  a.ptnra_dom_id, " _
                & "  a.ptnra_en_id, " _
                & "  a.ptnra_add_by, " _
                & "  a.ptnra_add_date, " _
                & "  a.ptnra_upd_by, " _
                & "  a.ptnra_upd_date, " _
                & "  a.ptnra_line, " _
                & "  a.ptnra_line_1, " _
                & "  a.ptnra_line_2, " _
                & "  a.ptnra_line_3, " _
                & "  a.ptnra_phone_1, " _
                & "  a.ptnra_phone_2, " _
                & "  a.ptnra_fax_1, " _
                & "  a.ptnra_fax_2, " _
                & "  a.ptnra_zip, " _
                & "  a.ptnra_ptnr_oid, " _
                & "  a.ptnra_addr_type, " _
                & "  a.ptnra_comment,ptnra_city, " _
                & "  a.ptnra_active, " _
                & "  a.ptnra_dt, " _
                & "  b.dom_desc, " _
                & "  c.en_desc, " _
                & "  code_name as address_type,ptnra_customer_id, " _
                & "  public.ptnr_mstr.ptnr_name " _
                & "FROM " _
                & "  public.ptnra_addr a " _
                & "  left outer JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
                & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
                & " where ptnra_ptnr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid").ToString & "'"


            sql = sql + sql_tambahan
            load_data_detail(sql, gc_detail_address, "address")

            Try
                ds.Tables("contactperson").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  a.ptnrac_oid, " _
                    & "  a.addrc_ptnra_oid, " _
                    & "  a.ptnrac_add_by, " _
                    & "  a.ptnrac_add_date, " _
                    & "  a.ptnrac_seq, " _
                    & "  a.ptnrac_function, " _
                    & "  a.ptnrac_contact_name, " _
                    & "  a.ptnrac_phone_1, " _
                    & "  a.ptnrac_phone_2, " _
                    & "  a.ptnrac_email, " _
                    & "  a.ptnrac_dt, " _
                    & "  b.ptnra_line, " _
                    & "  code_name as ptnrac_function_name, " _
                    & "  ptnra_ptnr_oid, " _
                    & "  ptnr_name " _
                    & "FROM " _
                    & "  public.ptnrac_cntc a " _
                    & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                    & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                    & "  Inner join public.code_mstr on code_id = ptnrac_function " _
                    & " Where ptnra_ptnr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid").ToString & "'"

            sql = sql + sql_tambahan
            load_data_detail(sql, gc_detail_cp, "contactperson")

            Try
                sql = "select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
                & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
                & " where ptnr_id in " _
                & " ( select menu_id from get_all_child(" _
                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id").ToString _
                & ")) or ptnr_id in (" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id").ToString & ") "

                Dim dt_tree As New DataTable
                dt_tree = GetTableData(sql)

                TreeList1.DataSource = dt_tree
                TreeList1.ExpandAll()
            Catch ex As Exception

            End Try


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub ptnr_parent_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptnr_parent.ButtonClick
        Try
            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = ptnr_parent
            frm.type_form = True
            frm._en_id = sc_le_ptnr_en_id.EditValue
            'frm._is_brach = ptnr_is_bm.EditValue
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub btClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClear.Click
        Try
            ptnr_parent.EditValue = ""
            ptnr_parent.Tag = ""
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub TreeList1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeList1.DoubleClick
        Try
            TreeList1.ExportToXls(master_new.ModFunction.AskSaveAsFile("Excel Files | *.xls"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub BtImportPartnerGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImportPartnerGroup.Click
        Try
            Dim filex As String = ""
            filex = AskOpenFile("Excel Files | *.xls*")

            If filex = "" Then
                Exit Sub
            End If

            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(filex)
            Dim dt As New DataTable


            'Dim ds_import As New DataSet
            'ds_import.ReadXml(filex)
            'Dim _ptnr_id As String = ""

            Dim ssqls As New ArrayList

            For Each dr As DataRow In ds.Tables(0).Rows
                If dr("kode grup") <> "" Then
                    ssql = "update ptnr_mstr set ptnr_ptnrg_id=(select ptnrg_id from ptnrg_grp where ptnrg_code='" & dr("kode grup") & "' limit 1) " _
                       & " where ptnr_code ='" & dr("Kode Agen") & "'"

                    ssqls.Add(ssql)
                End If

            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If

            Box("Import success")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtCheckParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCheckParent.Click
        Try
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If SetNumber(ds.Tables(0).Rows(i).Item("ptnr_id")) = SetNumber(ds.Tables(0).Rows(i).Item("ptnr_parent")) Then
                    Box("Found error parent " & ds.Tables(0).Rows(i).Item("ptnr_name"))
                    BindingContext(ds.Tables(0)).Position = i
                    Exit For
                End If
            Next
            Box("Check finish")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtCheckID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCheckID.Click
        Try
            Box(SetInteger(GetID_Local(sc_le_ptnr_en_id.GetColumnValue("en_code"))))
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PtnrImport.Click
        Try
            Dim filex As String = ""
            filex = AskOpenFile("Excel Files | *.xls*")

            If filex = "" Then
                Exit Sub
            End If

            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(filex)
            Dim dt As New DataTable


            'Dim ds_import As New DataSet
            'ds_import.ReadXml(filex)
            'Dim _ptnr_id As String = ""
            Dim dr_entity As DataRow
            Dim ssqls As New ArrayList
            Dim x As Integer = 1
            For Each dr As DataRow In ds.Tables(0).Rows
                If SetString(dr("ptnr_code")) <> "" And SetString(dr("en_desc")) <> "" Then
                    dr_entity = GetRowInfo("select * from en_mstr where en_desc='" & dr("en_desc") & "'")

                    If dr_entity Is Nothing Then
                        Box("Please check entity , rows : " & x)
                        Exit Sub
                    End If
                End If
                x = x + 1
            Next

            x = 1
            For Each dr As DataRow In ds.Tables(0).Rows
                If SetString(dr("ptnr_code")) <> "" And SetString(dr("en_desc")) <> "" Then
                    'ssql = "update ptnr_mstr set ptnr_ptnrg_id=(select ptnrg_id from ptnrg_grp where ptnrg_code='" & dr("kode grup") & "' limit 1) " _
                    '   & " where ptnr_code ='" & dr("Kode Agen") & "'"

                    'ssqls.Add(ssql)

                    dr_entity = GetRowInfo("select * from en_mstr where en_desc='" & dr("en_desc") & "'")

                    If dr_entity Is Nothing Then
                        Box("Please check entity")
                        Exit Sub
                    End If

                    ssql = "SELECT count(*) as jml from ptnr_mstr where ptnr_code='" & dr("ptnr_code") & "'"

                    If GetRowInfo(ssql)(0) = 0 Then


                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran



                                    ssqls.Clear()
                                    Dim _ptnr_oid As Guid
                                    _ptnr_oid = Guid.NewGuid

                                    'Dim _ptnr_code As String = ""
                                    Dim _ptnr_id As Long

                                    '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", sc_le_ptnr_en_id.EditValue.ToString))
                                    _ptnr_id = SetInteger(GetID_Local(dr_entity("en_code")))

                                    'Clipboard.SetText(_ptnr_id)
                                    'If dr("ptnr_is_cust") = "Y" Then
                                    '    _ptnr_code = _ptnr_code + "CU"
                                    'End If
                                    'If dr("ptnr_is_vend") = "Y" Then
                                    '    _ptnr_code = _ptnr_code + "SP"
                                    'End If
                                    'If dr("ptnr_is_member") = "Y" Then
                                    '    _ptnr_code = _ptnr_code + "SL"
                                    'End If
                                    'If dr("ptnr_is_emp") = "Y" Then
                                    '    _ptnr_code = _ptnr_code + "EM"
                                    'End If
                                    'If dr("ptnr_is_writer") = "Y" Then
                                    '    _ptnr_code = _ptnr_code + "WR"
                                    'End If

                                    'If Len(_ptnr_code) = 2 Then
                                    '    _ptnr_code = _ptnr_code + "00"
                                    'End If

                                    'Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(4, Len(_ptnr_id.ToString) - 4)


                                    'If Len(_ptnr_id_s) = 1 Then
                                    '    _ptnr_id_s = master_new.ClsVar.sServerCode + "0000" + _ptnr_id_s.ToString
                                    'ElseIf Len(_ptnr_id_s) = 2 Then
                                    '    _ptnr_id_s = master_new.ClsVar.sServerCode + "000" + _ptnr_id_s.ToString
                                    'ElseIf Len(_ptnr_id_s) = 3 Then
                                    '    _ptnr_id_s = master_new.ClsVar.sServerCode + "00" + _ptnr_id_s.ToString
                                    'ElseIf Len(_ptnr_id_s) = 4 Then
                                    '    _ptnr_id_s = master_new.ClsVar.sServerCode + "0" + _ptnr_id_s.ToString
                                    'ElseIf Len(_ptnr_id_s) = 5 Then
                                    '    _ptnr_id_s = master_new.ClsVar.sServerCode + _ptnr_id_s.ToString
                                    'End If

                                    '_ptnr_code = _ptnr_code + IIf(dr_entity("en_code") = 0, "99", dr_entity("en_code")) + _ptnr_id_s.ToString


                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.ptnr_mstr " _
                                                        & "( " _
                                                        & "  ptnr_oid, " _
                                                        & "  ptnr_dom_id, " _
                                                        & "  ptnr_en_id, " _
                                                        & "  ptnr_add_by, " _
                                                        & "  ptnr_add_date, " _
                                                        & "  ptnr_id, " _
                                                        & "  ptnr_code,ptnr_address_tax,ptnr_contact_tax,ptnr_nik,ptnr_imei2, " _
                                                        & "  ptnr_name, " _
                                                        & "  ptnr_name_alt, " _
                                                        & "  ptnr_ptnrg_id, " _
                                                        & "  ptnr_url, " _
                                                        & "  ptnr_email, " _
                                                        & "  ptnr_npwp, " _
                                                        & "  ptnr_nppkp, " _
                                                        & "  ptnr_remarks, " _
                                                        & "  ptnr_is_cust, " _
                                                        & "  ptnr_is_vend, " _
                                                        & "  ptnr_is_internal, " _
                                                        & "  ptnr_is_member, " _
                                                        & "  ptnr_is_emp, " _
                                                        & "  ptnr_is_writer, " _
                                                        & "  ptnr_ac_ar_id, " _
                                                        & "  ptnr_sb_ar_id, " _
                                                        & "  ptnr_cc_ar_id, " _
                                                        & "  ptnr_ac_ap_id, " _
                                                        & "  ptnr_sb_ap_id, " _
                                                        & "  ptnr_cc_ap_id,ptnr_imei, " _
                                                        & "  ptnr_cu_id,ptnr_bank,ptnr_no_rek,ptnr_rek_name, " _
                                                        & "  ptnr_limit_credit,ptnr_user_name,ptnr_is_bm, " _
                                                        & "  ptnr_active,ptnr_is_volunteer,ptnr_is_sbm, " _
                                                        & "  ptnr_transaction_code_id,ptnr_is_ps,ptnr_lvl_id,ptnr_parent,ptnr_start_periode, " _
                                                        & "  ptnr_dt, " _
                                                        & "  ptnr_ktp, " _
                                                        & "  ptnr_sex, " _
                                                        & "  ptnr_goldarah, " _
                                                        & "  ptnr_birthcity, " _
                                                        & "  ptnr_birthday, " _
                                                        & "  ptnr_negara, " _
                                                        & "  ptnr_bp_date, " _
                                                        & "  ptnr_bp_type, " _
                                                        & "  ptnr_waris_name,ptnr_marital_status,ptnr_amount_dependents, " _
                                                        & "  ptnr_waris_ktp " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetSetring(dr_entity("en_id")) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                        & _ptnr_id & ",  " _
                                                        & SetSetring(dr("ptnr_code")) & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring(dr("ptnr_name")) & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & "null" & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring(dr("ptnr_is_cust")) & ",  " _
                                                        & SetSetring(dr("ptnr_is_vend")) & ",  " _
                                                        & SetSetring(dr("ptnr_is_internal")) & ",  " _
                                                        & SetSetring(dr("ptnr_is_member")) & ",  " _
                                                        & SetSetring(dr("ptnr_is_emp")) & ",  " _
                                                        & SetSetring(dr("ptnr_is_writer")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & SetSetring("Y") & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring("") & "  " _
                                                        & ")"

                                    ssqls.Add(.Command.CommandText)

                                    'Clipboard.SetText(Clipboard.GetText & " >> " & .Command.CommandText)

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()


                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.ptnra_addr " _
                                                        & "( " _
                                                        & "  ptnra_oid, " _
                                                        & "  ptnra_id, " _
                                                        & "  ptnra_dom_id, " _
                                                        & "  ptnra_en_id, " _
                                                        & "  ptnra_add_by, " _
                                                        & "  ptnra_add_date, " _
                                                        & "  ptnra_line, " _
                                                        & "  ptnra_line_1, " _
                                                        & "  ptnra_line_2, " _
                                                        & "  ptnra_line_3, " _
                                                        & "  ptnra_phone_1, " _
                                                        & "  ptnra_phone_2, " _
                                                        & "  ptnra_fax_1, " _
                                                        & "  ptnra_fax_2, " _
                                                        & "  ptnra_zip, " _
                                                        & "  ptnra_ptnr_oid, " _
                                                        & "  ptnra_addr_type, " _
                                                        & "  ptnra_comment, " _
                                                        & "  ptnra_active, " _
                                                        & "  ptnra_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(func_coll.GetID("ptnra_addr", dr_entity("en_code"), "ptnra_id", "ptnra_en_id", dr_entity("en_id"))) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(dr_entity("en_id")) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                        & SetInteger(1) & ",  " _
                                                        & SetSetringDB(dr("ptnra_line_1")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_line_2")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_line_3")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_phone_1")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_phone_2")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_fax_1")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_fax_2")) & ",  " _
                                                        & SetSetringDB(dr("ptnra_zip")) & ",  " _
                                                        & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                        & SetInteger(992) & ",  " _
                                                        & SetSetringDB("") & ",  " _
                                                        & SetSetring("Y") & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                        & ")"
                                    ssqls.Add(.Command.CommandText)

                                    'Clipboard.SetText(Clipboard.GetText & " >> " & .Command.CommandText)

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.ptnrac_cntc " _
                                                        & "( " _
                                                        & "  ptnrac_oid, " _
                                                        & "  addrc_ptnra_oid, " _
                                                        & "  ptnrac_add_by, " _
                                                        & "  ptnrac_add_date, " _
                                                        & "  ptnrac_seq, " _
                                                        & "  ptnrac_function, " _
                                                        & "  ptnrac_contact_name, " _
                                                        & "  ptnrac_phone_1, " _
                                                        & "  ptnrac_phone_2, " _
                                                        & "  ptnrac_email, " _
                                                        & "  ptnrac_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetSetring("") & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                        & SetInteger(1) & ",  " _
                                                        & SetInteger(9945) & ",  " _
                                                        & SetSetringDB("-") & ",  " _
                                                        & SetSetringDB("") & ",  " _
                                                        & SetSetringDB("") & ",  " _
                                                        & SetSetringDB("") & ",  " _
                                                        & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                        & ")"
                                    ssqls.Add(.Command.CommandText)
                                    'Clipboard.SetText(Clipboard.GetText & " >> " & .Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()




                                    'Dim i As Integer
                                    'For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1

                                    'If SetString(dr("ptnra_line_1")) <> "" Then




                                    'End If

                                    'If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")).Length < 10 Then
                                    '    sqlTran.Rollback()
                                    '    MessageBox.Show("Silahkan isi alamat, minimal kota dan propinsi, jangan - atau titik.")
                                    '    'insert = False
                                    '    'Return False
                                    '    Exit Sub
                                    'End If

                                    'Next

                                    'Dim i As Integer
                                    'For i = 0 To ds_edit_cp.Tables(0).Rows.Count - 1

                                    'Next

                                    '.Command.CommandType = CommandType.Text
                                    '.Command.CommandText = insert_log("Insert Partner Complete " & _ptnr_code)
                                    'ssqls.Add(.Command.CommandText)
                                    '.Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    If master_new.PGSqlConn.status_sync = True Then
                                        For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = Data
                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
                                        Next
                                    End If

                                    sqlTran.Commit()
                                    'after_success()
                                    'set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
                                    'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                                    'insert = True

                                    PtnrImport.Text = x & " of " & ds.Tables(0).Rows.Count
                                    System.Windows.Forms.Application.DoEvents()
                                Catch ex As nPgSqlException
                                    sqlTran.Rollback()
                                    MessageBox.Show(ex.Message)

                                    Exit Sub
                                Finally

                                End Try

                            End With
                            objinsert.Dispose()
                        End Using

                    End If


                End If
                x = x + 1
            Next

            PtnrImport.Text = "Import Partner"
            'If master_new.PGSqlConn.status_sync = True Then
            '    If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

            '        Exit Sub
            '    End If
            '    ssqls.Clear()
            'Else
            '    If DbRunTran(ssqls, "") = False Then

            '        Exit Sub
            '    End If
            '    ssqls.Clear()
            'End If

            Box("Import success")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub BtImportPtnr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImportPtnr.Click
        Try
            Dim filex As String = ""
            filex = AskOpenFile("Excel Files | *.xls*")
            Dim _error As String = ""
            If filex = "" Then
                Exit Sub
            End If

            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(filex)
            Dim dt As New DataTable


            'Dim ds_import As New DataSet
            'ds_import.ReadXml(filex)
            'Dim _ptnr_id As String = ""
            Dim jsonstring As String

            Dim dr_entity As DataRow
            Dim ssqls As New ArrayList
            Dim x As Integer = 1
            Dim _count As Integer = ds.Tables(0).Rows.Count

            BtImportPtnr.Enabled = False

            For Each dr As DataRow In ds.Tables(0).Rows
                'If SetString(dr("ptnr_customer_id")) = "" Then
                '    Box("Please check customer id , rows : " & x)
                '    Exit Sub
                'End If

                'If SetString(dr("ptnra_customer_id")) = "" Then
                '    Box("Please check customer address id , rows : " & x)
                '    Exit Sub
                'End If

                If SetString(dr("ptnr_customer_id")) <> "" And SetString(dr("ptnr_en_code")) <> "" Then
                    dr_entity = GetRowInfo("select * from en_mstr where en_code='" & dr("ptnr_en_code") & "'")

                    If dr_entity Is Nothing Then
                        Box("Please check entity , rows : " & x)
                        Exit Sub
                    End If
                End If
                x = x + 1
            Next

            x = 1
            For Each dr As DataRow In ds.Tables(0).Rows

                LabelControl1.Text = x & " of " & _count
                System.Windows.Forms.Application.DoEvents()
                If SetString(dr("ptnr_customer_id")) <> "" And SetString(dr("ptnr_en_code")) <> "" Then


                    'dr_entity = GetRowInfo("select * from en_mstr where en_desc='" & dr("en_desc") & "'")

                    'If dr_entity Is Nothing Then
                    '    Box("Please check entity")
                    '    Exit Sub
                    'End If

                    ssql = "SELECT count(*) as jml from ptnr_mstr where ptnr_customer_id='" & dr("ptnr_customer_id") & "'"

                    If GetRowInfo(ssql)(0) = 0 Then

                        'jsonstring = "{""ptnr_name"": """ & SetStringJson(dr("ptnr_name")) _
                        '   & """,""ptnr_en_code"":""" & dr("ptnr_en_code") & """,""ptnr_email"": """ & SetStringJson(dr("ptnr_email")) & """," _
                        '   & """ptnr_customer_id: """ & dr("ptnr_customer_id") & """," _
                        '   & """ptnr_url"": """ & "" & """,""ptnr_npwp"": """ & "" _
                        '   & """,""ptnr_remarks"": """ & "" & """," _
                        '   & """address"": [" & "{""ptnra_customer_id"": """ & dr("ptnra_customer_id") _
                        '   & """,""ptnra_line_1"":""" & SetStringJson(dr("ptnra_line_1")) & """,""ptnra_line_2"": """ & SetStringJson(dr("ptnra_line_2")) & """," _
                        '   & """ptnra_line_3"": """ & SetStringJson(dr("ptnra_line_3")) & """," _
                        '   & """ptnra_phone_1"": """ & SetStringJson(dr("ptnra_phone_1")) & """" _
                        '   & ",""ptnra_phone_2"": """ & "" _
                        '   & """,""ptnra_fax_1"": """ & "" _
                        '   & """,""ptnra_fax_2"": """ & "" _
                        '   & """,""ptnra_zip"": """ & SetStringJson(dr("ptnra_zip")) & """}]}"


                        Dim dictData As New Dictionary(Of String, Object)
                        dictData.Add("ptnr_name", dr("ptnr_name"))
                        dictData.Add("ptnr_en_code", dr("ptnr_en_code"))
                        dictData.Add("ptnr_email", dr("ptnr_email"))
                        dictData.Add("ptnr_url", "")
                        dictData.Add("ptnr_remarks", "")
                        dictData.Add("ptnr_npwp", "")
                        dictData.Add("ptnr_customer_id", dr("ptnr_customer_id"))



                        Dim dict As New Dictionary(Of String, Object)
                        dict.Add("ptnra_customer_id", dr("ptnra_customer_id"))
                        dict.Add("ptnra_line_1", dr("ptnra_line_1"))
                        dict.Add("ptnra_line_2", dr("ptnra_line_2"))
                        dict.Add("ptnra_line_3", dr("ptnra_line_3"))
                        dict.Add("ptnra_phone_1", dr("ptnra_phone_1"))
                        dict.Add("ptnra_phone_2", dr("ptnra_phone_2"))
                        dict.Add("ptnra_fax_1", "")
                        dict.Add("ptnra_fax_2", "")
                        dict.Add("ptnra_zip", dr("ptnra_zip"))

                        'jsonstring = jsonstring.Replace(" ", "%20")

                        Dim both = New List(Of Dictionary(Of String, Object))

                        both.Add(dict)

                        dictData.Add("address", both)


                        Dim _url As String
                        Dim _app As String = ""

                        If func_coll.get_conf_file("syspro_approval_code") = "DUTA" Then
                            _app = "DUTA"
                            _url = URL_ROOT & "sdijkt/ptnr_mstr_cus"
                        ElseIf func_coll.get_conf_file("syspro_approval_code") = "SDI" Then
                            _app = "SDI"
                            _url = URL_ROOT & "sdi/ptnr_mstr_cus"
                        Else
                            Exit Sub
                        End If

                        Dim result As String

                        'Dim url As String = func_coll.get_http_server_api() & "php56/json/curl_api_sdi.php?app=" _
                        '    & _app & "&path=ptnr_mstr_cus&json_string=" & jsonstring

                        Try
                            Dim webClient As New WebClient()
                            Dim resByte As Byte()
                            Dim reqByte As Byte()
                            Dim reqString() As Byte
                            Dim resString As String
                            Dim temp_str As String

                            reqString = System.Text.Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
                            temp_str = System.Text.Encoding.Default.GetString(reqString)
                            temp_str = SetString(temp_str)
                            reqString = System.Text.Encoding.Default.GetBytes(temp_str)
                            webClient.Headers("content-type") = "application/json"
                            webClient.Headers("api_token") = "f2a7f94d-79ce-4d4b-b113-55c3b22ef049"
                            webClient.Headers("api_user") = "sdi"
                            webClient.Headers("api_password") = "271fb739ef0d9a027d8596f9f2c521dd"

                            resByte = webClient.UploadData(_url, "post", reqString)
                            resString = System.Text.Encoding.Default.GetString(resByte)
                            result = resString
                            webClient.Dispose()

                            'Exit Sub

                        Catch ex As Exception
                            '_error = _error & ex.Message
                        End Try



                        'BtImportPtnr.Text = "Run api " & x & " of " & _count & " " & dr("ptnr_name") & " start"
                        'System.Windows.Forms.Application.DoEvents()

                        'result = mf.run_get_to_api(url & "")
                        Dim _pb_code_reff As String = ""

                        LabelControl1.Text = x & " of " & _count
                        System.Windows.Forms.Application.DoEvents()
                        Try
                            If SetString(result) <> "" Then
                                Dim result2 As JObject = JObject.Parse(result.ToString)
                                If SetString(result2.GetValue("data").ToString()) <> "" Then
                                    Dim _data As JArray = JArray.Parse(result2.GetValue("data").ToString())
                                    For Each item As JObject In _data
                                        _pb_code_reff = item.GetValue("ptnr_code").ToString()
                                        LabelControl1.Text = x & " of " & _count
                                        System.Windows.Forms.Application.DoEvents()
                                    Next

                                    If _pb_code_reff = "" Then
                                        _error = _error & " " & dr("ptnr_name") & vbNewLine
                                    End If
                                Else
                                    _error = _error & " " & dr("ptnr_name") & vbNewLine
                                End If
                            End If
                        Catch ex As Exception
                            _error = _error & " " & dr("ptnr_name") & vbNewLine

                            'MsgBox(ex.Message & " " & dr("ptnr_name"))
                        End Try

                    End If


                End If
                x = x + 1
            Next

            BtImportPtnr.Enabled = True
            BtImportPtnr.Text = "Import Api"
            System.Windows.Forms.Application.DoEvents()

            Box("Import success")
            If _error <> "" Then
                MsgBox(_error)
                Clipboard.SetText(_error)
            End If
            
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
