Imports npgsql
Imports master_new.ModFunction

Public Class FAssetTransfer
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime
    Dim _astrnf_oid_mstr As String
    Dim _conf_value As String

    Private Sub FAssetTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        astrnf_en_id.Properties.DataSource = dt_bantu
        astrnf_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        astrnf_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        astrnf_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Asset Transfer Code", "astrnf_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "astrnf_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "astrnf_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "astrnf_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "astrnf_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "astrnf_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "astrnf_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "astrnfd_astrnf_oid", False)
        add_column_copy(gv_detail, "Asset Code", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Asset Model", "ass_model", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "From Employee", "xemp_name_from", DevExpress.Utils.HorzAlignment.Default)
        'ant 22 feb 2011
        add_column_copy(gv_detail, "From Location", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        '------------------
        add_column_copy(gv_detail, "From Department", "code_name_depfrom", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "From Region", "code_name_rgfrom", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "To Employee", "xemp_name_to", DevExpress.Utils.HorzAlignment.Default)
        'ant 22 feb 2011
        add_column_copy(gv_detail, "To Location", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        '------------------
        add_column_copy(gv_detail, "To Department", "code_name_depto", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "To Region", "code_name_rgto", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "astrnfd_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "astrnfd_astrnf_oid", False)
        add_column(gv_edit, "astrnfd_ass_id", False)
        add_column(gv_edit, "Asset Code", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Asset Model", "ass_model", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "astrnfd_emp_id_from", False)
        add_column(gv_edit, "From Employee", "xemp_name_from", DevExpress.Utils.HorzAlignment.Default)
        'ant 22 feb 2011
        add_column(gv_edit, "astrnfd_loc_id_from", False)
        add_column(gv_edit, "From Location", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        '------------------
        add_column(gv_edit, "astrnfd_dept_id_from", False)
        add_column(gv_edit, "From Department", "code_name_depfrom", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "astrnfd_rg_id_from", False)
        add_column(gv_edit, "From Region", "code_name_rgfrom", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "astrnfd_emp_id_to", False)
        add_column(gv_edit, "To Employee", "xemp_name_to", DevExpress.Utils.HorzAlignment.Default)
        'ant 22 feb 2011
        add_column(gv_edit, "astrnfd_loc_id_to", False)
        add_column(gv_edit, "To Location", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        '------------------
        add_column(gv_edit, "astrnfd_dept_id_to", False)
        add_column(gv_edit, "To Department", "code_name_depto", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "astrnfd_rg_id_to", False)
        add_column(gv_edit, "To Region", "code_name_rgto", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "astrnfd_rmks", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  astrnf_oid, " _
                    & "  astrnf_dom_id, " _
                    & "  astrnf_en_id, " _
                    & "  astrnf_add_by, " _
                    & "  astrnf_add_date, " _
                    & "  astrnf_upd_by, " _
                    & "  astrnf_upd_date, " _
                    & "  astrnf_code, " _
                    & "  astrnf_date, " _
                    & "  astrnf_rmks, " _
                    & "  astrnf_tran_id, " _
                    & "  astrnf_trans_id, " _
                    & "  astrnf_trans_rmks, " _
                    & "  en_desc, " _
                    & "  astrnf_dt " _
                    & "FROM  " _
                    & "  public.astrnf_transfer " _
                    & "  INNER JOIN en_mstr on astrnf_en_id = en_id " _
                    & "  where astrnf_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and astrnf_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and astrnf_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"


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
        'Ant 22 feb 2011
        sql = "SELECT  " _
            & "  public.astrnfd_det.astrnfd_oid, " _
            & "  public.astrnfd_det.astrnfd_astrnf_oid, " _
            & "  public.astrnfd_det.astrnfd_seq, " _
            & "  public.astrnfd_det.astrnfd_ass_id, " _
            & "  public.astrnfd_det.astrnfd_emp_id_from, " _
            & "  public.astrnfd_det.astrnfd_emp_id_to, " _
            & "  public.astrnfd_det.astrnfd_rmks, " _
            & "  public.astrnfd_det.astrnfd_dt, " _
            & "  public.astrnfd_det.astrnfd_dept_id_to, " _
            & "  public.astrnfd_det.astrnfd_rg_id_to, " _
            & "  public.astrnfd_det.astrnfd_dept_id_from, " _
            & "  public.astrnfd_det.astrnfd_rg_id_from, " _
            & "  public.astrnfd_det.astrnfd_loc_id_from, " _
            & "  public.astrnfd_det.astrnfd_loc_id_to, " _
            & "  xemp_mstr_from.xemp_name AS xemp_name_from, " _
            & "  code_mstr_depfrom.code_name AS code_name_depfrom, " _
            & "  code_mstr_rgfrom.code_name AS code_name_rgfrom, " _
            & "  xemp_mstr_to.xemp_name AS xemp_name_to, " _
            & "  code_mstr_depto.code_name AS code_name_depto, " _
            & "  code_mstr_rgto.code_name AS code_name_rgto, " _
            & "  loc_mstr_from.loc_desc AS loc_desc_from, " _
            & "  loc_mstr_to.loc_desc AS loc_desc_to, " _
            & "  public.ass_mstr.ass_code, " _
            & "  public.ass_mstr.ass_model " _
            & "FROM " _
            & "  public.astrnfd_det " _
            & "  INNER JOIN public.astrnf_transfer ON (public.astrnfd_det.astrnfd_astrnf_oid = public.astrnf_transfer.astrnf_oid) " _
            & "  INNER JOIN public.ass_mstr ON (public.astrnfd_det.astrnfd_ass_id = public.ass_mstr.ass_id) " _
            & "  INNER JOIN public.xemp_mstr xemp_mstr_from ON (public.astrnfd_det.astrnfd_emp_id_from = xemp_mstr_from.xemp_id) " _
            & "  INNER JOIN public.code_mstr code_mstr_depfrom ON (public.astrnfd_det.astrnfd_dept_id_from = code_mstr_depfrom.code_id) " _
            & "  INNER JOIN public.code_mstr code_mstr_rgfrom ON (public.astrnfd_det.astrnfd_rg_id_from = code_mstr_rgfrom.code_id) " _
            & "  INNER JOIN public.xemp_mstr xemp_mstr_to ON (public.astrnfd_det.astrnfd_emp_id_to = xemp_mstr_to.xemp_id) " _
            & "  INNER JOIN public.code_mstr code_mstr_depto ON (public.astrnfd_det.astrnfd_dept_id_to = code_mstr_depto.code_id) " _
            & "  INNER JOIN public.code_mstr code_mstr_rgto ON (public.astrnfd_det.astrnfd_rg_id_to = code_mstr_rgto.code_id)" _
            & "  INNER JOIN public.loc_mstr loc_mstr_from ON (public.astrnfd_det.astrnfd_loc_id_from = loc_mstr_from.loc_id)" _
            & "  INNER JOIN public.loc_mstr loc_mstr_to ON (public.astrnfd_det.astrnfd_loc_id_to = loc_mstr_to.loc_id)" _
            & "  where astrnf_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and astrnf_date <= " + SetDate(pr_txttglakhir.DateTime.Date)
        '-------------------------------
        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("astrnfd_astrnf_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[astrnfd_astrnf_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("astrnf_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("astrnfd_astrnf_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("astrnf_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Sub insert_data_awal()
        astrnf_en_id.Focus()
        astrnf_en_id.ItemIndex = 0
        astrnf_date.DateTime = _now
        astrnf_rmks.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    'Ant 22 feb 2011
                    .SQL = "SELECT  " _
                            & "  public.astrnfd_det.astrnfd_oid, " _
                            & "  public.astrnfd_det.astrnfd_astrnf_oid, " _
                            & "  public.astrnfd_det.astrnfd_seq, " _
                            & "  public.astrnfd_det.astrnfd_ass_id, " _
                            & "  public.astrnfd_det.astrnfd_emp_id_from, " _
                            & "  public.astrnfd_det.astrnfd_emp_id_to, " _
                            & "  public.astrnfd_det.astrnfd_rmks, " _
                            & "  public.astrnfd_det.astrnfd_dt, " _
                            & "  public.astrnfd_det.astrnfd_dept_id_to, " _
                            & "  public.astrnfd_det.astrnfd_rg_id_to, " _
                            & "  public.astrnfd_det.astrnfd_dept_id_from, " _
                            & "  public.astrnfd_det.astrnfd_rg_id_from, " _
                            & "  public.astrnfd_det.astrnfd_loc_id_from, " _
                            & "  public.astrnfd_det.astrnfd_loc_id_to, " _
                            & "  xemp_mstr_from.xemp_name AS xemp_name_from, " _
                            & "  code_mstr_depfrom.code_name AS code_name_depfrom, " _
                            & "  code_mstr_rgfrom.code_name AS code_name_rgfrom, " _
                            & "  xemp_mstr_to.xemp_name AS xemp_name_to, " _
                            & "  code_mstr_depto.code_name AS code_name_depto, " _
                            & "  code_mstr_rgto.code_name AS code_name_rgto, " _
                            & "  loc_mstr_from.loc_desc AS loc_desc_from, " _
                            & "  loc_mstr_to.loc_desc AS loc_desc_to, " _
                            & "  public.ass_mstr.ass_code, " _
                            & "  public.ass_mstr.ass_model " _
                            & "FROM " _
                            & "  public.astrnfd_det " _
                            & "  INNER JOIN public.astrnf_transfer ON (public.astrnfd_det.astrnfd_astrnf_oid = public.astrnf_transfer.astrnf_oid) " _
                            & "  INNER JOIN public.ass_mstr ON (public.astrnfd_det.astrnfd_ass_id = public.ass_mstr.ass_id) " _
                            & "  INNER JOIN public.xemp_mstr xemp_mstr_from ON (public.ass_mstr.ass_emp_id = xemp_mstr_from.xemp_id) " _
                            & "  INNER JOIN public.code_mstr code_mstr_depfrom ON (public.ass_mstr.ass_emp_dept = code_mstr_depfrom.code_id) " _
                            & "  INNER JOIN public.code_mstr code_mstr_rgfrom ON (public.ass_mstr.ass_emp_rg = code_mstr_rgfrom.code_id) " _
                            & "  INNER JOIN public.xemp_mstr xemp_mstr_to ON (public.astrnfd_det.astrnfd_emp_id_to = xemp_mstr_to.xemp_id) " _
                            & "  INNER JOIN public.code_mstr code_mstr_depto ON (public.astrnfd_det.astrnfd_dept_id_to = code_mstr_depto.code_id) " _
                            & "  INNER JOIN public.code_mstr code_mstr_rgto ON (public.astrnfd_det.astrnfd_rg_id_to = code_mstr_rgto.code_id)" _
                            & "  INNER JOIN public.loc_mstr loc_mstr_from ON (public.astrnfd_det.astrnfd_loc_id_from = loc_mstr_from.loc_id)" _
                            & "  INNER JOIN public.loc_mstr loc_mstr_to ON (public.astrnfd_det.astrnfd_loc_id_to = loc_mstr_to.loc_id)" _
                            & " where public.astrnfd_det.astrnfd_seq = -99"
                    '-----------------------------------
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function insert() As Boolean
        Dim _astrnf_oid As Guid
        _astrnf_oid = Guid.NewGuid

        Dim _asstrnf_code As String
        'Dim _req_total As Double
        'Dim i, j As Integer
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Dim ssqls As New ArrayList
      
        _asstrnf_code = func_coll.get_transaction_number("AT", astrnf_en_id.GetColumnValue("en_code"), "astrnf_transfer", "astrnf_code")

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
                                                & "  public.astrnf_transfer " _
                                                & "( " _
                                                & "  astrnf_oid, " _
                                                & "  astrnf_dom_id, " _
                                                & "  astrnf_en_id, " _
                                                & "  astrnf_add_by, " _
                                                & "  astrnf_add_date, " _
                                                & "  astrnf_code, " _
                                                & "  astrnf_date, " _
                                                & "  astrnf_rmks, " _
                                                & "  astrnf_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_astrnf_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(astrnf_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ", " _
                                                & " current_timestamp " & ", " _
                                                & SetSetring(_asstrnf_code.ToString) & ",  " _
                                                & SetDate(astrnf_date.EditValue) & ",  " _
                                                & SetSetring(astrnf_rmks.Text) & ",  " _
                                                & " current_timestamp " & " " _
                                                & ");"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            'Ant 22 Feb 2011
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.astrnfd_det " _
                                                    & "( " _
                                                    & "  astrnfd_oid, " _
                                                    & "  astrnfd_astrnf_oid, " _
                                                    & "  astrnfd_seq, " _
                                                    & "  astrnfd_ass_id, " _
                                                    & "  astrnfd_emp_id_from, " _
                                                    & "  astrnfd_emp_id_to, " _
                                                    & "  astrnfd_loc_id_from, " _
                                                    & "  astrnfd_loc_id_to, " _
                                                    & "  astrnfd_rmks, " _
                                                    & "  astrnfd_dt, " _
                                                    & "  astrnfd_dept_id_to, " _
                                                    & "  astrnfd_rg_id_to, " _
                                                    & "  astrnfd_dept_id_from, " _
                                                    & "  astrnfd_rg_id_from " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_astrnf_oid.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_ass_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_emp_id_from")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_emp_id_to")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_loc_id_from")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_loc_id_to")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("astrnfd_rmks")) & ",  " _
                                                    & "current_timestamp,  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_dept_id_to")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_rg_id_to")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_dept_id_from")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_rg_id_from")) & "  " _
                                                    & ");"
                            '-------------------------------------
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'ant 22 feb 2011
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = " update ass_mstr " + _
                                                   " set ass_qty_assgn = 1, " + _
                                                   " ass_emp_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_emp_id_to")) + ", " + _
                                                   " ass_loc_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_loc_id_to")) + ", " + _
                                                   " ass_emp_dept = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_dept_id_to")) + ", " + _
                                                   " ass_emp_rg = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_rg_id_to")) + " " + _
                                                   " where ass_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("astrnfd_ass_id"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            '-------------------------------------        
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
                        set_row(_astrnf_oid.ToString, "astrnf_oid")
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _astrnf_en_id As Integer = astrnf_en_id.EditValue

        If _col = "ass_code" Then
            Dim frm As New FAssetSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _astrnf_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "xemp_name_to" Then
            Dim frm As New FEmpSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _astrnf_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name_depto" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _astrnf_en_id
            frm._type = "emp_dept"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name_rgto" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _astrnf_en_id
            frm._type = "emp_region"
            frm.type_form = True
            frm.ShowDialog()
            'Ant 22 feb 2011
        ElseIf _col = "loc_desc_to" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _astrnf_en_id
            frm.type_form = True
            frm.ShowDialog()
            '--------------------------------
        End If
    End Sub
End Class
