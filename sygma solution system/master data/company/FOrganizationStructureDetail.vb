Imports npgsql
Imports master_new.ModFunction

Public Class FOrganizationStructureDetail
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Dim _orgsd_oid_mstr As String

    Private Sub FOrganizationStructureDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("orgs_mstr", ""))
        sc_le_orgsd_orgs_id.Properties.DataSource = dt_bantu
        sc_le_orgsd_orgs_id.Properties.DisplayMember = dt_bantu.Columns("orgs_desc").ToString
        sc_le_orgsd_orgs_id.Properties.ValueMember = dt_bantu.Columns("orgs_oid").ToString
        sc_le_orgsd_orgs_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Organization Structure Master", "orgs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Organization Type", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Organization Name", "org_name_child", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Parent", "org_name_parent", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "orgsd_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "orgsd_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "orgsd_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "orgsd_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
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

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_orgsd_orgs_id.Focus()
        sc_le_orgsd_orgs_id.ItemIndex = 0
        sc_le_orgsd_orgs_id_ValueChanged()
        'sc_le_orgsd_org_type.ItemIndex = 0
        'sc_le_orgsd_org_id.ItemIndex = 0
        'sc_le_orgsd_parent_org.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _orgsd_oid As Guid
        _orgsd_oid = Guid.NewGuid
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
                                            & "  public.orgsd_det " _
                                            & "( " _
                                            & "  orgsd_oid, " _
                                            & "  orgsd_add_by, " _
                                            & "  orgsd_add_date, " _
                                            & "  orgsd_seq, " _
                                            & "  orgsd_orgs_oid, " _
                                            & "  orgsd_org_type, " _
                                            & "  orgsd_org_id, " _
                                            & "  orgsd_parent_org, " _
                                            & "  orgsd_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_orgsd_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "(select coalesce(max(orgsd_seq),0) + 1 from orgsd_det where orgsd_orgs_oid = '" + sc_le_orgsd_orgs_id.EditValue + "')" & ",  " _
                                            & SetSetring(sc_le_orgsd_orgs_id.EditValue) & ",  " _
                                            & sc_le_orgsd_org_type.EditValue & ",  " _
                                            & sc_le_orgsd_org_id.EditValue & ",  " _
                                            & sc_le_orgsd_parent_org.EditValue & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " )"

                        '& IIf(sc_le_orgsd_parent_org.ItemIndex = 0, "null", sc_le_orgsd_parent_org.EditValue) & ",  " _
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

    Private Sub sc_le_orgsd_orgs_id_ValueChanged()
        Dim _org_type As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select orgsd_org_type from orgsd_det od " + _
                                           " where od.orgsd_orgs_oid = '" + sc_le_orgsd_orgs_id.EditValue.ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _org_type = .DataReader("orgsd_org_type")
                        End While
                    Else
                        _org_type = 0
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'org_type_id' " + _
                           " and code_active ~~* 'Y'" + _
                           " and code_id > " + _org_type.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "org_type")
                    sc_le_orgsd_org_type.Properties.DataSource = ds_bantu.Tables("org_type")
                    sc_le_orgsd_org_type.Properties.DisplayMember = ds_bantu.Tables("org_type").Columns("code_desc").ToString
                    sc_le_orgsd_org_type.Properties.ValueMember = ds_bantu.Tables("org_type").Columns("code_id").ToString
                    sc_le_orgsd_org_type.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sc_le_orgsd_orgs_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_orgsd_orgs_id.EditValueChanged
        sc_le_orgsd_orgs_id_ValueChanged()
    End Sub

    Private Sub sc_le_orgsd_org_type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_orgsd_org_type.EditValueChanged
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select org_id, org_type_id, org_name from org_mstr where org_type_id = " + sc_le_orgsd_org_type.EditValue.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "org_name")
                    sc_le_orgsd_org_id.Properties.DataSource = ds_bantu.Tables("org_name")
                    sc_le_orgsd_org_id.Properties.DisplayMember = ds_bantu.Tables("org_name").Columns("org_name").ToString
                    sc_le_orgsd_org_id.Properties.ValueMember = ds_bantu.Tables("org_name").Columns("org_id").ToString
                    sc_le_orgsd_org_id.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sc_le_orgsd_org_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_orgsd_org_id.EditValueChanged
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select org_id, org_name from org_mstr where org_type_id < " + sc_le_orgsd_org_id.GetColumnValue("org_type_id").ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "org_name")
                    sc_le_orgsd_parent_org.Properties.DataSource = ds_bantu.Tables("org_name")
                    sc_le_orgsd_parent_org.Properties.DisplayMember = ds_bantu.Tables("org_name").Columns("org_name").ToString
                    sc_le_orgsd_parent_org.Properties.ValueMember = ds_bantu.Tables("org_name").Columns("org_id").ToString
                    sc_le_orgsd_parent_org.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

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
                            .Command.CommandText = "delete from orgsd_det where orgsd_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("orgsd_oid").ToString + "'"
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
