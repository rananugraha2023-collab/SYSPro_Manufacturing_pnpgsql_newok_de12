Imports System.Net
Imports System.IO

Imports master_new.ModFunction

Public Class frmUpdate
    Public func_coll As New function_collection
    Private WithEvents WClient As New WebClient
    Private DlStage As Integer
    Private FileName As String = "Current Filename"
    Private LastCount As Integer = 0
    Dim _url As String = func_coll.get_conf_file("http_update")
    Dim path_download As String = GetCurrentPath() & "download\"
    Dim _file As String
    Dim _path_to As String


    Dim outputZip As String = "output zip file path"
    Dim inputZip As String = _path_to
    Dim inputFolder As String = "input folder path"
    Dim outputFolder As String = GetCurrentPath() & "extract"

    Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))



    Sub Zip()

        'Lets create an empty Zip File .
        'The following data represents an empty zip file.
        Dim startBytes() As Byte = {80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, _
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        'Data for an empty zip file.

        FileIO.FileSystem.WriteAllBytes(outputZip, startBytes, False)
        'We have successfully created the empty zip file.

        'Declare the folder which contains the items (files/folders) that you want to zip.
        Dim input As Object = shObj.NameSpace((inputFolder))

        'Declare the created empty zip file.
        Dim output As Object = shObj.NameSpace((outputZip))

        'Compress the items into the zip file.
        output.CopyHere((input.Items), 4)

    End Sub

    Sub UnZip()

        'Create directory in which you will unzip your items.
        If IO.Directory.Exists(outputFolder) = False Then
            IO.Directory.CreateDirectory(outputFolder)
        End If

        TextBox1.Text = "Extract file.."
        'Declare the folder where the items will be extracted.
        Dim output As Object = shObj.NameSpace((outputFolder))
        inputZip = _path_to
        'Declare the input zip file.
        Dim input As Object = shObj.NameSpace((inputZip))

        'Extract the items from the zip file.
        output.CopyHere((input.Items), 4)

        TextBox1.Text = "Extract file complete"
    End Sub




    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'WClient.DownloadFileAsync(New Uri("http://devandro.xyz/down/syspro.zip"), "D:\amd64\icon\syspro.zip")
    End Sub

    Private Sub wClient_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WClient.DownloadProgressChanged
        Try
            'ProgressBar1.Value = e.ProgressPercentage
            ProgressBarControl1.EditValue = e.ProgressPercentage
            If e.ProgressPercentage > LastCount Then

                TextBox1.Text = "Download update " & "... " & LastCount.ToString & " % Completed"
                LastCount += 5
            End If
        Catch ex As Exception

        End Try
        

    End Sub

    Private Sub wClient_DownloadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles WClient.DownloadFileCompleted
        Try
            TextBox1.Text = FileName & "... " & "100 % Complete"

            LastCount = 0
            UnZip()
            'Process.Start(outputFolder & "\update.bat")
            TextBox1.Text = "Double click update.bat to update application.."

            ' If MessageBox.Show("Apakah data ini akan dieksport?", "Konfirmasi...", _
            'MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            '     Exit Sub
            ' End If
            Box("Double click update.bat to update application..")
            Process.Start("explorer.exe", outputFolder)
        Catch ex As Exception

        End Try
       

    End Sub

    Private Sub frmUpdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IO.Directory.Exists(path_download) = False Then
                IO.Directory.CreateDirectory(path_download)
            End If
            If IO.Directory.Exists(outputFolder) = True Then
                IO.Directory.Delete(outputFolder, True)
            End If

            Dim _uri As New Uri(_url)
            _file = _uri.Segments(_uri.Segments.Length - 1)

           

            _path_to = path_download & _file

            WClient.DownloadFileAsync(New Uri(_url), _path_to)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtRetry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetry.Click
        Try
            LastCount = 0
            WClient.CancelAsync()
            Threading.Thread.Sleep(2000)
            frmUpdate_Load(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class