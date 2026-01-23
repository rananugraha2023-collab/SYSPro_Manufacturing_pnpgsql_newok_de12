Imports master_new.ModFunction

Public Class FPartnerSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj, _objk, _ptnr_id, _is_internal As Object
    Dim func_data As New function_data
    Dim dt_bantu As DataTable
    Dim _is_brach As Boolean
    Dim _is_cust, _is_vendor As Boolean

    Private Sub FPartnerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        load_cb()
        help_load_data(True)

        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

        'If fobject.name = FPartnerTaxGroup.Name Or fobject.name = FSalesOrder.Name Then
        '    scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
        '    gv_master.Columns("status").Visible = True
        'Else
        '    scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '    gv_master.Columns("status").Visible = False
        'End If
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        par_entity.Properties.DataSource = dt_bantu
        par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        par_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()

        If fobject.name = "FPartnerIntitutionName" Then
            add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            'func_coll.get_conf_file("wf_sales_order")
            add_column(gv_master, "ID", "ptnr_id", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Category", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = "FSalesPlanImportTarget" Then
            add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            'func_coll.get_conf_file("wf_sales_order")
            add_column(gv_master, "ID", "ptnr_id", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Parent", "ptnr_name_parent", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Category", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            'func_coll.get_conf_file("wf_sales_order")
            add_column(gv_master, "ID", "ptnr_id", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        End If


    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        Dim _en_id_coll As String = func_data.entity_parent(_en_id)


        If fobject.name = "FCashOut" Or fobject.name = FVoucher.Name Then
            If CeAllEntity.Checked = True Then
                get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_vend ~~* 'Y' " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            Else
                get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  = " + SetInteger(_en_id) + "" _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_vend ~~* 'Y' " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            End If

        ElseIf fobject.name = FPartnerAll.Name Then
            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  a.ptnr_sex, a.ptnr_ktp," _
                   & "  a.ptnr_goldarah, " _
                   & "  a.ptnr_birthcity, " _
                   & "  a.ptnr_birthday, " _
                   & "  a.ptnr_negara, " _
                   & "  a.ptnr_bp_date, " _
                   & "  a.ptnr_bp_type, " _
                   & "  a.ptnr_waris_name, " _
                   & "  a.ptnr_waris_ktp, " _
                   & "  en_desc " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & " where  ptnr_active ~~* 'Y' and ptnr_en_id  = " & _en_id & " " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

        ElseIf fobject.name = FCanvasingPlanner.Name Or fobject.name = FCanvasingSalesPlanning.Name Then
            get_sequel = "SELECT  false as status, " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, " _
                    & "  en_desc " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & " where ptnr_en_id = " + _en_id.ToString _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_member ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

            'ptnr_en_id  in (" + _en_id_coll + ")" _
            '       & " and

        ElseIf fobject.name = "FCashIn" Or fobject.name = FLocation.Name Or fobject.name = FARMerge.Name Then
            If CeAllEntity.Checked = True Then
                get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                   & " and ptnr_active ~~* 'Y'  " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            Else
                get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                   & " and ptnr_active ~~* 'Y'  " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            End If

        ElseIf fobject.name = "FDRCRMemo" Or fobject.name = "FDRCRMemoDetail" Then
            If func_coll.get_conf_file("partner_so_cross_entity") = "1" And CeAllEntity.Checked Then
                get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where  ptnr_active ~~* 'Y' " _
                   & " and ptnr_is_cust ~~* 'Y' " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            Else
                get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  left outer JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (select user_en_id from tconfuserentity " _
                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                   & " and ptnr_active ~~* 'Y' " _
                   & " and ptnr_is_cust ~~* 'Y' " _
                   & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            End If

        ElseIf fobject.name = FPartnerTaxGroup.Name Then
            get_sequel = "SELECT  false as status, " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " where ptnr_en_id = " + par_entity.EditValue.ToString _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

            'End If
        ElseIf fobject.name = FPWSubmissionRequest.Name Then
            get_sequel = "SELECT  false as status, " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " where ptnr_en_id = " + par_entity.EditValue.ToString _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

        ElseIf fobject.name = FPartnerIntitutionName.Name Then
            get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_ptnrc_id, " _
                    & "  b.ptnrc_name, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, a.ptnr_email," _
                    & "  a.ptnr_sex, a.ptnr_ktp," _
                    & "  a.ptnr_goldarah, " _
                    & "  a.ptnr_birthcity, " _
                    & "  a.ptnr_birthday, " _
                    & "  a.ptnr_negara, " _
                    & "  a.ptnr_bp_date, " _
                    & "  a.ptnr_bp_type, " _
                    & "  a.ptnr_waris_name, " _
                    & "  a.ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & "  LEFT OUTER JOIN public.ptnrc_cat b ON (a.ptnr_ptnrc_id = b.ptnrc_id)" _
                    & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

        ElseIf fobject.name = FPartnerIntitutionName.Name Then
            get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_ptnrc_id, " _
                    & "  b.ptnrc_name, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, a.ptnr_email," _
                    & "  a.ptnr_sex, a.ptnr_ktp," _
                    & "  a.ptnr_goldarah, " _
                    & "  a.ptnr_birthcity, " _
                    & "  a.ptnr_birthday, " _
                    & "  a.ptnr_negara, " _
                    & "  a.ptnr_bp_date, " _
                    & "  a.ptnr_bp_type, " _
                    & "  a.ptnr_waris_name, " _
                    & "  a.ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & "  LEFT OUTER JOIN public.ptnrc_cat b ON (a.ptnr_ptnrc_id = b.ptnrc_id)" _
                    & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' " 

        ElseIf fobject.name = FSalesOrderFakturPenjualanPrint.Name Then
            get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_ptnrc_id, " _
                    & "  b.ptnrc_name, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, a.ptnr_email," _
                    & "  a.ptnr_sex, a.ptnr_ktp," _
                    & "  a.ptnr_goldarah, " _
                    & "  a.ptnr_birthcity, " _
                    & "  a.ptnr_birthday, " _
                    & "  a.ptnr_negara, " _
                    & "  a.ptnr_bp_date, " _
                    & "  a.ptnr_bp_type, " _
                    & "  a.ptnr_waris_name, " _
                    & "  a.ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & "  LEFT OUTER JOIN public.ptnrc_cat b ON (a.ptnr_ptnrc_id = b.ptnrc_id)" _
                    & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

            'ElseIf fobject.name = FPartnerIntitutionName.Name Then
            '    get_sequel = "SELECT  false as status, " _
            '            & "  a.ptnr_oid, " _
            '            & "  a.ptnr_dom_id, " _
            '            & "  a.ptnr_en_id, " _
            '            & "  a.ptnr_id, " _
            '            & "  en_desc, " _
            '            & "  a.ptnr_code, " _
            '            & "  a.ptnr_name, " _
            '            & "  a.ptnr_ptnrc_id, " _
            '            & "  ptnrc_name, " _
            '            & "  ptnr_ac_ar_id " _
            '            & "FROM " _
            '            & "  public.ptnr_mstr a " _
            '            & "  LEFT OUTER JOIN public.ptnrc_cat b ON (a.ptnr_ptnrc_id = b.ptnrc_id)" _
            '            & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
            '            & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            '            & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
            '            & " where ptnr_en_id = " + par_entity.EditValue.ToString _
            '            & " and ptnr_active ~~* 'Y' " _
            '            & " and ptnr_is_cust ~~* 'Y' " _
            '            & " and ptnr_name ~~* '%" & te_name.EditValue & "%' " _
            '            & " order by ptnr_name"

        ElseIf fobject.name = "FSalesQuotation" Or fobject.name = FSalesQuotationConsigment.Name Then
            If func_coll.get_conf_file("partner_so_cross_entity") = "1" And CeAllEntity.Checked Then
                get_sequel = "SELECT  " _
                    & "  ptnr_oid, " _
                    & "  ptnr_dom_id, " _
                    & "  ptnr_en_id, " _
                    & "  ptnr_id, " _
                    & "  ptnr_code, " _
                    & "  ptnr_name, ptnr_email, " _
                    & "  ptnr_sex, ptnr_ktp, " _
                    & "  ptnr_goldarah, " _
                    & "  ptnr_birthcity, " _
                    & "  ptnr_birthday, " _
                    & "  ptnr_negara, " _
                    & "  ptnr_bp_date, " _
                    & "  ptnr_bp_type, " _
                    & "  ptnr_waris_name, " _
                    & "  ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                    & "  coalesce(ptnra_phone_1,'') as ptnra_phone_1, " _
                    & "  coalesce(ptnra_phone_2,'') as ptnra_phone_2, " _
                    & "  coalesce(ptnra_zip,'') as ptnra_zip, " _
                    & "  ptnratt_oid, " _
                    & "  ptnratt_kjb_code, " _
                    & "  ptnratt_bekerja_pada, " _
                    & "  ptnratt_jabatan_bagian, " _
                    & "  ptnratt_kantor_alamat_1, " _
                    & "  ptnratt_kantor_alamat_2, " _
                    & "  ptnratt_kantor_lantai, " _
                    & "  ptnratt_kantor_telp, " _
                    & "  ptnratt_ktp, " _
                    & "  ptnratt_email, " _
                    & "  ptnratt_rumah_alamat_1, " _
                    & "  ptnratt_rumah_alamat_2, " _
                    & "  ptnratt_rumah_kode_pos, " _
                    & "  ptnratt_rumah_telp, " _
                    & "  ptnratt_rumah_hp, " _
                    & "  ptnratt_status_alamat_kirim, " _
                    & "  ptnratt_status_alamat_tagih, " _
                    & "  ptnratt_suami_nama, " _
                    & "  ptnratt_suami_bekerja, " _
                    & "  ptnratt_suami_jabatan, " _
                    & "  ptnratt_suami_kantor_alamat_1, " _
                    & "  ptnratt_suami_kantor_alamat_2, " _
                    & "  ptnratt_suami_telp, " _
                    & "  ptnratt_suami_hp, " _
                    & "  ptnratt_anak_nama_1, " _
                    & "  ptnratt_anak_tgl_lahir_1, " _
                    & "  ptnratt_anak_sekolah_1, " _
                    & "  ptnratt_anak_nama_2, " _
                    & "  ptnratt_anak_tgl_lahir_2, " _
                    & "  ptnratt_anak_sekolah_2, " _
                    & "  ptnratt_anak_nama_3, " _
                    & "  ptnratt_anak_tgl_lahir_3, " _
                    & "  ptnratt_anak_sekolah_3, " _
                    & "  ptnratt_keluarga_dekat_nama, " _
                    & "  ptnratt_keluarga_dekat_alamat_1, " _
                    & "  ptnratt_keluarga_dekat_alamat_2, " _
                    & "  ptnratt_keluarga_dekat_telp, " _
                    & "  ptnratt_keluarga_dekat_hp, " _
                    & "  ptnratt_status_tempat_tinggal, " _
                    & "  ptnratt_jenis_kartu_kredit, " _
                    & "  ptnratt_no_kartu_kredit, " _
                    & "  ptnratt_berlaku_sd, " _
                    & "  ptnratt_dt, " _
                    & "  ptnratt_bank, " _
                    & "  ptnratt_status_rumah_id, " _
                    & "  ptnratt_lama_tinggal_id, " _
                    & "  ptnratt_masa_kerja_id, " _
                    & "  ptnratt_income_id, " _
                    & "  ptnratt_kepribadian_id, " _
                    & "  ptnratt_tanggungan_id, " _
                    & "  ptnratt_jaminan_id, " _
                    & "  sr.code_name as status_rumah, " _
                    & "  lt.code_name as lama_tinggal, " _
                    & "  ms.code_name as masa_kerja, " _
                    & "  ic.code_name as income, " _
                    & "  kp.code_name as kepribadian, " _
                    & "  tg.code_name as tanggungan, " _
                    & "  jm.code_name as jaminan " _
                    & "FROM " _
                    & "  public.ptnr_mstr " _
                    & "  INNER JOIN public.en_mstr ON (ptnr_en_id = en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  LEFT OUTER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                    & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                    & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                    & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                    & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                    & "  LEFT OUTER JOIN public.code_mstr kp on kp.code_id = ptnratt_kepribadian_id " _
                    & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                    & "  LEFT OUTER JOIN public.code_mstr jm on jm.code_id = ptnratt_jaminan_id " _
                    & " where ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            Else
                get_sequel = "SELECT  " _
                    & "  ptnr_oid, " _
                    & "  ptnr_dom_id, " _
                    & "  ptnr_en_id, " _
                    & "  ptnr_id, " _
                    & "  ptnr_code, " _
                    & "  ptnr_name, ptnr_email, " _
                    & "  ptnr_sex, ptnr_ktp, " _
                    & "  ptnr_goldarah, " _
                    & "  ptnr_birthcity, " _
                    & "  ptnr_birthday, " _
                    & "  ptnr_negara, " _
                    & "  ptnr_bp_date, " _
                    & "  ptnr_bp_type, " _
                    & "  ptnr_waris_name, " _
                    & "  ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                    & "  coalesce(ptnra_phone_1,'') as ptnra_phone_1, " _
                    & "  coalesce(ptnra_phone_2,'') as ptnra_phone_2, " _
                    & "  coalesce(ptnra_zip,'') as ptnra_zip, " _
                    & "  ptnratt_oid, " _
                    & "  ptnratt_kjb_code, " _
                    & "  ptnratt_bekerja_pada, " _
                    & "  ptnratt_jabatan_bagian, " _
                    & "  ptnratt_kantor_alamat_1, " _
                    & "  ptnratt_kantor_alamat_2, " _
                    & "  ptnratt_kantor_lantai, " _
                    & "  ptnratt_kantor_telp, " _
                    & "  ptnratt_ktp, " _
                    & "  ptnratt_email, " _
                    & "  ptnratt_rumah_alamat_1, " _
                    & "  ptnratt_rumah_alamat_2, " _
                    & "  ptnratt_rumah_kode_pos, " _
                    & "  ptnratt_rumah_telp, " _
                    & "  ptnratt_rumah_hp, " _
                    & "  ptnratt_status_alamat_kirim, " _
                    & "  ptnratt_status_alamat_tagih, " _
                    & "  ptnratt_suami_nama, " _
                    & "  ptnratt_suami_bekerja, " _
                    & "  ptnratt_suami_jabatan, " _
                    & "  ptnratt_suami_kantor_alamat_1, " _
                    & "  ptnratt_suami_kantor_alamat_2, " _
                    & "  ptnratt_suami_telp, " _
                    & "  ptnratt_suami_hp, " _
                    & "  ptnratt_anak_nama_1, " _
                    & "  ptnratt_anak_tgl_lahir_1, " _
                    & "  ptnratt_anak_sekolah_1, " _
                    & "  ptnratt_anak_nama_2, " _
                    & "  ptnratt_anak_tgl_lahir_2, " _
                    & "  ptnratt_anak_sekolah_2, " _
                    & "  ptnratt_anak_nama_3, " _
                    & "  ptnratt_anak_tgl_lahir_3, " _
                    & "  ptnratt_anak_sekolah_3, " _
                    & "  ptnratt_keluarga_dekat_nama, " _
                    & "  ptnratt_keluarga_dekat_alamat_1, " _
                    & "  ptnratt_keluarga_dekat_alamat_2, " _
                    & "  ptnratt_keluarga_dekat_telp, " _
                    & "  ptnratt_keluarga_dekat_hp, " _
                    & "  ptnratt_status_tempat_tinggal, " _
                    & "  ptnratt_jenis_kartu_kredit, " _
                    & "  ptnratt_no_kartu_kredit, " _
                    & "  ptnratt_berlaku_sd, " _
                    & "  ptnratt_dt, " _
                    & "  ptnratt_bank, " _
                    & "  ptnratt_status_rumah_id, " _
                    & "  ptnratt_lama_tinggal_id, " _
                    & "  ptnratt_masa_kerja_id, " _
                    & "  ptnratt_income_id, " _
                    & "  ptnratt_kepribadian_id, " _
                    & "  ptnratt_tanggungan_id, " _
                    & "  ptnratt_jaminan_id, " _
                    & "  sr.code_name as status_rumah, " _
                    & "  lt.code_name as lama_tinggal, " _
                    & "  ms.code_name as masa_kerja, " _
                    & "  ic.code_name as income, " _
                    & "  kp.code_name as kepribadian, " _
                    & "  tg.code_name as tanggungan, " _
                    & "  jm.code_name as jaminan " _
                    & "FROM " _
                    & "  public.ptnr_mstr " _
                    & "  INNER JOIN public.en_mstr ON (ptnr_en_id = en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  LEFT OUTER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                    & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                    & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                    & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                    & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                    & "  LEFT OUTER JOIN public.code_mstr kp on kp.code_id = ptnratt_kepribadian_id " _
                    & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                    & "  LEFT OUTER JOIN public.code_mstr jm on jm.code_id = ptnratt_jaminan_id " _
                    & " where ptnr_en_id  in (" + _en_id.ToString + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            End If

            'ElseIf fobject.name = FSalesPlan.Name Or fobject.name = FSalesPlanImportNew.Name Or fobject.name = FSalesPlanImportNewTarget.Name Then
            '    If _obj.name = "gv_edit" Then
            '        get_sequel = "SELECT  " _
            '          & "  a.ptnr_oid, " _
            '          & "  a.ptnr_dom_id, " _
            '          & "  a.ptnr_en_id, " _
            '          & "  a.ptnr_id, " _
            '          & "  a.ptnr_code, " _
            '          & "  a.ptnr_name, " _
            '          & "  en_desc, " _
            '          & "  ptnr_ac_ar_id, " _
            '          & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
            '          & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
            '          & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
            '          & "FROM " _
            '          & "  public.ptnr_mstr a " _
            '          & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
            '          & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            '          & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
            '          & " where ptnr_en_id  in (" + _en_id_coll + ") " _
            '          & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y'  " _
            '          & " and ptnr_name ~~* '%" & te_name.EditValue & "%' " _
            '          & " order by ptnr_name"
            '    Else
            '        get_sequel = "SELECT  " _
            '          & "  a.ptnr_oid, " _
            '          & "  a.ptnr_dom_id, " _
            '          & "  a.ptnr_en_id, " _
            '          & "  a.ptnr_id, " _
            '          & "  a.ptnr_code, " _
            '          & "  a.ptnr_name, " _
            '          & "  en_desc, " _
            '          & "  ptnr_ac_ar_id, " _
            '          & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
            '          & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
            '          & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
            '          & "FROM " _
            '          & "  public.ptnr_mstr a " _
            '          & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
            '          & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            '          & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
            '          & " where ptnr_en_id  =" + _en_id.ToString + " " _
            '          & " and ptnr_active ~~* 'Y' " _
            '          & " and ptnr_is_member ~~* 'Y' " _
            '          & " and ptnr_name ~~* '%" & te_name.EditValue & "%' " _
            '          & " order by ptnr_name"
            '    End If

            'ElseIf fobject.name = FSalesPlanImportNew.Name Or fobject.name = FSalesPlanImportNewTarget.Name Or fobject.name = FSalesPlanImportTarget2.Name Then
            '    If _obj.name = "gv_edit" Then
            '        get_sequel = "SELECT  " _
            '          & "  a.ptnr_oid, " _
            '          & "  a.ptnr_dom_id, " _
            '          & "  a.ptnr_en_id, " _
            '          & "  a.ptnr_id, " _
            '          & "  a.ptnr_code, " _
            '          & "  a.ptnr_name, " _
            '          & "  en_desc, " _
            '          & "  ptnr_ac_ar_id, " _
            '          & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
            '          & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
            '          & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
            '          & "FROM " _
            '          & "  public.ptnr_mstr a " _
            '          & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
            '          & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            '          & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
            '          & " where ptnr_en_id  in (" + _en_id_coll + ") " _
            '          & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y'  " _
            '          & " and ptnr_name ~~* '%" & te_name.EditValue & "%' " _
            '          & " order by ptnr_name"
            '    Else
            '        get_sequel = "SELECT  " _
            '            & "  a.ptnr_oid, " _
            '            & "  a.ptnr_dom_id, " _
            '            & "  a.ptnr_en_id, " _
            '            & "  b.en_desc, " _
            '            & "  a.ptnr_id, " _
            '            & "  a.ptnr_code, " _
            '            & "  a.ptnr_name, " _
            '            & "  a.ptnr_nik, " _
            '            & "  a.ptnr_sls_emp_pos, " _
            '            & "  c.emp_pos_name, " _
            '            & "  a.ptnr_sls_emp_cat, " _
            '            & "  d.emp_cat_name, " _
            '            & "  e.emp_lvl_min, " _
            '            & "  e.emp_lvl_intsf, " _
            '            & "  a.ptnr_sls_emp_lvl, " _
            '            & "  e.emp_lvl_name, " _
            '            & "  a.ptnr_parent, " _
            '            & "  f.ptnr_name as ptnr_name_parent " _
            '            & "FROM public.ptnr_mstr a " _
            '            & "  INNER JOIN public.en_mstr b ON (a.ptnr_en_id = b.en_id) " _
            '            & "  INNER JOIN public.sls_emp_cat d ON (a.ptnr_sls_emp_cat = d.emp_cat_id) " _
            '            & "  INNER JOIN public.sls_emp_lvl e ON (a.ptnr_sls_emp_lvl = e.emp_lvl_id) " _
            '            & "  INNER JOIN public.sls_emp_pos c ON (a.ptnr_sls_emp_pos = c.emp_pos_id) " _
            '            & "  LEFT OUTER JOIN ptnr_mstr f on (f.ptnr_id = a.ptnr_parent) " _
            '            & " where a.ptnr_en_id  =" + _en_id.ToString + " " _
            '            & " and a.ptnr_active ~~* 'Y' " _
            '            & " and a.ptnr_is_member ~~* 'Y' " _
            '            & " and a.ptnr_name ~~* '%" & te_name.EditValue & "%' " _
            '            & " order by a.ptnr_name"


            '    End If

        ElseIf fobject.name = FSalesPlanImportTarget.Name Then
            If _obj.name = "gv_edit" Then
                get_sequel = "SELECT  " _
                  & "  a.ptnr_oid, " _
                  & "  a.ptnr_dom_id, " _
                  & "  a.ptnr_en_id, " _
                  & "  a.ptnr_id, " _
                  & "  a.ptnr_code, " _
                  & "  a.ptnr_name, " _
                  & "  en_desc, " _
                  & "  ptnr_ac_ar_id, " _
                  & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                  & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                  & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                  & "FROM " _
                  & "  public.ptnr_mstr a " _
                  & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                  & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                  & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                  & " where ptnr_en_id  in (" + _en_id_coll + ") " _
                  & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y'  " _
                  & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            Else
                get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, " _
                    & "  a.ptnr_nik, " _
                    & "  a.ptnr_sls_emp_pos, " _
                    & "  c.emp_pos_name, " _
                    & "  a.ptnr_sls_emp_cat, " _
                    & "  d.emp_cat_name, " _
                    & "  e.emp_lvl_min, " _
                    & "  e.emp_lvl_intsf, " _
                    & "  a.ptnr_sls_emp_lvl, " _
                    & "  e.emp_lvl_name, " _
                    & "  a.ptnr_parent, " _
                    & "  f.ptnr_name as ptnr_name_parent " _
                    & "FROM public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.ptnr_en_id = b.en_id) " _
                    & "  INNER JOIN public.sls_emp_cat d ON (a.ptnr_sls_emp_cat = d.emp_cat_id) " _
                    & "  INNER JOIN public.sls_emp_lvl e ON (a.ptnr_sls_emp_lvl = e.emp_lvl_id) " _
                    & "  INNER JOIN public.sls_emp_pos c ON (a.ptnr_sls_emp_pos = c.emp_pos_id) " _
                    & "  LEFT OUTER JOIN ptnr_mstr f on (f.ptnr_id = a.ptnr_parent) " _
                    & " where a.ptnr_en_id  =" + _en_id.ToString + " " _
                    & " and a.ptnr_active ~~* 'Y' " _
                    & " and a.ptnr_is_member ~~* 'Y' " _
                    & " and a.ptnr_name ~~* '%" & te_name.EditValue & "%' " _
                    & " order by a.ptnr_name"


            End If

        ElseIf fobject.name = FItemInternalPriceCost.Name Then

            get_sequel = "SELECT  " _
                & "  a.ptnr_oid, " _
                & "  a.ptnr_dom_id, " _
                & "  a.ptnr_en_id, " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name, a.ptnr_email," _
                & "  a.ptnr_sex, a.ptnr_ktp," _
                & "  a.ptnr_goldarah, " _
                & "  a.ptnr_birthcity, " _
                & "  a.ptnr_birthday, " _
                & "  a.ptnr_negara, " _
                & "  a.ptnr_bp_date, " _
                & "  a.ptnr_bp_type, " _
                & "  a.ptnr_waris_name, " _
                & "  a.ptnr_waris_ktp, " _
                & "  a.ptnr_is_internal, " _
                & "  en_desc, " _
                & "  ptnr_ac_ar_id, " _
                & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                & "FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                & "  LEFT OUTER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                & " and ptnr_active ~~* 'Y' " _
                & " and ptnr_is_vend ~~* 'Y' " _
                & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

            If _is_internal = True Then
                get_sequel += " and ptnr_is_internal = 'Y'"
            End If

        ElseIf fobject.name = FSalesOrder.Name Then

            If func_coll.get_conf_file("partner_so_cross_entity") = "1" And CeAllEntity.Checked Then

                get_sequel = "SELECT  " _
                       & "  a.ptnr_oid, " _
                       & "  a.ptnr_dom_id, " _
                       & "  a.ptnr_en_id, " _
                       & "  a.ptnr_id, " _
                       & "  a.ptnr_code, " _
                       & "  a.ptnr_name, a.ptnr_email," _
                       & "  a.ptnr_sex, a.ptnr_ktp," _
                       & "  a.ptnr_goldarah, " _
                       & "  a.ptnr_birthcity, " _
                       & "  a.ptnr_birthday, " _
                       & "  a.ptnr_negara, " _
                       & "  a.ptnr_bp_date, " _
                       & "  a.ptnr_bp_type, " _
                       & "  a.ptnr_waris_name, " _
                       & "  a.ptnr_waris_ktp, " _
                       & "  en_desc, " _
                       & "  ptnr_ac_ar_id, " _
                       & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                       & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                       & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                       & "FROM " _
                       & "  public.ptnr_mstr a " _
                       & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                       & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                       & "  LEFT OUTER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                       & " where  ptnr_active ~~* 'Y' " _
                       & " and ptnr_is_cust ~~* 'Y' " _
                       & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            Else
                get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, a.ptnr_email," _
                    & "  a.ptnr_sex, a.ptnr_ktp," _
                    & "  a.ptnr_goldarah, " _
                    & "  a.ptnr_birthcity, " _
                    & "  a.ptnr_birthday, " _
                    & "  a.ptnr_negara, " _
                    & "  a.ptnr_bp_date, " _
                    & "  a.ptnr_bp_type, " _
                    & "  a.ptnr_waris_name, " _
                    & "  a.ptnr_waris_ktp, " _
                    & "  a.ptnr_is_internal, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  LEFT OUTER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "
            End If

            If _is_internal = True Then
                get_sequel += " and ptnr_is_internal = 'Y'"
            End If
        Else

            get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, a.ptnr_email," _
                    & "  a.ptnr_sex, a.ptnr_ktp," _
                    & "  a.ptnr_goldarah, " _
                    & "  a.ptnr_birthcity, " _
                    & "  a.ptnr_birthday, " _
                    & "  a.ptnr_negara, " _
                    & "  a.ptnr_bp_date, " _
                    & "  a.ptnr_bp_type, " _
                    & "  a.ptnr_waris_name, " _
                    & "  a.ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " where ptnr_en_id  in (" + SetInteger(_en_id) + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " and ptnr_ptnrc_id = '9918' " _
                    & " and ptnr_name ~~* '%" & te_name.EditValue & "%' "

            If _is_internal = True Then
                get_sequel += " and ptnr_is_internal = 'Y'"
            End If


        End If

        get_sequel += " ORDER BY ptnr_name"
        'get_sequel = get_sequel & " limit 500"
        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        ds.Dispose()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FSalesOrder" Then
            fobject._so_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.so_ptnr_id_sold.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.so_shipping_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + " " _
                                                    + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + " " _
                                                    + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

        ElseIf fobject.name = "FSalesQuotation" Then
            fobject._ptnratt_ptnr_oid = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_oid").ToString)
            'fobject.sqa_kjb_code.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kjb_code"))
            fobject.sqa_bekerja_pada.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_bekerja_pada"))
            fobject.sqa_jabatan_bagian.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_jabatan_bagian"))
            fobject.sqa_kantor_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_alamat_1"))
            fobject.sqa_kantor_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_alamat_2"))
            fobject.sqa_kantor_lantai.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_lantai"))
            fobject.sqa_kantor_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_telp"))
            fobject.sqa_ktp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_ktp"))
            fobject.sqa_email.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_email"))
            fobject.sqa_rumah_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1"))
            fobject.sqa_rumah_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2"))
            fobject.sqa_rumah_kode_pos.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_zip"))
            fobject.sqa_rumah_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_phone_2"))
            fobject.sqa_rumah_hp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_phone_1"))
            fobject.sqa_status_alamat_kirim.EditValue = SetBitYNB(ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_alamat_kirim"))
            fobject.sqa_status_alamat_tagih.EditValue = SetBitYNB(ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_alamat_tagih"))
            fobject.sqa_suami_nama.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_nama"))
            fobject.sqa_suami_bekerja.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_bekerja"))
            fobject.sqa_suami_jabatan.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_jabatan"))
            fobject.sqa_suami_kantor_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_kantor_alamat_1"))
            fobject.sqa_suami_kantor_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_kantor_alamat_2"))
            fobject.sqa_suami_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_telp"))
            fobject.sqa_suami_hp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_hp"))
            fobject.sqa_anak_nama_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_nama_1"))
            'fobject.sqa_anak_tgl_lahir_1.DateTime = SetDate(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_tgl_lahir_1"))
            fobject.sqa_anak_sekolah_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_sekolah_1"))
            fobject.sqa_anak_nama_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_nama_2"))
            'fobject.sqa_anak_tgl_lahir_2.DateTime = SetDate(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_tgl_lahir_2"))
            fobject.sqa_anak_sekolah_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_sekolah_2"))
            fobject.sqa_anak_nama_3.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_nama_3"))
            'fobject.sqa_anak_tgl_lahir_3.DateTime = ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_tgl_lahir_3")
            fobject.sqa_anak_sekolah_3.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_sekolah_3"))
            fobject.sqa_keluarga_dekat_nama.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_nama"))
            fobject.sqa_keluarga_dekat_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_alamat_1"))
            fobject.sqa_keluarga_dekat_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_alamat_2"))
            fobject.sqa_keluarga_dekat_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_telp"))
            fobject.sqa_keluarga_dekat_hp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_hp"))
            fobject.sqa_status_tempat_tinggal.EditValue = SetBitYNB(ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_tempat_tinggal"))
            fobject.sqa_jenis_kartu_kredit.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_jenis_kartu_kredit"))
            fobject.sqa_no_kartu_kredit.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_no_kartu_kredit"))
            'fobject.sqa_berlaku_sd.Text = ds.Tables(0).Rows(_row_gv).Item("ptnratt_berlaku_sd")
            fobject.sqa_bank.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_bank"))
            fobject._sq_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.sq_bantu_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))
            fobject.sqa_status_rumah_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_rumah_id")
            fobject.sqa_status_rumah_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("status_rumah"))
            fobject.sqa_lama_tinggal_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_lama_tinggal_id")
            fobject.sqa_lama_tinggal_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("lama_tinggal"))
            fobject.sqa_masa_kerja_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_masa_kerja_id")
            fobject.sqa_masa_kerja_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("masa_kerja"))
            fobject.sqa_income_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_income_id")
            fobject.sqa_income_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("income"))
            fobject.sqa_kepribadian_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_kepribadian_id")
            fobject.sqa_kepribadian_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("kepribadian"))
            fobject.sqa_tanggungan_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_tanggungan_id")
            fobject.sqa_tanggungan_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("tanggungan"))
            fobject.sqa_jaminan_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_jaminan_id")
            fobject.sqa_jaminan_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("jaminan"))
            fobject.sqa_status_rumah_id.ClosePopup()
            fobject.sqa_lama_tinggal_id.ClosePopup()
            fobject.sqa_masa_kerja_id.ClosePopup()
            fobject.sqa_income_id.ClosePopup()
            fobject.sqa_kepribadian_id.ClosePopup()
            fobject.sqa_tanggungan_id.ClosePopup()
            fobject.sqa_jaminan_id.ClosePopup()
        ElseIf fobject.name = FSalesQuotationConsigment.Name Then
            fobject.sq_ptnr_id_sold.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.sq_bantu_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

        ElseIf fobject.name = "FRouting" Then
            fobject.gv_edit.SetRowCellValue(_row, "rod_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_code", ds.Tables(0).Rows(_row_gv).Item("ptnr_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
        ElseIf fobject.name = "FProject" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.__prj_ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FCashOut.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.__ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FCashIn.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.__ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FDRCRMemo.Name Or fobject.name = "FDRCRMemoDetail" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject._par_cus_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FDBCRReScheduleSDI.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FVoucher.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject._par_vendor_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FPartnerTaxGroup.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "tipgd_en_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "tipgd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FLocation.Name Or fobject.name = FARMerge.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
        ElseIf fobject.name = FPartnerAll.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FInvestment.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

            'ElseIf fobject.name = "FPartnerIntitutionName".Name Then
            '    fobject.ptnrc_ptnr_name_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            '    fobject.ptnrc_ptnr_name_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            'fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            'fobject.sq_bantu_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))


        ElseIf fobject.name = "FPartnerIntitutionName" Then
            fobject.ptnru_ptnr_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.ptnru_ptnr_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.ptnru_ptnr_ptnrc_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_ptnrc_id")
            fobject.ptnru_ptnr_ptnrc_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnrc_name")
            fobject.ptnru_name.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            'fobject.ptnru_ptnrc_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_ptnrc_id")
            'fobject.ptnru_ptnrc_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnrc_name")
            'fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            'fobject.so_shipping_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + " " _
            '                                        + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + " " _
            '                                        + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

        ElseIf fobject.name = "FSalesOrderFakturPenjualanPrint" Then
            fobject.be_sold_to.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.be_sold_to.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")

            'fobject.ptnru_ptnr_ptnrc_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_ptnrc_id")
            'fobject.ptnru_ptnr_ptnrc_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnrc_name")
            'fobject.ptnru_name.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")

            'ElseIf fobject.name = "FSalesOrderFakturPenjualanPrint" Then
            '    _ptnr_id.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_name")
            '    _ptnr_id.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id")


            'ElseIf fobject.name = FSalesPlan.Name Or fobject.name = FSalesPlanImportNew.Name Then
            '    If _obj.name = "plans_sales_id" Then
            '        _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            '        _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            '    Else
            '        fobject.gv_edit.SetRowCellValue(_row, "plansd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            '        fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            '        fobject.gv_edit.SetRowCellValue(_row, "ptnr_code", ds.Tables(0).Rows(_row_gv).Item("ptnr_code"))
            '        fobject.gv_edit.BestFitColumns()
            '    End If
            'End If

        ElseIf fobject.name = "FItemInternalPriceCost" Then
            fobject.ip_vendor_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.ip_vendor_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")

        ElseIf fobject.name = FCanvasingPlanner.Name Or fobject.name = FCanvasingSalesPlanning.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

            'ElseIf fobject.name = "FCanvasingProgram" Then
            '    fobject.cvn_sls_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            '    fobject.cvn_sls_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")

        ElseIf fobject.name = FSalesPlanImportNewTarget.Name Or fobject.name = FSalesPlanImportTarget.Name Then
            If _obj.name = "plans_sales_id" Then
                _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
                fobject.plans_emp_pos_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_sls_emp_pos")
                fobject.plans_emp_pos_id.text = ds.Tables(0).Rows(_row_gv).Item("emp_pos_name")
                fobject.plans_emp_cat_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_sls_emp_cat")
                fobject.plans_emp_cat_id.text = ds.Tables(0).Rows(_row_gv).Item("emp_cat_name")
                fobject.plans_emp_lvl_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_sls_emp_lvl")
                fobject.plans_emp_lvl_id.text = ds.Tables(0).Rows(_row_gv).Item("emp_lvl_name")
                fobject.plans_amount_tgt.text = ds.Tables(0).Rows(_row_gv).Item("emp_lvl_min")
                fobject.plans_insentif.text = ds.Tables(0).Rows(_row_gv).Item("emp_lvl_intsf")
                fobject.plans_emp_nik.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_nik")

                ' Check if ptnr_parent is DBNull.Value
                Dim ptnrParent As Object = ds.Tables(0).Rows(_row_gv).Item("ptnr_parent")
                If ptnrParent Is DBNull.Value Then
                    fobject.plans_emp_parent_id.tag = Nothing
                    fobject.plans_emp_parent_id.text = "" ' Set to empty string
                Else
                    fobject.plans_emp_parent_id.tag = ptnrParent
                    fobject.plans_emp_parent_id.text = If(ds.Tables(0).Rows(_row_gv).Item("ptnr_name_parent") Is DBNull.Value, "", ds.Tables(0).Rows(_row_gv).Item("ptnr_name_parent").ToString())
                End If

            Else
                fobject.gv_edit.SetRowCellValue(_row, "plansd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
                fobject.gv_edit.SetRowCellValue(_row, "ptnr_code", ds.Tables(0).Rows(_row_gv).Item("ptnr_code"))
                fobject.gv_edit.BestFitColumns()
            End If
        End If
        'End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("status") = True Then
                    jml = jml + 1
                End If
            Next
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("status") = True Then
                    If fobject.name = FPartnerTaxGroup.Name Then
                        If jml = 0 Then
                            fobject.gv_edit.SetRowCellValue(_row, "tipgd_en_id", ds.Tables(0).Rows(i).Item("ptnr_en_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "tipgd_ptnr_id", ds.Tables(0).Rows(i).Item("ptnr_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
                            jml = jml + 1
                        Else
                            fobject.gv_edit.AddNewRow()
                            _row_pos = fobject.gv_edit.FocusedRowHandle()
                            fobject.gv_edit.SetRowCellValue(_row_pos, "tipgd_en_id", ds.Tables(0).Rows(i).Item("ptnr_en_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "tipgd_ptnr_id", ds.Tables(0).Rows(i).Item("ptnr_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
                        End If

                        fobject.gv_edit.BestFitColumns()
                    End If
                End If
            Next
            If jml = 0 Then
                MsgBox("Please checklist data first")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ds.Dispose()
        Me.Close()
    End Sub
End Class
