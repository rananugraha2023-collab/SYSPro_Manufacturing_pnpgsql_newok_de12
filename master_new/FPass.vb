Imports npgsql
Imports master_new.ModFunction
Imports System.Net
Imports System.IO
Imports System.Management

Public Class FPass

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    Public _sama As Boolean = False
    Public Overridable Sub login()

    End Sub

    Private Sub update_last_access()
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
                        .Command.CommandText = "update tconfuser set last_access = current_date + current_time" + _
                                               " where userid = " + master_new.ClsVar.sUserID.ToString
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
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message, "Error Update Last Access", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Update Last Access", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub


    Private Function before_login() As Boolean
        before_login = True
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select u.userid, groupid, form_skin, mainmenu_style,usernik, time_reminder, grid_style, coalesce(id_karyawan,-1) as id_karyawan " _
                         + " from tconfuser u " _
                         + " left outer join tconfskin s on u.userid = s.userid " _
                         + " where usernama = '" + Trim(txt_username.Text) + "'" _
                         + " and password = " + SetSetring(txt_password.Text.Trim) + " and useractive='Y'"

                    '+ " and password = md5('" + Trim(txt_password.Text) + "')"
                    '+ " and password = '" + Trim(txt_password.Text) + "'"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader

                    master_new.ClsVar.CExit = True

                    If .DataReader.HasRows = False Then
                        before_login = False
                    Else

                        Dim ssql As String = ""


                        While .DataReader.Read()
                            'If get_conf_file("app_code") = "SEA" Then
                            '    ssql = "SELECT emp_active from emp_mstr where emp_nik_old=" & SetSetring(.DataReader.Item("usernik"))

                            '    Dim dt_nik As New DataTable
                            '    dt_nik = master_new.PGSqlConn.GetTableData(ssql)
                            '    Dim _status = "N"
                            '    For Each dr_nik As DataRow In dt_nik.Rows
                            '        _status = SetString(dr_nik(0))
                            '    Next

                            '    If SetString(_status) = "N" Then
                            '        Box("Status user belum aktif atau belum disetting NIK Karyawan")
                            '        before_login = False
                            '        Exit Function
                            '    End If
                            'Else
                            '    Dim _alamat_api As String = "http://" & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "url_nik") & "/allgateway/_mob_login.php?variable=" & .DataReader.Item("usernik")
                            '    Dim result As String
                            '    result = run_get_to_api(_alamat_api & "")

                            '    If SetString(result).Contains("Y") Then
                            '        '_MakeReport("Data empty")

                            '    Else
                            '        Box("Status user belum aktif atau belum disetting NIK Karyawan")
                            '        before_login = False
                            '        Exit Function
                            '    End If
                            'End If


                            master_new.ClsVar.sUserID = .DataReader.Item("UserID")
                            master_new.ClsVar.sPassword = MD5Encrypt(Trim(txt_password.Text))
                            master_new.ClsVar.sGroupID = .DataReader.Item("GroupID")
                            master_new.ClsVar.sNama = Trim(txt_username.Text)
                            master_new.ClsVar.sFormSkin = IIf(.DataReader.Item("form_skin") Is System.DBNull.Value, "Office2003", .DataReader.Item("form_skin"))
                            master_new.ClsVar.SMainMenuStyle = IIf(.DataReader.Item("mainmenu_style") Is System.DBNull.Value, "NavigationPane", .DataReader.Item("mainmenu_style")) '.DataReader.Item("mainmenu_style")
                            master_new.ClsVar.sGridStyle = IIf(.DataReader.Item("grid_style") Is System.DBNull.Value, "Blue Office", .DataReader.Item("grid_style")) '.DataReader.Item("grid_style")
                            master_new.ClsVar.sKaryawanID = .DataReader.Item("id_karyawan")
                            master_new.ClsVar.sTimeInterval = .DataReader.Item("time_reminder")
                            master_new.ClsVar._status_sync = master_new.PGSqlConn.status_sync

                        End While

                        If txt_username.Text.ToLower = txt_password.Text.ToLower Then
                            _sama = True
                            Box("Untuk keamanan data segera ganti password Anda")
                        End If

                    End If
                    master_new.ClsVar.sdom_id = le_domain.EditValue
                    master_new.ClsVar.ibase_cur_id = le_domain.GetColumnValue("dom_base_cur_id")
                    'master_new.ClsVar.sNamaKaryawan = func_coll.get_nama_karyawan(master_new.ClsVar.sKaryawanID)
                End With
            End Using
        Catch ex As Exception
            before_login = False
            'Application.Exit()
            'Close()
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(server_code,'zz') as server_code from tconfsetting "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader

                    While .DataReader.Read()
                        master_new.ClsVar.sServerCode = .DataReader.Item("server_code")
                    End While

                    If master_new.ClsVar.sServerCode = "zz" Then
                        MessageBox.Show("Please Define Server Code First..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End With
            End Using
        Catch ex As Exception
            before_login = False
            MessageBox.Show(ex.Message)
        End Try
        Return before_login
    End Function
    Public Function run_get_to_api(ByVal par_variable As String) As String
        Try
            Dim request As WebRequest = WebRequest.Create(par_variable)

            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As WebResponse = request.GetResponse()
            ' Display the status.
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            Dim responseFromServer As String = reader.ReadToEnd()
            'Dim responseFromServer As String = reader.ReadToEnd()
            'Console.WriteLine(responseFromServer)
            Dim result = responseFromServer 'JsonConvert.DeserializeObject(Of ArrayList)(responseFromServer)

            Return result
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Public Function get_conf_file(ByVal par_type As String) As String
        get_conf_file = ""
        Try
            Dim dr As DataRow
            Dim ssql As String
            ssql = "select conf_value from conf_file " + _
                    " where conf_name = '" + par_type + "'"

            dr = master_new.PGSqlConn.GetRowInfo(ssql)
            If dr Is Nothing Then
                'Box("Sorry, configuration " & par_type & " doesn't exist")
                ' get_conf_file
            ElseIf dr(0) Is System.DBNull.Value Then
                Box("Sorry, configuration " & par_type & " is null")
            Else
                get_conf_file = dr(0).ToString
            End If

            'Using objkalendar As New master_new.WDABasepgsql("", "")
            '    With objkalendar
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "select conf_value from conf_file " + _
            '                               " where conf_name = '" + par_type + "'"
            '        .InitializeCommand()
            '        .DataReader = .Command.ExecuteReader
            '        While .DataReader.Read
            '            get_conf_file = .DataReader.Item("conf_value")
            '        End While
            '    End With
            'End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_conf_file
    End Function

    Private Sub FPass_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub FPass_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    'Private Sub FPass_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    txt_username.Focus()
    'End Sub

    Public Overridable Sub FPass_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DevExpress.UserSkins.BonusSkins.Register()
        '        DevExpress.UserSkins.OfficeSkins


        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "user_default") <> "" Then
            txt_username.Text = konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "user_default")
        End If

        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "pass_default") <> "" Then
            txt_password.Text = konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "pass_default")
        End If

        LogoPictureBox.ImageLocation = appbase() + "\zpendukung\logo_front.jpg" '"\\192.168.1.150\Soft\_syssetiadi\publish sygma\logo.jpg"
        Try
            LogoPictureBox.Load()
        Catch ex As Exception
        End Try


        'jika ada file dilocal maka pake yang local jika tidak maka memakai gambar yang ada diprogram
        'Dim status_file As String = DevExpress.Utils.FilesHelper.FindingFileName("c:\syspro", "logo.jpg", False)
        'If status_file = "" Then

        'Else
        'LogoPictureBox.ImageLocation = "C:\syspro\logo.jpg"
        'End If

        'ToolTipController1.ShowHint("Untuk Mengubah Gambar, Silahkan Buat File .jpg di folder C:\syspro\ Dengan nama logo.jpg", "Informasi", LogoPictureBox, DevExpress.Utils.ToolTipLocation.BottomLeft)

        'Try
        '    Dim sSql As String = "select dom_id, dom_code, dom_base_cur_id from dom_mstr where dom_active ~~* 'Y' and dom_id > 0"
        '    Dim dt As New DataTable
        '    dt = master_new.PGSqlConn.GetTableData(sSql)
        '    le_domain.Properties.DataSource = dt
        '    le_domain.Properties.DisplayMember = dt.Columns("dom_code").ToString
        '    le_domain.Properties.ValueMember = dt.Columns("dom_id").ToString
        '    le_domain.ItemIndex = 0

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select dom_id, dom_code, dom_base_cur_id from dom_mstr where dom_active ~~* 'Y' and dom_id > 0"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "domain")
                    le_domain.Properties.DataSource = ds_cb.Tables("domain")
                    le_domain.Properties.DisplayMember = ds_cb.Tables("domain").Columns("dom_code").ToString
                    le_domain.Properties.ValueMember = ds_cb.Tables("domain").Columns("dom_id").ToString
                    le_domain.ItemIndex = 0
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_cancel.Click
        'Try
        '    MessageBox.Show(LogoPictureBox.ImageLocation)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        'Try
        '    MessageBox.Show(appbase() + "\zpendukung\logo_front.jpg")
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Me.Close()
    End Sub

    Public Overridable Sub sb_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_ok.Click
        setconnection()
        If before_login() = True Then
            update_last_access()
            txt_username.Text = ""
            txt_password.Text = ""
            txt_username.Focus()

            Dim Oid As Guid = Guid.NewGuid()
            master_new.ClsVar._oid = Oid
            Dim ssqls As New ArrayList

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            '.Command.CommandType = CommandType.StoredProcedure
                            '.Command.CommandText = "userac_accs_insert"
                            '.AddParameter("@userac_oid", Oid)
                            '.AddParameter("@userac_user_id", master_new.ClsVar.sUserID)

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.userac_accs " _
                                                & "( " _
                                                & "  userac_oid, " _
                                                & "  userac_user_id, " _
                                                & "  userac_login_date,userac_user_computer,userac_ip_address,userac_computer_name,userac_user_syspro,userac_syspro_version " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sUserID) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetSetring(SystemInformation.UserName) & "," & SetSetring(master_new.ModFunction.IPAddresses(System.Net.Dns.GetHostName)) _
                                                & "," & SetSetring(System.Net.Dns.GetHostName) & "," & SetSetring(master_new.ClsVar.sNama) & "," & SetSetring(master_new.ModFunction.GetVersion) _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            Dim hasil As New ArrayList
                            hasil.Add(System.Net.Dns.GetHostName & " " & SystemInformation.UserName & " " & IPAddresses(System.Net.Dns.GetHostName))

                            InsertInfo2("Win32_Registry", hasil, "Name,InstallDate")
                            InsertInfo2("Win32_Processor", hasil, "Name,ProcessorId,NumberOfCores,NumberOfLogicalProcessors")
                            InsertInfo2("Win32_PhysicalMemory", hasil, "BankLabel,Capacity,Manufacturer,PartNumber,SerialNumber,Speed")
                            InsertInfo2("Win32_BaseBoard", hasil, "Manufacturer,Product,SerialNumber,Version")
                            InsertInfo2("Win32_Bios", hasil, "BIOSVersion,ReleaseDate")
                            InsertInfo2("Win32_VideoController", hasil, "AdapterRAM,Caption,VideoModeDescription")
                            InsertInfo2("Win32_DiskDrive", hasil, "Caption,Partitions,SerialNumber,Size")
                            InsertInfo2("Win32_LogicalDisk", hasil, "Caption,Size,FreeSpace")

                            hasil = hasil

                            Dim _hasil As String = ""

                            For Each Data As String In hasil
                                _hasil = _hasil & Data & vbNewLine
                            Next

                            'Dim dt_pc As New DataTable
                            'dt_pc = PGSqlConn.GetTableData("SELECT * from pc_mstr where pc_name=" & SetSetring(System.Net.Dns.GetHostName) _
                            '                               & " and pc_user=" & SetSetring(SystemInformation.UserName) _
                            '                               & " and pc_syspro=" & SetSetring(ClsVar.sNama))

                            'If dt_pc.Rows.Count > 0 Then

                            'Else




                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "DELETE from  " _
                                                & "  public.pc_mstr " _
                                                & " where " _
                                                & "pc_name=" & SetSetring(System.Net.Dns.GetHostName) _
                                               & " and pc_user=" & SetSetring(SystemInformation.UserName) _
                                               & " and pc_syspro=" & SetSetring(ClsVar.sNama)


                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pc_mstr " _
                                                & "( " _
                                                & "  pc_oid, " _
                                                & "  pc_name, " _
                                                & "  pc_user,pc_syspro, " _
                                                & "  pc_detail " _
                                                & ") " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(System.Net.Dns.GetHostName) & ",  " _
                                                & SetSetring(SystemInformation.UserName) & ",  " _
                                                & SetSetring(ClsVar.sNama) & ",  " _
                                                & SetSetring(_hasil) & "  " _
                                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'End If


                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If
                            sqlTran.Commit()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            login()
        Else
            txt_username.Text = ""
            txt_password.Text = ""
            txt_username.Focus()
            MessageBox.Show("Maaf User / Password Yang Anda Masukan Salah.." + Chr(13) + "Silahkan Hubungi SysAdmin Untuk Meminta User dan Password Apabila Anda Belum Punya", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Overridable Sub setconnection()

    End Sub

    Private Sub le_domain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles le_domain.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                sb_ok_Click(sender, e)
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub InsertInfo2(ByVal Key As String, ByRef lst As ArrayList, ByVal par_filter As String)
        'lst.Clear()
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher("select * from " & Key)

        Try

            For Each share As ManagementObject In searcher.[Get]()
                'Dim grp As ListViewGroup

                Try
                    'grp = lst.Groups.Add(share("Name").ToString(), share("Name").ToString())

                    ' lst.Add("@" & Key & ":" & share("Name").ToString() & "")
                Catch
                    'lst.Add("@" & Key & ":" & share.ToString() & "")
                    'grp = lst.Groups.Add(share.ToString(), share.ToString())
                End Try

                If share.Properties.Count <= 0 Then
                    'MessageBox.Show("No Information Available", "No Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                For Each PC As PropertyData In share.Properties
                    'Dim item As ListViewItem = New ListViewItem(grp)

                    'If lst.Items.Count Mod 2 <> 0 Then
                    '    item.BackColor = Color.White
                    'Else
                    '    item.BackColor = Color.WhiteSmoke
                    'End If

                    'item.Text = PC.Name

                    If PC.Value IsNot Nothing AndAlso PC.Value.ToString() <> "" Then

                        Dim i As String
                        Dim a() As String
                        Dim j As Integer
                        i = par_filter
                        a = i.Split(",")
                        For j = 0 To a.GetUpperBound(0)
                            If PC.Name.ToLower = a(j).ToLower Then

                                Select Case PC.Value.[GetType]().ToString()
                                    Case "System.String[]"
                                        Dim str As String() = CType(PC.Value, String())
                                        Dim str2 As String = ""

                                        For Each st As String In str
                                            str2 += st & " "
                                        Next
                                        lst.Add("@" & Key & "$" & PC.Name & ":" & str2)
                                        'item.SubItems.Add(str2)
                                    Case "System.UInt16[]"
                                        Dim shortData As UShort() = CType(PC.Value, UShort())
                                        Dim tstr2 As String = ""

                                        For Each st As UShort In shortData
                                            tstr2 += st.ToString() & " "
                                        Next
                                        lst.Add("@" & Key & "$" & PC.Name & ":" & tstr2)
                                        'item.SubItems.Add(tstr2)
                                    Case Else
                                        lst.Add("@" & Key & "$" & PC.Name & ":" & PC.Value.ToString())
                                        'item.SubItems.Add(PC.Value.ToString())
                                End Select
                            End If
                        Next


                        'For Each Data As String In par_filter
                        '    If PC.Name.ToLower = Data.ToLower Then

                        '        Select Case PC.Value.[GetType]().ToString()
                        '            Case "System.String[]"
                        '                Dim str As String() = CType(PC.Value, String())
                        '                Dim str2 As String = ""

                        '                For Each st As String In str
                        '                    str2 += st & " "
                        '                Next
                        '                lst.Add("@" & Key & "$" & PC.Name & ":" & str2)
                        '                'item.SubItems.Add(str2)
                        '            Case "System.UInt16[]"
                        '                Dim shortData As UShort() = CType(PC.Value, UShort())
                        '                Dim tstr2 As String = ""

                        '                For Each st As UShort In shortData
                        '                    tstr2 += st.ToString() & " "
                        '                Next
                        '                lst.Add("@" & Key & "$" & PC.Name & ":" & tstr2)
                        '                'item.SubItems.Add(tstr2)
                        '            Case Else
                        '                lst.Add("@" & Key & "$" & PC.Name & ":" & PC.Value.ToString())
                        '                'item.SubItems.Add(PC.Value.ToString())
                        '        End Select
                        '    End If
                        'Next

                    Else

                        'If Not DontInsertNull Then
                        '    item.SubItems.Add("No Information available")
                        'Else
                        Continue For
                        'End If
                    End If

                    'lst.Items.Add(item)
                Next
            Next

        Catch exp As Exception
            'MessageBox.Show("can't get data because of the followeing error " & vbLf & exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

End Class
