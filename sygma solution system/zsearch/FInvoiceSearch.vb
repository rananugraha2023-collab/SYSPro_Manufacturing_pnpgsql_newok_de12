Imports master_new.ModFunction

Public Class FInvoiceSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public _ppn_type As String
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInvoiceSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        If fobject.name = FTaxInvoice.Name Then
            sb_fill.Visible = True
            gv_master.Columns("status").Visible = True
        Else
            sb_fill.Visible = False
            gv_master.Columns("status").Visible = False
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Rev.", "ar_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Invoice Date", "ar_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Status", "ar_trans_id", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FFakturPajak" Then
            get_sequel = "SELECT  " _
                        & "  ar_oid, " _
                        & "  ar_dom_id, " _
                        & "  ar_en_id, " _
                        & "  ar_bill_to, " _
                        & "  ar_code, " _
                        & "  ar_date, " _
                        & "  ar_cu_id, " _
                        & "  en_desc, " _
                        & "  ptnr_name, " _
                        & "  cu_name " _
                        & "FROM  " _
                        & "  public.ar_mstr  " _
                        & "  inner join en_mstr on en_id = ar_en_id " _
                        & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                        & "  inner join cu_mstr on cu_id = ar_cu_id " _
                        & "  left outer join fp_mstr on fp_ar_oid = ar_oid " _
                        & " where (ar_oid not in (select fp_ar_oid from fp_mstr where coalesce(fp_trans_id,'I') = 'I'))" _
                        & " and ar_date >= " + SetDate(pr_txttglawal.DateTime) _
                        & " and ar_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        & " and ar_en_id = " + _en_id.ToString
        ElseIf fobject.name = FTaxInvoice.Name Then
            get_sequel = "SELECT false as status, " _
                       & "  ar_oid, " _
                       & "  ar_dom_id, " _
                       & "  ar_en_id, " _
                       & "  ar_bill_to, " _
                       & "  ar_code, " _
                       & "  ar_date, " _
                       & "  ar_cu_id, " _
                       & "  en_desc, " _
                       & "  ptnr_name, " _
                       & "  cu_name " _
                       & "FROM  " _
                       & "  public.ar_mstr  " _
                       & "  inner join en_mstr on en_id = ar_en_id " _
                       & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                       & "  inner join cu_mstr on cu_id = ar_cu_id " _
                       & "  where (ar_oid not in (select tia_ar_oid from tia_ar ))" _
                        & " and ar_date >= " + SetDate(pr_txttglawal.DateTime) _
                       & "  and ar_date <= " + SetDate(pr_txttglakhir.DateTime) _
                       & "  and coalesce(ar_ppn_type,'E') ~~* " + SetSetring(_ppn_type) _
                       & "  and coalesce(ar_ti_in_use,'N') ~~* 'N'" _
                       & "  and ar_bill_to in (select " + _ptnr_id.ToString + " as ptnr_id union " _
                                         & " select tipgd_ptnr_id from tipg_group " _
                                         & " inner join tipgd_det on tipgd_tipg_oid = tipg_oid " _
                                         & " where tipg_ptnr_id =  " + _ptnr_id.ToString + ")"
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
       If fobject.name = "FFakturPajak" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")
            fobject._fp_ar_oid = ds.Tables(0).Rows(_row_gv).Item("ar_oid").ToString
            fobject.fp_ptnr_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ar_bill_to")
        ElseIf fobject.name = FTaxInvoice.Name Then
            fobject.gv_edit_ar.SetRowCellValue(_row, "tia_oid", Guid.NewGuid.ToString)
            fobject.gv_edit_ar.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            fobject.gv_edit_ar.SetRowCellValue(_row, "tia_ar_oid", ds.Tables(0).Rows(_row_gv).Item("ar_oid").ToString)
            fobject.gv_edit_ar.SetRowCellValue(_row, "ar_code", ds.Tables(0).Rows(_row_gv).Item("ar_code"))
            fobject.gv_edit_ar.SetRowCellValue(_row, "ar_date", ds.Tables(0).Rows(_row_gv).Item("ar_date"))
            fobject.gv_edit_ar.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.gv_edit_ar.BestFitColumns()
        End If
    End Sub

    Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("status") = True Then
                    If fobject.name = FTaxInvoice.Name Then
                        If jml = 0 Then
                            fobject.gv_edit_ar.SetRowCellValue(_row, "tia_oid", Guid.NewGuid.ToString)
                            fobject.gv_edit_ar.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit_ar.SetRowCellValue(_row, "tia_ar_oid", ds.Tables(0).Rows(i).Item("ar_oid").ToString)
                            fobject.gv_edit_ar.SetRowCellValue(_row, "ar_code", ds.Tables(0).Rows(i).Item("ar_code"))
                            fobject.gv_edit_ar.SetRowCellValue(_row, "ar_date", ds.Tables(0).Rows(i).Item("ar_date"))
                            fobject.gv_edit_ar.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))

                            jml = jml + 1
                        Else
                            fobject.gv_edit_ar.AddNewRow()
                            _row_pos = fobject.gv_edit_ar.FocusedRowHandle()

                            fobject.gv_edit_ar.SetRowCellValue(_row_pos, "tia_oid", Guid.NewGuid.ToString)
                            fobject.gv_edit_ar.SetRowCellValue(_row_pos, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit_ar.SetRowCellValue(_row_pos, "tia_ar_oid", ds.Tables(0).Rows(i).Item("ar_oid").ToString)
                            fobject.gv_edit_ar.SetRowCellValue(_row_pos, "ar_code", ds.Tables(0).Rows(i).Item("ar_code"))
                            fobject.gv_edit_ar.SetRowCellValue(_row_pos, "ar_date", ds.Tables(0).Rows(i).Item("ar_date"))
                            fobject.gv_edit_ar.SetRowCellValue(_row_pos, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
                        End If

                        fobject.gv_edit_ar.BestFitColumns()
                    End If
                End If
            Next
        Catch
        End Try

        Me.Close()
    End Sub
End Class
