Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FItemStatus
    Dim _its_oid As String
    Dim sSql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("transaction_id", ""))
        With le_transaction
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Private Sub FInventoryStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.its_oid, " _
                & "  a.its_dom_id, " _
                & "  a.its_add_by, " _
                & "  a.its_add_date, " _
                & "  a.its_upd_by, " _
                & "  a.its_upd_date, " _
                & "  a.its_id, " _
                & "  a.its_code, " _
                & "  a.its_desc, " _
                & "  a.its_dt, " _
                & "  b.dom_desc " _
                & "FROM " _
                & "  public.its_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.its_dom_id = b.dom_id)"
        Return get_sequel
    End Function

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "its_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "its_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "its_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "its_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "its_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "its_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail1, "itsd_its_oid", False)
        add_column_copy(gv_detail1, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "User Create", "itsd_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Date Create", "itsd_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_detail1, "User Update", "itsd_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Date Update", "itsd_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
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
            & "  a.itsd_oid, " _
            & "  a.itsd_add_by, " _
            & "  a.itsd_add_date, " _
            & "  a.itsd_upd_by, " _
            & "  a.itsd_upd_date, " _
            & "  a.itsd_its_oid, " _
            & "  a.itsd_seq, " _
            & "  a.itsd_tran_id, " _
            & "  a.itsd_dt, " _
            & "  b.tran_name " _
            & "FROM " _
            & "  public.itsd_det a " _
            & "  INNER JOIN public.tran_mstr b ON (a.itsd_tran_id = b.tran_id)"
        load_data_detail(sql, gcc_detail1, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail1.Columns("itsd_its_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("itsd_its_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("its_oid").ToString & "'")
            gv_detail1.BestFitColumns()

            'gv_detail1.Columns("itsd_its_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("its_oid"))
            'gv_detail1.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        sc_te_its_code.Focus()
        sc_te_its_code.Text = ""
        sc_te_its_desc.Text = ""

    End Sub
    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _its_oid = .Item("its_oid").ToString
                sc_te_its_code.Text = .Item("its_code")
                sc_te_its_desc.Text = SetString(.Item("its_desc"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function insert() As Boolean
        Dim _its_oid As Guid
        _its_oid = Guid.NewGuid

        Try
            sSql = "INSERT INTO  " _
                    & "  public.its_mstr " _
                    & "( " _
                    & "  its_oid, " _
                    & "  its_dom_id, " _
                    & "  its_add_by, " _
                    & "  its_add_date, " _
                    & "  its_id, " _
                    & "  its_code, " _
                    & "  its_desc, " _
                    & "  its_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_its_oid.ToString) & ",  " _
                    & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                    & SetInteger(GetNewID("its_mstr", "its_id")) & ",  " _
                    & SetSetring(sc_te_its_code.Text) & ",  " _
                    & SetSetring(sc_te_its_desc.Text) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                    & ")"

            DbRun(sSql)

            set_to_data_insert()
            after_success()
            set_row(Trim(sc_te_its_code.Text), "its_code")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True

        Catch ex As npgSql.nPgSqlException
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub sb_add_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_save.Click
        If MessageBox.Show("Add this item ..?", "Confirmation", _
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _itsd_oid As Guid
        _itsd_oid = Guid.NewGuid

        Dim _its_code As String
        _its_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("its_code")

        Try
            sSql = "INSERT INTO  " _
                    & "  public.itsd_det " _
                    & "( " _
                    & "  itsd_oid, " _
                    & "  itsd_add_by, " _
                    & "  itsd_add_date, " _
                    & "  itsd_its_oid, " _
                    & "  itsd_seq, " _
                    & "  itsd_tran_id, " _
                    & "  itsd_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_itsd_oid.ToString) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                    & SetSetring(ds.Tables(0).Rows(BindingContext( _
                      ds.Tables(0)).Position).Item("its_oid").ToString) & ",  " _
                    & SetInteger(GetNewID("itsd_det", "itsd_seq", "itsd_its_oid", _
                            ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("its_oid").ToString)) & ",  " _
                    & SetSetring(le_transaction.EditValue) & ",  " _
                    & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                    & ")"

            DbRun(sSql)
            load_data_grid_detail()
            MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            set_row(_its_code, "its_code")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_delete.Click
        If MessageBox.Show("Delete This item ..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _its_code As String
        _its_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("its_code")

        Try
            sSql = "delete from itsd_det where itsd_oid =  '" + ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("itsd_oid").ToString + "'"
            DbRun(sSql)

            load_data_grid_detail()
            MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            set_row(_its_code, "its_code")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
                                            & "  public.its_mstr   " _
                                            & "SET  " _
                                            & "  its_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  its_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  its_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  its_code = " & SetSetring(sc_te_its_code.Text) & ",  " _
                                            & "  its_desc = " & SetSetring(sc_te_its_desc.Text) & ",  " _
                                            & "  its_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  its_oid = " & SetSetring(_its_oid.ToString) & " "

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
                        set_row(Trim(sc_te_its_code.Text), "its_code")
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
                            .Command.CommandText = "delete from its_mstr where its_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("its_oid").ToString + "'"
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
End Class
