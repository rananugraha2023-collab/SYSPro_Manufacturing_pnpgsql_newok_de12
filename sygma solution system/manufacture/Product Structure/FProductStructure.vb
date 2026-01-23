Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FProductStructure
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As New DataSet
    Dim _ps_oid_mstr, _psa_oid_mstr As String
    Dim _ptbom_id_mstr, _ptbom_oid_det As String
    Dim det_awal As String
    Dim _id, _ref As String
    Dim _tglSt, _tglEnd As Date
    Dim mf As New master_new.ModFunction
    Dim _conf_value As String


    Private Sub FProductStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler gc_edit.EmbeddedNavigator.ButtonClick, AddressOf Grid_DataNavigator_ButtonClick

        _conf_value = func_coll.get_conf_file("wf_prod_structure")
        form_first_load()
        If ce_all_data.Checked = True Then
            be_first.Enabled = False
            be_to.Enabled = False
        Else
            be_first.Enabled = True
            be_to.Enabled = True
        End If

        If _conf_value = "0" Then
            xtc_detail.TabPages(1).PageVisible = False
            'xtc_detail.TabPages(3).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(1).PageVisible = True
            'xtc_detail.TabPages(3).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        ps_en_id.Properties.DataSource = dt_bantu
        ps_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ps_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ps_en_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            ps_tran_id.Properties.DataSource = dt_bantu
            ps_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            ps_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            ps_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            ps_tran_id.Properties.DataSource = dt_bantu
            ps_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            ps_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            ps_tran_id.ItemIndex = 0
        End If

    End Sub

    Private Sub ps_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ps_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Function load_pt_bom_mstr(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT en_mstr.en_desc, pt_code as codeptbom, pt_desc1 as descptbom, " _
                                & "  pt_id as idptbom " _
                                & "FROM " _
                                & "  public.pt_mstr  " _
                                & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_pm_code='M' and pt_en_id in (0," & par_en_id & ")" _
                                & " order by pt_desc1"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptbom_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (load_pt_bom_mstr(ps_en_id.EditValue))
        ps_pt_bom_id.Properties.DataSource = dt_bantu
        ps_pt_bom_id.Properties.DisplayMember = dt_bantu.Columns("codeptbom").ToString
        ps_pt_bom_id.Properties.ValueMember = dt_bantu.Columns("idptbom").ToString
        ps_pt_bom_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ps_par", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Desc", "ptbomdesc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ps_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Ratio", "ps_ratio", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Is Active", "ps_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "ps_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "ps_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ps_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "ps_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ps_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "psd_oid", False)
        add_column(gv_detail, "psd_ps_oid", False)
        add_column(gv_detail, "psd_pt_bom_id", False)
        add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Indirect", "psd_indirect", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_detail, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        'If _conf_value = "0" Then
        add_column_copy(gv_detail, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n5")
        'Else
        '    'add_column_copy(gv_detail, "Qty Plan", "psd_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        '    add_column_copy(gv_detail, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        '    add_column_copy(gv_detail, "Qty Variance", "psd_qty_variance", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'End If

        add_column_copy(gv_detail, "Insheet", "psd_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sequence", "psd_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
       
        add_column(gv_edit, "psd_oid", False)
        add_column(gv_edit, "psd_ps_oid", False)
        add_column(gv_edit, "psd_pt_bom_id", False)
        add_column(gv_edit, "Component Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Component Desc", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Indirect", "psd_indirect", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_edit(gv_edit, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        'If _conf_value = "0" Then
        add_column_edit(gv_edit, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n5")
        'Else
        '    add_column_edit(gv_edit, "Qty Plan", "psd_qty_plan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        '    add_column(gv_edit, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        '    add_column(gv_edit, "Qty Variance", "psd_qty_variance", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'End If

        add_column_edit(gv_edit, "Insheet", "psd_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column(gv_edit, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column(gv_edit, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)


        '==================================approval=======================================

        add_column(gv_wf, "wf_ref_code", False)
        add_column(gv_wf, "wf_ref_oid", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "ps_oid", False)
        add_column(gv_email, "psd_oid", False)
        add_column(gv_email, "psd_ps_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Seq", "psd_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Reference", "psd_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty Plan", "psd_qty_plan", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Qty Variance", "psd_qty_variance", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Type", "psd_str_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_smart, "Code", "ps_par", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

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
                    & "   ps_remarks,  " _
                    & "   ps_use_bom, " _
                    & "   ps_pt_bom_id,  " _
                    & "   ps_active, pt_code, " _
                    & "   pt_desc1 AS ptbomdesc, ps_tran_id, ps_trans_id,tran_name,coalesce(ps_rev,0) as ps_rev " _
                    & " FROM  " _
                    & "   public.ps_mstr  " _
                    & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN pt_mstr on (ps_pt_bom_id = pt_id) " _
                    & "  LEFT OUTER JOIN tran_mstr on (ps_mstr.ps_tran_id = tran_id) " _
                    & " WHERE ps_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " & master_new.ClsVar.sUserID.ToString & ") and ps_is_assembly='N'"

        If ce_all_data.Checked = False Then
            get_sequel += " AND ps_pt_bom_id between " & be_first.EditValue & " and " & be_to.EditValue
        End If

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
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
           & " WHERE ps_en_id in (select user_en_id from tconfuserentity " _
           & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "

        If ce_all_data.Checked = False Then
            sql += " AND ps_pt_bom_id between " & be_first.EditValue & " and " & be_to.EditValue
        End If

        load_data_detail(sql, gc_detail, "detail")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " & _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " & _
                  " wf_iscurrent, wf_seq " & _
                  " from wf_mstr w " & _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " & _
                  " inner join ps_mstr on ps_par = wf_ref_code " _
                  & " WHERE ps_en_id in (select user_en_id from tconfuserentity " _
                  & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "

            If ce_all_data.Checked = False Then
                sql += " and ps_pt_bom_id between " & be_first.EditValue & " and " & be_to.EditValue & " "
            End If

            sql += " order by wf_ref_code, wf_seq "

            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
               & "  psd_det.psd_oid, " _
               & "  psd_det.psd_ps_oid, " _
               & "  psd_det.psd_use_bom, " _
               & "  psd_det.psd_pt_bom_id, " _
               & "  psd_det.psd_comp, " _
               & "  psd_det.psd_ref, " _
               & "  psd_det.psd_desc, " _
               & "  psd_det.psd_start_date, " _
               & "  psd_det.psd_end_date, " _
               & "  psd_det.psd_qty,psd_det.psd_qty_plan,psd_det.psd_qty_variance, " _
               & "  psd_det.psd_str_type, " _
               & "  psd_det.psd_scrp_pct, " _
               & "  psd_det.psd_lt_off, " _
               & "  psd_det.psd_op, " _
               & "  psd_det.psd_seq, " _
               & "  psd_det.psd_fcst_pct, " _
               & "  psd_det.psd_group, " _
               & "  psd_det.psd_process, " _
               & "  pt_code, " _
               & "  en_desc, " _
               & "  pt_desc1 || pt_desc2 as pt_desc, " _
               & "  code_mstr_group.code_en_id AS code_group_en_id, " _
               & "  code_mstr_group.code_id AS code_group_id, " _
               & "  code_mstr_group.code_field AS code_group_field, " _
               & "  code_mstr_group.code_code AS code_group_code, " _
               & "  code_mstr_group.code_name AS code_group_name, " _
               & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
               & "  code_mstr_proc.code_id AS code_proc_id, " _
               & "  code_mstr_proc.code_field AS code_proc_field, " _
               & "  code_mstr_proc.code_code AS code_proc_code, " _
               & "  code_mstr_proc.code_name AS code_proc_name,code_mstr.code_name as um_desc,ps_par,ps_desc,ps_oid " _
               & " FROM " _
               & "  psd_det " _
               & "  INNER JOIN ps_mstr on psd_ps_oid=ps_oid " _
               & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
               & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
               & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
               & "  INNER JOIN en_mstr ON en_id = ps_en_id " _
               & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
               & " WHERE ps_en_id in (select user_en_id from tconfuserentity " _
               & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "

            If ce_all_data.Checked = False Then
                sql += " AND ps_pt_bom_id between " & be_first.EditValue & " and " & be_to.EditValue
            End If

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select ps_oid, ps_par,ps_desc, ps_trans_id, false as status from ps_mstr " _
                & " where ps_trans_id ~~* 'd' " _
                & " and ps_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "


            load_data_detail(sql, gc_smart, "smart")
        End If

    End Sub

    Public Overrides Sub relation_detail()
        Try

            gv_detail.Columns("psd_ps_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[psd_ps_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid").ToString & "'")
            gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("psd_ps_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[psd_ps_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid").ToString & "'")
                gv_email.BestFitColumns()

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If e.Column.Name = "psd_qty_plan" Then
            Dim _qty_plan, _qty, _qty_variance As Double

            _qty_plan = SetNumber(e.Value)
            _qty = SetNumber(gv_edit.GetRowCellValue(_row, "psd_qty"))
            _qty_variance = _qty_plan - _qty
            gv_edit.SetRowCellValue(_row, "psd_qty_variance", _qty_variance)

        End If


        If det_awal = "N" Then
            If e.Column.Name = "psd_qty" Then
                '********* Cek Qty Processed

                If e.Value <= 0 Then
                    MessageBox.Show("Qty Invalid ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gv_edit.CancelUpdateCurrentRow()
                    Exit Sub
                End If
                '********************************
            End If

            If e.Column.Name = "psd_pt_bom_id" Then

                If e.Value = _ptbom_id_mstr Then
                    MessageBox.Show("Part/BOM cannot same with Header..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gv_edit.CancelUpdateCurrentRow()
                    Exit Sub
                End If
                '_id(gv_edit.) = e.Value
            End If

            'If e.Column.Name = "psd_ref" Then
            '    _ref(gv_edit.RowCount) = e.Value
            'End If

            'If e.Column.Name = "psd_start_date" Then
            '    _tglSt(gv_edit.RowCount) = e.Value
            'End If

            'If e.Column.Name = "psd_end_date" Then
            '    _tglEnd(gv_edit.RowCount) = e.Value
            'End If

            'If e.Column.Name = "psd_use_bom" Then
            '   

            '    If (e.Value = "Y") Or (e.Value = "N") Then
            '    Else
            '        gv_edit.SetRowCellValue(_row, "psd_use_bom", "N")
            '        Exit Sub
            '    End If
            '    
            'End If

            'If e.Column.Name = "psd_str_type" Then
            '    '********* Cek Qty Processed

            '    If (e.Value = "A") Or (e.Value = "D") Or (e.Value = "J") Or (e.Value = "X") Or (e.Value = "P") Or (e.Value = "O") Or (e.Value = "N") Then
            '    Else
            '        gv_edit.SetRowCellValue(_row, "psd_str_type", "N")
            '        Exit Sub
            '    End If
            '    '********************************
            'End If
        End If

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
        Dim _psd_en_id As Integer = ps_en_id.EditValue

        If _col = "pt_code" Then
            Dim frm As New FPTBOMSrch
            frm.set_win(Me)
            frm._row = _row
            frm._pil = 3
            frm._en_id = _psd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_group_name" Then
            Dim frm As New FCodeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._col = _col
            frm._en_id = _psd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_proc_name" Then
            Dim frm As New FCodeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._col = _col
            frm._en_id = _psd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "op_name" Or _col = "psd_op" Then
            Dim frm As New FOpSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        Dim jml As Integer

        det_awal = "Y"
        Dim ssql As String
        'ssql = "select ro_yield from ro_mstr where ro_pt_id=" & ps_pt_bom_id.EditValue
        'Dim dt As New DataTable

        'dt = master_new.PGSqlConn.GetTableData(ssql)
        'Dim _yield As Double = 0.0
        'For Each dr As DataRow In dt.Rows
        '    _yield = dr(0)
        'Next

        With gv_edit
            jml = .RowCount
            .SetRowCellValue(e.RowHandle, "psd_use_bom", "N")
            .SetRowCellValue(e.RowHandle, "psd_start_date", Now())
            .SetRowCellValue(e.RowHandle, "psd_end_date", Now())
            .SetRowCellValue(e.RowHandle, "psd_qty_plan", 1.0)
            .SetRowCellValue(e.RowHandle, "psd_qty", 0.0)
            '.SetRowCellValue(e.RowHandle, "psd_comp", "-")
            .SetRowCellValue(e.RowHandle, "psd_ref", "-")
            .SetRowCellValue(e.RowHandle, "psd_desc", "-")
            .SetRowCellValue(e.RowHandle, "psd_str_type", "N")
            .SetRowCellValue(e.RowHandle, "psd_insheet_pct", 0.0)
            .SetRowCellValue(e.RowHandle, "psd_lt_off", 0)
            '.SetRowCellValue(e.RowHandle, "psd_op", 0)
            .SetRowCellValue(e.RowHandle, "psd_fcst_pct", 0)
            .SetRowCellValue(e.RowHandle, "psd_group", 0)
            .SetRowCellValue(e.RowHandle, "code_group_name", "-")
            .SetRowCellValue(e.RowHandle, "psd_process", 0)
            .SetRowCellValue(e.RowHandle, "code_proc_name", "-")
            .BestFitColumns()
        End With
        det_awal = "N"

       
    End Sub


    Public Overrides Sub insert_data_awal()
        ps_en_id.ItemIndex = 0
        ps_par.Text = ""
        ps_desc.Text = ""
        ps_remarks.Text = ""
        ps_pt_bom_id.ItemIndex = 0
        ps_active.EditValue = True
        ps_en_id.Focus()
        'ps_par.Enabled = False
        ps_tran_id.ItemIndex = 0
        ps_ratio.EditValue = 1.0

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  psd_det.psd_oid, " _
                        & "  psd_det.psd_ps_oid, " _
                        & "  psd_det.psd_use_bom, " _
                        & "  psd_det.psd_desc as ptbomdesc, " _
                        & "  psd_det.psd_pt_bom_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1 || pt_desc2 as pt_desc, " _
                        & "  psd_det.psd_ref, " _
                        & "  psd_det.psd_desc, " _
                        & "  psd_det.psd_start_date,psd_indirect,'' as op_name, " _
                        & "  psd_det.psd_end_date, " _
                        & "  psd_det.psd_qty,psd_det.psd_qty_plan,psd_det.psd_qty_variance, " _
                        & "  psd_det.psd_str_type, " _
                        & "  psd_det.psd_insheet_pct, " _
                        & "  psd_det.psd_lt_off, " _
                        & "  psd_det.psd_op, " _
                        & "  psd_det.psd_seq, " _
                        & "  psd_det.psd_fcst_pct, " _
                        & "  psd_det.psd_group, " _
                        & "  psd_det.psd_process, " _
                        & "  code_mstr_group.code_en_id AS code_group_en_id, " _
                        & "  code_mstr_group.code_id AS code_group_id, " _
                        & "  code_mstr_group.code_field AS code_group_field, " _
                        & "  code_mstr_group.code_code AS code_group_code, " _
                        & "  code_mstr_group.code_name AS code_group_name, " _
                        & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
                        & "  code_mstr_proc.code_id AS code_proc_id, " _
                        & "  code_mstr_proc.code_field AS code_proc_field, " _
                        & "  code_mstr_proc.code_code AS code_proc_code, " _
                        & "  code_mstr_proc.code_name AS code_proc_name,'' as um_desc " _
                        & "FROM " _
                        & "  psd_det " _
                        & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                        & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                        & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
                        & " where psd_det.psd_seq = -99"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If
        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Function cek_pt_bom_mstr(ByVal ptbom_id As String) As String
        cek_pt_bom_mstr = ""
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT pt_id " _
                                & "FROM " _
                                & "  public.pt_mstr where pt_id = " + ptbom_id.ToString
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptbom_mstr")
                        If ds_bantu.Tables(0).Rows.Count <> 0 Then
                            cek_pt_bom_mstr = "N"
                        Else
                            cek_pt_bom_mstr = "Y"
                        End If

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return cek_pt_bom_mstr
        End Using
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ps_oid As Guid
        _ps_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim ds_bantu As New DataSet
        Dim i, x, sama As Integer

        Dim _ps_id As Integer
        _ps_id = SetInteger(func_coll.GetID("ps_mstr", ps_en_id.GetColumnValue("en_code"), "ps_id", "ps_en_id", ps_en_id.EditValue.ToString))

        Dim _ps_trans_id As String = ""

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(ps_tran_id.EditValue)
            _ps_trans_id = "D"
        Else
            _ps_trans_id = "I"
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
                        .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ps_mstr " _
                                                & "( " _
                                                & "  ps_oid, " _
                                                & "  ps_dom_id, " _
                                                & "  ps_en_id, " _
                                                & "  ps_add_by, " _
                                                & "  ps_add_date, " _
                                                & "  ps_id, " _
                                                & "  ps_par, " _
                                                & "  ps_desc, " _
                                                & "  ps_remarks, " _
                                                & "  ps_use_bom, " _
                                                & "  ps_pt_bom_id,ps_ratio, " _
                                                & "  ps_active,ps_tran_id,ps_trans_id, ps_is_assembly," _
                                                & "  ps_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_ps_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(ps_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetInteger(_ps_id) & ",  " _
                                                & SetSetring(ps_par.Text) & ",  " _
                                                & SetSetring(ps_desc.Text) & ",  " _
                                                & SetSetring(ps_remarks.Text) & ",  " _
                                                & SetSetring(ps_use_bom.Text) & ",  " _
                                                & SetInteger(ps_pt_bom_id.EditValue) & ",  " _
                                                 & SetDec(ps_ratio.EditValue) & ",  " _
                                                & SetBitYN(ps_active.EditValue) & ",  " _
                                                & SetInteger(ps_tran_id.EditValue) & ",  " _
                                                & SetSetring(_ps_trans_id) & ",'N',  " _
                                                & " current_timestamp " _
                                                & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            ''handling data duplicate
                            '_id = ds_edit.Tables(0).Rows(i).Item("psd_pt_bom_id")
                            '_ref = ds_edit.Tables(0).Rows(i).Item("psd_ref")
                            '_tglSt = ds_edit.Tables(0).Rows(i).Item("psd_start_date")
                            '_tglEnd = ds_edit.Tables(0).Rows(i).Item("psd_end_date")
                            'sama = 0
                            'For x = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '    If (_id = ds_edit.Tables(0).Rows(x).Item("psd_pt_bom_id")) And _
                            '        (_ref = ds_edit.Tables(0).Rows(x).Item("psd_ref")) And _
                            '        (FormatDateTime(_tglSt, DateFormat.ShortDate)) = (FormatDateTime(ds_edit.Tables(0).Rows(x).Item("psd_start_date"), DateFormat.ShortDate)) And _
                            '        (FormatDateTime(_tglEnd, DateFormat.ShortDate)) = (FormatDateTime(ds_edit.Tables(0).Rows(x).Item("psd_end_date"), DateFormat.ShortDate)) Then
                            '        sama = sama + 1
                            '        If sama = 2 Then
                            '            'sqlTran.Rollback()
                            '            'MsgBox("Duplicate Part/BOM on detail...", MsgBoxStyle.Critical, "Error..")
                            '            'Return False
                            '            '20120217 belum dipake karena emang bisa double dikarenakan beda operation, tapi masalahnya operation blum user pahami, jadi sementara diloloskan saja dulu
                            '        End If
                            '    End If
                            'Next
                            '****************************************
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.psd_det " _
                                                    & "( " _
                                                    & "  psd_oid, " _
                                                    & "  psd_ps_oid, " _
                                                    & "  psd_add_by, " _
                                                    & "  psd_add_date, " _
                                                    & "  psd_use_bom, " _
                                                    & "  psd_pt_bom_id, " _
                                                    & "  psd_ref, " _
                                                    & "  psd_desc, " _
                                                    & "  psd_start_date, " _
                                                    & "  psd_end_date, " _
                                                    & "  psd_qty, " _
                                                    & "  psd_str_type, " _
                                                    & "  psd_insheet_pct, " _
                                                    & "  psd_lt_off, " _
                                                    & "  psd_op, " _
                                                    & "  psd_seq, " _
                                                    & "  psd_fcst_pct, " _
                                                    & "  psd_group, " _
                                                    & "  psd_process,psd_indirect, " _
                                                    & "  psd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_ps_oid.ToString) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring("N") & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_pt_bom_id")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_ref")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_desc")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("psd_start_date")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("psd_end_date")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_qty")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_str_type")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_insheet_pct")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_lt_off")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_op")) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_fcst_pct")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_group")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_process")) & ",  " _
                                                     & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_indirect").ToString.ToUpper) & ",  " _
                                                    & "current_timestamp" _
                                                    & ")"
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
                                                        & SetInteger(ps_en_id.EditValue) & ",  " _
                                                        & SetSetring(ps_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_ps_oid.ToString) & ",  " _
                                                        & SetSetring(ps_par.Text) & ",  " _
                                                        & SetSetring("Product Structure") & ",  " _
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
                        .Command.CommandText = insert_log("Insert Product Structure " & ps_par.Text)
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
                        set_row(Trim(_ps_oid.ToString), "ps_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        insert = False
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

    Public Overrides Function edit_data() As Boolean
        If _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_trans_id") = "W" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If MyBase.edit_data = True Then
            ps_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ps_oid_mstr = .Item("ps_oid").ToString
                ps_en_id.EditValue = .Item("ps_en_id")
                ps_par.Text = SetString(.Item("ps_par"))
                'ps_par.Enabled = False
                ps_desc.Text = SetString(.Item("ps_desc"))
                ps_remarks.Text = SetString(.Item("ps_remarks"))
                ps_use_bom.Text = SetString(.Item("ps_use_bom"))
                ps_pt_bom_id.EditValue = .Item("ps_pt_bom_id")
                ps_tran_id.EditValue = .Item("ps_tran_id")
                ps_active.EditValue = SetBitYNB(.Item("ps_active"))
                'ps_ratio.EditValue = .Item("ps_ratio")
            End With

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  psd_det.psd_oid, " _
                            & "  psd_det.psd_ps_oid, " _
                            & "  psd_det.psd_use_bom, " _
                            & "  pt_code, " _
                            & "  coalesce(pt_desc1,'') || coalesce(pt_desc2,'') as pt_desc, " _
                            & "  psd_det.psd_pt_bom_id, " _
                            & "  psd_det.psd_comp, " _
                            & "  psd_det.psd_ref, " _
                            & "  psd_det.psd_desc, " _
                            & "  psd_det.psd_start_date,psd_indirect, " _
                            & "  psd_det.psd_end_date, " _
                            & "  psd_det.psd_qty, "

                        If SetString(ds.Tables(0).Rows(row).Item("ps_trans_id")) = "I" Or SetString(ds.Tables(0).Rows(row).Item("ps_trans_id")) = "" Then
                            .SQL += "psd_det.psd_qty as psd_qty_plan,0.0 as psd_qty_variance,"
                        ElseIf SetString(ds.Tables(0).Rows(row).Item("ps_trans_id")) = "D" Then
                            .SQL += "psd_det.psd_qty_plan,psd_det.psd_qty_variance,"
                        End If

                        .SQL += "  psd_det.psd_str_type, " _
                            & "  psd_det.psd_insheet_pct, " _
                            & "  psd_det.psd_lt_off, " _
                            & "  psd_det.psd_op,op_name, " _
                            & "  psd_det.psd_seq, " _
                            & "  psd_det.psd_fcst_pct, " _
                            & "  psd_det.psd_group, " _
                            & "  psd_det.psd_process, " _
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
                            & "FROM " _
                            & "  psd_det " _
                            & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                            & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                            & "  INNER JOIN pt_mstr on pt_id = psd_pt_bom_id " _
                            & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
                            & "  LEFT OUTER JOIN op_mstr on psd_op=op_code " _
                            & " where public.psd_det.psd_ps_oid = '" + ds.Tables(0).Rows(row).Item("ps_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim i, x, sama, _ps_rev As Integer
        Dim ds_bantu As New DataSet
        Dim _ps_trans_id As String = ""
        Dim ssqls As New ArrayList

        If _conf_value = "1" Then
            If ps_tran_id.EditValue Is System.DBNull.Value Then
                Box("Transaction can't blank")
                edit = False
                Exit Function
            End If

            ds_bantu = func_data.load_aprv_mstr(ps_tran_id.EditValue)
            _ps_trans_id = "D"
        Else
            _ps_trans_id = "I"
        End If

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_trans_id").ToString.ToUpper = "D" Then
            _ps_rev = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_rev")
        Else : _ps_rev = SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_rev")) + 1
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
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.ps_mstr   " _
                                                & "SET  " _
                                                & "  ps_en_id = " & SetInteger(ps_en_id.EditValue) & ",  " _
                                                & "  ps_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ps_upd_date = current_timestamp,  " _
                                                & "  ps_par = " & SetSetring(ps_par.Text) & ",  " _
                                                & "  ps_desc = " & SetSetring(ps_desc.Text) & ",  " _
                                                & "  ps_remarks = " & SetSetring(ps_remarks.Text) & ",  " _
                                                & "  ps_use_bom = " & SetSetring(ps_use_bom.Text) & ",  " _
                                                & "  ps_pt_bom_id = " & SetInteger(ps_pt_bom_id.EditValue) & ",  " _
                                                & "  ps_ratio = " & SetDec(ps_ratio.EditValue) & ",  " _
                                                & "  ps_trans_id = " & SetSetring(_ps_trans_id) & ",  " _
                                                & "  ps_tran_id = " & SetInteger(ps_tran_id.EditValue) & ",  " _
                                                & "  ps_rev = " & SetInteger(_ps_rev) & ",  " _
                                                & "  ps_active = " & SetBitYN(ps_active.EditValue) _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  ps_oid = " & SetSetring(_ps_oid_mstr.ToString) & "  " _
                                                & ";"


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from psd_det where psd_ps_oid = '" + _ps_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            ''handling data duplicate
                            '_id = ds_edit.Tables(0).Rows(i).Item("psd_pt_bom_id")
                            '_ref = SetString(ds_edit.Tables(0).Rows(i).Item("psd_ref"))
                            '_tglSt = ds_edit.Tables(0).Rows(i).Item("psd_start_date")
                            '_tglEnd = ds_edit.Tables(0).Rows(i).Item("psd_end_date")
                            'sama = 0
                            'For x = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '    If (_id = ds_edit.Tables(0).Rows(x).Item("psd_pt_bom_id")) And _
                            '        (_ref = SetString(ds_edit.Tables(0).Rows(x).Item("psd_ref"))) And _
                            '        (FormatDateTime(_tglSt, DateFormat.ShortDate)) = (FormatDateTime(ds_edit.Tables(0).Rows(x).Item("psd_start_date"), DateFormat.ShortDate)) And _
                            '        (FormatDateTime(_tglEnd, DateFormat.ShortDate)) = (FormatDateTime(ds_edit.Tables(0).Rows(x).Item("psd_end_date"), DateFormat.ShortDate)) Then
                            '        sama = sama + 1
                            '        If sama = 2 Then
                            '            'sqlTran.Rollback()
                            '            'MsgBox("Duplicate Part/BOM on detail...", MsgBoxStyle.Critical, "Error..")
                            '            'Return False
                            '            '20120217 belum dipake karena emang bisa double dikarenakan beda operation, tapi masalahnya operation blum user pahami, jadi sementara diloloskan saja dulu
                            '        End If
                            '    End If
                            'Next
                            ''****************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.psd_det " _
                                                    & "( " _
                                                    & "  psd_oid, " _
                                                    & "  psd_ps_oid, " _
                                                    & "  psd_add_by, " _
                                                    & "  psd_add_date, " _
                                                    & "  psd_use_bom, " _
                                                    & "  psd_pt_bom_id, " _
                                                    & "  psd_ref, " _
                                                    & "  psd_desc, " _
                                                    & "  psd_start_date, " _
                                                    & "  psd_end_date, " _
                                                    & "  psd_qty, psd_qty_plan,psd_qty_variance," _
                                                    & "  psd_str_type, " _
                                                    & "  psd_insheet_pct, " _
                                                    & "  psd_lt_off, " _
                                                    & "  psd_op, " _
                                                    & "  psd_seq, " _
                                                    & "  psd_fcst_pct, " _
                                                    & "  psd_group, " _
                                                    & "  psd_process,psd_indirect, " _
                                                    & "  psd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_ps_oid_mstr.ToString) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_use_bom")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_pt_bom_id")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_ref")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_desc")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("psd_start_date")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("psd_end_date")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_qty")) & ",  " _
                                                     & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_qty_plan")) & ",  " _
                                                      & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_qty_variance")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_str_type")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_insheet_pct")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_lt_off")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_op")) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("psd_fcst_pct")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_group")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("psd_process")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("psd_indirect").ToString.ToUpper) & ",  " _
                                                    & "current_timestamp" _
                                                    & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If _conf_value = "1" Then
                            If SetNumber(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_tran_id")) <> ps_tran_id.EditValue Or _
                               ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_trans_id") = "I" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _ps_oid_mstr.ToString + "'"
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
                                                            & SetInteger(ps_en_id.EditValue) & ",  " _
                                                            & SetSetring(ps_tran_id.EditValue) & ",  " _
                                                            & SetSetring(_ps_oid_mstr.ToString) & ",  " _
                                                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")) & ",  " _
                                                            & SetSetring("Product Structure") & ",  " _
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
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Product Structure " & ps_par.Text)
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
                        set_row(Trim(_ps_oid_mstr.ToString), "ps_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        edit = False
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
        Dim ssqls As New ArrayList


        row = BindingContext(ds.Tables(0)).Position
        If _conf_value = "1" Then
            If ds.Tables(0).Rows(row).Item("ps_trans_id") <> "D" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        End If


        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Akan Menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from ps_mstr where ps_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Product Structure " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par"))
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
                            delete_data = False
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

    Private Sub ps_pt_bom_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ps_pt_bom_id.EditValueChanged
        Try
            ps_use_bom.Text = "N"
            _ptbom_id_mstr = ps_pt_bom_id.EditValue.ToString
            ps_par.EditValue = ps_pt_bom_id.Text
            ps_desc.EditValue = ps_pt_bom_id.GetColumnValue("descptbom")
            ps_remarks.EditValue = ps_pt_bom_id.GetColumnValue("descptbom")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
       
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = "select user_en_id from tconfuserentity where userid = " + master_new.ClsVar.sUserID.ToString + " "
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = "select user_en_id from tconfuserentity where userid = " + master_new.ClsVar.sUserID.ToString + " "
        frm._pil = 2
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub ce_all_data_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ce_all_data.EditValueChanged
        If ce_all_data.Checked = True Then
            be_first.Enabled = False
            be_to.Enabled = False
        Else
            be_first.Enabled = True
            be_to.Enabled = True
        End If
    End Sub

    Public Overrides Sub approve_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String

        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid").ToString
        _colom = "ps_trans_id"
        _table = "ps_mstr"
        _criteria = "ps_par"
        _initial = "ps"
        _type = "ps"
        _title = "Product Structure"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_oid")
        '_colom = "ps_trans_id"
        '_table = "ps_mstr"
        '_criteria = "ps_par"
        '_initial = "ps"
        '_type = "ps"
        '_title = "Product Structure"
        'cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
        Box("This menu is not available")

    End Sub

    Public Overrides Sub reminder_mail()
        Dim _code, _type, _user, _title As String

        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
        _type = "ps"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Product Structure"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If func_coll.get_conf_file("smart_approve") = "0" Then
            MessageBox.Show("Smart Approve Is Disable...Please Approve Line To Process Your Data..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'kayaknya harus dibetulin dulu smart approve ini....(gimana kalau approve gara2 dirollback, kayaknya belum diakomodir)

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu, _title As String
        Dim i As Integer

        _title = "Product Structure"

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        Dim sSQLs As New ArrayList

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("ps_oid").FilterInfo = _
                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[ps_oid] = '" & ds.Tables("smart").Rows(i).Item("ps_oid").ToString & "'")
                    gv_email.BestFitColumns()

                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("ps_par"), 0)
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
                                .Command.CommandText = "update ps_mstr set ps_trans_id = '" + _trans_id + "'," + _
                                               " ps_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " ps_upd_date = current_timestamp " + _
                                               " where ps_oid = '" + ds.Tables("smart").Rows(i).Item("ps_oid").ToString + "'"

                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("ps_par") + "'" + _
                                                       " and wf_seq = 0"

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

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("ps_par"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("ps_par") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email(_title, ds.Tables("smart").Rows(i).Item("ps_par")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

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
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Grid_DataNavigator_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
        If _conf_value = "1" Then
            If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
                Dim _qty As Double
                _qty = SetNumber(gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "psd_qty"))
                If _qty > 0 Then
                    Box("Can't Delete Qty > 0")
                    e.Handled = True
                End If
            End If
        End If
    End Sub

    Private Sub btBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBrowse.Click
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._pil = 0
        frm._en_id = ps_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

   
    Private Sub copy_ps_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles copy_ps.ButtonClick
        Try
            Dim frm As New FProdStrucSearch()
            frm.set_win(Me)
            frm._en_id = ps_en_id.EditValue
            frm._prj_code = ""
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

   
End Class
