Imports npgsql
Imports master_new.ModFunction

Public Class FQRSmartLogPrint
    Dim _invqr_oid_mstr As String
    Public dt_bantu As DataTable
    Dim ds_edit As DataSet
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _pt_id, _pi_id, _si_id As Integer
    Public _pi_oid As Integer

    Private Sub FQRSmartLogPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr())

        init_le(invcd_en_id, "en_mstr")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'invqr_en_id.Properties.DataSource = dt_bantu
        'invqr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'invqr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'invqr_en_id.ItemIndex = 0

        With invcd_si_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            .ItemIndex = 0
        End With

        'With invqr_pi_id
        '    dt_bantu = (func_data.load_pi_mstr_qr(invqr_en_id.EditValue))
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        '    .ItemIndex = 0
        'End With

    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "invr_pi_oid", False)
        'add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Pricelist Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Pricelist Name", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partnumber", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price P Jawa", "invqr_price1", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price L Jawa", "invqr_price2", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Print Date", "invqr_date_print", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Cost", "invqr_lead", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.invqr_table.invqr_oid, " _
                    & "  public.invqr_table.invqr_dom_id, " _
                    & "  public.invqr_table.invqr_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.invqr_table.invqr_pi_id, " _
                    & "  public.pi_mstr.pi_code, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.invqr_table.invqr_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.invqr_table.invqr_price1, " _
                    & "  public.invqr_table.invqr_price2, " _
                    & "  public.invqr_table.invqr_date_print, " _
                    & "  public.invqr_table.invqr_add_by, " _
                    & "  public.invqr_table.invqr_add_date, " _
                    & "  public.invqr_table.invqr_upd_date, " _
                    & "  public.invqr_table.invqr_upd_by " _
                    & "FROM " _
                    & "  public.invqr_table " _
                    & "  INNER JOIN public.en_mstr ON (public.invqr_table.invqr_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.invqr_table.invqr_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.pi_mstr ON (public.invqr_table.invqr_pi_id = public.pi_mstr.pi_id)"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        invcd_en_id.Focus()
        invcd_en_id.ItemIndex = 0
        'invqr_pi_desc.ItemIndex = 0
        'invqr_pi_id.Enabled = True

        invcd_pt_id.Text = ""
        'invqr_pi_desc.Text = ""
        invqr_price_jawa.EditValue = 0.0
        'invqr_price_ljawa.EditValue = 0.0
        _pt_id = -1

        '_pi_oid = ""
        '_pi_id = ""

    End Sub

    Public Overrides Function insert() As Boolean
        'Dim _riu_oid As Guid = Guid.NewGuid

        If _pt_id = -1 Then
            MessageBox.Show("Part Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim sql As String
        sql = "select * from invqr_table where invqr_pt_id=" & _pt_id
        Dim i, k, j As Integer
        Dim dt_cek As New DataTable
        dt_cek = master_new.PGSqlConn.GetTableData(sql)

        If dt_cek.Rows.Count > 0 Then
            MsgBox("Duplicate Partnumber")
            Return False
            Exit Function
        End If

        Dim ssqls As New ArrayList
        Dim _invqr_oid As Guid = Guid.NewGuid

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'Try
                        '    Using objinsert As New master_new.WDABasepgsql("", "")
                        '        With objinsert
                        '            .Connection.Open()
                        '            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()

                        '            Try
                        '                For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '                    .Command = .Connection.CreateCommand
                        '                    .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.invqr_table " _
                                            & "( " _
                                            & "  invqr_oid, " _
                                            & "  invqr_dom_id, " _
                                            & "  invqr_si_id, " _
                                            & "  invqr_en_id, " _
                                            & "  invqr_pt_id, " _
                                            & "  invqr_pi_id, " _
                                            & "  invqr_price1, " _
                                            & "  invqr_price2, " _
                                            & "  invqr_date_print, " _
                                            & "  invqr_add_by, " _
                                            & "  invqr_add_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_invqr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(invcd_si_id.EditValue) & ",  " _
                                            & SetSetring(invcd_en_id.EditValue) & ",  " _
                                            & SetSetring(_pt_id) & ",  " _
                                            & SetInteger(_pi_id) & ",  " _
                                            & SetDbl(invqr_price_jawa.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & " " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        'Update relation PR apabila terdapat relasi pr
                        'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("invqr_pt_id")) = False Then
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "update pt_mstr set pt_lead_time = coalesce(pt_lead_time,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("invqr_lead").ToString) _
                        '                         & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()

                        'Uodate status PPartnumber yang di monitor /rans 180422
                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "update pt_mstr set pt_monitor = 'Y'" _
                        '                     & " where pt_oiid = '" + ds_edit.Tables(0).Rows(i).Item("invqr_pt_id").ToString + "'" _
                        '                     & " and invqr_minitored = 'Y' "
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        'End If
                        'Next

                        sqlTran.Commit()

                        after_success()
                        set_row(_invqr_oid.ToString, "invqr_oid")
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
                _invqr_oid_mstr = .Item("invqr_oid").ToString
                'invqr_si_id.EditValue = .Item("invqr_si_id")
                _pt_id = .Item("invqr_pt_id")
                _pi_id = .Item("invqr_pi_id")
                invcd_pt_id.EditValue = .Item("invqr_pt_id")
                invcd_en_id.EditValue = .Item("invqr_en_id")
                invqr_price_jawa.EditValue = .Item("invqr_price1")

                'invqr_monitored.EditValue = .Item("invqr_monitored")
                'invqr_monitored.EditValue = SetBitYNB(.Item("invqr_monitored"))
            End With
            invcd_si_id.Focus()
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
                                            & "  public.invqr_table   " _
                                            & "SET  " _
                                            & "  invqr_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  invqr_si_id = " & SetInteger(invcd_si_id.EditValue) & ",  " _
                                            & "  invqr_en_id = " & SetInteger(invcd_en_id.EditValue) & ",  " _
                                            & "  invqr_pt_id = " & SetInteger(_pt_id) & ",  " _
                                            & "  invqr_pi_id = " & SetInteger(_pi_id) & ",  " _
                                            & "  invqr_price1 = " & SetDec(invqr_price_jawa.EditValue) & ",  " _
                                            & "  invqr_si_id = " & SetInteger(invcd_si_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  invqr_oid = " & SetSetring(_invqr_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'End If
                        sqlTran.Commit()

                        after_success()
                        set_row(_invqr_oid_mstr.ToString, "invqr_oid")
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
        'MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                            .Command.CommandText = "delete from invqr_table where invqr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invqr_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Item Site Cost " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'If master_new.PGSqlConn.status_sync = True Then
                            '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                            '        .Command.CommandType = CommandType.Text
                            '        .Command.CommandText = Data
                            '        .Command.ExecuteNonQuery()
                            '        .Command.Parameters.Clear()
                            '    Next
                            'End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Private Sub invqr_pi_desc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim frm As New FPriceListSearch()
        frm.set_win(Me)
        frm._en_id = invcd_en_id.EditValue
        'frm._pi_id = invqr_pi_id.EditValue
        'frm._obj = invqr_pi_desc
        'frm._objk = invqr_pi_desc
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    'Private Sub invqr_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles invqr_pt_id.ButtonClick
    '    Dim frm As New FPartNumberSearch()
    '    frm.set_win(Me)
    '    frm._obj = invqr_pt_id
    '    frm.type_form = True
    '    frm.ShowDialog()
    'End Sub

    Private Sub invqr_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles invcd_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._en_id = invcd_en_id.EditValue
        frm._obj = invcd_pt_id
        'frm._pi_id = invqr_pi_id.EditValue
        'frm._pi_id = _pi_id
        'frm._price = invqr_pi_id.EditValue
        'frm._objk = invqr_pi_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub



    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _ptcode, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invqr_en_id")
        _type = 10
        _table = "invqr_table"
        _initial = "qr"
        _ptcode = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code")
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        '_sql = "SELECT  " _
        '        & "  public.pt_mstr.pt_id, " _
        '        & "  public.pt_mstr.pt_code, " _
        '        & "  public.pt_mstr.pt_desc1, " _
        '        & "  public.pid_det.pid_pt_id, " _
        '        & "  public.pidd_det.pidd_area_id, " _
        '        & "  public.pidd_det.pidd_payment_type, " _
        '        & "  public.pidd_det.pidd_price, " _
        '        & "  public.pi_mstr.pi_id, " _
        '        & "  public.pi_mstr.pi_code, " _
        '        & "  public.pt_mstr.pt_syslog_code, " _
        '        & "  public.pi_mstr.pi_desc, " _
        '        & "  public.area_mstr.area_name " _
        '        & "FROM " _
        '        & "  public.pi_mstr " _
        '        & "  INNER JOIN public.pid_det ON (public.pi_mstr.pi_oid = public.pid_det.pid_pi_oid) " _
        '        & "  INNER JOIN public.pt_mstr ON (public.pid_det.pid_pt_id = public.pt_mstr.pt_id) " _
        '        & "  INNER JOIN public.pidd_det ON (public.pid_det.pid_oid = public.pidd_det.pidd_pid_oid) " _
        '        & "  INNER JOIN public.area_mstr ON (public.pidd_det.pidd_area_id = public.area_mstr.area_id) " _
        '        & "WHERE " _
        '        & "  public.pidd_det.pidd_payment_type = '9941' AND  " _
        '        & "  public.pi_mstr.pi_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_code") + "' AND  " _
        '        & "  public.pt_mstr.pt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") + "'"


        _sql = "SELECT  " _
                & "  public.invqr_table.invqr_pt_id, " _
                & "  public.pt_mstr.pt_code, " _
                & "  public.pt_mstr.pt_desc1, " _
                & "  public.invqr_table.invqr_price1, " _
                & "  public.invqr_table.invqr_price2, " _
                & "  public.invqr_table.invqr_date_print, " _
                & "  public.invqr_table.invqr_add_by, " _
                & "  public.invqr_table.invqr_add_date, " _
                & "  public.invqr_table.invqr_upd_date, " _
                & "  public.invqr_table.invqr_upd_by " _
                & "FROM " _
                & "  public.invqr_table " _
                & "  INNER JOIN public.pt_mstr ON (public.invqr_table.invqr_pt_id = public.pt_mstr.pt_id)" _
                & "WHERE " _
                & "public.pt_mstr.pt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "rptLabelPartnumber"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code")
        frm.ShowDialog()

    End Sub

End Class
