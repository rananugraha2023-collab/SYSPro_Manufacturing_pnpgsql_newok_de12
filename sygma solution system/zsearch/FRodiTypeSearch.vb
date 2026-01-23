Imports master_new.ModFunction

Public Class FRodiTypeSearch
    Public _row, _en_id As Integer

    Private Sub FEmpSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Operation Code", "rodit_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Operation Name", "rodit_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Operation Desc", "rodit_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        
        get_sequel = "SELECT  " _
                & "  a.rodit_code, " _
                & "  a.rodit_name, " _
                & "  a.rodit_desc " _
                & "FROM " _
                & "  public.rodi_type_mstr a " _
                & "WHERE " _
                & "  a.rodit_active = 'Y' "

        If SetString(te_search.EditValue) <> "" Then
            get_sequel += " and rodit_name ~~* '%" & Trim(te_search.Text) & "%' "
        End If

        get_sequel += " ORDER BY " _
                & "  a.rodit_name"

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

        If fobject.name = FRouting.Name Then
            fobject.gv_indirect_edit.SetRowCellValue(_row, "rodi_type", ds.Tables(0).Rows(_row_gv).Item("rodit_code"))
            fobject.gv_indirect_edit.SetRowCellValue(_row, "rodit_name", ds.Tables(0).Rows(_row_gv).Item("rodit_name"))
            fobject.gv_indirect_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
