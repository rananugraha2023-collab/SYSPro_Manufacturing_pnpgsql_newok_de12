Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDPackingSheetPrintOutSO
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    'Dim func_bill As New Cls_BilanganRetrieve
    Dim _pcs_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist As DataSet
    Public _pcs_arp_oid As String
    Dim _now As DateTime
    Public _par_cus_id As String

    Private Sub FDPackingSheetPrintOutSO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        init_le(pcso_en_id, "en_mstr")
    End Sub

    Public Overrides Sub load_cb_en()
        init_le(pcso_bill_to, "cus_mstr_parent", pcso_en_id.EditValue)
    End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcso_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Packing Code", "pcs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "By Shipment", "pcs_by_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "pcs_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "pcs_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Expected Date", "pcs_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "pcs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Created", "pcs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Created", "pcs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Updated", "pcs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Updated", "pcs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_so, "pcso_pcs_oid", False)
        add_column_copy(gv_detail_so, "SO Number", "pcso_so_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_shipment, "pcss_pcs_oid", False)
        add_column_copy(gv_detail_shipment, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Qty Open", "pcss_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "Qty SO", "pcss_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "Qty Packing", "pcss_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "Collie Number", "pcss_collie_number", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Close Line", "pcss_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so, "pcso_oid", False)
        add_column(gv_edit_so, "pcso_pcs_oid", False)
        add_column(gv_edit_so, "pcso_so_oid", False)
        add_column(gv_edit_so, "SO Number", "pcso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so, "pcso_so_date", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_shipment, "pcss_oid", False)
        add_column(gv_edit_shipment, "pcss_pcs_oid", False)
        add_column(gv_edit_shipment, "pcss_soshipd_oid", False)
        add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_shipment, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "pt_id", False)
        add_column(gv_edit_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Qty Open", "pcss_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_shipment, "Qty SO", "pcss_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_shipment, "Qty Packing", "pcss_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_shipment, "Collie Number", "pcss_collie_number", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_shipment, "Close Line", "pcss_close_line", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT DISTINCT  " _
                & "  public.pcs_mstr.pcs_oid, " _
                & "  public.pcs_mstr.pcs_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.pcs_mstr.pcs_code, " _
                & "  public.pcs_mstr.pcs_by_shipment, " _
                & "  public.pcs_mstr.pcs_bill_to, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.pcs_mstr.pcs_date, " _
                & "  public.pcs_mstr.pcs_due_date, " _
                & "  public.pcs_mstr.pcs_eff_date, " _
                & "  public.pcs_mstr.pcs_expt_date, " _
                & "  public.pcs_mstr.pcs_remarks, " _
                & "  public.pcs_mstr.pcs_add_by, " _
                & "  public.pcs_mstr.pcs_add_date, " _
                & "  public.pcs_mstr.pcs_upd_by, " _
                & "  public.pcs_mstr.pcs_upd_date " _
                & "FROM " _
                & "  public.pcs_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.pcs_mstr.pcs_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.pcs_mstr.pcs_bill_to = public.ptnr_mstr.ptnr_id)" _
                & " where pcs_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and pcs_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and pcs_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        If par_cus.Text <> "" Then
            If par_so.Text <> "" Then
                get_sequel = "SELECT DISTINCT  " _
                & "  public.pcs_mstr.pcs_oid, " _
                & "  public.pcs_mstr.pcs_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.pcs_mstr.pcs_code, " _
                & "  public.pcs_mstr.pcs_by_shipment, " _
                & "  public.pcs_mstr.pcs_bill_to, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.pcs_mstr.pcs_date, " _
                & "  public.pcs_mstr.pcs_due_date, " _
                & "  public.pcs_mstr.pcs_eff_date, " _
                & "  public.pcs_mstr.pcs_expt_date, " _
                & "  public.pcs_mstr.pcs_remarks, " _
                & "  public.pcs_mstr.pcs_add_by, " _
                & "  public.pcs_mstr.pcs_add_date, " _
                & "  public.pcs_mstr.pcs_upd_by, " _
                & "  public.pcs_mstr.pcs_upd_date " _
                & "FROM " _
                & "  public.pcs_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.pcs_mstr.pcs_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.pcs_mstr.pcs_bill_to = public.ptnr_mstr.ptnr_id)" _
                            & " where pcs_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and pcs_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and pcs_en_id in (select user_en_id from tconfuserentity " _
                            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                            & " and pcso_so_code='" & par_so.Text & "'"

            Else
                get_sequel = "SELECT DISTINCT  " _
                & "  public.pcs_mstr.pcs_oid, " _
                & "  public.pcs_mstr.pcs_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.pcs_mstr.pcs_code, " _
                & "  public.pcs_mstr.pcs_by_shipment, " _
                & "  public.pcs_mstr.pcs_bill_to, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.pcs_mstr.pcs_date, " _
                & "  public.pcs_mstr.pcs_due_date, " _
                & "  public.pcs_mstr.pcs_eff_date, " _
                & "  public.pcs_mstr.pcs_expt_date, " _
                & "  public.pcs_mstr.pcs_remarks, " _
                & "  public.pcs_mstr.pcs_add_by, " _
                & "  public.pcs_mstr.pcs_add_date, " _
                & "  public.pcs_mstr.pcs_upd_by, " _
                & "  public.pcs_mstr.pcs_upd_date " _
                & "FROM " _
                & "  public.pcs_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.pcs_mstr.pcs_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.pcs_mstr.pcs_bill_to = public.ptnr_mstr.ptnr_id)" _
                          & " where pcs_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                          & " and pcs_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                          & " and pcs_en_id in (select user_en_id from tconfuserentity " _
                          & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                          & " and pcs_bill_to=" & _par_cus_id
            End If
        End If

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        Try
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        'ARPCode.Text = ""
        '_pcs_arp_oid = ""
        'pcs_arp_code.Enabled = True

        ce_pcso_doc.EditValue = True

        pcso_eff_date.Enabled = True
        'by sys 20110427 permintaan pak aji..ingin eff date diambil dari shipment terakhir
        pcso_eff_date.DateTime = _now

        pcso_en_id.Focus()
        pcso_en_id.ItemIndex = 0
        pcso_bill_to.ItemIndex = 0

        'pcs_eff_date.DateTime = _now
        pcso_due_date.DateTime = _now
        pcso_expt_date.DateTime = _now
        pcso_remarks.Text = ""

        pcso_en_id.Enabled = True
        pcso_bill_to.Enabled = True

        gc_edit_so.Enabled = True
        gc_edit_shipment.Enabled = True
        'gc_edit_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = True
        'sb_retrieve_dist.Enabled = True

        'gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = True
        'gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = True
        'gv_edit_shipment.Columns("pcss_collie_number").OptionsColumn.AllowEdit = True

        'gv_edit_dist.Columns("pcsd_taxable").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_tax_inc").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_amount").OptionsColumn.AllowEdit = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_so = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  pcso_oid, " _
                        & "  pcso_pcs_oid, " _
                        & "  pcso_seq, " _
                        & "  pcso_so_oid, " _
                        & "  pcso_so_code, " _
                        & "  pcso_so_date, " _
                        & "  pcso_dt " _
                        & "FROM  " _
                        & "  public.pcso_so  " _
                        & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
                        & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
                        & "  where pcso_so_code ~~* 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_so, "list_so")
                    gc_edit_so.DataSource = ds_edit_so.Tables(0)
                    gv_edit_so.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_shipment = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  pcss_oid, True as ceklist, " _
                        & "  pcss_pcs_oid, " _
                        & "  pcss_seq, " _
                        & "  pcss_soshipd_oid, " _
                        & "  so_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pcss_open, " _
                        & "  pcss_shipment, " _
                        & "  pcss_packing, " _
                        & "  pcss_collie_number, " _
                        & "  pcss_close_line, " _
                        & "  pcss_dt " _
                        & "FROM  " _
                        & "  public.pcss_ship " _
                        & "  inner join public.soshipd_det on public.pcss_ship.pcss_soshipd_oid = public.soshipd_det.soshipd_oid " _
                        & "  inner join public.so_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                        & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                        & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.pcs_mstr on public.pcss_ship.pcss_pcs_oid = public.pcs_mstr.pcs_oid" _
                        & " where pcss_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                    gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                    gv_edit_shipment.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit_so.UpdateCurrentRow()
        gv_edit_shipment.UpdateCurrentRow()

        ds_edit_so.AcceptChanges()
        ds_edit_shipment.AcceptChanges()

        '*********************
        'Cek close line di tab shipment
        'Dim i As Integer
        'For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
        '    With ds_edit_shipment.Tables(0).Rows(i)
        '        If (.Item("pcss_open") = .Item("pcss_invoice")) And (.Item("pcss_so_price") = .Item("pcss_invoice_price")) Then
        '            .Item("pcss_close_line") = "Y"
        '        End If
        '    End With
        'Next
        '*********************

        'If ds_edit_so.Tables(0).Rows.Count >= 2 Then
        '    MessageBox.Show("SO detail can't over than 1 rows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position

        'If SetString(ds.Tables(0).Rows(row).Item("pcs_status")).ToString.ToLower = "c" Then
        '    Box("Can't edit close transaction")
        '    Return False
        '    Exit Function
        'End If

        If MyBase.edit_data = True Then


            With ds.Tables(0).Rows(row)
                _pcs_oid_mstr = .Item("pcs_oid")
                pcso_en_id.EditValue = .Item("pcs_en_id")
                pcso_bill_to.EditValue = .Item("pcs_bill_to")
                pcso_eff_date.DateTime = .Item("pcs_eff_date")
                pcso_due_date.DateTime = .Item("pcs_due_date")
                pcso_expt_date.DateTime = .Item("pcs_expt_date")
            End With

            pcso_en_id.Focus()
            pcso_en_id.Enabled = False
            pcso_bill_to.Enabled = False
            gc_edit_so.Enabled = False
            gc_edit_shipment.Enabled = False
            'gc_edit_dist.Enabled = False
            pcso_eff_date.Enabled = False
            sb_retrieve_receive_item.Enabled = False
            'sb_retrieve_dist.Enabled = False

            Try
                'tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_so = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pcso_oid, " _
                            & "  pcso_pcs_oid, " _
                            & "  pcso_seq, " _
                            & "  pcso_so_oid, " _
                            & "  pcso_so_code, " _
                            & "  pcso_so_date, " _
                            & "  pcso_dt " _
                            & "FROM  " _
                            & "  public.pcso_so  " _
                            & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
                            & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
                            & "  where pcso_pcs_oid = '" + _pcs_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_so, "list_so")
                        gc_edit_so.DataSource = ds_edit_so.Tables(0)
                        gv_edit_so.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_shipment = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  public.pcss_ship.pcss_oid, " _
                            & "  public.pcss_ship.pcss_pcs_oid, " _
                            & "  public.pcss_ship.pcss_soshipd_oid, " _
                            & "  public.so_mstr.so_code, " _
                            & "  public.pt_mstr.pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pcss_ship.pcss_dt, " _
                            & "  public.pcss_ship.pcss_shipment, " _
                            & "  public.pcss_ship.pcss_invoice, " _
                            & "  public.pcss_ship.pcss_packing, " _
                            & "  public.pcss_ship.pcss_collie_number, " _
                            & "  public.pcss_ship.pcss_close_line, " _
                            & "  public.pcss_ship.pcss_soship_code " _
                            & "FROM " _
                            & "  public.pcss_ship " _
                            & "  INNER JOIN public.sod_det ON (public.pcss_ship.pcss_soshipd_oid = public.sod_det.sod_oid) " _
                            & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid) " _
                            & "  INNER JOIN public.pt_mstr ON (public.so_mstr.so_pt_id = public.pt_mstr.pt_id) " _
                            & " where pcss_pcs_oid = '" + _pcs_oid_mstr + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit_shipment, "shipment")
                        gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                        gv_edit_shipment.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

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
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.pcs_mstr   " _
                                            & "SET  " _
                                            & "  pcs_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pcs_en_id = " & SetInteger(pcso_en_id.EditValue) & ",  " _
                                            & "  pcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pcs_eff_date = " & SetDate(pcso_eff_date.DateTime) & ",  " _
                                            & "  pcs_expt_date = " & SetDate(pcso_expt_date.DateTime) & ",  " _
                                            & "  pcs_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pcs_due_date = " & SetDate(pcso_due_date.DateTime) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pcs_oid = " & SetSetring(_pcs_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = insert_log("Edit Debit Credit Memo " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code"))
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_pcs_oid_mstr, "pcs_oid")
                        edit = True
                    Catch ex As PgSqlException
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _pcs_eff_date As Date = master_new.PGSqlConn.CekTanggal
        Dim _pcs_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_en_id")
        'Dim _gcald_det_status As String = func_data.get_gcald_det_status(_pcs_en_id, "gcald_ar", _pcs_eff_date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _pcs_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _pcs_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Harus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_so_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.DoubleClick
        browse_so()
    End Sub
    'shortcut pada append
    Private Sub gv_edit_so_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_so.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_so.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_so.DeleteSelectedRows()
        End If
    End Sub
    'shortcut pada browse
    Private Sub gv_edit_so_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_so.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_so()
        End If
    End Sub

    Private Sub browse_so()
        Dim _col As String = gv_edit_so.FocusedColumn.Name
        Dim _row As Integer = gv_edit_so.FocusedRowHandle

        'Browse PO berdasar kepada entity, patner, currency......
        If _col = "pcso_so_code" Then
            'If pcs_sod_shipment.Checked = True Then
            '    Dim frm As New FSalesOrderSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = pcs_en_id.EditValue
            '    frm._ptnr_id = pcs_bill_to.EditValue
            '    'frm._cu_id = pcs_cu_id.EditValue
            '    frm._obj = gv_edit_so
            '    'frm._ppn_type = pcs_ppn_type.EditValue
            '    frm.type_form = True
            '    frm.ShowDialog()
            'Else
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pcso_en_id.EditValue
            frm._ptnr_id = pcso_bill_to.EditValue
            'frm._cu_id = pcs_cu_id.EditValue
            frm._obj = gv_edit_so
            'frm._ppn_type = pcs_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'End If
        End If
    End Sub

    Private Sub gv_edit_shipment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_shipment.DoubleClick
        browse_soship()
    End Sub

    Private Sub gv_edit_shipment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_shipment.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_shipment.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_shipment.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_shipment_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_shipment.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_soship()
        End If
    End Sub

    Private Sub browse_soship()
        Dim _col As String = gv_edit_shipment.FocusedColumn.Name
        Dim _row As Integer = gv_edit_shipment.FocusedRowHandle
        Dim _so_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("pcso_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)
        'Browse PO berdasar kepada entity, patner, currency......
        If _col = "soship_code" Then
            'If pcs_sod_shipment.Checked = True Then
            '    Dim frm As New FSalesOrderSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = pcs_en_id.EditValue
            '    frm._ptnr_id = pcs_bill_to.EditValue
            '    'frm._cu_id = pcs_cu_id.EditValue
            '    frm._obj = gv_edit_so
            '    'frm._ppn_type = pcs_ppn_type.EditValue
            '    frm.type_form = True
            '    frm.ShowDialog()
            'Else
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pcso_en_id.EditValue
            frm._soo_code = _so_code
            frm._ptnr_id = pcso_bill_to.EditValue
            'frm._cu_id = pcs_cu_id.EditValue
            frm._obj = gv_edit_so
            'frm._ppn_type = pcs_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'End If
        End If
    End Sub

    'Private Sub pcs_arp_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Dim frm As New FARPSearch
    '    frm.set_win(Me)
    '    frm._en_id = pcs_en_id.EditValue
    '    'frm._date = so_date.DateTime
    '    frm.type_form = True
    '    'frm._tran_oid = _so_sq_ref_oid
    '    frm.ShowDialog()
    'End Sub

    Private Sub sb_retrieve_shipment_item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_receive_item.Click

        If ds_edit_so.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_so.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim _so_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("pcso_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  True AS ceklist, " _
                        & "  public.so_mstr.so_code, " _
                        & "  public.sod_det.sod_oid, " _
                        & "  public.sod_det.sod_so_oid, " _
                        & "  public.sod_det.sod_seq, " _
                        & "  public.sod_det.sod_pt_id, " _
                        & "  public.pt_mstr.pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  coalesce(public.sod_det.sod_qty, 0) AS qty_so, " _
                        & "  coalesce(public.sod_det.sod_qty, 0) AS qty_packing " _
                        & "FROM " _
                        & "  public.sod_det " _
                        & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid) " _
                        & "  where so_code in (" + _so_code + ") " _
                        & "  order by so_date "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "soship_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssql As String
        ssql = "select sum(coalesce(so_shipping_charges,0)) as jml from so_mstr where so_code in (" & _so_code & ")"

        Dim dt_so As New DataTable
        dt_so = GetTableData(ssql)


        'add_column(gv_edit_shipment, "pcss_oid", False)
        'add_column(gv_edit_shipment, "pcss_pcs_oid", False)
        'add_column(gv_edit_shipment, "pcss_soshipd_oid", False)
        'add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        'add_column(gv_edit_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "pt_id", False)
        'add_column(gv_edit_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Qty Open", "pcss_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit_shipment, "Qty Shipment", "pcss_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_shipment, "Qty Packing", "pcss_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_shipment, "Collie Number", "pcss_collie_number", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_shipment, "Close Line", "pcss_close_line", DevExpress.Utils.HorzAlignment.Default)


        ds_edit_shipment.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("qty_packing") <> 0 Then
                _dtrow = ds_edit_shipment.Tables(0).NewRow
                _dtrow("pcss_oid") = Guid.NewGuid.ToString
                _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
                _dtrow("pcss_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                '_dtrow("pcss_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("pcss_shipment") = ds_bantu.Tables(0).Rows(i).Item("qty_so")
                _dtrow("pcss_packing") = ds_bantu.Tables(0).Rows(i).Item("qty_packing")
                _dtrow("pcss_collie_number") = "1"
                _dtrow("pcss_close_line") = "N"
                ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
            End If
        Next

        'Dim ssql As String
        'ssql = "SELECT  " _
        '    & "  soship_date " _
        '    & "FROM  " _
        '    & "  public.soshipd_det " _
        '    & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
        '    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
        '    & "  inner join so_mstr on so_oid = sod_so_oid " _
        '    & "  inner join pt_mstr on pt_id = sod_pt_id " _
        '    & "  where coalesce(soshipd_close_line,'N') = 'N' " _
        '    & "  and so_code in (" + _so_code + ") and soship_is_shipment='Y'   " _
        '    & "  order by soship_date desc"

        'Dim dt As New DataTable
        'dt = master_new.PGSqlConn.GetTableData(ssql)


        'pcs_eff_date.DateTime = dt.Rows(0).Item("soship_date")
        '(i) disini pasti line yang terakhir

        ds_edit_shipment.Tables(0).AcceptChanges()

        gv_edit_shipment.BestFitColumns()

    End Sub


    Public Overrides Function insert() As Boolean
        Dim _pcs_oid As Guid
        _pcs_oid = Guid.NewGuid

        Dim _pcs_code As String
        'Dim _pcs_amount As Double = 0
        'Dim _prepaid As Double = 0
        Dim i As Integer
        Dim ssqls As New ArrayList
        'Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        '_pcs_code = func_coll.get_transaction_number("PCS", pcs_en_id.GetColumnValue("en_code"), "pcs_mstr", "pcs_code")

        _pcs_code = GetNewNumberYM("pcs_mstr", "pcs_code", 5, "PC" & pcso_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit_so.EmbeddedNavigator.Buttons.DoClick(gc_edit_so.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit_so.Tables(0).AcceptChanges()

        'For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
        '    _pcs_amount = _pcs_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        'Next
        '_pcs_terbilang = func_bill.TERBILANG_FIX(_ar_amount)
        'Dim _code As String

        '_pcs_code = GetNewNumberYM("arp_print", "arp_code", 5, "ARP" & pcs_en_id.GetColumnValue("en_code") _
        '& CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        'gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        'ds_edit.Tables(0).AcceptChanges()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pcs_mstr " _
                                            & "( " _
                                            & "  pcs_oid, " _
                                            & "  pcs_dom_id, " _
                                            & "  pcs_en_id, " _
                                            & "  pcs_add_by, " _
                                            & "  pcs_add_date, " _
                                            & "  pcs_code, " _
                                            & "  pcs_date, " _
                                            & "  pcs_bill_to, " _
                                            & "  pcs_eff_date, " _
                                            & "  pcs_expt_date, " _
                                            & "  pcs_remarks, " _
                                            & "  pcs_dt, " _
                                            & "  pcs_due_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pcs_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pcso_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_pcs_code) & ",  " _
                                            & SetDate(pcso_eff_date.DateTime) & " " & ",  " _
                                            & SetInteger(pcso_bill_to.EditValue) & ",  " _
                                            & SetDate(pcso_eff_date.DateTime) & ",  " _
                                            & SetDate(pcso_expt_date.DateTime) & ",  " _
                                            & SetSetring(pcso_remarks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(pcso_due_date.DateTime) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Update()

                        'Untuk Insert Data List so
                        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pcso_so " _
                                                & "( " _
                                                & "  pcso_oid, " _
                                                & "  pcso_pcs_oid, " _
                                                & "  pcso_seq, " _
                                                & "  pcso_so_oid, " _
                                                & "  pcso_so_code, " _
                                                & "  pcso_so_date, " _
                                                & "  pcso_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("pcso_oid").ToString) & ",  " _
                                                & SetSetring(_pcs_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("pcso_so_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("pcso_so_code").ToString) & ",  " _
                                                & SetDate(ds_edit_so.Tables(0).Rows(i).Item("pcso_so_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List shipment
                        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                                'If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.pcss_ship " _
                                                    & "( " _
                                                    & "  pcss_oid, " _
                                                    & "  pcss_pcs_oid, " _
                                                    & "  pcss_seq, " _
                                                    & "  pcss_soshipd_oid, " _
                                                    & "  pcss_open, " _
                                                    & "  pcss_shipment, " _
                                                    & "  pcss_packing, " _
                                                    & "  pcss_close_line, " _
                                                    & "  pcss_collie_number, " _
                                                    & "  pcss_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_oid").ToString) & ",  " _
                                                    & SetSetring(_pcs_oid.ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_soshipd_oid").ToString) & ",  " _
                                                    & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_open")) & ",  " _
                                                    & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_shipment")) & ",  " _
                                                    & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_packing")) & ",  " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_close_line")) & ",  " _
                                                    & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_collie_number")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update Table shipment Detail untuk kolom shipd_qty_inv dan shipd_close_line nya
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update soshipd_det set soshipd_packing_code = coalesce(soshipd_qty_inv,0) + " + SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice").ToString) + _
                                '                       ", soshipd_close_line = '" + ds_edit_shipment.Tables(0).Rows(i).Item("pcss_close_line") + "'" + _
                                '                       " where soshipd_oid = '" + ds_edit_shipment.Tables(0).Rows(i).Item("pcss_soshipd_oid") + "'"
                                'ssqls.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                'If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 Then
                                '    'Update Table so Detail untuk kolom sod_qty_invoice
                                '    .Command.CommandType = CommandType.Text
                                '    .Command.CommandText = "update sod_det set sod_qty_invoice = coalesce(sod_qty_invoice,0) + " + SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice").ToString) + _
                                '                           " where sod_oid = (select soshipd_sod_oid from soshipd_det where soshipd_oid = '" + ds_edit_shipment.Tables(0).Rows(i).Item("pcss_soshipd_oid") + "')"
                                '    ssqls.Add(.Command.CommandText)
                                '    .Command.ExecuteNonQuery()
                                '    .Command.Parameters.Clear()
                                'End If
                                'End If
                            End If
                        Next

                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'End If

                        sqlTran.Commit()
                        after_success()
                        set_row(_pcs_oid.ToString, "pcs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
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


    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_en_id")
        _type = 13
        _table = "pcs_mstr"
        _initial = "pcs"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
                & "SUM(pcss_packing) AS packing, " _
                & "SUM(pcss_shipment) AS hitung, " _
                & "SUM(sod_qty_invoice) AS invoiced, " _
                & "code_desc, " _
                & "pcss_collie_number, " _
                & "en_desc, " _
                & "pcs_code, " _
                & "pcs_date, " _
                & "pcs_remarks, " _
                & "sod_pt_id, " _
                & "pt_code, " _
                & "pt_desc1, " _
                & "cmaddr_line_1, " _
                & "cmaddr_line_2, " _
                & "cmaddr_line_3, " _
                & "cmaddr_phone_1, " _
                & "cmaddr_phone_2, " _
                & "ptnr_name, " _
                & "ptnra_line, " _
                & "ptnra_line_1, " _
                & "ptnra_line_2, " _
                & "ptnra_line_3, " _
                & "ptnra_phone_1, " _
                & "ptnra_phone_2, " _
                & "coalesce(tranaprvd_name_1, '') AS tranaprvd_name_1, " _
                & "coalesce(tranaprvd_name_2, '') AS tranaprvd_name_2, " _
                & "coalesce(tranaprvd_name_3, '') AS tranaprvd_name_3, " _
                & "coalesce(tranaprvd_name_4, '') AS tranaprvd_name_4, " _
                & "tranaprvd_pos_1, " _
                & "tranaprvd_pos_2, " _
                & "tranaprvd_pos_3, " _
                & "tranaprvd_pos_4 " _
                & "FROM " _
                & "pcss_ship " _
                & "INNER JOIN soshipd_det ON soshipd_oid = pcss_soshipd_oid " _
                & "INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
                & "INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
                & "INNER JOIN pcs_mstr ON pcs_oid = pcss_pcs_oid " _
                & "INNER JOIN en_mstr ON pcs_en_id = en_id " _
                & "INNER JOIN cmaddr_mstr ON cmaddr_en_id = en_id " _
                & "INNER JOIN ptnr_mstr ON ptnr_id = pcs_bill_to " _
                & "INNER JOIN ptnra_addr ON ptnra_ptnr_oid = ptnr_oid " _
                & "INNER JOIN code_mstr ON (sod_det.sod_um = code_id) " _
                & "and pcs_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code") + "'" _
                & "LEFT OUTER JOIN tranaprvd_dok ON (pcs_oid = tranaprvd_oid) " _
                & "GROUP BY " _
                & "code_desc, " _
                & "pcss_collie_number, " _
                & "en_desc, " _
                & "pcs_code, " _
                & "pcs_date, " _
                & "pcs_remarks, " _
                & "sod_pt_id, " _
                & "pt_desc1, " _
                & "pt_code, " _
                & "cmaddr_line_1, " _
                & "cmaddr_line_2, " _
                & "cmaddr_line_3, " _
                & "cmaddr_phone_1, " _
                & "cmaddr_phone_2, " _
                & "ptnr_name, " _
                & "ptnra_line, " _
                & "ptnra_line_1, " _
                & "ptnra_line_2, " _
                & "ptnra_line_3, " _
                & "ptnra_phone_1, " _
                & "ptnra_phone_2, " _
                & "tranaprvd_name_1, " _
                & "tranaprvd_name_2, " _
                & "tranaprvd_name_3, " _
                & "tranaprvd_name_4, " _
                & "tranaprvd_pos_1, " _
                & "tranaprvd_pos_2, " _
                & "tranaprvd_pos_3, " _
                & "tranaprvd_pos_4 " _
                & "ORDER BY " _
                & "pcs_code, " _
                & "pt_code, " _
                & "pt_desc1, " _
                & "pcss_collie_number " _
                & "DESC"

        If ce_pcso_doc.Checked = True Then
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            'frm._report = "XRPackingSheetPrint"
            frm._report = "XRPackingSheetLabel"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")
            frm.ShowDialog()
        Else
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "XRPackingSheetPrint"
            'frm._report = "XRPackingSheetLabel"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")
            frm.ShowDialog()
        End If

    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String

            Try
                ds.Tables("detail_so").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  pcso_oid, " _
                & "  pcso_pcs_oid, " _
                & "  pcso_seq, " _
                & "  pcso_so_oid, " _
                & "  pcso_so_code, " _
                & "  pcso_so_date, " _
                & "  pcso_dt " _
                & "FROM  " _
                & "  public.pcso_so  " _
                & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
                & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
                & " where pcs_mstr.pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid").ToString & "'"
            '& "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_so, "detail_so")

            Try
                ds.Tables("detail_shipment").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  pcss_oid, " _
                & "  pcss_pcs_oid, " _
                & "  pcss_seq, " _
                & "  pcss_soshipd_oid, " _
                & "  soship_code, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  pcss_open,pcss_shipment, " _
                & "  pcss_packing, " _
                & "  pcss_collie_number, " _
                & "  pcss_close_line, " _
                & "  pcss_dt " _
                & "FROM  " _
                & "  public.pcss_ship " _
                & "  inner join public.soshipd_det on public.pcss_ship.pcss_soshipd_oid = public.soshipd_det.soshipd_oid " _
                & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                & "  inner join public.pcs_mstr on public.pcss_ship.pcss_pcs_oid = public.pcs_mstr.pcs_oid" _
                & " where pcs_mstr.pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid").ToString & "'"
            '& "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_shipment, "detail_shipment")

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub par_so_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_so.ButtonClick
        Try

            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._obj = par_so
            frm._ptnr_id = _par_cus_id
            frm.type_form = True
            frm.ShowDialog()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub par_cus_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_cus.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = par_cus
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    'Private Sub UpdateTerbilangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateTerbilangToolStripMenuItem.Click
    '    Try
    '        Dim ssql As String
    '        Dim _terbilang As String

    '        _terbilang = func_bill.TERBILANG_FIX(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_amount"))
    '        ssql = "update pcs_mstr set pcs_terbilang=" & SetSetring(_terbilang) & " where pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid") & "'"

    '        Dim ssqls As New ArrayList
    '        ssqls.Add(ssql)

    '        If master_new.PGSqlConn.status_sync = True Then
    '            If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then

    '                Exit Sub
    '            End If
    '            ssqls.Clear()
    '        Else
    '            If DbRunTran(ssqls, "") = False Then

    '                Exit Sub
    '            End If
    '            ssqls.Clear()
    '        End If
    '        Box("Update success, please refresh data")

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Private Sub gv_edit_so_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.RowCountChanged
    '    Try
    '        If gv_edit_so.RowCount >= 1 Then
    '            gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = False
    '            gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = True
    '        Else
    '            gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = True
    '            gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

End Class
