Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FTaxRate
    Dim _taxr_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FTaxRate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxclass_mstr())
        taxr_tax_class.Properties.DataSource = dt_bantu
        taxr_tax_class.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        taxr_tax_class.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        taxr_tax_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxtype_mstr())
        taxr_tax_type.Properties.DataSource = dt_bantu
        taxr_tax_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        taxr_tax_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        taxr_tax_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        taxr_ac_sales_id.Properties.DataSource = dt_bantu
        taxr_ac_sales_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        taxr_ac_sales_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        taxr_ac_sales_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr())
        taxr_sb_sales_id.Properties.DataSource = dt_bantu
        taxr_sb_sales_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        taxr_sb_sales_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        taxr_sb_sales_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr())
        taxr_cc_sales_id.Properties.DataSource = dt_bantu
        taxr_cc_sales_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        taxr_cc_sales_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        taxr_cc_sales_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        taxr_ac_ap_id.Properties.DataSource = dt_bantu
        taxr_ac_ap_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        taxr_ac_ap_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        taxr_ac_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr())
        taxr_sb_ap_id.Properties.DataSource = dt_bantu
        taxr_sb_ap_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        taxr_sb_ap_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        taxr_sb_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr())
        taxr_cc_ap_id.Properties.DataSource = dt_bantu
        taxr_cc_ap_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        taxr_cc_ap_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        taxr_cc_ap_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Type", "tax_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Date", "taxr_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Rate", "taxr_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Sub Account", "sb_desc_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Cost Center", "cc_desc_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Code", "ac_code_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Name", "ac_name_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Sub Account", "sb_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Cost Center", "cc_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "taxr_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "taxr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "taxr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "taxr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "taxr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  taxr_oid, " _
                & "  taxr_dom_id, " _
                & "  taxr_add_by, " _
                & "  taxr_add_date, " _
                & "  taxr_upd_by, " _
                & "  taxr_upd_date, " _
                & "  taxr_tax_type, " _
                & "  taxr_tax_class, " _
                & "  taxr_date, " _
                & "  taxr_rate, " _
                & "  taxr_ac_sales_id, " _
                & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                & "  taxr_sb_sales_id, " _
                & "  sb_mstr_sales.sb_desc as sb_desc_sales, " _
                & "  taxr_cc_sales_id, " _
                & "  cc_mstr_sales.cc_desc as cc_desc_sales, " _
                & "  taxr_ac_ap_id, " _
                & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                & "  ac_mstr_ap.ac_name as ac_name_ap, " _
                & "  taxr_sb_ap_id, " _
                & "  sb_mstr_ap.sb_desc as sb_desc_ap, " _
                & "  taxr_cc_ap_id, " _
                & "  cc_mstr_ap.cc_desc as cc_desc_ap, " _
                & "  taxr_active, " _
                & "  taxr_dt, " _
                & "  taxtype_mstr.code_name as tax_type_name, " _
                & "  taxclass_mstr.code_name as tax_class_name " _
                & "FROM  " _
                & "  public.taxr_mstr " _
                & "  inner join public.code_mstr taxtype_mstr on taxtype_mstr.code_id = taxr_tax_type " _
                & "  inner join public.code_mstr taxclass_mstr on taxclass_mstr.code_id = taxr_tax_class " _
                & "  inner join public.ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = taxr_ac_sales_id " _
                & "  inner join public.ac_mstr ac_mstr_ap on ac_mstr_ap.ac_id = taxr_ac_ap_id " _
                & "  inner join public.sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = taxr_sb_sales_id " _
                & "  inner join public.sb_mstr sb_mstr_ap on sb_mstr_ap.sb_id = taxr_sb_ap_id " _
                & "  inner join public.cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = taxr_cc_sales_id " _
                & "  inner join public.cc_mstr cc_mstr_ap on cc_mstr_ap.cc_id = taxr_cc_ap_id "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        taxr_tax_class.Focus()
        taxr_tax_class.ItemIndex = 0
        taxr_tax_type.ItemIndex = 0
        taxr_date.DateTime = Now
        taxr_rate.Text = "0"
        taxr_active.EditValue = True
        taxr_ac_sales_id.ItemIndex = 0
        taxr_sb_sales_id.ItemIndex = 0
        taxr_cc_sales_id.ItemIndex = 0
        taxr_ac_ap_id.ItemIndex = 0
        taxr_sb_ap_id.ItemIndex = 0
        taxr_cc_ap_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _taxr_oid As Guid
        _taxr_oid = Guid.NewGuid
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.taxr_mstr " _
                                            & "( " _
                                            & "  taxr_oid, " _
                                            & "  taxr_dom_id, " _
                                            & "  taxr_add_by, " _
                                            & "  taxr_add_date, " _
                                            & "  taxr_tax_type, " _
                                            & "  taxr_tax_class, " _
                                            & "  taxr_date, " _
                                            & "  taxr_rate, " _
                                            & "  taxr_ac_sales_id, " _
                                            & "  taxr_sb_sales_id, " _
                                            & "  taxr_cc_sales_id, " _
                                            & "  taxr_ac_ap_id, " _
                                            & "  taxr_sb_ap_id, " _
                                            & "  taxr_cc_ap_id, " _
                                            & "  taxr_active, " _
                                            & "  taxr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_taxr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(taxr_tax_type.EditValue) & ",  " _
                                            & SetInteger(taxr_tax_class.EditValue) & ",  " _
                                            & SetDate(taxr_date.DateTime) & ",  " _
                                            & SetDbl(taxr_rate.EditValue) & ",  " _
                                            & SetInteger(taxr_ac_sales_id.EditValue) & ",  " _
                                            & SetInteger(taxr_sb_sales_id.EditValue) & ",  " _
                                            & SetInteger(taxr_cc_sales_id.EditValue) & ",  " _
                                            & SetInteger(taxr_ac_ap_id.EditValue) & ",  " _
                                            & SetInteger(taxr_sb_ap_id.EditValue) & ",  " _
                                            & SetInteger(taxr_cc_ap_id.EditValue) & ",  " _
                                            & SetBitYN(taxr_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
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
                        set_row(Trim(_taxr_oid.ToString), "taxr_oid")
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
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _taxr_oid_mstr = .Item("taxr_oid").ToString
                taxr_tax_type.EditValue = .Item("taxr_tax_type")
                taxr_tax_class.EditValue = .Item("taxr_tax_class")
                taxr_date.DateTime = .Item("taxr_date")
                taxr_rate.Text = SetString(.Item("taxr_rate"))
                taxr_ac_sales_id.EditValue = .Item("taxr_ac_sales_id")
                taxr_sb_sales_id.EditValue = .Item("taxr_sb_sales_id")
                taxr_cc_sales_id.EditValue = .Item("taxr_cc_sales_id")
                taxr_ac_ap_id.EditValue = .Item("taxr_ac_ap_id")
                taxr_sb_ap_id.EditValue = .Item("taxr_sb_ap_id")
                taxr_cc_ap_id.EditValue = .Item("taxr_cc_ap_id")
                taxr_active.EditValue = SetBitYNB(.Item("taxr_active"))
            End With
            taxr_tax_class.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.taxr_mstr   " _
                                            & "SET  " _
                                            & "  taxr_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  taxr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  taxr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & "  taxr_tax_type = " & SetInteger(taxr_tax_type.EditValue) & ",  " _
                                            & "  taxr_tax_class = " & SetInteger(taxr_tax_class.EditValue) & ",  " _
                                            & "  taxr_date = " & SetDate(taxr_date.DateTime) & ",  " _
                                            & "  taxr_rate = " & SetDbl(taxr_rate.EditValue) & ",  " _
                                            & "  taxr_ac_sales_id = " & SetInteger(taxr_ac_sales_id.EditValue) & ",  " _
                                            & "  taxr_sb_sales_id = " & SetInteger(taxr_sb_sales_id.EditValue) & ",  " _
                                            & "  taxr_cc_sales_id = " & SetInteger(taxr_cc_sales_id.EditValue) & ",  " _
                                            & "  taxr_ac_ap_id = " & SetInteger(taxr_ac_ap_id.EditValue) & ",  " _
                                            & "  taxr_sb_ap_id = " & SetInteger(taxr_sb_ap_id.EditValue) & ",  " _
                                            & "  taxr_cc_ap_id = " & SetInteger(taxr_cc_ap_id.EditValue) & ",  " _
                                            & "  taxr_active = " & SetBitYN(taxr_active.EditValue) & ",  " _
                                            & "  taxr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  taxr_oid = " & SetSetring(_taxr_oid_mstr.ToString) & "  " _
                                            & ";"

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
                        set_row(_taxr_oid_mstr.ToString, "taxr_oid")
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
                            .Command.CommandText = "delete from taxr_mstr where taxr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("taxr_oid").ToString + "'"
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
