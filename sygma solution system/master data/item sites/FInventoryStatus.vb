Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FInventoryStatus
    Dim _is_oid As String
    Dim sSql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("transaction_id", ""))
        With le_transaction
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        With sc_le_is_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Private Sub FInventoryStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.is_oid, " _
                & "  a.is_dom_id, " _
                & "  a.is_en_id, " _
                & "  a.is_add_by, " _
                & "  a.is_add_date, " _
                & "  a.is_upd_by, " _
                & "  a.is_upd_date, " _
                & "  a.is_id, " _
                & "  a.is_code, " _
                & "  a.is_desc, " _
                & "  a.is_avail, " _
                & "  a.is_nettable, " _
                & "  a.is_overissue, " _
                & "  a.is_dt, " _
                & "  b.dom_desc, " _
                & "  c.en_desc " _
                & " FROM " _
                & "  public.is_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.is_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.is_en_id = c.en_id) " _
                & "  AND (b.dom_id = c.en_dom_id) " _
                & " where is_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " ORDER BY " _
                & "  a.is_code"

        Return get_sequel
    End Function

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "is_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "is_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Available", "is_avail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Nettable", "is_nettable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Overissue", "is_overissue", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "is_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "is_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "is_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "is_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail1, "isd_its_oid", False)
        add_column_copy(gv_detail1, "tran_name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "User Create", "isd_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Date Create", "isd_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_detail1, "User Update", "isd_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Date Update", "isd_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

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
            & "  a.isd_oid, " _
            & "  a.isd_add_by, " _
            & "  a.isd_add_date, " _
            & "  a.isd_upd_by, " _
            & "  a.isd_upd_date, " _
            & "  a.isd_its_oid, " _
            & "  a.isd_seq, " _
            & "  a.isd_tran_id, " _
            & "  a.isd_dt, " _
            & "  b.tran_table, " _
            & "  b.tran_name, " _
            & "  b.tran_desc, " _
            & "  b.tran_review_amount " _
            & "FROM " _
            & "  public.isd_det a " _
            & "  INNER JOIN public.tran_mstr b ON (a.isd_tran_id = b.tran_id)"

        load_data_detail(sql, gcc_detail1, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail1.Columns("isd_its_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("isd_its_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_oid").ToString & "'")
            gv_detail1.BestFitColumns()

            'gv_detail1.Columns("isd_its_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_oid"))
            'gv_detail1.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        sc_le_is_en_id.Focus()
        sc_le_is_en_id.EditValue = 0
        sc_te_is_code.Text = ""
        sc_te_is_desc.Text = ""
        sc_ce_is_avail.EditValue = False
        sc_ce_is_nettable.EditValue = False
        sc_ce_is_overissue.EditValue = False
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _is_oid = .Item("is_oid").ToString
                sc_le_is_en_id.EditValue = .Item("is_en_id")
                sc_te_is_code.Text = .Item("is_code")
                sc_te_is_desc.Text = .Item("is_desc")
                sc_ce_is_avail.EditValue = SetBitYNB(.Item("is_avail"))
                sc_ce_is_nettable.EditValue = SetBitYNB(.Item("is_nettable"))
                sc_ce_is_overissue.EditValue = SetBitYNB(.Item("is_overissue"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function insert() As Boolean
        Dim _is_oid As Guid
        _is_oid = Guid.NewGuid

        Try

            sSql = "INSERT INTO  " _
                    & "  public.is_mstr " _
                    & "( " _
                    & "  is_oid, " _
                    & "  is_dom_id, " _
                    & "  is_en_id, " _
                    & "  is_add_by, " _
                    & "  is_add_date, " _
                    & "  is_upd_by, " _
                    & "  is_upd_date, " _
                    & "  is_id, " _
                    & "  is_code, " _
                    & "  is_desc, " _
                    & "  is_avail, " _
                    & "  is_nettable, " _
                    & "  is_overissue, " _
                    & "  is_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_is_oid.ToString) & ",  " _
                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    & SetInteger(sc_le_is_en_id.EditValue) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                    & "Null" & ",  " _
                    & "Null" & ",  " _
                    & SetInteger(GetNewID("is_mstr", "is_id")) & ",  " _
                    & SetSetring(sc_te_is_code.Text) & ",  " _
                    & SetSetring(sc_te_is_desc.Text) & ",  " _
                    & SetBitYN(sc_ce_is_avail.EditValue) & ",  " _
                    & SetBitYN(sc_ce_is_nettable.EditValue) & ",  " _
                    & SetBitYN(sc_ce_is_overissue.EditValue) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                    & ")"

            DbRun(sSql)

            set_to_data_insert()
            after_success()
            set_row(Trim(sc_te_is_code.Text), "is_code")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True

        Catch ex As npgSql.nPgSqlException
            MessageBox.Show(ex.Message)
        End Try
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
                                            & "  public.is_mstr   " _
                                            & "SET  " _
                                            & "  is_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  is_en_id = " & sc_le_is_en_id.EditValue & ",  " _
                                            & "  is_add_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  is_add_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  is_code = " & SetSetring(sc_te_is_code.Text) & ",  " _
                                            & "  is_desc = " & SetSetring(sc_te_is_desc.Text) & ",  " _
                                            & "  is_avail = " & SetBitYN(sc_ce_is_avail.EditValue) & ",  " _
                                            & "  is_nettable = " & SetBitYN(sc_ce_is_nettable.EditValue) & ",  " _
                                            & "  is_overissue = " & SetBitYN(sc_ce_is_overissue.EditValue) & ",  " _
                                            & "  is_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  is_oid = " & SetSetring(_is_oid.ToString) & " "
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
                        set_row(Trim(sc_te_is_code.Text), "is_code")
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
                            .Command.CommandText = "delete from is_mstr where is_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_oid").ToString + "'"
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
        If MessageBox.Show("Add this item ..?", "Confirmation", _
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _isd_oid As Guid
        Dim _is_code As String

        _isd_oid = Guid.NewGuid
        _is_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_code")
        Try

            sSql = "INSERT INTO  " _
                    & "  public.isd_det " _
                    & "( " _
                    & "  isd_oid, " _
                    & "  isd_add_by, " _
                    & "  isd_add_date, " _
                    & "  isd_its_oid, " _
                    & "  isd_seq, " _
                    & "  isd_tran_id, " _
                    & "  isd_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_isd_oid.ToString) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                    & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_oid").ToString) & ",  " _
                    & SetInteger(func_coll.GetID("isd_det", "isd_seq", "isd_its_oid", ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_oid").ToString)) & ",  " _
                    & SetSetring(le_transaction.EditValue) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                    & ")"

            DbRun(sSql)
            load_data_grid_detail()
            MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            set_row(_is_code, "is_code")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_delete.Click
        If MessageBox.Show("Delete This Group From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _is_code As String
        _is_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("is_code")

        Try

            sSql = "delete from isd_det where isd_oid = '" + ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("isd_oid").ToString + "'"
            DbRun(sSql)

            load_data_grid_detail()
            MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            set_row(_is_code, "is_code")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

End Class
