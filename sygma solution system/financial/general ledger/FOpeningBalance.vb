Imports npgsql
Imports master_new.ModFunction

Public Class FOpeningBalance
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim sSQL As String
    Dim _status_edit As String = ""
    Private Sub FOpeningBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_gcal_mstr())
        'le_periode.Properties.DataSource = dt_bantu
        'le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        'le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        'le_periode.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'glbal_en_id.Properties.DataSource = dt_bantu
        'glbal_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'glbal_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'glbal_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_gcal_mstr())
        'glbal_gcal_oid.Properties.DataSource = dt_bantu
        'glbal_gcal_oid.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        'glbal_gcal_oid.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        'glbal_gcal_oid.ItemIndex = 0


        init_le(le_entity, "en_mstr")
        init_le(le_periode, "gcal_mstr")

        init_le(glbal_en_id, "en_mstr")
        init_le(glbal_gcal_oid, "gcal_mstr")

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Year", "gcal_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode", "gcal_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "gcal_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "gcal_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account ID", "glbal_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Opening Balance", "glbal_balance_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n8")
        add_column_copy(gv_master, "Unposted Balance", "glbal_balance_unposted", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Posted Balance", "glbal_balance_posted", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Profit Loss Save", "glbal_balance_posted_end_month", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Trial Balance Save", "glbal_balance_trial", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "User Create", "glbal_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "glbal_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "glbal_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "glbal_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_edit, "glbal_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glbal_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glbal_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glbal_cu_id", False)
        add_column(gv_edit, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Opening Balance", "glbal_balance_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n8")
    End Sub

    Public Overrides Function get_sequel() As String
        If le_entity.EditValue = 0 Then
            get_sequel = "SELECT  " _
                   & "  glbal_oid, " _
                   & "  glbal_dom_id, " _
                   & "  glbal_en_id, " _
                   & "  glbal_add_by, " _
                   & "  glbal_add_date, " _
                   & "  glbal_upd_by, " _
                   & "  glbal_upd_date, " _
                   & "  glbal_gcal_oid, " _
                   & "  glbal_ac_id, " _
                   & "  glbal_sb_id, " _
                   & "  glbal_cc_id, " _
                   & "  glbal_cu_id, " _
                   & "  glbal_balance_open, " _
                   & "  glbal_balance_unposted, " _
                   & "  glbal_balance_posted,glbal_balance_posted_end_month, glbal_balance_trial,   " _
                   & "  glbal_dt, " _
                   & "  en_desc, " _
                   & "  gcal_year, " _
                   & "  gcal_periode, " _
                   & "  gcal_start_date, " _
                   & "  gcal_end_date, " _
                   & "  ac_code, " _
                   & "  ac_name, " _
                   & "  sb_desc, " _
                   & "  cc_desc, " _
                   & "  cu_name " _
                   & "FROM  " _
                   & "  public.glbal_balance " _
                   & "  inner join en_mstr on en_id = glbal_en_id " _
                   & "  inner join ac_mstr on ac_id = glbal_ac_id " _
                   & "  inner join sb_mstr on sb_id = glbal_sb_id " _
                   & "  inner join cc_mstr on cc_id = glbal_cc_id " _
                   & "  inner join cu_mstr on cu_id = glbal_cu_id " _
                   & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
                   & " where  glbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" 
        Else
            get_sequel = "SELECT  " _
                   & "  glbal_oid, " _
                   & "  glbal_dom_id, " _
                   & "  glbal_en_id, " _
                   & "  glbal_add_by, " _
                   & "  glbal_add_date, " _
                   & "  glbal_upd_by, " _
                   & "  glbal_upd_date, " _
                   & "  glbal_gcal_oid, " _
                   & "  glbal_ac_id, " _
                   & "  glbal_sb_id, " _
                   & "  glbal_cc_id, " _
                   & "  glbal_cu_id, " _
                   & "  glbal_balance_open, " _
                   & "  glbal_balance_unposted, " _
                   & "  glbal_balance_posted,glbal_balance_posted_end_month, glbal_balance_trial,   " _
                   & "  glbal_dt, " _
                   & "  en_desc, " _
                   & "  gcal_year, " _
                   & "  gcal_periode, " _
                   & "  gcal_start_date, " _
                   & "  gcal_end_date, " _
                   & "  ac_code, " _
                   & "  ac_name, " _
                   & "  sb_desc, " _
                   & "  cc_desc, " _
                   & "  cu_name " _
                   & "FROM  " _
                   & "  public.glbal_balance " _
                   & "  inner join en_mstr on en_id = glbal_en_id " _
                   & "  inner join ac_mstr on ac_id = glbal_ac_id " _
                   & "  inner join sb_mstr on sb_id = glbal_sb_id " _
                   & "  inner join cc_mstr on cc_id = glbal_cc_id " _
                   & "  inner join cu_mstr on cu_id = glbal_cu_id " _
                   & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
                   & " where glbal_en_id = " + le_entity.EditValue.ToString _
                   & " and glbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
                   & " and glbal_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If
       

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        glbal_en_id.Focus()
        glbal_en_id.ItemIndex = 0
        glbal_gcal_oid.ItemIndex = 0
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        Dim ds_bantu As New DataSet
        Dim i As Integer

        ds_bantu = New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ac_id, ac_code, ac_name, ac_cu_id, cu_name " _
                        & "  from ac_mstr " _
                        & "  inner join cu_mstr on cu_id = ac_cu_id " _
                        & "  where ac_is_sumlevel = 'N' and ac_id <> 0" _
                        & "  order by ac_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ac_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  glbal_oid, " _
                        & "  glbal_dom_id, " _
                        & "  glbal_en_id, " _
                        & "  glbal_add_by, " _
                        & "  glbal_add_date, " _
                        & "  glbal_upd_by, " _
                        & "  glbal_upd_date, " _
                        & "  glbal_gcal_oid, " _
                        & "  glbal_ac_id, " _
                        & "  glbal_sb_id, " _
                        & "  glbal_cc_id, " _
                        & "  glbal_cu_id, " _
                        & "  glbal_balance_open, " _
                        & "  glbal_balance_unposted, " _
                        & "  glbal_balance_posted, " _
                        & "  glbal_dt, " _
                        & "  en_desc, " _
                        & "  gcal_year, " _
                        & "  gcal_periode, " _
                        & "  gcal_start_date, " _
                        & "  gcal_end_date, " _
                        & "  ac_code, " _
                        & "  ac_name, " _
                        & "  sb_desc, " _
                        & "  cc_desc, " _
                        & "  cu_name " _
                        & "FROM  " _
                        & "  public.glbal_balance " _
                        & "  inner join en_mstr on en_id = glbal_en_id " _
                        & "  inner join ac_mstr on ac_id = glbal_ac_id " _
                        & "  inner join sb_mstr on sb_id = glbal_sb_id " _
                        & "  inner join cc_mstr on cc_id = glbal_cc_id " _
                        & "  inner join cu_mstr on cu_id = glbal_cu_id " _
                        & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
                        & " where ac_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "bantu")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        Dim _dtrow As DataRow

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit.Tables(0).NewRow
            _dtrow("glbal_ac_id") = ds_bantu.Tables(0).Rows(i).Item("ac_id")
            _dtrow("ac_code") = ds_bantu.Tables(0).Rows(i).Item("ac_code")
            _dtrow("ac_name") = ds_bantu.Tables(0).Rows(i).Item("ac_name")
            _dtrow("glbal_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("glbal_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("glbal_cu_id") = ds_bantu.Tables(0).Rows(i).Item("ac_cu_id")
            _dtrow("cu_name") = ds_bantu.Tables(0).Rows(i).Item("cu_name")
            _dtrow("glbal_balance_open") = 0

            ds_edit.Tables(0).Rows.Add(_dtrow)
        Next
        ds_edit.Tables(0).AcceptChanges()
        gv_edit.BestFitColumns()
    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function
    Public Overrides Function edit()
        edit = True
        sSQL = ""
        'If _conf_value = "1" Then
        '    If ptnr_email.EditValue = "" Then
        '        MessageBox.Show("Email Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If

        '    If sc_te_ptnr_name.EditValue = "" Then
        '        MessageBox.Show("Name Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If

        'End If
        Dim sSQLs As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        If CeClean.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update glbal_balance set glbal_balance_open=0  where glbal_gcal_oid = '" + glbal_gcal_oid.EditValue.ToString _
                                + "'"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from glbal_balance where glbal_gcal_oid = '" + glbal_gcal_oid.EditValue.ToString _
                            + "' and glbal_en_id=" & SetInteger(glbal_en_id.EditValue)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.glbal_balance " _
                                                & "( " _
                                                & "  glbal_oid, " _
                                                & "  glbal_dom_id, " _
                                                & "  glbal_en_id, " _
                                                & "  glbal_add_by, " _
                                                & "  glbal_add_date, " _
                                                & "  glbal_gcal_oid, " _
                                                & "  glbal_ac_id, " _
                                                & "  glbal_sb_id, " _
                                                & "  glbal_cc_id, " _
                                                & "  glbal_cu_id, " _
                                                & "  glbal_balance_open, " _
                                                & "  glbal_balance_unposted, " _
                                                & "  glbal_balance_posted, " _
                                                & "  glbal_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(glbal_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(glbal_gcal_oid.EditValue) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_ac_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_sb_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_cc_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_cu_id")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("glbal_balance_open")) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'LabelControl4.Text = "Entity : (" & n & " of " & dt.Rows.Count & " ) " & dr("en_desc") & " " & ", Account : ( " & i + 1 & " of " & ds_edit.Tables(0).Rows.Count & " ) " & ds_edit.Tables(0).Rows(i).Item("ac_name")
                            'System.Windows.Forms.Application.DoEvents()

                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Opening balance" & le_entity.Text & " " & le_periode.Text)
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
                        'set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
                        ' dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
    Public Overrides Function edit_data() As Boolean
        ' MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Try
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = True Then

                If MyBase.edit_data = True Then
                    glbal_en_id.EditValue = le_entity.EditValue
                    glbal_gcal_oid.EditValue = le_periode.EditValue.ToString

                    ds_edit = New DataSet
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "SELECT  " _
                                   & "  glbal_oid, " _
                                   & "  glbal_dom_id, " _
                                   & "  glbal_en_id, " _
                                   & "  glbal_add_by, " _
                                   & "  glbal_add_date, " _
                                   & "  glbal_upd_by, " _
                                   & "  glbal_upd_date, " _
                                   & "  glbal_gcal_oid, " _
                                   & "  glbal_ac_id, " _
                                   & "  glbal_sb_id, " _
                                   & "  glbal_cc_id, " _
                                   & "  glbal_cu_id, " _
                                   & "  glbal_balance_open, " _
                                   & "  glbal_balance_unposted, " _
                                   & "  glbal_balance_posted,glbal_balance_posted_end_month, glbal_balance_trial,   " _
                                   & "  glbal_dt, " _
                                   & "  en_desc, " _
                                   & "  gcal_year, " _
                                   & "  gcal_periode, " _
                                   & "  gcal_start_date, " _
                                   & "  gcal_end_date, " _
                                   & "  ac_code, " _
                                   & "  ac_name, " _
                                   & "  sb_desc, " _
                                   & "  cc_desc, " _
                                   & "  cu_name " _
                                   & "FROM  " _
                                   & "  public.glbal_balance " _
                                   & "  inner join en_mstr on en_id = glbal_en_id " _
                                   & "  inner join ac_mstr on ac_id = glbal_ac_id " _
                                   & "  inner join sb_mstr on sb_id = glbal_sb_id " _
                                   & "  inner join cc_mstr on cc_id = glbal_cc_id " _
                                   & "  inner join cu_mstr on cu_id = glbal_cu_id " _
                                   & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
                                   & " where glbal_en_id = " + le_entity.EditValue.ToString _
                                   & " and glbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
                                   & " and glbal_en_id in (select user_en_id from tconfuserentity " _
                                          & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                                .InitializeCommand()
                                .FillDataSet(ds_edit, "bantu")
                                gc_edit.DataSource = ds_edit.Tables(0)
                                gv_edit.BestFitColumns()
                                _status_edit = "Y"
                                edit_data = True
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                End If

            Else
                MessageBox.Show("You can't access this menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim _jml As Integer
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    If glbal_en_id.GetColumnValue("en_code").ToString = "0" Then
                        .Command.CommandText = "select count(*) as jml from glbal_balance where glbal_gcal_oid = '" + glbal_gcal_oid.EditValue.ToString + "'"
                    Else
                        .Command.CommandText = "select count(*) as jml from glbal_balance where glbal_gcal_oid = '" + glbal_gcal_oid.EditValue.ToString + "'" + _
                                              " and glbal_en_id = " + glbal_en_id.EditValue.ToString
                    End If

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader("jml")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        If _status_edit = "Y" Then
        Else
            If _jml > 0 Then
                MessageBox.Show("Opening Balance Still Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        'dimatikan dulu 2010-04-10



        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
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

                        Dim dt As New DataTable
                        If glbal_en_id.GetColumnValue("en_code") = "0" Then
                            dt = master_new.PGSqlConn.GetTableData("select en_id,en_desc from en_mstr where en_active='Y' and en_code <> '0' and coalesce(en_endmonth,'Y')='Y' order by en_desc")
                        Else
                            dt = master_new.PGSqlConn.GetTableData("select en_id,en_desc from en_mstr where en_id=" & glbal_en_id.EditValue.ToString & " order by en_id")
                        End If

                        Dim n As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            'Untuk Insert Data List PO

                            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.glbal_balance " _
                                                    & "( " _
                                                    & "  glbal_oid, " _
                                                    & "  glbal_dom_id, " _
                                                    & "  glbal_en_id, " _
                                                    & "  glbal_add_by, " _
                                                    & "  glbal_add_date, " _
                                                    & "  glbal_gcal_oid, " _
                                                    & "  glbal_ac_id, " _
                                                    & "  glbal_sb_id, " _
                                                    & "  glbal_cc_id, " _
                                                    & "  glbal_cu_id, " _
                                                    & "  glbal_balance_open, " _
                                                    & "  glbal_balance_unposted, " _
                                                    & "  glbal_balance_posted, " _
                                                    & "  glbal_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(dr("en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(glbal_gcal_oid.EditValue) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_ac_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_sb_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_cc_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("glbal_cu_id")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("glbal_balance_open")) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                LabelControl4.Text = "Entity : (" & n & " of " & dt.Rows.Count & " ) " & dr("en_desc") & " " & ", Account : ( " & i + 1 & " of " & ds_edit.Tables(0).Rows.Count & " ) " & ds_edit.Tables(0).Rows(i).Item("ac_name")
                                System.Windows.Forms.Application.DoEvents()

                            Next
                            n = n + 1
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
                        le_periode.EditValue = glbal_gcal_oid.EditValue.ToString
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

    Private Sub InsertOpeningBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertOpeningBalanceToolStripMenuItem.Click
        Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            sSQL = "select ac_id,en_id, amount from opening_adjustment order by ac_id"
            Dim sSQLs As New ArrayList

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            For Each dr As DataRow In dt.Rows
                sSQL = "UPDATE  " _
                    & "  public.glbal_balance   " _
                    & "SET  " _
                    & "  glbal_balance_open = glbal_balance_open +  " & SetDec(dr("amount")) _
                    & " WHERE  " _
                    & " glbal_ac_id = " & SetInteger(dr("ac_id")) & "  " _
                    & " and   glbal_en_id = " & SetInteger(dr("en_id")) & "  " _
                    & " and glbal_gcal_oid = " & SetSetring(le_periode.EditValue.ToString) & " "

                sSQLs.Add(sSQL)
            Next

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

            Box("Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

  
    Private Sub BtImport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImport.Click
        Try
            Dim ds_import As New DataSet
            Dim _file As String = AskOpenFile("Format import pembelian Excel 2003 | *.xls")

            If _file = "" Then
                Exit Sub
            End If

            ds_import = ImportExcel(_file)

            For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                ds_edit.Tables(0).Rows(i).Item("glbal_balance_open") = 0.0
                For Each dr As DataRow In ds_import.Tables(0).Rows
                    If SetNumber(ds_edit.Tables(0).Rows(i).Item("glbal_ac_id")) = SetNumber(dr("ac_id")) Then
                        ds_edit.Tables(0).Rows(i).Item("glbal_balance_open") = SetNumber(dr("opening"))
                        Exit For
                    End If
                Next
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()

            Box("Success")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
