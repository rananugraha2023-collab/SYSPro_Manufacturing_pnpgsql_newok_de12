Imports npgsql
Imports master_new.ModFunction


Public Class FPartNumberGroupForecast
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wc_oid_mstr As String
    Dim sSQLs As New ArrayList

    Private Sub FWc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_group_mstr())
        ptgr_gr_id.Properties.DataSource = dt_bantu
        ptgr_gr_id.Properties.DisplayMember = dt_bantu.Columns("gr_name").ToString
        ptgr_gr_id.Properties.ValueMember = dt_bantu.Columns("gr_id").ToString
        ptgr_gr_id.ItemIndex = 0
    End Sub

    Private Sub wc_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Name", "gr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptgr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptgr_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "ptgr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptgr_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ptgr_oid, " _
                & "  a.ptgr_code, " _
                & "  a.ptgr_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.ptgr_gr_id, " _
                & "  c.gr_name, " _
                & "  a.ptgr_add_by, " _
                & "  a.ptgr_add_date, " _
                & "  a.ptgr_upd_by, " _
                & "  a.ptgr_upd_date " _
                & "FROM " _
                & "  public.ptgr_mstr a " _
                & "  INNER JOIN public.pt_mstr b ON (a.ptgr_pt_id = b.pt_id) " _
                & "  INNER JOIN public.gr_mstr c ON (a.ptgr_gr_id = c.gr_id) " _
                & "ORDER BY " _
                & "  b.pt_code"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ptgr_pt_id.Tag = ""
        ptgr_pt_id.Text = ""
        ptgr_gr_id.ItemIndex = 0

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _wc_oid As Guid
        _wc_oid = Guid.NewGuid

        If ptgr_pt_id.EditValue = "" Then
            Box("Part Number can't empty")
            Exit Function
        End If

        sSQLs.Clear()

        'Dim _wc_id As Integer
        '_wc_id = SetInteger(func_coll.GetID("wc_mstr", wc_en_id.GetColumnValue("en_code"), "wc_id", "wc_en_id", wc_en_id.EditValue.ToString))

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
                            & "  public.ptgr_mstr " _
                            & "( " _
                            & "  ptgr_oid, " _
                            & "  ptgr_code, " _
                            & "  ptgr_pt_id, " _
                            & "  ptgr_gr_id, " _
                            & "  ptgr_add_by, " _
                            & "  ptgr_add_date " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(_wc_oid.ToString) & ",  " _
                            & SetSetring("") & ",  " _
                            & SetInteger(ptgr_pt_id.Tag) & ",  " _
                            & SetInteger(ptgr_gr_id.EditValue) & ",  " _
                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                            & ")"
                        sSQLs.Add(.Command.CommandText)
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
                        set_row(Trim(_wc_oid.ToString), "ptgr_oid")
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
            'wc_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _wc_oid_mstr = .Item("ptgr_oid").ToString
                ptgr_pt_id.Tag = .Item("ptgr_pt_id")
                ptgr_pt_id.EditValue = .Item("pt_desc1")
                ptgr_gr_id.EditValue = .Item("ptgr_gr_id")
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        sSQLs.Clear()
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
                            & "  public.ptgr_mstr   " _
                            & "SET  " _
                            & "  ptgr_pt_id = " & SetInteger(ptgr_pt_id.Tag) & ",  " _
                            & "  ptgr_gr_id = " & SetInteger(ptgr_gr_id.EditValue) & ",  " _
                            & "  ptgr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "  ptgr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                            & "WHERE  " _
                            & "  ptgr_oid = " & SetSetring(_wc_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)

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
                        set_row(Trim(_wc_oid_mstr.ToString), "ptgr_oid")
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Akan Menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If
        sSQLs.Clear()

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
                            .Command.CommandText = "delete from ptgr_mstr where ptgr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptgr_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
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

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub

    Private Sub ptgr_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptgr_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._en_id = 1
        frm._obj = ptgr_pt_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
