Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FLocationSub
    Dim _wh_oid_mstr As String
    Dim _id As Integer
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FWarehouse_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_req1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_req1.DataTable1)
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))

        With locs_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID Sub", "losc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name Sub", "locs_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks Sub", "locs_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Location ID", "locs_loc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location Desc", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "locs_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "locs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "locs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "locs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "locs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr_to(locs_en_id.EditValue))
        locs_loc_id.Properties.DataSource = dt_bantu
        locs_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        locs_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        locs_loc_id.ItemIndex = 0

    End Sub

    Private Sub wh_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles locs_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Function get_sequel() As String
     
        get_sequel = "SELECT  " _
                & "  a.locs_en_id, " _
                & "  b.en_desc, " _
                & "  a.locs_loc_id, " _
                & "  c.loc_code, " _
                & "  c.loc_desc, " _
                & "  a.losc_id, " _
                & "  a.locs_name, " _
                & "  a.locs_remarks, " _
                & "  a.locs_active, " _
                & "  a.locs_add_date, " _
                & "  a.locs_add_by, " _
                & "  a.locs_upd_date, " _
                & "  a.locs_upd_by " _
                & "FROM " _
                & "  public.locs_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.locs_en_id = b.en_id) " _
                & "  INNER JOIN public.loc_mstr c ON (a.locs_loc_id = c.loc_id) " _
                & " where locs_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "order by loc_desc"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        locs_en_id.Focus()
        locs_en_id.ItemIndex = 0
        locs_remarks.Text = ""
        locs_name.Text = ""
        locs_active.EditValue = True
        locs_loc_id.ItemIndex = 0
    End Sub

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(wh_id as varchar),5,100) as integer)),0) as max_col  from wh_mstr " + _
                                           " where substring(cast(wh_id as varchar),5,100) <> ''"
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

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _wh_oid As Guid
        _wh_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _wh_id As Integer
        _wh_id = SetInteger(func_coll.GetID("locs_mstr", "losc_id"))

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
                                    & "  public.locs_mstr " _
                                    & "( " _
                                    & "  locs_en_id, " _
                                    & "  losc_id, " _
                                    & "  locs_loc_id, " _
                                    & "  locs_add_date, " _
                                    & "  locs_add_by, " _
                                    & "  locs_name, " _
                                    & "  locs_remarks, " _
                                    & "  locs_active " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(locs_en_id.EditValue) & ",  " _
                                    & SetInteger(_wh_id) & ",  " _
                                    & SetSetring(locs_loc_id.EditValue) & ",  " _
                                    & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & SetSetring(locs_name.EditValue) & ",  " _
                                    & SetSetring(locs_remarks.EditValue) & ",  " _
                                    & SetBitYN(locs_active.EditValue) & "  " _
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
                _id = .Item("losc_id")
                locs_en_id.EditValue = .Item("locs_en_id")
                locs_loc_id.EditValue = .Item("locs_loc_id")
                'locs_id.Text = .Item("locs_id")
                locs_name.EditValue = .Item("locs_name")
                locs_remarks.EditValue = .Item("locs_remarks")
                'wh_parent.EditValue = .Item("wh_parent")
                locs_active.EditValue = IIf(.Item("locs_active") = "Y", True, False)
            End With
            locs_en_id.Focus()
            'wh_parent.Enabled = False
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
                                    & "  public.locs_mstr  " _
                                    & "SET  " _
                                    & "  locs_en_id = " & SetInteger(locs_en_id.EditValue) & ",  " _
                                    & "  locs_loc_id = " & SetSetring(locs_loc_id.EditValue) & ",  " _
                                    & "  locs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  locs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  locs_name = " & SetSetring(locs_name.EditValue) & ",  " _
                                    & "  locs_remarks = " & SetSetring(locs_remarks.EditValue) & ",  " _
                                    & "  locs_active = " & SetBitYN(locs_active.EditValue) & "  " _
                                    & "WHERE  " _
                                    & "  losc_id = " & SetInteger(_id) & " "


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
                            .Command.CommandText = "delete from locs_mstr where losc_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("losc_id")) + ""
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

    Public Overrides Sub preview()

        '_en_id = le_entity.EditValue
        '_type = 10
        '_table = "pb_mstr"
        '_initial = "pb"
        '_code_awal = Trim(be_first.Text)
        '_code_akhir = Trim(be_to.Text)


        Try
            Dim ssql As String
            ssql = "SELECT  " _
                & "  a.locs_en_id, " _
                & "  a.locs_loc_id, " _
                & "  c.loc_code, " _
                & "  c.loc_desc, " _
                & "  a.losc_id, " _
                & "  to_char(a.losc_id,'FM999999999999999999') || ' ' || a.locs_name as locs_name, " _
                & "  a.locs_remarks, " _
                & "  a.locs_active, " _
                & "  b.en_desc " _
                & "FROM " _
                & "  public.locs_mstr a " _
                & "  INNER JOIN public.loc_mstr c ON (a.locs_loc_id = c.loc_id) " _
                & "  INNER JOIN public.en_mstr b ON (a.locs_en_id = b.en_id) " _
                & " Where locs_active='Y' " _
                & "ORDER BY " _
                & "  a.locs_name"



            Dim rpt As New rptLabelLocationSub
            With rpt
                Dim ds As New DataSet
                ds = ReportDataset(ssql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If


                .DataSource = ds
                .DataMember = "Table"

                ' Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Location Sub"
                ''.PrintingSystem = ps
                'printTool.ShowPreview()

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PreviewForm.Text = "Location Sub"
                printTool.ShowPreview()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub
End Class

