'Imports MyPGDll.ModFunction
Imports System.IO
Imports master_new.ModFunction
Public Class Class7z

    Public Shared Sub kompres(ByVal file_tujuan As String, ByVal folder_asal As String, ByVal opsi As String)
        Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
        With myprocess
            .StartInfo.FileName = GetCurrentPath() & "7-Zip\7zG.exe"
            .StartInfo.RedirectStandardOutput = False
            .StartInfo.UseShellExecute = False
            .StartInfo.Arguments = "a " & ControlChars.Quote & file_tujuan & ControlChars.Quote _
                & " " & ControlChars.Quote & folder_asal & " " & opsi & ControlChars.Quote   'a c:\hasil\test.7z C:\temp\*.* -r

            .StartInfo.CreateNoWindow = True
            .StartInfo.UseShellExecute = False
            .StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            .Start()
            .WaitForExit()
            .Close()
        End With
    End Sub
    Public Shared Function kompres(ByVal file_tujuan As String, ByVal file_asal As String, ByVal par_error As ArrayList) As Boolean
        Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
        Dim outputReader As StreamReader = Nothing
        Dim errorReader As StreamReader = Nothing

        Try
            With myprocess
                .StartInfo.FileName = GetCurrentPath() & "7-Zip\7z.exe"
                .StartInfo.RedirectStandardOutput = True
                .StartInfo.RedirectStandardError = True
                .StartInfo.RedirectStandardInput = True
                .StartInfo.ErrorDialog = False
                .StartInfo.UseShellExecute = False
                .StartInfo.Arguments = "a " & ControlChars.Quote & file_tujuan & ControlChars.Quote _
                    & " " & ControlChars.Quote & file_asal & ControlChars.Quote   'a c:\hasil\test.7z C:\temp\*.* -r
                .StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                .Start()

                outputReader = myprocess.StandardOutput
                errorReader = myprocess.StandardError

                .WaitForExit()

                Dim _message As String = outputReader.ReadToEnd()
                Dim _error As String = errorReader.ReadToEnd()

                If _message.ToLower.Contains("everything is ok") Then
                    Return True
                Else
                    par_error.Add(_message)
                    Return False
                End If
                .Close()
            End With
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        Finally
            If outputReader IsNot Nothing Then
                outputReader.Close()
            End If
            If errorReader IsNot Nothing Then
                errorReader.Close()
            End If
        End Try
    End Function

    Public Shared Function kompres(ByVal file_tujuan As String, ByVal file_asal As String, ByVal par_error As ArrayList, ByVal par_opsi As String) As Boolean
        Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
        Dim outputReader As StreamReader = Nothing
        Dim errorReader As StreamReader = Nothing

        Try
            With myprocess
                .StartInfo.FileName = GetCurrentPath() & "7-Zip\7z.exe"
                .StartInfo.RedirectStandardOutput = True
                .StartInfo.RedirectStandardError = True
                .StartInfo.RedirectStandardInput = True
                .StartInfo.ErrorDialog = False
                .StartInfo.UseShellExecute = False
                .StartInfo.Arguments = "a " & ControlChars.Quote & file_tujuan & ControlChars.Quote _
                    & " " & ControlChars.Quote & file_asal & " " & par_opsi & ControlChars.Quote   'a c:\hasil\test.7z C:\temp\*.* -r
                .StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                .Start()

                outputReader = myprocess.StandardOutput
                errorReader = myprocess.StandardError

                .WaitForExit()

                Dim _message As String = outputReader.ReadToEnd()
                Dim _error As String = errorReader.ReadToEnd()

                If _message.ToLower.Contains("everything is ok") Then
                    Return True
                Else
                    par_error.Add(_message)
                    Return False
                End If
                .Close()
            End With
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        Finally
            If outputReader IsNot Nothing Then
                outputReader.Close()
            End If
            If errorReader IsNot Nothing Then
                errorReader.Close()
            End If
        End Try
    End Function

    Public Shared Function unkompres(ByVal file As String, ByVal folder_ekstrak As String, ByVal par_error As ArrayList) As Boolean
        Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
        Dim outputReader As StreamReader = Nothing
        Dim errorReader As StreamReader = Nothing
        Try

            With myprocess

                .StartInfo.FileName = GetCurrentPath() & "7-Zip\7z.exe"
                .StartInfo.RedirectStandardOutput = True
                .StartInfo.RedirectStandardError = True
                .StartInfo.RedirectStandardInput = True
                .StartInfo.ErrorDialog = False
                .StartInfo.UseShellExecute = False
                .StartInfo.Arguments = "e " & ControlChars.Quote & file & ControlChars.Quote _
                & " -o" & ControlChars.Quote & folder_ekstrak & ControlChars.Quote & " -y"
                .StartInfo.WindowStyle = ProcessWindowStyle.Minimized

                .Start()

                outputReader = myprocess.StandardOutput
                errorReader = myprocess.StandardError

                .WaitForExit()

                Dim _message As String = outputReader.ReadToEnd()
                Dim _error As String = errorReader.ReadToEnd()

                If _message.ToLower.Contains("everything is ok") Then
                    Return True
                Else
                    par_error.Add(_message)
                    Return False
                End If
                .Close()

            End With
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        Finally
            If outputReader IsNot Nothing Then
                outputReader.Close()
            End If
            If errorReader IsNot Nothing Then
                errorReader.Close()
            End If
        End Try

    End Function
    Public Shared Sub unkompres(ByVal file As String, ByVal folder_ekstrak As String)
        Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
        With myprocess
            .StartInfo.FileName = GetCurrentPath() & "7-Zip\7zG.exe"
            .StartInfo.RedirectStandardOutput = False
            .StartInfo.UseShellExecute = False
            .StartInfo.Arguments = "e " & ControlChars.Quote & file & ControlChars.Quote _
            & " -o" & ControlChars.Quote & folder_ekstrak & ControlChars.Quote & " -y"

            'If showprogress.Checked = False Then
            .StartInfo.CreateNoWindow = True
            .StartInfo.UseShellExecute = False
            .StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'End If

            .Start()
            .WaitForExit()
            .Close()
        End With
    End Sub
End Class
