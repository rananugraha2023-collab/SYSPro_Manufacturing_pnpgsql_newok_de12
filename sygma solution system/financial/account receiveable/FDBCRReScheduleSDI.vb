Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid

Public Class FDBCRReScheduleSDI
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim dt As New DataTable
    Dim ssql As String
    Private downHitInfo As GridHitInfo = Nothing
    Private Const OrderFieldName As String = "sokp_due_date"

    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dt2 As New DataTable
            init_le(le_entity, "en_mstr")

            format_grid()

            gv_master.Columns(OrderFieldName).SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            gv_master.OptionsCustomization.AllowSort = False
            gv_master.OptionsView.ShowGroupPanel = False

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub format_grid()
        add_column(gv_master, "sokp_oid", False)
        add_column(gv_master, "arso_ar_oid", False)
        add_column(gv_master, "sokp_ar_oid", False)
        add_column_copy(gv_master, "Seq", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_master, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Amount", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Status", "sokp_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "sokp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sokp_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sokp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sokp_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub


    Private Sub BtRetreive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetreive.Click
        Try
            ssql = "SELECT  " _
            & "  sokp_oid, " _
            & "  arso_ar_oid, " _
            & "  sokp_so_oid, " _
            & "  sokp_seq, " _
            & "  sokp_amount, " _
            & "  sokp_due_date, " _
            & "  sokp_amount_pay, " _
            & "  sokp_description,sokp_date_payment,sokp_status,sokp_add_by,sokp_add_date,sokp_upd_by,sokp_upd_date " _
            & "FROM  " _
            & "  public.sokp_piutang " _
            & "  inner join public.arso_so on arso_so_oid = sokp_so_oid " _
            & "  inner join public.ar_mstr on ar_mstr.ar_oid = arso_ar_oid" _
            & "  where arso_ar_oid in (select ar_oid from ar_mstr where ar_code='" & ARNumber.EditValue & "') " _
            & " order by sokp_seq"
            dt = master_new.PGSqlConn.GetTableData(ssql)
            gc_master.DataSource = dt
            gv_master.BestFitColumns()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSimpan.Click
        Try
            Dim ssqls As New ArrayList
            gv_master.UpdateCurrentRow()
            dt.AcceptChanges()
            For i As Integer = 0 To gv_master.RowCount - 1

                ssql = "UPDATE  " _
                       & "  public.sokp_piutang   " _
                       & "SET  " _
                       & "  sokp_seq = " & SetInteger(i) & "  "

                If SetString(gv_master.GetRowCellValue(i, "sokp_status")) <> "C" Then

                    ssql = ssql & " , sokp_due_date = " & SetDate(gv_master.GetRowCellValue(i, "sokp_due_date")) & ",  " _
                        & "  sokp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & "  sokp_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  "

                End If

                ssql = ssql & " WHERE  " _
                        & "  sokp_oid = " & SetSetring(gv_master.GetRowCellValue(i, "sokp_oid")) & " "

                ssqls.Add(ssql)
            Next


            ssql = insert_log("Reschedule AR SDI " & ARNumber.EditValue)
            ssqls.Add(ssql)
           

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
            Box("Data Saved")

            BtRetreive_Click(sender, e)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub ARNumber_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ARNumber.ButtonClick
        Dim frm As New FDRCRMemoSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._ptnr_id = le_ptnr_id.Tag
        frm._obj = ARNumber
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub le_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles le_ptnr_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = le_ptnr_id
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

   
    Private Sub gv_master_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_master.CellValueChanged
        Try
            If SetString(gv_master.GetRowCellValue(e.RowHandle, "sokp_status")) = "C" Then
                MessageBox.Show("Can't edit closed transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_master.CancelUpdateCurrentRow()
                Exit Sub
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
        
    End Sub
End Class
