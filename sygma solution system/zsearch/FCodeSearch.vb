Imports master_new.ModFunction

Public Class FCodeSearch
    Public _row, _en_id As Integer
    Public _col As String

    Private Sub FCodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code Field", "code_field", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.code_mstr.code_en_id, " _
                    & "  public.code_mstr.code_id, " _
                    & "  public.code_mstr.code_field, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.en_mstr.en_desc " _
                    & "FROM " _
                    & "  public.code_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.code_mstr.code_en_id = public.en_mstr.en_id)"

        If fobject.name = "FProductStructure" Then
            If _col = "code_group_name" Then
                get_sequel = get_sequel + " WHERE (code_mstr.code_field= 'psd_group') or (code_mstr.code_id=0)"
            ElseIf _col = "code_proc_name" Then
                get_sequel = get_sequel + " WHERE (code_mstr.code_field= 'psd_process') or (code_mstr.code_id=0)"
            End If
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

        If fobject.name = "FProductStructure" Or fobject.name = FProductStructureAssembly.Name Then
            If _col = "code_group_name" Then
                fobject.gv_edit.SetRowCellValue(_row, "psd_group", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "code_group_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            ElseIf _col = "code_proc_name" Then
                fobject.gv_edit.SetRowCellValue(_row, "psd_process", ds.Tables(0).Rows(_row_gv).Item("code_id"))
                fobject.gv_edit.SetRowCellValue(_row, "code_proc_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
                fobject.gv_edit.BestFitColumns()
            End If
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
