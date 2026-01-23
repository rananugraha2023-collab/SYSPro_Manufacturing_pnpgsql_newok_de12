Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FPreOrderSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _filter As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Preorder Number", "preorder_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty", "preorder_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Qty SQ", "preorder_qty_sq", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Qty SO", "preorder_qty_so", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Function get_sequel() As String

        
        get_sequel = "SELECT  " _
                & "  a.preorder_oid,preorder_code, " _
                & "  a.preorder_date, " _
                & "  a.preorder_en_id, " _
                & "  b.en_code, " _
                & "  b.en_desc, " _
                & "  a.preorder_dom_id, " _
                & "  a.preorder_pt_id, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  c.pt_desc2, " _
                & "  a.preorder_qty, " _
                & "  a.preorder_qty_sq, " _
                & "  a.preorder_qty_so, " _
                & "  a.preorder_active, " _
                & "  a.preorder_remarks, " _
                & "  a.preorder_add_by, " _
                & "  a.preorder_add_date, " _
                & "  a.preorder_upd_by, " _
                & "  a.preorder_upd_date " _
                & "FROM " _
                & "  public.preorder_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.preorder_en_id = b.en_id) " _
                & "  INNER JOIN public.pt_mstr c ON (a.preorder_pt_id = c.pt_id) " _
                & "WHERE " _
                & "  c.pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%'"

        If _filter <> "" Then
            get_sequel += _filter
        End If

        get_sequel += "  order by a.preorder_code"

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
        'Dim sSQL As String
        Try
            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position

            If fobject.name = FSalesQuotationConsigment.Name Then
                'fobject.gv_edit.SetRowCellValue(_row, "prjd_cse_id", ds.Tables(0).Rows(_row_gv).Item("cse_id"))
                'fobject.gv_edit.SetRowCellValue(_row, "cse_code", ds.Tables(0).Rows(_row_gv).Item("cse_code"))
                'fobject.gv_edit.SetRowCellValue(_row, "cse_desc", ds.Tables(0).Rows(_row_gv).Item("cse_desc"))

                'fobject.gv_edit.BestFitColumns()

                _obj.text = ds.Tables(0).Rows(_row_gv).Item("preorder_code")

           
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
