Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FPartNumberApprovalTax

    Dim _pt_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _en_id_coll As String
    Dim mf As New master_new.ModFunction

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        _en_id_coll = func_data.entity_parent(func_data.entity_user())
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("unitmeasure", pt_en_id.EditValue))

        With pt_um
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("its_mstr", pt_en_id.EditValue))

        With pt_its_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("its_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("its_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(pt_en_id.EditValue))

        With pt_loc_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("loc_type_mstr", pt_en_id.EditValue))

        With pt_loc_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("is_mstr", pt_en_id.EditValue))

        With pt_po_is
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("is_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("is_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("group_mstr", pt_en_id.EditValue))

        With pt_group
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_is_writer(pt_en_id.EditValue))

        With pt_writer_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = func_data.load_ppn_type()

        With pt_ppn_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = func_data.load_taxclass_mstr()

        With pt_tax_class
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(pt_en_id.EditValue))
        pt_si_id.Properties.DataSource = dt_bantu
        pt_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        pt_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        pt_si_id.ItemIndex = 0
    End Sub

    Private Sub sc_le_pt_en_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pt_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        With pt_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pl_mstr", ""))
        With pt_pl_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("pl_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("pl_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_type", ""))
        With pt_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_cost_method", ""))
        With pt_cost_method
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_ls", ""))
        With pt_ls
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_pm_code", ""))
        With pt_pm_code
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pt_class())
        With pt_class
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code Syslog", "pt_syslog_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prod. Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inv. Status", "its_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "pt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Writer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Class", "pt_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Method", "pt_cost_method", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Loc. Type", "loc_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inv. Status On PO Receive", "is_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "pt_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ProcureMethod", "pt_pm_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Lot/Serial/Non", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Safety Stock", "pt_sfty_stk", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reorder Point", "pt_rop", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Minimum Order", "pt_ord_min", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Maximum Order", "pt_ord_max", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost", "pt_cost", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Price", "pt_price", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Contract Code", "ptsp_contract_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Royalti", "ptsp_royalti", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Jenis Buku", "ptsp_jenis_buku", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Judul", "ptsp_judul", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Judul Asli", "ptsp_judul_asli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Judul", "ptsp_subjudul", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Penulis", "ptsp_penulis", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Penerjemah", "ptsp_penerjemah", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Editor", "ptsp_editor", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ISBN", "ptsp_isbn", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cover Preview", "ptsp_cover_preview", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cover", "ptsp_cover", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cetak Isi", "ptsp_cetak_isi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tgl Terbit", "ptsp_tgl_terbit", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "User Create", "pt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  distinct " _
                    & "  pt_oid, " _
                    & "  pt_dom_id, " _
                    & "  pt_en_id, " _
                    & "  en_desc, " _
                    & "  pt_si_id, " _
                    & "  si_desc, " _
                    & "  pt_add_by, " _
                    & "  pt_add_date, " _
                    & "  pt_upd_by, " _
                    & "  pt_upd_date, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_syslog_code, " _
                    & "  pt_pl_id, " _
                    & "  pl_desc, " _
                    & "  pt_um, " _
                    & "  um_mstr.code_name as um_name, " _
                    & "  pt_its_id, " _
                    & "  its_desc, " _
                    & "  pt_type, " _
                    & "  pt_cost_method, " _
                    & "  pt_loc_id,pt_approval_status, " _
                    & "  loc_desc, " _
                    & "  pt_loc_type, " _
                    & "  loc_type_mstr.code_name as loc_type_name, " _
                    & "  pt_po_is, " _
                    & "  is_desc, " _
                    & "  pt_group, " _
                    & "  group_mstr.code_name as group_name, " _
                    & "  pt_taxable, " _
                    & "  pt_tax_inc, " _
                    & "  pt_ppn_type, " _
                    & "  pt_pm_code, " _
                    & "  pt_ls, " _
                    & "  pt_sfty_stk, " _
                    & "  pt_rop, " _
                    & "  pt_ord_min, " _
                    & "  pt_ord_max, " _
                    & "  pt_cost, " _
                    & "  pt_price, " _
                    & "  pt_class, " _
                    & "  ptnr_name, pt_writer_id, " _
                    & "  ptsp_pt_oid, " _
                    & "  ptsp_contract_code, " _
                    & "  ptsp_royalti, " _
                    & "  ptsp_jenis_buku, " _
                    & "  ptsp_judul, " _
                    & "  ptsp_judul_asli, " _
                    & "  ptsp_subjudul, " _
                    & "  ptsp_penulis, " _
                    & "  ptsp_penerjemah, " _
                    & "  ptsp_editor, " _
                    & "  ptsp_isbn, " _
                    & "  ptsp_cover_preview, " _
                    & "  ptsp_cover, " _
                    & "  ptsp_cetak_isi, " _
                    & "  ptsp_tgl_terbit, " _
                    & "  tax_class_mstr.code_name as tax_class_name, pt_tax_class, pt_dt " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join pl_mstr on pl_id = pt_pl_id " _
                    & " left outer join loc_mstr on pt_loc_id = loc_id " _
                    & " left outer join ptnr_mstr on ptnr_id = pt_writer_id " _
                    & " inner join code_mstr um_mstr on um_mstr.code_id = pt_um " _
                    & " inner join its_mstr on its_id = pt_its_id " _
                    & " inner join code_mstr loc_type_mstr on loc_type_mstr.code_id = pt_loc_type " _
                    & " inner join is_mstr on is_id = pt_po_is " _
                    & " inner join code_mstr group_mstr on group_mstr.code_id = pt_group " _
                    & " left outer join ptsp_mstr on ptsp_pt_oid = pt_oid " _
                    & " left outer join si_mstr on si_id = pt_si_id" _
                    & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                    & " where  pt_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") and pt_approval_status='I' "

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        Box("This menus is not available")
        Exit Sub
        pt_en_id.Focus()
        pt_en_id.ItemIndex = 0
        pt_si_id.ItemIndex = 0

        pt_syslog_code.Text = ""
        pt_desc1.Text = ""
        pt_desc2.Text = ""

        pt_pl_id.ItemIndex = 0
        pt_ls.ItemIndex = 0
        pt_type.ItemIndex = 0
        pt_pm_code.ItemIndex = 0

        pt_sfty_stk.Text = "0"
        pt_rop.Text = "0"
        pt_ord_min.Text = "0"
        pt_ord_max.Text = "0"
        pt_taxable.EditValue = False
        pt_tax_inc.EditValue = False
        pt_tax_class.ItemIndex = 0
        pt_ppn_type.ItemIndex = 0
        pt_cost_method.ItemIndex = "0"
        'pt_cost.Text = "0"
        'pt_price.Text = "0"

        pt_writer_id.ItemIndex = 0
        pt_class.ItemIndex = 0

        ptsp_cetak_isi.Text = ""
        ptsp_contract_code.Text = ""
        ptsp_cover.Text = ""
        ptsp_cover_preview.Text = ""
        ptsp_editor.Text = ""
        ptsp_isbn.Text = ""
        ptsp_jenis_buku.Text = ""
        ptsp_judul.Text = ""
        ptsp_judul_asli.Text = ""
        ptsp_penerjemah.Text = ""
        ptsp_penulis.Text = ""
        ptsp_royalti.EditValue = 0.0
        ptsp_subjudul.Text = ""
        ptsp_tgl_terbit.DateTime = master_new.PGSqlConn.CekTanggal
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        If pt_pl_id.Text = "[EditValue is null]" Then
            MessageBox.Show("Product Line Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_pl_id.Focus()
            before_save = False
        ElseIf pt_group.Text = "[EditValue is null]" Then
            MessageBox.Show("Group Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_group.Focus()
            before_save = False
        ElseIf pt_ls.Text = "[EditValue is null]" Then
            MessageBox.Show("Lot/Serial Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_ls.Focus()
            before_save = False
        ElseIf pt_um.Text = "[EditValue is null]" Then
            MessageBox.Show("Unit Measure Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_um.Focus()
            before_save = False
        ElseIf pt_type.Text = "[EditValue is null]" Then
            MessageBox.Show("Type Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_type.Focus()
            before_save = False
        ElseIf pt_pm_code.Text = "[EditValue is null]" Then
            MessageBox.Show("Purchase/Manufacture Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_pm_code.Focus()
            before_save = False
        ElseIf pt_loc_type.Text = "[EditValue is null]" Then
            MessageBox.Show("Location Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_loc_type.Focus()
            before_save = False
        ElseIf pt_its_id.Text = "[EditValue is null]" Then
            MessageBox.Show("Inventory Status Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_its_id.Focus()
            before_save = False
        ElseIf pt_po_is.Text = "[EditValue is null]" Then
            MessageBox.Show("PO Receipt Status Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_po_is.Focus()
            before_save = False
        ElseIf pt_cost_method.Text = "[EditValue is null]" Then
            MessageBox.Show("Cost Methode Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pt_cost_method.Focus()
            before_save = False
        End If

        Return before_save
    End Function

    Private Function get_ptsp_mstr(ByVal par_pt_oid As String) As Boolean
        get_ptsp_mstr = True

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(ptsp_oid) as jml from ptsp_mstr " + _
                                           " where ptsp_pt_oid = '" + par_pt_oid + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_ptsp_mstr = .DataReader("jml").ToString
                        If .DataReader("jml") = 0 Then
                            get_ptsp_mstr = False
                        Else
                            get_ptsp_mstr = True
                        End If
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Return get_ptsp_mstr
    End Function

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text '10879
                    '.Command.CommandText = "select coalesce(max(cast(substring(cast(" + par_colom + " as varchar),3,100) as integer)),0) as max_col  from " + par_table + _
                    '                       " where " + par_colom_criteria + " = '" + criteria + "'" + _
                    '                       " and substring(cast(" + par_colom + " as varchar),3,100) <> ''"
                    .Command.CommandText = "select coalesce(max(cast(substring(cast(pt_id as varchar),3,100) as integer)),0) as max_col  from pt_mstr " + _
                                           " where substring(cast(pt_id as varchar),3,100) <> ''"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CInt(par_en_code + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _pt_oid As Guid
        _pt_oid = Guid.NewGuid

        Dim ssqls As New ArrayList
        Dim _pt_code As String = "PT"
        'Dim _pt_id As Integer = SetInteger(func_coll.GetID("pt_mstr", pt_en_id.GetColumnValue("en_code"), "pt_id", "pt_en_id", pt_en_id.EditValue.ToString))
        Dim _pt_id As Integer = SetInteger(GetID_Local(pt_en_id.GetColumnValue("en_code")))
        Dim _pt_id_s As String = _pt_id.ToString.Substring(2, Len(_pt_id.ToString) - 2)
        '_pt_id_s = _pt_id.ToString.Substring(2, 3 - 2)
        If Len(_pt_id_s) = 1 Then
            _pt_id_s = "000000" + _pt_id_s.ToString
        ElseIf Len(_pt_id_s) = 2 Then
            _pt_id_s = "00000" + _pt_id_s.ToString
        ElseIf Len(_pt_id_s) = 3 Then
            _pt_id_s = "0000" + _pt_id_s.ToString
        ElseIf Len(_pt_id_s) = 4 Then
            _pt_id_s = "000" + _pt_id_s.ToString
        ElseIf Len(_pt_id_s) = 5 Then
            _pt_id_s = "00" + _pt_id_s.ToString
        ElseIf Len(_pt_id_s) = 6 Then
            _pt_id_s = "0" + _pt_id_s.ToString
        ElseIf Len(_pt_id_s) = 7 Then
            _pt_id_s = _pt_id_s.ToString
        End If

        _pt_code = _pt_code + pt_pl_id.GetColumnValue("pl_code").ToString.Substring(0, 2) + _pt_id.ToString.Substring(0, 2) + _pt_id_s

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pt_mstr " _
                                            & "( " _
                                            & "  pt_oid, " _
                                            & "  pt_dom_id, " _
                                            & "  pt_en_id, " _
                                            & "  pt_si_id, " _
                                            & "  pt_add_by, " _
                                            & "  pt_add_date, " _
                                            & "  pt_id, " _
                                            & "  pt_code, " _
                                            & "  pt_syslog_code, " _
                                            & "  pt_desc1, " _
                                            & "  pt_desc2, " _
                                            & "  pt_pl_id, " _
                                            & "  pt_um, " _
                                            & "  pt_its_id, " _
                                            & "  pt_type, " _
                                            & "  pt_cost_method, " _
                                            & "  pt_loc_id, " _
                                            & "  pt_loc_type, " _
                                            & "  pt_po_is, " _
                                            & "  pt_group, " _
                                            & "  pt_taxable, " _
                                            & "  pt_tax_inc, " _
                                            & "  pt_tax_class, " _
                                            & "  pt_ppn_type, " _
                                            & "  pt_pm_code, " _
                                            & "  pt_ls, " _
                                            & "  pt_sfty_stk, " _
                                            & "  pt_rop, " _
                                            & "  pt_ord_min, " _
                                            & "  pt_ord_max, " _
                                            & "  pt_cost, " _
                                            & "  pt_price, " _
                                            & "  pt_writer_id, " _
                                            & "  pt_class, " _
                                            & "  pt_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pt_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pt_en_id.EditValue) & ",  " _
                                            & SetInteger(pt_si_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & _pt_id & ",  " _
                                            & SetSetring(_pt_code) & ",  " _
                                            & SetSetring(pt_syslog_code.Text) & ",  " _
                                            & SetSetring(pt_desc1.Text) & ",  " _
                                            & SetSetring(pt_desc2.Text) & ",  " _
                                            & SetInteger(pt_pl_id.EditValue) & ",  " _
                                            & SetInteger(pt_um.EditValue) & ",  " _
                                            & SetInteger(pt_its_id.EditValue) & ",  " _
                                            & SetSetring(pt_type.EditValue) & ",  " _
                                            & SetSetring(pt_cost_method.EditValue) & ",  " _
                                            & SetInteger(pt_loc_id.EditValue) & ",  " _
                                            & SetInteger(pt_loc_type.EditValue) & ",  " _
                                            & SetInteger(pt_po_is.EditValue) & ",  " _
                                            & SetInteger(pt_group.EditValue) & ",  " _
                                            & SetBitYN(pt_taxable.EditValue) & ",  " _
                                            & SetBitYN(pt_tax_inc.EditValue) & ",  " _
                                            & SetInteger(pt_tax_class.EditValue) & ",  " _
                                            & SetSetring(pt_ppn_type.EditValue) & ",  " _
                                            & SetSetring(pt_pm_code.EditValue) & ",  " _
                                            & SetSetring(pt_ls.EditValue) & ",  " _
                                            & SetDbl(pt_sfty_stk.EditValue) & ",  " _
                                            & SetDbl(pt_rop.EditValue) & ",  " _
                                            & SetDbl(pt_ord_min.EditValue) & ",  " _
                                            & SetDbl(pt_ord_max.EditValue) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetInteger(pt_writer_id.EditValue) & ",  " _
                                            & SetSetring(pt_class.EditValue) & ",  " _
                                            & "  " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.invct_table " _
                                            & "( " _
                                            & "  invct_oid, " _
                                            & "  invct_dom_id, " _
                                            & "  invct_pt_id, " _
                                            & "  invct_cost, " _
                                            & "  invct_si_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(_pt_id) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetInteger(pt_si_id.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.ptsp_mstr " _
                                            & "( " _
                                            & "  ptsp_oid, " _
                                            & "  ptsp_dom_id, " _
                                            & "  ptsp_en_id, " _
                                            & "  ptsp_add_by, " _
                                            & "  ptsp_add_date, " _
                                            & "  ptsp_pt_oid, " _
                                            & "  ptsp_contract_code, " _
                                            & "  ptsp_royalti, " _
                                            & "  ptsp_jenis_buku, " _
                                            & "  ptsp_judul, " _
                                            & "  ptsp_judul_asli, " _
                                            & "  ptsp_subjudul, " _
                                            & "  ptsp_penulis, " _
                                            & "  ptsp_penerjemah, " _
                                            & "  ptsp_editor, " _
                                            & "  ptsp_isbn, " _
                                            & "  ptsp_cover_preview, " _
                                            & "  ptsp_cover, " _
                                            & "  ptsp_cetak_isi, " _
                                            & "  ptsp_tgl_terbit, " _
                                            & "  ptsp_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pt_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_pt_oid.ToString) & ",  " _
                                            & SetSetring(ptsp_contract_code.Text) & ",  " _
                                            & SetDbl(ptsp_royalti.EditValue) & ",  " _
                                            & SetSetring(ptsp_jenis_buku.Text) & ",  " _
                                            & SetSetring(ptsp_judul.Text) & ",  " _
                                            & SetSetring(ptsp_judul_asli.Text) & ",  " _
                                            & SetSetring(ptsp_subjudul.Text) & ",  " _
                                            & SetSetring(ptsp_penulis.Text) & ",  " _
                                            & SetSetring(ptsp_penerjemah.Text) & ",  " _
                                            & SetSetring(ptsp_editor.Text) & ",  " _
                                            & SetSetring(ptsp_isbn.Text) & ",  " _
                                            & SetSetring(ptsp_cover_preview.Text) & ",  " _
                                            & SetSetring(ptsp_cover.Text) & ",  " _
                                            & SetSetring(ptsp_cetak_isi.Text) & ",  " _
                                            & SetDate(ptsp_tgl_terbit.DateTime) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()


                        after_success()
                        set_row(Trim(_pt_oid.ToString), "pt_oid")
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            pt_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position
            'ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)
            With ds.Tables(0).Rows(row)
                _pt_oid = .Item("pt_oid").ToString
                pt_syslog_code.Text = SetString(.Item("pt_syslog_code"))
                pt_desc1.Text = SetString(.Item("pt_desc1"))
                pt_desc2.Text = SetString(.Item("pt_desc2"))
                pt_en_id.EditValue = .Item("pt_en_id")

                If IsDBNull(.Item("pt_si_id")) = True Then
                    pt_si_id.ItemIndex = 0
                Else
                    pt_si_id.EditValue = .Item("pt_si_id")
                End If

                pt_pl_id.EditValue = .Item("pt_pl_id")
                pt_group.EditValue = .Item("pt_group")
                pt_ls.EditValue = .Item("pt_ls")
                pt_um.EditValue = .Item("pt_um")
                pt_type.EditValue = .Item("pt_type")
                pt_pm_code.EditValue = .Item("pt_pm_code")
                pt_loc_id.EditValue = .Item("pt_loc_id")
                pt_loc_type.EditValue = .Item("pt_loc_type")
                pt_its_id.EditValue = .Item("pt_its_id")
                pt_po_is.EditValue = .Item("pt_po_is")
                pt_sfty_stk.EditValue = .Item("pt_sfty_stk")
                pt_rop.EditValue = .Item("pt_rop")
                pt_ord_min.EditValue = .Item("pt_ord_min")
                pt_ord_max.EditValue = .Item("pt_ord_max")
                pt_taxable.EditValue = IIf(.Item("pt_taxable") = "Y", True, False)
                pt_tax_inc.EditValue = IIf(IsDBNull(.Item("pt_tax_inc")) = True, False, SetBitYNB(.Item("pt_tax_inc")))

                If IsDBNull(.Item("pt_tax_class")) = True Then
                    pt_tax_class.ItemIndex = 0
                Else
                    pt_tax_class.EditValue = .Item("pt_tax_class")
                End If

                pt_ppn_type.EditValue = .Item("pt_ppn_type")
                pt_cost_method.EditValue = .Item("pt_cost_method")
                'pt_cost.EditValue = .Item("pt_cost")
                'pt_price.EditValue = .Item("pt_price")
                pt_writer_id.EditValue = .Item("pt_writer_id")
                pt_class.EditValue = SetString(.Item("pt_class"))

                ptsp_cetak_isi.Text = SetString(.Item("ptsp_cetak_isi"))
                ptsp_contract_code.Text = SetString(.Item("ptsp_contract_code"))
                ptsp_cover.Text = SetString(.Item("ptsp_cover"))
                ptsp_cover_preview.Text = SetString(.Item("ptsp_cover_preview"))
                ptsp_editor.Text = SetString(.Item("ptsp_editor"))
                ptsp_isbn.Text = SetString(.Item("ptsp_isbn"))
                ptsp_jenis_buku.Text = SetString(.Item("ptsp_jenis_buku"))
                ptsp_judul.Text = SetString(.Item("ptsp_judul"))
                ptsp_judul_asli.Text = SetString(.Item("ptsp_judul_asli"))
                ptsp_penerjemah.Text = SetString(.Item("ptsp_penerjemah"))
                ptsp_penulis.Text = SetString(.Item("ptsp_penulis"))
                ptsp_royalti.EditValue = .Item("ptsp_royalti")
                ptsp_subjudul.Text = SetString(.Item("ptsp_subjudul"))

                If IsDBNull(.Item("ptsp_tgl_terbit")) = True Then
                    ptsp_tgl_terbit.Text = ""
                Else
                    ptsp_tgl_terbit.DateTime = .Item("ptsp_tgl_terbit")
                End If

            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList

        Dim _status_ptsp As Boolean
        _status_ptsp = get_ptsp_mstr(_pt_oid)

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
                                            & "  public.pt_mstr   " _
                                            & "SET  " _
                                            & "  pt_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pt_en_id = " & SetInteger(pt_en_id.EditValue) & ",  " _
                                            & "  pt_si_id = " & SetInteger(pt_si_id.EditValue) & ",  " _
                                            & "  pt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pt_syslog_code = " & SetSetring(pt_syslog_code.Text) & ",  " _
                                            & "  pt_desc1 = " & SetSetring(pt_desc1.Text) & ",  " _
                                            & "  pt_desc2 = " & SetSetring(pt_desc2.Text) & ",  " _
                                            & "  pt_pl_id = " & SetInteger(pt_pl_id.EditValue) & ",  " _
                                            & "  pt_um = " & SetInteger(pt_um.EditValue) & ",  " _
                                            & "  pt_its_id = " & SetInteger(pt_its_id.EditValue) & ",  " _
                                            & "  pt_type = " & SetSetring(pt_type.EditValue) & ",  " _
                                            & "  pt_cost_method = " & SetSetring(pt_cost_method.EditValue) & ",  " _
                                            & "  pt_loc_id = " & SetInteger(pt_loc_id.EditValue) & ",  " _
                                            & "  pt_loc_type = " & SetInteger(pt_loc_type.EditValue) & ",  " _
                                            & "  pt_po_is = " & SetInteger(pt_po_is.EditValue) & ",  " _
                                            & "  pt_group = " & SetInteger(pt_group.EditValue) & ",  " _
                                            & "  pt_taxable = " & SetBitYN(pt_taxable.EditValue) & ",  " _
                                            & "  pt_tax_inc = " & SetBitYN(pt_tax_inc.EditValue) & ",  " _
                                            & "  pt_tax_class = " & SetInteger(pt_tax_class.EditValue) & ",  " _
                                            & "  pt_ppn_type = " & SetSetring(pt_ppn_type.EditValue) & ",  " _
                                            & "  pt_approval_status = 'T',  " _
                                            & "  pt_pm_code = " & SetSetring(pt_pm_code.EditValue) & ",  " _
                                            & "  pt_ls = " & SetSetring(pt_ls.EditValue) & ",  " _
                                            & "  pt_sfty_stk = " & SetDbl(pt_sfty_stk.EditValue) & ",  " _
                                            & "  pt_rop = " & SetDbl(pt_rop.EditValue) & ",  " _
                                            & "  pt_ord_min = " & SetDbl(pt_ord_min.EditValue) & ",  " _
                                            & "  pt_ord_max = " & SetDbl(pt_ord_max.EditValue) & ",  " _
                                            & "  pt_cost = " & SetDbl(0) & ",  " _
                                            & "  pt_price = " & SetDbl(0) & ",  " _
                                            & "  pt_writer_id = " & SetInteger(pt_writer_id.EditValue) & ",  " _
                                            & "  pt_class = " & SetSetring(pt_class.EditValue) & ",  " _
                                            & "  pt_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pt_oid = " & SetSetring(_pt_oid.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If _status_ptsp = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.ptsp_mstr   " _
                                                & "SET  " _
                                                & "  ptsp_en_id = " & SetInteger(pt_en_id.EditValue) & ",  " _
                                                & "  ptsp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ptsp_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " ,  " _
                                                & "  ptsp_contract_code = " & SetSetring(ptsp_contract_code.Text) & ",  " _
                                                & "  ptsp_royalti = " & SetDblDB(ptsp_royalti.EditValue) & ",  " _
                                                & "  ptsp_jenis_buku = " & SetSetring(ptsp_jenis_buku.Text) & ",  " _
                                                & "  ptsp_judul = " & SetSetring(ptsp_judul.Text) & ",  " _
                                                & "  ptsp_judul_asli = " & SetSetring(ptsp_judul_asli.Text) & ",  " _
                                                & "  ptsp_subjudul = " & SetSetring(ptsp_subjudul.Text) & ",  " _
                                                & "  ptsp_penulis = " & SetSetring(ptsp_penulis.Text) & ",  " _
                                                & "  ptsp_penerjemah = " & SetSetring(ptsp_penerjemah.Text) & ",  " _
                                                & "  ptsp_editor = " & SetSetring(ptsp_editor.Text) & ",  " _
                                                & "  ptsp_isbn = " & SetSetring(ptsp_isbn.Text) & ",  " _
                                                & "  ptsp_cover_preview = " & SetSetring(ptsp_cover_preview.Text) & ",  " _
                                                & "  ptsp_cover = " & SetSetring(ptsp_cover.Text) & ",  " _
                                                & "  ptsp_cetak_isi = " & SetSetring(ptsp_cetak_isi.Text) & ",  " _
                                                & "  ptsp_tgl_terbit = " & SetDate(ptsp_tgl_terbit.EditValue) & ",  " _
                                                & "  ptsp_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                & "  " _
                                                & "WHERE ptsp_pt_oid = '" + _pt_oid.ToString + "'"


                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptsp_mstr " _
                                                & "( " _
                                                & "  ptsp_oid, " _
                                                & "  ptsp_dom_id, " _
                                                & "  ptsp_en_id, " _
                                                & "  ptsp_add_by, " _
                                                & "  ptsp_add_date, " _
                                                & "  ptsp_pt_oid, " _
                                                & "  ptsp_contract_code, " _
                                                & "  ptsp_royalti, " _
                                                & "  ptsp_jenis_buku, " _
                                                & "  ptsp_judul, " _
                                                & "  ptsp_judul_asli, " _
                                                & "  ptsp_subjudul, " _
                                                & "  ptsp_penulis, " _
                                                & "  ptsp_penerjemah, " _
                                                & "  ptsp_editor, " _
                                                & "  ptsp_isbn, " _
                                                & "  ptsp_cover_preview, " _
                                                & "  ptsp_cover, " _
                                                & "  ptsp_cetak_isi, " _
                                                & "  ptsp_tgl_terbit, " _
                                                & "  ptsp_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(pt_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & SetSetring(_pt_oid.ToString) & ",  " _
                                                & SetSetring(ptsp_contract_code.Text) & ",  " _
                                                & SetDblDB(ptsp_royalti.EditValue) & ",  " _
                                                & SetSetring(ptsp_jenis_buku.Text) & ",  " _
                                                & SetSetring(ptsp_judul.Text) & ",  " _
                                                & SetSetring(ptsp_judul_asli.Text) & ",  " _
                                                & SetSetring(ptsp_subjudul.Text) & ",  " _
                                                & SetSetring(ptsp_penulis.Text) & ",  " _
                                                & SetSetring(ptsp_penerjemah.Text) & ",  " _
                                                & SetSetring(ptsp_editor.Text) & ",  " _
                                                & SetSetring(ptsp_isbn.Text) & ",  " _
                                                & SetSetring(ptsp_cover_preview.Text) & ",  " _
                                                & SetSetring(ptsp_cover.Text) & ",  " _
                                                & SetSetring(ptsp_cetak_isi.Text) & ",  " _
                                                & SetDate(ptsp_tgl_terbit.DateTime) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Approve Tax Part Number " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        'If func_coll.get_conf_file("partnumber_send_sms") = "1" Then
                        '    If sent_notification("", "pt_approve_acc", "Mohon di setting product line untuk part number baru = " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & " " & pt_desc1.Text & " agar dapat digunakan untuk transaksi") = False Then
                        '        Box("Gagal mengirim pesan")
                        '        Exit Function
                        '    End If
                        'End If


                        Dim _nik As String = func_coll.get_conf_file("partnumber_approval_accounting_nik")

                        If _nik <> "" Then
                            Dim Pesan As String
                            Pesan = "Mohon di setting accounting untuk part number baru : " _
                            & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & " " & pt_desc1.Text _
                            & " SYSPRO : " & func_coll.get_conf_file("syspro_approval_code") & " agar dapat digunakan untuk transaksi"

                            mf.send_wa_to_nik(_nik, Pesan)

                        Else
                            MsgBox("Approval accounting belum disetting")
                        End If


                        'Dim ssql As String

                        'ssql = "select notification_type,notification_phone  from tphone where notification_status='pt_approve_acc'"
                        'Dim dr_notif As DataRow = GetRowInfo(ssql)

                        'If dr_notif(0) = "telegram" Then
                        '    'If func_coll.get_conf_file("send_telegram") = "1" Then

                        '    Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval").Replace("php56/json/approval_telegram.php", "php72/api_iot/kirim_id.php") _
                        '    & "?id=" & dr_notif(1) & "&pesan=Mohon di setting pajak untuk part number baru : " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & " " & pt_desc1.Text _
                        '    & " SYSPRO : " & func_coll.get_conf_file("syspro_approval_code") & " agar dapat digunakan untuk transaksi"

                        '    Dim result As String
                        '    result = mf.run_get_to_api(_alamat_api & "")

                        '    If SetString(result).Contains("success") Then
                        '        '_MakeReport("Data empty")

                        '    Else
                        '        Box("Gagal notif telegram")
                        '        Exit Function
                        '    End If
                        '    'End If
                        'ElseIf dr_notif(0) = "pidgin" Then


                        '    Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval").Replace("approval_telegram.php", "send_pidgin.php") _
                        '       & "?id=" & dr_notif(1) & "&pesan=Mohon di setting pajak untuk part number baru : " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & " " & pt_desc1.Text _
                        '       & " agar dapat digunakan untuk transaksi"

                        '    Dim result As String
                        '    result = mf.run_get_to_api(_alamat_api & "")

                        '    If SetString(result).Contains("success") Then
                        '        '_MakeReport("Data empty")

                        '    Else
                        '        Box("Gagal notif pidgin " & dr_notif(1))
                        '        Exit Function
                        '    End If
                        'End If

                        after_success()
                        set_row(Trim(_pt_oid.ToString), "pt_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
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
                            .Command.CommandText = "delete from pt_mstr where pt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function
End Class




