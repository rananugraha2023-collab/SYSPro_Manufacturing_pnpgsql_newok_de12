Imports master_new.ModFunction

Public Class FEmpSearch
    Public _row, _en_id As Integer

    Private Sub FEmpSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "xemp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select xemp_id, xemp_code, xemp_name, xemp_dom_id, xemp_en_id " + _
                     " from xemp_mstr " + _
                     " inner join en_mstr on xemp_en_id = en_id " + _
                     " where xemp_en_id = " + _en_id.ToString + _
                     " order by xemp_id "
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

        If fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_emp_id", ds.Tables(0).Rows(_row_gv).Item("xemp_id"))
            fobject.gv_edit.SetRowCellValue(_row, "xemp_name", ds.Tables(0).Rows(_row_gv).Item("xemp_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetTransferIssue" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_emp_id", ds.Tables(0).Rows(_row_gv).Item("xemp_id"))
            fobject.gv_edit.SetRowCellValue(_row, "xemp_name", ds.Tables(0).Rows(_row_gv).Item("xemp_name"))
            fobject.gv_edit.BestFitColumns()
            'Ant 22 Feb 2011
        ElseIf fobject.name = "FAssetTransfer" Then
            fobject.gv_edit.SetRowCellValue(_row, "astrnfd_emp_id_to", ds.Tables(0).Rows(_row_gv).Item("xemp_id"))
            fobject.gv_edit.SetRowCellValue(_row, "xemp_name_to", ds.Tables(0).Rows(_row_gv).Item("xemp_name"))
            fobject.gv_edit.BestFitColumns()
            '-----------------------------
            'Ant 5 maret 2011
        ElseIf fobject.name = "FReqTransferIssue" Then
            fobject.gv_edit_serial.SetRowCellValue(_row, "reqsdd_emp_id", ds.Tables(0).Rows(_row_gv).Item("xemp_id"))
            fobject.gv_edit_serial.SetRowCellValue(_row, "xemp_name_to", ds.Tables(0).Rows(_row_gv).Item("xemp_name"))
            fobject.gv_edit_serial.BestFitColumns()
            '-----------------------------
        ElseIf fobject.name = "FDisbursementRealization" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_xemp_id", ds.Tables(0).Rows(_row_gv).Item("xemp_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "xemp_name", ds.Tables(0).Rows(_row_gv).Item("xemp_name"))
            fobject.gv_edit_cash.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
