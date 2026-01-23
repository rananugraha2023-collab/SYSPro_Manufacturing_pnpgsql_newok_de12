Imports master_new.ModFunction

Public Class FInventorySearch
    Public _row, _en_id, _si_id, _pt_id, _qty As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data

    Private Sub FInventorySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Item", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty Available", "invc_qty_open", DevExpress.Utils.HorzAlignment.Default)
        
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        get_sequel = ""

       
        get_sequel = "SELECT  " _
            & "  public.invc_mstr.invc_oid, " _
            & "  public.invc_mstr.invc_dom_id, " _
            & "  public.invc_mstr.invc_en_id, " _
            & "  public.invc_mstr.invc_si_id, " _
            & "  public.invc_mstr.invc_loc_id, " _
            & "  public.invc_mstr.invc_pt_id, " _
            & "  public.invc_mstr.invc_qty, " _
            & "  public.invc_mstr.invc_serial, " _
            & "  public.invc_mstr.invc_qty_alloc, " _
            & "  coalesce(public.invc_mstr.invc_qty,0) - coalesce(public.invc_mstr.invc_qty_alloc,0) as invc_qty_open, " _
            & "  public.loc_mstr.loc_id, " _
            & "  public.loc_mstr.loc_desc, " _
            & "  public.si_mstr.si_id, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.pt_mstr.pt_id, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.en_mstr.en_id, " _
            & "  public.en_mstr.en_desc " _
            & "FROM " _
            & "  public.invc_mstr " _
            & "  INNER JOIN public.en_mstr ON (public.invc_mstr.invc_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.si_mstr ON (public.invc_mstr.invc_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.pt_mstr ON (public.invc_mstr.invc_pt_id = public.pt_mstr.pt_id)" _
            & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id)" _
            & " where (loc_desc ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
            & " and invc_si_id = " + _si_id.ToString _
            & " and invc_pt_id = " + _pt_id.ToString _
            & " and invc_en_id in (" + _en_id_coll + ")"
       
        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FSalesOrderManualAllocation" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", ds.Tables(0).Rows(_row_gv).Item("invc_oid").ToString)
            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_loc_id", ds.Tables(0).Rows(_row_gv).Item("invc_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc_invc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))

            If ds.Tables(0).Rows(_row_gv).Item("invc_qty_open") >= _qty Then
                fobject.gv_edit.SetRowCellValue(_row, "sod_qty_allocated", _qty)
            Else
                fobject.gv_edit.SetRowCellValue(_row, "sod_qty_allocated", ds.Tables(0).Rows(_row_gv).Item("invc_qty_open"))
            End If

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

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
