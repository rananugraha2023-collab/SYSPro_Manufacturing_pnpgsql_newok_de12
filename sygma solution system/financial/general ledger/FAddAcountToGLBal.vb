Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FAddAcountToGLBal
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ssql As String

    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            init_le(le_entity, "en_mstr")
            init_le(le_account, "account")
            init_le(le_gcal, "gcal_mstr")
            init_le(le_cu, "cu_mstr")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub btSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            Dim sSQLs As New ArrayList

            ssql = "select * from glbal_balance " _
            & "where glbal_gcal_oid=" & SetSetring(le_gcal.EditValue) & " and glbal_ac_id=" & SetInteger(le_account.EditValue) & " and glbal_en_id=" & SetInteger(le_entity.EditValue)

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            If dt.Rows.Count > 0 Then
                Box("Data allready exist")
                Exit Sub
            End If


            ssql = "INSERT INTO  " _
                & "  public.glbal_balance " _
                & "( " _
                & "  glbal_oid, " _
                & "  glbal_dom_id, " _
                & "  glbal_en_id, " _
                & "  glbal_add_by, " _
                & "  glbal_add_date, " _
                & "  glbal_gcal_oid, " _
                & "  glbal_ac_id, " _
                & "  glbal_sb_id, " _
                & "  glbal_cc_id, " _
                & "  glbal_cu_id, " _
                & "  glbal_balance_open, " _
                & "  glbal_balance_unposted, " _
                & "  glbal_balance_posted, " _
                & "  glbal_dt " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetInteger(le_entity.EditValue) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                & SetSetring(le_gcal.EditValue) & ",  " _
                & SetInteger(le_account.EditValue) & ",  " _
                & SetInteger(0) & ",  " _
                & SetInteger(0) & ",  " _
                & SetInteger(le_cu.EditValue) & ",  " _
                & SetDbl(0) & ",  " _
                & SetDbl(0) & ",  " _
                & SetDbl(0) & ",  " _
                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                & ")"
            sSQLs.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            End If
            Box("Data have been save")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
