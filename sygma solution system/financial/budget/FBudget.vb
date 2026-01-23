Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Imports DevExpress.XtraExport

Public Class FBudget
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As New DataSet
    Dim ds_detail As New DataSet
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Dim _oid_mstr As String
    Dim _code As String
    'Public _pt_id As String
    'Public _si_id As String
    'Public _loc_id As String
    'Public _pt_ac_id As String
    'Public _ptd_ac_code As String
    'Public _ps_oid As String
    'Public _asmb_cost As Double

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
        bdgt_en_id.Properties.DataSource = dt_bantu
        bdgt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bdgt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bdgt_en_id.ItemIndex = 0

        init_le(bdgt_gcal_oid, "gcal_mstr")
        init_le(bdgt_type_code, "bdgtt_type")

    End Sub

    Private Sub asmb_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bdgt_en_id.EditValueChanged
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

        add_column(gv_master, "bdgt_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "bdgt_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Periode", "gcal_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Type", "type_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "bdgt_trans_id", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "bdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "bdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "bdgtd_bdgt_oid", False)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        'add_column(gv_detail, "asmbd_oid", False)
        'add_column(gv_detail, "asmbd_asmb_oid", False)
        'add_column(gv_detail, "asmbd_pt_bom_id", False)
        'add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Seq", "asmbd_seq", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_detail, "Reference", "asmbd_ref", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description", "asmbd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Qty", "asmbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_detail, "Str Type", "asmbd_str_type", DevExpress.Utils.HorzAlignment.Far)
        ''add_column_copy(gv_detail, "Scrap", "asmbd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        ''add_column_copy(gv_detail, "Lead Time Offset", "asmbd_lt_off", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        ''add_column_copy(gv_detail, "Operation", "asmbd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        ''add_column_copy(gv_detail, "Sequence", "asmbd_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        ''add_column_copy(gv_detail, "Forecast Percent", "asmbd_fcst_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        ''add_column_copy(gv_detail, "Option Group", "code_group_name", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_detail, "Process", "code_proc_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "bdgtd_oid", False)
        add_column(gv_edit, "bdgtd_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "{0:n}")

        'add_column(gv_edit, "loc_id", False)
        'add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Str Type", "psd_str_type", DevExpress.Utils.HorzAlignment.Far)
        'add_column_edit(gv_edit, "Scrap", "psd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_edit(gv_edit, "Lead Time Offset", "psd_lt_off", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_edit(gv_edit, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_edit(gv_edit, "Forecast Percent", "psd_fcst_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column(gv_edit, "asmbd_group", False)
        'add_column(gv_edit, "Option Group", "code_group_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "psd_process", False)
        'add_column(gv_edit, "Process", "code_proc_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "pla_ac_id", False)
        'add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "asmbd_sb_id", False)
        'add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "asmbd_cc_id", False)
        'add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.bdgt_oid, " _
                & "  a.bdgt_dom_id, " _
                & "  a.bdgt_en_id, " _
                & "  b.en_desc, " _
                & " bdgt_add_by, " _
                & "  bdgt_add_date, " _
                & "  bdgt_upd_by, " _
                & "  bdgt_upd_date," _
                & "  a.bdgt_date, " _
                & "  a.bdgt_remarks, " _
                & "  a.bdgt_cc_id, " _
                & "  c.type_desc, " _
                & "  a.bdgt_code, " _
                & "  a.bdgt_gcal_oid,bdgt_trans_id,bdgt_type_code, " _
                & "  d.gcal_start_date, " _
                & "  d.gcal_end_date, " _
                & "  d.gcal_year, " _
                & "  d.gcal_periode " _
                & "FROM " _
                & "  public.bdgt_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.bdgt_en_id = b.en_id) " _
                & "  INNER JOIN public.gcal_mstr d ON (a.bdgt_gcal_oid = d.gcal_oid) " _
                & "  INNER JOIN public.bdgtt_type c ON (a.bdgt_type_code = c.type_code) " _
                & " where bdgt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and  bdgt_en_id in (select user_en_id from tconfuserentity " _
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
                & "  a.bdgtd_oid,bdgtd_bdgt_oid, " _
                & "  a.bdgtd_ac_id, " _
                & "  b.ac_code, " _
                & "  b.ac_name, " _
                & "  a.bdgtd_budget " _
                & "FROM " _
                & "  public.bdgtd_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.bdgtd_ac_id = b.ac_id) " _
                & "  INNER JOIN public.bdgt_mstr c ON (a.bdgtd_bdgt_oid = c.bdgt_oid) " _
                & "WHERE " _
                & "  bdgt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and  bdgt_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " & master_new.ClsVar.sUserID.ToString & ") "

        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub relation_detail()
        Try

            gv_detail.Columns("bdgtd_bdgt_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[bdgtd_bdgt_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Sub insert_data_awal()
        bdgt_en_id.ItemIndex = 0
        'be_asmb_ps.EditValue = ""
        bdgt_date.DateTime = master_new.PGSqlConn.CekTanggal
        bdgt_type_code.ItemIndex = 0
        bdgt_remarks.Text = ""


    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()


        Dim ssql As String
        ssql = "select ac_id,ac_name,ac_code from ac_mstr where ac_is_sumlevel='N' and ac_active='Y' and ac_code <> '-' order by ac_code"
        Dim dt_ac As New DataTable
        dt_ac = master_new.PGSqlConn.GetTableData(ssql)

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.bdgtd_oid, " _
                        & "  a.bdgtd_ac_id, " _
                        & "  b.ac_code, " _
                        & "  b.ac_name, " _
                        & "  a.bdgtd_budget " _
                        & "FROM " _
                        & "  public.bdgtd_det a " _
                        & "  INNER JOIN public.ac_mstr b ON (a.bdgtd_ac_id = b.ac_id) " _
                        & "WHERE " _
                        & "  a.bdgtd_bdgt_oid is null "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")

                    Dim _row As DataRow

                    For Each dr_ac As DataRow In dt_ac.Rows
                        _row = ds_edit.Tables(0).NewRow

                        _row.Item("bdgtd_oid") = Guid.NewGuid.ToString
                        _row.Item("bdgtd_ac_id") = dr_ac("ac_id")
                        _row.Item("ac_code") = dr_ac("ac_code")
                        _row.Item("ac_name") = dr_ac("ac_name")
                        _row.Item("bdgtd_budget") = 0.0
                        ds_edit.Tables(0).Rows.Add(_row)

                    Next

                    ds_edit.AcceptChanges()

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


        'Dim _date As Date = bdgt_date.DateTime
        'Dim _gcald_det_status As String = func_data.get_gcald_det_status(bdgt_en_id.EditValue, "gcald_ic", _date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If ds_detail.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        Dim i As Integer


        '*********************
        'Cek part number, Location,account
        'For i = 0 To ds_detail.Tables(0).Rows.Count - 1
        '    If ds_detail.Tables(0).Rows(i).Item("loc_id").ToString.Trim = "" Then
        '        MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_detail.Tables(0)).Position = i
        '        Return False
        '    ElseIf ds_detail.Tables(0).Rows(i).Item("pla_ac_id").ToString.Trim = "" Then
        '        MessageBox.Show("Account Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_detail.Tables(0)).Position = i
        '        Return False
        '    ElseIf ds_detail.Tables(0).Rows(i).Item("psd_qty") = 0 Then
        '        MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        '    If ds_detail.Tables(0).Rows(i).Item("cost") = 0 Then
        '        MessageBox.Show("Can't Save For cost 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'Next
        '*********************


        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function insert() As Boolean
        'Dim _tran_id, _en_id As Integer
        'Dim _pt_id_det As Integer
        'Dim _loc_id_det As Integer
        'Dim _serial, _pt_code_det As String
        Dim ssqls As New ArrayList
        'Dim _cost_avg, _qty As Double
        'Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        ''sb_generate_Click(Nothing, Nothing)
        'Dim _jml As Double = SetNumber(asmb_qty.EditValue)

        'For n As Integer = 0 To ds_detail.Tables(0).Rows.Count - 1
        '    If _jml <> SetNumber(ds_detail.Tables(0).Rows(n).Item("psd_qty")) Then
        '        Box("QTY header dan detail is not equal")
        '        Return False
        '        Exit Function
        '    End If
        '    _jml = SetNumber(ds_detail.Tables(0).Rows(n).Item("psd_qty"))

        'Next

        Dim _oid As String
        _oid = Guid.NewGuid.ToString

        'Dim ds_bantu As New DataSet
        'Dim i, i_2 As Integer

        '_tran_id = func_coll.get_id_tran_mstr("rct-unp")
        'If _tran_id = -1 Then
        '    MessageBox.Show("Inventory Receipt In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    insert = False
        '    Exit Function
        'End If

        'Dim _asmb_id As Integer
        '_asmb_id = SetInteger(func_coll.GetID("asmb_mstr", bdgt_en_id.GetColumnValue("en_code"), "asmb_id", "asmb_en_id", bdgt_en_id.EditValue.ToString))


        Dim _asmb_code As String
        _asmb_code = func_coll.get_transaction_number("BG", bdgt_en_id.GetColumnValue("en_code"), "bdgt_mstr", "bdgt_code")

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
                                        & "  public.bdgt_mstr " _
                                        & "( " _
                                        & "  bdgt_oid, " _
                                        & "  bdgt_dom_id, " _
                                        & "  bdgt_en_id, " _
                                        & "  bdgt_add_by, " _
                                        & "  bdgt_add_date, " _
                                        & "  bdgt_upd_by, " _
                                        & "  bdgt_upd_date, " _
                                        & "  bdgt_date, " _
                                        & "  bdgt_year, " _
                                        & "  bdgt_remarks, " _
                                        & "  bdgt_trans_id, " _
                                        & "  bdgt_dt, " _
                                        & "  bdgt_tran_id, " _
                                        & "  bdgt_type_code, " _
                                        & "  bdgt_code, " _
                                        & "  bdgt_rev, " _
                                        & "  bdgt_active, " _
                                        & "  bdgt_year_periode, " _
                                        & "  bdgt_gcal_oid " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(_oid) & ",  " _
                                        & SetInteger(1) & ",  " _
                                        & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(CekTanggal) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetDateNTime00(bdgt_date.EditValue) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring(bdgt_remarks.EditValue) & ",  " _
                                        & SetSetring("D") & ",  " _
                                        & SetDateNTime(CekTanggal) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring(bdgt_type_code.EditValue) & ",  " _
                                        & SetSetring(_asmb_code) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring(bdgt_gcal_oid.EditValue) & "  " _
                                        & ")"



                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '****************************************
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.bdgtd_det " _
                                        & "( " _
                                        & "  bdgtd_oid, " _
                                        & "  bdgtd_bdgt_oid, " _
                                        & "  bdgtd_add_by, " _
                                        & "  bdgtd_add_date, " _
                                        & "  bdgtd_upd_by, " _
                                        & "  bdgtd_upd_date, " _
                                        & "  bdgtd_bdgtp_id, " _
                                        & "  bdgtd_ac_id, " _
                                        & "  bdgtd_sb_id, " _
                                        & "  bdgtd_budget, " _
                                        & "  bdgtd_alokasi, " _
                                        & "  bdgtd_realisasi, " _
                                        & "  bdgtd_dt " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_oid) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(CekTanggal) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_ac_id")) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetDec(ds_edit.Tables(0).Rows(i).Item("bdgtd_budget")) & ",  " _
                                        & SetInteger(0) & ",  " _
                                        & SetInteger(0) & ",  " _
                                        & SetDateNTime(CekTanggal) & "  " _
                                        & ")"



                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()



                        Next



                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert budget " & _asmb_code)
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
                        set_row(Trim(_oid.ToString), "bdgt_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
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
                & "Where enacc_en_id=" & SetInteger(bdgt_en_id.EditValue) & " and enacc_code='asmbl_account')" 'dbcr_account'asmbl_account


        If _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._pil = 2
            frm._en_id = bdgt_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "ac_code" Then

            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            If limit_account(bdgt_en_id.EditValue) = True Then
                frm._obj = _filter
            End If
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            frm._row = _row
            If limit_account(bdgt_en_id.EditValue) = True Then
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
        Dim ssqls As New ArrayList
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        ssqls.Clear()

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            'load_data_to_detail()

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            ''Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sod_pod_oid from sod_det where sod_so_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString) + ")"
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                            ''******************************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from bdgt_mstr where bdgt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid") + "'"
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_psn_ref_code")) <> "" Then
                            '    .Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "update  " _
                            '                        & "  public.pesanan " _
                            '                        & "set pesan_status='Belum Proses',pesan_so_code=null  where pesan_code=" & _
                            '                        SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_psn_ref_code"))

                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    .Command.Parameters.Clear()
                            'End If


                            'If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_parent_oid")) <> "" Then
                            '    For Each dr_det As DataRow In ds.Tables("detail").Rows
                            '        .Command.CommandType = CommandType.Text
                            '        .Command.CommandText = "update  " _
                            '                            & "  sod_det " _
                            '                            & "set sod_qty_child= coalesce(sod_qty_child,0) - " & SetDbl(dr_det("sod_qty")) _
                            '                            & " where sod_oid=" & SetSetring(dr_det("sod_sod_parent_oid"))

                            '        ssqls.Add(.Command.CommandText)
                            '        .Command.ExecuteNonQuery()
                            '        .Command.Parameters.Clear()
                            '    Next

                            'End If






                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Budget " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code"))
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

    Public Overrides Function edit()
        edit = True
        Dim sSQL As String = ""

        Dim sSQLs As New ArrayList

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
                                        & "  public.bdgt_mstr  " _
                                        & "SET  " _
                                        & "  bdgt_en_id = " & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                        & "  bdgt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  bdgt_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  bdgt_date = " & SetDateNTime00(bdgt_date.EditValue) & ",  " _
                                        & "  bdgt_remarks = " & SetSetring(bdgt_remarks.EditValue) & ",  " _
                                        & "  bdgt_dt = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  bdgt_type_code = " & SetSetring(bdgt_type_code.EditValue) & ",  " _
                                        & "  bdgt_gcal_oid = " & SetSetring(bdgt_gcal_oid.EditValue) & "  " _
                                        & "WHERE  " _
                                        & "  bdgt_oid = " & SetSetring(_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from bdgtd_det " _
                                        & "WHERE  " _
                                        & "  bdgtd_bdgt_oid = " & SetSetring(_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.bdgtd_det " _
                                        & "( " _
                                        & "  bdgtd_oid, " _
                                        & "  bdgtd_bdgt_oid, " _
                                        & "  bdgtd_add_by, " _
                                        & "  bdgtd_add_date, " _
                                        & "  bdgtd_upd_by, " _
                                        & "  bdgtd_upd_date, " _
                                        & "  bdgtd_bdgtp_id, " _
                                        & "  bdgtd_ac_id, " _
                                        & "  bdgtd_sb_id, " _
                                        & "  bdgtd_budget, " _
                                        & "  bdgtd_alokasi, " _
                                        & "  bdgtd_realisasi, " _
                                        & "  bdgtd_dt " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_oid_mstr) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(CekTanggal) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_ac_id")) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetDec(ds_edit.Tables(0).Rows(i).Item("bdgtd_budget")) & ",  " _
                                        & SetInteger(0) & ",  " _
                                        & SetInteger(0) & ",  " _
                                        & SetDateNTime(CekTanggal) & "  " _
                                        & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'LabelControl4.Text = "Entity : (" & n & " of " & dt.Rows.Count & " ) " & dr("en_desc") & " " & ", Account : ( " & i + 1 & " of " & ds_edit.Tables(0).Rows.Count & " ) " & ds_edit.Tables(0).Rows(i).Item("ac_name")
                            'System.Windows.Forms.Application.DoEvents()

                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit budget " & _code & " ")
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
                        after_success()
                        set_row(Trim(_code), "bdgt_code")
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

    Public Overrides Function edit_data() As Boolean
        If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_trans_id")) = "I" Then
            MessageBox.Show("This data has been approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If


        ' gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _oid_mstr = .Item("bdgt_oid").ToString
                _code = .Item("bdgt_code")

                bdgt_en_id.EditValue = .Item("bdgt_en_id")
                bdgt_date.EditValue = .Item("bdgt_date")
                bdgt_remarks.EditValue = .Item("bdgt_remarks")
                bdgt_type_code.EditValue = .Item("bdgt_type_code")
                bdgt_gcal_oid.EditValue = .Item("bdgt_gcal_oid").ToString

            End With
            bdgt_en_id.Focus()


            'Try
            '    tcg_header.SelectedTabPageIndex = 0
            '    tcg_detail.SelectedTabPageIndex = 0
            '    tcg_customer.SelectedTabPageIndex = 0
            'Catch ex As Exception
            'End Try

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.bdgtd_oid, " _
                            & "  a.bdgtd_ac_id, " _
                            & "  b.ac_code, " _
                            & "  b.ac_name, " _
                            & "  a.bdgtd_budget " _
                            & "FROM " _
                            & "  public.bdgtd_det a " _
                            & "  INNER JOIN public.ac_mstr b ON (a.bdgtd_ac_id = b.ac_id) " _
                            & "WHERE " _
                            & "  bdgtd_bdgt_oid = '" + ds.Tables(0).Rows(row).Item("bdgt_oid").ToString + "' order by ac_code"

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
    Private Sub sb_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'generate_detail()
    End Sub

    Private Sub be_asmb_ps_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim frm As New FPTBOMSrch
        frm.set_win(Me)
        frm._en_id = bdgt_en_id.EditValue
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'sb_generate.PerformClick()
    End Sub


    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub be_asmb_ps_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub asmb_loc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim frm As New FLocationSearch
        frm.set_win(Me)
        frm._en_id = bdgt_en_id.EditValue
        frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'sb_generate.PerformClick()
    End Sub

    Private Sub asmb_loc_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub asmb_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ds_detail.AcceptChanges()

            If ds_detail.Tables.Count = 0 Then
                Exit Sub
            End If
            'For Each dr As DataRow In ds_detail.Tables(0).Rows
            '    dr("psd_qty") = asmb_qty.EditValue
            'Next
            ds_detail.AcceptChanges()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImport.Click
        Try


            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(AskOpenFile("Excel Files | *.xls"))


            For Each dr_edit As DataRow In ds_edit.Tables(0).Rows
                dr_edit("bdgtd_budget") = 0.0
            Next
            ds_edit.AcceptChanges()

            For Each dr_edit As DataRow In ds_edit.Tables(0).Rows
                For Each dr_import As DataRow In ds.Tables(0).Rows
                    If dr_edit.Item("ac_code") = dr_import.Item("Account Code") Then
                        dr_edit("bdgtd_budget") = dr_import.Item("Budget")

                        Exit For
                    End If
                Next
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
        gv_edit.BestFitColumns()
    End Sub
End Class
