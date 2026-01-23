Imports MyPGDll.ModFunction
Imports MyPGDll.PGSqlConn
Imports MyPGDll.ClassReportDev

Public Class rptBukuBesar

    Dim SaldoAwal, hasil, debit, kredit As System.Decimal
    ' Reset counters when printing a new group.
    Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) _
    Handles GroupHeader1.BeforePrint

        If GetCurrentColumnValue("saldo_awal") Is System.DBNull.Value Then
            SaldoAwal = 0
        Else
            SaldoAwal = CType(GetCurrentColumnValue("saldo_awal"), System.Decimal)
        End If
        hasil = hasil + SaldoAwal
    End Sub

    ' Add the values of the next data row.
    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) _
    Handles Detail.BeforePrint
        If GetCurrentColumnValue("glt_debit") Is System.DBNull.Value Then
            debit = 0
        Else
            debit = CType(GetCurrentColumnValue("glt_debit"), System.Decimal)
        End If
        If GetCurrentColumnValue("glt_credit") Is System.DBNull.Value Then
            kredit = 0
        Else
            kredit = CType(GetCurrentColumnValue("glt_credit"), System.Decimal)
        End If
        Dim tipe As String

        If GetCurrentColumnValue("ac_type") Is System.DBNull.Value Then
            tipe = ""
        Else
            tipe = Trim(CType(GetCurrentColumnValue("ac_type"), System.String))
        End If
        hasil = hasil + f_calc_gl(tipe, debit, kredit)

    End Sub

    Private Sub saldo_berjalan_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles saldo_berjalan.BeforePrint
        sender.Text = MaskDec(hasil)
    End Sub

    Private Sub GroupFooter1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        perubahan.Text = MaskDec(hasil - SaldoAwal)
        ending_balance.Text = MaskDec(hasil)
        hasil = 0
    End Sub

    Private Sub rptBukuBesar_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        hasil = 0
        debit = 0
        kredit = 0
        LPeriode.Text = Parameters("PPeriode").Value
        LPosisi.Text = Parameters("PPosisi").Value
    End Sub
    Function f_calc_gl(ByVal tipe_akun As String, ByVal vdebit As Double, ByVal vkredit As Double) As Double
        If tipe_akun = "R" Or tipe_akun = "L" Or tipe_akun = "C" Then
            f_calc_gl = vkredit - vdebit
        Else
            f_calc_gl = vdebit - vkredit
        End If
    End Function

    Private Sub glt_code_PreviewClick(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles glt_code.PreviewClick
        Dim sSql As String
        Try
            sSql = "SELECT  " _
                & "  a.glt_type, " _
                & "  a.glt_date, " _
                & "  a.glt_desc, " _
                & "  a.glt_code, " _
                & "  b.ac_code, " _
                & "  b.ac_name, " _
                & "  a.glt_debit, " _
                & "  a.glt_credit " _
                & "FROM " _
                & "  public.glt_det a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_code ='" & e.Brick.Text & "'"

            Dim rpt As New rptTransJurnal
            With rpt
                Dim ds As New DataSet
                ds = ReportDataset(sSql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong")
                    Exit Sub
                End If
                .DataSource = ds
                .DataMember = "Table"
                .Parameters("PPeriode").Value = ""

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Personal Selling SDI Report"
                printTool.ShowPreview()
                '.ShowPreview()
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub glt_code_PreviewMouseMove(ByVal sender As Object, ByVal e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles glt_code.PreviewMouseMove
        Try
            Windows.Forms.Cursor.Current = Cursors.Hand
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class