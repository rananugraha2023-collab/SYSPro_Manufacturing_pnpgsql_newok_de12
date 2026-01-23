Imports master_new.ModFunction
Public Class FReminderInfo

    Private Sub FReminder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        load_data_many(True)
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_wf, "Transaction Type", "wf_ref_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Transaction Number", "wf_ref_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Transaction Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "select wf_ref_desc, wf_ref_code, wf_dt from wf_mstr " + _
                          " where wf_user_id ~~* '" + master_new.ClsVar.sNama + "'" + _
                          " and wf_iscurrent ~~* 'y'" + _
                          " and wf_wfs_id = '0' " + _
                          " union " + _
                          " select wf_ref_desc, wf_ref_code, wf_dt from wf_mstr " + _
                          " where wf_user_id ~~* '" + master_new.ClsVar.sNama + "'" + _
                          " and wf_iscurrent ~~* 'y'" + _
                          " and wf_wfs_id = '2' " + _
                          " and wf_date_to <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " "

                        .InitializeCommand()
                        .FillDataSet(ds, "wf")
                        gc_wf.DataSource = ds.Tables("wf")

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
