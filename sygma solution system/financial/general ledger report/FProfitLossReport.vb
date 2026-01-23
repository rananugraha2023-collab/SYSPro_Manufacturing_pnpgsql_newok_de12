Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FProfitLossReport
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
            init_le(le_entity, "en_mstr")
            ssql = "select DISTINCT ac_level from ac_mstr where ac_level > 1 order by ac_level "
            InsertCombo(ssql, Cb_Level)
            Cb_Level.EditValue = 2


            init_le(bdgt_type_code, "bdgtt_type")


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditalueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged

    End Sub

    Public Overrides Sub preview()
        Dim level, dom, en, sb, cc As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList


        Try
            dom = 0
            en = 0
            sb = 0
            cc = 0

            If le_domain.EditValue > 0 Then
                level = 1
                dom = CInt(le_domain.EditValue)
                If le_entity.EditValue > 0 Then
                    level = 2
                    en = CInt(le_entity.EditValue)
                End If
            Else
                level = 1
                dom = 1
            End If


          

            If CeDetail.EditValue = False Then

                If CEBudget.EditValue = True Then
                    ssql = "SELECT  " _
                  & "  a.pl_oid, " _
                  & "  a.pl_footer, " _
                  & "  a.pl_sign, " _
                  & "  a.pl_number, " _
                  & "  b.pls_oid, " _
                  & "  b.pls_item, " _
                  & "  b.pls_number, " _
                  & "  c.pla_ac_id, " _
                  & "  d.ac_code, " _
                  & "  d.ac_name, " _
                  & "  c.pla_ac_hirarki,(select  sum(v_nilai) as jml from ( SELECT  " _
                  & "  x.ac_id, " _
                  & "  x.ac_code_hirarki, " _
                  & "  x.ac_code, " _
                  & "  x.ac_name, " _
                  & "  x.ac_type,f_get_balance_sheet(x.ac_id," _
                           & "cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetSetring(bdgt_type_code.EditValue) & ") as v_nilai " _
                  & "FROM " _
                  & "  public.ac_mstr x " _
                  & "WHERE " _
                  & "  substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND  " _
                  & "  ac_is_sumlevel = 'N') as temp) * pls_value as v_nilai " _
                  & "FROM " _
                  & "  public.pl_setting_mstr a " _
                  & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                  & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                  & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                  & "ORDER BY " _
                  & "  a.pl_number, " _
                  & "  b.pls_number "
                Else
                    ssql = "SELECT  " _
                  & "  a.pl_oid, " _
                  & "  a.pl_footer, " _
                  & "  a.pl_sign, " _
                  & "  a.pl_number, " _
                  & "  b.pls_oid, " _
                  & "  b.pls_item, " _
                  & "  b.pls_number, " _
                  & "  c.pla_ac_id, " _
                  & "  d.ac_code, " _
                  & "  d.ac_name, " _
                  & "  c.pla_ac_hirarki,(select  sum(v_nilai) as jml from ( SELECT  " _
                  & "  x.ac_id, " _
                  & "  x.ac_code_hirarki, " _
                  & "  x.ac_code, " _
                  & "  x.ac_name, " _
                  & "  x.ac_type,f_get_balance_sheet_pl(x.ac_id," _
                  & level & "," & dom & "," & en & ",cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai " _
                  & "FROM " _
                  & "  public.ac_mstr x " _
                  & "WHERE " _
                  & "  substring(ac_code_hirarki, 1, length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND  " _
                  & "  ac_is_sumlevel = 'N') as temp) * pls_value as v_nilai " _
                  & "FROM " _
                  & "  public.pl_setting_mstr a " _
                  & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                  & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                  & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                  & "ORDER BY " _
                  & "  a.pl_number, " _
                  & "  b.pls_number "
                End If
                

                Dim rpt As New rptProfitLossReport
                With rpt
                    Dim ds As New DataSet
                    ds = ReportDataset(ssql)
                    If ds.Tables(0).Rows.Count < 1 Then
                        Box("Maaf data kosong")
                        Exit Sub
                    End If

                    '.vtglawal = tanggal.ToString
                    '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                    '.vlevel = level
                    '.vdom = dom
                    '.ven = en
                    '.vsb = sb
                    '.vcc = cc



                    ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

                    Dim dt As New DataTable
                    dt = GetTableData(ssql)

                    For Each dr As DataRow In dt.Rows
                        .XrLabelTitle.Text = dr("dom_company")
                    Next

                    If Ce_Posting.EditValue = True Then
                        .Posting_Option = True
                    Else
                        .Posting_Option = False
                    End If

                    .periode = de_end.DateTime.ToString("dd MMMM yyyy")
                    .DataSource = ds
                    .DataMember = "Table"
                    '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                    '.Parameters("PPosisi").Value = posisi

                     Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                    printTool.PreviewForm.Text = "Profit And Loss Report"
                    '.PrintingSystem = ps
                    printTool.ShowPreview()

                End With
            Else
                'detail

                If CEBudget.EditValue = True Then
                    ssql = "SELECT  " _
                      & "  a.pl_oid, " _
                      & "  a.pl_footer, " _
                      & "  a.pl_sign, " _
                      & "  a.pl_number, " _
                      & "  b.pls_oid, " _
                      & "  b.pls_item, " _
                      & "  b.pls_number, " _
                      & "  c.pla_ac_id, " _
                      & "  d.ac_code, " _
                      & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                      & "  f_get_balance_sheet(y.ac_id," _
                           & "cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetSetring(bdgt_type_code.EditValue) & ") * pls_value as v_nilai, " _
                      & "  c.pla_ac_hirarki " _
                      & "FROM " _
                      & "  public.pl_setting_mstr a " _
                      & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                      & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                      & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                      & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                      & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                      & "ORDER BY " _
                      & "  a.pl_number, " _
                      & "  b.pls_number "
                Else
                    ssql = "SELECT  " _
                      & "  a.pl_oid, " _
                      & "  a.pl_footer, " _
                      & "  a.pl_sign, " _
                      & "  a.pl_number, " _
                      & "  b.pls_oid, " _
                      & "  b.pls_item, " _
                      & "  b.pls_number, " _
                      & "  c.pla_ac_id, " _
                      & "  d.ac_code, " _
                      & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                      & "  f_get_balance_sheet_pl(y.ac_id," _
                      & level & "," & dom & "," & en & ",cast('" & le_gcal.EditValue.ToString & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value as v_nilai, " _
                      & "  c.pla_ac_hirarki " _
                      & "FROM " _
                      & "  public.pl_setting_mstr a " _
                      & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                      & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                      & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                      & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                      & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                      & "ORDER BY " _
                      & "  a.pl_number, " _
                      & "  b.pls_number "
                End If
                

                Dim rpt As New rptProfitLossDetailReport
                With rpt
                    Dim ds As New DataSet
                    ds = ReportDataset(ssql)
                    If ds.Tables(0).Rows.Count < 1 Then
                        Box("Maaf data kosong")
                        Exit Sub
                    End If

                    '.vtglawal = tanggal.ToString
                    '.vtglakhir = EndOfMonth(tanggal, 0).ToString

                    '.vlevel = level
                    '.vdom = dom
                    '.ven = en
                    '.vsb = sb
                    '.vcc = cc



                    ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

                    Dim dt As New DataTable
                    dt = GetTableData(ssql)

                    For Each dr As DataRow In dt.Rows
                        .XrLabelTitle.Text = dr("dom_company")
                    Next

                    If Ce_Posting.EditValue = True Then
                        .Posting_Option = True
                    Else
                        .Posting_Option = False
                    End If

                    .periode = de_end.DateTime.ToString("dd MMMM yyyy")
                    .DataSource = ds
                    .DataMember = "Table"
                    '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                    '.Parameters("PPosisi").Value = posisi

                     Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                    printTool.PreviewForm.Text = "Profit And Loss Report"
                    '.PrintingSystem = ps
                    printTool.ShowPreview()

                End With
            End If
            
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
