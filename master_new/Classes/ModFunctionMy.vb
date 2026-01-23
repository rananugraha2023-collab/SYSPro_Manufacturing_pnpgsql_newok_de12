Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Text.Encoding
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Public Class ModFunctionMy
    Public Shared Function GetFileContents(ByVal FullPath As String, _
                Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
            'GetFileContents = strContents
        Catch Ex As Exception
            Return ""
            ErrInfo = Ex.Message
        End Try
    End Function

    Public Shared Function SaveTextToFile(ByVal strData As String, _
     ByVal FullPath As String, _
       Optional ByVal ErrInfo As String = "") As Boolean

        'Dim Contents As String
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message

        End Try
        Return bAns
    End Function
    Public Shared Function appbase() As String
        appbase = My.Application.Info.DirectoryPath
    End Function
    Public Shared Sub Pesan(ByVal XError As ErrObject)
        Try
            MsgBox("Error Number : " & Err.Number & "." & vbNewLine & "Description : " & Err.Description & vbNewLine & "Cause : " & Err.Source, MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox("Error Number : " & Err.Number & ". Description : " & Err.Description & " Cause : " & Err.Source, MsgBoxStyle.Information, "Information")
        End Try
    End Sub
    Public Shared Sub Box(ByVal XPesan As String, Optional ByVal Title As String = "Information")
        Try
            MsgBox(XPesan, MsgBoxStyle.Information, Title)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Shared Function SetSetring(ByVal Str As Object) As String

        SetSetring = "Null"

        If Len(Str) > 0 Then
            SetSetring = "'" & Replace(Str, "'", "") & "'"
        End If
       
    End Function

    Public Shared Function SetSetringDB(ByVal Str As Object) As String

        SetSetringDB = "Null"

        If Str Is System.DBNull.Value Then
            Return SetSetringDB
        End If
        'Return CType(Str, String)
        SetSetringDB = "'" & Replace(Str, "'", "") & "'"
      
    End Function


    Public Shared Function SetString(ByVal Str As Object) As String

        SetString = ""

        If Str Is System.DBNull.Value Then
            Return SetString
        End If
        Return CType(Str, String)
       
    End Function
    Public Shared Function SetStringTime(ByVal Str As Object) As String
        SetStringTime = ""

        If Str Is System.DBNull.Value Then
            Return SetStringTime
        End If
        'Left(CekTanggal().TimeOfDay.ToString, 8)
        Return Microsoft.VisualBasic.Left(TimeValue(Str).TimeOfDay.ToString, 8)
    End Function
    Public Shared Function SetStringTimeJM(ByVal Str As Object) As String
        SetStringTimeJM = ""

        If Str Is System.DBNull.Value Then
            Return SetStringTimeJM
        End If

        Return Microsoft.VisualBasic.Left(TimeValue(Str).TimeOfDay.ToString, 5)
    End Function
    Public Shared Function SetStringDate(ByVal Str As Object) As String
        SetStringDate = ""

        If Str Is System.DBNull.Value Then
            Return SetStringDate
        End If

        Return Format(CDate(Str), "dd/MM/yyyy")
    End Function
    Public Shared Function SetStringTimeFull(ByVal Str As Object) As String
        SetStringTimeFull = ""

        If Str Is System.DBNull.Value Then
            Return SetStringTimeFull
        End If
        'Left(CekTanggal().TimeOfDay.ToString, 8)
        Return Format(CDate(Str), "dd/MM/yyyy HH:mm:ss")
    End Function
    Public Shared Function SetDateNTime(ByVal Str As Object) As String

        SetDateNTime = "Null"
        If Len(Str) > 0 Then
            SetDateNTime = "'" & Format(CDate(Str), "yyyy/MM/dd HH:mm:ss") & "'"
        End If
      

    End Function
    Public Shared Function SetDateNTimeDB(ByVal Str As Object) As String

        SetDateNTimeDB = "Null"
        If Str Is System.DBNull.Value Then
            Return SetDateNTimeDB
        End If
        SetDateNTimeDB = "'" & Format(CDate(Str), "yyyy/MM/dd hh:mm:ss") & "'"
      
    End Function

    Public Shared Function SetNumber(ByVal Str As Object) As Double
        SetNumber = 0

        If Str Is System.DBNull.Value Then
            Return SetNumber
        End If
        Return CType(Str, Double)
    End Function
    '==================== fungsiku dech, gila nyarinya lima hari =============================
    Public Shared Function SetDateMask(ByVal Str As Object) As String

        SetDateMask = "Null"

        If Len(Str) > 6 Then
            SetDateMask = "'" & Format(CDate(Str), "yyyy/MM/dd") & " 0:00:00'"
        End If

    End Function

    Public Shared Function SetDateNTime00(ByVal Str As Object) As String

        'System.Globalization.DateTimeStyles.
        SetDateNTime00 = "Null"
        If Len(Str) > 0 Then
            'SetDateNTime00 = "'" & Microsoft.VisualBasic.DateAndTime.Day(CDate(Str)).ToString & "/" & _
            'Month(CDate(Str)).ToString & "/" & _
            'Year(CDate(Str)).ToString & " 00:00:00" & "'"

            SetDateNTime00 = "'" & Format(CDate(Str), "yyyy/MM/dd") & " 00:00:00" & "'"
        End If
      
    End Function
    Public Shared Function SetDateNTime59(ByVal Str As Object) As String

        'System.Globalization.DateTimeStyles.
        SetDateNTime59 = "Null"
        If Len(Str) > 0 Then
            'SetDateNTime00 = "'" & Microsoft.VisualBasic.DateAndTime.Day(CDate(Str)).ToString & "/" & _
            'Month(CDate(Str)).ToString & "/" & _
            'Year(CDate(Str)).ToString & " 00:00:00" & "'"

            SetDateNTime59 = "'" & Format(CDate(Str), "yyyy/MM/dd") & " 23:59:59" & "'"
        End If
        
    End Function
    Public Shared Function SetJam(ByVal Jam As Object, ByVal Tgl As Object) As String
        SetJam = "Null"

        If Len(Jam) > 3 Then
            'SetJam = "'" & Month(CDate(Tgl)).ToString & "/" & _
            'Microsoft.VisualBasic.DateAndTime.Day(CDate(Tgl)).ToString & "/" & _
            'Year(CDate(Tgl)).ToString & " " & Jam & ":00" & "'"

            SetJam = "'" & Format(CDate(Tgl), "yyyy/MM/dd") & " " & Jam & ":00'"
        End If

    End Function
    Public Shared Function SetInteger(ByVal Str As Object) As String

        SetInteger = "Null"

        If Len(Str) > 0 Then
            SetInteger = "" & Str & ""
        End If
       
    End Function
    Public Shared Function SetDec(ByVal Str As Object) As String
        SetDec = "Null"
        If Len(Str) > 0 Then
            SetDec = Replace(CDbl(Str).ToString, ",", ".")
        End If
    End Function
    Public Shared Function MaskDec(ByVal Str As Object, Optional ByVal Digit As Integer = 2) As String

        If Str Is System.DBNull.Value Then
            MaskDec = ""
        Else
            If Len(Str) > 0 Then
                If Digit = 0 Then
                    MaskDec = (CDbl(Str)).ToString("#,###")
                ElseIf Digit = 1 Then
                    MaskDec = (CDbl(Str)).ToString("#,###.0")
                ElseIf Digit = 2 Then
                    MaskDec = (CDbl(Str)).ToString("#,###.00")
                ElseIf Digit = 3 Then
                    MaskDec = (CDbl(Str)).ToString("#,###.000")
                Else
                    MaskDec = ""
                End If
            Else
                MaskDec = ""
            End If

        End If
    End Function
    Public Shared Function RmMaskDec(ByVal Str As Object) As String

        RmMaskDec = ""
        If Len(Str) > 0 Then
            RmMaskDec = CDbl(Str)
        End If
       

    End Function
    Public Shared Function SetDbl(ByVal Str As Object) As Double
        SetDbl = 0
        If Len(Str) > 0 Then
            SetDbl = CDbl(Str)
        End If

    End Function
    

    Public Shared Function RmInvalidChar(ByVal Str As Object) As String
        Dim r, s, t As String

        RmInvalidChar = Str

        If Len(Str) > 0 Then
            r = Replace(Str, "#", "")
            s = Replace(r, "~", "")
            t = Replace(s, ";", "")
            RmInvalidChar = t
        End If
       
    End Function
    Public Shared Function SetBit(ByVal Str As Object) As String
        SetBit = 0

        If Str = True Then
            SetBit = 1
        End If

    End Function

    Public Shared Function SetKlmn(ByVal StrLaki As Object, ByVal StrPr As Object) As String
        SetKlmn = "Null"

        If StrLaki = True Then
            SetKlmn = "'" & "Laki-laki" & "'"
        ElseIf StrPr = True Then
            SetKlmn = "'" & "Perempuan" & "'"
        End If

    End Function
    Public Shared Function CekDate(ByVal Str As Object) As Integer
        CekDate = 0

        If Len(Str) > 6 Then
            CekDate = 1
        End If

    End Function

    Public Shared Function CekSabtu(ByVal Str As Object) As String
        CekSabtu = 0

        If Format(Str, "dddd") = "Sabtu" Then
            CekSabtu = 1
        End If

    End Function

    Public Shared Function CekMinggu(ByVal Str As Object) As String
        CekMinggu = 0

        If Format(Str, "dddd") = "Minggu" Then
            CekMinggu = 1
        End If

    End Function
    Public Shared Function EndOfMonth(ByVal d As Date, ByVal m As Integer) As Date
        Return d.AddMonths(m + 1).AddDays(-d.Day)
    End Function
    Public Shared Sub WriteXmlToFile(ByVal Path As String, ByVal filename As String, ByVal thisDataSet As DataSet)
        If thisDataSet Is Nothing Then
            Return
        End If

        ' Create a file name to write to.
        'Dim filename As String = "myXmlDoc.xml"
        ' Create the FileStream to write with.
        Dim myFileStream As New System.IO.FileStream _
           (Path & "\" & filename, System.IO.FileMode.Create)

        ' Create an XmlTextWriter with the fileStream.
        Dim myXmlWriter As New System.Xml.XmlTextWriter _
           (myFileStream, System.Text.Encoding.Unicode)
        ' Write to the file with the WriteXml method.
        thisDataSet.WriteXml(myXmlWriter)
        myXmlWriter.Close()
    End Sub
    Public Shared Sub Blereng(ByVal list As ListView)

        Dim i As Integer
        For i = 0 To list.Items.Count - 1
            If i Mod 2 = 0 Then
                list.Items(i).BackColor = Color.AliceBlue
            End If
        Next


    End Sub
    Public Shared Sub Blereng2(ByVal Grid As DataGridView)

        Dim i As Integer
        For i = 0 To Grid.Rows.Count - 1
            If i Mod 2 = 0 Then
                Grid.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            End If
        Next


    End Sub
    Public Shared Sub HeaderColor(ByVal Grid As DataGridView)

        Dim i As Integer
        For i = 0 To Grid.ColumnCount - 1
            Dim header_style As New DataGridViewCellStyle
            header_style.BackColor = Color.CornflowerBlue
            Grid.Columns(i).HeaderCell.Style = header_style
        Next
        Grid.EnableHeadersVisualStyles = False
      
    End Sub

    Public Shared Function GetAge(ByVal Birthdate As System.DateTime, _
    Optional ByVal AsOf As System.DateTime = #1/1/1700#) _
    As String

        'Don't set second parameter if you want Age as of today

        'Demo 1: get age of person born 2/11/1954
        'Dim objDate As New System.DateTime(1954, 2, 11)
        'Debug.WriteLine(GetAge(objDate))

        'Demo 1: get same person's age 10 years from now
        'Dim objDate As New System.DateTime(1954, 2, 11)
        'Dim objdate2 As System.DateTime
        'objdate2 = Now.AddYears(10)
        'Debug.WriteLine(GetAge(objDate, objdate2))

        Dim iMonths As Integer
        Dim iYears As Integer
        Dim dYears As Decimal
        Dim lDayOfBirth As Long
        Dim lAsOf As Long
        Dim iBirthMonth As Integer
        Dim iAsOFMonth As Integer

        If AsOf = "#1/1/1700#" Then
            AsOf = DateTime.Now
        End If
        lDayOfBirth = DatePart(DateInterval.Day, Birthdate)
        lAsOf = DatePart(DateInterval.Day, AsOf)

        iBirthMonth = DatePart(DateInterval.Month, Birthdate)
        iAsOFMonth = DatePart(DateInterval.Month, AsOf)

        iMonths = DateDiff(DateInterval.Month, Birthdate, AsOf)

        dYears = iMonths / 12

        iYears = Math.Floor(dYears)

        If iBirthMonth = iAsOFMonth Then
            If lAsOf < lDayOfBirth Then
                iYears = iYears - 1
            End If
        End If

        Return iYears
    End Function
    Public Shared Function Terbilang(ByVal x As Integer) As String

        Dim bilangan As String() = {"", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas"}

        Dim temp As String = ""

        If x < 12 Then
            temp = " " + bilangan(x)
        ElseIf x < 20 Then
            temp = Terbilang(x - 10).ToString + " belas"
        ElseIf x < 100 Then
            temp = Terbilang(x / 10) + " puluh" + Terbilang(x Mod 10)
        ElseIf x < 200 Then
            temp = " seratus" + Terbilang(x - 100)
        ElseIf x < 1000 Then
            temp = Terbilang(x / 100) + " ratus" + Terbilang(x Mod 100)
        ElseIf x < 2000 Then
            temp = " seribu" + Terbilang(x - 1000)
        ElseIf x < 1000000 Then
            temp = Terbilang(x / 1000) + " ribu" + Terbilang(x Mod 1000)
        ElseIf x < 1000000000 Then
            temp = Terbilang(x / 1000000) + " juta" + Terbilang(x Mod 1000000)
        End If

        Return temp
    End Function

    Public Shared Function ComboAutoComplete(ByRef comboObj As ComboBox) As Boolean
        Dim lngItemNum As Long
        Dim lngSelectedLength As Long
        Dim lngMatchLength As Long
        'Dim strCurrentText As String
        Dim strSearchText As String
        Dim sTypedText As String
        Const CB_LOCKED = &H255
        Try
            With (comboObj)
                If .Items.Count > 0 Then
                    If .Text = Nothing Then
                        Exit Function
                    End If
                    comboObj.BeginUpdate()
                    If ((InStr(1, .Text, .Tag, vbTextCompare) <> 0 And Len(.Tag) = Len(.Text) - 1) Or (Microsoft.VisualBasic.Left(.Text, 1) <> Microsoft.VisualBasic.Left(.Tag, 1) And .Tag <> Nothing)) And .Tag <> CStr(CB_LOCKED) Then

                        strSearchText = .Text
                        lngSelectedLength = Len(strSearchText)

                        lngItemNum = .FindString(strSearchText)
                        ComboAutoComplete = Not (lngItemNum = -1)

                        If ComboAutoComplete Then
                            .Tag = CB_LOCKED
                            .SelectedIndex = lngItemNum
                            lngMatchLength = Len(.Text) - lngSelectedLength
                            sTypedText = strSearchText
                            .SelectionStart = lngSelectedLength
                            Dim Temp As Integer
                            Temp = lngMatchLength
                            .SelectionLength = Temp
                            .Tag = sTypedText
                        End If
                    ElseIf .Tag <> CStr(CB_LOCKED) Then
                        .Tag = .Text
                    End If
                    comboObj.EndUpdate()
                End If
            End With
        Catch err As Exception
            MsgBox(err.Message & err.StackTrace)
        End Try
    End Function
    Public Shared Function CekPath(ByVal path As String) As String
        If Microsoft.VisualBasic.Strings.Right(path, 1) = "\" Then
            Return path
        Else
            Return path + "\"
        End If
    End Function


    Public Shared Function CekForm(ByVal TForm As Form) As Boolean
        With TForm
            If .Location.ToString <> "{X=0,Y=0}" Then
                Return True
            Else
                Return False
            End If
        End With
    End Function

    Public Shared Sub CekForms(ByVal XForm As Form)
        If CekForm(XForm) = True Then
            XForm.Close()
        End If
    End Sub
    Public Shared Sub Angka(ByVal Objek As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) 'agar hanya angka sata yg bs input
        If Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar) Or e.KeyChar = "," Or e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Public Shared Sub SetDigit(ByVal Objek As TextBox, ByVal Mode As Integer)
        Try
            If Mode = 1 Then
                Objek.Text = MaskDec(Objek.Text)
            Else
                Objek.Text = RmMaskDec(Objek.Text)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function JadiTanggal(ByVal Str As String) As String
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim Tgl, Bln, Thn As Integer

        Bln = Now.Month
        Thn = Now.Year
        Try
            i = Str
            a = i.Split("/")
            For j = 0 To a.GetUpperBound(0)
                If j = 0 Then
                    If Len(a(j)) > 0 Then
                        Tgl = a(j)
                    End If
                End If
                If j = 1 Then
                    If Len(a(j)) > 0 Then
                        Bln = a(j)
                    End If
                End If
                If j = 2 Then
                    If Len(a(j)) > 0 Then
                        Thn = a(j)
                    End If
                End If
            Next
            JadiTanggal = Format(CDate(Tgl & "/" & Bln & "/" & Thn), "dd/MM/yyyy")

        Catch ex As Exception
            JadiTanggal = ""
        End Try
    End Function
    Public Shared Function JadiJam(ByVal Str As String) As String
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim Jam, Menit As Integer

        Jam = Now.Hour
        Menit = Now.Minute
        Try
            i = Str
            a = i.Split(".")
            For j = 0 To a.GetUpperBound(0)
                If j = 0 Then
                    If Len(a(j)) > 0 Then
                        Jam = a(j)
                    End If
                End If
                If j = 1 Then
                    If Len(a(j)) > 0 Then
                        Menit = a(j)
                    End If
                End If
            Next
            JadiJam = Format(Jam, "00") & ":" & Format(Menit, "00")

        Catch ex As Exception
            JadiJam = ""
        End Try
    End Function

    Public Shared Function AskDirectory() As String
        Dim folderBrowse As New FolderBrowserDialog
        If folderBrowse.ShowDialog() = DialogResult.OK Then
            Return folderBrowse.SelectedPath
        Else
            Return ""
        End If
    End Function

    Public Shared Function AskSaveAsFile() As String
        Dim fileD As New SaveFileDialog
        fileD.Filter = "Zip Files | *.zip"
        If fileD.ShowDialog() = DialogResult.OK Then
            Return fileD.FileName
        Else
            Return ""
        End If
    End Function
    Public Shared Function AskOpenFile() As String
        Dim fileD As New OpenFileDialog
        fileD.Filter = "Zip Files | *.zip"
        If fileD.ShowDialog() = DialogResult.OK Then
            Return fileD.FileName
        Else
            Return ""
        End If
    End Function

    Public Shared Sub CompactDB()
        Dim sDBFile As String = CekPath(GetFileContents(appbase() & "\conf\server.txt", "")) + "order.mdb"
        Dim tempFile As String = CekPath(GetFileContents(appbase() & "\conf\server.txt", "")) + "temp.mdb"
        Dim sBackUpFile As String = CekPath(GetFileContents(appbase() & "\conf\server.txt", "")) + "backup_order.mdb" '"D:\myDB_BackUp.mdb"

        Try

            If File.Exists(sDBFile) Then

                ''''''Dim db As New DAO.DBEngine
                'required DAO 3.6
                ' Firstly backup *.mdb database to temp file
                File.Copy(sDBFile, sBackUpFile, True)

                'db.CompactDatabase(sDBFile, tempFile, , , ";pwd=bukanyagampang")
                '' Secondly copy temp file to destination file
                File.Copy(tempFile, sDBFile, True)
                '' Lastly delete the temp file in order to get rid of the "Database Exists" issue
                File.Delete(tempFile)
                MsgBox("Compact database sucess", MsgBoxStyle.Information, "Informasi")
            Else
                MsgBox("Database file not exists", MsgBoxStyle.Information, "Informasi")

            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Shared Sub SubCekTombol(ByVal Bt1 As Button, ByVal Bt2 As Button, _
   ByVal Bt3 As Button, ByVal Bt4 As Button, ByVal JmlHal As Integer, ByVal Pss As Integer)
        Try
            If JmlHal = 1 Then
                Bt1.Enabled = False
                Bt2.Enabled = False
                Bt3.Enabled = False
                Bt4.Enabled = False
            Else
                If JmlHal = Pss Then
                    Bt1.Enabled = True
                    Bt2.Enabled = True
                    Bt3.Enabled = False
                    Bt4.Enabled = False
                ElseIf Pss = 1 Then
                    Bt1.Enabled = False
                    Bt2.Enabled = False
                    Bt3.Enabled = True
                    Bt4.Enabled = True
                ElseIf Pss > 1 And Pss < JmlHal Then
                    Bt1.Enabled = True
                    Bt2.Enabled = True
                    Bt3.Enabled = True
                    Bt4.Enabled = True
                End If
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Shared Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "110110011")
    End Function

    'Decrypt the text 
    Public Shared Function DecryptText(ByVal strText As String) As String
        Return Decrypt(strText, "110110011")
    End Function
    Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey _
                 As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(strEncrKey, 8))

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), _
                   CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return System.Convert.ToBase64String(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Shared Function Decrypt(ByVal strText As String, ByVal sDecrKey _
               As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = System.Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, _
                 IV), CryptoStreamMode.Write)

            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

            Return encoding.GetString(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    Public Shared Function konfigurasi(ByVal teks As String, ByVal katakunci As String) As String
        Try
            Dim a, b As Integer
            Dim TandaAwal, TandaAkhir As String

            TandaAwal = katakunci
            TandaAkhir = ";"

            a = teks.IndexOf(TandaAwal & "=")
            Dim r, s As String
            r = teks.Substring(a, Len(teks) - a)
            b = r.IndexOf(TandaAkhir)
            s = r
            s = s.Substring(Len(katakunci) + 1, b - Len(katakunci) - 1)

            konfigurasi = s
        Catch ex As Exception
            konfigurasi = ""
        End Try
    End Function
End Class
