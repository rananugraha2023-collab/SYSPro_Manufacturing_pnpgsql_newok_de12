Imports master_new.ModFunction

Public Class FSearchPartNumberSearch
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _sq_type As String
    Public _tran_oid As String = ""
    Public _ppn_type As String = ""
    Dim func_data As New function_data
    Public _filter As String
    Public grid_call As String = ""

    Public _qty_receive As Double
    Public _pt_id As Integer

    Private Sub FSearchPartNumberSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If _tran_oid.ToString <> "" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Include", "pt_tax_inc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        End If
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        get_sequel = ""

        If fobject.name = "FSalesQuotation" Or fobject.name = FSalesQuotationConsigment.Name Then
            If grid_call = "header" Then
                get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " left outer join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " left outer join invct_table on invct_pt_id = pt_id " _
                    & " left outer join si_mstr on si_id = pt_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (" + _en_id_coll + ")  and pt_type='I' and pt_id in (select pack_pt_id from pack_mstr) "
            Else
                get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " left outer join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " left outer join invct_table on invct_pt_id = pt_id " _
                    & " left outer join si_mstr on si_id = pt_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (" + _en_id_coll + ")  and pt_type='I' "
            End If
            


        ElseIf fobject.name = "FInventoryReceipts" Then
            If _tran_oid.ToString <> "" Then
                get_sequel = "SELECT  " _
                    & "  sqd_oid, " _
                    & "  sqd_sq_oid, " _
                    & "  sqd_seq, " _
                    & "  sqd_qty, " _
                    & "  sqd_um, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_ls, " _
                    & "  pt_um, " _
                    & "  pt_type,  " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  pt_ppn_type,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  sqd_cost as invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_pl_id, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name, " _
                    & "  coalesce(sqd_qty_ir,0) sqd_qty_ir, " _
                    & "  sqd_qty - coalesce(sqd_qty_ir,0) qty_open " _
                    & "FROM  " _
                    & "  public.sqd_det " _
                    & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                    & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                    & "  inner join en_mstr on en_id = pt_en_id " _
                    & "  inner join loc_mstr on loc_id = pt_loc_id " _
                    & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & "  left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & "  inner join si_mstr on si_id = sqd_si_id " _
                    & "  where sqd_qty - coalesce(sqd_qty_ir,0) > 0" _
                    & " and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and sq_oid = '" + _tran_oid + "'"
            Else
                get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " inner join invct_table on invct_pt_id = pt_id " _
                    & " inner join si_mstr on si_id = invct_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and invct_si_id = " + _si_id.ToString _
                    & " and pt_en_id in (" + _en_id_coll + ")"
            End If

        ElseIf fobject.name = "FInventoryIssues" Then
            If _tran_oid.ToString <> "" Then
                get_sequel = "SELECT  " _
                    & "  pbd_oid, " _
                    & "  pbd_pb_oid, " _
                    & "  pbd_seq, " _
                    & "  pbd_qty, " _
                    & "  pbd_um, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_ls, " _
                    & "  pt_um, " _
                    & "  pt_type,  " _
                    & "  pt_loc_id, " _
                    & "  loc_desc,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  pt_ppn_type, " _
                    & "  invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_pl_id, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name, " _
                    & "  coalesce(pbd_qty_riud,0) pbd_qty_riud, " _
                    & "  pbd_qty - coalesce(pbd_qty_riud,0) qty_open " _
                    & "FROM  " _
                    & "  public.pbd_det " _
                    & "  inner join pb_mstr on pb_oid = pbd_pb_oid " _
                    & "  inner join pt_mstr on pt_id = pbd_pt_id " _
                    & "  inner join en_mstr on en_id = pt_en_id " _
                    & "  inner join loc_mstr on loc_id = pt_loc_id " _
                    & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & "  left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & "  inner join si_mstr on si_id = pbd_si_id " _
                    & "  inner join invct_table on invct_si_id = pbd_si_id and invct_pt_id = pbd_pt_id " _
                    & "  where pbd_qty - coalesce(pbd_qty_riud,0) > 0" _
                    & " and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and invct_si_id = " + _si_id.ToString _
                    & " and pb_oid = '" + _tran_oid + "'"
            Else
                get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " inner join invct_table on invct_pt_id = pt_id " _
                    & " inner join si_mstr on si_id = invct_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and invct_si_id = " + _si_id.ToString _
                    & " and pt_en_id in (" + _en_id_coll + ")"
            End If
        ElseIf fobject.name = "FItemSiteCost" Then
            get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'"
        ElseIf fobject.name = "FPriceList" Or fobject.name = "FPriceListDetail" Then
            get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (" + _en_id_coll + ")"
        ElseIf fobject.name = "FProductStructure" Or fobject.name = "FPartNumberSubtitute" _
            Or fobject.name = "FWorkOrder" Or fobject.name = "FBillOfMaterial" Or fobject.name = "FWOMaintenance" _
            Or fobject.name = "FProject" Or fobject.name = FInventoryHistory.Name Then
            If _sq_type = "from" Then
                get_sequel = "SELECT  distinct " _
                   & "  en_id, " _
                   & "  en_desc, " _
                   & "  coalesce(si_desc,'') as si_desc, " _
                   & "  pt_id, " _
                   & "  pt_code, " _
                   & "  pt_desc1, " _
                   & "  pt_desc2, " _
                   & "  pt_cost, " _
                   & "  coalesce(invct_cost,0) as invct_cost, " _
                   & "  pt_price, " _
                   & "  pt_type,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                   & "  pt_um, " _
                   & "  pt_pl_id, " _
                   & "  pt_ls, " _
                   & "  pt_loc_id, " _
                   & "  loc_desc, " _
                   & "  pt_taxable, " _
                   & "  pt_tax_inc, " _
                   & "  pt_tax_class, " _
                   & "  tax_class_mstr.code_name as tax_class_name, " _
                   & "  pt_ppn_type, " _
                   & "  um_mstr.code_name as um_name " _
                   & "FROM  " _
                   & "  public.pt_mstr" _
                   & " inner join en_mstr on en_id = pt_en_id " _
                   & " inner join loc_mstr on loc_id = pt_loc_id " _
                   & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                   & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                   & " inner join invct_table on invct_pt_id = pt_id " _
                   & " inner join si_mstr on si_id = invct_si_id " _
                   & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                   & " and en_active ~~* 'Y'" _
                   & " and pt_en_id in (" + _en_id_coll + ") and pt_id in (SELECT b.psd_pt_bom_id FROM  public.ps_mstr a  INNER JOIN public.psd_det b ON (a.ps_oid = b.psd_ps_oid)WHERE  a.ps_id = " & _filter & ")"


            Else
                get_sequel = "SELECT  distinct " _
                   & "  en_id, " _
                   & "  en_desc, " _
                   & "  coalesce(si_desc,'') as si_desc, " _
                   & "  pt_id, " _
                   & "  pt_code, coalesce(pt_approval_status,'A') as pt_approval_status," _
                   & "  pt_desc1, " _
                   & "  pt_desc2, " _
                   & "  pt_cost, " _
                   & "  coalesce(invct_cost,0) as invct_cost, " _
                   & "  pt_price, " _
                   & "  pt_type, " _
                   & "  pt_um, " _
                   & "  pt_pl_id, " _
                   & "  pt_ls, " _
                   & "  pt_loc_id, " _
                   & "  loc_desc, " _
                   & "  pt_taxable, " _
                   & "  pt_tax_inc, " _
                   & "  pt_tax_class, " _
                   & "  tax_class_mstr.code_name as tax_class_name, " _
                   & "  pt_ppn_type, " _
                   & "  um_mstr.code_name as um_name " _
                   & "FROM  " _
                   & "  public.pt_mstr" _
                   & " inner join en_mstr on en_id = pt_en_id " _
                   & " inner join loc_mstr on loc_id = pt_loc_id " _
                   & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                   & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                   & " inner join invct_table on invct_pt_id = pt_id " _
                   & " inner join si_mstr on si_id = invct_si_id " _
                   & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                   & " and en_active ~~* 'Y'" _
                   & " and pt_en_id in (" + _en_id_coll + ")"
            End If
        ElseIf fobject.name = "FWOComponentIssue" Then

            If fobject.__wo_use_bom = "Y" Then

                get_sequel = "SELECT  " _
                            & "  a.pts_pt_sub_id,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                            & "  b.pt_code,  b.pt_tax_class, b.pt_ppn_type, b.pt_cost, " _
                            & "  b.pt_desc1,  e.en_desc, d.si_desc, b.pt_desc2, b.pt_taxable, " _
                            & "  c.code_name AS unit_measure " _
                            & "FROM " _
                            & "  public.pts_mstr a " _
                            & "  INNER JOIN public.pt_mstr b ON (a.pts_pt_sub_id = b.pt_id) " _
                            & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                            & "  INNER JOIN public.en_mstr e ON (a.pts_en_id = e.en_id) " _
                            & " INNER JOIN public.si_mstr d ON (b.pt_si_id = d.si_id) " _
                            & "WHERE " _
                            & "  a.pts_ps_id = (SELECT c.ps_id FROM public.wo_mstr a " _
                            & "  INNER JOIN public.bom_mstr b ON (a.wo_bom_id = b.bom_id) " _
                            & " INNER JOIN public.ps_mstr c ON (b.bom_id = c.ps_bom_id) " _
                            & "WHERE a.wo_id = '" & fobject.__woci_wo_id & "' AND a.wo_use_bom = 'Y') AND " _
                            & "(pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%') " _
                            & " and pt_en_id in (" + _en_id_coll + ") "



            Else
                get_sequel = "SELECT  " _
                            & "  a.pts_pt_sub_id, " _
                            & "  b.pt_code, coalesce(pt_approval_status,'A') as pt_approval_status," _
                            & "  b.pt_desc1, " _
                            & "  b.pt_desc2, " _
                            & "  d.en_desc, " _
                            & "  e.si_desc, " _
                            & "  c.code_name AS unit_measure " _
                            & "FROM " _
                            & "  public.pts_mstr a " _
                            & "  INNER JOIN public.pt_mstr b ON (a.pts_pt_sub_id = b.pt_id) " _
                            & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                            & "  INNER JOIN public.en_mstr d ON (b.pt_en_id = d.en_id) " _
                            & "  INNER JOIN public.si_mstr e ON (b.pt_si_id = e.si_id) " _
                            & "WHERE " _
                            & "  a.pts_ps_id = (SELECT b.ps_id FROM public.wo_mstr a  " _
                            & "  INNER JOIN public.ps_mstr b ON (a.wo_pt_id = b.ps_pt_id)  " _
                            & "  WHERE a.wo_id = '" & fobject.__woci_wo_id & "' AND a.wo_use_bom = 'N') AND " _
                            & " (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%') " _
                            & " and pt_en_id in (" + _en_id_coll + ") "

            End If
        ElseIf fobject.name = "FProjectMaintenance" Then
            get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  coalesce(si_desc,'') as si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  coalesce(invct_cost,0) as invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " inner join invct_table on invct_pt_id = pt_id " _
                    & " inner join si_mstr on si_id = invct_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (" + _en_id_coll + ")"
        ElseIf fobject.name = FRequisition.Name Then
            get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  coalesce(si_desc,'') as si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  coalesce(0) as invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " inner join si_mstr on si_id = pt_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_si_id = " + _si_id.ToString _
                    & " and pt_en_id in (" + _en_id_coll + ")"
        Else
            get_sequel = "SELECT  distinct " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  coalesce(si_desc,'') as si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  coalesce(invct_cost,0) as invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " left outer join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " left outer join invct_table on invct_pt_id = pt_id " _
                    & " left outer join si_mstr on si_id = pt_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (" + _en_id_coll + ")"
        End If

        If fobject.name = "FPurchaseOrder" Then
        ElseIf fobject.name = "FRequisition" Then
        ElseIf fobject.name = "FInventoryRequest" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FPriceList" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FPriceListDetail" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryReceipts" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryIssues" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            ' get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FTransferIssues" Then
            'get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FSalesQuotation" Then
            'ant 17 maret 2011
        ElseIf fobject.name = FTransferIssuesWIP.Name Then
            get_sequel = get_sequel + " and pt_id in (SELECT  " _
                    & "  a.invw_pt_id " _
                    & "FROM " _
                    & "  public.invw_wip a " _
                    & "WHERE " _
                    & "  a.invw_en_id = " & _en_id & " AND  " _
                    & "  a.invw_dom_id = " & master_new.ClsVar.sdom_id & " AND  " _
                    & "  a.invw_wo_oid = " & SetSetring(_tran_oid) & " AND  " _
                    & "  a.invw_wc_id = " & _pt_id & ")"

        ElseIf fobject.name = "FReqTransferIssue" Then
            get_sequel = get_sequel + "and (pt_type ~~* 'I' or pt_type ~~* 'A')"
            '-----------------------------
        ElseIf fobject.name = FPartNumberSubtitute.Name Then
            get_sequel = get_sequel + " and pt_type ~~* 'I' "
        ElseIf fobject.name = FWOBillMaintenance.Name Or fobject.name = FBillOfQuantity.Name Then
            get_sequel = get_sequel + " and pt_type ~~* 'I' "
        ElseIf fobject.name = FWorkInProgress.Name Then
            Dim sSQL As String
            sSQL = "SELECT  " _
              & "  b.pt_id, " _
              & "  b.pt_code, " _
              & "  b.pt_desc1, " _
              & "  b.pt_desc2, " _
              & "  b.pt_um , " _
              & "  c.code_code as um_desc,a.psd_qty " _
              & "FROM " _
              & "  public.psd_det a " _
              & "  INNER JOIN public.pt_mstr b ON (a.psd_pt_bom_id = b.pt_id) " _
              & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
              & "  INNER JOIN public.ps_mstr d ON (a.psd_ps_oid = d.ps_oid) " _
              & "WHERE " _
              & "  d.ps_pt_bom_id=" & _pt_id

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)
            Dim _pt_id_to As String = ""
            For Each dr As DataRow In dt.Rows
                _pt_id_to += dr("pt_id") & ","
            Next
            _pt_id_to = _pt_id_to.Substring(0, Microsoft.VisualBasic.Len(_pt_id_to) - 1)
            get_sequel = get_sequel + " and pt_id in (" & _pt_id_to & ")"
        End If

        'pt_type ~~* 'I' dikomentarin agar barang bisa ditransfer atau diapapun walaupun bukan Inventory
        get_sequel += " and pt_its_id=1 order by pt_desc1 "


        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Public Overrides Sub fill_data()
        Try


            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position
            Dim dt_bantu As New DataTable()
            Dim func_coll As New function_collection

            If ds.Tables(0).Rows(_row_gv).Item("pt_approval_status").ToString.ToUpper = "I" Then
                Box("This product has not been approved by tax department")
                Exit Sub
            ElseIf ds.Tables(0).Rows(_row_gv).Item("pt_approval_status").ToString.ToUpper = "T" Then
                Box("This product has not been approved by accounting department")
                Exit Sub
            End If

            If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrderFilm" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

                If ds.Tables(0).Rows(_row_gv).Item("pt_type").ToString.ToUpper = "E" Then
                    fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "M")
                Else
                    fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "")
                End If

                fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))

                fobject.gv_edit.SetRowCellValue(_row, "pod_taxable", ds.Tables(0).Rows(_row_gv).Item("pt_taxable"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_tax_inc", ds.Tables(0).Rows(_row_gv).Item("pt_tax_inc"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class", ds.Tables(0).Rows(_row_gv).Item("pt_tax_class"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("tax_class_name"))

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FRequisition" Then
                fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryRequest" Then
                fobject.gv_edit.SetRowCellValue(_row, "pbd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "pbd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FPriceList" Then
                fobject.gv_edit_item.SetRowCellValue(_row, "pid_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit_item.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit_item.BestFitColumns()
            ElseIf fobject.name = "FPriceListDetail" Then
                fobject.gv_edit_item.SetRowCellValue(_row, "pid_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit_item.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit_item.BestFitColumns()
            ElseIf fobject.name = "FInventoryReceipts" Then
                dt_bantu = (func_coll.get_prodline_account(ds.Tables(0).Rows(_row_gv).Item("pt_pl_id"), "WO_COP+"))
                If dt_bantu Is Nothing Then
                    Exit Sub
                End If
                fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

                fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", dt_bantu.Rows(0).Item("pla_ac_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code", dt_bantu.Rows(0).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name", dt_bantu.Rows(0).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_sb_id", dt_bantu.Rows(0).Item("pla_sb_id"))
                fobject.gv_edit.SetRowCellValue(_row, "sb_desc", "-")
                fobject.gv_edit.SetRowCellValue(_row, "riud_cc_id", dt_bantu.Rows(0).Item("pla_cc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cc_desc", "-")

                If _tran_oid.ToString <> "" Then
                    fobject.gv_edit.SetRowCellValue(_row, "riud_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
                    fobject.gv_edit.SetRowCellValue(_row, "riud_sqd_oid", ds.Tables(0).Rows(_row_gv).Item("sqd_oid").ToString)
                End If

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryIssues" Then
                dt_bantu = (func_coll.get_prodline_account(ds.Tables(0).Rows(_row_gv).Item("pt_pl_id"), "WO_COP-"))
                If dt_bantu Is Nothing Then
                    Exit Sub
                End If
                fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

                fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", dt_bantu.Rows(0).Item("pla_ac_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code", dt_bantu.Rows(0).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name", dt_bantu.Rows(0).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_sb_id", dt_bantu.Rows(0).Item("pla_sb_id"))
                fobject.gv_edit.SetRowCellValue(_row, "sb_desc", "-")
                fobject.gv_edit.SetRowCellValue(_row, "riud_cc_id", dt_bantu.Rows(0).Item("pla_cc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cc_desc", "-")

                If _tran_oid.ToString <> "" Then
                    fobject.gv_edit.SetRowCellValue(_row, "riud_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
                    fobject.gv_edit.SetRowCellValue(_row, "riud_pbd_oid", ds.Tables(0).Rows(_row_gv).Item("pbd_oid").ToString)
                End If

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
                dt_bantu = (func_coll.get_prodline_account(ds.Tables(0).Rows(_row_gv).Item("pt_pl_id"), "WO_COP+"))
                If dt_bantu Is Nothing Then
                    Exit Sub
                End If
                fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

                fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", dt_bantu.Rows(0).Item("pla_ac_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code", dt_bantu.Rows(0).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name", dt_bantu.Rows(0).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_sb_id", dt_bantu.Rows(0).Item("pla_sb_id"))
                fobject.gv_edit.SetRowCellValue(_row, "sb_desc", "-")
                fobject.gv_edit.SetRowCellValue(_row, "riud_cc_id", dt_bantu.Rows(0).Item("pla_cc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cc_desc", "-")

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
                dt_bantu = (func_coll.get_prodline_account(ds.Tables(0).Rows(_row_gv).Item("pt_pl_id"), "WO_COP-"))

                If dt_bantu Is Nothing Then
                    Exit Sub
                End If
                fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

                fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", dt_bantu.Rows(0).Item("pla_ac_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code", dt_bantu.Rows(0).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name", dt_bantu.Rows(0).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_sb_id", dt_bantu.Rows(0).Item("pla_sb_id"))
                fobject.gv_edit.SetRowCellValue(_row, "sb_desc", "-")
                fobject.gv_edit.SetRowCellValue(_row, "riud_cc_id", dt_bantu.Rows(0).Item("pla_cc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cc_desc", "-")

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryBeginingBalance" Then
                fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FTransferIssues" Then



                fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                fobject.gv_edit.BestFitColumns()


            ElseIf fobject.name = "FInventoryCycleCount" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject._pt_id_global = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.ccre_loc_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_loc_id")
                fobject.ccre_um_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_um")
                fobject.ccre_cost.editvalue = ds.Tables(0).Rows(_row_gv).Item("invct_cost")
            ElseIf fobject.name = "FItemSiteCost" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")

            ElseIf fobject.name = "FProject" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.__prj_pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")

            ElseIf fobject.name = "FSalesQuotation" Then
                Dim ds_bantu As New DataSet

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                & "From pla_mstr  " _
                                                & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                & "where pla_pl_id = " + ds.Tables(0).Rows(_row_gv).Item("pt_pl_id").ToString _
                                                & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "prodline")

                            If ds_bantu.Tables(0).Rows.Count = 0 Then
                                Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                Exit Sub
                            ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                Exit Sub
                            ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                Exit Sub
                            End If

                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                fobject.gv_edit.SetRowCellValue(_row, "sqd_ppn_type", ds.Tables(0).Rows(_row_gv).Item("pt_ppn_type"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_taxable", ds.Tables(0).Rows(_row_gv).Item("pt_taxable"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_inc", ds.Tables(0).Rows(_row_gv).Item("pt_tax_inc"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class", ds.Tables(0).Rows(_row_gv).Item("pt_tax_class"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("tax_class_name"))

                fobject.gv_edit.SetRowCellValue(_row, "sqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))

                'If _sq_type <> "D" Then
                '    fobject.gv_edit.SetRowCellValue(_row, "sqd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                'End If

                fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                fobject.gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = FSalesQuotationConsigment.Name Then

                If grid_call = "header" Then

                    fobject.sq_pt_id.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                    fobject.sq_pt_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                    fobject.pt_desc1.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                    fobject.pt_desc2.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")

                    Dim dt_price As New DataTable
                    Dim sSQL As String
                    sSQL = "SELECT  " _
                            & "  pi_id, " _
                            & "  pidd_oid, " _
                            & "  pidd_pid_oid, " _
                            & "  pidd_payment_type, " _
                            & "  pidd_price, " _
                            & "  pidd_disc, " _
                            & "  pidd_dp, " _
                            & "  pidd_interval, " _
                            & "  coalesce(pidd_payment,0) as pidd_payment, " _
                            & "  pidd_min_qty, " _
                            & "  pidd_sales_unit, " _
                            & "  pid_pt_id " _
                            & "FROM  " _
                            & "  public.pidd_det " _
                            & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                            & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                            & "  where pi_id = " & fobject.sq_pi_id.EditValue _
                            & "  and pidd_payment_type = " & fobject.sq_pay_type.EditValue _
                            & "  and pid_pt_id = " & ds.Tables(0).Rows(_row_gv).Item("pt_id") _
                            & "  order by pidd_min_qty desc "

                    dt_price = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_price.Rows
                        fobject.sq_price.editvalue = SetNumber(dr("pidd_price"))
                    Next

                    Dim dt_temp As New DataTable
                    Dim _row As Integer = 0


                    Dim dt_pt As New DataTable
                    ' Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")

                    'If _file = "" Then
                    '    Exit Sub
                    'End If

                    ' file_excel.EditValue = _file
                    'ds = master_new.excelconn.ImportExcel(_file)


                    sSQL = "SELECT  " _
                        & "  a.packd_oid, " _
                        & "  a.packd_pack_code, " _
                        & "  a.packd_pt_id, " _
                        & "  a.packd_dt " _
                        & "FROM " _
                        & "  public.packd_detail a " _
                        & "  INNER JOIN public.pack_mstr b ON (a.packd_pack_code = b.pack_code) " _
                        & "WHERE " _
                        & "  b.pack_pt_id =" & SetInteger(ds.Tables(0).Rows(_row_gv).Item("pt_id"))


                    dt_pt = master_new.PGSqlConn.GetTableData(sSQL)

                    fobject.ds_edit.Tables(0).Rows.Clear()
                    fobject.ds_edit.AcceptChanges()

                    fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    For Each dr As DataRow In dt_pt.Rows
                        sSQL = "SELECT  distinct " _
                           & "  en_id, " _
                           & "  en_desc, " _
                           & "  si_desc, " _
                           & "  pt_id, " _
                           & "  pt_code, " _
                           & "  pt_desc1, " _
                           & "  pt_desc2, " _
                           & "  pt_cost, " _
                           & "  invct_cost, " _
                           & "  pt_price, " _
                           & "  pt_type, " _
                           & "  pt_um, " _
                           & "  pt_pl_id, " _
                           & "  pt_ls, " _
                           & "  pt_loc_id, " _
                           & "  loc_desc, " _
                           & "  pt_taxable, " _
                           & "  pt_tax_inc, " _
                           & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                           & "  tax_class_mstr.code_name as tax_class_name, " _
                           & "  pt_ppn_type, " _
                           & "  um_mstr.code_name as um_name " _
                           & "FROM  " _
                           & "  public.pt_mstr" _
                           & " inner join en_mstr on en_id = pt_en_id " _
                           & " inner join loc_mstr on loc_id = pt_loc_id " _
                           & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                           & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                           & " inner join invct_table on invct_pt_id = pt_id " _
                           & " inner join si_mstr on si_id = invct_si_id " _
                           & " where pt_id =" & SetInteger(dr("packd_pt_id")) & " "

                        dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

                        For Each dr_temp As DataRow In dt_temp.Rows

                            Dim ds_bantu As New DataSet

                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                            & "From pla_mstr  " _
                                                            & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                            & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                            & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                            & "where pla_pl_id = " + dr_temp("pt_pl_id").ToString _
                                                            & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                        .InitializeCommand()
                                        .FillDataSet(ds_bantu, "prodline")

                                        If ds_bantu.Tables(0).Rows.Count = 0 Then
                                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                            Exit Sub
                                        ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                            dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                            Exit Sub
                                        ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                            Exit Sub
                                        End If

                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try
                            'fobject.gv_edit.AddNewRow()
                            'fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_pt_id", dr_temp("pt_id"))
                            'fobject.gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                            'fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                            'fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                            'fobject.gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                            'fobject.gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_loc_id", dr_temp("pt_loc_id"))
                            'fobject.gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_um", dr_temp("pt_um"))
                            'fobject.gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_cost", dr_temp("invct_cost"))

                            ''If _so_type <> "D" Then
                            ''    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                            ''End If

                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                            'fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                            'fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                            'fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                            'fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                            'fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_taxable", dr_temp("pt_taxable"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_inc", dr_temp("pt_tax_inc"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class", dr_temp("pt_tax_class"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", dr_temp("tax_class_name"))

                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_ppn_type", dr_temp("pt_ppn_type"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_qty", dr("qty"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sqd_disc", dr("disc"))


                            fobject.gv_edit.AddNewRow()
                            fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                            fobject.gv_edit.SetRowCellValue(_row, "sqd_pt_id", dr_temp("pt_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_loc_id", dr_temp("pt_loc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_um", dr_temp("pt_um"))
                            fobject.gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_cost", dr_temp("invct_cost"))

                            'If _so_type <> "D" Then
                            '    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                            'End If

                            fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                            fobject.gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                            fobject.gv_edit.SetRowCellValue(_row, "sqd_taxable", dr_temp("pt_taxable"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_inc", dr_temp("pt_tax_inc"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class", dr_temp("pt_tax_class"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", dr_temp("tax_class_name"))

                            fobject.gv_edit.SetRowCellValue(_row, "sqd_ppn_type", dr_temp("pt_ppn_type"))
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_qty", 0)
                            fobject.gv_edit.SetRowCellValue(_row, "sqd_disc", 0)

                            'dt_bantu = New DataTable
                            ' dt_bantu = (load_price_list(so_pi_id.EditValue, _sod_pt_id, fobject.so_pay_type.EditValue, e.Value))
                            Dim dt_bantu_new As New DataTable()

                            Using ds_bantu_new As New DataSet()

                                Try
                                    Using objcb As New master_new.WDABasepgsql("", "")
                                        With objcb
                                            .SQL = "SELECT  " _
                                                & "  pi_id, " _
                                                & "  pidd_oid, " _
                                                & "  pidd_pid_oid, " _
                                                & "  pidd_payment_type, " _
                                                & "  pidd_price, " _
                                                & "  pidd_disc, " _
                                                & "  pidd_dp, " _
                                                & "  pidd_interval, " _
                                                & "  coalesce(pidd_payment,0) as pidd_payment, " _
                                                & "  pidd_min_qty, " _
                                                & "  pidd_sales_unit, " _
                                                & "  pid_pt_id " _
                                                & "FROM  " _
                                                & "  public.pidd_det " _
                                                & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                                                & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                                                & "  where pi_id = " & fobject.sq_pi_id.EditValue _
                                                & "  and pidd_payment_type = " & fobject.sq_pay_type.EditValue _
                                                & "  and pid_pt_id = " & dr_temp("pt_id") _
                                                & "  order by pidd_min_qty desc "
                                            .InitializeCommand()
                                            .FillDataSet(ds_bantu_new, "pi_mstr")
                                            dt_bantu_new = ds_bantu_new.Tables(0)
                                        End With
                                    End Using
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message)
                                End Try
                            End Using

                            If dt_bantu_new.Rows.Count > 0 Then
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_price", dt_bantu_new.Rows(0).Item("pidd_price"))
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_disc", dt_bantu_new.Rows(0).Item("pidd_disc"))

                                fobject.gv_edit.SetRowCellValue(_row, "sqd_payment", dt_bantu_new.Rows(0).Item("pidd_payment"))
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_dp", dt_bantu_new.Rows(0).Item("pidd_dp"))
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_unit", dt_bantu_new.Rows(0).Item("pidd_sales_unit"))
                            Else
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_price", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_disc", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_payment", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_dp", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_unit", 0)
                            End If


                            fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                            _row = _row + 1
                            System.Windows.Forms.Application.DoEvents()

                        Next
                    Next
                    fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                    fobject.gv_edit.BestFitColumns()
                Else
                    Dim ds_bantu As New DataSet

                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                    & "From pla_mstr  " _
                                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                    & "where pla_pl_id = " + ds.Tables(0).Rows(_row_gv).Item("pt_pl_id").ToString _
                                                    & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                .InitializeCommand()
                                .FillDataSet(ds_bantu, "prodline")

                                If ds_bantu.Tables(0).Rows.Count = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                    Exit Sub
                                End If

                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    fobject.gv_edit.SetRowCellValue(_row, "sqd_ppn_type", ds.Tables(0).Rows(_row_gv).Item("pt_ppn_type"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_taxable", ds.Tables(0).Rows(_row_gv).Item("pt_taxable"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_inc", ds.Tables(0).Rows(_row_gv).Item("pt_tax_inc"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class", ds.Tables(0).Rows(_row_gv).Item("pt_tax_class"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("tax_class_name"))

                    fobject.gv_edit.SetRowCellValue(_row, "sqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                    fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))

                    fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                    fobject.gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                    fobject.gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                    fobject.gv_edit.BestFitColumns()
                End If
               
            ElseIf fobject.name = "FPartNumberSubtitute" Then
                If _sq_type = "from" Then
                    _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                    fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                Else
                    _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                    fobject._pt_sub_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                End If

            ElseIf fobject.name = "FWorkOrder" Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.__wo_pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")

                fobject.wo_remarks.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code") & ", " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")

            ElseIf fobject.name = "FBillOfMaterial" Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.__bom_pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")

                fobject.bom_desc.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code") & ", " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.bom_um_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_um")


            ElseIf fobject.name = "FProductStructure" Then
                If _sq_type = "header" Then
                    _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                    fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                    fobject.ps_desc.text = ds.Tables(0).Rows(_row_gv).Item("pt_code") & ", " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                Else
                    fobject.gv_edit.SetRowCellValue(_row, "psd_pt_bom_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                    fobject.gv_edit.SetRowCellValue(_row, "psd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                    fobject.gv_edit.SetRowCellValue(_row, "code_code", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "psd_um_conv", 1)
                End If

            ElseIf fobject.name = "FWOMaintenance" Then

                fobject.gv_edit.SetRowCellValue(_row, "wod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                fobject.gv_edit.SetRowCellValue(_row, "wod_qty_per", 0)
                fobject.gv_edit.SetRowCellValue(_row, "wod_cost", get_cost(ds.Tables(0).Rows(_row_gv).Item("pt_id"), _si_id))

            ElseIf fobject.name = "FWOComponentIssue" Then
                fobject.gv_edit.SetRowCellValue(_row, "wocid_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))



            ElseIf fobject.name = FInventoryHistory.Name Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject.__pt_id = ds.Tables(0).Rows(_row_gv).Item("pt_id")

            ElseIf fobject.name = FPreOrder.Name Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_desc.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            ElseIf fobject.name = "FProjectMaintenance" Then
                If grid_call = "gv_edit" Then
                    fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                    fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                    fobject.gv_edit.SetRowCellValue(_row, "prjd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
                    fobject.gv_edit.SetRowCellValue(_row, "prjd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                    fobject.gv_edit.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                ElseIf grid_call = "gv_edit_cust" Then
                    fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                End If
                '=====================
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()

        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

End Class
