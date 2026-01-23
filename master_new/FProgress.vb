Public Class FProgress

    Public Sub New(ByVal parent As Form)
        InitializeComponent()
        If Not parent Is Nothing Then
            Left = parent.Left + (parent.Width - Width) / 2
            Top = parent.Top + (parent.Height - Height) / 2
        End If
        Me.Height = progressBarControl1.Height + progressBarControl1.Top * 2 + 4
    End Sub

    Public Sub SetProgressValue(ByVal position As Integer)
        ProgressBarControl1.Position = position
        ProgressBarControl1.Text = position & " %"
        Me.Update()
    End Sub
End Class