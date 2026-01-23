Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FCashFlowReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ssql As String


    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _now = func_coll.get_now
            de_first.DateTime = _now
            de_end.DateTime = _now

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_dom_mstr())
            le_domain.Properties.DataSource = dt_bantu
            le_domain.Properties.DisplayMember = dt_bantu.Columns("dom_desc").ToString
            le_domain.Properties.ValueMember = dt_bantu.Columns("dom_id").ToString
            le_domain.ItemIndex = 0

            init_le(le_gcal, "gcal_mstr")
          
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditalueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub preview()
        Dim level As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList
        Dim dt As New DataTable
        Try

            level = 1

            ssql = "SELECT  " _
                & "  a.code, " _
                & "  a.remark, " _
                & "  a.sort_number, " _
                & "  a.remark_header, " _
                & "  a.remark_footer, " _
                & "  a.cfsign_header, " _
                & "  a.cf_value_sign, " _
                & "  b.cfdet_oid, " _
                & "  b.cfdet_pk, " _
                & "  b.seq, " _
                & "  b.sub_header, " _
                & "  c.code as code_det, " _
                & "  c.seq as seq_det, " _
                & "  c.ac_hirarki, " _
                & "  d.ac_id, " _
                & "  d.ac_code, " _
                & "  d.ac_name,f_get_cfvalue(d.ac_id,'" & le_gcal.EditValue.ToString & "','Y') * a.cf_value_sign as cf_value,0.0 as cf_value_beginning,0.0 as cf_value_ending " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                & "WHERE " _
                & "  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='I' " _
                & "ORDER BY " _
                & "  a.sort_number, " _
                & "  b.seq,c.seq"

            Dim rpt As New rptCashFlow
            With rpt
                Dim ds As New DataSet
                ds = ReportDataset(ssql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If

                ssql = "SELECT  " _
                    & " sum((SELECT  " _
                    & "       sum(glbal_balance_open) AS jml " _
                    & "       FROM " _
                    & "       public.glbal_balance x " _
                    & "       WHERE " _
                    & "       x.glbal_gcal_oid = '" & le_gcal.EditValue.ToString & "' AND   " _
                    & "       x.glbal_ac_id = d.ac_id )) as total " _
                    & "FROM " _
                    & "  public.tconfsettingcashflow a " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                    & "WHERE " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='I' "

                Dim _awal, _akhir As Double
                _awal = 0
                _akhir = 0

                Dim dt_temp As New DataTable

                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In dt_temp.Rows
                    _awal = dr("total")
                Next

                ssql = "SELECT  " _
                   & " sum((SELECT  " _
                   & "       sum(glbal_balance_open + glbal_balance_posted ) AS jml " _
                   & "       FROM " _
                   & "       public.glbal_balance x " _
                   & "       WHERE " _
                   & "       x.glbal_gcal_oid = '" & le_gcal.EditValue.ToString & "' AND   " _
                   & "       x.glbal_ac_id = d.ac_id )) as total " _
                   & "FROM " _
                   & "  public.tconfsettingcashflow a " _
                   & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                   & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                   & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                   & "WHERE " _
                   & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='I' "


                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr As DataRow In dt_temp.Rows
                    _akhir = dr("total")
                Next


                For Each dr As DataRow In ds.Tables(0).Rows
                    dr("cf_value_beginning") = _awal
                    dr("cf_value_ending") = _akhir
                Next

                '.vtglawal = tanggal.ToString
                '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                '.vlevel = level
                '.vdom = dom
                '.ven = en
                '.vsb = sb
                '.vcc = cc

                If Ce_Posting.EditValue = True Then
                    .Posting_Option = True
                Else
                    .Posting_Option = False
                End If

                .periode = de_end.DateTime.Date.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"
                '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                '.Parameters("PPosisi").Value = posisi

                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Indirect Cash Flow Report"
                '.PrintingSystem = ps
                printTool.ShowPreview()

            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal.EditValueChanged
        Try
            de_first.EditValue = le_gcal.GetColumnValue("gcal_start_date")
            de_end.EditValue = le_gcal.GetColumnValue("gcal_end_date")

            If le_gcal.GetColumnValue("gcal_closing") = "Y" Then
                Ce_Posting.EditValue = True
            Else
                Ce_Posting.EditValue = False
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
