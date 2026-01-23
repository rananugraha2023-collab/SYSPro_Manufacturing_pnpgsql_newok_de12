Public Class FDownTimeReason

    Private Sub FDownTimeReason_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _code_field = "down_reason"

        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_data("down_time_cat_mstr", ""))

            With sc_te_code_usr_1
                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
                .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
                .ItemIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
