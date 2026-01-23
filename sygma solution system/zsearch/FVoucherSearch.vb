Imports master_new.ModFunction

Public Class FVoucherSearch

    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FVoucherSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Voucher Date", "ap_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Total", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FPaymentManualChecks" Then
            get_sequel = "SELECT  " _
                & "  public.ap_mstr.ap_oid, " _
                & "  public.ap_mstr.ap_en_id, " _
                & "  public.ap_mstr.ap_code, " _
                & "  public.ap_mstr.ap_date, " _
                & "  public.ap_mstr.ap_ptnr_id, " _
                & "  public.ap_mstr.ap_cu_id, " _
                & "  public.ap_mstr.ap_bk_id, " _
                & "  public.ap_mstr.ap_type, " _
                & "  public.ap_mstr.ap_amount, " _
                & "  public.ap_mstr.ap_pay_amount, " _
                & "  public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount as ap_outstanding, " _
                & "  public.ap_mstr.ap_status, " _
                & "  public.ap_mstr.ap_invoice, " _
                & "  public.ap_mstr.ap_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.bk_mstr.bk_name, " _
                & "  public.ap_mstr.ap_ap_ac_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ap_mstr.ap_ap_sb_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.ap_mstr.ap_ap_cc_id, " _
                & "  public.cc_mstr.cc_desc, ap_exc_rate " _
                & "FROM " _
                & "  public.ap_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ap_mstr.ap_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.ap_mstr.ap_bk_id = public.bk_mstr.bk_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ap_mstr.ap_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ap_mstr.ap_ap_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ap_mstr.ap_ap_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ap_mstr.ap_ap_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ap_type ON (public.ap_mstr.ap_type = ap_type.code_id) " _
                & " where coalesce(ap_status,'') = '' " _
                & " and ap_trans_id  ~~* 'A' " _
                & " and ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ap_en_id = " + _en_id.ToString _
                & " and ap_ptnr_id = " + _ptnr_id.ToString

            'by sys 20110419 
            'usd hanya bisa dibayar usd
            If _cu_id <> master_new.ClsVar.ibase_cur_id Then
                get_sequel = get_sequel + " and ap_cu_id = " + _cu_id.ToString
            End If
        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        Dim _exr_cu_rate_1 As Double = 0
        If _cu_id = master_new.ClsVar.ibase_cur_id Then
            _exr_cu_rate_1 = 1
        Else
            '_exr_cu_rate_1 = func_data.get_exchange_rate(ds.Tables(0).Rows(_row_gv).Item("ap_cu_id").ToString)
            _exr_cu_rate_1 = func_data.get_exchange_rate(_cu_id)
        End If
        

        Dim ds_bantu As New DataSet
        If fobject.name = "FPaymentManualChecks" Then
            fobject.gv_edit.SetRowCellValue(_row, "appayd_ap_ref", ds.Tables(0).Rows(_row_gv).Item("ap_code"))
            fobject.gv_edit.SetRowCellValue(_row, "appayd_ap_oid", ds.Tables(0).Rows(_row_gv).Item("ap_oid").ToString)
            fobject.gv_edit.SetRowCellValue(_row, "appayd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ap_ap_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "appayd_sb_id", ds.Tables(0).Rows(_row_gv).Item("ap_ap_sb_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds.Tables(0).Rows(_row_gv).Item("sb_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "appayd_cc_id", ds.Tables(0).Rows(_row_gv).Item("ap_ap_cc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds.Tables(0).Rows(_row_gv).Item("cc_desc"))

            If ds.Tables(0).Rows(_row_gv).Item("ap_cu_id") = _cu_id Then
                fobject.gv_edit.SetRowCellValue(_row, "appayd_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ap_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ap_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("ap_cu_id") <> master_new.ClsVar.ibase_cur_id And _
               _cu_id <> master_new.ClsVar.ibase_cur_id Then
                fobject.gv_edit.SetRowCellValue(_row, "appayd_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ap_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ap_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("ap_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                'fobject.gv_edit.SetRowCellValue(_row, "appayd_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding") * _exr_cu_rate_1)
                'fobject.gv_edit.SetRowCellValue(_row, "appayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding") * _exr_cu_rate_1)
                'fobject.gv_edit.SetRowCellValue(_row, "appayd_exc_rate", _exr_cu_rate_1)
                'fobject.gv_edit.SetRowCellValue(_row, "ap_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ap_exc_rate"))

                'fobject.gv_edit.SetRowCellValue(_row, "appayd_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding") * _exr_cu_rate_1)
                'fobject.gv_edit.SetRowCellValue(_row, "appayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding") * _exr_cu_rate_1)
                'tidak usah dikalikan kurs 20110419 by sys
                fobject.gv_edit.SetRowCellValue(_row, "appayd_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ap_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ap_exc_rate"))
            Else
                fobject.gv_edit.SetRowCellValue(_row, "appayd_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "appayd_exc_rate", 1)
                fobject.gv_edit.SetRowCellValue(_row, "ap_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ap_exc_rate"))
            End If

            fobject.gv_edit.SetRowCellValue(_row, "ap_cu_id", ds.Tables(0).Rows(_row_gv).Item("ap_cu_id"))
            fobject.gv_edit.SetRowCellValue(_row, "appayd_disc_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "appayd_cur_amount", ds.Tables(0).Rows(_row_gv).Item("ap_outstanding"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
