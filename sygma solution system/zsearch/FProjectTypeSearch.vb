Imports master_new.ModFunction

Public Class FProjectTypeSearch
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer

    Private Sub FProjectTypeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()

        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'If fobject.name = "FProjectMaintenance" Then
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
        'End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FProjectMaintenance" Then
            get_sequel = "SELECT  " _
                        & "  code_id, " _
                        & "  code_code, " _
                        & "  code_name, " _
                        & "  code_desc " _
                        & "FROM  " _
                        & "  public.code_mstr " _
                        & " where code_active = 'Y'" _
                        & " and code_field = 'prjd_type' " _
                        & " and code_en_id = " & SetInteger(_en_id) _
                        & " order by code_code asc "
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
        Dim ds_bantu As New DataSet
        If fobject.name = "FProjectMaintenance" Then
            fobject.gv_edit.SetRowCellValue(_row, "prjd_type", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_code", ds.Tables(0).Rows(_row_gv).Item("code_code"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
