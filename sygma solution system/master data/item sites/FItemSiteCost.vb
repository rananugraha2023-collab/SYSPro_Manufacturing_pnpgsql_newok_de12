Imports npgsql
Imports master_new.ModFunction

Public Class FItemSiteCost
    Dim _invct_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _pt_id As Integer

    Private Sub FItemSiteCost_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr())

        With invct_si_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  invct_oid, " _
                    & "  invct_dom_id, " _
                    & "  invct_pt_id, " _
                    & "  invct_cost, " _
                    & "  invct_si_id, " _
                    & "  si_desc, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2 " _
                    & "FROM  " _
                    & "  public.invct_table " _
                    & "  inner join public.pt_mstr on pt_id = invct_pt_id " _
                    & "  inner join public.si_mstr on si_id = invct_si_id " _
                    & " order by si_desc, pt_code"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        invct_si_id.Focus()
        invct_si_id.ItemIndex = 0
        invct_pt_id.Text = ""
        invct_cost.EditValue = 0.0
        _pt_id = -1
    End Sub

    Public Overrides Function insert() As Boolean
        If _pt_id = -1 Then
            MessageBox.Show("Part Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim sql As String
        sql = "select * from invct_table where invct_pt_id=" & _pt_id
        Dim dt_cek As New DataTable
        dt_cek = master_new.PGSqlConn.GetTableData(sql)

        If dt_cek.Rows.Count > 0 Then
            MsgBox("Duplicate cost")
            Return False
            Exit Function
        End If

        Dim ssqls As New ArrayList
        Dim _invct_oid As Guid = Guid.NewGuid
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
                                            & "  public.invct_table " _
                                            & "( " _
                                            & "  invct_oid, " _
                                            & "  invct_dom_id, " _
                                            & "  invct_pt_id, " _
                                            & "  invct_cost, " _
                                            & "  invct_si_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_invct_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(_pt_id) & ",  " _
                                            & SetDbl(invct_cost.EditValue) & ",  " _
                                            & SetInteger(invct_si_id.EditValue) & "  " _
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
                        set_row(_invct_oid.ToString, "invct_oid")
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
                _invct_oid_mstr = .Item("invct_oid").ToString
                invct_si_id.EditValue = .Item("invct_si_id")
                _pt_id = .Item("invct_pt_id")
                invct_pt_id.EditValue = .Item("pt_code")
                invct_cost.EditValue = .Item("invct_cost")
            End With
            invct_si_id.Focus()
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
                                            & "  public.invct_table   " _
                                            & "SET  " _
                                            & "  invct_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  invct_pt_id = " & SetInteger(_pt_id) & ",  " _
                                            & "  invct_cost = " & SetDec(invct_cost.EditValue) & ",  " _
                                            & "  invct_si_id = " & SetInteger(invct_si_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  invct_oid = " & SetSetring(_invct_oid_mstr) & " "
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
                        set_row(_invct_oid_mstr.ToString, "invct_oid")
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
        'MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                            .Command.CommandText = "delete from invct_table where invct_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invct_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Item Site Cost " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code"))
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

    Private Sub invct_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles invct_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._obj = invct_pt_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
