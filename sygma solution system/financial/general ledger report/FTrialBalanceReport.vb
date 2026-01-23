Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FTrialBalanceReport
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


            If Ce_Posting.EditValue = True Then
                ssql = "select ac_id, ac_code,ac_code_hirarki,ac_name,ac_level,ac_parent,ac_is_sumlevel,ac_sign, " _
                        & "f_trialbalance(ac_id,cast('SWD' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('Y' as varchar)) as SWD , " _
                        & "f_trialbalance(ac_id,cast('SWK' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('Y' as varchar)) as SWK, " _
                        & "f_trialbalance(ac_id,cast('SKD' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('Y' as varchar)) as SKD, " _
                        & "f_trialbalance(ac_id,cast('SKK' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('Y' as varchar)) as SKK " _
                        & "from ac_mstr where lower(ac_is_sumlevel)='n' and ac_id>0"
            Else
                ssql = "select ac_id, ac_code,ac_code_hirarki,ac_name,ac_level,ac_parent,ac_is_sumlevel,ac_sign, " _
                        & "f_trialbalance(ac_id,cast('SWD' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('N' as varchar)) as SWD , " _
                        & "f_trialbalance(ac_id,cast('SWK' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('N' as varchar)) as SWK, " _
                        & "f_trialbalance(ac_id,cast('SKD' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('N' as varchar)) as SKD, " _
                        & "f_trialbalance(ac_id,cast('SKK' as varchar),cast('" & le_gcal.EditValue.ToString & "' as uuid)," & level & "," & dom & "," & en & ",0,0,cast('N' as varchar)) as SKK " _
                        & "from ac_mstr where lower(ac_is_sumlevel)='n' and ac_id>0"

            End If

            If le_domain.EditValue > 0 Then
                posisi += "Domain : " & le_domain.GetColumnValue("dom_desc") & ", "
                If le_entity.EditValue > 0 Then
                    posisi += "Entity : " & le_entity.GetColumnValue("en_desc") & ", "

                End If
            End If

            If Microsoft.VisualBasic.Right(posisi, 2) = ", " Then
                posisi = posisi.Substring(0, Len(posisi) - 2)
            End If

            Dim rpt As New rptTrialBalance
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

                If ce_display_0.EditValue = False Then
                    .FilterString = "[f_sad] <> 0 or [f_sak] <> 0"
                End If

                .periode = de_end.DateTime.Date.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"

              
                 Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
                printTool.PreviewForm.Text = "Trial Balance Report"
                'ps.PreviewFormEx.MdiParent = FMainMenu
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
