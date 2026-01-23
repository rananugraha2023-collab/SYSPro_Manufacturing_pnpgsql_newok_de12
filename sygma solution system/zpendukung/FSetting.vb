Imports master_new.PGSqlConn
Imports master_new.ModFunction
Public Class FSetting

    Private Sub FSetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sSQl As String

            sSQl = "select * from tconfsetting"
            Dim dt_temp As New DataTable
            dt_temp = GetTableData(sSQl)

            For Each dr As DataRow In dt_temp.Rows
                'serv_code.EditValue = dr("serv_code")
                'server_code.EditValue = dr("server_code")
                version_id.EditValue = dr("version_id")
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtUpdate.Click
        Try

            Dim ssql As String

            ssql = "update tconfsetting set  version_id=" & SetInteger(version_id.EditValue)

            Dim ssqls As New ArrayList
            ssqls.Add(ssql)

            'Dim par_error As New ArrayList
            'If dml(ssqls, par_error) Then
            '    box("Success")
            'Else
            '    Box(par_error.Item(0).ToString)

            'End If

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            End If

            'after_success()
            'set_row(_mstr_oid, "casho_oid")
            'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            'insert = True

            Box("Sukses")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class