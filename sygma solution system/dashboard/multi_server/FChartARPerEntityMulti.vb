Imports npgsql
Imports master_new.ModFunction
Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class FChartARPerEntityMulti
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan

    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'form_first_load()

        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        UpdateControls()
        InitExplodeModeComboBox()
        cbLabelPosition.EditValue = "Two Columns"

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

            ssql = "SELECT  " _
                & "  sum(a.ar_amount/1000000) as nvalue, " _
                & "  b.en_desc as ngroup " _
                & "FROM " _
                & "  public.ar_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.ar_en_id = b.en_id) " _
                & "WHERE " _
                & "  a.ar_date BETWEEN " & master_new.ModFunction.SetDate(pr_txttglawal.DateTime) & " and " & master_new.ModFunction.SetDate(pr_txttglakhir.DateTime) & " " _
                & "  group by en_desc"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql, "", _constring)
            ChartControl1.Series(0).DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Sub UpdateControls()
        'MyBase.UpdateControls()
        If ChartControl1.Series.Count = 0 Then
            Return
        End If
        Dim label As Pie3DSeriesLabel = TryCast(Me.ChartControl1.Series(0).Label, Pie3DSeriesLabel)
        If Not label Is Nothing Then
            cbLabelPosition.SelectedIndex = CInt(Fix(label.Position))
            If TypeOf ChartControl1.Series(0).PointOptions Is PiePointOptions Then
                Me.checkEditValueAsPercent.Checked = (CType(Me.ChartControl1.Series(0).PointOptions, PiePointOptions)).PercentOptions.ValueAsPercent
            End If
            'Me.cbLabelPosition.Enabled = ShowLabels
            'Me.labelPosition.Enabled = ShowLabels
            If TypeOf ChartControl1.Series(0).View Is Pie3DSeriesView Then
                Me.txtDistance.EditValue = (CType(Me.ChartControl1.Series(0).View, Pie3DSeriesView)).ExplodedDistancePercentage
            End If
        End If
    End Sub
    Private Sub InitExplodeModeComboBox()
        If ChartControl1.Series.Count = 0 Then
            Return
        End If
        cbExplodeMode.Properties.Items.AddRange(PieExplodingHelper.CreateModeList(ChartControl1.Series(0).Points, False))
        cbExplodeMode.SelectedIndex = 0
    End Sub

    Protected Sub UpdateRotationAngles(ByVal diagram As Diagram3D)
        diagram.RotationOrder = RotationOrder.ZXY
        diagram.RotationAngleX = -35
        diagram.RotationAngleY = 0
        diagram.RotationAngleZ = 15
    End Sub



    Private Sub checkEditValueAsPercent_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkEditValueAsPercent.EditValueChanged
        If ChartControl1.Series.Count = 0 Then
            Return
        End If
        Dim options As PiePointOptions = TryCast(Me.ChartControl1.Series(0).PointOptions, PiePointOptions)
        If Not options Is Nothing Then
            options.PercentOptions.ValueAsPercent = Me.checkEditValueAsPercent.Checked
            If Me.checkEditValueAsPercent.Checked Then
                SetNumericOptions(Me.ChartControl1.Series(0), NumericFormat.Percent, 0)
            Else
                SetNumericOptions(Me.ChartControl1.Series(0), NumericFormat.Currency, 1)
            End If
        End If
    End Sub

    Private Sub cbLabelPosition_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLabelPosition.SelectedIndexChanged
        If ChartControl1.Series.Count = 0 Then
            Return
        End If
        Dim label As Pie3DSeriesLabel = TryCast(Me.ChartControl1.Series(0).Label, Pie3DSeriesLabel)
        If Not label Is Nothing Then
            label.Position = CType(cbLabelPosition.SelectedIndex, PieSeriesLabelPosition)
            label.Antialiasing = label.Position = PieSeriesLabelPosition.Radial Or label.Position = PieSeriesLabelPosition.Tangent
        End If
    End Sub

    Private Sub cbExplodeMode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbExplodeMode.SelectedIndexChanged
        If ChartControl1.Series.Count = 0 Then
            Return
        End If
        Dim view As Pie3DSeriesView = TryCast(Me.ChartControl1.Series(0).View, Pie3DSeriesView)
        If Not view Is Nothing Then
            Dim mode As String = CStr(cbExplodeMode.SelectedItem)
            PieExplodingHelper.ApplyMode(view, mode)
        End If
    End Sub

    Private Sub checkEditShowLabels_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkEditShowLabels.CheckedChanged
        For Each series As Series In ChartControl1.Series
            series.Label.Visible = Me.checkEditShowLabels.Checked
        Next series
        UpdateControls()
    End Sub

    Private Sub txtDistance_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDistance.EditValueChanged
        If ChartControl1.Series.Count = 0 Then
            Return
        End If
        Dim view As Pie3DSeriesView = TryCast(Me.ChartControl1.Series(0).View, Pie3DSeriesView)
        If Not view Is Nothing Then
            view.ExplodedDistancePercentage = Convert.ToDouble(Me.txtDistance.EditValue)
        End If
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
