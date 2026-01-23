Imports master_new.ModFunction

Public Class FFakturPajakSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Public _ppn_type, _so_cash As String
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FFakturPajakSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = FTaxInvoice.Name Or fobject.name = FTaxInvoicePrint.Name Or fobject.name = FTaxInvoiceAttachmentPrint.Name Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Invoice Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Invoice Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Tax Invoice Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Invoice Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        End If
        
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FFakturPajakPrint" Then
            get_sequel = "SELECT  " _
                    & "  fp_oid, " _
                    & "  fp_dom_id, " _
                    & "  fp_en_id, " _
                    & "  fp_code, " _
                    & "  fp_date, " _
                    & "  fp_sign,   " _
                    & "  fp_status, " _
                    & "  fp_ppn_type, " _
                    & "  fp_ptnr_id, " _
                    & "  fp_tax_inc, " _
                    & "  en_desc, " _
                    & "  ar_code, " _
                    & "  ar_date, " _
                    & "  ar_cu_id, " _
                    & "  ptnr_name " _
                    & "FROM  " _
                    & "  public.fp_mstr " _
                    & "  inner join ar_mstr on ar_oid = fp_ar_oid " _
                    & "  inner join ptnr_mstr on ptnr_id = fp_ptnr_id " _
                    & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                    & "  inner join en_mstr on en_id = fp_en_id" _
                    & "  where fp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and fp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and fp_en_id = " + _en_id.ToString _
                    & "  and coalesce(fp_trans_id,'I') = 'I' " _
                    & "  and ar_cu_id = " & _cu_id

        ElseIf fobject.name = "FFakturPajak" Then
            get_sequel = "SELECT  " _
                    & "  fp_oid, " _
                    & "  fp_dom_id, " _
                    & "  fp_en_id, " _
                    & "  fp_code, " _
                    & "  fp_date, " _
                    & "  fp_sign,   " _
                    & "  fp_status, " _
                    & "  fp_ppn_type, " _
                    & "  fp_ptnr_id, " _
                    & "  fp_tax_inc, " _
                    & "  en_desc, " _
                    & "  ar_code, " _
                    & "  ar_date, " _
                    & "  ptnr_name " _
                    & "FROM  " _
                    & "  public.fp_mstr " _
                    & "  inner join ar_mstr on ar_oid = fp_ar_oid " _
                    & "  inner join ptnr_mstr on ptnr_id = fp_ptnr_id " _
                    & "  inner join en_mstr on en_id = fp_en_id" _
                    & "  where fp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and fp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and fp_trans_id = 'X' " _
                    & "  and fp_ptnr_id = " + _ptnr_id.ToString _
                    & "  and fp_en_id = " + _en_id.ToString _
                    & " ORDER BY fp_rev desc limit 1 "
        ElseIf fobject.name = FTaxInvoice.Name Then
            get_sequel = "SELECT  " _
                    & "  ti_oid, " _
                    & "  ti_dom_id, " _
                    & "  ti_en_id, " _
                    & "  ti_code, " _
                    & "  ti_date, " _
                    & "  ti_status, " _
                    & "  ti_ppn_type, " _
                    & "  ti_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  en_desc " _
                    & "FROM  " _
                    & "  public.ti_mstr " _
                    & "  inner join en_mstr on en_id = ti_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = ti_ptnr_id " _
                    & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and ti_trans_id = 'X' " _
                    & "  and ti_ppn_type = '" + _ppn_type + "'" _
                    & "  and ti_ptnr_id = " + _ptnr_id.ToString _
                    & "  and ti_en_id = " + _en_id.ToString _
                    & " ORDER BY ti_rev desc limit 1 "
        ElseIf fobject.name = FTaxInvoicePrint.Name Then
            get_sequel = "SELECT  " _
                & "  ti_oid, " _
                & "  ti_dom_id, " _
                & "  ti_en_id, " _
                & "  ti_code, " _
                & "  ti_date, " _
                & "  ti_status, " _
                & "  ti_ppn_type, " _
                & "  ti_ptnr_id, " _
                & "  ptnr_name, " _
                & "  en_desc " _
                & "FROM  " _
                & "  public.ti_mstr " _
                & "  inner join en_mstr on en_id = ti_en_id " _
                & "  inner join ptnr_mstr on ptnr_id = ti_ptnr_id " _
                & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and coalesce(ti_trans_id,'I') = 'I' " _
                & "  and ti_ppn_type = '" + _ppn_type + "'" _
                & "  and ti_cu_id = " + _cu_id.ToString _
                & "  and ti_en_id = " + _en_id.ToString _
                & "  and ti_so_cash = " + SetSetring(_so_cash) _
                & "  order by ti_code "
        ElseIf fobject.name = FTaxInvoiceAttachmentPrint.Name Then
            get_sequel = "SELECT  " _
                & "  ti_oid, " _
                & "  ti_dom_id, " _
                & "  ti_en_id, " _
                & "  ti_code, " _
                & "  ti_date, " _
                & "  ti_status, " _
                & "  ti_ppn_type, " _
                & "  ti_ptnr_id, " _
                & "  ptnr_name, " _
                & "  en_desc " _
                & "FROM  " _
                & "  public.ti_mstr " _
                & "  inner join en_mstr on en_id = ti_en_id " _
                & "  inner join ptnr_mstr on ptnr_id = ti_ptnr_id " _
                & "  where ti_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and ti_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and coalesce(ti_trans_id,'I') = 'I' " _
                & "  and ti_en_id = " + _en_id.ToString _
                & "  order by ti_code "
        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

        Dim ds_bantu As New DataSet
        If fobject.name = "FFakturPajakPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("fp_code")
        ElseIf fobject.name = "FFakturPajak" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("fp_code")
            fobject._fp_gnt_oid = ds.Tables(0).Rows(_row_gv).Item("fp_oid").ToString
        ElseIf fobject.name = FTaxInvoice.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ti_code")
            fobject._ti_gnt_oid = ds.Tables(0).Rows(_row_gv).Item("ti_oid").ToString
        ElseIf fobject.name = FTaxInvoicePrint.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ti_code")
        ElseIf fobject.name = FTaxInvoiceAttachmentPrint.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ti_code")
        End If
    End Sub
End Class
