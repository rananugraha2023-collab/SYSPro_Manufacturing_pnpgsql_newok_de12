Imports npgsql
Imports master_new.ModFunction

Public Class FCanvasingPlanner

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _cnv_oid_mstr As String
    Dim _now As DateTime
    Public ds_edit_item, ds_edit_rule As DataSet

    Private Sub FCanvasingPlanner_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        'AddHandler gv_edit_item.FocusedRowChanged, AddressOf relation_detail
        'AddHandler gv_edit_item.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        cnv_en_id.Properties.DataSource = dt_bantu
        cnv_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cnv_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cnv_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_so_type())
        cnv_so_type.Properties.DataSource = dt_bantu
        cnv_so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        cnv_so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        cnv_so_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        cnv_cu_id.Properties.DataSource = dt_bantu
        cnv_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        cnv_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        cnv_cu_id.ItemIndex = 0


        dt_bantu = New DataTable
        dt_bantu = (func_data.load_area_mstr())
        cnv_region.Properties.DataSource = dt_bantu
        cnv_region.Properties.DisplayMember = dt_bantu.Columns("area_name").ToString
        cnv_region.Properties.ValueMember = dt_bantu.Columns("area_id").ToString
        cnv_region.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_so_type())
        'cnv_so_type.Properties.DataSource = dt_bantu
        'cnv_so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        'cnv_so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        'cnv_so_type.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr_ptnr())
        'cnv_loc_id.Properties.DataSource = dt_bantu
        'cnv_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'cnv_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'cnv_loc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr_ptnr(cnv_en_id.EditValue))
        'cnv_loc_id.Properties.DataSource = dt_bantu
        'cnv_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'cnv_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'cnv_loc_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("ptnrg_grp", cnv_en_id.EditValue))
        'cnv_ptnrg_id.Properties.DataSource = dt_bantu
        'cnv_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        'cnv_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        'cnv_ptnrg_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_promo_mstr(cnv_en_id.EditValue))
        'cnv_promo_id.Properties.DataSource = dt_bantu
        'cnv_promo_id.Properties.DisplayMember = dt_bantu.Columns("promo_desc").ToString
        'cnv_promo_id.Properties.ValueMember = dt_bantu.Columns("promo_id").ToString
        'cnv_promo_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_sales_program(cnv_en_id.EditValue))
        'cnv_sales_program.Properties.DataSource = dt_bantu
        'cnv_sales_program.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'cnv_sales_program.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'cnv_sales_program.ItemIndex = 0
    End Sub

    Private Sub cnv_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cnv_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "cnv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "cnv_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Region", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Type", "cnv_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Pricelist", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Programe", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Partner Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Capital", "cnv_cap", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cash", "cnv_cash", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Start Date", "cnv_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "cnv_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Close Date", "cnv_close_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")


        add_column_copy(gv_master, "Is Active", "cnv_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cnv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cnv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cnv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cnv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_item, "cnvd_cnv_oid", False)
        add_column_copy(gv_detail_item, "IR Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "IR Date", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Transfer Code", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Transfer Issue Date", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Transfer Receive Date", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Transfer Return Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_item, "Transfer Return Date", "pt_code", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_detail_rule, "cnvd_cnv_oid", False)
        'add_column_copy(gv_detail_rule, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_rule, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_rule, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_rule, "Discount", "pidd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail_rule, "Prepayment", "pidd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_copy(gv_detail_rule, "Interval", "pidd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_copy(gv_detail_rule, "Payment", "pidd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_rule, "Min. Qty", "pidd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail_rule, "Sales Unit", "pidd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column(gv_edit_item, "cnvd_cnv_oid", False)
        'add_column(gv_edit_item, "cnvd_oid", False)
        ''add_column(gv_edit_item, "pid_pt_id", False)
        'add_column(gv_edit_item, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_item, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_item, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_edit_rule, "pidd_oid", False)
        'add_column(gv_edit_rule, "pidd_pid_oid", False)
        'add_column(gv_edit_rule, "pidd_payment_type", False)
        'add_column(gv_edit_rule, "Payment Type", "payment_type_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit_rule, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_rule, "Discount", "pidd_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_edit(gv_edit_rule, "Prepayment", "pidd_dp", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_edit(gv_edit_rule, "Interval", "pidd_interval", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_edit(gv_edit_rule, "Payment", "pidd_payment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_rule, "Min. Qty", "pidd_min_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_rule, "Sales Unit", "pidd_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.cnv_mstr.cnv_oid, " _
                    & "  public.cnv_mstr.cnv_id, " _
                    & "  public.cnv_mstr.cnv_en_id, " _
                    & "  public.cnv_mstr.cnv_code, " _
                    & "  public.cnv_mstr.cnv_desc, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.cnv_mstr.cnv_date, " _
                    & "  public.cnv_mstr.cnv_sls_id, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.cnv_mstr.cnv_region, " _
                    & "  public.area_mstr.area_name, " _
                    & "  public.cnv_mstr.cnv_so_type, " _
                    & "  public.cnv_mstr.cnv_pi_id, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.cnv_mstr.cnv_ptnrg_id, " _
                    & "  public.ptnrg_grp.ptnrg_name, " _
                    & "  public.cnv_mstr.cnv_promo_id, " _
                    & "  public.promo_mstr.promo_desc, " _
                    & "  public.cnv_mstr.cnv_prg_id, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.cnv_mstr.cnv_loc_id, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.cnv_mstr.cnv_start_date, " _
                    & "  public.cnv_mstr.cnv_end_date, " _
                    & "  public.cnv_mstr.cnv_cap, " _
                    & "  public.cnv_mstr.cnv_cash, " _
                    & "  public.cnv_mstr.cnv_ttl, " _
                    & "  public.cnv_mstr.cnv_close_date, " _
                    & "  public.cnv_mstr.cnv_active, " _
                    & "  public.cnv_mstr.cnv_add_by, " _
                    & "  public.cnv_mstr.cnv_add_date, " _
                    & "  public.cnv_mstr.cnv_upd_by, " _
                    & "  public.cnv_mstr.cnv_upd_date " _
                    & "FROM " _
                    & "  public.cnv_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.cnv_mstr.cnv_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.cnv_mstr.cnv_sls_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.area_mstr ON (public.cnv_mstr.cnv_region = public.area_mstr.area_id) " _
                    & "  INNER JOIN public.pi_mstr ON (public.cnv_mstr.cnv_pi_id = public.pi_mstr.pi_id) " _
                    & "  LEFT OUTER JOIN public.ptnrg_grp ON (public.cnv_mstr.cnv_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                    & "  INNER JOIN public.promo_mstr ON (public.cnv_mstr.cnv_promo_id = public.promo_mstr.promo_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.cnv_mstr.cnv_prg_id = public.code_mstr.code_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.cnv_mstr.cnv_loc_id = public.loc_mstr.loc_id) " _
                    & " where cnv_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        cnv_en_id.Focus()
        cnv_en_id.ItemIndex = 0
        cnv_code.EditValue = False
        cnv_code.Text = ""
        cnv_desc.Text = ""
        cnv_sls_id.Text = ""
        cnv_sls_id.Tag = ""
        cnv_pi_id.Text = ""
        cnv_pi_id.Tag = ""
        cnv_loc_id.Text = ""
        cnv_loc_id.Tag = ""

        cnv_so_type.ItemIndex = 0
        cnv_promo_id.Text = ""
        cnv_cu_id.ItemIndex = 0
        cnv_sales_program.Text = ""
        cnv_date.DateTime = _now
        cnv_start_date.DateTime = _now
        cnv_end_date.DateTime = _now
        cnv_active.EditValue = True
        cnv_ptnrg_id.Text = ""
        'cnv_loc_id.ItemIndex = 0
        cnv_region.ItemIndex = 0

    End Sub

    

    Public Overrides Function before_save() As Boolean
        before_save = True
        'gv_edit_item.UpdateCurrentRow()
        'gv_edit_rule.UpdateCurrentRow()

        'ds_edit_item.AcceptChanges()
        'ds_edit_rule.AcceptChanges()

        'Ini Diperbolehkan kosong...,kalau kosong artinya berlaku untuk semua barang
        'If ds_edit_item.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If ds_edit_rule.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'Dim i As Integer

        'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("riud_loc_id")) = True Then
        '    MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    BindingContext(ds_edit.Tables(0)).Position = i
        '    Return False
        'End If

        'If IsDBNull cnv_loc_id.GetColumnValue("value") = True Then

        If cnv_desc.Text = "" Then
            MessageBox.Show("Desc Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If cnv_sls_id.Text = "" Then
            MessageBox.Show("Sales Person Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If cnv_pi_id.Text = "" Then
            MessageBox.Show("Pricelist Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If cnv_loc_id.Text = "" Then
            MessageBox.Show("Location Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If


        'End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _cnv_oid As Guid = Guid.NewGuid
        Dim _cnv_code As String
        Dim i As Integer
        Dim ssqls As New ArrayList


        _cnv_code = func_coll.get_transaction_number("CN", cnv_en_id.GetColumnValue("en_code"), "cnv_mstr", "cnv_code")
        ssqls.Clear()

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
                                            & "  public.cnv_mstr " _
                                            & "( " _
                                            & "  cnv_oid, " _
                                            & "  cnv_dom_id, " _
                                            & "  cnv_en_id, " _
                                            & "  cnv_add_by, " _
                                            & "  cnv_add_date, " _
                                            & "  cnv_id, " _
                                            & "  cnv_code, " _
                                            & "  cnv_desc, " _
                                            & "  cnv_sls_id, " _
                                            & "  cnv_region, " _
                                            & "  cnv_so_type, " _
                                            & "  cnv_pi_id, " _
                                            & "  cnv_ptnrg_id, " _
                                            & "  cnv_promo_id, " _
                                            & "  cnv_prg_id, " _
                                            & "  cnv_cu_id, " _
                                            & "  cnv_loc_id, " _
                                            & "  cnv_cap, " _
                                            & "  cnv_cash, " _
                                            & "  cnv_start_date, " _
                                            & "  cnv_end_date, " _
                                            & "  cnv_active, " _
                                            & "  cnv_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_cnv_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(cnv_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("cnv_mstr", cnv_en_id.GetColumnValue("en_code"), "cnv_id", "cnv_en_id", cnv_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(_cnv_code) & ",  " _
                                            & SetSetring(cnv_desc.Text) & ",  " _
                                            & SetInteger(cnv_sls_id.Tag) & ",  " _
                                            & SetSetring(cnv_region.EditValue) & ",  " _
                                            & SetSetring(cnv_so_type.EditValue) & ",  " _
                                            & SetInteger(cnv_pi_id.Tag) & ",  " _
                                            & SetInteger(cnv_ptnrg_id.Tag) & ",  " _
                                            & SetInteger(cnv_promo_id.Tag) & ",  " _
                                            & SetInteger(cnv_sales_program.Tag) & ",  " _
                                            & SetInteger(cnv_cu_id.EditValue) & ",  " _
                                            & SetInteger(cnv_loc_id.Tag) & ",  " _
                                            & SetSetring(cnv_cap.EditValue) & ",  " _
                                            & SetSetring(cnv_cash.EditValue) & ",  " _
                                            & SetDate(cnv_start_date.DateTime) & ",  " _
                                            & SetDate(cnv_end_date.DateTime) & ",  " _
                                            & SetBitYN(cnv_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(_cnv_oid.ToString, "cnv_oid")
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

    Private Sub cvn_sls_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cnv_sls_id.ButtonClick

        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._obj = cnv_sls_id
        frm._en_id = cnv_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    Private Sub cnv_loc_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cnv_loc_id.ButtonClick

        Dim frm As New FLocationCanvasingSearch
        frm.set_win(Me)
        frm._obj = cnv_loc_id
        frm._sls_id = cnv_sls_id.Tag
        frm._en_id = cnv_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    Private Sub cnv_pi_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cnv_pi_id.ButtonClick

        Dim frm As New FPriceListCanvasingSearch()
        frm.set_win(Me)
        frm._en_id = cnv_en_id.EditValue
        frm._obj = cnv_pi_id
        frm._start = cnv_start_date.EditValue
        frm._end = cnv_end_date.EditValue
        frm._so_type = cnv_so_type.EditValue
        frm.type_form = True
        frm.ShowDialog()

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
                            .Command.CommandText = "delete from cnv_mstr where cnv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If MyPGDll.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cnv_oid_mstr = .Item("cnv_oid")
                cnv_en_id.EditValue = .Item("cnv_en_id")
                cnv_code.Text = .Item("cnv_code")
                cnv_desc.Text = SetString(.Item("cnv_desc"))
                cnv_so_type.EditValue = .Item("cnv_so_type")
                cnv_promo_id.EditValue = .Item("cnv_promo_id")
                cnv_cu_id.EditValue = .Item("cnv_cu_id")
                cnv_sales_program.EditValue = .Item("cnv_sales_program")
                cnv_start_date.DateTime = .Item("cnv_start_date")
                cnv_end_date.DateTime = .Item("cnv_end_date")
                cnv_active.EditValue = SetBitYNB(.Item("cnv_active"))
                cnv_ptnrg_id.EditValue = .Item("cnv_ptnrg_id")
            End With

            cnv_en_id.Focus()

            'Try
            '    tcg_header.SelectedTabPageIndex = 0
            'Catch ex As Exception
            'End Try

            'ds_edit_item = New DataSet
            'Try
            '    Using objcb As New master_new.WDABasepgsql("", "")
            '        With objcb
            '            .SQL = "SELECT  " _
            '                & "  pid_oid, " _
            '                & "  pid_add_by, " _
            '                & "  pid_add_date, " _
            '                & "  pid_upd_date, " _
            '                & "  pid_upd_by, " _
            '                & "  pid_cnv_oid, " _
            '                & "  pid_pt_id, " _
            '                & "  pt_code, pt_desc1, pt_desc2, " _
            '                & "  pid_dt " _
            '                & "FROM  " _
            '                & "  public.pid_det " _
            '                & " inner join pt_mstr on pt_id = pid_pt_id " _
            '                & " inner join cnv_mstr on cnv_oid = pid_cnv_oid " _
            '                & "  where cnv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid") + "'"

            '            .InitializeCommand()
            '            .FillDataSet(ds_edit_item, "pid_det")
            '            gc_edit_item.DataSource = ds_edit_item.Tables(0)
            '            gv_edit_item.BestFitColumns()
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            'ds_edit_rule = New DataSet
            'Try
            '    Using objcb As New master_new.WDABasepgsql("", "")
            '        With objcb
            '            .SQL = "SELECT  " _
            '                & "  pidd_oid, " _
            '                & "  pidd_add_by, " _
            '                & "  pidd_add_date, " _
            '                & "  pidd_upd_date, " _
            '                & "  pidd_upd_by, " _
            '                & "  pidd_pid_oid, " _
            '                & "  pidd_payment_type, " _
            '                & "  code_name as payment_type_name, " _
            '                & "  pidd_price, " _
            '                & "  pidd_disc, " _
            '                & "  pidd_dp, " _
            '                & "  pidd_interval, " _
            '                & "  pidd_payment, " _
            '                & "  pidd_min_qty, " _
            '                & "  pidd_sales_unit, " _
            '                & "  pidd_dt, pt_code, pt_desc1, pt_desc2 " _
            '                & "FROM  " _
            '                & "  public.pidd_det " _
            '                & "  inner join public.code_mstr on code_id = pidd_payment_type " _
            '                & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
            '                & "  inner join public.cnv_mstr on cnv_oid = pid_cnv_oid " _
            '                & "  inner join public.pt_mstr on pt_id = pid_pt_id " _
            '                & "  where cnv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cnv_oid") + "'"

            '            .InitializeCommand()
            '            .FillDataSet(ds_edit_rule, "rule")
            '            gc_edit_rule.DataSource = ds_edit_rule.Tables(0)
            '            gv_edit_rule.BestFitColumns()
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

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

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.cnv_mstr   " _
                                            & "SET  " _
                                            & "  cnv_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  cnv_en_id = " & SetInteger(cnv_en_id.EditValue) & ",  " _
                                            & "  cnv_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  cnv_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  cnv_code = " & SetSetring(cnv_code.Text) & ",  " _
                                            & "  cnv_desc = " & SetSetring(cnv_desc.Text) & ",  " _
                                            & "  cnv_so_type = " & SetSetring(cnv_so_type.EditValue) & ",  " _
                                            & "  cnv_promo_id = " & SetInteger(cnv_promo_id.EditValue) & ",  " _
                                            & "  cnv_ptnrg_id = " & SetInteger(cnv_ptnrg_id.EditValue) & ",  " _
                                            & "  cnv_cu_id = " & SetInteger(cnv_cu_id.EditValue) & ",  " _
                                            & "  cnv_sales_program = " & SetInteger(cnv_sales_program.EditValue) & ",  " _
                                            & "  cnv_start_date = " & SetDate(cnv_start_date.DateTime) & ",  " _
                                            & "  cnv_end_date = " & SetDate(cnv_end_date.DateTime) & ",  " _
                                            & "  cnv_active = " & SetBitYN(cnv_active.EditValue) & ",  " _
                                            & "  cnv_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cnv_oid = " & SetSetring(_cnv_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pid_det where pid_cnv_oid = '" + _cnv_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.pid_det " _
                        '                        & "( " _
                        '                        & "  pid_oid, " _
                        '                        & "  pid_add_by, " _
                        '                        & "  pid_add_date, " _
                        '                        & "  pid_cnv_oid, " _
                        '                        & "  pid_pt_id, " _
                        '                        & "  pid_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("pid_oid").ToString) & ",  " _
                        '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        '                        & SetSetring(_cnv_oid_mstr) & ",  " _
                        '                        & SetInteger(ds_edit_item.Tables(0).Rows(i).Item("pid_pt_id").ToString) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ")"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        ''Untuk Update data rule
                        'For i = 0 To ds_edit_rule.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.pidd_det " _
                        '                        & "( " _
                        '                        & "  pidd_oid, " _
                        '                        & "  pidd_add_by, " _
                        '                        & "  pidd_add_date, " _
                        '                        & "  pidd_pid_oid, " _
                        '                        & "  pidd_payment_type, " _
                        '                        & "  pidd_price, " _
                        '                        & "  pidd_disc, " _
                        '                        & "  pidd_dp, " _
                        '                        & "  pidd_interval, " _
                        '                        & "  pidd_payment, " _
                        '                        & "  pidd_min_qty, " _
                        '                        & "  pidd_sales_unit, " _
                        '                        & "  pidd_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(ds_edit_rule.Tables(0).Rows(i).Item("pidd_oid").ToString) & ",  " _
                        '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        '                        & SetSetring(ds_edit_rule.Tables(0).Rows(i).Item("pidd_pid_oid").ToString) & ",  " _
                        '                        & SetInteger(ds_edit_rule.Tables(0).Rows(i).Item("pidd_payment_type")) & ",  " _
                        '                        & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_price")) & ",  " _
                        '                        & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_disc")) & ",  " _
                        '                        & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_dp")) & ",  " _
                        '                        & SetDblDB(0) & ",  " _
                        '                        & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_payment")) & ",  " _
                        '                        & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_min_qty")) & ",  " _
                        '                        & SetDblDB(ds_edit_rule.Tables(0).Rows(i).Item("pidd_sales_unit")) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ")"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_cnv_oid_mstr, "cnv_oid")
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

End Class

