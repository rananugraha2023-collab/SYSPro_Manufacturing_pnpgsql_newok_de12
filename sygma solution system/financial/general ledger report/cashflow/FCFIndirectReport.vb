Imports npgsql
Imports master_new.ModFunction

Public Class FCFIndirectReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ssql As String
    Dim ds_cashflow As DataSet

    Private Sub FCFIndirectReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _now = func_coll.get_now
            de_from_first.DateTime = _now
            de_from_end.DateTime = _now

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_dom_mstr())
            le_domain.Properties.DataSource = dt_bantu
            le_domain.Properties.DisplayMember = dt_bantu.Columns("dom_desc").ToString
            le_domain.Properties.ValueMember = dt_bantu.Columns("dom_id").ToString
            le_domain.ItemIndex = 0

            init_le(le_gcal_from, "gcal_mstr")
            init_le(le_entity, "en_mstr")
            init_le(le_gcal_to, "gcal_mstr")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()
        Dim level, dom, en As Integer
        Dim ssqls As New ArrayList
        Dim _new_guid As String

        _new_guid = Guid.NewGuid.ToString
        'Try
        dom = 0
        en = 0

        Dim ds_period As DataSet

        ds_period = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select gcal_oid, TRIM(to_char(date(gcal_start_date),'MONTH')) as _month from gcal_mstr " + _
                           " where gcal_start_date >= " + SetDate(le_gcal_from.GetColumnValue("gcal_start_date")) + _
                           " and gcal_end_date <= " + SetDate(le_gcal_to.GetColumnValue("gcal_end_date")) + _
                           " order by gcal_start_date "

                    .InitializeCommand()
                    .FillDataSet(ds_period, "direct_period")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


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

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cfid_report where cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For p As Integer = 0 To ds_period.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO cfid_report  " _
                                                & "SELECT  " _
                                                & SetSetring(_new_guid.ToString) & ", " _
                                                & "cfidruled_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(p).Item("gcal_oid")) & ", " _
                                                & "f_get_cashflow_indirect( ac_id, " & level & ", " & dom & ", " & en & ", cast('" & ds_period.Tables(0).Rows(p).Item("gcal_oid").ToString & "' as uuid),coalesce(cfidrule_is_invert,'N')) as invert, " _
                                                & SetInteger(master_new.ClsVar.sUserID) & "  " _
                                                & " from cfidruled_det  " _
                                                & " INNER JOIN ac_mstr on ac_id = cfidruled_ac_id  " _
                                                & " INNER JOIN cfidrule_mstr on cfidrule_oid = cfidruled_cfidrule_oid  "

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        sqlTran.Commit()


                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim _cfid_nett_text As String = func_coll.get_conf_file("text_cfid_nett")
        Dim _cfid_start_period As String = func_coll.get_conf_file("text_cfid_start_period")
        Dim _cfid_end_period As String = func_coll.get_conf_file("text_cfid_end_period")

        Dim _sql As String
        Dim _calc_start_month As Double = 0
        Dim _calc_per_month As Double = 0
        Dim _calc_end_month As Double = 0

        ds_cashflow = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    _sql = "SELECT  " _
                            & "  public.cfid_report.cfid_oid, " _
                            & "  public.cfid_report.cfid_cfidruled_oid, " _
                            & "  public.cfid_report.cfid_gcal_oid, " _
                            & "  TRIM(to_char(date(gcal_start_date),'MONTH')) as _month, " _
                            & "  public.cfidrule_mstr.cfidrule_seq, " _
                            & "  public.cfidrule_mstr.cfidrule_header, " _
                            & "  public.cfidrule_mstr.cfidrule_subheader, " _
                            & "  public.cfidruled_det.cfidruled_seq, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.cfid_report.cfid_amount, " _
                            & "  public.cfidrule_mstr.cfidrule_footer " _
                            & "FROM " _
                            & "  public.cfid_report " _
                            & "  INNER JOIN public.gcal_mstr ON (public.cfid_report.cfid_gcal_oid = public.gcal_mstr.gcal_oid) " _
                            & "  INNER JOIN public.cfidruled_det ON (public.cfid_report.cfid_cfidruled_oid = public.cfidruled_det.cfidruled_oid) " _
                            & "  INNER JOIN public.ac_mstr ON (public.cfidruled_det.cfidruled_ac_id = public.ac_mstr.ac_id) " _
                            & "  INNER JOIN public.cfidrule_mstr ON (public.cfidruled_det.cfidruled_cfidrule_oid = public.cfidrule_mstr.cfidrule_oid)" _
                            & "  where cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID)

                    Try
                        Using objcek As New master_new.WDABasepgsql("", "")
                            With objcek
                                .Connection.Open()
                                .Command = .Connection.CreateCommand
                                For q As Integer = 0 To ds_period.Tables(0).Rows.Count - 1
                                    If q = 0 Then

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "select sum(glbal_balance_open) as balance_open from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(ds_period.Tables(0).Rows(q).Item("gcal_oid")) & " and ac_is_cf = 'Y' "
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader

                                        While .DataReader.Read
                                            _calc_start_month = .DataReader("balance_open")
                                        End While

                                    End If

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "select sum(cfid_amount) as total_per_month from cfid_report WHERE cfid_gcal_oid = " & SetSetring(ds_period.Tables(0).Rows(q).Item("gcal_oid")) & " and cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID)
                                    .InitializeCommand()
                                    .DataReader = .Command.ExecuteReader

                                    While .DataReader.Read
                                        _calc_per_month = .DataReader("total_per_month")
                                    End While

                                    _calc_end_month = _calc_start_month + _calc_per_month

                                    _sql = _sql & " UNION  " _
                                                & " SELECT  " _
                                                & SetSetring(_new_guid.ToString) & " as cfid_oid, " _
                                                & " null as cfid_cfidruled_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(q).Item("gcal_oid")) & " as cfid_gcal_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(q).Item("_month")) & " as _month, " _
                                                & "  101 as cfidrule_seq, " _
                                                & "  'SUMMARY' as cfidrule_header, " _
                                                & "  null as cfidrule_subheader, " _
                                                & "  101 as cfidruled_seq, " _
                                                & SetSetring(_cfid_nett_text) & " as ac_name, " _
                                                & SetDblDB(_calc_per_month) & " as cfid_amount, " _
                                                & "  null as cfidrule_footer " _
                                            & " UNION  " _
                                                & " SELECT  " _
                                                & SetSetring(_new_guid.ToString) & " as cfid_oid, " _
                                                & " null as cfid_cfidruled_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(q).Item("gcal_oid")) & " as cfid_gcal_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(q).Item("_month")) & " as _month, " _
                                                & "  102 as cfidrule_seq, " _
                                                & "  'SUMMARY' as cfidrule_header, " _
                                                & "  null as cfidrule_subheader, " _
                                                & "  102 as cfidruled_seq, " _
                                                & SetSetring(_cfid_start_period) & " as ac_name, " _
                                                & SetDblDB(_calc_start_month) & " as cfid_amount, " _
                                                & "  null as cfidrule_footer " _
                                            & " UNION  " _
                                                & " SELECT  " _
                                                & SetSetring(_new_guid.ToString) & " as cfid_oid, " _
                                                & " null as cfid_cfidruled_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(q).Item("gcal_oid")) & " as cfid_gcal_oid, " _
                                                & SetSetring(ds_period.Tables(0).Rows(q).Item("_month")) & " as _month, " _
                                                & "  103 as cfidrule_seq, " _
                                                & "  'SUMMARY' as cfidrule_header, " _
                                                & "  null as cfidrule_subheader, " _
                                                & "  103 as cfidruled_seq, " _
                                                & SetSetring(_cfid_end_period) & " as ac_name, " _
                                                & SetDblDB(_calc_end_month) & " as cfid_amount, " _
                                                & "  null as cfidrule_footer " _
                                            & " ORDER BY cfidrule_seq , cfidruled_seq  "


                                    _calc_start_month = _calc_end_month   'kas awal bulan selanjutnya adalah kas akhir bulan sebelumnya

                                Next
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    .SQL = _sql

                    .InitializeCommand()
                    .FillDataSet(ds_cashflow, "direct_cashflow")

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'hapus lagi isi temporary tablenya
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cfid_report where cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



        Dim rpt As New XRCFIndirectReport
        Try
            With rpt

                If ds_cashflow.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doesn't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                '.DataSource = ds_bantu
                '.DataMember = "data"
                .xpg_data.DataSource = ds_cashflow
                .xpg_data.DataMember = "direct_cashflow"
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Personal Selling SDI Report"
                printTool.ShowPreview()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub le_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal_from.EditValueChanged
        Try
            de_from_first.EditValue = le_gcal_from.GetColumnValue("gcal_start_date")
            de_from_end.EditValue = le_gcal_from.GetColumnValue("gcal_end_date")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_gcal_to_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal_to.EditValueChanged
        Try
            de_to_first.EditValue = le_gcal_to.GetColumnValue("gcal_start_date")
            de_to_end.EditValue = le_gcal_to.GetColumnValue("gcal_end_date")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
