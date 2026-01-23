Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FLKGenerate
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ssql = "SELECT  " _
                & "  sum(public.glbal_balance.glbal_balance_open) AS glbal_balance_open, " _
                & "  sum(coalesce(public.glbal_balance.glbal_balance_unposted,0) + coalesce( public.glbal_balance.glbal_balance_posted,0)) AS glbal_balance_posted, " _
                & "  public.glbal_balance.glbal_ac_id, " _
                & "   TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode " _
                & "FROM " _
                & "  public.glbal_balance " _
                & "  INNER JOIN public.gcal_mstr ON (public.glbal_balance.glbal_gcal_oid = public.gcal_mstr.gcal_oid) " _
                & "WHERE " _
                & "  TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') in ('202001','202002','202003','202004') " _
                & "GROUP BY " _
                & "  public.glbal_balance.glbal_ac_id, " _
                & "  periode"


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FLKGenerate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            init_le(le_year, "year_periode")
            init_le(gcal_start, "gcal_mstr")
            init_le(gcal_end, "gcal_mstr")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtGenUnclosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenUnclosing.Click
        Try

            If MessageBox.Show("Are you sure to generate this periode ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
            LblProses.Text = "Init data"
            Windows.Forms.Application.DoEvents()
            Dim _waktu_awal As Date = Now

            ''cek periode yang belum closing, hanya periode yang belum closing saja yang akan di generate
            ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode  " _
                & "from gcal_mstr where  " _
                & "gcal_closing='N' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') between '" & gcal_start.GetColumnValue("periode") _
                & "' and '" & gcal_end.GetColumnValue("periode") & "' order by gcal_start_date "

            Dim dt_unclose_periode As New DataTable
            dt_unclose_periode = GetTableData(ssql)

            Dim _periode_start As String = ""
            Dim _periode_end As String = ""
            Dim z As Integer = 0

            For Each dr As DataRow In dt_unclose_periode.Rows
                If z = 0 Then
                    _periode_start = dr("periode")
                    _periode_end = dr("periode")
                Else
                    _periode_end = dr("periode")
                End If
                z = z + 1
            Next

            'jika cutoff tapi periode start sudah close maka tidak boleh, 
            If CeCutOff.Checked Then
                If gcal_start.GetColumnValue("gcal_closing") = "Y" Then
                    Box("Period cut off has been closed")
                    Exit Sub
                End If

                If _periode_start <> gcal_start.GetColumnValue("periode") Then
                    Box("The initial period must not be closed")
                    Exit Sub
                End If
            End If

            'End If

            ''cek apakah ada periode yang masih open dibawah yang akan ditampilkan
            ''jika ada maka harus ikut dihitung
            ''jika tidak ada maka cukup hitung periode yg akan ditampilkan

            ssql = "SELECT  " _
                & "  a.ac_id, " _
                & "  a.ac_code, ac_code_hirarki, " _
                & "  a.ac_name " _
                & "FROM " _
                & "  public.ac_mstr a " _
                & "WHERE " _
                & "  a.ac_active = 'Y' AND  " _
                & "  a.ac_is_sumlevel = 'N' and ac_code <> '-' and a.ac_id not in (select tb_ac_id from tb_trial where tb_year='" & _periode_start.Substring(0, 4) & "')  " _
                & "order by ac_id"

            Dim dt_ac As New DataTable
            dt_ac = GetTableData(ssql)

            Dim ssqls As New ArrayList

            For Each dr As DataRow In dt_ac.Rows
                ssql = "INSERT INTO  " _
                    & "  public.tb_trial " _
                    & "( " _
                    & "  tb_oid, " _
                    & "  tb_ac_id, " _
                    & "  tb_year, " _
                    & "  tb_month_01_tran, " _
                    & "  tb_type,tb_sort_number, " _
                    & "  tb_month_01_opening, " _
                    & "  tb_month_01_ending, " _
                    & "  tb_month_01_closing " _
                    & ") " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & SetInteger(dr("ac_id")) & ",  " _
                    & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
                    & "0" & ",  " _
                    & SetSetring("TB") & ",  " _
                    & SetSetring(dr("ac_code_hirarki")) & ",  " _
                    & SetDbl(0) & ",  " _
                    & "0" & ",  " _
                    & SetSetring("") & "  " _
                    & ")"

                ssqls.Add(ssql)
            Next


            'ssql = "SELECT  " _
            '        & "  tb_sort_number " _
            '        & "FROM " _
            '        & "  public.tb_trial a " _
            '        & "WHERE " _
            '        & "  a.tb_type = 'BS' AND  " _
            '        & "  a.tb_year='" & le_year.EditValue & "'"




            'Dim dt_bs As New DataTable
            'dt_bs = GetTableData(ssql)

            'Dim _v_harta As Boolean = False
            'Dim _v_kewajiban As Boolean = False
            'Dim _v_modal As Boolean = False
            'Dim _v_kewajiban_modal As Boolean = False
            'For Each dr As DataRow In dt_bs.Rows
            '    If dr(0).ToString = "19999" Then
            '        _v_harta = True
            '    End If
            '    If dr(0).ToString = "29999" Then
            '        _v_kewajiban = True
            '    End If
            '    If dr(0).ToString = "39991" Then
            '        _v_modal = True
            '    End If
            '    If dr(0).ToString = "39992" Then
            '        _v_kewajiban_modal = True
            '    End If

            'Next

            'If _v_harta = False Then
            '    ssql = "INSERT INTO  " _
            '        & "  public.tb_trial " _
            '        & "( " _
            '        & "  tb_oid, " _
            '        & "  tb_ac_id, " _
            '        & "  tb_year, " _
            '        & "  tb_month_01_tran, " _
            '        & "  tb_type, tb_sort_number,tb_desc, " _
            '        & "  tb_month_01_opening, " _
            '        & "  tb_month_01_ending, " _
            '        & "  tb_month_01_closing " _
            '        & ") " _
            '        & "VALUES ( " _
            '        & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '        & "null" & ",  " _
            '        & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
            '        & "0" & ",  " _
            '        & SetSetring("BS") & ", '19999','TOTAL HARTA', " _
            '        & SetDbl(0) & ",  " _
            '        & "0" & ",  " _
            '        & SetSetring("") & "  " _
            '        & ")"

            '    ssqls.Add(ssql)
            'End If

            'If _v_kewajiban = False Then
            '    ssql = "INSERT INTO  " _
            '       & "  public.tb_trial " _
            '       & "( " _
            '       & "  tb_oid, " _
            '       & "  tb_ac_id, " _
            '       & "  tb_year, " _
            '       & "  tb_month_01_tran, " _
            '       & "  tb_type, tb_sort_number,tb_desc, " _
            '       & "  tb_month_01_opening, " _
            '       & "  tb_month_01_ending, " _
            '       & "  tb_month_01_closing " _
            '       & ") " _
            '       & "VALUES ( " _
            '       & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '       & "null" & ",  " _
            '       & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
            '       & "0" & ",  " _
            '       & SetSetring("BS") & ", '29999', 'TOTAL KEWAJIBAN'," _
            '       & SetDbl(0) & ",  " _
            '       & "0" & ",  " _
            '       & SetSetring("") & "  " _
            '       & ")"

            '    ssqls.Add(ssql)
            'End If


            'If _v_modal = False Then
            '    ssql = "INSERT INTO  " _
            '     & "  public.tb_trial " _
            '     & "( " _
            '     & "  tb_oid, " _
            '     & "  tb_ac_id, " _
            '     & "  tb_year, " _
            '     & "  tb_month_01_tran, " _
            '     & "  tb_type, tb_sort_number,tb_desc, " _
            '     & "  tb_month_01_opening, " _
            '     & "  tb_month_01_ending, " _
            '     & "  tb_month_01_closing " _
            '     & ") " _
            '     & "VALUES ( " _
            '     & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '     & "null" & ",  " _
            '     & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
            '     & "0" & ",  " _
            '     & SetSetring("BS") & ", '39991', 'TOTAL MODAL'," _
            '     & SetDbl(0) & ",  " _
            '     & "0" & ",  " _
            '     & SetSetring("") & "  " _
            '     & ")"

            '    ssqls.Add(ssql)
            'End If

            'If _v_kewajiban_modal = False Then
            '    ssql = "INSERT INTO  " _
            '     & "  public.tb_trial " _
            '     & "( " _
            '     & "  tb_oid, " _
            '     & "  tb_ac_id, " _
            '     & "  tb_year, " _
            '     & "  tb_month_01_tran, " _
            '     & "  tb_type, tb_sort_number,tb_desc, " _
            '     & "  tb_month_01_opening, " _
            '     & "  tb_month_01_ending, " _
            '     & "  tb_month_01_closing " _
            '     & ") " _
            '     & "VALUES ( " _
            '     & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '     & "null" & ",  " _
            '     & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
            '     & "0" & ",  " _
            '     & SetSetring("BS") & ", '39992', 'TOTAL KEWAJIBAN + MODAL' ," _
            '     & SetDbl(0) & ",  " _
            '     & "0" & ",  " _
            '     & SetSetring("") & "  " _
            '     & ")"

            '    ssqls.Add(ssql)
            'End If


            ssql = "SELECT  " _
                & "  a.bs_code, " _
                & "  a.bs_sort_number, " _
                & "  a.bs_desc " _
                & "FROM " _
                & "  public.tconfsettingbs a " _
                & "WHERE " _
                & "  a.bs_code NOT IN (SELECT tb_code from tb_trial " _
                & " where tb_trial.tb_type='BS' and tb_year='" & le_year.EditValue & "')"


            Dim dt_conf_bs As New DataTable
            dt_conf_bs = GetTableData(ssql)

            For Each dr As DataRow In dt_conf_bs.Rows

                ssql = "INSERT INTO  " _
                    & "  public.tb_trial " _
                    & "( " _
                    & "  tb_oid, " _
                    & "  tb_ac_id, " _
                    & "  tb_year, " _
                    & "  tb_month_01_tran, " _
                    & "  tb_type, tb_sort_number,tb_desc,tb_code, " _
                    & "  tb_month_01_opening, " _
                    & "  tb_month_01_ending, " _
                    & "  tb_month_01_closing " _
                    & ") " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & "null" & ",  " _
                    & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
                    & "0" & ",  " _
                    & SetSetring("BS") & ", '" & dr("bs_sort_number") & "','" & dr("bs_desc") & "', " _
                    & SetSetring(dr("bs_code")) & ", " _
                    & SetDbl(0) & ",  " _
                    & "0" & ",  " _
                    & SetSetring("") & "  " _
                    & ")"

                ssqls.Add(ssql)

            Next



            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If


            For Each dr As DataRow In dt_unclose_periode.Rows
                Dim _periode As Integer = dr("periode").ToString.Substring(4, 2)

                ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_opening=0" _
                       & ",  tb_month_" & _periode.ToString("00") & "_ending =0,  tb_month_" & _periode.ToString("00") _
                       & "_tran =0 " _
                       & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' "

                ssqls.Add(ssql)

            Next


            LblProses.Text = "Reset data"
            Windows.Forms.Application.DoEvents()
            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If


            If CeCutOff.Checked = True Then


                ssql = "SELECT  " _
                    & "  sum(public.glbal_balance.glbal_balance_open) AS glbal_balance_open, " _
                    & "  sum(coalesce(public.glbal_balance.glbal_balance_unposted,0) + coalesce( public.glbal_balance.glbal_balance_posted,0)) AS glbal_balance_posted, " _
                    & "  public.glbal_balance.glbal_ac_id,ac_code,ac_name, " _
                    & "   TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode,ac_cu_id " _
                    & "FROM " _
                    & "  public.glbal_balance " _
                    & "  INNER JOIN public.gcal_mstr ON (public.glbal_balance.glbal_gcal_oid = public.gcal_mstr.gcal_oid) " _
                    & "  inner join ac_mstr on ac_id=glbal_ac_id " _
                    & "WHERE " _
                    & "  TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') = '" & _periode_start & "' " _
                    & " GROUP BY " _
                    & "  public.glbal_balance.glbal_ac_id,ac_code,ac_name, " _
                    & "  TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00'),ac_cu_id " _
                    & "order by periode, ac_code"

                Dim dt_start As New DataTable
                dt_start = GetTableData(ssql)

                ssqls.Clear()

                For Each dr As DataRow In dt_start.Rows
                    Dim _periode As Integer = _periode_start.Substring(4, 2)

                    ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_opening=" & SetDec(dr("glbal_balance_open")) _
                       & " " _
                       & " where tb_year='" & _periode_start.Substring(0, 4) & "' and tb_ac_id=" & SetInteger(dr("glbal_ac_id"))

                    ssqls.Add(ssql)

                Next
            Else
                ssqls.Clear()

                Dim _periode As Integer = _periode_start.Substring(4, 2) 'gcal_start.GetColumnValue("periode").ToString.Substring(4, 2)

                Dim _periode_before As Integer = _periode - 1
                Dim _year As Integer = _periode_start.Substring(0, 4)

                If _periode_before = 0 Then
                    _periode_before = 12
                    _year = _year - 1

                End If


                ssql = "SELECT  " _
                    & "  a.tb_oid, " _
                    & "  a.tb_year, " _
                    & "  a.tb_ac_id, " _
                    & "  a.tb_month_" & _periode_before.ToString("00") & "_ending as jml,ac_type " _
                    & "FROM " _
                    & "  public.tb_trial a " _
                    & " INNER JOIN public.ac_mstr ON (a.tb_ac_id = public.ac_mstr.ac_id) " _
                    & "WHERE " _
                    & "  a.tb_year = '" & _year & "'  " _
                    & "  order by a.tb_ac_id"

                'and a.tb_ac_id in (select ac_id from ac_mstr where ac_is_sumlevel='N' and ac_type not in ('R','E') )

                Dim dt_opening As New DataTable
                dt_opening = GetTableData(ssql)

                For Each dr As DataRow In dt_opening.Rows



                    If dr("ac_type") = "R" Or dr("ac_type") = "E" Then
                    Else
                        ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_opening=" & SetDec(dr("jml")) _
                      & " " _
                      & " where tb_year='" & _periode_start.Substring(0, 4) & "' and tb_ac_id=" & SetInteger(dr("tb_ac_id"))

                        ssqls.Add(ssql)
                    End If


                Next

            End If

            LblProses.Text = "Opening balance"
            Windows.Forms.Application.DoEvents()
            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If


            ssql = "SELECT  " _
               & "  to_char(public.glt_det.glt_date, 'YYYYMM') AS glt_date, " _
               & "  public.glt_det.glt_ac_id, " _
               & "  sum(public.glt_det.glt_debit * glt_exc_rate) AS glt_debit, " _
               & "  sum(public.glt_det.glt_credit * glt_exc_rate) AS glt_credit, " _
               & "  max(public.ac_mstr.ac_sign) as ac_sign " _
               & "FROM " _
               & "  public.glt_det " _
               & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
               & "WHERE " _
               & "  to_char(public.glt_det.glt_date, 'YYYYMM')  between '" & _periode_start & "' and '" & _periode_end & "' " _
               & "GROUP BY " _
               & "  to_char(public.glt_det.glt_date, 'YYYYMM'), " _
               & "  public.glt_det.glt_ac_id, " _
               & "  public.ac_mstr.ac_sign " _
               & " order by glt_date, glt_ac_id "


            ssqls.Clear()
            Dim dt_sum As New DataTable
            dt_sum = GetTableData(ssql)

            For Each dr As DataRow In dt_sum.Rows
                If dr("ac_sign").ToString = "D" Then
                    ssql = "update tb_trial set tb_month_" & dr("glt_date").ToString.Substring(4, 2) & "_tran = " & SetDbl(dr("glt_debit") - dr("glt_credit")) _
                    & " where tb_ac_id=" & SetInteger(dr("glt_ac_id")) & " and tb_year='" & le_year.EditValue & "'"

                    ssqls.Add(ssql)
                Else
                    ssql = "update tb_trial set tb_month_" & dr("glt_date").ToString.Substring(4, 2) & "_tran = " & SetDbl(dr("glt_credit") - dr("glt_debit")) _
                    & " where tb_ac_id=" & SetInteger(dr("glt_ac_id")) & " and tb_year='" & le_year.EditValue & "'"

                    ssqls.Add(ssql)
                End If

            Next

            LblProses.Text = "Update data tran "
            Windows.Forms.Application.DoEvents()

            Dim _sukses As Boolean = False
            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    _sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    _sukses = True
                End If
                ssqls.Clear()
            End If



            'ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode from gcal_mstr where TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') >= '" _
            '   & gcal_start.GetColumnValue("periode") & "' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') <= '" & gcal_end.GetColumnValue("periode") & "'  order by periode"

            'Dim dt_periode As New DataTable
            'dt_periode = GetTableData(ssql)

            Dim _ac_id_running_profit As Integer = func_coll.get_conf_file("ac_running_profit")
            Dim _ac_retained_earning As Integer = func_coll.get_conf_file("ac_retained_earning")



            Dim y As Integer = 0
            For Each dr As DataRow In dt_unclose_periode.Rows
                Dim _periode As Integer = dr("periode").ToString.Substring(4, 2)

                If y = 0 Then

                    ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_ending = tb_month_" & _periode.ToString("00") & "_opening + tb_month_" & _periode.ToString("00") & "_tran " _
                   & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "'"

                    ssqls.Add(ssql)

                    LblProses.Text = "Update data ending " & dr("periode").ToString
                    Windows.Forms.Application.DoEvents()


                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    Else
                        If DbRunTran(ssqls, "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    End If

                    ssql = "select sum(tb_month_" & _periode.ToString("00") & "_ending) as jml  from tb_trial where tb_year='" _
                        & dr("periode").ToString.Substring(0, 4) & "' and tb_year='" & dr("periode").ToString.Substring(0, 4) _
                        & "' and tb_ac_id in (select ac_id from ac_mstr where ac_is_sumlevel='N' and ac_type='R')"

                    Dim _revenue As Double = 0
                    _revenue = GetRowInfo(ssql)(0)

                    ssql = "select sum(tb_month_" & _periode.ToString("00") & "_ending) as jml  from tb_trial where tb_year='" _
                       & dr("periode").ToString.Substring(0, 4) & "' and tb_year='" & dr("periode").ToString.Substring(0, 4) _
                       & "' and tb_ac_id in (select ac_id from ac_mstr where ac_is_sumlevel='N' and ac_type='E')"

                    Dim _expense As Double = 0
                    _expense = GetRowInfo(ssql)(0)



                    Dim _saldo_awal_laba_berjalan As Double = 0.0

                    ssql = "select tb_month_" & _periode.ToString("00") & "_opening from tb_trial where tb_year='" & le_year.EditValue _
                        & "' and tb_ac_id=" & SetInteger(_ac_id_running_profit)

                    _saldo_awal_laba_berjalan = GetRowInfo(ssql)(0)

                    Dim _saldo As Double = _revenue - _expense


                    'ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_tran=tb_month_" & _periode.ToString("00") & "_tran + " & SetDec(_saldo) _
                    '    & ",  tb_month_" & _periode.ToString("00") & "_ending = tb_month_" & _periode.ToString("00") _
                    '    & "_opening + tb_month_" & _periode.ToString("00") & "_tran +  " & SetDec(_saldo) & " " _
                    '    & " where tb_year='" & le_year.EditValue & "' and tb_ac_id=" & SetInteger(_ac_id_running_profit)

                    ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_ending =  tb_month_" & _periode.ToString("00") _
                        & "_opening + tb_month_" & _periode.ToString("00") & "_tran +  " & SetDec(_saldo_awal_laba_berjalan) & " " _
                       & " where tb_year='" & le_year.EditValue & "' and tb_ac_id=" & SetInteger(_ac_retained_earning)

                    ssqls.Add(ssql)

                    ssql = "update tb_trial set  tb_month_" & _periode.ToString("00") & "_tran=tb_month_" & _periode.ToString("00") & "_tran + " & SetDec(_saldo) _
                        & ", tb_month_" & _periode.ToString("00") & "_ending =  tb_month_" & _periode.ToString("00") & "_tran +  " & SetDec(_saldo) & " " _
                       & " where tb_year='" & le_year.EditValue & "' and tb_ac_id=" & SetInteger(_ac_id_running_profit)

                    ssqls.Add(ssql)


                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    Else
                        If DbRunTran(ssqls, "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    End If

                Else
                    Dim _periode_before As Integer = _periode - 1
                    'Dim _year As Integer = _periode_start.Substring(0, 4)

                    'If _periode_before = 0 Then
                    '    _periode_before = 12
                    '    _year = le_year.EditValue - 1

                    'End If

                    ssql = "SELECT  " _
                        & "  a.tb_oid, " _
                        & "  a.tb_year, " _
                        & "  a.tb_ac_id, " _
                        & "  a.tb_month_" & _periode_before.ToString("00") & "_ending as jml,ac_type " _
                        & "FROM " _
                        & "  public.tb_trial a " _
                        & " INNER JOIN public.ac_mstr ON (a.tb_ac_id = public.ac_mstr.ac_id) " _
                        & "WHERE " _
                        & "  a.tb_year = '" & dr("periode").ToString.Substring(0, 4) & "'  " _
                        & "  order by a.tb_ac_id"

                    'and a.tb_ac_id in (select ac_id from ac_mstr where ac_is_sumlevel='N' and ac_type not in ('R','E') )

                    Dim dt_opening As New DataTable
                    dt_opening = GetTableData(ssql)

                    For Each dr_opening As DataRow In dt_opening.Rows

                        If dr_opening("ac_type") = "R" Or dr_opening("ac_type") = "E" Then
                            ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_opening=" & SetDec(0) _
                               & ",  tb_month_" & _periode.ToString("00") & "_ending = tb_month_" & _periode.ToString("00") _
                               & "_tran  " _
                               & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_ac_id=" & SetInteger(dr_opening("tb_ac_id"))

                            ssqls.Add(ssql)
                        Else
                            ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_opening=" & SetDec(dr_opening("jml")) _
                               & ",  tb_month_" & _periode.ToString("00") & "_ending = tb_month_" & _periode.ToString("00") _
                               & "_tran + " & SetDec(dr_opening("jml")) & " " _
                               & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_ac_id=" & SetInteger(dr_opening("tb_ac_id"))

                            ssqls.Add(ssql)
                        End If
                    Next

                    LblProses.Text = "Update data ending " & dr("periode").ToString
                    Windows.Forms.Application.DoEvents()


                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    Else
                        If DbRunTran(ssqls, "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    End If

                    ssql = "select sum(tb_month_" & _periode.ToString("00") & "_ending) as jml  from tb_trial where tb_year='" _
                        & dr("periode").ToString.Substring(0, 4) & "' and tb_year='" & dr("periode").ToString.Substring(0, 4) _
                        & "' and tb_ac_id in (select ac_id from ac_mstr where ac_is_sumlevel='N' and ac_type='R')"

                    Dim _revenue As Double = 0
                    _revenue = GetRowInfo(ssql)(0)

                    ssql = "select sum(tb_month_" & _periode.ToString("00") & "_ending) as jml  from tb_trial where tb_year='" _
                       & dr("periode").ToString.Substring(0, 4) & "' and tb_year='" & dr("periode").ToString.Substring(0, 4) _
                       & "' and tb_ac_id in (select ac_id from ac_mstr where ac_is_sumlevel='N' and ac_type='E')"

                    Dim _expense As Double = 0
                    _expense = GetRowInfo(ssql)(0)

                    Dim _saldo As Double = _revenue - _expense


                    ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_tran=" & SetDec(_saldo) _
                        & ",  tb_month_" & _periode.ToString("00") & "_ending = tb_month_" & _periode.ToString("00") _
                        & "_opening + " & SetDec(_saldo) & " " _
                        & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_ac_id=" & SetInteger(_ac_id_running_profit)

                    ssqls.Add(ssql)


                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    Else
                        If DbRunTran(ssqls, "") = False Then
                            Exit Sub
                        Else
                            '_sukses = True
                        End If
                        ssqls.Clear()
                    End If

                End If

                y = y + 1
            Next


            For Each dr As DataRow In dt_unclose_periode.Rows
                Dim _periode As Integer = dr("periode").ToString.Substring(4, 2)

                ssql = "SELECT  " _
                    & "  sum(a.tb_month_" & _periode.ToString("00") & "_ending) as jml " _
                    & "FROM " _
                    & "  public.tb_trial a " _
                    & "WHERE " _
                    & "  a.tb_type = 'TB' AND  " _
                    & "  a.tb_year='" & dr("periode").ToString.Substring(0, 4) & "' AND  " _
                    & "  a.tb_sort_number < '19999'"

                Dim _harta As Double = 0
                _harta = GetRowInfo(ssql)(0)

                ssql = "SELECT  " _
                   & "  sum(a.tb_month_" & _periode.ToString("00") & "_ending) as jml " _
                   & "FROM " _
                   & "  public.tb_trial a " _
                   & "WHERE " _
                   & "  a.tb_type = 'TB' AND  " _
                   & "  a.tb_year='" & dr("periode").ToString.Substring(0, 4) & "' AND  " _
                   & "  a.tb_sort_number > '19999' and  a.tb_sort_number < '29999'"

                Dim _kewajiban As Double = 0
                _kewajiban = GetRowInfo(ssql)(0)

                ssql = "SELECT  " _
                   & "  sum(a.tb_month_" & _periode.ToString("00") & "_ending) as jml " _
                   & "FROM " _
                   & "  public.tb_trial a " _
                   & "WHERE " _
                   & "  a.tb_type = 'TB' AND  " _
                   & "  a.tb_year='" & dr("periode").ToString.Substring(0, 4) & "' AND  " _
                   & "  a.tb_sort_number > '29999' and  a.tb_sort_number < '39999'"

                Dim _modal As Double = 0
                _modal = GetRowInfo(ssql)(0)

                ssql = "update tb_trial set  tb_month_" & _periode.ToString("00") & "_ending = " & SetDec(_harta) & " " _
                       & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_sort_number=" & SetSetring("19999")

                ssqls.Add(ssql)

                ssql = "update tb_trial set  tb_month_" & _periode.ToString("00") & "_ending = " & SetDec(_kewajiban) & " " _
                      & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_sort_number=" & SetSetring("29999")

                ssqls.Add(ssql)


                ssql = "update tb_trial set  tb_month_" & _periode.ToString("00") & "_ending = " & SetDec(_modal) & " " _
                     & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_sort_number=" & SetSetring("39991")

                ssqls.Add(ssql)


                ssql = "update tb_trial set  tb_month_" & _periode.ToString("00") & "_ending = " & SetDec(_modal + _kewajiban) & " " _
                     & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "' and tb_sort_number=" & SetSetring("39992")

                ssqls.Add(ssql)



                LblProses.Text = "Update data BS " & dr("periode").ToString
                Windows.Forms.Application.DoEvents()

                If master_new.PGSqlConn.status_sync = True Then
                    If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                        Exit Sub
                    Else
                        '_sukses = True
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        Exit Sub
                    Else
                        '_sukses = True
                    End If
                    ssqls.Clear()
                End If

            Next

            Dim _lama_proses As Integer = 0
            Dim _waktu_akhir As Date = Now
            _lama_proses = DateDiff(DateInterval.Second, _waktu_awal, _waktu_akhir) + 1
            LblProses.Text = "Lama proses : " & _lama_proses & " detik . Start : " & _waktu_awal.ToString("dd/MM/yyyy hh:mm:ss") & " to " & _waktu_akhir.ToString("dd/MM/yyyy hh:mm:ss")



            If _sukses Then
                Box("Sukses")
            Else
                Box("Gagal")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtClosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtClosing.Click
        Try

            If MessageBox.Show("Are you sure to closing this periode ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode from gcal_mstr where TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') >= '" _
               & gcal_start.GetColumnValue("periode") & "' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') <= '" & gcal_end.GetColumnValue("periode") & "'  order by periode"

            Dim dt_periode As New DataTable
            dt_periode = GetTableData(ssql)

            Dim ssqls As New ArrayList

            For Each dr As DataRow In dt_periode.Rows
                ssql = "update public.gcal_mstr set  gcal_closing='Y', gcal_pra_closing='Y' where gcal_oid=" & SetSetring(dr("gcal_oid"))

                ssqls.Add(ssql)

                ssql = "UPDATE  " _
                   & "  public.gcald_det   " _
                   & "SET  " _
                   & "  gcald_ap = 'Y', " _
                   & "  gcald_ar = 'Y', " _
                   & "  gcald_fa = 'Y', " _
                   & "  gcald_ic = 'Y', " _
                   & "  gcald_so = 'Y', " _
                   & "  gcald_gl = 'Y', " _
                   & "  gcald_year = 'N', " _
                   & "  gcal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                   & "  " _
                   & "WHERE gcald_gcal_oid = " & SetSetring(dr("gcal_oid")) & " "

                ssqls.Add(ssql)

            Next
            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If

            init_le(gcal_start, "gcal_mstr")
            init_le(gcal_end, "gcal_mstr")

            le_year_EditValueChanged(Nothing, Nothing)
            Box("Sukses")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub le_year_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_year.EditValueChanged
        Try
            Dim _start_oid, _end_oid As String

            Try
                _start_oid = GetRowInfo("select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode from gcal_mstr where TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') = '" _
                & le_year.EditValue & "01'")(0)

                gcal_start.EditValue = _start_oid
            Catch ex As Exception
            End Try
            
            Try
                _end_oid = GetRowInfo("select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode from gcal_mstr where TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') = '" _
                & le_year.EditValue & "12'")(0)

                gcal_end.EditValue = _end_oid
            Catch ex As Exception
            End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtOpenPeriode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOpenPeriode.Click
        Try
            If MessageBox.Show("Are you sure to open this periode ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode from gcal_mstr where TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') >= '" _
              & gcal_start.GetColumnValue("periode") & "' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') <= '" & gcal_end.GetColumnValue("periode") & "'  order by periode"

            Dim dt_periode As New DataTable
            dt_periode = GetTableData(ssql)

            Dim ssqls As New ArrayList

            For Each dr As DataRow In dt_periode.Rows
                Dim _periode As Integer = dr("periode").ToString.Substring(4, 2)

                ssql = "update public.gcal_mstr set  gcal_closing='N', gcal_pra_closing='N' where gcal_oid=" & SetSetring(dr("gcal_oid"))

                ssqls.Add(ssql)

                ssql = "UPDATE  " _
                   & "  public.gcald_det   " _
                   & "SET  " _
                   & "  gcald_ap = 'N', " _
                   & "  gcald_ar = 'N', " _
                   & "  gcald_fa = 'N', " _
                   & "  gcald_ic = 'N', " _
                   & "  gcald_so = 'N', " _
                   & "  gcald_gl = 'N', " _
                   & "  gcald_year = 'N', " _
                   & "  gcal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                   & "  " _
                   & "WHERE gcald_gcal_oid = " & SetSetring(dr("gcal_oid")) & " "

                ssqls.Add(ssql)


                ssql = "update tb_trial set tb_month_" & _periode.ToString("00") & "_opening=0," _
                       & " tb_month_" & _periode.ToString("00") & "_tran=0," _
                       & " tb_month_" & _periode.ToString("00") & "_ending=0 " _
                       & " where tb_year='" & dr("periode").ToString.Substring(0, 4) & "'"

                ssqls.Add(ssql)

            Next
            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If

            init_le(gcal_start, "gcal_mstr")
            init_le(gcal_end, "gcal_mstr")

            le_year_EditValueChanged(Nothing, Nothing)

            Box("Sukses")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtGenCFDirect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenCFDirect.Click
        Try
            If MessageBox.Show("Are you sure to generate this periode ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
            LblProses.Text = ""
            Windows.Forms.Application.DoEvents()

            Dim _waktu_awal As Date = Now

            ''cek periode yang belum closing, hanya periode yang belum closing saja yang akan di generate
            ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode,gcal_start_date,gcal_end_date  " _
                & "from gcal_mstr where  " _
                & "gcal_closing='N' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') between '" & gcal_start.GetColumnValue("periode") _
                & "' and '" & gcal_end.GetColumnValue("periode") & "' order by gcal_start_date "

            Dim dt_unclose_periode As New DataTable
            dt_unclose_periode = GetTableData(ssql)

            Dim _periode_start As String = ""
            Dim _periode_end As String = ""
            Dim _start_date As Date
            Dim _end_date As Date
            Dim z As Integer = 0

            For Each dr As DataRow In dt_unclose_periode.Rows
                If z = 0 Then
                    _periode_start = dr("periode")
                    _start_date = dr("gcal_start_date")
                    _periode_end = dr("periode")
                    _end_date = dr("gcal_end_date")
                Else
                    _periode_end = dr("periode")
                    _end_date = dr("gcal_end_date")
                End If
                z = z + 1
            Next


            'jika cutoff tapi periode start sudah close maka tidak boleh, 
            If CeCutOff.Checked Then
                If gcal_start.GetColumnValue("gcal_closing") = "Y" Then
                    Box("Period cut off has been closed")
                    Exit Sub
                End If

                If _periode_start <> gcal_start.GetColumnValue("periode") Then
                    Box("The initial period must not be closed")
                    Exit Sub
                End If
            End If

            'End If

            ''cek apakah ada periode yang masih open dibawah yang akan ditampilkan
            ''jika ada maka harus ikut dihitung
            ''jika tidak ada maka cukup hitung periode yg akan ditampilkan

        

            ssql = "SELECT  " _
                & "  a.code, " _
                & "  a.remark, " _
                & "  a.sort_number, " _
                & "  a.remark_header, " _
                & "  a.remark_footer, " _
                & "  a.cfsign_header, " _
                & "  a.cf_value_sign, " _
                & "  a.cf_type, " _
                & "  b.code as code_detail, " _
                & "  b.seq, " _
                & "  b.sub_header, " _
                & "  b.cfdet_pk, " _
                & "  b.cfdet_oid, " _
                & "  b.ac_value_header, " _
                & "  b.sub_header2, " _
                & "  b.""group"" " _
                & "FROM " _
                & "  public.tconfsettingcashflow a " _
                & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                & "WHERE " _
                & "  a.cf_type = 'D' and b.cfdet_pk not in  (select cf_cfdet_pk from cf_direct where cf_year='" & _periode_start.Substring(0, 4) & "')  " _
                & "  order by a.sort_number, seq"

            Dim dt_ac As New DataTable
            dt_ac = GetTableData(ssql)
            'Exit Sub

            Dim ssqls As New ArrayList

            For Each dr As DataRow In dt_ac.Rows
                

                ssql = "INSERT INTO  " _
                    & "  public.cf_direct " _
                    & "( " _
                    & "  cf_oid, " _
                    & "  cf_year,cf_cfdet_pk,cf_sort_number,cf_seq,cf_sub_header, " _
                    & "   " _
                    & "  cf_month_01, " _
                    & "  cf_month_02, " _
                    & "  cf_month_03, " _
                    & "  cf_month_04, " _
                    & "  cf_month_05, " _
                    & "  cf_month_06, " _
                    & "  cf_month_07, " _
                    & "  cf_month_08, " _
                    & "  cf_month_09, " _
                    & "  cf_month_10, " _
                    & "  cf_month_11, " _
                    & "  cf_month_12 " _
                    & ") " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & SetSetring(_periode_start.Substring(0, 4)) & ",  " _
                    & SetSetring(dr("cfdet_pk")) & ",  " _
                    & SetSetring(dr("sort_number")) & ",  " _
                    & SetSetring(dr("seq")) & ",  " _
                    & SetSetring(dr("sub_header")) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & "  " _
                    & ")"


                ssqls.Add(ssql)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If


            For Each dr As DataRow In dt_unclose_periode.Rows
                Dim _periode As Integer = dr("periode").ToString.Substring(4, 2)

                ssql = "update cf_direct set cf_month_" & _periode.ToString("00") & "=0" _
                       & " " _
                       & " where cf_year='" & dr("periode").ToString.Substring(0, 4) & "' "

                ssqls.Add(ssql)

            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If


            'If CeCutOff.Checked = True Then

            'Else

            'End If

            ssql = "select to_char(y.glt_date, 'YYYYMM') as glt_periode,  y.glt_ac_id,ac_mstr.ac_code,  " _
                    & "ac_mstr.ac_name,  " _
                    & "sum(y.glt_debit * y.glt_exc_rate) as glt_debit  , " _
                    & "sum(y.glt_credit * y.glt_exc_rate) as glt_credit ,'C' as position, rule.cfdet_pk, rule.ac_value " _
                    & "from glt_det y   " _
                    & "inner join ac_mstr on (y.glt_ac_id=ac_id)   " _
                    & "left outer join (SELECT   " _
                    & "                  d.ac_id , d.ac_code, d.ac_name, b.cfdet_pk, c.ac_sign, c.ac_value " _
                    & "                FROM  " _
                    & "                  public.tconfsettingcashflow a  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "                  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "                WHERE  " _
                    & "                  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D') as rule on (rule.ac_id=y.glt_ac_id and rule.ac_sign='C') " _
                    & "where y.glt_date between " & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " " _
                    & " and y.glt_code in (  select distinct x.glt_code from glt_det x where x.glt_date  between  " _
                    & "" & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " and x.glt_ac_id in (SELECT    d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                    & "  and (x.glt_debit + x.glt_credit) <>0)  " _
                    & "  and y.glt_ac_id not in   (SELECT   " _
                    & "  d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D') and y.glt_debit=0 " _
                    & "  group by glt_periode,glt_ac_id,ac_mstr.ac_code, ac_mstr.ac_name, rule.cfdet_pk,rule.ac_value " _
                    & "   " _
                    & "  UNION ALL " _
                    & "   " _
                    & "  select to_char(y.glt_date, 'YYYYMM') as glt_periode,  y.glt_ac_id,ac_mstr.ac_code,  " _
                    & "ac_mstr.ac_name,  " _
                    & "sum(y.glt_debit * y.glt_exc_rate) as glt_debit  , " _
                    & "sum(y.glt_credit * y.glt_exc_rate) as glt_credit ,'D' as position, rule.cfdet_pk, rule.ac_value " _
                    & "from glt_det y   " _
                    & "inner join ac_mstr on (y.glt_ac_id=ac_id)   " _
                    & "left outer join (SELECT   " _
                    & "                  d.ac_id , d.ac_code, d.ac_name, b.cfdet_pk, c.ac_sign, c.ac_value " _
                    & "                FROM  " _
                    & "                  public.tconfsettingcashflow a  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "                  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "                WHERE  " _
                    & "                  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D') as rule on (rule.ac_id=y.glt_ac_id and rule.ac_sign='D') " _
                    & "where y.glt_date between " & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " " _
                    & " and y.glt_code in (  select distinct x.glt_code from glt_det x where x.glt_date  between  " _
                    & "" & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " and x.glt_ac_id in (SELECT    d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                    & "  and (x.glt_debit + x.glt_credit) <>0)  " _
                    & "  and y.glt_ac_id not in   (SELECT   " _
                    & "  d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D') and y.glt_credit=0 " _
                    & "  group by glt_periode,glt_ac_id,ac_mstr.ac_code, ac_mstr.ac_name, rule.cfdet_pk,rule.ac_value " _
                    & "  order by glt_periode,cfdet_pk " _
                    & "   " _
                    & "  "

            Dim dt_jurnal As New DataTable
            dt_jurnal = GetTableData(ssql)

            ssql = "select '' as ac_code, '' as ac_name ,'' as sign,'' as transaction"

            Dim dt_hasil As New DataTable
            dt_hasil = GetTableData(ssql)

            dt_hasil.Rows.Clear()

            Dim _belum_seting As Boolean = False
            For Each dr As DataRow In dt_jurnal.Rows
                If SetString(dr("cfdet_pk")) = "" Then
                    _belum_seting = True

                    Dim dr_new As DataRow
                    dr_new = dt_hasil.NewRow

                    dr_new("ac_code") = dr("ac_code")
                    dr_new("ac_name") = dr("ac_name")
                    dr_new("sign") = dr("position")
                

                    'dr_new("transaction") = dr_cf("glt_code")

                    dt_hasil.Rows.Add(dr_new)
                    dt_hasil.AcceptChanges()
                End If
            Next

            If _belum_seting = True Then

                Dim frm As New frmExport
                With frm
                    .Text = "The account list has not been set"
                    add_column_edit(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                    add_column_edit(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
                    add_column_edit(.gv_export, "Sign", "sign", DevExpress.Utils.HorzAlignment.Default)
                    'add_column_edit(.gv_export, "Transaction", "transaction", DevExpress.Utils.HorzAlignment.Default)
                    .gc_export.DataSource = dt_hasil
                    .gv_export.BestFitColumns()
                End With

                frm.Show()

                'Exit Sub
            End If

            Dim _month As String = ""
            Dim _year As String = ""
            Dim _glt_periode As String = ""

            For Each dr As DataRow In dt_jurnal.Rows
                _month = dr("glt_periode").ToString.Substring(4, 2)
                _year = dr("glt_periode").ToString.Substring(0, 4)
                
                
                If dr("position").ToString = "C" And SetString(dr("cfdet_pk")) <> "" Then

                    ssql = "update cf_direct set cf_month_" & _month & " = cf_month_" & _month & " + " & SetDec(dr("glt_credit") * dr("ac_value")) _
                    & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk=" & SetSetring(dr("cfdet_pk"))

                    ssqls.Add(ssql)

                    ssql = "update cf_direct set cf_month_" & _month & " = cf_month_" & _month & " + " & SetDec(dr("glt_credit") * dr("ac_value")) _
                  & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_01'"

                    ssqls.Add(ssql)

                    '_total = _total + (dr("glt_credit") * dr("ac_value"))
                End If
                If dr("position").ToString = "D" And SetString(dr("cfdet_pk")) <> "" Then

                    ssql = "update cf_direct set cf_month_" & _month & " = cf_month_" & _month & " + " & SetDec(dr("glt_debit") * dr("ac_value")) _
                    & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk=" & SetSetring(dr("cfdet_pk"))


                    ssqls.Add(ssql)
                    ssql = "update cf_direct set cf_month_" & _month & " = cf_month_" & _month & " + " & SetDec(dr("glt_debit") * dr("ac_value")) _
                  & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_01'"

                    ssqls.Add(ssql)

                    '_total = _total + (dr("glt_debit") * dr("ac_value"))
                End If

                
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If


            Dim _total As Double = 0
            For Each dr As DataRow In dt_unclose_periode.Rows
                For Each dr2 As DataRow In dt_jurnal.Rows
                    If dr("periode") = dr2("glt_periode") And SetString(dr2("cfdet_pk")) <> "" Then
                        _total = _total + ((dr2("glt_debit") + dr2("glt_credit")) * dr2("ac_value"))
                    End If
                Next

                _month = dr("periode").ToString.Substring(4, 2)
                _year = dr("periode").ToString.Substring(0, 4)

                ssql = "select sum(tb_month_" & _month & "_opening) as opening, sum(tb_month_" & _month & "_ending) as ending  " _
                & "from tb_trial " _
                & "where tb_ac_id in (SELECT     d.ac_id   " _
                & "FROM    public.tconfsettingcashflow a     " _
                & "INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)     " _
                & "INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)     " _
                & "INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)   " _
                & "WHERE    a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                & "and tb_year=" & SetInteger(_year) & " "

                Dim dt_temp As New DataTable
                dt_temp = GetTableData(ssql)

                Dim _opening As Double = 0
                Dim _ending As Double = 0

                For Each dr2 As DataRow In dt_temp.Rows
                    _opening = dr2(0)
                    _ending = dr2(1)
                Next

                ssql = "update cf_direct set cf_month_" & _month & " = " & SetDec(_opening) _
                      & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_03'"

                ssqls.Add(ssql)

                ssql = "update cf_direct set cf_month_" & _month & " = " & SetDec(_ending) _
                      & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_04'"

                ssqls.Add(ssql)
                Dim _perubahan As Double = _ending - _opening
                Dim _balance_new As Double = _perubahan - _total

                ssql = "update cf_direct set cf_month_" & _month & " = " & SetDec(_balance_new) _
                      & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_02'"

                ssqls.Add(ssql)
                _total = 0

               
            Next


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                Else
                    '_sukses = True
                End If
                ssqls.Clear()
            End If

            'If _glt_periode <> dr("glt_periode") Then
            '    _glt_periode = dr("glt_periode")

            '    ssql = "select sum(tb_month_" & _month & "_opening) as opening, sum(tb_month_" & _month & "_ending) as ending  " _
            '    & "from tb_trial " _
            '    & "where tb_ac_id in (SELECT     d.ac_id   " _
            '    & "FROM    public.tconfsettingcashflow a     " _
            '    & "INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)     " _
            '    & "INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)     " _
            '    & "INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)   " _
            '    & "WHERE    a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
            '    & "and tb_year=" & SetInteger(_year) & " "

            '    Dim dt_temp As New DataTable
            '    dt_temp = GetTableData(ssql)

            '    Dim _opening As Double = 0
            '    Dim _ending As Double = 0

            '    For Each dr2 As DataRow In dt_temp.Rows
            '        _opening = dr2(0)
            '        _ending = dr2(1)
            '    Next

            '    ssql = "update cf_direct set cf_month_" & _month & " = " & SetDec(_opening) _
            '          & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_03'"

            '    ssqls.Add(ssql)

            '    ssql = "update cf_direct set cf_month_" & _month & " = " & SetDec(_ending) _
            '          & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_04'"

            '    ssqls.Add(ssql)

            '    Dim _balance As Double = _ending - _opening - _total

            '    ssql = "update cf_direct set cf_month_" & _month & " = " & SetDec(_balance) _
            '          & " where cf_year=" & SetInteger(_year) & " and cf_cfdet_pk='cfd_saldo_awal_02'"

            '    ssqls.Add(ssql)

            '    _total = 0
            'End If


            



            Dim _lama_proses As Integer = 0
            Dim _waktu_akhir As Date = Now
            _lama_proses = DateDiff(DateInterval.Second, _waktu_awal, _waktu_akhir) + 1
            LblProses.Text = "Lama proses : " & _lama_proses & " detik . Start : " & _waktu_awal.ToString("dd/MM/yyyy hh:mm:ss") & " to " & _waktu_akhir.ToString("dd/MM/yyyy hh:mm:ss")


            Box("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtDetailCF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDetailCF.Click
        Try
            ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode,gcal_start_date,gcal_end_date  " _
                & "from gcal_mstr where  " _
                & "gcal_closing='N' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') between '" & gcal_start.GetColumnValue("periode") _
                & "' and '" & gcal_end.GetColumnValue("periode") & "' order by gcal_start_date "

            Dim dt_unclose_periode As New DataTable
            dt_unclose_periode = GetTableData(ssql)

            Dim _periode_start As String = ""
            Dim _periode_end As String = ""
            Dim _start_date As Date
            Dim _end_date As Date
            Dim z As Integer = 0

            For Each dr As DataRow In dt_unclose_periode.Rows
                If z = 0 Then
                    _periode_start = dr("periode")
                    _start_date = dr("gcal_start_date")
                    _periode_end = dr("periode")
                    _end_date = dr("gcal_end_date")
                Else
                    _periode_end = dr("periode")
                    _end_date = dr("gcal_end_date")
                End If
                z = z + 1
            Next

            ssql = "select to_char(y.glt_date, 'YYYYMM') as glt_periode, y.glt_code, y.glt_ac_id,ac_mstr.ac_code,  " _
                    & "ac_mstr.ac_name,  " _
                    & "sum(y.glt_debit * y.glt_exc_rate) as glt_debit  , " _
                    & "sum(y.glt_credit * y.glt_exc_rate) as glt_credit ,'C' as position, rule.cfdet_pk, rule.ac_value " _
                    & "from glt_det y   " _
                    & "inner join ac_mstr on (y.glt_ac_id=ac_id)   " _
                    & "left outer join (SELECT   " _
                    & "                  d.ac_id , d.ac_code, d.ac_name, b.cfdet_pk, c.ac_sign, c.ac_value " _
                    & "                FROM  " _
                    & "                  public.tconfsettingcashflow a  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "                  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "                WHERE  " _
                    & "                  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D') as rule on (rule.ac_id=y.glt_ac_id and rule.ac_sign='C') " _
                    & "where y.glt_date between " & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " " _
                    & " and y.glt_code in (  select distinct x.glt_code from glt_det x where x.glt_date  between  " _
                    & "" & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " and x.glt_ac_id in (SELECT    d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                    & "  and (x.glt_debit + x.glt_credit) <>0)  " _
                    & "  and y.glt_ac_id not in   (SELECT   " _
                    & "  d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D') and y.glt_debit=0 " _
                    & "  group by glt_periode,glt_ac_id,ac_mstr.ac_code, ac_mstr.ac_name, rule.cfdet_pk,rule.ac_value,glt_code " _
                    & "   " _
                    & "  UNION ALL " _
                    & "   " _
                    & "  select to_char(y.glt_date, 'YYYYMM') as glt_periode, y.glt_code, y.glt_ac_id,ac_mstr.ac_code,  " _
                    & "ac_mstr.ac_name,  " _
                    & "sum(y.glt_debit * y.glt_exc_rate) as glt_debit  , " _
                    & "sum(y.glt_credit * y.glt_exc_rate) as glt_credit ,'D' as position, rule.cfdet_pk, rule.ac_value " _
                    & "from glt_det y   " _
                    & "inner join ac_mstr on (y.glt_ac_id=ac_id)   " _
                    & "left outer join (SELECT   " _
                    & "                  d.ac_id , d.ac_code, d.ac_name, b.cfdet_pk, c.ac_sign, c.ac_value " _
                    & "                FROM  " _
                    & "                  public.tconfsettingcashflow a  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "                  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "                WHERE  " _
                    & "                  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D') as rule on (rule.ac_id=y.glt_ac_id and rule.ac_sign='D') " _
                    & "where y.glt_date between " & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " " _
                    & " and y.glt_code in (  select distinct x.glt_code from glt_det x where x.glt_date  between  " _
                    & "" & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " and x.glt_ac_id in (SELECT    d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                    & "  and (x.glt_debit + x.glt_credit) <>0)  " _
                    & "  and y.glt_ac_id not in   (SELECT   " _
                    & "  d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D') and y.glt_credit=0 " _
                    & "  group by glt_periode,glt_ac_id,ac_mstr.ac_code, ac_mstr.ac_name, rule.cfdet_pk,rule.ac_value,glt_code " _
                    & "  order by glt_periode,cfdet_pk " _
                    & "   " _
                    & "  "


            Dim dt_hasil As New DataTable
            dt_hasil = GetTableData(ssql)

            Dim frm As New frmExport
            With frm
                'glt_periode
                .Text = "CASH FLOW DIRECT DETAIL"
                add_column_copy(.gv_export, "Periode", "glt_periode", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "GL Code", "glt_code", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

                add_column_copy(.gv_export, "Position", "position", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Group", "cfdet_pk", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Value", "ac_value", DevExpress.Utils.HorzAlignment.Default)



                .gc_export.DataSource = dt_hasil
                .gv_export.BestFitColumns()
            End With


            frm.Show()



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CFJournal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CFJournal.Click
        Try
            ssql = "select gcal_oid, TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') as periode,gcal_start_date,gcal_end_date  " _
                & "from gcal_mstr where  " _
                & "gcal_closing='N' and TO_CHAR(gcal_year, 'fm0000')  || TO_CHAR(gcal_periode, 'fm00') between '" & gcal_start.GetColumnValue("periode") _
                & "' and '" & gcal_end.GetColumnValue("periode") & "' order by gcal_start_date "

            Dim dt_unclose_periode As New DataTable
            dt_unclose_periode = GetTableData(ssql)

            Dim _periode_start As String = ""
            Dim _periode_end As String = ""
            Dim _start_date As Date
            Dim _end_date As Date
            Dim z As Integer = 0

            For Each dr As DataRow In dt_unclose_periode.Rows
                If z = 0 Then
                    _periode_start = dr("periode")
                    _start_date = dr("gcal_start_date")
                    _periode_end = dr("periode")
                    _end_date = dr("gcal_end_date")
                Else
                    _periode_end = dr("periode")
                    _end_date = dr("gcal_end_date")
                End If
                z = z + 1
            Next

            ssql = "select to_char(y.glt_date, 'YYYYMM') as glt_periode, y.glt_code, y.glt_ac_id,ac_mstr.ac_code,  " _
                    & "ac_mstr.ac_name,  " _
                    & "sum(y.glt_debit * y.glt_exc_rate) as glt_debit  , " _
                    & "sum(y.glt_credit * y.glt_exc_rate) as glt_credit ,'C' as position, rule.cfdet_pk, rule.ac_value " _
                    & "from glt_det y   " _
                    & "inner join ac_mstr on (y.glt_ac_id=ac_id)   " _
                    & "left outer join (SELECT   " _
                    & "                  d.ac_id , d.ac_code, d.ac_name, b.cfdet_pk, c.ac_sign, c.ac_value " _
                    & "                FROM  " _
                    & "                  public.tconfsettingcashflow a  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "                  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "                WHERE  " _
                    & "                  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D') as rule on (rule.ac_id=y.glt_ac_id and rule.ac_sign='C') " _
                    & "where y.glt_date between " & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " " _
                    & " and y.glt_code in (  select distinct x.glt_code from glt_det x where x.glt_date  between  " _
                    & "" & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " and x.glt_ac_id in (SELECT    d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                    & "  and (x.glt_debit + x.glt_credit) <>0)  " _
                    & "   and y.glt_debit=0 " _
                    & "  group by glt_periode,glt_ac_id,ac_mstr.ac_code, ac_mstr.ac_name, rule.cfdet_pk,rule.ac_value,glt_code " _
                    & "   " _
                    & "  UNION ALL " _
                    & "   " _
                    & "  select to_char(y.glt_date, 'YYYYMM') as glt_periode, y.glt_code, y.glt_ac_id,ac_mstr.ac_code,  " _
                    & "ac_mstr.ac_name,  " _
                    & "sum(y.glt_debit * y.glt_exc_rate) as glt_debit  , " _
                    & "sum(y.glt_credit * y.glt_exc_rate) as glt_credit ,'D' as position, rule.cfdet_pk, rule.ac_value " _
                    & "from glt_det y   " _
                    & "inner join ac_mstr on (y.glt_ac_id=ac_id)   " _
                    & "left outer join (SELECT   " _
                    & "                  d.ac_id , d.ac_code, d.ac_name, b.cfdet_pk, c.ac_sign, c.ac_value " _
                    & "                FROM  " _
                    & "                  public.tconfsettingcashflow a  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "                  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "                  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "                WHERE  " _
                    & "                  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='D') as rule on (rule.ac_id=y.glt_ac_id and rule.ac_sign='D') " _
                    & "where y.glt_date between " & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " " _
                    & " and y.glt_code in (  select distinct x.glt_code from glt_det x where x.glt_date  between  " _
                    & "" & SetDateNTime00(_start_date) & " and " & SetDateNTime00(_end_date) & " and x.glt_ac_id in (SELECT    d.ac_id  " _
                    & "FROM  " _
                    & "  public.tconfsettingcashflow a  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code)  " _
                    & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code)  " _
                    & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki)  " _
                    & "WHERE  " _
                    & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='D')  " _
                    & "  and (x.glt_debit + x.glt_credit) <>0)  " _
                    & "   and y.glt_credit=0 " _
                    & "  group by glt_periode,glt_ac_id,ac_mstr.ac_code, ac_mstr.ac_name, rule.cfdet_pk,rule.ac_value,glt_code " _
                    & "  order by glt_periode,glt_code, glt_credit" _
                    & "   " _
                    & "  "


            Dim dt_hasil As New DataTable
            dt_hasil = GetTableData(ssql)

            Dim frm As New frmExport
            With frm
                'glt_periode
                .Text = "CASH FLOW DIRECT DETAIL"
                add_column_copy(.gv_export, "Periode", "glt_periode", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "GL Code", "glt_code", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

                add_column_copy(.gv_export, "Position", "position", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Group", "cfdet_pk", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Value", "ac_value", DevExpress.Utils.HorzAlignment.Default)



                .gc_export.DataSource = dt_hasil
                .gv_export.BestFitColumns()

            End With

            For i As Integer = 0 To frm.gv_export.RowCount - 2

            Next

            frm.Show()



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtBalanceSheetReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtBalanceSheetReport.Click
        Try
            ssql = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
                       & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, y.ac_code, " _
                       & "  y.ac_name, " _
                       & "  y.ac_type," _
                       & "z.tb_month_01_ending, " _
                        & "  z.tb_month_02_ending, " _
                        & "  z.tb_month_03_ending, " _
                        & "  z.tb_month_04_ending, " _
                        & "  z.tb_month_05_ending, " _
                        & "  z.tb_month_06_ending, " _
                        & "  z.tb_month_07_ending, " _
                        & "  z.tb_month_08_ending, " _
                        & "  z.tb_month_09_ending, " _
                        & "  z.tb_month_10_ending, " _
                        & "  z.tb_month_11_ending, " _
                        & "  z.tb_month_12_ending " _
                       & "FROM " _
                       & "  public.bs_mstr a " _
                       & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
                       & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) " _
                       & "  INNER JOIN public.bsda_account x ON (x.bsda_bsdi_oid = c.bsdi_oid) " _
                       & "  INNER JOIN public.ac_mstr y ON (substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki)  " _
                       & "   inner join tb_trial z on z.tb_ac_id=y.ac_id " _
                       & " Where y.ac_is_sumlevel='N' and tb_year=" & SetInteger(le_year.EditValue) & ""


            Dim rpt As New rptBalanceSheetDetailReportNew
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
                'System.Windows.Forms.Application.DoEvents()

                'If Ce_Posting.EditValue = True Then
                '    .Posting_Option = True
                'Else
                '    .Posting_Option = False
                'End If

                ssql = "select * from dom_mstr where dom_company <> ''"

                Dim dt As New DataTable
                dt = GetTableData(ssql)

                For Each dr As DataRow In dt.Rows
                    .XrLabelTitle.Text = dr("dom_company")
                Next

                .periode = le_year.EditValue 'de_end.DateTime.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"


                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Balance Sheet Report"
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Balance Sheet Report"
                printTool.ShowPreview()

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtPLReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPLReport.Click
        Try
            ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, y.ac_type, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                       & "z.tb_month_01_ending as v_nilai1, " _
                        & "  z.tb_month_02_ending as v_nilai2, " _
                        & "  z.tb_month_03_ending as v_nilai3, " _
                        & "  z.tb_month_04_ending as v_nilai4, " _
                        & "  z.tb_month_05_ending as v_nilai5, " _
                        & "  z.tb_month_06_ending as v_nilai6, " _
                        & "  z.tb_month_07_ending as v_nilai7, " _
                        & "  z.tb_month_08_ending as v_nilai8, " _
                        & "  z.tb_month_09_ending as v_nilai9, " _
                        & "  z.tb_month_10_ending as v_nilai10, " _
                        & "  z.tb_month_11_ending as v_nilai11, " _
                        & "  z.tb_month_12_ending as v_nilai12, " _
                    & "  c.pla_ac_hirarki, case when y.ac_type='E' then -1.0 else 1.0 end as value,  case when y.ac_type='E' then z.tb_month_01_ending * -1.0 else z.tb_month_01_ending end as v_nilai1b " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & "   left outer join tb_trial z on z.tb_ac_id=y.ac_id " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "



            Dim rpt As New rptProfitLossDetailReportNew
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



                ssql = "select * from dom_mstr where dom_company <> ''"

                Dim dt As New DataTable
                dt = GetTableData(ssql)

                For Each dr As DataRow In dt.Rows
                    .XrLabelTitle.Text = dr("dom_company")
                Next

                'If Ce_Posting.EditValue = True Then
                '    .Posting_Option = True
                'Else
                '    .Posting_Option = False
                'End Ifs

                .periode = le_year.EditValue ' de_end.DateTime.ToString("dd MMMM yyyy")
                .DataSource = ds
                .DataMember = "Table"
                '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
                '.Parameters("PPosisi").Value = posisi

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Profit And Loss Report"
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Profit And Loss Report"
                printTool.ShowPreview()

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
