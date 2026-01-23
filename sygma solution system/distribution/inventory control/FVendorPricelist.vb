Imports npgsql
Imports master_new.ModFunction

Public Class FVendorPricelist
    Dim _invct_oid_mstr As String
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _pt_ls As String = ""
    Public _pt_id_global As Integer
    Public _pt_id As Integer

    Private Sub FInventoryCycleCount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr())

        With ip_si_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            .ItemIndex = 0
        End With

        init_le(ip_en_id, "en_mstr")

    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_cust_int", ip_en_id.EditValue))
        ip_cust_id.Properties.DataSource = dt_bantu
        ip_cust_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        ip_cust_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        ip_cust_id.ItemIndex = 0

        'Public Overrides Sub load_cb_en()

        '    dt_bantu = New DataTable
        '    dt_bantu = (func_data.load_data("ptnr_mstr_vend", po_en_id.EditValue))
        '    po_ptnr_id.Properties.DataSource = dt_bantu
        '    po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        '    po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        '    po_ptnr_id.ItemIndex = 0

    End Sub

    Private Sub ip_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ip_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "cust", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "vend", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Internal", "ip_is_internal", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost", "ip_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Margin", "ip_margin_pct", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price", "ip_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Best Price", "ip_is_best", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ip_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ip_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ip_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ip_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.ip_date, " _
                    & "  a.ip_dom_id, " _
                    & "  b.dom_desc, " _
                    & "  a.ip_si_id, " _
                    & "  c.si_desc, " _
                    & "  a.ip_oid, " _
                    & "  a.ip_id, " _
                    & "  a.ip_en_id, " _
                    & "  d.en_desc, " _
                    & "  a.ip_cust_id, " _
                    & "  cust_mstr.ptnr_name AS cust, " _
                    & "  a.ip_is_internal, " _
                    & "  a.ip_is_best, " _
                    & "  a.ip_vendor_id, " _
                    & "  vend_mstr.ptnr_name AS vend, " _
                    & "  a.ip_pt_id, " _
                    & "  e.pt_code, " _
                    & "  e.pt_desc1, " _
                    & "  a.ip_cost, " _
                    & "  a.ip_margin_pct, " _
                    & "  a.ip_price, " _
                    & "  a.ip_start_date, " _
                    & "  a.ip_end_date, " _
                    & "  a.ip_is_active, " _
                    & "  a.ip_add_by, " _
                    & "  a.ip_add_date, " _
                    & "  a.ip_upd_by, " _
                    & "  a.ip_upd_date " _
                    & "FROM " _
                    & "  public.ipcst_mstr a " _
                    & "  INNER JOIN public.si_mstr c ON (a.ip_si_id = c.si_id) " _
                    & "  INNER JOIN public.en_mstr d ON (a.ip_en_id = d.en_id) " _
                    & "  INNER JOIN public.dom_mstr b ON (a.ip_dom_id = b.dom_id) " _
                    & "  INNER JOIN public.ptnr_mstr cust_mstr ON (a.ip_cust_id = cust_mstr.ptnr_id) " _
                    & "  LEFT OUTER JOIN public.ptnr_mstr vend_mstr ON (a.ip_vendor_id = vend_mstr.ptnr_id) " _
                    & "  LEFT OUTER JOIN public.pt_mstr e ON (a.ip_pt_id = e.pt_id)" _
                    & " where ip_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and ip_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and ip_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ip_si_id.ItemIndex = 0
        ip_en_id.Focus()
        ip_date.DateTime = _now
        ip_pt_id.Text = ""
        ip_cost.EditValue = 0.0
        _pt_id = -1

        ip_date.DateTime = _now
        ip_start_date.DateTime = _now
        ip_end_date.DateTime = _now

        ce_ip_active.Checked = True
        ip_cost.Properties.ReadOnly = True
        ip_price.Properties.ReadOnly = True

    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim _date As Date = ip_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(ip_en_id.EditValue, "gcald_ic", _date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If _pt_ls.ToUpper = "S" Then
        '    If ip_qty.EditValue > 1 Then
        '        MessageBox.Show("Qty Can't Higher Than 1 For Serial Item..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If _pt_ls.ToUpper = "S" Then
        '    If Trim(ip_lot_serial.Text) = "" Then
        '        MessageBox.Show("Serial Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If _pt_ls.ToUpper = "L" Then
        '    If Trim(ip_lot_serial.Text) = "" Then
        '        MessageBox.Show("Lot Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If ip_type.EditValue.ToString.ToUpper = "I" Then
        '    If pt_desc1.EditValue < 1 Then
        '        MessageBox.Show("Qty Can't Lower Than 1..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ip_qty.Focus()
        '        Return False
        '    End If
        'End If

        Return before_save
    End Function

    'Private Sub ip_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    pt_desc1.EditValue = ip_qty.EditValue * ip_um_conv.EditValue
    'End Sub

    'Private Sub ip_um_conv_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pt_desc2.EditValueChanged
    '    pt_desc1.EditValue = ip_qty.EditValue * ip_um_conv.EditValue
    'End Sub

    'Private Sub ip_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ip_pt_id.ButtonClick
    '    Dim frm As New FPartNumberSearch()
    '    frm.set_win(Me)
    '    frm._en_id = ip_en_id.EditValue
    '    frm._si_id = ip_si_id.EditValue
    '    frm._obj = ip_pt_id
    '    frm.type_form = True
    '    frm.ShowDialog()
    'End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _invct_oid_mstr = .Item("invct_oid").ToString
                ip_si_id.EditValue = .Item("invct_si_id")
                _pt_id = .Item("invct_pt_id")
                ip_pt_id.EditValue = .Item("pt_code")
                ip_cost.EditValue = .Item("invct_cost")
            End With
            ip_si_id.Focus()
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
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.invct_table   " _
                                            & "SET  " _
                                            & "  invct_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  invct_pt_id = " & SetInteger(_pt_id) & ",  " _
                                            & "  invct_cost = " & SetDec(ip_cost.EditValue) & ",  " _
                                            & "  invct_si_id = " & SetInteger(ip_si_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  invct_oid = " & SetSetring(_invct_oid_mstr) & " "
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
                        set_row(_invct_oid_mstr.ToString, "invct_oid")
                        edit = True
                    Catch ex As NpgsqlException
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
        'MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                        Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from invct_table where invct_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invct_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Item Site Cost " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code"))
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
                        Catch ex As NpgsqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Private Sub ip_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ip_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._obj = ip_pt_id
        frm._en_id = ip_en_id.EditValue
        frm._is_best = ce_ip_best.EditValue
        frm._is_internal = ce_ip_is_internal.EditValue
        frm._ptnr_id = ip_vendor_id.Tag
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub ip_vendor_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ip_vendor_id.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = ip_en_id.EditValue
        frm._is_internal = ce_ip_is_internal.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    'Private Sub so_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles so_taxable.CheckedChanged
    '    If so_taxable.EditValue = False Then
    '        so_tax_class.Enabled = False
    '        so_tax_class.ItemIndex = 0
    '        so_tax_inc.Enabled = False
    '        so_tax_inc.Checked = False
    '    Else
    '        so_tax_class.Enabled = True
    '        so_tax_inc.Enabled = True
    '        so_tax_inc.Checked = False
    '    End If
    'End Sub

    Private Sub ce_ip_best_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_ip_best.CheckedChanged
        If ce_ip_best.Checked = True Then
            'ce_ip_is_internal.Enabled = True
            ce_ip_is_internal.Properties.ReadOnly = True
            ce_ip_is_internal.Checked = False
            ce_ip_is_internal.Enabled = False

            ip_vendor_id.Text = ""
            ip_vendor_id.Enabled = False
            ip_vendor_id.Properties.ReadOnly = True
            'so_tax_inc.Checked = False
        Else
            ip_vendor_id.Enabled = True
            ip_vendor_id.Properties.ReadOnly = False
            ce_ip_is_internal.Enabled = True
            ce_ip_is_internal.Properties.ReadOnly = False
        End If
    End Sub
    Private Sub ip_cost_EditValueChanged(sender As Object, e As EventArgs) Handles ip_cost.EditValueChanged
        HitungHarga()
    End Sub

    Private Sub ip_margin_pct_EditValueChanged(sender As Object, e As EventArgs) Handles ip_margin_pct.EditValueChanged
        HitungHarga()
    End Sub

    Private Sub HitungHarga()
        Dim cost As Double = 0
        Dim margin As Double = 0

        ' Ambil nilai cost
        Double.TryParse(ip_cost.Text.Replace(",", ""), cost)

        ' Ambil nilai margin tanpa %
        Dim marginStr = ip_margin_pct.Text.Replace("%", "").Replace(",", "").Trim()
        Double.TryParse(marginStr, margin)

        ' Ubah ke desimal (contoh 5% menjadi 0.05)
        margin /= 100

        ' Hitung harga
        Dim price As Double = cost * (1 + margin)

        ' Tampilkan di txtPrice dengan format uang
        ip_price.Text = Format(price, "#,##0.00")
    End Sub

    Private Sub file_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_excel.ButtonClick
        Try
            Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")
            If _file = "" Then Exit Sub

            file_excel.EditValue = _file
            Using ds As DataSet = master_new.excelconn.ImportExcel(_file)
                'ds_edit.Tables(0).Rows.Clear()
                'ds_edit.AcceptChanges()
                'gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                'Dim _row As Integer = 0
                'For Each dr As DataRow In ds.Tables(0).Rows
                '    Dim ssql As String = "SELECT  distinct " _
                '                   & "  en_id, " _
                '                   & "  en_desc, " _
                '                   & "  si_desc, " _
                '                   & "  pt_id, " _
                '                   & "  pt_code, " _
                '                   & "  pt_desc1, " _
                '                   & "  pt_desc2, " _
                '                   & "  pt_cost, " _
                '                   & "  invct_cost, " _
                '                   & "  pt_price, " _
                '                   & "  pt_type, " _
                '                   & "  pt_um, " _
                '                   & "  pt_pl_id, " _
                '                   & "  pt_ls, " _
                '                   & "  pt_loc_id, " _
                '                   & "  loc_desc, " _
                '                   & "  pt_taxable, " _
                '                   & "  pt_tax_inc, " _
                '                   & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                '                   & "  tax_class_mstr.code_name as tax_class_name, " _
                '                   & "  pt_ppn_type, " _
                '                   & "  um_mstr.code_name as um_name " _
                '                   & "FROM  " _
                '                   & "  public.pt_mstr" _
                '                   & " inner join en_mstr on en_id = pt_en_id " _
                '                   & " inner join loc_mstr on loc_id = pt_loc_id " _
                '                   & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                '                   & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                '                   & " inner join invct_table on invct_pt_id = pt_id " _
                '                   & " inner join si_mstr on si_id = invct_si_id " _
                '                   & " where pt_code ='" & dr("pt_code") & "' "

                '    Using dt_temp As DataTable = master_new.PGSqlConn.GetTableData(ssql)

                '        For Each dr_temp As DataRow In dt_temp.Rows
                '            gv_edit.AddNewRow()
                '            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
                '            gv_edit.SetRowCellValue(_row, "pt_id", dr_temp("pt_id"))
                '            gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                '            gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                '            gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                '            gv_edit.SetRowCellValue(_row, "ptsfrd_um", dr_temp("pt_um"))
                '            gv_edit.SetRowCellValue(_row, "ptsfrd_qty_open", dr("qty"))
                '            gv_edit.SetRowCellValue(_row, "ptsfrd_qty", dr("qty"))
                '            gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", dr_temp("um_name"))
                '            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                '            _row += 1
                '            'System.Windows.Forms.Application.DoEvents()
                '            If _row Mod 10 = 0 Then System.Windows.Forms.Application.DoEvents()
                '        Next
                '    End Using
                'Next
            End Using
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub btImportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btImportExcel.Click
        'Try
        '    Dim dscs As New DataSet
        '    dscs = master_new.excelconn.ImportExcel(AskOpenFile("Excel Files | *.xls"))

        '    Dim frm As New frmShowExcelData
        '    frm._dscs = dscs
        '    frm.Show()
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try
    End Sub
End Class
