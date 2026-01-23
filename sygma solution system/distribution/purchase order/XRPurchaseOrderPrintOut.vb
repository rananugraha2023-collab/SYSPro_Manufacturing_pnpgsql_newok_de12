
Imports DevExpress.XtraPrinting
Imports npgsql
Imports master_new.ModFunction
Imports MessagingToolkit.QRCode

Public Class XRPurchaseOrderPrintOut
    Dim QR_code As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Public ds_po As New DataSet
    Dim _po_oid As String
    Dim ds As New DataSet
    Dim dt As DataTable
    Dim dr As DataRow
    Dim po_oid As New DataColumn
    Dim po_code As New DataColumn
    Dim page As New DataColumn
    Dim page_num, pod_num As Integer
    Dim flag, flag2 As String

    Dim ds_paper As DataSet
    Dim _paper_oid As String
    Dim _available, _landscape_def As Boolean
    Dim _paperkind_def, _topmargin_def, _bottommargin_def, _leftmargin_def, _rightmargin_def As Integer

    Private Sub XRPurchaseOrderPrintOut_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.AfterPrint
        'MsgBox("test")
        'set_paper()
    End Sub

    Private Sub XRPurchaseOrderPrintOut_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        ''xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
        'page_num = 0
        'flag2 = GetCurrentColumnValue("po_oid").ToString

        'ds = New DataSet
        'dt = New DataTable
        'po_oid = New DataColumn("po_oid", Type.GetType("System.String"))
        'po_code = New DataColumn("po_code", Type.GetType("System.String"))
        'page = New DataColumn("page", Type.GetType("System.Int32"))
        'dt.Columns.Add(po_oid)
        'dt.Columns.Add(po_code)S
        'dt.Columns.Add(page)

        xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        Try
            XrPictureBox1.Image = QR_code.Encode(GetCurrentColumnValue("po_code"))
        Catch ex As Exception

        End Try

        If GetCurrentColumnValue("tranaprvd_name_1") = "" Then
            tranaprvd_line_1.Visible = False
        Else
            tranaprvd_line_1.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_2") = "" Then
            tranaprvd_line_2.Visible = False
        Else
            tranaprvd_line_2.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_3") = "" Then
            tranaprvd_line_3.Visible = False
        Else
            tranaprvd_line_3.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_4") = "" Then
            tranaprvd_line_4.Visible = False
        Else
            tranaprvd_line_4.Visible = True
        End If

        Dim _transparent As Integer = 230
        If GetCurrentColumnValue("po_trans_id") = "D" Then
            Me.Watermark.Text = "DRAFT"
            Me.Watermark.TextTransparency = _transparent
            Me.Footnote.Visible = True
        ElseIf GetCurrentColumnValue("po_trans_id") = "W" Then
            Me.Watermark.Text = "NOT APPROVED"
            Me.Watermark.TextTransparency = _transparent
            Me.Footnote.Visible = False
        ElseIf GetCurrentColumnValue("po_trans_id") = "X" Then
            Me.Watermark.Text = "CANCELED"
            Me.Watermark.TextTransparency = _transparent
            Me.Footnote.Visible = False
        ElseIf GetCurrentColumnValue("po_trans_id") = "C" Then
            Me.Watermark.Text = "CLOSED"
            Me.Watermark.TextTransparency = _transparent
            Me.Footnote.Visible = False
        ElseIf GetCurrentColumnValue("po_trans_id") = "I" Then
            Me.Watermark.Text = ""
            Me.Watermark.TextTransparency = _transparent
            Me.Footnote.Visible = False
        End If

        Me.Watermark.ForeColor = Color.Black

        'cek_paper_available()
    End Sub

    'Private Sub XRPurchaseOrderPrintOut_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress



    '    'MsgBox(e.GetType.GetField(XrLabel1.Name))
    '    If flag <> SetSetring(ds.Tables(0).Rows(e.PageIndex).Item("po_code")) Then


    '        _po_oid = SetSetring(ds.Tables(0).Rows(e.PageIndex).Item("po_oid"))

    '        Try
    '            Using objinsert As New master_new.WDABasepgsql("", "")
    '                With objinsert
    '                    .Connection.Open()
    '                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                    Try
    '                        .Command = .Connection.CreateCommand
    '                        .Command.Transaction = sqlTran


    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "UPDATE  " _
    '                                            & "  public.po_mstr   " _
    '                                            & "SET  " _
    '                                            & "  po_print = (select coalesce(po_print,0) as po_print from po_mstr where po_oid = " + _po_oid + ") + 1 " _
    '                                            & "WHERE  " _
    '                                            & "  po_oid = " & _po_oid & " "


    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        sqlTran.Commit()
    '                    Catch ex As nPgSqlException
    '                        sqlTran.Rollback()
    '                        MessageBox.Show(ex.Message)
    '                    End Try
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try

    '        flag = SetSetring(ds.Tables(0).Rows(e.PageIndex).Item("po_code"))
    '    End If

    'End Sub

    'Private Sub GroupHeader1_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupHeader1.AfterPrint


    'End Sub

    Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint

        pod_num = pod_num + 1
        XrLabel37.Text = pod_num


    End Sub

    'Private Sub GroupHeader2_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupHeader2.AfterPrint
    '    If ds.Tables.Count = 0 Then
    '        ds.Tables.Add(dt)
    '    End If
    'End Sub

    'Private Sub GroupHeader2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint
    '    If flag2 <> GetCurrentColumnValue("po_oid").ToString Then
    '        pod_num = 0
    '    End If
    '    flag2 = GetCurrentColumnValue("po_oid").ToString

    '    dr = dt.NewRow()
    '    dr("po_oid") = GetCurrentColumnValue("po_oid").ToString
    '    dr("po_code") = GetCurrentColumnValue("po_code").ToString
    '    dr("page") = page_num
    '    dt.Rows.Add(dr)

    '    page_num = page_num + 1
    'End Sub
    Private Sub cek_paper_available()
        ds_paper = New DataSet
        ds_paper.Clear()
        _available = False
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  paper_oid, " _
                            & "  paper_paperkind, " _
                            & "  paper_landscape, " _
                            & "  paper_left_margin, " _
                            & "  paper_right_margin, " _
                            & "  paper_top_margin, " _
                            & "  paper_bottom_margin, " _
                            & "  paper_report_name " _
                            & "FROM  " _
                            & "  public.paper_mstr " _
                            & " where paper_dom_id = " & master_new.ClsVar.sdom_id.ToString _
                            & " and paper_en_id = " & GetCurrentColumnValue("po_en_id").ToString _
                            & " and paper_user_id = " & master_new.ClsVar.sUserID.ToString _
                            & " and paper_report_name = '" & Me.ToString & "'"
                    .InitializeCommand()
                    .FillDataSet(ds_paper, "paper_set")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If ds_paper.Tables(0).Rows.Count > 0 Then
            _available = True
            _paper_oid = ds_paper.Tables(0).Rows(0).Item("paper_oid").ToString
            PaperKind = ds_paper.Tables(0).Rows(0).Item("paper_paperkind")
            Landscape = SetBitYNB(ds_paper.Tables(0).Rows(0).Item("paper_landscape"))
            Margins.Top = ds_paper.Tables(0).Rows(0).Item("paper_top_margin")
            Margins.Bottom = ds_paper.Tables(0).Rows(0).Item("paper_bottom_margin")
            Margins.Left = ds_paper.Tables(0).Rows(0).Item("paper_left_margin")
            Margins.Right = ds_paper.Tables(0).Rows(0).Item("paper_right_margin")

            _paperkind_def = ds_paper.Tables(0).Rows(0).Item("paper_paperkind")
            _topmargin_def = (ds_paper.Tables(0).Rows(0).Item("paper_top_margin"))
            _bottommargin_def = ds_paper.Tables(0).Rows(0).Item("paper_bottom_margin")
            _leftmargin_def = ds_paper.Tables(0).Rows(0).Item("paper_left_margin")
            _rightmargin_def = ds_paper.Tables(0).Rows(0).Item("paper_right_margin")
            _landscape_def = SetBitYNB(ds_paper.Tables(0).Rows(0).Item("paper_landscape"))

        End If
    End Sub

    Private Sub set_paper()
        Dim _set_default As MsgBoxResult

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        If _available = False Then
                            _paper_oid = Guid.NewGuid.ToString
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.paper_mstr " _
                                            & "( " _
                                            & "  paper_oid, " _
                                            & "  paper_dom_id, " _
                                            & "  paper_en_id, " _
                                            & "  paper_user_id, " _
                                            & "  paper_paperkind, " _
                                            & "  paper_landscape, " _
                                            & "  paper_left_margin, " _
                                            & "  paper_right_margin, " _
                                            & "  paper_top_margin, " _
                                            & "  paper_bottom_margin, " _
                                            & "  paper_dt, " _
                                            & "  paper_report_name " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_paper_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id.ToString) & ",  " _
                                            & SetInteger(GetCurrentColumnValue("po_en_id").ToString) & "," _
                                            & SetInteger(master_new.ClsVar.sUserID.ToString) & ",  " _
                                            & SetInteger(PrintingSystem.PageSettings.PaperKind) & ",  " _
                                            & SetBitYN(PrintingSystem.PageSettings.Landscape.ToString) & ",  " _
                                            & SetInteger(PrintingSystem.PageSettings.LeftMargin.ToString) & ",  " _
                                            & SetInteger(PrintingSystem.PageSettings.RightMargin.ToString) & ",  " _
                                            & SetInteger(PrintingSystem.PageSettings.TopMargin.ToString) & ",  " _
                                            & SetInteger(PrintingSystem.PageSettings.BottomMargin.ToString) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetSetring(Me.ToString) & "  " _
                                            & ");"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            sqlTran.Commit()

                            _paperkind_def = PrintingSystem.PageSettings.PaperKind
                            _topmargin_def = PrintingSystem.PageSettings.TopMargin.ToString
                            _bottommargin_def = PrintingSystem.PageSettings.BottomMargin.ToString
                            _leftmargin_def = PrintingSystem.PageSettings.LeftMargin.ToString
                            _rightmargin_def = PrintingSystem.PageSettings.RightMargin.ToString
                            _landscape_def = PrintingSystem.PageSettings.Landscape.ToString

                            _available = True
                        Else
                            If (_paperkind_def <> PrintingSystem.PageSettings.PaperKind) _
                               Or (_landscape_def <> PrintingSystem.PageSettings.Landscape.ToString) _
                               Or (_topmargin_def <> PrintingSystem.PageSettings.TopMargin.ToString) _
                               Or (_bottommargin_def <> PrintingSystem.PageSettings.BottomMargin.ToString) _
                               Or (_leftmargin_def <> PrintingSystem.PageSettings.LeftMargin.ToString) _
                               Or (_rightmargin_def <> PrintingSystem.PageSettings.RightMargin.ToString) Then


                                _set_default = MsgBox("Save Paper Changes as Default?", MsgBoxStyle.YesNo, "Save Confirmation")

                                If _set_default = MsgBoxResult.Yes Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE  " _
                                        & "  public.paper_mstr   " _
                                        & "SET  " _
                                        & "  paper_paperkind = " & SetInteger(PrintingSystem.PageSettings.PaperKind) & ",  " _
                                        & "  paper_landscape = " & SetBitYN(PrintingSystem.PageSettings.Landscape.ToString) & ",  " _
                                        & "  paper_left_margin = " & SetInteger(PrintingSystem.PageSettings.LeftMargin.ToString) & ",  " _
                                        & "  paper_right_margin = " & SetInteger(PrintingSystem.PageSettings.RightMargin.ToString) & ",  " _
                                        & "  paper_top_margin = " & SetInteger(PrintingSystem.PageSettings.TopMargin.ToString) & ",  " _
                                        & "  paper_bottom_margin = " & SetInteger(PrintingSystem.PageSettings.BottomMargin.ToString) & ",  " _
                                        & "  paper_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  paper_oid = " & SetSetring(_paper_oid.ToString) & " "

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                    sqlTran.Commit()

                                    _paperkind_def = PrintingSystem.PageSettings.PaperKind
                                    _topmargin_def = PrintingSystem.PageSettings.TopMargin.ToString
                                    _bottommargin_def = PrintingSystem.PageSettings.BottomMargin.ToString
                                    _leftmargin_def = PrintingSystem.PageSettings.LeftMargin.ToString
                                    _rightmargin_def = PrintingSystem.PageSettings.RightMargin.ToString
                                    _landscape_def = PrintingSystem.PageSettings.Landscape.ToString

                                    _available = True
                                End If
                            End If
                        End If
                        PrintingSystem.Document.AutoFitToPagesWidth = 1
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class