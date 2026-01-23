Imports npgsql
Imports master_new.ModFunction

Public Class FPaymentManualChecks
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet

#Region "Setting Awal"

    Private Sub FPaymentManualChecks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        appay_en_id.Properties.DataSource = dt_bantu
        appay_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        appay_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        appay_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        appay_cu_id.Properties.DataSource = dt_bantu
        appay_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        appay_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        appay_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        appay_ap_ac_id.Properties.DataSource = dt_bantu
        appay_ap_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        appay_ap_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        appay_ap_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        appay_disc_ac_id.Properties.DataSource = dt_bantu
        appay_disc_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        appay_disc_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        appay_disc_ac_id.ItemIndex = 0


    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        'dt_bantu = (func_data.load_supplier(appay_en_id.EditValue))
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", appay_en_id.EditValue))

        appay_supplier.Properties.DataSource = dt_bantu
        appay_supplier.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        appay_supplier.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        appay_supplier.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_bk_mstr(appay_en_id.EditValue))
        'appay_bk_id.Properties.DataSource = dt_bantu
        'appay_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_code").ToString
        'appay_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        'appay_bk_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(appay_en_id.EditValue))
        appay_ap_sb_id.Properties.DataSource = dt_bantu
        appay_ap_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        appay_ap_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        appay_ap_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(appay_en_id.EditValue))
        appay_ap_cc_id.Properties.DataSource = dt_bantu
        appay_ap_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        appay_ap_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        appay_ap_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(appay_en_id.EditValue))
        appay_disc_sb_id.Properties.DataSource = dt_bantu
        appay_disc_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        appay_disc_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        appay_disc_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(appay_en_id.EditValue))
        appay_disc_cc_id.Properties.DataSource = dt_bantu
        appay_disc_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        appay_disc_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        appay_disc_cc_id.ItemIndex = 0

        Dim _filter As String

        _filter = "and bk_id in (SELECT  " _
            & "  bk_id " _
            & "FROM  " _
            & "  public.bk_mstr  " _
            & "WHERE  " _
            & "bk_ac_id in (SELECT  " _
            & "  enacc_ac_id " _
            & "FROM  " _
            & "  public.enacc_mstr  " _
            & "Where enacc_en_id=" & SetInteger(appay_en_id.EditValue) & " and enacc_code='payment_ap_bank'))"

        dt_bantu = New DataTable
        If limit_account(appay_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_bk_mstr(appay_en_id.EditValue, _filter, True))
        Else
            dt_bantu = (func_data.load_bk_mstr(appay_en_id.EditValue))
        End If


        appay_bk_id.Properties.DataSource = dt_bantu
        appay_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        appay_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        appay_bk_id.ItemIndex = 0

    End Sub

    Private Sub appay_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles appay_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub appay_bk_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles appay_bk_id.EditValueChanged
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bk_cu_id, bk_ac_id, bk_sb_id, bk_cc_id from bk_mstr where bk_id = " + appay_bk_id.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        appay_cu_id.EditValue = .DataReader.Item("bk_cu_id")
                        appay_ap_ac_id.EditValue = .DataReader.Item("bk_ac_id")
                        appay_ap_sb_id.EditValue = .DataReader.Item("bk_sb_id")
                        appay_ap_cc_id.EditValue = .DataReader.Item("bk_cc_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub appay_date_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles appay_date.EditValueChanged
        appay_eff_date.DateTime = appay_date.DateTime
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Number", "appay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier ID", "appay_supplier", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Date", "appay_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Payment Eff. Date", "appay_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account Code", "ac_ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account", "ac_ap_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_ap_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_ap_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Acct Code", "ac_disc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Acct", "ac_disc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Sub Acct", "sb_disc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Discount Cost Center", "cc_disc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total Amount", "appay_total_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "appay_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "appay_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "appay_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "appay_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "appay_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "appayd_appay_oid", False)
        add_column_copy(gv_detail, "Reference Voucher", "appayd_ap_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Type", "appayd_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Exchange Rate", "appayd_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Amount", "appayd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Cash Amount", "appayd_cash_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Disc. Amount", "appayd_disc_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Currency Amount", "appayd_cur_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Remarks", "appayd_remarks", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "appayd_oid", False)
        add_column(gv_edit, "appayd_ap_oid", False)
        add_column(gv_edit, "Reference Voucher", "appayd_ap_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "appayd_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "appayd_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "appayd_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Exchange Rate AP", "ap_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Exchange Rate Today", "appayd_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "ap_cu_id", False)
        add_column_edit(gv_edit, "Amount", "appayd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_edit(gv_edit, "Cash Amount", "appayd_cash_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_edit, "Disc. Amount", "appayd_disc_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_edit(gv_edit, "Currency Amount", "appayd_cur_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_edit(gv_edit, "Remarks", "appayd_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.appay_payment.appay_oid, " _
                & "  public.appay_payment.appay_dom_id, " _
                & "  public.appay_payment.appay_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.appay_payment.appay_add_by, " _
                & "  public.appay_payment.appay_add_date, " _
                & "  public.appay_payment.appay_upd_by, " _
                & "  public.appay_payment.appay_upd_date, " _
                & "  public.appay_payment.appay_code, " _
                & "  public.appay_payment.appay_supplier, " _
                & "  public.appay_payment.appay_cu_id, " _
                & "  public.appay_payment.appay_bk_id, " _
                & "  public.appay_payment.appay_ap_ac_id, " _
                & "  public.appay_payment.appay_ap_sb_id, " _
                & "  public.appay_payment.appay_ap_cc_id, " _
                & "  public.appay_payment.appay_disc_ac_id, " _
                & "  public.appay_payment.appay_disc_sb_id, " _
                & "  public.appay_payment.appay_disc_cc_id, " _
                & "  public.appay_payment.appay_date, " _
                & "  public.appay_payment.appay_eff_date, " _
                & "  public.appay_payment.appay_total_amount, " _
                & "  public.appay_payment.appay_remarks, " _
                & "  public.appay_payment.appay_dt, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.bk_mstr.bk_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  ac_ap_mstr.ac_code as ac_ap_code, " _
                & "  ac_ap_mstr.ac_name as ac_ap_name, " _
                & "  sb_ap_mstr.sb_desc as sb_ap_desc, " _
                & "  cc_ap_mstr.cc_desc as cc_ap_desc, " _
                & "  ac_disc_mstr.ac_code as ac_disc_code, " _
                & "  ac_disc_mstr.ac_name as ac_disc_name, " _
                & "  sb_disc_mstr.sb_desc as sb_disc_desc, " _
                & "  cc_disc_mstr.cc_desc as cc_disc_desc " _
                & "FROM " _
                & "  public.appay_payment " _
                & "  INNER JOIN public.en_mstr ON (public.appay_payment.appay_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.appay_payment.appay_supplier = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.appay_payment.appay_bk_id = public.bk_mstr.bk_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.appay_payment.appay_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ac_ap_mstr ON (public.appay_payment.appay_ap_ac_id = ac_ap_mstr.ac_id)" _
                & "  INNER JOIN public.sb_mstr sb_ap_mstr ON (public.appay_payment.appay_ap_sb_id = sb_ap_mstr.sb_id)" _
                & "  INNER JOIN public.cc_mstr cc_ap_mstr ON (public.appay_payment.appay_ap_cc_id = cc_ap_mstr.cc_id)" _
                & "  INNER JOIN public.ac_mstr ac_disc_mstr ON (public.appay_payment.appay_disc_ac_id = ac_disc_mstr.ac_id)" _
                & "  INNER JOIN public.sb_mstr sb_disc_mstr ON (public.appay_payment.appay_disc_sb_id = sb_disc_mstr.sb_id)" _
                & "  INNER JOIN public.cc_mstr cc_disc_mstr ON (public.appay_payment.appay_disc_cc_id = cc_disc_mstr.cc_id)" _
                & " where appay_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and appay_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and appay_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                       & IIf(SetString(par_ap.EditValue) <> "", " and appay_oid in (select appay_oid from appay_payment x inner join appayd_det y on (y.appayd_appay_oid=x.appay_oid) inner join ap_mstr z on (z.ap_oid=y.appayd_ap_oid) where z.ap_code=" & SetSetring(par_ap.EditValue) & ")", "")

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
            & "  appayd_oid, " _
            & "  appayd_appay_oid, " _
            & "  appayd_ap_oid, " _
            & "  appayd_ap_ref, " _
            & "  appayd_type, " _
            & "  appayd_ac_id, " _
            & "  ac_code,  " _
            & "  ac_name, " _
            & "  appayd_sb_id, " _
            & "  sb_desc, " _
            & "  appayd_cc_id, " _
            & "  cc_desc, " _
            & "  appayd_amount, ap_invoice, " _
            & "  appayd_cash_amount, " _
            & "  appayd_disc_amount, " _
            & "  appayd_remarks, " _
            & "  appayd_dt, " _
            & "  appayd_cur_amount, ap_exc_rate, appayd_exc_rate " _
            & "FROM  " _
            & "  public.appayd_det  " _
            & "  inner join public.appay_payment on appay_oid = appayd_appay_oid " _
            & "  inner join public.ap_mstr on ap_oid = appayd_ap_oid " _
            & "  inner join public.ac_mstr on ac_id = appayd_ac_id " _
            & "  inner join public.sb_mstr on sb_id = appayd_sb_id " _
            & "  inner join public.cc_mstr on cc_id = appayd_cc_id" _
            & "  where appay_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and appay_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("appayd_appay_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("appayd_appay_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("appayd_appay_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Public Overrides Sub insert_data_awal()
        appay_en_id.ItemIndex = 0
        appay_bk_id.ItemIndex = 0
        appay_date.DateTime = Now
        appay_eff_date.DateTime = Now
        'appay_eff_date.Enabled = False
        appay_cu_id.Enabled = False
        appay_ap_ac_id.ItemIndex = 0
        appay_disc_ac_id.ItemIndex = 0
        appay_ap_ac_id.Enabled = False
        appay_ap_sb_id.Enabled = False
        appay_ap_cc_id.Enabled = False
        appay_remarks.Text = ""
        appay_bk_id.ItemIndex = 0
        appay_en_id.Focus()

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
                        & "  appayd_oid, " _
                        & "  appayd_appay_oid, " _
                        & "  appayd_ap_oid, " _
                        & "  appayd_ap_ref, " _
                        & "  appayd_type, " _
                        & "  appayd_ac_id, " _
                        & "  ac_code,  " _
                        & "  ac_name, " _
                        & "  appayd_sb_id, " _
                        & "  sb_desc, " _
                        & "  appayd_cc_id, " _
                        & "  cc_desc, " _
                        & "  appayd_amount,  appayd_exc_rate, " _
                        & "  appayd_cash_amount, " _
                        & "  appayd_disc_amount, " _
                        & "  appayd_remarks, " _
                        & "  appayd_dt, " _
                        & "  ap_cu_id, " _
                        & "  appayd_cur_amount, ap_exc_rate " _
                        & "FROM  " _
                        & "  public.appayd_det  " _
                        & "  inner join public.appay_payment on appay_oid = appayd_appay_oid " _
                        & "  inner join public.ap_mstr on ap_oid = appayd_ap_oid " _
                        & "  inner join public.ac_mstr on ac_id = appayd_ac_id " _
                        & "  inner join public.sb_mstr on sb_id = appayd_sb_id " _
                        & "  inner join public.cc_mstr on cc_id = appayd_cc_id" _
                        & " where appayd_ac_id = -99"
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
        Dim _ap_cu_id As Integer
        Dim _appayd_amount As Double
        'Dim _appayd_cur_amount As Double
        Try
            _ap_cu_id = gv_edit.GetRowCellValue(e.RowHandle, "ap_cu_id")
        Catch ex As Exception
            Exit Sub
        End Try

        If e.Column.Name = "appayd_amount" Then
            gv_edit.SetRowCellValue(e.RowHandle, "appayd_cash_amount", e.Value)
            'by sys 20110419 diberikan komentar
            '_ap_cu_id = gv_edit.GetRowCellValue(e.RowHandle, "ap_cu_id")
            'If _ap_cu_id = appay_cu_id.EditValue Then
            '    'tidak ada yang terjadi...karena kan sama currency nya....tapi ini harus tetep ada disini codingannya
            'ElseIf _ap_cu_id <> master_new.ClsVar.ibase_cur_id Then
            '    gv_edit.SetRowCellValue(e.RowHandle, "appayd_cur_amount", e.Value / func_data.get_exchange_rate(_ap_cu_id))
            'Else
            gv_edit.SetRowCellValue(e.RowHandle, "appayd_cur_amount", e.Value)
            'End If
        ElseIf e.Column.Name = "appayd_cash_amount" Then
            _appayd_amount = gv_edit.GetRowCellValue(e.RowHandle, "appayd_amount")
            gv_edit.SetRowCellValue(e.RowHandle, "appayd_disc_amount", _appayd_amount - e.Value)
        ElseIf e.Column.Name = "appayd_exc_rate" Then
            'by sys 20110419 diberikan komentar
            'If _ap_cu_id <> master_new.ClsVar.ibase_cur_id And appay_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            'ElseIf _ap_cu_id <> appay_cu_id.EditValue Then
            '    _appayd_cur_amount = gv_edit.GetRowCellValue(e.RowHandle, "appayd_cur_amount")
            '    gv_edit.SetRowCellValue(e.RowHandle, "appayd_amount", e.Value * _appayd_cur_amount)
            '    gv_edit.SetRowCellValue(e.RowHandle, "appayd_cash_amount", e.Value * _appayd_cur_amount)
            'End If
            'ini tidak dipake walaupun bayar dengan idr tapi amount dan cash amount tetep harus pake nilai asli di vouchernya (tidak dikali kurs)
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

        If _col = "appayd_ap_ref" Then
            Dim frm As New FVoucherSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = appay_en_id.EditValue
            frm._ptnr_id = appay_supplier.EditValue
            frm._cu_id = appay_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "appayd_oid", Guid.NewGuid.ToString)
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If appay_bk_id.ItemIndex = -1 Then
            MessageBox.Show("Bank can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _gcald_det_status As String = func_data.get_gcald_det_status(appay_en_id.EditValue, "gcald_ap", appay_eff_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + appay_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + appay_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("appayd_exc_rate") = 0 Then
                MessageBox.Show("Exchange Rate Does'nt Exist...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                before_save = False
                Exit For
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("appayd_disc_amount") <> 0 Then
                If appay_disc_ac_id.ItemIndex = 0 Then
                    MessageBox.Show("Account Discount Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    before_save = False
                    Exit For
                End If
            End If
        Next
        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _appay_eff_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_eff_date")
        Dim _appay_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_appay_en_id, "gcald_ap", _appay_eff_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _appay_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _appay_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

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
                                If ds.Tables("detail").Rows(i).Item("appayd_appay_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_oid") Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update ap_mstr set ap_pay_amount = ap_pay_amount - " _
                                            & SetDec(ds.Tables("detail").Rows(i).Item("appayd_cur_amount")) & " , ap_status = null where ap_oid = '" _
                                            & ds.Tables("detail").Rows(i).Item("appayd_ap_oid").ToString & "'"

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'update rekonsiliasi
                                    If update_rec(objinsert, ssqls, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_en_id"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_bk_id"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_cu_id"), _
                                         ds.Tables("detail").Rows(i).Item("appayd_exc_rate"), ds.Tables("detail").Rows(i).Item("appayd_cur_amount"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_date"), ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_code"), _
                                         "Delete " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_name") & " " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_remarks"), "AP PAYMENT") = False Then
                                        sqlTran.Rollback()
                                        Return False
                                        Exit Function
                                    End If
                                End If
                            Next

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from appay_payment where appay_oid = '" _
                                    & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_oid").ToString & "'"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If _create_jurnal = True Then
                                Dim _appay_eff_date As Date = func_coll.get_tanggal_sistem() 'karena cancel harus menjurnal ditanggal dia cancel
                                Dim _appay_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_en_id")
                                Dim _gcald_det_status As String = func_data.get_gcald_det_status(_appay_en_id, "gcald_ap", _appay_eff_date)

                                If _gcald_det_status = "" Then
                                    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _appay_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Exit Function
                                ElseIf _gcald_det_status.ToUpper = "Y" Then
                                    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _appay_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Exit Function
                                End If

                                If delete_glt_det_ap_payment(ssqls, objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_oid").ToString, _
                                                           ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_code")) = False Then
                                    sqlTran.Rollback()
                                    Exit Function
                                End If
                            End If


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete AP Payment " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_code"))
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

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Private Function delete_glt_det_ap_payment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_appay_oid As String, ByVal par_appay_code As String) As Boolean
        delete_glt_det_ap_payment = True
        Dim _glt_code As String = ""
        Dim _glt_posted As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select glt_code, glt_posted from glt_det " + _
                                           " where glt_ref_oid = '" + par_appay_oid + "'" + _
                                           " and glt_ref_trans_code = '" + par_appay_code + "'"
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
            ''by sys 20110425
            ''tetap melakukan jurnal balik tanpa mendelete jurnal sebelumnya
            'If insert_glt_det_ap_payment_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
            '    Return False
            '    Exit Function
            'End If
        Else
            If insert_glt_det_ap_payment_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If
        End If
    End Function

    Private Function insert_glt_det_ap_payment_jurnal_balik(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_glt_code As String) As Boolean
        'ini prosedur untuk jurnal balik jurnal ap payment karena didelete tetapi sudah di posting...
        insert_glt_det_ap_payment_jurnal_balik = True
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
        Dim _glt_code As String = func_coll.get_transaction_number("PY", _en_code, "glt_det", "glt_code")
        Dim _date As Date = func_coll.get_tanggal_sistem

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
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("AP") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
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
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("AP") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
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

#Region "DML"
    Public Overrides Function insert() As Boolean
        Dim _appay_oid As Guid = Guid.NewGuid

        Dim _appay_code As String = func_coll.get_transaction_number("PY", appay_en_id.GetColumnValue("en_code"), "appay_payment", "appay_code")
        Dim _appay_total_amount As Double = 0
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _appay_total_amount = _appay_total_amount + ds_edit.Tables(0).Rows(i).Item("appayd_amount")
        Next

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
                                            & "  public.appay_payment " _
                                            & "( " _
                                            & "  appay_oid, " _
                                            & "  appay_dom_id, " _
                                            & "  appay_en_id, " _
                                            & "  appay_add_by, " _
                                            & "  appay_add_date, " _
                                            & "  appay_code, " _
                                            & "  appay_supplier, " _
                                            & "  appay_cu_id, " _
                                            & "  appay_bk_id, " _
                                            & "  appay_ap_ac_id, " _
                                            & "  appay_ap_sb_id, " _
                                            & "  appay_ap_cc_id, " _
                                            & "  appay_disc_ac_id, " _
                                            & "  appay_disc_sb_id, " _
                                            & "  appay_disc_cc_id, " _
                                            & "  appay_date, " _
                                            & "  appay_eff_date, " _
                                            & "  appay_total_amount, " _
                                            & "  appay_remarks, " _
                                            & "  appay_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_appay_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(appay_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_appay_code) & ",  " _
                                            & SetInteger(appay_supplier.EditValue) & ",  " _
                                            & SetInteger(appay_cu_id.EditValue) & ",  " _
                                            & SetInteger(appay_bk_id.EditValue) & ",  " _
                                            & SetInteger(appay_ap_ac_id.EditValue) & ",  " _
                                            & SetInteger(appay_ap_sb_id.EditValue) & ",  " _
                                            & SetInteger(appay_ap_cc_id.EditValue) & ",  " _
                                            & SetInteger(appay_disc_ac_id.EditValue) & ",  " _
                                            & SetInteger(appay_disc_sb_id.EditValue) & ",  " _
                                            & SetInteger(appay_disc_cc_id.EditValue) & ",  " _
                                            & SetDate(appay_date.DateTime) & ",  " _
                                            & SetDate(appay_eff_date.DateTime) & ",  " _
                                            & SetDbl(_appay_total_amount) & ",  " _
                                            & SetSetring(appay_remarks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Insert Data Detailnya
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.appayd_det " _
                                                & "( " _
                                                & "  appayd_oid, " _
                                                & "  appayd_appay_oid, " _
                                                & "  appayd_ap_oid, " _
                                                & "  appayd_ap_ref, " _
                                                & "  appayd_type, " _
                                                & "  appayd_ac_id, " _
                                                & "  appayd_sb_id, " _
                                                & "  appayd_cc_id, " _
                                                & "  appayd_amount, " _
                                                & "  appayd_cash_amount, " _
                                                & "  appayd_disc_amount, " _
                                                & "  appayd_remarks, " _
                                                & "  appayd_dt, " _
                                                & "  appayd_cur_amount, " _
                                                & "  appayd_exc_rate " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appayd_oid").ToString) & ",  " _
                                                & SetSetring(_appay_oid.ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appayd_ap_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appayd_ap_ref")) & ",  " _
                                                & SetSetring("Y") & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("appayd_ac_id")) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("appayd_sb_id")) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("appayd_cc_id")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("appayd_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("appayd_cash_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("appayd_disc_amount")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("appayd_remarks")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("appayd_cur_amount")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("appayd_exc_rate")) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table ap_mstr untuk update field ap_pay_amount
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ap_mstr set ap_pay_amount = coalesce(ap_pay_amount,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("appayd_cur_amount")) + _
                                                   " where ap_oid = '" + ds_edit.Tables(0).Rows(i).Item("appayd_ap_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table ap_mstr untuk status apabila sudah lunas
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ap_mstr set ap_status = 'c' " + _
                                                   " where ap_pay_amount >= ap_amount and ap_oid = '" + ds_edit.Tables(0).Rows(i).Item("appayd_ap_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'update rekonsiliasi
                            If update_rec(objinsert, ssqls, appay_en_id.EditValue, appay_bk_id.EditValue, appay_cu_id.EditValue, _
                                  ds_edit.Tables(0).Rows(i).Item("appayd_exc_rate"), ds_edit.Tables(0).Rows(i).Item("appayd_cur_amount") * -1, appay_date.DateTime, _appay_code, _
                                  appay_supplier.Text & " " & appay_remarks.Text, "AP PAYMENT") = False Then
                                sqlTran.Rollback()
                                Return False
                                Exit Function
                            End If



                        Next

                        'insert Jurnal 
                        If _create_jurnal = True Then
                            If insert_glt_det_appay(ssqls, objinsert, ds_edit, _
                                                       appay_en_id.EditValue, appay_en_id.GetColumnValue("en_code"), _
                                                       _appay_oid.ToString, _appay_code, _
                                                       appay_eff_date.DateTime, _
                                                       appay_cu_id.EditValue, _
                                                       "AP", "AP-PAY", _
                                                       appay_ap_ac_id.EditValue, appay_ap_sb_id.EditValue, appay_ap_cc_id.EditValue, _
                                                       appay_ap_ac_id.GetColumnValue("ac_name"), _
                                                       _appay_total_amount) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If
                        '************************************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert AP Payment " & _appay_code)
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
                        set_row(_appay_oid.ToString, "appay_oid")
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

    Private Function insert_glt_det_appay(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_type As String, ByVal par_daybook As String, _
                                   ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
                                   ByVal par_cc_id As Integer, _
                                   ByVal par_desc As String, ByVal par_amount As Double) As Boolean

        insert_glt_det_appay = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number("PY", par_en_code, "glt_det", "glt_code")
        Dim _exc_rate As Double = 1
        Dim _nilai_awal, _nilai_akhir, _nilai_hitung As Double
        Dim _seq As Double = -1
        Dim _glt_desc As String = ""

        'Insert Untuk Yang Debet Dulu, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1
            With par_obj
                Try

                    _glt_desc = "AR Payment " & appay_supplier.Text & " " & appay_remarks.Text & " " & par_ds.Tables(0).Rows(i).Item("appayd_remarks") 'par_desc 'IIf(IsDBNull(par_ds.Tables(0).Rows(i).Item("appayd_remarks")) = True, "AP Payment", par_ds.Tables(0).Rows(i).Item("appayd_remarks"))

                    _exc_rate = par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
                    _nilai_awal = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("ap_exc_rate")
                    _nilai_hitung = _nilai_awal / par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")

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
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_type) & ",  " _
                                        & SetInteger(par_cu_id) & ",  " _
                                        & SetDbl(_exc_rate) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetInteger(par_ds.Tables(0).Rows(i).Item("appayd_ac_id")) & ",  " _
                                        & SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_sb_id")) & ",  " _
                                        & SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_cc_id")) & ",  " _
                                        & SetSetring(_glt_desc) & ",  " _
                                        & SetDblDB(_nilai_hitung) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(par_daybook) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                         par_ds.Tables(0).Rows(i).Item("appayd_ac_id"), _
                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_sb_id")), _
                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_cc_id")), _
                                                         par_en_id, par_cu_id, _
                                                         _exc_rate, _nilai_hitung, "D") = False Then

                        Return False
                        Exit Function
                    End If

                    'Insert untuk yang credit 
                    _seq = _seq + 1
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
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_type) & ",  " _
                                        & SetInteger(par_cu_id) & ",  " _
                                        & SetDbl(_exc_rate) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetInteger(appay_ap_ac_id.EditValue) & ",  " _
                                        & SetIntegerDB(0) & ",  " _
                                        & SetIntegerDB(0) & ",  " _
                                        & SetSetring(_glt_desc) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("appayd_cash_amount")) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(par_daybook) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                    'Return False
                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                        appay_ap_ac_id.EditValue, _
                                                        SetIntegerDB(0), _
                                                        SetIntegerDB(0), _
                                                        par_en_id, par_cu_id, _
                                                        _exc_rate, par_ds.Tables(0).Rows(i).Item("appayd_cash_amount"), "C") = False Then

                        Return False
                        Exit Function
                    End If

                    'Untuk Real Exchange
                    _seq = _seq + 1
                    If par_ds.Tables(0).Rows(i).Item("appayd_exc_rate") < par_ds.Tables(0).Rows(i).Item("ap_exc_rate") Then
                        'maka ini adalah gain / keuntungan dan ada di sisi credit
                        'Insert untuk yang credit 

                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_real_account(appay_cu_id.EditValue, "gain"))

                        _nilai_awal = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("ap_exc_rate")
                        _nilai_akhir = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
                        _nilai_hitung = (_nilai_awal - _nilai_akhir) / par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
                        'nilai awal - nilai akhir agar tidak jadi minus...kan ditaronya di credit jadi sudah menunjukan -

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
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(dt_bantu.Rows(0).Item("ac_name") & " " & _glt_desc) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_nilai_hitung) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                            dt_bantu.Rows(0).Item("ac_id"), _
                                                            SetIntegerDB(0), _
                                                            SetIntegerDB(0), _
                                                            par_en_id, par_cu_id, _
                                                            _exc_rate, _nilai_hitung, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    ElseIf par_ds.Tables(0).Rows(i).Item("appayd_exc_rate") > par_ds.Tables(0).Rows(i).Item("ap_exc_rate") Then
                        'maka ini adalah loss / rugi dan ada di sisi debet
                        'Insert untuk yang debet

                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_real_account(appay_cu_id.EditValue, "loss"))

                        _nilai_awal = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("ap_exc_rate")
                        _nilai_akhir = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
                        _nilai_hitung = (_nilai_akhir - _nilai_awal) / par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
                        'terbalik dengan yang diatas...agar tetep jadi + jg

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
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(dt_bantu.Rows(0).Item("ac_name") & " " & _glt_desc) & ",  " _
                                            & SetDblDB(_nilai_hitung) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                            dt_bantu.Rows(0).Item("ac_id"), _
                                                            SetIntegerDB(0), _
                                                            SetIntegerDB(0), _
                                                            par_en_id, par_cu_id, _
                                                            _exc_rate, _nilai_hitung, "D") = False Then

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

        'Insert Untuk Account Discountnya.....kalau > 0 berarti credit kalau < 0 berarti debet
        _seq = _seq + 1
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    _glt_desc = "AR Payment " & appay_supplier.Text & " " & appay_remarks.Text & " " & par_ds.Tables(0).Rows(i).Item("appayd_remarks") 'IIf(IsDBNull(par_ds.Tables(0).Rows(i).Item("appayd_remarks")) = True, "AR Payment", par_ds.Tables(0).Rows(i).Item("appayd_remarks"))

                    _exc_rate = par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")

                    If par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") < 0 Then
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
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(appay_disc_ac_id.EditValue) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetring(_glt_desc) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") * -1) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             appay_disc_ac_id.EditValue, _
                                                             SetIntegerDB(0), _
                                                             SetIntegerDB(0), _
                                                             par_en_id, par_cu_id, _
                                                             _exc_rate, par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") * -1, "D") = False Then

                            Return False
                            Exit Function
                        End If
                    ElseIf par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") > 0 Then
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
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(appay_disc_ac_id.EditValue) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetring(_glt_desc) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("appayd_disc_amount")) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             appay_disc_ac_id.EditValue, _
                                                             SetIntegerDB(0), _
                                                             SetIntegerDB(0), _
                                                             par_en_id, par_cu_id, _
                                                             _exc_rate, par_ds.Tables(0).Rows(i).Item("appayd_disc_amount"), "C") = False Then

                            Return False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
            _seq = _seq + 1
        Next
    End Function

    'by sys 20110419 diganti dengan yang baru
    'Private Function insert_glt_det_appay(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
    '                               ByVal par_en_id As Integer, ByVal par_en_code As String, _
    '                               ByVal par_oid As String, ByVal par_trans_code As String, _
    '                               ByVal par_date As Date, ByVal par_cu_id As Integer, _
    '                               ByVal par_type As String, ByVal par_daybook As String, _
    '                               ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
    '                               ByVal par_cc_id As Integer, _
    '                               ByVal par_desc As String, ByVal par_amount As Double) As Boolean

    '    insert_glt_det_appay = True
    '    Dim i As Integer
    '    Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
    '    Dim _exc_rate As Double = 1
    '    Dim _nilai_awal, _nilai_akhir, _nilai_hitung As Double
    '    'Return False
    '    'Insert Untuk Yang Debet Dulu, Looping dari dataset
    '    For i = 0 To par_ds.Tables(0).Rows.Count - 1
    '        With par_obj
    '            Try
    '                _exc_rate = par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")

    '                _nilai_awal = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("ap_exc_rate")
    '                _nilai_hitung = _nilai_awal / par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")

    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "INSERT INTO  " _
    '                                    & "  public.glt_det " _
    '                                    & "( " _
    '                                    & "  glt_oid, " _
    '                                    & "  glt_dom_id, " _
    '                                    & "  glt_en_id, " _
    '                                    & "  glt_add_by, " _
    '                                    & "  glt_add_date, " _
    '                                    & "  glt_code, " _
    '                                    & "  glt_date, " _
    '                                    & "  glt_type, " _
    '                                    & "  glt_cu_id, " _
    '                                    & "  glt_exc_rate, " _
    '                                    & "  glt_seq, " _
    '                                    & "  glt_ac_id, " _
    '                                    & "  glt_sb_id, " _
    '                                    & "  glt_cc_id, " _
    '                                    & "  glt_desc, " _
    '                                    & "  glt_debit, " _
    '                                    & "  glt_credit, " _
    '                                    & "  glt_ref_oid, " _
    '                                    & "  glt_ref_trans_code, " _
    '                                    & "  glt_posted, " _
    '                                    & "  glt_dt, " _
    '                                    & "  glt_daybook " _
    '                                    & ")  " _
    '                                    & "VALUES ( " _
    '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                    & SetInteger(par_en_id) & ",  " _
    '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                    & SetSetring(_glt_code) & ",  " _
    '                                    & SetDate(par_date) & ",  " _
    '                                    & SetSetring(par_type) & ",  " _
    '                                    & SetInteger(par_cu_id) & ",  " _
    '                                    & SetDbl(_exc_rate) & ",  " _
    '                                    & SetInteger(i) & ",  " _
    '                                    & SetInteger(par_ds.Tables(0).Rows(i).Item("appayd_ac_id")) & ",  " _
    '                                    & SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_sb_id")) & ",  " _
    '                                    & SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_cc_id")) & ",  " _
    '                                    & SetSetringDB(par_ds.Tables(0).Rows(i).Item("appayd_remarks")) & ",  " _
    '                                    & SetDblDB(_nilai_hitung) & ",  " _
    '                                    & SetDblDB(0) & ",  " _
    '                                    & SetSetring(par_oid) & ",  " _
    '                                    & SetSetring(par_trans_code) & ",  " _
    '                                    & SetSetring("N") & ",  " _
    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                    & SetSetring(par_daybook) & "  " _
    '                                    & ")"
    '                par_ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                .Command.Parameters.Clear()

    '                If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                     par_ds.Tables(0).Rows(i).Item("appayd_ac_id"), _
    '                                                     SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_sb_id")), _
    '                                                     SetIntegerDB(par_ds.Tables(0).Rows(i).Item("appayd_cc_id")), _
    '                                                     par_en_id, par_cu_id, _
    '                                                     _exc_rate, _nilai_hitung, "D") = False Then

    '                    Return False
    '                    Exit Function
    '                End If

    '                'Insert untuk yang credit 
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "INSERT INTO  " _
    '                                    & "  public.glt_det " _
    '                                    & "( " _
    '                                    & "  glt_oid, " _
    '                                    & "  glt_dom_id, " _
    '                                    & "  glt_en_id, " _
    '                                    & "  glt_add_by, " _
    '                                    & "  glt_add_date, " _
    '                                    & "  glt_code, " _
    '                                    & "  glt_date, " _
    '                                    & "  glt_type, " _
    '                                    & "  glt_cu_id, " _
    '                                    & "  glt_exc_rate, " _
    '                                    & "  glt_seq, " _
    '                                    & "  glt_ac_id, " _
    '                                    & "  glt_sb_id, " _
    '                                    & "  glt_cc_id, " _
    '                                    & "  glt_desc, " _
    '                                    & "  glt_debit, " _
    '                                    & "  glt_credit, " _
    '                                    & "  glt_ref_oid, " _
    '                                    & "  glt_ref_trans_code, " _
    '                                    & "  glt_posted, " _
    '                                    & "  glt_dt, " _
    '                                    & "  glt_daybook " _
    '                                    & ")  " _
    '                                    & "VALUES ( " _
    '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                    & SetInteger(par_en_id) & ",  " _
    '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                    & SetSetring(_glt_code) & ",  " _
    '                                    & SetDate(par_date) & ",  " _
    '                                    & SetSetring(par_type) & ",  " _
    '                                    & SetInteger(par_cu_id) & ",  " _
    '                                    & SetDbl(_exc_rate) & ",  " _
    '                                    & SetInteger(i) & ",  " _
    '                                    & SetInteger(appay_ap_ac_id.EditValue) & ",  " _
    '                                    & SetIntegerDB(0) & ",  " _
    '                                    & SetIntegerDB(0) & ",  " _
    '                                    & SetSetringDB(par_ds.Tables(0).Rows(i).Item("appayd_remarks")) & ",  " _
    '                                    & SetDblDB(0) & ",  " _
    '                                    & SetDblDB(par_ds.Tables(0).Rows(i).Item("appayd_cash_amount")) & ",  " _
    '                                    & SetSetring(par_oid) & ",  " _
    '                                    & SetSetring(par_trans_code) & ",  " _
    '                                    & SetSetring("N") & ",  " _
    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                    & SetSetring(par_daybook) & "  " _
    '                                    & ")"
    '                par_ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                .Command.Parameters.Clear()

    '                If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                    appay_ap_ac_id.EditValue, _
    '                                                    SetIntegerDB(0), _
    '                                                    SetIntegerDB(0), _
    '                                                    par_en_id, par_cu_id, _
    '                                                    _exc_rate, par_ds.Tables(0).Rows(i).Item("appayd_cash_amount"), "C") = False Then

    '                    Return False
    '                    Exit Function
    '                End If

    '                'Untuk Real Exchange
    '                If par_ds.Tables(0).Rows(i).Item("appayd_exc_rate") < par_ds.Tables(0).Rows(i).Item("ap_exc_rate") Then
    '                    'maka ini adalah gain / keuntungan dan ada di sisi credit
    '                    'Insert untuk yang credit 

    '                    dt_bantu = New DataTable
    '                    dt_bantu = (func_data.get_real_account(appay_cu_id.EditValue, "gain"))

    '                    _nilai_awal = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("ap_exc_rate")
    '                    _nilai_akhir = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
    '                    _nilai_hitung = (_nilai_awal - _nilai_akhir) / par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
    '                    'nilai awal - nilai akhir agar tidak jadi minus...kan ditaronya di credit jadi sudah menunjukan -

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.glt_det " _
    '                                        & "( " _
    '                                        & "  glt_oid, " _
    '                                        & "  glt_dom_id, " _
    '                                        & "  glt_en_id, " _
    '                                        & "  glt_add_by, " _
    '                                        & "  glt_add_date, " _
    '                                        & "  glt_code, " _
    '                                        & "  glt_date, " _
    '                                        & "  glt_type, " _
    '                                        & "  glt_cu_id, " _
    '                                        & "  glt_exc_rate, " _
    '                                        & "  glt_seq, " _
    '                                        & "  glt_ac_id, " _
    '                                        & "  glt_sb_id, " _
    '                                        & "  glt_cc_id, " _
    '                                        & "  glt_desc, " _
    '                                        & "  glt_debit, " _
    '                                        & "  glt_credit, " _
    '                                        & "  glt_ref_oid, " _
    '                                        & "  glt_ref_trans_code, " _
    '                                        & "  glt_posted, " _
    '                                        & "  glt_dt, " _
    '                                        & "  glt_daybook " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetInteger(par_en_id) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(_glt_code) & ",  " _
    '                                        & SetDate(par_date) & ",  " _
    '                                        & SetSetring(par_type) & ",  " _
    '                                        & SetInteger(par_cu_id) & ",  " _
    '                                        & SetDbl(_exc_rate) & ",  " _
    '                                        & SetInteger(i) & ",  " _
    '                                        & SetInteger(dt_bantu.Rows(0).Item("ac_id")) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetSetringDB(dt_bantu.Rows(0).Item("ac_name")) & ",  " _
    '                                        & SetDblDB(0) & ",  " _
    '                                        & SetDblDB(_nilai_hitung) & ",  " _
    '                                        & SetSetring(par_oid) & ",  " _
    '                                        & SetSetring(par_trans_code) & ",  " _
    '                                        & SetSetring("N") & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(par_daybook) & "  " _
    '                                        & ")"
    '                    par_ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                        dt_bantu.Rows(0).Item("ac_id"), _
    '                                                        SetIntegerDB(0), _
    '                                                        SetIntegerDB(0), _
    '                                                        par_en_id, par_cu_id, _
    '                                                        _exc_rate, _nilai_hitung, "C") = False Then

    '                        Return False
    '                        Exit Function
    '                    End If
    '                ElseIf par_ds.Tables(0).Rows(i).Item("appayd_exc_rate") > par_ds.Tables(0).Rows(i).Item("ap_exc_rate") Then
    '                    'maka ini adalah loss / rugi dan ada di sisi debet
    '                    'Insert untuk yang debet

    '                    dt_bantu = New DataTable
    '                    dt_bantu = (func_data.get_real_account(appay_cu_id.EditValue, "loss"))

    '                    _nilai_awal = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("ap_exc_rate")
    '                    _nilai_akhir = par_ds.Tables(0).Rows(i).Item("appayd_amount") * par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
    '                    _nilai_hitung = (_nilai_akhir - _nilai_awal) / par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")
    '                    'terbalik dengan yang diatas...agar tetep jadi + jg

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.glt_det " _
    '                                        & "( " _
    '                                        & "  glt_oid, " _
    '                                        & "  glt_dom_id, " _
    '                                        & "  glt_en_id, " _
    '                                        & "  glt_add_by, " _
    '                                        & "  glt_add_date, " _
    '                                        & "  glt_code, " _
    '                                        & "  glt_date, " _
    '                                        & "  glt_type, " _
    '                                        & "  glt_cu_id, " _
    '                                        & "  glt_exc_rate, " _
    '                                        & "  glt_seq, " _
    '                                        & "  glt_ac_id, " _
    '                                        & "  glt_sb_id, " _
    '                                        & "  glt_cc_id, " _
    '                                        & "  glt_desc, " _
    '                                        & "  glt_debit, " _
    '                                        & "  glt_credit, " _
    '                                        & "  glt_ref_oid, " _
    '                                        & "  glt_ref_trans_code, " _
    '                                        & "  glt_posted, " _
    '                                        & "  glt_dt, " _
    '                                        & "  glt_daybook " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetInteger(par_en_id) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(_glt_code) & ",  " _
    '                                        & SetDate(par_date) & ",  " _
    '                                        & SetSetring(par_type) & ",  " _
    '                                        & SetInteger(par_cu_id) & ",  " _
    '                                        & SetDbl(_exc_rate) & ",  " _
    '                                        & SetInteger(i) & ",  " _
    '                                        & SetInteger(dt_bantu.Rows(0).Item("ac_id")) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetSetringDB(dt_bantu.Rows(0).Item("ac_name")) & ",  " _
    '                                        & SetDblDB(_nilai_hitung) & ",  " _
    '                                        & SetDblDB(0) & ",  " _
    '                                        & SetSetring(par_oid) & ",  " _
    '                                        & SetSetring(par_trans_code) & ",  " _
    '                                        & SetSetring("N") & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(par_daybook) & "  " _
    '                                        & ")"
    '                    par_ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                        dt_bantu.Rows(0).Item("ac_id"), _
    '                                                        SetIntegerDB(0), _
    '                                                        SetIntegerDB(0), _
    '                                                        par_en_id, par_cu_id, _
    '                                                        _exc_rate, _nilai_hitung, "D") = False Then

    '                        Return False
    '                        Exit Function
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '                Return False
    '            End Try
    '        End With
    '    Next

    '    'Insert Untuk Account Discountnya.....kalau > 0 berarti credit kalau < 0 berarti debet
    '    For i = 0 To par_ds.Tables(0).Rows.Count - 1
    '        With par_obj
    '            Try
    '                _exc_rate = par_ds.Tables(0).Rows(i).Item("appayd_exc_rate")

    '                If par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") < 0 Then
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.glt_det " _
    '                                        & "( " _
    '                                        & "  glt_oid, " _
    '                                        & "  glt_dom_id, " _
    '                                        & "  glt_en_id, " _
    '                                        & "  glt_add_by, " _
    '                                        & "  glt_add_date, " _
    '                                        & "  glt_code, " _
    '                                        & "  glt_date, " _
    '                                        & "  glt_type, " _
    '                                        & "  glt_cu_id, " _
    '                                        & "  glt_exc_rate, " _
    '                                        & "  glt_seq, " _
    '                                        & "  glt_ac_id, " _
    '                                        & "  glt_sb_id, " _
    '                                        & "  glt_cc_id, " _
    '                                        & "  glt_desc, " _
    '                                        & "  glt_debit, " _
    '                                        & "  glt_credit, " _
    '                                        & "  glt_ref_oid, " _
    '                                        & "  glt_ref_trans_code, " _
    '                                        & "  glt_posted, " _
    '                                        & "  glt_dt, " _
    '                                        & "  glt_daybook " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetInteger(par_en_id) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(_glt_code) & ",  " _
    '                                        & SetDate(par_date) & ",  " _
    '                                        & SetSetring(par_type) & ",  " _
    '                                        & SetInteger(par_cu_id) & ",  " _
    '                                        & SetDbl(_exc_rate) & ",  " _
    '                                        & SetInteger(i) & ",  " _
    '                                        & SetInteger(appay_disc_ac_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("appayd_remarks")) & ",  " _
    '                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") * -1) & ",  " _
    '                                        & SetDblDB(0) & ",  " _
    '                                        & SetSetring(par_oid) & ",  " _
    '                                        & SetSetring(par_trans_code) & ",  " _
    '                                        & SetSetring("N") & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(par_daybook) & "  " _
    '                                        & ")"
    '                    par_ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                         appay_disc_ac_id.EditValue, _
    '                                                         SetIntegerDB(0), _
    '                                                         SetIntegerDB(0), _
    '                                                         par_en_id, par_cu_id, _
    '                                                         _exc_rate, par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") * -1, "D") = False Then

    '                        Return False
    '                        Exit Function
    '                    End If
    '                ElseIf par_ds.Tables(0).Rows(i).Item("appayd_disc_amount") > 0 Then
    '                    'Insert untuk yang credit 
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.glt_det " _
    '                                        & "( " _
    '                                        & "  glt_oid, " _
    '                                        & "  glt_dom_id, " _
    '                                        & "  glt_en_id, " _
    '                                        & "  glt_add_by, " _
    '                                        & "  glt_add_date, " _
    '                                        & "  glt_code, " _
    '                                        & "  glt_date, " _
    '                                        & "  glt_type, " _
    '                                        & "  glt_cu_id, " _
    '                                        & "  glt_exc_rate, " _
    '                                        & "  glt_seq, " _
    '                                        & "  glt_ac_id, " _
    '                                        & "  glt_sb_id, " _
    '                                        & "  glt_cc_id, " _
    '                                        & "  glt_desc, " _
    '                                        & "  glt_debit, " _
    '                                        & "  glt_credit, " _
    '                                        & "  glt_ref_oid, " _
    '                                        & "  glt_ref_trans_code, " _
    '                                        & "  glt_posted, " _
    '                                        & "  glt_dt, " _
    '                                        & "  glt_daybook " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetInteger(par_en_id) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(_glt_code) & ",  " _
    '                                        & SetDate(par_date) & ",  " _
    '                                        & SetSetring(par_type) & ",  " _
    '                                        & SetInteger(par_cu_id) & ",  " _
    '                                        & SetDbl(_exc_rate) & ",  " _
    '                                        & SetInteger(i) & ",  " _
    '                                        & SetInteger(appay_disc_ac_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("appayd_remarks")) & ",  " _
    '                                        & SetDblDB(0) & ",  " _
    '                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("appayd_disc_amount")) & ",  " _
    '                                        & SetSetring(par_oid) & ",  " _
    '                                        & SetSetring(par_trans_code) & ",  " _
    '                                        & SetSetring("N") & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(par_daybook) & "  " _
    '                                        & ")"
    '                    par_ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                         appay_disc_ac_id.EditValue, _
    '                                                         SetIntegerDB(0), _
    '                                                         SetIntegerDB(0), _
    '                                                         par_en_id, par_cu_id, _
    '                                                         _exc_rate, par_ds.Tables(0).Rows(i).Item("appayd_disc_amount"), "C") = False Then

    '                        Return False
    '                        Exit Function
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '                Return False
    '            End Try
    '        End With
    '    Next
    'End Function

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  appay_oid, " _
            & "  appay_dom_id, " _
            & "  appay_en_id, " _
            & "  appay_add_by, " _
            & "  appay_add_date, " _
            & "  appay_upd_by, " _
            & "  appay_upd_date, " _
            & "  appay_code, " _
            & "  appay_supplier, " _
            & "  appay_cu_id, " _
            & "  appay_bk_id, " _
            & "  appay_date, " _
            & "  appay_eff_date, " _
            & "  appay_total_amount, " _
            & "  appay_remarks, " _
            & "  appayd_ac_id, " _
            & "  appayd_cash_amount, " _
            & "  appayd_exc_rate, " _
            & "  appayd_cash_amount * appayd_exc_rate as appayd_cash_amount_ext, " _
            & "  appayd_remarks, " _
            & "  ac_code, " _
            & "  ac_name, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  ptnr_name " _
            & "FROM  " _
            & "  appay_payment " _
            & "  inner join appayd_det on appayd_appay_oid = appay_oid " _
            & "  inner join ac_mstr on ac_id = appayd_ac_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = appay_en_id " _
            & "  inner join ptnr_mstr on ptnr_id = appay_supplier" _
            & "  where appay_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRCashOutAPPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("appay_code")
        frm.ShowDialog()
    End Sub
#End Region

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub
End Class
