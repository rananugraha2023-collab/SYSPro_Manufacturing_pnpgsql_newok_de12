Imports master_new.ModFunction

Public Class FItemStatusSearch
    Public _row, _en_id As Integer

    Private Sub FItemStatusSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "its_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "its_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  its_id, " _
                    & "  its_code, " _
                    & "  its_desc " _
                    & " FROM  " _
                    & "  public.its_mstr"
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FAssetBack" Then
            fobject.gv_edit.SetRowCellValue(_row, "asbackd_its_id", ds.Tables(0).Rows(_row_gv).Item("its_id"))
            fobject.gv_edit.SetRowCellValue(_row, "its_desc", ds.Tables(0).Rows(_row_gv).Item("its_desc"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
