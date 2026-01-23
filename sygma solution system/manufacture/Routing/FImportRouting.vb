Imports npgsql
Imports master_new.ModFunction

Public Class FImportRouting
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_bef_insert As DataSet
    Dim dt_table As DataTable

    Private Sub FImportRouting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_excel, "Code", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Operation", "rod_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Start Date", "rod_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "End Date", "rod_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Detail Description", "rod_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Work Center Code", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Work Center Desc.", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Machines per Operation", "rod_mch_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Overlap Unit", "rod_tran_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Queue Time", "rod_queue", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Wait Time", "rod_wait", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Move Time", "rod_move", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Run Time", "rod_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Setup Time", "rod_setup", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Yield Percent", "rod_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_excel, "Millstone Operation", "rod_milestone", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_excel, "Subcontract LT", "rod_sub_lead", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Setup Crew", "rod_setup_men", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Rod Crew", "rod_men_mch", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Tool Code Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Subcontract Cost", "rod_sub_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub


    Private Sub be_import_xls_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_import_xls.ButtonClick
        Dim opendialog As New OpenFileDialog
        Dim ds_import As DataSet

        ds_import = New DataSet
        If opendialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            be_import_xls.Text = opendialog.FileName
            ds_import = New DataSet
            ds_import = ImportFromExcel(be_import_xls.Text)

            ds_bef_insert = New DataSet
            ds_bef_insert = arrange_from_excel(ds_import)
            dt_table = ds_bef_insert.Tables(0)
            'dt_table.DefaultView.Sort = "ro_code ASC"

            gc_excel.DataSource = dt_table
            gv_excel.BestFitColumns()
        End If
    End Sub

    Public Function ImportFromExcel(ByVal PrmPathExcelFile As String) As DataSet
        ImportFromExcel = Nothing
        Dim MyConnection As System.Data.OleDb.OleDbConnection = Nothing

        Try
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [sheet1$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "routing")

            DtSet = New System.Data.DataSet

            MyCommand.Fill(DtSet)
            MyConnection.Close()

            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
        End Try
    End Function

    Public Function arrange_from_excel(ByVal _par_ds_import As DataSet) As DataSet
        Dim dr_list As DataRow
        Dim ds_arrange As DataSet = Nothing
        Dim ds_tool, ds_wc As DataSet
        Dim i As Integer

        If _par_ds_import.Tables(0).Rows.Count > 0 Then

            ds_arrange = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT " _
                                & "  ro_code, " _
                                & "  ro_desc, " _
                                & "  rod_op, " _
                                & "  rod_start_date, " _
                                & "  rod_end_date, " _
                                & "  rod_wc_id, " _
                                & "  rod_desc, " _
                                & "  rod_mch_op, " _
                                & "  rod_tran_qty, " _
                                & "  rod_queue, " _
                                & "  rod_wait, " _
                                & "  rod_move, " _
                                & "  rod_run, " _
                                & "  rod_setup, " _
                                & "  rod_yield_pct, " _
                                & "  rod_milestone, " _
                                & "  rod_sub_lead, " _
                                & "  rod_setup_men, " _
                                & "  rod_men_mch, " _
                                & "  rod_tool_code, " _
                                & "  rod_ptnr_id, " _
                                & "  rod_sub_cost, " _
                                & "  '' as wc_code, " _
                                & "  '' as wc_desc, " _
                                & "  '' as code_name, " _
                                & "  '' as ptnr_name " _
                                & "FROM  " _
                                & "  public.rod_det " _
                                & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid" _
                                & " where public.rod_det.rod_add_by = 'm/-n'"

                        .InitializeCommand()
                        .FillDataSet(ds_arrange, "arrange_ro")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            i = 0

            For Each _dr As DataRow In _par_ds_import.Tables(0).Rows
                If IsDBNull(_par_ds_import.Tables(0).Rows(i).Item("routing code")) = False Then
                    Try
                        dr_list = ds_arrange.Tables(0).NewRow
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Please Contact Your Administrator")
                        ds_arrange.Clear()
                        Return ds_arrange
                    End Try
                    dr_list("ro_code") = SetString(_par_ds_import.Tables(0).Rows(i).Item("routing code"))
                    dr_list("ro_desc") = SetString(_par_ds_import.Tables(0).Rows(i).Item("description"))
                    dr_list("rod_op") = SetInteger(_par_ds_import.Tables(0).Rows(i).Item("operation"))
                    If SetNull(_par_ds_import.Tables(0).Rows(i).Item("start date").ToString) <> "" Then
                        dr_list("rod_start_date") = _par_ds_import.Tables(0).Rows(i).Item("start date").ToString
                    End If

                    If SetNull(_par_ds_import.Tables(0).Rows(i).Item("end date").ToString) <> "" Then
                        dr_list("rod_end_date") = _par_ds_import.Tables(0).Rows(i).Item("end date").ToString
                    End If

                    dr_list("rod_desc") = SetString(_par_ds_import.Tables(0).Rows(i).Item("description"))

                    ds_wc = New DataSet
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "SELECT  wc_id, wc_code, wc_desc " _
                               & " FROM wc_mstr " _
                               & "  where lower(wc_code) = lower(" & SetSetring(_par_ds_import.Tables(0).Rows(i).Item("wc_desc")) & ") " _
                               & "  and wc_en_id = " & le_entity.EditValue
                                .InitializeCommand()
                                .FillDataSet(ds_wc, "wc_mstr")
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try


                    If ds_wc.Tables(0).Rows.Count > 0 Then
                        dr_list("rod_wc_id") = SetInteger(ds_wc.Tables(0).Rows(0).Item("wc_id"))
                        dr_list("wc_code") = SetString(ds_wc.Tables(0).Rows(0).Item("wc_code"))
                        dr_list("wc_desc") = SetString(ds_wc.Tables(0).Rows(0).Item("wc_desc"))
                    Else
                        MsgBox("Unknown WorkCenter Code At Line " & i + 1 & "....", MsgBoxStyle.Critical, "Workcenter Not Found")
                        ds_arrange.Clear()
                        be_import_xls.Text = ""
                        Return ds_arrange
                    End If

                    dr_list("rod_mch_op") = SetInteger(_par_ds_import.Tables(0).Rows(i).Item("machine"))
                    dr_list("rod_tran_qty") = SetInteger(_par_ds_import.Tables(0).Rows(i).Item("overlap un"))
                    dr_list("rod_queue") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("queue time"))
                    dr_list("rod_wait") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("wait time"))
                    dr_list("rod_move") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("move time"))
                    dr_list("rod_run") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("run time"))
                    dr_list("rod_setup") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("setup time"))
                    dr_list("rod_yield_pct") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("yield %"))
                    dr_list("rod_milestone") = SetString(_par_ds_import.Tables(0).Rows(i).Item("mpo"))
                    dr_list("rod_sub_lead") = SetInteger(_par_ds_import.Tables(0).Rows(i).Item("sub lt"))
                    dr_list("rod_setup_men") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("setup crew"))
                    dr_list("rod_men_mch") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("run crew"))

                    ds_tool = New DataSet
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "SELECT  code_id, code_name" _
                               & " FROM code_mstr " _
                               & "  where lower(code_code) = lower(" & SetSetring(_par_ds_import.Tables(0).Rows(i).Item("tool code")) & ") " _
                               & "  and code_en_id = " & le_entity.EditValue

                                .InitializeCommand()
                                .FillDataSet(ds_tool, "code_mstr")
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    If ds_tool.Tables(0).Rows.Count > 0 Then
                        dr_list("rod_tool_code") = SetInteger(ds_tool.Tables(0).Rows(0).Item("code_id"))
                        dr_list("code_name") = SetString(ds_tool.Tables(0).Rows(0).Item("code_name"))
                    Else
                        If Trim(_par_ds_import.Tables(0).Rows(i).Item(2).ToString) <> "" Then
                            MsgBox("Unknown Tool Code At Line " & i + 1 & "....", MsgBoxStyle.Critical, "Tool Code Not Found")
                            ds_arrange.Clear()
                            be_import_xls.Text = ""
                            Return ds_arrange
                        End If
                    End If

                    'ds_ptnr = New DataSet
                    'Try
                    '    Using objcb As New master_new.WDABasepgsql("", "")
                    '        With objcb
                    '            .SQL = "SELECT  ptnr_id, ptnr_name" _
                    '           & " FROM ptnr_mstr " _
                    '           & "  where lower(ptnr_name) = lower(" & SetSetring(_par_ds_import.Tables(0).Rows(i).Item("partner name")) & ") " _
                    '           & "  and ptnr_en_id = " & le_entity.EditValue
                    '            .InitializeCommand()
                    '            .FillDataSet(ds_ptnr, "ptnr_mstr")
                    '        End With
                    '    End Using
                    'Catch ex As Exception
                    '    MessageBox.Show(ex.Message)
                    'End Try

                    'If ds_ptnr.Tables(0).Rows.Count > 0 Then
                    '    dr_list("rod_ptnr_id") = SetInteger(ds_ptnr.Tables(0).Rows(0).Item("ptnr_id"))
                    '    dr_list("ptnr_name") = SetString(ds_ptnr.Tables(0).Rows(0).Item("ptnr_name"))
                    'Else
                    '    MsgBox("Unknown Partner Name At Line " & i + 1 & "....", MsgBoxStyle.Critical, "Partner Not Found")
                    '    be_import_xls.Text = ""
                    '    ds_arrange.Clear()
                    '    Return ds_arrange
                    'End If


                    dr_list("rod_sub_cost") = SetDbl(_par_ds_import.Tables(0).Rows(i).Item("subcont cost"))

                    ds_arrange.Tables(0).Rows.Add(dr_list)
                    i = i + 1
                End If
            Next

            ds_arrange.Tables(0).AcceptChanges()
            
        End If

        Return ds_arrange

    End Function


    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i As Integer
        Dim _ro_oid As Guid
        Dim _ro_id As Integer
        Dim _ro_code As String = ""
       

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'dikurangi 1 karena saat looping ditambah 1
                        _ro_id = SetInteger(func_coll.GetID("ro_mstr", le_entity.GetColumnValue("en_code"), "ro_id", "ro_en_id", le_entity.EditValue.ToString)) - 1

                        For i = 0 To dt_table.Rows.Count - 1
                            If dt_table.Rows(i).Item("ro_code").ToString <> _ro_code Then
                                _ro_oid = Guid.NewGuid
                                _ro_id = _ro_id + 1

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.ro_mstr " _
                                                    & "( " _
                                                    & "  ro_oid, " _
                                                    & "  ro_dom_id, " _
                                                    & "  ro_en_id, " _
                                                    & "  ro_add_by, " _
                                                    & "  ro_add_date, " _
                                                    & "  ro_id, " _
                                                    & "  ro_code, " _
                                                    & "  ro_desc, " _
                                                    & "  ro_active, " _
                                                    & "  ro_dt " _
                                                    & ") " _
                                                    & "VALUES (" _
                                                    & SetSetringDB(_ro_oid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(le_entity.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & SetInteger(_ro_id) & ",  " _
                                                    & SetSetringDB(dt_table.Rows(i).Item("ro_code")) & ",  " _
                                                    & SetSetringDB(dt_table.Rows(i).Item("ro_desc")) & ",  " _
                                                    & "'Y',  " _
                                                    & " current_timestamp " & "  " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _ro_code = dt_table.Rows(i).Item("ro_code").ToString
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.rod_det " _
                                            & "( " _
                                            & "  rod_oid, " _
                                            & "  rod_ro_oid, " _
                                            & "  rod_add_by, " _
                                            & "  rod_add_date, " _
                                            & "  rod_op, " _
                                            & "  rod_start_date, " _
                                            & "  rod_end_date, " _
                                            & "  rod_wc_id, " _
                                            & "  rod_desc, " _
                                            & "  rod_mch_op, " _
                                            & "  rod_tran_qty, " _
                                            & "  rod_queue, " _
                                            & "  rod_wait, " _
                                            & "  rod_move, " _
                                            & "  rod_run, " _
                                            & "  rod_setup, " _
                                            & "  rod_yield_pct, " _
                                            & "  rod_milestone, " _
                                            & "  rod_sub_lead, " _
                                            & "  rod_setup_men, " _
                                            & "  rod_men_mch, " _
                                            & "  rod_tool_code, " _
                                            & "  rod_ptnr_id, " _
                                            & "  rod_sub_cost, " _
                                            & "  rod_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetInteger(dt_table.Rows(i).Item("rod_op")) & ",  " _
                                            & SetDate(dt_table.Rows(i).Item("rod_start_date")) & ",  " _
                                            & SetDate(dt_table.Rows(i).Item("rod_end_date")) & ",  " _
                                            & SetInteger(dt_table.Rows(i).Item("rod_wc_id")) & ",  " _
                                            & SetSetringDB(dt_table.Rows(i).Item("rod_desc")) & ",  " _
                                            & SetIntegerDB(dt_table.Rows(i).Item("rod_mch_op")) & ",  " _
                                            & SetIntegerDB(dt_table.Rows(i).Item("rod_tran_qty")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_queue")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_wait")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_move")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_run")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_setup")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_yield_pct")) & ",  " _
                                            & SetSetring(dt_table.Rows(i).Item("rod_milestone").ToString.ToUpper) & ",  " _
                                            & SetIntegerDB(dt_table.Rows(i).Item("rod_sub_lead")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_setup_men")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_men_mch")) & ",  " _
                                            & SetIntegerDB(dt_table.Rows(i).Item("rod_tool_code")) & ",  " _
                                            & SetIntegerDB(dt_table.Rows(i).Item("rod_ptnr_id")) & ",  " _
                                            & SetDblDB(dt_table.Rows(i).Item("rod_sub_cost")) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        sqlTran.Commit()
                        MsgBox("Import Successfull", MsgBoxStyle.Information, "Data Has Been Imported")
                        be_import_xls.Text = ""
                        ds_bef_insert.Clear()
                        gc_excel.DataSource = ds_bef_insert.Tables(0)
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

    Public Function SetNull(ByVal _par_string As String) As String
        SetNull = "Null"

        If _par_string <> "-" Then
            SetNull = _par_string
        End If
    End Function
End Class
