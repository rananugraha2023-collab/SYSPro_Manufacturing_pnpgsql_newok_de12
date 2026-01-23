Imports master_new.ModFunction

Public Class FItemSubsSearch
    Public _row, _en_id, _pt_id As Integer

    Private Sub FItemSubsSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty", "pts_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Unit Measure", "code_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.pts_mstr.pts_pt_sub_id, " _
                    & "  public.pts_mstr.pts_qty, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pt_mstr.pt_um, " _
                    & "  public.pt_mstr.pt_type, " _
                    & "  public.pt_mstr.pt_ls, " _
                    & "  public.pt_mstr.pt_cost, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.pts_mstr.pts_desc, en_desc " _
                    & "FROM " _
                    & "  public.pts_mstr " _
                    & "  INNER JOIN public.pt_mstr ON (public.pts_mstr.pts_pt_sub_id = public.pt_mstr.pt_id)" _
                    & "  INNER JOIN public.code_mstr ON (public.code_mstr.code_id = public.pt_mstr.pt_um)" _
                    & "  INNER JOIN public.en_mstr ON (public.en_mstr.en_id = public.pts_mstr.pts_en_id)" _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and pt_en_id in (0," + _en_id.ToString + ")" _
                    & " and pts_pt_id = " & _pt_id.ToString

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    
    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Try
            If fobject.name = "FWorkOrderIssue" Then
                fobject.gv_edit.SetRowCellValue(_row, "wocid_substitute", "Y")
                fobject.gv_edit.SetRowCellValue(_row, "wocid_pt_subs_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code_subs", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc1_subs", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc2_subs", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_cost_subs", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name_subs", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_type_subs", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_ls_subs", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                fobject.gv_edit.BestFitColumns()
            End If
        Catch
            fobject.gv_edit.SetRowCellValue(_row, "wocid_substitute", "")
        End Try
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
