Imports master_new.ModFunction

Public Class FSubAccountSearch
    Public _row, _en_id As Integer

    Private Sub FSubAccountSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "sb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sb_en_id, " _
                    & "  en_code, " _
                    & "  sb_id, " _
                    & "  sb_code, " _
                    & "  sb_desc " _
                    & "FROM  " _
                    & "  public.sb_mstr" _
                    & " inner join public.en_mstr on en_id = sb_en_id" _
                    & " where (sb_code ~~* '%" + Trim(te_search.Text) + "%' or sb_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and sb_active ~~* 'Y'" _
                    & " and sb_en_id in (0," + _en_id.ToString + ")" _
                    & " order by sb_desc"

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
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrderFilm" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_sb_id", ds.Tables(0).Rows(_row_gv).Item("sb_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds.Tables(0).Rows(_row_gv).Item("sb_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FStandardTransaction" Then
            fobject.gv_edit.SetRowCellValue(_row, "glt_sb_id", ds.Tables(0).Rows(_row_gv).Item("sb_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds.Tables(0).Rows(_row_gv).Item("sb_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_sb_id", ds.Tables(0).Rows(_row_gv).Item("sb_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds.Tables(0).Rows(_row_gv).Item("sb_desc"))
            fobject.gv_edit.BestFitColumns()
        End If
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
