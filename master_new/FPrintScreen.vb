Imports DevExpress.XtraPrinting

Public Class FPrintScreen

    Private Sub NotifyIcon(ByVal i As Integer)
        Dim s As String = "previewgreen.ico"
        Select Case i
            Case 1
                s = "previewred.ico"
            Case 2
                s = "pagesetup.ico"
        End Select
        'notifyIcon1.Icon = New Icon("\\192.50.1.3\syspro programe\" + s)
    End Sub

    Private Sub FPrintScreen_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawImageUnscaled(Icon.ToBitmap(), 10, 10)
    End Sub

    Protected Property ClipboardImage() As Image
        Get
            Dim iData As IDataObject = Clipboard.GetDataObject()
            If Not iData Is Nothing Then
                If iData.GetDataPresent(DataFormats.Bitmap) Then
                    Return CType(iData.GetData(DataFormats.Bitmap), Image)
                End If
            End If
            Return Nothing
        End Get
        Set(ByVal Value As Image)
            Clipboard.SetDataObject(Value)
        End Set
    End Property

    Private closeCount As Integer = 0

    Private Sub CreateScreenShot(ByVal autoWidth As Boolean)
        Dim currentCursor As Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor
        timer1.Start()

        Dim gr As BrickGraphics = printSystem1.Graph
        SendKeys.SendWait("^{PRTSC}")
        Dim img As Image = ClipboardImage
        printSystem1.Begin()
        gr.Modifier = BrickModifier.Detail
        gr.DrawEmptyBrick(New RectangleF(0, 0, 100, 100))
        Dim r As RectangleF = New RectangleF(New PointF(0, 0), gr.ClientPageSize)

        If Not (img Is Nothing) Then
            If Not autoWidth Then
                r = New RectangleF(0, 0, img.Width, img.Height)
            End If
            Dim brick As Brick = gr.DrawImage(img, r, BorderSide.None, Color.Transparent)
            brick.Separable = True
        Else
            MessageBox.Show("Image is null...")
        End If

        gr.Font = New Font("Arial", 8, FontStyle.Underline)
        gr.BackColor = Color.Transparent
        gr.Modifier = BrickModifier.MarginalFooter

        r = New RectangleF(0, 0, 0, gr.Font.Height)

        Dim pibrick As PageInfoBrick = gr.DrawPageInfo(PageInfo.Number, "", Color.Blue, r, DevExpress.XtraPrinting.BorderSide.None)
        pibrick.Url = "www.hariff.com"
        pibrick.Hint = pibrick.Url
        pibrick.Alignment = BrickAlignment.Far
        pibrick.AutoWidth = True

        printSystem1.End()
        If closeCount = 0 Then
            AddHandler PrintSystem1.PreviewFormEx.Closed, New EventHandler(AddressOf ClosePreview)
            closeCount += 1
        End If
        PrintSystem1.PreviewFormEx.Show()
        PrintSystem1.PreviewFormEx.Activate()

        Cursor.Current = currentCursor
    End Sub

    Private Sub ClosePreview(ByVal sender As Object, ByVal e As EventArgs)
        'RemoveHandler printSystem1.PreviewForm.Closed, AddressOf ClosePreview
        closeCount -= 1
        timer1.Stop()
        NotifyIcon(0)
    End Sub

    Private Sub menuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuItem1.Click
        CreateScreenShot(False)
    End Sub

    Private Sub menuItem3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuItem3.Click
        NotifyIcon(2)
        printSystem1.PageSetup()
        NotifyIcon(0)
    End Sub

    Private Sub notifyIcon1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles notifyIcon1.MouseDown
        If e.Button = MouseButtons.Left Then
            CreateScreenShot(False)
        End If
    End Sub

    Private i As Integer = 0

    Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer1.Tick
        i = IIf(i = 0, 1, 0)
        NotifyIcon(i)
    End Sub
End Class