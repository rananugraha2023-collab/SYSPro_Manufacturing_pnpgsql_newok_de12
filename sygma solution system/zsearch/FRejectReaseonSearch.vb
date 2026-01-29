Imports master_new.ModFunction

Public Class FRejectReaseonSearch
    Public _row, _en_id As Integer
    Public _type As String

    Private Sub FWCSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Reject Code", "qc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Reject", "qc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Type", "qc_type", DevExpress.Utils.HorzAlignment.Default)
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
                    & "  a.qc_id, " _
                    & "  a.qc_desc, " _
                    & "  a.qc_type " _
                    & "FROM " _
                    & "  public.qc_reason_mstr a " _
                    & " where qc_type='" & _type & "' and qc_desc ~~* '%" + Trim(te_search.Text) + "%' "

        'If _filter <> "" Then
        '    get_sequel += _filter
        'End If

        get_sequel += "  order by a.qc_desc"

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

        If fobject.name = "FWOLaborFeedback" Then
            fobject.gv_reject_edit.SetRowCellValue(_row, "lbrfd_reason_id", ds.Tables(0).Rows(_row_gv).Item("qc_id"))
            fobject.gv_reject_edit.SetRowCellValue(_row, "qc_desc", ds.Tables(0).Rows(_row_gv).Item("qc_desc"))
            fobject.gv_reject_edit.SetRowCellValue(_row, "lbrfd_reject_type", ds.Tables(0).Rows(_row_gv).Item("qc_type"))
            fobject.gv_reject_edit.BestFitColumns()
            fobject.gv_reject_edit.updatecurrentrow()

            fobject.gv_reject_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
