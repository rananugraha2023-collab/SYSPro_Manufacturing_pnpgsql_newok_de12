Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn


Public Class FGenPSSalesNew
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList

    Private Sub FRouting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal

    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        psgen_en_id.Properties.DataSource = dt_bantu
        psgen_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        psgen_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        psgen_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr())
        psgen_periode_code.Properties.DataSource = dt_bantu
        psgen_periode_code.Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        psgen_periode_code.Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
        psgen_periode_code.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Code", "psgen_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "psgen_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "psgen_periode_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "periode_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End Date", "periode_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "All Child", "psgen_all_child", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "psgen_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "psgen_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "psgen_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "psgen_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "psgen_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "psgend_ptnr_id", False)
        add_column(gv_edit, "psgend_lvl_id", False)
        add_column_copy(gv_edit, "ID", "psgend_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Level", "lvl_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Personal Vol Amount", "psgend_ps_vol", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Team Vol Amount", "psgend_pt_vol", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Child Count", "psgend_network_child", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Child Level", "psgend_child_level", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Amount", "psgend_sales_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Commision", "psgend_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Network Bonus", "psgend_network_bonus", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Total Fee", "psgend_thp_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Level Status", "psgend_level_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Level Old", "lvl_name_old", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_detail, "psgend_ptnr_id", False)
        add_column(gv_detail, "psgend_lvl_id", False)
        add_column_copy(gv_detail, "ID", "psgend_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Level", "lvl_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Personal Vol Amount", "psgend_ps_vol", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Team Vol Amount", "psgend_pt_vol", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Child Count", "psgend_network_child", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Child Level", "psgend_child_level", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Amount", "psgend_sales_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Commision", "psgend_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Network Bonus", "psgend_network_bonus", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Total Fee", "psgend_thp_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Level Status", "psgend_level_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Level Old", "lvl_name_old", DevExpress.Utils.HorzAlignment.Default)



    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.psgen_oid, " _
                & "  a.psgen_dom_id, " _
                & "  a.psgen_en_id, " _
                & "  b.en_desc, " _
                & "  a.psgen_code, " _
                & "  a.psgen_date, " _
                & "  a.psgen_remarks, " _
                & "  a.psgen_all_child, " _
                & "  a.psgen_add_by, " _
                & "  a.psgen_add_date, " _
                & "  a.psgen_upd_by, " _
                & "  a.psgen_upd_date, " _
                & "  a.psgen_periode_code, " _
                & "  c.periode_start_date, " _
                & "  c.periode_end_date " _
                & "FROM " _
                & "  public.psgen_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.psgen_en_id = b.en_id) " _
                & "  INNER JOIN public.psperiode_mstr c ON (a.psgen_periode_code = c.periode_code) " _
                & "WHERE " _
                & " psgen_new_mode='Y' and a.psgen_date BETWEEN " & SetDate(pr_txttglawal.Text) & " AND " & SetDate(pr_txttglakhir.Text) & " " _
                & " and psgen_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " ORDER BY " _
                & "  a.psgen_code"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try


        sql = "SELECT  " _
                & "  a.psgend_oid, " _
                & "  a.psgend_ptnr_id, " _
                & "  b.ptnr_name, " _
                & "  a.psgend_lvl_id,psgend_start_periode, " _
                & "  c.lvl_name, " _
                & "  a.psgend_sales_amount, " _
                & "  a.psgend_level_up, " _
                & "  a.psgend_rookie_amount, " _
                & "  a.psgend_rookie_count, " _
                & "  a.psgend_bronze_amount, " _
                & "  a.psgend_bronze_count, " _
                & "  a.psgend_silver_amount, " _
                & "  a.psgend_silver_count, " _
                & "  a.psgend_gold_amount, " _
                & "  a.psgend_gold_count, " _
                & "  a.psgend_platinum_amount, " _
                & "  a.psgend_platinum_count, " _
                & "  a.psgend_seq, " _
                & "  a.psgend_sales_total, " _
                & "  a.psgend_group_total, psgend_level_status,psgend_lvl_id_old,'' as lvl_name_old, " _
                & "  a.psgend_sales_bonus,psgend_ps_vol,psgend_pt_vol,psgend_network_bonus,psgend_commision,psgend_network_child,psgend_child_level, " _
                & "  a.psgend_group_bonus,psgend_sales_amount_recruitment,psgend_bonus_recruitment,psgend_bonus_total, psgend_thp_total " _
                & "FROM " _
                & "  public.psgend_det a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.psgend_ptnr_id = b.ptnr_id) " _
                & "  INNER JOIN public.pslvl_mstr c ON (a.psgend_lvl_id = c.lvl_id) " _
                & "WHERE " _
                & "  a.psgend_psgen_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.psgend_seq"


        load_data_detail(sql, gc_detail, "detail")
        gv_detail.BestFitColumns()


        Try
            'sql = "select ptnr_id,ptnr_parent,ptnr_name,lvl_name,psgend_sales_amount,psgend_sales_total from (select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
            '  & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
            '  & " where ptnr_id in " _
            '  & " ( select menu_id from get_all_child(" _
            '  & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("psgend_ptnr_id").ToString _
            '  & ")) or ptnr_id in (" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("psgend_ptnr_id").ToString & ")) as temp  " _
            '   & " left outer join public.psgend_det  ON (psgend_ptnr_id = ptnr_id) where psgend_psgen_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_oid").ToString & "'"

            'Dim dt_tree As New DataTable
            'dt_tree = master_new.PGSqlConn.GetTableData(sql)

            'TreeList1.DataSource = dt_tree
            'TreeList1.ExpandAll()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub relation_detail()
        Try
            'load_data_grid_detail()
            'gv_detail.Columns("rod_ro_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rod_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        psgen_en_id.ItemIndex = 0
        psgen_periode_code.ItemIndex = 0
        psgen_en_id.Focus()
        psgen_date.EditValue = master_new.PGSqlConn.CekTanggal
        psgen_all_child.EditValue = False
        psgen_remarks.EditValue = ""

        Try
            XtraTabControl1.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.psgend_oid, " _
                        & "  a.psgend_ptnr_id, " _
                        & "  b.ptnr_name, " _
                        & "  a.psgend_lvl_id,psgend_start_periode, " _
                        & "  c.lvl_name, " _
                        & "  a.psgend_sales_amount, " _
                        & "  a.psgend_level_up, " _
                        & "  a.psgend_rookie_amount, " _
                        & "  a.psgend_rookie_count, " _
                        & "  a.psgend_bronze_amount, " _
                        & "  a.psgend_bronze_count, " _
                        & "  a.psgend_silver_amount, " _
                        & "  a.psgend_silver_count, " _
                        & "  a.psgend_gold_amount, " _
                        & "  a.psgend_gold_count, " _
                        & "  a.psgend_platinum_amount, " _
                        & "  a.psgend_platinum_count, " _
                        & "  a.psgend_seq, " _
                        & "  a.psgend_sales_total, " _
                        & "  a.psgend_group_total, psgend_level_status,psgend_lvl_id_old,'' as lvl_name_old, " _
                        & "  a.psgend_sales_bonus,psgend_ps_vol,psgend_pt_vol,psgend_network_bonus,psgend_commision,psgend_network_child,psgend_child_level, " _
                        & "  a.psgend_group_bonus,psgend_sales_amount_recruitment,psgend_bonus_recruitment,psgend_bonus_total, psgend_thp_total " _
                        & "FROM " _
                        & "  public.psgend_det a " _
                        & "  INNER JOIN public.ptnr_mstr b ON (a.psgend_ptnr_id = b.ptnr_id) " _
                        & "  INNER JOIN public.pslvl_mstr c ON (a.psgend_lvl_id = c.lvl_id) " _
                        & "WHERE " _
                        & "  a.psgend_psgen_oid is null  " _
                        & "ORDER BY " _
                        & "  a.psgend_seq"


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

    Public Overrides Function insert() As Boolean
        Dim i As Integer

        Dim _ro_oid As Guid
        _ro_oid = Guid.NewGuid

        sSQLs.Clear()

        Dim _code As String
        _code = func_coll.get_transaction_number("PS", psgen_en_id.GetColumnValue("en_code"), "psgen_mstr", "psgen_code")

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
                            & "  public.psgen_mstr " _
                            & "( " _
                            & "  psgen_oid, " _
                            & "  psgen_dom_id, " _
                            & "  psgen_en_id, " _
                            & "  psgen_code, " _
                            & "  psgen_date, " _
                            & "  psgen_remarks, " _
                            & "  psgen_all_child, " _
                            & "  psgen_add_by, " _
                            & "  psgen_add_date, " _
                            & "  psgen_periode_code " _
                            & ") " _
                            & "VALUES ( " _
                            & SetSetring(_ro_oid.ToString) & ",  " _
                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                            & SetInteger(psgen_en_id.EditValue) & ",  " _
                            & SetSetring(_code) & ",  " _
                            & SetDateNTime00(psgen_date.EditValue) & ",  " _
                            & SetSetring(psgen_remarks.EditValue) & ",  " _
                            & SetBitYN(psgen_all_child.EditValue) & ",  " _
                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                            & SetSetring(psgen_periode_code.EditValue) & "  " _
                            & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.psgend_det " _
                                        & "( " _
                                        & "  psgend_oid, " _
                                        & "  psgend_psgen_oid, " _
                                        & "  psgend_sales_amount, " _
                                        & "  psgend_ptnr_id, " _
                                        & "  psgend_lvl_id, " _
                                        & "  psgend_seq, " _
                                        & "  psgend_ps_vol, " _
                                        & "  psgend_pt_vol, " _
                                        & "  psgend_network_bonus, " _
                                        & "  psgend_commision, " _
                                        & "  psgend_network_child, " _
                                        & "  psgend_child_level, " _
                                        & "  psgend_level_status, " _
                                        & "  psgend_thp_total,psgend_lvl_id_old " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid.ToString) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_sales_amount")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_ptnr_id")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_lvl_id")) & ",  " _
                                        & SetDec(i) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_ps_vol")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_pt_vol")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_network_bonus")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_commision")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_network_child")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_child_level")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_level_status")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_thp_total")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psgend_lvl_id_old")) & "  " _
                                        & ")"


                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

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
                        set_row(Trim(_ro_oid.ToString), "psgen_oid")
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        'If SetString(genpr_year.EditValue) = "" Then
        '    Box("Year can't empt")
        '    Return False
        '    Exit Function
        'End If
        '*********************
        'Cek UM
        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_wc_id")) = True Then
        '        MessageBox.Show("Workstation Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next
        '*********************

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_tool_code")) = True Then
        '        MessageBox.Show("Tool Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_ptnr_id")) = True Then
        '        MessageBox.Show("Partner Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_milestone")) = True Then
        '        MessageBox.Show("Milestone Can't Empty.. Fill with (Y/N)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False
        'If MyBase.edit_data = True Then
        '    genpr_en_id.Focus()

        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _ro_oid_mstr = .Item("fcs_oid")
        '        genpr_en_id.EditValue = .Item("fcs_en_id")
        '        genpr_qrtr_id.EditValue = .Item("fcs_qrtr_id")
        '        genpr_remarks.EditValue = .Item("fcs_remarks")
        '        genpr_year.EditValue = .Item("fcs_year")
        '    End With

        '    ds_edit = New DataSet
        '    'ds_update_related = New DataSet
        '    Try
        '        Using objcb As New master_new.WDABasepgsql("", "")
        '            With objcb
        '                .SQL = "SELECT  " _
        '                    & "  a.fcsd_oid, " _
        '                    & "  a.fcsd_fcs_oid, " _
        '                    & "  a.fcsd_pt_id, " _
        '                    & "  b.pt_code, " _
        '                    & "  b.pt_desc1, " _
        '                    & "  b.pt_desc2, " _
        '                    & "  d.gr_name, " _
        '                    & "  a.fcsd_01_amount, " _
        '                    & "  a.fcsd_02_amount, " _
        '                    & "  a.fcsd_03_amount, " _
        '                    & "  a.fcsd_total_amount, " _
        '                    & "  a.fcsd_buffer_amount, " _
        '                    & "  a.fcsd_seq " _
        '                    & "FROM " _
        '                    & "  public.fcsd_det a " _
        '                    & "  INNER JOIN public.pt_mstr b ON (a.fcsd_pt_id = b.pt_id) " _
        '                    & "  INNER JOIN public.ptgr_mstr c ON (b.pt_id = c.ptgr_pt_id) " _
        '                    & "  INNER JOIN public.gr_mstr d ON (c.ptgr_gr_id = d.gr_id) " _
        '                    & "WHERE " _
        '                    & "  a.fcsd_fcs_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fcs_oid").ToString & "' " _
        '                    & "ORDER BY " _
        '                    & "  a.fcsd_seq"


        '                .InitializeCommand()
        '                .FillDataSet(ds_edit, "detail_upd")

        '                gc_edit.DataSource = ds_edit.Tables("detail_upd")
        '                gv_edit.BestFitColumns()
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try

        '    edit_data = True
        'End If
    End Function

    Public Overrides Function edit()
        Dim i As Integer

        edit = True
        sSQLs.Clear()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "UPDATE  " _
                        '            & "  public.fcs_mstr   " _
                        '            & "SET  " _
                        '            & "  fcs_en_id = " & SetInteger(genpr_en_id.EditValue) & ",  " _
                        '            & "  fcs_remarks = " & SetSetring(genpr_remarks.EditValue) & ",  " _
                        '            & "  fcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '            & "  fcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                        '            & "  fcs_qrtr_id = " & SetInteger(genpr_qrtr_id.EditValue) & ",  " _
                        '            & "  fcs_year = " & SetInteger(genpr_year.EditValue) & "  " _
                        '            & "WHERE  " _
                        '            & "  fcs_oid = " & SetSetring(_ro_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from fcsd_det where fcsd_fcs_oid = '" + _ro_oid_mstr + "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.fcsd_det " _
                                            & "( " _
                                            & "  fcsd_oid, " _
                                            & "  fcsd_fcs_oid, " _
                                            & "  fcsd_pt_id, " _
                                            & "  fcsd_01_amount, " _
                                            & "  fcsd_02_amount, " _
                                            & "  fcsd_03_amount, " _
                                            & "  fcsd_total_amount, " _
                                            & "  fcsd_buffer_amount, " _
                                            & "  fcsd_seq " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid_mstr.ToString) & ",  " _
                                            & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_pt_id")) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_01_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_02_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_03_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_total_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_buffer_amount"))) & ",  " _
                                            & SetInteger(i) & "  " _
                                            & ")"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

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
                        set_row(Trim(_ro_oid_mstr.ToString), "fcs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If
            sSQLs.Clear()

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from psgen_mstr where psgen_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_oid").ToString + "'"
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged

        Try
            'If e.Column.Name = "fcsd_01_amount" Or e.Column.Name = "fcsd_02_amount" Or e.Column.Name = "fcsd_03_amount" Then
            '    Dim _buffer_persen As Double = 0
            '    Dim _buffer As Double = 0
            '    If gv_edit.GetRowCellValue(e.RowHandle, "gr_code") = "TOP10" Then
            '        _buffer_persen = 0.5
            '    Else
            '        _buffer_persen = 0.2
            '    End If
            '    Dim _total As Double
            '    _total = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_01_amount")) + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_02_amount")) _
            '    + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_03_amount"))

            '    _buffer = _total / 3 * _buffer_persen
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_total_amount", _total)
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_buffer_amount", _buffer)

            'End If
            'gv_edit.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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
        Dim _rod_en_id As Integer = psgen_en_id.EditValue

        'If _col = "wc_desc" Then
        '    Dim frm As New FWCSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "code_name" Then
        '    Dim frm As New FToolSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "ptnr_name" Then
        '    Dim frm As New FPartnerSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'Dim _now As DateTime
        '_now = func_coll.get_now
        'With gv_edit
        '    .SetRowCellValue(e.RowHandle, "rod_op", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_start_date", _now)
        '    .SetRowCellValue(e.RowHandle, "rod_end_date", _now)
        '    .SetRowCellValue(e.RowHandle, "rod_mch_op", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_tran_qty", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_queue", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_wait", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_move", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_run", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_setup", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_yield_pct", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_milestone", "N")
        '    .SetRowCellValue(e.RowHandle, "rod_sub_lead", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_setup_men", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_men_mch", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_sub_cost", 0)
        '    .BestFitColumns()
        'End With
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try

            Dim _en_id_all As String

            If psgen_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(psgen_en_id.EditValue)
            Else

                _en_id_all = psgen_en_id.EditValue

            End If

            Dim sSQL As String

            sSQL = "SELECT  " _
                    & "  b.lvl_oid, " _
                    & "  b.lvl_id, " _
                    & "  b.lvl_code, " _
                    & "  b.lvl_name, " _
                    & "  b.lvl_ps_min, " _
                    & "  b.lvl_pt_min, " _
                    & "  b.lvl_network_min, " _
                    & "  b.lvl_network_min_code, " _
                    & "  b.lvl_ps_commision, " _
                    & "  b.lvl_network_commision " _
                    & "FROM " _
                    & "  public.pslvl_mstr b " _
                    & "  order by lvl_id"

            Dim dt_lvl As New DataTable
            dt_lvl = GetTableData(sSQL)



            sSQL = "SELECT  " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name, " _
                & "  b.lvl_code, " _
                & "  b.lvl_name,a.ptnr_lvl_id, " _
                & "  lvl_sales_min, " _
                & "  lvl_min_child, " _
                & "  lvl_sales_gorup_min, " _
                & "  lvl_gen_rookie, " _
                & "  lvl_gen_bronze, " _
                & "  lvl_gen_silver, " _
                & "  lvl_gen_platinum, " _
                & "  lvl_gen_gold, " _
                & "  lvl_disc, " _
                & "  lvl_bonus_pribadi, " _
                & "  lvl_bonus_grup,lvl_bonus_recruitment," _
                & "  a.ptnr_start_periode " _
                & "FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.pslvl_mstr b ON (a.ptnr_lvl_id = b.lvl_id) " _
                & "WHERE " _
                & "  a.ptnr_is_ps = 'Y' AND ptnr_en_id=" & psgen_en_id.EditValue _
                & " AND a.ptnr_active = 'Y' " _
                & " order by ptnr_lvl_id "


            Dim dt_fs As New DataTable
            dt_fs = master_new.PGSqlConn.GetTableData(sSQL)
            Dim dr_data As DataRow
            ds_edit.Tables(0).Rows.Clear()
            Dim dt_tarif As New DataTable
            Dim dt_tarif2 As New DataTable

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                _row("pwgend_ptnr_id") = dr("ptnr_id")
                _row("ptnr_name") = dr("ptnr_name")
                _row("pwgend_lvl_id") = dr("ptnr_lvl_id")
                _row("lvl_name") = dr("lvl_name")
                '_row("pwgend_start_periode") = dr("ptnr_start_periode")

                sSQL = "SELECT  " _
                  & "SUM(coalesce(sod_sales_unit,0) * soshipd_qty * -1 ) as  su_total " _
                  & "FROM soshipd_det " _
                  & "inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                  & "inner join sod_det on sod_oid = soshipd_sod_oid " _
                  & "inner join so_mstr on so_oid = sod_so_oid " _
                  & "inner join pt_mstr on pt_id = sod_pt_id " _
                  & "inner join en_mstr on en_id = soship_en_id " _
                  & "inner join code_mstr x on x.code_id = so_pay_type " _
                  & "inner join  " _
                  & "(SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr " _
                  & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                  & " code_name ~~* 'ppn') taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class " _
                  & "inner join  " _
                  & "(SELECT taxr_tax_class, taxr_rate as rate_pph FROM taxr_mstr " _
                  & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                  & " code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = sod_tax_class " _
                  & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                  & "inner join ptnr_mstr bill_mstr on bill_mstr.ptnr_id = so_ptnr_id_bill " _
                  & "inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id " _
                  & "where soship_date >= " + SetDate(psgen_start_date.EditValue) _
                  & " and soship_date <= " + SetDate(psgen_end_date.EditValue) _
                  & " and (so_sales_person= " & SetInteger(dr("ptnr_id")) & " or so_ptnr_id_bill=" & SetInteger(dr("ptnr_id")) & ")"

                System.Windows.Forms.Application.DoEvents()
                dr_data = master_new.PGSqlConn.GetRowInfo(sSQL)

                _row("pwgend_ps_vol") = SetNumber(dr_data(0))

                sSQL = "SELECT  " _
                  & "SUM(coalesce(sod_sales_unit,0) * soshipd_qty * -1 ) as  su_total " _
                  & "FROM soshipd_det " _
                  & "inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                  & "inner join sod_det on sod_oid = soshipd_sod_oid " _
                  & "inner join so_mstr on so_oid = sod_so_oid " _
                  & "inner join pt_mstr on pt_id = sod_pt_id " _
                  & "inner join en_mstr on en_id = soship_en_id " _
                  & "inner join code_mstr x on x.code_id = so_pay_type " _
                  & "inner join  " _
                  & "(SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr " _
                  & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                  & " code_name ~~* 'ppn') taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class " _
                  & "inner join  " _
                  & "(SELECT taxr_tax_class, taxr_rate as rate_pph FROM taxr_mstr " _
                  & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                  & " code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = sod_tax_class " _
                  & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                  & "inner join ptnr_mstr bill_mstr on bill_mstr.ptnr_id = so_ptnr_id_bill " _
                  & "inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id " _
                  & "where soship_date >= " + SetDate(psgen_start_date.EditValue) _
                  & " and soship_date <= " + SetDate(psgen_end_date.EditValue) _
                  & " and ( so_sales_person in (select ptnr_id from ptnr_mstr where ptnr_parent=" & SetInteger(dr("ptnr_id")) _
                  & ") or so_ptnr_id_bill in (select ptnr_id from ptnr_mstr where ptnr_parent=" & SetInteger(dr("ptnr_id")) & "  ))"

                System.Windows.Forms.Application.DoEvents()
                dr_data = master_new.PGSqlConn.GetRowInfo(sSQL)

                _row("pwgend_pt_vol") = SetNumber(dr_data(0))


                sSQL = "SELECT  " _
                & "SUM(soshipd_qty * sod_price * -1 ) as  su_total, SUM(soshipd_qty * sod_price * coalesce(sod_commision,0) * -1) as  sod_commision " _
                & "FROM soshipd_det " _
                & "inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                & "inner join sod_det on sod_oid = soshipd_sod_oid " _
                & "inner join so_mstr on so_oid = sod_so_oid " _
                & "inner join pt_mstr on pt_id = sod_pt_id " _
                & "inner join en_mstr on en_id = soship_en_id " _
                & "inner join code_mstr x on x.code_id = so_pay_type " _
                & "inner join  " _
                & "(SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr " _
                & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                & " code_name ~~* 'ppn') taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class " _
                & "inner join  " _
                & "(SELECT taxr_tax_class, taxr_rate as rate_pph FROM taxr_mstr " _
                & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                & " code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = sod_tax_class " _
                & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                & "inner join ptnr_mstr bill_mstr on bill_mstr.ptnr_id = so_ptnr_id_bill " _
                & "inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id " _
                & "where soship_date >= " + SetDate(psgen_start_date.EditValue) _
                & " and soship_date <= " + SetDate(psgen_end_date.EditValue) _
                & " and ( so_sales_person = " & SetInteger(dr("ptnr_id")) _
                & " or so_ptnr_id_bill =" & SetInteger(dr("ptnr_id")) & "  )"

                System.Windows.Forms.Application.DoEvents()
                dr_data = master_new.PGSqlConn.GetRowInfo(sSQL)

                _row("pwgend_sales_amount") = SetNumber(dr_data(0))
                _row("pwgend_commision") = SetNumber(dr_data(1))

                sSQL = "SELECT  " _
                    & "  left(b.lvl_code,2) as lvl_code,count(*) as jml " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.pslvl_mstr b ON (a.ptnr_lvl_id = b.lvl_id) " _
                    & "WHERE " _
                    & "  a.ptnr_parent = " & SetInteger(dr("ptnr_id")) & " " _
                    & "  group by left(b.lvl_code,2),lvl_sort order by lvl_sort desc "

                Dim _jml As Double = 0
                Dim _level As String = ""

                Dim dt_child As New DataTable
                dt_child = GetTableData(sSQL)

                For Each dr_child As DataRow In dt_child.Rows
                    If SetNumber(dr_child("jml")) > 1 Then
                        _jml = _jml + dr_child("jml")
                        _level = dr_child("lvl_code")
                        Exit For
                    End If
                Next

                _row("pwgend_network_child") = _jml
                _row("pwgend_child_level") = _level

                'If SetNumber(_row("pwgend_ptnr_id")) = 35006218 Then
                '    Dim ccc As Integer = 0
                'End If

                Dim _lvl_ps_commision As Double = 0
                Dim _lvl_network_commision As Double = 0
                If (_row("pwgend_ps_vol") > 0) Or (_row("pwgend_pt_vol") > 0) Then


                    If _level <> "" Then
                        sSQL = "SELECT  " _
                          & "  a.lvl_ps_commision,lvl_network_commision,lvl_id,lvl_name " _
                          & "FROM " _
                          & "  public.pslvl_mstr a " _
                          & "WHERE " _
                          & "  a.lvl_ps_min<=" & SetDec(_row("pwgend_ps_vol")) & " AND  " _
                          & "  a.lvl_pt_min<=" & SetDec(_row("pwgend_pt_vol")) & " and lvl_network_min <=" & SetInteger(_jml) _
                          & "  and lvl_network_min_code=" & SetSetring(Microsoft.VisualBasic.Left(_level, 2)) _
                          & " ORDER BY " _
                          & "  a.lvl_id DESC limit 1"
                    Else
                        sSQL = "SELECT  " _
                          & "  a.lvl_ps_commision,lvl_network_commision,lvl_id,lvl_name " _
                          & "FROM " _
                          & "  public.pslvl_mstr a " _
                          & "WHERE " _
                          & "  a.lvl_ps_min<=" & SetDec(_row("pwgend_ps_vol")) & " AND  " _
                          & "  a.lvl_pt_min<=" & SetDec(_row("pwgend_pt_vol")) & " and lvl_ps_commision > 0 " _
                          & " ORDER BY " _
                          & "  a.lvl_id DESC limit 1"
                    End If


                    dt_tarif = GetTableData(sSQL)


                    sSQLs.Clear()
                    If dt_tarif.Rows.Count > 0 Then
                        _lvl_ps_commision = dt_tarif.Rows(0).Item(0)
                        _lvl_network_commision = dt_tarif.Rows(0).Item(1)
                        If _row("pwgend_lvl_id") < dt_tarif.Rows(0).Item("lvl_id") Then

                            _row("lvl_name_old") = _row("lvl_name")
                            _row("pwgend_lvl_id_old") = _row("pwgend_lvl_id")
                            _row("pwgend_lvl_id") = dt_tarif.Rows(0).Item("lvl_id")
                            _row("lvl_name") = dt_tarif.Rows(0).Item("lvl_name")

                            sSQL = "update ptnr_mstr set ptnr_lvl_id=" & SetInteger(_row("pwgend_lvl_id")) & " where ptnr_id=" & SetInteger(dr("ptnr_id"))
                            sSQLs.Add(sSQL)

                            _row("pwgend_level_status") = "UP"
                        ElseIf _row("pwgend_lvl_id") > dt_tarif.Rows(0).Item("lvl_id") Then
                            _row("pwgend_level_status") = "DOWN"
                        ElseIf _row("pwgend_lvl_id") = dt_tarif.Rows(0).Item("lvl_id") Then
                            _row("pwgend_level_status") = "NORMAL"
                        End If
                    Else
                        'If _level <> "" Then
                        sSQL = "SELECT  " _
                         & "  a.lvl_ps_commision,lvl_network_commision,lvl_id,lvl_name " _
                         & "FROM " _
                         & "  public.pslvl_mstr a " _
                         & "WHERE " _
                         & "  a.lvl_ps_min<=" & SetDec(_row("pwgend_ps_vol")) & " AND  " _
                         & "  a.lvl_pt_min<=" & SetDec(_row("pwgend_pt_vol")) & " and lvl_ps_commision = 0 and lvl_id<" & SetInteger(_row("pwgend_lvl_id")) _
                         & " ORDER BY " _
                         & "  a.lvl_id DESC limit 1"

                        dt_tarif2 = GetTableData(sSQL)
                        For Each dr2 As DataRow In dt_tarif2.Rows
                            _row("pwgend_level_status") = "DOWN"
                            _lvl_network_commision = dr2(1)
                        Next
                        'End If
                    End If


                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                            Exit Sub
                        End If
                        sSQLs.Clear()
                    Else
                        If DbRunTran(sSQLs, "") = False Then
                            Exit Sub
                        End If
                        sSQLs.Clear()
                    End If
                End If
                '_row("pwgend_commision") = (_lvl_ps_commision * _row("pwgend_ps_vol") * 10000) + (_lvl_ps_commision * 10000 * _row("pwgend_pt_vol"))
                _row("pwgend_network_bonus") = (_lvl_network_commision)

                sSQL = "SELECT  " _
                         & "  a.lvl_ps_commision,lvl_network_commision,lvl_id,lvl_name " _
                         & "FROM " _
                         & "  public.pslvl_mstr a " _
                         & "WHERE " _
                         & "  a.lvl_id=" & SetInteger(_row("pwgend_lvl_id")) & " "

                If GetRowInfo(sSQL)(0) = 0 Then
                    _row("pwgend_commision") = 0
                End If


                _row("pwgend_thp_total") = _row("pwgend_commision") + _row("pwgend_network_bonus")

                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
            Box("Generate Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub psgen_periode_code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles psgen_periode_code.EditValueChanged
        Try
            psgen_start_date.EditValue = psgen_periode_code.GetColumnValue("periode_start_date")
            psgen_end_date.EditValue = psgen_periode_code.GetColumnValue("periode_end_date")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_detail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_detail.Click
        Try
            gv_detail_SelectionChanged(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gv_detail_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_detail.SelectionChanged
        Try
            If ds.Tables("detail").Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String
            sql = "select ptnr_id,ptnr_parent,ptnr_name,lvl_name,psgend_sales_amount,psgend_sales_total from (select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
              & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
              & " where ptnr_id in " _
              & " ( select menu_id from get_all_child(" _
              & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("psgend_ptnr_id").ToString _
              & ")) or ptnr_id in (" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("psgend_ptnr_id").ToString & ")) as temp  " _
               & " left outer join public.psgend_det  ON (psgend_ptnr_id = ptnr_id) where psgend_psgen_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_oid").ToString & "'"

            Dim dt_tree As New DataTable
            dt_tree = master_new.PGSqlConn.GetTableData(sql)

            Try
                TreeList1.DataSource = dt_tree
                TreeList1.ExpandAll()
            Catch ex As Exception
            End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Try
            Dim _file As String = ""
            _file = AskSaveAsFile("Xls Files | *.xls")
            If _file = "" Then
                Box("Please select file name to export")
                Exit Sub
            End If

            TreeList1.ExportToXls(_file)
            OpenFile(_file)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub preview()

        Dim _sql As String
        _sql = "SELECT  " _
            & "  a.psgen_oid, " _
            & "  a.psgen_en_id, " _
            & "  f.en_desc, " _
            & "  a.psgen_code, " _
            & "  a.psgen_date, " _
            & "  a.psgen_remarks, " _
            & "  a.psgen_all_child, " _
            & "  a.psgen_periode_code, " _
            & "  b.periode_start_date, " _
            & "  b.periode_end_date, " _
            & "  c.psgend_ptnr_id, " _
            & "  d.ptnr_name, " _
            & "  c.psgend_lvl_id, " _
            & "  e.lvl_name, " _
            & "  c.psgend_sales_amount, " _
            & "  c.psgend_sales_total, " _
            & "  c.psgend_group_total, " _
            & "  c.psgend_sales_bonus, " _
            & "  c.psgend_group_bonus, " _
            & "  c.psgend_sales_amount_recruitment, " _
            & "  c.psgend_bonus_recruitment, " _
            & "  c.psgend_thp_total, " _
            & "  c.psgend_rookie_count + c.psgend_bronze_count + c.psgend_silver_count + c.psgend_gold_count + c.psgend_platinum_count AS child_count, " _
            & "  d.ptnr_parent, " _
            & "  d.ptnr_id " _
            & "FROM " _
            & "  public.psgen_mstr a " _
            & "  INNER JOIN public.psgend_det c ON (a.psgen_oid = c.psgend_psgen_oid) " _
            & "  INNER JOIN public.psperiode_mstr b ON (a.psgen_periode_code = b.periode_code) " _
            & "  INNER JOIN public.ptnr_mstr d ON (c.psgend_ptnr_id = d.ptnr_id) " _
            & "  INNER JOIN public.pslvl_mstr e ON (c.psgend_lvl_id = e.lvl_id) " _
            & "  INNER JOIN public.en_mstr f ON (a.psgen_en_id = f.en_id) " _
            & "WHERE " _
            & "  a.psgen_periode_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_periode_code") + "' AND  " _
            & "  a.psgen_en_id = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_en_id").ToString + ""


        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRPSReport"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("psgen_code")
        'frm.ShowDialog()


        Dim rpt As New XRPSReport
        With rpt
            Dim ds As New DataSet
            ds = master_new.PGSqlConn.ReportDataset(_sql)
            If ds.Tables(0).Rows.Count < 1 Then
                Box("Maaf data kosong")
                Exit Sub
            End If




            'ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

            'Dim dt As New DataTable
            'dt = GetTableData(ssql)

            'For Each dr As DataRow In dt.Rows
            '    .XrLabelTitle.Text = dr("dom_company")
            'Next

            'If Ce_Posting.EditValue = True Then
            '    .Posting_Option = True
            'Else
            '    .Posting_Option = False
            'End If

            '.periode = de_end.DateTime.ToString("dd MMMM yyyy")
            '.TreeList1.ExpandAll()
            .TreeList1.DataSource = ds.Tables(0)
            '.TreeList1.ExpandAll()
            .XrLabelPeriode.Text = "PERIODE : " & Format(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_start_date"), "dd/MM/yyyy") & " - " & Format(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_end_date"), "dd/MM/yyyy")

            '.DataSource = ds
            '.DataMember = "Table"
            '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
            '.Parameters("PPosisi").Value = posisi

             Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            printTool.PreviewForm.Text = "Personal Selling Report"
            '.PrintingSystem = ps
            printTool.ShowPreview()
            .TreeList1.ExpandAll()

        End With
    End Sub
End Class
