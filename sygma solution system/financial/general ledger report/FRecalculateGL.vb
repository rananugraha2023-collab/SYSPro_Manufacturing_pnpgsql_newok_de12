Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FRecalculateGL
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

    Private Sub le_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal.EditValueChanged
        Try
            de_first.EditValue = le_gcal.GetColumnValue("gcal_start_date")
            de_end.EditValue = le_gcal.GetColumnValue("gcal_end_date")
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

            'If le_entity.EditValue = 0 Then
            '    ssql = "SELECT  " _
            '       & "  a.en_id, " _
            '       & "  a.en_desc " _
            '       & "FROM " _
            '       & "  public.en_mstr a " _
            '       & "WHERE " _
            '       & "  a.en_id > 0 " _
            '       & "ORDER BY " _
            '       & "  a.en_id"
            'Else
            '    ssql = "SELECT  " _
            '       & "  a.en_id, " _
            '       & "  a.en_desc " _
            '       & "FROM " _
            '       & "  public.en_mstr a " _
            '       & "WHERE " _
            '       & "  a.en_id = " & le_entity.EditValue _
            '       & " ORDER BY " _
            '       & "  a.en_id"
            'End If


            'Dim dt As New DataTable
            'dt = GetTableData(ssql)

            'For Each dr As DataRow In dt.Rows
            '    ssql = "SELECT  " _
            '        & "  b.ac_id, " _
            '        & "  b.ac_code, " _
            '        & "  b.ac_name, " _
            '        & "  b.ac_is_sumlevel, " _
            '        & "  b.ac_sign, " _
            '        & "  b.ac_active " _
            '        & "FROM " _
            '        & "  public.ac_mstr b " _
            '        & "WHERE " _
            '        & "  b.ac_is_sumlevel = 'N' AND  " _
            '        & "  b.ac_id > 0 " _
            '        & "ORDER BY " _
            '        & "  b.ac_code"

            '    Dim dt_akun As New DataTable
            '    dt_akun = GetTableData(ssql)
            '    Dim x As Integer = 1
            '    For Each dr_akun As DataRow In dt_akun.Rows
            '        LabelControl1.Text = "Proses akun : " & dr_akun("ac_name") & " entitas : " & dr("en_desc") & " dimulai (count " & x & " of " & dt_akun.Rows.Count & ")"
            '        System.Windows.Forms.Application.DoEvents()
            '        'cari dulu yg unpost
            '        If dr_akun("ac_sign").ToString.ToUpper = "D" Then
            '            ssql = "SELECT  " _
            '                & "  sum((c.glt_debit-c.glt_credit) * c.glt_exc_rate) as jml " _
            '                & "FROM " _
            '                & "  public.glt_det c " _
            '                & "WHERE " _
            '                & "  c.glt_en_id = " & dr("en_id") & " AND  " _
            '                & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
            '                & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
            '                & "  c.glt_posted = 'N' " _
            '                & "GROUP by glt_ac_id"


            '        Else
            '            ssql = "SELECT  " _
            '              & "  sum((c.glt_credit-c.glt_debit) * c.glt_exc_rate) as jml " _
            '              & "FROM " _
            '              & "  public.glt_det c " _
            '              & "WHERE " _
            '              & "  c.glt_en_id = " & dr("en_id") & " AND  " _
            '              & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
            '              & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
            '              & "  c.glt_posted = 'N' " _
            '              & "GROUP by glt_ac_id"
            '        End If

            '        Dim dt_hasil As New DataTable
            '        dt_hasil = GetTableData(ssql)
            '        Dim _glbal_value_unpost, _glbal_value_posted As Double

            '        If dt_hasil.Rows.Count > 0 Then
            '            _glbal_value_unpost = dt_hasil.Rows(0)(0)
            '        Else
            '            _glbal_value_unpost = 0
            '        End If

            '        'baru cari yg posted
            '        If dr_akun("ac_sign").ToString.ToUpper = "D" Then

            '            ssql = "SELECT  " _
            '                & "  sum((c.glt_debit-c.glt_credit) * c.glt_exc_rate) as jml " _
            '                & "FROM " _
            '                & "  public.glt_det c " _
            '                & "WHERE " _
            '                & "  c.glt_en_id = " & dr("en_id") & " AND  " _
            '                & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
            '                & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
            '                & "  c.glt_posted = 'Y' " _
            '                & "GROUP by glt_ac_id"

            '        Else
            '            ssql = "SELECT  " _
            '              & "  sum((c.glt_credit-c.glt_debit) * c.glt_exc_rate) as jml " _
            '              & "FROM " _
            '              & "  public.glt_det c " _
            '              & "WHERE " _
            '              & "  c.glt_en_id = " & dr("en_id") & " AND  " _
            '              & "  c.glt_ac_id = " & dr_akun("ac_id") & " AND  " _
            '              & "  c.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " AND  " _
            '              & "  c.glt_posted = 'Y' " _
            '              & "GROUP by glt_ac_id"
            '        End If

            '        dt_hasil = GetTableData(ssql)

            '        If dt_hasil.Rows.Count > 0 Then
            '            _glbal_value_posted = dt_hasil.Rows(0)(0)
            '        Else
            '            _glbal_value_posted = 0
            '        End If

            '        ssql = "UPDATE  " _
            '          & "  public.glbal_balance   " _
            '          & "SET  " _
            '          & "  glbal_balance_unposted = " & SetDec(_glbal_value_unpost) & ",  " _
            '          & "  glbal_balance_posted = " & SetDec(_glbal_value_posted) & "  " _
            '          & "WHERE  " _
            '          & "  glbal_gcal_oid = " & SetSetring(le_gcal.EditValue) & " and  " _
            '          & "  glbal_en_id = " & dr("en_id") & " and " _
            '          & "  glbal_ac_id = " & dr_akun("ac_id") & " "

            '        sSQLs.Add(ssql)

            '        LabelControl1.Text = "Proses akun : " & dr_akun("ac_name") & " entitas : " & dr("en_desc") & " selesai (count " & x & " of " & dt_akun.Rows.Count & ")"
            '        System.Windows.Forms.Application.DoEvents()
            '        x += 1

            '    Next


            ssql = "UPDATE  " _
                  & "  public.glbal_balance   " _
                  & "SET  " _
                  & "  glbal_balance_unposted = 0,  " _
                  & "  glbal_balance_posted = 0, glbal_balance_posted_end_month=0  " _
                  & "WHERE  " _
                  & "  glbal_gcal_oid = " & SetSetring(le_gcal.EditValue)

            sSQLs.Add(ssql)

            ssql = " select glt_ac_id,glt_en_id,sum(unpost) as unpost_all,sum(posted) as posted_all from ( " _
                & " SELECT " _
                & "a.glt_ac_id,a.glt_en_id, " _
                & " sum(0) as unpost, sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit) * a.glt_exc_rate) as posted  " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " and a.glt_posted='Y' " _
                & " group by a.glt_ac_id,a.glt_en_id " _
                & " union all  " _
                & "  SELECT " _
                & "a.glt_ac_id,a.glt_en_id, " _
                & "  sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit)  * a.glt_exc_rate) as unpost, sum(0) as posted  " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_date BETWEEN " & SetDateNTime00(de_first.DateTime) & " AND " & SetDateNTime00(de_end.DateTime) & " and a.glt_posted='N' " _
                & " group by a.glt_ac_id,a.glt_en_id) as temp " _
                & " group by glt_ac_id,glt_en_id"

            Dim dt As New DataTable
            dt = GetTableData(ssql)


            For Each dr As DataRow In dt.Rows

                ssql = "UPDATE  " _
                  & "  public.glbal_balance   " _
                  & "SET  " _
                  & "  glbal_balance_unposted = " & SetDec(dr("unpost_all")) & ",  " _
                  & "  glbal_balance_posted = " & SetDec(dr("posted_all")) & "  " _
                  & "WHERE  " _
                  & "  glbal_gcal_oid = " & SetSetring(le_gcal.EditValue) & " and  " _
                  & "  glbal_en_id = " & dr("glt_en_id") & " and " _
                  & "  glbal_ac_id = " & dr("glt_ac_id") & " "

                sSQLs.Add(ssql)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.ModFunction.FinsertSQL2Array(sSQLs), "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            End If

            'Next

            Box("Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
