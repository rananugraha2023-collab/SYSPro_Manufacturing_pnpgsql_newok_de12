Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraEditors.Controls

Public Class FGenKomisiSDIbackup
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
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_en_mstr_mstr())
            segen_en_id.Properties.DataSource = dt_bantu
            segen_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            segen_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            segen_en_id.ItemIndex = 0

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_periode_mstr_se())
            With segen_periode
                If .Properties.Columns.VisibleCount = 0 Then
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_code", "Code", 20))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_bonus_gen", "Generate Bonus", 20))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_payment_date", "Payment Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
                    .Properties.Columns.Add(New LookUpColumnInfo("seperiode_remarks", "Remarks", 20))
                End If

                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("seperiode_code").ToString
                .Properties.ValueMember = dt_bantu.Columns("seperiode_code").ToString
                If dt_bantu.Rows.Count > 0 Then
                    .EditValue = dt_bantu.Rows(0).Item(.Properties.ValueMember)
                    segen_start_date.EditValue = segen_periode.GetColumnValue("seperiode_start_date")
                    segen_end_date.EditValue = segen_periode.GetColumnValue("seperiode_end_date")
                    segen_remarks.EditValue = segen_periode.GetColumnValue("seperiode_remarks")
                End If

                .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
                .Properties.BestFit()
                .Properties.DropDownRows = 12
                .Properties.PopupWidth = 600
                .ItemIndex = 0
            End With



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "All Child", "segen_all_child", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Code", "segen_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "segen_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "segen_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "seperiode_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End Date", "seperiode_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Generate Bonus", "seperiode_bonus_gen", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Income", "segen_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Add Income", "segen_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Coaching Income", "segen_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Income", "segen_total_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Remarks", "segen_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "segen_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "segen_add_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "segend_lvl_id", False)
        add_column_copy(gv_edit, "ID", "segend_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Sales Name", "segend_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Level", "segend_level", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "entity", "segend_entity", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_edit, "NPWP", "segend_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Tax Name", "segend_nama_tax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Marital Status", "segend_status_marital", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Amount Dependents", "segend_amount_dependents", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_edit, "SU", "segend_su_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Multiplier Commission", "segend_pengali_komisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Commission", "segend_komisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit, "Month Commission", "segend_komisi_bulanan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Paid Commission", "segend_komisi_telah_dibayar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_edit, "Active Sales", "segend_active_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Group SU", "segend_su_group", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Percent Add Income", "segend_percent_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_edit, "Multiplier Add Income", "segend_pengali_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Add Income", "segend_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Percent Coaching Income", "segend_percent_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Multiplier Coaching Income", "segend_pengali_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Coaching Income", "segend_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Income Total", "segend_total_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_edit, "Payment Date", "segend_payment_date", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_detail, "segend_lvl_id", False)
        add_column_copy(gv_detail, "ID", "segend_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Name", "segend_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Level", "segend_level", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "entity", "segend_entity", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "NPWP", "segend_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Name", "segend_nama_tax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Marital Status", "segend_status_marital", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Amount Dependents", "segend_amount_dependents", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "SU", "segend_su_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Multiplier Commission", "segend_pengali_komisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Commission", "segend_komisi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column_copy(gv_detail, "Month Commission", "segend_komisi_bulanan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Paid Commission", "segend_komisi_telah_dibayar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column_copy(gv_detail, "Active Sales", "segend_active_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Group SU", "segend_su_group", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Percent Add Income", "segend_percent_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Multiplier Add Income", "segend_pengali_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Add Income", "segend_add_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Percent Coaching Income", "segend_percent_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Multiplier Coaching Income", "segend_pengali_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Coaching Income", "segend_coaching_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Income Total", "segend_total_income", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Payment Date", "segend_payment_date", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.segen_oid, " _
                & "  a.segen_dom_id, " _
                & "  a.segen_en_id, " _
                & "  b.en_desc, " _
                & "  a.segen_date, " _
                & "  a.segen_code, " _
                & "  a.segen_periode, " _
                & "  a.segen_total_income,segen_income,segen_add_income,segen_coaching_income, segen_all_child, " _
                & "  a.segen_remarks, " _
                & "  a.segen_add_by, " _
                & "  a.segen_add_date, " _
                & "  a.segen_total_su, " _
                & "  c.seperiode_start_date, " _
                & "  c.seperiode_end_date, " _
                & "  c.seperiode_bonus_gen " _
                & "FROM " _
                & "  public.segen_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.segen_en_id = b.en_id) " _
                & "  INNER JOIN public.seperiode_mstr c ON (a.segen_periode = c.seperiode_code) " _
                & "WHERE " _
                & "  a.segen_date BETWEEN " & SetDate(pr_txttglawal.Text) & " AND " & SetDate(pr_txttglakhir.Text) & "  " _
                  & " and segen_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "ORDER BY " _
                & "  a.segen_code"


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
                & "  a.segend_oid, " _
                & "  a.segend_segen_oid, " _
                & "  a.segend_ptnr_id, " _
                & "  a.segend_level, " _
                & "  a.segend_nama, " _
                & "  a.segend_active_sales, " _
                & "  a.segend_su_sales, " _
                & "  a.segend_pengali_komisi, " _
                & "  a.segend_komisi, " _
                & "  a.segend_su_group, " _
                & "  a.segend_percent_add_income, " _
                & "  a.segend_pengali_add_income, " _
                & "  a.segend_add_income, " _
                & "  a.segend_percent_coaching_income, " _
                & "  a.segend_pengali_coaching_income,segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_entity, " _
                & "  a.segend_lvl_id, " _
                & "  a.segend_parent,segend_komisi_telah_dibayar,segend_komisi_bulanan " _
                & "FROM " _
                & "  public.segend_det a " _
                & "WHERE " _
                & "  a.segend_segen_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.segend_nama"


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
        segen_en_id.ItemIndex = 0
        segen_periode.ItemIndex = 0
        segen_en_id.Focus()
        segen_date.EditValue = master_new.PGSqlConn.CekTanggal
        segen_remarks.EditValue = ""

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
                        & "  a.segend_oid, " _
                        & "  a.segend_segen_oid, " _
                        & "  a.segend_ptnr_id, " _
                        & "  a.segend_level, " _
                        & "  a.segend_nama, " _
                        & "  a.segend_active_sales,segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents, " _
                        & "  a.segend_su_sales, " _
                        & "  a.segend_pengali_komisi, " _
                        & "  a.segend_komisi, " _
                        & "  a.segend_su_group, " _
                        & "  a.segend_percent_add_income, " _
                        & "  a.segend_pengali_add_income, " _
                        & "  a.segend_add_income, " _
                        & "  a.segend_percent_coaching_income, " _
                        & "  a.segend_pengali_coaching_income, " _
                        & "  a.segend_coaching_income, " _
                        & "  a.segend_total_income, " _
                        & "  a.segend_payment_date, segend_en_id," _
                        & "  a.segend_entity,segend_lvl_id,segend_parent,segend_komisi_telah_dibayar,segend_komisi_bulanan " _
                        & "FROM " _
                        & "  public.segend_det a where segend_oid is null"

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
        _code = func_coll.get_transaction_number("SE", segen_en_id.GetColumnValue("en_code"), "segen_mstr", "segen_code")
        Dim _su As Double
        Dim _income As Double = 0
        Dim _add_income As Double = 0
        Dim _coach_income As Double
        Dim _total As Double

        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
            _income += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi"))
            _add_income += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_add_income"))
            _coach_income += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_coaching_income"))
            _total += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_total_income"))
        Next
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
                                & "  public.segen_mstr " _
                                & "( " _
                                & "  segen_oid, " _
                                & "  segen_dom_id, " _
                                & "  segen_en_id, " _
                                & "  segen_date, " _
                                & "  segen_code, " _
                                & "  segen_periode, " _
                                & "  segen_total_income, " _
                                & "  segen_remarks, " _
                                & "  segen_add_by, " _
                                & "  segen_add_date, " _
                                & "  segen_total_su, " _
                                & "  segen_income, " _
                                & "  segen_add_income,segen_all_child, " _
                                & "  segen_coaching_income " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_ro_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(segen_en_id.EditValue) & ",  " _
                                & SetDateNTime00(segen_date.EditValue) & ",  " _
                                & SetSetring(_code) & ",  " _
                                & SetSetring(segen_periode.EditValue) & ",  " _
                                & SetDec(_total) & ",  " _
                                & SetSetring(segen_remarks.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetDec(_su) & ",  " _
                                & SetDec(_income) & ",  " _
                                & SetDec(_add_income) & ",  " _
                                & SetBitYN(segen_all_child.EditValue) & ",  " _
                                & SetDec(_coach_income) & "  " _
                                & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            If SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_total_income")) > 0.0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                        & "  public.segend_det " _
                                        & "( " _
                                        & "  segend_oid, " _
                                        & "  segend_segen_oid, " _
                                        & "  segend_ptnr_id, " _
                                        & "  segend_level, " _
                                        & "  segend_nama, " _
                                        & "  segend_active_sales, " _
                                        & "  segend_su_sales, " _
                                        & "  segend_pengali_komisi, " _
                                        & "  segend_komisi, " _
                                        & "  segend_su_group, " _
                                        & "  segend_percent_add_income, " _
                                        & "  segend_pengali_add_income, " _
                                        & "  segend_add_income, " _
                                        & "  segend_percent_coaching_income, " _
                                        & "  segend_pengali_coaching_income, " _
                                        & "  segend_coaching_income, " _
                                        & "  segend_total_income,segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents, " _
                                        & "  segend_payment_date,segend_komisi_telah_dibayar,segend_komisi_bulanan, " _
                                        & "  segend_entity, " _
                                        & "  segend_lvl_id,segend_en_id, " _
                                        & "  segend_parent " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid.ToString) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_ptnr_id")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_level")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_nama")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_active_sales")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_su_sales")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pengali_komisi")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_su_group")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_percent_add_income")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pengali_add_income")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_add_income")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_percent_coaching_income")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pengali_coaching_income")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_coaching_income")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_total_income")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_nama_tax")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_npwp")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_status_marital")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_amount_dependents")) & ",  " _
                                        & SetDateNTime00(ds_edit.Tables("insert_edit").Rows(i).Item("segend_payment_date")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_telah_dibayar")) & ",  " _
                                        & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_bulanan")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("segend_entity")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_lvl_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_en_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("segend_parent")) & "  " _
                                        & ")"

                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If


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
                        set_row(Trim(_ro_oid.ToString), "segen_oid")
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
                            .Command.CommandText = "delete from segen_mstr where segen_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid") + "'"
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
        Dim _rod_en_id As Integer = segen_en_id.EditValue

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

            If segen_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(segen_en_id.EditValue)
            Else

                _en_id_all = segen_en_id.EditValue

            End If

            Dim sSQL As String

            sSQL = "SELECT  " _
                & "  a.ptnr_id,ptnr_en_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name, " _
                & "  c.en_desc,ptnr_marital_status,ptnr_amount_dependents,ptnr_npwp,ptnr_name_alt, " _
                & "  b.lvl_code, " _
                & "  a.ptnr_parent ,a.ptnr_lvl_id " _
                & "FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.pslvl_mstr b ON (a.ptnr_lvl_id = b.lvl_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id) " _
                & "WHERE " _
                & "  a.ptnr_is_ps = 'Y' AND ptnr_en_id in (" & _en_id_all _
                & ") AND a.ptnr_active = 'Y'"


            Dim dt_fs As New DataTable
            dt_fs = GetTableData(sSQL)
            Dim dr_data As DataRow
            ds_edit.Tables(0).Rows.Clear()
            Dim dt_komisi As New DataTable
            Dim dt_count As New DataTable
            Dim dt_add As New DataTable
            Dim dt_coach As New DataTable
            Dim dt_bayar As New DataTable

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                _row("segend_ptnr_id") = dr("ptnr_id")
                _row("segend_nama") = dr("ptnr_name")
                _row("segend_lvl_id") = dr("ptnr_lvl_id")
                _row("segend_level") = dr("lvl_code")
                _row("segend_entity") = dr("en_desc")
                _row("segend_parent") = dr("ptnr_parent")
                _row("segend_en_id") = dr("ptnr_en_id")

                _row("segend_status_marital") = dr("ptnr_marital_status")
                _row("segend_amount_dependents") = dr("ptnr_amount_dependents")
                _row("segend_npwp") = dr("ptnr_npwp")
                _row("segend_nama_tax") = dr("ptnr_name_alt")

                sSQL = "SELECT  " _
                    & "SUM((sod_sales_unit * (-1) * soshipd_qty)) sod_sales_unit_total " _
                     & "FROM  " _
                       & "  public.soship_mstr " _
                       & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                       & "  inner join so_mstr on so_oid = soship_so_oid " _
                       & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                       & "  inner join pi_mstr on so_pi_id = pi_id " _
                       & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                       & "  inner join en_mstr on en_id = soship_en_id " _
                       & "  inner join si_mstr on si_id = soship_si_id " _
                       & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                       & "  inner join pt_mstr on pt_id = sod_pt_id " _
                       & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                       & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                       & "  inner join cu_mstr on cu_id = so_cu_id " _
                       & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                       & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                       & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                       & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                       & "  left outer join ar_mstr on ar_oid = ars_ar_oid " _
                       & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                       & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                       & "  inner join pl_mstr on pl_id = pt_pl_id " _
                    & "where soship_date >= " + SetDate(segen_start_date.EditValue) _
                    & " and soship_date <= " + SetDate(segen_end_date.EditValue) _
                    & " and (so_sales_person= " & dr("ptnr_id") & ") "

                System.Windows.Forms.Application.DoEvents()
                dr_data = master_new.PGSqlConn.GetRowInfo(sSQL)

                _row("segend_su_sales") = SetNumber(dr_data(0))

                sSQL = "SELECT  " _
                    & "  a.komisi_oid, " _
                    & "  a.komisi_su_start, " _
                    & "  a.komisi_su_end, " _
                    & "  a.komisi_pengali " _
                    & "FROM " _
                    & "  public.sekomisi_mstr a " _
                    & "ORDER BY " _
                    & "  a.komisi_su_start"

                dt_komisi = GetTableData(sSQL)

                For Each dr_komisi As DataRow In dt_komisi.Rows
                    If SetNumber(_row("segend_su_sales")) > dr_komisi("komisi_su_start") And SetNumber(_row("segend_su_sales")) <= dr_komisi("komisi_su_end") Then
                        _row("segend_pengali_komisi") = SetNumber(dr_komisi("komisi_pengali"))
                        Exit For
                    End If
                Next

                _row("segend_komisi") = SetNumber(_row("segend_pengali_komisi")) * SetNumber(_row("segend_su_sales"))


                If segen_periode.GetColumnValue("seperiode_bonus_gen") = "Y" Then

                    _row("segend_komisi_bulanan") = _row("segend_komisi")

                    sSQL = "SELECT  " _
                        & " sum( a.segend_komisi) as jml " _
                        & "FROM " _
                        & "  public.segend_det a " _
                        & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
                        & "WHERE " _
                        & " left( b.segen_periode,6) = '" & Microsoft.VisualBasic.Left(segen_periode.EditValue, 6) _
                        & "' and b.segen_periode <> '" & segen_periode.EditValue & "' and segend_ptnr_id=" & SetInteger(dr("ptnr_id"))


                    dt_bayar = GetTableData(sSQL)

                    For Each dr_bayar As DataRow In dt_bayar.Rows
                        _row("segend_komisi_telah_dibayar") = SetNumber(dr_bayar("jml"))
                    Next

                    _row("segend_komisi") = SetNumber(_row("segend_komisi_bulanan")) - SetNumber(_row("segend_komisi_telah_dibayar"))

                    sSQL = "SELECT  " _
                      & "SUM((sod_sales_unit * (-1) * soshipd_qty)) sod_sales_unit_total " _
                        & "FROM  " _
                       & "  public.soship_mstr " _
                       & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                       & "  inner join so_mstr on so_oid = soship_so_oid " _
                       & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                       & "  inner join pi_mstr on so_pi_id = pi_id " _
                       & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                       & "  inner join en_mstr on en_id = soship_en_id " _
                       & "  inner join si_mstr on si_id = soship_si_id " _
                       & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                       & "  inner join pt_mstr on pt_id = sod_pt_id " _
                       & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                       & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                       & "  inner join cu_mstr on cu_id = so_cu_id " _
                       & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                       & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                       & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                       & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                       & "  left outer join ar_mstr on ar_oid = ars_ar_oid " _
                       & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                       & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                       & "  inner join pl_mstr on pl_id = pt_pl_id " _
                      & "where soship_date >= " + SetDate(segen_start_date.EditValue) _
                      & " and soship_date <= " + SetDate(segen_end_date.EditValue) _
                      & " and (so_sales_person in  (select menu_id from get_all_child(" _
                      & dr("ptnr_id") & ") ))"

                    System.Windows.Forms.Application.DoEvents()
                    dr_data = master_new.PGSqlConn.GetRowInfo(sSQL)

                    _row("segend_su_group") = SetNumber(dr_data(0))


                    sSQL = "select count(*) as jml from ( SELECT  " _
                      & "distinct sales_mstr.ptnr_code as sales_code " _
                        & "FROM  " _
                       & "  public.soship_mstr " _
                       & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                       & "  inner join so_mstr on so_oid = soship_so_oid " _
                       & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                       & "  inner join pi_mstr on so_pi_id = pi_id " _
                       & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                       & "  inner join en_mstr on en_id = soship_en_id " _
                       & "  inner join si_mstr on si_id = soship_si_id " _
                       & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                       & "  inner join pt_mstr on pt_id = sod_pt_id " _
                       & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                       & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                       & "  inner join cu_mstr on cu_id = so_cu_id " _
                       & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                       & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                       & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                       & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                       & "  left outer join ar_mstr on ar_oid = ars_ar_oid " _
                       & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                       & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                       & "  inner join pl_mstr on pl_id = pt_pl_id " _
                    & "where sod_sales_unit > 0 and soshipd_qty < 0 and soship_date >= " + SetDate(segen_start_date.EditValue) _
                      & " and soship_date <= " + SetDate(segen_end_date.EditValue) _
                      & " and (so_sales_person in  (select ptnr_id from ptnr_mstr where ptnr_parent = " _
                      & dr("ptnr_id") & " ))  ) as temp"

                    'If dr("ptnr_id") = 820110542 Then
                    '    Dim cc As Integer
                    '    cc = 1
                    'End If
                    System.Windows.Forms.Application.DoEvents()
                    dr_data = master_new.PGSqlConn.GetRowInfo(sSQL)


                    _row("segend_active_sales") = SetNumber(dr_data(0))

                    sSQL = "SELECT  " _
                        & "  a.seadd_oid, " _
                        & "  a.seadd_type, " _
                        & "  a.seadd_lower_level_active, " _
                        & "  a.seadd_su_start, " _
                        & "  a.seadd_su_end, " _
                        & "  a.seadd_percent, " _
                        & "  a.seadd_pengali " _
                        & "FROM " _
                        & "  public.seadd_mstr a " _
                        & "WHERE " _
                        & "  a.seadd_type = '" & _row("segend_level") & "' " _
                        & "ORDER BY " _
                        & "a.seadd_lower_level_active desc,  a.seadd_su_start " _
                        & "  "

                    dt_add = GetTableData(sSQL)

                    For Each dr_add As DataRow In dt_add.Rows
                        If _row("segend_active_sales") >= dr_add("seadd_lower_level_active") Then
                            If _row("segend_su_group") > dr_add("seadd_su_start") And _row("segend_su_group") <= dr_add("seadd_su_end") Then
                                _row("segend_percent_add_income") = dr_add("seadd_percent")
                                _row("segend_pengali_add_income") = dr_add("seadd_pengali")
                                Exit For
                            End If
                        Else
                            If dr_add("seadd_lower_level_active") = 0 Then
                                _row("segend_percent_add_income") = dr_add("seadd_percent")
                                _row("segend_pengali_add_income") = dr_add("seadd_pengali")
                            End If
                        End If
                    Next

                    _row("segend_add_income") = SetNumber(_row("segend_percent_add_income")) * SetNumber(_row("segend_pengali_add_income")) _
                                            * SetNumber(_row("segend_su_group"))


                    sSQL = "SELECT  " _
                        & "  a.secoach_pengali " _
                        & "FROM " _
                        & "  public.secoaching_mstr a " _
                        & "WHERE " _
                        & "  a.secoach_en_id = " & SetInteger(_row("segend_en_id")) & " AND  " _
                        & "  a.secoach_type = '" & _row("segend_level") & "'"

                    dt_coach = GetTableData(sSQL)

                    For Each dr_coach As DataRow In dt_coach.Rows
                        _row("segend_pengali_coaching_income") = SetNumber(dr_coach("secoach_pengali"))
                    Next

                    If _row("segend_active_sales") > 0 Then

                        If _row("segend_level") = "ASM" Then

                            If SetNumber(_row("segend_active_sales")) > 10 Then
                                _row("segend_active_sales") = 10
                            End If

                            _row("segend_percent_coaching_income") = SetNumber(_row("segend_active_sales")) / 5
                            _row("segend_coaching_income") = SetNumber(_row("segend_active_sales")) / 5 * SetNumber(_row("segend_pengali_coaching_income"))
                        Else

                            If SetNumber(_row("segend_active_sales")) > 20 Then
                                _row("segend_active_sales") = 20
                            End If
                            _row("segend_percent_coaching_income") = SetNumber(_row("segend_active_sales")) / 10
                            _row("segend_coaching_income") = SetNumber(_row("segend_active_sales")) / 10 * SetNumber(_row("segend_pengali_coaching_income"))
                        End If

                    Else
                        _row("segend_coaching_income") = 0
                    End If


                End If
                _row("segend_total_income") = SetNumber(_row("segend_coaching_income")) + SetNumber(_row("segend_add_income")) + SetNumber(_row("segend_komisi"))
                _row("segend_payment_date") = segen_periode.GetColumnValue("seperiode_payment_date")

                ds_edit.Tables(0).Rows.Add(_row)
                System.Windows.Forms.Application.DoEvents()
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
            Box("Generate Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub psgen_periode_code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles segen_periode.EditValueChanged
        Try
            segen_start_date.EditValue = segen_periode.GetColumnValue("seperiode_start_date")
            segen_end_date.EditValue = segen_periode.GetColumnValue("seperiode_end_date")
            segen_remarks.EditValue = segen_periode.GetColumnValue("seperiode_remarks")
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
            sql = "select ptnr_id,ptnr_parent,ptnr_name,lvl_name,segend_komisi,segend_add_income, segend_coaching_income,segend_total_income  from (select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
              & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
              & " where ptnr_id in " _
              & " ( select menu_id from get_all_child(" _
              & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("segend_ptnr_id").ToString _
              & ")) or ptnr_id in (" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("segend_ptnr_id").ToString & ")) as temp  " _
               & " left outer join public.segend_det  ON (segend_ptnr_id = ptnr_id) where segend_segen_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "'"

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
                & "  a.segend_oid, " _
                & "  a.segend_segen_oid, " _
                & "  a.segend_ptnr_id, " _
                & "  a.segend_level, " _
                & "  a.segend_entity || ' ' || a.segend_nama as segend_nama_sort,a.segend_nama ,ptnr_code, " _
                & "  a.segend_active_sales, segend_nama_tax,segend_npwp,segend_status_marital,segend_amount_dependents," _
                & "  a.segend_su_sales, " _
                & "  a.segend_pengali_komisi, " _
                & "  a.segend_komisi, " _
                & "  a.segend_su_group, " _
                & "  a.segend_percent_add_income, " _
                & "  a.segend_pengali_add_income, " _
                & "  a.segend_add_income, " _
                & "  a.segend_percent_coaching_income, " _
                & "  a.segend_pengali_coaching_income, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_entity, " _
                & "  a.segend_lvl_id,segend_komisi_bulanan,segend_komisi_telah_dibayar, " _
                & "  a.segend_parent, " _
                & "  a.segend_komisi_telah_dibayar, " _
                & "  a.segend_komisi_bulanan, " _
                & "  b.segen_remarks, " _
                & "  c.ptnr_bank as bank, " _
                & "  c.ptnr_no_rek as norek, " _
                & "  c.ptnr_rek_name " _
                & "FROM " _
                & "  public.segend_det a " _
                & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
                & "  LEFT OUTER JOIN public.ptnr_mstr c ON (a.segend_ptnr_id = c.ptnr_id) " _
                & "WHERE " _
                & "  b.segen_code = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_code")) & " " _
                & "ORDER BY " _
                & "  a.segend_nama"

        '_sql = "SELECT  " _
        '    & "  a.segend_oid, " _
        '    & "  a.segend_segen_oid, " _
        '    & "  a.segend_ptnr_id, " _
        '    & "  a.segend_level, " _
        '    & "  a.segend_nama, " _
        '    & "  a.segend_active_sales, " _
        '    & "  a.segend_su_sales, " _
        '    & "  a.segend_pengali_komisi, " _
        '    & "  a.segend_komisi, " _
        '    & "  a.segend_su_group, " _
        '    & "  a.segend_percent_add_income, " _
        '    & "  a.segend_pengali_add_income, " _
        '    & "  a.segend_add_income, " _
        '    & "  a.segend_percent_coaching_income, " _
        '    & "  a.segend_pengali_coaching_income, " _
        '    & "  a.segend_coaching_income, " _
        '    & "  a.segend_total_income, " _
        '    & "  a.segend_payment_date, " _
        '    & "  a.segend_entity, " _
        '    & "  a.segend_lvl_id, " _
        '    & "  a.segend_parent, " _
        '    & "  a.segend_komisi_telah_dibayar, " _
        '    & "  a.segend_komisi_bulanan " _
        '    & "FROM " _
        '    & "  public.segend_det a " _
        '    & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
        '    & "WHERE " _
        '    & "  b.segen_code = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_code")) & " " _
        '    & "ORDER BY " _
        '    & "  a.segend_nama"


        Dim rpt As New XRSEReport
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

            '.XrLabelPeriode.Text = "PERIODE : " & Format(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_start_date"), "dd/MM/yyyy") & " - " & Format(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_end_date"), "dd/MM/yyyy")

            '.DataSource = ds
            '.DataMember = "Table"
            '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
            '.Parameters("PPosisi").Value = posisi

             Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            printTool.PreviewForm.Text = "Personal Selling SDI Report"
            'ps.Document.AutoFitToPagesWidth = 0
            'ps.Document.ScaleFactor = 0.8F
            '.PrintingSystem = ps
            printTool.ShowPreview()
            .TreeList1.ExpandAll()

        End With
    End Sub
End Class
