Imports master_new.ModFunction

Public Class FDeptSearch
    Public _row, _en_id As Integer
    Public _type As String

    Private Sub FDeptSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If _type = "emp_dept" Then
            get_sequel = "select code_en_id,en_desc,code_id, code_code,code_name " + _
                         " from code_mstr " + _
                         " inner join en_mstr on en_id = code_en_id " + _
                         " where code_en_id = " + _en_id.ToString + _
                         " and code_field = 'emp_dept' " + _
                         " order by code_id "
        ElseIf _type = "emp_region" Then
            get_sequel = "select code_en_id,en_desc,code_id, code_code,code_name " + _
                         " from code_mstr " + _
                         " inner join en_mstr on en_id = code_en_id " + _
                         " where code_en_id = " + _en_id.ToString + _
                         " and code_field = 'emp_region' " + _
                         " order by code_id "
        ElseIf _type = "retirement_type" Then
            get_sequel = "select code_en_id,en_desc,code_id, code_code,code_name " + _
                         " from code_mstr " + _
                         " inner join en_mstr on en_id = code_en_id " + _
                         " where code_en_id = " + _en_id.ToString + _
                         " and code_field = " + SetSetring(_type) + _
                         " order by code_id "
        ElseIf _type = "retirement_reason" Then
            get_sequel = "select code_en_id,en_desc,code_id, code_code,code_name " + _
                         " from code_mstr " + _
                         " inner join en_mstr on en_id = code_en_id " + _
                         " where code_en_id = " + _en_id.ToString + _
                         " and code_field = " + SetSetring(_type) + _
                         " order by code_id "
        ElseIf _type = "asbackd_reason" Then
            get_sequel = "select code_en_id,en_desc,code_id, code_code,code_name " + _
                         " from code_mstr " + _
                         " inner join en_mstr on en_id = code_en_id " + _
                         " where code_en_id = " + _en_id.ToString + _
                         " and code_field = " + SetSetring(_type) + _
                         " order by code_id "
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

        If fobject.name = "FAssetTransferIssue" Then
            If _type = "emp_dept" Then
                fobject.gv_edit.SetRowCellValue(_row, "xemp_dept", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "department", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf _type = "emp_region" Then
                fobject.gv_edit.SetRowCellValue(_row, "xemp_rg", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "regional", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            End If
            'Ant 22 feb 2011
        ElseIf fobject.name = "FAssetTransfer" Then
            If _type = "emp_dept" Then
                fobject.gv_edit.SetRowCellValue(_row, "astrnfd_dept_id_to", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name_depto", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf _type = "emp_region" Then
                fobject.gv_edit.SetRowCellValue(_row, "astrnfd_rg_id_to", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name_rgto", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            End If
            'ant 5 maret 2011
        ElseIf fobject.name = "FReqTransferIssue" Then
            If _type = "emp_dept" Then
                fobject.gv_edit_serial.SetRowCellValue(_row, "reqsdd_dept_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit_serial.SetRowCellValue(_row, "code_name_depto", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit_serial.BestFitColumns()
            ElseIf _type = "emp_region" Then
                fobject.gv_edit_serial.SetRowCellValue(_row, "reqsdd_rg", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit_serial.SetRowCellValue(_row, "code_name_rgto", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit_serial.BestFitColumns()
            End If
            '------------------------
        ElseIf fobject.name = "FAssetRetirement" Then
            If _type = "retirement_type" Then
                fobject.gv_edit.SetRowCellValue(_row, "asrtrd_type", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "type", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf _type = "retirement_reason" Then
                fobject.gv_edit.SetRowCellValue(_row, "asrtrd_reason", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "reason", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            End If

        ElseIf fobject.name = "FAssetBack" Then
            If _type = "asbackd_reason" Then
                fobject.gv_edit.SetRowCellValue(_row, "asbackd_reason", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "reason", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            End If

        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
