Imports npgsql
Imports master_new.ModFunction

Public Class FYearEnd
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection


    Private Sub FYearEnd_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_gcal_mstr())
        'le_glperiode.Properties.DataSource = dt_bantu
        'le_glperiode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        'le_glperiode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        'le_glperiode.ItemIndex = 0

        init_le(le_entity, "en_mstr")
        init_le(le_glperiode, "gcal_mstr")

    End Sub

    Private Function before_close() As Boolean
        before_close = True

        Dim _jml As Integer
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(glt_posted) as jml from glt_det where glt_posted ~~* 'N' " + _
                                           " and glt_en_id = " + le_entity.EditValue.ToString + _
                                           " and glt_date <= '" + le_glperiode.GetColumnValue("gcal_end_date") + "'"
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

        If _jml > 0 Then
            MessageBox.Show("Unposted Transaction Still Exist.." + Chr(13) + "Please Transaction Post To All Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_close
    End Function

    Private Sub sb_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_close.Click
        If MessageBox.Show("Close Transaction For This Periode...", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If before_close() = False Then
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        Dim i, _dom_pl_ac, _dom_re_ac As Integer
        Dim ds_bantu As New DataSet
        Dim ssqls As New ArrayList

        'untuk mencari account profit/loss dan account retained earnings
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select dom_pl_ac, dom_re_ac from dom_mstr " + _
                                           " where dom_id = " + master_new.ClsVar.sdom_id
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _dom_pl_ac = .DataReader("dom_pl_ac").ToString
                        _dom_re_ac = .DataReader("dom_re_ac").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

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
                        & "  ac_type, " _
                        & "  glbal_dt " _
                        & "FROM  " _
                        & "  public.glbal_balance" _
                        & "  inner join ac_mstr on ac_id = glbal_ac_id " _
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

        Dim _glbal_balance_open As Double = 0
        Dim _dom_pl_ac_value As Double = 0

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
                                            & "  gcald_year = 'Y', " _
                                            & "  gcal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE gcald_gcal_oid = '" & le_glperiode.EditValue.ToString & "' " _
                                            & " and gcald_en_id = " & le_entity.EditValue.ToString
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'mencari nilai profit/loss account untuk di pindahkan ke retained earnings account
                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            If ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id") = _dom_pl_ac Then
                                _dom_pl_ac_value = ds_bantu.Tables(0).Rows(i).Item("glbal_balance_posted")
                                Exit For
                            End If
                        Next

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            'kalau type nya R atau E maka kalau tutup tahun harus kembali ke 0
                            'tetapi kalau selain R dan E itu harus perhitungan antara open + posted
                            If ds_bantu.Tables(0).Rows(i).Item("ac_type").ToString.ToUpper = "R" Then
                                _glbal_balance_open = 0
                            ElseIf ds_bantu.Tables(0).Rows(i).Item("ac_type").ToString.ToUpper = "E" Then
                                _glbal_balance_open = 0
                            Else
                                _glbal_balance_open = ds_bantu.Tables(0).Rows(i).Item("glbal_balance_open") + ds_bantu.Tables(0).Rows(i).Item("glbal_balance_posted")
                            End If

                            'kalau account ini adalah account profit/loss maka harus juga 0
                            'kalau account ini adalah account retained earnings maka harus diambil nilainya dari account profit/loss
                            If ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id").ToString.ToUpper = _dom_pl_ac Then
                                _glbal_balance_open = 0
                            ElseIf ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id").ToString.ToUpper = _dom_re_ac Then
                                _glbal_balance_open = _dom_pl_ac_value
                            End If

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
                                                & SetDbl(_glbal_balance_open) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
End Class
