Imports npgsql
Imports master_new.ModFunction

Public Class FARPaymentDetail
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim _terbilang As String

    Private Sub FARPayment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        arpay_en_id.Properties.DataSource = dt_bantu
        arpay_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        arpay_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        arpay_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        arpay_cu_id.Properties.DataSource = dt_bantu
        arpay_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        arpay_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        arpay_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        arpay_ar_ac_id.Properties.DataSource = dt_bantu
        arpay_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        arpay_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        arpay_ar_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        arpay_disc_ac_id.Properties.DataSource = dt_bantu
        arpay_disc_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        arpay_disc_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        arpay_disc_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        arpay_exp_ac_id.Properties.DataSource = dt_bantu
        arpay_exp_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        arpay_exp_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        arpay_exp_ac_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer(arpay_en_id.EditValue))
        arpay_bill_to.Properties.DataSource = dt_bantu
        arpay_bill_to.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        arpay_bill_to.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        arpay_bill_to.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(arpay_en_id.EditValue))
        arpay_bk_id.Properties.DataSource = dt_bantu
        arpay_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        arpay_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        arpay_bk_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(arpay_en_id.EditValue))
        arpay_ar_sb_id.Properties.DataSource = dt_bantu
        arpay_ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        arpay_ar_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        arpay_ar_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(arpay_en_id.EditValue))
        arpay_ar_cc_id.Properties.DataSource = dt_bantu
        arpay_ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        arpay_ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        arpay_ar_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(arpay_en_id.EditValue))
        arpay_disc_sb_id.Properties.DataSource = dt_bantu
        arpay_disc_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        arpay_disc_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        arpay_disc_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(arpay_en_id.EditValue))
        arpay_disc_cc_id.Properties.DataSource = dt_bantu
        arpay_disc_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        arpay_disc_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        arpay_disc_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(arpay_en_id.EditValue))
        arpay_exp_sb_id.Properties.DataSource = dt_bantu
        arpay_exp_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        arpay_exp_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        arpay_exp_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(arpay_en_id.EditValue))
        arpay_exp_cc_id.Properties.DataSource = dt_bantu
        arpay_exp_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        arpay_exp_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        arpay_exp_cc_id.ItemIndex = 0
    End Sub

    Private Sub arpay_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles arpay_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub arpay_bk_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles arpay_bk_id.EditValueChanged
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bk_cu_id, bk_ac_id, bk_sb_id, bk_cc_id from bk_mstr where bk_id = " + arpay_bk_id.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        arpay_cu_id.EditValue = .DataReader.Item("bk_cu_id")
                        arpay_ar_ac_id.EditValue = .DataReader.Item("bk_ac_id")
                        arpay_ar_sb_id.EditValue = .DataReader.Item("bk_sb_id")
                        arpay_ar_cc_id.EditValue = .DataReader.Item("bk_cc_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub arpay_date_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles arpay_date.EditValueChanged
        arpay_eff_date.DateTime = arpay_date.DateTime
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Number", "arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Date", "arpay_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Payment Eff. Date", "arpay_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account Code", "ac_ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account", "ac_ar_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_ar_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_ar_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Acct Code", "ac_disc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Acct", "ac_disc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Sub Acct", "sb_disc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Cost Center", "cc_disc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Expense Acct Code", "ac_exp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Expense Acct", "ac_exp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Expense Sub Acct", "sb_exp_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Expense Cost Center", "cc_exp_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total Amount", "arpay_total_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "arpay_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "arpay_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "arpay_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "arpay_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "arpay_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "arpayd_arpay_oid", False)
        add_column_copy(gv_detail, "Reference Voucher", "arpayd_ar_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Reference SO", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Angsuran ke-", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Type", "arpayd_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Exchange Rate", "arpayd_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Amount", "arpayd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Cash Amount", "arpayd_cash_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Disc. Amount", "arpayd_disc_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Exp. Amount", "arpayd_exp_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Currency Amount", "arpayd_cur_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Remarks", "arpayd_remarks", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "arpayd_oid", False)
        add_column(gv_edit, "arpayd_ar_oid", False)
        add_column(gv_edit, "arpayd_ar_ref", False)
        add_column(gv_edit, "sokp_oid", False)
        add_column(gv_edit, "SO Ref", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Angsuran Ke-", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "arpayd_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "arpayd_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "arpayd_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Exc. Rate AR", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Exc. Rate Today", "arpayd_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "ar_cu_id", False)
        add_column_edit(gv_edit, "Amount", "arpayd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Cash Amount", "arpayd_cash_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Disc. Amount", "arpayd_disc_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Exp. Amount", "arpayd_exp_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Currency Amount", "arpayd_cur_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Remarks", "arpayd_remarks", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.arpay_payment.arpay_oid, " _
                & "  public.arpay_payment.arpay_dom_id, " _
                & "  public.arpay_payment.arpay_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.arpay_payment.arpay_add_by, " _
                & "  public.arpay_payment.arpay_add_date, " _
                & "  public.arpay_payment.arpay_upd_by, " _
                & "  public.arpay_payment.arpay_upd_date, " _
                & "  public.arpay_payment.arpay_code, " _
                & "  public.arpay_payment.arpay_bill_to, " _
                & "  public.arpay_payment.arpay_cu_id, " _
                & "  public.arpay_payment.arpay_bk_id, " _
                & "  public.arpay_payment.arpay_ar_ac_id, " _
                & "  public.arpay_payment.arpay_ar_sb_id, " _
                & "  public.arpay_payment.arpay_ar_cc_id, " _
                & "  public.arpay_payment.arpay_disc_ac_id, " _
                & "  public.arpay_payment.arpay_disc_sb_id, " _
                & "  public.arpay_payment.arpay_disc_cc_id, " _
                & "  public.arpay_payment.arpay_date, " _
                & "  public.arpay_payment.arpay_eff_date, " _
                & "  public.arpay_payment.arpay_total_amount, " _
                & "  public.arpay_payment.arpay_remarks, " _
                & "  public.arpay_payment.arpay_dt, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.bk_mstr.bk_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  ac_ar_mstr.ac_code as ac_ar_code, " _
                & "  ac_ar_mstr.ac_name as ac_ar_name, " _
                & "  sb_ar_mstr.sb_desc as sb_ar_desc, " _
                & "  cc_ar_mstr.cc_desc as cc_ar_desc, " _
                & "  ac_disc_mstr.ac_code as ac_disc_code, " _
                & "  ac_disc_mstr.ac_name as ac_disc_name, " _
                & "  sb_disc_mstr.sb_desc as sb_disc_desc, " _
                & "  cc_disc_mstr.cc_desc as cc_disc_desc, " _
                & "  ac_exp_mstr.ac_code as ac_exp_code, " _
                & "  ac_exp_mstr.ac_name as ac_exp_name, " _
                & "  sb_exp_mstr.sb_desc as sb_exp_desc, " _
                & "  cc_exp_mstr.cc_desc as cc_exp_desc,arpay_cashi_oid " _
                & "FROM " _
                & "  public.arpay_payment " _
                & "  INNER JOIN public.en_mstr ON (public.arpay_payment.arpay_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.arpay_payment.arpay_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.arpay_payment.arpay_bk_id = public.bk_mstr.bk_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.arpay_payment.arpay_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ac_ar_mstr ON (public.arpay_payment.arpay_ar_ac_id = ac_ar_mstr.ac_id)" _
                & "  INNER JOIN public.sb_mstr sb_ar_mstr ON (public.arpay_payment.arpay_ar_sb_id = sb_ar_mstr.sb_id)" _
                & "  INNER JOIN public.cc_mstr cc_ar_mstr ON (public.arpay_payment.arpay_ar_cc_id = cc_ar_mstr.cc_id)" _
                & "  INNER JOIN public.ac_mstr ac_disc_mstr ON (public.arpay_payment.arpay_disc_ac_id = ac_disc_mstr.ac_id)" _
                & "  INNER JOIN public.sb_mstr sb_disc_mstr ON (public.arpay_payment.arpay_disc_sb_id = sb_disc_mstr.sb_id)" _
                & "  INNER JOIN public.cc_mstr cc_disc_mstr ON (public.arpay_payment.arpay_disc_cc_id = cc_disc_mstr.cc_id)" _
                & "  INNER JOIN public.ac_mstr ac_exp_mstr ON (public.arpay_payment.arpay_exp_ac_id = ac_exp_mstr.ac_id)" _
                & "  INNER JOIN public.sb_mstr sb_exp_mstr ON (public.arpay_payment.arpay_exp_sb_id = sb_exp_mstr.sb_id)" _
                & "  INNER JOIN public.cc_mstr cc_exp_mstr ON (public.arpay_payment.arpay_exp_cc_id = cc_exp_mstr.cc_id)" _
                & " where arpay_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and arpay_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and arpay_en_id in (select user_en_id from tconfuserentity " _
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
            & "  arpayd_oid, " _
            & "  arpayd_arpay_oid, " _
            & "  arpayd_ar_oid, " _
            & "  arpayd_ar_ref, " _
            & "  arpayd_type, " _
            & "  arpayd_ac_id, " _
            & "  ac_code,  " _
            & "  ac_name, " _
            & "  arpayd_sb_id, " _
            & "  sb_desc, " _
            & "  arpayd_cc_id, " _
            & "  cc_desc, " _
            & "  arpayd_amount, " _
            & "  arpayd_cash_amount, " _
            & "  arpayd_disc_amount, " _
            & "  arpayd_exp_amount, " _
            & "  arpayd_remarks, " _
            & "  arpayd_dt, " _
            & "  arpayd_cur_amount, " _
            & "  arpayd_sokp_oid, " _
            & "  so_code, " _
            & "  sokp_oid, " _
            & "  sokp_so_oid, " _
            & "  sokp_seq, " _
            & "  sokp_amount, " _
            & "  sokp_due_date, " _
            & "  sokp_amount_pay, " _
            & "  sokp_description,arpayd_exc_rate " _
            & "FROM  " _
            & "  public.arpayd_det  " _
            & "  inner join public.arpay_payment on arpay_oid = arpayd_arpay_oid " _
            & "  inner join public.sokp_piutang on arpayd_sokp_oid = sokp_oid" _
            & "  inner join public.so_mstr on sokp_so_oid = so_oid" _
            & "  inner join public.ac_mstr on ac_id = arpayd_ac_id " _
            & "  inner join public.sb_mstr on sb_id = arpayd_sb_id " _
            & "  inner join public.cc_mstr on cc_id = arpayd_cc_id" _
            & "  where arpay_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and arpay_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("arpayd_arpay_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("arpayd_arpay_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("arpayd_arpay_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        arpay_en_id.Focus()
        arpay_en_id.ItemIndex = 0
        arpay_date.DateTime = Now
        arpay_eff_date.DateTime = Now
        arpay_eff_date.Enabled = False
        arpay_cu_id.Enabled = False
        'arpay_ar_ac_id.ItemIndex = 0
        arpay_disc_ac_id.ItemIndex = 0
        arpay_exp_ac_id.ItemIndex = 0
        arpay_ar_ac_id.Enabled = False
        arpay_ar_sb_id.Enabled = False
        arpay_ar_cc_id.Enabled = False
        arpay_cashi_oid.Text = ""
        arpay_cashi_oid.Tag = ""
        arpay_cashi_amount.EditValue = 0.0
        arpay_remarks.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  arpayd_oid, " _
                        & "  arpayd_arpay_oid, " _
                        & "  arpayd_ar_oid, " _
                        & "  arpayd_ar_ref, " _
                        & "  arpayd_type, " _
                        & "  arpayd_ac_id, " _
                        & "  ac_code,  " _
                        & "  ac_name, " _
                        & "  arpayd_sb_id, " _
                        & "  sb_desc, " _
                        & "  arpayd_cc_id, " _
                        & "  cc_desc, " _
                        & "  arpayd_amount,  arpayd_exc_rate, " _
                        & "  arpayd_cash_amount, " _
                        & "  arpayd_disc_amount, " _
                        & "  arpayd_exp_amount, " _
                        & "  arpayd_remarks, " _
                        & "  arpayd_dt, " _
                        & "  ar_cu_id, " _
                        & "  arpayd_cur_amount, ar_exc_rate, " _
                        & "  sokp_oid, " _
                        & "  arso_ar_oid, " _
                        & "  arso_so_code, " _
                        & "  sokp_so_oid, " _
                        & "  sokp_seq, " _
                        & "  sokp_amount, " _
                        & "  sokp_due_date, " _
                        & "  sokp_amount_pay, " _
                        & "  sokp_description " _
                        & "FROM  " _
                        & "  public.arpayd_det  " _
                        & "  inner join public.arpay_payment on arpay_oid = arpayd_arpay_oid " _
                        & "  inner join public.ar_mstr on ar_oid = arpayd_ar_oid " _
                        & "  inner join public.arso_so on ar_mstr.ar_oid = arso_ar_oid " _
                        & "  inner join public.sokp_piutang on arso_so_oid = sokp_so_oid" _
                        & "  inner join public.ac_mstr on ac_id = arpayd_ac_id " _
                        & "  inner join public.sb_mstr on sb_id = arpayd_sb_id " _
                        & "  inner join public.cc_mstr on cc_id = arpayd_cc_id" _
                        & " where arpayd_ac_id = -99"
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

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _ar_cu_id As Integer
        Dim _arpayd_amount As Double = 0
        Try
            _ar_cu_id = gv_edit.GetRowCellValue(e.RowHandle, "ar_cu_id")
        Catch ex As Exception
            Exit Sub
        End Try

        If e.Column.Name = "arpayd_amount" Then
            gv_edit.SetRowCellValue(e.RowHandle, "arpayd_cash_amount", e.Value)
            _ar_cu_id = gv_edit.GetRowCellValue(e.RowHandle, "ar_cu_id")
            If _ar_cu_id <> master_new.ClsVar.ibase_cur_id Then
                gv_edit.SetRowCellValue(e.RowHandle, "arpayd_cur_amount", e.Value / func_data.get_exchange_rate(_ar_cu_id))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "arpayd_cur_amount", e.Value)
            End If
        ElseIf e.Column.Name = "arpayd_cash_amount" Then
            '_arpayd_amount = gv_edit.GetRowCellValue(e.RowHandle, "arpayd_amount")
            'gv_edit.SetRowCellValue(e.RowHandle, "arpayd_disc_amount", _arpayd_amount - e.Value)
            'dikomentarin karena sekarang ada kolom baru expense amount
        End If
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "arso_so_code" Or _col = "sokp_seq" Then
            'Dim frm As New FDRCRMemoSearch
            Dim frm As New FKPSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = arpay_en_id.EditValue
            frm._ptnr_id = arpay_bill_to.EditValue
            frm._cu_id = arpay_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "arpayd_oid", Guid.NewGuid.ToString)
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        Dim _gcald_det_status As String = func_data.get_gcald_det_status(arpay_en_id.EditValue, "gcald_ar", arpay_eff_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + arpay_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + arpay_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("arpayd_exc_rate") = 0 Then
                MessageBox.Show("Exchange Rate Does'nt Exist...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("arpayd_disc_amount") <> 0 Then
                If arpay_disc_ac_id.EditValue = 0 Then
                    MessageBox.Show("Account Discount Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("arpayd_exp_amount") <> 0 Then
                If arpay_exp_ac_id.EditValue = 0 Then
                    MessageBox.Show("Account Expense Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("arpayd_exp_amount") < 0 Then
                If arpay_exp_ac_id.EditValue = 0 Then
                    MessageBox.Show("Account Expense Can't Lower Than 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next

        Dim _arpayd_amount, _arpayd_cash_amount, _arpayd_disc_amount, _arpayd_exp_amount As Double
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            _arpayd_amount = ds_edit.Tables(0).Rows(i).Item("arpayd_amount")
            _arpayd_cash_amount = ds_edit.Tables(0).Rows(i).Item("arpayd_cash_amount")
            _arpayd_disc_amount = ds_edit.Tables(0).Rows(i).Item("arpayd_disc_amount")
            _arpayd_exp_amount = ds_edit.Tables(0).Rows(i).Item("arpayd_exp_amount")

            If _arpayd_amount <> _arpayd_cash_amount + _arpayd_disc_amount + _arpayd_exp_amount Then
                MessageBox.Show("Not Valid Amount...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _arpay_oid As Guid = Guid.NewGuid
        Dim ssqls As New ArrayList
        Dim _arpay_code As String = func_coll.get_transaction_number("AY", arpay_en_id.GetColumnValue("en_code"), "arpay_payment", "arpay_code")
        Dim _arpay_total_amount As Double = 0
        Dim i As Integer
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _arpay_total_amount = _arpay_total_amount + ds_edit.Tables(0).Rows(i).Item("arpayd_amount")
        Next

        If arpay_cashi_oid.Text <> "" Then
            If _arpay_total_amount > arpay_cashi_amount.EditValue Then
                MessageBox.Show("Not Valid Amount Cash In amount ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
                Exit Function
            End If
        End If

        _terbilang = Terbilang(_arpay_total_amount)
        _terbilang = _terbilang + "Rupiah"

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
                                            & "  public.arpay_payment " _
                                            & "( " _
                                            & "  arpay_oid, " _
                                            & "  arpay_dom_id, " _
                                            & "  arpay_en_id, " _
                                            & "  arpay_add_by, " _
                                            & "  arpay_add_date, " _
                                            & "  arpay_code, " _
                                            & "  arpay_bill_to, " _
                                            & "  arpay_cu_id, " _
                                            & "  arpay_bk_id, " _
                                            & "  arpay_ar_ac_id, " _
                                            & "  arpay_ar_sb_id, " _
                                            & "  arpay_ar_cc_id, " _
                                            & "  arpay_disc_ac_id, " _
                                            & "  arpay_disc_sb_id, " _
                                            & "  arpay_disc_cc_id, " _
                                            & "  arpay_exp_ac_id, " _
                                            & "  arpay_exp_sb_id, " _
                                            & "  arpay_exp_cc_id, " _
                                            & "  arpay_date, " _
                                            & "  arpay_eff_date, " _
                                            & "  arpay_total_amount,arpay_cashi_oid,arpay_cashi_amount, " _
                                            & "  arpay_tbilang, " _
                                            & "  arpay_remarks, " _
                                            & "  arpay_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_arpay_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(arpay_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_arpay_code) & ",  " _
                                            & SetInteger(arpay_bill_to.EditValue) & ",  " _
                                            & SetInteger(arpay_cu_id.EditValue) & ",  " _
                                            & SetInteger(arpay_bk_id.EditValue) & ",  " _
                                            & SetInteger(arpay_ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(arpay_ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(arpay_ar_cc_id.EditValue) & ",  " _
                                            & SetInteger(arpay_disc_ac_id.EditValue) & ",  " _
                                            & SetInteger(arpay_disc_sb_id.EditValue) & ",  " _
                                            & SetInteger(arpay_disc_cc_id.EditValue) & ",  " _
                                            & SetInteger(arpay_exp_ac_id.EditValue) & ",  " _
                                            & SetInteger(arpay_exp_sb_id.EditValue) & ",  " _
                                            & SetInteger(arpay_exp_cc_id.EditValue) & ",  " _
                                            & SetDate(arpay_date.DateTime) & ",  " _
                                            & SetDate(arpay_eff_date.DateTime) & ",  " _
                                            & SetDbl(_arpay_total_amount) & ",  " _
                                            & SetSetring(arpay_cashi_oid.Tag) & ",  " _
                                            & SetDec(arpay_cashi_amount.EditValue) & ",  " _
                                            & SetSetring(_terbilang.ToString) & ",  " _
                                            & SetSetring(arpay_remarks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If arpay_cashi_oid.Text <> "" Then

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE cashi_in set cashi_amount_used=coalesce(cashi_amount_used,0) +  " & SetDec(_arpay_total_amount) _
                                                    & " , cashi_amount_remains=cashi_amount_remains -  " & SetDec(_arpay_total_amount) _
                                                    & " where cashi_oid='" & arpay_cashi_oid.Tag & "'"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If


                        'Untuk Insert Data Detailnya
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.arpayd_det " _
                                                & "( " _
                                                & "  arpayd_oid, " _
                                                & "  arpayd_arpay_oid, " _
                                                & "  arpayd_sokp_oid, " _
                                                & "  arpayd_ar_oid, " _
                                                & "  arpayd_ar_ref, " _
                                                & "  arpayd_type, " _
                                                & "  arpayd_ac_id, " _
                                                & "  arpayd_sb_id, " _
                                                & "  arpayd_cc_id, " _
                                                & "  arpayd_amount, " _
                                                & "  arpayd_cash_amount, " _
                                                & "  arpayd_disc_amount, " _
                                                & "  arpayd_exp_amount, " _
                                                & "  arpayd_remarks, " _
                                                & "  arpayd_dt, " _
                                                & "  arpayd_cur_amount, " _
                                                & "  arpayd_exc_rate " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpayd_oid").ToString) & ",  " _
                                                & SetSetring(_arpay_oid.ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sokp_oid")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpayd_ar_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpayd_ar_ref")) & ",  " _
                                                & SetSetring("Y") & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("arpayd_ac_id")) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("arpayd_sb_id")) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("arpayd_cc_id")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_cash_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_disc_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_exp_amount")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("arpayd_remarks")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_cur_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_exc_rate")) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table ar_mstr untuk update field ar_pay_amount
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ar_mstr set ar_pay_amount = coalesce(ar_pay_amount,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("arpayd_cur_amount").ToString) + _
                                                   " where ar_oid = '" + ds_edit.Tables(0).Rows(i).Item("arpayd_ar_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table ar_mstr untuk status arabila sudah lunas
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ar_mstr set ar_status = 'c' " + _
                                                   " where ar_pay_amount >= ar_amount and ar_oid='" + ds_edit.Tables(0).Rows(i).Item("arpayd_ar_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table sokp_mstr untuk update field sokp_pay_amount
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update sokp_piutang set sokp_amount_pay = coalesce(sokp_amount_pay,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("arpayd_cur_amount").ToString) + ", " _
                                                    & " sokp_date_payment = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                                                    & " sokp_ar_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpayd_ar_oid").ToString) & " " _
                                                    & " where sokp_oid = '" + ds_edit.Tables(0).Rows(i).Item("sokp_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table sokp_mstr untuk update status sokp_pay_amount
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update sokp_piutang set sokp_status = 'C' " _
                                                    & " where sokp_oid = '" + ds_edit.Tables(0).Rows(i).Item("sokp_oid").ToString + "'" _
                                                    & " and sokp_amount_pay >= sokp_amount "

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Insert Kartu Piutang Untuk Yang Mempunyai Kartu Piutang
                            'If update_insert_kartu_piutang(ssqls, objinsert, ds_edit.Tables(0).Rows(i).Item("arpayd_ar_oid").ToString, _arpay_total_amount) = False Then
                            '    sqlTran.Rollback()
                            '    insert = False
                            '    Exit Function
                            'End If
                            '*******************************************************

                            'update rekonsiliasi
                            If update_rec(objinsert, ssqls, arpay_en_id.EditValue, arpay_bk_id.EditValue, arpay_cu_id.EditValue, _
                                  ds_edit.Tables(0).Rows(i).Item("arpayd_exc_rate"), ds_edit.Tables(0).Rows(i).Item("arpayd_cash_amount"), arpay_date.DateTime, _arpay_code, _
                                  arpay_bill_to.Text & " " & arpay_remarks.Text, "AR PAYMENT SDI") = False Then
                                sqlTran.Rollback()
                                Return False
                                Exit Function
                            End If

                        Next

                        'insert Jurnal 
                        If _create_jurnal = True Then
                            If insert_glt_det_arpay(ssqls, objinsert, ds_edit, _
                                                       arpay_en_id.EditValue, arpay_en_id.GetColumnValue("en_code"), _
                                                       _arpay_oid.ToString, _arpay_code, _
                                                       arpay_eff_date.DateTime, _
                                                       arpay_cu_id.EditValue, _
                                                       "AR", "AR-PAY", _
                                                       arpay_ar_ac_id.EditValue, arpay_ar_sb_id.EditValue, arpay_ar_cc_id.EditValue, _
                                                       arpay_ar_ac_id.GetColumnValue("ac_name"), _
                                                       _arpay_total_amount) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If
                        '************************************************


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert AR Payment SDI " & _arpay_code)
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
                        'print(_arpay_oid.ToString)
                        set_row(_arpay_oid.ToString, "arpay_oid")
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

    Private Function insert_glt_det_arpay(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_type As String, ByVal par_daybook As String, _
                                   ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
                                   ByVal par_cc_id As Integer, _
                                   ByVal par_desc As String, ByVal par_amount As Double) As Boolean

        insert_glt_det_arpay = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _exc_rate As Double = 1
        Dim _nilai_awal, _nilai_akhir, _nilai_hitung As Double
        _nilai_akhir = 0
        'Insert Untuk Yang Debet Dulu, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    _exc_rate = par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")

                    _nilai_awal = par_ds.Tables(0).Rows(i).Item("arpayd_amount") * par_ds.Tables(0).Rows(i).Item("ar_exc_rate")
                    _nilai_hitung = _nilai_awal / par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.glt_det " _
                                        & "( " _
                                        & "  glt_oid, " _
                                        & "  glt_dom_id, " _
                                        & "  glt_en_id, " _
                                        & "  glt_add_by, " _
                                        & "  glt_add_date, " _
                                        & "  glt_code, " _
                                        & "  glt_date, " _
                                        & "  glt_type, " _
                                        & "  glt_cu_id, " _
                                        & "  glt_exc_rate, " _
                                        & "  glt_seq, " _
                                        & "  glt_ac_id, " _
                                        & "  glt_sb_id, " _
                                        & "  glt_cc_id, " _
                                        & "  glt_desc, " _
                                        & "  glt_debit, " _
                                        & "  glt_credit, " _
                                        & "  glt_ref_oid, " _
                                        & "  glt_ref_trans_code, " _
                                        & "  glt_posted, " _
                                        & "  glt_dt, " _
                                        & "  glt_daybook " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_type) & ",  " _
                                        & SetInteger(par_cu_id) & ",  " _
                                        & SetDbl(_exc_rate) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(par_ds.Tables(0).Rows(i).Item("arpayd_ac_id")) & ",  " _
                                        & SetIntegerDB(par_ds.Tables(0).Rows(i).Item("arpayd_sb_id")) & ",  " _
                                        & SetIntegerDB(par_ds.Tables(0).Rows(i).Item("arpayd_cc_id")) & ",  " _
                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("arpayd_remarks")) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(_nilai_hitung) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(par_daybook) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                         par_ds.Tables(0).Rows(i).Item("arpayd_ac_id"), _
                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("arpayd_sb_id")), _
                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("arpayd_cc_id")), _
                                                         par_en_id, par_cu_id, _
                                                         _exc_rate, _nilai_hitung, "C") = False Then

                        Return False
                        Exit Function
                    End If

                    'Insert untuk yang debit 
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.glt_det " _
                                        & "( " _
                                        & "  glt_oid, " _
                                        & "  glt_dom_id, " _
                                        & "  glt_en_id, " _
                                        & "  glt_add_by, " _
                                        & "  glt_add_date, " _
                                        & "  glt_code, " _
                                        & "  glt_date, " _
                                        & "  glt_type, " _
                                        & "  glt_cu_id, " _
                                        & "  glt_exc_rate, " _
                                        & "  glt_seq, " _
                                        & "  glt_ac_id, " _
                                        & "  glt_sb_id, " _
                                        & "  glt_cc_id, " _
                                        & "  glt_desc, " _
                                        & "  glt_debit, " _
                                        & "  glt_credit, " _
                                        & "  glt_ref_oid, " _
                                        & "  glt_ref_trans_code, " _
                                        & "  glt_posted, " _
                                        & "  glt_dt, " _
                                        & "  glt_daybook " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_type) & ",  " _
                                        & SetInteger(par_cu_id) & ",  " _
                                        & SetDbl(_exc_rate) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(arpay_ar_ac_id.EditValue) & ",  " _
                                        & SetIntegerDB(0) & ",  " _
                                        & SetIntegerDB(0) & ",  " _
                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("arpayd_remarks")) & ",  " _
                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("arpayd_cash_amount")) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(par_daybook) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                        arpay_ar_ac_id.EditValue, _
                                                        SetIntegerDB(0), _
                                                        SetIntegerDB(0), _
                                                        par_en_id, par_cu_id, _
                                                        _exc_rate, par_ds.Tables(0).Rows(i).Item("arpayd_cash_amount"), "D") = False Then

                        Return False
                        Exit Function
                    End If

                    'belum jadi prioritas jadi belum dikerjain
                    ''Untuk Real Exchange
                    'If par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate") < par_ds.Tables(0).Rows(i).Item("ar_exc_rate") Then
                    '    'maka ini adalah gain / keuntungan dan ada di sisi credit
                    '    'Insert untuk yang credit 

                    '    dt_bantu = New DataTable
                    '    dt_bantu = (func_data.get_real_account(arpay_cu_id.EditValue, "gain"))

                    '    _nilai_awal = par_ds.Tables(0).Rows(i).Item("arpayd_amount") * par_ds.Tables(0).Rows(i).Item("ar_exc_rate")
                    '    _nilai_akhir = par_ds.Tables(0).Rows(i).Item("arpayd_amount") * par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")
                    '    _nilai_hitung = (_nilai_awal - _nilai_akhir) / par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")
                    '    'nilai awal - nilai akhir agar tidak jadi minus...kan ditaronya di credit jadi sudah menunjukan -

                    '    .Command.CommandType = CommandType.Text
                    '    .Command.CommandText = "INSERT INTO  " _
                    '                        & "  public.glt_det " _
                    '                        & "( " _
                    '                        & "  glt_oid, " _
                    '                        & "  glt_dom_id, " _
                    '                        & "  glt_en_id, " _
                    '                        & "  glt_add_by, " _
                    '                        & "  glt_add_date, " _
                    '                        & "  glt_code, " _
                    '                        & "  glt_date, " _
                    '                        & "  glt_type, " _
                    '                        & "  glt_cu_id, " _
                    '                        & "  glt_exc_rate, " _
                    '                        & "  glt_seq, " _
                    '                        & "  glt_ac_id, " _
                    '                        & "  glt_sb_id, " _
                    '                        & "  glt_cc_id, " _
                    '                        & "  glt_desc, " _
                    '                        & "  glt_debit, " _
                    '                        & "  glt_credit, " _
                    '                        & "  glt_ref_oid, " _
                    '                        & "  glt_ref_trans_code, " _
                    '                        & "  glt_posted, " _
                    '                        & "  glt_dt, " _
                    '                        & "  glt_daybook " _
                    '                        & ")  " _
                    '                        & "VALUES ( " _
                    '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    '                        & SetInteger(par_en_id) & ",  " _
                    '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    '                        & SetSetring(_glt_code) & ",  " _
                    '                        & setdate(par_date) & ",  " _
                    '                        & SetSetring(par_type) & ",  " _
                    '                        & SetInteger(par_cu_id) & ",  " _
                    '                        & SetDbl(_exc_rate) & ",  " _
                    '                        & SetInteger(i) & ",  " _
                    '                        & SetInteger(dt_bantu.Rows(0).Item("ac_id")) & ",  " _
                    '                        & SetIntegerDB(0) & ",  " _
                    '                        & SetIntegerDB(0) & ",  " _
                    '                        & SetSetringDB(dt_bantu.Rows(0).Item("ac_name")) & ",  " _
                    '                        & SetDblDB(0) & ",  " _
                    '                        & SetDblDB(_nilai_hitung) & ",  " _
                    '                        & SetSetring(par_oid) & ",  " _
                    '                        & SetSetring(par_trans_code) & ",  " _
                    '                        & SetSetring("N") & ",  " _
                    '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    '                        & SetSetring(par_daybook) & "  " _
                    '                        & ")"
                    '    par_ssqls.Add(.Command.CommandText)
                    '    .Command.ExecuteNonQuery()
                    '    .Command.Parameters.Clear()

                    '    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                    '                                        dt_bantu.Rows(0).Item("ac_id"), _
                    '                                        SetIntegerDB(0), _
                    '                                        SetIntegerDB(0), _
                    '                                        par_en_id, par_cu_id, _
                    '                                        _exc_rate, _nilai_hitung, "C") = False Then

                    '        Return False
                    '        Exit Function
                    '    End If
                    'ElseIf par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate") > par_ds.Tables(0).Rows(i).Item("ar_exc_rate") Then
                    '    'maka ini adalah loss / rugi dan ada di sisi debet
                    '    'Insert untuk yang debet

                    '    dt_bantu = New DataTable
                    '    dt_bantu = (func_data.get_real_account(appay_cu_id.EditValue, "loss"))

                    '    _nilai_awal = par_ds.Tables(0).Rows(i).Item("arpayd_amount") * par_ds.Tables(0).Rows(i).Item("ar_exc_rate")
                    '    _nilai_akhir = par_ds.Tables(0).Rows(i).Item("arpayd_amount") * par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")
                    '    _nilai_hitung = (_nilai_akhir - _nilai_awal) / par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")
                    '    'terbalik dengan yang diatas...agar tetep jadi + jg

                    '    .Command.CommandType = CommandType.Text
                    '    .Command.CommandText = "INSERT INTO  " _
                    '                        & "  public.glt_det " _
                    '                        & "( " _
                    '                        & "  glt_oid, " _
                    '                        & "  glt_dom_id, " _
                    '                        & "  glt_en_id, " _
                    '                        & "  glt_add_by, " _
                    '                        & "  glt_add_date, " _
                    '                        & "  glt_code, " _
                    '                        & "  glt_date, " _
                    '                        & "  glt_type, " _
                    '                        & "  glt_cu_id, " _
                    '                        & "  glt_exc_rate, " _
                    '                        & "  glt_seq, " _
                    '                        & "  glt_ac_id, " _
                    '                        & "  glt_sb_id, " _
                    '                        & "  glt_cc_id, " _
                    '                        & "  glt_desc, " _
                    '                        & "  glt_debit, " _
                    '                        & "  glt_credit, " _
                    '                        & "  glt_ref_oid, " _
                    '                        & "  glt_ref_trans_code, " _
                    '                        & "  glt_posted, " _
                    '                        & "  glt_dt, " _
                    '                        & "  glt_daybook " _
                    '                        & ")  " _
                    '                        & "VALUES ( " _
                    '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    '                        & SetInteger(par_en_id) & ",  " _
                    '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    '                        & SetSetring(_glt_code) & ",  " _
                    '                        & SetDate(par_date) & ",  " _
                    '                        & SetSetring(par_type) & ",  " _
                    '                        & SetInteger(par_cu_id) & ",  " _
                    '                        & SetDbl(_exc_rate) & ",  " _
                    '                        & SetInteger(i) & ",  " _
                    '                        & SetInteger(dt_bantu.Rows(0).Item("ac_id")) & ",  " _
                    '                        & SetIntegerDB(0) & ",  " _
                    '                        & SetIntegerDB(0) & ",  " _
                    '                        & SetSetringDB(dt_bantu.Rows(0).Item("ac_name")) & ",  " _
                    '                        & SetDblDB(_nilai_hitung) & ",  " _
                    '                        & SetDblDB(0) & ",  " _
                    '                        & SetSetring(par_oid) & ",  " _
                    '                        & SetSetring(par_trans_code) & ",  " _
                    '                        & SetSetring("N") & ",  " _
                    '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    '                        & SetSetring(par_daybook) & "  " _
                    '                        & ")"
                    '    par_ssqls.Add(.Command.CommandText)
                    '    .Command.ExecuteNonQuery()
                    '    .Command.Parameters.Clear()

                    '    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                    '                                        dt_bantu.Rows(0).Item("ac_id"), _
                    '                                        SetIntegerDB(0), _
                    '                                        SetIntegerDB(0), _
                    '                                        par_en_id, par_cu_id, _
                    '                                        _exc_rate, _nilai_hitung, "D") = False Then

                    '        Return False
                    '        Exit Function
                    '    End If
                    'End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

        'Insert Untuk Account Discountnya.....kalau > 0 berarti debit kalau < 0 berarti credit
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    _exc_rate = par_ds.Tables(0).Rows(i).Item("arpayd_exc_rate")

                    If par_ds.Tables(0).Rows(i).Item("arpayd_disc_amount") < 0 Then
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(_exc_rate) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(arpay_disc_ac_id.EditValue) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("arpayd_remarks")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("arpayd_disc_amount") * -1) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             arpay_disc_ac_id.EditValue, _
                                                             SetIntegerDB(0), _
                                                             SetIntegerDB(0), _
                                                             par_en_id, par_cu_id, _
                                                             _exc_rate, par_ds.Tables(0).Rows(i).Item("arpayd_disc_amount") * -1, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    ElseIf par_ds.Tables(0).Rows(i).Item("arpayd_disc_amount") > 0 Then
                        'Insert untuk yang credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(_exc_rate) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(arpay_disc_ac_id.EditValue) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("arpayd_remarks")) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("arpayd_disc_amount")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             arpay_disc_ac_id.EditValue, _
                                                             SetIntegerDB(0), _
                                                             SetIntegerDB(0), _
                                                             par_en_id, par_cu_id, _
                                                             _exc_rate, par_ds.Tables(0).Rows(i).Item("arpayd_disc_amount"), "D") = False Then

                            Return False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                If par_ds.Tables(0).Rows(i).Item("arpayd_exp_amount") > 0 Then
                    'Insert untuk yang Expense Account 
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.glt_det " _
                                        & "( " _
                                        & "  glt_oid, " _
                                        & "  glt_dom_id, " _
                                        & "  glt_en_id, " _
                                        & "  glt_add_by, " _
                                        & "  glt_add_date, " _
                                        & "  glt_code, " _
                                        & "  glt_date, " _
                                        & "  glt_type, " _
                                        & "  glt_cu_id, " _
                                        & "  glt_exc_rate, " _
                                        & "  glt_seq, " _
                                        & "  glt_ac_id, " _
                                        & "  glt_sb_id, " _
                                        & "  glt_cc_id, " _
                                        & "  glt_desc, " _
                                        & "  glt_debit, " _
                                        & "  glt_credit, " _
                                        & "  glt_ref_oid, " _
                                        & "  glt_ref_trans_code, " _
                                        & "  glt_posted, " _
                                        & "  glt_dt, " _
                                        & "  glt_daybook " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_type) & ",  " _
                                        & SetInteger(par_cu_id) & ",  " _
                                        & SetDbl(_exc_rate) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(arpay_exp_ac_id.EditValue) & ",  " _
                                        & SetIntegerDB(0) & ",  " _
                                        & SetIntegerDB(0) & ",  " _
                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("arpayd_remarks")) & ",  " _
                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("arpayd_exp_amount")) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(par_daybook) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                         arpay_exp_ac_id.EditValue, _
                                                         SetIntegerDB(0), _
                                                         SetIntegerDB(0), _
                                                         par_en_id, par_cu_id, _
                                                         _exc_rate, par_ds.Tables(0).Rows(i).Item("arpayd_exp_amount"), "D") = False Then

                        Return False
                        Exit Function
                    End If
                End If
            End With
        Next
    End Function

    Private Function update_insert_kartu_piutang(ByVal par_ssql As ArrayList, ByVal par_obj As Object, ByVal par_ar_oid As String, ByVal par_amount As Double) As Boolean
        update_insert_kartu_piutang = True

        Dim ds_bantu As New DataSet()
        Dim i As Integer

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select sokp_oid,  " _
                        & " sokp_so_oid, " _
                        & " sokp_seq, " _
                        & " sokp_amount, " _
                        & " sokp_amount_pay, " _
                        & " sokp_amount - sokp_amount_pay as sokp_amount_outstanding, " _
                        & " sokp_description " _
                        & " from arso_so " _
                        & " inner join sokp_piutang on sokp_so_oid = arso_so_oid " _
                        & " where sokp_amount_pay < sokp_amount " _
                        & " and arso_ar_oid = '" + par_ar_oid + "'" _
                        & " order by sokp_seq"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sokp")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text

                    If par_amount > ds_bantu.Tables(0).Rows(i).Item("sokp_amount_outstanding") Then
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.sokp_piutang   " _
                                            & "  SET  " _
                                            & "  sokp_amount_pay = sokp_amount_pay + " & ds_bantu.Tables(0).Rows(i).Item("sokp_amount_outstanding").ToString & ",  " _
                                            & "  sokp_date_payment = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " _
                                            & "  " _
                                            & "  WHERE  " _
                                            & "  sokp_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("sokp_oid").ToString) & " "
                    Else
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.sokp_piutang   " _
                                            & "  SET  " _
                                            & "  sokp_amount_pay = " & par_amount.ToString & ",  " _
                                            & "  sokp_date_payment = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " _
                                            & "  " _
                                            & "  WHERE  " _
                                            & "  sokp_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("sokp_oid").ToString) & " "
                    End If
                    par_ssql.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If par_amount > ds_bantu.Tables(0).Rows(i).Item("sokp_amount_outstanding") Then
                        par_amount = par_amount - ds_bantu.Tables(0).Rows(i).Item("sokp_amount_outstanding")
                    Else
                        Exit For
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

        Return update_insert_kartu_piutang
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _arpay_eff_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_eff_date")
        Dim _arpay_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_arpay_en_id, "gcald_ar", _arpay_eff_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _arpay_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _arpay_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
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

        If MessageBox.Show(String.Format("Yakin {0} Hapus Data Ini..?", master_new.ClsVar.sNama), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim i As Integer
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

                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("arpayd_arpay_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_oid").ToString Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update ar_mstr set ar_pay_amount = ar_pay_amount - " & SetDec(ds.Tables("detail").Rows(i).Item("arpayd_cur_amount")) _
                                        & ", ar_status = null where ar_oid = '" & ds.Tables("detail").Rows(i).Item("arpayd_ar_oid").ToString & "'"

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'Update Delete Kartu Piutang Untuk Yang Mempunyai Kartu Piutang 

                                    'Update Table sokp_mstr untuk update field sokp_pay_amount
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update sokp_piutang set sokp_amount_pay = coalesce(sokp_amount_pay,0) - " + SetDec(ds.Tables("detail").Rows(i).Item("arpayd_cur_amount").ToString) + ", " _
                                                            & " sokp_date_payment = null, " _
                                                            & " sokp_status = null " _
                                                            & " where sokp_oid = '" + ds.Tables("detail").Rows(i).Item("arpayd_sokp_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()


                                    '.Command.CommandType = CommandType.Text
                                    '.Command.CommandText = "update sokp_piutang set sokp_amount_pay = coalesce(sokp_amount_pay,0) - " + SetDec(ds.Tables("detail").Rows(i).Item("arpayd_cur_amount").ToString) + ", " _
                                    '                        & " sokp_date_payment = (select arpay_date from arpayd_det INNER JOIN arpay_payment on arpayd_arpay_oid = arpay_oid where arpayd_sokp_oid = '" + ds.Tables("detail").Rows(i).Item("arpayd_sokp_oid") + "' ORDER BY arpay_date desc limit 1), " _
                                    '                        & " sokp_status = null " _
                                    '                        & " where sokp_oid = '" + ds.Tables("detail").Rows(i).Item("arpayd_sokp_oid") + "'"
                                    'ssqls.Add(.Command.CommandText)
                                    '.Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    ''Update Table sokp_mstr untuk update status sokp_pay_amount
                                    '.Command.CommandType = CommandType.Text
                                    '.Command.CommandText = "update sokp_piutang set sokp_ar_oid = null , " _
                                    '                        & " sokp_date_payment = null " _
                                    '                        & " where sokp_oid = '" + ds.Tables("detail").Rows(i).Item("arpayd_sokp_oid") + "'" _
                                    '                        & " and sokp_amount_pay = 0 "

                                    'ssqls.Add(.Command.CommandText)
                                    '.Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    'update rekonsiliasi
                                    If update_rec(objinsert, ssqls, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_en_id"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_bk_id"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_cu_id"), _
                                         ds.Tables("detail").Rows(i).Item("arpayd_exc_rate"), ds.Tables("detail").Rows(i).Item("arpayd_cash_amount") * -1, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_date"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_code"), _
                                         "Delete " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_name") & " " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_remarks"), "AR PAYMENT SDI") = False Then
                                        sqlTran.Rollback()
                                        Return False
                                        Exit Function
                                    End If


                                    'If update_delete_kartu_piutang(ssqls, objinsert, ds.Tables("detail").Rows(i).Item("arpayd_ar_oid").ToString, ds.Tables("detail").Rows(i).Item("arpayd_amount")) = False Then
                                    '    sqlTran.Rollback()
                                    '    delete_data = False
                                    '    Exit Function
                                    'End If
                                    '*******************************************************
                                End If
                            Next

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from arpay_payment where arpay_oid ='" _
                                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_oid").ToString & "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_cashi_oid")) <> "" Then

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE cashi_in set cashi_amount_used=coalesce(cashi_amount_used,0) -  " & SetDec(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_total_amount")) _
                                      & " , cashi_amount_remains=cashi_amount_remains +  " & SetDec(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_total_amount")) _
                                      & " where cashi_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_cashi_oid").ToString & "'"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If


                            If delete_glt_det_ar_payment(ssqls, objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_oid").ToString, _
                                                          ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_code")) = False Then
                                sqlTran.Rollback()
                                Exit Function
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete AR Payment SDI " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_code"))
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
                            MessageBox.Show("Data Telah Berhasil Di Harus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Function delete_glt_det_ar_payment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_arpay_oid As String, ByVal par_arpay_code As String) As Boolean
        delete_glt_det_ar_payment = True
        Dim _glt_code As String = ""
        Dim _glt_posted As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select glt_code, glt_posted from glt_det " + _
                                           " where glt_ref_oid = '" + par_arpay_oid + "'" + _
                                           " and glt_ref_trans_code = '" + par_arpay_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _glt_code = .DataReader("glt_code")
                        _glt_posted = .DataReader("glt_posted")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _glt_posted.ToUpper = "N" Then
            If func_coll.delete_glt_det(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If
        Else
            If insert_glt_det_ar_payment_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If
        End If
    End Function

    Private Function insert_glt_det_ar_payment_jurnal_balik(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_glt_code As String) As Boolean
        'ini prosedur untuk jurnal balik jurnal ar payment karena didelete tetapi sudah di posting...
        insert_glt_det_ar_payment_jurnal_balik = True
        Dim i As Integer
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT glt_oid, glt_dom_id, glt_en_id, glt_gl_oid, glt_code, glt_date, " _
                         & " glt_type, glt_cu_id, glt_exc_rate, glt_seq, glt_ac_id, glt_sb_id, glt_cc_id, " _
                         & " glt_desc, glt_debit, glt_credit, glt_ref_tran_id, glt_ref_trans_code, glt_posted, " _
                         & " glt_daybook, glt_ref_oid, en_code " _
                         & " FROM  " _
                         & " public.glt_det " _
                         & " inner join en_mstr on en_id = glt_en_id" _
                         & " where glt_code = '" + par_glt_code + "' order by glt_seq"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "glt_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        Dim _en_code As String = ds_bantu.Tables(0).Rows(0).Item("en_code")
        Dim _glt_code As String = func_coll.get_transaction_number("AY", _en_code, "glt_det", "glt_code")

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If ds_bantu.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
                        'debet dibalik jadi credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_is_reverse, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring("AP") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_debit")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
                                            & ")"
                        'debet di set 0 dan credit diset nilainya dari debet agar menjadi jurnal balik
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, master_new.PGSqlConn.CekTanggal, _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_debit"), _
                                                         "C") = False Then
                            Return False
                            Exit Function
                        End If
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
                        'debet dibalik jadi credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_is_reverse, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring("AP") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                            & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_credit")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
                                            & ")"
                        'credit di set 0 dan debet diset nilainya dari credit agar menjadi jurnal balik
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, master_new.PGSqlConn.CekTanggal, _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_credit"), _
                                                         "D") = False Then
                            Return False
                            Exit Function
                        End If
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Private Function update_delete_kartu_piutang(ByVal par_ssql As ArrayList, ByVal par_obj As Object, ByVal par_ar_oid As String, ByVal par_amount As Double) As Boolean
        update_delete_kartu_piutang = True

        Dim ds_bantu As New DataSet()
        Dim i As Integer

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select sokp_oid,  " _
                        & " sokp_so_oid, " _
                        & " sokp_seq, " _
                        & " sokp_amount, " _
                        & " sokp_amount_pay, " _
                        & " sokp_description " _
                        & " from arso_so " _
                        & " inner join sokp_piutang on sokp_so_oid = arso_so_oid " _
                        & " where sokp_amount_pay > 0 " _
                        & " and arso_ar_oid = '" + par_ar_oid + "'" _
                        & " order by sokp_seq, sokp_seq desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sokp")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text

                    If par_amount > ds_bantu.Tables(0).Rows(i).Item("sokp_amount_pay") Then
                        .Command.CommandText = "UPDATE " _
                                            & "  public.sokp_piutang " _
                                            & "  SET  " _
                                            & "  sokp_amount_pay = 0 " _
                                            & "  " _
                                            & "  WHERE  " _
                                            & "  sokp_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("sokp_oid").ToString) & " "
                    Else
                        .Command.CommandText = "UPDATE " _
                                            & "  public.sokp_piutang " _
                                            & "  SET  " _
                                            & "  sokp_amount_pay = sokp_amount_pay - " & par_amount.ToString & "  " _
                                            & "  " _
                                            & "  WHERE  " _
                                            & "  sokp_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("sokp_oid").ToString) & " "
                    End If
                    par_ssql.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If par_amount > ds_bantu.Tables(0).Rows(i).Item("sokp_amount_pay") Then
                        par_amount = par_amount - ds_bantu.Tables(0).Rows(i).Item("sokp_amount_pay")
                    Else
                        Exit For
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

        Return update_delete_kartu_piutang
    End Function

    Public Overrides Sub preview()
        print(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_oid").ToString)
    End Sub

    Public Sub print(ByVal _arpar_oid As String)
        Dim ds_print As New DataSet()
        Dim _sql As String
        
        _sql = "SELECT " _
                    & " arpay_payment.arpay_code, " _
                    & " arpay_payment.arpay_bill_to, " _
                    & " arpay_payment.arpay_date, " _
                    & " arpay_payment.arpay_total_amount,  " _
                    & " arpay_payment.arpay_tbilang, " _
                    & " arpay_payment.arpay_remarks, " _
                    & " arpayd_det.arpayd_amount, " _
                    & " arpayd_det.arpayd_cash_amount, " _
                    & " arpayd_det.arpayd_disc_amount,  " _
                    & " arpayd_det.arpayd_sokp_oid, " _
                    & " sokp_piutang.sokp_seq, " _
                    & " sokp_piutang.sokp_amount_pay, " _
                    & " sokp_piutang.sokp_date_payment, " _
                    & " ptnr_mstr.ptnr_name,  " _
                    & " ptnra_addr.ptnra_line, " _
                    & " ptnra_addr.ptnra_line_1, " _
                    & " ptnra_addr.ptnra_line_2, " _
                    & " ptnra_addr.ptnra_line_3 " _
                    & "FROM  " _
                    & " arpay_payment " _
                    & " INNER JOIN ptnr_mstr ON arpay_payment.arpay_bill_to = ptnr_mstr.ptnr_id  " _
                    & " INNER JOIN arpayd_det ON arpay_payment.arpay_oid = arpayd_det.arpayd_arpay_oid " _
                    & " INNER JOIN sokp_piutang ON arpayd_det.arpayd_sokp_oid = sokp_piutang.sokp_oid " _
                    & " INNER JOIN ptnra_addr ON ptnr_mstr.ptnr_oid = ptnra_addr.ptnra_ptnr_oid" _
                    & " WHERE arpay_oid = " & SetSetring(_arpar_oid.ToString) & " " _
                    & " order by arpay_code, sokp_seq"



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRKwitansiAR"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arpay_code")
        frm.ShowDialog()


    End Sub

 
    Public Function Terbilang(ByVal x As Integer) As String

        Dim bilangan As String() = {"", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas"}

        Dim temp As String = ""



        If x < 12 Then

            temp = " " + bilangan(x)

        ElseIf x < 20 Then

            temp = Terbilang(Int(x - 10)).ToString + " Belas"

        ElseIf x < 100 Then

            temp = Terbilang(Int(x / 10)) + " Puluh" + Terbilang(x Mod 10)

        ElseIf x < 200 Then

            temp = " Seratus" + Terbilang(Int(x - 100))

        ElseIf x < 1000 Then

            temp = Terbilang(Int(x / 100)) + " Ratus" + Terbilang(x Mod 100)

        ElseIf x < 2000 Then

            temp = " Seribu" + Terbilang(Int(x - 1000))

        ElseIf x < 1000000 Then

            temp = Terbilang(Int(x / 1000)) + " Ribu" + Terbilang(x Mod 1000)

        ElseIf x < 1000000000 Then

            temp = Terbilang(Int(x / 1000000)) + " Juta" + Terbilang(x Mod 1000000)

        End If

        Return temp

    End Function

    Private Sub arpay_cashi_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles arpay_cashi_oid.ButtonClick
        Try
            Dim frm As New FCashInSearch
            frm.set_win(Me)

            frm._ptnr_id = arpay_bill_to.EditValue
            frm._obj = arpay_cashi_oid
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
