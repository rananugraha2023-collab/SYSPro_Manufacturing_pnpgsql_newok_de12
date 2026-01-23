Imports npgsql
Imports master_new.ModFunction

Public Class FEmployee
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FEmployee_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_is_employee_oid())
        sales_id.Properties.DataSource = dt_bantu
        sales_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        sales_id.Properties.ValueMember = dt_bantu.Columns("ptnr_oid").ToString
        sales_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gender())
        emp_gender.Properties.DataSource = dt_bantu
        emp_gender.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        emp_gender.Properties.ValueMember = dt_bantu.Columns("value").ToString
        emp_gender.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_orgs_mstr())
        emp_orgs_id.Properties.DataSource = dt_bantu
        emp_orgs_id.Properties.DisplayMember = dt_bantu.Columns("orgs_desc").ToString
        emp_orgs_id.Properties.ValueMember = dt_bantu.Columns("orgs_id").ToString
        emp_orgs_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_orgs_mstr())
        emp_orgs_aprv.Properties.DataSource = dt_bantu
        emp_orgs_aprv.Properties.DisplayMember = dt_bantu.Columns("orgs_desc").ToString
        emp_orgs_aprv.Properties.ValueMember = dt_bantu.Columns("orgs_id").ToString
        emp_orgs_aprv.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr("position"))
        emp_pos_id.Properties.DataSource = dt_bantu
        emp_pos_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        emp_pos_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        emp_pos_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_area_mstr())
        emp_area_id.Properties.DataSource = dt_bantu
        emp_area_id.Properties.DisplayMember = dt_bantu.Columns("area_name").ToString
        emp_area_id.Properties.ValueMember = dt_bantu.Columns("area_id").ToString
        emp_area_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "First Name", "emp_fname", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Middle Name", "emp_mname", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Last Name", "emp_lname", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Gender", "emp_gender", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Position", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Birt Date", "emp_birth_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Birth Place", "emp_birth_place", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Organization Structure", "orgs_structure_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Approval Structure", "aprv_structure_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Relation Status", "emp_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  emp_oid, " _
                    & "  emp_id, " _
                    & "  emp_add_by, " _
                    & "  emp_add_date, " _
                    & "  emp_upd_by, " _
                    & "  emp_upd_date, " _
                    & "  emp_fname, " _
                    & "  emp_mname, " _
                    & "  emp_lname, " _
                    & "  emp_gender, " _
                    & "  emp_pos_id, " _
                    & "  emp_birth_date, " _
                    & "  emp_birth_place, " _
                    & "  emp_relation, " _
                    & "  emp_orgs_id, " _
                    & "  org.orgs_desc as orgs_structure_name, " _
                    & "  emp_orgs_aprv, " _
                    & "  aprv.orgs_desc as aprv_structure_name, " _
                    & "  code_name, " _
                    & "  ptnr_name, " _
                    & "  emp_area_id, " _
                    & "  area_name, " _
                    & "  emp_dt " _
                    & "FROM  " _
                    & "  public.emp_mstr " _
                    & " left outer join public.ptnr_mstr on ptnr_oid = emp_oid" _
                    & " inner join public.code_mstr on code_id = emp_pos_id" _
                    & " inner join public.orgs_mstr org on org.orgs_id = emp_orgs_id" _
                    & " inner join public.orgs_mstr aprv on aprv.orgs_id = emp_orgs_aprv " _
                    & " inner join public.area_mstr on area_id = emp_area_id "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        emp_fname.Focus()
        emp_fname.Text = ""
        emp_mname.Text = ""
        emp_lname.Text = ""
        emp_gender.ItemIndex = 0
        emp_pos_id.ItemIndex = 0
        emp_birth_date.Text = ""
        emp_birth_place.Text = ""
        emp_orgs_aprv.ItemIndex = 0
        emp_orgs_id.ItemIndex = 0
        emp_relation.EditValue = False
        lci_relation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        sales_id.ItemIndex = 0
        emp_area_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _emp_oid As String
        Dim ssqls As New ArrayList

        If emp_relation.EditValue = True Then
            _emp_oid = sales_id.EditValue.ToString
        Else
            _emp_oid = Guid.NewGuid.ToString
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
                                            & "  public.emp_mstr " _
                                            & "( " _
                                            & "  emp_oid, " _
                                            & "  emp_add_by, " _
                                            & "  emp_add_date, " _
                                            & "  emp_id, " _
                                            & "  emp_fname, " _
                                            & "  emp_mname, " _
                                            & "  emp_lname, " _
                                            & "  emp_orgs_id, " _
                                            & "  emp_orgs_aprv, " _
                                            & "  emp_gender, " _
                                            & "  emp_pos_id, " _
                                            & "  emp_area_id, " _
                                            & "  emp_birth_date, " _
                                            & "  emp_birth_place, " _
                                            & "  emp_relation, " _
                                            & "  emp_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_emp_oid) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("emp_mstr", "emp_id")) & ",  " _
                                            & SetSetring(emp_fname.Text) & ",  " _
                                            & SetSetring(emp_mname.Text) & ",  " _
                                            & SetSetring(emp_lname.Text) & ",  " _
                                            & SetInteger(emp_orgs_id.EditValue) & ",  " _
                                            & SetInteger(emp_orgs_aprv.EditValue) & ",  " _
                                            & SetSetring(emp_gender.EditValue) & ",  " _
                                            & SetInteger(emp_pos_id.EditValue) & ",  " _
                                            & SetInteger(emp_area_id.EditValue) & ",  " _
                                            & SetDate(emp_birth_date.DateTime) & ",  " _
                                            & SetSetring(emp_birth_place.Text) & ",  " _
                                            & SetBitYN(emp_relation.EditValue) & ",  " _
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
                        set_row(Trim(emp_fname.Text), "emp_fname")
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
                emp_fname.Text = SetString(.Item("emp_fname"))
                emp_mname.Text = SetString(.Item("emp_mname"))
                emp_lname.Text = SetString(.Item("emp_lname"))
                emp_gender.EditValue = .Item("emp_gender")
                emp_pos_id.EditValue = .Item("emp_pos_id")
                emp_area_id.EditValue = .Item("emp_area_id")
                emp_birth_date.DateTime = .Item("emp_birth_date")
                emp_birth_place.Text = SetString(.Item("emp_birth_place"))
                emp_orgs_id.EditValue = .Item("emp_orgs_id")
                emp_orgs_aprv.EditValue = .Item("emp_orgs_aprv")
                emp_relation.EditValue = SetBitYNB(.Item("emp_relation"))

                If .Item("emp_relation").ToString.ToUpper = "Y" Then
                    lci_relation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    sales_id.EditValue = .Item("emp_oid")
                Else
                    lci_relation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

            End With
            emp_fname.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim _emp_oid As String
        Dim ssqls As New ArrayList

        If emp_relation.EditValue = True Then
            _emp_oid = sales_id.EditValue.ToString
        Else
            _emp_oid = Guid.NewGuid.ToString
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.emp_mstr   " _
                                            & "SET  " _
                                            & "  emp_oid = " & SetSetring(_emp_oid.ToString) & ",  " _
                                            & "  emp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  emp_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  emp_fname = " & SetSetring(emp_fname.Text) & ",  " _
                                            & "  emp_mname = " & SetSetring(emp_mname.Text) & ",  " _
                                            & "  emp_lname = " & SetSetring(emp_lname.Text) & ",  " _
                                            & "  emp_orgs_id = " & SetInteger(emp_orgs_id.EditValue) & ",  " _
                                            & "  emp_orgs_aprv = " & SetInteger(emp_orgs_aprv.EditValue) & ",  " _
                                            & "  emp_gender = " & SetSetring(emp_gender.EditValue) & ",  " _
                                            & "  emp_pos_id = " & SetInteger(emp_pos_id.EditValue) & ",  " _
                                            & "  emp_area_id = " & SetInteger(emp_area_id.EditValue) & ",  " _
                                            & "  emp_birth_date = " & SetDate(emp_birth_date.DateTime) & ",  " _
                                            & "  emp_birth_place = " & SetSetring(emp_birth_place.Text) & ",  " _
                                            & "  emp_relation = " & SetBitYN(emp_relation.EditValue) & ",  " _
                                            & "  emp_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  emp_id = " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("emp_id").ToString & " "
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
                        set_row(Trim(emp_fname.Text), "emp_fname")
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
                            .Command.CommandText = "delete from emp_mstr where emp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("emp_oid").ToString + "'"
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

    Private Sub ce_relation_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles emp_relation.EditValueChanged
        If emp_relation.EditValue = False Then
            lci_relation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lci_relation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            sales_id.ItemIndex = 0
        End If
    End Sub
End Class
