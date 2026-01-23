Imports npgsql
Imports master_new.ModFunction

Public Class FLocation

    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))

        With sc_le_loc_en
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Warehouse", "wh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "loc_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Consignment", "is_consignment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category", "loc_cat_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Status", "is_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "loc_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Default", "loc_default", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is GIT", "loc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "loc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "loc_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "loc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "loc_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  loc_oid, " _
                    & "  loc_dom_id, " _
                    & "  loc_en_id, " _
                    & "  en_code, " _
                    & "  en_desc, " _
                    & "  loc_add_by, " _
                    & "  loc_add_date, " _
                    & "  loc_upd_by, " _
                    & "  loc_upd_date, " _
                    & "  loc_id, " _
                    & "  loc_wh_id, " _
                    & "  wh_desc, " _
                    & "  loc_si_id, " _
                    & "  si_desc, " _
                    & "  loc_code, " _
                    & "  loc_desc, " _
                    & "  loc_type, " _
                    & "  loc_type_mstr.code_name as loc_type_name, " _
                    & "  loc_type_mstr.code_usr_1 as is_consignment, " _
                    & "  loc_cat, " _
                    & "  loc_cat_mstr.code_name as loc_cat_name, " _
                    & "  loc_is_id, " _
                    & "  is_desc, " _
                    & "  loc_active, " _
                    & "  loc_default, " _
                    & "  loc_dt,coalesce(loc_git,'N') as loc_git,loc_ptnr_id,ptnr_name " _
                    & "FROM  " _
                    & "  public.loc_mstr" _
                    & " inner join en_mstr on en_id = loc_en_id " _
                    & " inner join wh_mstr on wh_id = loc_wh_id " _
                    & " inner join si_mstr on si_id = loc_si_id " _
                    & " inner join is_mstr on is_id = loc_is_id " _
                    & " inner join code_mstr as loc_type_mstr on loc_type_mstr.code_id = loc_type" _
                    & " inner join code_mstr as loc_cat_mstr on loc_cat_mstr.code_id = loc_cat" _
                    & " left outer join ptnr_mstr on loc_ptnr_id=ptnr_id " _
                    & " where loc_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_loc_en.ItemIndex = 0
        sc_le_loc_en.Focus()
        sc_te_loc_code.Text = ""
        sc_te_loc_desc.Text = ""
        loc_ptnr_id.Text = ""
        loc_ptnr_id.Tag = ""
        sc_ce_loc_active.EditValue = False
        sc_ce_is_default.EditValue = False
        loc_git.EditValue = False
    End Sub

    'Private Function GetID_Local(ByVal par_en_code As String) As Integer
    '    Try
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text

    '                .Command.CommandText = "select coalesce(max(cast(substring(cast(loc_id as varchar),5,100) as integer)),0) as max_col  from loc_mstr " + _
    '                                       " where substring(cast(loc_id as varchar),5,100) <> ''"
    '                .InitializeCommand()

    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read

    '                    GetID_Local = .DataReader("max_col") + 1
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    If par_en_code = "0" Then
    '        par_en_code = "99"
    '    End If

    '    GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)


    '    Return GetID_Local
    'End Function

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    Dim _len As Integer = par_en_code.Length + master_new.ClsVar.sServerCode.Length + 1

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(loc_id as varchar)," & _len.ToString & ",100) as integer)),0) as max_col  from loc_mstr " + _
                                           " where substring(cast(loc_id as varchar)," & _len.ToString & ",100) <> ''"
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
        Dim _loc_oid As Guid
        _loc_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _loc_id As Integer
        _loc_id = SetInteger(GetID_Local(sc_le_loc_en.GetColumnValue("en_code")))

        Dim ssql As String
        ssql = "select count(*) as jml from loc_mstr where lower(loc_desc)='" & sc_te_loc_desc.Text.ToLower _
            & "' and loc_en_id=" & sc_le_loc_en.EditValue

        If SetNumber(master_new.PGSqlConn.GetRowInfo(ssql)(0)) > 0 Then
            MessageBox.Show("Duplicate location description")
            Exit Function
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.loc_mstr " _
                                            & "( " _
                                            & "  loc_oid, " _
                                            & "  loc_dom_id, " _
                                            & "  loc_en_id, " _
                                            & "  loc_add_by, " _
                                            & "  loc_add_date, " _
                                            & "  loc_id, " _
                                            & "  loc_wh_id, " _
                                            & "  loc_si_id, " _
                                            & "  loc_code, " _
                                            & "  loc_desc, " _
                                            & "  loc_type, " _
                                            & "  loc_cat, " _
                                            & "  loc_is_id, " _
                                            & "  loc_active,loc_default,loc_git,loc_ptnr_id, " _
                                            & "  loc_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_loc_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sc_le_loc_en.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(_loc_id) & ",  " _
                                            & SetInteger(sc_le_loc_wh.EditValue) & ",  " _
                                            & SetInteger(sc_le_loc_si.EditValue) & ",  " _
                                            & SetSetring(sc_te_loc_code.Text) & ",  " _
                                            & SetSetring(sc_te_loc_desc.Text) & ",  " _
                                            & SetSetring(sc_le_loc_type.EditValue) & ",  " _
                                            & SetSetring(sc_le_loc_cat.EditValue) & ",  " _
                                            & SetSetring(sc_le_loc_is.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_loc_active.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_is_default.EditValue) & ",  " _
                                            & SetBitYN(loc_git.EditValue) & ",  " _
                                            & SetInteger(loc_ptnr_id.Tag) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If loc_git.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE loc_mstr set loc_git='N' where loc_en_id=" & SetInteger(sc_le_loc_en.EditValue) _
                                        & " and loc_id <> " & SetInteger(_loc_id)

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Location " & sc_te_loc_code.Text)
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
            sc_te_loc_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _loc_oid_mstr = .Item("loc_oid").ToString
                sc_le_loc_en.EditValue = .Item("loc_en_id")
                sc_te_loc_code.EditValue = .Item("loc_code")
                sc_te_loc_desc.EditValue = .Item("loc_desc")
                sc_le_loc_wh.EditValue = .Item("loc_wh_id")
                sc_le_loc_type.EditValue = .Item("loc_type")
                sc_le_loc_cat.EditValue = .Item("loc_cat")
                sc_le_loc_si.EditValue = .Item("loc_si_id")
                sc_le_loc_is.EditValue = .Item("loc_is_id")
                sc_ce_loc_active.EditValue = IIf(.Item("loc_active") = "Y", True, False)
                sc_ce_is_default.EditValue = SetBitYNB(.Item("loc_git"))
                loc_git.EditValue = SetBitYNB(.Item("loc_git"))
                loc_ptnr_id.Tag = SetString(.Item("loc_ptnr_id"))
                loc_ptnr_id.EditValue = .Item("ptnr_name")
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.loc_mstr   " _
                                            & "SET  " _
                                            & "  loc_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  loc_en_id = " & SetInteger(sc_le_loc_en.EditValue) & ",  " _
                                            & "  loc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  loc_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  loc_wh_id = " & SetInteger(sc_le_loc_wh.EditValue) & ",  " _
                                            & "  loc_si_id = " & SetInteger(sc_le_loc_si.EditValue) & ",  " _
                                            & "  loc_git = " & SetBitYN(loc_git.EditValue) & ",  " _
                                            & "  loc_ptnr_id = " & SetInteger(loc_ptnr_id.Tag) & ",  " _
                                            & "  loc_code = " & SetSetring(sc_te_loc_code.Text) & ",  " _
                                            & "  loc_desc = " & SetSetring(sc_te_loc_desc.Text) & ",  " _
                                            & "  loc_type = " & SetSetring(sc_le_loc_type.EditValue) & ",  " _
                                            & "  loc_cat = " & SetSetring(sc_le_loc_cat.EditValue) & ",  " _
                                            & "  loc_is_id = " & SetSetring(sc_le_loc_is.EditValue) & ",  " _
                                            & "  loc_active = " & SetBitYN(sc_ce_loc_active.EditValue) & ",  " _
                                            & "  loc_default = " & SetBitYN(sc_ce_is_default.EditValue) & ",  " _
                                            & "  loc_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  loc_oid = " & SetSetring(_loc_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If loc_git.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE loc_mstr set loc_git='N' where loc_en_id=" & SetInteger(sc_le_loc_en.EditValue) _
                                        & " and loc_oid <> " & SetSetring(_loc_oid_mstr)

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Location " & sc_te_loc_code.Text)
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
                            .Command.CommandText = "delete from loc_mstr where loc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("loc_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Location " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("loc_code"))
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

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("wh_mstr", sc_le_loc_en.EditValue))

        With sc_le_loc_wh
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("wh_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("wh_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("is_mstr", sc_le_loc_en.EditValue))

        With sc_le_loc_is
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("is_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("is_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("loc_cat_mstr", sc_le_loc_en.EditValue))

        With sc_le_loc_cat
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("loc_type_mstr", sc_le_loc_en.EditValue))

        With sc_le_loc_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", sc_le_loc_en.EditValue))

        With sc_le_loc_si
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Private Sub sc_le_loc_en_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_loc_en.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub loc_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles loc_ptnr_id.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._obj = loc_ptnr_id
        frm._en_id = sc_le_loc_en.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
