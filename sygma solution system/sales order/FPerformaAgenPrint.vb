Imports master_new.ModFunction

Public Class FPerformaAgenPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        init_le(le_entity, "en_mstr")

        TxtStartDate.DateTime = Now

    End Sub


    Public Overrides Sub preview()


        Dim _en_id_all As String

        If CeChild.EditValue = True Then
            _en_id_all = get_en_id_child(le_entity.EditValue)
        Else

            _en_id_all = le_entity.EditValue

        End If

        Dim _sql As String
        Dim _tahun As Integer = TxtStartDate.DateTime.Year

        _sql = ""
        For x As Integer = 0 To 2
            _sql = _sql & " SELECT    " _
               & "  en_desc, " _
               & "  so_code, " _
               & "  so_date, " _
               & "  ptnr_mstr.ptnr_name, " _
               & "  ptnr_mstr.ptnr_code, " _
               & "  soship_code, " _
               & "  soship_date, to_char(soship_date,'yyyy') as date_periode," _
               & "  si_desc, " _
               & "  soship_is_shipment, " _
               & "  soshipd_seq, " _
               & "  cu_name, " _
               & "  so_exc_rate, " _
               & "  pt_code,sod_cost, " _
               & "  pt_desc1, " _
               & "  pt_desc2, " _
               & "  sod_taxable, " _
               & "  sod_tax_inc, " _
               & " sod_sales_unit, " _
               & " (sod_sales_unit * (-1) * soshipd_qty) as sod_sales_unit_total, " _
               & "  tax_mstr.code_name as tax_name, " _
               & "  soshipd_qty * -1 as soshipd_qty, " _
               & "  sod_price, " _
               & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
               & "  sod_disc,  soshipd_qty * -1 * sod_cost  as total_cost, " _
               & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, "


            If x = 0 Then
                _sql = _sql & "  case upper(sod_tax_inc) " _
                & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                & " end as dpp_" & _tahun.ToString & ", 0.0 as dpp_" & (_tahun + 1).ToString & ", 0.0 as dpp_" & (_tahun + 2).ToString & ","
            ElseIf x = 1 Then
                _sql = _sql & "  0.0 as dpp_" & (_tahun - 1).ToString & ", case upper(sod_tax_inc) " _
                & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                & " end as dpp_" & _tahun.ToString & ", 0.0 as dpp_" & (_tahun + 1).ToString & ","
            ElseIf x = 2 Then
                _sql = _sql & "  0.0 as dpp_" & (_tahun + -2).ToString & ", 0.0 as dpp_" & (_tahun + -1).ToString & ", case upper(sod_tax_inc) " _
                & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                & " end as dpp_" & _tahun.ToString & ","
            End If


            _sql = _sql & "  ptnrg_name, " _
                & "sales_mstr.ptnr_name as sales_name, " _
                & "  pay_type.code_name as pay_type_desc,pi_desc, case when ptnr_mstr.ptnr_is_ps='Y' then 'PS' else 'NON PS' end as ps_status  " _
                & "FROM  " _
                & "  public.soship_mstr " _
                & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                & "  inner join so_mstr on so_oid = soship_so_oid " _
                & "  inner join pi_mstr on so_pi_id = pi_id " _
                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                & " inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                & "  inner join en_mstr on en_id = soship_en_id " _
                & "  inner join si_mstr on si_id = soship_si_id " _
                & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                & "  inner join cu_mstr on cu_id = so_cu_id " _
                & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                & "  left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id " _
                & "  inner join pl_mstr on pl_id = pt_pl_id " _
                & "  where to_char(soship_date,'yyyy') = '" + _tahun.ToString _
                & "'  and so_en_id in  (" & _en_id_all & ") union all"

            _tahun = _tahun + 1

        Next

        _sql = Microsoft.VisualBasic.Left(_sql, _sql.Length - 9)

        _tahun = TxtStartDate.DateTime.Year

        _sql = "select en_desc,ptnr_code,ptnr_name,ptnrg_name, sum(dpp_" & _tahun.ToString & ") as dpp_1 , sum(dpp_" & (_tahun + 1).ToString & ") as dpp_2, sum(dpp_" & (_tahun + 2).ToString & ") as dpp_3," _
            & " 0.0 as persen_1 , 0.0 as persen_2   from (" & _sql & ") as temp group by en_desc,ptnr_code,ptnr_name,ptnrg_name"


        Dim rpt As New XrPerformaAgen
        With rpt
            Dim ds_report As New DataSet
            ds_report = master_new.PGSqlConn.ReportDataset(_sql)

            For Each dr As DataRow In ds_report.Tables(0).Rows
                If SetNumber(dr("dpp_1")) = 0 Then
                    dr("persen_1") = 0.0
                Else
                    dr("persen_1") = (SetNumber(dr("dpp_2")) - SetNumber(dr("dpp_1"))) / SetNumber(dr("dpp_1"))
                End If
                If SetNumber(dr("dpp_2")) = 0 Then
                    dr("persen_2") = 0.0
                Else
                    dr("persen_2") = (SetNumber(dr("dpp_3")) - SetNumber(dr("dpp_2"))) / SetNumber(dr("dpp_2"))
                End If

            Next

            ds_report.AcceptChanges()

            If ds_report.Tables(0).Rows.Count < 1 Then
                MsgBox("Maaf data kosong")
                Exit Sub
            End If

            ' Exit Sub
            '.XrLblPeriode.Text = TxtStartDate.DateTime.ToString("dd/MM/yyyy") & " to " & TxtEndDate.DateTime.ToString("dd/MM/yyyy")
            '.XrPivotGrid1.DataSource = ds
            '.XrPivotGrid1.DataMember = "Table"
            .XrTableCell2.Text = _tahun.ToString
            .XrTableCell3.Text = (_tahun + 1).ToString
            .XrTableCell14.Text = (_tahun + 2).ToString
            .DataSource = ds_report
            .DataMember = "Table1"

             Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
            printTool.PreviewForm.Text = "Performa Agen"
            '.PrintingSystem = ps
            printTool.ShowPreview()

        End With


    End Sub


End Class
