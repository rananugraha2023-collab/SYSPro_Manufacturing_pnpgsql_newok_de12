Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FAssetMaster
    Dim ssql As String
    Dim _ptnr_oid As String
    Public dt_bantu As DataTable
    Dim ds_edit As DataSet
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAssetMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        ass_en_id.Properties.DataSource = dt_bantu
        ass_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ass_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ass_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = func_data.load_pt_class("A','E")
        ass_class.Properties.DataSource = dt_bantu
        ass_class.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_class.Properties.ValueMember = dt_bantu.Columns("code_code").ToString
        ass_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ptnr_mstr())
        ass_ptnr_id.Properties.DataSource = dt_bantu
        ass_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        ass_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        ass_ptnr_id.ItemIndex = 0

    End Sub

    Private Sub sc_le_ptnr_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ass_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = func_data.load_pt_mstr(ass_en_id.EditValue)
        ass_pt_id.Properties.DataSource = dt_bantu
        ass_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_code").ToString
        ass_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        ass_pt_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = func_data.load_end_user(ass_en_id.EditValue)
        ass_emp_id.Properties.DataSource = dt_bantu
        ass_emp_id.Properties.DisplayMember = dt_bantu.Columns("xemp_name").ToString
        ass_emp_id.Properties.ValueMember = dt_bantu.Columns("xemp_id").ToString
        ass_emp_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "emp_dept"))
        ass_emp_dept.Properties.DataSource = dt_bantu
        ass_emp_dept.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_emp_dept.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_emp_dept.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "emp_region"))
        ass_emp_rg.Properties.DataSource = dt_bantu
        ass_emp_rg.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_emp_rg.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_emp_rg.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "ass_st_purc"))
        ass_st_purc.Properties.DataSource = dt_bantu
        ass_st_purc.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_st_purc.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_st_purc.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "ass_lic_type"))
        ass_lic_type.Properties.DataSource = dt_bantu
        ass_lic_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_lic_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_lic_type.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Line", "ass_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "group", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Asset Code", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Serial Number", "ass_sn", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ass_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Barcode", "ass_barcode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Class", "ass_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status Purc", "status_purc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Licence Type", "licence", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Model", "ass_model", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "ass_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Assign", "ass_qty_assgn", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Del", "ass_qty_del", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Status", "its_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Service Date", "ass_service_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Garansi Date", "ass_gar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost", "ass_service_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Salvage Cost", "ass_salvage_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Basis Cost", "ass_basis_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Depresaisi Akumulasi", "ass_depr_acum", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cost After Depresiasi", "cost_sisa", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Depresiasi Date", "ass_depr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Dispose Cost", "ass_disp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Dispose Date", "ass_disp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Confirm", "ass_confirm", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Department", "department", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Regional", "regional", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Req No", "ass_ref_req", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO No", "ass_ref_po", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Receipt No", "ass_ref_rcpt", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_detail, "assbk_ass_oid", False)
        'add_column_copy(gv_detail, "Fix Asset Book", "fabk_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Fix Asset Methode", "famt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Exp.Life", "assbk_exp_life", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Posted", "fabk_posted", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_detail, "Cost", "assbk_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Depresiasi Akumulasi", "assbk_depr_acum", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Per Year", "assbk_per_year", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_detail, "Per Month", "assbk_per_month", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "assbk_oid", False)
        add_column(gv_edit, "assbk_ass_oid", False)
        add_column(gv_edit, "assbk_fabk_id", False)
        add_column(gv_edit, "Fix Asset Book", "fabk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "assbk_famt_id", False)
        add_column(gv_edit, "Fix Asset Methode", "famt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Exp.Life", "assbk_exp_life", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Cost", "assbk_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _qty_del As Integer
        If ce_retirement.Checked = True Then
            _qty_del = 1
        Else
            _qty_del = 0
        End If

        Dim _filter As String
        _filter = pr_filter.Text

        get_sequel = "SELECT  " _
                & "  ass_oid, " _
                & "  ass_dom_id, " _
                & "  ass_en_id, en_desc, " _
                & "  ass_add_by, " _
                & "  ass_add_date, " _
                & "  ass_upd_by, " _
                & "  ass_upd_date, " _
                & "  ass_id, " _
                & "  ass_pt_id,pt_code,pt_desc1,pt_desc2, " _
                & "  ass_code, " _
                & "  ass_barcode, " _
                & "  ass_desc, " _
                & "  ass_class, " _
                & "  ass_ref_req, " _
                & "  ass_ref_po, " _
                & "  ass_ref_rcpt, " _
                & "  ass_ref_rcpt_oid, " _
                & "  ass_ref_inv, " _
                & "  ass_model, " _
                & "  ass_qty, " _
                & "  ass_qty_assgn, " _
                & "  ass_qty_del, " _
                & "  ass_sn, " _
                & "  ass_service_date, " _
                & "  ass_gar_date, " _
                & "  ass_line, " _
                & "  ass_ptnr_id,ptnr_name, " _
                & "  ass_st_purc, st_purc.code_name as status_purc, " _
                & "  ass_lic_type,lic.code_name as licence, " _
                & "  ass_service_cost, ass_service_cost - ass_depr_acum as cost_sisa, " _
                & "  ass_emp_id,xemp_name, " _
                & "  ass_emp_dept,dept.code_name as department, " _
                & "  ass_emp_rg,rg.code_name as regional, " _
                & "  ass_confirm, " _
                & "  ass_its_id,its_desc, " _
                & "  ass_dt, " _
                & "  coalesce(ass_salvage_cost,0) as ass_salvage_cost, " _
                & "  ass_basis_cost, " _
                & "  ass_depr_acum, " _
                & "  ass_depr_date, " _
                & "  ass_disp_amount, " _
                & "  ass_disp_date,ass_remarks, " _
                & "  loc_code, " _
                & "  loc_desc, " _
                & "  grp.code_name as group " _
                & "FROM  " _
                & "  public.ass_mstr " _
                & "  inner join its_mstr on its_id = ass_its_id " _
                & "  inner join pt_mstr on pt_id = ass_pt_id " _
                & "  left outer join ptnr_mstr on ptnr_id = ass_ptnr_id " _
                & "  left outer join xemp_mstr on xemp_id = ass_emp_id " _
                & "  inner join en_mstr on en_id = ass_en_id " _
                & "  left outer join code_mstr st_purc on st_purc.code_id = ass_st_purc " _
                & "  left outer join code_mstr lic on lic.code_id = ass_lic_type " _
                & "  left outer join code_mstr dept on dept.code_id = ass_emp_dept " _
                & "  left outer join code_mstr rg on rg.code_id = ass_emp_rg " _
                & "  left outer join loc_mstr on loc_id = ass_loc_id " _
                & "  inner join code_mstr grp on grp.code_id = pt_group " _
                & "  where ass_qty_del = " + SetInteger(_qty_del) _
                & "  and (ass_code ~~* '%" + _filter + "%'" _
                & "  or pt_code ~~* '%" + _filter + "%'" _
                & "  or pt_desc1 ~~* '%" + _filter + "%'" _
                & "  or pt_desc2 ~~* '%" + _filter + "%'" _
                & "  or ass_desc ~~* '%" + _filter + "%'" _
                & "  or ass_sn ~~* '%" + _filter + "%')" _
                & " order by ass_pt_id,ass_line asc "
        Return get_sequel
    End Function

    'Public Overrides Sub load_data_grid_detail()
    '    Dim _qty_del As Integer
    '    If ce_retirement.Checked = True Then
    '        _qty_del = 1
    '    Else
    '        _qty_del = 0
    '    End If


    '    If ds.Tables(0).Rows.Count = 0 Then
    '        Exit Sub
    '    End If

    '    Dim sql As String
    '    Try
    '        ds.Tables("detail").Clear()
    '    Catch ex As Exception
    '    End Try

    '    sql = "SELECT  " _
    '        & "  assbk_oid, " _
    '        & "  assbk_ass_oid, " _
    '        & "  assbk_fabk_id,fabk_code,fabk_posted, " _
    '        & "  assbk_famt_id,famt_oid,famt_code,famt_method, " _
    '        & "  assbk_exp_life, " _
    '        & "  assbk_cost, " _
    '        & "  assbk_depr_acum, " _
    '        & "  assbk_per_year, " _
    '        & "  assbk_per_month, " _
    '        & "  assbk_dt " _
    '        & "FROM  " _
    '        & "  public.assbk_mstr " _
    '        & "  inner join fabk_mstr on fabk_id = assbk_fabk_id " _
    '        & "  inner join famt_mstr on famt_id = assbk_famt_id " _
    '        & "  inner join ass_mstr on ass_oid = assbk_ass_oid " _
    '        & "  where ass_qty_del = " + SetInteger(_qty_del)
    '    load_data_detail(sql, gc_detail, "detail")
    'End Sub

    'Public Overrides Sub relation_detail()
    '    Try
    '        gv_detail.Columns("assbk_ass_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_oid"))
    '        gv_detail.BestFitColumns()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("This Form Can't Insert Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            ass_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnr_oid = .Item("ass_oid").ToString
                ass_en_id.EditValue = .Item("ass_en_id")
                ass_pt_id.EditValue = .Item("ass_pt_id")
                ass_code.Text = SetString(.Item("ass_code"))
                ass_sn.Text = SetString(.Item("ass_sn"))
                ass_barcode.Text = SetString(.Item("ass_barcode"))
                ass_model.Text = SetString(.Item("ass_model"))
                ass_desc.Text = SetString(.Item("ass_desc"))
                ass_class.EditValue = .Item("ass_class")
                ass_qty.Text = .Item("ass_qty")
                ass_service_date.EditValue = .Item("ass_service_date")
                ass_gar_date.EditValue = .Item("ass_gar_date")
                ass_ptnr_id.EditValue = .Item("ass_ptnr_id")
                ass_cost.Text = .Item("ass_service_cost")
                ass_salvage_cost.Text = .Item("ass_salvage_cost")
                ass_st_purc.EditValue = .Item("ass_st_purc")
                ass_lic_type.EditValue = .Item("ass_lic_type")
                ass_emp_id.EditValue = .Item("ass_emp_id")
                ass_emp_dept.EditValue = .Item("ass_emp_dept")
                ass_emp_rg.EditValue = .Item("ass_emp_rg")
                'ass_confirm.EditValue = SetBitYNB(.Item("ass_confirm"))
                ass_remarks.Text = SetString(.Item("ass_remarks"))
            End With

            ass_emp_id.Enabled = True
            ass_emp_dept.Enabled = True
            ass_emp_rg.Enabled = True

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  assbk_oid, " _
                            & "  assbk_ass_oid, " _
                            & "  assbk_fabk_id,fabk_code, " _
                            & "  assbk_famt_id,famt_oid,famt_code,famt_method, " _
                            & "  assbk_exp_life, " _
                            & "  assbk_cost, " _
                            & "  assbk_depr_acum, " _
                            & "  assbk_per_year, " _
                            & "  assbk_per_month, " _
                            & "  assbk_dt " _
                            & "FROM  " _
                            & "  public.assbk_mstr " _
                            & "  inner join fabk_mstr on fabk_id = assbk_fabk_id " _
                            & "  inner join famt_mstr on famt_id = assbk_famt_id " _
                            & "  inner join ass_mstr on ass_oid = assbk_ass_oid " _
                            & "  where assbk_ass_oid = '" + ds.Tables(0).Rows(row).Item("ass_oid").ToString + "'"
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
        Dim ssqls As New ArrayList
        Dim _basis_cost As Double
        _basis_cost = (SetDbl(ass_cost.Text)) - (SetDbl(ass_salvage_cost.Text))
        'Dim i As Integer

        edit = True
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
                                                & "  public.ass_mstr   " _
                                                & "SET  " _
                                                & "  ass_en_id = " & SetInteger(ass_en_id.EditValue) & ",  " _
                                                & "  ass_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ass_upd_date = current_timestamp " & ",  " _
                                                & "  ass_pt_id = " & SetInteger(ass_pt_id.EditValue) & ",  " _
                                                & "  ass_code = E'" & SetSetringE(ass_code.Text) & "',  " _
                                                & "  ass_sn = " & SetSetring(ass_sn.Text) & ",  " _
                                                & "  ass_barcode = " & SetSetring(ass_barcode.Text) & ",  " _
                                                & "  ass_desc = " & SetSetring(ass_desc.Text) & ",  " _
                                                & "  ass_class = " & SetSetring(ass_class.Text) & ",  " _
                                                & "  ass_qty = " & SetDbl(ass_qty.Text) & ",  " _
                                                & "  ass_remarks = " & SetSetring(ass_remarks.Text) & ",  " _
                                                & "  ass_model = " & SetSetring(ass_model.Text) & ",  " _
                                                & "  ass_service_date = " & SetDate(ass_service_date.DateTime) & ",  " _
                                                & "  ass_gar_date = " & SetDate(ass_gar_date.DateTime) & ",  " _
                                                & "  ass_ptnr_id = " & SetInteger(ass_ptnr_id.EditValue) & ",  " _
                                                & "  ass_st_purc = " & SetInteger(ass_st_purc.EditValue) & ",  " _
                                                & "  ass_lic_type = " & SetInteger(ass_lic_type.EditValue) & ",  " _
                                                & "  ass_service_cost = " & SetDbl(ass_cost.Text) & ",  " _
                                                & "  ass_salvage_cost = " & SetDbl(ass_salvage_cost.Text) & ",  " _
                                                & "  ass_basis_cost = " & SetDbl(_basis_cost) & ",  " _
                                                & "  ass_emp_id = " & SetInteger(ass_emp_id.EditValue) & ",  " _
                                                & "  ass_emp_dept = " & SetInteger(ass_emp_dept.EditValue) & ",  " _
                                                & "  ass_emp_rg = " & SetInteger(ass_emp_rg.EditValue) & ",  " _
                                                & "  ass_confirm = " & SetSetring("N") & ",  " _
                                                & "  ass_dt = current_timestamp " & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  ass_oid = " & SetSetring(_ptnr_oid) & "  " _
                                                & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from assbk_mstr where assbk_ass_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_oid"))
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        ''******************************************************

                        ''Insert dan update data detail
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.assbk_mstr " _
                        '                        & "( " _
                        '                        & "  assbk_oid, " _
                        '                        & "  assbk_ass_oid, " _
                        '                        & "  assbk_fabk_id, " _
                        '                        & "  assbk_famt_id, " _
                        '                        & "  assbk_exp_life, " _
                        '                        & "  assbk_cost, " _
                        '                        & "  assbk_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_oid")) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("assbk_fabk_id")) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("assbk_famt_id")) & ",  " _
                        '                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("assbk_exp_life")) & ",  " _
                        '                        & SetDbl(ass_cost.EditValue) & ",  " _
                        '                        & "current_timestamp" & "  " _
                        '                        & ");"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

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
                        set_row(Trim(_ptnr_oid.ToString), "ass_oid")
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
        MessageBox.Show("This Form Can't To Delete Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Private Sub nbi_view_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New FViewDepresiasi()
        frm._famt_oid = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("famt_oid").ToString
        frm._famt_method = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("famt_method")
        frm._exp_life = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("assbk_exp_life")
        frm._cost = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("assbk_cost")
        frm._salv_cost = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_salvage_cost")
        frm._month = Month(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_service_date"))
        frm._start_date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_service_date")
        frm._year = Year(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_service_date"))
        frm._fabk_code = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("fabk_code")
        frm._famt_code = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("famt_code")
        frm._part_number = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code")
        frm._serial = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_code")
        frm._assbk_oid = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("assbk_oid").ToString

        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "fabk_code" Then
            Dim frm As New FFixAssetBookSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "famt_code" Then
            Dim frm As New FFixAssetMethodeSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'With gv_edit
        '    '.SetRowCellValue(e.RowHandle, "pod_en_id", asrtr_en_id.EditValue)
        '    .BestFitColumns()
        'End With
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

End Class
