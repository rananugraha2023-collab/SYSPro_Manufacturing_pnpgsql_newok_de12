Imports master_new.PGSqlConn
Imports npgsql
Imports master_new.ModFunction

Public Class FRossetaStone
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Private Sub FInventoryReportDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("entity", ""))
        'le_en_id.Properties.DataSource = dt_bantu
        'le_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_en_id.ItemIndex = 0

        'init_le(le_periode, "en_id")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())

        If le_periode.Properties.Columns.VisibleCount = 0 Then
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
        End If

        le_periode.Properties.DropDownRows = 12
        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0

        'de_from.DateTime = Now.Date
        ' xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        format_grid()

    End Sub

    Private Sub le_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_periode.EditValueChanged
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_loc_mstr())
        'le_loc.Properties.DataSource = dt_bantu
        'le_loc.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        'le_loc.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        'le_loc.ItemIndex = 0
    End Sub

    Public Overrides Sub help_load_data(ByVal arg As Boolean)

        Cursor = Cursors.WaitCursor

        Dim ssql As String

        If arg <> False Then

            If xtc_master.SelectedTabPageIndex = 0 Then
                ssql = "SELECT  " _
                    & "  a.rosd_rosg_code, " _
                    & " a.rosd_rosg_code || ' ' || b.rosg_desc as rosg_desc, " _
                    & "  b.rosg_default_sign, " _
                    & "  a.rosd_rost_code, " _
                    & " a.rosd_rost_code || ' ' || c.rost_desc as rost_desc, " _
                    & "  c.rost_cf, " _
                    & "  a.rosd_amount, " _
                    & "  a.rosd_periode, " _
                    & "  d.gcal_year, " _
                    & "  d.gcal_periode, " _
                    & "  d.gcal_start_date, " _
                    & "  d.gcal_end_date " _
                    & "FROM " _
                    & "  public.rosd_data a " _
                    & "  INNER JOIN public.rosg_group b ON (a.rosd_rosg_code = b.rosg_code) " _
                    & "  INNER JOIN public.rost_trans_line c ON (a.rosd_rost_code = c.rost_code) " _
                    & "  INNER JOIN public.gcal_mstr d ON (a.rosd_periode = d.gcal_oid) " _
                    & "WHERE " _
                    & "   rosd_periode='" & le_periode.EditValue.ToString & "' "


                Dim dt_inv As New DataTable
                dt_inv = GetTableData(ssql)

                pgc_master.DataSource = dt_inv
                pgc_master.BestFit()

            ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                ssql = "SELECT  " _
                    & "  a.rosh_oid, " _
                    & "  a.rosh_dt, " _
                    & "  a.rosh_seg, " _
                    & "  a.rosh_rosg_code, " _
                    & "  b.rosg_desc, " _
                    & "  a.rosh_rost_code, " _
                    & "  c.rost_desc, " _
                    & "  a.rosh_periode, " _
                    & "  a.rosh_amount, " _
                    & "  a.rosh_ac_id1, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name, " _
                    & "  a.rosh_hirarki1, " _
                    & "  a.rosh_ac_id2, " _
                    & "  e.ac_code as ac_code2, " _
                    & "  e.ac_name as ac_name2, " _
                    & "  a.rosh_hirarki2,rosh_glt_code " _
                    & "FROM " _
                    & "  public.rosh_history a " _
                    & "  INNER JOIN public.rosg_group b ON (a.rosh_rosg_code = b.rosg_code) " _
                    & "  INNER JOIN public.rost_trans_line c ON (a.rosh_rost_code = c.rost_code) " _
                    & "  INNER JOIN public.ac_mstr d ON (a.rosh_ac_id1 = d.ac_id) " _
                    & "  INNER JOIN public.ac_mstr e ON (a.rosh_ac_id2 = e.ac_id) " _
                    & "ORDER BY " _
                    & "  a.rosh_dt,rosh_glt_code,a.rosh_seg,rosh_amount " _
                    & "  "


                gc_master.DataSource = GetTableData(ssql)
                gv_master.BestFitColumns()
            End If
            


        End If
        Cursor = Cursors.Arrow
    End Sub
    Public Overrides Sub format_grid()
    
        add_column_copy(gv_master, "GL Number", "rosh_glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Time", "rosh_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Sequence", "rosh_seg", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_master, "Group", "rosg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Line", "rost_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code 1", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name 1", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Hirarki 1", "rosh_hirarki1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code 2", "ac_code2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name 2", "ac_name2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Hirarki 2", "rosh_hirarki2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "rosh_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Private Sub be_loc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim frm As New FLocationSearch()
        frm.set_win(Me)
        frm._en_id = le_periode.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
