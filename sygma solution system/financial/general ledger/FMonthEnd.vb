Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FMonthEnd
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    Private Sub FMonthEnd_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        ssql = "select count(*) as jml from conf_file where conf_name='rec_gl_bal' and conf_value='1'"
        Try
            If GetRowInfo(ssql)(0) = 0 Then
                btRecountGLBal.Visible = False
                CeCalcOpen.Visible = False
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
        
    End Sub

    Public Overrides Sub load_cb()
       
        init_le(le_entity, "en_mstr")
        init_le(le_glperiode, "gcal_mstr")
    End Sub

    Private Function before_close() As Boolean
        before_close = True

        Dim _jml As Double = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    '.Command.CommandText = "select count(glt_posted) as jml from glt_det where glt_posted ~~* 'N' " + _
                    '                       " and glt_en_id = " + le_entity.EditValue.ToString + _
                    '                       " and glt_date <= '" + le_glperiode.GetColumnValue("gcal_end_date") + "'"

                    .Command.CommandText = "select sum(glbal_balance_unposted) as jml from glbal_balance  " _
                                           & " Where glbal_gcal_oid = '" & le_glperiode.EditValue & "'"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader("jml")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        ssql = "select count(glt_posted) as jml from glt_det where glt_posted ~~* 'N' and " + _
                                " glt_date between " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_start_date")) & " and " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date"))

        Dim _jml2 As Double = GetRowInfo(ssql)(0)

        If _jml <> 0 Or _jml2 <> 0 Then
            MessageBox.Show("Unposted Transaction Still Exist.." + Chr(13) + "Please Transaction Post To All Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If


        'ssql = "select * from glt_det where glt_date between " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_start_date")) _
        '    & " and " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & " and glt_posted='N'"

        Return before_close
    End Function

    Private Sub sb_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MessageBox.Show("Close Transaction For This Periode...", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        'pengecekan jurnal yang belum posting
        If before_close() = False Then
            'Exit Sub
            If DevExpress.XtraEditors.XtraMessageBox.Show("Do you want to continue?", "Confirmation ...", _
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
        End If

        Cursor = Cursors.WaitCursor
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Dim ssqls As New ArrayList

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  glbal_oid, " _
                        & "  glbal_dom_id, " _
                        & "  glbal_en_id, " _
                        & "  glbal_add_by, " _
                        & "  glbal_add_date, " _
                        & "  glbal_upd_by, " _
                        & "  glbal_upd_date, " _
                        & "  glbal_gcal_oid, " _
                        & "  glbal_ac_id, " _
                        & "  glbal_sb_id, " _
                        & "  glbal_cc_id, " _
                        & "  glbal_cu_id, " _
                        & "  glbal_balance_open, " _
                        & "  coalesce(glbal_balance_unposted,0) as glbal_balance_unposted , " _
                        & "  coalesce(glbal_balance_posted,0) as glbal_balance_posted, " _
                        & "  glbal_dt " _
                        & "FROM  " _
                        & "  public.glbal_balance" _
                        & " where glbal_gcal_oid = '" + le_glperiode.EditValue.ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Mencari gl calendar berikutnya ********************************
        Dim _next_date As Date
        Dim _gcal_oid As String = ""
        _next_date = le_glperiode.GetColumnValue("gcal_end_date")
        _next_date = _next_date.AddDays(1)

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_oid from gcal_mstr " + _
                                           " where gcal_start_date <='" + _next_date + "'" + _
                                           " and gcal_end_date >='" + _next_date + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _gcal_oid = .DataReader("gcal_oid").ToString
                        End While
                    Else
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _next_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        '***************************************************************

        Dim _glbal_oid As String = ""

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
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
                                            & "WHERE gcald_gcal_oid = '" & le_glperiode.EditValue.ToString & "' " _
                                            & " and gcald_en_id = " & le_entity.EditValue.ToString

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .Connection.Open()
                                        .Command = .Connection.CreateCommand
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                                               " where glbal_en_id = " + le_entity.EditValue.ToString + _
                                                               " and glbal_ac_id = " + ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id").ToString + _
                                                               " and glbal_sb_id = " + ds_bantu.Tables(0).Rows(i).Item("glbal_sb_id").ToString + _
                                                               " and glbal_cc_id = " + ds_bantu.Tables(0).Rows(i).Item("glbal_cc_id").ToString + _
                                                               " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader
                                        If .DataReader.HasRows Then
                                            While .DataReader.Read
                                                _glbal_oid = .DataReader("glbal_oid").ToString
                                            End While
                                        Else
                                            _glbal_oid = ""
                                        End If

                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                sqlTran.Rollback()
                                Exit Sub
                            End Try

                            If _glbal_oid = "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.glbal_balance " _
                                                    & "( " _
                                                    & "  glbal_oid, " _
                                                    & "  glbal_dom_id, " _
                                                    & "  glbal_en_id, " _
                                                    & "  glbal_add_by, " _
                                                    & "  glbal_add_date, " _
                                                    & "  glbal_gcal_oid, " _
                                                    & "  glbal_ac_id, " _
                                                    & "  glbal_sb_id, " _
                                                    & "  glbal_cc_id, " _
                                                    & "  glbal_cu_id, " _
                                                    & "  glbal_balance_open, " _
                                                    & "  glbal_balance_unposted, " _
                                                    & "  glbal_balance_posted, " _
                                                    & "  glbal_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_gcal_oid.ToString) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_sb_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_cc_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_cu_id")) & ",  " _
                                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glbal_balance_open") + ds_bantu.Tables(0).Rows(i).Item("glbal_balance_posted")) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & " public.glbal_balance " _
                                                    & "SET  " _
                                                    & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                    & "  glbal_balance_open = " & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glbal_balance_open") + ds_bantu.Tables(0).Rows(i).Item("glbal_balance_posted")) & ", " _
                                                    & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        MessageBox.Show("Transaction This Periode Have Been Closed..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Cursor = Cursors.Arrow
    End Sub

    Private Sub BtPraClosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPraClosing.Click
        Try

            Dim ssqls As New ArrayList
            ssql = "SELECT  " _
                    & "  a.gcal_pra_closing,gcal_closing " _
                    & "FROM " _
                    & "  public.gcal_mstr a " _
                    & "WHERE gcal_oid='" & le_glperiode.EditValue & "'"

            Dim dt As New DataTable

            dt = GetTableData(ssql)

            Dim _status As String = ""

            For Each dr As DataRow In dt.Rows
                _status = SetString(dr("gcal_pra_closing"))
            Next

            If _status = "Y" Then
                Box("This periode have been processed")
                Exit Sub
            End If



            '===============recalculate====================
            LblProses.Text = "Status : Pra Closing, Recalculate"
            System.Windows.Forms.Application.DoEvents()

            ssql = "UPDATE  " _
                 & "  public.glbal_balance   " _
                 & "SET  " _
                 & "  glbal_balance_unposted = 0,  " _
                  & "  glbal_balance_posted_end_month = 0,  " _
                 & "  glbal_balance_posted = 0,glbal_balance_end_month1=0, glbal_balance_trial=0  " _
                 & "WHERE  " _
                 & "  glbal_gcal_oid = " & SetSetring(le_glperiode.EditValue)

            ssqls.Add(ssql)

            ssql = " select glt_ac_id,glt_en_id,sum(unpost) as unpost_all,sum(posted) as posted_all from ( " _
                & " SELECT " _
                & "a.glt_ac_id,a.glt_en_id, " _
                & " sum(0) as unpost, sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit) * a.glt_exc_rate) as posted  " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_date BETWEEN " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_start_date")) & " AND " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & " and a.glt_posted='Y' " _
                & " group by a.glt_ac_id,a.glt_en_id " _
                & " union all  " _
                & "  SELECT " _
                & "a.glt_ac_id,a.glt_en_id, " _
                & "  sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit)  * a.glt_exc_rate) as unpost, sum(0) as posted  " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_date BETWEEN " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_start_date")) & " AND " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & " and a.glt_posted='N' " _
                & " group by a.glt_ac_id,a.glt_en_id) as temp " _
                & " group by glt_ac_id,glt_en_id"

            'Dim dt As New DataTable
            dt = GetTableData(ssql)


            For Each dr As DataRow In dt.Rows

                ssql = "UPDATE  " _
                  & "  public.glbal_balance   " _
                  & "SET  " _
                  & "  glbal_balance_unposted = " & SetDec(dr("unpost_all")) & ",  " _
                  & "  glbal_balance_posted = " & SetDec(dr("posted_all")) & "  " _
                  & "WHERE  " _
                  & "  glbal_gcal_oid = " & SetSetring(le_glperiode.EditValue) & " and  " _
                  & "  glbal_en_id = " & dr("glt_en_id") & " and " _
                  & "  glbal_ac_id = " & dr("glt_ac_id") & " "

                ssqls.Add(ssql)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If



            ''step 1
            'If le_entity.EditValue = 0 Then
            '    ssql = "select en_id,en_desc from en_mstr where en_id<>0 and en_active='Y' order by en_id "
            'Else
            '    ssql = "select en_id,en_desc from en_mstr where en_id=" & le_entity.EditValue & " order by en_id "
            '    If ce_all_child.EditValue = True Then
            '        ssql = "select en_id,en_code,en_desc from en_mstr where (en_parent=" & le_entity.EditValue _
            '            & " or en_id=" & le_entity.EditValue & ") and en_active='Y' order by en_code"
            '    End If
            'End If

            'Dim dt As New DataTable
            'dt = GetTableData(ssql)

            ''pemindahan dr biaya operasional ke kepala 5
            'For Each dr As DataRow In dt.Rows
            '    If move_biaya_operasional(ssqls, dr("en_id"), 1) = False Then
            '        Exit Sub
            '    End If
            'Next

            If before_close() = False Then
                Exit Sub
            End If
            LblProses.Text = "Status : Pra Closing, Copy Direct Clash Flow"
            System.Windows.Forms.Application.DoEvents()
            ssql = "update glbal_balance set glbal_balance_end_month1=coalesce(glbal_balance_posted,0) " _
               & "where glbal_gcal_oid='" & le_glperiode.EditValue & "' and glbal_ac_id in " _
               & "(select ac_id from ac_mstr where substring(ac_code_hirarki,1,2) in ( " _
                & "SELECT  " _
                & "  public.ac_mstr.ac_code_hirarki " _
                & "FROM " _
                & "  public.ac_mstr " _
                & "  INNER JOIN public.tconfsettingendmoth a ON (public.ac_mstr.ac_id = a.end_ac_id_from) " _
                & "WHERE " _
                & "  a.end_step = 1)) "

            ssqls.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            'Box("Copy Direct Cash Flow succes")
            System.Windows.Forms.Application.DoEvents()

            'stop sama pa arif 29/11/13
            ' ''If func_coll.get_conf_file("move_pra_closing") = "1" Then

            ' ''    LblProses.Text = "Status : Pra Closing, Step 1"
            ' ''    System.Windows.Forms.Application.DoEvents()
            ' ''    If move_biaya_operasional(ssqls, 1) = False Then
            ' ''        Exit Sub
            ' ''    End If

            ' ''    'Exit Sub
            ' ''    'If ask("Anda yakin akan memproses step 1 (pemindahan kepala 6 ke 51)?", "Konfirmasi...") Then

            ' ''    If master_new.PGSqlConn.status_sync = True Then

            ' ''        If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
            ' ''            'Return False
            ' ''            Exit Sub
            ' ''        End If
            ' ''        'Return False
            ' ''        ' Exit Sub

            ' ''        ssqls.Clear()
            ' ''    Else
            ' ''        If DbRunTran(ssqls, "") = False Then
            ' ''            'Return False
            ' ''            Exit Sub
            ' ''        End If
            ' ''        ssqls.Clear()
            ' ''    End If
            ' ''    'Box("Proses Step 1 sucess")

            ' ''    'Else
            ' ''    'Exit Sub
            ' ''    'End If


            ' ''    ''pemindahan dr kepala 51 ke 1012001 (wip)
            ' ''    'For Each dr As DataRow In dt.Rows
            ' ''    '    If move_biaya_operasional(ssqls, dr("en_id"), 2) = False Then
            ' ''    '        Exit Sub
            ' ''    '    End If
            ' ''    'Next

            ' ''    LblProses.Text = "Status : Pra Closing, Step 2"
            ' ''    System.Windows.Forms.Application.DoEvents()
            ' ''    'pemindahan dr kepala 51 ke 1012001 (wip)
            ' ''    If move_biaya_operasional(ssqls, 2) = False Then
            ' ''        Exit Sub
            ' ''    End If

            ' ''End If

            ssql = "update public.gcal_mstr set  gcal_pra_closing='Y' where gcal_oid=" & SetSetring(le_glperiode.EditValue)

            ssqls.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If

            LblProses.Text = "Status : Pra Closing, Step 2 Success. Pra Closing finish"
            System.Windows.Forms.Application.DoEvents()
            Box("Pra Closing, Step 2 Success. Pra Closing finish")


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Function move_biaya_operasional(ByVal _ssqls As ArrayList, ByVal _en As String, ByVal _step As Integer) As Boolean
        Try
            Dim _level As Integer = 2
            Dim _dom As Integer = 1
            Dim _glt_code As String

            ssql = "SELECT  " _
            & "  a.end_oid, " _
            & "  a.end_ac_id_from, " _
            & "  b.ac_code AS ac_code_from, " _
            & "  b.ac_name AS ac_name_from, " _
            & "  b.ac_code_hirarki AS ac_code_hirarki_from, " _
            & "  a.end_ac_id_to, " _
            & "  c.ac_code AS ac_code_to, " _
            & "  c.ac_name AS ac_name_to, " _
            & "  c.ac_code_hirarki AS ac_code_hirarki_to, " _
            & "  a.end_step, " _
            & "  a.end_seq, " _
            & "  a.end_remark, " _
            & "  b.ac_sign AS ac_sign_from, " _
            & "  b.ac_cu_id AS ac_cu_id_from, " _
            & "  c.ac_cu_id AS ac_cu_id_to, " _
            & "  c.ac_sign AS ac_sign_to " _
            & "FROM " _
            & "  public.tconfsettingendmoth a " _
            & "  INNER JOIN public.ac_mstr b ON (a.end_ac_id_from = b.ac_id) " _
            & "  INNER JOIN public.ac_mstr c ON (a.end_ac_id_to = c.ac_id) " _
            & "ORDER BY " _
            & "  a.end_step, " _
            & "  a.end_seq"

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            Dim _en_code As String
            _en_code = GetRowInfo("select en_code from en_mstr where en_id=" & _en)(0)

            _glt_code = func_coll.get_transaction_number("JP", _en_code, "glt_det", "glt_code")

            For Each dr As DataRow In dt.Rows
                If dr("end_step") = _step Then
                    'pemindahan dari 6 ke 51
                    ssql = "SELECT  " _
                        & "  a.ac_id, " _
                        & "  a.ac_code, " _
                        & "  a.ac_name, " _
                        & "  a.ac_sign, " _
                        & "  a.ac_level, " _
                        & "  a.ac_code_hirarki,a.ac_cu_id, " _
                        & "  a.ac_is_sumlevel,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                        & ", " & _en & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                        & "FROM " _
                        & "  public.ac_mstr a " _
                        & "  where substring(a.ac_code_hirarki,1," & Len(dr("ac_code_hirarki_from")) & ") ='" _
                        & dr("ac_code_hirarki_from") & "' and a.ac_is_sumlevel='N' and a.ac_active='Y'"

                    Dim dt_step_1 As New DataTable
                    dt_step_1 = GetTableData(ssql)


                    Dim i As Integer = 0
                    For Each dr_step_1 As DataRow In dt_step_1.Rows
                        'lakukan jurnal
                        If dr_step_1("ac_value") <> 0 Then

                            'insert debet (akun tujuan)
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(_en) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JP") & ",  " _
                             & SetInteger(dr("ac_cu_id_to")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr("end_ac_id_to")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment" & " " & dr("ac_name_to") & " " & GetIDByName("en_mstr", "en_desc", "en_id", _en)) & ",  " _
                             & SetDbl(dr_step_1("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            _ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(_ssqls, le_glperiode.EditValue, _en, dr("end_ac_id_to"), "D", dr_step_1("ac_value"), _
                                             dr("ac_cu_id_to"), func_data.get_exchange_rate(dr("ac_cu_id_to"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then
                                Return False
                                Exit Function
                            End If

                            'insert kredit
                            ssql = "INSERT INTO  " _
                              & "  public.glt_det " _
                              & "( " _
                              & "  glt_oid, " _
                              & "  glt_dom_id, " _
                              & "  glt_en_id, " _
                              & "  glt_add_by, " _
                              & "  glt_add_date, " _
                              & "  glt_code, " _
                              & "  glt_date, " _
                              & "  glt_type, " _
                              & "  glt_cu_id, " _
                              & "  glt_exc_rate, " _
                              & "  glt_seq, " _
                              & "  glt_ac_id, " _
                              & "  glt_sb_id, " _
                              & "  glt_cc_id, " _
                              & "  glt_desc, " _
                              & "  glt_debit, " _
                              & "  glt_credit, " _
                              & "  glt_ref_trans_code, " _
                              & "  glt_daybook, " _
                              & "  glt_posted, " _
                              & "  glt_dt " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                              & SetInteger(_en) & ",  " _
                              & SetSetring(master_new.ClsVar.sNama) & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                              & SetSetring(_glt_code) & ",  " _
                              & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                              & SetSetring("JP") & ",  " _
                              & SetInteger(dr_step_1("ac_cu_id")) & ",  " _
                              & SetDbl(1) & ",  " _
                              & SetInteger(i + 1) & ",  " _
                              & SetIntegerDB(dr_step_1("ac_id")) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetSetringDB("End Month Adjusment" & " " & dr_step_1("ac_name") & " " & GetIDByName("en_mstr", "en_desc", "en_id", _en)) & ",  " _
                              & SetDbl(0) & ",  " _
                              & SetDbl(dr_step_1("ac_value")) & ",  " _
                              & SetSetring("") & ",  " _
                              & SetSetring("MONTH END") & ",  " _
                              & SetSetring("Y") & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                              & ")"

                            _ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(_ssqls, le_glperiode.EditValue, _en, dr_step_1("ac_id"), "C", dr_step_1("ac_value"), _
                                             dr_step_1("ac_cu_id"), func_data.get_exchange_rate(dr_step_1("ac_cu_id"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then
                                Return False
                                Exit Function
                            End If

                            i += 2
                        End If

                        ' _glt_code = Microsoft.VisualBasic.Left(_glt_code, Len(_glt_code) - 5) & Format(CInt(Microsoft.VisualBasic.Right(_glt_code, 5)) + 1, "00000")
                    Next
                End If
            Next
            Return True

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function
    Function move_biaya_operasional(ByVal _ssqls As ArrayList, ByVal _step As Integer) As Boolean
        Try
            Dim _en As Integer = 0
            Dim _level As Integer = 1
            Dim _dom As Integer = 1
            Dim _glt_code As String

            ssql = "SELECT  " _
                & "  a.end_oid, " _
                & "  a.end_ac_id_from, " _
                & "  b.ac_code AS ac_code_from, " _
                & "  b.ac_name AS ac_name_from, " _
                & "  b.ac_code_hirarki AS ac_code_hirarki_from, " _
                & "  a.end_ac_id_to, " _
                & "  c.ac_code AS ac_code_to, " _
                & "  c.ac_name AS ac_name_to, " _
                & "  c.ac_code_hirarki AS ac_code_hirarki_to, " _
                & "  a.end_step, " _
                & "  a.end_seq, " _
                & "  a.end_remark, " _
                & "  b.ac_sign AS ac_sign_from, " _
                & "  b.ac_cu_id AS ac_cu_id_from, " _
                & "  c.ac_cu_id AS ac_cu_id_to, " _
                & "  c.ac_sign AS ac_sign_to " _
                & "FROM " _
                & "  public.tconfsettingendmoth a " _
                & "  INNER JOIN public.ac_mstr b ON (a.end_ac_id_from = b.ac_id) " _
                & "  INNER JOIN public.ac_mstr c ON (a.end_ac_id_to = c.ac_id) " _
                & "ORDER BY " _
                & "  a.end_step, " _
                & "  a.end_seq"

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            'Return False
            'Exit Function

            Dim _en_code As String
            _en_code = GetRowInfo("select en_id,en_code from en_mstr where en_parent is null and en_id>0")(1)
            _en = GetRowInfo("select en_id,en_code from en_mstr where en_parent is null and en_id>0")(0)
            _glt_code = func_coll.get_transaction_number("JE", _en_code, "glt_det", "glt_code")
            Dim x As Integer = 0
            For Each dr As DataRow In dt.Rows
                If dr("end_step") = _step Then
                    'pemindahan dari 6 ke 51
                    ssql = "SELECT  " _
                        & "  a.ac_id, " _
                        & "  a.ac_code, " _
                        & "  a.ac_name, " _
                        & "  a.ac_sign, " _
                        & "  a.ac_level, " _
                        & "  a.ac_code_hirarki,a.ac_cu_id, " _
                        & "  a.ac_is_sumlevel,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                        & ", " & _en & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                        & "FROM " _
                        & "  public.ac_mstr a " _
                        & "  where substring(a.ac_code_hirarki,1," & Len(dr("ac_code_hirarki_from")) & ") ='" _
                        & dr("ac_code_hirarki_from") & "' and a.ac_is_sumlevel='N' and a.ac_active='Y'"

                    Dim dt_step_1 As New DataTable
                    dt_step_1 = GetTableData(ssql)
                    'Return False
                    'Exit Function

                    Dim i As Integer = 0
                    For Each dr_step_1 As DataRow In dt_step_1.Rows
                        'lakukan jurnal
                        If dr_step_1("ac_value") <> 0 Then

                            'insert debet (akun tujuan)
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(_en) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JE") & ",  " _
                             & SetInteger(dr("ac_cu_id_to")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr("end_ac_id_to")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 1, " & _step & " " & dr("end_seq") & " " & Format(i, "000") & " D " & Microsoft.VisualBasic.Left(dr("ac_name_to").ToString, 26) & " " & GetIDByName("en_mstr", "en_desc", "en_id", _en)) & ",  " _
                             & SetDbl(dr_step_1("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            _ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(_ssqls, le_glperiode.EditValue, _en, dr("end_ac_id_to"), "D", dr_step_1("ac_value"), _
                                             dr("ac_cu_id_to"), 1) = False Then
                                Return False
                                Exit Function
                            End If

                            'insert kredit
                            ssql = "INSERT INTO  " _
                              & "  public.glt_det " _
                              & "( " _
                              & "  glt_oid, " _
                              & "  glt_dom_id, " _
                              & "  glt_en_id, " _
                              & "  glt_add_by, " _
                              & "  glt_add_date, " _
                              & "  glt_code, " _
                              & "  glt_date, " _
                              & "  glt_type, " _
                              & "  glt_cu_id, " _
                              & "  glt_exc_rate, " _
                              & "  glt_seq, " _
                              & "  glt_ac_id, " _
                              & "  glt_sb_id, " _
                              & "  glt_cc_id, " _
                              & "  glt_desc, " _
                              & "  glt_debit, " _
                              & "  glt_credit, " _
                              & "  glt_ref_trans_code, " _
                              & "  glt_daybook, " _
                              & "  glt_posted, " _
                              & "  glt_dt " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                              & SetInteger(_en) & ",  " _
                              & SetSetring(master_new.ClsVar.sNama) & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                              & SetSetring(_glt_code) & ",  " _
                              & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                              & SetSetring("JE") & ",  " _
                              & SetInteger(dr_step_1("ac_cu_id")) & ",  " _
                              & SetDbl(1) & ",  " _
                              & SetInteger(i + 1) & ",  " _
                              & SetIntegerDB(dr_step_1("ac_id")) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetSetringDB("End Month Adjusment 1, " & _step & " " & dr("end_seq") & " " & Format(i, "000") & " K " & Microsoft.VisualBasic.Left(dr_step_1("ac_name"), 26) & " " & GetIDByName("en_mstr", "en_desc", "en_id", _en)) & ",  " _
                              & SetDbl(0) & ",  " _
                              & SetDbl(dr_step_1("ac_value")) & ",  " _
                              & SetSetring("") & ",  " _
                              & SetSetring("MONTH END") & ",  " _
                              & SetSetring("Y") & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                              & ")"

                            _ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(_ssqls, le_glperiode.EditValue, _en, dr_step_1("ac_id"), "C", dr_step_1("ac_value"), _
                                             dr_step_1("ac_cu_id"), 1) = False Then
                                Return False
                                Exit Function
                            End If

                            i += 1
                        End If
                    Next
                End If
                x += 1
            Next
            Return True

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Function update_gl_bal(ByVal _ssqls As ArrayList, ByVal _gcal_oid As String, ByVal _en_id As String, _
                           ByVal _ac_id As String, ByVal _par_sign As String, ByVal _glt_value As Double, _
                           ByVal _cu_id As String, ByVal _exc_rate As Double) As Boolean
        Try
            ssql = "select ac_sign, ac_cu_id from ac_mstr where ac_id = " + _ac_id
            Dim _ac_cu_id As Integer = GetRowInfo(ssql)(1)


            If _par_sign <> GetRowInfo(ssql)(0) Then
                _glt_value = _glt_value * -1
            End If

            _glt_value = _glt_value * _exc_rate

            ssql = "select glbal_oid from glbal_balance" + _
                   " where glbal_en_id = " + _en_id + _
                   " and glbal_ac_id = " + _ac_id + _
                   " and glbal_gcal_oid = '" + _gcal_oid + "'"

            Dim dt_glbal As New DataTable
            dt_glbal = GetTableData(ssql)

            If dt_glbal.Rows.Count = 0 Then
                Dim _glbal_oid As String = Guid.NewGuid.ToString
                ssql = "INSERT INTO  " _
                    & "  public.glbal_balance " _
                    & "( " _
                    & "  glbal_oid, " _
                    & "  glbal_dom_id, " _
                    & "  glbal_en_id, " _
                    & "  glbal_add_by, " _
                    & "  glbal_add_date, " _
                    & "  glbal_gcal_oid, " _
                    & "  glbal_ac_id, " _
                    & "  glbal_sb_id, " _
                    & "  glbal_cc_id, " _
                    & "  glbal_cu_id, " _
                    & "  glbal_balance_open, " _
                    & "  glbal_balance_unposted, " _
                    & "  glbal_balance_posted, " _
                    & "  glbal_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_glbal_oid) & ",  " _
                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    & SetInteger(_en_id) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    & SetSetring(_gcal_oid) & ",  " _
                    & SetInteger(_ac_id) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(_cu_id) & ",  " _
                    & SetDbl(0) & ",  " _
                    & SetDbl(0) & ",  " _
                    & SetDbl(0) & ",  " _
                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                    & ")"

                _ssqls.Add(ssql)

                ssql = "UPDATE  " _
                  & "  public.glbal_balance   " _
                  & "SET  " _
                  & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                  & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "," _
                  & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(_glt_value) & ",  " _
                  & "  glbal_dt =   " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                  & "  " _
                  & "WHERE  " _
                  & "  glbal_oid = " & SetSetring(_glbal_oid) & " "

                _ssqls.Add(ssql)

            Else
                ssql = "UPDATE  " _
                   & "  public.glbal_balance   " _
                   & "SET  " _
                   & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                   & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "," _
                   & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(_glt_value) & ",  " _
                   & "  glbal_dt =   " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                   & "  " _
                   & "WHERE  " _
                   & "  glbal_oid = " & SetSetring(dt_glbal.Rows(0).Item(0)) & " "

                _ssqls.Add(ssql)
            End If


            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Function update_gl_bal(ByVal _ssqls As ArrayList, ByVal _gcal_oid As String, ByVal _en_id As String, _
                           ByVal _ac_id As String, ByVal _par_sign As String, ByVal _glt_value As Double, _
                           ByVal _cu_id As String, ByVal _exc_rate As Double, ByVal _par_copy As Boolean) As Boolean
        Try
            ssql = "select ac_sign, ac_cu_id from ac_mstr where ac_id = " + _ac_id
            Dim _ac_cu_id As Integer = GetRowInfo(ssql)(1)


            If _par_sign <> GetRowInfo(ssql)(0) Then
                _glt_value = _glt_value * -1
            End If

            _glt_value = _glt_value * _exc_rate

            ssql = "select glbal_oid from glbal_balance" + _
                   " where glbal_en_id = " + _en_id + _
                   " and glbal_ac_id = " + _ac_id + _
                   " and glbal_gcal_oid = '" + _gcal_oid + "'"

            Dim dt_glbal As New DataTable
            dt_glbal = GetTableData(ssql)

            If dt_glbal.Rows.Count = 0 Then
                Dim _glbal_oid As String = Guid.NewGuid.ToString
                ssql = "INSERT INTO  " _
                    & "  public.glbal_balance " _
                    & "( " _
                    & "  glbal_oid, " _
                    & "  glbal_dom_id, " _
                    & "  glbal_en_id, " _
                    & "  glbal_add_by, " _
                    & "  glbal_add_date, " _
                    & "  glbal_gcal_oid, " _
                    & "  glbal_ac_id, " _
                    & "  glbal_sb_id, " _
                    & "  glbal_cc_id, " _
                    & "  glbal_cu_id, " _
                    & "  glbal_balance_open, " _
                    & "  glbal_balance_unposted, " _
                    & "  glbal_balance_posted, " _
                    & "  glbal_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_glbal_oid) & ",  " _
                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    & SetInteger(_en_id) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    & SetSetring(_gcal_oid) & ",  " _
                    & SetInteger(_ac_id) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(_cu_id) & ",  " _
                    & SetDbl(0) & ",  " _
                    & SetDbl(0) & ",  " _
                    & SetDbl(0) & ",  " _
                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                    & ")"

                _ssqls.Add(ssql)

                ssql = "UPDATE  " _
                  & "  public.glbal_balance   " _
                  & "SET  " _
                  & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                  & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "," _
                  & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(_glt_value) & ",  " _
                  & "  glbal_dt =   " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                  & "  " _
                  & "WHERE  " _
                  & "  glbal_oid = " & SetSetring(_glbal_oid) & " "

                '_ssqls.Add(ssql)

            Else
                ssql = "UPDATE  " _
                   & "  public.glbal_balance   " _
                   & "SET  " _
                   & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                   & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "," _
                   & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(_glt_value) & ",  " _
                   & "  glbal_dt =   " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                   & "  " _
                   & "WHERE  " _
                   & "  glbal_oid = " & SetSetring(dt_glbal.Rows(0).Item(0)) & " "

                _ssqls.Add(ssql)
            End If


            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    Function move_lababerjalan(ByVal _ssqls As ArrayList, ByVal _en_id As String) As Boolean

        ssql = " SELECT  " _
               & "  a.glbal_balance_posted, " _
               & "  b.ac_sign, " _
               & "  b.ac_type, " _
               & "  a.glbal_ac_id " _
               & "FROM " _
               & "  public.ac_mstr b " _
               & "  INNER JOIN public.glbal_balance a ON (b.ac_id = a.glbal_ac_id) " _
               & "WHERE " _
               & "  a.glbal_gcal_oid = '" & le_glperiode.EditValue & "' AND  " _
               & "  coalesce(a.glbal_balance_posted, 0) <> 0 AND  " _
               & "  a.glbal_en_id = " & _en_id & " AND  b.ac_type IN ('R','E')"

        Dim dt_profit As New DataTable
        dt_profit = GetTableData(ssql)

        For Each dr_profit As DataRow In dt_profit.Rows

        Next

    End Function

    Private Sub btRecountGLBal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRecountGLBal.Click
        Try
            Dim ssqls As New ArrayList
            'Dim level, dom, en As Integer

            If MessageBox.Show("Recalculate?", "Recalculate", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            'dom = 0
            'en = 0

            'ssqls.Clear()
            'ssql = "update glbal_balance set glbal_balance_unposted=0,glbal_balance_posted=0 where glbal_gcal_oid='" & le_glperiode.EditValue & "' "
            'ssqls.Add(ssql)

            'If le_entity.EditValue > 0 Then
            '    level = 2
            '    en = CInt(le_entity.EditValue)
            'Else
            '    level = 2
            '    dom = 1
            'End If

            'If le_entity.EditValue = 0 Then
            '    ssql = "select en_id,en_desc from en_mstr where en_id<>0  and en_limit_account='Y'  order by en_id "
            'Else
            '    ssql = "select en_id,en_desc from en_mstr where en_id=" & en & " order by en_id "
            'End If

            'Dim dt_en As New DataTable
            'dt_en = GetTableData(ssql)
            'Dim i As Integer = 0
            'For Each dr_en As DataRow In dt_en.Rows

            '    If CeCalcOpen.EditValue = True Then

            '        ssql = "select ac_id, f_get_glbal(ac_id," & level & "," & dom & "," & _
            '              dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','N') as gl_unposted, f_get_glbal(ac_id," & level & "," & dom & "," & _
            '              dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','Y') as gl_posted,f_get_glbal(ac_id," & level & "," & dom & "," & _
            '              dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','O') as gl_open, ac_code_hirarki, ac_name, ac_sign,ac_is_sumlevel,ac_cu_id from ac_mstr " _
            '              & "where ac_is_sumlevel='N' and ac_id<>0 order by ac_id"

            '    Else
            '        ssql = "select ac_id, f_get_glbal(ac_id," & level & "," & dom & "," & _
            '              dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','N') as gl_unposted, f_get_glbal(ac_id," & level & "," & dom & "," & _
            '              dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','Y') as gl_posted,f_get_glbal(ac_id," & level & "," & dom & "," & _
            '              dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','C') as gl_open,ac_code_hirarki, ac_name, ac_sign,ac_is_sumlevel,ac_cu_id from ac_mstr " _
            '              & "where ac_is_sumlevel='N' and ac_id<>0 order by ac_id"
            '    End If

            '    Dim dt As New DataTable
            '    dt = GetTableData(ssql)
            '    i += 1
            '    btRecountGLBal.Text = "Recount GL Total = " & System.Math.Round(i / dt_en.Rows.Count * 100, 0) & " %"
            '    System.Windows.Forms.Application.DoEvents()

            '    For Each dr_ac As DataRow In dt.Rows

            '        ssql = "select * from glbal_balance where glbal_en_id=" & dr_en("en_id") _
            '            & " and glbal_gcal_oid=" & SetSetring(le_glperiode.EditValue.ToString) _
            '            & " and glbal_ac_id=" & dr_ac("ac_id")

            '        If CekRowSelect(ssql) > 0 Then
            '            ssql = "update glbal_balance set glbal_balance_unposted=" & SetDec(dr_ac("gl_unposted")) _
            '            & " , glbal_balance_posted=" & SetDec(dr_ac("gl_posted")) & " where glbal_en_id=" & dr_en("en_id") _
            '            & " and glbal_gcal_oid=" & SetSetring(le_glperiode.EditValue.ToString) _
            '            & " and glbal_ac_id=" & dr_ac("ac_id")

            '        Else

            '            ssql = "INSERT INTO  " _
            '                & "  public.glbal_balance " _
            '                & "( " _
            '                & "  glbal_oid, " _
            '                & "  glbal_dom_id, " _
            '                & "  glbal_en_id, " _
            '                & "  glbal_gcal_oid, " _
            '                & "  glbal_ac_id, " _
            '                & "  glbal_sb_id, " _
            '                & "  glbal_cc_id, " _
            '                & "  glbal_cu_id, " _
            '                & "  glbal_balance_open, " _
            '                & "  glbal_balance_unposted, " _
            '                & "  glbal_balance_posted " _
            '                & ")  " _
            '                & "VALUES ( " _
            '                & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '                & SetInteger("1") & ",  " _
            '                & SetInteger(dr_en("en_id")) & ",  " _
            '                & SetSetring(le_glperiode.EditValue.ToString) & ",  " _
            '                & SetInteger(dr_ac("ac_id")) & ",  " _
            '                & SetInteger("0") & ",  " _
            '                & SetInteger("0") & ",  " _
            '                & SetInteger(dr_ac("ac_cu_id")) & ",  " _
            '                & SetDec(dr_ac("gl_open")) & ",  " _
            '                & SetDec(dr_ac("gl_unposted")) & ",  " _
            '                & SetDec(dr_ac("gl_posted")) & "  " _
            '                & ")"
            '        End If

            '        ssqls.Add(ssql)
            '    Next

            '    'Dim xx As New ArrayList
            '    'xx = ssqls
            '    If DbRunTran(ssqls) = False Then
            '        Exit Sub
            '    End If
            '    ssqls.Clear()
            'Next



            Dim dt As New DataTable

            '===============recalculate====================
            LblProses.Text = "Status : Pra Closing, Recalculate"
            System.Windows.Forms.Application.DoEvents()

            ssql = "UPDATE  " _
                 & "  public.glbal_balance   " _
                 & "SET  " _
                 & "  glbal_balance_unposted = 0,  " _
                  & "  glbal_balance_posted_end_month = 0,  " _
                 & "  glbal_balance_posted = 0,glbal_balance_end_month1=0, glbal_balance_trial=0  " _
                 & "WHERE  " _
                 & "  glbal_gcal_oid = " & SetSetring(le_glperiode.EditValue)

            ssqls.Add(ssql)

            ssql = " select glt_ac_id,glt_en_id,sum(unpost) as unpost_all,sum(posted) as posted_all from ( " _
                & " SELECT " _
                & "a.glt_ac_id,a.glt_en_id, " _
                & " sum(0) as unpost, sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit) * a.glt_exc_rate) as posted  " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_date BETWEEN " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_start_date")) & " AND " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & " and a.glt_posted='Y' " _
                & " group by a.glt_ac_id,a.glt_en_id " _
                & " union all  " _
                & "  SELECT " _
                & "a.glt_ac_id,a.glt_en_id, " _
                & "  sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit)  * a.glt_exc_rate) as unpost, sum(0) as posted  " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_date BETWEEN " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_start_date")) & " AND " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & " and a.glt_posted='N' " _
                & " group by a.glt_ac_id,a.glt_en_id) as temp " _
                & " group by glt_ac_id,glt_en_id"

            'Dim dt As New DataTable
            dt = GetTableData(ssql)


            For Each dr As DataRow In dt.Rows

                ssql = "UPDATE  " _
                  & "  public.glbal_balance   " _
                  & "SET  " _
                  & "  glbal_balance_unposted = " & SetDec(dr("unpost_all")) & ",  " _
                  & "  glbal_balance_posted = " & SetDec(dr("posted_all")) & "  " _
                  & "WHERE  " _
                  & "  glbal_gcal_oid = " & SetSetring(le_glperiode.EditValue) & " and  " _
                  & "  glbal_en_id = " & dr("glt_en_id") & " and " _
                  & "  glbal_ac_id = " & dr("glt_ac_id") & " "

                ssqls.Add(ssql)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If



            btRecountGLBal.Text = "Recount GL Total = Execute "
            System.Windows.Forms.Application.DoEvents()
            'DbRunTran(ssqls)
            Box("Sukses")
            btRecountGLBal.Text = "Recount GL Total"
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged
        Try
            If (le_entity.EditValue = 1 Or le_entity.EditValue = 0) Then
                If ce_all_child.EditValue = True Then
                    ce_all_child.EditValue = False
                    ce_all_child.Enabled = False
                Else
                    ce_all_child.Enabled = False
                End If
            Else
                ce_all_child.Enabled = True
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sb_close_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_close.Click

        ssql = "SELECT  " _
           & "  coalesce(a.gcal_pra_closing,'N') as gcal_pra_closing,coalesce(gcal_closing,'N') as gcal_closing " _
           & "FROM " _
           & "  public.gcal_mstr a " _
           & "WHERE gcal_oid='" & le_glperiode.EditValue & "'"

        Dim dt_close As New DataTable

        dt_close = GetTableData(ssql)

        Dim _status_pra, _status_close As String
        _status_pra = ""
        _status_close = ""

        For Each dr As DataRow In dt_close.Rows
            _status_close = SetString(dr("gcal_closing"))
            _status_pra = SetString(dr("gcal_pra_closing"))
        Next

        If _status_pra = "N" Then
            Box("Pra Closing first, please..")
            Exit Sub
        End If

        If _status_close = "Y" Then
            Box("This periode have been processed")
            Exit Sub
        End If

        'proses pemindahan dari semua pendapatan dan beban ke laba berjalan
        'closing juga supaya transaksi tidak bisa diedit
        'proses pemindahan saldo akhir ke opening bulan berikutnya atau jika belum ada maka akan membuat gcal baru
        Dim _en As Integer = 0
        Dim _level As Integer = 1
        Dim _dom As Integer = 1
        Dim _glt_code As String

        ' _en = GetRowInfo("select en_id,en_code from en_mstr where en_parent is null and en_id>0")(0)

        System.Windows.Forms.Application.DoEvents()

        ssql = "SELECT  " _
                & "  a.ac_id, " _
                & "  a.ac_code, " _
                & "  a.ac_name, " _
                & "  a.ac_type, " _
                & "  a.ac_is_sumlevel, " _
                & "  a.ac_sign, " _
                & "  a.ac_cu_id, " _
                & "  a.ac_level, " _
                & "  b.code " _
                & "FROM " _
                & "  public.ac_mstr a INNER JOIN " _
                & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
                & "  where b.code='acc_profit_running'"

        Dim dr_laba_berjalan As DataRow
        dr_laba_berjalan = GetRowInfo(ssql)


        ssql = "SELECT  " _
                & "  a.ac_id, " _
                & "  a.ac_code, " _
                & "  a.ac_name, " _
                & "  a.ac_type, " _
                & "  a.ac_is_sumlevel, " _
                & "  a.ac_sign, " _
                & "  a.ac_cu_id, " _
                & "  a.ac_level, " _
                & "  b.code " _
                & "FROM " _
                & "  public.ac_mstr a INNER JOIN " _
                & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
                & "  where b.code='acc_profit_hold'"

        Dim dr_laba_ditahan As DataRow
        dr_laba_ditahan = GetRowInfo(ssql)

        Try
            Dim ssqls As New ArrayList

            LblProses.Text = "Status : Closing, Copy Profit Loss & Trial Balance."
            System.Windows.Forms.Application.DoEvents()

            ssql = "update glbal_balance set glbal_balance_posted_end_month=coalesce(glbal_balance_posted,0) " _
                & "where glbal_gcal_oid='" & le_glperiode.EditValue & "' and glbal_ac_id in " _
                & "(select ac_id from ac_mstr where ac_type in ('E','R') and ac_active='Y' and ac_is_sumlevel='N') "

            ssqls.Add(ssql)

            ssql = "update glbal_balance set glbal_balance_trial=coalesce(glbal_balance_posted,0) " _
               & "where glbal_gcal_oid='" & le_glperiode.EditValue & "' and glbal_ac_id in " _
               & "(select ac_id from ac_mstr where  ac_active='Y' and ac_is_sumlevel='N') "

            ssqls.Add(ssql)


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            ' Box("Copy Profit Loss & Trial Balance succes")
            System.Windows.Forms.Application.DoEvents()

            '=======================================================
            'For Each dr As DataRow In dt.Rows

            LblProses.Text = "Status : Closing, Move Expence & Revenue to Running Profit."
            System.Windows.Forms.Application.DoEvents()

            ssql = "select en_id,en_code,en_desc from en_mstr where en_id<>0 and en_active='Y' and coalesce(en_endmonth,'N')='Y'  order by en_desc "
            Dim dt As New DataTable
            dt = GetTableData(ssql)

            _level = 2

            'Dim _en_id_string As String = ""

            'For Each dr As DataRow In dt.Rows
            '    _en_id_string = _en_id_string & dr("en_id") & ","
            'Next

            '_en_id_string = _en_id_string.Substring(0, _en_id_string.Length - 1)

            For Each dr As DataRow In dt.Rows
                ssql = "SELECT  " _
                    & "  a.ac_id, " _
                    & "  a.ac_code, " _
                    & "  a.ac_name, " _
                    & "  a.ac_type, " _
                    & "  a.ac_is_sumlevel, " _
                    & "  a.ac_sign, " _
                    & "  a.ac_cu_id, " _
                    & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                    & ", " & dr("en_id") & ", cast('" & le_glperiode.EditValue & "' as uuid), 'N') as ac_value " _
                    & "FROM " _
                    & "  public.ac_mstr a " _
                    & "WHERE " _
                    & "  a.ac_type in ('E','R') and ac_active='Y' and ac_is_sumlevel='N' order by ac_type desc,ac_code"

                Dim dt_ac As New DataTable
                dt_ac = GetTableData(ssql)

                Dim _en_code As String
                _en_code = dr("en_code") 'GetRowInfo("select en_id,en_code from en_mstr where en_parent is null and en_id>0")(1)
                _en = dr("en_id")

                _glt_code = func_coll.get_transaction_number("JE", _en_code, "glt_det", "glt_code")
                'pemindahan dari beban dan pendapatan ke laba berjalan
                Dim i As Integer = 0
                Dim n As Integer = 0
                For Each dr_ac As DataRow In dt_ac.Rows
                    If dr_ac("ac_value") <> 0 Then
                        If dr_ac("ac_type") = "E" Then
                            'beban dulu yang disesuaikan
                            'insert debet (akun tujuan (laba berjalan))
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(_en) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JE") & ",  " _
                             & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " D " & dr_ac("ac_code") & " " & Microsoft.VisualBasic.Left(dr_ac("ac_name").ToString, 25)) & ",  " _
                             & SetDbl(dr_ac("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"


                            ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, _en, dr_laba_berjalan("ac_id"), "D", dr_ac("ac_value"), _
                                             dr_laba_berjalan("ac_cu_id"), 1) = False Then

                                Exit Sub
                            End If

                            'insert kredit
                            ssql = "INSERT INTO  " _
                              & "  public.glt_det " _
                              & "( " _
                              & "  glt_oid, " _
                              & "  glt_dom_id, " _
                              & "  glt_en_id, " _
                              & "  glt_add_by, " _
                              & "  glt_add_date, " _
                              & "  glt_code, " _
                              & "  glt_date, " _
                              & "  glt_type, " _
                              & "  glt_cu_id, " _
                              & "  glt_exc_rate, " _
                              & "  glt_seq, " _
                              & "  glt_ac_id, " _
                              & "  glt_sb_id, " _
                              & "  glt_cc_id, " _
                              & "  glt_desc, " _
                              & "  glt_debit, " _
                              & "  glt_credit, " _
                              & "  glt_ref_trans_code, " _
                              & "  glt_daybook, " _
                              & "  glt_posted, " _
                              & "  glt_dt " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                              & SetInteger(_en) & ",  " _
                              & SetSetring(master_new.ClsVar.sNama) & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                              & SetSetring(_glt_code) & ",  " _
                              & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                              & SetSetring("JE") & ",  " _
                              & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                              & SetDbl(1) & ",  " _
                              & SetInteger(i + 1) & ",  " _
                              & SetIntegerDB(dr_ac("ac_id")) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " K ") & ",  " _
                              & SetDbl(0) & ",  " _
                              & SetDbl(dr_ac("ac_value")) & ",  " _
                              & SetSetring("") & ",  " _
                              & SetSetring("MONTH END") & ",  " _
                              & SetSetring("Y") & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                              & ")"

                            ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, _en, dr_ac("ac_id"), "C", dr_ac("ac_value"), _
                                             dr_ac("ac_cu_id"), 1, True) = False Then
                                Exit Sub
                            End If
                        Else

                            'insert debet (akun tujuan (akun pendapatan di debet berarti berkurang))
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(_en) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JE") & ",  " _
                             & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i + 1) & ",  " _
                             & SetIntegerDB(dr_ac("ac_id")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " D ") & ",  " _
                             & SetDbl(dr_ac("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, _en, dr_ac("ac_id"), "D", dr_ac("ac_value"), _
                                             dr_ac("ac_cu_id"), 1, True) = False Then
                                Exit Sub
                            End If


                            'insert kredit akun laba di kredit berarti bertambah

                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(_en) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JE") & ",  " _
                             & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " K " & dr_ac("ac_code") & " " & Microsoft.VisualBasic.Left(dr_ac("ac_name").ToString, 25)) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetDbl(dr_ac("ac_value")) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, _en, dr_laba_berjalan("ac_id"), "C", dr_ac("ac_value"), _
                                             dr_laba_berjalan("ac_cu_id"), 1) = False Then

                                Exit Sub
                            End If
                        End If
                        i += 1
                    End If
                    n += 1
                    LblProses.Text = "Akun yang di proses " & n & " " & dr("en_desc") & " " & dr_ac("ac_code") & " " & dr_ac("ac_name") & " " & dt_ac.Rows.Count
                    System.Windows.Forms.Application.DoEvents()
                Next
            Next

            'Next


            'pemindahan open + posted ke opening bulan berikutnya
            'If ask("Anda yakin akan memproses laba berjalan?", "Konfirmasi...") Then
            'SaveArraytoFile(ssqls)


            If master_new.PGSqlConn.status_sync = True Then
                'DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            'Box("Proses Step 1 sucess")

            LblProses.Text = "Status : Closing, Step 1 Success."
            System.Windows.Forms.Application.DoEvents()

            'Else
            'Exit Sub
            'End If

            'Exit Sub

            LblProses.Text = "Status : Closing, Copy Ending Balance to next periode."
            System.Windows.Forms.Application.DoEvents()

            'pemindahan open + posted ke opening bulan berikutnya
            'looping entity

            Dim dt_glbal As New DataTable

            Dim dr_gcal As DataRow = get_next_gcal(le_glperiode.EditValue)

            ssql = "update glbal_balance set glbal_balance_open=0 ," _
                   & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                   & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                   & " where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                   & "  and glbal_gcal_oid='" & dr_gcal("gcal_oid").ToString & "'"

            ssqls.Add(ssql)

            _level = 2

            For Each dr As DataRow In dt.Rows
                ssql = "SELECT  " _
                   & "  a.ac_id, " _
                   & "  a.ac_code, " _
                   & "  a.ac_name, " _
                   & "  a.ac_type, " _
                   & "  a.ac_is_sumlevel, " _
                   & "  a.ac_sign, " _
                   & "  a.ac_cu_id, " _
                   & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                       & ", " & dr("en_id") & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                   & "FROM " _
                   & "  public.ac_mstr a " _
                   & "WHERE " _
                   & " ac_active='Y' and ac_is_sumlevel='N' and ac_id<>0 order by ac_type,ac_id,ac_code"

                Dim dt_ac As New DataTable
                dt_ac = GetTableData(ssql)

                Dim x As Integer = 0

                ssql = "select glbal_oid,glbal_ac_id from glbal_balance where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                          & " and glbal_en_id=" & dr("en_id") & " and glbal_gcal_oid='" & dr_gcal("gcal_oid").ToString _
                          & "' order by glbal_ac_id "

                dt_glbal = GetTableData(ssql)

                Dim _ada As Boolean = False
                For Each dr_ac As DataRow In dt_ac.Rows
                    _ada = False
                    For Each dr_glbal As DataRow In dt_glbal.Rows
                        If dr_ac("ac_id") = dr_glbal("glbal_ac_id") Then
                            _ada = True

                            ssql = "update glbal_balance set glbal_balance_open=coalesce(" & SetDec(dr_ac("ac_value")) _
                               & ",0),  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                               & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                               & " where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                               & " and glbal_en_id=" & dr("en_id") & " and glbal_gcal_oid='" & dr_gcal("gcal_oid").ToString _
                               & "' and glbal_ac_id=" & dr_ac("ac_id")

                            ssqls.Add(ssql)

                            Exit For
                        End If

                    Next

                    If _ada = False Then

                        ssql = "INSERT INTO  " _
                                & "  public.glbal_balance " _
                                & "( " _
                                & "  glbal_oid, " _
                                & "  glbal_dom_id, " _
                                & "  glbal_en_id, " _
                                & "  glbal_add_by, " _
                                & "  glbal_add_date, " _
                                & "  glbal_gcal_oid, " _
                                & "  glbal_ac_id, " _
                                & "  glbal_sb_id, " _
                                & "  glbal_cc_id, " _
                                & "  glbal_cu_id, " _
                                & "  glbal_balance_open, " _
                                & "  glbal_balance_unposted, " _
                                & "  glbal_balance_posted, " _
                                & "  glbal_dt " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(dr("en_id")) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetSetring(dr_gcal("gcal_oid").ToString) & ",  " _
                                & SetInteger(dr_ac("ac_id")) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                                & SetDbl(dr_ac("ac_value")) & ",  " _
                                & SetDbl(0) & ",  " _
                                & SetDbl(0) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                & ")"

                        ssqls.Add(ssql)
                    End If
                    x += 1
                    LblProses.Text = "Akun yang di proses " & x & " " & dr_ac("ac_code") & " " & dr_ac("ac_name") & " " & dt_ac.Rows.Count & " " & dr("en_desc")
                    System.Windows.Forms.Application.DoEvents()
                Next
            Next


            'If ask("Anda yakin akan memproses step 2 (pemindahan kepala 51 ke WIP)?", "Konfirmasi...") Then
            If master_new.PGSqlConn.status_sync = True Then
                'DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            'Box("Proses Closing tahap pembuatan Opening Balance Sukses")
            System.Windows.Forms.Application.DoEvents()

            LblProses.Text = "Status : Closing, Copy Ending Balance to next periode. Success"
            System.Windows.Forms.Application.DoEvents()
            'Else
            'Exit Sub
            'End If

            'perubahan status di gcal_det
            For Each dr As DataRow In dt.Rows
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
                        & "WHERE gcald_gcal_oid = '" & le_glperiode.EditValue.ToString & "' " _
                        & " and gcald_en_id = " & dr("en_id")

                ssqls.Add(ssql)
            Next


            'Dim tanggal_awal As Date
            'tanggal_awal = DateAdd(DateInterval.Day, 1, CDate(le_glperiode.GetColumnValue("gcal_end_date")))

            LblProses.Text = "Status : Closing, Create Profit next periode."
            System.Windows.Forms.Application.DoEvents()

            'If get_next_gcal(le_glperiode.EditValue, dt_gcal) = False Then
            '    Box("Next GL Calender not found")
            '    Exit Sub
            'End If

            '_level = 1
            'pembuatan jurnal otomatis uktuk awal bulan
            'For Each dr As DataRow In dt.Rows

            _level = 2

            For Each dr As DataRow In dt.Rows
                _en = dr("en_id")

                ssql = "SELECT  " _
                  & "  a.ac_id, " _
                  & "  a.ac_code, " _
                  & "  a.ac_name, " _
                  & "  a.ac_type, " _
                  & "  a.ac_is_sumlevel, " _
                  & "  a.ac_sign, " _
                  & "  a.ac_cu_id, " _
                  & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                      & ", " & _en & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                  & "FROM " _
                  & "  public.ac_mstr a " _
                  & "WHERE " _
                  & "  ac_id=" & dr_laba_berjalan(0) & ""

                Dim dt_ac As New DataTable
                dt_ac = GetTableData(ssql)


                _glt_code = func_coll.get_transaction_number("JE", dr("en_code"), "glt_det", "glt_code")

                Dim i As Integer = 0
                For Each dr_ac In dt_ac.Rows
                    '(akun tujuan (laba berjalan))

                    ssql = "INSERT INTO  " _
                     & "  public.glt_det " _
                     & "( " _
                     & "  glt_oid, " _
                     & "  glt_dom_id, " _
                     & "  glt_en_id, " _
                     & "  glt_add_by, " _
                     & "  glt_add_date, " _
                     & "  glt_code, " _
                     & "  glt_date, " _
                     & "  glt_type, " _
                     & "  glt_cu_id, " _
                     & "  glt_exc_rate, " _
                     & "  glt_seq, " _
                     & "  glt_ac_id, " _
                     & "  glt_sb_id, " _
                     & "  glt_cc_id, " _
                     & "  glt_desc, " _
                     & "  glt_debit, " _
                     & "  glt_credit, " _
                     & "  glt_ref_trans_code, " _
                     & "  glt_daybook, " _
                     & "  glt_posted, " _
                     & "  glt_dt " _
                     & ")  " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                     & SetInteger(_en) & ",  " _
                     & SetSetring(master_new.ClsVar.sNama) & ",  " _
                     & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                     & SetSetring(_glt_code) & ",  " _
                     & SetDate(dr_gcal("gcal_start_date")) & ",  " _
                     & SetSetring("JE") & ",  " _
                     & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                     & SetDbl(1) & ",  " _
                     & SetInteger(i) & ",  " _
                     & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                     & SetIntegerDB(0) & ",  " _
                     & SetIntegerDB(0) & ",  " _
                     & SetSetringDB("End Month Adjusment 3, D ") & ",  " _
                     & SetDbl(dr_ac("ac_value")) & ",  " _
                     & SetDbl(0) & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("MONTH END") & ",  " _
                     & SetSetring("Y") & ",  " _
                     & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                     & ")"

                    ssqls.Add(ssql)

                    'update gl bal
                    If update_gl_bal(ssqls, dr_gcal("gcal_oid").ToString, _en, dr_laba_berjalan("ac_id"), "D", dr_ac("ac_value"), _
                                     dr_laba_berjalan("ac_cu_id"), 1) = False Then

                        Exit Sub
                    End If

                    'insert kredit
                    ssql = "INSERT INTO  " _
                      & "  public.glt_det " _
                      & "( " _
                      & "  glt_oid, " _
                      & "  glt_dom_id, " _
                      & "  glt_en_id, " _
                      & "  glt_add_by, " _
                      & "  glt_add_date, " _
                      & "  glt_code, " _
                      & "  glt_date, " _
                      & "  glt_type, " _
                      & "  glt_cu_id, " _
                      & "  glt_exc_rate, " _
                      & "  glt_seq, " _
                      & "  glt_ac_id, " _
                      & "  glt_sb_id, " _
                      & "  glt_cc_id, " _
                      & "  glt_desc, " _
                      & "  glt_debit, " _
                      & "  glt_credit, " _
                      & "  glt_ref_trans_code, " _
                      & "  glt_daybook, " _
                      & "  glt_posted, " _
                      & "  glt_dt " _
                      & ")  " _
                      & "VALUES ( " _
                      & SetSetring(Guid.NewGuid.ToString) & ",  " _
                      & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                      & SetInteger(_en) & ",  " _
                      & SetSetring(master_new.ClsVar.sNama) & ",  " _
                      & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                      & SetSetring(_glt_code) & ",  " _
                      & SetDate(dr_gcal("gcal_start_date")) & ",  " _
                      & SetSetring("JE") & ",  " _
                      & SetInteger(dr_laba_ditahan("ac_cu_id")) & ",  " _
                      & SetDbl(1) & ",  " _
                      & SetInteger(i + 1) & ",  " _
                      & SetIntegerDB(dr_laba_ditahan("ac_id")) & ",  " _
                      & SetIntegerDB(0) & ",  " _
                      & SetIntegerDB(0) & ",  " _
                      & SetSetringDB("End Month Adjusment 3, K ") & ",  " _
                      & SetDbl(0) & ",  " _
                      & SetDbl(dr_ac("ac_value")) & ",  " _
                      & SetSetring("") & ",  " _
                      & SetSetring("MONTH END") & ",  " _
                      & SetSetring("Y") & ",  " _
                      & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                      & ")"

                    ssqls.Add(ssql)

                    'update glbal
                    If update_gl_bal(ssqls, dr_gcal("gcal_oid").ToString, _en, dr_laba_ditahan("ac_id"), "C", dr_ac("ac_value"), _
                                     dr_laba_ditahan("ac_cu_id"), 1) = False Then
                        Exit Sub
                    End If

                Next
            Next



            ssql = "update public.gcal_mstr set  gcal_closing='Y' where gcal_oid=" & SetSetring(le_glperiode.EditValue)

            ssqls.Add(ssql)


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            'Box("Proses Closing tahap pembuatan Jurnal Laba Berjalan ke Laba Ditahan Sukses")
            LblProses.Text = "Status : Closing, End Month Success."
            Box("Closing, End Month Success.")
            System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub
    Public Function get_next_gcal(ByVal _gcal_uid As String) As DataRow
        Try
            ssql = "select gcal_oid,gcal_start_date,gcal_end_date from gcal_mstr where gcal_start_date > (select gcal_end_date from gcal_mstr where gcal_oid='" & _gcal_uid & "') order by gcal_start_date limit 1"

            Dim dr As DataRow
            dr = GetRowInfo(ssql)

            Return dr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub le_glperiode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_glperiode.EditValueChanged
        Try
            LblPraClosing.Text = "Pra Closing : " & le_glperiode.GetColumnValue("gcal_pra_closing")
            LblClosing.Text = "Closing : " & le_glperiode.GetColumnValue("gcal_closing")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub le_glperiode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_glperiode.GotFocus
        'init_le(le_glperiode, "gcal_mstr")
        ssql = "SELECT  " _
                    & "  a.gcal_oid, " _
                    & "  a.gcal_year, " _
                    & "  a.gcal_periode, " _
                    & "  a.gcal_start_date, " _
                    & "  a.gcal_end_date,coalesce(a.gcal_pra_closing,'N') as gcal_pra_closing,coalesce(gcal_closing,'N') as  gcal_closing " _
                    & "FROM " _
                    & "  public.gcal_mstr a " _
                    & "ORDER BY " _
                    & "  a.gcal_year desc, " _
                    & "  a.gcal_periode desc"

        Dim dt2 As New DataTable

        dt2 = GetTableData(ssql)
        With le_glperiode
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_oid", "gcal_oid", 10, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Default))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_year", "Year", 20))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_periode", "Periode", 20))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_start_date", "Start Date", 10, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Default))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_end_date", "Start Date", 10, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Default))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_pra_closing", "Pra Closing", 20))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_closing", "Closing", 20))
            End If
            .Properties.DataSource = dt2
            .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Properties.DisplayFormat.FormatString = "d"
            .Properties.DisplayMember = dt2.Columns("gcal_start_date").ToString
            .Properties.ValueMember = dt2.Columns("gcal_oid").ToString
            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 20
            .Properties.PopupWidth = 300
        End With
    End Sub

    Private Sub BtUpdatePL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtUpdatePL.Click
        Try

            Dim ssqls As New ArrayList

            If ask("Anda yakin akan mengupdate profil loss?", "Konfirmasi...") = False Then
                Exit Sub
            End If

            ssql = "UPDATE  " _
                         & "  public.glbal_balance   " _
                         & "SET  " _
                         & "  glbal_balance_unposted = 0,  " _
                          & "  glbal_balance_posted_end_month = 0,  " _
                         & "  glbal_balance_posted = 0,glbal_balance_end_month1=0, glbal_balance_trial=0  " _
                         & "WHERE  " _
                         & "  glbal_gcal_oid = " & SetSetring(le_glperiode.EditValue) & "  and  glbal_ac_id in " _
                        & "(select ac_id from ac_mstr where ac_type in ('E','R') and ac_active='Y' and ac_is_sumlevel='N')"

            ssqls.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                If master_new.PGSqlConn.DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If master_new.PGSqlConn.DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If

            System.Windows.Forms.Application.DoEvents()

            ssql = "SELECT  " _
               & "  a.ac_id, " _
               & "  a.ac_code, " _
               & "  a.ac_name, " _
               & "  a.ac_type, " _
               & "  a.ac_is_sumlevel, " _
               & "  a.ac_sign, " _
               & "  a.ac_cu_id, " _
               & "  a.ac_level, " _
               & "  b.code " _
               & "FROM " _
               & "  public.ac_mstr a INNER JOIN " _
               & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
               & "  where b.code='acc_profit_running'"

            Dim dr_laba_berjalan As DataRow
            dr_laba_berjalan = GetRowInfo(ssql)



            ssql = "SELECT  " _
                & "  public.glt_det.glt_oid, " _
                & "  public.glt_det.glt_dom_id, " _
                & "  public.glt_det.glt_en_id, " _
                & "  public.glt_det.glt_code, " _
                & "  public.glt_det.glt_date, " _
                & "  public.glt_det.glt_type, " _
                & "  public.glt_det.glt_cu_id, " _
                & "  public.glt_det.glt_exc_rate, " _
                & "  public.glt_det.glt_seq, " _
                & "  public.glt_det.glt_ac_id, " _
                & "  public.glt_det.glt_sb_id, " _
                & "  public.glt_det.glt_cc_id, " _
                & "  public.glt_det.glt_desc, " _
                & "  public.glt_det.glt_debit, " _
                & "  public.glt_det.glt_credit, " _
                & "  public.glt_det.glt_ref_tran_id, " _
                & "  public.glt_det.glt_ref_trans_code, " _
                & "  public.glt_det.glt_posted, " _
                & "  public.glt_det.glt_dt, " _
                & "  public.glt_det.glt_daybook, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ac_mstr.ac_type, " _
                & "  public.ac_mstr.ac_sign " _
                & "FROM " _
                & "  public.glt_det " _
                & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                & "WHERE " _
                & "  glt_date BETWEEN " & SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & " AND " & _
                                                         SetDateNTime00(le_glperiode.GetColumnValue("gcal_end_date")) & "  AND  " _
                & "  public.glt_det.glt_type = 'JE' AND  " _
                & "  glt_ac_id NOT IN (" & SetInteger(dr_laba_berjalan("ac_id")) & ") " _
                & " ORDER BY " _
                & "  ac_type,glt_dt"

            Dim dt_gl As New DataTable
            dt_gl = GetTableData(ssql)



            For Each dr_gl As DataRow In dt_gl.Rows
                If dr_gl("ac_sign") = "D" Then
                    ssql = "update glbal_balance set glbal_balance_posted_end_month=" & SetDec(dr_gl("glt_debit") - dr_gl("glt_credit")) _
                         & ",glbal_balance_trial=" & SetDec(dr_gl("glt_debit") - dr_gl("glt_credit")) & " where glbal_gcal_oid='" & le_glperiode.EditValue _
                         & "' and glbal_ac_id =" & SetInteger(dr_gl("glt_ac_id")) & " and glbal_en_id=" & SetInteger(dr_gl("glt_en_id"))

                    ssqls.Add(ssql)

                    'ssql = "update glbal_balance set glbal_balance_trial=" & SetDec(dr_gl("glt_debit") - dr_gl("glt_credit")) _
                    '   & " where glbal_gcal_oid='" & le_glperiode.EditValue & "' and glbal_ac_id =" & SetInteger(dr_gl("glt_ac_id"))

                    'ssqls.Add(ssql)

                Else
                    ssql = "update glbal_balance set glbal_balance_posted_end_month=" & SetDec(dr_gl("glt_credit") - dr_gl("glt_debit")) _
                        & ",glbal_balance_trial=" & SetDec(dr_gl("glt_credit") - dr_gl("glt_debit")) _
                        & " where glbal_gcal_oid='" & le_glperiode.EditValue & "' and glbal_ac_id =" & SetInteger(dr_gl("glt_ac_id")) _
                        & " and glbal_en_id=" & SetInteger(dr_gl("glt_en_id"))

                    ssqls.Add(ssql)

                    'ssql = "update glbal_balance set glbal_balance_trial=" & SetDec(dr_gl("glt_credit") - dr_gl("glt_debit")) _
                    ' & " where glbal_gcal_oid='" & le_glperiode.EditValue & "' and glbal_ac_id =" & SetInteger(dr_gl("glt_ac_id"))

                    'ssqls.Add(ssql)

                End If


            Next


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If

            LblProses.Text = "Status : Update PL success"
            Box("Update PL success")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtMassiveClosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtMassiveClosing.Click
        Try
            If SetNumber(te_month.EditValue) > 0 Then

                ssql = "SELECT  " _
                    & "  a.gcal_oid, " _
                    & "  a.gcal_dom_id, " _
                    & "  a.gcal_add_by, " _
                    & "  a.gcal_add_date, " _
                    & "  a.gcal_upd_by, " _
                    & "  a.gcal_upd_date, " _
                    & "  a.gcal_year, " _
                    & "  a.gcal_periode, " _
                    & "  a.gcal_start_date, " _
                    & "  a.gcal_end_date, " _
                    & "  a.gcal_dt, " _
                    & "  a.gcal_pra_closing, " _
                    & "  a.gcal_closing " _
                    & "FROM " _
                    & "  public.gcal_mstr a " _
                    & "where  " _
                    & "gcal_start_date >= (select gcal_start_date from gcal_mstr where gcal_oid='" & le_glperiode.EditValue & "') " _
                    & "order by gcal_start_date limit " & SetInteger(te_month.EditValue)


                Dim dt_gl_cal As New DataTable
                dt_gl_cal = GetTableData(ssql)

                For Each dr_gl_cal As DataRow In dt_gl_cal.Rows
                    LblMonthPeriode.Text = Format(dr_gl_cal("gcal_start_date"), "dd/MM/yyyy")
                    Try

                        Dim ssqls As New ArrayList
                        ssql = "SELECT  " _
                                & "  a.gcal_pra_closing,gcal_closing " _
                                & "FROM " _
                                & "  public.gcal_mstr a " _
                                & "WHERE gcal_oid='" & dr_gl_cal("gcal_oid").ToString & "'"

                        Dim dt As New DataTable

                        dt = GetTableData(ssql)

                        Dim _status As String = ""

                        For Each dr As DataRow In dt.Rows
                            _status = SetString(dr("gcal_pra_closing"))
                        Next

                        If _status = "Y" Then
                            'Box("This periode have been processed")
                            LblProses.Text = "This periode have been processed"
                            System.Windows.Forms.Application.DoEvents()
                            Exit Sub
                        End If



                        '===============recalculate====================
                        LblProses.Text = "Status : Pra Closing, Recalculate"
                        System.Windows.Forms.Application.DoEvents()

                        ssql = "UPDATE  " _
                             & "  public.glbal_balance   " _
                             & "SET  " _
                             & "  glbal_balance_unposted = 0,  " _
                              & "  glbal_balance_posted_end_month = 0,  " _
                             & "  glbal_balance_posted = 0,glbal_balance_end_month1=0, glbal_balance_trial=0  " _
                             & "WHERE  " _
                             & "  glbal_gcal_oid = " & SetSetring(dr_gl_cal("gcal_oid").ToString)

                        ssqls.Add(ssql)

                        ssql = " select glt_ac_id,glt_en_id,sum(unpost) as unpost_all,sum(posted) as posted_all from ( " _
                            & " SELECT " _
                            & "a.glt_ac_id,a.glt_en_id, " _
                            & " sum(0) as unpost, sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit) * a.glt_exc_rate) as posted  " _
                            & "FROM " _
                            & "  public.glt_det a " _
                            & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                            & "WHERE " _
                            & "  a.glt_date BETWEEN " & SetDateNTime00(dr_gl_cal("gcal_start_date")) & " AND " & SetDateNTime00(dr_gl_cal("gcal_end_date")) & " and a.glt_posted='Y' " _
                            & " group by a.glt_ac_id,a.glt_en_id " _
                            & " union all  " _
                            & "  SELECT " _
                            & "a.glt_ac_id,a.glt_en_id, " _
                            & "  sum(public.f_calc_gl(b.ac_sign, a.glt_debit, a.glt_credit)  * a.glt_exc_rate) as unpost, sum(0) as posted  " _
                            & "FROM " _
                            & "  public.glt_det a " _
                            & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                            & "WHERE " _
                            & "  a.glt_date BETWEEN " & SetDateNTime00(dr_gl_cal("gcal_start_date")) & " AND " & SetDateNTime00(dr_gl_cal("gcal_end_date")) & " and a.glt_posted='N' " _
                            & " group by a.glt_ac_id,a.glt_en_id) as temp " _
                            & " group by glt_ac_id,glt_en_id"

                        'Dim dt As New DataTable
                        dt = GetTableData(ssql)


                        For Each dr As DataRow In dt.Rows

                            ssql = "UPDATE  " _
                              & "  public.glbal_balance   " _
                              & "SET  " _
                              & "  glbal_balance_unposted = " & SetDec(dr("unpost_all")) & ",  " _
                              & "  glbal_balance_posted = " & SetDec(dr("posted_all")) & "  " _
                              & "WHERE  " _
                              & "  glbal_gcal_oid = " & SetSetring(dr_gl_cal("gcal_oid").ToString) & " and  " _
                              & "  glbal_en_id = " & dr("glt_en_id") & " and " _
                              & "  glbal_ac_id = " & dr("glt_ac_id") & " "

                            ssqls.Add(ssql)
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If



                        If before_close() = False Then
                            Exit Sub
                        End If
                        LblProses.Text = "Status : Pra Closing, Copy Direct Clash Flow"
                        System.Windows.Forms.Application.DoEvents()
                        ssql = "update glbal_balance set glbal_balance_end_month1=coalesce(glbal_balance_posted,0) " _
                           & "where glbal_gcal_oid='" & dr_gl_cal("gcal_oid").ToString & "' and glbal_ac_id in " _
                           & "(select ac_id from ac_mstr where substring(ac_code_hirarki,1,2) in ( " _
                            & "SELECT  " _
                            & "  public.ac_mstr.ac_code_hirarki " _
                            & "FROM " _
                            & "  public.ac_mstr " _
                            & "  INNER JOIN public.tconfsettingendmoth a ON (public.ac_mstr.ac_id = a.end_ac_id_from) " _
                            & "WHERE " _
                            & "  a.end_step = 1)) "

                        ssqls.Add(ssql)

                        If master_new.PGSqlConn.status_sync = True Then
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If
                        'Box("Copy Direct Cash Flow succes")
                        System.Windows.Forms.Application.DoEvents()



                        ssql = "update public.gcal_mstr set  gcal_pra_closing='Y' where gcal_oid=" & SetSetring(dr_gl_cal("gcal_oid").ToString)

                        ssqls.Add(ssql)

                        If master_new.PGSqlConn.status_sync = True Then
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If

                        LblProses.Text = "Status : Pra Closing, Step 2 Success. Pra Closing finish"
                        System.Windows.Forms.Application.DoEvents()
                        'Box("Pra Closing, Step 2 Success. Pra Closing finish")


                    Catch ex As Exception
                        Pesan(Err)
                    End Try

                    '========================================closing



                    ssql = "SELECT  " _
                       & "  coalesce(a.gcal_pra_closing,'N') as gcal_pra_closing,coalesce(gcal_closing,'N') as gcal_closing " _
                       & "FROM " _
                       & "  public.gcal_mstr a " _
                       & "WHERE gcal_oid='" & dr_gl_cal("gcal_oid").ToString & "'"

                    Dim dt_close As New DataTable

                    dt_close = GetTableData(ssql)

                    Dim _status_pra, _status_close As String
                    _status_pra = ""
                    _status_close = ""

                    For Each dr As DataRow In dt_close.Rows
                        _status_close = SetString(dr("gcal_closing"))
                        _status_pra = SetString(dr("gcal_pra_closing"))
                    Next

                    If _status_pra = "N" Then
                        'Box("Pra Closing first, please..")
                        LblProses.Text = "Pra Closing first, please.."
                        System.Windows.Forms.Application.DoEvents()
                        Exit Sub
                    End If

                    If _status_close = "Y" Then
                        'Box("This periode have been processed")
                        LblProses.Text = "This periode have been processed"
                        System.Windows.Forms.Application.DoEvents()
                        Exit Sub
                    End If

                    'proses pemindahan dari semua pendapatan dan beban ke laba berjalan
                    'closing juga supaya transaksi tidak bisa diedit
                    'proses pemindahan saldo akhir ke opening bulan berikutnya atau jika belum ada maka akan membuat gcal baru
                    Dim _en As Integer = 0
                    Dim _level As Integer = 1
                    Dim _dom As Integer = 1
                    Dim _glt_code As String

                    ' _en = GetRowInfo("select en_id,en_code from en_mstr where en_parent is null and en_id>0")(0)

                    System.Windows.Forms.Application.DoEvents()

                    ssql = "SELECT  " _
                            & "  a.ac_id, " _
                            & "  a.ac_code, " _
                            & "  a.ac_name, " _
                            & "  a.ac_type, " _
                            & "  a.ac_is_sumlevel, " _
                            & "  a.ac_sign, " _
                            & "  a.ac_cu_id, " _
                            & "  a.ac_level, " _
                            & "  b.code " _
                            & "FROM " _
                            & "  public.ac_mstr a INNER JOIN " _
                            & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
                            & "  where b.code='acc_profit_running'"

                    Dim dr_laba_berjalan As DataRow
                    dr_laba_berjalan = GetRowInfo(ssql)


                    ssql = "SELECT  " _
                            & "  a.ac_id, " _
                            & "  a.ac_code, " _
                            & "  a.ac_name, " _
                            & "  a.ac_type, " _
                            & "  a.ac_is_sumlevel, " _
                            & "  a.ac_sign, " _
                            & "  a.ac_cu_id, " _
                            & "  a.ac_level, " _
                            & "  b.code " _
                            & "FROM " _
                            & "  public.ac_mstr a INNER JOIN " _
                            & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
                            & "  where b.code='acc_profit_hold'"

                    Dim dr_laba_ditahan As DataRow
                    dr_laba_ditahan = GetRowInfo(ssql)

                    Try
                        Dim ssqls As New ArrayList

                        LblProses.Text = "Status : Closing, Copy Profit Loss & Trial Balance."
                        System.Windows.Forms.Application.DoEvents()

                        ssql = "update glbal_balance set glbal_balance_posted_end_month=coalesce(glbal_balance_posted,0) " _
                            & "where glbal_gcal_oid='" & dr_gl_cal("gcal_oid").ToString & "' and glbal_ac_id in " _
                            & "(select ac_id from ac_mstr where ac_type in ('E','R') and ac_active='Y' and ac_is_sumlevel='N') "

                        ssqls.Add(ssql)

                        ssql = "update glbal_balance set glbal_balance_trial=coalesce(glbal_balance_posted,0) " _
                           & "where glbal_gcal_oid='" & dr_gl_cal("gcal_oid").ToString & "' and glbal_ac_id in " _
                           & "(select ac_id from ac_mstr where  ac_active='Y' and ac_is_sumlevel='N') "

                        ssqls.Add(ssql)


                        If master_new.PGSqlConn.status_sync = True Then
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If
                        ' Box("Copy Profit Loss & Trial Balance succes")
                        System.Windows.Forms.Application.DoEvents()

                        '=======================================================
                        'For Each dr As DataRow In dt.Rows

                        LblProses.Text = "Status : Closing, Move Expence & Revenue to Running Profit."
                        System.Windows.Forms.Application.DoEvents()

                        ssql = "select en_id,en_code,en_desc from en_mstr where en_id<>0 and en_active='Y' and coalesce(en_endmonth,'Y')='Y'  order by en_desc "
                        Dim dt As New DataTable
                        dt = GetTableData(ssql)

                        _level = 2

                        'Dim _en_id_string As String = ""

                        'For Each dr As DataRow In dt.Rows
                        '    _en_id_string = _en_id_string & dr("en_id") & ","
                        'Next

                        '_en_id_string = _en_id_string.Substring(0, _en_id_string.Length - 1)

                        For Each dr As DataRow In dt.Rows
                            ssql = "SELECT  " _
                                & "  a.ac_id, " _
                                & "  a.ac_code, " _
                                & "  a.ac_name, " _
                                & "  a.ac_type, " _
                                & "  a.ac_is_sumlevel, " _
                                & "  a.ac_sign, " _
                                & "  a.ac_cu_id, " _
                                & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                                & ", " & dr("en_id") & ", cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid), 'N') as ac_value " _
                                & "FROM " _
                                & "  public.ac_mstr a " _
                                & "WHERE " _
                                & "  a.ac_type in ('E','R') and ac_active='Y' and ac_is_sumlevel='N' order by ac_type desc,ac_code"

                            Dim dt_ac As New DataTable
                            dt_ac = GetTableData(ssql)

                            Dim _en_code As String
                            _en_code = dr("en_code") 'GetRowInfo("select en_id,en_code from en_mstr where en_parent is null and en_id>0")(1)
                            _en = dr("en_id")

                            _glt_code = func_coll.get_transaction_number("JE", _en_code, "glt_det", "glt_code")
                            'pemindahan dari beban dan pendapatan ke laba berjalan
                            Dim i As Integer = 0
                            Dim n As Integer = 0
                            For Each dr_ac As DataRow In dt_ac.Rows
                                If dr_ac("ac_value") <> 0 Then
                                    If dr_ac("ac_type") = "E" Then
                                        'beban dulu yang disesuaikan
                                        'insert debet (akun tujuan (laba berjalan))
                                        ssql = "INSERT INTO  " _
                                         & "  public.glt_det " _
                                         & "( " _
                                         & "  glt_oid, " _
                                         & "  glt_dom_id, " _
                                         & "  glt_en_id, " _
                                         & "  glt_add_by, " _
                                         & "  glt_add_date, " _
                                         & "  glt_code, " _
                                         & "  glt_date, " _
                                         & "  glt_type, " _
                                         & "  glt_cu_id, " _
                                         & "  glt_exc_rate, " _
                                         & "  glt_seq, " _
                                         & "  glt_ac_id, " _
                                         & "  glt_sb_id, " _
                                         & "  glt_cc_id, " _
                                         & "  glt_desc, " _
                                         & "  glt_debit, " _
                                         & "  glt_credit, " _
                                         & "  glt_ref_trans_code, " _
                                         & "  glt_daybook, " _
                                         & "  glt_posted, " _
                                         & "  glt_dt " _
                                         & ")  " _
                                         & "VALUES ( " _
                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                         & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                         & SetInteger(_en) & ",  " _
                                         & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                         & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                         & SetSetring(_glt_code) & ",  " _
                                         & SetDate(dr_gl_cal("gcal_end_date")) & ",  " _
                                         & SetSetring("JE") & ",  " _
                                         & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                                         & SetDbl(1) & ",  " _
                                         & SetInteger(i) & ",  " _
                                         & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                                         & SetIntegerDB(0) & ",  " _
                                         & SetIntegerDB(0) & ",  " _
                                         & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " D " & dr_ac("ac_code") & " " & Microsoft.VisualBasic.Left(dr_ac("ac_name").ToString, 25)) & ",  " _
                                         & SetDbl(dr_ac("ac_value")) & ",  " _
                                         & SetDbl(0) & ",  " _
                                         & SetSetring("") & ",  " _
                                         & SetSetring("MONTH END") & ",  " _
                                         & SetSetring("Y") & ",  " _
                                         & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                         & ")"


                                        ssqls.Add(ssql)

                                        'update gl bal
                                        If update_gl_bal(ssqls, dr_gl_cal("gcal_oid").ToString, _en, dr_laba_berjalan("ac_id"), "D", dr_ac("ac_value"), _
                                                         dr_laba_berjalan("ac_cu_id"), 1) = False Then

                                            Exit Sub
                                        End If

                                        'insert kredit
                                        ssql = "INSERT INTO  " _
                                          & "  public.glt_det " _
                                          & "( " _
                                          & "  glt_oid, " _
                                          & "  glt_dom_id, " _
                                          & "  glt_en_id, " _
                                          & "  glt_add_by, " _
                                          & "  glt_add_date, " _
                                          & "  glt_code, " _
                                          & "  glt_date, " _
                                          & "  glt_type, " _
                                          & "  glt_cu_id, " _
                                          & "  glt_exc_rate, " _
                                          & "  glt_seq, " _
                                          & "  glt_ac_id, " _
                                          & "  glt_sb_id, " _
                                          & "  glt_cc_id, " _
                                          & "  glt_desc, " _
                                          & "  glt_debit, " _
                                          & "  glt_credit, " _
                                          & "  glt_ref_trans_code, " _
                                          & "  glt_daybook, " _
                                          & "  glt_posted, " _
                                          & "  glt_dt " _
                                          & ")  " _
                                          & "VALUES ( " _
                                          & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                          & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                          & SetInteger(_en) & ",  " _
                                          & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                          & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                          & SetSetring(_glt_code) & ",  " _
                                          & SetDate(dr_gl_cal("gcal_end_date")) & ",  " _
                                          & SetSetring("JE") & ",  " _
                                          & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                                          & SetDbl(1) & ",  " _
                                          & SetInteger(i + 1) & ",  " _
                                          & SetIntegerDB(dr_ac("ac_id")) & ",  " _
                                          & SetIntegerDB(0) & ",  " _
                                          & SetIntegerDB(0) & ",  " _
                                          & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " K ") & ",  " _
                                          & SetDbl(0) & ",  " _
                                          & SetDbl(dr_ac("ac_value")) & ",  " _
                                          & SetSetring("") & ",  " _
                                          & SetSetring("MONTH END") & ",  " _
                                          & SetSetring("Y") & ",  " _
                                          & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                          & ")"

                                        ssqls.Add(ssql)

                                        'update glbal
                                        If update_gl_bal(ssqls, dr_gl_cal("gcal_oid").ToString, _en, dr_ac("ac_id"), "C", dr_ac("ac_value"), _
                                                         dr_ac("ac_cu_id"), 1, True) = False Then
                                            Exit Sub
                                        End If
                                    Else

                                        'insert debet (akun tujuan (akun pendapatan di debet berarti berkurang))
                                        ssql = "INSERT INTO  " _
                                         & "  public.glt_det " _
                                         & "( " _
                                         & "  glt_oid, " _
                                         & "  glt_dom_id, " _
                                         & "  glt_en_id, " _
                                         & "  glt_add_by, " _
                                         & "  glt_add_date, " _
                                         & "  glt_code, " _
                                         & "  glt_date, " _
                                         & "  glt_type, " _
                                         & "  glt_cu_id, " _
                                         & "  glt_exc_rate, " _
                                         & "  glt_seq, " _
                                         & "  glt_ac_id, " _
                                         & "  glt_sb_id, " _
                                         & "  glt_cc_id, " _
                                         & "  glt_desc, " _
                                         & "  glt_debit, " _
                                         & "  glt_credit, " _
                                         & "  glt_ref_trans_code, " _
                                         & "  glt_daybook, " _
                                         & "  glt_posted, " _
                                         & "  glt_dt " _
                                         & ")  " _
                                         & "VALUES ( " _
                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                         & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                         & SetInteger(_en) & ",  " _
                                         & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                         & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                         & SetSetring(_glt_code) & ",  " _
                                         & SetDate(dr_gl_cal("gcal_end_date")) & ",  " _
                                         & SetSetring("JE") & ",  " _
                                         & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                                         & SetDbl(1) & ",  " _
                                         & SetInteger(i + 1) & ",  " _
                                         & SetIntegerDB(dr_ac("ac_id")) & ",  " _
                                         & SetIntegerDB(0) & ",  " _
                                         & SetIntegerDB(0) & ",  " _
                                         & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " D ") & ",  " _
                                         & SetDbl(dr_ac("ac_value")) & ",  " _
                                         & SetDbl(0) & ",  " _
                                         & SetSetring("") & ",  " _
                                         & SetSetring("MONTH END") & ",  " _
                                         & SetSetring("Y") & ",  " _
                                         & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                         & ")"

                                        ssqls.Add(ssql)

                                        'update glbal
                                        If update_gl_bal(ssqls, dr_gl_cal("gcal_oid").ToString, _en, dr_ac("ac_id"), "D", dr_ac("ac_value"), _
                                                         dr_ac("ac_cu_id"), 1, True) = False Then
                                            Exit Sub
                                        End If


                                        'insert kredit akun laba di kredit berarti bertambah

                                        ssql = "INSERT INTO  " _
                                         & "  public.glt_det " _
                                         & "( " _
                                         & "  glt_oid, " _
                                         & "  glt_dom_id, " _
                                         & "  glt_en_id, " _
                                         & "  glt_add_by, " _
                                         & "  glt_add_date, " _
                                         & "  glt_code, " _
                                         & "  glt_date, " _
                                         & "  glt_type, " _
                                         & "  glt_cu_id, " _
                                         & "  glt_exc_rate, " _
                                         & "  glt_seq, " _
                                         & "  glt_ac_id, " _
                                         & "  glt_sb_id, " _
                                         & "  glt_cc_id, " _
                                         & "  glt_desc, " _
                                         & "  glt_debit, " _
                                         & "  glt_credit, " _
                                         & "  glt_ref_trans_code, " _
                                         & "  glt_daybook, " _
                                         & "  glt_posted, " _
                                         & "  glt_dt " _
                                         & ")  " _
                                         & "VALUES ( " _
                                         & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                         & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                         & SetInteger(_en) & ",  " _
                                         & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                         & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                         & SetSetring(_glt_code) & ",  " _
                                         & SetDate(dr_gl_cal("gcal_end_date")) & ",  " _
                                         & SetSetring("JE") & ",  " _
                                         & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                                         & SetDbl(1) & ",  " _
                                         & SetInteger(i) & ",  " _
                                         & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                                         & SetIntegerDB(0) & ",  " _
                                         & SetIntegerDB(0) & ",  " _
                                         & SetSetringDB("End Month Adjusment 2, " & Format(i, "000") & " K " & dr_ac("ac_code") & " " & Microsoft.VisualBasic.Left(dr_ac("ac_name").ToString, 25)) & ",  " _
                                         & SetDbl(0) & ",  " _
                                         & SetDbl(dr_ac("ac_value")) & ",  " _
                                         & SetSetring("") & ",  " _
                                         & SetSetring("MONTH END") & ",  " _
                                         & SetSetring("Y") & ",  " _
                                         & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                         & ")"

                                        ssqls.Add(ssql)

                                        'update gl bal
                                        If update_gl_bal(ssqls, dr_gl_cal("gcal_oid").ToString, _en, dr_laba_berjalan("ac_id"), "C", dr_ac("ac_value"), _
                                                         dr_laba_berjalan("ac_cu_id"), 1) = False Then

                                            Exit Sub
                                        End If
                                    End If
                                    i += 1
                                End If
                                n += 1
                                LblProses.Text = "Akun yang di proses " & n & " " & dr("en_desc") & " " & dr_ac("ac_code") & " " & dr_ac("ac_name") & " " & dt_ac.Rows.Count
                                System.Windows.Forms.Application.DoEvents()
                            Next
                        Next

                        'Next


                        'pemindahan open + posted ke opening bulan berikutnya
                        'If ask("Anda yakin akan memproses laba berjalan?", "Konfirmasi...") Then
                        'SaveArraytoFile(ssqls)


                        If master_new.PGSqlConn.status_sync = True Then
                            'DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                'Return False
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                'Return False
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If
                        'Box("Proses Step 1 sucess")

                        LblProses.Text = "Status : Closing, Step 1 Success."
                        System.Windows.Forms.Application.DoEvents()

                        'Else
                        'Exit Sub
                        'End If

                        'Exit Sub

                        LblProses.Text = "Status : Closing, Copy Ending Balance to next periode."
                        System.Windows.Forms.Application.DoEvents()

                        'pemindahan open + posted ke opening bulan berikutnya
                        'looping entity

                        Dim dt_glbal As New DataTable

                        Dim dr_gcal As DataRow = get_next_gcal(dr_gl_cal("gcal_oid").ToString)

                        ssql = "update glbal_balance set glbal_balance_open=0 ," _
                               & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                               & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                               & " where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                               & "  and glbal_gcal_oid='" & dr_gcal("gcal_oid").ToString & "'"

                        ssqls.Add(ssql)

                        _level = 2

                        For Each dr As DataRow In dt.Rows
                            ssql = "SELECT  " _
                               & "  a.ac_id, " _
                               & "  a.ac_code, " _
                               & "  a.ac_name, " _
                               & "  a.ac_type, " _
                               & "  a.ac_is_sumlevel, " _
                               & "  a.ac_sign, " _
                               & "  a.ac_cu_id, " _
                               & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                                   & ", " & dr("en_id") & ", cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid), 'Y') as ac_value " _
                               & "FROM " _
                               & "  public.ac_mstr a " _
                               & "WHERE " _
                               & " ac_active='Y' and ac_is_sumlevel='N' and ac_id<>0 order by ac_type,ac_id,ac_code"

                            Dim dt_ac As New DataTable
                            dt_ac = GetTableData(ssql)

                            Dim x As Integer = 0

                            ssql = "select glbal_oid,glbal_ac_id from glbal_balance where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                                      & " and glbal_en_id=" & dr("en_id") & " and glbal_gcal_oid='" & dr_gcal("gcal_oid").ToString _
                                      & "' order by glbal_ac_id "

                            dt_glbal = GetTableData(ssql)

                            Dim _ada As Boolean = False
                            For Each dr_ac As DataRow In dt_ac.Rows
                                _ada = False
                                For Each dr_glbal As DataRow In dt_glbal.Rows
                                    If dr_ac("ac_id") = dr_glbal("glbal_ac_id") Then
                                        _ada = True

                                        ssql = "update glbal_balance set glbal_balance_open=coalesce(" & SetDec(dr_ac("ac_value")) _
                                           & ",0),  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                           & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                                           & " where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                                           & " and glbal_en_id=" & dr("en_id") & " and glbal_gcal_oid='" & dr_gcal("gcal_oid").ToString _
                                           & "' and glbal_ac_id=" & dr_ac("ac_id")

                                        ssqls.Add(ssql)

                                        Exit For
                                    End If

                                Next

                                If _ada = False Then

                                    ssql = "INSERT INTO  " _
                                            & "  public.glbal_balance " _
                                            & "( " _
                                            & "  glbal_oid, " _
                                            & "  glbal_dom_id, " _
                                            & "  glbal_en_id, " _
                                            & "  glbal_add_by, " _
                                            & "  glbal_add_date, " _
                                            & "  glbal_gcal_oid, " _
                                            & "  glbal_ac_id, " _
                                            & "  glbal_sb_id, " _
                                            & "  glbal_cc_id, " _
                                            & "  glbal_cu_id, " _
                                            & "  glbal_balance_open, " _
                                            & "  glbal_balance_unposted, " _
                                            & "  glbal_balance_posted, " _
                                            & "  glbal_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(dr("en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetSetring(dr_gcal("gcal_oid").ToString) & ",  " _
                                            & SetInteger(dr_ac("ac_id")) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                                            & SetDbl(dr_ac("ac_value")) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & ")"

                                    ssqls.Add(ssql)
                                End If
                                x += 1
                                LblProses.Text = "Akun yang di proses " & x & " " & dr_ac("ac_code") & " " & dr_ac("ac_name") & " " & dt_ac.Rows.Count & " " & dr("en_desc")
                                System.Windows.Forms.Application.DoEvents()
                            Next
                        Next


                        'If ask("Anda yakin akan memproses step 2 (pemindahan kepala 51 ke WIP)?", "Konfirmasi...") Then
                        If master_new.PGSqlConn.status_sync = True Then
                            'DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                'Return False
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                'Return False
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If
                        'Box("Proses Closing tahap pembuatan Opening Balance Sukses")
                        System.Windows.Forms.Application.DoEvents()

                        LblProses.Text = "Status : Closing, Copy Ending Balance to next periode. Success"
                        System.Windows.Forms.Application.DoEvents()
                        'Else
                        'Exit Sub
                        'End If

                        'perubahan status di gcal_det
                        For Each dr As DataRow In dt.Rows
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
                                    & "WHERE gcald_gcal_oid = '" & dr_gl_cal("gcal_oid").ToString & "' " _
                                    & " and gcald_en_id = " & dr("en_id")

                            ssqls.Add(ssql)
                        Next


                        'Dim tanggal_awal As Date
                        'tanggal_awal = DateAdd(DateInterval.Day, 1, CDate(le_glperiode.GetColumnValue("gcal_end_date")))

                        LblProses.Text = "Status : Closing, Create Profit next periode."
                        System.Windows.Forms.Application.DoEvents()

                        'If get_next_gcal(le_glperiode.EditValue, dt_gcal) = False Then
                        '    Box("Next GL Calender not found")
                        '    Exit Sub
                        'End If

                        '_level = 1
                        'pembuatan jurnal otomatis uktuk awal bulan
                        'For Each dr As DataRow In dt.Rows

                        _level = 2

                        For Each dr As DataRow In dt.Rows
                            _en = dr("en_id")

                            ssql = "SELECT  " _
                              & "  a.ac_id, " _
                              & "  a.ac_code, " _
                              & "  a.ac_name, " _
                              & "  a.ac_type, " _
                              & "  a.ac_is_sumlevel, " _
                              & "  a.ac_sign, " _
                              & "  a.ac_cu_id, " _
                              & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                                  & ", " & _en & ", cast('" & dr_gl_cal("gcal_oid").ToString & "' as uuid), 'Y') as ac_value " _
                              & "FROM " _
                              & "  public.ac_mstr a " _
                              & "WHERE " _
                              & "  ac_id=" & dr_laba_berjalan(0) & ""

                            Dim dt_ac As New DataTable
                            dt_ac = GetTableData(ssql)


                            _glt_code = func_coll.get_transaction_number("JE", dr("en_code"), "glt_det", "glt_code")

                            Dim i As Integer = 0
                            For Each dr_ac In dt_ac.Rows
                                '(akun tujuan (laba berjalan))

                                ssql = "INSERT INTO  " _
                                 & "  public.glt_det " _
                                 & "( " _
                                 & "  glt_oid, " _
                                 & "  glt_dom_id, " _
                                 & "  glt_en_id, " _
                                 & "  glt_add_by, " _
                                 & "  glt_add_date, " _
                                 & "  glt_code, " _
                                 & "  glt_date, " _
                                 & "  glt_type, " _
                                 & "  glt_cu_id, " _
                                 & "  glt_exc_rate, " _
                                 & "  glt_seq, " _
                                 & "  glt_ac_id, " _
                                 & "  glt_sb_id, " _
                                 & "  glt_cc_id, " _
                                 & "  glt_desc, " _
                                 & "  glt_debit, " _
                                 & "  glt_credit, " _
                                 & "  glt_ref_trans_code, " _
                                 & "  glt_daybook, " _
                                 & "  glt_posted, " _
                                 & "  glt_dt " _
                                 & ")  " _
                                 & "VALUES ( " _
                                 & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                 & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                 & SetInteger(_en) & ",  " _
                                 & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                 & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                 & SetSetring(_glt_code) & ",  " _
                                 & SetDate(dr_gcal("gcal_start_date")) & ",  " _
                                 & SetSetring("JE") & ",  " _
                                 & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                                 & SetDbl(1) & ",  " _
                                 & SetInteger(i) & ",  " _
                                 & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                                 & SetIntegerDB(0) & ",  " _
                                 & SetIntegerDB(0) & ",  " _
                                 & SetSetringDB("End Month Adjusment 3, D ") & ",  " _
                                 & SetDbl(dr_ac("ac_value")) & ",  " _
                                 & SetDbl(0) & ",  " _
                                 & SetSetring("") & ",  " _
                                 & SetSetring("MONTH END") & ",  " _
                                 & SetSetring("Y") & ",  " _
                                 & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                 & ")"

                                ssqls.Add(ssql)

                                'update gl bal
                                If update_gl_bal(ssqls, dr_gcal("gcal_oid").ToString, _en, dr_laba_berjalan("ac_id"), "D", dr_ac("ac_value"), _
                                                 dr_laba_berjalan("ac_cu_id"), 1) = False Then

                                    Exit Sub
                                End If

                                'insert kredit
                                ssql = "INSERT INTO  " _
                                  & "  public.glt_det " _
                                  & "( " _
                                  & "  glt_oid, " _
                                  & "  glt_dom_id, " _
                                  & "  glt_en_id, " _
                                  & "  glt_add_by, " _
                                  & "  glt_add_date, " _
                                  & "  glt_code, " _
                                  & "  glt_date, " _
                                  & "  glt_type, " _
                                  & "  glt_cu_id, " _
                                  & "  glt_exc_rate, " _
                                  & "  glt_seq, " _
                                  & "  glt_ac_id, " _
                                  & "  glt_sb_id, " _
                                  & "  glt_cc_id, " _
                                  & "  glt_desc, " _
                                  & "  glt_debit, " _
                                  & "  glt_credit, " _
                                  & "  glt_ref_trans_code, " _
                                  & "  glt_daybook, " _
                                  & "  glt_posted, " _
                                  & "  glt_dt " _
                                  & ")  " _
                                  & "VALUES ( " _
                                  & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                  & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                  & SetInteger(_en) & ",  " _
                                  & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                  & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                  & SetSetring(_glt_code) & ",  " _
                                  & SetDate(dr_gcal("gcal_start_date")) & ",  " _
                                  & SetSetring("JE") & ",  " _
                                  & SetInteger(dr_laba_ditahan("ac_cu_id")) & ",  " _
                                  & SetDbl(1) & ",  " _
                                  & SetInteger(i + 1) & ",  " _
                                  & SetIntegerDB(dr_laba_ditahan("ac_id")) & ",  " _
                                  & SetIntegerDB(0) & ",  " _
                                  & SetIntegerDB(0) & ",  " _
                                  & SetSetringDB("End Month Adjusment 3, K ") & ",  " _
                                  & SetDbl(0) & ",  " _
                                  & SetDbl(dr_ac("ac_value")) & ",  " _
                                  & SetSetring("") & ",  " _
                                  & SetSetring("MONTH END") & ",  " _
                                  & SetSetring("Y") & ",  " _
                                  & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                  & ")"

                                ssqls.Add(ssql)

                                'update glbal
                                If update_gl_bal(ssqls, dr_gcal("gcal_oid").ToString, _en, dr_laba_ditahan("ac_id"), "C", dr_ac("ac_value"), _
                                                 dr_laba_ditahan("ac_cu_id"), 1) = False Then
                                    Exit Sub
                                End If

                            Next
                        Next



                        ssql = "update public.gcal_mstr set  gcal_closing='Y' where gcal_oid=" & SetSetring(dr_gl_cal("gcal_oid").ToString)

                        ssqls.Add(ssql)


                        If master_new.PGSqlConn.status_sync = True Then
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then
                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If
                        'Box("Proses Closing tahap pembuatan Jurnal Laba Berjalan ke Laba Ditahan Sukses")
                        LblProses.Text = "Status : Closing, End Month Success."
                        'Box("Closing, End Month Success.")
                        System.Windows.Forms.Application.DoEvents()

                    Catch ex As Exception
                        Pesan(Err)
                    End Try


                Next

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
