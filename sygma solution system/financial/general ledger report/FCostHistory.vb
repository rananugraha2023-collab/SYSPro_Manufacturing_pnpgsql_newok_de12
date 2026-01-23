Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid

Public Class FCostHistory
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim dt As New DataTable
    Dim ssql As String


    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            start_date.DateTime = Now
            end_date.DateTime = Now

            format_grid()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub format_grid()
        add_column(gv_master, "PO Code", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PO Date", "po_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Private Sub pt_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_code.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtRetrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetrieve.Click
        Try
            If SetString(pt_code.Tag) = "" Then
                Box("Please select part number first")
                pt_code.Focus()
                Exit Sub
            End If

            Dim sSQL As String
            sSQL = "SELECT  " _
                & "  a.pod_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.pod_cost,pod_qty, " _
                & "  c.po_code, " _
                & "  c.po_date " _
                & "FROM " _
                & "  public.pod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.pod_pt_id = b.pt_id) " _
                & "  INNER JOIN public.po_mstr c ON (a.pod_po_oid = c.po_oid) " _
                & "WHERE " _
                & "  a.pod_pt_id = " & pt_code.Tag _
                & " And po_date between " & SetDateNTime00(start_date.DateTime) & " and " & SetDateNTime00(end_date.DateTime) _
                & " ORDER BY " _
                & "  c.po_date"

            Dim dt As New DataTable
            dt = GetTableData(sSQL)
            gc_master.DataSource = dt
            gv_master.BestFitColumns()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Function export_data() As Boolean
        Dim _file As String

        _file = AskSaveAsFile("Excel Files | *.xls")

        If _file = "" Then
            Return False
            Exit Function
        End If

        gv_master.ExportToExcelOld(_file)

        If ask("Do you want to open this file?", "Export To...") = True Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = _file
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                'MessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Pesan(Err)
            End Try
        End If
    End Function

End Class
