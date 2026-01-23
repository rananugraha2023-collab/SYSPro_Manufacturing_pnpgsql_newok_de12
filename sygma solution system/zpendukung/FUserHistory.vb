Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FUserHistory
    Dim ssql As String
    Dim _wo_oid_master As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Public ds_edit As DataSet

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        start_date.DateTime = Now.ToString("dd/MM/yyyy 00:00:00")
        end_date.DateTime = Now.ToString("dd/MM/yyyy 23:59:59")

        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True

    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column_copy(gv_master, "User Syspro", "usernama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Application", "userac_app", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Login", "userac_login_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Date Logout", "userac_logout_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Computer Name", "userac_computer_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Name", "userac_user_computer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IP Address", "userac_ip_address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Syspro Version", "userac_syspro_version", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_act, "User Syspro", "user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_act, "Date Activity", "date_activity", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_act, "Activity", "activity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_act, "Detail User", "detail_user", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_pc, "PC Name", "pc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_pc, "PC User", "pc_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_pc, "User Syspro", "pc_syspro", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_pc, "Bios Date", "pc_bios_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_pc, "pc_detail", False)



    End Sub

    Public Overrides Sub find()
        Try
            ds = New DataSet
            Using objload As New master_new.WDABasepgsql("", "")
                With objload
                    If xtc_master.SelectedTabPageIndex = 0 Then
                        .SQL = "SELECT  " _
                               & "  a.userac_user_syspro,coalesce(userac_app,'SYSPRO') as userac_app, " _
                               & "  a.userac_login_date, " _
                               & "  a.userac_logout_date, " _
                               & "  a.userac_computer_name, " _
                               & "  a.userac_user_computer, " _
                               & "  a.userac_ip_address, " _
                               & "  a.userac_syspro_version,b.usernama " _
                               & "FROM " _
                               & "   public.userac_accs a  INNER JOIN public.tconfuser b ON (a.userac_user_id = b.userid) " _
                               & "WHERE " _
                               & "  a.userac_login_date BETWEEN " & SetDateNTime(start_date.DateTime) & " AND " & SetDateNTime(end_date.DateTime) _
                               & " order by userac_login_date desc"

                        .InitializeCommand()
                        .FillDataSet(ds, Me.Name + "_select")
                        gc_master.DataSource = ds.Tables(0)
                        gv_master.BestFitColumns()

                        'BindingContext(ds.Tables(0)).Position = row
                        'bestfit_column()
                        'load_data_grid_detail()
                        'ConditionsAdjustment()
                    ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                        .SQL = "SELECT  " _
                            & "  a.date_activity, " _
                            & "  a.user_id, " _
                            & "  a.activity, " _
                            & "  a.detail_user " _
                            & "FROM " _
                            & "  public.useraccd_dml a " _
                            & "WHERE " _
                            & "  a.date_activity BETWEEN " & SetDateNTime(start_date.DateTime) & " AND " & SetDateNTime(end_date.DateTime) _
                            & " order by date_activity desc"

                        .InitializeCommand()
                        .FillDataSet(ds, Me.Name + "_select")
                        gc_act.DataSource = ds.Tables(0)
                        gv_act.BestFitColumns()

                    ElseIf xtc_master.SelectedTabPageIndex = 3 Then
                        .SQL = "SELECT  " _
                           & "  a.pc_oid, " _
                           & "  a.pc_name, " _
                           & "  a.pc_user, " _
                           & "  a.pc_syspro,pc_detail, '' as pc_bios_date " _
                           & "FROM " _
                           & "  public.pc_mstr a " _
                           & "WHERE " _
                           & " 1=1  " _
                           & " order by pc_syspro"

                        .InitializeCommand()
                        .FillDataSet(ds, "pc")
                        For Each dr As DataRow In ds.Tables("pc").Rows
                            Try
                                If (dr("pc_detail").ToString.IndexOf("@Win32_Bios$ReleaseDate") > 0) Then
                                    dr("pc_bios_date") = dr("pc_detail").ToString.Substring(dr("pc_detail").ToString.IndexOf("@Win32_Bios$ReleaseDate") + 24, 8)
                                End If

                            Catch ex As Exception

                            End Try



                        Next
                        ds.AcceptChanges()

                        gc_pc.DataSource = ds.Tables("pc")

                       
                        gv_pc.BestFitColumns()
                    End If
                   
                End With

            End Using

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    'Public Overrides Function get_sequel() As String
    '    get_sequel = "SELECT  " _
    '        & "  a.userac_user_syspro, " _
    '        & "  a.userac_login_date, " _
    '        & "  a.userac_logout_date, " _
    '        & "  a.userac_computer_name, " _
    '        & "  a.userac_user_computer, " _
    '        & "  a.userac_ip_address, " _
    '        & "  a.userac_syspro_version,b.usernama " _
    '        & "FROM " _
    '        & "   public.userac_accs a  INNER JOIN public.tconfuser b ON (a.userac_user_id = b.userid) " _
    '        & "WHERE " _
    '        & "  a.userac_login_date BETWEEN " & SetDateNTime(start_date.DateTime) & " AND " & SetDateNTime(end_date.DateTime) _
    '        & " order by userac_login_date"

    '    Return get_sequel
    'End Function

    Public Overrides Sub insert_data_awal()
        Box("This menu not available")
        Exit Sub
       
    End Sub

    Public Overrides Function insert() As Boolean
        Try
            insert = False

        Catch ex As Exception
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        Box("This menu not available")
        Return False
        Exit Function

    End Function

    Public Overrides Function edit()

        Try
            edit = False

        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        Box("This menu not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        Return before_save
    End Function

   
    Private Sub gv_pc_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_pc.SelectionChanged
        Try
            pc_detail.Text = gv_pc.GetFocusedRowCellValue("pc_detail").ToString
        Catch ex As Exception

        End Try
    End Sub
End Class
