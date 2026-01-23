Imports master_new.ModFunction

Public Class FProdStrucSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _prj_code As String = ""
    Public _pt_id As Integer

    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub FProdStrucSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()

        If fobject.name = FWorkOrder.Name Or fobject.name = FCogsSimulation.Name Then
            If _obj.name = "wo_bom_id" Then
                add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Partnumber Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Partnumber Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Status", "ps_trans_id", DevExpress.Utils.HorzAlignment.Default)
            Else
                add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Partnumber Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Partnumber Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                add_column(gv_master, "Status", "ps_trans_id", DevExpress.Utils.HorzAlignment.Default)
            End If

        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Prod. Structure Number", "ps_id", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "ps_par", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Partnumber Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Partnumber Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Status", "ps_trans_id", DevExpress.Utils.HorzAlignment.Default)
        End If

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        If fobject.name = FWorkOrder.Name Then
            If _prj_code = "" Then
                'jika tidak menggunakan SO
                get_sequel = "SELECT   " _
                            & "   en_desc,  " _
                            & "   ps_oid,  " _
                            & "   ps_dom_id,  " _
                            & "   ps_en_id,  " _
                            & "   ps_add_by,  " _
                            & "   ps_add_date,  " _
                            & "   ps_upd_by,  " _
                            & "   ps_upd_date,  " _
                            & "   ps_par,  " _
                            & "   ps_id,  " _
                            & "   ps_desc,  " _
                            & "   ps_use_bom, " _
                            & "   ps_pt_bom_id,  " _
                            & "   ps_active, " _
                            & "   ps_trans_id, " _
                            & "   pt_desc1, " _
                            & "   pt_desc2 " _
                            & " FROM  " _
                            & "   public.ps_mstr  " _
                            & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                            & "  INNER JOIN pt_mstr on pt_id = ps_pt_bom_id where 1=1 "
            Else

                If _obj.name = "wo_ps_id" Then
                    'mencari ps dr partnumber
                    'ini untuk product structure

                    get_sequel = "SELECT   " _
                            & "   en_desc,  " _
                            & "   ps_oid,  " _
                            & "   ps_dom_id,  " _
                            & "   ps_en_id,  " _
                            & "   ps_add_by,  " _
                            & "   ps_add_date,  " _
                            & "   ps_upd_by,  " _
                            & "   ps_upd_date,  " _
                            & "   ps_par,  " _
                            & "   ps_id,  " _
                            & "   ps_desc,  " _
                            & "   ps_use_bom, " _
                            & "   ps_pt_bom_id,  " _
                            & "   ps_active, " _
                            & "   ps_trans_id,pt_code, " _
                            & "   pt_desc1, " _
                            & "   pt_desc2 " _
                            & " FROM  " _
                            & "   public.ps_mstr  " _
                            & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                            & "  INNER JOIN pt_mstr on pt_id = ps_pt_bom_id where (pt_code ~~* '%" _
                            + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) _
                            + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')  " _
                            & " and ps_pt_bom_id =" & _pt_id & " "

                ElseIf _obj.name = "wo_pt_id_prj" Then
                    'untuk partnumber wo

                    get_sequel = "SELECT  " _
                        & "  en_id, " _
                        & "  en_desc, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  coalesce(pt_desc2,'') as pt_desc2, " _
                        & "  pt_cost, " _
                        & "  pt_price, " _
                        & "  pt_type, " _
                        & "  pt_um, " _
                        & "  pt_pl_id, " _
                        & "  pt_ls, " _
                        & "  pt_loc_id, " _
                        & "  loc_desc, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  coalesce(0,0) as sct_total, " _
                        & "  coalesce(0,0) as sct_price,sod_qty,sod_price,sod_oid " _
                        & "FROM  " _
                        & "  public.pt_mstr" _
                        & " inner join en_mstr on en_id = pt_en_id " _
                        & " left outer join loc_mstr on loc_id = pt_loc_id " _
                        & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                         & " inner join sod_det  on sod_pt_id = pt_id " _
                         & " inner join so_mstr  on so_oid = sod_so_oid " _
                        & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and en_active ~~* 'Y'" _
                        & " and pt_its_id = 1 and so_code ='" & _prj_code & "'"


                ElseIf _obj.name = "wo_pt_id" Then
                    'untuk partnumber wo

                    get_sequel = "SELECT  " _
                        & "  en_id, " _
                        & "  en_desc, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  coalesce(pt_desc2,'') as pt_desc2, " _
                        & "  pt_cost, " _
                        & "  pt_price, " _
                        & "  pt_type, " _
                        & "  pt_um, " _
                        & "  pt_pl_id, " _
                        & "  pt_ls, " _
                        & "  pt_loc_id, " _
                        & "  loc_desc, " _
                        & "  um_mstr.code_name as um_name " _
                        & "FROM  " _
                        & "  public.pt_mstr" _
                        & " inner join en_mstr on en_id = pt_en_id " _
                        & " left outer join loc_mstr on loc_id = pt_loc_id " _
                        & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                        & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and en_active ~~* 'Y'" _
                        & " and pt_its_id = 1 and pt_id in (select psd_pt_bom_id from public.get_ps_first( " _
                        & "(select ps_id from ps_mstr where ps_mstr.ps_pt_bom_id=" & _pt_id & " limit 1) ,1) where variable_4>0) "

                End If


            End If
        ElseIf fobject.name = FWorkInProgress.Name Then
            get_sequel = "SELECT   " _
                    & "   en_desc,  " _
                    & "   ps_oid,  " _
                    & "   ps_dom_id,  " _
                    & "   ps_en_id,  " _
                    & "   ps_add_by,  " _
                    & "   ps_add_date,  " _
                    & "   ps_upd_by,  " _
                    & "   ps_upd_date,  " _
                    & "   ps_par,  " _
                    & "   ps_id,  " _
                    & "   ps_desc,  " _
                    & "   ps_use_bom, " _
                    & "   ps_pt_bom_id,  " _
                    & "   ps_active, " _
                    & "   ps_trans_id, " _
                    & "   pt_desc1, " _
                    & "   pt_desc2 " _
                    & " FROM  " _
                    & "   public.ps_mstr  " _
                    & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN pt_mstr on pt_id = ps_pt_bom_id " _
                    & " where ps_pt_bom_id in (select   psd_pt_bom_id  from get_all_header((select wo_pt_id from wo_mstr where wo_oid='" & _
                    _prj_code & "'),'Y',1,'Y',CURRENT_DATE) where ps_is_header='Y') or ps_pt_bom_id=(select wo_pt_id from wo_mstr where wo_oid='" & _
                    _prj_code & "') "
        ElseIf fobject.name = FProdStrucTree.Name Or fobject.name = FRoutingStrucTree.Name Or fobject.name = FCogsSimulation.Name Then
            get_sequel = "SELECT   " _
                    & "   en_desc,  " _
                    & "   ps_oid,  " _
                    & "   ps_dom_id,  " _
                    & "   ps_en_id,  " _
                    & "   ps_add_by,  " _
                    & "   ps_add_date,  " _
                    & "   ps_upd_by,  " _
                    & "   ps_upd_date,  " _
                    & "   ps_par,  " _
                    & "   ps_id,  " _
                    & "   ps_desc,  " _
                    & "   ps_use_bom, " _
                    & "   ps_pt_bom_id,  " _
                    & "   ps_active, " _
                    & "   ps_trans_id,pt_code, " _
                    & "   pt_desc1, " _
                    & "   pt_desc2 " _
                    & " FROM  " _
                    & "   public.ps_mstr  " _
                    & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN pt_mstr on pt_id = ps_pt_bom_id where (pt_code ~~* '%" _
                    + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) _
                    + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%') and ps_is_assembly='N' "
        Else
            get_sequel = "SELECT   " _
                    & "   en_desc,  " _
                    & "   ps_oid,  " _
                    & "   ps_dom_id,  " _
                    & "   ps_en_id,  " _
                    & "   ps_add_by,  " _
                    & "   ps_add_date,  " _
                    & "   ps_upd_by,  " _
                    & "   ps_upd_date,  " _
                    & "   ps_par,  " _
                    & "   ps_id,  " _
                    & "   ps_desc,  " _
                    & "   ps_use_bom, " _
                    & "   ps_pt_bom_id,  " _
                    & "   ps_active, " _
                    & "   pt_desc1, " _
                    & "   pt_desc2 " _
                    & " FROM  " _
                    & "   public.ps_mstr  " _
                    & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN pt_mstr on pt_id = ps_pt_bom_id"
        End If

        If fobject.name <> FWorkOrder.Name Then
            get_sequel += " and (ps_par ~~* '%" & te_search.Text & "%' or ps_desc ~~* '%" & te_search.Text & "%')"
        End If

        Return get_sequel

    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
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

        Dim ds_bantu As New DataSet
        If fobject.name = FProdStrucTree.Name Or fobject.name = FRoutingStrucTree.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & ", " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_id")

        ElseIf fobject.name = FPartnumberSubtitute.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_id")

        ElseIf fobject.name = FWorkOrder.Name Then
            If func_coll.get_conf_file("wf_prod_structure") = "1" Then
                If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_trans_id") <> "I" Then
                    MessageBox.Show("Not Approval Product Structure...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            If _obj.name = "wo_ps_id" Then
                _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & ", " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1")
                _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_id")
            ElseIf _obj.name = "wo_pt_id_prj" Then
                _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & ", " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1")
                _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_id")
                fobject._wo_sod_oid = SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sod_oid"))
                fobject.wo_qty.editvalue = ds.Tables(0).Rows(_row_gv).Item("sod_qty")
                fobject.wo_cost.editvalue = ds.Tables(0).Rows(_row_gv).Item("sod_price")
            ElseIf _obj.name = "wo_pt_id" Then
                _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & ", " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1")
                _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_id")

            End If

        ElseIf fobject.name = FWorkInProgress.Name Then
            If func_coll.get_conf_file("wf_prod_structure") = "1" Then
                If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_trans_id") <> "I" Then
                    MessageBox.Show("Not Approval Product Structure...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_pt_bom_id")
            fobject.pt_desc1.text = SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1"))
            fobject.pt_desc2.text = SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc2"))
            fobject.wim_pt_um.editvalue = master_new.PGSqlConn.GetRowInfo("select pt_um from pt_mstr where pt_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_pt_bom_id"))(0)


        ElseIf fobject.name = FWOBillMaintenance.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wod_pt_bom_id", ds.Tables(0).Rows(_row_gv).Item("ps_pt_bom_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("ps_par"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "wod_qty_per", 0.0)
            fobject.gv_edit.SetRowCellValue(_row, "wod_qty_req", 0.0)

        ElseIf fobject.name = FProductStructure.Name Or fobject.name = FProductStructureAssembly.Name Then
            fobject.copy_ps.editvalue = SetString(ds.Tables(0).Rows(_row_gv).Item("ps_par")) & " " & SetString(ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            Dim sSQL As String
            sSQL = "SELECT  " _
                  & "  psd_det.psd_oid, " _
                  & "  psd_det.psd_ps_oid, " _
                  & "  psd_det.psd_use_bom, " _
                  & "  psd_det.psd_pt_bom_id, " _
                  & "  psd_det.psd_comp, " _
                  & "  psd_det.psd_ref,psd_indirect, " _
                  & "  psd_det.psd_desc, " _
                  & "  psd_det.psd_start_date, " _
                  & "  psd_det.psd_end_date, " _
                  & "  psd_det.psd_qty,psd_det.psd_qty_plan,psd_det.psd_qty_variance, " _
                  & "  psd_det.psd_str_type, " _
                  & "  psd_det.psd_yield_pct, " _
                  & "  psd_det.psd_lt_off, " _
                  & "  psd_det.psd_op,op_name, " _
                  & "  psd_det.psd_seq, " _
                  & "  psd_det.psd_fcst_pct, " _
                  & "  psd_det.psd_group, " _
                  & "  psd_det.psd_process, " _
                  & "  pt_code, " _
                  & "  coalesce(pt_desc1,'') || coalesce(pt_desc2,'') as pt_desc, " _
                  & "  code_mstr_group.code_en_id AS code_group_en_id, " _
                  & "  code_mstr_group.code_id AS code_group_id, " _
                  & "  code_mstr_group.code_field AS code_group_field, " _
                  & "  code_mstr_group.code_code AS code_group_code, " _
                  & "  code_mstr_group.code_name AS code_group_name, " _
                  & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
                  & "  code_mstr_proc.code_id AS code_proc_id, " _
                  & "  code_mstr_proc.code_field AS code_proc_field, " _
                  & "  code_mstr_proc.code_code AS code_proc_code, " _
                  & "  code_mstr_proc.code_name AS code_proc_name,code_mstr.code_name as um_desc " _
                  & " FROM " _
                  & "  psd_det " _
                  & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                  & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                  & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
                  & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
                  & "  INNER JOIN ps_mstr on psd_ps_oid=ps_oid " _
                  & "  LEFT OUTER JOIN op_mstr on psd_op=op_code " _
                  & " WHERE ps_id = " & ds.Tables(0).Rows(_row_gv).Item("ps_id")



            sSQL = "SELECT  " _
                       & "  psd_det.psd_oid, " _
                       & "  psd_det.psd_ps_oid, " _
                       & "  psd_det.psd_use_bom, " _
                       & "  psd_det.psd_pt_bom_id, " _
                       & "  psd_det.psd_comp, " _
                       & "  psd_det.psd_ref,psd_indirect, " _
                       & "  psd_det.psd_desc, " _
                       & "  psd_det.psd_start_date, " _
                       & "  psd_det.psd_end_date, " _
                       & "  psd_det.psd_qty,psd_det.psd_qty_plan,psd_det.psd_qty_variance, " _
                       & "  psd_det.psd_str_type, " _
                       & "  psd_det.psd_insheet_pct, " _
                       & "  psd_det.psd_lt_off, " _
                       & "  psd_det.psd_op,op_name, " _
                       & "  psd_det.psd_seq, " _
                       & "  psd_det.psd_fcst_pct, " _
                       & "  psd_det.psd_group, " _
                       & "  psd_det.psd_process, " _
                       & "  pt_code, " _
                       & "  coalesce(pt_desc1,'') || coalesce(pt_desc2,'') as pt_desc, " _
                       & "  code_mstr_group.code_en_id AS code_group_en_id, " _
                       & "  code_mstr_group.code_id AS code_group_id, " _
                       & "  code_mstr_group.code_field AS code_group_field, " _
                       & "  code_mstr_group.code_code AS code_group_code, " _
                       & "  code_mstr_group.code_name AS code_group_name, " _
                       & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
                       & "  code_mstr_proc.code_id AS code_proc_id, " _
                       & "  code_mstr_proc.code_field AS code_proc_field, " _
                       & "  code_mstr_proc.code_code AS code_proc_code, " _
                       & "  code_mstr_proc.code_name AS code_proc_name,code_mstr.code_name as um_desc " _
                       & " FROM " _
                       & "  psd_det " _
                       & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                       & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                       & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
                       & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
                       & "  INNER JOIN ps_mstr on psd_ps_oid=ps_oid " _
                       & "  LEFT OUTER JOIN op_mstr on psd_op=op_code " _
                        & " WHERE ps_id = " & ds.Tables(0).Rows(_row_gv).Item("ps_id")

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)
            Dim i As Integer = 0
            For Each dr As DataRow In dt.Rows
                fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.Append)
                fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)
                fobject.gv_edit.SetRowCellValue(i, "psd_pt_bom_id", dr("psd_pt_bom_id"))
                fobject.gv_edit.SetRowCellValue(i, "pt_code", dr("pt_code"))
                fobject.gv_edit.SetRowCellValue(i, "pt_desc", dr("pt_desc"))
                fobject.gv_edit.SetRowCellValue(i, "um_desc", dr("um_desc"))
                fobject.gv_edit.SetRowCellValue(i, "psd_indirect", dr("psd_indirect"))
                fobject.gv_edit.SetRowCellValue(i, "psd_desc", dr("psd_desc"))
                fobject.gv_edit.SetRowCellValue(i, "psd_start_date", dr("psd_start_date"))
                fobject.gv_edit.SetRowCellValue(i, "psd_end_date", dr("psd_end_date"))

                fobject.gv_edit.SetRowCellValue(i, "psd_qty", dr("psd_qty"))
                fobject.gv_edit.SetRowCellValue(i, "psd_insheet_pct", dr("psd_insheet_pct"))
                fobject.gv_edit.SetRowCellValue(i, "psd_op", dr("psd_op"))
                fobject.gv_edit.SetRowCellValue(i, "op_name", dr("op_name"))

                fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)
                i += 1
            Next
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FCogsSimulation.Name Then

            Dim ssql As String
            ssql = "SELECT  " _
                & "  a.ro_yield " _
                & "FROM " _
                & "  public.ro_mstr a " _
                & "WHERE " _
                & "  a.ro_pt_id = " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_pt_bom_id")

            Dim dt_ro As New DataTable
            dt_ro = master_new.PGSqlConn.GetTableData(ssql)

            If dt_ro.Rows.Count > 0 Then
                fobject.cogsc_yield.editvalue = dt_ro.Rows(0).Item(0)
            Else
                Box("This item does not have routing")
                Exit Sub
            End If

            fobject.cogsc_pt_id.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_pt_bom_id")
            fobject.cogsc_pt_id.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & ", " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1")
        End If
    End Sub
End Class
