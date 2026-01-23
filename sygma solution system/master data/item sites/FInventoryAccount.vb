Imports npgsql
Imports master_new.ModFunction

Public Class FInventoryAccount
    Dim _plinv_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FInventoryAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pl_mstr())
        plinv_pl_id.Properties.DataSource = dt_bantu
        plinv_pl_id.Properties.DisplayMember = dt_bantu.Columns("pl_desc").ToString
        plinv_pl_id.Properties.ValueMember = dt_bantu.Columns("pl_id").ToString
        plinv_pl_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr())
        plinv_loc_id.Properties.DataSource = dt_bantu
        plinv_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        plinv_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        plinv_loc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        pla_ac_id.Properties.DataSource = dt_bantu
        pla_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        pla_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        pla_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr())
        pla_sb_id.Properties.DataSource = dt_bantu
        pla_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        pla_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        pla_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr())
        pla_cc_id.Properties.DataSource = dt_bantu
        pla_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        pla_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        pla_cc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "pl_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "plinv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "plinv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "plinv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "plinv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "plinva_plinv_oid", False)
        add_column_copy(gv_detail, "Code", "plinva_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "plinva_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account Code", "sb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account Description", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center Code", "cc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center Description", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  plinv_oid, " _
                    & "  plinv_dom_id, " _
                    & "  plinv_add_by, " _
                    & "  plinv_add_date, " _
                    & "  plinv_upd_by, " _
                    & "  plinv_upd_date, " _
                    & "  plinv_pl_id, " _
                    & "  pl_code, " _
                    & "  pl_desc, " _
                    & "  plinv_loc_id, " _
                    & "  loc_desc, " _
                    & "  plinv_dt " _
                    & "FROM  " _
                    & "  public.plinv_inv " _
                    & " inner join pl_mstr on pl_id = plinv_pl_id " _
                    & " inner join loc_mstr on loc_id = plinv_loc_id "

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
            & "  plinva_oid, " _
            & "  plinva_plinv_oid, " _
            & "  plinva_seq, " _
            & "  plinva_code, " _
            & "  plinva_desc, " _
            & "  plinva_ac_id, " _
            & "  plinva_sb_id, " _
            & "  plinva_cc_id, " _
            & "  public.ac_mstr.ac_name, " _
            & "  public.ac_mstr.ac_code, " _
            & "  public.cc_mstr.cc_code, " _
            & "  public.cc_mstr.cc_desc, " _
            & "  public.sb_mstr.sb_code, " _
            & "  public.sb_mstr.sb_desc, " _
            & "  plinva_dt " _
            & "FROM  " _
            & "  public.plinva_acct" _
            & "  left outer JOIN public.ac_mstr ON (public.plinva_acct.plinva_ac_id = public.ac_mstr.ac_id) " _
            & "  left outer JOIN public.cc_mstr ON (public.plinva_acct.plinva_cc_id = public.cc_mstr.cc_id) " _
            & "  left outer JOIN public.sb_mstr ON (public.plinva_acct.plinva_sb_id = public.sb_mstr.sb_id)"

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("plinva_plinv_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("plinva_plinv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plinv_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("plinva_plinv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plinv_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        plinv_pl_id.Focus()
        plinv_pl_id.ItemIndex = 0
        plinv_loc_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _plinv_oid As Guid = Guid.NewGuid
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from plam_mstr where plam_param ~~* 'inventory account'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "plam_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
                                            & "  public.plinv_inv " _
                                            & "( " _
                                            & "  plinv_oid, " _
                                            & "  plinv_dom_id, " _
                                            & "  plinv_add_by, " _
                                            & "  plinv_add_date, " _
                                            & "  plinv_pl_id, " _
                                            & "  plinv_loc_id, " _
                                            & "  plinv_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_plinv_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(plinv_pl_id.EditValue) & ",  " _
                                            & SetInteger(plinv_loc_id.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.plinva_acct " _
                                            & "( " _
                                            & "  plinva_oid, " _
                                            & "  plinva_plinv_oid, " _
                                            & "  plinva_seq, " _
                                            & "  plinva_code, " _
                                            & "  plinva_desc, " _
                                            & "  plinva_ac_id, " _
                                            & "  plinva_sb_id, " _
                                            & "  plinva_cc_id, " _
                                            & "  plinva_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_plinv_oid.ToString) & ",  " _
                                            & SetSetring(i) & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_code")) & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_desc")) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
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
                        set_row(_plinv_oid.ToString, "plinv_oid")
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
            plinv_pl_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _plinv_oid_mstr = .Item("plinv_oid").ToString
                plinv_pl_id.EditValue = .Item("plinv_pl_id")
                plinv_loc_id.EditValue = .Item("plinv_loc_id")
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
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
                                            & "  public.plinv_inv   " _
                                            & "SET  " _
                                            & "  plinv_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  plinv_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  plinv_pl_id = " & SetInteger(plinv_pl_id.EditValue) & ",  " _
                                            & "  plinv_loc_id = " & SetInteger(plinv_loc_id.EditValue) & ",  " _
                                            & "  plinv_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  plinv_oid = " & SetSetring(_plinv_oid_mstr) & " "
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
                        set_row(_plinv_oid_mstr, "plinv_oid")
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
                            .Command.CommandText = "delete from plinv_inv where plinv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("plinv_oid").ToString + "'"
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

    Private Sub sb_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_edit.Click
        Dim _plinva_oid As String = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("plinva_oid").ToString
        Dim _pl_code As String = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_code")
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
                        .Command.CommandText = "update plinva_acct set plinva_ac_id = " + pla_ac_id.EditValue.ToString + ", " + _
                                               " plinva_sb_id = " + pla_sb_id.EditValue.ToString + ", " + _
                                               " plinva_cc_id = " + pla_cc_id.EditValue.ToString + _
                                               " where plinva_oid = '" + _plinva_oid + "'"
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
                        set_row(_pl_code, "pl_code")

                        For i = 0 To ds.Tables("detail").Rows.Count - 1
                            If ds.Tables("detail").Rows(i).Item("plinva_oid").ToString = _plinva_oid Then
                                BindingContext(ds.Tables("detail")).Position = i
                            End If
                        Next

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
