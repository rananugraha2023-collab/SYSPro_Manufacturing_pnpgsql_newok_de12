Imports master_new.ModFunction

Public Class FFixAssetBookSearch
    Public _row, _cu_id As Integer

    Private Sub FFixAssetBookSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "fabk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "fabk_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Deskripsi", "ac_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  fabk_oid, " _
                    & "  fabk_dom_id, " _
                    & "  fabk_add_by, " _
                    & "  fabk_add_date, " _
                    & "  fabk_upd_by, " _
                    & "  fabk_upd_date, " _
                    & "  fabk_id, " _
                    & "  fabk_code, " _
                    & "  fabk_desc, " _
                    & "  fabk_posted, " _
                    & "  fabk_dt " _
                    & "FROM  " _
                    & "  public.fabk_mstr "
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

        If fobject.name = "FAssetMasterFinancial" Then
            fobject.gv_edit.SetRowCellValue(_row, "assbk_fabk_id", ds.Tables(0).Rows(_row_gv).Item("fabk_id"))
            fobject.gv_edit.SetRowCellValue(_row, "fabk_code", ds.Tables(0).Rows(_row_gv).Item("fabk_code"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FProductLine" Then
            fobject.gv_edit.SetRowCellValue(_row, "plfabk_fabk_id", ds.Tables(0).Rows(_row_gv).Item("fabk_id"))
            fobject.gv_edit.SetRowCellValue(_row, "fabk_code", ds.Tables(0).Rows(_row_gv).Item("fabk_code"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetRegister" Then
            fobject.gv_edit.SetRowCellValue(_row, "assbk_fabk_id", ds.Tables(0).Rows(_row_gv).Item("fabk_id"))
            fobject.gv_edit.SetRowCellValue(_row, "fabk_code", ds.Tables(0).Rows(_row_gv).Item("fabk_code"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
