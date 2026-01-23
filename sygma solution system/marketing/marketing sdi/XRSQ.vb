Imports master_new.ModFunction
Imports DevExpress.XtraPrinting
Imports npgsql

Public Class XRSQ
    Dim ds_paper As DataSet
    Dim _paper_oid As String
    Dim _available, _landscape_def As Boolean
    Dim _paperkind_def, _topmargin_def, _bottommargin_def, _leftmargin_def, _rightmargin_def As Integer
    Public _sq_en_id As Integer

    Private Sub XRSQ_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

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
        'cek_paper_available()
    End Sub
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
                            & " and paper_en_id = " & _sq_en_id.ToString _
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
                                            & SetInteger(_sq_en_id) & "," _
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