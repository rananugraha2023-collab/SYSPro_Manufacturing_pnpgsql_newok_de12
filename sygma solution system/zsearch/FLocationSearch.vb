Imports master_new.ModFunction

Public Class FLocationSearch
    Public _row, _en_id As Integer
    Public _obj, _sls_id As Object
    Public grid_call As String = ""
    Public _pil As Integer

    Private Sub FLocationSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Warehouse", "wh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Type", "loc_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category", "loc_cat_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        If fobject.name = "FCanvasingPlanner" Then
            get_sequel = "SELECT  " _
                        & "  loc_oid, " _
                        & "  loc_dom_id, " _
                        & "  loc_en_id, " _
                        & "  en_code, " _
                        & "  loc_id, " _
                        & "  loc_wh_id, " _
                        & "  wh_desc, " _
                        & "  loc_si_id, " _
                        & "  si_desc, " _
                        & "  en_desc, " _
                        & "  loc_code, " _
                        & "  loc_desc, " _
                        & "  loc_type, " _
                        & "  loc_type_mstr.code_name as loc_type_name, " _
                        & "  loc_cat, " _
                        & "  loc_cat_mstr.code_name as loc_cat_name, " _
                        & "  loc_is_id, " _
                        & "  is_desc, " _
                        & "  loc_active, " _
                        & "  loc_dt " _
                        & "FROM  " _
                        & "  public.loc_mstr" _
                        & " inner join en_mstr on en_id = loc_en_id " _
                        & " inner join wh_mstr on wh_id = loc_wh_id " _
                        & " inner join si_mstr on si_id = loc_si_id " _
                        & " inner join is_mstr on is_id = loc_is_id " _
                        & " inner join code_mstr as loc_type_mstr on loc_type_mstr.code_id = loc_type" _
                        & " inner join code_mstr as loc_cat_mstr on loc_cat_mstr.code_id = loc_cat" _
                        & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and loc_active ~~* 'Y'" _
                        & " and loc_desc <> '-'" _
                        & " and loc_en_id in (0," + _en_id.ToString + ")" _
                        & " and loc_ptnr_id = " + _sls_id.ToString

        ElseIf fobject.name = "FSalesOrderShipment" Then
            get_sequel = "SELECT  " _
                        & "  loc_oid, " _
                        & "  loc_dom_id, " _
                        & "  loc_en_id, " _
                        & "  en_code, " _
                        & "  loc_id, " _
                        & "  loc_wh_id, " _
                        & "  wh_desc, " _
                        & "  loc_si_id, " _
                        & "  si_desc, " _
                        & "  en_desc, " _
                        & "  loc_code, " _
                        & "  loc_desc, " _
                        & "  loc_type, " _
                        & "  loc_type_mstr.code_name as loc_type_name, " _
                        & "  loc_cat, " _
                        & "  loc_cat_mstr.code_name as loc_cat_name, " _
                        & "  loc_is_id, " _
                        & "  is_desc, " _
                        & "  loc_active, " _
                        & "  loc_dt " _
                        & "FROM  " _
                        & "  public.loc_mstr" _
                        & " inner join en_mstr on en_id = loc_en_id " _
                        & " inner join wh_mstr on wh_id = loc_wh_id " _
                        & " inner join si_mstr on si_id = loc_si_id " _
                        & " inner join is_mstr on is_id = loc_is_id " _
                        & " inner join code_mstr as loc_type_mstr on loc_type_mstr.code_id = loc_type" _
                        & " inner join code_mstr as loc_cat_mstr on loc_cat_mstr.code_id = loc_cat" _
                        & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and loc_active ~~* 'Y'" _
                        & " and loc_desc <> '-'" _
                        & " and loc_git = 'N'" _
                        & " and loc_en_id in (0," + _en_id.ToString + ")"
        Else

            get_sequel = "SELECT  " _
                        & "  loc_oid, " _
                        & "  loc_dom_id, " _
                        & "  loc_en_id, " _
                        & "  en_code, " _
                        & "  loc_id, " _
                        & "  loc_wh_id, " _
                        & "  wh_desc, " _
                        & "  loc_si_id, " _
                        & "  si_desc, " _
                        & "  en_desc, " _
                        & "  loc_code, " _
                        & "  loc_desc, " _
                        & "  loc_type, " _
                        & "  loc_type_mstr.code_name as loc_type_name, " _
                        & "  loc_cat, " _
                        & "  loc_cat_mstr.code_name as loc_cat_name, " _
                        & "  loc_is_id, " _
                        & "  is_desc, " _
                        & "  loc_active, " _
                        & "  loc_dt " _
                        & "FROM  " _
                        & "  public.loc_mstr" _
                        & " inner join en_mstr on en_id = loc_en_id " _
                        & " inner join wh_mstr on wh_id = loc_wh_id " _
                        & " inner join si_mstr on si_id = loc_si_id " _
                        & " inner join is_mstr on is_id = loc_is_id " _
                        & " inner join code_mstr as loc_type_mstr on loc_type_mstr.code_id = loc_type" _
                        & " inner join code_mstr as loc_cat_mstr on loc_cat_mstr.code_id = loc_cat" _
                        & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and loc_active ~~* 'Y'" _
                        & " and loc_desc <> '-'" _
                        & " and loc_git = 'N'" _
                        & " and loc_en_id in (0," + _en_id.ToString + ")"
        End If
        Return get_sequel
    End Function

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
        Try
            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position

            If fobject.name = "FPurchaseReceipt" Then
                fobject.gv_edit.SetRowCellValue(_row, "rcvd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FPurchaseReceiptCrossSelling" Then
                fobject.gv_edit.SetRowCellValue(_row, "rcvd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FPurchaseReturn" Then
                fobject.gv_edit.SetRowCellValue(_row, "rcvd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryReceipts" Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryIssues" Or fobject.name = FInventoryIssuesLintas.Name Or fobject.name = FInventoryReceiptsLintas.Name Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FInventoryBeginingBalance" Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FSalesOrderShipment" Then
                fobject.gv_edit.SetRowCellValue(_row, "soshipd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FSalesOrderReturn" Then
                fobject.gv_edit.SetRowCellValue(_row, "soshipd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FSalesOrder" Then
                fobject.gv_edit.SetRowCellValue(_row, "sod_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()

                'ElseIf fobject.name = "FCanvasingPlanner" Then
                '    fobject.gv_edit.SetRowCellValue(_row, "cnv_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                '    fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                '    fobject.gv_edit.BestFitColumns()

                'ElseIf fobject.name = "FCanvasingPlanner" Then
                '    '_obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
                '    'fobject._pi_id = ds.Tables(0).Rows(_row_gv).Item("pi_id")
                '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("loc_id")
                '    _obj.tag = ds.Tables(0).Rows(_row_gv).Item("loc_desc")

            ElseIf fobject.name = FSalesQuotationConsigment.Name Then
                fobject.gv_edit.SetRowCellValue(_row, "sqd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()

            ElseIf fobject.name = FTransferIssuesMultiLoc.Name Then
                fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_loc_from_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()

            ElseIf fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrderFilm" Or fobject.name = "FPurchaseOrderICrossSelling" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = FWorkOrderIssue.Name Then
                fobject.gv_edit.SetRowCellValue(_row, "wocid_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = FInventoryIssuesHadiah.Name Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = "FProjectMaintenance" Then
                If grid_call = "gv_edit" Then
                    fobject.gv_edit.SetRowCellValue(_row, "prjd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                    fobject.gv_edit.BestFitColumns()
                ElseIf grid_call = "gv_edit_cust" Then
                    fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                    fobject.gv_edit_cust.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                    fobject.gv_edit_cust.BestFitColumns()
                End If

            ElseIf fobject.name = "FInventoryDisAssembly" Or fobject.name = "FInventoryAssembly" Then
                If _pil = 1 Then
                    fobject._loc_id = ds.Tables(0).Rows(_row_gv).Item("loc_id")
                    fobject.asmb_loc.Text = ds.Tables(0).Rows(_row_gv).Item("loc_desc")

                ElseIf _pil = 2 Then
                    fobject.gv_edit.SetRowCellValue(_row, "loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                    fobject.gv_edit.BestFitColumns()
                End If
            ElseIf fobject.name = FSalesQuotation.Name Then
                fobject.gv_edit.SetRowCellValue(_row, "sqd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf fobject.name = FInventoryReportDate.Name Then
                fobject.be_loc.text = ds.Tables(0).Rows(_row_gv).Item("loc_desc")
                fobject.be_loc.tag = ds.Tables(0).Rows(_row_gv).Item("loc_id")
            End If
        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
