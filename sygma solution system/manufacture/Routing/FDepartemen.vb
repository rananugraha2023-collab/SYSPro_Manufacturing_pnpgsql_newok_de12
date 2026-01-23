Imports npgsql
Imports master_new.ModFunction

Public Class FDepartemen
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _dpt_oid_mstr As String
    Public ds_aclist As DataSet
    Dim ssqls As New ArrayList

    Private Sub FDepartemen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        dpt_en_id.Properties.DataSource = dt_bantu
        dpt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        dpt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        dpt_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        le_account.Properties.DataSource = dt_bantu
        le_account.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        le_account.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        le_account.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr())
        le_sub.Properties.DataSource = dt_bantu
        le_sub.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        le_sub.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        le_sub.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr())
        le_cost.Properties.DataSource = dt_bantu
        le_cost.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        le_cost.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        le_cost.ItemIndex = 0


        dt_bantu = New DataTable
        dt_bantu = (func_data.load_dpt_group())
        dpt_group.Properties.DataSource = dt_bantu
        dpt_group.Properties.DisplayMember = dt_bantu.Columns("dptg_desc").ToString
        dpt_group.Properties.ValueMember = dt_bantu.Columns("dptg_kode").ToString
        dpt_group.ItemIndex = 0


    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Dept. Code", "dpt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Dept. Desc", "dpt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Capacity Quran", "dpt_lbr_cap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Capacity Book", "dpt_lbr_cap_book", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Group Kode", "dpt_group", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Desc", "dptg_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Is Active", "dpt_active", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "User Create", "dpt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dpt_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "dpt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dpt_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_account, "dpta_dpt_oid", False)
        add_column_copy(gv_account, "Type", "dpta_acc_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_account, "Description", "dpta_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_account, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_account, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_account, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  dpt_oid, " _
                    & "  dpt_dom_id, " _
                    & "  dpt_en_id, " _
                    & "  dpt_add_by, " _
                    & "  dpt_add_date, " _
                    & "  dpt_upd_by, " _
                    & "  dpt_upd_date, " _
                    & "  dpt_id, " _
                    & "  dpt_code, " _
                    & "  dpt_desc, " _
                    & "  dpt_lbr_cap, " _
                    & "  dpt_active, " _
                    & "  dpt_dt,dpt_lbr_cap_book,dpt_group,dptg_desc, " _
                    & "  en_desc" _
                    & " FROM  " _
                    & "  public.dpt_mstr " _
                    & "  inner join en_mstr on en_id = dpt_en_id " _
                    & "  left outer join dptg_group on dpt_group = dptg_kode " _
                    & " order by dpt_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        dpt_en_id.ItemIndex = 0
        dpt_code.Text = ""
        dpt_desc.Text = ""
        dpt_lbr_cap.Text = 0
        dpt_active.EditValue = True
        dpt_en_id.Focus()
        dpt_lbr_cap.EditValue = 0.0
        dpt_lbr_cap_book.EditValue = 0.0
        dpt_group.ItemIndex = 0

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _dpt_oid As Guid
        _dpt_oid = Guid.NewGuid

        Dim _dpt_id As Integer
        _dpt_id = SetInteger(func_coll.GetID("dpt_mstr", dpt_en_id.GetColumnValue("en_code"), "dpt_id", "dpt_en_id", dpt_en_id.EditValue.ToString))

        ds_aclist = New DataSet
        ssqls.Clear()
        Dim i As Integer
        Using ds_aclist
            'Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  dptal_oid, " _
                                & "  dptal_seq, " _
                                & "  dptal_acc_type, " _
                                & "  dptal_ac_id, " _
                                & "  dptal_sb_id, " _
                                & "  dptal_cc_id, " _
                                & "  dptal_desc, " _
                                & "  dptal_dt " _
                                & "FROM  " _
                                & "  public.dptal_list ;"

                        .InitializeCommand()
                        .FillDataSet(ds_aclist, "dptal_mstr")
                        '  dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using

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
                                            & "  public.dpt_mstr " _
                                            & "( " _
                                            & "  dpt_oid, " _
                                            & "  dpt_dom_id, " _
                                            & "  dpt_en_id, " _
                                            & "  dpt_add_by, " _
                                            & "  dpt_add_date, " _
                                            & "  dpt_id, " _
                                            & "  dpt_code, " _
                                            & "  dpt_desc, " _
                                            & "  dpt_lbr_cap,dpt_lbr_cap_book,dpt_group, " _
                                            & "  dpt_active, " _
                                            & "  dpt_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_dpt_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(dpt_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetInteger(_dpt_id) & ",  " _
                                            & SetSetring(dpt_code.Text) & ",  " _
                                            & SetSetring(dpt_desc.Text) & ",  " _
                                            & SetDec(dpt_lbr_cap.Text) & ",  " _
                                            & SetDec(dpt_lbr_cap_book.Text) & ",  " _
                                            & SetSetring(dpt_group.EditValue) & ",  " _
                                            & SetBitYN(dpt_active.EditValue) & ",  " _
                                            & " current_timestamp " & "  " _
                                            & ");"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To (ds_aclist.Tables(0).Rows.Count - 1)
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.dpta_acct " _
                                                & "( " _
                                                & "  dpta_oid, " _
                                                & "  dpta_dpt_oid, " _
                                                & "  dpta_acc_type, " _
                                                & "  dpta_ac_id, " _
                                                & "  dpta_sb_id, " _
                                                & "  dpta_cc_id, " _
                                                & "  dpta_desc, " _
                                                & "  dpta_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_dpt_oid.ToString) & ",  " _
                                                & SetSetring(ds_aclist.Tables(0).Rows(i).Item("dptal_acc_type")) & ", " _
                                                & SetInteger(ds_aclist.Tables(0).Rows(i).Item("dptal_ac_id")) & ", " _
                                                & SetInteger(ds_aclist.Tables(0).Rows(i).Item("dptal_sb_id")) & ", " _
                                                & SetInteger(ds_aclist.Tables(0).Rows(i).Item("dptal_cc_id")) & ", " _
                                                & SetSetring(ds_aclist.Tables(0).Rows(i).Item("dptal_desc")) & ", " _
                                                & " current_timestamp " & "  " _
                                                & ");"

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
                        set_row(Trim(_dpt_oid.ToString), "dpt_oid")
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            dpt_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _dpt_oid_mstr = .Item("dpt_oid").ToString
                dpt_en_id.EditValue = .Item("dpt_en_id")
                dpt_active.EditValue = SetBitYNB(.Item("dpt_active"))
                dpt_code.Text = SetString(.Item("dpt_code"))
                dpt_desc.Text = SetString(.Item("dpt_desc"))
                dpt_lbr_cap.Text = SetIntegerDB(.Item("dpt_lbr_cap"))
                dpt_lbr_cap_book.EditValue = .Item("dpt_lbr_cap_book")
                dpt_group.EditValue = .Item("dpt_group")
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        ssqls.Clear()
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
                                            & "  public.dpt_mstr   " _
                                            & "SET  " _
                                            & "  dpt_en_id = " & SetSetring(dpt_en_id.EditValue) & ",  " _
                                            & "  dpt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  dpt_upd_date = current_timestamp ,  " _
                                            & "  dpt_code = " & SetSetring(dpt_code.Text) & ",  " _
                                            & "  dpt_desc = " & SetSetring(dpt_desc.Text) & ",  " _
                                            & "  dpt_group = " & SetSetring(dpt_group.EditValue) & ",  " _
                                            & "  dpt_lbr_cap = " & SetDec(dpt_lbr_cap.EditValue) & ",  " _
                                            & "  dpt_lbr_cap_book = " & SetDec(dpt_lbr_cap_book.EditValue) & ",  " _
                                            & "  dpt_active = " & SetBitYN(dpt_active.EditValue) & ",  " _
                                            & "  dpt_dt = current_timestamp " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  dpt_oid = " & SetSetring(_dpt_oid_mstr.ToString) & "  " _
                                            & ";"


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
                        set_row(Trim(_dpt_oid_mstr.ToString), "dpt_oid")
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
            ssqls.Clear()
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from dpt_mstr where dpt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dpt_oid").ToString + "'"
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

    Private Sub sb_add_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_save.Click
        If MessageBox.Show("Add This Account To This Departement..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _dpta_oid As String
        _dpta_oid = ds.Tables("account").Rows(BindingContext(ds.Tables("account")).Position).Item("dpta_oid").ToString

        Dim _dpt_oid As String
        _dpt_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dpt_oid").ToString

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "update dpta_acct " + _
                           " set " + _
                           "dpta_ac_id = " + SetInteger(le_account.EditValue) + ", " + _
                           "dpta_sb_id = " + SetInteger(le_sub.EditValue) + ", " + _
                           "dpta_cc_id = " + SetInteger(le_cost.EditValue) + ", " + _
                           "dpta_dt = current_timestamp " + _
                            "where dpta_dpt_oid = " + SetSetring(_dpt_oid) + _
                            " and dpta_oid = " + SetSetring(_dpta_oid)

                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    load_data_grid_detail()
                    set_row(Trim(_dpt_oid.ToString), "dpt_oid")
                    Dim i As Integer
                    For i = 0 To ds.Tables("account").Rows.Count - 1
                        If _dpta_oid = ds.Tables("account").Rows(i).Item("dpta_oid").ToString Then
                            BindingContext(ds.Tables("account")).Position = i
                            Exit For
                        End If
                    Next
                    MessageBox.Show("Congratulations " + master_new.ClsVar.sNama + ", Data Has Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)


                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("account").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " + _
             "  public.dpta_acct.dpta_oid, " + _
             "  public.dpta_acct.dpta_dpt_oid, " + _
             "  public.dpta_acct.dpta_acc_type, " + _
             "  public.dpta_acct.dpta_ac_id, " + _
             "  public.dpta_acct.dpta_sb_id, " + _
             "  public.dpta_acct.dpta_cc_id, " + _
             "  public.dpta_acct.dpta_desc, " + _
             "  public.dpta_acct.dpta_dt, " + _
             "  public.cc_mstr.cc_id, " + _
             "  public.cc_mstr.cc_desc, " + _
             "  public.dpt_mstr.dpt_id, " + _
             "  public.dpt_mstr.dpt_desc, " + _
             "  public.ac_mstr.ac_id, " + _
             "  public.ac_mstr.ac_name, " + _
             "  public.sb_mstr.sb_id, " + _
             "  public.sb_mstr.sb_desc " + _
             "FROM " + _
             "  public.ac_mstr " + _
             "  INNER JOIN public.dpta_acct ON (public.ac_mstr.ac_id = public.dpta_acct.dpta_ac_id) " + _
             "  INNER JOIN public.cc_mstr ON (public.dpta_acct.dpta_cc_id = public.cc_mstr.cc_id) " + _
             "  INNER JOIN public.dpt_mstr ON (public.dpta_acct.dpta_dpt_oid = public.dpt_mstr.dpt_oid) " + _
             "  INNER JOIN public.sb_mstr ON (public.dpta_acct.dpta_sb_id = public.sb_mstr.sb_id)"

        load_data_detail(sql, gc_account, "account")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_account.Columns("dpta_dpt_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[dpta_dpt_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dpt_oid").ToString & "'")
            gv_account.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
End Class

