Imports master_new.ModFunction

Public Class FPartNumberSearchCheck
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data
    Public _filter As String

    Private Sub FPartNumberSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            add_column_edit(gv_master, "Check", "check_list", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        get_sequel = ""

        If fobject.name = "FSalesOrder" Then
            If _tran_oid.ToString <> "" Then
                get_sequel = "SELECT  " _
                    & "  pod_oid, " _
                    & "  pod_po_oid, " _
                    & "  pod_seq, " _
                    & "  pod_qty, " _
                    & "  pod_um, " _
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
                    & "  pt_taxable,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                    & "  pt_tax_inc, " _
                    & "  pt_tax_class, " _
                    & "  pt_ppn_type, " _
                    & "  pod_cost as invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_pl_id, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name, " _
                    & "  coalesce(pod_qty_so,0) pod_qty_so, " _
                    & "  pod_qty - coalesce(pod_qty_so,0) qty_open " _
                    & "FROM  " _
                    & "  public.pod_det " _
                    & "  inner join po_mstr on po_oid = pod_po_oid " _
                    & "  inner join pt_mstr on pt_id = pod_pt_id " _
                    & "  inner join en_mstr on en_id = pt_en_id " _
                    & "  inner join si_mstr on si_id = pod_si_id " _
                    & "  inner join loc_mstr on loc_id = pt_loc_id " _
                    & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & "  left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & "  where pod_qty - coalesce(pod_qty_so,0) > 0" _
                    & " and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and po_oid = '" + _tran_oid + "'"
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
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " inner join invct_table on invct_pt_id = pt_id " _
                    & " inner join si_mstr on si_id = invct_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and invct_si_id = " + _si_id.ToString _
                    & " and pt_en_id in (" + _en_id_coll + ")"
            End If
        ElseIf fobject.name = "FInventoryReceipts" Then
            If _tran_oid.ToString <> "" Then
                get_sequel = "SELECT  " _
                    & "  sod_oid, " _
                    & "  sod_so_oid, " _
                    & "  sod_seq, " _
                    & "  sod_qty, " _
                    & "  sod_um, " _
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
                    & "  sod_cost as invct_cost, " _
                    & "  pt_price, " _
                    & "  pt_pl_id, " _
                    & "  tax_class_mstr.code_name as tax_class_name, " _
                    & "  pt_ppn_type, " _
                    & "  um_mstr.code_name as um_name, " _
                    & "  coalesce(sod_qty_ir,0) sod_qty_ir, " _
                    & "  sod_qty - coalesce(sod_qty_ir,0) qty_open " _
                    & "FROM  " _
                    & "  public.sod_det " _
                    & "  inner join so_mstr on so_oid = sod_so_oid " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join en_mstr on en_id = pt_en_id " _
                    & "  inner join loc_mstr on loc_id = pt_loc_id " _
                    & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & "  left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & "  inner join si_mstr on si_id = sod_si_id " _
                    & "  where sod_qty - coalesce(sod_qty_ir,0) > 0" _
                    & " and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and so_oid = '" + _tran_oid + "'"
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
            If _so_type = "from" Then
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
        ElseIf fobject.name = FInventoryReportDetailLog.Name Then
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
                  & "  um_mstr.code_name as um_name, false as check_list " _
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
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " inner join invct_table on invct_pt_id = pt_id " _
                    & " inner join si_mstr on si_id = invct_si_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and invct_si_id = " + _si_id.ToString _
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
        ElseIf fobject.name = "FSalesOrder" Then
        End If

        'pt_type ~~* 'I' dikomentarin agar barang bisa ditransfer atau diapapun walaupun bukan Inventory
        get_sequel += " and pt_its_id=1 "

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

            If fobject.name = FInventoryReportDetailLog.Name Then
                Dim _pt_code, _pt_id As String
                _pt_code = ""
                _pt_id = ""
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("check_list") = True Then
                        _pt_id = _pt_id & ds.Tables(0).Rows(i).Item("pt_id") & ","
                        _pt_code = _pt_code & ds.Tables(0).Rows(i).Item("pt_desc1") & ","
                    End If
                Next

                _obj.text = Microsoft.VisualBasic.Left(_pt_code, Len(_pt_code) - 1)
                fobject._par_item = Microsoft.VisualBasic.Left(_pt_id, Len(_pt_id) - 1)

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
