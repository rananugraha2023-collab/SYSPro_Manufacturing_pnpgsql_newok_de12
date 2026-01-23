Imports npgsql
Imports master_new.ModFunction

Public Class FTrialBalance
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim sSQL As String
    Dim _status_edit As String = ""
    Private Sub FOpeningBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_req1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_req1.DataTable1)
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
        'le_periode.Properties.DataSource = dt_bantu
        'le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        'le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        'le_periode.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'glbal_en_id.Properties.DataSource = dt_bantu
        'glbal_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'glbal_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'glbal_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_gcal_mstr())
        'glbal_gcal_oid.Properties.DataSource = dt_bantu
        'glbal_gcal_oid.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        'glbal_gcal_oid.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        'glbal_gcal_oid.ItemIndex = 0


        'init_le(le_entity, "en_mstr")
        'init_le(le_periode, "gcal_mstr")

        'init_le(glbal_en_id, "en_mstr")
        'init_le(glbal_gcal_oid, "gcal_mstr")

    End Sub

    Public Overrides Sub format_grid()
        add_column_fix(gv_master, "ID", "tb_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_fix(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_fix(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
       
        add_column_copy(gv_master, "Opening 01", "tb_month_01_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 01", "tb_month_01_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 01", "tb_month_01_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 02", "tb_month_02_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 02", "tb_month_02_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 02", "tb_month_02_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")


        add_column_copy(gv_master, "Opening 03", "tb_month_03_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 03", "tb_month_03_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 03", "tb_month_03_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 04", "tb_month_04_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 04", "tb_month_04_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 04", "tb_month_04_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 05", "tb_month_05_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 05", "tb_month_05_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 05", "tb_month_05_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 06", "tb_month_06_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 06", "tb_month_06_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 06", "tb_month_06_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 07", "tb_month_07_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 07", "tb_month_07_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 07", "tb_month_07_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 08", "tb_month_08_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 08", "tb_month_08_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 08", "tb_month_08_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 09", "tb_month_09_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 09", "tb_month_09_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 09", "tb_month_09_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 10", "tb_month_10_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 10", "tb_month_10_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 10", "tb_month_10_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 11", "tb_month_11_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 11", "tb_month_11_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 11", "tb_month_11_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")

        add_column_copy(gv_master, "Opening 12", "tb_month_12_opening", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Tran 12", "tb_month_12_tran", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Ending 12", "tb_month_12_ending", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2")


        init_le(le_year, "year_periode")
     
    End Sub

    Public Overrides Function get_sequel() As String
       
        'get_sequel = "SELECT  " _
        '       & "  glbal_oid, " _
        '       & "  glbal_dom_id, " _
        '       & "  glbal_en_id, " _
        '       & "  glbal_add_by, " _
        '       & "  glbal_add_date, " _
        '       & "  glbal_upd_by, " _
        '       & "  glbal_upd_date, " _
        '       & "  glbal_gcal_oid, " _
        '       & "  glbal_ac_id, " _
        '       & "  glbal_sb_id, " _
        '       & "  glbal_cc_id, " _
        '       & "  glbal_cu_id, " _
        '       & "  glbal_balance_open, " _
        '       & "  glbal_balance_unposted, " _
        '       & "  glbal_balance_posted,glbal_balance_posted_end_month, glbal_balance_trial,   " _
        '       & "  glbal_dt, " _
        '       & "  en_desc, " _
        '       & "  gcal_year, " _
        '       & "  gcal_periode, " _
        '       & "  gcal_start_date, " _
        '       & "  gcal_end_date, " _
        '       & "  ac_code, " _
        '       & "  ac_name, " _
        '       & "  sb_desc, " _
        '       & "  cc_desc, " _
        '       & "  cu_name " _
        '       & "FROM  " _
        '       & "  public.glbal_balance " _
        '       & "  inner join en_mstr on en_id = glbal_en_id " _
        '       & "  inner join ac_mstr on ac_id = glbal_ac_id " _
        '       & "  inner join sb_mstr on sb_id = glbal_sb_id " _
        '       & "  inner join cc_mstr on cc_id = glbal_cc_id " _
        '       & "  inner join cu_mstr on cu_id = glbal_cu_id " _
        '       & "  inner join gcal_mstr on gcal_oid = glbal_gcal_oid " _
        '       & " where glbal_en_id = " + le_entity.EditValue.ToString _
        '       & " and glbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
        '       & " and glbal_en_id in (select user_en_id from tconfuserentity " _
        '                          & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        get_sequel = "SELECT  " _
            & "  a.tb_oid, " _
            & "  a.tb_year, " _
            & "  a.tb_ac_id, " _
            & "  public.ac_mstr.ac_code, " _
            & "  public.ac_mstr.ac_name, " _
            & "  a.tb_month_01_opening, " _
            & "  a.tb_month_01_tran, " _
            & "  a.tb_month_01_ending, " _
            & "  a.tb_month_02_opening, " _
            & "  a.tb_month_02_tran, " _
            & "  a.tb_month_02_ending, " _
            & "  a.tb_month_03_opening, " _
            & "  a.tb_month_03_tran, " _
            & "  a.tb_month_03_ending, " _
            & "  a.tb_month_04_opening, " _
            & "  a.tb_month_04_tran, " _
            & "  a.tb_month_04_ending, " _
            & "  a.tb_month_05_opening, " _
            & "  a.tb_month_05_tran, " _
            & "  a.tb_month_05_ending, " _
            & "  a.tb_month_06_opening, " _
            & "  a.tb_month_06_tran, " _
            & "  a.tb_month_06_ending, " _
            & "  a.tb_month_07_opening, " _
            & "  a.tb_month_07_tran, " _
            & "  a.tb_month_07_ending, " _
            & "  a.tb_month_08_opening, " _
            & "  a.tb_month_08_tran, " _
            & "  a.tb_month_08_ending, " _
            & "  a.tb_month_09_opening, " _
            & "  a.tb_month_09_tran, " _
            & "  a.tb_month_09_ending, " _
            & "  a.tb_month_10_opening, " _
            & "  a.tb_month_10_tran, " _
            & "  a.tb_month_10_ending, " _
            & "  a.tb_month_11_opening, " _
            & "  a.tb_month_11_tran, " _
            & "  a.tb_month_11_ending, " _
            & "  a.tb_month_12_opening, " _
            & "  a.tb_month_12_tran, " _
            & "  a.tb_month_12_ending " _
            & "FROM " _
            & "  public.tb_trial a " _
            & "  INNER JOIN public.ac_mstr ON (a.tb_ac_id = public.ac_mstr.ac_id) " _
            & "WHERE " _
            & "  a.tb_year =  " & le_year.EditValue _
            & " ORDER BY " _
            & "  ac_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        'glbal_en_id.Focus()
        'glbal_en_id.ItemIndex = 0
        'glbal_gcal_oid.ItemIndex = 0
    End Sub

    

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function
   
   
   

End Class
