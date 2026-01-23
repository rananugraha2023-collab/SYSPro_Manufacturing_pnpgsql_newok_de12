Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FPriceListDetailByPaymentType
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Public ds_edit_item, ds_edit_rule As DataSet
    Public _pi_oid As String

    Private Sub FPriceListDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        AddHandler gv_master.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_master.ColumnFilterChanged, AddressOf relation_detail
        AddHandler gv_master.Click, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pi_en_id.Properties.DataSource = dt_bantu
        pi_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pi_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pi_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Type", "pi_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Programe", "sales_program_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "pi_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "pi_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "pi_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "pid_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pid_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pid_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pid_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_rule, "pidd_pid_oid", False)
        add_column_copy(gv_detail_rule, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_rule, "Part Number", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_rule, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_rule, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_rule, "Discount", "pidd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_rule, "Prepayment", "pidd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_rule, "Interval", "pidd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_detail_rule, "Payment", "pidd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_rule, "Min. Qty", "pidd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_rule, "Sales Unit", "pidd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_rule, "Commision", "pidd_commision", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit_item, "pid_oid", False)
        add_column(gv_edit_item, "pid_pt_id", False)
        add_column(gv_edit_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_item, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

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
        add_column_edit(gv_edit_rule, "Commision", "pidd_commision", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  pi_oid, " _
                    & "  pi_dom_id, " _
                    & "  pi_en_id, " _
                    & "  en_desc, " _
                    & "  pid_add_by, " _
                    & "  pid_add_date, " _
                    & "  pid_upd_by, " _
                    & "  pid_upd_date, " _
                    & "  pi_id, " _
                    & "  pi_code, " _
                    & "  pi_desc, " _
                    & "  pi_so_type, " _
                    & "  pi_promo_id, " _
                    & "  promo_desc, " _
                    & "  pi_cu_id, " _
                    & "  cu_name, " _
                    & "  pi_sales_program, " _
                    & "  code_name as sales_program_name, " _
                    & "  pi_start_date, " _
                    & "  pi_end_date, " _
                    & "  pi_active, " _
                    & "  pid_oid, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pi_dt " _
                    & "FROM  " _
                    & "  public.pi_mstr " _
                    & " inner join en_mstr on en_id = pi_en_id " _
                    & " inner join cu_mstr on cu_id = pi_cu_id " _
                    & " inner join promo_mstr on promo_id = pi_promo_id " _
                    & " inner join code_mstr on code_id = pi_sales_program " _
                    & " inner join public.pid_det on pid_pi_oid = pi_oid " _
                    & " inner join pt_mstr on pt_id = pid_pt_id " _
                    & " where pi_en_id in (0," & func_data.entity_parent(master_new.PGSqlConn.GetRowInfo("select en_id from tconfuser where userid=" _
                            & master_new.ClsVar.sUserID)(0)) & ") or pi_en_id in (select user_en_id from tconfuserentity " _
                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        '& " where pi_en_id in (select user_en_id from tconfuserentity " _
        '                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "

        Return get_sequel

    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String = ""

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
            & "  code_name as payment_type_name,pidd_commision, " _
            & "  pidd_price, " _
            & "  pidd_disc, " _
            & "  pidd_dp, " _
            & "  pidd_interval, " _
            & "  pidd_payment, " _
            & "  pidd_min_qty, " _
            & "  pidd_sales_unit, " _
            & "  pid_pi_oid, " _
            & "  pt_code,pt_desc1, " _
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
            gv_detail_rule.Columns("pidd_pid_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pidd_pid_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid").ToString & "'")
            gv_detail_rule.BestFitColumns()

            'gv_detail_rule.Columns("pidd_pid_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid"))
            'gv_detail_rule.BestFitColumns()
        Catch ex As Exception
        End Try


    End Sub

    Private Sub pi_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pi_code.ButtonClick
        Dim frm As New FPriceListSearch()
        frm.set_win(Me)
        frm._en_id = pi_en_id.EditValue
        frm._obj = pi_code
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub insert_data_awal()
        pi_en_id.Focus()
        pi_en_id.ItemIndex = 0
        pi_code.Text = ""
        _pi_oid = ""

        pi_en_id.Enabled = True
        pi_code.Enabled = True

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
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
                    gc_edit_item.Enabled = True 'untuk insert bisa untuk lebih dari satu barang
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
                        & "  code_name as payment_type_name,pidd_commision, " _
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
            .SetRowCellValue(e.RowHandle, "pidd_payment", 0)
            .SetRowCellValue(e.RowHandle, "pidd_sales_unit", 0)
            .SetRowCellValue(e.RowHandle, "pidd_commision", 0)
        End With
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit_item.UpdateCurrentRow()
        gv_edit_rule.UpdateCurrentRow()

        ds_edit_item.AcceptChanges()
        ds_edit_rule.AcceptChanges()

        If _pi_oid = "" Then
            MessageBox.Show("Please Define Price List Name First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

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
                                                & "  pidd_sales_unit,pidd_commision, " _
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
                                                 & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_commision")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_en_id") <> master_new.PGSqlConn.GetRowInfo("select en_id from tconfuser where userid=" _
            '                                                    & master_new.ClsVar.sUserID)(0) Then
            '    Box("Sorry you can't edit this data")
            '    Return False
            '    Exit Function
            'End If

            With ds.Tables(0).Rows(row)
                _pi_oid = .Item("pi_oid").ToString
                pi_en_id.EditValue = .Item("pi_en_id")
                pi_code.Text = .Item("pi_code")
            End With

            pi_en_id.Focus()

            pi_en_id.Enabled = False
            pi_code.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            gv_edit_rule.ClearColumnsFilter()

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
                            & "  pt_code, pt_desc1, pt_desc2,invct_cost, " _
                            & "  pid_dt " _
                            & "FROM  " _
                            & "  public.pid_det " _
                            & " inner join pt_mstr on pt_id = pid_pt_id " _
                            & " left outer join invct_table on invct_pt_id = pt_id " _
                            & "  where pid_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_item, "pid_det")
                        gc_edit_item.DataSource = ds_edit_item.Tables(0)
                        gv_edit_item.BestFitColumns()
                        gc_edit_item.Enabled = False 'untuk edit hanya bisa untuk satu barang saja
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
                            & "  code_name as payment_type_name, pidd_commision," _
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
                            & "  where pidd_pid_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_rule, "rule")
                        'MessageBox.Show(ds_edit_rule.Tables(0).Rows.Count)
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
        Dim _pid_oid As String = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid").ToString
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pidd_det where pidd_pid_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid").ToString)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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
                                                & "  pidd_sales_unit,pidd_commision, " _
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
                                                  & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_commision")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_pid_oid, "pid_oid")
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
                            .Command.CommandText = "delete from pid_det where pid_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pid_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If MyPGDll.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
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

    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                    & "  en_desc, " _
                    & "  pid_add_by, " _
                    & "  pid_add_date, " _
                    & "  pid_upd_by, " _
                    & "  pid_upd_date, " _
                    & "  pi_id, " _
                    & "  pi_code, " _
                    & "  pi_desc, " _
                    & "  pi_so_type, " _
                    & "  pi_promo_id, " _
                    & "  promo_desc, " _
                    & "  pi_cu_id, " _
                    & "  cu_name, " _
                    & "  pi_sales_program, " _
                    & "  code_name as sales_program_name, " _
                    & "  pi_start_date, " _
                    & "  pi_end_date, " _
                    & "  pi_active, " _
                    & "  pid_oid, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pi_dt,(select max(pidd_price) from pidd_det where pidd_pid_oid = pid_oid) as pidd_price " _
                    & "FROM  " _
                    & "  public.pi_mstr " _
                    & " inner join en_mstr on en_id = pi_en_id " _
                    & " inner join cu_mstr on cu_id = pi_cu_id " _
                    & " inner join promo_mstr on promo_id = pi_promo_id " _
                    & " inner join code_mstr on code_id = pi_sales_program " _
                    & " inner join public.pid_det on pid_pi_oid = pi_oid " _
                    & " inner join pt_mstr on pt_id = pid_pt_id " _
                    & " where pi_en_id in (0," & func_data.entity_parent(master_new.PGSqlConn.GetRowInfo("select en_id from tconfuser where userid=" _
                            & master_new.ClsVar.sUserID)(0)) & ") "

            If gv_master.ActiveFilterString <> "" Then
                ssql = ssql & " and " & gv_master.ActiveFilterString.Replace("[", "").Replace("]", "").ToLower.Replace("like", "~~*")
            End If


            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Private Sub BtImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImport.Click

        Dim sSQL As String = ""


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
                    'gc_edit_item.DataSource = ds_edit_item.Tables(0)
                    'gv_edit_item.BestFitColumns()
                    'gc_edit_item.Enabled = True 'untuk insert bisa untuk lebih dari satu barang
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
                    'gc_edit_rule.DataSource = ds_edit_rule.Tables(0)
                    'gv_edit_rule.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(AskOpenFile("Excel Files | *.xls"))
            Dim dt As New DataTable
            Dim i As Integer


            Dim _dtrow As DataRow
            Dim _dtrow2 As DataRow
            ds_edit_item.Tables(0).Rows.Clear()

            Dim _ptcode As String = ""
            Dim _code As String = ""
            Dim _entity As String = ""
            Dim _pi_oid As String = ""
            Dim _pt_id As String = ""
            Dim _payment_id As String = ""

            'If ds.Tables(0).Rows.Count > 0 Then
            '    _ptcode = ds.Tables(0).Rows(0).Item("Part Number")
            'End If
            Dim _uuid As String = ""
            For i = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)

                    sSQL = "select en_id from en_mstr where en_desc='" & .Item("Entity") & "'"
                    _entity = GetRowInfo(sSQL)(0).ToString

                    sSQL = "SELECT  " _
                        & "  a.pi_oid " _
                        & "FROM " _
                        & "  public.pi_mstr a " _
                        & "WHERE " _
                        & "  a.pi_code = '" & .Item("Code") & "'"

                    _pi_oid = GetRowInfo(sSQL)(0).ToString


                    sSQL = "select pt_id from pt_mstr where pt_code='" & .Item("Part Number") & "'"
                    _pt_id = GetRowInfo(sSQL)(0).ToString

                    sSQL = "select code_id from code_mstr where code_name='" & .Item("Payment Type") & "' and code_field ~~* 'payment_type'"
                    _payment_id = GetRowInfo(sSQL)(0).ToString

                    If _code = .Item("Code") Then

                        If _ptcode = .Item("Part Number") Then

                        Else
                            _ptcode = .Item("Part Number")

                            _dtrow = ds_edit_item.Tables(0).NewRow
                            _uuid = Guid.NewGuid.ToString
                            _dtrow("pid_oid") = _uuid
                            _dtrow("pid_pi_oid") = _pi_oid
                            _dtrow("pid_pt_id") = _pt_id

                            ds_edit_item.Tables(0).Rows.Add(_dtrow)
                        End If
                        _ptcode = .Item("Part Number")
                        _code = .Item("Code")
                    Else

                        If _ptcode = .Item("Part Number") Then

                        Else
                            _ptcode = .Item("Part Number")

                            _dtrow = ds_edit_item.Tables(0).NewRow
                            _uuid = Guid.NewGuid.ToString
                            _dtrow("pid_oid") = _uuid
                            _dtrow("pid_pi_oid") = _pi_oid
                            _dtrow("pid_pt_id") = _pt_id

                            ds_edit_item.Tables(0).Rows.Add(_dtrow)
                        End If
                        _code = .Item("Code")
                        _ptcode = .Item("Part Number")
                    End If

                    _dtrow2 = ds_edit_rule.Tables(0).NewRow

                    _dtrow2("pidd_pid_oid") = _uuid
                    _dtrow2("pidd_payment_type") = _payment_id
                    _dtrow2("pidd_price") = .Item("Price")
                    _dtrow2("pidd_disc") = .Item("Discount")
                    _dtrow2("pidd_dp") = .Item("Prepayment")
                    _dtrow2("pidd_interval") = 0.0
                    _dtrow2("pidd_payment") = .Item("Payment")
                    _dtrow2("pidd_min_qty") = .Item("Min Qty")
                    _dtrow2("pidd_sales_unit") = .Item("Sales Unit")

                    ds_edit_rule.Tables(0).Rows.Add(_dtrow2)

                End With

                System.Windows.Forms.Application.DoEvents()

            Next

            ds_edit_item.AcceptChanges()
            ds_edit_rule.AcceptChanges()


            Dim ssqls As New ArrayList

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran


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
                                                    & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("pid_pi_oid").ToString) & ",  " _
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
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
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

                            If MyPGDll.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()
                            after_success()
                            set_row(_pi_oid.ToString, "pi_oid")
                            'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)

                        End Try
                    End With
                End Using
            Catch ex As Exception
                row = 0
                MessageBox.Show(ex.Message)
            End Try

            MessageBox.Show("Import Success")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
