Imports MyPGDll.ModFunction
Imports MyPGDll.ClassReportDev
Imports MyPGDll.PGSqlConn

Public Class rptNeraca
    public vtglawal,vtglakhir,vlevel,vdom,ven,vsb,vcc
    Dim Harta, Kewajiban, Modal As System.Decimal
    Dim Saldo, hasil,  count2 As System.Decimal

    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint

        If GetCurrentColumnValue("jumlah") Is System.DBNull.Value Then
            hasil = 0
        Else
            hasil = CType(GetCurrentColumnValue("jumlah"), System.Decimal)
        End If
        Saldo = Saldo + hasil

    End Sub

    Private Sub GroupFooter1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        count2 = count2 + 1
        If count2 = 1 Then
            Harta = Saldo
        ElseIf count2 = 2 Then
            Kewajiban = Saldo
        ElseIf count2 = 3 Then
            Modal = Saldo
        End If
        Saldo = 0

    End Sub

    Private Sub rptNeraca_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        count2 = 0
        Saldo = 0
        LPeriode.Text = Parameters("PPeriode").Value
        LPosisi.Text = Parameters("PPosisi").Value
    End Sub

    Private Sub Hasil_Modal_Kewajiban_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Hasil_Modal_Kewajiban.BeforePrint
        Hasil_Modal_Kewajiban.Text = MaskDec(Kewajiban + Modal)
    End Sub

    Private Sub ac_code_PreviewClick(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles ac_code.PreviewClick
        'Dim sSql As String
        'Dim level, dom, en, sb, cc As Integer
        'Dim posisi As String = ""
        'Dim bulan As String
        'Try
        '    dom = 0
        '    en = 0
        '    sb = 0
        '    cc = 0

        '    With frmNeracaPreview
        '        bulan = GetIDByName2Prmtr("code_mstr", "code_code", "code_field", "month_code", "code_name", "'" & .Bulan.Text & "'")

        '        If .dom_id.Text.Length > 0 Then
        '            level = 1
        '            dom = CInt(.dom_id.Text)
        '            If .en_id.Text.Length > 0 Then
        '                level = 2
        '                en = CInt(.en_id.Text)
        '                If .sb_id.Text.Length > 0 Then
        '                    level = 3
        '                    sb = CInt(.sb_id.Text)
        '                    If .cc_id.Text.Length > 0 Then
        '                        level = 4
        '                        cc = CInt(.cc_id.Text)
        '                    End If
        '                End If
        '            End If
        '        Else
        '            level = 1
        '            dom = 1
        '        End If

        '        sSql = "SELECT  " _
        '            & "  b.ac_code, " _
        '            & "  b.ac_name, " _
        '            & "  b.ac_type, " _
        '            & "  b.ac_subclass, " _
        '            & "  f_calc_neraca_saldo(b.ac_id,'SWD',to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'), " _
        '            & "  f_lastdateofmonth(to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'))," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as saldo_awal_debet, " _
        '            & "  f_calc_neraca_saldo(b.ac_id,'SWK',to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'), " _
        '            & "  f_lastdateofmonth(to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'))," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as saldo_awal_kredit, " _
        '            & "  f_calc_neraca_saldo(b.ac_id,'PD',to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'), " _
        '            & "  f_lastdateofmonth(to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'))," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as perubahan_debet, " _
        '            & "  f_calc_neraca_saldo(b.ac_id,'PK',to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'), " _
        '            & "  f_lastdateofmonth(to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'))," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as perubahan_kredit, " _
        '            & "  f_calc_neraca_saldo(b.ac_id,'SKD',to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'), " _
        '            & "  f_lastdateofmonth(to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'))," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as saldo_akhir_debet, " _
        '            & "  f_calc_neraca_saldo(b.ac_id,'SKK',to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'), " _
        '            & "  f_lastdateofmonth(to_date('1/" & bulan & "/" & .Tahun.Text & "','dd/MM/yyyy'))," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as saldo_akhir_kredit " _
        '            & "FROM " _
        '            & "  public.ac_mstr b " _
        '            & " WHERE b.ac_code='" & e.Brick.Text & "' " _
        '            & "ORDER BY " _
        '            & "  b.ac_code"

        '        If .dom_id.Text.Length > 0 Then
        '            posisi += "Domain : " & .dom_desc.Text & ", "
        '            If .en_id.Text.Length > 0 Then
        '                posisi += "Entity : " & .en_desc.Text & ", "
        '                If .sb_id.Text.Length > 0 Then
        '                    posisi += "Sub Account : " & .sb_desc.Text & ", "
        '                    If .cc_id.Text.Length > 0 Then
        '                        posisi += "Cost Center : " & .cc_desc.Text
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End With

        '    If Microsoft.VisualBasic.Right(posisi, 2) = ", " Then
        '        posisi = posisi.Substring(0, Len(posisi) - 2)
        '    End If

        '    Dim rpt As New rptNeracaSaldo
        '    With rpt
        '        Dim ds As New DataSet
        '        ds = ReportDataset(sSql)
        '        If ds.Tables(0).Rows.Count < 1 Then
        '            Box("Maaf data kosong")
        '            Exit Sub
        '        End If

        '        .vtglawal = vtglawal
        '        .vtglakhir = vtglakhir

        '        .vlevel = level
        '        .vdom = dom
        '        .ven = en
        '        .vsb = sb
        '        .vcc = cc

        '        .DataSource = ds
        '        .DataMember = "Table"
        '        .Parameters("PPeriode").Value = "PERIODE : " & frmNeracaPreview.Bulan.Text & " " & frmNeracaPreview.Tahun.Text
        '        .Parameters("PPosisi").Value = posisi
        '        printTool.ShowPreview()
        '    End With
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try

        'Dim level, dom, en, sb, cc As Integer
        'Dim sSql As String
        'Dim posisi As String = ""
        'dim bulan as string
        'Try
        '    dom = 0
        '    en = 0
        '    sb = 0
        '    cc = 0

        '    With frmNeracaPreview
        '        bulan=GetIDByName2Prmtr("code_mstr", "code_code", "code_field", "month_code", "code_name", "'" & .Bulan.Text & "'")

        '        If .dom_id.Text.Length > 0 Then
        '            level = 1
        '            dom = CInt(.dom_id.Text)
        '            If .en_id.Text.Length > 0 Then
        '                level = 2
        '                en = CInt(.en_id.Text)
        '                If .sb_id.Text.Length > 0 Then
        '                    level = 3
        '                    sb = CInt(.sb_id.Text)
        '                    If .cc_id.Text.Length > 0 Then
        '                        level = 4
        '                        cc = CInt(.cc_id.Text)
        '                    End If
        '                End If
        '            End If
        '        Else
        '            level = 1
        '            dom = 1
        '        End If

        '        sSql = "SELECT  " _
        '            & "  a.glt_type, " _
        '            & "  a.glt_date, " _
        '            & "  a.glt_desc, " _
        '            & "  a.glt_code,a.glt_ac_id, " _
        '            & "  b.ac_code,b.ac_type, " _
        '            & "  b.ac_name, " _
        '            & "  a.glt_debit, " _
        '            & "  a.glt_credit,f_get_begining_balance_acc(a.glt_ac_id, to_date(" & SetDateNTime00(.TglAwal.Text) _
        '            & ",'dd/MM/yyyy')," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as saldo_awal " _
        '            & "FROM " _
        '            & "  public.glt_det a " _
        '            & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
        '            & "WHERE " _
        '            & "  to_char(a.glt_date, 'MM')='" & format(cint(bulan),"00") & "' and to_char(a.glt_date, 'YYYY')='" & .tahun.text & "'" _
        '            & " and a.glt_ac_id='" & GetIDByName("ac_mstr", "ac_id", "ac_code", e.Brick.Text) & "'"

        '        If .dom_id.Text.Length > 0 Then
        '            sSql += " AND a.glt_dom_id=" & .dom_id.Text
        '            posisi += "Domain : " & .dom_desc.Text & ", "
        '            If .en_id.Text.Length > 0 Then
        '                sSql += " AND a.glt_en_id=" & .en_id.Text
        '                posisi += "Entity : " & .en_desc.Text & ", "
        '                If .sb_id.Text.Length > 0 Then
        '                    sSql += " AND a.glt_sb_id=" & .sb_id.Text
        '                    posisi += "Sub Account : " & .sb_desc.Text & ", "
        '                    If .cc_id.Text.Length > 0 Then
        '                        sSql += " AND a.glt_cc_id=" & .cc_id.Text
        '                        posisi += "Cost Center : " & .cc_desc.Text
        '                    End If
        '                End If
        '            End If
        '        End If

        '        If Microsoft.VisualBasic.Right(posisi, 2) = ", " Then
        '            posisi = posisi.Substring(0, Len(posisi) - 2)
        '        End If

        '        Dim rpt As New rptBukuBesar
        '        With rpt
        '            Dim ds As New DataSet
        '            ds = ReportDataset(sSql)
        '            If ds.Tables(0).Rows.Count < 1 Then
        '                Box("Maaf transaksi buku besar bulan ini data kosong")
        '                Exit Sub
        '            End If
        '            .DataSource = ds
        '            .DataMember = "Table"
        '            .Parameters("PPeriode").Value = "PERIODE : " & frmNeracaPreview.TglAwal.Text & " s/d " & frmNeracaPreview.TglAkhir.Text
        '            .Parameters("PPosisi").Value = posisi
        '            printTool.ShowPreview()
        '        End With
        '    End With
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try

    End Sub

    Private Sub ac_code_PreviewMouseMove(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles ac_code.PreviewMouseMove
        Try
            Windows.Forms.Cursor.Current = Cursors.Hand
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class