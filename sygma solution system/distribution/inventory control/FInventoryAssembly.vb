Imports Npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FInventoryAssembly
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As New DataSet
    Dim ds_detail As New DataSet
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _pt_id As String
    Public _si_id As String
    Public _loc_id As String
    Public _pt_ac_id As String
    Public _ptd_ac_code As String
    Public _ps_oid As String
    Public _asmb_cost As Double

    Private Sub FProductStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        asmb_en_id.Properties.DataSource = dt_bantu
        asmb_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        asmb_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        asmb_en_id.ItemIndex = 0
    End Sub

    Private Sub asmb_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles asmb_en_id.EditValueChanged
        load_cb_en()
    End Sub
    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT   " _
                    & "   en_desc,  " _
                    & "    " _
                    & "   asmb_code,  " _
                    & "   asmb_date, " _
                    & "   " _
                    & "   asmb_qty as qty_master,  " _
                    & "   asmb_desc,  " _
                    & "   asmb_remarks,  " _
                    & "    " _
                    & "   pt_master.pt_code as pt_code_master,pt_master.pt_desc1 as pt_desc1_master,  " _
           & "  asmbd_det.asmbd_seq, " _
           & "  pt_det.pt_code, " _
           & "  pt_det.pt_desc1 || coalesce(pt_det.pt_desc2,'') as pt_desc, " _
                      & "  asmbd_det.asmbd_qty, code_mstr.code_name as um_desc, " _
           & "  loc_desc, " _
           & "  ac_code, " _
           & "  ac_name, " _
           & "  sb_desc, " _
           & "  cc_desc " _
                    & " FROM  " _
                    & "   public.asmb_mstr  " _
                    & "  INNER JOIN en_mstr on (asmb_mstr.asmb_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN pt_mstr pt_master on pt_master.pt_id =asmb_pt_bom_id  " _
                     & "  INNER JOIN asmbd_det on asmbd_asmb_oid =asmb_oid " _
           & "  left outer JOIN code_mstr code_mstr_group ON (asmbd_det.asmbd_group = code_mstr_group.code_id) " _
           & "  left outer JOIN code_mstr code_mstr_proc ON (asmbd_det.asmbd_process = code_mstr_proc.code_id)" _
           & "  INNER JOIN pt_mstr pt_det ON pt_det.pt_id = asmbd_pt_bom_id " _
           & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_det.pt_um " _
           & "  INNER JOIN loc_mstr on loc_id=asmbd_loc_id " _
           & "  INNER JOIN public.ac_mstr ON (public.asmbd_det.asmbd_ac_id = public.ac_mstr.ac_id) " _
           & "  INNER JOIN public.sb_mstr ON (public.asmbd_det.asmbd_sb_id = public.sb_mstr.sb_id) " _
           & "  INNER JOIN public.cc_mstr ON (public.asmbd_det.asmbd_cc_id = public.cc_mstr.cc_id) " _
                    & " where asmb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and asmb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and asmb_type='A' and asmb_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "


            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function
    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "asmb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "asmb_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location ID", "asmb_loc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location Desc", "loc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "asmb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Assembly", "asmb_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "asmb_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "asmb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "asmb_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "asmb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "asmb_upd_date", DevExpress.Utils.HorzAlignment.Center)
        _
        add_column(gv_detail, "asmbd_oid", False)
        add_column(gv_detail, "asmbd_asmb_oid", False)
        add_column(gv_detail, "asmbd_pt_bom_id", False)
        add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Seq", "asmbd_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Reference", "asmbd_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "asmbd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "asmbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Str Type", "asmbd_str_type", DevExpress.Utils.HorzAlignment.Far)
        'add_column_copy(gv_detail, "Scrap", "asmbd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Lead Time Offset", "asmbd_lt_off", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Operation", "asmbd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Sequence", "asmbd_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Forecast Percent", "asmbd_fcst_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Option Group", "code_group_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Process", "code_proc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "asmbd_oid", False)
        add_column(gv_edit, "asmbd_asmb_oid", False)
        add_column(gv_edit, "asmbd_use_bom", False)
        add_column(gv_edit, "asmbd_pt_bom_id", False)
        add_column(gv_edit, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Component Desc", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Reference", "psd_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_edit(gv_edit, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "Qty Structure", "psd_qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Str Type", "psd_str_type", DevExpress.Utils.HorzAlignment.Far)
        'add_column_edit(gv_edit, "Scrap", "psd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_edit(gv_edit, "Lead Time Offset", "psd_lt_off", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_edit(gv_edit, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_edit(gv_edit, "Forecast Percent", "psd_fcst_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit, "asmbd_group", False)
        'add_column(gv_edit, "Option Group", "code_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "psd_process", False)
        'add_column(gv_edit, "Process", "code_proc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pla_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "asmbd_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "asmbd_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT   " _
                    & "   en_desc,  " _
                    & "   asmb_oid,  " _
                    & "   asmb_dom_id,  " _
                    & "   asmb_en_id,  " _
                    & "   asmb_add_by,  " _
                    & "   asmb_add_date,  " _
                    & "   asmb_upd_by,  " _
                    & "   asmb_upd_date,  " _
                    & "   asmb_code,  " _
                    & "   asmb_date, " _
                    & "   asmb_id,  " _
                    & "   asmb_qty,  " _
                    & "   asmb_desc,  " _
                    & "   asmb_remarks,  " _
                    & "   asmb_pt_bom_id,  " _
                    & "   pt_code,pt_desc1,asmb_loc_id,loc_code,loc_desc  " _
                    & " FROM  " _
                    & "   public.asmb_mstr  " _
                    & "  INNER JOIN en_mstr on (asmb_mstr.asmb_en_id = en_mstr.en_id) " _
                    & "  INNER JOIN pt_mstr on pt_id =asmb_pt_bom_id  " _
                     & "  LEFT OUTER JOIN loc_mstr on asmb_loc_id =loc_id  " _
                    & " where asmb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and asmb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and asmb_type='A' and asmb_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "

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
           & "  asmbd_det.asmbd_oid, " _
           & "  asmbd_det.asmbd_asmb_oid, " _
           & "  asmbd_det.asmbd_use_bom, " _
           & "  asmbd_det.asmbd_pt_bom_id, " _
           & "  asmbd_det.asmbd_comp, " _
           & "  asmbd_det.asmbd_ref, " _
           & "  asmbd_det.asmbd_desc, " _
           & "  asmbd_det.asmbd_start_date, " _
           & "  asmbd_det.asmbd_end_date, " _
           & "  asmbd_det.asmbd_qty,asmbd_det.asmbd_qty_plan,asmbd_det.asmbd_qty_variance, " _
           & "  asmbd_det.asmbd_str_type, " _
           & "  asmbd_det.asmbd_scrp_pct, " _
           & "  asmbd_det.asmbd_lt_off, " _
           & "  asmbd_det.asmbd_op, " _
           & "  asmbd_det.asmbd_seq, " _
           & "  asmbd_det.asmbd_fcst_pct, " _
           & "  asmbd_det.asmbd_group, " _
           & "  asmbd_det.asmbd_process, " _
           & "  pt_code, " _
           & "  pt_desc1 || pt_desc2 as pt_desc, " _
           & "  asmbd_loc_id,loc_desc, " _
           & "  asmbd_ac_id, " _
           & "  ac_code, " _
           & "  ac_name, " _
           & "  asmbd_sb_id, " _
           & "  sb_desc, " _
           & "  asmbd_cc_id, " _
           & "  cc_desc, " _
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
           & "  asmbd_det " _
           & "  left outer JOIN code_mstr code_mstr_group ON (asmbd_det.asmbd_group = code_mstr_group.code_id) " _
           & "  left outer JOIN code_mstr code_mstr_proc ON (asmbd_det.asmbd_process = code_mstr_proc.code_id)" _
           & "  INNER JOIN pt_mstr ON pt_id = asmbd_pt_bom_id " _
           & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
           & "  INNER JOIN asmb_mstr on asmbd_asmb_oid=asmb_oid " _
           & "  INNER JOIN loc_mstr on loc_id=asmbd_loc_id " _
           & "  INNER JOIN public.ac_mstr ON (public.asmbd_det.asmbd_ac_id = public.ac_mstr.ac_id) " _
           & "  INNER JOIN public.sb_mstr ON (public.asmbd_det.asmbd_sb_id = public.sb_mstr.sb_id) " _
           & "  INNER JOIN public.cc_mstr ON (public.asmbd_det.asmbd_cc_id = public.cc_mstr.cc_id) " _
           & " WHERE asmb_en_id in (select user_en_id from tconfuserentity " _
           & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "


        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub relation_detail()
        Try

            gv_detail.Columns("asmbd_asmb_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asmbd_asmb_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asmb_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
    Private Sub generate_detail()

        If asmb_qty.Text = "" Or asmb_qty.Text = 0 Then
            asmb_qty.EditValue = 1
        End If


        ds_detail = New DataSet
        'ds_update_related = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  psd_det.psd_oid, " _
                        & "  psd_det.psd_ps_oid, " _
                        & "  psd_det.psd_use_bom, " _
                        & "  pt_code, " _
                        & "  pt_pl_id, " _
                        & "  coalesce(pt_desc1,'') || coalesce(pt_desc2,'') as pt_desc, " _
                        & "  psd_det.psd_pt_bom_id, " _
                        & "  psd_det.psd_comp, " _
                        & "  psd_det.psd_ref, " _
                        & "  psd_det.psd_desc, " _
                        & "  psd_det.psd_start_date, " _
                        & "  psd_det.psd_end_date,psd_qty as psd_qty_open, " _
                        & "  (psd_det.psd_qty * " + SetInteger(asmb_qty.EditValue) + ") as psd_qty , " _
                        & "  psd_det.psd_str_type, " _
                        & "  psd_det.psd_scrp_pct, " _
                        & "  psd_det.psd_lt_off, " _
                        & "  psd_det.psd_op, " _
                        & "  psd_det.psd_seq, " _
                        & "  psd_det.psd_fcst_pct, " _
                        & "  psd_det.psd_group, " _
                        & "  psd_det.psd_process,coalesce((SELECT   x.invct_cost FROM  public.invct_table x WHERE  x.invct_pt_id=psd_det.psd_pt_bom_id  limit 1),0) as cost, " _
                        & "  " + SetInteger(_loc_id) + " as loc_id , " _
                        & "  " + SetSetring(asmb_loc.Text) + " as loc_desc , " _
                        & "  '' as pla_ac_id , " _
                        & "  '-' as ac_code , " _
                        & "  '-' as ac_name, " _
                        & "  0 as pla_sb_id, '-' as sb_desc, 0 as pla_cc_id, '-' as cc_desc, " _
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
                        & "  left outer JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                        & "  left outer JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                        & "  INNER JOIN pt_mstr on pt_id = psd_pt_bom_id " _
                        & "  INNER JOIN ps_mstr on ps_oid = psd_ps_oid " _
                        & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
                        & "  where public.psd_det.psd_ps_oid = '" + _ps_oid.ToString + "'"

                    .InitializeCommand()
                    .FillDataSet(ds_detail, "detail_ps")

                    gc_edit.DataSource = ds_detail.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Public Overrides Sub insert_data_awal()
        asmb_en_id.ItemIndex = 0
        be_asmb_ps.EditValue = ""
        asmb_date.DateTime = _now
        be_asmb_ps.Properties.ReadOnly = True
        asmb_loc.Text = ""
        asmb_desc.Text = ""
        asmb_remarks.Text = ""
        asmb_qty.Text = 0
        asmb_en_id.Focus()
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  asmbd_det.asmbd_oid, " _
                        & "  asmbd_det.asmbd_asmb_oid, " _
                        & "  asmbd_det.asmbd_use_bom, " _
                        & "  asmbd_det.asmbd_desc as ptbomdesc, " _
                        & "  asmbd_det.asmbd_pt_bom_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1 || pt_desc2 as pt_desc, " _
                        & "  asmbd_det.asmbd_ref, " _
                        & "  asmbd_det.asmbd_desc, " _
                        & "  asmbd_det.asmbd_start_date, " _
                        & "  asmbd_det.asmbd_end_date, " _
                        & "  asmbd_det.asmbd_qty,asmbd_det.asmbd_qty_plan,asmbd_det.asmbd_qty_variance, " _
                        & "  asmbd_det.asmbd_str_type, " _
                        & "  asmbd_det.asmbd_scrp_pct, " _
                        & "  asmbd_det.asmbd_lt_off, " _
                        & "  asmbd_det.asmbd_op, " _
                        & "  asmbd_det.asmbd_seq, " _
                        & "  asmbd_det.asmbd_fcst_pct, " _
                        & "  asmbd_det.asmbd_group, " _
                        & "  asmbd_det.asmbd_process, " _
                        & "  asmbd_det.asmbd_ac_id, " _
                        & "  ac_code, " _
                        & "  ac_name, " _
                        & "  asmbd_det.asmbd_sb_id, " _
                        & "  sb_desc, " _
                        & "  asmbd_det.asmbd_cc_id, " _
                        & "  cc_desc, " _
                        & "  code_mstr_group.code_en_id AS code_group_en_id, " _
                        & "  code_mstr_group.code_id AS code_group_id, " _
                        & "  code_mstr_group.code_field AS code_group_field, " _
                        & "  code_mstr_group.code_code AS code_group_code, " _
                        & "  code_mstr_group.code_name AS code_group_name, " _
                        & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
                        & "  code_mstr_proc.code_id AS code_proc_id, " _
                        & "  code_mstr_proc.code_field AS code_proc_field, " _
                        & "  code_mstr_proc.code_code AS code_proc_code, " _
                        & "  code_mstr_proc.code_name AS code_proc_name " _
                        & "FROM " _
                        & "  asmbd_det " _
                        & "  INNER JOIN code_mstr code_mstr_group ON (asmbd_det.asmbd_group = code_mstr_group.code_id) " _
                        & "  INNER JOIN code_mstr code_mstr_proc ON (asmbd_det.asmbd_process = code_mstr_proc.code_id)" _
                        & "  INNER JOIN pt_mstr ON pt_id = asmbd_pt_bom_id " _
                        & "  INNER JOIN public.ac_mstr ON (public.asmbd_det.asmbd_ac_id = public.ac_mstr.ac_id) " _
                        & "  INNER JOIN public.sb_mstr ON (public.asmbd_det.asmbd_sb_id = public.sb_mstr.sb_id) " _
                        & "  INNER JOIN public.cc_mstr ON (public.asmbd_det.asmbd_cc_id = public.cc_mstr.cc_id) " _
                        & " where asmbd_det.asmbd_seq = -99"

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


        Dim _date As Date = asmb_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(asmb_en_id.EditValue, "gcald_ic", _date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_detail.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i As Integer


        '*********************
        'Cek part number, Location,account
        For i = 0 To ds_detail.Tables(0).Rows.Count - 1
            If ds_detail.Tables(0).Rows(i).Item("loc_id").ToString.Trim = "" Then
                MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_detail.Tables(0)).Position = i
                Return False
            ElseIf ds_detail.Tables(0).Rows(i).Item("pla_ac_id").ToString.Trim = "" Then
                MessageBox.Show("Account Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_detail.Tables(0)).Position = i
                Return False
            ElseIf ds_detail.Tables(0).Rows(i).Item("psd_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_detail.Tables(0)).Position = i
                Return False
            End If
            If ds_detail.Tables(0).Rows(i).Item("cost") = 0 Then
                MessageBox.Show("Can't Save For cost 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_detail.Tables(0)).Position = i
                Return False
            End If
        Next
        '*********************


        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function insert() As Boolean
        Dim _tran_id, _en_id As Integer
        Dim _pt_id_det As Integer
        Dim _loc_id_det As Integer
        Dim _serial, _pt_code_det As String
        Dim ssqls As New ArrayList
        Dim _cost_avg, _qty As Double
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        'sb_generate_Click(Nothing, Nothing)
        Dim _jml As Double = SetNumber(asmb_qty.EditValue)

        For n As Integer = 0 To ds_detail.Tables(0).Rows.Count - 1
            If _jml <> SetNumber(ds_detail.Tables(0).Rows(n).Item("psd_qty")) Then
                'Box("QTY header dan detail is not equal")
                'Return False
                'Exit Function
            End If
            _jml = SetNumber(ds_detail.Tables(0).Rows(n).Item("psd_qty"))

        Next

        Dim _asmb_oid As Guid
        _asmb_oid = Guid.NewGuid

        Dim ds_bantu As New DataSet
        Dim i, i_2 As Integer

        _tran_id = func_coll.get_id_tran_mstr("rct-unp")
        If _tran_id = -1 Then
            MessageBox.Show("Inventory Receipt In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
        End If

        Dim _asmb_id As Integer
        _asmb_id = SetInteger(func_coll.GetID("asmb_mstr", asmb_en_id.GetColumnValue("en_code"), "asmb_id", "asmb_en_id", asmb_en_id.EditValue.ToString))


        Dim _asmb_code As String
        _asmb_code = func_coll.get_transaction_number("AS", asmb_en_id.GetColumnValue("en_code"), "asmb_mstr", "asmb_code")

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                                & "  public.asmb_mstr " _
                                                & "( " _
                                                & "  asmb_oid, " _
                                                & "  asmb_dom_id, " _
                                                & "  asmb_en_id, " _
                                                & "  asmb_add_by, " _
                                                & "  asmb_add_date, " _
                                                & "  asmb_id, " _
                                                & "  asmb_code, " _
                                                & "  asmb_date, " _
                                                & "  asmb_desc, " _
                                                & "  asmb_pt_bom_id, " _
                                                & "  asmb_qty, " _
                                                & "  asmb_dt, " _
                                                & "  asmb_remarks, " _
                                                & "  asmb_ps_oid,asmb_loc_id, " _
                                                & "  asmb_type " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_asmb_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(asmb_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetInteger(_asmb_id) & ",  " _
                                                & SetSetring(_asmb_code.ToString) & ",  " _
                                                & "" & SetDateNTime(asmb_date.DateTime) & "" & ",  " _
                                                & SetSetring(asmb_desc.Text) & ",  " _
                                                & SetInteger(_pt_id) & ",  " _
                                                & SetInteger(asmb_qty.EditValue) & ",  " _
                                                & " current_timestamp, " _
                                                & SetSetring(asmb_remarks.Text) & ",  " _
                                                & SetSetring(_ps_oid) & ",  " _
                                                & SetInteger(_loc_id) & ",  " _
                                                & SetSetring("A") & "  " _
                                                & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        _en_id = asmb_en_id.EditValue
                        _serial = "''"
                        _qty = asmb_qty.EditValue

                        _asmb_cost = 0

                        For Each dr As DataRow In ds_detail.Tables(0).Rows
                            _asmb_cost = _asmb_cost + dr("cost")
                        Next


                        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                            sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If

                        'Update History Inventory                                    
                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _asmb_cost)
                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 0, _en_id, Trim(_asmb_code), _
                            _asmb_oid.ToString, "Inventory Assembly", "", _si_id, _loc_id, _pt_id, _qty, _asmb_cost, _
                            _cost_avg, "", asmb_date.DateTime) = False Then
                            sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If

                        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                            sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If



                        i_2 = 0
                        For i = 0 To ds_detail.Tables(0).Rows.Count - 1
                            '****************************************
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.asmbd_det " _
                                                    & "( " _
                                                    & "  asmbd_oid, " _
                                                    & "  asmbd_asmb_oid, " _
                                                    & "  asmbd_add_by, " _
                                                    & "  asmbd_add_date, " _
                                                    & "  asmbd_use_bom, " _
                                                    & "  asmbd_pt_bom_id, " _
                                                    & "  asmbd_loc_id, " _
                                                    & "  asmbd_ref, " _
                                                    & "  asmbd_desc, " _
                                                    & "  asmbd_start_date, " _
                                                    & "  asmbd_end_date, " _
                                                    & "  asmbd_qty, " _
                                                    & "  asmbd_str_type, " _
                                                    & "  asmbd_scrp_pct, " _
                                                    & "  asmbd_lt_off, " _
                                                    & "  asmbd_op, " _
                                                    & "  asmbd_seq, " _
                                                    & "  asmbd_fcst_pct, " _
                                                    & "  asmbd_group, " _
                                                    & "  asmbd_process, " _
                                                    & "  asmbd_ac_id, " _
                                                    & "  asmbd_sb_id, " _
                                                    & "  asmbd_cc_id, " _
                                                    & "  asmbd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_asmb_oid.ToString) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring(ds_detail.Tables(0).Rows(i).Item("psd_use_bom")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("psd_pt_bom_id")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("loc_id")) & ",  " _
                                                    & SetSetring(ds_detail.Tables(0).Rows(i).Item("psd_ref")) & ",  " _
                                                    & SetSetring(ds_detail.Tables(0).Rows(i).Item("psd_desc")) & ",  " _
                                                    & SetDate(ds_detail.Tables(0).Rows(i).Item("psd_start_date")) & ",  " _
                                                    & SetDate(ds_detail.Tables(0).Rows(i).Item("psd_end_date")) & ",  " _
                                                    & SetDbl(ds_detail.Tables(0).Rows(i).Item("psd_qty")) & ",  " _
                                                    & SetSetring(ds_detail.Tables(0).Rows(i).Item("psd_str_type")) & ",  " _
                                                    & SetDbl(ds_detail.Tables(0).Rows(i).Item("psd_scrp_pct")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("psd_lt_off")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("psd_op")) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetDbl(ds_detail.Tables(0).Rows(i).Item("psd_fcst_pct")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("psd_group")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("psd_process")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("pla_ac_id")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("pla_sb_id")) & ",  " _
                                                    & SetInteger(ds_detail.Tables(0).Rows(i).Item("pla_cc_id")) & ",  " _
                                                    & "current_timestamp" _
                                                    & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            i_2 += 1


                            _en_id = asmb_en_id.EditValue
                            _pt_id_det = ds_detail.Tables(0).Rows(i).Item("psd_pt_bom_id")
                            _pt_code_det = ds_detail.Tables(0).Rows(i).Item("pt_code")
                            _loc_id_det = ds_detail.Tables(0).Rows(i).Item("loc_id")
                            _serial = "''"
                            _qty = ds_detail.Tables(0).Rows(i).Item("psd_qty")
                            If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id_det, _pt_id_det, _pt_code_det, _serial, _qty) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                            'Update History Inventory                                    
                            _qty = _qty * -1
                            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id_det, _qty, 0)
                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, Trim(_asmb_code), _asmb_oid.ToString, "Inventory Assembly", "", _si_id, _loc_id_det, _pt_id_det, _qty, 0, _cost_avg, "", asmb_date.DateTime) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                        Next

                        If _create_jurnal = True Then
                            If insert_glt_det_inv_rct(ssqls, objinsert, ds_detail, _
                                                 asmb_en_id.EditValue, asmb_en_id.GetColumnValue("en_code"), _
                                                 _asmb_oid.ToString, _asmb_code, _
                                                 asmb_date.DateTime, _
                                                 "IC", "RCT-UNP") = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Inv Assembly " & _asmb_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_asmb_oid.ToString), "asmb_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As NpgsqlException
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
    Private Sub browse_data()
        gv_edit.UpdateCurrentRow()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        Dim _filter As String
        _filter = " and b.ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(asmb_en_id.EditValue) & " and enacc_code='asmbl_account')" 'dbcr_account'asmbl_account


        If _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._pil = 2
            frm._en_id = asmb_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "ac_code" Then

            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            If limit_account(asmb_en_id.EditValue) = True Then
                frm._obj = _filter
            End If
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            frm._row = _row
            If limit_account(asmb_en_id.EditValue) = True Then
                frm._obj = _filter
            End If
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub
    Private Function insert_glt_det_inv_rct(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean
        insert_glt_det_inv_rct = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim _cost As Double
        Dim dt_bantu As DataTable
        _glt_code = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("psd_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("psd_pt_bom_id"))
                        dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                        _cost = par_ds.Tables(0).Rows(i).Item("psd_qty") * par_ds.Tables(0).Rows(i).Item("cost")

                        'Insert Untuk Yang debet Dulu
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                            & SetDbl(1) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring("Inventory Assembly") & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                        '********************** finish untuk yang debet

                        'Insert Untuk credit nya....
                        _seq = _seq + 1
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                            & SetDbl(1) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("Inventory Assembly") & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         par_ds.Tables(0).Rows(i).Item("pla_ac_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("pla_sb_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function
    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function
    Private Sub sb_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_generate.Click
        generate_detail()
    End Sub

    Private Sub be_asmb_ps_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_asmb_ps.ButtonClick
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = asmb_en_id.EditValue
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        sb_generate.PerformClick()
    End Sub


    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub be_asmb_ps_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles be_asmb_ps.EditValueChanged

    End Sub

    Private Sub asmb_loc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles asmb_loc.ButtonClick
        Dim frm As New FLocationSearch
        frm.set_win(Me)
        frm._en_id = asmb_en_id.EditValue
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        sb_generate.PerformClick()
    End Sub

    Private Sub asmb_loc_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles asmb_loc.EditValueChanged

    End Sub

    Private Sub asmb_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles asmb_qty.EditValueChanged
        Try
            ds_detail.AcceptChanges()

            If ds_detail.Tables.Count = 0 Then
                Exit Sub
            End If
            For Each dr As DataRow In ds_detail.Tables(0).Rows
                dr("psd_qty") = asmb_qty.EditValue
            Next
            ds_detail.AcceptChanges()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub LabelControl3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelControl3.DoubleClick
        Try
            Dim ssql As String = ""
            Dim sSqls As New ArrayList

            Dim _oid As String = ""
            For Each dr As DataRow In ds.Tables(0).Rows
                _oid = _oid & "'" & dr("asmb_oid").ToString & "',"

            Next

            If _oid.Length > 0 Then
                _oid = _oid.Substring(0, _oid.Length - 1)
            End If


            If _oid <> "" Then
                ssql = "SELECT  " _
                    & "  public.invh_mstr.invh_loc_id, " _
                    & "  public.invh_mstr.invh_trn_code, " _
                    & "  public.invh_mstr.invh_trn_oid, " _
                    & "  public.invh_mstr.invh_oid,invh_pt_id " _
                    & "FROM " _
                    & "  public.invh_mstr " _
                    & "WHERE " _
                    & "  public.invh_mstr.invh_trn_oid in  ( " & _oid & " ) and invh_qty > 0"

                Dim dt_invh As New DataTable
                dt_invh = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In ds.Tables(0).Rows
                    For Each dr_invh As DataRow In dt_invh.Rows
                        If dr("asmb_oid").ToString = dr_invh("invh_trn_oid").ToString And dr("asmb_pt_bom_id") = dr_invh("invh_pt_id") Then
                            ssql = "update asmb_mstr set asmb_loc_id=" & SetInteger(dr_invh("invh_loc_id")) & " where asmb_oid=" & SetSetring(dr("asmb_oid").ToString)

                            sSqls.Add(ssql)
                            Exit For
                        End If
                    Next
                Next
            End If


            'If master_new.PGSqlConn.status_sync = True Then
            '    If DbRunTran(sSqls, "", master_new.PGSqlConn.FinsertSQL2Array(sSqls), "") = False Then

            '        Exit Sub
            '    End If
            '    sSqls.Clear()
            'Else
            '    If DbRunTran(sSqls, "") = False Then

            '        Exit Sub
            '    End If
            '    sSqls.Clear()
            'End If

            Box("Update success")
            after_success()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
