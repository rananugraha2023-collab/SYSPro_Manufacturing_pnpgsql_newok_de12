Imports master_new.ModFunction

Public Class FFixAssetMethodeSearch
    Public _row, _cu_id As Integer

    Private Sub FFixAssetMethodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "famt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "famt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Methode", "famt_method", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Salvage", "famt_salv", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Actual", "famt_actual", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Exp. Life", "famt_exp_life", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  famt_oid, " _
                    & "  famt_dom_id, " _
                    & "  famt_add_by, " _
                    & "  famt_add_date, " _
                    & "  famt_upd_by, " _
                    & "  famt_upd_date, " _
                    & "  famt_id, " _
                    & "  famt_code, " _
                    & "  famt_method, " _
                    & "  famt_conv, " _
                    & "  famt_desc, " _
                    & "  famt_salv, " _
                    & "  famt_actual, " _
                    & "  famt_exp_life, " _
                    & "  famt_dt " _
                    & "FROM  " _
                    & "  public.famt_mstr ;"

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

        If fobject.name = "FProductLine" Then
            fobject.gv_edit.SetRowCellValue(_row, "plfabk_famt_id", ds.Tables(0).Rows(_row_gv).Item("famt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "famt_code", ds.Tables(0).Rows(_row_gv).Item("famt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "plfabk_exp_life", ds.Tables(0).Rows(_row_gv).Item("famt_exp_life"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetMasterFinancial" Then
            fobject.gv_edit.SetRowCellValue(_row, "assbk_famt_id", ds.Tables(0).Rows(_row_gv).Item("famt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "famt_code", ds.Tables(0).Rows(_row_gv).Item("famt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "assbk_exp_life", ds.Tables(0).Rows(_row_gv).Item("famt_exp_life"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetRegister" Then
            fobject.gv_edit.SetRowCellValue(_row, "assbk_famt_id", ds.Tables(0).Rows(_row_gv).Item("famt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "famt_code", ds.Tables(0).Rows(_row_gv).Item("famt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "assbk_exp_life", ds.Tables(0).Rows(_row_gv).Item("famt_exp_life"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
