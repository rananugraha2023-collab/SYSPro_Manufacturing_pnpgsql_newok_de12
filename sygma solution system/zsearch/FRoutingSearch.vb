Imports master_new.ModFunction

Public Class FRoutingSearch
    Public _row, _en_id, _pt_id As Integer
    Dim sSQL As String
    Public _obj As Object
    Private Sub FRoutingSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Routing Code", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cost Set", "cs_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        

        If fobject.name = FWorkOrder.Name Then

            get_sequel = "select en_desc,ro_code, ro_id, ro_desc,ro_insheet from ro_mstr " _
                & " inner join en_mstr on ro_en_id = en_id " _
                & " where ro_active ~~* 'Y'" _
                & " AND ro_dom_id = " & master_new.ClsVar.sdom_id _
                & " and ro_en_id in (0," + _en_id.ToString + ")" _
                & " and (ro_code ~~* '%" + Trim(te_search.Text) + "%' or ro_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                & " and ro_pt_id = " + _pt_id.ToString _
                & " order by ro_desc"
        ElseIf fobject.name = FRouting.Name Then

            get_sequel = "select en_desc,ro_code, ro_id, ro_desc,ro_insheet,ro_oid from ro_mstr " _
                & " inner join en_mstr on ro_en_id = en_id " _
                & " where ro_active ~~* 'Y'" _
                & " AND ro_dom_id = " & master_new.ClsVar.sdom_id _
                & " and ro_en_id in (0," + _en_id.ToString + ")" _
                & " and (ro_code ~~* '%" + Trim(te_search.Text) + "%' or ro_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                & " order by ro_desc"

        Else

            get_sequel = "select en_desc,ro_code, ro_id, ro_desc from ro_mstr " _
                     & " inner join en_mstr on ro_en_id = en_id " _
                     & " where ro_active ~~* 'Y'" _
                     & " AND ro_dom_id = " & master_new.ClsVar.sdom_id _
                     & " and ro_en_id in (0," + _en_id.ToString + ")" _
                     & " and (ro_code ~~* '%" + Trim(te_search.Text) + "%' or ro_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                     & " order by ro_desc"
        End If
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

        If fobject.name = FWorkOrder.Name Then
            'fobject.gv_edit.SetRowCellValue(_row, "rod_wc_id", ds.Tables(0).Rows(_row_gv).Item("wc_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "wc_desc", ds.Tables(0).Rows(_row_gv).Item("wc_desc"))
            'fobject.gv_edit.BestFitColumns()

            fobject.wo_ro_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ro_code")) & ", " & SetString(ds.Tables(0).Rows(_row_gv).Item("ro_desc"))
            fobject.wo_ro_id.tag = ds.Tables(0).Rows(_row_gv).Item("ro_id")
            fobject.wo_insheet_pct.editvalue = ds.Tables(0).Rows(_row_gv).Item("ro_insheet")
        ElseIf fobject.name = FRouting.Name Then
            fobject.ro_copy.editvalue = SetString(ds.Tables(0).Rows(_row_gv).Item("ro_code")) & " " & SetString(ds.Tables(0).Rows(_row_gv).Item("ro_desc"))
            sSQL = "SELECT  " _
                & "  a.rod_op, " _
                & "  b.op_name, " _
                & "  a.rod_start_date, " _
                & "  a.rod_end_date, " _
                & "  a.rod_wc_id, " _
                & "  c.wc_desc, " _
                & "  a.rod_desc, " _
                & "  a.rod_insheet_pct, " _
                & "  a.rod_seq " _
                & "FROM " _
                & "  public.rod_det a " _
                & "  INNER JOIN public.wc_mstr c ON (a.rod_wc_id = c.wc_id) " _
                & "  left outer JOIN public.op_mstr b ON (a.rod_op = b.op_code) " _
                & "WHERE " _
                & "  a.rod_ro_oid = '" & ds.Tables(0).Rows(_row_gv).Item("ro_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.rod_seq"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)
            Dim i As Integer = 0
            For Each dr As DataRow In dt.Rows

                fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.Append)
                fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                fobject.gv_edit.SetRowCellValue(i, "rod_op", dr("rod_op"))
                fobject.gv_edit.SetRowCellValue(i, "op_name", dr("op_name"))
                fobject.gv_edit.SetRowCellValue(i, "rod_start_date", dr("rod_start_date"))
                fobject.gv_edit.SetRowCellValue(i, "rod_end_date", dr("rod_end_date"))
                fobject.gv_edit.SetRowCellValue(i, "rod_wc_id", dr("rod_wc_id"))
                fobject.gv_edit.SetRowCellValue(i, "wc_desc", dr("wc_desc"))
                fobject.gv_edit.SetRowCellValue(i, "rod_insheet_pct", dr("rod_insheet_pct"))
                fobject.gv_edit.SetRowCellValue(i, "rod_desc", dr("rod_desc"))

                fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)
                i += 1
            Next
            fobject.gv_edit.BestFitColumns()

            'ditambahkan untuk print by ran 06062024
        ElseIf fobject.name = FListRoutingPrint.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ro_code")
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
