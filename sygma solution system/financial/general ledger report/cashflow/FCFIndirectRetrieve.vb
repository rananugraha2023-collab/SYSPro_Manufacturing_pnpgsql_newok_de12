Imports npgsql
Imports master_new.ModFunction

Public Class FCFIndirectRetrieve
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim ds_glt As DataSet
    Dim ds_group_glt As DataSet
    Dim ds_cashflow As DataSet
    Dim _is_setara_kas As Boolean = False

    Private Sub FCFIndirectRetrieve_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        'help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        'add_groupsummary()
        'AllowIncrementalSearch()
        set_component()
        'load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False
    End Sub


    Public Overrides Sub load_cb()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())
        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "cfid_oid", False)
        add_column(gv_master, "cfid_cfidruled_oid", False)
        'add_column_copy(gv_master, "Sequence", "cfdrule_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Sum of Rule", "cfdrule_is_sum", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Group Header", "cfidrule_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group SubHeader", "cfidrule_subheader", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Footer", "cfidrule_footer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "cfid_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Remarks", "cfidrule_remarks", DevExpress.Utils.HorzAlignment.Default)
        
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        
        Dim level, dom, en As Integer
        Dim ssqls As New ArrayList
        Dim _new_guid As String

        _new_guid = Guid.NewGuid.ToString

        dom = master_new.ClsVar.sdom_id
        en = 0
        level = 1

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

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO cfid_report  " _
                                            & "SELECT  " _
                                            & SetSetring(_new_guid.ToString) & ", " _
                                            & "cfidruled_oid, " _
                                            & SetSetring(le_periode.EditValue) & ", " _
                                            & "f_get_cashflow_indirect( ac_id, " & level & ", " & dom & ", " & en & ", cast('" & le_periode.EditValue & "' as uuid),coalesce(cfidrule_is_invert,'N')) as invert, " _
                                            & SetInteger(master_new.ClsVar.sUserID) & "  " _
                                            & " from cfidruled_det  " _
                                            & " INNER JOIN ac_mstr on ac_id = cfidruled_ac_id  " _
                                            & " INNER JOIN cfidrule_mstr on cfidrule_oid = cfidruled_cfidrule_oid  "

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

        Dim _cfid_nett_text As String = func_coll.get_conf_file("text_cfid_nett")
        Dim _cfid_start_period As String = func_coll.get_conf_file("text_cfid_start_period")
        Dim _cfid_end_period As String = func_coll.get_conf_file("text_cfid_end_period")

        ds_cashflow = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  public.cfid_report.cfid_oid, " _
                            & "  public.cfid_report.cfid_cfidruled_oid, " _
                            & "  public.cfid_report.cfid_gcal_oid, " _
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
                            & "  where cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID) _
                        & " UNION  " _
                            & " SELECT  " _
                            & SetSetring(_new_guid.ToString) & " as cfid_oid, " _
                            & " null as cfid_cfidruled_oid, " _
                            & SetSetring(le_periode.EditValue) & " as cfid_gcal_oid, " _
                            & "  101 as cfidrule_seq, " _
                            & "  'SUMMARY' as cfidrule_header, " _
                            & "  null as cfidrule_subheader, " _
                            & "  101 as cfidruled_seq, " _
                            & SetSetring(_cfid_nett_text) & " as ac_name, " _
                            & "  (SELECT sum(cfid_amount) from cfid_report where cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID) & " ) as cfid_amount, " _
                            & "  null as cfidrule_footer " _
                        & " UNION  " _
                            & " SELECT  " _
                            & SetSetring(_new_guid.ToString) & " as cfid_oid, " _
                            & " null as cfid_cfidruled_oid, " _
                            & SetSetring(le_periode.EditValue) & " as cfid_gcal_oid, " _
                            & "  102 as cfidrule_seq, " _
                            & "  'SUMMARY' as cfidrule_header, " _
                            & "  null as cfidrule_subheader, " _
                            & "  102 as cfidruled_seq, " _
                            & SetSetring(_cfid_start_period) & " as ac_name, " _
                            & " (SELECT sum(glbal_balance_open) from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " and ac_is_cf = 'Y') as cfid_amount, " _
                            & "  null as cfidrule_footer " _
                        & " UNION  " _
                            & " SELECT  " _
                            & SetSetring(_new_guid.ToString) & " as cfid_oid, " _
                            & " null as cfid_cfidruled_oid, " _
                            & SetSetring(le_periode.EditValue) & " as cfid_gcal_oid, " _
                            & "  103 as cfidrule_seq, " _
                            & "  'SUMMARY' as cfidrule_header, " _
                            & "  null as cfidrule_subheader, " _
                            & "  103 as cfidruled_seq, " _
                            & SetSetring(_cfid_end_period) & " as ac_name, " _
                            & " ((SELECT sum(cfid_amount) from cfid_report where cfid_user_id = " & SetInteger(master_new.ClsVar.sUserID) & " ) + (select sum(glbal_balance_open) from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " and ac_is_cf = 'Y')) as cfid_amount, " _
                            & "  null as cfidrule_footer " _
                        & " ORDER BY cfidrule_seq , cfidruled_seq  "
                            

                    .InitializeCommand()
                    .FillDataSet(ds_cashflow, "direct_cashflow")
                    gc_master.DataSource = ds_cashflow.Tables(0)
                    gv_master.BestFitColumns()
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
        '---------

    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            gc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
