Imports npgsql
Imports master_new.ModFunction


Imports master_new.PGSqlConn
Public Class FGLCalendar
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim _gcal_oid_mstr As String
    Dim sSQL As String
#Region "Seting Awal"
    Private Sub FGLCalendar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Year Periode", "gcal_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "gcal_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode Start", "gcal_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Periode End", "gcal_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "gcal_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "gcal_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "gcal_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "gcal_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Pra Closing", "gcal_pra_closing", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Closing", "gcal_closing", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "gcald_gcal_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "Account Payable", "gcald_ap", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "Account Receiveable", "gcald_ar", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "Fix Asset", "gcald_fa", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "Inventory Control", "gcald_ic", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "Sales Order", "gcald_so", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "General Ledger", "gcald_gl", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_detail, "Year", "gcald_year", DevExpress.Utils.HorzAlignment.Near)

        add_column(gv_edit, "gcald_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "Account Payable", "gcald_ap", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "Account Receiveable", "gcald_ar", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "Fix Asset", "gcald_fa", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "Inventory Control", "gcald_ic", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "Sales Order", "gcald_so", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "General Ledger", "gcald_gl", DevExpress.Utils.HorzAlignment.Near)
        add_column_edit(gv_edit, "Year", "gcald_year", DevExpress.Utils.HorzAlignment.Near)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.gcal_mstr.gcal_oid, " _
                    & "  public.gcal_mstr.gcal_dom_id, " _
                    & "  public.gcal_mstr.gcal_add_by, " _
                    & "  public.gcal_mstr.gcal_add_date, " _
                    & "  public.gcal_mstr.gcal_upd_by, " _
                    & "  public.gcal_mstr.gcal_upd_date, " _
                    & "  public.gcal_mstr.gcal_year, " _
                    & "  public.gcal_mstr.gcal_periode, " _
                    & "  public.gcal_mstr.gcal_start_date, " _
                    & "  public.gcal_mstr.gcal_end_date, " _
                    & "  public.gcal_mstr.gcal_dt,coalesce(gcal_pra_closing,'N') as gcal_pra_closing,coalesce(gcal_closing,'N') as  gcal_closing " _
                    & "FROM " _
                    & "  public.gcal_mstr order by gcal_start_date desc"
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
            & "  gcald_oid, " _
            & "  gcald_gcal_oid, " _
            & "  gcald_en_id, " _
            & "  en_desc, " _
            & "  gcald_ap, " _
            & "  gcald_ar, " _
            & "  gcald_fa, " _
            & "  gcald_ic, " _
            & "  gcald_so, " _
            & "  gcald_gl, " _
            & "  gcald_year, " _
            & "  gcal_dt " _
            & "FROM  " _
            & "  public.gcald_det " _
            & "  inner join public.en_mstr on en_id = gcald_en_id order by en_desc"

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("gcald_gcal_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("gcald_gcal_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("gcald_gcal_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        gcal_periode.Focus()
        gcal_year.DateTime = Now
        gcal_periode.Text = ""
        gcal_start_date.DateTime = Now
        gcal_end_date.DateTime = Now

        gcal_year.Enabled = True
        gcal_periode.Enabled = True
        gcal_start_date.Enabled = True
        gcal_end_date.Enabled = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  gcald_oid, " _
                        & "  gcald_gcal_oid, " _
                        & "  gcald_en_id, " _
                        & "  en_desc, " _
                        & "  gcald_ap, " _
                        & "  gcald_ar, " _
                        & "  gcald_fa, " _
                        & "  gcald_ic, " _
                        & "  gcald_so, " _
                        & "  gcald_gl, " _
                        & "  gcald_year, " _
                        & "  gcal_dt " _
                        & "FROM  " _
                        & "  public.gcald_det " _
                        & "  inner join public.en_mstr on en_id = gcald_en_id " _
                        & " where gcald_en_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If SetString(gcal_periode.EditValue) = "" Then
            MessageBox.Show("Periode Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Return before_save
    End Function

    Private Function before_save_local() As Boolean
        before_save_local = True

        If gcal_start_date.DateTime > gcal_end_date.DateTime Then
            MessageBox.Show("Start Date Can't Higher Than End Date..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gcal_end_date.Focus()
            Return False
        End If

        If gcal_start_date.Text = "" Then
            MessageBox.Show("Start Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gcal_start_date.Focus()
            Return False
        End If

        If gcal_end_date.Text = "" Then
            MessageBox.Show("End Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gcal_end_date.Focus()
            Return False
        End If

        Dim _gcal_end_date As Date = "01/01/1909"
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_end_date from gcal_mstr " + _
                                           " order by gcal_end_date desc limit 1"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _gcal_end_date = .DataReader("gcal_end_date").ToString
                    End While

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _gcal_end_date > gcal_start_date.DateTime Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gcal_start_date.Focus()
            Return False
        End If

        Return before_save_local
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_local() = False Then
            Return False
        End If

        Dim _gcal_oid As String
        _gcal_oid = Guid.NewGuid.ToString
        Dim ssqls As New ArrayList

        Dim i As Integer

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
                                            & "  public.gcal_mstr " _
                                            & "( " _
                                            & "  gcal_oid, " _
                                            & "  gcal_dom_id, " _
                                            & "  gcal_add_by, " _
                                            & "  gcal_add_date, " _
                                            & "  gcal_year, " _
                                            & "  gcal_periode, " _
                                            & "  gcal_start_date, " _
                                            & "  gcal_end_date, " _
                                            & "  gcal_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_gcal_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(gcal_year.Text) & ",  " _
                                            & SetInteger(gcal_periode.EditValue) & ",  " _
                                            & SetDate(gcal_start_date.DateTime) & ",  " _
                                            & SetDate(gcal_end_date.DateTime) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.gcald_det " _
                                                & "( " _
                                                & "  gcald_oid, " _
                                                & "  gcald_gcal_oid, " _
                                                & "  gcald_en_id, " _
                                                & "  gcald_ap, " _
                                                & "  gcald_ar, " _
                                                & "  gcald_fa, " _
                                                & "  gcald_ic, " _
                                                & "  gcald_so, " _
                                                & "  gcald_gl, " _
                                                & "  gcald_year, " _
                                                & "  gcal_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_gcal_oid.ToString) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("gcald_en_id")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_ap")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_ar")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_fa")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_ic")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_so")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_gl")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_year")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

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
                        set_row(_gcal_oid, "gcal_oid")
                        insert = True
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
        Dim ssql As String

        row = BindingContext(ds.Tables(0)).Position

        ssql = "SELECT  " _
               & "  a.gcal_pra_closing,gcal_closing " _
               & "FROM " _
               & "  public.gcal_mstr a " _
               & "WHERE gcal_oid='" & ds.Tables(0).Rows(row).Item("gcal_oid").ToString & "'"

        Dim dt As New DataTable

        dt = master_new.PGSqlConn.GetTableData(ssql)

        Dim _status As String = ""

        For Each dr As DataRow In dt.Rows
            _status = SetString(dr("gcal_pra_closing"))
        Next

        If _status = "Y" Then
            Box("This periode have been processed")
            'cancel_data()
            Return False
            Exit Function
        End If
        If MyBase.edit_data = True Then

            With ds.Tables(0).Rows(row)
                _gcal_oid_mstr = .Item("gcal_oid").ToString
                gcal_year.DateTime = "01/01/" + .Item("gcal_year").ToString
                gcal_periode.EditValue = .Item("gcal_periode")
                gcal_start_date.DateTime = .Item("gcal_start_date")
                gcal_end_date.DateTime = .Item("gcal_end_date")
            End With

            gcal_year.Focus()

            gcal_year.Enabled = False
            gcal_periode.Enabled = False

            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = True Then
                gcal_start_date.Enabled = True
                gcal_end_date.Enabled = True
            Else
                gcal_start_date.Enabled = False
                gcal_end_date.Enabled = False
            End If


            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  gcald_oid, " _
                                & "  gcald_gcal_oid, " _
                                & "  gcald_en_id, " _
                                & "  en_desc, " _
                                & "  gcald_ap, " _
                                & "  gcald_ar, " _
                                & "  gcald_fa, " _
                                & "  gcald_ic, " _
                                & "  gcald_so, " _
                                & "  gcald_gl, " _
                                & "  gcald_year, " _
                                & "  gcal_dt " _
                                & "FROM  " _
                                & "  public.gcald_det " _
                                & "  inner join public.en_mstr on en_id = gcald_en_id " _
                                & " where gcald_gcal_oid = '" + ds.Tables(0).Rows(row).Item("gcal_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
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
        Dim i As Integer
        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        'Ini tidak bisa diedit bung !!!
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.gcal_mstr   " _
                                            & "SET  " _
                                            & "  gcal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  gcal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " ,  " _
                                            & "  gcal_start_date = " & SetDateNTime00(gcal_start_date.DateTime) & " ,  " _
                                            & "  gcal_end_date = " & SetDateNTime00(gcal_end_date.DateTime) & " ,  " _
                                            & "  gcal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  gcal_oid = " & SetSetring(_gcal_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Delete Data Detail Sebelum Insert
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from gcald_det where gcald_gcal_oid = '" + _gcal_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.gcald_det " _
                                                & "( " _
                                                & "  gcald_oid, " _
                                                & "  gcald_gcal_oid, " _
                                                & "  gcald_en_id, " _
                                                & "  gcald_ap, " _
                                                & "  gcald_ar, " _
                                                & "  gcald_fa, " _
                                                & "  gcald_ic, " _
                                                & "  gcald_so, " _
                                                & "  gcald_gl, " _
                                                & "  gcald_year, " _
                                                & "  gcal_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_gcal_oid_mstr.ToString) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("gcald_en_id")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_ap")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_ar")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_fa")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_ic")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_so")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_gl")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("gcald_year")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_gcal_oid_mstr, "gcal_oid")
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
                            .Command.CommandText = "delete from gcal_mstr where gcal_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid") + "'"
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Sub cancel_line()
        If ask("Are you sure to cancel ? ", "Confirmation ... ") = False Then
            Exit Sub
        End If
        Dim sSQLs As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization cancel...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        sSQL = "delete from glt_det where glt_type='JE' and glt_date between " _
            & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_end_date")) & " and " _
            & SetDateNTime00(DateAdd(DateInterval.Day, 1, CDate(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_end_date"))))
        sSQLs.Add(sSQL)

        'Dim dt As New DataTable
        'dt = master_new.PGSqlConn.GetTableData(sSQL)


        'sSQL = "update glbal_balance set glbal_balance_end_month1=0,glbal_balance_posted_end_month=0, glbal_balance_trial=0 " _
        '      & "where glbal_gcal_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid").ToString & "'  "

        'sSQLs.Add(sSQL)

        If master_new.PGSqlConn.status_sync = True Then
            If master_new.PGSqlConn.DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                Exit Sub
            End If
            sSQLs.Clear()
        Else
            If master_new.PGSqlConn.DbRunTran(sSQLs, "") = False Then
                Exit Sub
            End If
            sSQLs.Clear()
        End If

        System.Windows.Forms.Application.DoEvents()

        '===============recalculate====================
        ' LblProses.Text = "Status : Pra Closing, Recalculate"
        System.Windows.Forms.Application.DoEvents()

        sSQL = "UPDATE  " _
             & "  public.glbal_balance   " _
             & "SET  " _
             & "  glbal_balance_unposted = 0,  " _
              & "  glbal_balance_posted_end_month = 0,  " _
             & "  glbal_balance_posted = 0,glbal_balance_end_month1=0, glbal_balance_trial=0  " _
             & "WHERE  " _
             & "  glbal_gcal_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid").ToString)

        sSQLs.Add(sSQL)

        sSQL = " select glt_ac_id,glt_en_id,sum(unpost) as unpost_all,sum(posted) as posted_all from ( " _
            & " SELECT " _
            & "a.glt_ac_id,a.glt_en_id, " _
            & " sum(0) as unpost, sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit) * a.glt_exc_rate) as posted  " _
            & "FROM " _
            & "  public.glt_det a " _
            & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
            & "WHERE " _
            & "  a.glt_date BETWEEN " & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_start_date")) _
            & " AND " & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_end_date")) & " and a.glt_posted='Y' " _
            & " group by a.glt_ac_id,a.glt_en_id " _
            & " union all  " _
            & "  SELECT " _
            & "a.glt_ac_id,a.glt_en_id, " _
            & "  sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit)  * a.glt_exc_rate) as unpost, sum(0) as posted  " _
            & "FROM " _
            & "  public.glt_det a " _
            & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
            & "WHERE " _
            & "  a.glt_date BETWEEN " & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_start_date")) _
            & " AND " & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_end_date")) & " and a.glt_posted='N' " _
            & " group by a.glt_ac_id,a.glt_en_id) as temp " _
            & " group by glt_ac_id,glt_en_id"

        Dim dt As New DataTable
        dt = GetTableData(sSQL)


        For Each dr As DataRow In dt.Rows

            sSQL = "UPDATE  " _
              & "  public.glbal_balance   " _
              & "SET  " _
              & "  glbal_balance_unposted = " & SetDec(dr("unpost_all")) & ",  " _
              & "  glbal_balance_posted = " & SetDec(dr("posted_all")) & "  " _
              & "WHERE  " _
              & "  glbal_gcal_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid").ToString) & " and  " _
              & "  glbal_en_id = " & dr("glt_en_id") & " and " _
              & "  glbal_ac_id = " & dr("glt_ac_id") & " "

            sSQLs.Add(sSQL)
        Next

        If master_new.PGSqlConn.status_sync = True Then
            If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                Exit Sub
            End If
            sSQLs.Clear()
        Else
            If DbRunTran(sSQLs, "") = False Then
                Exit Sub
            End If
            sSQLs.Clear()
        End If


        sSQL = "update gcal_mstr set gcal_pra_closing='N', gcal_closing='N'  where gcal_oid='" _
        & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("gcal_oid").ToString & "'"

        sSQLs.Add(sSQL)

        If master_new.PGSqlConn.status_sync = True Then
            If master_new.PGSqlConn.DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                Exit Sub
            End If
            sSQLs.Clear()
        Else
            If master_new.PGSqlConn.DbRunTran(sSQLs, "") = False Then
                Exit Sub
            End If
            sSQLs.Clear()
        End If
        Box("Success, Please retrieve to load data")

    End Sub

  
#End Region

#Region "gv_edit"
    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "gcald_ap", "N")
            .SetRowCellValue(e.RowHandle, "gcald_ar", "N")
            .SetRowCellValue(e.RowHandle, "gcald_fa", "N")
            .SetRowCellValue(e.RowHandle, "gcald_ic", "N")
            .SetRowCellValue(e.RowHandle, "gcald_so", "N")
            .SetRowCellValue(e.RowHandle, "gcald_gl", "N")
            .SetRowCellValue(e.RowHandle, "gcald_year", "N")
            .BestFitColumns()
        End With
    End Sub
#End Region
End Class
