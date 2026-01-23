Imports npgsql
Imports master_new.ModFunction

Public Class FRoyaltiRule
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _royt_oid_mstr As String

    Private Sub FRoyalti_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        royt_en_id.Properties.DataSource = dt_bantu
        royt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        royt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        royt_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Writer", "pt_writer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Seq", "royt_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "royt_qty_royalti", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Prosentase", "royt_royalti_amt", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Qty SO", "royt_qty_so", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Amount Ext.", "royt_royalti_amt_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Remarks", "royt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "royt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "royt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "royt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "royt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  royt_oid, " _
                    & "  royt_dom_id, " _
                    & "  royt_add_by, " _
                    & "  royt_add_date, " _
                    & "  royt_upd_by, " _
                    & "  royt_upd_date, " _
                    & "  royt_pt_id, " _
                    & "  royt_seq, " _
                    & "  royt_qty_royalti, " _
                    & "  royt_royalti_amt, " _
                    & "  royt_qty_so, " _
                    & "  royt_royalti_amt * royt_qty_so as royt_royalti_amt_ext, " _
                    & "  royt_remarks, " _
                    & "  royt_dt, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  royt_en_id, " _
                    & "  en_desc " _
                    & "FROM  " _
                    & "  public.royt_table " _
                    & "  inner join public.pt_mstr on pt_id = royt_pt_id " _
                    & "  inner join public.en_mstr on en_id = royt_en_id " _
                    & "  order by pt_code, royt_seq "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        royt_pt_id.Focus()
        royt_pt_id.ItemIndex = 0
        royt_remarks.Text = ""
        royt_qty_royalti.Text = 0
        royt_royalti_amt.Text = 0
        royt_pt_id.Enabled = True
        royt_en_id.ItemIndex = 0
        royt_en_id.Enabled = True
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _royt_oid As Guid = Guid.NewGuid

        'mencari nilai sequence
        Dim _royt_seq As Integer
        Dim ssqls As New ArrayList

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(royt_seq),0) + 1 as seq from royt_table " + _
                                           " where royt_pt_id = " + royt_pt_id.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _royt_seq = .DataReader("seq")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
                                            & "  public.royt_table " _
                                            & "( " _
                                            & "  royt_oid, " _
                                            & "  royt_dom_id, " _
                                            & "  royt_add_by, " _
                                            & "  royt_add_date, " _
                                            & "  royt_pt_id, " _
                                            & "  royt_seq, " _
                                            & "  royt_qty_royalti, " _
                                            & "  royt_qty_so, " _
                                            & "  royt_royalti_amt, " _
                                            & "  royt_remarks, " _
                                            & "  royt_en_id, " _
                                            & "  royt_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_royt_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(royt_pt_id.EditValue) & ",  " _
                                            & SetInteger(_royt_seq) & ",  " _
                                            & SetDbl(royt_qty_royalti.EditValue) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetDbl(royt_royalti_amt.EditValue) & ",  " _
                                            & SetSetring(royt_remarks.Text) & ",  " _
                                            & SetInteger(royt_en_id.EditValue) & ",  " _
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
                        set_row(Trim(_royt_oid.ToString), "royt_oid")
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
                _royt_oid_mstr = .Item("royt_oid").ToString
                royt_pt_id.EditValue = .Item("royt_pt_id")
                royt_qty_royalti.EditValue = .Item("royt_qty_royalti")
                royt_royalti_amt.EditValue = .Item("royt_royalti_amt")
                royt_en_id.EditValue = .Item("royt_en_id")
                royt_remarks.Text = SetString(.Item("royt_remarks"))
                royt_pt_id.Enabled = False
                royt_en_id.Enabled = False
            End With
            royt_pt_id.Focus()
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
                                                & "  public.royt_table   " _
                                                & "SET  " _
                                                & "  royt_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & "  royt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  royt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                & "  royt_qty_royalti = " & SetDbl(royt_qty_royalti.EditValue) & ",  " _
                                                & "  royt_royalti_amt = " & SetDbl(royt_royalti_amt.EditValue) & ",  " _
                                                & "  royt_remarks = " & SetSetring(royt_remarks.Text) & ",  " _
                                                & "  royt_en_id = " & SetInteger(royt_en_id.EditValue) & ",  " _
                                                & "  royt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  royt_oid = " & SetSetring(_royt_oid_mstr) & " "
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
                        set_row(_royt_oid_mstr, "royt_oid")
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _royt_qty_so As Integer = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select royt_qty_so from royt_table " + _
                                           " where royt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("royt_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _royt_qty_so = .DataReader("royt_qty_so")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _royt_qty_so > 0 Then
            MessageBox.Show("Can't Delete Processed Royalti...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_delete
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
                            .Command.CommandText = "delete from royt_table where royt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("royt_oid").ToString + "'"
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

    Private Sub royt_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles royt_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pt_mstr(royt_en_id.EditValue))
        royt_pt_id.Properties.DataSource = dt_bantu
        royt_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_code").ToString
        royt_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        royt_pt_id.ItemIndex = 0
    End Sub
End Class
