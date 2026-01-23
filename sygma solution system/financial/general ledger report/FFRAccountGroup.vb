Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports npgsql
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class FFRAccountGroup
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Dim ssql As String
    Dim _session As String

    Private Sub FFinancialReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _now = func_coll.get_now
            de_from_first.DateTime = _now
            de_from_end.DateTime = _now
            form_first_load()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub load_cb()
        init_le(le_from_gcal, "gcal_mstr")
        init_le(le_to_gcal, "gcal_mstr")
        init_le(le_report, "fs_report_mstr")
        load_cb_en()
    End Sub

    Private Sub load_cb_en()
        dt_bantu = New DataTable
        Using ds_bantu As New DataSet()

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select xfs1_desc from xfs1_mstr where xfs1_type = 'FS' " + _
                           " and xfs1_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " order by xfs1_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "xfs")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using

        le_report.Properties.DataSource = dt_bantu
        le_report.Properties.DisplayMember = dt_bantu.Columns("xfs1_desc").ToString
        le_report.Properties.ValueMember = dt_bantu.Columns("xfs1_desc").ToString
        le_report.ItemIndex = 0
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        If arg <> False Then
            Dim ds_show As DataSet
            Dim ds_account As DataSet

            ds_show = New DataSet
            ds_account = New DataSet

            _session = Guid.NewGuid.ToString
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        'If le_entity.EditValue = 0 Then

                        .SQL = "SELECT  " _
                        & "  gcal_year,gcal_periode, " _
                        & "  CASE gcal_periode when 1 then '01#Januari' when 2 then '02#Februari' when 3 then '03#Maret' when 4 then '04#April' " _
                        & "  when 5 then '05#Mei' when 6 then '06#Juni' when 7 then '07#Juli' when 8 then '08#Agustus' when 9 then '09#September' " _
                        & "  when 10 then '10#Oktober' when 11 then '11#November' when 12 then '12#Desember' else '' end as periode, " _
                        & "  glbal_ac_id,ac_code,ac_name, " _
                        & "  glbal_cc_id,cc_desc, " _
                        & "  glbal_cu_id,cu_name, " _
                        & "  sum(coalesce(glbal_balance_open,0) + coalesce(glbal_balance_unposted,0) + " _
                        & "  coalesce(glbal_balance_posted,0) + coalesce(glbal_balance_posted_end_month,0) + coalesce(glbal_balance_end_month1,0)) as glbal_balance_end " _
                        & " FROM  " _
                        & "  public.glbal_balance " _
                        & "  inner join ac_mstr on ac_id = glbal_ac_id " _
                        & "  inner join cc_mstr on cc_id = glbal_cc_id " _
                        & "  inner join cu_mstr on cu_id = glbal_cu_id " _
                        & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
                        & "  WHERE glbal_ac_id in (select xfs1d_ac_id from xfs1d_det " _
                                                 & " inner join xfs1_mstr on xfs1_oid = xfs1d_xfs1_oid " _
                                                 & " where xfs1_type = 'FS' " _
                                                 & " and xfs1_desc = " & SetSetring(le_report.Text) _
                                                 & " order by xfs1d_seq asc) " _
                        & "  AND glbal_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) _
                        & "  AND (gcal_year >= " & SetInteger(le_from_gcal.GetColumnValue("gcal_year")) _
                        & "  AND gcal_year <= " & SetInteger(le_to_gcal.GetColumnValue("gcal_year")) & ") " _
                        & "  AND (gcal_periode >= " & SetInteger(le_from_gcal.GetColumnValue("gcal_periode")) _
                        & "  AND gcal_periode <= " & SetInteger(le_to_gcal.GetColumnValue("gcal_periode")) & ") " _
                        & "  group by gcal_year, " _
                        & "  gcal_periode, glbal_ac_id,ac_code,ac_name, " _
                        & "  glbal_cu_id,cu_name, " _
                        & "  glbal_cc_id,cc_desc " _
                        & "  order by gcal_year,gcal_periode asc "

                        'Else
                        '    .SQL = "SELECT  " _
                        '    & "  glbal_en_id, " _
                        '    & "  gcal_year,gcal_periode, " _
                        '    & "  CASE gcal_periode when 1 then '01#Januari' when 2 then '02#Februari' when 3 then '03#Maret' when 4 then '04#April' " _
                        '    & "  when 5 then '05#Mei' when 6 then '06#Juni' when 7 then '07#Juli' when 8 then '08#Agustus' when 9 then '09#September' " _
                        '    & "  when 10 then '10#Oktober' when 11 then '11#November' when 12 then '12#Desember' else '' end as periode, " _
                        '    & "  glbal_ac_id,ac_code,ac_name, " _
                        '    & "  glbal_cc_id,cc_desc, " _
                        '    & "  glbal_cu_id,cu_name, " _
                        '    & "  coalesce(glbal_balance_open,0) + coalesce(glbal_balance_unposted,0) + coalesce(glbal_balance_posted,0) as glbal_balance_end " _
                        '    & " FROM  " _
                        '    & "  public.glbal_balance " _
                        '    & "  inner join ac_mstr on ac_id = glbal_ac_id " _
                        '    & "  inner join cc_mstr on cc_id = glbal_cc_id " _
                        '    & "  inner join cu_mstr on cu_id = glbal_cu_id " _
                        '    & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
                        '    & "  WHERE glbal_ac_id in (select xfs1d_ac_id from xfs1d_det " _
                        '                             & " inner join xfs1_mstr on xfs1_oid = xfs1d_xfs1_oid " _
                        '                             & " where xfs1_type = 'FS' and xfs1_en_id = " & SetInteger(le_entity.EditValue) _
                        '                             & " and xfs1_desc = " & SetSetring(le_report.Text) _
                        '                             & " order by xfs1d_seq asc) " _
                        '    & "  AND glbal_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) _
                        '    & "  AND glbal_en_id = " & SetInteger(le_entity.EditValue) _
                        '    & "  AND (gcal_year >= " & SetInteger(le_from_gcal.GetColumnValue("gcal_year")) _
                        '    & "  AND gcal_year <= " & SetInteger(le_to_gcal.GetColumnValue("gcal_year")) & ") " _
                        '    & "  AND (gcal_periode >= " & SetInteger(le_from_gcal.GetColumnValue("gcal_periode")) _
                        '    & "  AND gcal_periode <= " & SetInteger(le_to_gcal.GetColumnValue("gcal_periode")) & ") " _
                        '    & "  order by gcal_year,gcal_periode asc "
                        'End If

                        .InitializeCommand()
                        .FillDataSet(ds_show, "fs_report")
                        pgc_master.DataSource = ds_show
                        pgc_master.DataMember = "fs_report"
                        pgc_master.BestFit()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub le_from_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_from_gcal.EditValueChanged
        Try
            de_from_first.EditValue = le_from_gcal.GetColumnValue("gcal_start_date")
            de_from_end.EditValue = le_from_gcal.GetColumnValue("gcal_end_date")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button <> MouseButtons.Left OrElse e.Clicks > 1 Then
            Return
        End If

        Dim view As GridView = TryCast(sender, GridView)
        If view.State <> GridState.ColumnDown Then
            Return
        End If

        Dim p As Point = view.GridControl.PointToClient(MousePosition)
        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = view.CalcHitInfo(p)

        If info.HitTest = GridHitTest.Column Then
            DirectCast(e, DevExpress.Utils.DXMouseEventArgs).Handled = True
            Me.BeginInvoke(New MethodInvoker(AddressOf view.LayoutChanged))
        End If
    End Sub

    Private Sub le_to_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_to_gcal.EditValueChanged
        Try
            de_to_first.EditValue = le_to_gcal.GetColumnValue("gcal_start_date")
            de_to_end.EditValue = le_to_gcal.GetColumnValue("gcal_end_date")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
