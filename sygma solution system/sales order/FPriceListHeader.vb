Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Imports System.Net
Imports System.Text
Imports System.IO

Public Class FPriceListHeader
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pi_oid_mstr As String
    Dim _now As DateTime

    Private Sub FPriceListHeader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pi_en_id.Properties.DataSource = dt_bantu
        pi_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pi_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pi_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_so_type())
        pi_so_type.Properties.DataSource = dt_bantu
        pi_so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        pi_so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        pi_so_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        pi_cu_id.Properties.DataSource = dt_bantu
        pi_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        pi_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        pi_cu_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()


        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", pi_en_id.EditValue))
        pi_ptnrg_id.Properties.DataSource = dt_bantu
        pi_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        pi_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        pi_ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_promo_mstr(pi_en_id.EditValue))
        pi_promo_id.Properties.DataSource = dt_bantu
        pi_promo_id.Properties.DisplayMember = dt_bantu.Columns("promo_desc").ToString
        pi_promo_id.Properties.ValueMember = dt_bantu.Columns("promo_id").ToString
        pi_promo_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_sales_program(pi_en_id.EditValue))
        'pi_sls_program.Properties.DataSource = dt_bantu
        'pi_sls_program.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'pi_sls_program.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'pi_sls_program.ItemIndex = 0


        dt_bantu = New DataTable
        dt_bantu = func_data.load_sales_program()

        With pi_sls_program
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("sls_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("sls_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Private Sub pi_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pi_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID", "pi_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Type", "pi_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Programe", "sls_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "pi_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "pi_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "pi_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sync WEB", "pi_sync", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "pi_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pi_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pi_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pi_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT a.pi_oid, " _
                    & "  a.pi_dom_id, " _
                    & "  a.pi_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.pi_add_by, " _
                    & "  a.pi_add_date, " _
                    & "  a.pi_upd_by, " _
                    & "  a.pi_upd_date, " _
                    & "  a.pi_id, " _
                    & "  a.pi_code, " _
                    & "  a.pi_desc, " _
                    & "  a.pi_so_type, " _
                    & "  a.pi_promo_id, " _
                    & "  c.promo_desc, " _
                    & "  a.pi_cu_id, " _
                    & "  d.cu_name, " _
                    & "  a.pi_sls_program, " _
                    & "  a.pi_start_date, " _
                    & "  a.pi_end_date, " _
                    & "  a.pi_active, " _
                    & "  a.pi_dt, " _
                    & "  a.pi_ptnrg_id, " _
                    & "  f.ptnrg_name, " _
                    & "  coalesce(a.pi_sync, 'N') AS pi_sync, " _
                    & "  a.pi_is_project " _
                    & "FROM public.pi_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON a.pi_en_id = b.en_id " _
                    & "  INNER JOIN public.promo_mstr c ON a.pi_promo_id = c.promo_id " _
                    & "  INNER JOIN public.cu_mstr d ON a.pi_cu_id = d.cu_id " _
                    & "  LEFT JOIN public.ptnrg_grp f ON a.pi_ptnrg_id = f.ptnrg_id " _
                    & " where pi_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        pi_en_id.Focus()
        pi_en_id.ItemIndex = 0
        pi_code.Text = ""
        pi_desc.Text = ""
        pi_so_type.ItemIndex = 0
        pi_promo_id.ItemIndex = 0
        pi_cu_id.ItemIndex = 0
        pi_sls_program.ItemIndex = 0
        pi_sls_event.ItemIndex = 0
        pi_start_date.DateTime = _now
        pi_end_date.DateTime = _now
        pi_active.EditValue = True
        pi_ptnrg_id.ItemIndex = 0
        pi_is_internal.Checked = False

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pi_oid As Guid = Guid.NewGuid
        Dim i As Integer = 0
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
                                            & "  public.pi_mstr " _
                                            & "( " _
                                            & "  pi_oid, " _
                                            & "  pi_dom_id, " _
                                            & "  pi_en_id, " _
                                            & "  pi_add_by, " _
                                            & "  pi_add_date, " _
                                            & "  pi_id, " _
                                            & "  pi_code, " _
                                            & "  pi_desc, " _
                                            & "  pi_so_type, " _
                                            & "  pi_promo_id, " _
                                            & "  pi_cu_id, " _
                                            & "  pi_sls_program, " _
                                            & "  pi_start_date, " _
                                            & "  pi_end_date,pi_ptnrg_id, " _
                                            & "  pi_active, " _
                                            & "  pi_is_internal, " _
                                            & "  pi_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pi_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pi_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("pi_mstr", pi_en_id.GetColumnValue("en_code"), "pi_id", "pi_en_id", pi_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(pi_code.Text) & ",  " _
                                            & SetSetring(pi_desc.Text) & ",  " _
                                            & SetSetring(pi_so_type.EditValue) & ",  " _
                                            & SetInteger(pi_promo_id.EditValue) & ",  " _
                                            & SetInteger(pi_cu_id.EditValue) & ",  " _
                                            & SetInteger(pi_sls_program.EditValue) & ",  " _
                                            & SetDate(pi_start_date.DateTime) & ",  " _
                                            & SetDate(pi_end_date.DateTime) & ",  " _
                                             & SetInteger(pi_ptnrg_id.EditValue) & ",  " _
                                            & SetBitYN(pi_active.EditValue) & ",  " _
                                            & SetBitYN(pi_is_internal.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
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
                        set_row(_pi_oid.ToString, "pi_oid")
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
                            .Command.CommandText = "delete from pi_mstr where pi_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid").ToString + "'"
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pi_oid_mstr = .Item("pi_oid").ToString
                pi_en_id.EditValue = .Item("pi_en_id")
                pi_code.Text = .Item("pi_code")
                pi_desc.Text = SetString(.Item("pi_desc"))
                pi_so_type.EditValue = .Item("pi_so_type")
                pi_promo_id.EditValue = .Item("pi_promo_id")
                pi_cu_id.EditValue = .Item("pi_cu_id")
                pi_sls_program.EditValue = .Item("pi_sls_program")
                pi_start_date.DateTime = .Item("pi_start_date")
                pi_end_date.DateTime = .Item("pi_end_date")
                pi_active.EditValue = SetBitYNB(.Item("pi_active"))
                pi_is_internal.EditValue = SetBitYNB(.Item("pi_is_internal"))
                pi_ptnrg_id.EditValue = .Item("pi_ptnrg_id")
            End With

            pi_en_id.Focus()

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim i As Integer = 0
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
                                            & "  public.pi_mstr   " _
                                            & "SET  " _
                                            & "  pi_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pi_en_id = " & SetInteger(pi_en_id.EditValue) & ",  " _
                                            & "  pi_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pi_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  pi_code = " & SetSetring(pi_code.Text) & ",  " _
                                            & "  pi_desc = " & SetSetring(pi_desc.Text) & ",  " _
                                            & "  pi_so_type = " & SetSetring(pi_so_type.EditValue) & ",  " _
                                            & "  pi_promo_id = " & SetInteger(pi_promo_id.EditValue) & ",  " _
                                            & "  pi_ptnrg_id = " & SetInteger(pi_ptnrg_id.EditValue) & ",  " _
                                            & "  pi_cu_id = " & SetInteger(pi_cu_id.EditValue) & ",  " _
                                            & "  pi_sls_program = " & SetSetring(pi_sls_program.EditValue) & ",  " _
                                            & "  pi_start_date = " & SetDate(pi_start_date.DateTime) & ",  " _
                                            & "  pi_end_date = " & SetDate(pi_end_date.DateTime) & ",  " _
                                            & "  pi_active = " & SetBitYN(pi_active.EditValue) & ",  " _
                                            & "  pi_is_internal = " & SetBitYN(pi_is_internal.EditValue) & ",  " _
                                            & "  pi_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pi_oid = " & SetSetring(_pi_oid_mstr) & " "
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
                        set_row(_pi_oid_mstr, "pi_oid")
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

    Private Sub BtSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSend.Click
        Try
            '          System.Net.ServicePointManager.ServerCertificateValidationCallback = _
            'Function(se As Object, _
            'cert As System.Security.Cryptography.X509Certificates.X509Certificate, _
            'chain As System.Security.Cryptography.X509Certificates.X509Chain, _
            'sslerror As System.Net.Security.SslPolicyErrors) True

            'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls

            'ServicePointManager.SecurityProtocol = CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType)

            'Dim request As WebRequest = WebRequest.Create("https://localhost:5001/api/test")
            'request.Method = "POST"
            'Dim postData As String = "{""pi_id"":102}"
            'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            'request.ContentType = "application/json"
            'request.ContentLength = byteArray.Length

            'Dim dataStream As Stream = request.GetRequestStream()
            'dataStream.Write(byteArray, 0, byteArray.Length)
            'dataStream.Close()
            'Dim response As WebResponse = request.GetResponse()
            ''/Console.WriteLine((CType(response, HttpWebResponse)).StatusDescription)

            'dataStream = response.GetResponseStream()
            'Dim reader As StreamReader = New StreamReader(dataStream)
            'Dim responseFromServer As String = reader.ReadToEnd()
            ''Console.WriteLine(responseFromServer)


            'response.Close()

            'System.Net.ServicePointManager.ServerCertificateValidationCallback = Nothing

            If func_coll.get_conf_file("syspro_approval_code") = "DUTA" Then
               
            ElseIf func_coll.get_conf_file("syspro_approval_code") = "SDI" Then
             
            Else
                Exit Sub
            End If


            If ds.Tables.Count = 0 Then
                MsgBox("Nothing data")
                Exit Sub
            End If

            Dim _status As String



            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_active") = "Y" Then
                _status = "kirim data"
                'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_sync") = "Y" Then
                '    Box("Data have been sync")
                '    Exit Sub
                'End If
            Else
                _status = "non aktifkan data"
            End If



            If MessageBox.Show("Yakin akan " + _status + " ini ?", "Sync", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If



            Dim ssql As String
            Dim result As Object
            Dim _url As String
            Dim _err As New ArrayList

            If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                _url = func_coll.get_conf_file("server_api_online")
            Else
                _url = func_coll.get_conf_file("server_api_local")
            End If

            _url = _url & "php56/json/curl_api_sdi_pl.php?" & "status=" & _
            ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_active") & "&pl_id=" & _
            ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_id")
            'MsgBox(_url)
            result = get_data_from_api(_url, _err)

            If result Is Nothing Then
                If _err.Count > 0 Then
                    'LblStatusGen.Text = _err.Item(0).ToString
                    MsgBox(_err.Item(0).ToString)
                End If

                '_MakeReport("Data empty")
                Exit Sub
            Else

                If result.ToString.ToLower.Contains("error") Then
                    MsgBox("Failed : " & result)
                Else
                    ssql = "update pi_mstr set pi_sync='Y' where pi_id=" & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_id"))
                    Dim ssqls As New ArrayList
                    ssqls.Add(ssql)

                    If master_new.PGSqlConn.status_sync = True Then
                        If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                            'Return False
                            Exit Sub
                        End If
                        ssqls.Clear()
                    Else
                        If DbRunTran(ssqls, "") = False Then
                            'Return False
                            Exit Sub
                        End If
                        ssqls.Clear()
                    End If



                    MsgBox("Success")
                End If

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
