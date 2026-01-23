Imports npgsql
Imports master_new.ModFunction

Public Class FUMConversion
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _um_oid_mstr As String

    Private Sub FUMConversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Private Function load_pt_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select -1 as pt_id, '' as pt_code, '' as pt_desc1, '' as pt_desc2 from pt_mstr " + _
                               " union " + _
                               " select pt_id, pt_code, pt_desc1, pt_desc2 from pt_mstr " + _
                               " where pt_en_id in (select user_en_id from tconfuserentity " + _
                                             " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                               " and pt_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by pt_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = load_pt_mstr()
        um_pt_id.Properties.DataSource = dt_bantu
        um_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_desc1").ToString
        um_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        um_pt_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr("unitmeasure"))
        um_pt_um.Properties.DataSource = dt_bantu
        um_pt_um.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        um_pt_um.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        um_pt_um.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr("unitmeasure"))
        um_pt_um_alt.Properties.DataSource = dt_bantu
        um_pt_um_alt.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        um_pt_um_alt.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        um_pt_um_alt.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unit Measure", "pt_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unit Measure Alt.", "pt_um_name_alt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Conversion", "um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Is Active", "um_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "um_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "um_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "um_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "um_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  um_oid, " _
                    & "  um_dom_id, " _
                    & "  um_add_by, " _
                    & "  um_add_date, " _
                    & "  um_upd_by, " _
                    & "  um_upd_date, " _
                    & "  um_pt_um, " _
                    & "  um_pt_um_alt, " _
                    & "  um_conv, " _
                    & "  um_active, " _
                    & "  um_dt, " _
                    & "  um_pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  code_pt_um.code_name as pt_um_name, " _
                    & "  code_pt_um_alt.code_name as pt_um_name_alt " _
                    & "FROM  " _
                    & "  public.um_mstr " _
                    & "  left outer join pt_mstr on pt_id = um_pt_id " _
                    & "  inner join code_mstr code_pt_um on code_pt_um.code_id = um_pt_um " _
                    & "  inner join code_mstr code_pt_um_alt on code_pt_um_alt.code_id = um_pt_um_alt "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        load_cb()
        um_pt_id.Focus()
        um_pt_id.ItemIndex = 0
        um_pt_um.ItemIndex = 0
        um_pt_um_alt.ItemIndex = 0
        um_conv.EditValue = 0.0
        um_active.EditValue = False
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        If um_pt_um.EditValue = um_pt_um_alt.EditValue Then
            MessageBox.Show("Unit Measure Can't Same With Unit Measure Alternate..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If um_conv.EditValue = 0 Then
            MessageBox.Show("Qty Converstion Can't 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _um_oid As Guid = Guid.NewGuid
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
                                            & "  public.um_mstr " _
                                            & "( " _
                                            & "  um_oid, " _
                                            & "  um_dom_id, " _
                                            & "  um_add_by, " _
                                            & "  um_add_date, " _
                                            & "  um_pt_um, " _
                                            & "  um_pt_um_alt, " _
                                            & "  um_conv, " _
                                            & "  um_active, " _
                                            & "  um_dt, " _
                                            & "  um_pt_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_um_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(um_pt_um.EditValue) & ",  " _
                                            & SetInteger(um_pt_um_alt.EditValue) & ",  " _
                                            & SetDbl(um_conv.EditValue) & ",  " _
                                            & SetBitYN(um_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & IIf(um_pt_id.ItemIndex = 0, "null", SetInteger(um_pt_id.EditValue)) & "  " _
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
                        set_row(Trim(_um_oid.ToString), "um_oid")
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
                _um_oid_mstr = .Item("um_oid").ToString

                If IsDBNull(.Item("um_pt_id")) = True Then
                    um_pt_id.ItemIndex = 0
                Else
                    um_pt_id.EditValue = .Item("um_pt_id")
                End If

                um_pt_um.EditValue = .Item("um_pt_um")
                um_pt_um_alt.EditValue = .Item("um_pt_um_alt")
                um_conv.EditValue = .Item("um_conv")
                um_active.EditValue = SetBitYNB(.Item("um_active"))

            End With
            um_pt_id.Focus()
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
                                            & "  public.um_mstr   " _
                                            & "SET  " _
                                            & "  um_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  um_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  um_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  um_pt_um = " & SetInteger(um_pt_um.EditValue) & ",  " _
                                            & "  um_pt_um_alt = " & SetInteger(um_pt_um_alt.EditValue) & ",  " _
                                            & "  um_conv = " & SetDbl(um_conv.EditValue) & ",  " _
                                            & "  um_active = " & SetBitYN(um_active.EditValue) & ",  " _
                                            & "  um_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  um_pt_id = " & IIf(um_pt_id.ItemIndex = 0, "null", SetInteger(um_pt_id.EditValue)) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  um_oid = " & SetSetring(_um_oid_mstr) & " "
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
                        set_row(Trim(_um_oid_mstr), "um_oid")
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
                            .Command.CommandText = "delete from um_mstr where um_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("um_oid").ToString + "'"
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
