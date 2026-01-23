Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FCrossBudget
    Dim _cbdgt_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction

    Public _bdgt_oid As String

    Private Sub FCrossBudget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        pr_date_1.EditValue = Today()
        pr_date_2.EditValue = Today()
    End Sub

    Public Function load_periode_detail(ByVal par_budget_oid As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select distinct bdgtp_code,bdgtd_bdgtp_id " _
                            & "from bdgtd_det " _
                            & "inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "where bdgtd_bdgt_oid = " + SetSetring(par_budget_oid) _
                            & " order by bdgtd_bdgtp_id asc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "periode_detail")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_account(ByVal par_cc_id As Integer, ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select cca_ac_id,ac_code, ac_name " + _
                           " from cca_acount " + _
                           " inner join ac_mstr on ac_id = cca_ac_id " + _
                           " where ac_active ~~* 'Y'" + _
                           " and cca_cc_id = " + SetInteger(par_cc_id) + _
                           " and ac_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and cca_en_id = " + SetInteger(par_en_id) + _
                           " and cca_status = True or cca_cc_id = 0 " + _
                           " order by ac_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ac_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Sub load_detail(ByVal par_oid As String)
        dt_bantu = New DataTable
        dt_bantu = load_periode_detail(par_oid.ToString())
        cbdgt_periode_from.Properties.DataSource = dt_bantu
        cbdgt_periode_from.Properties.DisplayMember = dt_bantu.Columns("bdgtp_code").ToString
        cbdgt_periode_from.Properties.ValueMember = dt_bantu.Columns("bdgtd_bdgtp_id").ToString
        cbdgt_periode_from.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = load_periode_detail(par_oid.ToString())
        cbdgt_periode_to.Properties.DataSource = dt_bantu
        cbdgt_periode_to.Properties.DisplayMember = dt_bantu.Columns("bdgtp_code").ToString
        cbdgt_periode_to.Properties.ValueMember = dt_bantu.Columns("bdgtd_bdgtp_id").ToString
        cbdgt_periode_to.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        cbdgt_en_id.Properties.DataSource = dt_bantu
        cbdgt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cbdgt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cbdgt_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_transaction())
        cbdgt_tran_id.Properties.DataSource = dt_bantu
        cbdgt_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        cbdgt_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
        cbdgt_tran_id.ItemIndex = 0
    End Sub

    Private Sub cbdgt_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbdgt_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ccr_restrc(cbdgt_en_id.EditValue))
        cbdgt_cc_from_id.Properties.DataSource = dt_bantu
        cbdgt_cc_from_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        cbdgt_cc_from_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        cbdgt_cc_from_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ccr_restrc(cbdgt_en_id.EditValue))
        cbdgt_cc_to_id.Properties.DataSource = dt_bantu
        cbdgt_cc_to_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        cbdgt_cc_to_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        cbdgt_cc_to_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "cbdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Code", "cbdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Year", "cbdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Periode From", "periode_from", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Periode To", "periode_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Centre From", "cc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Centre To", "cc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account From", "ac_name_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account To", "ac_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ammount", "cbdgt_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "cbdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Status", "cbdgt_trans_id", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "cbdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cbdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cbdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cbdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "cbdgt_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Code", "cbdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Date", "cbdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Budget Year", "cbdgt_bdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Periode From", "periode_from", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Periode To", "periode_to", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Cost Centre From", "cc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Cost Centre To", "cc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account From", "ac_name_from", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account To", "ac_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Ammount", "cbdgt_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Remarks", "cbdgt_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "cbdgt_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  cbdgt_oid, " _
                    & "  cbdgt_dom_id, " _
                    & "  cbdgt_en_id,en_desc, " _
                    & "  cbdgt_add_by, " _
                    & "  cbdgt_add_date, " _
                    & "  cbdgt_upd_by, " _
                    & "  cbdgt_upd_date, " _
                    & "  cbdgt_date, " _
                    & "  cbdgt_year, " _
                    & "  cbdgt_ac_from_id,acf.ac_code as ac_code_from,acf.ac_name as ac_name_from, " _
                    & "  cbdgt_sb_from_id,  " _
                    & "  cbdgt_cc_from_id,ccf.cc_desc as cc_desc_from, " _
                    & "  cbdgt_periode_from,pfrom.bdgtp_code as periode_from, " _
                    & "  cbdgt_ac_to_id,acto.ac_code as ac_code_to,acto.ac_name as ac_name_to, " _
                    & "  cbdgt_sb_to_id, " _
                    & "  cbdgt_cc_to_id,ccto.cc_desc as cc_desc_to, " _
                    & "  cbdgt_periode_to,pto.bdgtp_code as periode_to, " _
                    & "  cbdgt_remarks, " _
                    & "  cbdgt_dt,cbdgt_code, " _
                    & "  cbdgt_amount,cbdgt_trans_id,cbdgt_tran_id, " _
                    & "  cbdgt_bdgt_oid,bdgt_code " _
                    & "FROM  " _
                    & "  public.cbdgt_mstr " _
                    & "  inner join en_mstr on en_id = cbdgt_en_id " _
                    & "  inner join ac_mstr acf on acf.ac_id = cbdgt_ac_from_id " _
                    & "  inner join ac_mstr acto on acto.ac_id = cbdgt_ac_to_id " _
                    & "  inner join cc_mstr ccf on ccf.cc_id = cbdgt_cc_from_id " _
                    & "  inner join cc_mstr ccto on ccto.cc_id = cbdgt_cc_to_id " _
                    & "  inner join bdgtp_periode pfrom on pfrom.bdgtp_id = cbdgt_periode_from " _
                    & "  inner join bdgtp_periode pto on pto.bdgtp_id = cbdgt_periode_to " _
                    & "  inner join bdgt_mstr on bdgt_oid = cbdgt_bdgt_oid " _
                    & "  where cbdgt_date >= " + SetDate(pr_date_1.DateTime) _
                    & "  and cbdgt_date <= " + SetDate(pr_date_2.DateTime) _
                    & "  and cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String
        Try
            ds.Tables("wf").Clear()
        Catch ex As Exception
        End Try

        sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
              " wf_iscurrent, wf_seq " + _
              " from wf_mstr w " + _
              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
              " inner join cbdgt_mstr on cbdgt_code = wf_ref_code " + _
              "  where cbdgt_date >= " + SetDate(pr_date_1.DateTime) + _
              "  and cbdgt_date <= " + SetDate(pr_date_2.DateTime) + _
              " and cbdgt_en_id in (select user_en_id from tconfuserentity " + _
                                   " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
              " order by wf_ref_code, wf_seq "
        load_data_detail(sql, gc_wf, "wf")
        gv_wf.BestFitColumns()

        Try
            ds.Tables("email").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  cbdgt_oid, " _
            & "  cbdgt_dom_id, " _
            & "  cbdgt_en_id,en_desc, " _
            & "  cbdgt_add_by, " _
            & "  cbdgt_add_date, " _
            & "  cbdgt_upd_by, " _
            & "  cbdgt_upd_date, " _
            & "  cbdgt_date, " _
            & "  cbdgt_year, " _
            & "  cbdgt_ac_from_id,acf.ac_code as ac_code_from,acf.ac_name as ac_name_from, " _
            & "  cbdgt_sb_from_id,  " _
            & "  cbdgt_cc_from_id,ccf.cc_desc as cc_desc_from, " _
            & "  cbdgt_periode_from,pfrom.bdgtp_code as periode_from, " _
            & "  cbdgt_ac_to_id,acto.ac_code as ac_code_to,acto.ac_name as ac_name_to, " _
            & "  cbdgt_sb_to_id, " _
            & "  cbdgt_cc_to_id,ccto.cc_desc as cc_desc_to, " _
            & "  cbdgt_periode_to,pto.bdgtp_code as periode_to, " _
            & "  cbdgt_remarks, " _
            & "  cbdgt_dt,cbdgt_code, " _
            & "  cbdgt_amount,cbdgt_trans_id,cbdgt_tran_id " _
            & "FROM  " _
            & "  public.cbdgt_mstr " _
            & "  inner join en_mstr on en_id = cbdgt_en_id " _
            & "  inner join ac_mstr acf on acf.ac_id = cbdgt_ac_from_id " _
            & "  inner join ac_mstr acto on acto.ac_id = cbdgt_ac_to_id " _
            & "  inner join cc_mstr ccf on ccf.cc_id = cbdgt_cc_from_id " _
            & "  inner join cc_mstr ccto on ccto.cc_id = cbdgt_cc_to_id " _
            & "  inner join bdgtp_periode pfrom on pfrom.bdgtp_id = cbdgt_periode_from " _
            & "  inner join bdgtp_periode pto on pto.bdgtp_id = cbdgt_periode_to " _
            & "  where cbdgt_date >= " + SetDate(pr_date_1.DateTime) _
            & "  and cbdgt_date <= " + SetDate(pr_date_2.DateTime) _
            & "  and cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_email, "email")
        gv_email.BestFitColumns()

        Try
            ds.Tables("smart").Clear()
        Catch ex As Exception
        End Try

        sql = "select cbdgt_oid, cbdgt_code, cbdgt_trans_id, false as status from cbdgt_mstr " _
            & " where cbdgt_trans_id ~~* 'd' " _
            & " and cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                  & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_smart, "smart")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_wf.Columns("wf_ref_code").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code").ToString & "'")
            gv_wf.BestFitColumns()

            gv_email.Columns("cbdgt_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cbdgt_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid").ToString & "'")
            gv_email.BestFitColumns()

            'gv_detail.Columns("pbyd_pby_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pby_oid"))
            'gv_detail.BestFitColumns()

            'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code"))
            'gv_wf.BestFitColumns()

            'gv_email.Columns("cbdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid"))
            'gv_email.BestFitColumns()

            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        Catch ex As Exception
        End Try

    End Sub

    Public Overrides Sub insert_data_awal()
        cbdgt_en_id.ItemIndex = 0
        cbdgt_en_id.Focus()
        cbdgt_bdgt_oid.Text = ""
        cbdgt_date.EditValue = Today()
        cbdgt_year.Text = Year(Today()).ToString()
        cbdgt_periode_from.Text = Month(Today()).ToString()
        cbdgt_periode_to.Text = Month(Today()).ToString()
        cbdgt_cc_from_id.ItemIndex = 0
        cbdgt_cc_to_id.ItemIndex = 0
        cbdgt_ac_from_id.ItemIndex = 0
        cbdgt_ac_to_id.ItemIndex = 0
        cbdgt_amount.Text = ""
        cbdgt_remarks.Text = ""
        cbdgt_tran_id.ItemIndex = 0
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        'If Trim(fabk_code.Text) = "" Then
        '    MessageBox.Show("Code Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    fabk_code.Focus()
        '    before_save = False
        '    MessageBox.Show(pt_pl_id.Text)
        'End If
        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _cbdgt_oid As Guid
        _cbdgt_oid = Guid.NewGuid

        Dim i, a As Integer
        a = 0
        Dim ds_bantu As New DataSet
        Dim _cbdgt_code As String
        _cbdgt_code = func_coll.get_transaction_number("CB", cbdgt_en_id.GetColumnValue("en_code"), "cbdgt_mstr", "cbdgt_code")
        ds_bantu = func_data.load_aprv_mstr(cbdgt_tran_id.EditValue)

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
                                            & "  public.cbdgt_mstr " _
                                            & "( " _
                                            & "  cbdgt_oid, " _
                                            & "  cbdgt_dom_id, " _
                                            & "  cbdgt_en_id, " _
                                            & "  cbdgt_add_by, " _
                                            & "  cbdgt_add_date, " _
                                            & "  cbdgt_date, " _
                                            & "  cbdgt_year, " _
                                            & "  cbdgt_ac_from_id, " _
                                            & "  cbdgt_sb_from_id, " _
                                            & "  cbdgt_cc_from_id, " _
                                            & "  cbdgt_periode_from, " _
                                            & "  cbdgt_ac_to_id, " _
                                            & "  cbdgt_sb_to_id, " _
                                            & "  cbdgt_cc_to_id, " _
                                            & "  cbdgt_periode_to, " _
                                            & "  cbdgt_remarks, " _
                                            & "  cbdgt_dt, " _
                                            & "  cbdgt_amount, " _
                                            & "  cbdgt_bdgt_oid," _
                                            & "  cbdgt_trans_id, " _
                                            & "  cbdgt_tran_id, " _
                                            & "  cbdgt_code " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_cbdgt_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(cbdgt_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(cbdgt_date.DateTime) & ",  " _
                                            & SetInteger(cbdgt_year.EditValue) & ",  " _
                                            & SetInteger(cbdgt_ac_from_id.EditValue) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(cbdgt_cc_from_id.EditValue) & ",  " _
                                            & SetInteger(cbdgt_periode_from.EditValue) & ",  " _
                                            & SetInteger(cbdgt_ac_to_id.EditValue) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(cbdgt_cc_to_id.EditValue) & ",  " _
                                            & SetInteger(cbdgt_periode_to.EditValue) & ",  " _
                                            & SetSetring(cbdgt_remarks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDbl(cbdgt_amount.EditValue) & ",  " _
                                            & SetSetring(_bdgt_oid.ToString()) & ",  " _
                                            & SetSetring("D") & " , " _
                                            & SetInteger(cbdgt_tran_id.EditValue) & ",  " _
                                            & SetSetring(_cbdgt_code) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.wf_mstr " _
                                                    & "( " _
                                                    & "  wf_oid, " _
                                                    & "  wf_dom_id, " _
                                                    & "  wf_en_id, " _
                                                    & "  wf_tran_id, " _
                                                    & "  wf_ref_oid, " _
                                                    & "  wf_ref_code, " _
                                                    & "  wf_ref_desc, " _
                                                    & "  wf_seq, " _
                                                    & "  wf_user_id, " _
                                                    & "  wf_wfs_id, " _
                                                    & "  wf_iscurrent, " _
                                                    & "  wf_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(cbdgt_en_id.EditValue) & ",  " _
                                                    & SetInteger(cbdgt_tran_id.EditValue) & ",  " _
                                                    & SetSetring(_cbdgt_oid.ToString) & ",  " _
                                                    & SetSetring(_cbdgt_code) & ",  " _
                                                    & SetSetring("Cross Budget") & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                    & SetInteger(0) & ",  " _
                                                    & SetSetring("N") & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                    & ")"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(cbdgt_bdgt_oid.Text), "bdgt_code")
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
        Dim func_coll As New function_collection
        If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code")) > 0 Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        If MyBase.edit_data = True Then
            cbdgt_year.Focus()
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cbdgt_oid_mstr = .Item("cbdgt_oid").ToString
                cbdgt_en_id.EditValue = .Item("cbdgt_en_id")
                cbdgt_bdgt_oid.Text = SetString(.Item("bdgt_code"))
                _bdgt_oid = SetString(.Item("cbdgt_bdgt_oid"))
                load_detail(_bdgt_oid.ToString())

                cbdgt_date.EditValue = .Item("cbdgt_date")
                cbdgt_year.EditValue = .Item("cbdgt_year")
                cbdgt_periode_from.EditValue = .Item("cbdgt_periode_from")
                cbdgt_periode_to.EditValue = .Item("cbdgt_periode_to")
                cbdgt_cc_from_id.EditValue = .Item("cbdgt_cc_from_id")
                cbdgt_cc_to_id.EditValue = .Item("cbdgt_cc_to_id")
                cbdgt_ac_from_id.EditValue = .Item("cbdgt_ac_from_id")
                cbdgt_ac_to_id.EditValue = .Item("cbdgt_ac_to_id")
                cbdgt_amount.EditValue = .Item("cbdgt_amount")
                cbdgt_remarks.Text = SetString(.Item("cbdgt_remarks"))
                cbdgt_tran_id.EditValue = .Item("cbdgt_tran_id")
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        Dim i, a As Integer
        Dim ds_bantu As New DataSet
        ds_bantu = func_data.load_aprv_mstr(cbdgt_tran_id.EditValue)
        a = 0
        edit = True
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
                                            & "  public.cbdgt_mstr   " _
                                            & "SET  " _
                                            & "  cbdgt_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  cbdgt_en_id = " & SetInteger(cbdgt_en_id.EditValue) & ",  " _
                                            & "  cbdgt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  cbdgt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  cbdgt_date = " & SetDate(cbdgt_date.DateTime) & ",  " _
                                            & "  cbdgt_year = " & SetInteger(cbdgt_year.EditValue) & ",  " _
                                            & "  cbdgt_ac_from_id = " & SetInteger(cbdgt_ac_from_id.EditValue) & ",  " _
                                            & "  cbdgt_sb_from_id = " & SetInteger(0) & ",  " _
                                            & "  cbdgt_cc_from_id = " & SetInteger(cbdgt_cc_from_id.EditValue) & ",  " _
                                            & "  cbdgt_periode_from = " & SetInteger(cbdgt_periode_from.EditValue) & ",  " _
                                            & "  cbdgt_ac_to_id = " & SetInteger(cbdgt_ac_to_id.EditValue) & ",  " _
                                            & "  cbdgt_sb_to_id = " & SetInteger(0) & ",  " _
                                            & "  cbdgt_cc_to_id = " & SetInteger(cbdgt_cc_to_id.EditValue) & ",  " _
                                            & "  cbdgt_periode_to = " & SetInteger(cbdgt_periode_to.EditValue) & ",  " _
                                            & "  cbdgt_remarks = " & SetSetring(cbdgt_remarks.Text) & ",  " _
                                            & "  cbdgt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  cbdgt_amount = " & SetDbl(cbdgt_amount.EditValue) & " , " _
                                            & "  cbdgt_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & " , " _
                                            & "  cbdgt_trans_id = 'D',  " _
                                            & "  cbdgt_tran_id = " & SetInteger(cbdgt_tran_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cbdgt_oid = " & SetSetring(_cbdgt_oid_mstr.ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _cbdgt_oid_mstr.ToString + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.wf_mstr " _
                                                    & "( " _
                                                    & "  wf_oid, " _
                                                    & "  wf_dom_id, " _
                                                    & "  wf_en_id, " _
                                                    & "  wf_tran_id, " _
                                                    & "  wf_ref_oid, " _
                                                    & "  wf_ref_code, " _
                                                    & "  wf_ref_desc, " _
                                                    & "  wf_seq, " _
                                                    & "  wf_user_id, " _
                                                    & "  wf_wfs_id, " _
                                                    & "  wf_iscurrent, " _
                                                    & "  wf_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(cbdgt_en_id.EditValue) & ",  " _
                                                    & SetSetring(cbdgt_tran_id.EditValue) & ",  " _
                                                    & SetSetring(_cbdgt_oid_mstr.ToString) & ",  " _
                                                    & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code")) & ",  " _
                                                    & SetSetring("Cross Budget") & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                    & SetInteger(0) & ",  " _
                                                    & SetSetring("N") & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                    & ")"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(cbdgt_bdgt_oid.Text), "bdgt_code")
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
        row = BindingContext(ds.Tables(0)).Position
        Dim func_coll As New function_collection
        If func_coll.get_status_wf(ds.Tables(0).Rows(row).Item("cbdgt_code")) > 0 Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If


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
                            .Command.CommandText = "delete from cbdgt_mstr where cbdgt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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

    Private Sub cbdgt_cc_from_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbdgt_cc_from_id.EditValueChanged
        cbdgt_cc_to_id.EditValue = cbdgt_cc_from_id.EditValue

        dt_bantu = New DataTable
        dt_bantu = load_account(cbdgt_cc_from_id.EditValue, cbdgt_en_id.EditValue)
        cbdgt_ac_from_id.Properties.DataSource = dt_bantu
        cbdgt_ac_from_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cbdgt_ac_from_id.Properties.ValueMember = dt_bantu.Columns("cca_ac_id").ToString
        cbdgt_ac_from_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = load_account(cbdgt_cc_from_id.EditValue, cbdgt_en_id.EditValue)
        cbdgt_ac_to_id.Properties.DataSource = dt_bantu
        cbdgt_ac_to_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cbdgt_ac_to_id.Properties.ValueMember = dt_bantu.Columns("cca_ac_id").ToString
        cbdgt_ac_to_id.ItemIndex = 0
    End Sub

    Public Overrides Sub approve_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid").ToString
        _colom = "cbdgt_trans_id"
        _table = "cbdgt_mstr"
        _criteria = "cbdgt_code"
        _initial = "cbdgt"
        _type = "cb"
        _title = "CrossBudget"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid").ToString
        _colom = "cbdgt_trans_id"
        _table = "cbdgt_mstr"
        _criteria = "cbdgt_code"
        _initial = "cbdgt"
        _type = "cb"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub reminder_mail()
        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_code")
        _type = "cb"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "CrossBudget"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("cbdgt_oid").FilterInfo = _
                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cbdgt_oid='" & ds.Tables("smart").Rows(BindingContext(ds.Tables("smart")).Position).Item("cbdgt_oid").ToString & "'")

                    'gv_email.Columns("cbdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("cbdgt_oid"))
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("cbdgt_code"), 0)
                user_wf_email = mf.get_email_address(user_wf)

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update cbdgt_mstr set cbdgt_trans_id = '" + _trans_id + "'," + _
                                               " cbdgt_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " cbdgt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where cbdgt_oid = '" + ds.Tables("smart").Rows(i).Item("cbdgt_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("cbdgt_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("cbdgt_code"), "cb")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("cbdgt_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("cb", ds.Tables("smart").Rows(i).Item("cbdgt_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

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
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cbdgt_bdgt_oid_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbdgt_bdgt_oid.ButtonClick
        Dim frm As New FBudgetSearch
        frm.set_win(Me)
        frm._en_id = cbdgt_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

End Class


