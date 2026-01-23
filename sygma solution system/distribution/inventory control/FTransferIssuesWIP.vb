Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraExport

Public Class FTransferIssuesWIP
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial, ds_comment As DataSet
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _so_oid, _pb_oid As String
    Dim _conf_value As String

    Private Sub FTransferIssues_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        wimtr_en_id.Properties.DataSource = dt_bantu
        wimtr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        wimtr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        wimtr_en_id.ItemIndex = 0

      
    End Sub

    Private Sub wimtr_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wimtr_en_id.EditValueChanged

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wc_mstr(wimtr_en_id.EditValue))
        wimtr_wc_id.Properties.DataSource = dt_bantu
        wimtr_wc_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
        wimtr_wc_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
        wimtr_wc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wc_mstr(wimtr_en_id.EditValue))
        wimtr_wc_to_id.Properties.DataSource = dt_bantu
        wimtr_wc_to_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
        wimtr_wc_to_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
        wimtr_wc_to_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", wimtr_en_id.EditValue))
        wimtr_ptnr_id.Properties.DataSource = dt_bantu
        wimtr_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        wimtr_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        wimtr_ptnr_id.ItemIndex = 0


    End Sub


    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transfer Issue Number", "wimtr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Date", "wimtr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center From", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center To", "wc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "wimtr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO Number", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Vendor Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Vendor Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "wimtr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wimtr_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "wimtr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wimtr_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "wimtrd_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "wimtrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost", "wimtrd_cost", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "wimtrd_oid", False)
        add_column(gv_edit, "wimtrd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "wimtrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "wimtrd_um", False)
        add_column(gv_edit, "UM", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "wimtrd_cost", False)
        add_column(gv_edit, "pbd_oid", False)

       
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.wimtr_oid, " _
                & "  a.wimtr_en_id, " _
                & "  b.en_desc, " _
                & "  a.wimtr_add_by, " _
                & "  a.wimtr_add_date, " _
                & "  a.wimtr_upd_by, " _
                & "  a.wimtr_upd_date, " _
                & "  a.wimtr_code, " _
                & "  a.wimtr_date, " _
                & "  a.wimtr_wc_id, " _
                & "  c.wc_desc, " _
                & "  a.wimtr_wc_to_id, " _
                & "  e.wc_desc as wc_desc_to, " _
                & "  a.wimtr_remarks, " _
                & "  a.wimtr_trans_id, " _
                & "  a.wimtr_dt, " _
                & "  a.wimtr_wo_oid, " _
                & "  f.wo_code, " _
                & "  a.wimtr_ptnr_id, " _
                & "  d.ptnr_code, " _
                & "  d.ptnr_name " _
                & "FROM " _
                & "  public.wimtr_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.wimtr_en_id = b.en_id) " _
                & "  INNER JOIN public.wc_mstr c ON (a.wimtr_wc_id = c.wc_id) " _
                & "  INNER JOIN public.wc_mstr e ON (a.wimtr_wc_to_id = e.wc_id) " _
                & "  INNER JOIN public.wo_mstr f ON (a.wimtr_wo_oid = f.wo_oid) " _
                & "  INNER JOIN public.ptnr_mstr d ON (a.wimtr_ptnr_id = d.ptnr_id) " _
                & "WHERE " _
                & "  a.wimtr_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) _
                & "  and  " & SetDate(pr_txttglakhir.DateTime.Date) _
                & " and wimtr_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " ORDER By wimtr_code"

        Return get_sequel
    End Function

   
    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            gc_detail.DataSource = Nothing
            Exit Sub
        End If

        Dim sql As String

        sql = "SELECT  " _
            & "  a.wimtrd_oid, " _
            & "  a.wimtrd_wimtr_oid, " _
            & "  a.wimtrd_seq, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.wimtrd_pt_id, " _
            & "  a.wimtrd_qty, " _
            & "  a.wimtrd_um, " _
            & "  c.code_code AS um_desc, " _
            & "  a.wimtrd_cost, " _
            & "  a.wimtrd_dt " _
            & "FROM " _
            & "  public.wimtrd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.wimtrd_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (a.wimtrd_um = c.code_id) " _
            & "WHERE " _
            & "  a.wimtrd_wimtr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wimtr_oid").ToString & "'"
        gc_detail.DataSource = GetTableData(sql)
        gv_detail.BestFitColumns()


    End Sub


    Public Overrides Sub insert_data_awal()
        wimtr_date.DateTime = _now
        wimtr_remarks.Text = ""
        wimtr_date.Focus()
        wimtr_wo_oid.Text = ""
        wimtr_wo_oid.Tag = ""
        wimtr_wo_oid.Enabled = True
        wimtr_ptnr_id.ItemIndex = 0
        wimtr_en_id.ItemIndex = 0
        wimtr_wc_id.ItemIndex = 0
        wimtr_wc_to_id.ItemIndex = 0

        gc_edit.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        _so_oid = ""
        _pb_oid = ""
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.wimtrd_oid, " _
                        & "  a.wimtrd_wimtr_oid, " _
                        & "  a.wimtrd_seq, " _
                        & "  b.pt_code, " _
                        & "  b.pt_desc1, " _
                        & "  b.pt_desc2, " _
                        & "  a.wimtrd_pt_id, " _
                        & "  a.wimtrd_qty, " _
                        & "  a.wimtrd_um, " _
                        & "  c.code_code AS um_desc, " _
                        & "  a.wimtrd_cost, " _
                        & "  a.wimtrd_dt " _
                        & "FROM " _
                        & "  public.wimtrd_det a " _
                        & "  INNER JOIN public.pt_mstr b ON (a.wimtrd_pt_id = b.pt_id) " _
                        & "  INNER JOIN public.code_mstr c ON (a.wimtrd_um = c.code_id) " _
                        & "WHERE " _
                        & "  a.wimtrd_wimtr_oid is null"

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

    Public Overrides Function before_delete() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function delete_data() As Boolean
       
        Return False
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

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
        gv_edit.UpdateCurrentRow()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._tran_oid = wimtr_wo_oid.Tag
            frm._pt_id = wimtr_wc_id.EditValue
            frm._en_id = wimtr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub


    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()

        ds_edit.AcceptChanges()
        
        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _wimtr_oid As String = Guid.NewGuid.ToString
        Dim _wimtr_code As String

        Dim ds_bantu As New DataSet
        Dim i As Integer = 0
        _wimtr_code = func_coll.get_transaction_number("TW", wimtr_en_id.GetColumnValue("en_code"), "wimtr_mstr", "wimtr_code")

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
                                        & "  public.wimtr_mstr " _
                                        & "( " _
                                        & "  wimtr_oid, " _
                                        & "  wimtr_dom_id, " _
                                        & "  wimtr_en_id, " _
                                        & "  wimtr_add_by, " _
                                        & "  wimtr_add_date, " _
                                        & "  wimtr_code, " _
                                        & "  wimtr_date, " _
                                        & "  wimtr_wc_id, " _
                                        & "  wimtr_wc_to_id, " _
                                        & "  wimtr_remarks, " _
                                        & "  wimtr_dt, " _
                                        & "  wimtr_wo_oid, " _
                                        & "  wimtr_ptnr_id " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_wimtr_oid) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(wimtr_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(_wimtr_code) & ",  " _
                                        & SetDate(wimtr_date.DateTime) & ",  " _
                                        & SetInteger(wimtr_wc_id.EditValue) & ",  " _
                                        & SetInteger(wimtr_wc_to_id.EditValue) & ",  " _
                                        & SetSetring(wimtr_remarks.Text) & ",  " _
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(wimtr_wo_oid.Tag) & ",  " _
                                        & SetInteger(wimtr_ptnr_id.EditValue) & "  " _
                                        & ")"


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("wimtrd_qty") <> 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                            & "  public.wimtrd_det " _
                                            & "( " _
                                            & "  wimtrd_oid, " _
                                            & "  wimtrd_wimtr_oid, " _
                                            & "  wimtrd_seq, " _
                                            & "  wimtrd_pt_id, " _
                                            & "  wimtrd_qty, " _
                                            & "  wimtrd_um, " _
                                            & "  wimtrd_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_wimtr_oid) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("wimtrd_pt_id")) & ",  " _
                                            & SetDec(ds_edit.Tables(0).Rows(i).Item("wimtrd_qty")) & ",  " _
                                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("wimtrd_um")) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'update wip + disini
                                If func_coll.update_inv_wip(objinsert, wimtr_wc_to_id.EditValue, ds_edit.Tables(0).Rows(i).Item("wimtrd_pt_id"), wimtr_en_id.EditValue, wimtr_wo_oid.Tag, ds_edit.Tables(0).Rows(i).Item("wimtrd_qty")) = False Then
                                    sqlTran.Rollback()
                                    Return False
                                    Exit Try
                                End If

                                'update wip - disini
                                If func_coll.update_inv_wip(objinsert, wimtr_wc_id.EditValue, ds_edit.Tables(0).Rows(i).Item("wimtrd_pt_id"), wimtr_en_id.EditValue, wimtr_wo_oid.Tag, ds_edit.Tables(0).Rows(i).Item("wimtrd_qty") * -1) = False Then
                                    sqlTran.Rollback()
                                    Return False
                                    Exit Try
                                End If
                            End If
                        Next


                        sqlTran.Commit()
                        after_success()
                        set_row(_wimtr_oid.ToString, "wimtr_oid")
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

    Private Sub wimtr_wo_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wimtr_wo_oid.ButtonClick
        Dim frm As New FWOSearch
        frm.set_win(Me)
        frm._en_id = wimtr_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub
End Class
