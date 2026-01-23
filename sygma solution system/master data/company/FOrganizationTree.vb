Public Class FOrganizationTree

    Private Sub FOrganizationTree_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Ds_orgsd_det.orgsd_det' table. You can move, or remove it, as needed.
        'Me.Ds_orgsd_det.orgsd_det.Clear()
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "select distinct orgsd_org_type, code_name, " _
        '                    & "orgsd_parent_org, parent.org_name as org_name_parent, " _
        '                    & "orgsd_org_id, child.org_name as org_name_child " _
        '                    & "from orgsd_det " _
        '                    & "inner join code_mstr on code_id = orgsd_org_type " _
        '                    & "inner join org_mstr parent on parent.org_id = orgsd_parent_org " _
        '                    & "inner join org_mstr child on child.org_id = orgsd_org_id " _
        '                    & "order by orgsd_org_type"

        '            .InitializeCommand()
        '            .FillDataSet(Ds_orgsd_det, "orgsd_det")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        'Me.Orgsd_detTableAdapter.Fill(Me.Ds_orgsd_det.orgsd_det)
    End Sub
End Class
