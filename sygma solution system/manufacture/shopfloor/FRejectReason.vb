Imports npgsql
Imports master_new.ModFunction

Public Class FRejectReason
    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FSite_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("wc_mstr_null", "0"))
        qc_wc_id.Properties.DataSource = dt_bantu
        qc_wc_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
        qc_wc_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
        qc_wc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("qc_type", "0"))
        qc_type.Properties.DataSource = dt_bantu
        qc_type.Properties.DisplayMember = dt_bantu.Columns("type_desc").ToString
        qc_type.Properties.ValueMember = dt_bantu.Columns("type_code").ToString
        qc_type.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID", "qc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "qc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "qc_type_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "qc_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "qc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "qc_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "qc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "qc_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String


        get_sequel = "Select q.qc_oid, " _
                & "                q.qc_id, " _
                & "                q.qc_desc, " _
                & "                q.qc_add_by, " _
                & "                q.qc_add_date, " _
                & "                q.qc_upd_by, " _
                & "                q.qc_upd_date, " _
                & "                q.qc_active, " _
                & "                q.qc_type, case when qc_type = 'I' then 'QC IN' else 'QC OUT' end as qc_type_desc, " _
                & "                q.qc_wc_id, " _
                & "                w.wc_desc " _
                & "            FROM public.qc_reason_mstr q " _
                & "            Left Join public.wc_mstr w ON q.qc_wc_id = w.wc_id " _
                & "            WHERE 1=1 " _
                & "            ORDER BY qc_type, q.qc_desc"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        qc_wc_id.Focus()
        qc_wc_id.ItemIndex = 0
        qc_type.ItemIndex = 0
        qc_desc.Text = ""
        qc_active.EditValue = True
    End Sub

    Public Overrides Function insert() As Boolean

        Dim _id As Integer
        _id = func_coll.GetID("qc_reason_mstr", "qc_id")


        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO public.qc_reason_mstr (" & _
                                 "qc_oid, qc_id, qc_desc, qc_add_by, qc_add_date, " & _
                                 "qc_active, qc_type, qc_wc_id) " & _
                                 "VALUES (" & _
                                 SetSetring(Guid.NewGuid.ToString) & ", " & _
                                 SetInteger(_id) & ", " & _
                                 SetSetring(qc_desc.Text) & ", " & _
                                 SetSetring(master_new.ClsVar.sNama) & ", " & _
                                 SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " & _
                                 SetBitYN(qc_active.EditValue) & ", " & _
                                 SetSetring(qc_type.EditValue) & ", " & _
                                 SetInteger(qc_wc_id.EditValue) & _
                                 ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandText = insert_log("Insert reject reason " & qc_desc.EditValue)
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
                        set_row(Trim(_id), "qc_id")
                        insert = True
                    Catch ex As NpgsqlException
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
                qc_wc_id.EditValue = .Item("qc_wc_id")
                qc_type.EditValue = .Item("qc_type")
                qc_desc.EditValue = .Item("qc_desc")
                qc_desc.Tag = .Item("qc_oid").ToString
                qc_active.EditValue = SetBitYNB(.Item("qc_active"))
            End With
            qc_wc_id.Focus()
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
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE public.qc_reason_mstr " & _
                             "SET " & _
                             "qc_desc = " & SetSetring(qc_desc.EditValue) & ", " & _
                             "qc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ", " & _
                             "qc_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " & _
                             "qc_active = " & SetBitYN(qc_active.EditValue) & ", " & _
                             "qc_type = " & SetSetring(qc_type.EditValue.ToString) & ", " & _
                             "qc_wc_id = " & SetInteger(qc_wc_id.EditValue) & " " & _
                             "WHERE qc_oid = " & SetSetring(qc_desc.Tag.ToString)

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandText = insert_log("update reject reason " & qc_desc.EditValue)
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
                        set_row(Trim(qc_desc.EditValue), "qc_desc")
                        edit = True
                    Catch ex As NpgsqlException
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
                        Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from qc_reason_mstr where qc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qc_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandText = insert_log("Delete reject reason " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qc_desc").ToString)
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
                        Catch ex As NpgsqlException
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
