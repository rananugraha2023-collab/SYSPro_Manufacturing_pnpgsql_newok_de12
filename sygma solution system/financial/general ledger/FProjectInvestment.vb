Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FProjectInvestment

    Dim _sb_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))

        With investp_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "investp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "investp_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Amount", "investp_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Start Date", "investp_tanggal_awal", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "investp_tanggal_akhir", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "IsActive", "investp_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "investp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "investp_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "investp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "investp_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.investp_en_id, " _
            & "  b.en_desc, " _
            & "  a.investp_code, " _
            & "  a.investp_desc, " _
            & "  a.investp_nilai, " _
            & "  a.investp_tanggal_awal, " _
            & "  a.investp_tanggal_akhir, " _
            & "  a.investp_active, " _
            & "  investp_add_by, " _
            & "  investp_upd_by, " _
            & "  investp_add_date, " _
            & "  investp_upd_date " _
            & "FROM " _
            & "  public.investp_project a " _
            & "  INNER JOIN public.en_mstr b ON (a.investp_en_id = b.en_id) " _
            & " where investp_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.investp_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        investp_en_id.Focus()
        investp_en_id.ItemIndex = 0
        investp_code.Text = ""
        investp_desc.Text = ""
        investp_nilai.EditValue = 0.0
        investp_tanggal_awal.DateTime = CekTanggal()
        investp_tanggal_akhir.DateTime = CekTanggal()
        investp_active.EditValue = True
        investp_code.Enabled = False

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _sb_oid As Guid
        Dim ssqls As New ArrayList

        _sb_oid = Guid.NewGuid

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "INSERT INTO  " _
                        '                    & "  public.sb_mstr " _
                        '                    & "( " _
                        '                    & "  sb_oid, " _
                        '                    & "  sb_dom_id, " _
                        '                    & "  sb_en_id, " _
                        '                    & "  sb_add_by, " _
                        '                    & "  sb_add_date, " _
                        '                    & "  sb_id, " _
                        '                    & "  sb_code, " _
                        '                    & "  sb_desc, " _
                        '                    & "  sb_active, " _
                        '                    & "  sb_dt " _
                        '                    & ")  " _
                        '                    & "VALUES ( " _
                        '                    & SetSetring(_sb_oid.ToString) & ",  " _
                        '                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                        '                    & SetInteger(investp_en_id.EditValue) & ",  " _
                        '                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        '                    & SetInteger(func_coll.GetID("sb_mstr", investp_en_id.GetColumnValue("en_code"), "sb_id", "sb_en_id", investp_en_id.EditValue.ToString)) & ",  " _
                        '                    & SetSetring(investp_code.Text) & ",  " _
                        '                    & SetSetring(investp_desc.Text) & ",  " _
                        '                    & SetBitYN(investp_active.EditValue) & ",  " _
                        '                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                    & ")"


                        investp_code.Text = GetNewNumberYM("investp_project", "investp_code", 3, "PJ-" & investp_en_id.GetColumnValue("en_code") _
                                      & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)


                        .Command.CommandText = "INSERT INTO  " _
                                        & "  public.investp_project " _
                                        & "( " _
                                        & "  investp_code, " _
                                        & "  investp_desc, " _
                                        & "  investp_nilai, " _
                                        & "  investp_tanggal_awal, " _
                                        & "  investp_tanggal_akhir, " _
                                        & "  investp_en_id, " _
                                        & "  investp_active, " _
                                        & "  investp_add_by, " _
                                        & "  investp_upd_by " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(investp_code.EditValue) & ",  " _
                                        & SetSetring(investp_desc.EditValue) & ",  " _
                                        & SetDec(investp_nilai.EditValue) & ",  " _
                                        & SetDateNTime00(investp_tanggal_awal.EditValue) & ",  " _
                                        & SetDateNTime00(investp_tanggal_akhir.EditValue) & ",  " _
                                        & SetInteger(investp_en_id.EditValue) & ",  " _
                                        & SetBitYN(investp_active.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(CekTanggal) & "  " _
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
            investp_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                investp_code.EditValue = .Item("investp_code")
                investp_desc.EditValue = .Item("investp_desc")
                investp_en_id.EditValue = .Item("investp_en_id")
                investp_active.EditValue = IIf(.Item("investp_active") = "Y", True, False)
                investp_nilai.EditValue = .Item("investp_nilai")
                investp_tanggal_awal.EditValue = .Item("investp_tanggal_awal")
                investp_tanggal_akhir.EditValue = .Item("investp_tanggal_akhir")
            End With
            investp_code.Enabled = False
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
                                & "  public.investp_project  " _
                                & "SET  " _
                                & "  investp_desc = " & SetSetring(investp_desc.EditValue) & ",  " _
                                & "  investp_nilai = " & SetDec(investp_nilai.EditValue) & ",  " _
                                & "  investp_tanggal_awal = " & SetDateNTime00(investp_tanggal_awal.EditValue) & ",  " _
                                & "  investp_tanggal_akhir = " & SetDateNTime00(investp_tanggal_akhir.EditValue) & ",  " _
                                & "  investp_en_id = " & SetInteger(investp_en_id.EditValue) & ",  " _
                                & "  investp_active = " & SetBitYN(investp_active.EditValue) & ",  " _
                                & "  investp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  investp_upd_date = " & SetDateNTime(CekTanggal) & "  " _
                                & "WHERE  " _
                                & "  investp_code = " & SetSetring(investp_code.EditValue) & " "



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
                            .Command.CommandText = "delete from investp_project where investp_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("investp_code") + "'"
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
