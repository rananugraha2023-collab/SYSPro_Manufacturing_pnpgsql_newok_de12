Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FCFInDirectRule
    Dim ssql As String
    Dim _cfidrule_oid_edit As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Dim _conf_value As String
    Public ds_edit As DataSet
    Public dt_edit_detail As DataTable
    Public dt_edit_sum As DataTable


    Private Sub FCFInDirectRule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        cfidrule_en_id.Properties.DataSource = dt_bantu
        cfidrule_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cfidrule_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cfidrule_en_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "cfidrule_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sequence", "cfidrule_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Header", "cfidrule_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Header", "cfidrule_subheader", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Footer", "cfidrule_footer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Invert", "cfidrule_is_invert", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "cfidrule_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cfidrule_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cfidrule_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "User Update", "cfidrule_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cfidrule_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")


        add_column(gv_detail, "cfidruled_oid", False)
        add_column(gv_detail, "cfidruled_cfidrule_oid", False)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        
        
        add_column(gv_edit, "cfidruled_oid", False)
        add_column(gv_edit, "cfidruled_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)


    End Sub


    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  cfidrule_oid, " _
                & "  cfidrule_dom_id, " _
                & "  cfidrule_en_id, " _
                & "  cfidrule_add_by, " _
                & "  cfidrule_add_date, " _
                & "  cfidrule_upd_by, " _
                & "  cfidrule_upd_date, " _
                & "  cfidrule_header, " _
                & "  cfidrule_subheader, " _
                & "  cfidrule_footer, " _
                & "  coalesce(cfidrule_seq,0) as cfidrule_seq, " _
                & "  cfidrule_remarks, " _
                & "  coalesce(cfidrule_is_invert,'N') as cfidrule_is_invert, " _
                & "  cfidrule_dt, " _
                & "  en_desc " _
                & "FROM  " _
                & "  public.cfidrule_mstr" _
                & "  INNER JOIN public.en_mstr ON (public.cfidrule_mstr.cfidrule_en_id = public.en_mstr.en_id)" _
                & "  order by cfidrule_seq "


        Return get_sequel
    End Function


    Public Overrides Sub load_data_grid_detail()
        Dim _get_oid As String = ""
        'Dim i As Integer

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  cfidruled_oid, " _
            & "  cfidruled_cfidrule_oid, " _
            & "  cfidruled_seq, " _
            & "  cfidruled_ac_id, " _
            & "  ac_mstr.ac_code as ac_code, " _
            & "  ac_mstr.ac_name as ac_name " _
            & "FROM  " _
            & "  public.cfidruled_det " _
            & "  INNER JOIN ac_mstr ON (cfidruled_ac_id = ac_mstr.ac_id) "

        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub insert_data_awal()

        _cfidrule_oid_edit = ""
        cfidrule_en_id.ItemIndex = 0
        cfidrule_header.Text = ""
        cfidrule_subheader.Text = ""
        cfidrule_footer.Text = ""
        cfidrule_seq.EditValue = 0
        cfidrule_remarks.Text = ""
        cfidrule_is_invert.Checked = False
        
        ssql = "SELECT  " _
            & "  cfidruled_oid, " _
            & "  cfidruled_cfidrule_oid, " _
            & "  cfidruled_seq, " _
            & "  cfidruled_ac_id, " _
            & "  ac_mstr.ac_code as ac_code, " _
            & "  ac_mstr.ac_name as ac_name " _
            & "FROM  " _
            & "  public.cfidruled_det " _
            & "  INNER JOIN ac_mstr ON (cfidruled_ac_id = ac_mstr.ac_id) " _
            & "WHERE " _
            & "  cfidruled_cfidrule_oid is null"

        dt_edit_detail = GetTableData(ssql)
        gc_edit.DataSource = dt_edit_detail
        gv_edit.BestFitColumns()

        Try

            tcg_header.SelectedTabPageIndex = 0
            tcg_edit.SelectedTabPageIndex = 0
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Function insert() As Boolean
        Dim _cfidrule_oid As String = Guid.NewGuid.ToString
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
                                        & "  public.cfidrule_mstr " _
                                        & "( " _
                                        & "  cfidrule_oid, " _
                                        & "  cfidrule_dom_id, " _
                                        & "  cfidrule_en_id, " _
                                        & "  cfidrule_seq, " _
                                        & "  cfidrule_add_by, " _
                                        & "  cfidrule_add_date, " _
                                        & "  cfidrule_header, " _
                                        & "  cfidrule_subheader, " _
                                        & "  cfidrule_footer, " _
                                        & "  cfidrule_is_invert, " _
                                        & "  cfidrule_dt, " _
                                        & "  cfidrule_remarks " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_cfidrule_oid) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(cfidrule_en_id.EditValue) & ",  " _
                                        & SetInteger(cfidrule_seq.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDate(CekTanggal) & ",  " _
                                        & SetSetring(cfidrule_header.Text) & ",  " _
                                        & SetSetring(cfidrule_subheader.Text) & ",  " _
                                        & SetSetring(cfidrule_footer.Text) & ",  " _
                                        & SetBitYN(cfidrule_is_invert.EditValue) & ",  " _
                                        & SetDate(CekTanggal) & ",  " _
                                        & SetSetring(cfidrule_remarks.Text) & "  " _
                                        & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0

                        
                        For Each dr As DataRow In dt_edit_detail.Rows
                            ssql = "INSERT INTO  " _
                                    & "  public.cfidruled_det " _
                                    & "( " _
                                    & "  cfidruled_oid, " _
                                    & "  cfidruled_cfidrule_oid, " _
                                    & "  cfidruled_seq, " _
                                    & "  cfidruled_ac_id " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_cfidrule_oid) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(dr("cfidruled_ac_id")) & "  " _
                                    & ");"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next


                        insert = True
                        sqlTran.Commit()

                        after_success()
                        set_row(_cfidrule_oid, "cfidrule_oid")
                        dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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


    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("cfidruled_cfidrule_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cfidruled_cfidrule_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cfidrule_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            cfidrule_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cfidrule_oid_edit = .Item("cfidrule_oid").ToString
                cfidrule_en_id.EditValue = .Item("cfidrule_en_id")
                cfidrule_seq.EditValue = .Item("cfidrule_seq")
                cfidrule_header.Text = SetString(.Item("cfidrule_header"))
                cfidrule_subheader.Text = SetString(.Item("cfidrule_subheader"))
                cfidrule_footer.Text = SetString(.Item("cfidrule_footer"))
                cfidrule_is_invert.EditValue = SetBitYNB(.Item("cfidrule_is_invert"))
                cfidrule_remarks.Text = SetString(.Item("cfidrule_remarks"))

            End With

            ssql = "SELECT  " _
                    & "  cfidruled_oid, " _
                    & "  cfidruled_cfidrule_oid, " _
                    & "  cfidruled_seq, " _
                    & "  cfidruled_ac_id, " _
                    & "  ac_mstr.ac_code as ac_code, " _
                    & "  ac_mstr.ac_name as ac_name " _
                    & "FROM  " _
                    & "  public.cfidruled_det " _
                    & "  INNER JOIN ac_mstr ON (cfidruled_ac_id = ac_mstr.ac_id) " _
                    & "WHERE " _
                    & "  cfidruled_cfidrule_oid =" & SetSetring(_cfidrule_oid_edit)

            dt_edit_detail = GetTableData(ssql)
            gc_edit.DataSource = dt_edit_detail
            gv_edit.BestFitColumns()
        
            Try
                tcg_header.SelectedTabPageIndex = 0
                DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit_detail.AcceptChanges()

        edit = True
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
                                            & "  public.cfidrule_mstr   " _
                                            & "SET  " _
                                            & "  cfidrule_en_id = " & SetInteger(cfidrule_en_id.EditValue) & ",  " _
                                            & "  cfidrule_seq = " & SetInteger(cfidrule_seq.EditValue) & ",  " _
                                            & "  cfidrule_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  cfidrule_upd_date = " & SetDate(CekTanggal) & ",  " _
                                            & "  cfidrule_header = " & SetSetringDB(cfidrule_header.Text) & ",  " _
                                            & "  cfidrule_subheader = " & SetSetringDB(cfidrule_subheader.Text) & ",  " _
                                            & "  cfidrule_footer = " & SetSetringDB(cfidrule_footer.Text) & ",  " _
                                            & "  cfidrule_is_invert = " & SetBitYN(cfidrule_is_invert.EditValue) & ",  " _
                                            & "  cfidrule_dt = " & SetDate(CekTanggal) & ",  " _
                                            & "  cfidrule_remarks = " & SetSetringDB(cfidrule_remarks.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cfidrule_oid = " & SetSetring(_cfidrule_oid_edit) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cfidruled_det where cfidruled_cfidrule_oid = " & SetSetring(_cfidrule_oid_edit)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0
                        
                        For Each dr As DataRow In dt_edit_detail.Rows
                            ssql = "INSERT INTO  " _
                                    & "  public.cfidruled_det " _
                                    & "( " _
                                    & "  cfidruled_oid, " _
                                    & "  cfidruled_cfidrule_oid, " _
                                    & "  cfidruled_seq, " _
                                    & "  cfidruled_ac_id " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_cfidrule_oid_edit) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(dr("cfidruled_ac_id")) & "  " _
                                    & ");"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
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
                        set_row(Trim(_cfidrule_oid_edit.ToString), "cfidrule_oid")
                        dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then

            row = BindingContext(ds.Tables(0)).Position

            'If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
            '    row = row - 1
            'ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
            '    row = 0
            'End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            ssql = "delete from cfidruled_det where cfidruled_cfidrule_oid = '" + ds.Tables(0).Rows(row).Item("cfidrule_oid").ToString + "'"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            ssql = "delete from cfidrule_mstr where cfidrule_oid = '" + ds.Tables(0).Rows(row).Item("cfidrule_oid").ToString + "'"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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


        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit_detail.AcceptChanges()
        

        If (dt_edit_detail.Rows.Count < 1) Then
            MsgBox("Detail Can't Empty...", MsgBoxStyle.Critical, "Unable to Save")
            before_save = False
        End If
        


        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub btn_gen_rule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_gen_rule.Click

        Dim _dtrow As DataRow
        Dim _avail_code As Boolean


        For _code As Integer = te_ac_from.EditValue To te_ac_to.EditValue

            If cek_avail_list(_code) = True Then
                MsgBox("Account : " & _code & " Already Available on List", MsgBoxStyle.Critical, "Duplicate")
                Exit Sub
            End If

            _avail_code = False
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand

                        .Command.CommandType = CommandType.Text
                        'ant 24 jan 2012 (revisi jika kode akun ada titik-nya)
                        .Command.CommandText = "select ac_id,ac_code,ac_name from ac_mstr where replace(ac_code,'.','') in (" & SetSetring(_code) & ")"
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader

                        _dtrow = dt_edit_detail.NewRow
                        _dtrow("cfidruled_oid") = Guid.NewGuid.ToString
                        While .DataReader.Read
                            If Replace(.DataReader("ac_code").ToString, ".", "") = _code.ToString Then
                                _avail_code = True
                                _dtrow("cfidruled_ac_id") = .DataReader("ac_id").ToString
                                _dtrow("ac_code") = .DataReader("ac_code").ToString
                                _dtrow("ac_name") = .DataReader("ac_name").ToString
                            End If
                        End While
                        '---------------------------------

                        'pak rana tidak mau pake
                        'If _avail_code = False Then
                        '    MsgBox("Account Code " & _code_from.ToString & " Is Unknown", MsgBoxStyle.Critical, "Account Unavailable")
                        '    Exit Sub
                        'End If

                        'solusi pak rana yg atas
                        If _avail_code = True Then
                            dt_edit_detail.Rows.Add(_dtrow)
                        End If


                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


        Next
        dt_edit_detail.AcceptChanges()
        '--------------------------------------
    End Sub

    Private Function cek_avail_list(ByVal _par_code As String) As Boolean
        cek_avail_list = False
        Try
            For i As Integer = 0 To dt_edit_detail.Rows.Count - 1
                If (_par_code = Replace(dt_edit_detail.Rows(i).Item("ac_code").ToString, ".", "")) Then
                    cek_avail_list = True
                End If
            Next
        Catch
        End Try
        Return cek_avail_list
    End Function


    '---------------------------------------------------------------

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = cfidrule_en_id.EditValue
            frm._col_name = _col
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

End Class
