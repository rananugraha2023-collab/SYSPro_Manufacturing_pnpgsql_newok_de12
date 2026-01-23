Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FEntityGroup
    Dim sSql As String
    Dim _eng_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FEntityGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        engd_en_id.Properties.DataSource = dt_bantu
        engd_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        engd_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        engd_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "eng_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "eng_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "eng_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "eng_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "eng_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "eng_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_en, "engd_eng_oid", False)
        add_column(gv_en, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  eng_oid, " _
                    & "  eng_add_by, " _
                    & "  eng_add_date, " _
                    & "  eng_dt, " _
                    & "  eng_code, " _
                    & "  eng_name, " _
                    & "  eng_id " _
                    & "FROM  " _
                    & "  public.eng_group "
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("entity").Clear()
        Catch ex As Exception
        End Try

        sql = "select engd_oid, engd_eng_oid, engd_en_id, en_desc " + _
              " from engd_det" + _
              " inner join en_mstr  on en_id = engd_en_id"

        load_data_detail(sql, gc_en, "entity")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_en.Columns("engd_eng_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("engd_eng_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("eng_oid").ToString & "'")
            gv_en.BestFitColumns()

            'gv_en.Columns("engd_eng_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("eng_oid"))
            'gv_en.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        eng_code.Focus()
        eng_code.Text = ""
        eng_name.Text = ""
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _eng_oid As Guid
        _eng_oid = Guid.NewGuid
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
                                            & "  public.eng_group " _
                                            & "( " _
                                            & "  eng_oid, " _
                                            & "  eng_add_by, " _
                                            & "  eng_add_date, " _
                                            & "  eng_dt, " _
                                            & "  eng_code, " _
                                            & "  eng_name, " _
                                            & "  eng_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_eng_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(eng_code.Text) & ",  " _
                                            & SetSetring(eng_name.Text) & ",  " _
                                            & SetInteger(func_coll.GetID("eng_group", "eng_id")) & "  " _
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
                        set_row(Trim(_eng_oid.ToString), "eng_oid")
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
            eng_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _eng_oid = .Item("eng_oid").ToString
                eng_code.Text = .Item("eng_code")
                eng_name.Text = .Item("eng_name")
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
                                            & "  public.eng_group   " _
                                            & "SET  " _
                                            & "  eng_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  eng_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  eng_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " ,  " _
                                            & "  eng_code = " & SetSetring(eng_code.Text) & ",  " _
                                            & "  eng_name = " & SetSetring(eng_name.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  eng_oid = " & SetSetring(_eng_oid) & " "
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
                        edit = True
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
                            .Command.CommandText = "delete from eng_group where eng_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("eng_oid").ToString + "'"
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
        If MessageBox.Show("Add This Entity To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _eng_oid_ins As String
        _eng_oid_ins = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("eng_oid").ToString

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
                                            & "  public.engd_det " _
                                            & "( " _
                                            & "  engd_oid, " _
                                            & "  engd_eng_oid, " _
                                            & "  engd_en_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_eng_oid_ins) & ",  " _
                                            & SetInteger(engd_en_id.EditValue) & "  " _
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_eng_oid_ins, "eng_oid")
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

End Class
