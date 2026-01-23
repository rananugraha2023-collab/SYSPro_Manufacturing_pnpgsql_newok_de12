Imports master_new.ModFunction

Public Class FUnvoucheredPOReceipt
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
                ssql = "SELECT  en_desc, " _
                    & "  a.rcvd_qty, " _
                    & "  coalesce(a.rcvd_qty_inv, 0) as rcvd_qty_inv, " _
                    & "  a.rcvd_qty - coalesce(a.rcvd_qty_inv, 0) as qty_accrue, " _
                    & "  (a.rcvd_qty - coalesce(a.rcvd_qty_inv, 0))  " _
                    & "  *  coalesce(b.pod_cost,0)  as ap_accrue, " _
                    & "  d.rcv_eff_date, " _
                    & "  b.pod_cost, " _
                    & "  c.po_exc_rate, " _
                    & "  c.po_code, " _
                    & "  c.po_cu_id, " _
                    & "  e.ptnr_name " _
                    & "FROM " _
                    & "  public.rcvd_det a " _
                    & "  INNER JOIN public.pod_det b ON (a.rcvd_pod_oid = b.pod_oid) " _
                    & "  INNER JOIN public.po_mstr c ON (b.pod_po_oid = c.po_oid) " _
                    & "  INNER JOIN public.rcv_mstr d ON (a.rcvd_rcv_oid = d.rcv_oid) " _
                    & "  INNER JOIN public.ptnr_mstr e ON (c.po_ptnr_id = e.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr ON (en_id = po_en_id) " _
                    & "WHERE " _
                    & "  a.rcvd_qty > coalesce(a.rcvd_qty_inv, 0) and c.po_cu_id = " & SetInteger(cu_id.EditValue) _
                    & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            Else
                ssql = "SELECT en_desc, " _
                    & "  a.rcvd_qty, " _
                    & "  coalesce(a.rcvd_qty_inv, 0) as rcvd_qty_inv, " _
                    & "  a.rcvd_qty - coalesce(a.rcvd_qty_inv, 0) as qty_accrue, " _
                    & "  (a.rcvd_qty - coalesce(a.rcvd_qty_inv, 0))  " _
                    & "  *  coalesce(b.pod_cost,0) * coalesce(rcv_exc_rate) as ap_accrue, " _
                    & "  d.rcv_eff_date, " _
                    & "  b.pod_cost, " _
                    & "  rcv_exc_rate, " _
                    & "  c.po_code, " _
                    & "  c.po_cu_id, " _
                    & "  e.ptnr_name " _
                    & "FROM " _
                    & "  public.rcvd_det a " _
                    & "  INNER JOIN public.pod_det b ON (a.rcvd_pod_oid = b.pod_oid) " _
                    & "  INNER JOIN public.po_mstr c ON (b.pod_po_oid = c.po_oid) " _
                    & "  INNER JOIN public.rcv_mstr d ON (a.rcvd_rcv_oid = d.rcv_oid) " _
                    & "  INNER JOIN public.ptnr_mstr e ON (c.po_ptnr_id = e.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr ON (en_id = po_en_id) " _
                    & "WHERE " _
                    & "  a.rcvd_qty > coalesce(a.rcvd_qty_inv, 0)" _
                    & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

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
End Class
