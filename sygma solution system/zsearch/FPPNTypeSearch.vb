Public Class FPPNTypeSearch
    Public _row As Integer

    Private Sub FPPNTypeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "PPN Type", "display", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select 'E' as value, 'PPN Bebas' as display " _
                            & "union " _
                            & "select 'A' as value, 'PPN Bayar' as display " _
                            & "union " _
                            & "select 'N' as value, 'None' as display "
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

        If fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_ppn_type", ds.Tables(0).Rows(_row_gv).Item("value"))
        ElseIf fobject.name = FSalesQuotationConsigment.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "sqd_ppn_type", ds.Tables(0).Rows(_row_gv).Item("value"))
        End If
    End Sub
End Class
