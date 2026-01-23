Imports npgsql
Imports master_new.ModFunction

Public Class FItemLeadTime
    Dim _invld_oid_mstr As String
    Public dt_bantu As DataTable
    Dim ds_edit As DataSet
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _pt_id As Integer

    Private Sub FItemLeadTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr())

        init_le(le_invld_en, "en_mstr")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'invld_en_id.Properties.DataSource = dt_bantu
        'invld_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'invld_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'invld_en_id.ItemIndex = 0

        With invld_si_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Lead Time", "invld_lead", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Weight", "invld_weight", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Monitoring", "invld_monitored", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost", "invld_lead", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        'get_sequel = "SELECT  " _
        '            & "  invld_oid, " _
        '            & "  invld_dom_id, " _
        '            & "  invld_en_id, " _
        '            & "  invld_si_id, " _
        '            & "  invld_pt_id, " _
        '            & "  invld_qty, " _
        '            & "  invld_lead, " _
        '            & "  invld_monitored, " _
        '            & "  si_desc, " _
        '            & "  en_desc, " _
        '            & "  pt_code, " _
        '            & "  pt_desc1, " _
        '            & "  pt_desc2 " _
        '            & "FROM  " _
        '            & "  public.invld_table " _
        '            & "  inner join public.pt_mstr on pt_id = invld_pt_id " _
        '            & "  inner join public.si_mstr on si_id = invld_si_id " _
        '            & "  inner join public.en_mstr on en_id = invld_en_id " _
        '            & " order by pt_code"

        get_sequel = "SELECT  " _
                    & "  public.invld_table.invld_oid, " _
                    & "  public.invld_table.invld_dom_id, " _
                    & "  public.invld_table.invld_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.invld_table.invld_si_id, " _
                    & "  public.invld_table.invld_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.invld_table.invld_qty, " _
                    & "  public.invld_table.invld_lead, " _
                    & "  public.invld_table.invld_weight, " _
                    & "  public.invld_table.invld_monitored, " _
                    & "  public.invld_table.invld_date " _
                    & "FROM " _
                    & "  public.invld_table " _
                    & "  INNER JOIN public.pt_mstr ON (public.invld_table.invld_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.invld_table.invld_en_id = public.en_mstr.en_id)"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        invld_si_id.Focus()
        invld_si_id.ItemIndex = 0
        invld_pt_id.Text = ""
        invld_lead.EditValue = 0.0
        _pt_id = -1
        invld_monitored.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        If _pt_id = -1 Then
            MessageBox.Show("Part Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim sql As String
        sql = "select * from invld_table where invld_pt_id=" & _pt_id
        Dim i, k, j As Integer
        Dim dt_cek As New DataTable
        dt_cek = master_new.PGSqlConn.GetTableData(sql)


        If dt_cek.Rows.Count > 0 Then
            MsgBox("Duplicate Partnumber")
            Return False
            Exit Function
        End If

        Dim ssqls As New ArrayList
        Dim _invld_oid As Guid = Guid.NewGuid

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()

                    Try
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.invld_table " _
                                                & "( " _
                                                & "  invld_oid, " _
                                                & "  invld_dom_id, " _
                                                & "  invld_en_id, " _
                                                & "  invld_pt_id, " _
                                                & "  invld_lead, " _
                                                & "  invld_weight, " _
                                                & "  invld_monitored, " _
                                                & "  invld_date " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_invld_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(le_invld_en.EditValue) & ",  " _
                                                & SetInteger(_pt_id) & ",  " _
                                                & SetDbl(invld_lead.EditValue) & ",  " _
                                                & SetDbl(invld_weight.EditValue) & ",  " _
                                                & SetBitYN(invld_monitored.EditValue) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            'Update relation PR apabila terdapat relasi pr
                            'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("invld_pt_id")) = False Then
                            '    .Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "update pt_mstr set pt_lead_time = coalesce(pt_lead_time,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("invld_lead").ToString) _
                            '                         & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    .Command.Parameters.Clear()

                            'Uodate status PPartnumber yang di monitor /rans 180422
                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update pt_mstr set pt_monitor = 'Y'" _
                            '                     & " where pt_oiid = '" + ds_edit.Tables(0).Rows(i).Item("invld_pt_id").ToString + "'" _
                            '                     & " and invld_minitored = 'Y' "
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                            'End If
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
                        set_row(_invld_oid.ToString, "invld_oid")
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
                _invld_oid_mstr = .Item("invld_oid").ToString
                invld_si_id.EditValue = .Item("invld_si_id")
                _pt_id = .Item("invld_pt_id")
                invld_pt_id.EditValue = .Item("invld_pt_id")
                le_invld_en.EditValue = .Item("invld_en_id")
                invld_lead.EditValue = .Item("invld_lead")
                invld_weight.EditValue = .Item("invld_weight")
                'invld_monitored.EditValue = .Item("invld_monitored")
                invld_monitored.EditValue = SetBitYNB(.Item("invld_monitored"))
            End With
            invld_si_id.Focus()
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
                                            & "  public.invld_table   " _
                                            & "SET  " _
                                            & "  invld_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  invld_en_id = " & SetInteger(le_invld_en.EditValue) & ",  " _
                                            & "  invld_pt_id = " & SetInteger(_pt_id) & ",  " _
                                            & "  invld_lead = " & SetDec(invld_lead.EditValue) & ",  " _
                                            & "  invld_weight = " & SetDec(invld_weight.EditValue) & ",  " _
                                            & "  invld_monitored = " & SetBitYN(invld_monitored.EditValue) & ",  " _
                                            & "  invld_si_id = " & SetInteger(invld_si_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  invld_oid = " & SetSetring(_invld_oid_mstr) & " "
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
                        set_row(_invld_oid_mstr.ToString, "invld_oid")
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
                            .Command.CommandText = "delete from invld_table where invld_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invld_oid").ToString + "'"
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

    Private Sub invld_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles invld_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._obj = invld_pt_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
