Imports npgsql
Imports master_new.ModFunction

Public Class FDSRule
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim _bdsr_oid_mstr As String

    Private Sub FDSRule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        bdsr_en_id.Properties.DataSource = dt_bantu
        bdsr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bdsr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bdsr_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Unit Form", "bdsr_sales_unit_from", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sales Unit To", "bdsr_sales_unit_to", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Faktor Pengali", "bdsr_faktor_pengali", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Start Date", "bdsr_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "bdsr_end_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "User Create", "bdsr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bdsr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "bdsr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bdsr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bdsr_oid, " _
                    & "  bdsr_dom_id, " _
                    & "  bdsr_en_id, " _
                    & "  bdsr_add_by, " _
                    & "  bdsr_add_date, " _
                    & "  bdsr_upd_by, " _
                    & "  bdsr_upd_date, " _
                    & "  bdsr_sales_unit_from, " _
                    & "  bdsr_sales_unit_to, " _
                    & "  bdsr_faktor_pengali, " _
                    & "  bdsr_start_date, " _
                    & "  bdsr_end_date, " _
                    & "  brsr_dt, " _
                    & "  en_desc " _
                    & "  FROM  " _
                    & "  public.bdsr_rule " _
                    & "  inner join en_mstr on en_id = bdsr_en_id" _
                    & "  where bdsr_add_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and bdsr_add_date <= '" + pr_txttglakhir.DateTime.Date.AddDays(1) + "'" _
                    & " and bdsr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        bdsr_en_id.Focus()
        bdsr_en_id.ItemIndex = 0
        bdsr_sales_unit_from.Text = ""
        bdsr_sales_unit_to.Text = ""
        bdsr_faktor_pengali.Text = ""
        bdsr_start_date.DateTime = _now
        bdsr_end_date.DateTime = _now
    End Sub

    Private Function before_save_local() As Boolean
        before_save_local = True

        If bdsr_start_date.DateTime > bdsr_end_date.DateTime Then
            MessageBox.Show("Start Date Can't Higher Than End Date..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bdsr_end_date.Focus()
            Return False
        End If

        If bdsr_start_date.Text = "" Then
            MessageBox.Show("Start Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bdsr_start_date.Focus()
            Return False
        End If

        If bdsr_end_date.Text = "" Then
            MessageBox.Show("End Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bdsr_end_date.Focus()
            Return False
        End If

        'Dim _bdsr_end_date As Date = "01/01/1909"
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select bdsr_end_date from bdsr_rule " + _
        '                                   " where bdsr_en_id = " + bdsr_en_id.EditValue.ToString + _
        '                                   " order by bdsr_end_date desc limit 1"
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _bdsr_end_date = .DataReader("bdsr_end_date").ToString
        '            End While

        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        'If _bdsr_end_date > bdsr_start_date.DateTime Then
        '    MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    bdsr_start_date.Focus()
        '    Return False
        'End If

        Return before_save_local
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_local() = False Then
            Return False
        End If

        Dim _bdsr_oid As Guid = Guid.NewGuid
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
                                            & "  public.bdsr_rule " _
                                            & "( " _
                                            & "  bdsr_oid, " _
                                            & "  bdsr_dom_id, " _
                                            & "  bdsr_en_id, " _
                                            & "  bdsr_add_by, " _
                                            & "  bdsr_add_date, " _
                                            & "  bdsr_sales_unit_from, " _
                                            & "  bdsr_sales_unit_to, " _
                                            & "  bdsr_faktor_pengali, " _
                                            & "  bdsr_start_date, " _
                                            & "  bdsr_end_date, " _
                                            & "  brsr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bdsr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(bdsr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDbl(bdsr_sales_unit_from.EditValue) & ",  " _
                                            & SetDbl(bdsr_sales_unit_to.EditValue) & ",  " _
                                            & SetDbl(bdsr_faktor_pengali.EditValue) & ",  " _
                                            & SetDate(bdsr_start_date.DateTime) & ",  " _
                                            & SetDate(bdsr_end_date.DateTime) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
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
                        set_row(Trim(_bdsr_oid.ToString), "bdsr_oid")
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
                _bdsr_oid_mstr = .Item("bdsr_oid").ToString
                bdsr_en_id.EditValue = .Item("bdsr_en_id")
                bdsr_sales_unit_from.EditValue = .Item("bdsr_sales_unit_from")
                bdsr_sales_unit_to.EditValue = .Item("bdsr_sales_unit_to")
                bdsr_faktor_pengali.EditValue = .Item("bdsr_faktor_pengali")
                bdsr_start_date.DateTime = .Item("bdsr_start_date")
                bdsr_end_date.DateTime = .Item("bdsr_end_date")
            End With
            bdsr_en_id.Focus()
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
                                            & "  public.bdsr_rule   " _
                                            & "SET  " _
                                            & "  bdsr_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  bdsr_en_id = " & SetInteger(bdsr_en_id.EditValue) & ",  " _
                                            & "  bdsr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bdsr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  bdsr_sales_unit_from = " & SetDbl(bdsr_sales_unit_from.EditValue) & ",  " _
                                            & "  bdsr_sales_unit_to = " & SetDbl(bdsr_sales_unit_to.EditValue) & ",  " _
                                            & "  bdsr_faktor_pengali = " & SetDbl(bdsr_faktor_pengali.EditValue) & ",  " _
                                            & "  bdsr_start_date = " & SetDate(bdsr_start_date.DateTime) & ",  " _
                                            & "  bdsr_end_date = " & SetDate(bdsr_end_date.DateTime) & ",  " _
                                            & "  brsr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bdsr_oid = " & SetSetring(_bdsr_oid_mstr) & " "
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
                        set_row(Trim(_bdsr_oid_mstr.ToString), "bdsr_oid")
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

    'Public Overrides Function edit_data() As Boolean
    '    MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Function

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
                            .Command.CommandText = "delete from bdsr_mstr where bdsr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdsr_oid").ToString + "'"
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
