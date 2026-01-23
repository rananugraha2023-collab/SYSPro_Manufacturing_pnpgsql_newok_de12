Imports master_new.ModFunction

Public Class FProductRequestSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Dim _conf_value As String
    Dim _conf_limit_date As String
    Public par_wo_id As String

    Private Sub FInventoryRequestSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
        '_conf_value = func_coll.get_conf_file("wf_inventory_request")
        '_conf_limit_date = ""
        '_conf_limit_date = func_coll.get_conf_file("inventory_request_limit_date")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Product Request Number", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Product Request Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Requested By", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = FInventoryRequest.Name Then
            get_sequel = "SELECT distinct " _
                        & "  pb_oid, " _
                        & "  pb_dom_id, " _
                        & "  pb_en_id, " _
                        & "  pb_date, " _
                        & "  pb_due_date, " _
                        & "  pb_requested, " _
                        & "  pb_end_user, " _
                        & "  pb_rmks, " _
                        & "  pb_status, " _
                        & "  pb_close_date, " _
                        & "  pb_dt, " _
                        & "  pb_code, " _
                        & "  en_desc,pb_pbt_code " _
                        & "FROM  " _
                        & "  public.pb2_mstr " _
                        & "  inner join en_mstr on en_id = pb_en_id " _
                        & "  inner join pb2d_det on pbd_pb_oid = pb_oid " _
                        & " where pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and pb_en_id = " + _en_id.ToString _
                        & "  and (pb_status = '' or pb_status is null) and coalesce(pbd_qty,0)-coalesce(pbd_qty_processed,0) > 0 "


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

        Dim ds_bantu As New DataSet
        Dim i As Integer

        If fobject.name = FInventoryRequest.Name Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pbd_oid, " _
                            & "  pbd_dom_id,pbd_si_id, " _
                            & "  pbd_en_id,en_desc,si_desc, " _
                            & "  pbd_add_by, " _
                            & "  pbd_add_date, " _
                            & "  pbd_upd_by, " _
                            & "  pbd_upd_date, " _
                            & "  pbd_pb_oid, " _
                            & "  pbd_seq, " _
                            & "  pbd_pt_id,pb_pbt_code, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_cost, " _
                            & "  invct_cost, " _
                            & "  pbd_rmks, " _
                            & "  pbd_end_user, " _
                            & "  pbd_qty, " _
                            & "  pbd_qty_processed, " _
                            & "  pbd_qty_completed, " _
                            & "  pbd_qty - coalesce(pbd_qty_processed,0) as qty_open, " _
                            & "  pbd_um, " _
                            & "  um_master.code_name as code_name, " _
                            & "  pbd_due_date, " _
                            & "  pbd_status, " _
                            & "  pbd_dt,pb_code,pb_oid ,ircd_qty_approve " _
                            & "FROM  " _
                            & "  public.pb2d_det " _
                            & "  inner join pb2_mstr on pb_oid = pbd_pb_oid " _
                            & "  inner join pt_mstr on pt_id = pbd_pt_id " _
                            & "  inner join en_mstr on en_id = pbd_en_id " _
                            & "  inner join code_mstr um_master on um_master.code_id = pbd_um" _
                            & "  inner join invct_table on invct_pt_id = pbd_pt_id " _
                            & "  inner join si_mstr on si_id = invct_si_id " _
                            & "  left outer join ircd_det on ircd_pbd_oid = pbd_oid " _
                            & "  where (pbd_qty - coalesce(pbd_qty_processed,0)) > 0 " _
                            & "  and pbd_pb_oid = '" + ds.Tables(0).Rows(_row_gv).Item("pb_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pbd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                If i = 0 Then
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_oid", Guid.NewGuid.ToString)
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_en_id", ds_bantu.Tables(0).Rows(i).Item("pbd_en_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds_bantu.Tables(0).Rows(i).Item("en_desc"))
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_si_id", ds_bantu.Tables(0).Rows(i).Item("pbd_si_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds_bantu.Tables(0).Rows(i).Item("si_desc"))

                    fobject.gv_edit.SetRowCellValue(_row, "pbd_pb2_oid", ds_bantu.Tables(0).Rows(i).Item("pb_oid").ToString)
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_pb2det_oid", ds_bantu.Tables(0).Rows(i).Item("pbd_oid").ToString)
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_pb2_code", ds_bantu.Tables(0).Rows(i).Item("pb_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_pt_id", ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id"))

                    fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds_bantu.Tables(0).Rows(i).Item("pt_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds_bantu.Tables(0).Rows(i).Item("pt_desc1"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds_bantu.Tables(0).Rows(i).Item("pt_desc2"))
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_rmks", ds_bantu.Tables(0).Rows(i).Item("pbd_rmks"))

                    fobject.gv_edit.SetRowCellValue(_row, "pbd_end_user", ds_bantu.Tables(0).Rows(i).Item("pbd_end_user"))
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_qty", ds_bantu.Tables(0).Rows(i).Item("qty_open"))
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_um", ds_bantu.Tables(0).Rows(i).Item("pbd_um"))
                    fobject.gv_edit.SetRowCellValue(_row, "code_name", ds_bantu.Tables(0).Rows(i).Item("code_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "pbd_due_date", ds_bantu.Tables(0).Rows(i).Item("pbd_due_date"))

                Else
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("pbd_oid") = Guid.NewGuid.ToString

                    _dtrow("pbd_en_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_en_id")
                    _dtrow("en_desc") = ds_bantu.Tables(0).Rows(i).Item("en_desc")
                    _dtrow("pbd_si_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_si_id")
                    _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")

                    _dtrow("pbd_pb2_oid") = ds_bantu.Tables(0).Rows(i).Item("pb_oid").ToString
                    _dtrow("pbd_pb2det_oid") = ds_bantu.Tables(0).Rows(i).Item("pbd_oid").ToString
                    _dtrow("pbd_pb2_code") = ds_bantu.Tables(0).Rows(i).Item("pb_code")



                    _dtrow("pbd_pt_id") = ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                    _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                    _dtrow("pbd_rmks") = ds_bantu.Tables(0).Rows(i).Item("pbd_rmks")
                    _dtrow("pbd_end_user") = ds_bantu.Tables(0).Rows(i).Item("pbd_end_user")


                    _dtrow("pbd_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("pbd_um") = ds_bantu.Tables(0).Rows(i).Item("pbd_um")
                    _dtrow("code_name") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                    _dtrow("pbd_due_date") = ds_bantu.Tables(0).Rows(i).Item("pbd_due_date")

                   
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                End If

            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            'fobject.ptsfr_pb_oid.enabled = False
            'fobject.ptsfr_sq_oid.enabled = False
            'fobject.ptsfr_sq_oid.text = ""
            'fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            'fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False


        End If

    End Sub
End Class
