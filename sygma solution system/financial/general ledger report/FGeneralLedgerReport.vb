Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FGeneralLedgerReport
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
            init_le(le_account, "account")

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

            ssql = " SELECT  " _
             & " '' as glt_code, " _
             & SetDate(de_first.DateTime.Date) & " as glt_date, " _
             & " '' as glt_type, " _
             & " a.ac_cu_id, " _
             & " 1 as glt_exc_rate, " _
             & " 0 as glt_seq, " _
             & "  a.ac_id, " _
             & "  a.ac_code, " _
             & "  a.ac_name,null as glt_dt, " _
             & "   'Saldo Awal' as glt_desc, " _
             & "  0 as glt_debit, " _
             & "  0 as glt_credit, " _
             & "  '' as glt_ref_trans_code,'' as en_desc, " _
             & "    f_get_begining_balance_acc(a.ac_id,CAST(" & SetDate(de_first.DateTime.Date) & " as date)," & level & "," & dom & "," & en & ",0,0) as value," _
             & SetDate(de_first.DateTime.Date) & " as glt_add_date " _
             & "FROM " _
             & "  public.ac_mstr a " _
             & "WHERE " _
             & "  lower(a.ac_is_sumlevel) = 'n' and ac_id > 0 " _
             & "UNION all " _
             & "SELECT  " _
             & "  a.glt_code, " _
             & "  a.glt_date, " _
             & "  a.glt_type, " _
             & "  a.glt_cu_id, " _
             & "  a.glt_exc_rate, " _
             & "  a.glt_seq, " _
             & "  a.glt_ac_id, " _
             & "  b.ac_code, " _
             & "  b.ac_name,a.glt_dt, " _
             & "  a.glt_desc, " _
             & "  a.glt_debit * glt_exc_rate as glt_debit , " _
             & "  a.glt_credit * glt_exc_rate as glt_credit, " _
             & "  a.glt_ref_trans_code, en_desc," _
             & "  f_calc_gl(b.ac_sign,a.glt_debit,a.glt_credit)*glt_exc_rate as value,glt_add_date " _
             & "FROM " _
             & "  public.ac_mstr b " _
             & "  INNER JOIN public.glt_det a ON (b.ac_id = a.glt_ac_id) " _
              & "  INNER JOIN public.en_mstr c ON (c.en_id = a.glt_en_id) " _
             & "where glt_ac_id>0 and glt_date between " & SetDate(de_first.DateTime.Date) & " and " & SetDate(de_end.DateTime.Date) & " "

            If le_account.EditValue > 0 Then

                If le_entity.Text <> "-" Then
                    ssql = " SELECT  " _
                         & " '' as glt_code, " _
                         & SetDate(de_first.DateTime.Date) & " as glt_date, " _
                         & " '' as glt_type, " _
                         & " a.ac_cu_id, " _
                         & " 1 as glt_exc_rate, " _
                         & " 0 as glt_seq, " _
                         & "  a.ac_id, " _
                         & "  a.ac_code, " _
                         & "  a.ac_name,null as glt_dt, " _
                         & "   'Saldo Awal' as glt_desc, " _
                         & "  0 as glt_debit, " _
                         & "  0 as glt_credit, " _
                         & "  '' as glt_ref_trans_code,'' as en_desc, " _
                         & "    f_get_begining_balance_acc(a.ac_id,CAST(" & SetDate(de_first.DateTime.Date) & " as date)," & level & "," & dom & "," & en & ",0,0) as value ," _
                         & SetDate(de_first.DateTime.Date) & " as glt_add_date " _
                         & "FROM " _
                         & "  public.ac_mstr a " _
                         & "WHERE " _
                         & "  lower(a.ac_is_sumlevel) = 'n' and ac_id = " & le_account.EditValue & " " _
                         & "UNION all " _
                         & "SELECT  " _
                         & "  a.glt_code, " _
                         & "  a.glt_date, " _
                         & "  a.glt_type, " _
                         & "  a.glt_cu_id, " _
                         & "  a.glt_exc_rate, " _
                         & "  a.glt_seq, " _
                         & "  a.glt_ac_id, " _
                         & "  b.ac_code, " _
                         & "  b.ac_name, a.glt_dt, " _
                         & "  a.glt_desc, " _
                         & "  a.glt_debit * glt_exc_rate as glt_debit , " _
                         & "  a.glt_credit * glt_exc_rate as glt_credit, " _
                         & "  a.glt_ref_trans_code, en_desc," _
                         & "  f_calc_gl(b.ac_sign,a.glt_debit,a.glt_credit)*glt_exc_rate as value,glt_add_date " _
                         & "FROM " _
                         & "  public.ac_mstr b " _
                         & "  INNER JOIN public.glt_det a ON (b.ac_id = a.glt_ac_id) " _
                         & "  INNER JOIN public.en_mstr c ON (c.en_id = a.glt_en_id) " _
                         & "where glt_ac_id  = " & le_account.EditValue & "  and glt_date between " & SetDate(de_first.DateTime.Date) & " and " & SetDate(de_end.DateTime.Date) & " and glt_en_id=" & le_entity.EditValue

                Else
                    ssql = " SELECT  " _
                         & " '' as glt_code, " _
                         & SetDate(de_first.DateTime.Date) & " as glt_date, " _
                         & " '' as glt_type, " _
                         & " a.ac_cu_id, " _
                         & " 1 as glt_exc_rate, " _
                         & " 0 as glt_seq, " _
                         & "  a.ac_id, " _
                         & "  a.ac_code, " _
                         & "  a.ac_name,null as glt_dt, " _
                         & "   'Saldo Awal' as glt_desc, " _
                         & "  0 as glt_debit, " _
                         & "  0 as glt_credit, " _
                         & "  '' as glt_ref_trans_code,'' as en_desc, " _
                         & "    f_get_begining_balance_acc(a.ac_id,CAST(" & SetDate(de_first.DateTime.Date) & " as date)," & level & "," & dom & "," & en & ",0,0) as value ," _
                         & SetDate(de_first.DateTime.Date) & " as glt_add_date " _
                         & "FROM " _
                         & "  public.ac_mstr a " _
                         & "WHERE " _
                         & "  lower(a.ac_is_sumlevel) = 'n' and ac_id = " & le_account.EditValue & " " _
                         & "UNION all " _
                         & "SELECT  " _
                         & "  a.glt_code, " _
                         & "  a.glt_date, " _
                         & "  a.glt_type, " _
                         & "  a.glt_cu_id, " _
                         & "  a.glt_exc_rate, " _
                         & "  a.glt_seq, " _
                         & "  a.glt_ac_id, " _
                         & "  b.ac_code, " _
                         & "  b.ac_name,a.glt_dt, " _
                         & "  a.glt_desc, " _
                         & "  a.glt_debit * glt_exc_rate as glt_debit , " _
                         & "  a.glt_credit * glt_exc_rate as glt_credit, " _
                         & "  a.glt_ref_trans_code,en_desc, " _
                         & "  f_calc_gl(b.ac_sign,a.glt_debit,a.glt_credit)*glt_exc_rate as value,glt_add_date " _
                         & "FROM " _
                         & "  public.ac_mstr b " _
                         & "  INNER JOIN public.glt_det a ON (b.ac_id = a.glt_ac_id) " _
                         & "  INNER JOIN public.en_mstr c ON (c.en_id = a.glt_en_id) " _
                         & "where glt_ac_id  = " & le_account.EditValue & "  and glt_date between " & SetDate(de_first.DateTime.Date) & " and " & SetDate(de_end.DateTime.Date) & ""

                End If
                
            End If

            If Ce_Posting.EditValue = True Then
                ssql += "and lower(glt_posted)='y'"
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

            Dim rpt As New rptGLReport
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

                If Ce_Posting.EditValue = True Then
                    .Posting_Option = True
                Else
                    .Posting_Option = False
                End If

                '.FilterString = "[value] <> 0"
                .periode = de_end.DateTime.Date.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"

                ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

                Dim dt As New DataTable
                dt = GetTableData(ssql)

                For Each dr As DataRow In dt.Rows
                    .XrLabelTitle.Text = dr("dom_company")
                Next
                '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                '.Parameters("PPosisi").Value = posisi

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "General Ledger Report"


                ''ps.PreviewFormEx.MdiParent = FMainMenu
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "General Ledger Report"
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

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Try
            If ask("Are you sure to recalculate?", "Information..") = False Then
                Exit Sub
            End If
            Dim sSQLs As New ArrayList
            ssql = "SELECT  " _
                & "  a.en_id, " _
                & "  a.en_desc " _
                & "FROM " _
                & "  public.en_mstr a " _
                & "WHERE " _
                & "  a.en_id > 0 " _
                & "ORDER BY " _
                & "  a.en_id"

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                ssql = "SELECT  " _
                    & "  b.ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  b.ac_is_sumlevel, " _
                    & "  b.ac_sign, " _
                    & "  b.ac_active " _
                    & "FROM " _
                    & "  public.ac_mstr b " _
                    & "WHERE " _
                    & "  b.ac_is_sumlevel = 'N' AND  " _
                    & "  b.ac_id > 0 " _
                    & "ORDER BY " _
                    & "  b.ac_code"

                Dim dt_akun As New DataTable
                dt_akun = GetTableData(ssql)
                Dim x As Integer = 1
                For Each dr_akun As DataRow In dt_akun.Rows
                    LabelControl1.Text = "Proses akun : " & dr_akun("ac_name") & " entitas : " & dr("en_desc") & " dimulai (count " & x & " of " & dt_akun.Rows.Count & ")"
                    System.Windows.Forms.Application.DoEvents()
                    'cari dulu yg unpost
                    If dr_akun("ac_sign").ToString.ToUpper = "D" Then
                        ssql = "SELECT  " _
                            & "  sum((c.glt_debit-c.glt_credit) * c.glt_exc_rate) as jml " _
                            & "FROM " _
                            & "  public.glt_det c " _
                            & "WHERE " _
                            & "  c.glt_en_id = " & dr("en_id") & " AND  " _
                            & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
                            & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
                            & "  c.glt_posted = 'N' " _
                            & "GROUP by glt_ac_id"


                    Else
                        ssql = "SELECT  " _
                          & "  sum((c.glt_credit-c.glt_debit) * c.glt_exc_rate) as jml " _
                          & "FROM " _
                          & "  public.glt_det c " _
                          & "WHERE " _
                          & "  c.glt_en_id = " & dr("en_id") & " AND  " _
                          & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
                          & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
                          & "  c.glt_posted = 'N' " _
                          & "GROUP by glt_ac_id"
                    End If

                    Dim dt_hasil As New DataTable
                    dt_hasil = GetTableData(ssql)
                    Dim _glbal_value_unpost, _glbal_value_posted As Double

                    If dt_hasil.Rows.Count > 0 Then
                        _glbal_value_unpost = dt_hasil.Rows(0)(0)
                    Else
                        _glbal_value_unpost = 0
                    End If

                    'baru cari yg posted
                    If dr_akun("ac_sign").ToString.ToUpper = "D" Then

                        ssql = "SELECT  " _
                            & "  sum((c.glt_debit-c.glt_credit) * c.glt_exc_rate) as jml " _
                            & "FROM " _
                            & "  public.glt_det c " _
                            & "WHERE " _
                            & "  c.glt_en_id = " & dr("en_id") & " AND  " _
                            & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
                            & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
                            & "  c.glt_posted = 'Y' " _
                            & "GROUP by glt_ac_id"

                    Else
                        ssql = "SELECT  " _
                          & "  sum((c.glt_credit-c.glt_debit) * c.glt_exc_rate) as jml " _
                          & "FROM " _
                          & "  public.glt_det c " _
                          & "WHERE " _
                          & "  c.glt_en_id = " & dr("en_id") & " AND  " _
                          & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
                          & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
                          & "  c.glt_posted = 'Y' " _
                          & "GROUP by glt_ac_id"
                    End If

                    dt_hasil = GetTableData(ssql)

                    If dt_hasil.Rows.Count > 0 Then
                        _glbal_value_posted = dt_hasil.Rows(0)(0)
                    Else
                        _glbal_value_posted = 0
                    End If

                    ssql = "UPDATE  " _
                      & "  public.glbal_balance   " _
                      & "SET  " _
                      & "  glbal_balance_unposted = " & SetDec(_glbal_value_unpost) & ",  " _
                      & "  glbal_balance_posted = " & SetDec(_glbal_value_posted) & "  " _
                      & "WHERE  " _
                      & "  glbal_gcal_oid = " & SetSetring(le_gcal.EditValue.ToString) & " and  " _
                      & "  glbal_en_id = " & dr("en_id") & " and " _
                      & "  glbal_ac_id = " & dr_akun("ac_id") & " "

                    sSQLs.Add(ssql)

                    LabelControl1.Text = "Proses akun : " & dr_akun("ac_name") & " entitas : " & dr("en_desc") & " selesai (count " & x & " of " & dt_akun.Rows.Count & ")"
                    System.Windows.Forms.Application.DoEvents()
                    x += 1

                Next

                If master_new.PGSqlConn.status_sync = True Then
                    If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                        Exit Sub
                    End If
                    sSQLs.Clear()
                Else
                    If DbRunTran(sSQLs, "") = False Then
                        Exit Sub
                    End If
                    sSQLs.Clear()
                End If
            Next

            Box("Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

   
End Class
