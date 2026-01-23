Imports master_new.ModFunction

Public Class XRSESlip
    Dim dt_periode As New DataTable
    Private Sub XRSESlip_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Try
            Dim ssql As String = ""
            ssql = "SELECT  " _
                & "  a.segend_komisi, " _
                & "  a.segend_add_income, " _
                & "  a.segend_coaching_income, " _
                & "  a.segend_total_income, " _
                & "  a.segend_payment_date, " _
                & "  a.segend_su_sales, " _
                & "  c.seperiode_start_date, " _
                & "  c.seperiode_end_date, " _
                & "  a.segend_komisi_bulanan, " _
                & "  a.segend_komisi_telah_dibayar, " _
                & "  c.seperiode_bonus_gen, b.segen_periode " _
                & "FROM " _
                & "  public.segend_det a " _
                & "  INNER JOIN public.segen_mstr b ON (a.segend_segen_oid = b.segen_oid) " _
                & "  INNER JOIN public.seperiode_mstr c ON (b.segen_periode = c.seperiode_code) " _
                & "WHERE " _
                & "  a.segend_ptnr_id = " & SetInteger(GetCurrentColumnValue("segend_ptnr_id")) & " AND  " _
                & " left( b.segen_periode,6) =" & SetSetring(Microsoft.VisualBasic.Left(GetCurrentColumnValue("segen_periode").ToString, 6)) & " " _
                & "order by  b.segen_periode"

            Dim rpt As New XRSlipSub

            XrSubreport1.ReportSource = rpt
            XrSubreport1.ReportSource.DataSource = master_new.PGSqlConn.ReportDataset(ssql)
            XrSubreport1.ReportSource.DataMember = "Table"

            ssql = "SELECT  " _
                & "  a.seperiode_start_date, " _
                & "  a.seperiode_end_date, " _
                & "  a.seperiode_code, " _
                & "  a.seperiode_remarks " _
                & "FROM " _
                & "  public.seperiode_mstr a " _
                & "WHERE " _
                & "  left(a.seperiode_code,6) =" & SetSetring(Microsoft.VisualBasic.Left(GetCurrentColumnValue("segen_periode").ToString, 6)) & " " _
                & " order by seperiode_code"


            dt_periode = master_new.PGSqlConn.GetTableData(ssql)
            Dim _qry As String = ""
            For Each dr As DataRow In dt_periode.Rows
                _qry += "SELECT " & SetSetring(dr("seperiode_code")) & " as seperiode_code, cast(" & SetDate(dr("seperiode_start_date")) & " as date) as seperiode_start_date,cast(" & SetDate(dr("seperiode_end_date")) & " as date) as seperiode_end_date, so_code, ptnr_mstr.ptnr_name,pt_desc1,pay_type.code_name as credit_term_name, " _
                    & "SUM((sod_sales_unit * (-1) * soshipd_qty)) sod_sales_unit_total " _
                    & "FROM  " _
                    & "public.soship_mstr " _
                    & "inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                    & "inner join so_mstr on so_oid = soship_so_oid " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid " _
                    & "inner join pi_mstr on so_pi_id = pi_id " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                    & "inner join en_mstr on en_id = soship_en_id " _
                    & "inner join si_mstr on si_id = soship_si_id " _
                    & "inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                    & "inner join pt_mstr on pt_id = sod_pt_id " _
                    & "inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                    & "inner join loc_mstr on loc_id = soshipd_loc_id " _
                    & "inner join cu_mstr on cu_id = so_cu_id " _
                    & "inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                    & "inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                    & "left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                    & "left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                    & "left outer join ar_mstr on ar_oid = ars_ar_oid " _
                    & "left outer join tia_ar on tia_ar_oid = ar_oid " _
                    & "left outer join ti_mstr on ti_oid = tia_ti_oid " _
                    & "inner join pl_mstr on pl_id = pt_pl_id " _
                    & " inner join code_mstr credit_term on credit_term.code_id = so_credit_term  " _
                    & "where soship_date >= " & SetDate(dr("seperiode_start_date")) _
                    & " and soship_date <= " & SetDate(dr("seperiode_end_date")) _
                    & " and (so_sales_person=  " & SetInteger(GetCurrentColumnValue("segend_ptnr_id")) & ")  " _
                    & "group by so_code, ptnr_mstr.ptnr_name,pt_desc1,pay_type.code_name " _
                    & "union all "

            Next
            If _qry.Length > 0 Then
                _qry = Microsoft.VisualBasic.Left(_qry, _qry.Length - 10)
            End If

            Dim rpt2 As New XRSlipSubPenjualan

            XrSubreport2.ReportSource = rpt2
            XrSubreport2.ReportSource.DataSource = master_new.PGSqlConn.ReportDataset(_qry)
            XrSubreport2.ReportSource.DataMember = "Table"


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class