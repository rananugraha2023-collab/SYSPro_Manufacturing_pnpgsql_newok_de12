Imports master_new.ModFunction

Public Class FTaxClassSearch
    Public _row, _en_id As Integer
    Public grid_call As String = ""

    Private Sub FTaxClassSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select en_desc, code_id, code_code, code_name, code_desc from code_mstr " + _
                     " inner join en_mstr on code_en_id = en_id " + _
                     " where code_field ~~* 'taxclass_mstr'" + _
                     " and code_en_id in (0," + _en_id.ToString + ")" _
                    & " and (code_code ~~* '%" + Trim(te_search.Text) + "%' or code_name ~~* '%" + Trim(te_search.Text) + "%' or code_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " order by code_name"
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
            fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FVoucher" Then
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_tax_class_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "taxclass_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit_dist.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_tax_class", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FSalesQuotationConsigment.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FProjectMaintenance" Then
            If grid_call = "gv_edit" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_tax_class", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "tax_class", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf grid_call = "gv_edit_cust" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjd_tax_class", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "tax_class", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit_cust.BestFitColumns()
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
End Class
