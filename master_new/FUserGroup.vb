Imports npgsql
Imports master_new.ModFunction

Public Class FUserGroup

    Dim sSQL As String
    Public dt_bantu As DataTable
    Dim _userid_mstr As Integer
    Public __menuid As String
    Dim dt_menu As New DataTable

    Private Sub FUserGroupOriginal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "select groupid , groupnama from tconfgroup order by groupnama"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "HelpGroup")
                    cb_group.Properties.DataSource = ds_cb.Tables("HelpGroup")
                    cb_group.Properties.DisplayMember = ds_cb.Tables("HelpGroup").Columns("groupnama").ToString
                    cb_group.Properties.ValueMember = ds_cb.Tables("HelpGroup").Columns("groupid").ToString
                    cb_group.EditValue = ds_cb.Tables("HelpGroup").Rows(0).Item("groupid")

                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " & ClsVar.sdom_id.ToString & " order by en_desc "
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "entity")
                    user_en_id.Properties.DataSource = ds_cb.Tables("entity")
                    user_en_id.Properties.DisplayMember = ds_cb.Tables("entity").Columns("en_desc").ToString
                    user_en_id.Properties.ValueMember = ds_cb.Tables("entity").Columns("en_id").ToString
                    user_en_id.EditValue = ds_cb.Tables("entity").Rows(0).Item("en_id")

                    '.FillDataSet(ds_cb, "userentity")
                    userentity.Properties.DataSource = ds_cb.Tables("entity")
                    userentity.Properties.DisplayMember = ds_cb.Tables("entity").Columns("en_desc").ToString
                    userentity.Properties.ValueMember = ds_cb.Tables("entity").Columns("en_id").ToString
                    userentity.EditValue = ds_cb.Tables("entity").Rows(0).Item("en_id")

                    .SQL = "select dom_id, dom_code,dom_desc from dom_mstr where dom_active ~~* 'Y' order by dom_desc "
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "domain")
                    user_dom_id.Properties.DataSource = ds_cb.Tables("domain")
                    user_dom_id.Properties.DisplayMember = ds_cb.Tables("domain").Columns("dom_desc").ToString
                    user_dom_id.Properties.ValueMember = ds_cb.Tables("domain").Columns("dom_id").ToString
                    user_dom_id.EditValue = ds_cb.Tables("domain").Rows(0).Item("dom_id")

                    .SQL = "select tran_id, tran_name from tran_mstr order by tran_name"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "transaction")
                    tranu_tran_id.Properties.DataSource = ds_cb.Tables("transaction")
                    tranu_tran_id.Properties.DisplayMember = ds_cb.Tables("transaction").Columns("tran_name").ToString
                    tranu_tran_id.Properties.ValueMember = ds_cb.Tables("transaction").Columns("tran_id").ToString
                    tranu_tran_id.EditValue = ds_cb.Tables("transaction").Rows(0).Item("tran_id")

                    .SQL = "select cc_id, cc_code, cc_desc from cc_mstr order by cc_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "cc_mstr")
                    le_cc.Properties.DataSource = ds_cb.Tables("cc_mstr")
                    le_cc.Properties.DisplayMember = ds_cb.Tables("cc_mstr").Columns("cc_code").ToString
                    le_cc.Properties.ValueMember = ds_cb.Tables("cc_mstr").Columns("cc_id").ToString
                    le_cc.EditValue = ds_cb.Tables("cc_mstr").Rows(0).Item("cc_id")

                    en_id_view.Properties.DataSource = ds_cb.Tables("entity")
                    en_id_view.Properties.DisplayMember = ds_cb.Tables("entity").Columns("en_desc").ToString
                    en_id_view.Properties.ValueMember = ds_cb.Tables("entity").Columns("en_id").ToString
                    en_id_view.EditValue = ds_cb.Tables("entity").Rows(0).Item("en_id")


                    .SQL = "select code_id ,code_name  from code_mstr where code_field='area_id' order by code_name"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "area_mstr")
                    le_area.Properties.DataSource = ds_cb.Tables("area_mstr")
                    le_area.Properties.DisplayMember = ds_cb.Tables("area_mstr").Columns("code_name").ToString
                    le_area.Properties.ValueMember = ds_cb.Tables("area_mstr").Columns("code_id").ToString
                    le_area.EditValue = ds_cb.Tables("area_mstr").Rows(0).Item("code_id")



                End With
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select userid, u.groupid,last_access, userkode, usernama,userphone,user_id_telegram, " _
                    & "useremail, password, last_access, usernik, useractive, en_desc, u.en_id,userpidgin,user_ptnr_id,ptnr_name,pin " + _
                     " from  tconfuser u " + _
                     " left outer join en_mstr e on e.en_id = u.en_id " + _
                      " left outer join ptnr_mstr f on f.ptnr_id = u.user_ptnr_id " + _
                     " order by usernama"
        Return get_sequel
    End Function

    Public Overrides Sub format_grid()
        add_column(gv_master, "userkode", False)
        add_column_copy(gv_master, "User", "usernama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PIN", "pin", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Password", "password", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Pidgin", "userpidgin", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NIK", "usernik", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Email", "useremail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone", "userphone", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity Default", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales ID", "user_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales ID", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID Telegram", "user_id_telegram", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "useractive", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Last Access", "last_access", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_group, "userid", False)
        add_column(gv_group, "Group", "groupnama", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_entity, "userid", False)
        add_column(gv_entity, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_entity_view, "userid", False)
        add_column(gv_entity_view, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_domain, "userid", False)
        add_column(gv_domain, "Domain", "dom_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_transaction, "tranu_user_id", False)
        add_column(gv_transaction, "transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_area, "area_id", False)
        add_column(gv_area, "userid", False)
        add_column(gv_area, "Area", "area_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "select userid, ug.groupid, groupnama" + _
              " from tconfusergroup ug" + _
              " inner join tconfgroup g on g.groupid = ug.groupid order by groupnama"

        load_data_detail(sql, gc_group, "detail")

        Try
            ds.Tables("entity").Clear()
        Catch ex As Exception
        End Try

        sql = "select userid, user_en_id, en_code, en_desc " + _
              " from tconfuserentity ue" + _
              " inner join en_mstr  on en_id = user_en_id order by en_desc"

        load_data_detail(sql, gc_entity, "entity")

        Try
            ds.Tables("entity_view").Clear()
        Catch ex As Exception
        End Try

        sql = "select userid, user_en_id, en_code, en_desc " + _
              " from tconfuserentityview ue" + _
              " inner join en_mstr  on en_id = user_en_id order by en_desc"

        load_data_detail(sql, gc_entity_view, "entity_view")

        Try
            ds.Tables("domain").Clear()
        Catch ex As Exception
        End Try

        sql = "select userid, user_dom_id, dom_code" + _
              " from tconfuserdomain ud" + _
              " inner join dom_mstr on dom_id = user_dom_id order by dom_code"

        load_data_detail(sql, gc_domain, "domain")

        Try
            ds.Tables("transaction").Clear()
        Catch ex As Exception
        End Try

        sql = "select tranu_oid, tranu_tran_id, tranu_user_id, tran_name from tranu_usr" + _
              " inner join tran_mstr on tran_id = tranu_tran_id order by tran_name"

        load_data_detail(sql, gc_transaction, "transaction")


        Try
            ds.Tables("area").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.userid, " _
            & "  a.area_id, " _
            & "  b.code_name as area_desc " _
            & "FROM " _
            & "  public.code_mstr b " _
            & "  INNER JOIN public.tconfuserarea a ON (b.code_id = a.area_id) order by code_name"

        load_data_detail(sql, gc_area, "area")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_group.Columns("userid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[userid]=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString & "")
            gv_group.BestFitColumns()

            gv_domain.Columns("userid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[userid]=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString & "")
            gv_domain.BestFitColumns()

            gv_entity.Columns("userid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[userid]=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString & "")
            gv_entity.BestFitColumns()

            gv_entity_view.Columns("userid").FilterInfo = _
          New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[userid]=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString & "")
            gv_entity_view.BestFitColumns()

            gv_transaction.Columns("tranu_user_id").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[tranu_user_id]=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString & "")
            gv_transaction.BestFitColumns()

            gv_area.Columns("userid").FilterInfo = _
         New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[userid]=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString & "")
            gv_area.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        sc_txtkode_1.Focus()
        sc_txtkode_1.Text = ""
        sc_txtuser.Text = ""
        sc_txtpassword.Text = ""
        usernik.Text = ""
        useremail.Text = ""
        userentity.ItemIndex = 0
        userphone.Text = "+62"
        useractive.EditValue = True
        sc_txtpassword.Properties.PasswordChar = "*"
        user_id_telegram.EditValue = ""
        pin.Text = ""

    End Sub
    Public Overrides Function cancel_data() As Boolean
        Return MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
    Public Overrides Function insert() As Boolean

        If userpidgin.Text = "" Then
            Box("User pidgin can't null")
            Return False
            Exit Function
        End If

        Dim userid As Integer
        Try
            Using objbantu As New master_new.WDABasepgsql("", "")
                With objbantu
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(userid),0) +1 as max_id from tconfuser"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read()
                        userid = .DataReader.Item("max_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupid from tconfgroup where groupdefault = true"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "group")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfuser " _
                                            & "( " _
                                            & "  userid, " _
                                            & "  userkode, " _
                                            & "  usernama, " _
                                            & "  password, " _
                                            & "  id_karyawan, " _
                                            & "  usernik, " _
                                            & "  useremail, " _
                                            & "  en_id,userpidgin,userphone,user_ptnr_id,user_id_telegram,pin, " _
                                            & "  useractive " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(userid) & ",  " _
                                            & SetSetring(sc_txtkode_1.Text) & ",  " _
                                            & SetSetring(sc_txtuser.Text) & ",  " _
                                            & SetSetring(sc_txtpassword.Text) & ",  " _
                                            & SetInteger(-1) & ",  " _
                                            & SetSetring(usernik.Text) & ",  " _
                                            & SetSetring(useremail.Text) & ",  " _
                                            & SetInteger(userentity.EditValue) & ",  " _
                                            & SetSetring(userpidgin.EditValue) & ",  " _
                                             & SetSetring(IIf(userphone.EditValue = "+62", "", userphone.EditValue)) & ",  " _
                                              & SetInteger(user_ptnr_id.EditValue) & ",  " _
                                              & SetInteger(user_id_telegram.EditValue) & ",  " _
                                              & SetSetring(pin.Text) & ",  " _
                                            & SetBitYN(useractive.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "insert into tconfskin (userid, form_skin, mainmenu_style, grid_style," + _
                                               " hris_master_data, hris_recrutment, hris_hr, hris_attendance, hris_payroll, " + _
                                               " erp_master_data, erp_distribution, erp_manufacturing, erp_financial, erp_customer_services)" + _
                                               " values (" + userid.ToString + ",'Office2003', 'NavigationPane', 'Blue Office', false, false, false, false, false,false, false, false, false, false) "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables("group").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "insert into tconfusergroup (userid, groupid) " _
                                 + " values (" + userid.ToString + "," + ds_bantu.Tables("group").Rows(i).Item("groupid").ToString + ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert user " & sc_txtuser.Text)
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
                        set_to_data_insert()
                        after_success()
                        set_row(Trim(userid), "userid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True

                    Catch ex As NpgsqlException
                        row = 0
                        insert = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As NpgsqlException
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _userid_mstr = SetString(.Item("userid"))
                sc_txtkode_1.Text = SetString(.Item("userkode"))
                sc_txtuser.Text = SetString(.Item("usernama"))
                sc_txtpassword.Text = SetString(.Item("password"))
                usernik.Text = SetString(.Item("usernik"))
                useremail.Text = SetString(.Item("useremail"))
                userentity.EditValue = .Item("en_id")
                useractive.EditValue = SetBitYNB(.Item("useractive"))
                userpidgin.EditValue = SetString(.Item("userpidgin"))
                userphone.EditValue = IIf(SetString(.Item("userphone")) = "", "+62", .Item("userphone"))
                sc_txtpassword.Properties.PasswordChar = "*"
                user_ptnr_id.EditValue = .Item("user_ptnr_id")
                user_id_telegram.EditValue = .Item("user_id_telegram")
                pin.EditValue = .Item("pin")
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
                                            & "  public.tconfuser   " _
                                            & "SET  " _
                                            & "  userkode = " & SetSetring(sc_txtkode_1.Text) & ",  " _
                                            & "  usernama = " & SetSetring(sc_txtuser.Text) & ",  " _
                                            & "  password = " & SetSetring(sc_txtpassword.Text) & ",  " _
                                            & "  pin = " & SetSetring(pin.Text) & ",  " _
                                            & "  usernik = " & SetSetring(usernik.Text) & ",  " _
                                            & "  useremail = " & SetSetring(useremail.Text) & ",  " _
                                            & "  en_id = " & SetInteger(userentity.EditValue) & ",  " _
                                            & "  userphone = " & SetSetring(IIf(userphone.EditValue = "+62", "", userphone.EditValue)) & ",  " _
                                            & "  useractive = " & SetBitYN(useractive.EditValue) & ",  " _
                                            & "  user_ptnr_id = " & SetInteger(user_ptnr_id.EditValue) & ",  " _
                                            & "  user_id_telegram = " & SetInteger(user_id_telegram.EditValue) & ",  " _
                                            & "  userpidgin = " & SetSetring(userpidgin.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  userid = " & SetInteger(_userid_mstr) & " "
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
                        set_row(Trim(_userid_mstr), "userid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Private Sub sb_add_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_save.Click
        If MessageBox.Show("Add This Group To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "insert into tconfusergroup (userid, groupid) " + _
                                               " values (" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString + "," + _
                                               cb_group.EditValue.ToString + ")"
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_delete.Click
        If MessageBox.Show("Delete This Group From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "delete from tconfusergroup where groupid =  " + ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("groupid").ToString + _
                                               " and userid = " + ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("userid").ToString
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_del_entity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_del_entity.Click
        If MessageBox.Show("Delete This Group From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "delete from tconfuserentity where user_en_id =  " + ds.Tables("entity").Rows(BindingContext(ds.Tables("entity")).Position).Item("user_en_id").ToString + _
                           " and userid = " + ds.Tables("entity").Rows(BindingContext(ds.Tables("entity")).Position).Item("userid").ToString
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_add_entity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_entity.Click
        If MessageBox.Show("Add This Entity To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "insert into tconfuserentity (userid, user_en_id) " + _
                                               " values (" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString + "," + _
                                               user_en_id.EditValue.ToString + ")"
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_add_domain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_domain.Click
        If MessageBox.Show("Add This Domain To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "insert into tconfuserdomain (userid, user_dom_id) " + _
                                               " values (" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString + "," + _
                                               user_dom_id.EditValue.ToString + ")"
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_del_domain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_del_domain.Click
        If MessageBox.Show("Delete This Domain From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "delete from tconfuserdomain where user_dom_id =  " + ds.Tables("domain").Rows(BindingContext(ds.Tables("domain")).Position).Item("user_dom_id").ToString + _
                                               " and userid = " + ds.Tables("domain").Rows(BindingContext(ds.Tables("domain")).Position).Item("userid").ToString
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_insert_transaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_insert_transaction.Click
        If MessageBox.Show("Add This Transaction To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                                            & "  public.tranu_usr " _
                                            & "( " _
                                            & "  tranu_oid, " _
                                            & "  tranu_tran_id, " _
                                            & "  tranu_user_id, " _
                                            & "  tranu_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(tranu_tran_id.EditValue) & ",  " _
                                            & SetInteger(_userid) & ",  " _
                                            & "current_timestamp" & "  " _
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_del_transaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_del_transaction.Click
        If MessageBox.Show("Delete This Transaction From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "delete from public.tranu_usr where tranu_oid =  '" + ds.Tables("transaction").Rows(BindingContext(ds.Tables("transaction")).Position).Item("tranu_oid").ToString + "'"
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
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
                            .Command.CommandText = "delete from tconfuser where userid = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString
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

    Private Sub menuid_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuid.ButtonClick
        Try

            Dim frm As New FMenuSearch
            frm.set_win(Me)
            frm._obj = menuid
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtAddlMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAddlMenu.Click
        Dim sSQLs As New ArrayList
        Try
            sSQL = "delete from tconfmenuuser where userid=" _
              & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString _
              & " and menuid=" & __menuid

            sSQLs.Add(sSQL)

            sSQL = "INSERT INTO  " _
                & "  public.tconfmenuuser " _
                & "( " _
                & "  userid, " _
                & "  menuid, " _
                & "  enablestatus, " _
                & "  visiblestatus, " _
                & "  editablestatus, " _
                   & "  insertablestatus, " _
                & "  deleteablestatus,cancelablestatus " _
                & ")  " _
                & "VALUES ( " _
                & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString) & ",  " _
                & SetInteger(__menuid) & ",  " _
                & CeEnable.EditValue & ",  " _
                & CeVisible.EditValue & ",  " _
                & CeEditable.EditValue & ",  " _
                  & CEInsertable.EditValue & ",  " _
                    & CeDeleteable.EditValue & ",  " _
                & CeCancelable.EditValue & "  " _
                & ")"
            sSQLs.Add(sSQL)

            'master_new.PGSqlConn.DbRunTran(sSQLs)

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


            load_menu()
            Box("Data saved")

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub BtDelMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDelMenu.Click
        Dim sSQLs As New ArrayList
        Try
            sSQL = "delete from tconfmenuuser where userid=" _
                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString _
                & " and menuid=" & dt_menu.Rows(BindingContext(dt_menu).Position).Item("menuid").ToString

            sSQLs.Add(sSQL)

            ' master_new.PGSqlConn.DbRun(sSQL)

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

            load_menu()
            Box("Data delete")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub load_menu()
        sSQL = "SELECT  " _
            & "  a.userid, " _
            & "  a.menuid, " _
            & "  b.menuname, " _
            & "  a.enablestatus, " _
            & "  a.visiblestatus, " _
            & "  a.insertablestatus, " _
            & "  a.editablestatus, " _
            & "  a.deleteablestatus,a.cancelablestatus " _
            & "FROM " _
            & "  public.tconfmenuuser a " _
            & "  INNER JOIN public.tconfmenucollection b ON (a.menuid = b.menuid) " _
            & "WHERE " _
            & "  a.userid =" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

        ' load_data_detail(sSQL, gc_menu, "menu")
        dt_menu = PGSqlConn.GetTableData(sSQL)

        gc_menu.DataSource = dt_menu
        gv_menu.BestFitColumns()

    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        Try
            Dim frm As New frmTestMenu

            frm.par_user_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid")
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            load_menu()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gc_menu_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gc_menu.DoubleClick
        Try
            With dt_menu.Rows(BindingContext(dt_menu).Position)
                menuid.EditValue = .Item("menuname")
                __menuid = .Item("menuid")
                CeVisible.EditValue = .Item("visiblestatus")
                CeEnable.EditValue = .Item("enablestatus")
                CeEditable.EditValue = .Item("editablestatus")
                CeDeleteable.EditValue = .Item("deleteablestatus")
                CEInsertable.EditValue = .Item("insertablestatus")
                CeCancelable.EditValue = .Item("cancelablestatus")
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtSaveEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSaveEntity.Click
        If MessageBox.Show("Add This Entity To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "insert into tconfuserentityview (userid, user_en_id) " + _
                                               " values (" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString + "," + _
                                               en_id_view.EditValue.ToString + ")"
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtDelEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDelEntity.Click
        If MessageBox.Show("Delete This Group From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "delete from tconfuserentityview where user_en_id =  " + ds.Tables("entity_view").Rows(BindingContext(ds.Tables("entity")).Position).Item("user_en_id").ToString + _
                           " and userid = " + ds.Tables("entity").Rows(BindingContext(ds.Tables("entity_view")).Position).Item("userid").ToString
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub btAddArea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddArea.Click
        If MessageBox.Show("Add This Area To This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "insert into tconfuserarea (userid, area_id) " + _
                                               " values (" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString + "," + _
                                               le_area.EditValue.ToString + ")"
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btDelArea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDelArea.Click
        If MessageBox.Show("Delete This Area From This User..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _userid As String
        _userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString

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
                        .Command.CommandText = "delete from tconfuserarea where area_id =  " + ds.Tables("area").Rows(BindingContext(ds.Tables("area")).Position).Item("area_id").ToString + _
                           " and userid = " + ds.Tables("area").Rows(BindingContext(ds.Tables("area")).Position).Item("userid").ToString
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
                        load_data_grid_detail()
                        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Deleted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        set_row(_userid, "userid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_add_cc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add_cc.Click

    End Sub

    Private Sub sb_del_cc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_del_cc.Click

    End Sub


    Private Sub BtShowPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtShowPassword.Click
        Try
            sc_txtpassword.Properties.PasswordChar = ""
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub user_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub userentity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles userentity.EditValueChanged
        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name, ptnr_ac_ap_id from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                                                " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                                                " and ptnr_en_id in (0," + userentity.EditValue.ToString + ")" + _
                                                " and ptnr_is_member ~~* 'Y' " + _
                                                " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "sales_mstr")
                    user_ptnr_id.Properties.DataSource = ds_cb.Tables("sales_mstr")
                    user_ptnr_id.Properties.DisplayMember = ds_cb.Tables("sales_mstr").Columns("ptnr_name").ToString
                    user_ptnr_id.Properties.ValueMember = ds_cb.Tables("sales_mstr").Columns("ptnr_id").ToString
                    user_ptnr_id.ItemIndex = 0
                End With

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtMultiSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtMultiSelect.Click
        Try
            Dim frm As New frmEntitySearch
            frm.set_win(Me)
            'frm._obj = 
            frm.userid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid").ToString
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub load_entity()
        load_data_grid_detail()
        MessageBox.Show("Congratulation " + master_new.ClsVar.sNama + ", Data Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class


