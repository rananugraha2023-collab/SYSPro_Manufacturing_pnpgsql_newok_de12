Imports System.Net
Imports System.Threading
Imports System.ComponentModel
Imports System.Net.Sockets
Imports System.Windows.Forms
Imports System

Public Class frmServer
    Delegate Sub setTextCallback(ByVal txt As String)
    Private serversocket As System.Net.Sockets.Socket
    Private clientsocket As System.Net.Sockets.Socket
    Public Event updatetext(ByVal str As String)
    Dim i As Integer = 0

    Private Sub frmServer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            serversocket.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmServer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        load_server()
    End Sub

    Private Sub myevent_update(ByVal str As String) Handles Me.updatetext
        ListBox1.Items.Add(str)
    End Sub
    Private Sub load_server()
        Try
            serversocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            Dim myip As IPAddress = IPAddress.Parse(ip_lokal(System.Net.Dns.GetHostName))
            Dim port As Integer = 8000
            Dim info As New IPEndPoint(myip, port)
            serversocket.Bind(info)
            serversocket.Listen(50)
            Dim readthread As New Thread(New ThreadStart(AddressOf myread))
            readthread.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function ip_lokal(ByVal par_host As String) As String
        Dim _hasil As String = ""
        Try
            Dim ASCII As New System.Text.ASCIIEncoding()

            ' Get server related information.
            Dim heserver As IPHostEntry = Dns.Resolve(par_host)

            ' Loop on the AddressList
            Dim curAdd As IPAddress
            For Each curAdd In heserver.AddressList

                ' Display the type of address family supported by the server. If the
                ' server is IPv6-enabled this value is: InternNetworkV6. If the server
                ' is also IPv4-enabled there will be an additional value of InterNetwork.
                'Console.WriteLine(("AddressFamily: " + curAdd.AddressFamily.ToString()))

                ' Display the ScopeId property in case of IPV6 addresses.
                If curAdd.AddressFamily.ToString() = ProtocolFamily.InterNetworkV6.ToString() Then
                    'Console.WriteLine(("Scope Id: " + curAdd.ScopeId.ToString()))
                End If

                ' Display the server IP address in the standard format. In 
                ' IPv4 the format will be dotted-quad notation, in IPv6 it will be
                ' in in colon-hexadecimal notation.
                ' Console.WriteLine(("Address: " + curAdd.ToString()))
                If curAdd.ToString.Contains("192.168.1") Then
                    _hasil = curAdd.ToString
                End If

                ' Display the server IP address in byte format.
                'Console.Write("AddressBytes: ")

                Dim bytes As [Byte]() = curAdd.GetAddressBytes()
                Dim i As Integer
                For i = 0 To bytes.Length - 1
                    Console.Write(bytes(i))
                Next i
                'Console.WriteLine(ControlChars.Cr + ControlChars.Lf)
            Next curAdd

            Return _hasil

        Catch e As Exception
            'Console.WriteLine(("[DoResolve] Exception: " + e.ToString()))
            Return ""
        End Try

    End Function
    Private Sub myread()
        Try
            Do While True
                clientsocket = serversocket.Accept()
                Dim bytes(1024) As Byte
                Dim i As Integer = clientsocket.Receive(bytes, 0, clientsocket.Available, SocketFlags.None)
                Dim msg As String = System.Text.Encoding.ASCII.GetString(bytes, 0, i)
                'RaiseEvent updatetext(msg)
                'Me.SetText(msg)
                If msg.ToLower = "kill" Then
                    'MsgBox(msg)
                    Timer1.Enabled = True
                    MsgBox("Aplikasi SYSPRO akan ditutup dalam waktu 1 menit, karena ada update." _
                           & vbNewLine & "Silahkan buka kembali setelah aplikasi ditutup.", MsgBoxStyle.Critical, "Informasi..")
                End If
            Loop
        Catch ex As Exception

        End Try
        

    End Sub
    Private Sub kill_app()
        Try
            Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("SygmaChat")
            For Each p As Process In pProcess
                p.Kill()
            Next
            master_new.ClsVar.CExit = False
            Global.System.Windows.Forms.Application.Exit()
            End
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SetText(ByVal txt As String)
        If Me.ListBox1.InvokeRequired Then
            Dim d As New setTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {txt})
        Else
            Me.ListBox1.Items.Add(txt)
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            i += 1
            If i > 60 Then
                kill_app()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class