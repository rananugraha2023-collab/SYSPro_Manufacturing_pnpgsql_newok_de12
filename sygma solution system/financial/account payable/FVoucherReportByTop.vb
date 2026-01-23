Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FVoucherReportByTop
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Private Sub FBPBRekap_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        cu_id.Properties.DataSource = dt_bantu
        cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        cu_id.ItemIndex = 0

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
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        Dim dt As New DataTable
        Dim ssql As String
        Try
            If RBFilterCU.Checked Then
                If CBPaid.Checked Then
                    ssql = "SELECT  en_desc, " _
                      & "  b.ptnr_name, " _
                      & "  c.code_name AS ap_type_name, " _
                      & "  sum(coalesce(a.ap_amount,0)) as ap_amount, " _
                      & "  sum(coalesce(a.ap_pay_amount,0)) as ap_pay_amount, " _
                      & "  sum(coalesce(a.ap_amount,0)-coalesce(a.ap_pay_amount,0)) as ap_outstanding " _
                      & " " _
                      & "FROM " _
                      & "  public.ap_mstr a " _
                      & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                      & "  INNER JOIN public.code_mstr c ON (a.ap_type = c.code_id) " _
                      & "  INNER JOIN public.en_mstr ON (en_id = ap_en_id) " _
                      & "WHERE " _
                      & "  lower(a.ap_status) = 'c' and a.ap_cu_id=" & SetInteger(cu_id.EditValue) _
                      & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                Else
                    ssql = "SELECT en_desc, " _
                      & "  b.ptnr_name, " _
                      & "  c.code_name AS ap_type_name, " _
                      & "  sum(coalesce(a.ap_amount,0)) as ap_amount, " _
                      & "  sum(coalesce(a.ap_pay_amount,0)) as ap_pay_amount, " _
                      & "  sum(coalesce(a.ap_amount,0)-coalesce(a.ap_pay_amount,0)) as ap_outstanding " _
                      & " " _
                      & "FROM " _
                      & "  public.ap_mstr a " _
                      & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                      & "  INNER JOIN public.code_mstr c ON (a.ap_type = c.code_id) " _
                      & "  INNER JOIN public.en_mstr ON (en_id = ap_en_id) " _
                      & "WHERE " _
                      & "  (lower(a.ap_status) <> 'c' OR  " _
                      & "  a.ap_status IS NULL) and a.ap_cu_id=" & SetInteger(cu_id.EditValue) _
                      & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                End If
                ssql += " group by en_desc, ap_type_name, ptnr_name order by ap_amount desc limit " & SetInteger(CBoTop.Text)
            Else
                If CBPaid.Checked Then
                    ssql = "SELECT  en_desc, " _
                      & "  b.ptnr_name, " _
                      & "  c.code_name AS ap_type_name, " _
                      & "  sum(coalesce(a.ap_amount, 0) * coalesce(a.ap_exc_rate, 0)) AS ap_amount, " _
                      & "  sum(f_get_ap_payment(a.ap_oid)) AS ap_pay_amount, " _
                      & "  sum((coalesce(a.ap_amount, 0) * coalesce(a.ap_exc_rate, 0)) - (f_get_ap_payment(a.ap_oid))) AS ap_outstanding " _
                      & "FROM " _
                      & "  public.ap_mstr a " _
                      & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                      & "  INNER JOIN public.code_mstr c ON (a.ap_type = c.code_id) " _
                      & "  INNER JOIN public.en_mstr ON (en_id = ap_en_id) " _
                      & "WHERE " _
                      & "  lower(a.ap_status) = 'c' " _
                      & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                Else
                    ssql = "SELECT  en_desc, " _
                      & "  b.ptnr_name, " _
                      & "  c.code_name AS ap_type_name, " _
                      & "  sum(coalesce(a.ap_amount, 0) * coalesce(a.ap_exc_rate, 0)) AS ap_amount, " _
                      & "  sum(f_get_ap_payment(a.ap_oid)) AS ap_pay_amount, " _
                      & "  sum((coalesce(a.ap_amount, 0) * coalesce(a.ap_exc_rate, 0)) - (f_get_ap_payment(a.ap_oid))) AS ap_outstanding " _
                      & "FROM " _
                      & "  public.ap_mstr a " _
                      & "  INNER JOIN public.ptnr_mstr b ON (a.ap_ptnr_id = b.ptnr_id) " _
                      & "  INNER JOIN public.code_mstr c ON (a.ap_type = c.code_id) " _
                      & "  INNER JOIN public.en_mstr ON (en_id = ap_en_id) " _
                      & "WHERE " _
                      & "  (lower(a.ap_status) <> 'c' OR  " _
                      & "  a.ap_status IS NULL) " _
                      & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                End If
                ssql += " group by en_desc, ap_type_name,ptnr_name order by ap_amount desc limit " & SetInteger(CBoTop.Text)
            End If


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

    Private Sub RBBaseCU_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBBaseCU.CheckedChanged
        If RBBaseCU.Checked Then
            cu_id.Enabled = False
            help_load_data(False)
        Else
            cu_id.Enabled = True
            help_load_data(False)
        End If
    End Sub

    Private Sub cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cu_id.EditValueChanged
        help_load_data(False)
    End Sub

    Private Sub CBPaid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBPaid.CheckedChanged
        help_load_data(False)
    End Sub

    Private Sub CBoTop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBoTop.SelectedIndexChanged
        help_load_data(False)
    End Sub
End Class
