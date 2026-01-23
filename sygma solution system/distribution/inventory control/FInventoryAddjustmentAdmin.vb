Imports master_new.ModFunction
Imports npgsql

Public Class FInventoryAddjustmentAdmin
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Dim dt_show As New DataTable
    Private Sub FInvIssuePrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     
        init_le(le_entity, "en_mstr")
    End Sub

  

    Private Sub BtShowDataTrfDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtShowDataTrfDel.Click
        Try

            Dim sSQL As String
            Dim sSQLs As New ArrayList
           

            sSQL = "SELECT  " _
                & "  public.invh_mstr.invh_oid, " _
                & "  public.invh_mstr.invh_tran_id, " _
                & "  public.invh_mstr.invh_seq, " _
                & "  public.invh_mstr.invh_dom_id, " _
                & "  public.invh_mstr.invh_en_id, " _
                & "  public.invh_mstr.invh_trn_code, " _
                & "  public.invh_mstr.invh_trn_oid, " _
                & "  public.invh_mstr.invh_date, " _
                & "  public.invh_mstr.invh_desc, " _
                & "  public.invh_mstr.invh_opn_type, " _
                & "  public.invh_mstr.invh_si_id, " _
                & "  public.invh_mstr.invh_loc_id, " _
                & "  public.invh_mstr.invh_pt_id,(select pt_code from pt_mstr x where x.pt_id=invh_pt_id) as pt_code, " _
                & "  public.invh_mstr.invh_qty, " _
                & "  public.invh_mstr.invh_cost, " _
                & "  public.invh_mstr.invh_serial, " _
                & "  public.invh_mstr.dt_timestamp, " _
                & "  public.invh_mstr.invh_avg_cost, " _
                & "  public.invh_mstr.invh_qty_old " _
                & "FROM " _
                & "  public.invh_mstr " _
                & "WHERE " _
                & "  public.invh_mstr.invh_en_id = " & SetInteger(le_entity.EditValue) & " AND  " _
                & "  public.invh_mstr.invh_trn_code = '" & trf_issue_number.EditValue & "' AND  " _
                & "  public.invh_mstr.invh_date = " & SetDateNTime00(trf_date.EditValue) _
                & " and public.invh_mstr.invh_pt_id = (select pt_id from pt_mstr x where x.pt_code='" & pt_code.Text & "') " _
                & " order by dt_timestamp desc"

            dt_show = master_new.PGSqlConn.GetTableData(sSQL)

            If dt_show.Rows.Count = 1 Then


                'sSQL = "DELETE from invh_mstr where invh_oid=" & SetSetring(dt_show.Rows(0).Item("invh_oid"))

                'sSQLs.Add(sSQL)

                '_en_id = dt_show.Rows(0).Item("invh_en_id")
                '_si_id = dt_show.Rows(0).Item("invh_si_id")
                '_loc_id = ptsfr_loc_git.EditValue
                '_pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                '_pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                '_serial = "''"
                '_qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") 'karena ini GIT maka harus dikurangi semua qty nya...

                'Me.Text = _text & " update_invc_mstr_minus start " & i
                'Windows.Forms.Application.DoEvents()

                'If func_coll.update_invc_mstr_plus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                '    sqlTran.Rollback()
                '    edit = False
                '    Exit Sub
                'End If


                ''If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                ''    sqlTran.Rollback()
                ''    edit = False
                ''    Exit Sub
                ''End If

                Log_Query.EditValue = dt_show.Rows(0).Item("invh_oid").ToString & vbNewLine & dt_show.Rows(0).Item("invh_qty")


            Else
                Log_Query.EditValue = dt_show.Rows(0).Item("invh_oid").ToString & vbNewLine & dt_show.Rows(0).Item("invh_qty")
                MsgBox("Data more than 1 rows")
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtDelDataTrfDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAddDataTrfDel.Click
        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If
        Dim sSQLs As New ArrayList
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim _pt_code, _serial As String


        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from invh_mstr where invh_oid=" & SetSetring(dt_show.Rows(0).Item("invh_oid").ToString)

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        _en_id = dt_show.Rows(0).Item("invh_en_id") ' ptsfr_en_id.EditValue
                        _si_id = dt_show.Rows(0).Item("invh_si_id") ' ptsfr_si_id.EditValue
                        _loc_id = dt_show.Rows(0).Item("invh_loc_id") ' ptsfr_loc_git.EditValue
                        _pt_id = dt_show.Rows(0).Item("invh_pt_id") 'ds_edit.Tables(0).Rows(i).Item("pt_id")
                        _pt_code = dt_show.Rows(0).Item("pt_code") ' ds_edit.Tables(0).Rows(i).Item("pt_code")
                        _serial = "''"
                        _qty = dt_show.Rows(0).Item("invh_qty") * -1.0 'ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                        If func_coll.update_invc_mstr_plus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                            sqlTran.Rollback()

                            Exit Sub
                        End If


                        '_en_id = ptsfr_en_to_id.EditValue
                        '_si_id = ptsfr_si_to_id.EditValue
                        '_loc_id = ptsfr_loc_to_id.EditValue
                        '_pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '_pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                        '_serial = "''"
                        '_qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")
                        'If func_coll.update_invc_mstr_plus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        '    sqlTran.Rollback()

                        '    Exit Sub
                        'End If
                        'If func_coll.update_invc_mstr_minus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        '    sqlTran.Rollback()

                        '    Exit Sub
                        'End If


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        MsgBox("Success")

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)

                    End Try
                End With
            End Using
        Catch ex As Exception

            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtDeleteStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDeleteStock.Click
        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If
        Dim sSQLs As New ArrayList
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim _pt_code, _serial As String


        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from invh_mstr where invh_oid=" & SetSetring(dt_show.Rows(0).Item("invh_oid").ToString)

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        _en_id = dt_show.Rows(0).Item("invh_en_id") ' ptsfr_en_id.EditValue
                        _si_id = dt_show.Rows(0).Item("invh_si_id") ' ptsfr_si_id.EditValue
                        _loc_id = dt_show.Rows(0).Item("invh_loc_id") ' ptsfr_loc_git.EditValue
                        _pt_id = dt_show.Rows(0).Item("invh_pt_id") 'ds_edit.Tables(0).Rows(i).Item("pt_id")
                        _pt_code = dt_show.Rows(0).Item("pt_code") ' ds_edit.Tables(0).Rows(i).Item("pt_code")
                        _serial = "''"
                        _qty = dt_show.Rows(0).Item("invh_qty")  'ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                        If func_coll.update_invc_mstr_minus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                            sqlTran.Rollback()

                            Exit Sub
                        End If




                        'If func_coll.update_invc_mstr_plus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        '    sqlTran.Rollback()

                        '    Exit Sub
                        'End If


                        '_en_id = ptsfr_en_to_id.EditValue
                        '_si_id = ptsfr_si_to_id.EditValue
                        '_loc_id = ptsfr_loc_to_id.EditValue
                        '_pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '_pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                        '_serial = "''"
                        '_qty = ds_edit.Tables(0).Rows(i).Item("ptsfrd_qty_receive")
                        'If func_coll.update_invc_mstr_plus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        '    sqlTran.Rollback()

                        '    Exit Sub
                        'End If
                        'If func_coll.update_invc_mstr_minus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        '    sqlTran.Rollback()

                        '    Exit Sub
                        'End If


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        MsgBox("Success")

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)

                    End Try
                End With
            End Using
        Catch ex As Exception

            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
