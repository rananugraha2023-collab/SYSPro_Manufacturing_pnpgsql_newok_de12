Public Class FEntitySearch
    Public _row As Integer
    Public grid_call As String = ""

    Private Sub FEntitySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "en_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  en_oid, " _
                    & "  en_dom_id, " _
                    & "  en_add_by, " _
                    & "  en_add_date, " _
                    & "  en_upd_by, " _
                    & "  en_upd_date, " _
                    & "  en_id, " _
                    & "  en_code, " _
                    & "  en_desc, " _
                    & "  en_parent, " _
                    & "  en_active, " _
                    & "  en_dt " _
                    & "FROM  " _
                    & "  public.en_mstr " _
                    & " where (en_desc ~~* '%" + Trim(te_search.Text) + "%' or en_code ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y' " _
                    & " and en_id <> 0 " _
                    & " and en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & " order by en_code "
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
            fobject.gv_edit.SetRowCellValue(_row, "pod_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        ElseIf fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        ElseIf fobject.name = "FGLCalendar" Then
            fobject.gv_edit.SetRowCellValue(_row, "gcald_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        ElseIf fobject.name = "FSalesOrderShipment" Then
            fobject.gv_edit.SetRowCellValue(_row, "soshipd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryRequest" Or fobject.name = "FInventoryRequestCabang" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        ElseIf fobject.name = "FProjectMaintenance" Then
            If grid_call = "gv_edit" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
                fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            ElseIf grid_call = "gv_edit_cust" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_en_id", ds.Tables(0).Rows(_row_gv).Item("en_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            End If
        End If
    End Sub
End Class
