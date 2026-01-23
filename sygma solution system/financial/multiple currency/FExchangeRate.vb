Imports npgsql
Imports master_new.ModFunction
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class FExchangeRate
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FExchangeRate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr("IDR"))
        exr_cu_id_1.Properties.DataSource = dt_bantu
        exr_cu_id_1.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        exr_cu_id_1.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        exr_cu_id_1.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        exr_cu_id_2.Properties.DataSource = dt_bantu
        exr_cu_id_2.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        exr_cu_id_2.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        exr_cu_id_2.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Currency 1", "cu_name_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency 2", "cu_name_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start", "exr_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "exr_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Exchange Rate 1", "exr_cu_rate_1", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Exchange Rate 2", "exr_cu_rate_2", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "exr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "exr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "exr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "exr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  exr_oid, " _
                    & "  exr_dom_id, " _
                    & "  exr_add_by, " _
                    & "  exr_add_date, " _
                    & "  exr_upd_by, " _
                    & "  exr_upd_date, " _
                    & "  exr_cu_id_1, " _
                    & "  exr_cu_id_2, " _
                    & " cu_mstr_1.cu_name as cu_name_1, " _
                    & " cu_mstr_2.cu_name as cu_name_2, " _
                    & "  exr_cu_rate_1, " _
                    & "  exr_cu_rate_2, " _
                    & "  exr_start_date, " _
                    & "  exr_end_date, " _
                    & "  exr_dt " _
                    & "FROM  " _
                    & "  public.exr_rate" _
                    & " inner join cu_mstr cu_mstr_1 on cu_mstr_1.cu_id = exr_cu_id_1 " _
                    & " inner join cu_mstr cu_mstr_2 on cu_mstr_2.cu_id = exr_cu_id_2 " _
                    & " order by exr_cu_id_1, exr_add_date desc"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        exr_cu_id_1.Focus()
        exr_cu_id_1.ItemIndex = 0
        exr_cu_id_2.ItemIndex = 0
        exr_cu_rate_1.EditValue = 1.0
        exr_cu_rate_2.EditValue = 1.0
        exr_cu_id_1.Enabled = False 'biar default ke idr aja biar gampang....:D
        exr_cu_rate_2.Enabled = False 'biar default ke 1 aja biar gampang....:D
        exr_start_date.Text = ""
        exr_end_date.Text = ""
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        If exr_cu_id_1.EditValue = exr_cu_id_2.EditValue Then
            MessageBox.Show("Error Currency", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            exr_cu_id_1.Focus()
            Return False
        End If

        If exr_start_date.Text = "" Then
            MessageBox.Show("Start Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            exr_start_date.Focus()
            Return False
        End If

        If exr_end_date.Text = "" Then
            MessageBox.Show("End Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            exr_end_date.Focus()
            Return False
        End If

        If exr_end_date.DateTime < exr_start_date.DateTime Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            exr_end_date.Focus()
            Return False
        End If


        Dim sSQL As String
        sSQL = "select exr_end_date from exr_rate " + _
               " where exr_cu_id_1 = " + exr_cu_id_1.EditValue.ToString + _
               " and exr_cu_id_2 = " + exr_cu_id_2.EditValue.ToString + _
               " and exr_start_date between " & SetDateNTime00(exr_start_date.DateTime) _
               & " and " & SetDateNTime00(exr_end_date.DateTime)

        Dim sSQL2 As String
        sSQL2 = "select exr_end_date from exr_rate " + _
               " where exr_cu_id_1 = " + exr_cu_id_1.EditValue.ToString + _
               " and exr_cu_id_2 = " + exr_cu_id_2.EditValue.ToString + _
               " and exr_end_date between " & SetDateNTime00(exr_start_date.DateTime) _
               & " and " & SetDateNTime00(exr_end_date.DateTime)

        If master_new.PGSqlConn.CekRowSelect(sSQL) > 0 Or master_new.PGSqlConn.CekRowSelect(sSQL2) > 0 Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If



        sSQL = "select exr_end_date from exr_rate " + _
               " where exr_cu_id_1 = " + exr_cu_id_1.EditValue.ToString + _
               " and exr_cu_id_2 = " + exr_cu_id_2.EditValue.ToString + _
               " and exr_start_date <= " & SetDateNTime00(exr_start_date.DateTime) _
               & " and exr_end_date >= " & SetDateNTime00(exr_start_date.DateTime)


        sSQL2 = "select exr_end_date from exr_rate " + _
               " where exr_cu_id_1 = " + exr_cu_id_1.EditValue.ToString + _
               " and exr_cu_id_2 = " + exr_cu_id_2.EditValue.ToString + _
               " and exr_start_date <= " & SetDateNTime00(exr_end_date.DateTime) _
               & " and exr_end_date >= " & SetDateNTime00(exr_end_date.DateTime)

        If master_new.PGSqlConn.CekRowSelect(sSQL) > 0 Or master_new.PGSqlConn.CekRowSelect(sSQL2) > 0 Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _exr_oid As Guid
        _exr_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.exr_rate " _
                                            & "( " _
                                            & "  exr_oid, " _
                                            & "  exr_dom_id, " _
                                            & "  exr_add_by, " _
                                            & "  exr_add_date, " _
                                            & "  exr_cu_id_1, " _
                                            & "  exr_cu_id_2, " _
                                            & "  exr_cu_rate_1, " _
                                            & "  exr_cu_rate_2, " _
                                            & "  exr_start_date, " _
                                            & "  exr_end_date, " _
                                            & "  exr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_exr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetInteger(exr_cu_id_1.EditValue) & ",  " _
                                            & SetInteger(exr_cu_id_2.EditValue) & ",  " _
                                            & SetDbl(exr_cu_rate_1.EditValue) & ",  " _
                                            & SetDbl(exr_cu_rate_2.EditValue) & ",  " _
                                            & SetDateNTime00(exr_start_date.DateTime) & ",  " _
                                            & SetDateNTime00(exr_end_date.DateTime) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from exr_rate where exr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("exr_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Private Sub BtCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCheck.Click
        Try
            Dim _hasil As String = ""
            Dim _html As String
            Dim _url_awal As String = GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt")
            Dim url As String = konfigurasi(_url_awal, "exc_rate_reff") & exr_start_date.DateTime.ToString("yyyy") & "-" & CInt(exr_start_date.DateTime.ToString("MM")) & "-" & CInt(exr_start_date.DateTime.ToString("dd"))
            ' Creates an HttpWebRequest for the specified URL.

            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            ' Sends the request and waits for a response.
            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)

            ' Calls the method GetResponseStream to return the stream associated with the response.
            Dim receiveStream As Stream = myHttpWebResponse.GetResponseStream()
            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")

            ' Pipes the response stream to a higher level stream reader with the required encoding format.
            Dim readStream As New StreamReader(receiveStream, encode)

            Dim EOF As Boolean = False
            _html = ""

            Do While EOF = False

                Dim contents As String = readStream.ReadLine
                'If InStr(contents, "textbar") <> 0 Then
                _html = _html & vbCrLf & contents
                ' End If
                If readStream.EndOfStream = True Then
                    EOF = True
                End If
            Loop

            ' Releases the resources of the Stream.
            readStream.Close()
            ' Releases the resources of the response.
            myHttpWebResponse.Close()


            Dim a, b As Integer
            Dim TandaAwal, TandaAkhir As String

            TandaAwal = "Dolar Amerika Serikat [" '"Baht Thailand [" '
            TandaAkhir = "</tr>"


            a = _html.IndexOf(TandaAwal)
            b = _html.IndexOf(TandaAkhir)

            Dim r As String = _html


            r = r.Substring(a, 325)
            '_hasil = r

            Dim i As String
            Dim d() As String
            Dim j As Integer


            i = r
            d = i.Split(">")
            For j = 0 To d.GetUpperBound(0)

                If j = 10 Then
                    If Len(d(j)) > 0 Then
                        _hasil = d(j).Replace("</td", "")
                    End If
                End If
            Next

            Dim _format_koma As String
            _format_koma = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator

            exr_cu_rate_1.EditValue = SetNumber(_hasil.Replace(".", _format_koma))

        Catch ex As Exception
            If ex.Message.Contains("StartIndex cannot be less than zero") Then
                Box("Data not found")
            Else
                Pesan(Err)
            End If

        End Try

    End Sub

End Class
