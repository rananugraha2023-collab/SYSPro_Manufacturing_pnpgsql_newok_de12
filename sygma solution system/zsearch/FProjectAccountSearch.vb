Imports master_new.ModFunction

Public Class FProjectAccountSearch
    Public _row, _en_id As Integer

    Private Sub FProjectAccountSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date Project", "pjc_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  en_code, " _
                    & "  pjc_id, " _
                    & "  pjc_code, " _
                    & "  pjc_date, " _
                    & "  pjc_desc " _
                    & "FROM  " _
                    & "  public.pjc_mstr" _
                    & " inner join public.en_mstr on en_id = pjc_en_id" _
                    & " where (pjc_code ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and pjc_active ~~* 'Y'" _
                    & " and pjc_en_id in (0," + _en_id.ToString + ")" _
                    & " order by pjc_code"

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
            fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit.BestFitColumns()
            'har 20110618
        ElseIf fobject.name = FWorkOrder.Name Then
            fobject.wo_pjc_id.text = ds.Tables(0).Rows(_row_gv).Item("pjc_code")
            fobject.wo_pjc_id.tag = ds.Tables(0).Rows(_row_gv).Item("pjc_id")
            fobject.pjc_desc.text = ds.Tables(0).Rows(_row_gv).Item("pjc_desc")
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
