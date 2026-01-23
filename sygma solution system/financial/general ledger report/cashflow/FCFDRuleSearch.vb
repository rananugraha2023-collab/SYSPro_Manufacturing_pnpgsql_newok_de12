Imports master_new.ModFunction

Public Class FCFDRuleSearch
    Public _row, _cu_id, _en_id As Integer
    Public _obj As Object
    Public _col_name As String
    
    Private Sub FCFDRuleSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "cfdrule_oid", False)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cashflow Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Sum of Rule", "cfdrule_is_sum", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Remarks", "cfdrule_remarks", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FCFDirectRule" Then
            get_sequel = "SELECT  " _
                    & "  cfdrule_oid, " _
                    & "  cfdrule_dom_id, " _
                    & "  cfdrule_en_id, " _
                    & "  cfdrule_add_by, " _
                    & "  cfdrule_add_date, " _
                    & "  cfdrule_upd_by, " _
                    & "  cfdrule_upd_date, " _
                    & "  cfdrule_group_id, " _
                    & "  coalesce(cfdrule_is_sum,'N') as cfdrule_is_sum, " _
                    & "  cfdrule_line_id, " _
                    & "  cfdrule_dt, " _
                    & "  cfdrule_seq, " _
                    & "  cfdrule_remarks, " _
                    & "  en_desc, " _
                    & "  group_mstr.code_name as group_name, " _
                    & "  tranline_mstr.code_name as tranline_name " _
                    & "FROM  " _
                    & "  public.cfdrule_mstr" _
                    & "  INNER JOIN public.en_mstr ON (public.cfdrule_mstr.cfdrule_en_id = public.en_mstr.en_id)" _
                    & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
                    & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id " _
                    & " where coalesce(cfdrule_is_sum,'N') = 'N' " _
                    & " and cfdrule_en_id = " + _en_id.ToString _
                    & " order by group_mstr.code_name"
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

        If fobject.name = "FCFDirectRule" Then
            fobject.gv_sum_edit.SetRowCellValue(_row, "cfdrules_ref_oid", ds.Tables(0).Rows(_row_gv).Item("cfdrule_oid"))
            fobject.gv_sum_edit.SetRowCellValue(_row, "group_name", ds.Tables(0).Rows(_row_gv).Item("group_name"))
            fobject.gv_sum_edit.SetRowCellValue(_row, "tranline_name", ds.Tables(0).Rows(_row_gv).Item("tranline_name"))
            fobject.gv_sum_edit.BestFitColumns()
        End If

    End Sub
End Class
