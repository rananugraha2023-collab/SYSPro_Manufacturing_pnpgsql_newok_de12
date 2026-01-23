Imports master_new.ModFunction

Public Class FPSSearch
    Public _row, _en_id As Integer

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "ps_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Use BOM", "ps_use_bom", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Bom Code", "bom_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Bom Desc", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number Name", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ps_oid, " _
                & "  a.ps_en_id, " _
                & "  d.en_desc, " _
                & "  a.ps_id, " _
                & "  a.ps_code, " _
                & "  a.ps_desc, " _
                & "  a.ps_use_bom, " _
                & "  a.ps_bom_id, " _
                & "  c.bom_code,c.bom_desc, " _
                & "  a.ps_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1 " _
                & "FROM " _
                & "  public.ps_mstr a " _
                & "  LEFT OUTER JOIN public.pt_mstr b ON (a.ps_pt_id = b.pt_id) " _
                & "  LEFT OUTER JOIN public.bom_mstr c ON (a.ps_bom_id = c.bom_id) " _
                & "  INNER JOIN public.en_mstr d ON (a.ps_en_id = d.en_id) " _
                & " Where a.ps_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") and ps_active='Y'" _
                & "ORDER BY " _
                & "  a.ps_code"

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

    Public Overrides Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FPartNumberSubtitute" Then
            fobject.pts_ps_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ps_code")
            fobject._ps_id = ds.Tables(0).Rows(_row_gv).Item("ps_id")
        End If
    End Sub
End Class
