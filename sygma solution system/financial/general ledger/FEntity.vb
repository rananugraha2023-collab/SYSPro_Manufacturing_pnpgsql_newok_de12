Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FEntity
    Dim sSql As String
    Dim _en_oid As String
    Public dt_bantu As DataTable
    Dim func_coll As New function_collection
    Public dt_edit As New DataTable

    Private Sub FEntity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub
    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (load_en_mstr_tran())
        en_parent.Properties.DataSource = dt_bantu
        en_parent.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        en_parent.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        en_parent.ItemIndex = 0
    End Sub

    Private Function load_en_mstr_tran() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select -1 as en_id, 'NP' as en_code, 'No Parent' as en_desc " + _
                           " union " + _
                               "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                            " order by en_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "City", "en_city", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Warehouse ID", "en_warehouse_id", DevExpress.Utils.HorzAlignment.Default)
        'en_city
        add_column_copy(gv_master, "Parent", "en_desc_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "en_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Limit Account", "en_limit_account", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "en_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "en_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "en_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "en_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail, "Code", "enacc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Name", "encd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "enacc_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit, "Type", "enacc_code", DevExpress.Utils.HorzAlignment.Default, init_le_repo("encd_code"))
        add_column(gv_edit, "enacc_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "enacc_remarks", DevExpress.Utils.HorzAlignment.Default)

        AddHandler gv_master.FocusedRowChanged, AddressOf load_detail
        AddHandler gv_master.Click, AddressOf load_detail

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.en_oid, a.en_city,a.en_warehouse_id, " _
                    & "  a.en_dom_id, " _
                    & "  a.en_add_by, " _
                    & "  a.en_add_date, " _
                    & "  a.en_upd_by, " _
                    & "  a.en_upd_date, " _
                    & "  a.en_id, " _
                    & "  a.en_code, " _
                    & "  a.en_desc, " _
                    & "  a.en_parent, " _
                    & "  a.en_active,coalesce(a.en_limit_account,'N') as en_limit_account, " _
                    & "  a.en_dt, " _
                    & "  b.en_desc as en_desc_parent " _
                    & "FROM  " _
                    & "  public.en_mstr a" _
                    & "  left outer join public.en_mstr b on b.en_id = a.en_parent " _
                    & " order by en_desc"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_te_en_code.Focus()
        sc_te_en_code.Text = ""
        sc_te_en_desc.Text = ""
        en_warehouse_id.Text = ""
        sc_ce_en_active.EditValue = True
        en_limit_account.EditValue = True

        sSql = "SELECT  " _
              & "  a.enacc_oid, " _
              & "  a.enacc_en_id, " _
              & "  a.enacc_code, " _
              & "  b.encd_desc, " _
              & "  a.enacc_ac_id, " _
              & "  c.ac_code, " _
              & "  c.ac_name, " _
              & "  a.enacc_remarks " _
              & "FROM " _
              & "  public.enacc_mstr a " _
              & "  INNER JOIN public.encd_code b ON (a.enacc_code = b.encd_code) " _
              & "  INNER JOIN public.ac_mstr c ON (a.enacc_ac_id = c.ac_id) " _
              & "WHERE " _
              & "  a.enacc_en_id = -99 " _
              & " ORDER BY " _
              & "  a.enacc_code "

        dt_edit = GetTableData(sSql)
        gc_edit.DataSource = dt_edit
        gv_edit.BestFitColumns()

        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        MyBase.help_load_data(par)
        If par = True Then
            load_detail()
        End If
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _en_oid As Guid
        _en_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        Dim _id As Integer = func_coll.GetID("en_mstr", "en_id")

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                                & "  public.en_mstr " _
                                                & "( " _
                                                & "  en_oid,en_warehouse_id, " _
                                                & "  en_dom_id, " _
                                                & "  en_add_by, " _
                                                & "  en_add_date, " _
                                                & "  en_id, " _
                                                & "  en_code, " _
                                                & "  en_desc,en_city, " _
                                                & "  en_parent,en_limit_account, " _
                                                & "  en_active, " _
                                                & "  en_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_en_oid.ToString) & ",  " _
                                                & SetSetring(Trim(en_warehouse_id.Text)) & ",  " _
                                                & SetNumber(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & SetInteger(_id) & ",  " _
                                                & SetSetring(Trim(sc_te_en_code.Text)) & ",  " _
                                                & SetSetring(Trim(sc_te_en_desc.Text)) & ",  " _
                                                & SetSetring(Trim(en_city.Text)) & ",  " _
                                                & IIf(en_parent.EditValue = -1, "null", en_parent.EditValue) & ",  " _
                                                & SetBitYN(en_limit_account.EditValue) & ",  " _
                                                & SetBitYN(sc_ce_en_active.EditValue) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "))"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.enacc_mstr " _
                                        & "( " _
                                        & "  enacc_oid, " _
                                        & "  enacc_en_id, " _
                                        & "  enacc_code, " _
                                        & "  enacc_ac_id, " _
                                        & "  enacc_remarks " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(_id) & ",  " _
                                        & SetSetring(dr("enacc_code")) & ",  " _
                                        & SetInteger(dr("enacc_ac_id")) & ",  " _
                                        & SetSetring(dr("enacc_remarks")) & "  " _
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
                        set_row(Trim(_en_oid.ToString), "en_oid")
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
            sc_te_en_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _en_oid = .Item("en_oid").ToString
                sc_te_en_code.Text = .Item("en_code")
                sc_te_en_desc.Text = .Item("en_desc")
                sc_ce_en_active.EditValue = SetBitYNB(.Item("en_active"))
                en_limit_account.EditValue = SetBitYNB(.Item("en_limit_account"))
                en_city.EditValue = .Item("en_city")
                en_warehouse_id.EditValue = .Item("en_warehouse_id")

                If IsDBNull(.Item("en_parent")) = True Then
                    en_parent.EditValue = -1
                Else
                    en_parent.EditValue = .Item("en_parent")
                End If
            End With

            sSql = "SELECT  " _
               & "  a.enacc_oid, " _
               & "  a.enacc_en_id, " _
               & "  a.enacc_code, " _
               & "  b.encd_desc, " _
               & "  a.enacc_ac_id, " _
               & "  c.ac_code, " _
               & "  c.ac_name, " _
               & "  a.enacc_remarks " _
               & "FROM " _
               & "  public.enacc_mstr a " _
               & "  INNER JOIN public.encd_code b ON (a.enacc_code = b.encd_code) " _
               & "  INNER JOIN public.ac_mstr c ON (a.enacc_ac_id = c.ac_id) " _
               & "WHERE " _
               & "  a.enacc_en_id = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("en_id")) _
               & " ORDER BY " _
               & "  a.enacc_code "

            dt_edit = GetTableData(sSql)
            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        gv_edit.UpdateCurrentRow()
        dt_edit.AcceptChanges()

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
                                                & "  public.en_mstr   " _
                                                & "SET  " _
                                                & "  en_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  en_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ") ,  " _
                                                & "  en_desc = " & SetSetring(Trim(sc_te_en_desc.Text)) & ",  " _
                                                & "  en_warehouse_id = " & SetSetring(Trim(en_warehouse_id.Text)) & ",  " _
                                                & "  en_city = " & SetSetring(Trim(en_city.Text)) & ",  " _
                                                & "  en_parent = " & IIf(en_parent.EditValue = -1, "null", SetInteger(en_parent.EditValue)) & ",  " _
                                                & "  en_active = " & SetBitYN(sc_ce_en_active.EditValue) & ",  " _
                                                & "  en_limit_account = " & SetBitYN(en_limit_account.EditValue) & ",  " _
                                                & "  en_dt  = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ") " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  en_oid = " & SetSetring(_en_oid.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from enacc_mstr where enacc_en_id  = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("en_id")) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.enacc_mstr " _
                                        & "( " _
                                        & "  enacc_oid, " _
                                        & "  enacc_en_id, " _
                                        & "  enacc_code, " _
                                        & "  enacc_ac_id, " _
                                        & "  enacc_remarks " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("en_id")) & ",  " _
                                        & SetSetring(dr("enacc_code")) & ",  " _
                                        & SetInteger(dr("enacc_ac_id")) & ",  " _
                                        & SetSetring(dr("enacc_remarks")) & "  " _
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
        Dim ssqls As New ArrayList

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

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from en_mstr where en_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("en_oid").ToString + "'"
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

    Public Sub load_detail()

        sSql = "SELECT  " _
            & "  a.enacc_oid, " _
            & "  a.enacc_en_id, " _
            & "  a.enacc_code, " _
            & "  b.encd_desc, " _
            & "  a.enacc_ac_id, " _
            & "  c.ac_code, " _
            & "  c.ac_name, " _
            & "  a.enacc_remarks " _
            & "FROM " _
            & "  public.enacc_mstr a " _
            & "  INNER JOIN public.encd_code b ON (a.enacc_code = b.encd_code) " _
            & "  INNER JOIN public.ac_mstr c ON (a.enacc_ac_id = c.ac_id) " _
            & "WHERE " _
            & "  a.enacc_en_id = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("en_id")) _
            & " ORDER BY " _
            & "  a.enacc_code"

        gc_detail.DataSource = GetTableData(sSql)
        gv_detail.BestFitColumns()

    End Sub


    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try

            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "ac_code" Then
                Dim frm As New FAccountSearch()
                frm.set_win(Me)
                frm._row = _row
                frm.type_form = True
                frm.ShowDialog()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

   
End Class
