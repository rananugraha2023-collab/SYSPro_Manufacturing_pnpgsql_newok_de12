Imports master_new.ModFunction
Imports master_new.PGSqlConn


Public Class FUpdateEntityShipment
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction

    Public _pb_oid As String
    Public _sq_code As String
    Public _pb_code As String
    Public _en_id_asal As Integer
    Public _en_desc_asal As String
    Public _customer As String
    Public _obj As Object


    Private Sub FUpdateEntityShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Private Sub BtUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtUpdate.Click
        Try
            Dim ssqls As New ArrayList
            Dim sSQL As String

            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " update Data Ini..?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If


            sSQL = "update pb_mstr set pb_en_id_shipment=" & SetInteger(pb_en_id_shipment.EditValue) _
            & ",pb_upd_date=" & SetDateNTime(CekTanggal) & ", pb_upd_by=" & SetSetring(master_new.ClsVar.sNama) _
            & " where pb_oid=" & SetSetring(_pb_oid)

            ssqls.Add(sSQL)


            sSQL = "update sq_mstr set sq_en_id_shipment=" & SetInteger(pb_en_id_shipment.EditValue) _
            & " where sq_code=" & SetSetring(_sq_code)

            ssqls.Add(sSQL)

            sSQL = "update so_mstr set so_en_id_shipment=" & SetInteger(pb_en_id_shipment.EditValue) _
            & " where so_sq_ref_code= " & SetSetring(_sq_code)

            ssqls.Add(sSQL)

            ssqls.Add(insert_log("Update Entity : " & _en_desc_asal & " to " & pb_en_id_shipment.GetColumnValue("en_desc")))

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If


            sSQL = "select * from tconfuser x where x.pin='LOG' and x.userid in (select z.userid from tconfuserentity z " _
                                       & " where  z.user_en_id = " & SetInteger(pb_en_id_shipment.EditValue) & ")"


            Dim dt_telegram As New DataTable
            dt_telegram = GetTableData(sSQL)
            Dim _id_tele As String = ""


            For Each dr_telegram As DataRow In dt_telegram.Rows
                _id_tele = SetString(dr_telegram("user_id_telegram"))
            Next

            Dim _url As String = ""
            Dim _pesan As String = ""

            _pesan = "Mohon diproses transfer issue : " + _pb_code & " " _
                    & pb_en_id.GetColumnValue("en_desc") & " (" & _sq_code & ") " & _customer

            _pesan = _pesan.Replace(" ", "%20")

            _url = func_coll.get_http_server_api() & "php72/api_iot/kirim_id.php?id=" _
                            & _id_tele & "&pesan=" & _pesan


            Dim result2 As String
            result2 = mf.run_get_to_api(_url & "")

            If _id_tele <> "" Then
                If SetString(result2).Contains("success") Then
                    '_MakeReport("Data empty")

                Else
                    Box("Gagal notif telegram")
                    'Exit Sub

                End If
            End If



            Box("Update success")
            pb_en_id_shipment.Enabled = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class