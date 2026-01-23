Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls

Public Class FInventoryReportDetail
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub FInventoryReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        CBChild.EditValue = False
        AddHandler gv_loc.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_loc.ColumnFilterChanged, AddressOf relation_detail
        CeNo0.EditValue = True
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'pr_entity.Properties.DataSource = dt_bantu
        'pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'pr_entity.ItemIndex = 0

        init_le(pr_entity, "en_mstr")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_loc, "invc_oid", False)
        add_column_copy(gv_loc, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Lot Number", "invc_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_loc, "Qty On Hand", "invc_qty_sum", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_loc, "Qty Allocated", "invc_qty_alloc_sum", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_loc, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_loc, "Price", "price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_loc, "Ext Cost", "invct_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_loc, "Ext Price", "price_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Deskripsi1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Deskripsi2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Product Line ", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Lot / Serial Number", "invc_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_serial, "Qty On Hand", "invc_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_serial, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_serial, "Ext Cost", "invct_cost_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_serial, "Ext Price", "price_ext", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column(gv_detail, "invc_oid", False)
        add_column_copy(gv_detail, "Sales Order Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Allocated", "qty_alloc_remain", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        Dim _en_id_all As String

        If CBChild.EditValue = True Then
            _en_id_all = get_en_id_child(pr_entity.EditValue)
        Else

            _en_id_all = pr_entity.EditValue

        End If

        Dim ssql As String

        'ssql = "insert into   SELECT pid_pt_id, max(t.pidd_price) as jml FROM public.pi_mstr r INNER JOIN public.pid_det s " _
        '           & "  ON(r.pi_oid = s.pid_pi_oid) INNER JOIN public.pidd_det t ON(s.pid_oid = t.pidd_pid_oid) " _
        '           & "  WHERE r.pi_id = " & pr_price_list.EditValue & " group by pid_pt_id "


        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                            & "  invc_oid, " _
                            & "  invc_dom_id, " _
                            & "  invc_en_id, " _
                            & "  invc_si_id, " _
                            & "  invc_loc_id, " _
                            & "  invc_pt_id, " _
                            & "  invc_serial, " _
                            & "  invc_qty as invc_qty_sum, " _
                            & "  invc_qty_alloc as invc_qty_alloc_sum, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  loc_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pl_desc, " _
                            & "  pt_cost, " _
                            & "  invct_cost, " _
                            & "  (invc_qty * invct_cost) as invct_cost_ext,coalesce(price,0) as price, (coalesce(price,0) * invc_qty) as price_ext " _
                            & "FROM  " _
                            & "  invc_mstr " _
                            & "  inner join en_mstr on en_id = invc_en_id " _
                            & "  inner join si_mstr on si_id = invc_si_id " _
                            & "  inner join loc_mstr on loc_id = invc_loc_id " _
                            & "  inner join pt_mstr on pt_id = invc_pt_id " _
                            & "  inner join pl_mstr on pt_pl_id = pl_id " _
                            & "  Left outer join invct_table on invct_pt_id = invc_pt_id  " _
                            & "  Left outer JOIN (select distinct pid_pt_id,price from public.pid_det inner join pi_mstr on (pi_oid=pid_pi_oid) inner join (select distinct pidd_pid_oid, max(pidd_price) as price from public.pidd_det group by pidd_det.pidd_pid_oid) as pidd_det_temp on pidd_pid_oid=pid_oid where pi_id=" & pr_price_list.EditValue & " ) as pid_det_temp on pid_pt_id=pt_id  " _
                            & "  where invc_en_id in (" + _en_id_all & ")   "

                        '& "  group by invc_oid, " _
                        '& "  invc_dom_id, " _
                        '& "  invc_en_id, " _
                        '& "  invc_si_id, " _
                        '& "  invc_loc_id, " _
                        '& "  invc_pt_id, " _
                        '& "  invc_serial, " _
                        '& "  en_desc, " _
                        '& "  si_desc, " _
                        '& "  loc_desc, " _
                        '& "  pt_code, " _
                        '& "  pt_desc1, " _
                        '& "  pt_desc2, " _
                        '& "  pl_desc, invct_cost, " _
                        '& "  pt_cost,price"

                        .InitializeCommand()
                        .FillDataSet(ds, "inv_location")
                        gc_loc.DataSource = ds.Tables("inv_location")

                        bestfit_column()
                        ConditionsAdjustment()

                        'Dim dt_temp As New DataTable
                        'Dim n As Integer = 1
                        'For Each dr As DataRow In ds.Tables("inv_location").Rows

                        '    If dr("invc_qty_sum") > 0 Then
                        '        ssql = "SELECT max(t.pidd_price) as jml FROM public.pi_mstr r INNER JOIN public.pid_det s " _
                        '               & "  ON(r.pi_oid = s.pid_pi_oid) INNER JOIN public.pidd_det t ON(s.pid_oid = t.pidd_pid_oid) " _
                        '               & "  WHERE r.pi_id = " & pr_price_list.EditValue & " AND s.pid_pt_id = " & dr("invc_pt_id")

                        '        dt_temp = GetTableData(ssql)
                        '        For Each dr_temp As DataRow In dt_temp.Rows
                        '            dr("price") = dr_temp(0)
                        '            dr("price_ext") = SetNumber(dr_temp(0)) * dr("invc_qty_sum")
                        '        Next
                        '        LblStatus.Text = "Status : " & n & " of " & ds.Tables("inv_location").Rows.Count
                        '    End If

                        '    System.Windows.Forms.Application.DoEvents()
                        '    n += 1
                        'Next
                        'LblStatus.Text = "Status : Finish load data."
                        'ds.Tables("inv_location").AcceptChanges()
                        'gv_loc.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub relation_detail()
        Try
            'gv_detail.Columns("invc_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("invc_oid='" & ds.Tables("inv_location").Rows(BindingContext(ds.Tables("inv_location")).Position).Item("invc_oid").ToString & "'")
            'gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub pr_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pr_entity.EditValueChanged
        Dim _en_id_coll As String = func_data.entity_parent(pr_entity.EditValue)
        Dim dt As New DataTable
        Dim ssql As String
        Try
            ssql = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
                 & " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
                 & " AND pi_en_id in (" & _en_id_coll + ")" _
                 & " AND pi_start_date <= " + SetDate(CekTanggal) + " and pi_end_date >= " + SetDate(CekTanggal) _
                 & " AND pi_active ~~* 'Y' " _
                 & " order by pi_desc"

            dt = GetTableData(ssql)
            With pr_price_list
                If .Properties.Columns.VisibleCount = 0 Then
                    .Properties.Columns.Add(New LookUpColumnInfo("pi_id", "ID", 10))
                    .Properties.Columns.Add(New LookUpColumnInfo("pi_desc", "Code", 15))
                End If

                .Properties.DataSource = dt
                .Properties.DisplayMember = dt.Columns("pi_desc").ToString
                .Properties.ValueMember = dt.Columns("pi_id").ToString
                If dt.Rows.Count > 0 Then
                    .EditValue = dt.Rows(0).Item("pi_id")
                End If
                .Properties.DropDownRows = 30
                .Properties.TextEditStyle = TextEditStyles.Standard
                .Properties.PopupWidth = 400
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub CeNo0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CeNo0.CheckedChanged
        Try
            If CeNo0.EditValue = True Then
                gv_loc.ActiveFilterString = "[invc_qty_sum]<>0"
            Else
                gv_loc.ActiveFilterString = ""
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
