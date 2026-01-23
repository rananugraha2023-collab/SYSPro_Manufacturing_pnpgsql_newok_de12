Imports npgsql
Imports master_new.ModFunction

Public Class FCurrency

    Dim _cu_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        cu_ac_unreal_exc_gain_id.Properties.DataSource = dt_bantu
        cu_ac_unreal_exc_gain_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cu_ac_unreal_exc_gain_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        cu_ac_unreal_exc_gain_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        cu_ac_unreal_exc_lost_id.Properties.DataSource = dt_bantu
        cu_ac_unreal_exc_lost_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cu_ac_unreal_exc_lost_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        cu_ac_unreal_exc_lost_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        cu_ac_real_exc_gain_id.Properties.DataSource = dt_bantu
        cu_ac_real_exc_gain_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cu_ac_real_exc_gain_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        cu_ac_real_exc_gain_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        cu_ac_real_exc_lost_id.Properties.DataSource = dt_bantu
        cu_ac_real_exc_lost_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cu_ac_real_exc_lost_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        cu_ac_real_exc_lost_id.ItemIndex = 0
    End Sub


    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "cu_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Symbol", "cu_symbol", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "cu_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "cu_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unrealized Exchange Gain Acct", "ac_unreal_exc_gain_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unrealized Exchange Lost Acct", "ac_unreal_exc_lost_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Realized Exchange Gain Acct", "ac_real_exc_gain_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Realized Exchange Lost Acct", "ac_real_exc_lost_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cu_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cu_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cu_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cu_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        'get_sequel = "select dom_oid, dom_id, dom_code, dom_desc, dom_active, dom_dt from dom_mstr"
        get_sequel = "SELECT  " _
                    & "  cu_oid, " _
                    & "  cu_add_by, " _
                    & "  cu_add_date, " _
                    & "  cu_upd_by, " _
                    & "  cu_upd_date, " _
                    & "  cu_id, " _
                    & "  cu_code, " _
                    & "  cu_name, " _
                    & "  cu_symbol, " _
                    & "  cu_desc, " _
                    & "  cu_ac_unreal_exc_gain_id, " _
                    & "  ac_mstr_unreal_gain.ac_code as ac_unreal_exc_gain_code, " _
                    & "  ac_mstr_unreal_gain.ac_name as ac_unreal_exc_gain, " _
                    & "  cu_ac_unreal_exc_lost_id, " _
                    & "  ac_mstr_unreal_lost.ac_code as ac_unreal_exc_lost_code, " _
                    & "  ac_mstr_unreal_lost.ac_name as ac_unreal_exc_lost, " _
                    & "  cu_ac_real_exc_gain_id, " _
                    & "  ac_mstr_real_gain.ac_code as ac_real_exc_gain_code, " _
                    & "  ac_mstr_real_gain.ac_name as ac_real_exc_gain, " _
                    & "  cu_ac_real_exc_lost_id, " _
                    & "  ac_mstr_real_lost.ac_code as ac_real_exc_lost_code, " _
                    & "  ac_mstr_real_lost.ac_name as ac_real_exc_lost, " _
                    & "  cu_dt, cu_active " _
                    & "FROM  " _
                    & "  public.cu_mstr" _
                    & "  inner join ac_mstr ac_mstr_unreal_gain on ac_mstr_unreal_gain.ac_id = cu_ac_unreal_exc_gain_id" _
                    & "  inner join ac_mstr ac_mstr_unreal_lost on ac_mstr_unreal_lost.ac_id = cu_ac_unreal_exc_lost_id" _
                    & "  inner join ac_mstr ac_mstr_real_gain on ac_mstr_real_gain.ac_id = cu_ac_real_exc_gain_id" _
                    & "  inner join ac_mstr ac_mstr_real_lost on ac_mstr_real_lost.ac_id = cu_ac_real_exc_lost_id"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_te_cu_code.Focus()
        sc_te_cu_code.Text = ""
        sc_te_cu_name.Text = ""
        sc_te_cu_symbol.Text = ""
        sc_te_cu_desc.Text = ""
        sc_ce_cu_active.EditValue = True
        cu_ac_unreal_exc_gain_id.ItemIndex = 0
        cu_ac_unreal_exc_lost_id.ItemIndex = 0
        cu_ac_real_exc_gain_id.ItemIndex = 0
        cu_ac_real_exc_lost_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _cu_oid As Guid
        Dim ssqls As New ArrayList
        _cu_oid = Guid.NewGuid

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
                                            & "  public.cu_mstr " _
                                            & "( " _
                                            & "  cu_oid, " _
                                            & "  cu_add_by, " _
                                            & "  cu_add_date," _
                                            & "  cu_id, " _
                                            & "  cu_code, " _
                                            & "  cu_name, " _
                                            & "  cu_symbol, " _
                                            & "  cu_desc, " _
                                            & "  cu_active, " _
                                            & "  cu_ac_unreal_exc_gain_id, " _
                                            & "  cu_ac_unreal_exc_lost_id, " _
                                            & "  cu_ac_real_exc_gain_id, " _
                                            & "  cu_ac_real_exc_lost_id, " _
                                            & "  cu_dt " _
                                            & ")  " _
                                            & "VALUES ( '" _
                                            + _cu_oid.ToString + "', '" _
                                            + master_new.ClsVar.sNama _
                                            + "', (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ") " _
                                            & ", (select coalesce(max(cu_id),0) + 1 from cu_mstr),'" + _
                                               Trim(sc_te_cu_code.Text) + "','" + _
                                               Trim(sc_te_cu_name.Text) + "','" + _
                                               Trim(sc_te_cu_symbol.Text) + "','" + _
                                               Trim(sc_te_cu_desc.Text) + "','" + _
                                               IIf(sc_ce_cu_active.EditValue = True, "Y", "N") + "'," _
                                           & SetInteger(cu_ac_unreal_exc_gain_id.EditValue) & ", " _
                                           & SetInteger(cu_ac_unreal_exc_lost_id.EditValue) & ", " _
                                           & SetInteger(cu_ac_real_exc_gain_id.EditValue) & ", " _
                                           & SetInteger(cu_ac_real_exc_lost_id.EditValue) & ", " _
                                           & " (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" _
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
            sc_te_cu_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cu_oid = .Item("cu_oid").ToString
                sc_te_cu_code.Text = .Item("cu_code")
                sc_te_cu_name.Text = .Item("cu_name")
                sc_te_cu_symbol.Text = .Item("cu_symbol")
                sc_te_cu_desc.Text = .Item("cu_desc")
                sc_ce_cu_active.EditValue = IIf(.Item("cu_active") = "Y", True, False)
                cu_ac_unreal_exc_gain_id.EditValue = .Item("cu_ac_unreal_exc_gain_id")
                cu_ac_unreal_exc_lost_id.EditValue = .Item("cu_ac_unreal_exc_lost_id")
                cu_ac_real_exc_gain_id.EditValue = .Item("cu_ac_real_exc_gain_id")
                cu_ac_real_exc_lost_id.EditValue = .Item("cu_ac_real_exc_lost_id")
            End With

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
                        .Command.CommandText = "update cu_mstr set cu_code = '" + Trim(sc_te_cu_code.Text) + "'," + _
                                               " cu_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " cu_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")," + _
                                               " cu_name = '" + Trim(sc_te_cu_name.Text) + "'," + _
                                               " cu_symbol = '" + Trim(sc_te_cu_symbol.Text) + "'," + _
                                               " cu_desc = '" + Trim(sc_te_cu_desc.Text) + "'," + _
                                               " cu_active = '" + IIf(sc_ce_cu_active.EditValue = True, "Y", "N") + "'," + _
                                               " cu_ac_unreal_exc_gain_id = " + cu_ac_unreal_exc_gain_id.EditValue.ToString + ", " + _
                                               " cu_ac_unreal_exc_lost_id = " + cu_ac_unreal_exc_lost_id.EditValue.ToString + ", " + _
                                               " cu_ac_real_exc_gain_id = " + cu_ac_real_exc_gain_id.EditValue.ToString + ", " + _
                                               " cu_ac_real_exc_lost_id = " + cu_ac_real_exc_lost_id.EditValue.ToString + ", " + _
                                               " cu_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" + _
                                               " where cu_oid = '" + _cu_oid + "'"
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

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from cu_mstr where cu_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cu_oid").ToString + "'"
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
End Class
