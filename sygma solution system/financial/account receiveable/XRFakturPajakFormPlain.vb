
Public Class XRFakturPajakFormPlain
    Dim _arinvd_sitenum As Integer
    Dim flag As String
    Dim _print_footer(100) As String
    Dim _page, _page_list As String
    Dim _num_array As Integer = 0
    Dim FFakturPajakPrint as New FFakturPajakPrint
    Dim FFakturPajak As New FFakturPajak
    Public Shared StatusForm As String
    Dim _conf_value As String
    Private func_coll As New function_collection

    Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        _arinvd_sitenum = _arinvd_sitenum + 1
        xrt_no.Text = _arinvd_sitenum

        If GetCurrentColumnValue("ar_cu_id") = master_new.ClsVar.ibase_cur_id Then
            xrtc_price_ext_idr.ForeColor = Color.Black
            xrtc_price_ext_usd.ForeColor = Color.White
            xrtc_cu_code_ext.ForeColor = Color.White
            xrtc_arinv_exc_rate.Visible = False
        Else
            xrtc_price_ext_idr.ForeColor = Color.White
            xrtc_price_ext_usd.ForeColor = Color.Black
            xrtc_cu_code_ext.ForeColor = Color.Black
            xrtc_arinv_exc_rate.Visible = True
        End If


    End Sub

    Private Sub XRFakturPajakFormPlain_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.AfterPrint

        'If XrPageInfo2.PageInfo.NumberOfTotal.GetValues = Me.Pages.Count Then

        'End If
        'Dim _s As String
        '_s = XrPageInfo1.PageInfo.NumberOfTotal.GetValues("string")
        '_s = XrPageInfo1.PageInfo.NumberOfTotal.ToString
        'MsgBox(XrPageInfo2.PageInfo.NumberOfTotal.GetValues)
        'MsgBox(XrPageInfo2.PageInfo.GetValues)
        'MessageBox.Show(Me.Pages.Count, "test")
        'Dim _i As Integer
        'For Each page In Me.PrintingSystem.Document.Pages
        '    MsgBox(_i.ToString)
        '    _i += 1
        '    If _i <> 2 Then

        '        xrtc_sum_aft_ext_idr.ForeColor = Color.White
        '    Else
        '        xrtc_sum_aft_ext_idr.ForeColor = Color.Black

        '    End If
        'Next
    End Sub

    Private Sub XRFakturPajakFormPlain_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        _conf_value = func_coll.get_conf_file("wf_faktur_pajak")
        'MessageBox.Show(Me.Pages.Count, "test")
        If _conf_value = "1" Then
            If GetCurrentColumnValue("fp_trans_id") = "D" Then
                Me.Watermark.Text = "NOT APPROVED"
            ElseIf GetCurrentColumnValue("fp_trans_id") = "W" Then
                Me.Watermark.Text = "NOT APPROVED"
            ElseIf GetCurrentColumnValue("fp_trans_id") = "X" Then
                Me.Watermark.Text = "CANCELED"
            ElseIf GetCurrentColumnValue("fp_trans_id") = "C" Then
                Me.Watermark.Text = "CLOSED"
            ElseIf GetCurrentColumnValue("fp_trans_id") = "I" Then
                Me.Watermark.Text = ""
            End If
        End If


        'If GetCurrentColumnValue("print_detail") = False Then
        Detail.Visible = False
        'Else
        'Detail.Visible = True
        'End If

        _arinvd_sitenum = 0
        flag = GetCurrentColumnValue("fp_oid").ToString


        'Setting koma
        '----------------
        'If GetCurrentColumnValue("print_comma") = False Then
        '    Me.xrtc_sum_price_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_subtotal", "{0:n0}")})
        '    Me.xrtc_sum_disc_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_disc_value", "{0:n0}")})
        '    Me.xrtc_sum_dpp_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_net_aft_disc_idr", "{0:n0}")})
        '    Me.xrtc_sum_aft_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_ppn_idr", "{0:n0}")})
        '    Me.xrtc_price_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinvd_price_ext_idr", "{0:n0}")})
        'Else
        '    Me.xrtc_sum_price_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_subtotal", "{0:n}")})
        '    Me.xrtc_sum_disc_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_disc_value", "{0:n}")})
        '    Me.xrtc_sum_dpp_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_net_aft_disc_idr", "{0:n}")})
        '    Me.xrtc_sum_aft_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinv_ppn_idr", "{0:n}")})
        '    Me.xrtc_price_ext_idr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.arinvd_price_ext_idr", "{0:n}")})
        'End If
        '------------------------------------------------------------------------

        'extract isi textbox untuk footer yg diprint ke _print_footer------------
        Dim _temp As String = ""
        Dim i As Integer
        _num_array = 0
        _page_list = LTrim(GetCurrentColumnValue("_footer_print"))
        For i = 1 To Len(_page_list)
            If Mid(_page_list, i, 1) = "," Then
                _print_footer(_num_array) = CInt(_temp) - 1
                _temp = ""
                _num_array = _num_array + 1
            Else
                _temp = _temp & Mid(_page_list, i, 1)
            End If
        Next
        If _page_list <> "" Then
            _print_footer(_num_array) = CInt(_temp) - 1
        End If
        '--------------------------------------------------------------

    End Sub

    Private Sub GroupFooter2_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupFooter2.AfterPrint
        'MessageBox.Show(Me.Pages.Count, "test")
        'Dim _page As Integer
        '_page = Me.Pages.Count

        'If _page = 0 Then
        '    xrtc_sum_aft_ext_idr.ForeColor = Color.White
        'Else
        '    xrtc_sum_aft_ext_idr.ForeColor = Color.Black
        'End If
    End Sub

    Private Sub GroupFooter2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter2.BeforePrint
        Dim _show_value As Boolean

        If GetCurrentColumnValue("ar_cu_id") = master_new.ClsVar.ibase_cur_id Then

            'Show or not, value on footer___________________________
            If _page_list <> "" Then
                _show_value = False
                For i As Integer = 0 To _num_array
                    If _print_footer(i) = Me.PrintingSystem.Document.PageCount.ToString Then
                        _show_value = True
                        Exit For
                    End If
                Next
                If _show_value = True Then
                    xrtc_sum_price_ext_idr.ForeColor = Color.Black
                    xrtc_sum_disc_ext_idr.ForeColor = Color.Black
                    xrtc_sum_dpp_ext_idr.ForeColor = Color.Black
                    xrtc_sum_aft_ext_idr.ForeColor = Color.Black
                Else
                    xrtc_sum_price_ext_idr.ForeColor = Color.White
                    xrtc_sum_disc_ext_idr.ForeColor = Color.White
                    xrtc_sum_dpp_ext_idr.ForeColor = Color.White
                    xrtc_sum_aft_ext_idr.ForeColor = Color.White
                End If

                xrtc_sum_price_ext_usd.ForeColor = Color.White
                xrtc_sum_disc_ext_usd.ForeColor = Color.White
                xrtc_sum_dpp_ext_usd.ForeColor = Color.White
                xrtc_sum_aft_ext_usd.ForeColor = Color.White

                xrtc_sum_cu_code.ForeColor = Color.White
                xrtc_dpp_cu_code.ForeColor = Color.White
                xrtc_disc_cu_code.ForeColor = Color.White
                xrtc_aft_cu_code.ForeColor = Color.White
            Else
                xrtc_sum_price_ext_idr.ForeColor = Color.Black
                xrtc_sum_disc_ext_idr.ForeColor = Color.Black
                xrtc_sum_dpp_ext_idr.ForeColor = Color.Black
                xrtc_sum_aft_ext_idr.ForeColor = Color.Black

                xrtc_sum_price_ext_usd.ForeColor = Color.White
                xrtc_sum_disc_ext_usd.ForeColor = Color.White
                xrtc_sum_dpp_ext_usd.ForeColor = Color.White
                xrtc_sum_aft_ext_usd.ForeColor = Color.White

                xrtc_sum_cu_code.ForeColor = Color.White
                xrtc_dpp_cu_code.ForeColor = Color.White
                xrtc_disc_cu_code.ForeColor = Color.White
                xrtc_aft_cu_code.ForeColor = Color.White
            End If
            '-------------------------------------------------------------
        Else

            'Show or not, value on footer___________________________
            If _page_list <> "" Then
                _show_value = False
                For i As Integer = 0 To _num_array
                    If _print_footer(i) = Me.PrintingSystem.Document.PageCount.ToString Then
                        _show_value = True
                        Exit For
                    End If
                Next
                If _show_value = True Then
                    xrtc_sum_dpp_ext_idr.ForeColor = Color.Black
                    xrtc_sum_aft_ext_idr.ForeColor = Color.Black

                    xrtc_sum_price_ext_usd.ForeColor = Color.Black
                    xrtc_sum_disc_ext_usd.ForeColor = Color.Black
                    xrtc_sum_dpp_ext_usd.ForeColor = Color.Black
                    xrtc_sum_aft_ext_usd.ForeColor = Color.Black

                    xrtc_sum_cu_code.ForeColor = Color.Black
                    xrtc_dpp_cu_code.ForeColor = Color.Black
                    xrtc_disc_cu_code.ForeColor = Color.Black
                    xrtc_aft_cu_code.ForeColor = Color.Black
                Else
                    xrtc_sum_dpp_ext_idr.ForeColor = Color.White
                    xrtc_sum_aft_ext_idr.ForeColor = Color.White

                    xrtc_sum_price_ext_usd.ForeColor = Color.White
                    xrtc_sum_disc_ext_usd.ForeColor = Color.White
                    xrtc_sum_dpp_ext_usd.ForeColor = Color.White
                    xrtc_sum_aft_ext_usd.ForeColor = Color.White

                    xrtc_sum_cu_code.ForeColor = Color.White
                    xrtc_dpp_cu_code.ForeColor = Color.White
                    xrtc_disc_cu_code.ForeColor = Color.White
                    xrtc_aft_cu_code.ForeColor = Color.White
                End If

                xrtc_sum_price_ext_idr.ForeColor = Color.White
                xrtc_sum_disc_ext_idr.ForeColor = Color.White
            Else
                xrtc_sum_price_ext_idr.ForeColor = Color.White
                xrtc_sum_disc_ext_idr.ForeColor = Color.White
                xrtc_sum_dpp_ext_idr.ForeColor = Color.Black
                xrtc_sum_aft_ext_idr.ForeColor = Color.Black

                xrtc_sum_price_ext_usd.ForeColor = Color.Black
                xrtc_sum_disc_ext_usd.ForeColor = Color.Black
                xrtc_sum_dpp_ext_usd.ForeColor = Color.Black
                xrtc_sum_aft_ext_usd.ForeColor = Color.Black

                xrtc_sum_cu_code.ForeColor = Color.Black
                xrtc_dpp_cu_code.ForeColor = Color.Black
                xrtc_disc_cu_code.ForeColor = Color.Black
                xrtc_aft_cu_code.ForeColor = Color.Black
            End If
            '-------------------------------------------------------------
        End If

        If IsDBNull(GetCurrentColumnValue("fp_unstrikeout")) Then
            xrl_hj.Visible = False
            xrl_pg.Visible = False
            xrl_um.Visible = False
            xrl_trm.Visible = False
        Else
            If GetCurrentColumnValue("fp_unstrikeout") = "Harga Jual" Then
                xrl_hj.Visible = False
                xrl_pg.Visible = True
                xrl_um.Visible = True
                xrl_trm.Visible = True
            ElseIf GetCurrentColumnValue("fp_unstrikeout") = "Penggantian" Then
                xrl_hj.Visible = True
                xrl_pg.Visible = False
                xrl_um.Visible = True
                xrl_trm.Visible = True
            ElseIf GetCurrentColumnValue("fp_unstrikeout") = "Uang Muka" Then
                xrl_hj.Visible = True
                xrl_pg.Visible = True
                xrl_um.Visible = False
                xrl_trm.Visible = True
            ElseIf GetCurrentColumnValue("fp_unstrikeout") = "Termin" Then
                xrl_hj.Visible = True
                xrl_pg.Visible = True
                xrl_um.Visible = True
                xrl_trm.Visible = False
            Else
                xrl_hj.Visible = False
                xrl_pg.Visible = False
                xrl_um.Visible = False
                xrl_trm.Visible = False
            End If
        End If

    End Sub

    Private Sub GroupHeader2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint
        If flag <> GetCurrentColumnValue("fp_oid").ToString Then
            _arinvd_sitenum = 0
        End If
        flag = GetCurrentColumnValue("fp_oid").ToString
          
        'Taufik / 24 Maret 2011 
        If StatusForm.Trim = "1" Then
            Dim PNPajak As String = FFakturPajak.PageNum2.ToString
            If PNPajak = "Y" Then
                XrPageInfo1.Visible = True
                XrLabel1.Visible = True
            Else
                XrPageInfo1.Visible = False
                XrLabel1.Visible = False
            End If
        End If

        If StatusForm.Trim = "2" Then
            Dim PNPajakPrint As String = "Y" 'FFakturPajakPrint.PageNum.ToString
            If PNPajakPrint = "Y" Then
                XrPageInfo1.Visible = True
                XrLabel1.Visible = True
            Else
                XrPageInfo1.Visible = False
                XrLabel1.Visible = False
            End If
        End If

        If StatusForm.Trim = "3" Then
            Dim PNPajakAproval As String = FFakturPajakApproval.PageNumAproval.ToString
            If PNPajakAproval = "Y" Then
                XrPageInfo1.Visible = True
                XrLabel1.Visible = True
            Else
                XrPageInfo1.Visible = False
                XrLabel1.Visible = False
            End If
        End If
    End Sub
End Class