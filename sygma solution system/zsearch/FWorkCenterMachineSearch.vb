Imports master_new.ModFunction

Public Class FWorkCenterMachineSearch
    Public _row, _en_id As Integer

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Work Center Code", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Work Center Desc", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Machine Code", "mch_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Machine Name", "mch_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Work Center Machine Desc", "wcm_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  b.en_desc, " _
                & "  a.wcm_id, " _
                & "  a.wcm_wc_id, " _
                & "  c.wc_code, " _
                & "  a.wcm_mch_id, " _
                & "  d.mch_code, " _
                & "  d.mch_name, " _
                & "  a.wcm_desc, " _
                & "  c.wc_desc " _
                & "FROM " _
                & "  public.en_mstr b " _
                & "  INNER JOIN public.wcm_mstr a ON (b.en_id = a.wcm_en_id) " _
                & "  INNER JOIN public.wc_mstr c ON (a.wcm_wc_id = c.wc_id) " _
                & "  INNER JOIN public.mch_mstr d ON (a.wcm_mch_id = d.mch_id) " _
                & " Where a.wcm_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "ORDER BY " _
                & "  a.wcm_id"

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

        If fobject.name = "FRouting" Then
            'fobject.pts_ps_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("wcm_desc")
            'fobject._ps_id = ds.Tables(0).Rows(_row_gv).Item("wcm_id")

            fobject.gv_edit.SetRowCellValue(_row, "wcm_desc", ds.Tables(0).Rows(_row_gv).Item("wcm_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "rod_wcm_id", ds.Tables(0).Rows(_row_gv).Item("wcm_id"))
        End If
    End Sub
End Class
