Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FFakturPajak
    Dim ssql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _fp_ar_oid As String
    Dim _conf_value, _fp_oid_edit As String
    Public _fp_gnt_oid As String
    Public Shared PageNum2 As String

    Private Sub FFakturPajak_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        'If _conf_value = "0" Then
        '    dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        'Else
        '    dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        'End If
        ce_page_number.Checked = True
        ce_page_number.Checked = False
        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        fp_en_id.Properties.DataSource = dt_bantu
        fp_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        fp_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        fp_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_fp_status())
        fp_status.Properties.DataSource = dt_bantu
        fp_status.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        fp_status.Properties.ValueMember = dt_bantu.Columns("value").ToString
        fp_status.ItemIndex = 0

        'fp_unstrikeout.Properties.Items.Remove()
        fp_unstrikeout.Properties.Items.Add("Harga Jual")
        fp_unstrikeout.Properties.Items.Add("Penggantian")
        fp_unstrikeout.Properties.Items.Add("Uang Muka")
        fp_unstrikeout.Properties.Items.Add("Termin")

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            fp_tran_id.Properties.DataSource = dt_bantu
            fp_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            fp_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            fp_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            fp_tran_id.Properties.DataSource = dt_bantu
            fp_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            fp_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            fp_tran_id.ItemIndex = 0
        End If
    End Sub

    Private Sub fp_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fp_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer(fp_en_id.EditValue))
        fp_ptnr_id.Properties.DataSource = dt_bantu
        fp_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        fp_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        fp_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("fp_sign_user", fp_en_id.EditValue))
        fp_sign.Properties.DataSource = dt_bantu
        fp_sign.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        fp_sign.Properties.ValueMember = dt_bantu.Columns("code_name").ToString
        fp_sign.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Faktur Pajak Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Status", "fp_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Replacement", "fp_code_pengganti", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Status Approval", "fp_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Revisi", "fp_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unstrikeout", "fp_unstrikeout", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "fp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "fp_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "fp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "fp_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_ar, "fp_oid", False)
        add_column_copy(gv_ar, "AR Code", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_ar, "Eff. Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_ar, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_ar, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_ar, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_ar, "Taxable", "ars_taxable", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_ar, "Tax Include", "ars_tax_inc", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_ar, "Qty Invoice", "ars_invoice", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_ar, "Price Invoice", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_ar, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")

        add_column(gv_wf, "wf_ref_code", False)
        add_column(gv_wf, "wf_ref_oid", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "fp_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Faktur Pajak Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Faktur Pajak Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Faktur Pajak Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Status", "fp_status_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Status", "fp_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Revisi", "fp_rev", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "fp_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  fp_mstr.fp_oid, " _
                    & "  fp_mstr.fp_dom_id, " _
                    & "  fp_mstr.fp_en_id, " _
                    & "  fp_mstr.fp_add_by, " _
                    & "  fp_mstr.fp_add_date, " _
                    & "  fp_mstr.fp_upd_by, " _
                    & "  fp_mstr.fp_upd_date, " _
                    & "  fp_mstr.fp_code, " _
                    & "  fp_mstr.fp_pengali_tax, " _
                    & "  fp_mstr.fp_dt, " _
                    & "  fp_mstr.fp_date, " _
                    & "  fp_mstr.fp_sign, " _
                    & "  fp_mstr.fp_status, " _
                    & "  CASE WHEN fp_mstr.fp_status = '0' " _
                    & "      THEN 'Normal' " _
                    & "      ELSE 'Penggantian' " _
                    & "  END AS fp_status_desc , " _
                    & "  fp_mstr.fp_customer_type, " _
                    & "  fp_mstr.fp_area, " _
                    & "  fp_mstr.fp_ppn_type, " _
                    & "  fp_mstr.fp_tax_inc, " _
                    & "  fp_mstr.fp_ptnr_id, " _
                    & "  fp_mstr.fp_tran_id, " _
                    & "  fp_mstr.fp_trans_id, " _
                    & "  fp_mstr.fp_ar_oid, " _
                    & "  fp_mstr.fp_rev, " _
                    & "  fp_mstr.fp_unstrikeout, " _
                    & "  fp_mstr.fp_fp_oid, " _
                    & "  fm.fp_code as fp_code_pengganti, " _
                    & "  en_desc, " _
                    & "  ar_code, " _
                    & "  fp_mstr.fp_ptnr_addr_oid, " _
                    & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                    & "  coalesce(ptnr_name_alt,ptnr_name) as ptnr_name, " _
                    & "  ptnr_npwp, " _
                    & "  ptnr_nppkp " _
                    & "FROM  " _
                    & "  public.fp_mstr " _
                    & "  inner join ptnr_mstr on ptnr_id = fp_mstr.fp_ptnr_id " _
                    & "  inner join en_mstr on en_id = fp_mstr.fp_en_id " _
                    & "  inner join ar_mstr on fp_mstr.fp_ar_oid = ar_oid " _
                    & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                    & "  left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
                    & "  where fp_mstr.fp_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & "  and fp_mstr.fp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and fp_mstr.fp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  order by fp_mstr.fp_code"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("ar").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  fp_oid, " _
            & "  fp_ar_oid, " _
            & "  ars_oid " _
            & "  ars_ar_oid, " _
            & "  ars_seq, " _
            & "  ars_soshipd_oid, " _
            & "  ar_code, " _
            & "  ar_eff_date, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  ars_taxable, " _
            & "  ars_tax_class_id, " _
            & "  ars_tax_inc, " _
            & "  ars_invoice, " _
            & "  ars_invoice_price, " _
            & "  sod_disc " _
            & "FROM  " _
            & "  public.fp_mstr " _
            & "  inner join ar_mstr on ar_oid = fp_ar_oid " _
            & "  inner join ars_ship on ars_ar_oid = ar_oid " _
            & "  inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id" _
            & " where fp_date >= " + SetDate(pr_txttglawal.DateTime) _
            & " and fp_date <= " + SetDate(pr_txttglakhir.DateTime) _
            & "  " _
            & " and fp_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by ar_code, sod_seq "
        load_data_detail(sql, gc_ar, "ar")
        gv_ar.BestFitColumns()

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join fp_mstr on fp_oid = wf_ref_oid " _
                & " where fp_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and fp_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and fp_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  fp_mstr.fp_oid, " _
                    & "  fp_mstr.fp_dom_id, " _
                    & "  fp_mstr.fp_en_id, " _
                    & "  fp_mstr.fp_add_by, " _
                    & "  fp_mstr.fp_add_date, " _
                    & "  fp_mstr.fp_upd_by, " _
                    & "  fp_mstr.fp_upd_date, " _
                    & "  fp_mstr.fp_code, " _
                    & "  fp_mstr.fp_pengali_tax, " _
                    & "  fp_mstr.fp_dt, " _
                    & "  fp_mstr.fp_date, " _
                    & "  fp_mstr.fp_sign, " _
                    & "  fp_mstr.fp_status, " _
                    & "  CASE WHEN fp_mstr.fp_status = '0' " _
                    & "      THEN 'Normal' " _
                    & "      ELSE 'Penggantian' " _
                    & "  END AS fp_status_desc , " _
                    & "  fp_mstr.fp_customer_type, " _
                    & "  fp_mstr.fp_area, " _
                    & "  fp_mstr.fp_ppn_type, " _
                    & "  fp_mstr.fp_tax_inc, " _
                    & "  fp_mstr.fp_ptnr_id, " _
                    & "  fp_mstr.fp_tran_id, " _
                    & "  fp_mstr.fp_trans_id, " _
                    & "  fp_mstr.fp_ar_oid, " _
                    & "  fp_mstr.fp_rev, " _
                    & "  fp_mstr.fp_unstrikeout, " _
                    & "  fp_mstr.fp_fp_oid, " _
                    & "  fm.fp_code as fp_code_pengganti, " _
                    & "  en_desc, " _
                    & "  ar_code, " _
                    & "  fp_mstr.fp_ptnr_addr_oid, " _
                    & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                    & "  ptnr_name, " _
                    & "  ptnr_npwp, " _
                    & "  ptnr_nppkp " _
                    & "FROM  " _
                    & "  public.fp_mstr " _
                    & "  inner join ptnr_mstr on ptnr_id = fp_mstr.fp_ptnr_id " _
                    & "  inner join en_mstr on en_id = fp_mstr.fp_en_id " _
                    & "  inner join ar_mstr on fp_mstr.fp_ar_oid = ar_oid " _
                    & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                    & "  left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
                    & "  where fp_mstr.fp_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & "  and fp_mstr.fp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and fp_mstr.fp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  order by fp_mstr.fp_code"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select fp_oid, fp_code, fp_trans_id, false as status from fp_mstr " _
                & " where fp_trans_id ~~* 'd' "

            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_ar.Columns("fp_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[fp_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString & "'")
            gv_ar.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("fp_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[fp_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString & "'")
                gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Overrides Sub insert_data_awal()
        fp_en_id.ItemIndex = 0
        fp_date.DateTime = _now
        fp_ptnr_id.ItemIndex = 0
        fp_customer_type.Text = "01"
        fp_tran_id.ItemIndex = 0
        fp_ptnr_addr_oid.ItemIndex = 0
        fp_status.ItemIndex = 0
        fp_sign.ItemIndex = 0
        fp_area.Text = "000"
        fp_code.Text = ""
        fp_ar_oid2.Text = ""
        fp_fp_code.Enabled = False

        fp_ar_oid2.Enabled = True
        fp_customer_type.Enabled = True
        fp_status.Enabled = True
        fp_area.Enabled = True
        fp_code.Enabled = True
        fp_status.Enabled = True

        _fp_gnt_oid = ""

        fp_en_id.Focus()
    End Sub

    Private Sub fp_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles fp_code.ButtonClick
        If Trim(fp_customer_type.Text) = "" Then
            MessageBox.Show("Customer Type Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf Trim(fp_area.Text) = "" Then
            MessageBox.Show("Area Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim _tanggal As Date
        Dim _tahun, _fp_code_full, _cmaddr_code_cabang, _fp_code As String
        Dim _no_urut As Integer
        _tanggal = func_coll.get_tanggal_sistem()
        _tahun = _tanggal.Year.ToString.Substring(2, 2)
        _fp_code_full = ""
        _fp_code = ""
        _cmaddr_code_cabang = Trim(fp_area.Text)

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "Select COALESCE(MAX(CAST(SUBSTRING(fp_code,12,100) AS INTEGER)),0) + 1 as no_urut " _
                         & " from fp_mstr " _
                         & " where SUBSTRING(fp_code,9,2) = '" + _tahun + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    _no_urut = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        _fp_code = Trim(fp_customer_type.Text) + fp_status.EditValue.ToString + "." + _cmaddr_code_cabang + "-"
        If Len(_no_urut.ToString) = 1 Then
            _fp_code_full = _fp_code + _tahun + "." + "0000000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 2 Then
            _fp_code_full = _fp_code + _tahun + "." + "000000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 3 Then
            _fp_code_full = _fp_code + _tahun + "." + "00000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 4 Then
            _fp_code_full = _fp_code + _tahun + "." + "0000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 5 Then
            _fp_code_full = _fp_code + _tahun + "." + "000" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 6 Then
            _fp_code_full = _fp_code + _tahun + "." + "00" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 7 Then
            _fp_code_full = _fp_code + _tahun + "." + "0" + _no_urut.ToString.ToString
        ElseIf Len(_no_urut.ToString) = 8 Then
            _fp_code_full = _fp_code + _tahun + "." + _no_urut.ToString.ToString
        End If

        fp_code.Text = _fp_code_full
    End Sub

    Private Sub fp_ar_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles fp_ar_oid2.ButtonClick
        Dim frm As New FInvoiceSearch
        frm.set_win(Me)
        frm._en_id = fp_en_id.EditValue
        frm._obj = fp_ar_oid2
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Function before_save_insert() As Boolean
        before_save_insert = True

        Dim _jml As Integer = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(fp_code) as jml from fp_mstr " + _
                                           " where fp_code = " + SetSetring(fp_code.Text) + _
                                           " and fp_trans_id <> 'X' "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader.Item("jml")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _jml > 0 Then
            MessageBox.Show("Duplicate Faktur Pajak Code...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save_insert
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        If fp_fp_code.Text = "" Then
            If fp_status.EditValue = 1 Then
                MsgBox("Faktur Pajak Replacement Is Empty", MsgBoxStyle.Critical, "Warning")
                before_save = False
            End If
        End If

        Return before_save
    End Function

    Private Function get_rev() As Integer
        get_rev = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(fp_code) as jml from fp_mstr " + _
                                           " where fp_code ~~* " + SetSetring(fp_code.Text) + _
                                           " and fp_trans_id = 'X' "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_rev = .DataReader.Item("jml")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Return get_rev
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_insert() = False Then
            Exit Function
        End If

        Dim ds_bantu As New DataSet
        Dim _fp_oid As Guid

        _fp_oid = Guid.NewGuid
        Dim i As Integer

        Dim _fp_trans_id As String

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(fp_tran_id.EditValue)
            _fp_trans_id = "D"
        Else
            _fp_trans_id = "I"
        End If

        If _fp_gnt_oid = "" Then
            _fp_gnt_oid = "null"
        Else
            _fp_gnt_oid = "'" & _fp_gnt_oid & "'"
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.fp_mstr " _
                                            & "( " _
                                            & "  fp_oid, " _
                                            & "  fp_dom_id, " _
                                            & "  fp_en_id, " _
                                            & "  fp_add_by, " _
                                            & "  fp_add_date, " _
                                            & "  fp_code, " _
                                            & "  fp_pengali_tax, " _
                                            & "  fp_dt, " _
                                            & "  fp_date, " _
                                            & "  fp_sign, " _
                                            & "  fp_status, " _
                                            & "  fp_customer_type, " _
                                            & "  fp_area, " _
                                            & "  fp_ppn_type, " _
                                            & "  fp_ptnr_id, " _
                                            & "  fp_ptnr_addr_oid, " _
                                            & "  fp_tax_inc, " _
                                            & "  fp_trans_id, " _
                                            & "  fp_tran_id, " _
                                            & "  fp_rev, " _
                                            & "  fp_unstrikeout, " _
                                            & "  fp_ar_oid, " _
                                            & "  fp_fp_oid " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_fp_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(fp_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetSetring(fp_code.Text) & ",  " _
                                            & SetDbl(100 / 100) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetDate(fp_date.DateTime) & ",  " _
                                            & SetSetring(fp_sign.Text) & ",  " _
                                            & SetSetring(fp_status.EditValue) & ",  " _
                                            & SetSetring(fp_customer_type.Text) & ",  " _
                                            & SetSetring(fp_area.Text) & ",  " _
                                            & SetSetring("A") & ",  " _
                                            & SetInteger(fp_ptnr_id.EditValue) & ",  " _
                                            & SetSetring(fp_ptnr_addr_oid.EditValue) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & SetSetring(_fp_trans_id) & ",  " _
                                            & SetInteger(fp_tran_id.EditValue) & ",  " _
                                            & SetInteger(get_rev) & ",  " _
                                            & SetSetring(fp_unstrikeout.Text) & ",  " _
                                            & SetSetring(_fp_ar_oid) & ",  " _
                                            & _fp_gnt_oid & "  " _
                                            & ")"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If _conf_value = "1" Then
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
                                                & SetInteger(fp_en_id.EditValue) & ",  " _
                                                & SetSetring(fp_tran_id.EditValue) & ",  " _
                                                & SetSetring(_fp_oid.ToString) & ",  " _
                                                & SetSetring(fp_code.Text) & ",  " _
                                                & SetSetring("Faktur Pajak") & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If


                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_fp_oid.ToString), "fp_oid")
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
        If _conf_value <> "0" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_code")) > 0 Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                Else
                    MessageBox.Show("Can't Edit Data..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        End If

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _fp_oid_edit = SetString(.Item("fp_oid"))
                fp_en_id.EditValue = .Item("fp_en_id")
                _fp_ar_oid = .Item("fp_ar_oid").ToString
                fp_ar_oid2.Text = .Item("ar_code")
                fp_date.DateTime = .Item("fp_date")
                fp_sign.EditValue = SetString(.Item("fp_sign"))
                fp_ptnr_id.EditValue = .Item("fp_ptnr_id")
                fp_ptnr_addr_oid.EditValue = .Item("fp_ptnr_addr_oid").ToString
                fp_customer_type.Text = .Item("fp_customer_type")
                fp_status.EditValue = .Item("fp_status")

                If IsDBNull(.Item("fp_fp_oid")) = False Then
                    _fp_gnt_oid = Trim(.Item("fp_fp_oid").ToString)
                    fp_fp_code.Text = Trim(.Item("fp_code_pengganti"))
                End If

                fp_area.Text = .Item("fp_area")
                fp_code.Text = .Item("fp_code")
                fp_unstrikeout.EditValue = .Item("fp_unstrikeout")
                fp_tran_id.EditValue = .Item("fp_tran_id")
            End With

            fp_ar_oid2.Enabled = False
            fp_customer_type.Enabled = False
            fp_status.Enabled = False
            fp_area.Enabled = False
            fp_code.Enabled = False
            fp_status.Enabled = False
            fp_en_id.Focus()

            edit_data = True
            fp_ptnr_id.Enabled = True
        End If

    End Function

    Public Overrides Function edit()
        edit = True

        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")
        Dim i As Integer

        'Cari total ammount
        Dim ds_bantu As New DataSet
        Dim _fp_trn_id As Integer
        Dim _fp_trn_satatus As String
        '=============================================================================

        _fp_trn_id = SetNumber(fp_tran_id.EditValue)
        'Return False
        'Exit Function
        _fp_trn_satatus = "D" 'set default langsung ke D

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(_fp_trn_id)
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.fp_mstr   " _
                                            & "SET  " _
                                            & "  fp_en_id = " & SetSetring(fp_en_id.EditValue) & ",  " _
                                            & "  fp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  fp_upd_date = " & "(select current_timestamp)" & ",  " _
                                            & "  fp_date = " & SetDate(fp_date.DateTime) & ",  " _
                                            & "  fp_sign = " & SetSetring(fp_sign.Text) & ",  " _
                                            & "  fp_ptnr_id = " & SetInteger(fp_ptnr_id.EditValue) & ",  " _
                                            & "  fp_unstrikeout = " & SetSetring(fp_unstrikeout.EditValue) & ",  " _
                                            & "  fp_tran_id = " & SetInteger(fp_tran_id.EditValue) & ",  " _
                                            & "  fp_trans_id = " & SetSetring(_fp_trn_satatus) & ",  " _
                                            & "  fp_dt = current_timestamp" _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  fp_oid = '" & _fp_oid_edit & "' "

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        '================================================================
                        If _conf_value = "1" Then

                            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_tran_id") <> fp_tran_id.EditValue Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _fp_oid_edit.ToString + "'"
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
                                                            & SetInteger(fp_en_id.EditValue) & ",  " _
                                                            & SetSetring(fp_tran_id.EditValue) & ",  " _
                                                            & SetSetring(_fp_oid_edit.ToString) & ",  " _
                                                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_code")) & ",  " _
                                                            & SetSetring("Faktur Pajak") & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                            & SetInteger(0) & ",  " _
                                                            & SetSetring("N") & ",  " _
                                                            & " current_timestamp " & "  " _
                                                            & ")"
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            ElseIf func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_code")) > 0 Then

                            End If
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(Trim(_fp_oid_edit.ToString), "fp_oid")
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

        'Dim i As Integer

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
                            .Command.CommandText = "delete from fp_mstr where fp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString + "'"
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

    Private Sub fp_ptnr_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fp_ptnr_id.EditValueChanged
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_name as address_type, (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, ptnra_oid " + _
                           " from ptnra_addr inner join code_mstr on code_id = ptnra_addr_type " + _
                           " where ptnra_ptnr_oid = '" + fp_ptnr_id.GetColumnValue("ptnr_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "address")
                    fp_ptnr_addr_oid.Properties.DataSource = ds_bantu.Tables("address")
                    fp_ptnr_addr_oid.Properties.DisplayMember = ds_bantu.Tables("address").Columns("address").ToString
                    fp_ptnr_addr_oid.Properties.ValueMember = ds_bantu.Tables("address").Columns("ptnra_oid").ToString
                    fp_ptnr_addr_oid.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fp_status_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fp_status.EditValueChanged
        If fp_status.EditValue = 1 Then
            fp_fp_code.Enabled = True
        ElseIf fp_status.EditValue = 0 Then
            fp_fp_code.Enabled = False
            fp_fp_code.Text = ""
            _fp_gnt_oid = ""
        End If
        _fp_gnt_oid = ""
    End Sub

    Private Sub fp_fp_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles fp_fp_code.ButtonClick
        Dim frm As New FFakturPajakSearch
        frm.set_win(Me)
        frm._en_id = fp_en_id.EditValue
        frm._ptnr_id = fp_ptnr_id.EditValue
        frm._obj = fp_fp_code
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString
        _colom = "fp_trans_id"
        _table = "fp_mstr"
        _criteria = "fp_code"
        _initial = "fp"
        _type = "fp"
        _title = "Faktur Pajak"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
        'par_initial contohnya pby
        'par_type contohnya dr
        Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String
        If mf.get_transaction_status_by_oid(par_colom, par_table, "fp_oid", par_oid) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        _pby_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
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
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '============================================================================
                        If func_coll.get_status_wf(par_code.ToString()) = 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                   " where wf_ref_oid = '" + par_oid + "'" + _
                                                   " and wf_seq = 0"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_oid = '" + par_oid + "'"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        ElseIf func_coll.get_status_wf(par_code.ToString()) > 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set " + _
                                                   " wf_iscurrent = 'Y', " + _
                                                   " wf_wfs_id = '0', " + _
                                                   " wf_desc = '', " + _
                                                   " wf_date_to = null, " + _
                                                   " wf_aprv_user = '', " + _
                                                   " wf_aprv_date = null " + _
                                                   " where wf_ref_oid = '" + par_oid + "'" + _
                                                   " and wf_wfs_id = '4' "
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '============================================================================

                        sqlTran.Commit()

                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        filename = "c:\syspro\" + par_code + ".xls"
                        ExportTo(par_gv, New ExportXlsProvider(filename))

                        If user_wf_email <> "" Then
                            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        Else
                            'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
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

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString
        _colom = "fp_trans_id"
        _table = "fp_mstr"
        _criteria = "fp_code"
        _initial = "fp"
        _type = "fp"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where fp_oid = '" + par_oid + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
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
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where fp_oid = '" + par_oid + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_oid = '" + par_oid + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
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

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_code")
        _type = "fp"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Faktur Pajak"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("fp_oid").FilterInfo = _
                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[fp_oid] = '" & ds.Tables("smart").Rows(i).Item("fp_oid").ToString & "'")
                    gv_email.BestFitColumns()
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("fp_code"), 0)
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
                                .Command.CommandText = "update fp_mstr set fp_trans_id = '" + _trans_id + "'," + _
                                               " fp_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " fp_upd_date = current_timestamp " + _
                                               " where fp_oid = '" + ds.Tables("smart").Rows(i).Item("fp_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                '============================================================================
                                If func_coll.get_status_wf(ds.Tables("smart").Rows(i).Item("fp_code")) = 0 Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                           " where wf_ref_code = '" + ds.Tables("smart").Rows(i).Item("fp_oid").ToString + "'" + _
                                                           " and wf_seq = 0"

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()


                                    'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                           " where wf_ref_oid = '" + ds.Tables("smart").Rows(i).Item("fp_oid").ToString + "'"

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                ElseIf func_coll.get_status_wf(ds.Tables("smart").Rows(i).Item("fp_code")) > 0 Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set " + _
                                                           " wf_iscurrent = 'Y', " + _
                                                           " wf_wfs_id = '0', " + _
                                                           " wf_desc = '', " + _
                                                           " wf_date_to = null, " + _
                                                           " wf_aprv_user = '', " + _
                                                           " wf_aprv_date = null " + _
                                                           " where wf_ref_oid = '" + ds.Tables("smart").Rows(i).Item("fp_oid").ToString + "'" + _
                                                           " and wf_wfs_id = '4' "
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                                '============================================================================

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("fp_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("fp_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Faktur Pajak", ds.Tables("smart").Rows(i).Item("fp_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "true as print_comma, " _
            & "true as print_detail, " _
            & "fp_mstr.fp_oid, " _
            & "fp_mstr.fp_dom_id, " _
            & "fp_mstr.fp_en_id, " _
            & "fp_mstr.fp_add_by, " _
            & "fp_mstr.fp_add_date, " _
            & "fp_mstr.fp_upd_by, " _
            & "fp_mstr.fp_upd_date, " _
            & "fp_mstr.fp_code, " _
            & "fp_mstr.fp_pengali_tax, " _
            & "fp_mstr.fp_dt, " _
            & "fp_mstr.fp_date, " _
            & "fp_mstr.fp_sign, " _
            & "fp_mstr.fp_status, " _
            & "fp_mstr.fp_customer_type, " _
            & "fp_mstr.fp_area, " _
            & "fp_mstr.fp_ppn_type, " _
            & "fp_mstr.fp_ptnr_id, " _
            & "fp_mstr.fp_tax_inc, " _
            & "fp_mstr.fp_ar_oid , " _
            & "fp_mstr.fp_unstrikeout , " _
            & "fm.fp_code as fp_code_pengganti, " _
            & "fm.fp_date as fp_date_pengganti, " _
            & "fp_mstr.fp_trans_id, " _
            & "ar_code, " _
            & "ar_date,  " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_npwp, " _
            & "cmaddr_pkp_date, " _
            & "coalesce(ptnr_name_alt,ptnr_name) as ptnr_name, " _
            & "ptnr_npwp, " _
            & "ptnr_nppkp, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ar_cu_id, " _
            & "ars_invoice, " _
            & "ars_invoice_price, " _
            & "ars_tax_class_id, " _
            & "sod_disc, " _
            & "ar_exc_rate, " _
            & "ar_credit_term, " _
            & "cu_code, " _
            & "credit_terms_mstr.code_name as top_name, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "pt_desc2, " _
            & "sod_seq, " _
            & "(select conf_value from conf_file where conf_name = 'faktur_pajak_city') as faktur_pajak_city, " _
            & "ars_tax_inc, " _
            & "ars_invoice, " _
            & "ars_invoice_price, " _
            & "sod_disc, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (ars_invoice_price * ars_invoice) " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) " _
            & "END AS price_ext_idr,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (ars_invoice_price * ars_invoice) " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) " _
            & "END AS price_ext_usd,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ars_invoice * ars_invoice_price * sod_disc " _
            & "WHEN 'Y' THEN ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc) " _
            & "END AS disc_value_idr,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ars_invoice * ars_invoice_price * sod_disc " _
            & "WHEN 'Y' THEN ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc) " _
            & "END AS disc_value_usd,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc)) " _
            & "END AS dpp_value_idr, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc)) " _
            & "END AS dpp_value_usd, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate) * 0.1 " _
            & "WHEN 'Y' THEN ((ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc))) * 0.1 " _
            & "END AS ppn_idr, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate) * 0.1 " _
            & "WHEN 'Y' THEN ((ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc))) * 0.1 " _
            & "END AS ppn_usd " _
            & "FROM  " _
            & "fp_mstr " _
            & "inner join ar_mstr on ar_oid = fp_mstr.fp_ar_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "inner join ptnra_addr on ptnra_oid = fp_ptnr_addr_oid " _
            & "inner join ars_ship on ars_ar_oid = ar_oid " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join code_mstr credit_terms_mstr on credit_terms_mstr.code_id = ar_credit_term  " _
            & "left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "where fp_mstr.fp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString + "'" _
            & "  order by fp_mstr.fp_code, sod_seq "

        Dim rpt As Object = Nothing

        'If ce_blank.Checked = True Then
        rpt = New XRFakturPajakFormPlain
        'Else
        'rpt = New XRFakturPajakForm
        'End If



        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub ce_page_number_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_page_number.CheckedChanged
        XRFakturPajakFormPlain.StatusForm = "1"
        'XRFakturPajakForm.StatusFormPajak = "1"
        If ce_page_number.Checked = True Then
            PageNum2 = "Y"
        ElseIf ce_page_number.Checked = False Then
            PageNum2 = "N"
        End If
    End Sub
End Class
