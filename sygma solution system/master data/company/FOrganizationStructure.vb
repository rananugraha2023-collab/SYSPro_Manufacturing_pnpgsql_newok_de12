Imports npgsql
Imports master_new.ModFunction

Public Class FOrganizationStructure

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _orgs_oid_mstr As String

    Private Sub FOrganizationStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        sc_le_orgs_en_id.Properties.DataSource = dt_bantu
        sc_le_orgs_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        sc_le_orgs_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        sc_le_orgs_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "orgs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "orgs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "orgs_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "orgs_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "orgs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "orgs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "orgs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "orgs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "orgsd_orgs_oid", False)
        add_column_copy(gv_detail, "Organization Structure Master", "orgs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Organization Type", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Organization Name", "org_name_child", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Parent", "org_name_parent", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.orgs_mstr.orgs_oid, " _
                    & "  public.orgs_mstr.orgs_dom_id, " _
                    & "  public.orgs_mstr.orgs_en_id, " _
                    & "  public.orgs_mstr.orgs_add_by, " _
                    & "  public.orgs_mstr.orgs_add_date, " _
                    & "  public.orgs_mstr.orgs_upd_by, " _
                    & "  public.orgs_mstr.orgs_upd_date, " _
                    & "  public.orgs_mstr.orgs_id, " _
                    & "  public.orgs_mstr.orgs_code, " _
                    & "  public.orgs_mstr.orgs_desc, " _
                    & "  public.orgs_mstr.orgs_type, " _
                    & "  public.orgs_mstr.orgs_active, " _
                    & "  public.orgs_mstr.orgs_dt, " _
                    & "  public.en_mstr.en_desc " _
                    & "FROM " _
                    & "  public.orgs_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.orgs_mstr.orgs_en_id = public.en_mstr.en_id) " _
                    & " where orgs_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String
        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  orgsd_oid, " _
            & "  orgsd_add_by, " _
            & "  orgsd_add_date, " _
            & "  orgsd_upd_by, " _
            & "  orgsd_upd_date, " _
            & "  orgsd_seq, " _
            & "  orgsd_orgs_oid, " _
            & "  orgs_desc, " _
            & "  orgsd_org_type, " _
            & "  code_name, " _
            & "  orgsd_org_id, " _
            & "  child.org_name as org_name_child, " _
            & "  orgsd_parent_org, " _
            & "  parent.org_name as org_name_parent, " _
            & "  orgsd_dt " _
            & "FROM  " _
            & "  public.orgsd_det  " _
            & "inner join orgs_mstr on orgs_mstr.orgs_oid = orgsd_orgs_oid " _
            & "inner join code_mstr on code_id = orgsd_org_type " _
            & "inner join org_mstr child on child.org_id = orgsd_org_id " _
            & "left outer join org_mstr parent on parent.org_id = orgsd_parent_org" _
            & " order by orgsd_orgs_oid, orgsd_seq "

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("orgsd_orgs_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("orgsd_orgs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("orgs_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("orgsd_orgs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("orgs_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        sc_le_orgs_en_id.Focus()
        sc_le_orgs_en_id.ItemIndex = 0
        sc_cbe_orgs_type.SelectedIndex = 0
        sc_te_orgs_code.Text = ""
        sc_te_orgs_desc.Text = ""
        sc_ce_orgs_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _orgs_oid As Guid
        _orgs_oid = Guid.NewGuid
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
                                            & "  public.orgs_mstr " _
                                            & "( " _
                                            & "  orgs_oid, " _
                                            & "  orgs_dom_id, " _
                                            & "  orgs_en_id, " _
                                            & "  orgs_add_by, " _
                                            & "  orgs_add_date, " _
                                            & "  orgs_id, " _
                                            & "  orgs_code, " _
                                            & "  orgs_desc, " _
                                            & "  orgs_type, " _
                                            & "  orgs_active, " _
                                            & "  orgs_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_orgs_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & sc_le_orgs_en_id.EditValue & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetInteger(func_coll.GetID("orgs_mstr", sc_le_orgs_en_id.GetColumnValue("en_code"), "orgs_id", "orgs_en_id", sc_le_orgs_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(Trim(sc_te_orgs_code.Text)) & ",  " _
                                            & SetSetring(Trim(sc_te_orgs_desc.Text)) & ",  " _
                                            & SetSetring(sc_cbe_orgs_type.Text) & ",  " _
                                            & SetBitYN(sc_ce_orgs_active.EditValue) & ",  " _
                                            & " (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ") " _
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
                _orgs_oid_mstr = .Item("orgs_oid").ToString
                sc_le_orgs_en_id.EditValue = .Item("orgs_en_id")
                sc_te_orgs_code.Text = .Item("orgs_code")
                sc_te_orgs_desc.Text = .Item("orgs_desc")
                sc_cbe_orgs_type.Text = .Item("orgs_type")
                sc_ce_orgs_active.EditValue = IIf(.Item("orgs_active") = "Y", True, False)
            End With
            sc_le_orgs_en_id.Focus()
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
                                                & "  public.orgs_mstr   " _
                                                & "SET  " _
                                                & "  orgs_en_id = " & sc_le_orgs_en_id.EditValue & ",  " _
                                                & "  orgs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  orgs_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "),  " _
                                                & "  orgs_code = " & SetSetring(Trim(sc_te_orgs_code.Text)) & ",  " _
                                                & "  orgs_desc = " & SetSetring(Trim(sc_te_orgs_desc.Text)) & ",  " _
                                                & "  orgs_type = " & SetSetring(sc_cbe_orgs_type.Text) & ",  " _
                                                & "  orgs_active = " & SetBitYN(sc_ce_orgs_active.EditValue) & ",  " _
                                                & "  orgs_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ") " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  orgs_oid = " & SetSetring(_orgs_oid_mstr.ToString) & "  " _
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
                            .Command.CommandText = "delete from orgs_mstr where orgs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("orgs_oid").ToString + "'"
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
