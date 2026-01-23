Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FProductLine

    Dim _pl_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("taxclass_mstr_non", ""))
        pl_tax_class.Properties.DataSource = dt_bantu
        pl_tax_class.Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        pl_tax_class.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        pl_tax_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        pla_ac_id.Properties.DataSource = dt_bantu
        pla_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        pla_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        pla_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr())
        pla_sb_id.Properties.DataSource = dt_bantu
        pla_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        pla_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        pla_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr())
        pla_cc_id.Properties.DataSource = dt_bantu
        pla_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        pla_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        pla_cc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "pl_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "pl_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "pl_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "pl_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pl_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pl_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pl_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail, "Code", "pla_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Type", "pla_param", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "pla_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "pla_pl_id", False)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account Code", "sb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account Description", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center Code", "cc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center Description", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  pl_oid, " _
                    & "  pl_dom_id, " _
                    & "  pl_add_by, " _
                    & "  pl_add_date, " _
                    & "  pl_upd_by, " _
                    & "  pl_upd_date, " _
                    & "  pl_id, " _
                    & "  pl_code, " _
                    & "  pl_desc, " _
                    & "  pl_taxable, " _
                    & "  pl_tax_class, " _
                    & "  code_name, " _
                    & "  pl_active, " _
                    & "  pl_dt " _
                    & "FROM  " _
                    & "  public.pl_mstr" _
                    & " left outer join code_mstr on code_id  = pl_tax_class"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String
        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  public.pla_mstr.pla_oid, " _
            & "  public.pla_mstr.pla_seq, " _
            & "  public.pla_mstr.pla_param, " _
            & "  public.pla_mstr.pla_code, " _
            & "  public.pla_mstr.pla_desc, " _
            & "  public.pla_mstr.pla_ac_id, " _
            & "  public.pla_mstr.pla_sb_id, " _
            & "  public.pla_mstr.pla_cc_id, " _
            & "  public.pla_mstr.pla_dt, " _
            & "  public.pla_mstr.pla_pl_id, " _
            & "  public.ac_mstr.ac_name, " _
            & "  public.ac_mstr.ac_code, " _
            & "  public.cc_mstr.cc_code, " _
            & "  public.cc_mstr.cc_desc, " _
            & "  public.sb_mstr.sb_code, " _
            & "  public.sb_mstr.sb_desc " _
            & "FROM " _
            & "  public.pla_mstr " _
            & "  left outer JOIN public.ac_mstr ON (public.pla_mstr.pla_ac_id = public.ac_mstr.ac_id) " _
            & "  left outer JOIN public.cc_mstr ON (public.pla_mstr.pla_cc_id = public.cc_mstr.cc_id) " _
            & "  left outer JOIN public.sb_mstr ON (public.pla_mstr.pla_sb_id = public.sb_mstr.sb_id) order by pla_code"

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("pla_pl_id").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pla_pl_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_id").ToString)
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()

        sc_te_pl_code.Focus()
        sc_te_pl_code.Text = ""
        sc_te_pl_desc.Text = ""
        sc_ce_pl_active.EditValue = False
        sc_ce_pl_taxable.EditValue = False


    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pl_oid As Guid
        _pl_oid = Guid.NewGuid
        Dim _taxclass, i, _pl_id As Integer

        If sc_ce_pl_taxable.EditValue = True Then
            _taxclass = pl_tax_class.EditValue
        Else
            _taxclass = -1
        End If

        Dim ssqls As New ArrayList
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from plam_mstr"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "plam_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        _pl_id = func_coll.GetID("pl_mstr", "pl_id")
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
                                            & "  public.pl_mstr " _
                                            & "( " _
                                            & "  pl_oid, " _
                                            & "  pl_dom_id, " _
                                            & "  pl_add_by, " _
                                            & "  pl_add_date, " _
                                            & "  pl_id, " _
                                            & "  pl_code, " _
                                            & "  pl_desc, " _
                                            & "  pl_taxable, " _
                                            & "  pl_tax_class, " _
                                            & "  pl_active, " _
                                            & "  pl_dt " _
                                            & ")  " _
                                            & "VALUES ( '" _
                                            + _pl_oid.ToString + "', '" _
                                            & SetNumber(master_new.ClsVar.sdom_id) & "', '" _
                                            + master_new.ClsVar.sNama _
                                            + "', (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "), " _
                                            & _pl_id.ToString + ",'" + _
                                               Trim(sc_te_pl_code.Text) + "','" + _
                                               Trim(sc_te_pl_desc.Text) + "','" + _
                                               IIf(sc_ce_pl_taxable.EditValue = True, "Y", "N") + "'," + _
                                               IIf(_taxclass = -1, "null", _taxclass.ToString) + ",'" + _
                                               IIf(sc_ce_pl_active.EditValue = True, "Y", "N") + "'," + _
                                               " (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pla_mstr " _
                                                & "( " _
                                                & "  pla_oid, " _
                                                & "  pla_seq, " _
                                                & "  pla_param, " _
                                                & "  pla_code, " _
                                                & "  pla_desc, " _
                                                & "  pla_ac_id, " _
                                                & "  pla_sb_id, " _
                                                & "  pla_cc_id, " _
                                                & "  pla_dt, " _
                                                & "  pla_pl_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(ds_bantu.Tables(0).Rows(i).Item("plam_seq")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_param")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_code")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_desc")) & ",  " _
                                                & "null" & ",  " _
                                                & "null" & ",  " _
                                                & "null" & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & _pl_id & "  " _
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

                        after_success()
                        set_row(sc_te_pl_code.Text, "pl_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
            sc_te_pl_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pl_oid = .Item("pl_oid").ToString
                sc_te_pl_code.Text = .Item("pl_code")
                sc_te_pl_desc.Text = .Item("pl_desc")
                sc_ce_pl_active.EditValue = IIf(.Item("pl_active") = "Y", True, False)
                sc_ce_pl_taxable.EditValue = IIf(.Item("pl_taxable") = "Y", True, False)
                If sc_ce_pl_taxable.EditValue = True Then
                    lci_TaxClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    pl_tax_class.EditValue = .Item("pl_tax_class")
                Else
                    lci_TaxClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _taxclass As Integer

        If sc_ce_pl_taxable.EditValue = True Then
            _taxclass = pl_tax_class.EditValue
        Else
            _taxclass = -1
        End If

        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update pl_mstr set pl_code = '" + Trim(sc_te_pl_code.Text) + "', " + _
                                               " pl_upd_by = '" + master_new.ClsVar.sNama + "', " + _
                                               " pl_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "), " + _
                                               " pl_desc = '" + Trim(sc_te_pl_desc.Text) + "', " + _
                                               " pl_taxable = '" + IIf(sc_ce_pl_taxable.EditValue = True, "Y", "N") + "'," + _
                                               " pl_active = '" + IIf(sc_ce_pl_active.EditValue = True, "Y", "N") + "'," + _
                                               " pl_tax_class = " + IIf(_taxclass = -1, "null", _taxclass.ToString) + "," + _
                                               " pl_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" + _
                                               " where pl_oid = '" + _pl_oid + "'"
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
                        set_row(sc_te_pl_code.Text, "pl_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
                            .Command.CommandText = "delete from pl_mstr where pl_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_oid").ToString + "'"
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

    Private Sub sc_ce_pl_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_ce_pl_taxable.CheckedChanged
        If sc_ce_pl_taxable.EditValue = True Then
            lci_TaxClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            pl_tax_class.ItemIndex = 0
        Else
            lci_TaxClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub sb_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_edit.Click
        Dim _pl_code, _pla_oid As String
        Dim i As Integer
        Dim ssqls As New ArrayList

        _pl_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_code")
        _pla_oid = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("pla_oid").ToString

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update pla_mstr set pla_ac_id = " + pla_ac_id.EditValue.ToString + ", " + _
                                               " pla_sb_id = " + pla_sb_id.EditValue.ToString + ", " + _
                                               " pla_cc_id = " + pla_cc_id.EditValue.ToString + _
                                               " where pla_oid = '" + _pla_oid + "'"
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
                        set_row(_pl_code, "pl_code")

                        For i = 0 To ds.Tables("detail").Rows.Count - 1
                            If ds.Tables("detail").Rows(i).Item("pla_oid").ToString = _pla_oid Then
                                BindingContext(ds.Tables("detail")).Position = i
                            End If
                        Next

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UpdateDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateDetailToolStripMenuItem.Click
        Dim _pl_oid As Guid
        _pl_oid = Guid.NewGuid
        Dim _taxclass, i, _pl_id As Integer


        Dim ssqls As New ArrayList
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from plam_mstr where plam_code not in (select pla_code from pla_mstr where pla_pl_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_id").ToString & ")"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "plam_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pla_mstr " _
                                                & "( " _
                                                & "  pla_oid, " _
                                                & "  pla_seq, " _
                                                & "  pla_param, " _
                                                & "  pla_code, " _
                                                & "  pla_desc, " _
                                                & "  pla_ac_id, " _
                                                & "  pla_sb_id, " _
                                                & "  pla_cc_id, " _
                                                & "  pla_dt, " _
                                                & "  pla_pl_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(ds_bantu.Tables(0).Rows(i).Item("plam_seq")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_param")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_code")) & ",  " _
                                                & SetSetring(ds_bantu.Tables(0).Rows(i).Item("plam_desc")) & ",  " _
                                                & "null" & ",  " _
                                                & "null" & ",  " _
                                                & "null" & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_id").ToString & "  " _
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

                        after_success()
                        set_row(sc_te_pl_code.Text, "pl_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)

                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btReload.Click
        Try
            Dim dt_plam As New DataTable
            Dim sSQL As String
            Dim sSQLs As New ArrayList
            sSQL = "select * from plam_mstr where plam_code not in (select pla_code from pla_mstr where pla_pl_id=" _
            & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_id") & ")"

            dt_plam = master_new.PGSqlConn.GetTableData(sSQL)

            For Each dr As DataRow In dt_plam.Rows
                sSQL = "INSERT INTO  " _
                  & "  public.pla_mstr " _
                  & "( " _
                  & "  pla_oid, " _
                  & "  pla_seq, " _
                  & "  pla_param, " _
                  & "  pla_code, " _
                  & "  pla_desc, " _
                  & "  pla_ac_id, " _
                  & "  pla_sb_id, " _
                  & "  pla_cc_id, " _
                  & "  pla_dt, " _
                  & "  pla_pl_id " _
                  & ")  " _
                  & "VALUES ( " _
                  & SetSetring(Guid.NewGuid.ToString) & ",  " _
                  & SetInteger(dr("plam_seq")) & ",  " _
                  & SetSetring(dr("plam_param")) & ",  " _
                  & SetSetring(dr("plam_code")) & ",  " _
                  & SetSetring(dr("plam_desc")) & ",  " _
                  & "null" & ",  " _
                  & "null" & ",  " _
                  & "null" & ",  " _
                  & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                  & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pl_id") & "  " _
                  & ")"

                sSQLs.Add(sSQL)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            End If
            Box("Success")
            load_data_grid_detail()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_detail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_detail.DoubleClick
        Try
            With ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position)
                pla_ac_id.EditValue = .Item("pla_ac_id")
                pla_sb_id.EditValue = .Item("pla_sb_id")
                pla_cc_id.EditValue = .Item("pla_cc_id")
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
