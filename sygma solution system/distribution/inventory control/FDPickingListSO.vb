Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDPickingListSO
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    'Dim func_bill As New Cls_BilanganRetrieve
    Dim _pickls_oid_mstr As String
    Public ds_edit_so, ds_edit_sales, ds_edit_dist As DataSet
    Public _pickls_arp_oid As String
    Dim _now As DateTime
    Public _par_cus_id As String

    Private Sub FDPickingListSO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()

        'form_first_load()
        _now = func_coll.get_now
        '_then = func_coll.get_then
        '_pay = func_coll.get_pay

        'form_first_load()
        '_now = func_coll.get_now
        'pr_txttglawal.DateTime = _now
        'pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        init_le(pcksl_en_id, "en_mstr")
    End Sub

    'Public Overrides Sub load_cb_en()
    '    init_le(pcksl_sold_to, "cus_mstr_parent", pcksl_en_id.EditValue)
    'End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcksl_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Packing Code", "pickls_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "By Shipment", "pickls_by_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "pickls_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Expected Date", "pickls_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "pickls_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "pickls_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Created", "pickls_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Created", "pickls_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Updated", "pickls_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Updated", "pickls_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_so, "picklsso_pickls_oid", False)
        add_column_copy(gv_detail_so, "SO Number", "picklsso_so_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so_detail, "picklsd_pickls_oid", False)
        add_column_copy(gv_edit_so_detail, "Sales Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Sub Location", "locs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_so_detail, "Qty Open", "picklsd_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit_so_detail, "Qty SO", "picklsd_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_so_detail, "Qty Packing", "picklsd_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_edit_so_detail, "Collie Number", "picklsd_collie_number", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_edit_so_detail, "Close Line", "picklsd_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so, "picklsso_oid", False)
        add_column(gv_edit_so, "picklsso_pickls_oid", False)
        add_column(gv_edit_so, "picklsso_so_oid", False)
        add_column(gv_edit_so, "SO Number", "picklsso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so, "picklsso_so_date", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so_detail, "picklsd_oid", False)
        add_column(gv_edit_so_detail, "picklsd_pickls_oid", False)
        add_column(gv_edit_so_detail, "picklsd_soshipd_oid", False)
        'add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_so_detail, "Sales Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "picklsd_pt_id", False)
        add_column(gv_edit_so_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "Sub Location", "locs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so_detail, "Qty Open", "picklsd_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit_shipment, "Qty Shipment", "picklsd_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_so_detail, "Qty Packing", "picklsd_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_so_detail, "Collie Number", "picklsd_collie_number", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_so_detail, "Close Line", "picklsd_close_line", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  public.pickls_mstr.pickls_oid, " _
                & "  public.pickls_mstr.pickls_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.pickls_mstr.pickls_code, " _
                & "  public.pickls_mstr.pickls_bill_to, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.pickls_mstr.pickls_date, " _
                & "  public.pickls_mstr.pickls_eff_date, " _
                & "  public.pickls_mstr.pickls_expt_date, " _
                & "  public.pickls_mstr.pickls_due_date, " _
                & "  public.pickls_mstr.pickls_remarks, " _
                & "  public.pickls_mstr.pickls_add_by, " _
                & "  public.pickls_mstr.pickls_add_date, " _
                & "  public.pickls_mstr.pickls_upd_by, " _
                & "  public.pickls_mstr.pickls_upd_date " _
                & "FROM " _
                & "  public.pickls_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.pickls_mstr.pickls_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.pickls_mstr.pickls_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  where pickls_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and pickls_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and pickls_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        Try
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        'ARPCode.Text = ""
        '_pickls_arp_oid = ""
        'pickls_arp_code.Enabled = True

        ce_pcksl_doc.EditValue = False

        pcksl_eff_date.Enabled = True
        'by sys 20110427 permintaan pak aji..ingin eff date diambil dari shipment terakhir
        pcksl_eff_date.DateTime = _now

        pcksl_en_id.Focus()
        pcksl_en_id.ItemIndex = 0
        'pcksl_sold_to.ItemIndex = 0

        pcksl_eff_date.DateTime = _now
        pcksl_due_date.DateTime = _now
        pcksl_expt_date.DateTime = _now
        pcksl_remarks.Text = ""

        pcksl_en_id.Enabled = True
        'pcksl_sold_to.Enabled = True

        gc_edit_so.Enabled = True
        gc_edit_so_detail.Enabled = True
        'gc_edit_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = True
        'sb_retrieve_dist.Enabled = True

        'gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = True
        'gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = True
        'gv_edit_shipment.Columns("picklsd_collie_number").OptionsColumn.AllowEdit = True

        'gv_edit_dist.Columns("pcsd_taxable").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_tax_inc").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = True
        'gv_edit_dist.Columns("pcsd_amount").OptionsColumn.AllowEdit = True
        'pcksl_eff_date.DateTime = _now
        'pcksl_expt_date.DateTime = _now
        'pcksl_due_date.DateTime = _now
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_so = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  picklsso_oid, " _
                        & "  picklsso_pickls_oid, " _
                        & "  picklsso_seq, " _
                        & "  picklsso_so_oid, " _
                        & "  picklsso_so_code, " _
                        & "  picklsso_so_date, " _
                        & "  picklsso_dt " _
                        & "FROM  " _
                        & "  public.picklsso_so  " _
                        & "  inner join public.pickls_mstr on pickls_mstr.pickls_oid = picklsso_pickls_oid" _
                        & "  inner join public.so_mstr on so_mstr.so_oid = picklsso_so_oid" _
                        & "  where picklsso_so_code ~~* 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_so, "list_so")
                    gc_edit_so.DataSource = ds_edit_so.Tables(0)
                    gv_edit_so.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_sales = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT " _
                        & "  public.picklsd_det.picklsd_oid, " _
                        & "  True as ceklist, " _
                        & "  public.picklsd_det.picklsd_pickls_oid, " _
                        & "  public.picklsd_det.picklsd_sod_oid, " _
                        & "  public.sod_det.sod_oid, " _
                        & "  public.picklsd_det.picklsd_dt, " _
                        & "  public.picklsd_det.picklsd_seq, " _
                        & "  public.picklsd_det.picklsd_pt_id, " _
                        & "  public.picklsd_det.picklsd_sod_qty, " _
                        & "  public.picklsd_det.picklsd_packing, " _
                        & "  public.picklsd_det.picklsd_collie_number, " _
                        & "  public.picklsd_det.picklsd_so_code, " _
                        & "  public.picklsd_det.picklsd_open, " _
                        & "  public.picklsd_det.picklsd_close_line, " _
                        & "  public.so_mstr.so_code, " _
                        & "  public.pt_mstr.pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2 " _
                        & "FROM " _
                        & "  public.picklsd_det " _
                        & "  INNER JOIN public.sod_det ON (public.picklsd_det.picklsd_sod_oid = public.sod_det.sod_oid) " _
                        & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid) " _
                        & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id)" _
                        & " where picklsd_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_sales, "sales")
                    gc_edit_so_detail.DataSource = ds_edit_sales.Tables(0)
                    gv_edit_so_detail.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit_so.UpdateCurrentRow()
        gv_edit_so_detail.UpdateCurrentRow()

        ds_edit_so.AcceptChanges()
        ds_edit_sales.AcceptChanges()

        '*********************
        'Cek close line di tab shipment
        'Dim i As Integer
        'For i = 0 To ds_edit_sales.Tables(0).Rows.Count - 1
        '    With ds_edit_sales.Tables(0).Rows(i)
        '        If (.Item("picklsd_open") = .Item("picklsd_invoice")) And (.Item("picklsd_so_price") = .Item("picklsd_invoice_price")) Then
        '            .Item("picklsd_close_line") = "Y"
        '        End If
        '    End With
        'Next
        '*********************

        'If ds_edit_so.Tables(0).Rows.Count >= 2 Then
        '    MessageBox.Show("SO detail can't over than 1 rows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        'cek apabila qty open > qty packing


        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position

        'If SetString(ds.Tables(0).Rows(row).Item("pickls_status")).ToString.ToLower = "c" Then
        '    Box("Can't edit close transaction")
        '    Return False
        '    Exit Function
        'End If

        If MyBase.edit_data = True Then


            With ds.Tables(0).Rows(row)
                _pickls_oid_mstr = .Item("pickls_oid").ToString
                pcksl_en_id.EditValue = .Item("pickls_en_id")
                'pcksl_sold_to.EditValue = .Item("pickls_bill_to")
                pcksl_eff_date.DateTime = .Item("pickls_eff_date")
                pcksl_due_date.DateTime = .Item("pickls_due_date")
                pcksl_expt_date.DateTime = .Item("pickls_expt_date")
            End With

            pcksl_en_id.Focus()
            pcksl_en_id.Enabled = False
            'pcksl_sold_to.Enabled = False
            'gc_edit_so.Enabled = False
            gc_edit_so_detail.Enabled = False
            gc_edit_so.Enabled = False
            pcksl_eff_date.Enabled = False
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
                            & "  picklsso_oid, " _
                            & "  picklsso_pickls_oid, " _
                            & "  picklsso_seq, " _
                            & "  picklsso_so_oid, " _
                            & "  picklsso_so_code, " _
                            & "  picklsso_so_date, " _
                            & "  picklsso_dt " _
                            & "FROM  " _
                            & "  public.picklsso_so  " _
                            & "  inner join public.pickls_mstr on pickls_mstr.pickls_oid = picklsso_pickls_oid" _
                            & "  inner join public.so_mstr on so_mstr.so_oid = picklsso_so_oid" _
                            & "  where picklsso_pickls_oid = '" + _pickls_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_so, "list_so")

                        gc_edit_so.DataSource = ds_edit_so.Tables(0)
                        gv_edit_so.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_sales = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT " _
                            & "  public.picklsd_det.picklsd_oid, " _
                            & "  public.picklsd_det.picklsd_pickls_oid, " _
                            & "  public.picklsd_det.picklsd_dt, " _
                            & "  public.picklsd_det.picklsd_seq, " _
                            & "  public.picklsd_det.picklsd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.picklsd_det.picklsd_open, " _
                            & "  public.picklsd_det.picklsd_packing, " _
                            & "  public.picklsd_det.picklsd_collie_number, " _
                            & "  public.picklsd_det.picklsd_close_line, " _
                            & "  public.pickls_mstr.pickls_code, " _
                            & "  public.pickls_mstr.pickls_bill_to " _
                            & "FROM " _
                            & "  public.picklsd_det " _
                            & "  INNER JOIN public.pickls_mstr ON (public.picklsd_det.picklsd_pickls_oid = public.pickls_mstr.pickls_oid) " _
                            & "  INNER JOIN public.pt_mstr ON (public.picklsd_det.picklsd_pt_id = public.pt_mstr.pt_id) " _
                            & " where picklsd_pickls_oid = '" + _pickls_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_sales, "sales")
                        gc_edit_so_detail.DataSource = ds_edit_sales.Tables(0)
                        gv_edit_so_detail.BestFitColumns()
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
        Dim i As Integer

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
                                            & "  public.pickls_mstr   " _
                                            & "SET  " _
                                            & "  pickls_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pickls_en_id = " & SetInteger(pcksl_en_id.EditValue) & ",  " _
                                            & "  pickls_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pickls_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pickls_eff_date = " & SetDate(pcksl_eff_date.DateTime) & ",  " _
                                            & "  pickls_expt_date = " & SetDate(pcksl_expt_date.DateTime) & ",  " _
                                            & "  pickls_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pickls_due_date = " & SetDate(pcksl_due_date.DateTime) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pickls_oid = " & SetSetring(_pickls_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'jika ada penambahan baris so
                        'For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.picklsso_so " _
                        '                        & "( " _
                        '                        & "  picklsso_oid, " _
                        '                        & "  picklsso_pickls_oid, " _
                        '                        & "  picklsso_seq, " _
                        '                        & "  picklsso_so_oid, " _
                        '                        & "  picklsso_so_code, " _
                        '                        & "  picklsso_so_date, " _
                        '                        & "  picklsso_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_pickls_oid").ToString) & ",  " _
                        '                        & SetInteger(i) & ",  " _
                        '                        & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_oid").ToString) & ",  " _
                        '                        & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_code").ToString) & ",  " _
                        '                        & SetDate(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_date")) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ")"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        'Untuk Insert Data List so
                        'For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.picklsso_so " _
                        '                        & "( " _
                        '                        & "  picklsso_oid, " _
                        '                        & "  picklsso_pickls_oid, " _
                        '                        & "  picklsso_seq, " _
                        '                        & "  picklsso_so_oid, " _
                        '                        & "  picklsso_so_code, " _
                        '                        & "  picklsso_so_date, " _
                        '                        & "  picklsso_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_pickls_oid").ToString) & ",  " _
                        '                        & SetInteger(i) & ",  " _
                        '                        & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_oid").ToString) & ",  " _
                        '                        & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_code").ToString) & ",  " _
                        '                        & SetDate(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_date")) & ",  " _
                        '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                        & ")"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next


                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = insert_log("Edit Debit Credit Memo " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code"))
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
                        set_row(_pickls_oid_mstr, "pickls_oid")
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _pickls_eff_date As Date = master_new.PGSqlConn.CekTanggal
        Dim _pickls_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_en_id")
        'Dim _gcald_det_status As String = func_data.get_gcald_det_status(_pickls_en_id, "gcald_ar", _pickls_eff_date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _pickls_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _pickls_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        If _col = "picklsso_so_code" Then
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pcksl_en_id.EditValue
            'frm._ptnr_id = pcksl_sold_to.EditValue
            'frm._cu_id = pickls_cu_id.EditValue
            frm._obj = gv_edit_so
            'frm._ppn_type = pickls_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'End If
        End If
    End Sub

    Private Sub gv_edit_shipment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so_detail.DoubleClick
        browse_soship()
    End Sub

    Private Sub gv_edit_shipment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_so_detail.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_so_detail.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_so_detail.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_shipment_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_so_detail.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_soship()
        End If
    End Sub

    Private Sub browse_soship()
        Dim _col As String = gv_edit_so_detail.FocusedColumn.Name
        Dim _row As Integer = gv_edit_so_detail.FocusedRowHandle
        Dim _so_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)
        'Browse PO berdasar kepada entity, patner, currency......
        If _col = "pt_code" Then
            Dim frm As New FPartNumberSearchPacking
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pcksl_en_id.EditValue
            frm._soo_code = _so_code
            'frm._ptnr_id = pcksl_sold_to.EditValue
            'frm._cu_id = pickls_cu_id.EditValue
            frm._obj = gv_edit_so_detail
            'frm._ppn_type = pickls_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'End If
        End If
    End Sub

    'Private Sub pickls_arp_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Dim frm As New FARPSearch
    '    frm.set_win(Me)
    '    frm._en_id = pickls_en_id.EditValue
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
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb


                    .SQL = "SELECT " _
                        & "  True as ceklist, " _
                        & "  public.pt_mstr.pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  SUM (coalesce (public.sod_det.sod_qty,0)) as qty, " _
                        & "  SUM (coalesce(public.sod_det.sod_qty,0)) as qty_packing, " _
                        & "  SUM (coalesce (public.sod_det.sod_qty_shipment)) qty_open " _
                        & "FROM " _
                        & "  public.sod_det " _
                        & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid) " _
                        & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                        & "  where public.so_mstr.so_code in (" + _so_code + ") " _
                        & "  GROUP BY ceklist, " _
                        & "  public.pt_mstr.pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2 "

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sod_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssql As String
        ssql = "select sum(coalesce(so_shipping_charges,0)) as jml from so_mstr where so_code in (" & _so_code & ")"

        Dim dt_so As New DataTable
        dt_so = GetTableData(ssql)


        'add_column(gv_edit_shipment, "picklsd_oid", False)
        'add_column(gv_edit_shipment, "picklsd_pickls_oid", False)
        'add_column(gv_edit_shipment, "picklsd_soshipd_oid", False)
        'add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        'add_column(gv_edit_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "pt_id", False)
        'add_column(gv_edit_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_shipment, "Qty Open", "picklsd_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit_shipment, "Qty Shipment", "picklsd_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_shipment, "Qty Packing", "picklsd_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_shipment, "Collie Number", "picklsd_collie_number", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit_shipment, "Close Line", "picklsd_close_line", DevExpress.Utils.HorzAlignment.Default)


        ds_edit_sales.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("qty_open") <> 0 Then
                _dtrow = ds_edit_sales.Tables(0).NewRow
                _dtrow("picklsd_oid") = Guid.NewGuid.ToString
                '_dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
                '_dtrow("picklsd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                '_dtrow("so_code") = ds_bantu.Tables(0).Rows(i).Item("so_code")
                _dtrow("picklsd_pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("picklsd_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '_dtrow("picklsd_sod_qty") = ds_bantu.Tables(0).Rows(i).Item("qty")
                _dtrow("picklsd_packing") = ds_bantu.Tables(0).Rows(i).Item("qty_packing")
                _dtrow("picklsd_collie_number") = "1"
                ds_edit_sales.Tables(0).Rows.Add(_dtrow)
            End If
        Next

        'Dim ssql As String
        ssql = "SELECT  " _
            & "  soship_date " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join so_mstr on so_oid = sod_so_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id " _
            & "  where coalesce(soshipd_close_line,'N') = 'N' " _
            & "  and so_code in (" + _so_code + ") and soship_is_shipment='Y'   " _
            & "  order by soship_date desc"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)


        'pickls_eff_date.DateTime = dt.Rows(0).Item("soship_date")
        '(i) disini pasti line yang terakhir

        ds_edit_sales.Tables(0).AcceptChanges()

        gv_edit_so_detail.BestFitColumns()

    End Sub


    Public Overrides Function insert() As Boolean
        Dim _pickls_oid As Guid
        _pickls_oid = Guid.NewGuid

        Dim _pickls_code As String
        'Dim _pickls_amount As Double = 0
        'Dim _prepaid As Double = 0
        Dim i As Integer
        Dim ssqls As New ArrayList
        'Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        '_pickls_code = func_coll.get_transaction_number("PCS", pickls_en_id.GetColumnValue("en_code"), "pickls_mstr", "pickls_code")

        _pickls_code = GetNewNumberYM("pickls_mstr", "pickls_code", 5, "PC" & pcksl_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit_so.EmbeddedNavigator.Buttons.DoClick(gc_edit_so.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit_so.Tables(0).AcceptChanges()

        'For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
        '    _pickls_amount = _pickls_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        'Next
        '_pickls_terbilang = func_bill.TERBILANG_FIX(_ar_amount)
        'Dim _code As String

        '_pickls_code = GetNewNumberYM("arp_print", "arp_code", 5, "ARP" & pickls_en_id.GetColumnValue("en_code") _
        '& CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        'gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        'ds_edit.Tables(0).AcceptChanges()

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
                                            & "  public.pickls_mstr " _
                                            & "( " _
                                            & "  pickls_oid, " _
                                            & "  pickls_dom_id, " _
                                            & "  pickls_en_id, " _
                                            & "  pickls_add_by, " _
                                            & "  pickls_add_date, " _
                                            & "  pickls_code, " _
                                            & "  pickls_date, " _
                                            & "  pickls_eff_date, " _
                                            & "  pickls_expt_date, " _
                                            & "  pickls_remarks, " _
                                            & "  pickls_dt, " _
                                            & "  pickls_due_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pickls_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pcksl_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_pickls_code) & ",  " _
                                            & SetDate(pcksl_eff_date.DateTime) & " " & ",  " _
                                            & SetDate(pcksl_expt_date.DateTime) & ",  " _
                                            & SetDate(pcksl_due_date.DateTime) & ",  " _
                                            & SetSetring(pcksl_remarks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(pcksl_due_date.DateTime) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Update()

                        'Untuk Insert Data List so
                        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.picklsso_so " _
                                                & "( " _
                                                & "  picklsso_oid, " _
                                                & "  picklsso_pickls_oid, " _
                                                & "  picklsso_seq, " _
                                                & "  picklsso_so_oid, " _
                                                & "  picklsso_so_code, " _
                                                & "  picklsso_so_date, " _
                                                & "  picklsso_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_oid").ToString) & ",  " _
                                                & SetSetring(_pickls_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_code").ToString) & ",  " _
                                                & SetDate(ds_edit_so.Tables(0).Rows(i).Item("picklsso_so_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List shipment
                        For i = 0 To ds_edit_sales.Tables(0).Rows.Count - 1
                            'If ds_edit_sales.Tables(0).Rows(i).Item("ceklist") = True Then
                            'If ds_edit_sales.Tables(0).Rows(i).Item("picklsd_invoice") <> 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.picklsd_det " _
                                                & "( " _
                                                & "  picklsd_oid, " _
                                                & "  picklsd_pickls_oid, " _
                                                & "  picklsd_seq, " _
                                                & "  picklsd_pt_id, " _
                                                & "  picklsd_open, " _
                                                & "  picklsd_packing, " _
                                                & "  picklsd_close_line, " _
                                                & "  picklsd_collie_number, " _
                                                & "  picklsd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_pickls_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetInteger(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_pt_id")) & ",  " _
                                                & SetDec(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_open")) & ",  " _
                                                & SetDec(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_packing")) & ",  " _
                                                & SetSetring(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_close_line")) & ",  " _
                                                & SetDec(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_collie_number")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update Table shipment Detail untuk kolom shipd_qty_inv dan shipd_close_line nya
                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update soshipd_det set soshipd_packing_code = coalesce(soshipd_qty_inv,0) + " + SetDec(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_invoice").ToString) + _
                            '                       ", soshipd_close_line = '" + ds_edit_sales.Tables(0).Rows(i).Item("picklsd_close_line") + "'" + _
                            '                       " where soshipd_oid = '" + ds_edit_sales.Tables(0).Rows(i).Item("picklsd_soshipd_oid") + "'"
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'If ds_edit_sales.Tables(0).Rows(i).Item("picklsd_invoice") <> 0 Then
                            '    'Update Table so Detail untuk kolom sod_qty_invoice
                            '    .Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "update sod_det set sod_qty_invoice = coalesce(sod_qty_invoice,0) + " + SetDec(ds_edit_sales.Tables(0).Rows(i).Item("picklsd_invoice").ToString) + _
                            '                           " where sod_oid = (select soshipd_sod_oid from soshipd_det where soshipd_oid = '" + ds_edit_sales.Tables(0).Rows(i).Item("picklsd_soshipd_oid") + "')"
                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    .Command.Parameters.Clear()
                            'End If
                            'End If
                            'End If
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
                        set_row(_pickls_oid.ToString, "pickls_oid")
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


    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_en_id")
        _type = 13
        _table = "pickls_mstr"
        _initial = "pckls"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        '_sql = "SELECT  " _
        '        & "SUM(picklsd_packing) AS packing, " _
        '        & "SUM(picklsd_shipment) AS hitung, " _
        '        & "SUM(sod_qty_invoice) AS invoiced, " _
        '        & "code_desc, " _
        '        & "picklsd_collie_number, " _
        '        & "en_desc, " _
        '        & "pickls_code, " _
        '        & "pickls_date, " _
        '        & "pickls_remarks, " _
        '        & "sod_pt_id, " _
        '        & "pt_code, " _
        '        & "pt_desc1, " _
        '        & "cmaddr_line_1, " _
        '        & "cmaddr_line_2, " _
        '        & "cmaddr_line_3, " _
        '        & "cmaddr_phone_1, " _
        '        & "cmaddr_phone_2, " _
        '        & "ptnr_name, " _
        '        & "ptnra_line, " _
        '        & "ptnra_line_1, " _
        '        & "ptnra_line_2, " _
        '        & "ptnra_line_3, " _
        '        & "ptnra_phone_1, " _
        '        & "ptnra_phone_2, " _
        '        & "coalesce(tranaprvd_name_1, '') AS tranaprvd_name_1, " _
        '        & "coalesce(tranaprvd_name_2, '') AS tranaprvd_name_2, " _
        '        & "coalesce(tranaprvd_name_3, '') AS tranaprvd_name_3, " _
        '        & "coalesce(tranaprvd_name_4, '') AS tranaprvd_name_4, " _
        '        & "tranaprvd_pos_1, " _
        '        & "tranaprvd_pos_2, " _
        '        & "tranaprvd_pos_3, " _
        '        & "tranaprvd_pos_4 " _
        '        & "FROM " _
        '        & "picklsd_det " _
        '        & "INNER JOIN soshipd_det ON soshipd_oid = picklsd_soshipd_oid " _
        '        & "INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
        '        & "INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
        '        & "INNER JOIN pickls_mstr ON pickls_oid = picklsd_pickls_oid " _
        '        & "INNER JOIN en_mstr ON pickls_en_id = en_id " _
        '        & "INNER JOIN cmaddr_mstr ON cmaddr_en_id = en_id " _
        '        & "INNER JOIN ptnr_mstr ON ptnr_id = pickls_bill_to " _
        '        & "INNER JOIN ptnra_addr ON ptnra_ptnr_oid = ptnr_oid " _
        '        & "INNER JOIN code_mstr ON (sod_det.sod_um = code_id) " _
        '        & "and pickls_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code") + "'" _
        '        & "LEFT OUTER JOIN tranaprvd_dok ON (pickls_oid = tranaprvd_oid) " _
        '        & "GROUP BY " _
        '        & "code_desc, " _
        '        & "picklsd_collie_number, " _
        '        & "en_desc, " _
        '        & "pickls_code, " _
        '        & "pickls_date, " _
        '        & "pickls_remarks, " _
        '        & "sod_pt_id, " _
        '        & "pt_desc1, " _
        '        & "pt_code, " _
        '        & "cmaddr_line_1, " _
        '        & "cmaddr_line_2, " _
        '        & "cmaddr_line_3, " _
        '        & "cmaddr_phone_1, " _
        '        & "cmaddr_phone_2, " _
        '        & "ptnr_name, " _
        '        & "ptnra_line, " _
        '        & "ptnra_line_1, " _
        '        & "ptnra_line_2, " _
        '        & "ptnra_line_3, " _
        '        & "ptnra_phone_1, " _
        '        & "ptnra_phone_2, " _
        '        & "tranaprvd_name_1, " _
        '        & "tranaprvd_name_2, " _
        '        & "tranaprvd_name_3, " _
        '        & "tranaprvd_name_4, " _
        '        & "tranaprvd_pos_1, " _
        '        & "tranaprvd_pos_2, " _
        '        & "tranaprvd_pos_3, " _
        '        & "tranaprvd_pos_4 " _
        '        & "ORDER BY " _
        '        & "pickls_code, " _
        '        & "pt_code, " _
        '        & "pt_desc1, " _
        '        & "picklsd_collie_number " _
        '        & "DESC"

        _sql = "SELECT DISTINCT" _
                & "  pickls_en_id, " _
                & "  en_desc, " _
                & "  pickls_code, " _
                & "  pickls_bill_to, " _
                & "  ptnr_name, " _
                & "  pickls_date, " _
                & "  pickls_eff_date, " _
                & "  pickls_expt_date, " _
                & "  pickls_dt, " _
                & "  picklsd_seq, " _
                & "  picklsd_pt_id, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  picklsd_open, " _
                & "  picklsd_sod_qty, " _
                & "  picklsd_packing, " _
                & "  picklsd_collie_number, " _
                & "  pickls_remarks, " _
                & "  ptnra_line, " _
                & "  ptnra_line_1, " _
                & "  ptnra_line_2, " _
                & "  ptnra_line_3, " _
                & "  ptnra_phone_1, " _
                & "  ptnra_phone_2, " _
                & "  ptnra_fax_1, " _
                & "  ptnra_fax_2, " _
                & "  ptnra_zip, " _
                & "  cmaddr_line_1, " _
                & "  cmaddr_line_2, " _
                & "  cmaddr_line_3, " _
                & "  cmaddr_phone_1, " _
                & "  cmaddr_phone_2, " _
                & "coalesce(tranaprvd_name_1, '') AS tranaprvd_name_1, " _
                & "coalesce(tranaprvd_name_2, '') AS tranaprvd_name_2, " _
                & "coalesce(tranaprvd_name_3, '') AS tranaprvd_name_3, " _
                & "coalesce(tranaprvd_name_4, '') AS tranaprvd_name_4, " _
                & "tranaprvd_pos_1, " _
                & "tranaprvd_pos_2, " _
                & "tranaprvd_pos_3, " _
                & "tranaprvd_pos_4 " _
                & "FROM " _
                & "  pickls_mstr " _
                & "  INNER JOIN picklsd_det ON (pickls_oid = picklsd_pickls_oid) " _
                & "  INNER JOIN en_mstr ON (pickls_en_id = en_id) " _
                & "  INNER JOIN ptnr_mstr ON (pickls_bill_to = ptnr_id) " _
                & "  INNER JOIN pt_mstr ON (picklsd_pt_id = pt_id) " _
                & "  INNER JOIN picklsso_so ON (pickls_oid = picklsso_pickls_oid) " _
                & "  INNER JOIN ptnra_addr ON (ptnr_oid = ptnra_ptnr_oid) " _
                & "  INNER JOIN cmaddr_mstr ON (en_id = cmaddr_en_id) " _
                & "  LEFT OUTER JOIN tranaprvd_dok ON (pickls_oid = tranaprvd_tran_oid) " _
                & "  where " _
                & "  pickls_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code") + "'" _
                & "  ORDER BY picklsd_collie_number "

        If ce_pcksl_doc.Checked = True Then
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            'frm._report = "XRPackingSheetPrint"
            frm._report = "XRPackingLabel"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code")
            frm.ShowDialog()
        Else
            Dim frm As New frmPrintDialog
            frm._ssql = _sql
            frm._report = "c"
            frm._report = "XRPackingListPrint"
            frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_code")
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
                & "  picklsso_oid, " _
                & "  picklsso_pickls_oid, " _
                & "  picklsso_seq, " _
                & "  picklsso_so_oid, " _
                & "  picklsso_so_code, " _
                & "  picklsso_so_date, " _
                & "  picklsso_dt " _
                & "FROM  " _
                & "  public.picklsso_so  " _
                & "  inner join public.pickls_mstr on pickls_mstr.pickls_oid = picklsso_pickls_oid" _
                & "  inner join public.so_mstr on so_mstr.so_oid = picklsso_so_oid" _
                & " where pickls_mstr.pickls_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_oid").ToString & "'"
            '& "  where pickls_mstr.pickls_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pickls_mstr.pickls_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_so, "detail_so")

            Try
                ds.Tables("detail_shipment").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT " _
                & "  public.picklsd_det.picklsd_oid, " _
                & "  public.picklsd_det.picklsd_pickls_oid, " _
                & "  public.picklsd_det.picklsd_seq, " _
                & "  public.picklsd_det.picklsd_pt_id, " _
                & "  public.picklsd_det.picklsd_close_line, " _
                & "  public.pt_mstr.pt_code, " _
                & "  public.pt_mstr.pt_desc1, " _
                & "  public.pt_mstr.pt_desc2, " _
                & "  public.picklsd_det.picklsd_dt, " _
                & "  public.picklsd_det.picklsd_sod_qty, " _
                & "  public.picklsd_det.picklsd_open, " _
                & "  public.picklsd_det.picklsd_packing, " _
                & "  public.picklsd_det.picklsd_collie_number, " _
                & "  public.pickls_mstr.pickls_oid " _
                & "FROM " _
                & "  public.picklsd_det " _
                & "  INNER JOIN public.pt_mstr ON (public.picklsd_det.picklsd_pt_id = public.pt_mstr.pt_id) " _
                & "  INNER JOIN public.pickls_mstr ON (public.picklsd_det.picklsd_pickls_oid = public.pickls_mstr.pickls_oid)" _
                & " where pickls_mstr.pickls_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pickls_oid").ToString & "'"
            '& "  where pickls_mstr.pickls_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pickls_mstr.pickls_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

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

End Class
