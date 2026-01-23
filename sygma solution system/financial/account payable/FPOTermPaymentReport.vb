Imports master_new.ModFunction
Public Class FPOTermPaymentReport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Private Sub FBPBRekap_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        'add_groupsummary()
        'AllowIncrementalSearch()
        set_component()
        'load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False


        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal

    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        Dim dt As New DataTable
        Dim ssql As String
        Try
            ssql = "SELECT  " _
                & "  b.podtp_date, " _
                & "  b.podtp_amount, " _
                & "  b.podtp_amount_percent, " _
                & "  a.po_code, " _
                & "  c.ptnr_name,d.en_desc " _
                & "FROM " _
                & "  public.pod_term_payment b " _
                & "  INNER JOIN public.po_mstr a ON (b.podtp_po_oid = a.po_oid) " _
                & "  INNER JOIN public.ptnr_mstr c ON (a.po_ptnr_id = c.ptnr_id) " _
                & "  INNER JOIN public.en_mstr d ON (a.po_en_id = d.en_id) " _
                & "WHERE " _
                & " a.po_trans_id = 'I' AND  " _
                & "  b.podtp_date BETWEEN " & SetDate(pr_txttglawal.EditValue) & " AND " & SetDate(pr_txttglakhir.EditValue) & " AND  " _
                & "  a.po_en_id in (select user_en_id from tconfuserentity " _
                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            dt = master_new.PGSqlConn.GetTableData(ssql)
            pgc_master.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        pgc_master.BestFit()
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function

    Public Overrides Sub email_data()
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            Dim frm As New master_new.FMail
            frm.sc_txtfile.Text = fileName
            frm.ShowDialog()
        End If
    End Sub

End Class
