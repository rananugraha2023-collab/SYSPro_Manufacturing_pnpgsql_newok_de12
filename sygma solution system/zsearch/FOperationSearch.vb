Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FOperationSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _wo_oid As String
    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Operation", "woop_op", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "rod_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty WIP", "woop_qty_wip", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column(gv_master, "Qty Outstanding", "qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column(gv_master, "Qty Complete", "woop_qty_complete", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column(gv_master, "Qty Complete Hold", "woop_qty_complete_hold", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column(gv_master, "Qty Reject", "woop_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column(gv_master, "Qty Reject Hold", "woop_qty_reject_hold", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column(gv_master, "Qty Rework", "woop_qty_rework", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column(gv_master, "Complete", "woop_op_complete", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & " a.woop_oid, a.woop_op, " _
            & "  b.rod_desc, " _
            & "  a.woop_qty_wip, " _
            & "  a.woop_qty_complete, woop_qty_complete_hold," _
            & "  a.woop_qty_reject,woop_qty_reject_hold, " _
            & "  a.woop_qty_rework,woop_qty_rework_history, coalesce(a.woop_qty_wip,0) + coalesce(a.woop_qty_rework,0) - coalesce(a.woop_qty_complete,0) as qty_outstanding, " _
            & "  a.woop_op_complete " _
            & "FROM " _
            & "  public.woop_operation a " _
            & "  INNER JOIN public.rod_det b ON (a.woop_rod_oid = b.rod_oid) " _
            & "WHERE " _
            & "  a.woop_wo_oid = '" & _wo_oid & "' and b.rod_desc  ~~* '%" + Trim(te_search.Text) + "%' order by woop_op"

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

        Try

            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position


           If fobject.name = FLabourFeedback.Name Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("woop_op")
                fobject.__wolbr_woop_oid = ds.Tables(0).Rows(_row_gv).Item("woop_oid").ToString
            End If


        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub


End Class
