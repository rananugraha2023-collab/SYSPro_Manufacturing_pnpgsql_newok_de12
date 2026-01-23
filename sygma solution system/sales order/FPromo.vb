Imports npgsql
Imports master_new.ModFunction

Public Class FPromo

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _promo_oid_mstr As String
    Dim _now As DateTime

    Private Sub FPromo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        promo_en_id.Properties.DataSource = dt_bantu
        promo_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        promo_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        promo_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales(promo_en_id.EditValue))
        promo_sales_id.Properties.DataSource = dt_bantu
        promo_sales_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        promo_sales_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        promo_sales_id.ItemIndex = 0
    End Sub

    Private Sub promo_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles promo_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promo Number", "promo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Penjamin", "promo_penjamin", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Start Date", "promo_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "promo_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "promo_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "promo_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "promo_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "promo_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "promo_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  promo_oid, " _
                & "  promo_dom_id, " _
                & "  promo_en_id, " _
                & "  en_desc, " _
                & "  promo_add_by, " _
                & "  promo_add_date, " _
                & "  promo_upd_by, " _
                & "  promo_upd_date, " _
                & "  promo_id, " _
                & "  promo_code, " _
                & "  promo_desc, " _
                & "  promo_sales_id, " _
                & "  ptnr_name, " _
                & "  promo_penjamin, " _
                & "  promo_start_date, " _
                & "  promo_end_date, " _
                & "  promo_active, " _
                & "  promo_dt " _
                & "FROM  " _
                & "  public.promo_mstr  " _
                & "  inner join public.en_mstr on en_id = promo_en_id " _
                & "  inner join public.ptnr_mstr on ptnr_id = promo_sales_id" _
                & " where promo_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        promo_en_id.Focus()
        promo_en_id.ItemIndex = 0
        promo_active.EditValue = True
        promo_desc.Text = ""
        promo_penjamin.EditValue = 0
        promo_sales_id.ItemIndex = 0
        promo_start_date.DateTime = _now
        promo_end_date.DateTime = _now
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _promo_oid As Guid = Guid.NewGuid
        Dim ssqls As New ArrayList
        Dim _promo_code As String = func_coll.get_transaction_number("PM", promo_en_id.GetColumnValue("en_code"), "promo_mstr", "promo_code")

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
                                            & "  public.promo_mstr " _
                                            & "( " _
                                            & "  promo_oid, " _
                                            & "  promo_dom_id, " _
                                            & "  promo_en_id, " _
                                            & "  promo_add_by, " _
                                            & "  promo_add_date, " _
                                            & "  promo_id, " _
                                            & "  promo_code, " _
                                            & "  promo_desc, " _
                                            & "  promo_sales_id, " _
                                            & "  promo_penjamin, " _
                                            & "  promo_start_date, " _
                                            & "  promo_end_date, " _
                                            & "  promo_active, " _
                                            & "  promo_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_promo_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(promo_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("promo_mstr", promo_en_id.GetColumnValue("en_code"), "promo_id", "promo_en_id", promo_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(_promo_code) & ",  " _
                                            & SetSetring(promo_desc.Text) & ",  " _
                                            & SetInteger(promo_sales_id.EditValue) & ",  " _
                                            & SetDbl(promo_penjamin.EditValue) & ",  " _
                                            & SetDate(promo_start_date.DateTime) & ",  " _
                                            & SetDate(promo_end_date.DateTime) & ",  " _
                                            & SetBitYN(promo_active.EditValue) & ",  " _
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
                        set_row(Trim(_promo_oid.ToString), "promo_oid")
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
                _promo_oid_mstr = .Item("promo_oid").ToString
                promo_en_id.EditValue = .Item("promo_en_id")
                promo_desc.Text = SetString(.Item("promo_desc"))
                promo_sales_id.EditValue = .Item("promo_sales_id")
                promo_penjamin.EditValue = .Item("promo_penjamin")
                promo_start_date.DateTime = .Item("promo_start_date")
                promo_end_date.DateTime = .Item("promo_end_date")
                promo_active.EditValue = SetBitYNB(.Item("promo_active"))
            End With
            promo_en_id.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        Dim ssqls As New ArrayList
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
                                            & "  public.promo_mstr   " _
                                            & "SET  " _
                                            & "  promo_en_id = " & SetInteger(promo_en_id.EditValue) & ",  " _
                                            & "  promo_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  promo_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  promo_desc = " & SetSetring(promo_desc.Text) & ",  " _
                                            & "  promo_sales_id = " & SetInteger(promo_sales_id.EditValue) & ",  " _
                                            & "  promo_penjamin = " & SetDbl(promo_penjamin.EditValue) & ",  " _
                                            & "  promo_start_date = " & SetDate(promo_start_date.DateTime) & ",  " _
                                            & "  promo_end_date = " & SetDate(promo_end_date.DateTime) & ",  " _
                                            & "  promo_active = " & SetBitYN(promo_active.EditValue) & ",  " _
                                            & "  promo_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  promo_oid = " & SetSetring(_promo_oid_mstr) & " "
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
                        set_row(Trim(_promo_oid_mstr.ToString), "promo_oid")
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
                            .Command.CommandText = "delete from promo_mstr where promo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("promo_oid").ToString + "'"
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
