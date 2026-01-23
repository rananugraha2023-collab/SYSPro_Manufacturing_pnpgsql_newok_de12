Imports npgsql
Imports master_new.ModFunction

Public Class FPOCustomer
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim _pocust_oid_mstr As String

    Private Sub FPOCustomer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pocust_en_id.Properties.DataSource = dt_bantu
        pocust_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pocust_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pocust_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        pocust_cu_id.Properties.DataSource = dt_bantu
        pocust_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        pocust_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        pocust_cu_id.ItemIndex = 0
    End Sub

    Private Sub pocust_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pocust_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer(pocust_en_id.EditValue))
        pocust_ptnr_id.Properties.DataSource = dt_bantu
        pocust_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        pocust_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        pocust_ptnr_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Number", "pocust_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Date", "pocust_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "pocust_star_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "pocust_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "pocust_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "pocust_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "pocust_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pocust_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "pocust_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pocust_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  pocust_oid, " _
                    & "  pocust_dom_id, " _
                    & "  pocust_en_id, " _
                    & "  pocust_add_by, " _
                    & "  pocust_add_date, " _
                    & "  pocust_upd_by, " _
                    & "  pocust_upd_date, " _
                    & "  pocust_dt, " _
                    & "  pocust_code, " _
                    & "  pocust_date, " _
                    & "  pocust_ptnr_id, " _
                    & "  pocust_star_date, " _
                    & "  pocust_end_date, " _
                    & "  pocust_remarks, " _
                    & "  pocust_cu_id, " _
                    & "  pocust_amount, " _
                    & "  en_desc, " _
                    & "  ptnr_name, " _
                    & "  cu_name " _
                    & "FROM  " _
                    & "  public.pocust_mstr " _
                    & "  inner join en_mstr on en_id = pocust_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = pocust_ptnr_id " _
                    & "  inner join cu_mstr on cu_id = pocust_cu_id" _
                    & " where pocust_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and pocust_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and pocust_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        pocust_en_id.ItemIndex = 0
        pocust_en_id.Focus()
        pocust_code.Text = ""
        pocust_date.DateTime = _now
        pocust_star_date.DateTime = _now
        pocust_end_date.DateTime = _now
        pocust_ptnr_id.ItemIndex = 0
        pocust_remarks.Text = ""
        pocust_cu_id.ItemIndex = 0
        pocust_amount.EditValue = 0.0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pocust_oid As Guid
        _pocust_oid = Guid.NewGuid

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
                                            & "  public.pocust_mstr " _
                                            & "( " _
                                            & "  pocust_oid, " _
                                            & "  pocust_dom_id, " _
                                            & "  pocust_en_id, " _
                                            & "  pocust_add_by, " _
                                            & "  pocust_add_date, " _
                                            & "  pocust_dt, " _
                                            & "  pocust_code, " _
                                            & "  pocust_date, " _
                                            & "  pocust_ptnr_id, " _
                                            & "  pocust_star_date, " _
                                            & "  pocust_end_date, " _
                                            & "  pocust_remarks, " _
                                            & "  pocust_cu_id, " _
                                            & "  pocust_amount " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pocust_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pocust_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(pocust_code.Text) & ",  " _
                                            & SetDate(pocust_date.DateTime) & ",  " _
                                            & SetInteger(pocust_ptnr_id.EditValue) & ",  " _
                                            & SetDate(pocust_star_date.DateTime) & ",  " _
                                            & SetDate(pocust_end_date.DateTime) & ",  " _
                                            & SetSetring(pocust_remarks.Text) & ",  " _
                                            & SetInteger(pocust_cu_id.EditValue) & ",  " _
                                            & SetDbl(pocust_amount.Text) & "  " _
                                            & ");"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(_pocust_oid.ToString, "pocust_oid")
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

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pocust_oid_mstr = .Item("pocust_oid").ToString
                pocust_en_id.EditValue = .Item("pocust_en_id")
                pocust_code.Text = .Item("pocust_code")
                pocust_date.DateTime = .Item("pocust_date")
                pocust_ptnr_id.EditValue = .Item("pocust_ptnr_id")
                pocust_star_date.DateTime = .Item("pocust_star_date")
                pocust_end_date.DateTime = .Item("pocust_end_date")
                pocust_remarks.Text = SetString(.Item("pocust_remarks"))
                pocust_cu_id.EditValue = .Item("pocust_cu_id")
                pocust_amount.EditValue = .Item("pocust_amount")
            End With

            pocust_en_id.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

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
                                            & "  public.pocust_mstr   " _
                                            & "SET  " _
                                            & "  pocust_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pocust_en_id = " & SetInteger(pocust_en_id.EditValue) & ",  " _
                                            & "  pocust_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pocust_upd_date = current_timestamp,  " _
                                            & "  pocust_dt = current_timestamp,  " _
                                            & "  pocust_code = " & SetSetring(pocust_code.Text) & ",  " _
                                            & "  pocust_date = " & SetDate(pocust_date.DateTime) & ",  " _
                                            & "  pocust_ptnr_id = " & SetInteger(pocust_ptnr_id.EditValue) & ",  " _
                                            & "  pocust_star_date = " & SetDate(pocust_star_date.DateTime) & ",  " _
                                            & "  pocust_end_date = " & SetDate(pocust_end_date.DateTime) & ",  " _
                                            & "  pocust_remarks = " & SetSetring(pocust_remarks.Text) & ",  " _
                                            & "  pocust_cu_id = " & SetInteger(pocust_cu_id.EditValue) & ",  " _
                                            & "  pocust_amount = " & SetDbl(pocust_amount.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pocust_oid = " & SetSetring(_pocust_oid_mstr) & " "
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(_pocust_oid_mstr, "pocust_oid")
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
                            .Command.CommandText = "delete from pocust_mstr where pocust_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pocust_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
