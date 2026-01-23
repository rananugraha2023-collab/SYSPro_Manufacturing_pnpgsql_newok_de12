Imports npgsql
Imports master_new.ModFunction

Public Class FTransaction
    Dim _tran_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        With trand_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("trans_status", ""))
        With trand_trans_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("trans_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("trans_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Table Name", "tran_table", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "tran_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Review Amount", "tran_review_amount", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "trand_tran_oid", False)
        add_column_copy(gv_detail, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Code", "trand_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Name", "trans_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Edit", "trand_edit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Delete", "trand_delete", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  tran_oid, " _
                & "  tran_id, " _
                & "  tran_table, " _
                & "  tran_name, " _
                & "  tran_desc, " _
                & "  tran_review_amount, " _
                & "  tran_dt " _
                & "FROM  " _
                & "  public.tran_mstr ;"
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
            & "  trand_oid, " _
            & "  trand_tran_oid, " _
            & "  trand_en_id, " _
            & "  en_code, " _
            & "  trand_seq, " _
            & "  trand_trans_id, " _
            & "  trans_desc ," _
            & "  trand_edit, " _
            & "  trand_delete, " _
            & "  trand_dt " _
            & "FROM  " _
            & "  public.trand_det " _
            & " inner join public.en_mstr on en_id = trand_en_id " _
            & " inner join public.trans_status on trans_id = trand_trans_id"

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("trand_tran_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("trand_tran_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("trand_tran_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        tran_table.Focus()
        tran_table.Text = ""
        tran_name.Text = ""
        tran_desc.Text = ""
        tran_review_amount.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _tran_oid As Guid
        _tran_oid = Guid.NewGuid
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
                                            & "  public.tran_mstr " _
                                            & "( " _
                                            & "  tran_oid, " _
                                            & "  tran_id, " _
                                            & "  tran_table, " _
                                            & "  tran_name, " _
                                            & "  tran_desc, " _
                                            & "  tran_review_amount, " _
                                            & "  tran_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_tran_oid.ToString) & ",  " _
                                            & SetInteger(func_coll.GetID("tran_mstr", "tran_id")) & ",  " _
                                            & SetSetring(tran_table.Text) & ",  " _
                                            & SetSetring(tran_name.Text) & ",  " _
                                            & SetSetring(tran_desc.Text) & ",  " _
                                            & SetBitYN(tran_review_amount.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
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
                        set_row(Trim(_tran_oid.ToString), "tran_oid")
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
                _tran_oid_mstr = .Item("tran_oid").ToString
                tran_table.Text = SetString(.Item("tran_table"))
                tran_name.Text = SetString(.Item("tran_name"))
                tran_desc.Text = SetString(.Item("tran_desc"))
                tran_review_amount.EditValue = SetBitYNB(.Item("tran_review_amount"))
            End With
            tran_table.Focus()
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
                                            & "  public.tran_mstr   " _
                                            & "SET  " _
                                            & "  tran_table = " & SetSetring(tran_table.Text) & ",  " _
                                            & "  tran_name = " & SetSetring(tran_name.Text) & ",  " _
                                            & "  tran_desc = " & SetSetring(tran_desc.Text) & ",  " _
                                            & "  tran_review_amount = " & SetBitYN(tran_review_amount.EditValue) & ",  " _
                                            & "  tran_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  tran_oid = " & SetSetring(_tran_oid_mstr.ToString) & " "
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
                        set_row(Trim(_tran_oid_mstr.ToString), "tran_oid")
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
                            .Command.CommandText = "delete from tran_mstr where tran_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid").ToString + "'"
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
        Dim ssqls As New ArrayList

        _tran_oid_mstr = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid").ToString

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
                                            & "  public.trand_det " _
                                            & "( " _
                                            & "  trand_oid, " _
                                            & "  trand_tran_oid, " _
                                            & "  trand_en_id, " _
                                            & "  trand_seq, " _
                                            & "  trand_trans_id, " _
                                            & "  trand_edit, " _
                                            & "  trand_delete, " _
                                            & "  trand_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_tran_oid_mstr) & ",  " _
                                            & SetInteger(trand_en_id.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("trand_det", trand_en_id.GetColumnValue("en_code"), "trand_seq", "trand_tran_oid", _tran_oid_mstr)) & ",  " _
                                            & SetSetring(trand_trans_id.EditValue) & ",  " _
                                            & SetBitYN(trand_edit.EditValue) & ",  " _
                                            & SetBitYN(trand_delete.EditValue) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                            & ")"
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
                        set_row(Trim(_tran_oid_mstr.ToString), "tran_oid")
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

    Private Sub sb_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_delete.Click
        If MessageBox.Show("Delete This Group From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

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
                        .Command.CommandText = "delete from trand_det where trand_oid =  " + ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("trand_oid").ToString
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
