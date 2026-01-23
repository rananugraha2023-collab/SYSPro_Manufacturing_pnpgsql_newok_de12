Imports npgsql
Imports master_new.ModFunction

Public Class FPartnerCategory

    Dim _ptnrc_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPartnerCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        ptnrc_en_id.Properties.DataSource = dt_bantu
        ptnrc_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptnrc_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptnrc_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ptnrc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ptnrc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Limit", "ptnrc_credit_limit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Term Of Payment (Days)", "ptnrc_term_of_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Is Active", "ptnrc_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptnrc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnrc_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnrc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnrc_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ptnrc_oid, " _
                    & "  ptnrc_dom_id, " _
                    & "  ptnrc_en_id, " _
                    & "  en_code, " _
                    & "  ptnrc_add_by, " _
                    & "  ptnrc_add_date, " _
                    & "  ptnrc_upd_by, " _
                    & "  ptnrc_upd_date, ptnrc_credit_limit, ptnrc_term_of_payment, " _
                    & "  ptnrc_id, " _
                    & "  ptnrc_code, " _
                    & "  ptnrc_name, " _
                    & "  ptnrc_desc, " _
                    & "  ptnrc_active, " _
                    & "  ptnrc_dt " _
                    & " FROM  " _
                    & "  public.ptnrc_cat" _
                    & " inner join public.en_mstr on en_id = ptnrc_en_id" _
                    & " where ptnrc_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ptnrc_en_id.Focus()
        ptnrc_en_id.ItemIndex = 0
        ptnrc_code.Text = ""
        ptnrc_name.Text = ""
        ptnrc_desc.Text = ""
        ptnrc_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnrc_oid As Guid
        _ptnrc_oid = Guid.NewGuid
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
                                            & "  public.ptnrc_cat " _
                                            & "( " _
                                            & "  ptnrc_oid, " _
                                            & "  ptnrc_dom_id, " _
                                            & "  ptnrc_en_id, " _
                                            & "  ptnrc_add_by, " _
                                            & "  ptnrc_add_date,ptnrc_credit_limit, ptnrc_term_of_payment, " _
                                            & "  ptnrc_id, " _
                                            & "  ptnrc_code, " _
                                            & "  ptnrc_name, " _
                                            & "  ptnrc_desc, " _
                                            & "  ptnrc_active, " _
                                            & "  ptnrc_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnrc_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ptnrc_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                             & SetDec(ptnrc_credit_limit.EditValue) & ",  " _
                                              & SetDec(ptnrc_term_of_payment.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("ptnrc_cat", ptnrc_en_id.GetColumnValue("en_code"), "ptnrc_id", "ptnrc_en_id", ptnrc_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(ptnrc_code.Text) & ",  " _
                                            & SetSetring(ptnrc_name.Text) & ",  " _
                                            & SetSetring(ptnrc_desc.Text) & ",  " _
                                            & SetBitYN(ptnrc_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
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
                        set_row(Trim(ptnrc_code.Text), "ptnrc_code")
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
                _ptnrc_oid = .Item("ptnrc_oid").ToString
                ptnrc_en_id.EditValue = .Item("ptnrc_en_id")
                ptnrc_code.Text = .Item("ptnrc_code")
                ptnrc_name.Text = .Item("ptnrc_name")
                ptnrc_desc.Text = .Item("ptnrc_desc")
                ptnrc_credit_limit.EditValue = .Item("ptnrc_credit_limit")
                ptnrc_term_of_payment.EditValue = .Item("ptnrc_term_of_payment")

                ptnrc_active.EditValue = IIf(.Item("ptnrc_active") = "Y", True, False)
            End With
            ptnrc_en_id.Focus()
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
                                            & "  public.ptnrc_cat   " _
                                            & "SET  " _
                                            & "  ptnrc_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  ptnrc_en_id = " & SetInteger(ptnrc_en_id.EditValue) & ",  " _
                                            & "  ptnrc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ptnrc_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ptnrc_code = " & SetSetring(ptnrc_code.Text) & ",  " _
                                            & "  ptnrc_name = " & SetSetring(ptnrc_name.Text) & ",  " _
                                            & "  ptnrc_credit_limit = " & SetSetring(ptnrc_credit_limit.EditValue) & ",  " _
                                            & "  ptnrc_term_of_payment = " & SetSetring(ptnrc_term_of_payment.EditValue) & ",  " _
                                            & "  ptnrc_desc = " & SetSetring(ptnrc_desc.Text) & ",  " _
                                            & "  ptnrc_active = " & SetBitYN(ptnrc_active.EditValue) & ",  " _
                                            & "  ptnrc_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ptnrc_oid = " & SetSetring(_ptnrc_oid.ToString) & " "

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
                        set_row(Trim(ptnrc_code.Text), "ptnrc_code")
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
                            .Command.CommandText = "delete from ptnrc_cat where ptnrc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnrc_oid").ToString + "'"
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
