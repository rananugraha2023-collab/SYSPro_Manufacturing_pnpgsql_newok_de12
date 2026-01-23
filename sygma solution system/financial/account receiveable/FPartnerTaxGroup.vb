Imports npgsql
Imports master_new.ModFunction

Public Class FPartnerTaxGroup
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _tipg_oid_mstr As String
    Dim ds_edit As DataSet
    Dim mf As New master_new.ModFunction

    Private Sub FPartnerTaxGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        AddHandler gv_master.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_master.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer())
        tipg_ptnr_id.Properties.DataSource = dt_bantu
        tipg_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        tipg_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        tipg_ptnr_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "tipg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Descriptions", "tipg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Parent", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "tipg_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "tipg_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "tipg_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "tipg_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "tipgd_tipg_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Customer Related", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "tipgd_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "tipgd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "tipgd_ptnr_id", False)
        add_column(gv_edit, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Remarks", "tipgd_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  tipg_oid, " _
                    & "  tipg_code, " _
                    & "  tipg_desc, " _
                    & "  ptnr_name, " _
                    & "  tipg_upd_date, " _
                    & "  tipg_upd_by, " _
                    & "  tipg_add_date, " _
                    & "  tipg_add_by,tipg_ptnr_id " _
                    & "FROM " _
                    & "  public.tipg_group " _
                    & "  inner join ptnr_mstr on ptnr_id = tipg_ptnr_id "

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
            & "  tipgd_oid, " _
            & "  tipgd_tipg_oid, " _
            & "  tipgd_en_id, " _
            & "  en_desc, " _
            & "  tipgd_seq, " _
            & "  tipgd_ptnr_id, " _
            & "  ptnr_name, " _
            & "  tipgd_remarks " _
            & "  FROM " _
            & "  public.tipgd_det " _
            & "  INNER JOIN en_mstr ON tipgd_en_id = en_id " _
            & "  INNER JOIN ptnr_mstr ON tipgd_ptnr_id = ptnr_id "

        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("tipgd_tipg_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tipgd_tipg_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tipg_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        tipg_code.Focus()
        tipg_code.Text = ""
        tipg_desc.Text = ""
        tipg_ptnr_id.ItemIndex = 0

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
                    .SQL = "SELECT  " _
                            & "  tipgd_oid, " _
                            & "  tipgd_tipg_oid, " _
                            & "  tipgd_en_id, " _
                            & "  en_desc, " _
                            & "  tipgd_seq, " _
                            & "  tipgd_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  tipgd_remarks " _
                            & "  FROM " _
                            & "  public.tipgd_det " _
                            & "  INNER JOIN en_mstr ON tipgd_en_id = en_id " _
                            & "  INNER JOIN ptnr_mstr ON tipgd_ptnr_id = ptnr_id " _
                            & " where tipgd_ptnr_id = -99"
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

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If Trim(tipg_code.Text) = "" Then
            MessageBox.Show("Code Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Trim(tipg_desc.Text) = "" Then
            MessageBox.Show("Descriptions Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _tipg_oid As Guid
        _tipg_oid = Guid.NewGuid

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
                                            & "  public.tipg_group " _
                                            & "( " _
                                            & "  tipg_oid, " _
                                            & "  tipg_dom_id, " _
                                            & "  tipg_code, " _
                                            & "  tipg_desc, " _
                                            & "  tipg_ptnr_id, " _
                                            & "  tipg_add_by, " _
                                            & "  tipg_add_date, " _
                                            & "  tipg_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_tipg_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(tipg_code.Text) & ",  " _
                                            & SetSetring(tipg_desc.Text) & ",  " _
                                            & SetInteger(tipg_ptnr_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ", " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tipgd_det " _
                                                & "( " _
                                                & "  tipgd_oid, " _
                                                & "  tipgd_tipg_oid, " _
                                                & "  tipgd_seq, " _
                                                & "  tipgd_en_id, " _
                                                & "  tipgd_ptnr_id, " _
                                                & "  tipgd_remarks " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_tipg_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("tipgd_en_id")) & ", " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("tipgd_ptnr_id")) & ", " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("tipgd_remarks")) _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Partner Tax Group " & Trim(tipg_code.Text))
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
                        set_row(tipg_code.Text, "tipg_code")
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _tipg_oid_mstr = .Item("tipg_oid").ToString
                tipg_code.Text = SetString(.Item("tipg_code"))
                tipg_desc.Text = SetString(.Item("tipg_desc"))
                tipg_ptnr_id.EditValue = .Item("tipg_ptnr_id")

            End With
            tipg_code.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tipgd_oid, " _
                            & "  tipgd_tipg_oid, " _
                            & "  tipgd_en_id, " _
                            & "  en_desc, " _
                            & "  tipgd_seq, " _
                            & "  tipgd_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  tipgd_remarks " _
                            & "  FROM " _
                            & "  public.tipgd_det " _
                            & "  INNER JOIN tipg_group ON tipg_oid = tipgd_tipg_oid " _
                            & "  INNER JOIN en_mstr ON tipgd_en_id = en_id " _
                            & "  INNER JOIN ptnr_mstr ON tipgd_ptnr_id = ptnr_id " _
                            & " where tipg_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tipg_oid"))

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
                                            & "  public.tipg_group " _
                                            & "SET  " _
                                            & "  tipg_code = " & SetSetring(tipg_code.Text) & ",  " _
                                            & "  tipg_desc = " & SetSetring(tipg_desc.Text) & ",  " _
                                            & "  tipg_ptnr_id = " & SetInteger(tipg_ptnr_id.EditValue) & ",  " _
                                            & "  tipg_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  tipg_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  tipg_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  tipg_oid = " & SetSetring(_tipg_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from tipgd_det where tipgd_tipg_oid = " & SetSetring(_tipg_oid_mstr.ToString)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tipgd_det " _
                                                & "( " _
                                                & "  tipgd_oid, " _
                                                & "  tipgd_tipg_oid, " _
                                                & "  tipgd_seq, " _
                                                & "  tipgd_en_id, " _
                                                & "  tipgd_ptnr_id, " _
                                                & "  tipgd_remarks " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_tipg_oid_mstr.ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("tipgd_en_id")) & ", " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("tipgd_ptnr_id")) & ", " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("tipgd_remarks")) _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Partner Tax Group " & Trim(tipg_code.Text))
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
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(Trim(tipg_code.Text), "tipg_code")
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
                            .Command.CommandText = "delete from tipg_group where tipg_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tipg_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            '----------------------------------

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
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_en_id")
        Dim _pod_si_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_si_id")

        If _col = "en_desc" Then
            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ptnr_name" Then
            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub
End Class
