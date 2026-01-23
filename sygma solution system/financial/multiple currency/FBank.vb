Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FBank
    Dim ssql As String
    Dim _bk_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("currency", ""))
        With sc_le_bk_cu_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("cu_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        With sc_le_bk_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ac_id", ""))
        With sc_le_bk_ac_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("ac_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cc_id", sc_le_bk_en_id.EditValue))
        With sc_le_bk_cc_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("cc_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("sb_id", sc_le_bk_en_id.EditValue))
        With sc_le_bk_sb_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("sb_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Private Sub sc_le_bk_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_bk_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID", "bk_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name Display", "bk_name_display", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cash Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cash Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Invoiced", "bk_invoiced", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "bk_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "bk_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bk_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "bk_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bk_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.bk_oid, " _
                & "  a.bk_dom_id, " _
                & "  b.dom_desc, " _
                & "  a.bk_en_id, " _
                & "  a.bk_add_by, " _
                & "  a.bk_add_date, " _
                & "  a.bk_upd_by, " _
                & "  a.bk_upd_date, " _
                & "  a.bk_id, " _
                & "  a.bk_code, " _
                & "  a.bk_name, " _
                & "  a.bk_cu_id, " _
                & "  cu_name, " _
                & "  a.bk_ac_id, " _
                & "  ac_code, " _
                & "  ac_name, " _
                & "  a.bk_cc_id, " _
                & "  cc_desc, " _
                & "  a.bk_sb_id, " _
                & "  sb_desc, " _
                & "  a.bk_active, " _
                & "  a.bk_invoiced, " _
                & "  a.bk_name_display, " _
                & "  a.bk_dt, " _
                & "  c.en_desc " _
                & "FROM " _
                & "  public.bk_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.bk_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.bk_en_id = c.en_id) " _
                & "  INNER JOIN public.cu_mstr ON (cu_id = bk_cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (ac_id = bk_ac_id) " _
                & "  INNER JOIN public.cc_mstr ON (cc_id = bk_cc_id) " _
                & "  INNER JOIN public.sb_mstr ON (sb_id = bk_sb_id) " _
                & "  AND (b.dom_id = c.en_dom_id)" _
                & " and bk_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        sc_le_bk_en_id.Focus()
        sc_te_bk_code.Text = ""
        sc_te_bk_name.Text = ""
        sc_te_bk_invoiced.Text = ""
        sc_le_bk_en_id.ItemIndex = 0
        sc_le_bk_cu_id.ItemIndex = 0
        sc_le_bk_cc_id.ItemIndex = 0
        sc_le_bk_ac_id.ItemIndex = 0
        sc_le_bk_sb_id.ItemIndex = 0
        sc_ce_bk_active.EditValue = True
        sc_ce_bk_is_invoiced.EditValue = True
        sc_le_bk_en_id.Enabled = True

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _bk_oid As Guid
        _bk_oid = Guid.NewGuid
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
                                            & "  public.bk_mstr " _
                                            & "( " _
                                            & "  bk_oid, " _
                                            & "  bk_dom_id, " _
                                            & "  bk_en_id, " _
                                            & "  bk_add_by, " _
                                            & "  bk_add_date, " _
                                            & "  bk_id, " _
                                            & "  bk_code, " _
                                            & "  bk_name, " _
                                            & "  bk_cu_id, " _
                                            & "  bk_ac_id, " _
                                            & "  bk_cc_id, " _
                                            & "  bk_sb_id, " _
                                            & "  bk_active, " _
                                            & "  bk_invoiced, " _
                                            & "  bk_name_display, " _
                                            & "  bk_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bk_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(sc_le_bk_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetInteger(func_coll.GetID("bk_mstr", sc_le_bk_en_id.GetColumnValue("en_code"), "bk_id", "bk_en_id", sc_le_bk_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(sc_te_bk_code.Text) & ",  " _
                                            & SetSetring(sc_te_bk_name.Text) & ",  " _
                                            & SetInteger(sc_le_bk_cu_id.EditValue) & ",  " _
                                            & SetInteger(sc_le_bk_ac_id.EditValue) & ",  " _
                                            & SetInteger(sc_le_bk_cc_id.EditValue) & ",  " _
                                            & SetInteger(sc_le_bk_sb_id.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_bk_active.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_bk_is_invoiced.EditValue) & ",  " _
                                            & SetSetring(sc_te_bk_invoiced.Text) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
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
                        set_row(Trim(sc_te_bk_code.Text), "bk_code")
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
            sc_le_bk_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _bk_oid = .Item("bk_oid").ToString
                sc_te_bk_code.Text = SetString(.Item("bk_code"))
                sc_te_bk_name.Text = SetString(.Item("bk_name"))
                sc_le_bk_en_id.EditValue = .Item("bk_en_id")
                sc_le_bk_en_id.Enabled = False
                sc_le_bk_cu_id.EditValue = .Item("bk_cu_id")
                sc_le_bk_cc_id.EditValue = .Item("bk_cc_id")
                sc_le_bk_ac_id.EditValue = .Item("bk_ac_id")
                sc_le_bk_sb_id.EditValue = .Item("bk_sb_id")
                sc_ce_bk_active.EditValue = SetBitYNB(.Item("bk_active"))
                sc_ce_bk_is_invoiced.EditValue = SetBitYNB(.Item("bk_invoiced"))
                sc_te_bk_invoiced.Text = SetString(.Item("bk_name_display"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ac_code As String
        Dim ssqls As New ArrayList

        ac_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bk_code")

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
                                                & "  public.bk_mstr   " _
                                                & "SET  " _
                                                & "  bk_en_id = " & SetSetring(sc_le_bk_en_id.EditValue) & ",  " _
                                                & "  bk_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  bk_upd_date = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & "  bk_code = " & SetSetring(sc_te_bk_code.Text) & ",  " _
                                                & "  bk_name = " & SetSetring(sc_te_bk_name.Text) & ",  " _
                                                & "  bk_cu_id = " & SetSetring(sc_le_bk_cu_id.EditValue) & ",  " _
                                                & "  bk_ac_id = " & SetSetring(sc_le_bk_ac_id.EditValue) & ",  " _
                                                & "  bk_cc_id = " & SetSetring(sc_le_bk_cc_id.EditValue) & ",  " _
                                                & "  bk_sb_id = " & SetSetring(sc_le_bk_sb_id.EditValue) & ",  " _
                                                & "  bk_active = " & SetBitYN(sc_ce_bk_active.EditValue) & ",  " _
                                                & "  bk_invoiced = " & SetBitYN(sc_ce_bk_active.EditValue) & ",  " _
                                                & "  bk_name_display = " & SetSetring(sc_te_bk_invoiced.Text) & ",  " _
                                                & "  bk_dt = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  bk_oid ='" & _bk_oid & "' "
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
                        set_row(ac_code, "bk_code")
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
                            .Command.CommandText = "delete from bk_mstr where bk_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bk_oid").ToString + "'"
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

