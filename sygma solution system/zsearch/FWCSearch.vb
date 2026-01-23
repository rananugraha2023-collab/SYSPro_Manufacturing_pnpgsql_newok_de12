Imports master_new.ModFunction

Public Class FWCSearch
    Public _row, _en_id As Integer

    Private Sub FWCSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Work Center Code", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select en_desc, wc_id, wc_code, wc_desc " + _
                     " from wc_mstr " + _
                     " inner join en_mstr on wc_en_id = en_id " + _
                     " where wc_active ~~* 'Y'" _
                    & " and (wc_code ~~* '%" + Trim(te_search.Text) + "%' or wc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and wc_en_id in (0," + _en_id.ToString + ")" _
                    & " order by wc_desc"
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

        If fobject.name = "FRouting" Then
            fobject.gv_edit.SetRowCellValue(_row, "rod_wc_id", ds.Tables(0).Rows(_row_gv).Item("wc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "wc_desc", ds.Tables(0).Rows(_row_gv).Item("wc_desc"))
            fobject.gv_edit.BestFitColumns()
            fobject.gv_edit.updatecurrentrow()

        ElseIf fobject.name = FWorkOrderIssue.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wocid_wc_id", ds.Tables(0).Rows(_row_gv).Item("wc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "wc_desc", ds.Tables(0).Rows(_row_gv).Item("wc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FWorkInProgress.Name Then
            fobject.gv_edit_wip.SetRowCellValue(_row, "wimd_wc_id", ds.Tables(0).Rows(_row_gv).Item("wc_id"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "wc_desc", ds.Tables(0).Rows(_row_gv).Item("wc_desc"))
            fobject.gv_edit_wip.BestFitColumns()
        ElseIf fobject.name = FWorkOrder.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wr_wc_id", ds.Tables(0).Rows(_row_gv).Item("wc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "wc_desc", ds.Tables(0).Rows(_row_gv).Item("wc_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "wr_setup_men", ds.Tables(0).Rows(_row_gv).Item("wc_setup_men"))
            fobject.gv_edit.SetRowCellValue(_row, "wr_setup_rate", ds.Tables(0).Rows(_row_gv).Item("wc_setup_rate"))
            fobject.gv_edit.SetRowCellValue(_row, "wr_lbr_rate", ds.Tables(0).Rows(_row_gv).Item("wc_lbr_rate"))
            fobject.gv_edit.SetRowCellValue(_row, "wr_men_mch", ds.Tables(0).Rows(_row_gv).Item("wc_men_mch"))
            fobject.gv_edit.SetRowCellValue(_row, "wr_mch_op", ds.Tables(0).Rows(_row_gv).Item("wc_mch_op"))
            fobject.gv_edit.SetRowCellValue(_row, "wr_mch_bdn_rate", ds.Tables(0).Rows(_row_gv).Item("wc_mch_bdn_rate"))
            fobject.gv_edit.SetRowCellValue(_row, "wr_trans_id", "D")

            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
