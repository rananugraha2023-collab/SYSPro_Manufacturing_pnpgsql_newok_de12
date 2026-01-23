Imports MyPGDll.ModFunction
Imports MyPGDll.ClassReportDev
Imports MyPGDll.PGSqlConn

Public Class rptRugiLaba
    Dim Saldo, hasil, count, count2 As System.Decimal
    Dim Pendapatan, HPP, Biaya, PendapatanLain2, BiayaLain2 As System.Decimal
    Dim kode As String

    Private Sub rptRugiLaba_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            Saldo = 0
            hasil = 0
            count = 0

            LPeriode.Text = Parameters("PPeriode").Value
            LPosisi.Text = Parameters("PPosisi").Value

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) _
   Handles Detail.BeforePrint

        If GetCurrentColumnValue("hasil") Is System.DBNull.Value Then
            hasil = 0
        Else
            hasil = CType(GetCurrentColumnValue("hasil"), System.Decimal)
        End If
        Saldo = Saldo + hasil

    End Sub

    Private Sub GroupFooter2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter2.BeforePrint

        count2 = count2 + 1
        If count2 = 1 Then
            Pendapatan = Saldo
        ElseIf count2 = 2 Then
            HPP = Saldo
        ElseIf count2 = 3 Then
            Biaya = Saldo
        ElseIf count2 = 4 Then
            PendapatanLain2 = Saldo
        ElseIf count2 = 5 Then
            BiayaLain2 = Saldo
        End If
        Saldo = 0

    End Sub

    Private Sub GroupFooter1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint

        count = count + 1
        If count = 1 Then
            lhitungCaption.Text = "Laba Kotor"
            Hasil_Akhir.Text = MaskDec(Pendapatan - HPP)
        ElseIf count = 2 Then
            lhitungCaption.Text = "Laba Operasional"
            Hasil_Akhir.Text = MaskDec(Pendapatan - HPP - Biaya)
        Else
            lhitungCaption.Text = "Laba Bersih"
            Hasil_Akhir.Text = MaskDec(Pendapatan - HPP - Biaya + PendapatanLain2 - BiayaLain2)
        End If

    End Sub

    'Private Sub ac_code_PreviewClick(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles ac_code.PreviewClick
    '    Dim level, dom, en, sb, cc As Integer
    '    Dim sSql As String
    '    Dim posisi As String = ""
    '    Try
    '        dom = 0
    '        en = 0
    '        sb = 0
    '        cc = 0

    '        With frmRugiLabaPreview
    '            If .dom_id.Text.Length > 0 Then
    '                level = 1
    '                dom = CInt(.dom_id.Text)
    '                If .en_id.Text.Length > 0 Then
    '                    level = 2
    '                    en = CInt(.en_id.Text)
    '                    If .sb_id.Text.Length > 0 Then
    '                        level = 3
    '                        sb = CInt(.sb_id.Text)
    '                        If .cc_id.Text.Length > 0 Then
    '                            level = 4
    '                            cc = CInt(.cc_id.Text)
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                level = 1
    '                dom = 1
    '            End If

    '            sSql = "SELECT  " _
    '                & "  a.glt_type, " _
    '                & "  a.glt_date, " _
    '                & "  a.glt_desc, " _
    '                & "  a.glt_code,a.glt_ac_id, " _
    '                & "  b.ac_code,b.ac_type, " _
    '                & "  b.ac_name, " _
    '                & "  a.glt_debit, " _
    '                & "  a.glt_credit,f_get_begining_balance_acc(a.glt_ac_id, to_date(" & SetDateNTime00(.TglAwal.Text) _
    '                & ",'dd/MM/yyyy')," & level & "," & dom & "," & en & "," & sb & "," & cc & ") as saldo_awal " _
    '                & "FROM " _
    '                & "  public.glt_det a " _
    '                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
    '                & "WHERE " _
    '                & "  a.glt_date BETWEEN " & SetDateNTime00(.TglAwal.Text) & "  AND " & SetDateNTime00(.TglAkhir.Text) & "  " _
    '                & " and a.glt_ac_id='" & GetIDByName("ac_mstr", "ac_id", "ac_code", e.Brick.Text) & "'"

    '            If .dom_id.Text.Length > 0 Then
    '                sSql += " AND a.glt_dom_id=" & .dom_id.Text
    '                posisi += "Domain : " & .dom_desc.Text & ", "
    '                If .en_id.Text.Length > 0 Then
    '                    sSql += " AND a.glt_en_id=" & .en_id.Text
    '                    posisi += "Entity : " & .en_desc.Text & ", "
    '                    If .sb_id.Text.Length > 0 Then
    '                        sSql += " AND a.glt_sb_id=" & .sb_id.Text
    '                        posisi += "Sub Account : " & .sb_desc.Text & ", "
    '                        If .cc_id.Text.Length > 0 Then
    '                            sSql += " AND a.glt_cc_id=" & .cc_id.Text
    '                            posisi += "Cost Center : " & .cc_desc.Text
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            If Microsoft.VisualBasic.Right(posisi, 2) = ", " Then
    '                posisi = posisi.Substring(0, Len(posisi) - 2)
    '            End If

    '            Dim rpt As New rptBukuBesar
    '            With rpt
    '                Dim ds As New DataSet
    '                ds = ReportDataset(sSql)
    '                If ds.Tables(0).Rows.Count < 1 Then
    '                    Box("Maaf data kosong")
    '                    Exit Sub
    '                End If
    '                .DataSource = ds
    '                .DataMember = "Table"
    '                .Parameters("PPeriode").Value = "PERIODE : " & frmRugiLabaPreview.TglAwal.Text & " s/d " & frmRugiLabaPreview.TglAkhir.Text
    '                .Parameters("PPosisi").Value = posisi
    '                printTool.ShowPreview()
    '            End With
    '        End With
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try

    'End Sub

    Private Sub ac_code_PreviewMouseMove(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles ac_code.PreviewMouseMove
        Try
            Windows.Forms.Cursor.Current = Cursors.Hand
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class