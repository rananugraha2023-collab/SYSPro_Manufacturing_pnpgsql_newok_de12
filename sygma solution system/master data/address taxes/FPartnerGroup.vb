Imports npgsql
Imports master_new.ModFunction

Public Class FPartnerGroup

    Dim _ptnrg_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPartnerGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        ptnrg_en_id.Properties.DataSource = dt_bantu
        ptnrg_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptnrg_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptnrg_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ptnrg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ptnrg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Limit", "ptnrg_credit_limit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Term Of Payment (Days)", "ptnrg_term_of_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Is Active", "ptnrg_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptnrg_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnrg_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnrg_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnrg_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ptnrg_oid, " _
                    & "  ptnrg_dom_id, " _
                    & "  ptnrg_en_id, " _
                    & "  en_code, " _
                    & "  ptnrg_add_by, " _
                    & "  ptnrg_add_date, " _
                    & "  ptnrg_upd_by, " _
                    & "  ptnrg_upd_date, ptnrg_credit_limit, ptnrg_term_of_payment, " _
                    & "  ptnrg_id, " _
                    & "  ptnrg_code, " _
                    & "  ptnrg_name, " _
                    & "  ptnrg_desc, " _
                    & "  ptnrg_active, " _
                    & "  ptnrg_dt " _
                    & " FROM  " _
                    & "  public.ptnrg_grp" _
                    & " inner join public.en_mstr on en_id = ptnrg_en_id" _
                    & " where ptnrg_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ptnrg_en_id.Focus()
        ptnrg_en_id.ItemIndex = 0
        ptnrg_code.Text = ""
        ptnrg_name.Text = ""
        ptnrg_desc.Text = ""
        ptnrg_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnrg_oid As Guid
        _ptnrg_oid = Guid.NewGuid
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
                                            & "  public.ptnrg_grp " _
                                            & "( " _
                                            & "  ptnrg_oid, " _
                                            & "  ptnrg_dom_id, " _
                                            & "  ptnrg_en_id, " _
                                            & "  ptnrg_add_by, " _
                                            & "  ptnrg_add_date,ptnrg_credit_limit, ptnrg_term_of_payment, " _
                                            & "  ptnrg_id, " _
                                            & "  ptnrg_code, " _
                                            & "  ptnrg_name, " _
                                            & "  ptnrg_desc, " _
                                            & "  ptnrg_active, " _
                                            & "  ptnrg_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnrg_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ptnrg_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                             & SetDec(ptnrg_credit_limit.EditValue) & ",  " _
                                              & SetDec(ptnrg_term_of_payment.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("ptnrg_grp", ptnrg_en_id.GetColumnValue("en_code"), "ptnrg_id", "ptnrg_en_id", ptnrg_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(ptnrg_code.Text) & ",  " _
                                            & SetSetring(ptnrg_name.Text) & ",  " _
                                            & SetSetring(ptnrg_desc.Text) & ",  " _
                                            & SetBitYN(ptnrg_active.EditValue) & ",  " _
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
                        set_row(Trim(ptnrg_code.Text), "ptnrg_code")
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
                _ptnrg_oid = .Item("ptnrg_oid").ToString
                ptnrg_en_id.EditValue = .Item("ptnrg_en_id")
                ptnrg_code.Text = .Item("ptnrg_code")
                ptnrg_name.Text = .Item("ptnrg_name")
                ptnrg_desc.Text = .Item("ptnrg_desc")
                ptnrg_credit_limit.EditValue = .Item("ptnrg_credit_limit")
                ptnrg_term_of_payment.EditValue = .Item("ptnrg_term_of_payment")

                ptnrg_active.EditValue = IIf(.Item("ptnrg_active") = "Y", True, False)
            End With
            ptnrg_en_id.Focus()
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
                                            & "  public.ptnrg_grp   " _
                                            & "SET  " _
                                            & "  ptnrg_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  ptnrg_en_id = " & SetInteger(ptnrg_en_id.EditValue) & ",  " _
                                            & "  ptnrg_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ptnrg_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ptnrg_code = " & SetSetring(ptnrg_code.Text) & ",  " _
                                            & "  ptnrg_name = " & SetSetring(ptnrg_name.Text) & ",  " _
                                            & "  ptnrg_credit_limit = " & SetSetring(ptnrg_credit_limit.EditValue) & ",  " _
                                            & "  ptnrg_term_of_payment = " & SetSetring(ptnrg_term_of_payment.EditValue) & ",  " _
                                            & "  ptnrg_desc = " & SetSetring(ptnrg_desc.Text) & ",  " _
                                            & "  ptnrg_active = " & SetBitYN(ptnrg_active.EditValue) & ",  " _
                                            & "  ptnrg_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ptnrg_oid = " & SetSetring(_ptnrg_oid.ToString) & " "

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
                        set_row(Trim(ptnrg_code.Text), "ptnrg_code")
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
                            .Command.CommandText = "delete from ptnrg_grp where ptnrg_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnrg_oid").ToString + "'"
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
