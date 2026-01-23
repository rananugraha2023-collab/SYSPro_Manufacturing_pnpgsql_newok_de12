Imports master_new.ModFunction

Public Class FUserApprovalSearch
    Public _row As Integer
    Public _obj As Object

    Private Sub FUserApprovalSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "User Approval", "name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Type", "type", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        If fobject.name = FRoutingApproval.Name Then
            If _obj.name = "tranu_user_id" Then
                get_sequel = "select userid, usernama as name, 'User' as type  " _
                  & " from tconfuser where useractive ~~* 'Y' order by usernama "
                
            ElseIf _obj.name = "gv_edit" Then
                get_sequel = "select usernama as name, 'User' as type  " _
                  & " from tconfuser where useractive ~~* 'Y' " _
                  & " union " _
                  & " select groupnama as name, 'Group' as type " _
                  & " from tconfgroup where groupactive ~~* 'Y' order by name"
            End If
        Else
            get_sequel = "select usernama as name, 'User' as type  " _
                   & " from tconfuser where useractive ~~* 'Y' " _
                   & " union " _
                   & " select groupnama as name, 'Group' as type " _
                   & " from tconfgroup where groupactive ~~* 'Y' order by name"
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

        If fobject.name = "FRoutingApproval" Then
            If _obj.name = "tranu_user_id" Then
                _obj.tag = ds.Tables(0).Rows(_row_gv).Item("userid")
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("name")
            ElseIf _obj.name = "gv_edit" Then
                fobject.gv_edit.SetRowCellValue(_row, "aprv_user_id", ds.Tables(0).Rows(_row_gv).Item("name"))
                fobject.gv_edit.SetRowCellValue(_row, "aprv_type", ds.Tables(0).Rows(_row_gv).Item("type"))
                fobject.gv_edit.BestFitColumns()
            End If
        Else
            _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("name")
        End If
    End Sub
End Class
