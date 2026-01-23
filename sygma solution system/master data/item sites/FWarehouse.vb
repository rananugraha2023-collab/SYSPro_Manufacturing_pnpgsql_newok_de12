Imports npgsql
Imports master_new.ModFunction

Public Class FWarehouse
    Dim _wh_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FWarehouse_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))

        With wh_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wh_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "wh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sequence", "wh_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "wh_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category", "wh_cat_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Parent", "wh_desc_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "wh_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "wh_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wh_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "wh_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wh_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("wh_cat_mstr", wh_en_id.EditValue))

        With wh_cat
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("wh_type_mstr", wh_en_id.EditValue))

        With wh_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select -1 as wh_id, 'No Parent' as wh_desc " + _
                           " union " + _
                           " select wh_id, wh_desc from wh_mstr where wh_active ~~* 'Y'" + _
                           " and wh_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and wh_en_id = " + wh_en_id.EditValue.ToString + _
                           " order by wh_id "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wh_mstr")
                    wh_parent.Properties.DataSource = ds_bantu.Tables(0)
                    wh_parent.Properties.DisplayMember = ds_bantu.Tables(0).Columns("wh_desc").ToString
                    wh_parent.Properties.ValueMember = ds_bantu.Tables(0).Columns("wh_id").ToString
                    wh_parent.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub wh_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wh_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.wh_mstr.wh_oid, " _
                    & "  public.wh_mstr.wh_dom_id, " _
                    & "  public.wh_mstr.wh_en_id, " _
                    & "  public.wh_mstr.wh_id, " _
                    & "  public.wh_mstr.wh_add_by, " _
                    & "  public.wh_mstr.wh_add_date, " _
                    & "  public.wh_mstr.wh_upd_by, " _
                    & "  public.wh_mstr.wh_upd_date, " _
                    & "  public.wh_mstr.wh_seq, " _
                    & "  public.wh_mstr.wh_parent, " _
                    & "  wh_parent_mstr.wh_desc as wh_desc_parent, " _
                    & "  public.wh_mstr.wh_code, " _
                    & "  public.wh_mstr.wh_desc, " _
                    & "  public.wh_mstr.wh_type, " _
                    & "  wh_type_mstr.code_name as wh_type_name, " _
                    & "  public.wh_mstr.wh_cat, " _
                    & "  wh_cat_mstr.code_name as wh_cat_name, " _
                    & "  public.wh_mstr.wh_active, " _
                    & "  public.wh_mstr.wh_dt, " _
                    & "  public.en_mstr.en_code " _
                    & "FROM " _
                    & "  public.wh_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wh_mstr.wh_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN public.code_mstr wh_cat_mstr ON wh_cat_mstr.code_id = wh_cat " _
                    & "  INNER JOIN public.code_mstr wh_type_mstr ON wh_type_mstr.code_id = wh_type " _
                    & "  left outer join public.wh_mstr wh_parent_mstr ON wh_parent_mstr.wh_id = public.wh_mstr.wh_parent " _
                    & " where wh_mstr.wh_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & " order by public.wh_mstr.wh_parent, wh_seq, wh_desc"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        wh_en_id.Focus()
        wh_en_id.ItemIndex = 0
        wh_code.Text = ""
        wh_desc.Text = ""
        wh_active.EditValue = False
        wh_parent.Enabled = True
    End Sub

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    Dim _len As Integer = par_en_code.Length + master_new.ClsVar.sServerCode.Length + 1

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(wh_id as varchar)," & _len.ToString & ",100) as integer)),0) as max_col  from wh_mstr " + _
                                           " where substring(cast(wh_id as varchar)," & _len.ToString & ",100) <> ''"
                    .InitializeCommand()

                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _wh_oid As Guid
        _wh_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _wh_id As Integer
        _wh_id = SetInteger(GetID_Local(wh_en_id.GetColumnValue("en_code")))

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
                                            & "  public.wh_mstr " _
                                            & "( " _
                                            & "  wh_oid, " _
                                            & "  wh_dom_id, " _
                                            & "  wh_en_id, " _
                                            & "  wh_id, " _
                                            & "  wh_add_by, " _
                                            & "  wh_add_date, " _
                                            & "  wh_seq, " _
                                            & "  wh_parent, " _
                                            & "  wh_code, " _
                                            & "  wh_desc, " _
                                            & "  wh_type, " _
                                            & "  wh_cat, " _
                                            & "  wh_active, " _
                                            & "  wh_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_wh_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(wh_en_id.EditValue) & ",  " _
                                            & SetInteger(_wh_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("wh_mstr", wh_en_id.GetColumnValue("en_code"), "wh_seq", "wh_parent", wh_parent.EditValue.ToString)) & ",  " _
                                            & IIf(wh_parent.ItemIndex = 0, "null", wh_parent.EditValue) & ",  " _
                                            & SetSetring(wh_code.Text) & ",  " _
                                            & SetSetring(wh_desc.Text) & ",  " _
                                            & SetInteger(wh_type.EditValue) & ",  " _
                                            & SetInteger(wh_cat.EditValue) & ",  " _
                                            & SetBitYN(wh_active.EditValue) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
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
                _wh_oid_mstr = .Item("wh_oid").ToString
                wh_en_id.EditValue = .Item("wh_en_id")
                wh_code.Text = .Item("wh_code")
                wh_desc.Text = .Item("wh_desc")
                wh_cat.EditValue = .Item("wh_cat")
                wh_type.EditValue = .Item("wh_type")
                wh_parent.EditValue = .Item("wh_parent")
                wh_active.EditValue = IIf(.Item("wh_active") = "Y", True, False)
            End With
            wh_en_id.Focus()
            wh_parent.Enabled = False
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
                                            & "  public.wh_mstr   " _
                                            & "SET  " _
                                            & "  wh_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  wh_en_id = " & SetInteger(wh_en_id.EditValue) & ",  " _
                                            & "  wh_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  wh_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  wh_code = " & SetSetring(wh_code.Text) & ",  " _
                                            & "  wh_desc = " & SetSetring(wh_desc.Text) & ",  " _
                                            & "  wh_type = " & SetInteger(wh_type.EditValue) & ",  " _
                                            & "  wh_cat = " & SetInteger(wh_cat.EditValue) & ",  " _
                                            & "  wh_active = " & SetBitYN(wh_active.EditValue) & ",  " _
                                            & "  wh_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  wh_oid = " & SetSetring(_wh_oid_mstr.ToString) & "  " _
                                            & ";"
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
                            .Command.CommandText = "delete from wh_mstr where wh_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wh_oid").ToString + "'"
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

