Imports master_new.ModFunction

Public Class FSiteSearch
    Public _row, _en_id As Integer
    Public grid_call As String = ""

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "si_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "si_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  en_code, " _
                    & "  si_id, " _
                    & "  si_code, " _
                    & "  si_desc " _
                    & " FROM  " _
                    & "  public.si_mstr" _
                    & " inner join public.en_mstr on en_id = si_en_id" _
                    & " where (si_code ~~* '%" + Trim(te_search.Text) + "%' or si_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and si_active ~~* 'Y'" _
                    & " and si_en_id in (0," + _en_id.ToString + ")"
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
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

        If fobject.name = "FPurchaseOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceipts" Or fobject.name = "FInventoryReceiptsLintas" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Or fobject.name = "FInventoryIssuesLintas" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryRequest" Or fobject.name = "FInventoryRequestBranch" Or fobject.name = "FInventoryRequestWO" Or fobject.name = "FInventoryRequestCabang" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FProjectMaintenance" Then
            If grid_call = "gv_edit" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
                fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf grid_call = "gv_edit_cust" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
                fobject.gv_edit_cust.BestFitColumns()
            End If
        ElseIf fobject.name = FWorkOrderIssue.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wocid_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
