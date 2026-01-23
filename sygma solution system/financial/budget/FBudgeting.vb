Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FBudgeting
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr As String
    Dim ds_edit, ds_acount, ds_periode As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim mf As New master_new.ModFunction
    Dim _bdgt_oid_mstr, _bdgt_code As String
    Dim _revisi As Integer

#Region "Seting Awal"
    Private Sub FBudgeting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Function load_budget_periode(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  distinct(bdgtp_year), " _
                            & "  bdgtp_en_id " _
                            & "FROM  " _
                            & "  public.bdgtp_periode  " _
                            & "  inner join en_mstr on en_id = bdgtp_en_id " _
                            & " where bdgtp_en_id = " + par_en_id.ToString()
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "entity")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        bdgt_en_id.Properties.DataSource = dt_bantu
        bdgt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bdgt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bdgt_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_transaction())
        bdgt_tran_id.Properties.DataSource = dt_bantu
        bdgt_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        bdgt_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
        bdgt_tran_id.ItemIndex = 0
    End Sub

    Private Sub bdgt_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bdgt_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ccr_restrc(bdgt_en_id.EditValue))
        bdgt_cc_id.Properties.DataSource = dt_bantu
        bdgt_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        bdgt_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        bdgt_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = load_budget_periode(bdgt_en_id.EditValue)
        bdgt_year_periode.Properties.DataSource = dt_bantu
        bdgt_year_periode.Properties.DisplayMember = dt_bantu.Columns("bdgtp_year").ToString
        bdgt_year_periode.Properties.ValueMember = dt_bantu.Columns("bdgtp_year").ToString
        bdgt_year_periode.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "bdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Tahun", "bdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "bdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "bdgt_trans_id", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Revisi", "bdgt_rev", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "IsActive", "bdgt_active", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "bdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "bdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_edit, "bdgtd_oid", False)
        'add_column(gv_edit, "bdgtd_bdgt_oid", False)
        'add_column_edit(gv_edit, "Month", "bdgtd_month", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "bdgtd_ac_id", False)
        'add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column(gv_detail, "bdgtd_oid", False)
        'add_column(gv_detail, "bdgtd_bdgt_oid", False)
        'add_column_copy(gv_detail, "Month", "bdgtd_month", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Alokasi", "bdgtd_alokasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Realisasi", "bdgtd_realisasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_wf, "wf_ref_oid", False)
        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "bdgtd_oid", False)
        add_column(gv_email, "bdgtd_bdgt_oid", False)
        add_column(gv_email, "Date", "bdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Tahun", "bdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Periode", "bdgtp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Alokasi", "bdgtd_alokasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Realisasi", "bdgtd_realisasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_smart, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bdgt_oid, " _
                    & "  bdgt_dom_id, " _
                    & "  bdgt_en_id,en_desc, " _
                    & "  bdgt_add_by, " _
                    & "  bdgt_add_date, " _
                    & "  bdgt_upd_by, " _
                    & "  bdgt_upd_date, " _
                    & "  bdgt_date,bdgt_code, " _
                    & "  bdgt_year,bdgt_year_periode, " _
                    & "  bdgt_remarks, " _
                    & "  bdgt_trans_id, " _
                    & "  bdgt_dt,bdgt_rev,bdgt_active, " _
                    & "  bdgt_tran_id,tran_name, " _
                    & "  bdgt_cc_id,cc_desc " _
                    & "FROM  " _
                    & "  public.bdgt_mstr " _
                    & "  inner join en_mstr on en_id = bdgt_en_id " _
                    & "  inner join tran_mstr on tran_id = bdgt_tran_id " _
                    & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                    & " where bdgt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and bdgt_en_id in (select user_en_id from tconfuserentity " _
                                          & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & " order by bdgt_date asc "

        Return get_sequel
    End Function

    Public Sub LoadDetail()
        Dim _ds_detail As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & " where bdgtd_bdgt_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid")).ToString() _
                            & " order by bdgtd_bdgtp_id asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_detail, "detail")
                    pgc_detail.DataSource = _ds_detail
                    pgc_detail.DataMember = "detail"
                    'pgc_detail.OptionsView.
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        LoadDetail()

        Dim sql As String
        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        'sql = "SELECT  " _
        '    & "  bdgtd_oid, " _
        '    & "  bdgtd_bdgt_oid, " _
        '    & "  bdgtd_add_by, " _
        '    & "  bdgtd_add_date, " _
        '    & "  bdgtd_upd_by, " _
        '    & "  bdgtd_upd_date, " _
        '    & "  bdgtd_month, " _
        '    & "  bdgtd_ac_id,ac_code,ac_name, " _
        '    & "  bdgtd_sb_id, " _
        '    & "  bdgtd_budget, " _
        '    & "  bdgtd_alokasi, " _
        '    & "  bdgtd_realisasi, " _
        '    & "  bdgtd_dt " _
        '    & "FROM  " _
        '    & "  public.bdgtd_det " _
        '    & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
        '    & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
        '    & " where bdgt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        '    & " and bdgt_en_id in (select user_en_id from tconfuserentity " _
        '    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
        '    & " order by bdgtd_month asc "
        'load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("wf").Clear()
        Catch ex As Exception
        End Try

        sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
              " wf_iscurrent, wf_seq " + _
              " from wf_mstr w " + _
              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
              " inner join bdgt_mstr on bdgt_code = wf_ref_code " + _
              " inner join bdgtd_det dt on dt.bdgtd_bdgt_oid = bdgt_oid " _
            & " where bdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
            & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
            & " and bdgt_en_id in (select user_en_id from tconfuserentity " _
                                  & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by wf_ref_code, wf_seq "
        load_data_detail(sql, gc_wf, "wf")
        gv_wf.BestFitColumns()

        Try
            ds.Tables("email").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  bdgtd_oid, " _
            & "  bdgtd_bdgt_oid, bdgt_code,bdgt_date,bdgt_year,cc_desc," _
            & "  bdgtd_add_by, " _
            & "  bdgtd_add_date, " _
            & "  bdgtd_upd_by, " _
            & "  bdgtd_upd_date, " _
            & "  bdgtd_bdgtp_id, " _
            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
            & "  bdgtd_ac_id,ac_code,ac_name, " _
            & "  bdgtd_sb_id, " _
            & "  bdgtd_budget, " _
            & "  bdgtd_alokasi, " _
            & "  bdgtd_realisasi, " _
            & "  bdgtd_dt " _
            & "FROM  " _
            & "  public.bdgtd_det " _
            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
            & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
            & " where bdgt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and bdgt_en_id in (select user_en_id from tconfuserentity " _
                                  & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by bdgtp_start_date asc "
        load_data_detail(sql, gc_email, "email")
        gv_email.BestFitColumns()

        Try
            ds.Tables("smart").Clear()
        Catch ex As Exception
        End Try

        sql = "select bdgt_oid, bdgt_code, bdgt_trans_id, false as status from bdgt_mstr " _
            & " where bdgt_trans_id ~~* 'd' " _
            & " and bdgt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and bdgt_en_id in (select user_en_id from tconfuserentity " _
                                  & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_smart, "smart")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_wf.Columns("wf_ref_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString & "'")
            gv_wf.BestFitColumns()

            gv_email.Columns("bdgtd_bdgt_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("bdgtd_bdgt_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString & "'")
            gv_email.BestFitColumns()

            'gv_detail.Columns("bdgtd_bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid"))
            'gv_detail.BestFitColumns()

            'gv_wf.Columns("wf_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid"))
            'gv_wf.BestFitColumns()

            'gv_email.Columns("bdgtd_bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid"))
            'gv_email.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"

    'Public Overrides Sub load_cb_en()
    '    dt_bantu = New DataTable
    '    dt_bantu = (func_data.load_data("ptnr_mstr_vend", asrtr_en_id.EditValue))
    '    po_ptnr_id.Properties.DataSource = dt_bantu
    '    po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
    '    po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
    '    po_ptnr_id.ItemIndex = 0
    'End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        ce_rev.Enabled = False
        bdgt_rev.Enabled = False

        bdgt_en_id.Focus()
        bdgt_en_id.ItemIndex = 0
        bdgt_date.DateTime = _now
        bdgt_year.Text = Year(Today()).ToString()
        bdgt_remarks.Text = ""
        bdgt_tran_id.ItemIndex = 0

        Try
            xtc_master.SelectedTabPageIndex = 1
            scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
            xtp_edit.PageVisible = True
            xtp_data.PageVisible = False

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ce_rev.Checked = False

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & " where bdgtd_bdgtp_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()

        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        ds_acount = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  cca_oid, " _
                            & "  cca_dom_id, " _
                            & "  cca_en_id, " _
                            & "  cca_add_by, " _
                            & "  cca_add_date, " _
                            & "  cca_upd_by, " _
                            & "  cca_upd_date, " _
                            & "  cca_cc_id, " _
                            & "  cca_ac_id,ac_code,ac_name, " _
                            & "  cca_remarks, " _
                            & "  cca_dt, " _
                            & "  cca_status " _
                            & "FROM  " _
                            & "  public.cca_acount " _
                            & "  inner join ac_mstr on ac_id = cca_ac_id " _
                            & " where cca_cc_id = " + SetInteger(bdgt_cc_id.EditValue) _
                            & " and cca_status = True "
                    .InitializeCommand()
                    .FillDataSet(ds_acount, "account")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_periode = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtp_oid, " _
                            & "  bdgtp_dom_id, " _
                            & "  bdgtp_en_id, " _
                            & "  bdgtp_add_by, " _
                            & "  bdgtp_add_date, " _
                            & "  bdgtp_upd_by, " _
                            & "  bdgtp_upd_date, " _
                            & "  bdgtp_id, " _
                            & "  bdgtp_code, " _
                            & "  bdgtp_remarks, " _
                            & "  bdgtp_start_date, " _
                            & "  bdgtp_end_date, " _
                            & "  bdgtp_dt,bdgtp_year " _
                            & "FROM  " _
                            & "  public.bdgtp_periode " _
                            & " where bdgtp_year = " + SetSetring(bdgt_year_periode.Text) _
                            & " order by bdgtp_id asc "
                    .InitializeCommand()
                    .FillDataSet(ds_periode, "periode")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ds_bantu As New DataSet
        Dim _bdgt_oid As Guid
        _bdgt_oid = Guid.NewGuid

        Dim _bdgt_code As String
        Dim i, a As Integer

        _bdgt_code = func_coll.get_transaction_number("BG", bdgt_en_id.GetColumnValue("en_code"), "bdgt_mstr", "bdgt_code")
        ds_bantu = func_data.load_aprv_mstr(bdgt_tran_id.EditValue)

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
                                            & "  public.bdgt_mstr " _
                                            & "( " _
                                            & "  bdgt_oid, " _
                                            & "  bdgt_dom_id, " _
                                            & "  bdgt_en_id, " _
                                            & "  bdgt_add_by, " _
                                            & "  bdgt_add_date, " _
                                            & "  bdgt_date, " _
                                            & "  bdgt_year, " _
                                            & "  bdgt_remarks, " _
                                            & "  bdgt_trans_id, " _
                                            & "  bdgt_dt, " _
                                            & "  bdgt_tran_id, " _
                                            & "  bdgt_cc_id, " _
                                            & "  bdgt_code, " _
                                            & "  bdgt_rev, " _
                                            & "  bdgt_active, " _
                                            & "  bdgt_year_periode " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bdgt_oid.ToString()) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(bdgt_date.DateTime) & ",  " _
                                            & SetInteger(bdgt_year.Text) & ",  " _
                                            & SetSetring(bdgt_remarks.Text) & ",  " _
                                            & SetSetring("D") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(bdgt_tran_id.EditValue) & ",  " _
                                            & SetInteger(bdgt_cc_id.EditValue) & ",  " _
                                            & SetSetring(_bdgt_code) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetSetring("Y") & " , " _
                                            & SetSetring(bdgt_year_periode.Text) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_periode.Tables(0).Rows.Count - 1
                            For a = 0 To ds_acount.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.bdgtd_det " _
                                                    & "( " _
                                                    & "  bdgtd_oid, " _
                                                    & "  bdgtd_bdgt_oid, " _
                                                    & "  bdgtd_add_by, " _
                                                    & "  bdgtd_add_date, " _
                                                    & "  bdgtd_bdgtp_id, " _
                                                    & "  bdgtd_ac_id, " _
                                                    & "  bdgtd_sb_id, " _
                                                    & "  bdgtd_budget, " _
                                                    & "  bdgtd_alokasi, " _
                                                    & "  bdgtd_realisasi, " _
                                                    & "  bdgtd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_bdgt_oid.ToString) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetInteger(ds_periode.Tables(0).Rows(i).Item("bdgtp_id")) & ",  " _
                                                    & SetInteger(ds_acount.Tables(0).Rows(a).Item("cca_ac_id")) & ",  " _
                                                    & SetInteger(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        Next

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
                                                    & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                                    & SetInteger(bdgt_tran_id.EditValue) & ",  " _
                                                    & SetSetring(_bdgt_oid.ToString) & ",  " _
                                                    & SetSetring(_bdgt_code) & ",  " _
                                                    & SetSetring("Budget Request") & ",  " _
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
                        set_row(_bdgt_oid.ToString, "bdgt_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_trans_id") <> "D" Then
        '    If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code")) > 0 Then
        '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Function
        '    End If
        'End If

        Dim _trans_id, _active As String
        _trans_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_trans_id")
        _active = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_active")

        'If _trans_id = "D" Or _active = "N" Then
        '    bdgt_rev.Enabled = False
        '    ce_rev.Enabled = False
        'Else
        '    bdgt_rev.Enabled = False
        '    ce_rev.Enabled = True
        'End If

        If _trans_id <> "I" Or _trans_id <> "C" Or _active = "N" Then
            bdgt_rev.Enabled = False
            ce_rev.Enabled = False
        Else
            bdgt_rev.Enabled = False
            ce_rev.Enabled = True
        End If

        If ds.Tables.Count = 0 Then
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            Exit Function
        End If


        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _bdgt_oid_mstr = .Item("bdgt_oid").ToString
                bdgt_en_id.EditValue = .Item("bdgt_en_id")
                _bdgt_code = .Item("bdgt_code")
                bdgt_year.EditValue = .Item("bdgt_year")
                _revisi = .Item("bdgt_rev")
                bdgt_rev.Value = .Item("bdgt_rev")
                bdgt_date.DateTime = .Item("bdgt_date")
                bdgt_cc_id.EditValue = .Item("bdgt_cc_id")
                bdgt_remarks.Text = SetString(.Item("bdgt_remarks"))
                bdgt_tran_id.EditValue = .Item("bdgt_tran_id")
                bdgt_year_periode.EditValue = SetString(.Item("bdgt_year_periode"))
            End With
            bdgt_en_id.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & " where bdgtd_bdgt_oid = '" + ds.Tables(0).Rows(row).Item("bdgt_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ce_rev.Checked = False
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        If bdgt_rev.Value > _revisi Then
            If CopyBudget() = False Then
                MessageBox.Show("Generate Revisi Budget Gagal,,!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False 
            Else
                edit = True
                after_success()
                set_row(_bdgt_oid_mstr, "bdgt_oid")
                Return edit
            End If
        End If

        ds_acount = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  cca_oid, " _
                            & "  cca_dom_id, " _
                            & "  cca_en_id, " _
                            & "  cca_add_by, " _
                            & "  cca_add_date, " _
                            & "  cca_upd_by, " _
                            & "  cca_upd_date, " _
                            & "  cca_cc_id, " _
                            & "  cca_ac_id,ac_code,ac_name, " _
                            & "  cca_remarks, " _
                            & "  cca_dt, " _
                            & "  cca_status " _
                            & "FROM  " _
                            & "  public.cca_acount " _
                            & "  inner join ac_mstr on ac_id = cca_ac_id " _
                            & " where cca_cc_id = " + SetInteger(bdgt_cc_id.EditValue)
                    .InitializeCommand()
                    .FillDataSet(ds_acount, "account")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_periode = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtp_oid, " _
                            & "  bdgtp_dom_id, " _
                            & "  bdgtp_en_id, " _
                            & "  bdgtp_add_by, " _
                            & "  bdgtp_add_date, " _
                            & "  bdgtp_upd_by, " _
                            & "  bdgtp_upd_date, " _
                            & "  bdgtp_id, " _
                            & "  bdgtp_code, " _
                            & "  bdgtp_remarks, " _
                            & "  bdgtp_start_date, " _
                            & "  bdgtp_end_date, " _
                            & "  bdgtp_dt,bdgtp_year " _
                            & "FROM  " _
                            & "  public.bdgtp_periode " _
                            & " where bdgtp_year = " + SetSetring(bdgt_year_periode.Text) _
                            & " order by bdgtp_id asc "
                    .InitializeCommand()
                    .FillDataSet(ds_periode, "periode")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        edit = True
        Dim _pod_qty_receive As Double = 0
        Dim i, a As Integer
        Dim ds_bantu As New DataSet

        ds_bantu = func_data.load_aprv_mstr(bdgt_tran_id.EditValue)

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
                                            & "  public.bdgt_mstr   " _
                                            & "SET  " _
                                            & "  bdgt_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  bdgt_en_id = " & bdgt_en_id.EditValue & ",  " _
                                            & "  bdgt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bdgt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  bdgt_date = " & SetDate(bdgt_date.DateTime) & ",  " _
                                            & "  bdgt_year = " & SetInteger(bdgt_year.Text) & ",  " _
                                            & "  bdgt_remarks = " & SetSetring(bdgt_remarks.Text) & ",  " _
                                            & "  bdgt_trans_id = " & SetSetring("D") & ",  " _
                                            & "  bdgt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  bdgt_tran_id = " & SetInteger(bdgt_tran_id.EditValue) & ",  " _
                                            & "  bdgt_cc_id = " & SetInteger(bdgt_cc_id.EditValue) & ",  " _
                                            & "  bdgt_year_periode = " & SetSetring(bdgt_year_periode.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bdgt_oid = " & SetSetring(_bdgt_oid_mstr.ToString()) & "  " _
                                            & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from bdgtd_det where bdgtd_bdgt_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid"))
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_periode.Tables(0).Rows.Count - 1
                            For a = 0 To ds_acount.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.bdgtd_det " _
                                                    & "( " _
                                                    & "  bdgtd_oid, " _
                                                    & "  bdgtd_bdgt_oid, " _
                                                    & "  bdgtd_add_by, " _
                                                    & "  bdgtd_add_date, " _
                                                    & "  bdgtd_bdgtp_id, " _
                                                    & "  bdgtd_ac_id, " _
                                                    & "  bdgtd_sb_id, " _
                                                    & "  bdgtd_budget, " _
                                                    & "  bdgtd_alokasi, " _
                                                    & "  bdgtd_realisasi, " _
                                                    & "  bdgtd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_bdgt_oid_mstr.ToString) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetInteger(ds_periode.Tables(0).Rows(i).Item("bdgtp_id")) & ",  " _
                                                    & SetInteger(ds_acount.Tables(0).Rows(a).Item("cca_ac_id")) & ",  " _
                                                    & SetInteger(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        Next

                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    For a = 0 To ds_acount.Tables(0).Rows.Count - 1
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = "INSERT INTO  " _
                        '                            & "  public.bdgtd_det " _
                        '                            & "( " _
                        '                            & "  bdgtd_oid, " _
                        '                            & "  bdgtd_bdgt_oid, " _
                        '                            & "  bdgtd_upd_by, " _
                        '                            & "  bdgtd_upd_date, " _
                        '                            & "  bdgtd_bdgtp_id, " _
                        '                            & "  bdgtd_ac_id, " _
                        '                            & "  bdgtd_sb_id, " _
                        '                            & "  bdgtd_budget, " _
                        '                            & "  bdgtd_alokasi, " _
                        '                            & "  bdgtd_realisasi, " _
                        '                            & "  bdgtd_dt " _
                        '                            & ")  " _
                        '                            & "VALUES ( " _
                        '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                            & SetSetring(_bdgt_oid_mstr.ToString) & ",  " _
                        '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        '                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_bdgtp_id")) & ",  " _
                        '                            & SetInteger(ds_acount.Tables(0).Rows(a).Item("cca_ac_id")) & ",  " _
                        '                            & SetInteger(0) & ",  " _
                        '                            & SetDbl(0) & ",  " _
                        '                            & SetDbl(0) & ",  " _
                        '                            & SetDbl(0) & ",  " _
                        '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                            & ");"
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next

                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.bdgtd_det " _
                        '                        & "( " _
                        '                        & "  bdgtd_oid, " _
                        '                        & "  bdgtd_bdgt_oid, " _
                        '                        & "  bdgtd_upd_by, " _
                        '                        & "  bdgtd_upd_date, " _
                        '                        & "  bdgtd_bdgtp_id, " _
                        '                        & "  bdgtd_ac_id, " _
                        '                        & "  bdgtd_sb_id, " _
                        '                        & "  bdgtd_budget, " _
                        '                        & "  bdgtd_alokasi, " _
                        '                        & "  bdgtd_realisasi, " _
                        '                        & "  bdgtd_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetSetring(_bdgt_oid_mstr.ToString) & ",  " _
                        '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_bdgtp_id")) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_ac_id")) & ",  " _
                        '                        & SetInteger(0) & ",  " _
                        '                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("bdgtd_budget")) & ",  " _
                        '                        & SetDbl(0) & ",  " _
                        '                        & SetDbl(0) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ");"
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _bdgt_oid_mstr.ToString + "'"
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
                                                    & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                                    & SetSetring(bdgt_tran_id.EditValue) & ",  " _
                                                    & SetSetring(_bdgt_oid_mstr.ToString) & ",  " _
                                                    & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code")) & ",  " _
                                                    & SetSetring("Budget Request") & ",  " _
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
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_bdgt_oid_mstr, "bdgt_oid")
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

    Public Function CopyBudget() As Boolean
        CopyBudget = True

        Dim _bdgt_oid As Guid
        _bdgt_oid = Guid.NewGuid
        Dim i As Integer

        Dim ds_bantu As New DataSet
        ds_bantu = func_data.load_aprv_mstr(bdgt_tran_id.EditValue)

        Dim _bdgt_code_rev As String
        _bdgt_code_rev = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code").ToString().Substring(0, 15) + "/" + bdgt_rev.Value.ToString()
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
                                            & "  public.bdgt_mstr " _
                                            & "( " _
                                            & "  bdgt_oid, " _
                                            & "  bdgt_dom_id, " _
                                            & "  bdgt_en_id, " _
                                            & "  bdgt_add_by, " _
                                            & "  bdgt_add_date, " _
                                            & "  bdgt_date, " _
                                            & "  bdgt_year, " _
                                            & "  bdgt_remarks, " _
                                            & "  bdgt_trans_id, " _
                                            & "  bdgt_dt, " _
                                            & "  bdgt_tran_id, " _
                                            & "  bdgt_cc_id, " _
                                            & "  bdgt_code, " _
                                            & "  bdgt_rev, " _
                                            & "  bdgt_active, " _
                                            & "  bdgt_year_periode " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bdgt_oid.ToString()) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(bdgt_date.DateTime) & ",  " _
                                            & SetInteger(bdgt_year.Text) & ",  " _
                                            & SetSetring(bdgt_remarks.Text) & ",  " _
                                            & SetSetring("D") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(bdgt_tran_id.EditValue) & ",  " _
                                            & SetInteger(bdgt_cc_id.EditValue) & ",  " _
                                            & SetSetring(_bdgt_code_rev) & ",  " _
                                            & SetInteger(bdgt_rev.Value) & ",  " _
                                            & SetSetring("Y") & ", " _
                                            & SetSetring(bdgt_year_periode.Text) & "  " _
                                            & ");"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update  " _
                                            & "  public.bdgt_mstr " _
                                            & "  set " _
                                            & "  bdgt_active = 'N' " _
                                            & "  where bdgt_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString())
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.bdgtd_det " _
                                                & "( " _
                                                & "  bdgtd_oid, " _
                                                & "  bdgtd_bdgt_oid, " _
                                                & "  bdgtd_add_by, " _
                                                & "  bdgtd_add_date, " _
                                                & "  bdgtd_bdgtp_id, " _
                                                & "  bdgtd_ac_id, " _
                                                & "  bdgtd_sb_id, " _
                                                & "  bdgtd_budget, " _
                                                & "  bdgtd_alokasi, " _
                                                & "  bdgtd_realisasi, " _
                                                & "  bdgtd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString()) & ",  " _
                                                & SetSetring(_bdgt_oid.ToString()) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_bdgtp_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("bdgtd_ac_id")) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("bdgtd_budget")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("bdgtd_alokasi")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("bdgtd_realisasi")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        Dim a As Integer
                        For a = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                    & SetInteger(bdgt_en_id.EditValue) & ",  " _
                                                    & SetInteger(bdgt_tran_id.EditValue) & ",  " _
                                                    & SetSetring(_bdgt_oid.ToString) & ",  " _
                                                    & SetSetring(_bdgt_code_rev) & ",  " _
                                                    & SetSetring("Budget Request") & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("aprv_seq")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("aprv_user_id")) & ",  " _
                                                    & SetInteger(0) & ",  " _
                                                    & SetSetring("N") & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                    & ")"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()

                        set_row(_bdgt_oid.ToString, "bdgt_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        CopyBudget = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            MessageBox.Show(ex.Message)
        End Try

        Return CopyBudget
    End Function


    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Return before_delete
    End Function

    Public Overrides Function delete_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position
        Dim func_coll As New function_collection
        If func_coll.get_status_wf(ds.Tables(0).Rows(row).Item("bdgt_code")) > 0 Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        Dim _asrtr_oid As String
        _asrtr_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString

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

        Dim i As Integer = 0

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
                            .Command.CommandText = "delete from bdgt_mstr where bdgt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString + "'"
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
#End Region

    Public Overrides Sub approve_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString
        _colom = "bdgt_trans_id"
        _table = "bdgt_mstr"
        _criteria = "bdgt_code"
        _initial = "bdgt"
        _type = "bg"
        _title = "Budget"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_oid").ToString
        _colom = "bdgt_trans_id"
        _table = "bdgt_mstr"
        _criteria = "bdgt_code"
        _initial = "bdgt"
        _type = "bg"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub reminder_mail()
        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgt_code")
        _type = "bg"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Budget"

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
                    gv_email.Columns("bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("bdgt_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("bdgt_code"), 0)
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
                                .Command.CommandText = "update bdgt_mstr set bdgt_trans_id = '" + _trans_id + "'," + _
                                               " bdgt_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " bdgt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where bdgt_oid = '" + ds.Tables("smart").Rows(i).Item("bdgt_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("bdgt_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("bdgt_code"), "bg")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("bdgt_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("bg", ds.Tables("smart").Rows(i).Item("bdgt_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
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


    Private Sub gv_master_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_master.FocusedRowChanged
        Try
            LoadDetail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ce_rev_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_rev.CheckedChanged
        If ce_rev.Checked = True Then
            bdgt_rev.Value = SetNewRev()
        End If
    End Sub

    Private Function SetNewRev() As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(bdgt_rev),0) + 1 as max_col from bdgt_mstr " + _
                                           " where bdgt_code = " + SetSetring(_bdgt_code)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        SetNewRev = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return SetNewRev
    End Function

    Private Sub ce_rev_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_rev.EditValueChanged
        If ce_rev.Checked = True Then
            bdgt_en_id.Enabled = False
            bdgt_date.Enabled = False
            bdgt_year.Enabled = False
            bdgt_cc_id.Enabled = False
            bdgt_remarks.Enabled = False
            bdgt_tran_id.Enabled = False
            bdgt_year_periode.Enabled = False
        End If
    End Sub

End Class
