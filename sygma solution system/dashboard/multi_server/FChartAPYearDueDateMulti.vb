Imports npgsql
Imports master_new.ModFunction
Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class FChartAPYearDueDateMulti
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan

    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'form_first_load()

        _now = func_coll.get_now
        pr_txtyear.DateTime = _now
        init_le(le_server, "server_list")

    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        Dim ssql As String
        Try
            Dim _en_id_all As String

            Dim _constring As String
            _constring = "Server= " & le_server.GetColumnValue("conf_ip") & " ;" _
                    & "Database=" & le_server.GetColumnValue("conf_db") & ";Port=" & le_server.GetColumnValue("conf_port") _
                    & ";User ID=" & le_server.GetColumnValue("conf_user") & ";Password=bangkar;Pooling=false;"

            _en_id_all = get_en_id_child(le_server.GetColumnValue("conf_en_id"), _constring)

            If _en_id_all.StartsWith(",") Then
                _en_id_all = _en_id_all.Substring(1)
            End If

            ssql = ""
            Dim _kode As String = ""
            For i As Integer = 1 To 12
                _kode = pr_txtyear.Text & Format(i, "00")


                ssql = ssql + " SELECT cast('" & _kode & "' as varchar)  as ngroup,  " _
                    & "  sum(a.ap_amount/1000000) as nvalue " _
                    & "   " _
                    & "FROM " _
                    & "  public.ap_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.ap_en_id = b.en_id) " _
                    & "WHERE " _
                    & "  to_char(ap_due_date,'yyyyMM')='" & _kode & "' and ap_en_id in (" & _en_id_all & ") and ap_trans_id not in ('X','C') " _
                    & "   union "
            Next

            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 6)

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql, "", _constring)
            ChartControl1.Series(0).DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub checkEditShowLabels_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkEditShowLabels.CheckedChanged
        For Each series As Series In ChartControl1.Series
            series.Label.Visible = Me.checkEditShowLabels.Checked
        Next series

    End Sub
    Public Overrides Function export_data() As Boolean
        Try
            Dim _file As String
            _file = AskSaveAsFile("Excel Files | *.xls")
            If _file.Length > 0 Then
                ChartControl1.ExportToXls(_file)
            End If
            Box("Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Function
End Class
