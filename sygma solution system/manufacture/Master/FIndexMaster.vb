Imports Devart.Data.postgresql
Imports master_new.ModFunction


Public Class FIndexMaster
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wc_oid_mstr As String
    Dim ssqls As New ArrayList

    Private Sub FIndexMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        indx_en_id.Properties.DataSource = dt_bantu
        indx_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        indx_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        indx_en_id.ItemIndex = 0
    End Sub

    Private Sub wc_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles indx_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_mch_mstr(indx_en_id.EditValue))
        indx_mch_id.Properties.DataSource = dt_bantu
        indx_mch_id.Properties.DisplayMember = dt_bantu.Columns("mch_desc").ToString
        indx_mch_id.Properties.ValueMember = dt_bantu.Columns("mch_id").ToString
        indx_mch_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wc_mstr(indx_en_id.EditValue))
        indx_wc_id.Properties.DataSource = dt_bantu
        indx_wc_id.Properties.DisplayMember = dt_bantu.Columns("wc_desc").ToString
        indx_wc_id.Properties.ValueMember = dt_bantu.Columns("wc_id").ToString
        indx_wc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cetak_bahan(indx_en_id.EditValue))
        indx_type_id.Properties.DataSource = dt_bantu
        indx_type_id.Properties.DisplayMember = dt_bantu.Columns("kode").ToString
        indx_type_id.Properties.ValueMember = dt_bantu.Columns("kode_id").ToString
        indx_type_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("unitmeasure", indx_en_id.EditValue))
        With indx_um_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("cetak_bahan", indx_en_id.EditValue))
        'With indx_type_id
        '.Properties.DataSource = dt_bantu
        '.Properties.DisplayMember = dt_bantu.Columns("kode").ToString
        '.Properties.ValueMember = dt_bantu.Columns("kode_id").ToString
        '.ItemIndex = 0
        'End With

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_mch_mstr(indx_en_id.EditValue))
        'indx_mch_id.Properties.DataSource = dt_bantu
        'indx_mch_id.Properties.DisplayMember = dt_bantu.Columns("mch_desc").ToString
        'indx_mch_id.Properties.ValueMember = dt_bantu.Columns("mch_id").ToString
        'indx_mch_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.unitmeasure(indx_en_id.EditValue))
        'indx_mch_id.Properties.DataSource = dt_bantu
        'indx_mch_id.Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        'indx_mch_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'indx_mch_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center Process Code", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center Process Desc", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center Machine", "mch_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Department", "dpt_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Queue Time", "wc_queue", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Wait Time", "wc_wait", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Mach/Op", "wc_mch_op", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_master, "Setup Crew", "wc_setup_men", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_master, "Run Crew", "wc_men_mch", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_master, "Machines", "wc_mch_wkctr", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Mach Burden Rate", "wc_mch_bdn_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Setup Rate", "wc_setup_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Labor Rate", "wc_lbr_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Labor Burden Rate", "wc_bdn_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Labor Bdn %", "wc_bdn_pct", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Is Active", "wc_active", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "wc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wc_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "wc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wc_upd_date", DevExpress.Utils.HorzAlignment.Center)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                        & "  wc_oid, " _
                        & "  wc_dom_id, " _
                        & "  wc_en_id, " _
                        & "  wc_add_by, " _
                        & "  wc_add_date, " _
                        & "  wc_upd_by, " _
                        & "  wc_upd_date, " _
                        & "  wc_id, " _
                        & "  wc_code, " _
                        & "  wc_machine, " _
                        & "  wc_desc, " _
                        & "  wc_dpt_id, " _
                        & "  wc_queue, " _
                        & "  wc_wait, " _
                        & "  wc_mch_op, " _
                        & "  wc_setup_men, " _
                        & "  wc_men_mch, " _
                        & "  wc_mch_wkctr, " _
                        & "  wc_mch_bdn_rate, " _
                        & "  wc_setup_rate, " _
                        & "  wc_lbr_rate, " _
                        & "  wc_bdn_rate, " _
                        & "  wc_bdn_pct, " _
                        & "  wc_active, " _
                        & "  wc_dt, " _
                        & "  en_desc, " _
                        & "  dpt_desc " _
                        & "FROM  " _
                        & "  public.wc_mstr " _
                        & "  inner join en_mstr on en_id = wc_en_id " _
                        & "  inner join dpt_mstr on dpt_id = wc_dpt_id "

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        indx_en_id.ItemIndex = 0
        'wc_code.Text = ""
        'wc_desc.Text = ""
        'wc_machine.Text = ""
        indx_mch_id.ItemIndex = 0
        wc_queue.EditValue = 0.0
        wc_wait.EditValue = 0.0
        wc_mch_op.EditValue = 0
        'wc_setup_men.EditValue = 0
        wc_men_mch.EditValue = 0
        wc_mch_wkctr.EditValue = 0
        wc_mch_bdn_rate.EditValue = 0.0
        'wc_setup_rate.EditValue = 0.0
        wc_lbr_rate.EditValue = 0.0
        wc_bdn_rate.EditValue = 0.0
        wc_bdn_pct.EditValue = 0.0
        wc_active.EditValue = True

        indx_en_id.Focus()
    End Sub

    Public Overrides Function insert() As Boolean
        'Dim _wc_oid As Guid
        '_wc_oid = Guid.NewGuid

        'Dim _wc_id As Integer
        '_wc_id = SetInteger(func_coll.GetID("wc_mstr", indx_en_id.GetColumnValue("en_code"), "wc_id", "wc_en_id", indx_en_id.EditValue.ToString))
        'ssqls.Clear()

        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran

        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "INSERT INTO  " _
        '                                        & "  public.wc_mstr " _
        '                                        & "( " _
        '                                        & "  wc_oid, " _
        '                                        & "  wc_dom_id, " _
        '                                        & "  wc_en_id, " _
        '                                        & "  wc_add_by, " _
        '                                        & "  wc_add_date, " _
        '                                        & "  wc_id, " _
        '                                        & "  wc_code, " _
        '                                        & "  wc_machine, " _
        '                                        & "  wc_desc, " _
        '                                        & "  wc_dpt_id, " _
        '                                        & "  wc_queue, " _
        '                                        & "  wc_wait, " _
        '                                        & "  wc_mch_op, " _
        '                                        & "  wc_setup_men, " _
        '                                        & "  wc_men_mch, " _
        '                                        & "  wc_mch_wkctr, " _
        '                                        & "  wc_mch_bdn_rate, " _
        '                                        & "  wc_setup_rate, " _
        '                                        & "  wc_lbr_rate, " _
        '                                        & "  wc_bdn_rate, " _
        '                                        & "  wc_bdn_pct, " _
        '                                        & "  wc_active, " _
        '                                        & "  wc_dt " _
        '                                        & ")  " _
        '                                        & "VALUES ( " _
        '                                        & SetSetring(_wc_oid.ToString) & ",  " _
        '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                                        & SetSetring(indx_en_id.EditValue) & ",  " _
        '                                         & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                        & " current_timestamp " & ",  " _
        '                                        & SetInteger(_wc_id) & ",  " _
        '                                        & SetSetring(wc_code.Text) & ",  " _
        '                                        & SetSetring(wc_machine.Text) & ",  " _
        '                                        & SetSetring(wc_desc.Text) & ",  " _
        '                                        & SetInteger(wc_dpt_id.EditValue) & ",  " _
        '                                        & SetDbl(wc_queue.EditValue) & ",  " _
        '                                        & SetDbl(wc_wait.EditValue) & ",  " _
        '                                        & SetDbl(wc_mch_op.EditValue) & ",  " _
        '                                        & SetDbl(wc_setup_men.EditValue) & ",  " _
        '                                        & SetDbl(wc_men_mch.EditValue) & ",  " _
        '                                        & SetDbl(wc_mch_wkctr.EditValue) & ",  " _
        '                                        & SetDbl(wc_mch_bdn_rate.EditValue) & ",  " _
        '                                        & SetDbl(wc_setup_rate.EditValue) & ",  " _
        '                                        & SetDbl(wc_lbr_rate.EditValue) & ",  " _
        '                                        & SetDbl(wc_bdn_rate.EditValue) & ",  " _
        '                                        & SetDbl(wc_bdn_pct.EditValue) & ",  " _
        '                                        & SetBitYN(wc_active.EditValue) & ",  " _
        '                                        & " current_timestamp " & "  " _
        '                                        & ");"
        '                ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()


        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = insert_log("Insert Work Center " & wc_code.Text)
        '                ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                If master_new.PGSqlConn.status_sync = True Then
        '                    For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
        '                        .Command.CommandType = CommandType.Text
        '                        .Command.CommandText = Data
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()
        '                    Next
        '                End If


        '                sqlTran.Commit()
        '                after_success()
        '                set_row(Trim(_wc_oid.ToString), "wc_oid")
        '                insert = True
        '            Catch ex As PgSqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    row = 0
        '    insert = False
        '    MessageBox.Show(ex.Message)
        'End Try
        'Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        'If MyBase.edit_data = True Then
        '    indx_en_id.Focus()

        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _wc_oid_mstr = .Item("wc_oid").ToString
        '        indx_en_id.EditValue = .Item("wc_en_id")
        '        wc_code.Text = SetString(.Item("wc_code"))
        '        wc_desc.Text = SetString(.Item("wc_desc"))
        '        wc_machine.Text = SetString(.Item("wc_machine"))
        '        indx_mch_id.EditValue = .Item("wc_dpt_id")
        '        wc_queue.Text = SetDbl(.Item("wc_queue"))
        '        wc_wait.Text = SetDbl(.Item("wc_wait"))
        '        wc_mch_op.Text = SetInteger(.Item("wc_mch_op"))
        '        'wc_setup_men.Text = SetDbl(.Item("wc_setup_men"))
        '        wc_men_mch.Text = SetDbl(.Item("wc_men_mch"))
        '        wc_mch_wkctr.Text = SetDbl(.Item("wc_mch_wkctr"))
        '        wc_mch_bdn_rate.Text = SetDbl(.Item("wc_mch_bdn_rate"))
        '        wc_setup_rate.Text = SetDbl(.Item("wc_setup_rate"))
        '        wc_lbr_rate.Text = SetDbl(.Item("wc_lbr_rate"))
        '        wc_bdn_rate.Text = SetDbl(.Item("wc_bdn_rate"))
        '        wc_bdn_pct.Text = SetDbl(.Item("wc_bdn_pct"))
        '        wc_active.EditValue = SetBitYNB(.Item("wc_active"))
        '    End With

        '    edit_data = True
        'End If
    End Function

    Public Overrides Function edit()
        'edit = True

        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran
        '                ssqls.Clear()

        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "UPDATE  " _
        '                                    & "  public.wc_mstr   " _
        '                                    & "SET  " _
        '                                    & "  wc_en_id = " & SetInteger(indx_en_id.EditValue) & ",  " _
        '                                    & "  wc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & "  wc_upd_date = current_timestamp,  " _
        '                                    '& "  wc_code = " & SetSetring(wc_code.Text) & ",  " _
        '                                    '& "  wc_machine = " & SetSetring(wc_machine.Text) & ",  " _
        '                '& "  wc_desc = " & SetSetring(wc_desc.Text) & ",  " _
        '                '& "  wc_dpt_id = " & SetInteger(wc_dpt_id.EditValue) & ",  " _
        '                                    & "  wc_queue = " & SetDblDB(wc_queue.EditValue) & ",  " _
        '                                    & "  wc_wait = " & SetDblDB(wc_wait.EditValue) & ",  " _
        '                                    & "  wc_mch_op = " & SetDbl(wc_mch_op.EditValue) & ",  " _
        '                                    '& "  wc_setup_men = " & SetDblDB(wc_setup_men.EditValue) & ",  " _
        '                                    & "  wc_men_mch = " & SetDblDB(wc_men_mch.EditValue) & ",  " _
        '                                    & "  wc_mch_wkctr = " & SetDblDB(wc_mch_wkctr.EditValue) & ",  " _
        '                                    & "  wc_mch_bdn_rate = " & SetDblDB(wc_mch_bdn_rate.EditValue) & ",  " _
        '                                    '& "  wc_setup_rate = " & SetDblDB(wc_setup_rate.EditValue) & ",  " _
        '                                    & "  wc_lbr_rate = " & SetDblDB(wc_lbr_rate.EditValue) & ",  " _
        '                                    & "  wc_bdn_rate = " & SetDblDB(wc_bdn_rate.EditValue) & ",  " _
        '                                    & "  wc_bdn_pct = " & SetDblDB(wc_bdn_pct.EditValue) & ",  " _
        '                                    & "  wc_active = " & SetBitYN(wc_active.EditValue) & ",  " _
        '                                    & "  wc_dt = current_timestamp " _
        '                                    & "  " _
        '                                    & "WHERE  " _
        '                                    & "  wc_oid = " & SetSetring(_wc_oid_mstr.ToString) & "  " _
        '                                    & ";"

        '                ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()



        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = insert_log("Edit Work Center " & indx_wc_id.Text)
        '                ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                If master_new.PGSqlConn.status_sync = True Then
        '                    For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
        '                        .Command.CommandType = CommandType.Text
        '                        .Command.CommandText = Data
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()
        '                    Next
        '                End If

        '                sqlTran.Commit()
        '                after_success()
        '                set_row(Trim(_wc_oid_mstr.ToString), "wc_oid")
        '                edit = True
        '            Catch ex As PgSqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    edit = False
        '    MessageBox.Show(ex.Message)
        'End Try
        'Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        'delete_data = True
        'If ds.Tables.Count = 0 Then
        '    delete_data = False
        '    Exit Function
        'ElseIf ds.Tables(0).Rows.Count = 0 Then
        '    delete_data = False
        '    Exit Function
        'End If

        'If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Akan Menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
        '    Exit Function
        'End If

        'If before_delete() = True Then
        '    row = BindingContext(ds.Tables(0)).Position

        '    If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
        '        row = row - 1
        '    ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
        '        row = 0
        '    End If
        '    ssqls.Clear()
        '    Try
        '        Using objinsert As New master_new.WDABasepgsql("", "")
        '            With objinsert
        '                .Connection.Open()
        '                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '                Try
        '                    .Command = .Connection.CreateCommand
        '                    .Command.Transaction = sqlTran

        '                    .Command.CommandType = CommandType.Text
        '                    .Command.CommandText = "delete from wc_mstr where wc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wc_oid").ToString + "'"
        '                    ssqls.Add(.Command.CommandText)
        '                    .Command.ExecuteNonQuery()
        '                    .Command.Parameters.Clear()


        '                    .Command.CommandType = CommandType.Text
        '                    .Command.CommandText = insert_log("Delete Work Center " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wc_code"))
        '                    ssqls.Add(.Command.CommandText)
        '                    .Command.ExecuteNonQuery()
        '                    .Command.Parameters.Clear()

        '                    If master_new.PGSqlConn.status_sync = True Then
        '                        For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
        '                            .Command.CommandType = CommandType.Text
        '                            .Command.CommandText = Data
        '                            .Command.ExecuteNonQuery()
        '                            .Command.Parameters.Clear()
        '                        Next
        '                    End If

        '                    sqlTran.Commit()

        '                    help_load_data(True)
        '                    MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                Catch ex As PgSqlException
        '                    sqlTran.Rollback()
        '                    MessageBox.Show(ex.Message)
        '                End Try
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If

        Return delete_data
    End Function

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub

End Class
