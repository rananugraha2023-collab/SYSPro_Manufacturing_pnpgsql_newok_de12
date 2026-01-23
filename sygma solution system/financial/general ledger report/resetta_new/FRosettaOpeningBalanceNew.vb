Imports npgsql
Imports master_new.ModFunction

Public Class FRosettaOpeningBalanceNew
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
        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Year", "gcal_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "gcal_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "gcal_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "gcal_end_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "Code Group", "rosd_rosg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Name", "rosg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Line Code", "rosd_rost_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Transaction Line", "rost_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cashflow Code", "rost_cf", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Opening", "rosd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_edit, "rosd_rosg_code", False)
        add_column(gv_edit, "rosd_rost_code", False)
        add_column(gv_edit, "Account Group", "rosg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Opening Balance", "rosd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
       
        get_sequel = "SELECT  " _
                    & "  a.rosd_rosg_code, " _
                    & "  b.rosg_desc, " _
                    & "  b.rosg_default_sign, " _
                    & "  a.rosd_rost_code, " _
                    & "  c.rost_desc, " _
                    & "  c.rost_cf, " _
                    & "  a.rosd_amount, " _
                    & "  a.rosd_periode, " _
                    & "  d.gcal_year, " _
                    & "  d.gcal_periode, " _
                    & "  d.gcal_start_date, " _
                    & "  d.gcal_end_date " _
                    & "FROM " _
                    & "  public.rosd_data a " _
                    & "  INNER JOIN public.rosg_group b ON (a.rosd_rosg_code = b.rosg_code) " _
                    & "  INNER JOIN public.rost_trans_line c ON (a.rosd_rost_code = c.rost_code) " _
                    & "  INNER JOIN public.gcal_mstr d ON (a.rosd_periode = d.gcal_oid) " _
                    & "WHERE " _
                    & "  a.rosd_rost_code = 'T01' " _
                    & " and rosd_periode='" & le_periode.EditValue.ToString & "' " _
                    & " order by rosd_rosg_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
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
                        & "  rosg_code as rosd_rosg_code, " _
                        & "  rosg_desc, " _
                        & "  rosg_default_sign, " _
                        & "  'T01' as rosd_rost_code, " _
                        & "  0.0 as rosd_amount " _
                        & " FROM " _
                        & "  public.rosg_group " _
                        & " " _
                        & "  order by rosd_rosg_code"

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
                rstbal_gcal_oid.EditValue = .Item("rosd_periode").ToString
                _rstbal_gcal_oid_edit = .Item("rosd_periode").ToString
            End With

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.rosd_rosg_code, " _
                            & "  b.rosg_desc, " _
                            & "  b.rosg_default_sign, " _
                            & "  a.rosd_rost_code, " _
                            & "  c.rost_desc, " _
                            & "  c.rost_cf, " _
                            & "  a.rosd_amount, " _
                            & "  a.rosd_periode, " _
                            & "  d.gcal_year, " _
                            & "  d.gcal_periode, " _
                            & "  d.gcal_start_date, " _
                            & "  d.gcal_end_date " _
                            & "FROM " _
                            & "  public.rosd_data a " _
                            & "  INNER JOIN public.rosg_group b ON (a.rosd_rosg_code = b.rosg_code) " _
                            & "  INNER JOIN public.rost_trans_line c ON (a.rosd_rost_code = c.rost_code) " _
                            & "  INNER JOIN public.gcal_mstr d ON (a.rosd_periode = d.gcal_oid) " _
                            & "WHERE " _
                            & "  a.rosd_rost_code = 'T01' " _
                            & " and rosd_periode='" & le_periode.EditValue.ToString & "' " _
                            & " order by rosd_rosg_code"

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
                        .Command.CommandText = "delete from rosd_data where rosd_periode = " & SetSetring(le_periode.EditValue)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1

                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.rstbal_mstr " _
                        '                        & "( " _
                        '                        & "  rstbal_oid, " _
                        '                        & "  rstbal_dom_id, " _
                        '                        & "  rstbal_en_id, " _
                        '                        & "  rstbal_add_by, " _
                        '                        & "  rstbal_add_date, " _
                        '                        & "  rstbal_rstrule_oid, " _
                        '                        & "  rstbal_gcal_oid, " _
                        '                        & "  rstbal_openbal_amount, " _
                        '                        & "  rstbal_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                        '                        & SetInteger("") & ",  " _
                        '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                        & "current_timestamp" & ",  " _
                        '                        & SetSetring(ds_edit.Tables(0).Rows(i).Item("rstrule_oid")) & ",  " _
                        '                        & SetSetring(rstbal_gcal_oid.EditValue) & ",  " _
                        '                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("rstbal_openbal_amount")) & ",  " _
                        '                        & " current_timestamp " & "  " _
                        '                        & ");"

                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()

                        'Next
                        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.rosd_data " _
                                            & "( " _
                                            & "  rosd_oid, " _
                                            & "  rosd_rosg_code, " _
                                            & "  rosd_amount, " _
                                            & "  rosd_rost_code, " _
                                            & "  rosd_periode " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("rosd_rosg_code")) & ",  " _
                                            & SetDec(ds_edit.Tables(0).Rows(i).Item("rosd_amount")) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("rosd_rost_code")) & ",  " _
                                            & SetSetring(rstbal_gcal_oid.EditValue) & "  " _
                                            & ")"


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
                        .Command.CommandText = "SELECT  " _
                                            & "  a.rosd_oid " _
                                            & "FROM " _
                                            & "  public.rosd_data a " _
                                            & "WHERE " _
                                            & "  a.rosd_periode =" & SetSetring(rstbal_gcal_oid.EditValue)

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
                                            & "  public.rosd_data " _
                                            & "( " _
                                            & "  rosd_oid, " _
                                            & "  rosd_rosg_code, " _
                                            & "  rosd_amount, " _
                                            & "  rosd_rost_code, " _
                                            & "  rosd_periode " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("rosd_rosg_code")) & ",  " _
                                            & SetDec(ds_edit.Tables(0).Rows(i).Item("rosd_amount")) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("rosd_rost_code")) & ",  " _
                                            & SetSetring(rstbal_gcal_oid.EditValue) & "  " _
                                            & ")"


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
