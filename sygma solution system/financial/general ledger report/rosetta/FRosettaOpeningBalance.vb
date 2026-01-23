Imports npgsql
Imports master_new.ModFunction

Public Class FRosettaOpeningBalance
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim ds_import As DataSet
    Dim _rstbal_en_id_edit As String
    Dim _rstbal_gcal_oid_edit As String
    Dim _is_edit As Boolean

    Private Sub FRosettaOpeningBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())

        If le_periode.Properties.Columns.VisibleCount = 0 Then
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
        End If

        le_periode.Properties.DropDownRows = 12
        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        rstbal_en_id.Properties.DataSource = dt_bantu
        rstbal_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        rstbal_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        rstbal_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())
        If rstbal_gcal_oid.Properties.Columns.VisibleCount = 0 Then
            rstbal_gcal_oid.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            rstbal_gcal_oid.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
        End If

        rstbal_gcal_oid.Properties.DropDownRows = 12
        rstbal_gcal_oid.Properties.DataSource = dt_bantu
        rstbal_gcal_oid.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        rstbal_gcal_oid.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        rstbal_gcal_oid.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "rstbal_oid", False)
        add_column(gv_master, "rstbal_rstrule_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Year", "gcal_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "gcal_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "gcal_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "gcal_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "account_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cashflow Code", "cashflow_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Opening", "rstbal_openbal_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Amount", "rstbal_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total", "rstbal_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "rstbal_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "rstbal_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "rstbal_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "rstbal_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "rstrule_oid", True)
        add_column(gv_edit, "rstrule_group_id", True)
        add_column(gv_edit, "Account Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "rstrule_name_id", True)
        add_column(gv_edit, "Account Name", "account_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "rstrule_line_id", True)
        add_column(gv_edit, "Transaction Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "rstrule_cashflow_id", True)
        add_column(gv_edit, "Cashflow Code", "cashflow_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_edit(gv_edit, "Opening Balance", "rstbal_openbal_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  rstbal_oid, " _
                    & "  rstbal_dom_id, " _
                    & "  rstbal_en_id, " _
                    & "  rstbal_add_by, " _
                    & "  rstbal_add_date, " _
                    & "  rstbal_upd_by, " _
                    & "  rstbal_upd_date, " _
                    & "  rstbal_rstrule_oid, " _
                    & "  rstbal_gcal_oid, " _
                    & "  rstbal_amount,rstbal_openbal_amount,coalesce(rstbal_openbal_amount,0) + coalesce(rstbal_amount,0) as rstbal_total,  " _
                    & "  rstbal_dt, " _
                    & "  en_desc, " _
                    & "  gcal_year, " _
                    & "  gcal_periode, " _
                    & "  gcal_start_date, " _
                    & "  gcal_end_date, " _
                    & "  group_mstr.code_name as group_name, " _
                    & "  account_mstr.code_name as account_name, " _
                    & "  tranline_mstr.code_name as tranline_name, " _
                    & "  cashflow_mstr.code_name as cashflow_name " _
                    & " FROM  " _
                    & "  public.rstbal_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
                    & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
                    & "  INNER JOIN public.rstrule_mstr ON (public.rstrule_mstr.rstrule_oid = public.rstbal_mstr.rstbal_rstrule_oid)" _
                    & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                    & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                    & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                    & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                    & " where rstbal_en_id = " + le_entity.EditValue.ToString _
                    & " and rstbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
                    & " and rstbal_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        rstbal_en_id.Focus()
        rstbal_en_id.ItemIndex = 0
        rstbal_gcal_oid.ItemIndex = 0
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        _is_edit = False
        Dim ds_bantu As New DataSet
        
        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  rstrule_oid, " _
                        & "  rstrule_group_id, " _
                        & "  rstrule_name_id, " _
                        & "  rstrule_line_id, " _
                        & "  rstrule_cashflow_id, " _
                        & "  group_mstr.code_name as group_name, " _
                        & "  account_mstr.code_name as account_name, " _
                        & "  tranline_mstr.code_name as tranline_name, " _
                        & "  cashflow_mstr.code_name as cashflow_name, " _
                        & "  0.00 as rstbal_openbal_amount " _
                        & "FROM  " _
                        & "  public.rstrule_mstr " _
                        & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                        & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                        & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                        & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                        & " order by group_name,cashflow_name,account_name, tranline_name"


                    .InitializeCommand()
                    .FillDataSet(ds_edit, "bantu")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function before_edit() As Boolean
        before_edit = True
        Return before_edit
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position
            _is_edit = True
            With ds.Tables(0).Rows(row)
                rstbal_en_id.EditValue = .Item("rstbal_en_id")
                rstbal_gcal_oid.EditValue = .Item("rstbal_gcal_oid").ToString

                'persiapan jika ganti periode
                _rstbal_en_id_edit = .Item("rstbal_en_id")
                _rstbal_gcal_oid_edit = .Item("rstbal_gcal_oid").ToString
                '-----------------------------------------
            End With

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  rstrule_oid, " _
                            & "  rstrule_group_id, " _
                            & "  rstrule_name_id, " _
                            & "  rstrule_line_id, " _
                            & "  rstrule_cashflow_id, " _
                            & "  group_mstr.code_name as group_name, " _
                            & "  account_mstr.code_name as account_name, " _
                            & "  tranline_mstr.code_name as tranline_name, " _
                            & "  cashflow_mstr.code_name as cashflow_name, " _
                            & "  sum(coalesce(rstbal_openbal_amount,0.00)) as rstbal_openbal_amount " _
                            & "FROM  " _
                            & "  public.rstrule_mstr " _
                            & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                            & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                            & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                            & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                            & "  LEFT OUTER JOIN rstbal_mstr on rstbal_rstrule_oid = rstrule_oid  " _
                            & " group by " _
                            & "  rstrule_oid, " _
                            & "  rstrule_group_id, " _
                            & "  rstrule_name_id, " _
                            & "  rstrule_line_id, " _
                            & "  rstrule_cashflow_id, " _
                            & "  group_mstr.code_name, " _
                            & "  account_mstr.code_name, " _
                            & "  tranline_mstr.code_name, " _
                            & "  cashflow_mstr.code_name "



                        .InitializeCommand()
                        .FillDataSet(ds_edit, "bantu")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True

        End If
    End Function

    Public Overrides Function edit()
        edit = True

        ds_edit.AcceptChanges()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from rstbal_mstr where rstbal_en_id = " & SetInteger(_rstbal_en_id_edit) & _
                                               " and rstbal_gcal_oid = " & SetSetring(_rstbal_gcal_oid_edit.ToString)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.rstbal_mstr " _
                                                & "( " _
                                                & "  rstbal_oid, " _
                                                & "  rstbal_dom_id, " _
                                                & "  rstbal_en_id, " _
                                                & "  rstbal_add_by, " _
                                                & "  rstbal_add_date, " _
                                                & "  rstbal_rstrule_oid, " _
                                                & "  rstbal_gcal_oid, " _
                                                & "  rstbal_openbal_amount, " _
                                                & "  rstbal_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(rstbal_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("rstrule_oid")) & ",  " _
                                                & SetSetring(rstbal_gcal_oid.EditValue) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("rstbal_openbal_amount")) & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ");"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        sqlTran.Commit()
                        le_periode.EditValue = rstbal_gcal_oid.EditValue.ToString
                        after_success()

                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function



    Public Overrides Function before_save() As Boolean
        before_save = True

        If _is_edit = False Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select rstbal_oid, gcal_start_date, gcal_end_date  from rstbal_mstr inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " + _
                                               " where rstbal_gcal_oid = " + SetSetring(rstbal_gcal_oid.EditValue)
                        '" and rstbal_en_id = " + SetInteger(rstbal_en_id.EditValue)

                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader

                        While .DataReader.Read
                            MsgBox("Rosetta Opening Balance for this Period Already Available", MsgBoxStyle.Critical, "Double Data")
                            Return False
                        End While

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            before_edit()
        End If
        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim i As Integer
        ds_edit.AcceptChanges()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.rstbal_mstr " _
                                                & "( " _
                                                & "  rstbal_oid, " _
                                                & "  rstbal_dom_id, " _
                                                & "  rstbal_en_id, " _
                                                & "  rstbal_add_by, " _
                                                & "  rstbal_add_date, " _
                                                & "  rstbal_rstrule_oid, " _
                                                & "  rstbal_gcal_oid, " _
                                                & "  rstbal_openbal_amount, " _
                                                & "  rstbal_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(rstbal_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("rstrule_oid")) & ",  " _
                                                & SetSetring(rstbal_gcal_oid.EditValue) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("rstbal_openbal_amount")) & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ");"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        sqlTran.Commit()
                        le_periode.EditValue = rstbal_gcal_oid.EditValue.ToString
                        after_success()

                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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

End Class
