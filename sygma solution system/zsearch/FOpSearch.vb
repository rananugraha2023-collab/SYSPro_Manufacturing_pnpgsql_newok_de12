Imports master_new.ModFunction

Public Class FOpSearch
    Public _row, _en_id As Integer

    Private Sub FEmpSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Operation Code", "op_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Operation Desc", "op_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.op_code, " _
                & "  a.op_name, " _
                & "  a.op_desc " _
                & "FROM " _
                & "  public.op_mstr a " _
                & "WHERE " _
                & "  a.op_active = 'Y' "


        If SetString(te_search.EditValue) <> "" Then
            get_sequel += " and op_name ~~* '%" & Trim(te_search.Text) & "%' "
        End If
        get_sequel += " ORDER BY " _
                & "  a.op_name"

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

        If fobject.name = FProductStructure.Name Or fobject.name = FProductStructureAssembly.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "psd_op", ds.Tables(0).Rows(_row_gv).Item("op_code"))
            fobject.gv_edit.SetRowCellValue(_row, "op_name", ds.Tables(0).Rows(_row_gv).Item("op_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FRouting.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "rod_op", ds.Tables(0).Rows(_row_gv).Item("op_code"))
            fobject.gv_edit.SetRowCellValue(_row, "op_name", ds.Tables(0).Rows(_row_gv).Item("op_name"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
