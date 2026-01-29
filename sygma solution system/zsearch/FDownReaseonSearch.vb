Imports master_new.ModFunction

Public Class FDownReaseonSearch
    Public _row, _en_id As Integer

    Private Sub FDownReaseonSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Reject Code", "qc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category", "category_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Down Reason", "down_reason", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    'Public Overrides Function get_sequel() As String
    '    get_sequel = "select qc_id, qc_desc, qc_type " + _
    '                 " from qc_reason_mstr " + _
    '                 " where qc_active ~~* 'Y'" _
    '                & " order by qc_desc"
    '    Return get_sequel
    'End Function

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT false as pilih, " _
                & "  a.code_field, " _
                & "  a.code_usr_1 as category_id, " _
                & "  b.code_name AS category_name, " _
                & "  a.code_code, " _
                & "  a.code_id, " _
                & "  a.code_name as down_reason " _
                & "FROM public.code_mstr a " _
                & "  INNER JOIN public.code_mstr b ON ( " _
                & "    CASE " _
                & "      WHEN a.code_usr_1 ~ '^[0-9]+$' THEN CAST (a.code_usr_1 AS INTEGER) " _
                & "      ELSE NULL " _
                & "    END = b.code_id) " _
                & "WHERE a.code_field = 'down_reason' ;"

        'If _filter <> "" Then
        '    get_sequel += _filter
        'End If

        'get_sequel += "  order by b.category_name"

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

    'Private Sub fill_data()
    '    Dim _row_gv As Integer
    '    _row_gv = BindingContext(ds.Tables(0)).Position

    '    If fobject.name = "FWOLaborFeedback" Then
    '        fobject.gc_downtime_edit.SetRowCellValue(_row, "lbrfd_reason_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
    '        fobject.gc_downtime_edit.SetRowCellValue(_row, "down_reason", ds.Tables(0).Rows(_row_gv).Item("down_reason"))
    '        'fobject.gc_downtime_edit.SetRowCellValue(_row, "lbrfd_reject_type", ds.Tables(0).Rows(_row_gv).Item("qc_type"))
    '        fobject.gc_downtime_edit.BestFitColumns()
    '        fobject.gc_downtime_edit.updatecurrentrow()

    '        fobject.gc_downtime_edit.BestFitColumns()
    '    End If
    'End Sub
    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FWOLaborFeedback" Then
            fobject.gv_downtime_edit.SetRowCellValue(_row, "lbrfd_down_reason_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_downtime_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("down_reason"))
            fobject.gv_downtime_edit.BestFitColumns()
        End If
    End Sub


    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
