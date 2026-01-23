Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FWOReleaseNew
    Dim ssql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssqls As New ArrayList

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        Me.RelDate.DateTime = Now

        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Private Sub wor_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Wo Type", "wo_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "BOQ Code", "boq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reference Rework", "wo_ref_rework", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number WO", "pt_code_bom", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1_bom", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2_bom", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Qty", "wo_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Yield Percent", "wo_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Qty Prod", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Date", "pjc_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold to", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "wo_ro_id", False)
        add_column_copy(gv_master, "Routing", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Routing", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wo_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Indirect", "wod_indirect", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Start Date", "wod_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_detail, "End Date", "wod_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_detail, "Qty", "wod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Yield", "wod_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Qty Yield", "wod_qty_yield", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sequence", "wod_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")

        'add_column_copy(gv_routing, "Sequence", "wodr_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Operation", "wodr_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Start Date", "wodr_start_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "End Date", "wodr_end_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Description", "wodr_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Workcenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Yield", "wodr_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_routing, "Qty In", "wodr_qty_in", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Qty Complete", "wodr_qty_complete", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Qty Reject", "wodr_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Qty Outstanding", "wodr_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Qty Out", "wodr_qty_out", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        'add_column_copy(gv_routing, "Sequence", "wodr_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Operation", "wodr_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Start Date", "wodr_start_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "End Date", "wodr_end_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Description", "wodr_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Workcenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Yield", "wodr_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        'add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Componect Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Unit Measure", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Indirect", "wod_indirect", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description", "wod_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Start Date", "wod_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_detail, "End Date", "wod_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_detail, "Qty", "wod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Yield", "wod_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sequence", "wod_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")


        add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Indirect", "psd_indirect", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_copy(gv_detail, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n5")
        add_column_copy(gv_detail, "Insheet", "psd_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sequence", "psd_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")

        'add_column_copy(gv_routing, "Operation", "rod_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_routing, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Start Date", "rod_start_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "End Date", "rod_end_date", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_routing, "Workcenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_routing, "Description", "rod_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_routing, "Insheet", "rod_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_routing, "Run", "rod_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n5")
        add_column_copy(gv_routing, "Qty Out", "rod_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
    End Sub


    Public Overrides Function get_sequel() As String


        get_sequel = "SELECT  false as pilih, " _
                & "  a.wo_oid, " _
                & "  a.wo_dom_id, " _
                & "  a.wo_en_id, " _
                & "  a.wo_si_id, " _
                & "  a.wo_id, " _
                & "  a.wo_code, " _
                & "  a.wo_type, " _
                & "  a.wo_pt_id, " _
                & "  a.wo_qty_ord, " _
                & "  a.wo_qty_comp, " _
                & "  a.wo_qty_rjc, " _
                & "  a.wo_ord_date, " _
                & "  a.wo_rel_date, " _
                & "  a.wo_due_date, " _
                & "  a.wo_insheet_pct, " _
                & "  a.wo_ro_id, " _
                & "  a.wo_status, " _
                & "  a.wo_remarks, " _
                & "  a.wo_dt, " _
                & "  a.wo_date_close, " _
                & "  a.wo_pjc_id, " _
                & "  a.wo_ref_rework, " _
                & "  a.wo_qty, " _
                & "  b.en_desc, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  c.pt_desc2,a.wo_ps_id,ps_par, " _
                & "  e.ro_code, " _
                & "  e.ro_desc, a.wo_so_oid, " _
                & "  g.so_code, " _
                & "  g.so_date, " _
                & "  g.so_ptnr_id_sold, " _
                & "  h.ptnr_name, " _
                & "  i.wo_code AS wo_ref_rework, " _
                & "  j.si_desc " _
                & "FROM " _
                & "  public.wo_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.wo_en_id = b.en_id) " _
                & "  INNER JOIN public.pt_mstr c ON (a.wo_pt_id = c.pt_id) " _
                & "  INNER JOIN public.ps_mstr d ON (a.wo_ps_id = d.ps_id) " _
                & "  INNER JOIN public.ro_mstr e ON (a.wo_ro_id = e.ro_id) " _
                & "  INNER JOIN public.so_mstr g ON (a.wo_so_oid = g.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr h ON (g.so_ptnr_id_sold = h.ptnr_id) " _
                & "  LEFT OUTER JOIN public.wo_mstr i ON (a.wo_ref_rework = i.wo_oid) " _
                & "  LEFT OUTER JOIN public.si_mstr j ON (a.wo_si_id = j.si_id) " _
                & "  where a.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and a.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and a.wo_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")  and a.wo_status='F' " _
                & " Order by wo_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
    End Sub

    Public Overrides Function insert() As Boolean
    End Function

    Public Overrides Function edit_data() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function edit()
        Return True
    End Function

    Public Overrides Function delete_data() As Boolean
        Return True
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = False

        Return before_save
    End Function
    Public Sub show_detail()
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
                  & "  psd_det inner join ps_mstr on (ps_oid=psd_ps_oid)  " _
                  & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                  & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                  & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
                  & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
                  & "  LEFT OUTER JOIN op_mstr on psd_op=op_code " _
                  & " WHERE ps_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_ps_id").ToString _
                  & " order by psd_seq"

     

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("routing").Clear()
        Catch ex As Exception
        End Try


        sql = "SELECT  " _
            & "  rod_oid, " _
            & "  rod_ro_oid, " _
            & "  rod_add_by, " _
            & "  rod_add_date, " _
            & "  rod_upd_by, " _
            & "  rod_upd_date, " _
            & "  rod_op, " _
            & "  rod_start_date, " _
            & "  rod_end_date, " _
            & "  rod_wc_id, " _
            & "  rod_desc, " _
            & "  rod_mch_op, " _
            & "  rod_tran_qty, " _
            & "  rod_queue, " _
            & "  rod_wait, " _
            & "  rod_move, " _
            & "  rod_run, " _
            & "  rod_setup, " _
            & "  rod_insheet_pct, " _
            & "  rod_milestone, " _
            & "  rod_sub_lead, " _
            & "  rod_setup_men, " _
            & "  rod_men_mch, " _
            & "  rod_tool_code, " _
            & "  rod_ptnr_id, " _
            & "  rod_sub_cost,rod_seq,rod_conversion, " _
            & "  wc_desc, " _
            & "  code_name, " _
            & "  ptnr_name, " _
            & "  rod_dt " _
            & "FROM  " _
            & "  public.rod_det " _
            & " INNER JOIN wc_mstr on rod_wc_id= wc_id" _
            & " LEFT OUTER JOIN code_mstr on rod_tool_code= code_id" _
            & " LEFT OUTER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id" _
            & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid " _
            & " where ro_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_ro_id").ToString _
            & "  order by rod_seq "


        load_data_detail(sql, gc_routing, "routing")
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        show_detail()
    End Sub

    Private Sub BtRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRelease.Click
        Try
            show_detail()
            For Each dr As DataRow In ds.Tables("routing").Rows
                If SetString(dr("rod_seq")) = "" Then
                    Box("This routing haven't sequence")
                    Exit Sub
                End If
            Next

            ssqls.Clear()
            'For Each dr As DataRow In ds.Tables(0).Rows
            '    If SetString(dr("boq_code")) = "" And dr("pilih") = True Then
            '        Box("BOQ is empty")
            '        Exit Sub
            '    End If
            'Next

            If ds.Tables(0).Rows.Count = 0 Then
                Box("Detail is empty")
                Exit Sub
            End If

            gv_master.UpdateCurrentRow()
            ds.AcceptChanges()

            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each dr_master As DataRow In ds.Tables(0).Rows
                            If dr_master("pilih") = True Then

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wo_mstr set wo_status='R', wo_rel_date=" & SetDateNTime00(RelDate.DateTime) _
                                                        & " where wo_oid=" & SetSetring(dr_master("wo_oid"))
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                ssqls.Add(.Command.CommandText)

                                'ssql = "select temp_2.*, invct_cost from (select psd_pt_bom_id , pt_code, pt_desc1, pt_desc2,code_name as code_code," _
                                '    & " pt_um,psd_indirect,psd_op,psd_yield_pct,psd_start_date,psd_end_date,psd_seq, " _
                                '    & " sum(psd_qty) as psd_qty  from ( select * from public.get_ps_first(" & dr_master("wo_ps_id") _
                                '    & ",1) where  variable_4=0) as temp " _
                                '    & " group by psd_pt_bom_id, pt_code, pt_desc1, pt_desc2, " _
                                '    & " code_name , pt_um , psd_indirect ,psd_op,psd_yield_pct,psd_start_date,psd_end_date,psd_seq) as temp_2 " _
                                '    & "  left outer join invct_table on invct_pt_id = psd_pt_bom_id "

                                ssql = "select temp_2.psd_pt_bom_id,temp_2.psd_op,temp_2.psd_indirect,temp_2.psd_qty,temp_2.psd_insheet_pct,temp_2.psd_seq,  invct_cost from (SELECT  " _
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
                                     & "  psd_det inner join ps_mstr on (ps_oid=psd_ps_oid)  " _
                                     & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
                                     & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
                                     & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
                                     & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
                                     & "  LEFT OUTER JOIN op_mstr on psd_op=op_code " _
                                     & " WHERE ps_id=" & SetInteger(dr_master("wo_ps_id")) _
                                     & " order by psd_seq) as temp_2 " _
                                     & "  left outer join invct_table on invct_pt_id = psd_pt_bom_id "


                                Dim dt_raw As New DataTable
                                dt_raw = master_new.PGSqlConn.GetTableData(ssql)

                                For Each dr As DataRow In dt_raw.Rows ' ds.Tables("detail").Rows
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.wod_det " _
                                        & "( " _
                                        & "  wod_oid, " _
                                        & "  wod_wo_oid, " _
                                        & "  wod_pt_bom_id, " _
                                        & "  wod_op, " _
                                        & "  wod_cost, " _
                                        & "  wod_dt, " _
                                        & "  wod_indirect, " _
                                        & "  wod_qty, " _
                                        & "  wod_insheet_pct, " _
                                        & "  wod_seq, " _
                                        & "  wod_qty_insheet " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(dr_master("wo_oid")) & ",  " _
                                        & SetInteger(dr("psd_pt_bom_id")) & ",  " _
                                        & SetInteger(dr("psd_op")) & ",  " _
                                        & SetDec(dr("invct_cost")) & ",  " _
                                        & SetDateNTime(CekTanggal) & ",  " _
                                        & SetSetring(dr("psd_indirect")) & ",  " _
                                        & IIf(dr("psd_indirect") = "Y", SetDec(dr("psd_qty")), SetDec(dr("psd_qty") * dr_master("wo_qty_ord"))) & ",  " _
                                        & SetDec(dr("psd_insheet_pct")) & ",  " _
                                        & SetDec(dr("psd_seq")) & ",  " _
                                        & SetDec(dr("psd_qty") * dr("psd_insheet_pct")) & "  " _
                                        & ") "

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next


                                ssql = "SELECT  " _
                                    & "  rod_oid, " _
                                    & "  rod_ro_oid, " _
                                    & "  rod_add_by, " _
                                    & "  rod_add_date, " _
                                    & "  rod_upd_by, " _
                                    & "  rod_upd_date, " _
                                    & "  rod_op, " _
                                    & "  rod_start_date, " _
                                    & "  rod_end_date, " _
                                    & "  rod_wc_id, " _
                                    & "  rod_desc, " _
                                    & "  rod_mch_op, " _
                                    & "  rod_tran_qty, " _
                                    & "  rod_queue, " _
                                    & "  rod_wait, " _
                                    & "  rod_move, " _
                                    & "  rod_run, " _
                                    & "  rod_setup, " _
                                    & "  rod_insheet_pct, " _
                                    & "  rod_milestone, " _
                                    & "  rod_sub_lead, " _
                                    & "  rod_setup_men, " _
                                    & "  rod_men_mch, " _
                                    & "  rod_tool_code, " _
                                    & "  rod_ptnr_id, " _
                                    & "  rod_sub_cost,rod_seq,rod_conversion, " _
                                    & "  wc_desc, " _
                                    & "  code_name, " _
                                    & "  ptnr_name, " _
                                    & "  rod_dt " _
                                    & "FROM  " _
                                    & "  public.rod_det " _
                                    & " INNER JOIN wc_mstr on rod_wc_id= wc_id" _
                                    & " LEFT OUTER JOIN code_mstr on rod_tool_code= code_id" _
                                    & " LEFT OUTER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id" _
                                    & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid " _
                                    & " where ro_id=" & SetInteger(dr_master("wo_ro_id")) _
                                    & "  order by rod_seq "

                                Dim dt_routing As New DataTable
                                dt_routing = master_new.PGSqlConn.GetTableData(ssql)

                                Dim x As Integer = 1
                                For Each dr As DataRow In dt_routing.Rows
                                    .Command.CommandType = CommandType.Text
                                    If x = 1 Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                           & "  public.wodr_routing " _
                                           & "( " _
                                           & "  wodr_uid, " _
                                           & "  wodr_wo_oid, " _
                                           & "  wodr_op, " _
                                           & "  wodr_start_date, " _
                                           & "  wodr_end_date, " _
                                           & "  wodr_desc, " _
                                           & "  wodr_wc_id, " _
                                           & "  wodr_yield_pct,wodr_qty_conversion,wodr_qty_in,wodr_qty_complete,wodr_qty_reject,wodr_qty_out, " _
                                           & "  wodr_seq " _
                                           & ")  " _
                                           & "VALUES ( " _
                                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                           & SetSetring(dr_master("wo_oid")) & ",  " _
                                           & SetInteger(dr("rod_op")) & ",  " _
                                           & SetDateNTime00(dr("rod_start_date")) & ",  " _
                                           & SetDateNTime00(dr("rod_end_date")) & ",  " _
                                           & SetSetring(dr("rod_desc")) & ",  " _
                                           & SetInteger(dr("rod_wc_id")) & ",  " _
                                           & SetDec(dr("rod_insheet_pct")) & ",  " _
                                           & SetDec(dr("rod_conversion")) & ",  " _
                                           & SetDec(dr_master("wo_qty_ord")) & ",0,0,0, " _
                                           & SetInteger(x) & "  " _
                                           & ")"

                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    Else
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                           & "  public.wodr_routing " _
                                           & "( " _
                                           & "  wodr_uid, " _
                                           & "  wodr_wo_oid, " _
                                           & "  wodr_op, " _
                                           & "  wodr_start_date, " _
                                           & "  wodr_end_date, " _
                                           & "  wodr_desc, " _
                                           & "  wodr_wc_id, " _
                                           & "  wodr_yield_pct,wodr_qty_conversion, " _
                                           & "  wodr_seq, " _
                                           & "  wodr_add_by, " _
                                           & "  wodr_add_date " _
                                           & ")  " _
                                           & "VALUES ( " _
                                           & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                           & SetSetring(dr_master("wo_oid")) & ",  " _
                                           & SetInteger(dr("rod_op")) & ",  " _
                                           & SetDateNTime00(dr("rod_start_date")) & ",  " _
                                           & SetDateNTime00(dr("rod_end_date")) & ",  " _
                                           & SetSetring(dr("rod_desc")) & ",  " _
                                           & SetInteger(dr("rod_wc_id")) & ",  " _
                                           & SetDec(dr("rod_insheet_pct")) & ",  " _
                                           & SetDec(dr("rod_conversion")) & ",  " _
                                           & SetInteger(x) & ",  " _
                                           & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                           & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                           & ")"

                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If

                                    
                                    x = x + 1
                                Next
                            End If
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("WO Release " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code"))
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
                        MsgBox("Release success")
                        help_load_data(True)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        sqlTran.Rollback()
                    End Try
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
