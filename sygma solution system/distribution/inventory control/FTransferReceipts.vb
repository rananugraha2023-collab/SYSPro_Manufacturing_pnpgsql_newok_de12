Imports npgsql
Imports master_new.ModFunction

Public Class FTransferReceipts
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime
    Dim _ptsfr_oid_mstr As String
    Dim _ptsfr_code_mstr As String
    Dim _conf_value As String
    Dim _text As String

    Private Sub FTransferReceipts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_transfer_printout1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_transfer_printout1.DataTable1)
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_transfer_issue")
        _text = Me.Text
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ptsfr_en_id.Properties.DataSource = dt_bantu
        ptsfr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptsfr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptsfr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ptsfr_en_to_id.Properties.DataSource = dt_bantu
        ptsfr_en_to_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptsfr_en_to_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptsfr_en_to_id.ItemIndex = 0
    End Sub

    Private Sub ptsfr_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ptsfr_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(ptsfr_en_id.EditValue))
        ptsfr_si_id.Properties.DataSource = dt_bantu
        ptsfr_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        ptsfr_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        ptsfr_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(ptsfr_en_id.EditValue))
        ptsfr_loc_id.Properties.DataSource = dt_bantu
        ptsfr_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        ptsfr_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        ptsfr_loc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(ptsfr_en_id.EditValue))
        ptsfr_loc_git.Properties.DataSource = dt_bantu
        ptsfr_loc_git.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        ptsfr_loc_git.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        ptsfr_loc_git.ItemIndex = 0
    End Sub

    Private Sub ptsfr_en_to_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ptsfr_en_to_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(ptsfr_en_to_id.EditValue))
        ptsfr_si_to_id.Properties.DataSource = dt_bantu
        ptsfr_si_to_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        ptsfr_si_to_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        ptsfr_si_to_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(ptsfr_en_to_id.EditValue))
        ptsfr_loc_to_id.Properties.DataSource = dt_bantu
        ptsfr_loc_to_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        ptsfr_loc_to_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        ptsfr_loc_to_id.EditValue = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transfer Issue Number", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Transfer Receipt Date", "ptsfr_receive_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ptsfr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "ptsfr_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptsfr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptsfr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptsfr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptsfr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_detail, "ptsfrd_ptsfr_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Kirim", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Receipt", "ptsfrd_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Return", "ptsfrd_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "ptsfrd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Lot Number", "ptsfrd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "ptsfrd_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost", "ptsfrd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_serial, "ptsfrd_ptsfr_oid", False)
        add_column_copy(gv_detail_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "ptsfrds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "ptsfrds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_serial, "Qty Serial Receipt", "ptsfrds_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_serial, "Qty Serial Return", "ptsfrds_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "ptsfrd_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Issue", "ptsfrd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Receipt", "ptsfrd_qty_receive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Return", "ptsfrd_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "ptsfrd_um", False)
        add_column(gv_edit, "UM", "ptsfrd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot Number", "ptsfrd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "ptsfrd_cost", False)
        add_column_copy(gv_edit, "Remarks", "ptsfrd_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_serial, "ptsfrds_oid", False)
        add_column(gv_serial, "ptsfrds_ptsfrd_oid", False)
        add_column(gv_serial, "Lot/Serial Number", "ptsfrds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "Qty", "ptsfrds_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_serial, "Qty Receipt", "ptsfrds_qty_receive", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_serial, "Qty Return", "ptsfrds_qty_return", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_serial, "ptsfrds_si_id", False)
        add_column(gv_serial, "ptsfrds_loc_id", False)
        add_column(gv_serial, "pt_id", False)
        add_column(gv_serial, "pt_code", False)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ptsfr_oid, " _
                    & "  ptsfr_dom_id, " _
                    & "  ptsfr_add_by, " _
                    & "  ptsfr_add_date, " _
                    & "  ptsfr_upd_by, " _
                    & "  ptsfr_upd_date, " _
                    & "  ptsfr_code, " _
                    & "  ptsfr_date, " _
                    & "  ptsfr_receive_date, " _
                    & "  ptsfr_en_id, " _
                    & "  en_mstr_from.en_desc as en_desc_from, " _
                    & "  ptsfr_si_id, " _
                    & "  si_mstr_from.si_desc as si_desc_from, " _
                    & "  ptsfr_loc_id, " _
                    & "  loc_mstr_from.loc_desc as loc_desc_from, " _
                    & "  ptsfr_loc_git, " _
                    & "  loc_mstr_git.loc_desc as loc_desc_git, " _
                    & "  ptsfr_en_to_id, " _
                    & "  en_mstr_to.en_desc as en_desc_to, " _
                    & "  ptsfr_si_to_id, " _
                    & "  si_mstr_to.si_desc as si_desc_to, " _
                    & "  ptsfr_loc_to_id, " _
                    & "  loc_mstr_to.loc_desc as loc_desc_to, " _
                    & "  ptsfr_remarks, " _
                    & "  ptsfr_trans_id, " _
                    & "  ptsfr_dt   " _
                    & "FROM  " _
                    & "  public.ptsfr_mstr " _
                    & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = ptsfr_en_id " _
                    & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = ptsfr_si_id " _
                    & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = ptsfr_loc_id " _
                    & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = ptsfr_loc_git " _
                    & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = ptsfr_en_to_id " _
                    & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = ptsfr_si_to_id " _
                    & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = ptsfr_loc_to_id " _
                    & "  where ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and ptsfr_en_to_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        If _conf_value = "1" Then
            get_sequel = get_sequel + " and ptsfr_trans_id in ('I','C','X') "
        End If

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  ptsfrd_oid, " _
            & "  ptsfrd_ptsfr_oid, " _
            & "  ptsfrd_seq, " _
            & "  ptsfrd_pt_id, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_ls, " _
            & "  ptsfrd_qty, " _
            & "  ptsfrd_qty_receive, " _
            & "  ptsfrd_qty - ptsfrd_qty_receive as ptsfrd_qty_return, " _
            & "  ptsfrd_um, " _
            & "  code_name as ptsfrd_um_name, " _
            & "  ptsfrd_lot_serial, " _
            & "  ptsfrd_cost,ptsfrd_remarks,  " _
            & "  ptsfrd_pbd_oid, " _
            & "  ptsfrd_dt, " _
            & "  ptsfr_pb_oid " _
            & "FROM  " _
            & "  public.ptsfrd_det" _
            & "  INNER JOIN public.ptsfr_mstr ON (public.ptsfrd_det.ptsfrd_ptsfr_oid = public.ptsfr_mstr.ptsfr_oid) " _
            & "  INNER JOIN public.pt_mstr ON (public.ptsfrd_det.ptsfrd_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.ptsfrd_det.ptsfrd_um = public.code_mstr.code_id) " _
            & "  where ptsfr_mstr.ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ptsfr_mstr.ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""
        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  ptsfrds_oid, " _
            & "  ptsfrds_ptsfrd_oid, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  ptsfrds_qty, " _
            & "  ptsfrds_qty_receive, " _
            & "  ptsfrds_qty - ptsfrds_qty_receive as ptsfrds_qty_return, " _
            & "  ptsfrds_si_id, " _
            & "  ptsfrds_loc_id, " _
            & "  ptsfrds_lot_serial, " _
            & "  ptsfrds_dt " _
            & "FROM  " _
            & "  public.ptsfrds_serial" _
            & "  inner join ptsfrd_det on ptsfrd_oid = ptsfrds_ptsfrd_oid " _
            & "  inner join ptsfr_mstr on ptsfr_oid = ptsfrd_ptsfr_oid " _
            & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
            & "  inner join si_mstr on si_id = ptsfrds_si_id " _
            & "  inner join loc_mstr on loc_id = ptsfrds_loc_id " _
            & "  where ptsfr_mstr.ptsfr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ptsfr_mstr.ptsfr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail_serial, "detail_serial")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("ptsfrd_ptsfr_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ptsfrd_ptsfr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_serial.Columns("ptsfrd_ptsfr_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ptsfrd_ptsfr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If before_edit() = False Then
            Return False
        End If

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptsfr_oid_mstr = .Item("ptsfr_oid").ToString
                _ptsfr_code_mstr = .Item("ptsfr_code")
                ptsfr_receive_date.DateTime = _now
                ptsfr_code.Text = .Item("ptsfr_code")
                ptsfr_en_id.EditValue = .Item("ptsfr_en_id")
                ptsfr_si_id.EditValue = .Item("ptsfr_si_id")
                ptsfr_loc_id.EditValue = .Item("ptsfr_loc_id")
                ptsfr_loc_git.EditValue = .Item("ptsfr_loc_git")
                ptsfr_en_to_id.EditValue = .Item("ptsfr_en_to_id")
                ptsfr_si_to_id.EditValue = .Item("ptsfr_si_to_id")
                ptsfr_loc_to_id.EditValue = .Item("ptsfr_loc_to_id")
                ptsfr_remarks.Text = SetString(.Item("ptsfr_remarks"))
            End With
            ptsfr_code.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  ptsfrd_oid, " _
                                & "  ptsfrd_ptsfr_oid, " _
                                & "  ptsfrd_seq, " _
                                & "  ptsfrd_pt_id, " _
                                & "  pt_id, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  pt_ls, " _
                                & "  ptsfrd_qty, " _
                                & "  ptsfrd_qty as ptsfrd_qty_receive, " _
                                & "  0 as ptsfrd_qty_return, " _
                                & "  ptsfrd_um, " _
                                & "  code_name as ptsfrd_um_name, " _
                                & "  ptsfrd_lot_serial,ptsfrd_remarks,  " _
                                & "  ptsfrd_cost, " _
                                & "  ptsfrd_pbd_oid, " _
                                & "  ptsfrd_dt, ptsfr_pb_oid ,ptsfrd_pb_oid " _
                                & " FROM  " _
                                & "  public.ptsfrd_det" _
                                & "  INNER JOIN public.ptsfr_mstr ON (public.ptsfrd_det.ptsfrd_ptsfr_oid = public.ptsfr_mstr.ptsfr_oid) " _
                                & "  INNER JOIN public.pt_mstr ON (public.ptsfrd_det.ptsfrd_pt_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.ptsfrd_det.ptsfrd_um = public.code_mstr.code_id) " _
                                & "  where ptsfrd_det.ptsfrd_ptsfr_oid = '" + ds.Tables(0).Rows(row).Item("ptsfr_oid").ToString + "' " _
                                & "  "

                        'and ptsfrd_qty > coalesce(ptsfrd_qty_receive,0)

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_serial = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  ptsfrds_oid, " _
                            & "  ptsfrds_ptsfrd_oid, " _
                            & "  pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  ptsfrds_qty, " _
                            & "  ptsfrds_qty_receive, " _
                            & "  0 as ptsfrds_qty_return, " _
                            & "  ptsfrds_si_id, " _
                            & "  ptsfrds_loc_id, " _
                            & "  ptsfrds_lot_serial, " _
                            & "  ptsfrds_dt " _
                            & "FROM  " _
                            & "  public.ptsfrds_serial" _
                            & "  inner join ptsfrd_det on ptsfrd_oid = ptsfrds_ptsfrd_oid " _
                            & "  inner join ptsfr_mstr on ptsfr_oid = ptsfrd_ptsfr_oid " _
                            & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
                            & "  inner join si_mstr on si_id = ptsfrds_si_id " _
                            & "  inner join loc_mstr on loc_id = ptsfrds_loc_id " _
                            & "  where ptsfrd_det.ptsfrd_ptsfr_oid = '" + ds.Tables(0).Rows(row).Item("ptsfr_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_serial, "serial")
                        gc_serial.DataSource = ds_serial.Tables(0)
                        gv_serial.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            edit_data = True
        End If
    End Function

    Public Overrides Function before_edit() As Boolean
        before_edit = True
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_trans_id").ToString.ToUpper = "C" Then
            MessageBox.Show("Can't Edit Closed Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function

    Public Overrides Function edit()
        Dim my As FMainMenu = CType(Me.ParentForm, FMainMenu)

        edit = True

        Dim _serial, _pt_code As String
        'Dim _cost_methode As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty, _qty_rcv As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList

       

        _tran_id = func_coll.get_id_tran_mstr("rct-tr")
        Me.Text = _text & " tran_id"
        Windows.Forms.Application.DoEvents()
        If _tran_id = -1 Then
            MessageBox.Show("Transfer Receipt In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            edit = False
            Exit Function
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
                        .Command.CommandText = "update ptsfr_mstr set ptsfr_receive_date = " + SetDate(ptsfr_receive_date.DateTime) + ",ptsfr_trans_id='C',ptsfr_upd_by= " & SetSetring(master_new.ClsVar.sNama) _
                                             & " where ptsfr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        Me.Text = _text & " update ptsfr_mstr"
                        Windows.Forms.Application.DoEvents()
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptsfrd_det set ptsfrd_qty_receive = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive").ToString) _
                                                 & " where ptsfrd_oid = '" + ds_edit.Tables(0).Rows(i).Item("ptsfrd_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            Me.Text = _text & " update ptsfrd_det " & i
                            Windows.Forms.Application.DoEvents()
                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update ptsfrd_det set ptsfrd_qty_receive = " + ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive").ToString _
                            '                     & " where ptsfrd_oid = '" + ds_edit.Tables(0).Rows(i).Item("ptsfrd_oid").ToString + "'"
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        'Untuk Update data serial
                        For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptsfrds_serial set ptsfrds_qty_receive = " + SetDbl(ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_receive").ToString) _
                                                 & " where ptsfrds_oid = '" + ds_serial.Tables(0).Rows(i).Item("ptsfrds_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        '*********************************************************************************
                        'Proses Pengurangan untuk lokasi git
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = ptsfr_en_id.EditValue
                                    _si_id = ptsfr_si_id.EditValue
                                    _loc_id = ptsfr_loc_git.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive") 'karena ini GIT maka harus dikurangi semua qty nya...

                                    Me.Text = _text & " update_invc_mstr_minus start " & i
                                    Windows.Forms.Application.DoEvents()

                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If

                                    Me.Text = _text & " update_invc_mstr_minus end " & i
                                    Windows.Forms.Application.DoEvents()

                                    'Update History Inventory                                    
                                    _qty = _qty * -1
                                    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                    Me.Text = _text & " update_invh_mstr start " & i
                                    Windows.Forms.Application.DoEvents()

                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "(3)Transfer Issues GIT", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If
                                    Me.Text = _text & " update_invh_mstr " & i
                                    Windows.Forms.Application.DoEvents()
                                End If

                                'Update pbd_det
                                'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pbd_oid")) = False Then
                                '    Me.Text = _text & " ptsfrd_pbd_oid " & i
                                '    Windows.Forms.Application.DoEvents()

                                '    .Command.CommandType = CommandType.Text
                                '    .Command.CommandText = "UPDATE  " _
                                '                        & "  public.pbd_det   " _
                                '                        & "SET  " _
                                '                        & "  pbd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                '                        & "  pbd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & ",  " _
                                '                        & "  pbd_qty_completed = coalesce(pbd_qty_completed,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")) & ",  " _
                                '                        & "  pbd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & "  " _
                                '                        & "  " _
                                '                        & "WHERE  " _
                                '                        & "  pbd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pbd_oid").ToString) & " "
                                '    ssqls.Add(.Command.CommandText)
                                '    .Command.ExecuteNonQuery()
                                '    .Command.Parameters.Clear()

                                '    Me.Text = _text & " pbd_det C " & i
                                '    Windows.Forms.Application.DoEvents()

                                '    .Command.CommandType = CommandType.Text
                                '    .Command.CommandText = "UPDATE  " _
                                '                        & "  public.pbd_det   " _
                                '                        & "SET  " _
                                '                        & "  pbd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                '                        & "  pbd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & ",  " _
                                '                        & "  pbd_status = 'C'" & ",  " _
                                '                        & "  pbd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & "  " _
                                '                        & "  " _
                                '                        & "WHERE  " _
                                '                        & "  pbd_qty = pbd_qty_completed and pbd_oid=" & SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pbd_oid").ToString) & " "
                                '    ssqls.Add(.Command.CommandText)
                                '    .Command.ExecuteNonQuery()
                                '    .Command.Parameters.Clear()

                                '    Me.Text = _text & " cek_status_pb " & i
                                '    Windows.Forms.Application.DoEvents()

                                '    'If cek_status_pb(ds_edit.Tables(0).Rows(i).Item("ptsfr_pb_oid"), ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")) = True Then
                                '    If SetString(ds_edit.Tables(0).Rows(i).Item("ptsfr_pb_oid").ToString) <> "" Then
                                '        .Command.CommandType = CommandType.Text
                                '        .Command.CommandText = "UPDATE  " _
                                '                            & "  public.pb_mstr " _
                                '                            & "SET  " _
                                '                            & "  pb_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                '                            & "  pb_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & ",  " _
                                '                            & "  pb_status = 'C'" & ",  " _
                                '                            & "  pb_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal, True) & " " & ",  " _
                                '                            & "  pb_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & "  " _
                                '                            & "  " _
                                '                            & "WHERE  " _
                                '                            & "  pb_oid = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfr_pb_oid").ToString) _
                                '                            & " and coalesce((select count(*) as jml From pbd_det " & _
                                '                           " where (pbd_qty - coalesce(pbd_qty_completed,0))=0 " & _
                                '                           " and pbd_pb_oid = '" & ds_edit.Tables(0).Rows(i).Item("ptsfr_pb_oid").ToString & "'" & _
                                '                           " group by pbd_pb_oid),0) = 0 "

                                '        ssqls.Add(.Command.CommandText)
                                '        .Command.ExecuteNonQuery()
                                '        .Command.Parameters.Clear()
                                '    End If

                                '    If SetString(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pb_oid").ToString) <> "" Then
                                '        .Command.CommandType = CommandType.Text
                                '        .Command.CommandText = "UPDATE  " _
                                '                            & "  public.pb_mstr " _
                                '                            & "SET  " _
                                '                            & "  pb_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                '                            & "  pb_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & ",  " _
                                '                            & "  pb_status = 'C'" & ",  " _
                                '                            & "  pb_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal, True) & " " & ",  " _
                                '                            & "  pb_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal, True) & "  " _
                                '                            & "  " _
                                '                            & "WHERE  " _
                                '                            & "  pb_oid = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("ptsfrd_pb_oid").ToString) _
                                '                            & " and coalesce((select count(*) as jml From pbd_det " & _
                                '                           " where (pbd_qty - coalesce(pbd_qty_completed,0))=0 " & _
                                '                           " and pbd_pb_oid = '" & ds_edit.Tables(0).Rows(i).Item("ptsfrd_pb_oid").ToString & "'" & _
                                '                           " group by pbd_pb_oid),0) = 0 "

                                '        ssqls.Add(.Command.CommandText)
                                '        .Command.ExecuteNonQuery()
                                '        .Command.Parameters.Clear()
                                '    End If
                                ' (sum(pbd_qty - coalesce(pbd_qty_completed,0)) - " & SetDbl(par_qty.ToString) & ") as sum_qty


                                'End If
                            End If

                            Me.Text = _text & " " & i
                            Windows.Forms.Application.DoEvents()
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        'For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        '    If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_receipt") > 0 Then
                        '        i_2 += 1

                        '        _en_id = ptsfr_en_id.EditValue
                        '        _si_id = ptsfr_si_id.EditValue
                        '        _loc_id = ptsfr_loc_git.EditValue
                        '        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        '        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        '        _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                        '        _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                        '        If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If

                        '        'Update History Inventory
                        '        _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                        '        _cost = _cost * -1
                        '        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '    _en_id = ptsfr_en_id.EditValue
                        '    _si_id = ptsfr_si_id.EditValue
                        '    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                        '    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                        '    If _cost_methode = "F" Or _cost_methode = "L" Then
                        '        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '        Return False
                        '        'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '        '    sqlTran.Rollback()
                        '        '    edit = False
                        '        '    Exit Function
                        '        'End If
                        '    ElseIf _cost_methode = "A" Then
                        '        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        '*********************************************************************************
                        'Proses penambahan (+) untuk lokasi tujuan
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = ptsfr_en_to_id.EditValue
                                    _si_id = ptsfr_si_to_id.EditValue
                                    _loc_id = ptsfr_loc_to_id.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "(4)Transfer Receipt to", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If
                                End If
                            End If

                            Me.Text = _text & " " & i
                            Windows.Forms.Application.DoEvents()
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        'For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        '    If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_receipt") > 0 Then
                        '        i_2 += 1

                        '        _en_id = ptsfr_en_to_id.EditValue
                        '        _si_id = ptsfr_si_to_id.EditValue
                        '        _loc_id = ptsfr_loc_to_id.EditValue
                        '        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        '        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        '        _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                        '        _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_receipt")
                        '        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If

                        '        'Update History Inventory
                        '        _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                        '        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '    _en_id = ptsfr_en_to_id.EditValue
                        '    _si_id = ptsfr_si_to_id.EditValue
                        '    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")
                        '    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                        '    If _cost_methode = "F" Or _cost_methode = "L" Then
                        '        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '        Return False
                        '        'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '        '    'If func_coll.update_invct_table_minus(objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '        '    sqlTran.Rollback()
                        '        '    edit = False
                        '        '    Exit Function
                        '        'End If
                        '    ElseIf _cost_methode = "A" Then
                        '        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        '*********************************************************************************
                        'dikoment by rana nugraha karena ada nya kesalahan saat penghitungan qty retur yang menyebabkan git di kurangi 2 kali 29/01/2024
                        '3. Update invc_mstr dan invh_mstr untuk barang yang bukan serial minus git
                        'update 19 12 2012 kealahan minus GI pada baris 803 - 832
                        'i_2 = 0
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_return") > 0 Then
                        '        If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                        '            i_2 += 1

                        '            _en_id = ptsfr_en_id.EditValue
                        '            _si_id = ptsfr_si_id.EditValue
                        '            _loc_id = ptsfr_loc_git.EditValue
                        '            _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '            _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                        '            _serial = "''"
                        '            _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_return")
                        '            _qty_rcv = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")
                        '            If func_coll.update_invc_mstr_minus_return(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        '                sqlTran.Rollback()
                        '                edit = False
                        '                Exit Function
                        '            End If

                        '            'Update History Inventory          
                        '            _qty = _qty * -1
                        '            _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                        '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "(5)Transfer Return from ", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                        '                sqlTran.Rollback()
                        '                edit = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next

                        'Proses penambahan (+) untuk lokasi asal dikarenakan yang di receipt tidak semua qty
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_return") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = ptsfr_en_id.EditValue
                                    _si_id = ptsfr_si_id.EditValue
                                    _loc_id = ptsfr_loc_id.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_return")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "(5)Transfer Return to", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        'For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        '    If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_return") = 0 Then
                        '        i_2 += 1

                        '        _en_id = ptsfr_en_id.EditValue
                        '        _si_id = ptsfr_si_id.EditValue
                        '        _loc_id = ptsfr_loc_id.EditValue
                        '        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        '        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        '        _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                        '        _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_return")
                        '        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If

                        '        'Update History Inventory
                        '        _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                        '        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next
                        
                        

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        'For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        '    If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_return") > 0 Then
                        '        i_2 += 1

                        '        _en_id = ptsfr_en_id.EditValue
                        '        _si_id = ptsfr_si_id.EditValue
                        '        _loc_id = ptsfr_loc_git.EditValue
                        '        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        '        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        '        _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                        '        _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty_return")
                        '        If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If

                        '        'Update History Inventory
                        '        _qty = _qty * -1
                        '        _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                        '        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _ptsfr_code_mstr, _ptsfr_oid_mstr.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ptsfr_receive_date.DateTime) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '    _en_id = ptsfr_en_id.EditValue
                        '    _si_id = ptsfr_si_id.EditValue
                        '    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '    _qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_return")
                        '    _cost = ds_edit.Tables(0).Rows(i).Item("ptsfrd_cost")
                        '    If _cost_methode = "F" Or _cost_methode = "L" Then
                        '        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '        Return False
                        '        'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '        '    'If func_coll.update_invct_table_minus(objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '        '    sqlTran.Rollback()
                        '        '    edit = False
                        '        '    Exit Function
                        '        'End If
                        '    ElseIf _cost_methode = "A" Then
                        '        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '        If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '            sqlTran.Rollback()
                        '            edit = False
                        '            Exit Function
                        '        End If
                        '    End If
                        'Next

                        '*****************************************************************************************

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                        '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                        '                         _rcv_oid.ToString, _rcv_code, _
                        '                         func_coll.get_tanggal_sistem, _
                        '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                        '                         "IC", "IC-RPO") = False Then
                        '    sqlTran.Rollback()
                        '    insert = False
                        '    Exit Function
                        'End If

                        'If func_coll.update_tranaprvd_det(ssqls, objinsert, ptsfr_en_to_id.EditValue, 9, _ptsfr_oid_mstr.ToString, _ptsfr_code_mstr, _now.Date) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Inv Transfer Receipt " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code"))
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
                        Me.Text = _text
                        after_success()
                        set_row(Trim(_ptsfr_oid_mstr), "ptsfr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        If e.Column.Name = "ptsfrd_qty_receive" Then
            Dim _ptsfrd_qty As Double

            _ptsfrd_qty = (gv_edit.GetRowCellValue(e.RowHandle, "ptsfrd_qty"))
            gv_edit.SetRowCellValue(e.RowHandle, "ptsfrd_qty_return", _ptsfrd_qty - e.Value)
        End If
    End Sub

    Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_serial.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_serial.DeleteSelectedRows()
        End If
    End Sub

    Private Function cek_status_pb(ByVal par_pb_oid As String, ByVal par_qty As Double) As Boolean
        cek_status_pb = True

        Try
            'Using objcek As New master_new.WDABasepgsql("", "")
            '    With objcek
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "select pb_oid, (sum(pbd_qty - coalesce(pbd_qty_completed,0)) - " & SetDbl(par_qty.ToString) & ") as sum_qty " _
            '                             & " from pbd_det " _
            '                             & " inner join pb_mstr on pb_oid = pbd_pb_oid " _
            '                             & " where pb_oid = '" & par_pb_oid & "'" _
            '                             & " group by pb_oid"
            '        .InitializeCommand()
            '        .DataReader = .Command.ExecuteReader
            '        While .DataReader.Read
            '            If .DataReader("sum_qty") = 0 Then
            '                cek_status_pb = True
            '            Else
            '                cek_status_pb = False
            '            End If
            '        End While
            '    End With
            'End Using

            Dim sSQL As String
            sSQL = "select pb_oid, (sum(pbd_qty - coalesce(pbd_qty_completed,0)) - " & SetDbl(par_qty.ToString) & ") as sum_qty " _
                 & " from pbd_det " _
                 & " inner join pb_mstr on pb_oid = pbd_pb_oid " _
                 & " where pb_oid = '" & par_pb_oid & "'" _
                 & " group by pb_oid"

            Dim dr As DataRow
            dr = master_new.PGSqlConn.GetRowInfo(sSQL)

            If dr("sum_qty") = 0 Then
                cek_status_pb = True
            Else
                cek_status_pb = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cek_status_pb = False
            Exit Function
        End Try

        Return cek_status_pb
    End Function

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_en_id")
        _type = 9
        _table = "ptsfr_mstr"
        _initial = "ptsfr"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")

        func_coll.update_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        'Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  ptsfr_oid, " _
            & "  ptsfr_dom_id, " _
            & "  ptsfr_en_id, " _
            & "  ptsfr_add_by, " _
            & "  ptsfr_add_date, " _
            & "  ptsfr_upd_by, " _
            & "  ptsfr_upd_date, " _
            & "  ptsfr_en_to_id, " _
            & "  ptsfr_code, " _
            & "  ptsfr_date, " _
            & "  ptsfr_receive_date, " _
            & "  ptsfr_si_id, " _
            & "  ptsfr_loc_id, " _
            & "  ptsfr_loc_git, " _
            & "  ptsfr_remarks, " _
            & "  ptsfr_trans_id, " _
            & "  ptsfr_dt, " _
            & "  ptsfr_loc_to_id, " _
            & "  ptsfr_si_to_id, " _
            & "  ptsfrd_pt_id, " _
            & "  ptsfrd_qty, " _
            & "  ptsfrd_qty_receive, " _
            & "  ptsfrd_um, " _
            & "  ptsfrd_lot_serial, " _
            & "  ptsfrd_cost, " _
            & "  from_cmaddr_mstr.cmaddr_name as from_cmaddr_name, " _
            & "  from_cmaddr_mstr.cmaddr_line_1 as from_cmaddr_line_1, " _
            & "  from_cmaddr_mstr.cmaddr_line_2 as from_cmaddr_line_2, " _
            & "  from_cmaddr_mstr.cmaddr_line_3 as from_cmaddr_line_3, " _
            & "  to_cmaddr_mstr.cmaddr_name as to_cmaddr_name, " _
            & "  to_cmaddr_mstr.cmaddr_line_1 as to_cmaddr_line_1, " _
            & "  to_cmaddr_mstr.cmaddr_line_2 as to_cmaddr_line_2, " _
            & "  to_cmaddr_mstr.cmaddr_line_3 as to_cmaddr_line_3, " _
            & "  from_loc_mstr.loc_desc as from_loc_desc, " _
            & "  to_loc_mstr.loc_desc as to_loc_desc, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_master.code_name as um_name, " _
            & "  coalesce(tranaprvd_name_5,'') as tranaprvd_name_5, coalesce(tranaprvd_name_6,'') as tranaprvd_name_6, coalesce(tranaprvd_name_7,'') as tranaprvd_name_7, coalesce(tranaprvd_name_8,'') as tranaprvd_name_8, " _
            & "  tranaprvd_pos_5, tranaprvd_pos_6, tranaprvd_pos_7, tranaprvd_pos_8 " _
            & "FROM  " _
            & "  ptsfr_mstr " _
            & "  inner join ptsfrd_det on ptsfrd_ptsfr_oid = ptsfr_oid " _
            & "  inner join loc_mstr from_loc_mstr on from_loc_mstr.loc_id = ptsfr_loc_id " _
            & "  inner join loc_mstr to_loc_mstr on to_loc_mstr.loc_id = ptsfr_loc_to_id " _
            & "  left outer join cmaddr_mstr from_cmaddr_mstr on from_cmaddr_mstr.cmaddr_id = ptsfr_en_id " _
            & "  left outer join cmaddr_mstr to_cmaddr_mstr on to_cmaddr_mstr.cmaddr_id = ptsfr_en_to_id " _
            & "  inner join pt_mstr on pt_id = ptsfrd_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = ptsfrd_um" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = ptsfr_oid " _
            & "  where ptsfr_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code") + "'"



        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRTransferReceiptPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_code")
        frm.ShowDialog()

    End Sub

    Public Overrides Function export_data() As Boolean
        'Return MyBase.export_data()
        Dim ssql As String
        Try
            ssql = "SELECT  " _
            & "  b.en_desc AS entity_from, " _
            & "  c.en_desc AS entity_to, " _
            & "  d.loc_desc AS location_from, " _
            & "  e.loc_desc AS location_to, " _
            & "  a.ptsfr_code, " _
            & "  a.ptsfr_date, " _
            & "  a.ptsfr_receive_date, " _
            & "  a.ptsfr_remarks, " _
            & "  f.pb_code AS inventory_request_code, " _
            & "  g.so_code AS sales_order_code, " _
            & "  i.pt_code, " _
            & "  i.pt_desc1,i.pt_desc2, " _
            & "  h.ptsfrd_qty as qty, " _
            & "  j.code_name as unit_measure " _
            & " " _
            & "FROM " _
            & "  public.ptsfr_mstr a " _
            & "  INNER JOIN public.loc_mstr d ON (a.ptsfr_loc_id = d.loc_id) " _
            & "  INNER JOIN public.loc_mstr e ON (a.ptsfr_loc_to_id = e.loc_id) " _
            & "  LEFT OUTER JOIN public.pb_mstr f ON (a.ptsfr_pb_oid = f.pb_oid) " _
            & "  INNER JOIN public.en_mstr b ON (a.ptsfr_en_id = b.en_id) " _
            & "  INNER JOIN public.en_mstr c ON (a.ptsfr_en_to_id = c.en_id) " _
            & "  LEFT OUTER JOIN public.so_mstr g ON (a.ptsfr_so_oid = g.so_oid) " _
            & "  INNER JOIN public.ptsfrd_det h ON (a.ptsfr_oid = h.ptsfrd_ptsfr_oid) " _
            & "  INNER JOIN public.pt_mstr i ON (h.ptsfrd_pt_id = i.pt_id) " _
            & "  INNER JOIN public.code_mstr j ON (h.ptsfrd_um = j.code_id) " _
            & "WHERE " _
            & "  a.ptsfr_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
            & " and ptsfr_en_to_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & " order by ptsfr_code,ptsfrd_seq"

            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Private Sub CekStokToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CekStokToolStripMenuItem.Click
        Try
            Dim my As master_new.MasterMDI = CType(Me.ParentForm, master_new.MasterMDI)

            Dim frm As New FInventoryReportDetailLog
            frm.MdiParent = my
            frm.set_window(my)
            frm.type_form = True
            frm._sql = "SELECT  " _
                      & "  invc_oid, " _
                      & "  invc_dom_id, " _
                      & "  invc_en_id, " _
                      & "  invc_si_id, " _
                      & "  invc_loc_id, " _
                      & "  invc_pt_id, " _
                      & "  invc_serial, " _
                      & "  sum(invc_qty) as invc_qty_sum, " _
                      & "  en_desc, " _
                      & "  si_desc, " _
                      & "  loc_desc, " _
                      & "  pt_code, " _
                      & "  pt_desc1, " _
                      & "  pt_desc2, " _
                      & "  pl_desc, " _
                      & "  pt_cost,um_mstr.code_name as um_name " _
                      & "FROM  " _
                      & "  invc_mstr " _
                      & "  inner join en_mstr on en_id = invc_en_id " _
                      & "  inner join si_mstr on si_id = invc_si_id " _
                      & "  inner join loc_mstr on loc_id = invc_loc_id " _
                      & "  inner join pt_mstr on pt_id = invc_pt_id " _
                      & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id  " _
                      & "  inner join pl_mstr on pt_pl_id = pl_id " _
                      & "  where invc_loc_id =" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_loc_git") & " " _
                      & " and invc_pt_id=" & gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "ptsfrd_pt_id") & " " _
                      & " and invc_en_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptsfr_en_id") & " " _
                      & "  group by invc_oid, " _
                      & "  invc_dom_id, " _
                      & "  invc_en_id, " _
                      & "  invc_si_id, " _
                      & "  invc_loc_id, " _
                      & "  invc_pt_id, " _
                      & "  invc_serial, " _
                      & "  en_desc, " _
                      & "  si_desc, " _
                      & "  loc_desc, " _
                      & "  pt_code, " _
                      & "  pt_desc1, " _
                      & "  pt_desc2, " _
                      & "  pl_desc, " _
                      & "  pt_cost, um_mstr.code_name "
            'frm.help_load_data(True)

            frm.Show()
            my.blb_retrieve_ItemClick(Nothing, Nothing)


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
