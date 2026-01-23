Imports npgsql
Imports master_new.ModFunction
Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class FChartSalesCashCreditMulti
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

            ssql = "select payment_type as ngroup , sum(sod_price_ori_aft_disc_aft_tax_ext/1000000) as nvalue from (SELECT  x.code_name as payment_type , soshipd_qty,soship_date, en_desc,      case  when sod_tax_inc ~~* 'N' " _
                    & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
                    & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                    & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                    & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
                    & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
                    & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
                    & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
                    & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
                    & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
                    & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
                    & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
                    & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
                    & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
                    & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
                    & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
                    & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
                    & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
                    & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
                    & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
                    & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
                    & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
                    & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
                    & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
                    & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
                    & "soship_date between " & master_new.ModFunction.SetDate(pr_txttglawal.DateTime) & " and " & master_new.ModFunction.SetDate(pr_txttglakhir.DateTime) & " and so_en_id in " _
                    & "(" & _en_id_all & ")) as temp  group by payment_type "


            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql, "", _constring)
            ChartControl1.Series(0).DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    'Public Overrides Sub load_data_many(ByVal par As Boolean)
    '    Dim ssql As String
    '    Try
    '        Dim _en_id_all As String


    '        _en_id_all = get_en_id_child(1)


    '        ssql = "select en_desc as ngroup , sum(sod_price_ori_aft_disc_aft_tax_ext) as nvalue from (SELECT   soshipd_qty,soship_date, en_desc,      case  when sod_tax_inc ~~* 'N' " _
    '                & "then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * " _
    '                & "(rate_pph/100.00))),2) * so_exc_rate  when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
    '                & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
    '                & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
    '                & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) " _
    '                & "- (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
    '                & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) " _
    '                & "* so_exc_rate end sod_price_ori_aft_disc_aft_tax,         case  when sod_tax_inc ~~* 'N' " _
    '                & "then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * " _
    '                & "(rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * " _
    '                & "so_exc_rate  when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - " _
    '                & "((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + " _
    '                & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
    '                & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - " _
    '                & "(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - " _
    '                & "((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * " _
    '                & "soshipd_qty_real * -1.00),2) * so_exc_rate end sod_price_ori_aft_disc_aft_tax_ext         " _
    '                & "FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid inner join sod_det on " _
    '                & "sod_oid = soshipd_sod_oid inner join so_mstr on so_oid = sod_so_oid inner join pt_mstr on pt_id = " _
    '                & "sod_pt_id inner join en_mstr on en_id = soship_en_id inner join code_mstr x on x.code_id = " _
    '                & "so_pay_type inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr inner " _
    '                & "join code_mstr tax_type on tax_type.code_id = taxr_tax_type where  code_name ~~* 'ppn') " _
    '                & "taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class inner join  (SELECT taxr_tax_class, " _
    '                & "taxr_rate as rate_pph FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = " _
    '                & "taxr_tax_type where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = " _
    '                & "sod_tax_class inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person inner join ptnr_mstr bill_mstr on " _
    '                & "bill_mstr.ptnr_id = so_ptnr_id_bill inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id where " _
    '                & "soship_date between " & master_new.ModFunction.SetDate(pr_txttglawal.DateTime) & " and " & master_new.ModFunction.SetDate(pr_txttglakhir.DateTime) & " and so_en_id in " _
    '                & "(" & _en_id_all & ")) as temp  "
    '        Dim dt As New DataTable
    '        dt = master_new.PGSqlConn.GetTableData(ssql)
    '        ChartControl1.Series(0).DataSource = dt

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub
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
