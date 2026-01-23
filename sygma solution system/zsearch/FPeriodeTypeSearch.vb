Public Class FPeriodeTypeSearch
    Public _row As Integer
    Public grid_call As String = ""
    Public _obj As Object
    Public _objk As Object
    Public _en_id, _code As Integer
    Public _year As Date

    Private Sub FPeriodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Period Tyoe", "period_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Month Count", "period_type_month_count", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.period_type_id, " _
                & "  a.period_type_name, " _
                & "  a.period_type_month_count " _
                & "FROM " _
                & "  public.period_type_mstr a " _
                & "ORDER BY " _
                & "  a.period_type_id"

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

        If fobject.name = "FRSQuarterPeriode" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("period_type_name")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("period_type_id")
            fobject.qrtr_period_count.editvalue = ds.Tables(0).Rows(_row_gv).Item("period_type_month_count")
        ElseIf fobject.name = "FGenFSN" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("period_type_name")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("period_type_id")
        ElseIf fobject.name = "FBonusInsentifMitraGenerator" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("period_type_name")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("period_type_id")
        End If
    End Sub
End Class
