Imports npgsql
Imports master_new.ModFunction

Public Class FPriceListOri

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pi_oid_mstr As String
    Dim _now As DateTime
    Public ds_edit_item, ds_edit_rule As DataSet

    Private Sub FPriceList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        AddHandler gv_edit_item.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_edit_item.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pi_en_id.Properties.DataSource = dt_bantu
        pi_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pi_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pi_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_so_type())
        pi_so_type.Properties.DataSource = dt_bantu
        pi_so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        pi_so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        pi_so_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        pi_cu_id.Properties.DataSource = dt_bantu
        pi_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        pi_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        pi_cu_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", pi_en_id.EditValue))
        pi_ptnrg_id.Properties.DataSource = dt_bantu
        pi_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        pi_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        pi_ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_promo_mstr(pi_en_id.EditValue))
        pi_promo_id.Properties.DataSource = dt_bantu
        pi_promo_id.Properties.DisplayMember = dt_bantu.Columns("promo_desc").ToString
        pi_promo_id.Properties.ValueMember = dt_bantu.Columns("promo_id").ToString
        pi_promo_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_sales_program(pi_en_id.EditValue))
        'pi_sales_program.Properties.DataSource = dt_bantu
        'pi_sales_program.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'pi_sales_program.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'pi_sales_program.ItemIndex = 0


        dt_bantu = New DataTable
        dt_bantu = func_data.load_sales_program()

        With pi_sales_program
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("sls_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("sls_code").ToString
            .ItemIndex = 0
        End With

    End Sub

    Private Sub pi_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pi_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Type", "pi_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Programe", "sls_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Start Date", "pi_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "pi_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "pi_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "pi_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pi_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pi_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pi_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_item, "pid_pi_oid", False)
        add_column_copy(gv_detail_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_rule, "pid_pi_oid", False)
        add_column_copy(gv_detail_rule, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_rule, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_rule, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_rule, "Discount", "pidd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_rule, "Prepayment", "pidd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_rule, "Interval", "pidd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_detail_rule, "Payment", "pidd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_rule, "Min. Qty", "pidd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_rule, "Sales Unit", "pidd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit_item, "pid_oid", False)
        add_column(gv_edit_item, "pid_pt_id", False)
        add_column(gv_edit_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_rule, "pidd_oid", False)
        add_column(gv_edit_rule, "pidd_pid_oid", False)
        add_column(gv_edit_rule, "pidd_payment_type", False)
        add_column(gv_edit_rule, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_rule, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_rule, "Discount", "pidd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit_rule, "Prepayment", "pidd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_rule, "Interval", "pidd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_edit(gv_edit_rule, "Payment", "pidd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_rule, "Min. Qty", "pidd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_rule, "Sales Unit", "pidd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  pi_oid, " _
                    & "  pi_dom_id, " _
                    & "  pi_en_id, " _
                    & "  en_desc, " _
                    & "  pi_add_by, " _
                    & "  pi_add_date, " _
                    & "  pi_upd_by, " _
                    & "  pi_upd_date, " _
                    & "  pi_id, " _
                    & "  pi_code, " _
                    & "  pi_desc, " _
                    & "  pi_so_type, " _
                    & "  pi_promo_id, " _
                    & "  promo_desc, " _
                    & "  pi_cu_id, " _
                    & "  cu_name, " _
                    & "  pi_sales_program, " _
                    & "  pi_sls_program,sls_name, " _
                    & "  pi_start_date, " _
                    & "  pi_end_date, " _
                    & "  pi_active, " _
                    & "  pi_dt,ptnrg_name " _
                    & "FROM  " _
                    & "  public.pi_mstr " _
                    & " inner join en_mstr on en_id = pi_en_id " _
                    & " inner join cu_mstr on cu_id = pi_cu_id " _
                    & " inner join promo_mstr on promo_id = pi_promo_id " _
                    & " left outer join sls_program on pi_sls_program = sls_code " _
                    & "  left outer JOIN public.ptnrg_grp ON ptnrg_id = pi_ptnrg_id " _
                    & " where pi_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String = ""

        Try
            ds.Tables("detail_item").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  pid_oid, " _
            & "  pid_add_by, " _
            & "  pid_add_date, " _
            & "  pid_upd_date, " _
            & "  pid_upd_by, " _
            & "  pid_pi_oid, " _
            & "  pid_pt_id, " _
            & "  pt_code, pt_desc1, pt_desc2, " _
            & "  pid_dt " _
            & "FROM  " _
            & "  public.pid_det " _
            & " inner join pt_mstr on pt_id = pid_pt_id "

        load_data_detail(sql, gc_detail_item, "detail_item")

        Try
            ds.Tables("detail_rule").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  pidd_oid, " _
            & "  pidd_add_by, " _
            & "  pidd_add_date, " _
            & "  pidd_upd_date, " _
            & "  pidd_upd_by, " _
            & "  pidd_pid_oid, " _
            & "  pidd_payment_type, " _
            & "  code_name as payment_type_name, " _
            & "  pidd_price, " _
            & "  pidd_disc, " _
            & "  pidd_dp, " _
            & "  pidd_interval, " _
            & "  pidd_payment, " _
            & "  pidd_min_qty, " _
            & "  pidd_sales_unit, " _
            & "  pid_pi_oid, " _
            & "  pt_code, " _
            & "  pidd_dt " _
            & "FROM  " _
            & "  public.pidd_det " _
            & "  inner join public.pid_det on pidd_pid_oid = pid_oid " _
            & "  inner join public.pt_mstr on pt_id = pid_pt_id " _
            & "  inner join public.code_mstr on code_id = pidd_payment_type "

        load_data_detail(sql, gc_detail_rule, "detail_rule")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_item.Columns("pid_pi_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pid_pi_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid").ToString & "'")
            gv_detail_item.BestFitColumns()

            gv_detail_rule.Columns("pid_pi_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pid_pi_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid").ToString & "'")
            gv_detail_rule.BestFitColumns()
        Catch ex As Exception
        End Try

        Try
            gv_edit_rule.Columns("pidd_pid_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pidd_pid_oid='" & ds_edit_item.Tables(0).Rows(BindingContext(ds_edit_item.Tables(0)).Position).Item("pid_oid").ToString & "'")
            gv_edit_rule.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        pi_en_id.Focus()
        pi_en_id.ItemIndex = 0
        pi_code.Text = ""
        pi_desc.Text = ""
        pi_so_type.ItemIndex = 0
        pi_promo_id.ItemIndex = 0
        pi_cu_id.ItemIndex = 0
        pi_sales_program.ItemIndex = 0
        pi_start_date.DateTime = _now
        pi_end_date.DateTime = _now
        pi_active.EditValue = True
        pi_ptnrg_id.ItemIndex = 0

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_item = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  pid_oid, " _
                        & "  pid_add_by, " _
                        & "  pid_add_date, " _
                        & "  pid_upd_date, " _
                        & "  pid_upd_by, " _
                        & "  pid_pi_oid, " _
                        & "  pid_pt_id, " _
                        & "  pt_code, pt_desc1, pt_desc2, " _
                        & "  pid_dt " _
                        & "FROM  " _
                        & "  public.pid_det " _
                        & " inner join pt_mstr on pt_id = pid_pt_id " _
                        & "  where pid_pt_id = -99 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit_item, "item")
                    gc_edit_item.DataSource = ds_edit_item.Tables(0)
                    gv_edit_item.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_rule = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  pidd_oid, " _
                        & "  pidd_add_by, " _
                        & "  pidd_add_date, " _
                        & "  pidd_upd_date, " _
                        & "  pidd_upd_by, " _
                        & "  pidd_pid_oid, " _
                        & "  pidd_payment_type, " _
                        & "  code_name as payment_type_name, " _
                        & "  pidd_price, " _
                        & "  pidd_disc, " _
                        & "  pidd_dp, " _
                        & "  pidd_interval, " _
                        & "  pidd_payment, " _
                        & "  pidd_min_qty, " _
                        & "  pidd_sales_unit, " _
                        & "  pidd_dt " _
                        & "FROM  " _
                        & "  public.pidd_det " _
                        & "  inner join public.code_mstr on code_id = pidd_payment_type " _
                        & " where pidd_price = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_rule, "rule")
                    gc_edit_rule.DataSource = ds_edit_rule.Tables(0)
                    gv_edit_rule.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Sub gv_edit_item_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_item.DoubleClick
        Dim _col As String = gv_edit_item.FocusedColumn.Name
        Dim _row As Integer = gv_edit_item.FocusedRowHandle

        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pi_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_item_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_item.InitNewRow
        With gv_edit_item
            .SetRowCellValue(e.RowHandle, "pid_oid", Guid.NewGuid.ToString)
        End With
    End Sub

    Private Sub gv_edit_rule_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_rule.DoubleClick
        Dim _col As String = gv_edit_rule.FocusedColumn.Name
        Dim _row As Integer = gv_edit_rule.FocusedRowHandle

        If _col = "payment_type_name" Then
            Dim frm As New FPaymentTypeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pi_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_rule_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_rule.InitNewRow
        With gv_edit_rule
            .SetRowCellValue(e.RowHandle, "pidd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "pidd_pid_oid", ds_edit_item.Tables(0).Rows(BindingContext(ds_edit_item.Tables(0)).Position).Item("pid_oid").ToString)
            .SetRowCellValue(e.RowHandle, "pidd_price", 0)
            .SetRowCellValue(e.RowHandle, "pidd_disc", 0)
            .SetRowCellValue(e.RowHandle, "pidd_dp", 0)
        End With
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit_item.UpdateCurrentRow()
        gv_edit_rule.UpdateCurrentRow()

        ds_edit_item.AcceptChanges()
        ds_edit_rule.AcceptChanges()

        'Ini Diperbolehkan kosong...,kalau kosong artinya berlaku untuk semua barang
        'If ds_edit_item.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If ds_edit_rule.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'Dim i As Integer

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _pi_oid As Guid = Guid.NewGuid
        Dim i As Integer
        Dim ssqls As New ArrayList

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
                                            & "  public.pi_mstr " _
                                            & "( " _
                                            & "  pi_oid, " _
                                            & "  pi_dom_id, " _
                                            & "  pi_en_id, " _
                                            & "  pi_add_by, " _
                                            & "  pi_add_date, " _
                                            & "  pi_id, " _
                                            & "  pi_code, " _
                                            & "  pi_desc, " _
                                            & "  pi_so_type, " _
                                            & "  pi_promo_id, " _
                                            & "  pi_cu_id, " _
                                            & "  pi_sls_program, " _
                                            & "  pi_start_date, " _
                                            & "  pi_end_date,pi_ptnrg_id, " _
                                            & "  pi_active, " _
                                            & "  pi_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pi_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pi_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("pi_mstr", pi_en_id.GetColumnValue("en_code"), "pi_id", "pi_en_id", pi_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(pi_code.Text) & ",  " _
                                            & SetSetring(pi_desc.Text) & ",  " _
                                            & SetSetring(pi_so_type.EditValue) & ",  " _
                                            & SetInteger(pi_promo_id.EditValue) & ",  " _
                                            & SetInteger(pi_cu_id.EditValue) & ",  " _
                                            & SetSetring(pi_sales_program.EditValue) & ",  " _
                                            & SetDate(pi_start_date.DateTime) & ",  " _
                                             & SetInteger(pi_ptnrg_id.EditValue) & ",  " _
                                            & SetDate(pi_end_date.DateTime) & ",  " _
                                            & SetBitYN(pi_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pid_det " _
                                                & "( " _
                                                & "  pid_oid, " _
                                                & "  pid_add_by, " _
                                                & "  pid_add_date, " _
                                                & "  pid_pi_oid, " _
                                                & "  pid_pt_id, " _
                                                & "  pid_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("pid_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_pi_oid.ToString) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("pid_pt_id").ToString) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Update data rule
                        For i = 0 To ds_edit_rule.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pidd_det " _
                                                & "( " _
                                                & "  pidd_oid, " _
                                                & "  pidd_add_by, " _
                                                & "  pidd_add_date, " _
                                                & "  pidd_pid_oid, " _
                                                & "  pidd_payment_type, " _
                                                & "  pidd_price, " _
                                                & "  pidd_disc, " _
                                                & "  pidd_dp, " _
                                                & "  pidd_interval, " _
                                                & "  pidd_payment, " _
                                                & "  pidd_min_qty, " _
                                                & "  pidd_sales_unit, " _
                                                & "  pidd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_rule.Tables(0).Rows(i).Item("pidd_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(ds_edit_rule.Tables(0).Rows(i).Item("pidd_pid_oid").ToString) & ",  " _
                                                & SetInteger(ds_edit_rule.Tables(0).Rows(i).Item("pidd_payment_type")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_price")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_disc")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_dp")) & ",  " _
                                                & SetDblDB(0) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_payment")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_min_qty")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_sales_unit")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
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
                        set_row(_pi_oid.ToString, "pi_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from pi_mstr where pi_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid").ToString + "'"
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pi_oid_mstr = .Item("pi_oid").ToString
                pi_en_id.EditValue = .Item("pi_en_id")
                pi_code.Text = .Item("pi_code")
                pi_desc.Text = SetString(.Item("pi_desc"))
                pi_so_type.EditValue = .Item("pi_so_type")
                pi_promo_id.EditValue = .Item("pi_promo_id")
                pi_cu_id.EditValue = .Item("pi_cu_id")
                pi_sales_program.EditValue = .Item("pi_sales_program")
                pi_start_date.DateTime = .Item("pi_start_date")
                pi_end_date.DateTime = .Item("pi_end_date")
                pi_active.EditValue = SetBitYNB(.Item("pi_active"))
                pi_ptnrg_id.EditValue = .Item("pi_ptnrg_id")
            End With

            pi_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_item = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pid_oid, " _
                            & "  pid_add_by, " _
                            & "  pid_add_date, " _
                            & "  pid_upd_date, " _
                            & "  pid_upd_by, " _
                            & "  pid_pi_oid, " _
                            & "  pid_pt_id, " _
                            & "  pt_code, pt_desc1, pt_desc2, " _
                            & "  pid_dt " _
                            & "FROM  " _
                            & "  public.pid_det " _
                            & " inner join pt_mstr on pt_id = pid_pt_id " _
                            & " inner join pi_mstr on pi_oid = pid_pi_oid " _
                            & "  where pi_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_item, "pid_det")
                        gc_edit_item.DataSource = ds_edit_item.Tables(0)
                        gv_edit_item.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_rule = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pidd_oid, " _
                            & "  pidd_add_by, " _
                            & "  pidd_add_date, " _
                            & "  pidd_upd_date, " _
                            & "  pidd_upd_by, " _
                            & "  pidd_pid_oid, " _
                            & "  pidd_payment_type, " _
                            & "  code_name as payment_type_name, " _
                            & "  pidd_price, " _
                            & "  pidd_disc, " _
                            & "  pidd_dp, " _
                            & "  pidd_interval, " _
                            & "  pidd_payment, " _
                            & "  pidd_min_qty, " _
                            & "  pidd_sales_unit, " _
                            & "  pidd_dt, pt_code, pt_desc1, pt_desc2 " _
                            & "FROM  " _
                            & "  public.pidd_det " _
                            & "  inner join public.code_mstr on code_id = pidd_payment_type " _
                            & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                            & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                            & "  inner join public.pt_mstr on pt_id = pid_pt_id " _
                            & "  where pi_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_rule, "rule")
                        gc_edit_rule.DataSource = ds_edit_rule.Tables(0)
                        gv_edit_rule.BestFitColumns()
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
        Dim i As Integer
        Dim ssqls As New ArrayList

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
                                            & "  public.pi_mstr   " _
                                            & "SET  " _
                                            & "  pi_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pi_en_id = " & SetInteger(pi_en_id.EditValue) & ",  " _
                                            & "  pi_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pi_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  pi_code = " & SetSetring(pi_code.Text) & ",  " _
                                            & "  pi_desc = " & SetSetring(pi_desc.Text) & ",  " _
                                            & "  pi_so_type = " & SetSetring(pi_so_type.EditValue) & ",  " _
                                            & "  pi_promo_id = " & SetInteger(pi_promo_id.EditValue) & ",  " _
                                             & "  pi_ptnrg_id = " & SetInteger(pi_ptnrg_id.EditValue) & ",  " _
                                            & "  pi_cu_id = " & SetInteger(pi_cu_id.EditValue) & ",  " _
                                            & "  pi_sales_program = " & SetInteger(pi_sales_program.EditValue) & ",  " _
                                            & "  pi_start_date = " & SetDate(pi_start_date.DateTime) & ",  " _
                                            & "  pi_end_date = " & SetDate(pi_end_date.DateTime) & ",  " _
                                            & "  pi_active = " & SetBitYN(pi_active.EditValue) & ",  " _
                                            & "  pi_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pi_oid = " & SetSetring(_pi_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pid_det where pid_pi_oid = '" + _pi_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pid_det " _
                                                & "( " _
                                                & "  pid_oid, " _
                                                & "  pid_add_by, " _
                                                & "  pid_add_date, " _
                                                & "  pid_pi_oid, " _
                                                & "  pid_pt_id, " _
                                                & "  pid_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("pid_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_pi_oid_mstr) & ",  " _
                                                & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("pid_pt_id").ToString) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Update data rule
                        For i = 0 To ds_edit_rule.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pidd_det " _
                                                & "( " _
                                                & "  pidd_oid, " _
                                                & "  pidd_add_by, " _
                                                & "  pidd_add_date, " _
                                                & "  pidd_pid_oid, " _
                                                & "  pidd_payment_type, " _
                                                & "  pidd_price, " _
                                                & "  pidd_disc, " _
                                                & "  pidd_dp, " _
                                                & "  pidd_interval, " _
                                                & "  pidd_payment, " _
                                                & "  pidd_min_qty, " _
                                                & "  pidd_sales_unit, " _
                                                & "  pidd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_rule.Tables(0).Rows(i).Item("pidd_oid").ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(ds_edit_rule.Tables(0).Rows(i).Item("pidd_pid_oid").ToString) & ",  " _
                                                & SetInteger(ds_edit_rule.Tables(0).Rows(i).Item("pidd_payment_type")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_price")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_disc")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_dp")) & ",  " _
                                                & SetDblDB(0) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_payment")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_min_qty")) & ",  " _
                                                & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_sales_unit")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
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
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_pi_oid_mstr, "pi_oid")
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
End Class

