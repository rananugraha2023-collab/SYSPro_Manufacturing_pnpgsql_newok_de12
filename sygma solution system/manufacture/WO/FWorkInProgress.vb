Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FWorkInProgress
    Dim ssql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Public dt_edit As New DataTable

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        wim_en_id.Properties.DataSource = dt_bantu
        wim_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        wim_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        wim_en_id.ItemIndex = 0

    End Sub

    Private Sub wim_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wim_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wc_mstr(wim_en_id.EditValue))
        wim_wc_id.Properties.DataSource = dt_bantu
        wim_wc_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
        wim_wc_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
        wim_wc_id.ItemIndex = 0

        'Exit Sub
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_unit_measure(wim_en_id.EditValue))
        wim_pt_um.Properties.DataSource = dt_bantu
        wim_pt_um.Properties.DisplayMember = dt_bantu.Columns("unit_desc").ToString
        wim_pt_um.Properties.ValueMember = dt_bantu.Columns("unit_id").ToString
        wim_pt_um.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "wim_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wim_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "wim_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "WO Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Receipt", "wim_qty_receipt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "wim_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "wim_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wim_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "wim_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wim_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "wimd_oid", False)
        add_column(gv_detail, "wimd_pt_id", False)
        add_column(gv_detail, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Pt Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Pt Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Base", "wimd_qty_base", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty", "wimd_qty_issue", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "wimd_pt_um", False)
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "wimd_wc_id", False)
        add_column_copy(gv_detail, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remark", "wimd_remark", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_wip, "wimd_oid", False)
        add_column(gv_edit_wip, "wimd_pt_id", False)
        add_column(gv_edit_wip, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_wip, "Pt Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_wip, "Pt Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_wip, "Qty Base", "wimd_qty_base", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_wip, "Qty", "wimd_qty_issue", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_wip, "wimd_pt_um", False)
        add_column(gv_edit_wip, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_wip, "wimd_wc_id", False)
        add_column(gv_edit_wip, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_wip, "Remark", "wimd_remark", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.wim_oid, " _
            & "  a.wim_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  a.wim_add_by, " _
            & "  a.wim_add_date, " _
            & "  a.wim_upd_by, " _
            & "  a.wim_upd_date, " _
            & "  a.wim_code, " _
            & "  a.wim_date, " _
            & "  a.wim_wo_oid, " _
            & "  d.wo_code, " _
            & "  a.wim_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.wim_pt_um, " _
            & "  c.code_code as um_desc, " _
            & "  a.wim_qty_receipt, " _
            & "  a.wim_remark, " _
            & "  a.wim_wc_id, " _
            & "  public.wc_mstr.wc_desc " _
            & "FROM " _
            & "  public.wim_mstr a " _
            & "  INNER JOIN public.pt_mstr b ON (a.wim_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (a.wim_pt_um = c.code_id) " _
            & "  INNER JOIN public.wo_mstr d ON (a.wim_wo_oid = d.wo_oid) " _
            & "  INNER JOIN public.en_mstr ON (a.wim_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.wc_mstr ON (a.wim_wc_id = public.wc_mstr.wc_id) " _
            & "WHERE " _
            & "  a.wim_date BETWEEN  " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " AND a.wim_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & " ORDER BY wo_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        wim_en_id.ItemIndex = 0
        wim_code.EditValue = ""
        wim_date.DateTime = CekTanggal()
        wim_wc_id.ItemIndex = 0
        wim_wo_oid.EditValue = ""
        wim_wo_oid.Tag = ""
        wim_remark.EditValue = ""
        wim_qty_receipt.EditValue = 0
        pt_desc1.Text = ""
        pt_desc2.Text = ""
        wim_pt_id.Text = ""
        wim_pt_id.Tag = ""
        wim_pt_um.ItemIndex = 0

        ssql = "SELECT  " _
            & "  a.wimd_oid, " _
            & "  a.wimd_add_by, " _
            & "  a.wimd_add_date, " _
            & "  a.wimd_upd_by, " _
            & "  a.wimd_upd_date, " _
            & "  a.wimd_dt, " _
            & "  a.wimd_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2,wimd_qty_base, " _
            & "  a.wimd_pt_um, " _
            & "  c.code_code as um_desc, " _
            & "  a.wimd_wc_id, " _
            & "  d.wc_desc, " _
            & "  a.wimd_qty_issue, " _
            & "  a.wimd_remark, " _
            & "  a.wimd_wim_oid " _
            & "FROM " _
            & "  public.wimd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.wimd_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (a.wimd_pt_um = c.code_id) " _
            & "  INNER JOIN public.wc_mstr d ON (a.wimd_wc_id = d.wc_id) " _
            & "  WHERE  " _
            & "  wimd_wim_oid is null"

        dt_edit = GetTableData(ssql)
        gc_edit_wip.DataSource = dt_edit
        gv_edit_wip.BestFitColumns()

        Try

            tcg_header.SelectedTabPageIndex = 0
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _wim_code As String = ""
        Dim _wim_oid As String = Guid.NewGuid.ToString
        Dim ssqls As New ArrayList

        _wim_code = func_coll.get_transaction_number("WP", wim_en_id.GetColumnValue("en_code"), "wim_mstr", "wim_code")

        wim_code.EditValue = _wim_code

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
                                        & "  public.wim_mstr " _
                                        & "( " _
                                        & "  wim_oid, " _
                                        & "  wim_dom_id, " _
                                        & "  wim_en_id, " _
                                        & "  wim_add_by, " _
                                        & "  wim_add_date, " _
                                        & "  wim_dt, " _
                                        & "  wim_code, " _
                                        & "  wim_date, " _
                                        & "  wim_wo_oid, " _
                                        & "  wim_pt_id, " _
                                        & "  wim_pt_um, " _
                                        & "  wim_qty_receipt, " _
                                        & "  wim_remark, " _
                                        & "  wim_wc_id " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_wim_oid) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(wim_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(CekTanggal()) & ",  " _
                                        & SetDateNTime(CekTanggal()) & ",  " _
                                        & SetSetring(_wim_code) & ",  " _
                                        & SetDate(wim_date.DateTime) & ",  " _
                                        & SetSetring(wim_wo_oid.Tag) & ",  " _
                                        & SetInteger(wim_pt_id.Tag) & ",  " _
                                        & SetInteger(wim_pt_um.EditValue) & ",  " _
                                        & SetDec(wim_qty_receipt.EditValue) & ",  " _
                                        & SetSetring(wim_remark.Text) & ",  " _
                                        & SetInteger(wim_wc_id.EditValue) & "  " _
                                        & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update ke WIP
                        If func_coll.update_inv_wip(objinsert, wim_wc_id.EditValue, wim_pt_id.Tag, wim_en_id.EditValue, wim_wo_oid.Tag, wim_qty_receipt.EditValue) = False Then
                            sqlTran.Rollback()
                            Return False
                            Exit Try
                        End If

                        For Each dr As DataRow In dt_edit.Rows
                            ssql = "INSERT INTO  " _
                                & "  public.wimd_det " _
                                & "( " _
                                & "  wimd_oid, " _
                                & "  wimd_wim_oid, " _
                                & "  wimd_add_by, " _
                                & "  wimd_add_date, " _
                                & "  wimd_pt_id, " _
                                & "  wimd_pt_um, " _
                                & "  wimd_wc_id, " _
                                & "  wimd_qty_issue, " _
                                & "  wimd_remark, " _
                                & "  wimd_qty_base " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_wim_oid) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(CekTanggal) & ",  " _
                                & SetInteger(dr("wimd_pt_id")) & ",  " _
                                & SetInteger(dr("wimd_pt_um")) & ",  " _
                                & SetInteger(dr("wimd_wc_id")) & ",  " _
                                & SetDec(dr("wimd_qty_issue")) & ",  " _
                                & SetSetring(dr("wimd_remark")) & ",  " _
                                & SetDec(dr("wimd_qty_base")) & "  " _
                                & ")"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'sqlTran.Rollback()
                            'Return False
                            'Exit Try

                            'update - ke wip
                            If func_coll.update_inv_wip(objinsert, dr("wimd_wc_id"), dr("wimd_pt_id"), wim_en_id.EditValue, wim_wo_oid.Tag, dr("wimd_qty_issue") * -1) = False Then
                                sqlTran.Rollback()
                                Return False
                                Exit Try
                            End If
                        Next

                        sqlTran.Commit()
                        after_success()
                        set_row(_wim_oid, "wim_oid")
                        show_detail()
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


    Public Sub show_detail()
        Try
            If ds.Tables.Count = 0 Then
                gc_detail.DataSource = Nothing
                Exit Sub
            End If
            If ds.Tables(0).Rows.Count = 0 Then
                gc_detail.DataSource = Nothing
                Exit Sub
            End If
            Dim sSQL As String
            sSQL = "SELECT  " _
                & "  a.wimd_oid, " _
                & "  a.wimd_add_by, " _
                & "  a.wimd_add_date, " _
                & "  a.wimd_upd_by, " _
                & "  a.wimd_upd_date, " _
                & "  a.wimd_dt, " _
                & "  a.wimd_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.wimd_pt_um, " _
                & "  c.code_code as um_desc, " _
                & "  a.wimd_wc_id, " _
                & "  d.wc_desc,wimd_qty_base, " _
                & "  a.wimd_qty_issue, " _
                & "  a.wimd_remark, " _
                & "  a.wimd_wim_oid " _
                & "FROM " _
                & "  public.wimd_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.wimd_pt_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (a.wimd_pt_um = c.code_id) " _
                & "  INNER JOIN public.wc_mstr d ON (a.wimd_wc_id = d.wc_id) " _
                & "  WHERE  " _
                & "  wimd_wim_oid =" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wim_oid"))

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            gc_detail.DataSource = dt
            gv_detail.BestFitColumns()

            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function edit()
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

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            ssql = "delete from wim_mstr where wim_oid = '" + ds.Tables(0).Rows(row).Item("wim_oid").ToString + "'"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'update ke WIP
                            If func_coll.update_inv_wip(objinsert, ds.Tables(0).Rows(row).Item("wim_wc_id"), ds.Tables(0).Rows(row).Item("wim_pt_id"), _
                            ds.Tables(0).Rows(row).Item("wim_en_id"), ds.Tables(0).Rows(row).Item("wim_wo_oid").ToString, ds.Tables(0).Rows(row).Item("wim_qty_receipt") * -1) = False Then
                                sqlTran.Rollback()
                                Exit Try
                            End If


                            ssql = "SELECT  " _
                                & "  a.wimd_oid, " _
                                & "  a.wimd_add_by, " _
                                & "  a.wimd_add_date, " _
                                & "  a.wimd_upd_by, " _
                                & "  a.wimd_upd_date, " _
                                & "  a.wimd_dt, " _
                                & "  a.wimd_pt_id, " _
                                & "  b.pt_code, " _
                                & "  b.pt_desc1, " _
                                & "  b.pt_desc2, " _
                                & "  a.wimd_pt_um, " _
                                & "  c.code_code as um_desc, " _
                                & "  a.wimd_wc_id, " _
                                & "  d.wc_desc,wimd_qty_base, " _
                                & "  a.wimd_qty_issue, " _
                                & "  a.wimd_remark, " _
                                & "  a.wimd_wim_oid " _
                                & "FROM " _
                                & "  public.wimd_det a " _
                                & "  INNER JOIN public.pt_mstr b ON (a.wimd_pt_id = b.pt_id) " _
                                & "  INNER JOIN public.code_mstr c ON (a.wimd_pt_um = c.code_id) " _
                                & "  INNER JOIN public.wc_mstr d ON (a.wimd_wc_id = d.wc_id) " _
                                & "  WHERE  " _
                                & "  wimd_wim_oid =" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wim_oid"))

                            Dim dt As New DataTable
                            dt = GetTableData(ssql)

                            'update ke wip
                            For Each dr As DataRow In dt.Rows
                                If func_coll.update_inv_wip(objinsert, dr("wimd_wc_id"), dr("wimd_pt_id"), _
                                    ds.Tables(0).Rows(row).Item("wim_en_id"), ds.Tables(0).Rows(row).Item("wim_wo_oid").ToString, dr("wimd_qty_issue")) = False Then
                                    sqlTran.Rollback()
                                    Exit Try
                                End If
                            Next

                            sqlTran.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gc_edit_wip.EmbeddedNavigator.Buttons.DoClick(gc_edit_wip.EmbeddedNavigator.Buttons.EndEdit)

        For Each dr As DataRow In dt_edit.Rows
            If SetString(dr("wimd_wc_id")) = "" Then
                Box("Work Center detail can't null")
                Return False
            End If
        Next

        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub wim_wo_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wim_wo_oid.ButtonClick
        Try

            Dim frm As New FWOSearch
            frm.set_win(Me)
            frm._en_id = wim_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        show_detail()
    End Sub

    Private Sub gv_master_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.GotFocus
        show_detail()
    End Sub


    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            show_detail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wim_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wim_pt_id.ButtonClick
        Try
            Dim frm As New FProdStrucSearch()
            frm.set_win(Me)
            frm._obj = wim_pt_id
            frm._prj_code = wim_wo_oid.Tag
            frm._en_id = wim_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtRetriveDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetriveDetail.Click
        Try
            If wim_qty_receipt.EditValue = 0 Then
                Box("Qty Receive can't blank")
                Exit Sub
            End If
            ssql = "SELECT  " _
                & "  b.pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  b.pt_um , " _
                & "  c.code_code as um_desc,a.psd_qty " _
                & "FROM " _
                & "  public.psd_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.psd_pt_bom_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "  INNER JOIN public.ps_mstr d ON (a.psd_ps_oid = d.ps_oid) " _
                & "WHERE " _
                & "  d.ps_pt_bom_id=" & wim_pt_id.Tag

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            Dim _row As DataRow
            dt_edit.Rows.Clear()
            For Each dr As DataRow In dt.Rows
                _row = dt_edit.NewRow

                _row("wimd_pt_id") = dr("pt_id")
                _row("pt_code") = dr("pt_code")
                _row("pt_desc1") = dr("pt_desc1")
                _row("pt_desc2") = dr("pt_desc2")
                _row("wimd_pt_um") = dr("pt_um")
                _row("um_desc") = dr("um_desc")
                _row("wimd_qty_base") = dr("psd_qty")
                _row("wimd_qty_issue") = dr("psd_qty") * wim_qty_receipt.EditValue
                dt_edit.Rows.Add(_row)

            Next
            gv_edit_wip.BestFitColumns()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_wip_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_wip.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub browse_data()
        Dim _col As String = gv_edit_wip.FocusedColumn.Name
        Dim _row As Integer = gv_edit_wip.FocusedRowHandle

        If _col = "wc_desc" Then
            Dim frm As New FWCSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = wim_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            If wim_qty_receipt.EditValue = 0 Then
                Box("Qty Receive can't blank")
                Exit Sub
            End If
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = wim_en_id.EditValue
            frm._qty_receive = wim_qty_receipt.EditValue
            frm._pt_id = wim_pt_id.Tag
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        For Each dr As DataRow In dt_edit.Rows
            dr("wimd_wc_id") = gv_edit_wip.GetFocusedRowCellValue("wimd_wc_id")
            dr("wc_desc") = gv_edit_wip.GetFocusedRowCellValue("wc_desc")
        Next
    End Sub
End Class
