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
Imports System.Net.Mail
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraExport
Imports System.Text.RegularExpressions
Imports DevExpress.Printing

Imports master_new.PGSqlConn
Imports System.Data.OleDb

Public Class ModFunction
    Public Shared Function GetCurrentPath() As String

        Dim sPath As String = System.Reflection.Assembly.GetExecutingAssembly.Location
        Dim n As Integer = InStrRev(sPath, "\")
        If (n > 0) Then
            sPath = Mid(sPath, 1, n)
        End If
        Return sPath
    End Function
    Public Shared Function FinsertSQL2Array(ByVal List As ArrayList) As ArrayList
        Dim sSqls As New ArrayList
        Dim sSql As String
        Dim i As Integer = 1
        Try
            For Each Sql As String In List

                sSql = "INSERT INTO  " _
                    & "  public.t_sql_out " _
                    & "( " _
                    & "  sql_uid, " _
                    & "  seq, " _
                    & "  sql_command, " _
                    & "  mili_second, " _
                    & "  waktu " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & i & ",  " _
                    & SetSetring(Sql) & ",  " _
                    & "cast(to_char(now(),'MS') as integer)" & ",  " _
                    & "now()" & "  " _
                    & ")"

                sSqls.Add(sSql)
                sSql = ""

                i = i + 1
            Next
            Return sSqls
        Catch ex As Exception
            Pesan(Err)
            Return sSqls
        End Try
    End Function
    Public Shared Function arr_to_str(ByVal par_arr As ArrayList) As String
        Try
            Dim _hasil As String = ""
            For Each sSQL As String In par_arr
                _hasil = _hasil & sSQL & vbNewLine
            Next
            Return _hasil
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function
    Public Function get_transaction_status_by_oid(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String) As String
        get_transaction_status_by_oid = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as value from " + par_table + _
                                           " where " + par_criteria + " = '" + par_oid + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_transaction_status_by_oid = .DataReader.Item("value")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_transaction_status_by_oid
    End Function

    '=======================fungsi manipulasi Data ====================

    Public Shared Function SetNewID_OLD(ByVal par_table As String, ByVal par_colom As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(" + par_colom + "),0) + 1 as max_col from " + par_table
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        SetNewID_OLD = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return SetNewID_OLD
    End Function

    Public Shared Function SetNewID_OLD(ByVal par_table As String, ByVal par_en_code As String, ByVal par_colom As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    '.Command.CommandText = "select coalesce(max(" + par_colom + "),0) + 1 as max_col from " + par_table
                    .Command.CommandText = "select coalesce(max(substring(cast(" + par_colom + " as varchar),3,100)),'0') as max_col " + _
                                           " from " + par_table
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        SetNewID_OLD = CInt(.DataReader("max_col")) + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        SetNewID_OLD = CInt(par_en_code + SetNewID_OLD.ToString)

        Return SetNewID_OLD
    End Function

    Public Shared Function SetNewID_OLD(ByVal par_table As String, ByVal par_colom As String, ByVal par_colom_criteria As String, ByVal criteria As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(" + par_colom + "),0) + 1 as max_col from " + par_table + _
                                           " where " + par_colom_criteria + " = '" + criteria + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        SetNewID_OLD = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return SetNewID_OLD
    End Function

    Public Shared Function SetNewID_OLD(ByVal par_table As String, ByVal par_en_code As String, ByVal par_colom As String, ByVal par_colom_criteria As String, ByVal criteria As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    '.Command.CommandText = "select coalesce(max(" + par_colom + "),0) + 1 as max_col from " + par_table + _
                    '                       " where " + par_colom_criteria + " = " + criteria
                    .Command.CommandText = "select coalesce(max(substring(cast(" + par_colom + " as varchar),3,100)),'0') as max_col  from " + par_table + _
                                           " where " + par_colom_criteria + " = '" + criteria + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        SetNewID_OLD = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        SetNewID_OLD = CInt(par_en_code + SetNewID_OLD.ToString)

        Return SetNewID_OLD
    End Function

    Public Shared Function SetDate(ByVal Str As Object) As String

        SetDate = "Null"

        If Str Is System.DBNull.Value Then
            Return SetDate
        End If

        If Len(Str) > 0 Then
            If PGSqlConn.CekStyeTanggal = "ISO, DMY" Then
                SetDate = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 00:00:00" & "'"
            Else
                SetDate = "'" & Format(CDate(Str), "MM/dd/yyyy") & " 00:00:00" & "'"
            End If
        End If

    End Function

    Public Shared Function SetDatePrn(ByVal Str As Object) As String

        SetDatePrn = "Null"

        If Str Is System.DBNull.Value Then
            Return SetDatePrn
        End If

        If Len(Str) > 0 Then
            If PGSqlConn.CekStyeTanggal = "ISO, DMY" Then
                SetDatePrn = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 00:00:00" & "'"
            Else
                SetDatePrn = "'" & Format(CDate(Str), "MM/dd/yyyy") & " 00:00:00" & "'"
            End If
        End If

    End Function
    Public Shared Function SetString(ByVal Str As Object) As String

        SetString = ""

        If Str Is System.DBNull.Value Or Str Is Nothing Then
            Return SetString
        End If
        'Return CType(Str, String)
        Try
            SetString = Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Encoding.ASCII.EncodingName, _
                        New EncoderReplacementFallback(String.Empty), New DecoderExceptionFallback()), _
                        Encoding.UTF8.GetBytes(Str.ToString)))

        Catch ex As Exception
            Return SetString
        End Try


        Return SetString
    End Function

    Public Shared Function SetStringNull(ByVal Str As Object) As Object
        SetStringNull = System.DBNull.Value
        If Str Is System.DBNull.Value Or Str Is Nothing Then
            Return SetStringNull
        Else
            If Str.ToString = "" Then
                Return SetStringNull
            Else
                Return Str.ToString
            End If

        End If

    End Function


    Public Shared Function SetStringTime(ByVal Str As Object) As String
        SetStringTime = ""

        If Str Is System.DBNull.Value Then
            Return SetStringTime
        End If

        Return Microsoft.VisualBasic.Left(TimeValue(Str).TimeOfDay.ToString, 8)
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

        Return Format(CDate(Str), "dd/MM/yyyy HH:mm:ss")
    End Function

    Public Shared Function SetNothing(ByVal Str As Object) As Object
        SetNothing = Nothing

        If Str Is System.DBNull.Value Then
            Return SetNothing
        Else
            Return Str
        End If

    End Function

    Public Shared Function SetNumber(ByVal Str As Object) As Double
        SetNumber = 0.0

        If Str Is System.DBNull.Value Or Str Is Nothing Then
            Return SetNumber
        End If

        If CStr(Str) = "" Then
            Return SetNumber
        End If
        Return CType(Str, Double)
    End Function

    Public Shared Function SetNumberNull(ByVal Str As Object) As Double
        SetNumberNull = Nothing

        If Str Is System.DBNull.Value Then
            Return SetNumberNull
        End If
        Return CType(Str, Integer)
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
        If Str Is System.DBNull.Value Then
            Return RmMaskDec
        End If

        If Len(Str) > 0 Then
            RmMaskDec = CDbl(Str)
        End If

    End Function

    Public Shared Function SetDbl(ByVal Str As Object) As String
        SetDbl = 0

        If Str Is System.DBNull.Value Then
            Return SetDbl
        End If

        If Len(Trim(Str)) > 0 Then
            SetDbl = Replace(CDbl(Str).ToString, ",", ".")
        End If
        Return SetDbl
    End Function

    Public Shared Function SetDblDB(ByVal Str As Object) As String
        SetDblDB = "Null"

        If Str Is System.DBNull.Value Then
            Return SetDblDB
        End If

        'If Str = "" Then
        '    Return SetDblDB
        'End If

        If Len(Str) > 0 Then
            SetDblDB = Replace(CDbl(Str).ToString, ",", ".")
            'SetDblDB = Str
        End If
        Return SetDblDB
    End Function


    Public Shared Function SetDateNTimeDB(ByVal Str As Object) As String

        SetDateNTimeDB = "Null"
        If Str Is System.DBNull.Value Then
            Return SetDateNTimeDB
        End If
        SetDateNTimeDB = "'" & Format(CDate(Str), "yyyy/MM/dd hh:mm:ss") & "'"

    End Function

    Public Shared Function SetStringTimeJM(ByVal Str As Object) As String
        SetStringTimeJM = ""

        If Str Is System.DBNull.Value Then
            Return SetStringTimeJM
        End If

        Return Microsoft.VisualBasic.Left(TimeValue(Str).TimeOfDay.ToString, 5)
    End Function

    '===================funsi untuk SQL syntaks ====================

    Public Shared Function SetDateNTime(ByVal Str As Object) As String
        SetDateNTime = "Null"

        If Str Is System.DBNull.Value Then
            Return SetDateNTime
        End If

        If Len(Str) > 0 Then

            If PGSqlConn.CekStyeTanggal = "ISO, DMY" Then
                SetDateNTime = "'" & Format(CDate(Str), "dd/MM/yyyy HH:mm:ss") & "'"
            Else
                SetDateNTime = "'" & Format(CDate(Str), "MM/dd/yyyy HH:mm:ss") & "'"
            End If
        End If

    End Function

    Public Shared Function SetDateNTime(ByVal Str As Object, ByVal par_pasti As Boolean) As String
        SetDateNTime = "Null"

        If Str Is System.DBNull.Value Then
            Return SetDateNTime
        End If

        If Len(Str) > 0 Then
            SetDateNTime = "'" & Format(CDate(Str), "dd/MM/yyyy HH:mm:ss") & "'"
        End If

    End Function

    Public Shared Function SetDateMask(ByVal Str As Object) As String

        SetDateMask = "Null"

        If Len(Str) > 6 Then
            SetDateMask = "'" & Format(CDate(Str), "dd/MM/yyyy") & "'"
        End If

    End Function

    Public Shared Function SetDateNTime00(ByVal Str As Object) As String

        SetDateNTime00 = "Null"
        If Str Is System.DBNull.Value Then
            Return SetDateNTime00
        End If

        If Len(Str) > 0 Then
            If PGSqlConn.CekStyeTanggal = "ISO, DMY" Then
                SetDateNTime00 = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 0:00:00'"
            Else
                SetDateNTime00 = "'" & Format(CDate(Str), "MM/dd/yyyy") & " 0:00:00'"
            End If

        End If

    End Function
    Public Shared Function SetDateNTime00(ByVal Str As Object, ByVal par_pasti As Boolean) As String

        SetDateNTime00 = "Null"
        If Str Is System.DBNull.Value Then
            Return SetDateNTime00
        End If

        If Len(Str) > 0 Then
            SetDateNTime00 = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 0:00:00'"
        End If

    End Function
    Public Shared Function SetDateNTime99(ByVal Str As Object) As String

        SetDateNTime99 = "Null"
        If Str Is System.DBNull.Value Then
            Return SetDateNTime99
        End If

        If Len(Str) > 0 Then
            If PGSqlConn.CekStyeTanggal = "ISO, DMY" Then
                SetDateNTime99 = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 23:29:59'"
            Else
                SetDateNTime99 = "'" & Format(CDate(Str), "MM/dd/yyyy") & " 23:29:59'"
            End If

            'SetDateNTime99 = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 23:29:59'"
        End If

    End Function

    Public Shared Function SetDateNTime991(ByVal Str As Object) As String

        SetDateNTime991 = "Null"
        If Str Is System.DBNull.Value Then
            Return SetDateNTime991
        End If

        If Len(Str) > 0 Then
            If PGSqlConn.CekStyeTanggal = "ISO, DMY" Then
                SetDateNTime991 = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 11:00:00'"
            Else
                SetDateNTime991 = "'" & Format(CDate(Str), "MM/dd/yyyy") & " 11:00:00'"
            End If

            'SetDateNTime99 = "'" & Format(CDate(Str), "dd/MM/yyyy") & " 23:29:59'"
        End If

    End Function
    Public Shared Function SetJam(ByVal Jam As Object, ByVal Tgl As Object) As String
        SetJam = "Null"

        If Len(Jam) > 3 Then
            SetJam = Format(CDate(Tgl), "dd/MM/yyyy") & " " & Jam & ":00"
        End If

    End Function

    Public Shared Function SetInteger(ByVal Str As Object) As String

        SetInteger = "Null"

        If Str Is System.DBNull.Value Then
            Return SetInteger
        End If

        If Len(Str) > 0 Then
            SetInteger = "" & CDbl(Str) & ""
        End If

    End Function

    Public Shared Function SetInteger_le(ByVal Str As Object) As String

        SetInteger_le = "Null"

        If Str Is System.DBNull.Value Then
            Return SetInteger_le
        Else
            If Str = 0 Or Str = -1 Then
                Return SetInteger_le
            Else
                SetInteger_le = "" & Str & ""
            End If
        End If
    End Function

    Public Shared Function SetIntegerDB(ByVal Str As Object) As String

        SetIntegerDB = "Null"

        If Str Is System.DBNull.Value Then
            Return SetIntegerDB
        End If

        If Len(Str) > 0 Then
            SetIntegerDB = "" & CInt(Str) & ""
        End If

    End Function

    Public Shared Function SetDec(ByVal Str As Object) As String
        SetDec = "Null"


        If Str Is System.DBNull.Value Then
            Return SetDec
        End If

        If Len(Str) > 0 Then
            SetDec = Replace(CDbl(Str).ToString, ",", ".")
        End If
    End Function

    Public Shared Function SetDecDB(ByVal Str As Object) As String
        SetDecDB = "Null"

        If Str Is System.DBNull.Value Then
            Return SetDecDB
        End If

        If Len(Str) > 0 Then
            SetDecDB = Replace(CDbl(Str).ToString, ",", ".")
        End If
    End Function

    Public Shared Function SetSetring(ByVal Str As Object) As String

        SetSetring = "Null"
        If Str Is System.DBNull.Value Or Str Is Nothing Then
            Return SetSetring
        End If


        If Len(Str.ToString) > 0 Then
            SetSetring = "'" & Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Encoding.ASCII.EncodingName, _
                        New EncoderReplacementFallback(String.Empty), New DecoderExceptionFallback()), _
                        Encoding.UTF8.GetBytes(Str.ToString))).ToString.Replace("'", "''") & "'"

            'SetSetring = "'" & Trim(Replace(Regex.Replace(Str, "\p{C}+", ""), "'", "''")) & "'"
        End If
        'SetSetring = "'" & Trim(Replace(Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(Str)), "'", "''")) & "'"
    End Function

    Public Shared Function SetSetringDB(ByVal Str As Object) As String

        SetSetringDB = "Null"

        If Str Is System.DBNull.Value Or Str Is Nothing Then
            Return SetSetringDB
        End If

        SetSetringDB = "'" & Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Encoding.ASCII.EncodingName, _
                        New EncoderReplacementFallback(String.Empty), New DecoderExceptionFallback()), _
                        Encoding.UTF8.GetBytes(Str.ToString))).ToString.Replace("'", "''") & "'"

        '"'" & Trim(Replace(Regex.Replace(Str, "\p{C}+", ""), "'", "''")) & "'"

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

    Public Shared Function SetBitYN(ByVal Str As Object) As String

        SetBitYN = "'N'"

        If Str Is System.DBNull.Value Then
            Return SetBitYN
        End If

        If Str = True Then
            SetBitYN = "'Y'"
        End If

        Return SetBitYN
    End Function
    Public Shared Function SetBitYNB(ByVal Str As Object) As Boolean

        SetBitYNB = False
        If Str Is System.DBNull.Value Then
            Return SetBitYNB
        End If

        If Str = "Y" Then
            SetBitYNB = True
        End If

    End Function

    Public Shared Function SetBit(ByVal Str As Object) As String
        SetBit = "False"

        If Str = True Then
            SetBit = "True"
        End If

    End Function
    Public Shared Function setBoolean(ByVal Str As Object) As Boolean
        setBoolean = False
        If Str Is System.DBNull.Value Then
            Return setBoolean
        End If

        If Str = True Then
            setBoolean = True
        End If
    End Function

    '============ fungsi umum =================

    Public Shared Function appbase() As String
        appbase = My.Application.Info.DirectoryPath
    End Function

    Public Shared Function EndOfMonth(ByVal d As Date, ByVal m As Integer) As Date
        'Return d.AddMonths(m + 1).AddDays(-d.Day)
        Return DateSerial(d.Year, d.Month + 1, 0)

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

        Dim bilangan As String() = {"", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas"}

        Dim temp As String = ""



        If x < 12 Then

            temp = " " + bilangan(x)

        ElseIf x < 20 Then

            temp = Terbilang(Int(x - 10)).ToString + " Belas"

        ElseIf x < 100 Then

            temp = Terbilang(Int(x / 10)) + " Puluh" + Terbilang(x Mod 10)

        ElseIf x < 200 Then

            temp = " Seratus" + Terbilang(Int(x - 100))

        ElseIf x < 1000 Then

            temp = Terbilang(Int(x / 100)) + " Ratus" + Terbilang(x Mod 100)

        ElseIf x < 2000 Then

            temp = " Seribu" + Terbilang(Int(x - 1000))

        ElseIf x < 1000000 Then

            temp = Terbilang(Int(x / 1000)) + " Ribu" + Terbilang(x Mod 1000)

        ElseIf x < 1000000000 Then

            temp = Terbilang(Int(x / 1000000)) + " Juta" + Terbilang(x Mod 1000000)

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
    Public Shared Function AskSaveAsFile(ByVal filter As String) As String
        Dim fileD As New SaveFileDialog
        fileD.Filter = filter
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
    Public Shared Function AskOpenFile(ByVal filter As String) As String
        Dim fileD As New OpenFileDialog
        fileD.Filter = filter

        If fileD.ShowDialog() = DialogResult.OK Then
            Return fileD.FileName
        Else
            Return ""
        End If
    End Function

    Public Shared Function ImportExcel(ByVal PrmPathExcelFile As String) As DataSet


        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim ds As New DataSet
        Dim da As System.Data.OleDb.OleDbDataAdapter
        Dim filteext As String = ""


        'Dim conn As System.Data.OleDb.OleDbConnection
        'conn = New System.Data.OleDb.OleDbConnection( _
        '        "provider=Microsoft.Jet.OLEDB.4.0; " & _
        '        "data source=" & PrmPathExcelFile & "; " & _
        '        "Extended Properties=" & filteext & ";")

        Try

            If PrmPathExcelFile = "" Then

                Return Nothing
                Exit Function
            End If

            Dim _ext As String = IO.Path.GetExtension(PrmPathExcelFile)
            If IO.Path.GetExtension(PrmPathExcelFile).ToLower = ".xls" Then
                filteext = "Excel 8.0"
            ElseIf IO.Path.GetExtension(PrmPathExcelFile).ToLower = ".xlsx" Then
                filteext = "Excel 12.0"
            End If

            MyConnection = New System.Data.OleDb.OleDbConnection _
              ("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & PrmPathExcelFile & "';Extended Properties=" & filteext & ";")
            MyConnection.Open()

            Dim myTableName = MyConnection.GetSchema("Tables").Rows(0)("TABLE_NAME")

            'da = New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [sheet1$]", conn)

            Dim MyCommand As OleDbDataAdapter = New OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", myTableName), MyConnection)

            MyCommand.TableMappings.Add("Table", "TestTable")

            MyCommand.Fill(ds)

            Return ds
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function

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
    Public Shared Function SaveArraytoFile(ByVal _array As ArrayList) As Boolean
        Dim data As String = ""

        Try
            For Each teks As String In _array
                data += teks & ";" & vbNewLine & vbNewLine
            Next
            SaveTextToFile(data, AskSaveAsFile("Txt Files | *.txt"))

            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
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
    Public Shared Function ask(ByVal _ask_string As String, ByVal _ask_info As String) As Boolean
        If DevExpress.XtraEditors.XtraMessageBox.Show(_ask_string, _ask_info, _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function ask(ByVal _ask_string As String, ByVal _ask_info As String, ByVal _default As MessageBoxDefaultButton) As Boolean
        If DevExpress.XtraEditors.XtraMessageBox.Show(_ask_string, _ask_info, _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, _default) = DialogResult.No Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Sub Pesan(ByVal XError As ErrObject)
        Try
            DevExpress.XtraEditors.XtraMessageBox.Show("Error Number : " & Err.Number _
                & "." & vbNewLine & "Description : " & Err.Description & vbNewLine _
                & "Cause : " & Err.Source, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MsgBox("Error Number : " & Err.Number & ". Description : " & Err.Description & " Cause : " & Err.Source, MsgBoxStyle.Information, "Information")
        End Try
    End Sub
    Public Shared Sub Pesan(ByVal XError As ErrObject, ByVal position As String)
        Try
            DevExpress.XtraEditors.XtraMessageBox.Show("Error Number : " & Err.Number _
                & "." & vbNewLine & "Description : " & Err.Description & vbNewLine _
                & "Cause : " & Err.Source, "Err position : " & position, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MsgBox("Error Number : " & Err.Number & ". Description : " & Err.Description & " Cause : " & Err.Source, MsgBoxStyle.Information, "Information")
        End Try
    End Sub
    Public Shared Sub Box(ByVal XPesan As String, Optional ByVal Title As String = "Information")
        Try
            'MsgBox(XPesan, MsgBoxStyle.Information, Title)
            DevExpress.XtraEditors.XtraMessageBox.Show(XPesan, Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Public Shared Function MD5Encrypt(ByVal str As String) As String

        'Imports System.Security.Cryptography

        Dim md5 As MD5CryptoServiceProvider
        Dim bytValue() As Byte
        Dim bytHash() As Byte
        Dim strOutput As String = ""
        Dim i As Integer

        ' Create New Crypto Service Provider Object
        md5 = New MD5CryptoServiceProvider

        ' Convert the original string to array of Bytes
        bytValue = System.Text.Encoding.UTF8.GetBytes(str)

        ' Compute the Hash, returns an array of Bytes
        bytHash = md5.ComputeHash(bytValue)
        md5.Clear()

        For i = 0 To bytHash.Length - 1
            'don't lose the leading 0
            strOutput &= bytHash(i).ToString("x").PadLeft(2, "0")
        Next

        MD5Encrypt = strOutput

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

    Public Function get_transaction_status(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_code As String) As String
        get_transaction_status = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as value from " + par_table + _
                                           " where " + par_criteria + " ~~* '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_transaction_status = .DataReader.Item("value")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_transaction_status
    End Function

    Public Function get_user_wf_seq(ByVal par_code As String) As Integer
        get_user_wf_seq = 0
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select wf_seq from wf_mstr  " + _
                                           " where wf_ref_code ~~* '" + par_code + "'" + _
                                           " and wf_iscurrent='Y' order by wf_seq desc limit 1"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_user_wf_seq = .DataReader.Item("wf_seq")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_user_wf_seq
    End Function

    Public Function get_user_wf(ByVal par_code As String, ByVal par_wf_seq As Integer) As String
        get_user_wf = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_user_id from wf_mstr  " + _
                                           " where wf_ref_code ~~* '" + par_code + "'" + _
                                           " and wf_seq = " + par_wf_seq.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_user_wf = .DataReader.Item("wf_user_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_user_wf
    End Function

    Public Function get_email_address(ByVal par_nama As String) As String
        get_email_address = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(useremail,'') as useremail From tconfuser  " + _
                                           " where usernama ~~* '" + par_nama + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_email_address = .DataReader.Item("useremail")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_email_address
    End Function

    Public Function get_id_telegram(ByVal par_nama As String) As String
        get_id_telegram = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(user_id_telegram,0) as user_id_telegram From tconfuser  " + _
                                           " where usernama ~~* '" + par_nama + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_id_telegram = .DataReader.Item("user_id_telegram")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_id_telegram
    End Function

    Public Function get_phone(ByVal par_nama As String) As String
        get_phone = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(userphone,'') as userphone From tconfuser  " + _
                                           " where usernama ~~* '" + par_nama + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_phone = .DataReader.Item("userphone")
                    End While

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_phone
    End Function
    Public Function format_email(ByVal par_nama As String, ByVal par_no As String, ByVal par_type As String) As String
        format_email = ""
        Dim wf_seq As Integer = 0
        Dim wf_user_id As String = "", nik As String = "", pwd As String = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(usernama,'') as usernik, password from tconfuser " + _
                                           " where usernama ~~* '" + par_nama + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        nik = .DataReader.Item("usernik")
                        pwd = .DataReader.Item("password")
                    End While
                    format_email = "*" + nik + "*" + pwd + "*" + par_type + "*" + par_no + "*A#"
                End With
            End Using
        Catch ex As Exception
        End Try

        Return format_email
    End Function

    Public Function get_max_seq(ByVal par_code As String) As Integer
        get_max_seq = -1
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select max(wf_seq) as max_seq from wf_mstr where wf_ref_code ~~* '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_max_seq = .DataReader.Item("max_seq")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_max_seq
    End Function

    Public Function sent_email(ByVal par_to As String, ByVal par_cc As String, ByVal par_subject As String, ByVal par_body As String, ByVal par_from As String, ByVal par_attachment As String) As Boolean
        'If par_cc = "" Then
        '    par_cc = "setiadi.sudrajat@hariff.com"
        'End If

        sent_email = True
        Dim message As New MailMessage
        Try

            message.From = New MailAddress(par_from)
            message.To.Add(par_to)

            If par_cc <> "" Then
                message.CC.Add(par_cc)
            End If

            message.Subject = par_subject
            message.Body = par_body


            If par_attachment <> "" Then
                Dim attachment = New Attachment(par_attachment)
                message.Attachments.Add(attachment)
            End If

            'Dim emailClient As New SmtpClient("mail.hariff.com")
            'Dim SMTPUserInfo As New System.Net.NetworkCredential("syspro@hariff.com", "sysprohariff")

            'Dim emailClient As New SmtpClient("smtp.gmail.com")
            'Dim SMTPUserInfo As New System.Net.NetworkCredential("email.handler2011@gmail.com", "bismillah123")

            'emailClient.Port = 587
            'emailClient.UseDefaultCredentials = False
            'emailClient.Credentials = SMTPUserInfo
            'emailClient.EnableSsl = True
            'emailClient.Send(message)

            Dim emailClient As New SmtpClient(master_new.ClsVar.email_server_name)
            Dim SMTPUserInfo As New System.Net.NetworkCredential(master_new.ClsVar.email_user_name, master_new.ClsVar.email_password)


            emailClient.Port = CInt(master_new.ClsVar.email_port)
            emailClient.UseDefaultCredentials = False
            emailClient.Credentials = SMTPUserInfo
            emailClient.EnableSsl = IIf(master_new.ClsVar.email_ssl = "0", False, True)
            emailClient.Send(message)
            message.Attachments.Dispose()

            Return True
        Catch ex As Exception
            Return False
            MessageBox.Show(ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Function

    Public Function get_conf_file(ByVal par_type As String) As String
        get_conf_file = ""
        Try
            Dim dr As DataRow
            Dim ssql As String
            ssql = "select conf_value from conf_file " + _
                    " where conf_name = '" + par_type + "'"

            dr = GetRowInfo(ssql)
            If dr Is Nothing Then
                'Box("Sorry, configuration " & par_type & " doesn't exist")
                ' get_conf_file
            ElseIf dr(0) Is System.DBNull.Value Then
                Box("Sorry, configuration " & par_type & " is null")
            Else
                get_conf_file = dr(0).ToString
            End If

            'Using objkalendar As New master_new.WDABasepgsql("", "")
            '    With objkalendar
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "select conf_value from conf_file " + _
            '                               " where conf_name = '" + par_type + "'"
            '        .InitializeCommand()
            '        .DataReader = .Command.ExecuteReader
            '        While .DataReader.Read
            '            get_conf_file = .DataReader.Item("conf_value")
            '        End While
            '    End With
            'End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_conf_file
    End Function

    Public Function get_http_approval() As String
        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
            Dim url As String = get_conf_file("server_api_online")
            If url = "" Then
                Box("http_approval_online has not been Configured")
                Return ""
            Else
                Return url
            End If
        Else
            Return get_conf_file("server_api_local")
        End If
    End Function

    Public Function send_wa_to_nik(ByVal par_nik As String, ByVal par_pesan As String) As Boolean

        Try
            Dim hasil As Boolean = False
            Dim _alamat_api As String = "" & get_http_approval() _
                            & "php72/api_iot/kirim_app.php?app=wa_send_to_nik&nik=" & par_nik & "&pesan=" & par_pesan

            _alamat_api = _alamat_api.Replace(" ", "%20")
            Dim result As String
            result = run_get_to_api(_alamat_api & "")

            If SetString(result).Contains("success") Then
                'Make_Report("Nik : " & par_nik & " pesan " & par_pesan & " success")
                hasil = True
            Else
                'Box("Gagal notif telegram")
                'Exit Function
                hasil = False
            End If


            Return hasil
        Catch ex As Exception
            'Make_Report(ex.Message)
            Return False
        End Try
    End Function


    Public Function run_get_to_api(ByVal par_variable As String) As String
        Try
            Dim request As WebRequest = WebRequest.Create(par_variable)

            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As WebResponse = request.GetResponse()
            ' Display the status.
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            Dim responseFromServer As String = reader.ReadToEnd()
            'Dim responseFromServer As String = reader.ReadToEnd()
            'Console.WriteLine(responseFromServer)
            Dim result = responseFromServer 'JsonConvert.DeserializeObject(Of ArrayList)(responseFromServer)

            Return result
        Catch ex As Exception

            Return Nothing
        End Try
    End Function

    Public Function title_email(ByVal par_title As String, ByVal par_no As String) As String
        title_email = "Email Gateway - " + par_title
        master_new.ClsVar.sBody = "The attach file contains the description of " + par_title + vbCrLf + "Please do the approval."

        If par_no <> "" Then
            title_email = title_email + " Number : " + par_no
        End If
        Return title_email
    End Function

    Public Function petunjuk() As String
        petunjuk = ""
        petunjuk = vbCrLf + vbCrLf + "The approval procedure can be done by using the following methodes (choose only one): "
        petunjuk = petunjuk + vbCrLf + "1. By SYSPRO Application, or"
        petunjuk = petunjuk + vbCrLf + "2. Reply This Email"
        'petunjuk = petunjuk + vbCrLf + vbCrLf + "A = Approve, H = Hold, X = Cancel"
        Return petunjuk
    End Function

    Public Shared Function required(ByVal fobject As DevExpress.XtraEditors.TextEdit, ByVal title As String) As Boolean
        If fobject.EditValue Is System.DBNull.Value Then
            fobject.Focus()
            Box("Sorry " & title & " cannot blank")
            Return False
        Else
            If fobject.EditValue Is Nothing Then
                fobject.Focus()
                Box("Sorry " & title & " cannot blank")
                Return False
            Else
                If fobject.EditValue.ToString = "" Then
                    fobject.Focus()
                    Box("Sorry " & title & " cannot blank")
                    Return False
                Else
                    If fobject.EditValue.ToString = "0" Then
                        fobject.Focus()
                        Box("Sorry " & title & " cannot blank")
                        Return False
                    Else
                        Return True
                    End If
                End If
            End If
        End If
    End Function

    Public Shared Sub add_col_treelist(ByVal tree_list As DevExpress.XtraTreeList.TreeList, ByVal par_caption As String, _
                               ByVal par_field As String)


        Dim col As DevExpress.XtraTreeList.Columns.TreeListColumn = tree_list.Columns.Add()

        col.Caption = par_caption
        col.FieldName = par_field
        col.VisibleIndex = tree_list.VisibleColumns.Count
        col.Name = par_field

        'col.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String
        'treeListColumn.VisibleIndex = 0
        'col.Width = 100
        'col.MinWidth = 81
        'Box(tree_list.VisibleColumns.Count)

    End Sub

    Public Shared Function GetIPAddress() As String
        Dim sam As System.Net.IPAddress
        Dim sam1 As String
        With System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            sam = New System.Net.IPAddress(.AddressList(0).Address)
            sam1 = sam.ToString
        End With
        GetIPAddress = sam1
    End Function

    Public Shared Function IPAddresses(ByVal server As String) As String
        Dim _hasil As String = ""
        Try
            Dim ASCII As New System.Text.ASCIIEncoding()

            ' Get server related information.
            Dim heserver As IPHostEntry = Dns.Resolve(server)

            ' Loop on the AddressList
            Dim curAdd As IPAddress
            For Each curAdd In heserver.AddressList

                ' Display the type of address family supported by the server. If the
                ' server is IPv6-enabled this value is: InternNetworkV6. If the server
                ' is also IPv4-enabled there will be an additional value of InterNetwork.
                'Console.WriteLine(("AddressFamily: " + curAdd.AddressFamily.ToString()))

                ' Display the ScopeId property in case of IPV6 addresses.
                If curAdd.AddressFamily.ToString() = ProtocolFamily.InterNetworkV6.ToString() Then
                    'Console.WriteLine(("Scope Id: " + curAdd.ScopeId.ToString()))
                End If

                ' Display the server IP address in the standard format. In 
                ' IPv4 the format will be dotted-quad notation, in IPv6 it will be
                ' in in colon-hexadecimal notation.
                ' Console.WriteLine(("Address: " + curAdd.ToString()))
                _hasil = _hasil & curAdd.ToString & " "
                ' Display the server IP address in byte format.
                'Console.Write("AddressBytes: ")

                Dim bytes As [Byte]() = curAdd.GetAddressBytes()
                Dim i As Integer
                For i = 0 To bytes.Length - 1
                    Console.Write(bytes(i))
                Next i
                'Console.WriteLine(ControlChars.Cr + ControlChars.Lf)
            Next curAdd

            Return _hasil

        Catch e As Exception
            'Console.WriteLine(("[DoResolve] Exception: " + e.ToString()))
            Return ""
        End Try
    End Function 'IPAddresses

    Public Shared Function GetVersion() As String
        Try
            Dim _hasil As String = ""
            Dim f As New FileInfo(appbase() & "\sygma solution system.exe")
            _hasil = f.Name & " build date : " & f.LastWriteTime.ToString

            Dim g As New FileInfo(appbase() & "\master_new.dll")
            _hasil = _hasil & ", " & g.Name & " build date : " & g.LastWriteTime.ToString

            Return _hasil
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function GetDetailUser() As String
        Dim _user As String
        Dim _host As String
        Dim _ip As String

        Try
            _user = SystemInformation.UserName
            _host = System.Net.Dns.GetHostName
            _ip = IPAddresses(_host)

            Return _host & " " & _user & " " & _ip
        Catch ex As Exception
            Pesan(Err)
            Return ""
        End Try
    End Function

    Public Shared Function insert_log(ByVal param_activity As String) As String
        Dim sSQL As String
        sSQL = "INSERT INTO  " _
        & "  public.useraccd_dml " _
        & "( " _
        & "  useraccd_oid, " _
        & "  date_activity, " _
        & "  user_id, " _
        & "  activity, " _
        & "  detail_user " _
        & ")  " _
        & "VALUES ( " _
        & SetSetring(Guid.NewGuid.ToString) & ",  " _
        & SetDateNTime(PGSqlConn.CekTanggal) & ",  " _
        & SetSetring(master_new.ClsVar.sNama) & ",  " _
        & SetSetring(param_activity) & ",  " _
        & SetSetring(GetDetailUser) & "  " _
        & ")"
        Return sSQL
    End Function


    Shared progressForm As FProgress = Nothing
    Public Shared Sub ExportToEx(ByVal _gv As GridView, ByVal filename As String, ByVal ext As String)
        StartExport()
        Dim currentCursor As Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor
        'Dim ps As DevExpress.XtraPrinting.IPrintingSystem = DevExpress.XtraPrinting.PrintHelper.GetCurrentPS()

        Dim printingSystem As New DevExpress.XtraPrinting.PrintingSystem()
        Dim link As New DevExpress.XtraPrinting.PrintableComponentLink(printingSystem)

        AddHandler printingSystem.AfterChange, AddressOf Export_ProgressEx
        'AddHandler ps.AfterChange, AddressOf Export_ProgressEx
        If ext = "rtf" Then
            _gv.ExportToRtf(filename)
        End If
        If ext = "pdf" Then
            _gv.ExportToPdf(filename)
        End If
        If ext = "mht" Then
            _gv.ExportToMht(filename)
        End If
        If ext = "htm" Then
            _gv.ExportToHtml(filename)
        End If
        If ext = "txt" Then
            _gv.ExportToText(filename)
        End If
        If ext = "xls" Then
            _gv.ExportToXls(filename)
        End If
        If ext = "xlsx" Then
            _gv.ExportToXlsx(filename)
        End If
        ' RemoveHandler ps.AfterChange, AddressOf Export_ProgressEx
        RemoveHandler printingSystem.AfterChange, AddressOf Export_ProgressEx
        Cursor.Current = currentCursor
        EndExport()
    End Sub
    Public Shared Sub StartExport()
        'If Me.MenuForm IsNot Nothing Then
        '    MenuForm.Update()
        'End If
        progressForm = New FProgress(Nothing)
        progressForm.Show()
        progressForm.Refresh()
    End Sub
    Public Shared Sub Export_ProgressEx(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.ChangeEventArgs)
        If e.EventName = DevExpress.XtraPrinting.SR.ProgressPositionChanged Then
            Dim pos As Integer = CInt(Fix(e.ValueOf(DevExpress.XtraPrinting.SR.ProgressPosition)))
            progressForm.SetProgressValue(pos)
        End If
    End Sub
    Public Shared Sub EndExport()
        progressForm.Dispose()
        progressForm = Nothing
    End Sub
    Public Shared Sub LoadRTF(ByVal fileName As String, ByVal rtb As RichTextBox)
        Dim rtfFile As String = fileName
        Try
            If rtfFile <> "" Then
                rtb.LoadFile(rtfFile)
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, ex.Source)
        End Try
    End Sub
    Public Shared Function SetSetringE(ByVal Str As Object) As String
        SetSetringE = ""
        If Len(Trim(Str)) > 0 Then
            SetSetringE = Replace((Str).ToString, "\", "\\")
        End If
    End Function



    Public Shared Function Upload(ByVal source As String, ByVal target As String, _
                                 ByVal credential As Net.NetworkCredential, ByVal par_err As ArrayList) As Boolean
        Try
            Dim request As Net.FtpWebRequest = _
             DirectCast(Net.WebRequest.Create(target), Net.FtpWebRequest)
            request.Method = Net.WebRequestMethods.Ftp.UploadFile
            request.Credentials = credential
            request.UsePassive = True

            Dim reader As New FileStream(source, FileMode.Open)
            Dim buffer(System.Convert.ToInt32(reader.Length - 1)) As Byte
            reader.Read(buffer, 0, buffer.Length)
            reader.Close()
            request.ContentLength = buffer.Length
            Dim stream As Stream = request.GetRequestStream
            stream.Write(buffer, 0, buffer.Length)
            stream.Close()
            Dim response As Net.FtpWebResponse = DirectCast(request.GetResponse, Net.FtpWebResponse)
            'MessageBox.Show(response.StatusDescription, "File Uploaded")
            response.Close()
            Return True

            'Try
            '    Dim filename As String = ftpURI & "PING.bmp"
            '    Dim client As New WebClient
            '    client.Credentials = New NetworkCredential("anonymous", "password")
            '    client.UploadFile(filename, "C:\PING.bmp")
            '    MsgBox("File uploaded!")
            'Catch ex As Exception
            '    MsgBox(ex.ToString)
            'End Try
        Catch ex As Exception
            par_err.Add(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Sub download(ByVal source As String, ByVal target As String, ByVal credential As Net.NetworkCredential)
        '' Get the object used to communicate with the server.
        ''Dim request As FtpWebRequest = DirectCast(WebRequest.Create("ftp://www.contoso.com/test.htm"), FtpWebRequest)
        'Dim request As FtpWebRequest = DirectCast(WebRequest.Create(source), FtpWebRequest)
        'request.Method = WebRequestMethods.Ftp.DownloadFile

        '' This example assumes the FTP site uses anonymous logon.
        'request.Credentials = credential

        'Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)

        'Dim responseStream As Stream = response.GetResponseStream()
        'Dim reader As New StreamReader(responseStream)
        'Console.WriteLine(reader.ReadToEnd())

        'Console.WriteLine("Download Complete, status {0}", response.StatusDescription)

        'reader.Close()
        'response.Close()
        Try
            Dim filename As String = source
            Dim client As New WebClient
            client.Credentials = credential
            My.Computer.FileSystem.WriteAllBytes(target, client.DownloadData(filename), False)

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub
    Public Shared Function download(ByVal source As String, ByVal target As String, ByVal credential As Net.NetworkCredential, ByVal _err As ArrayList) As Boolean
        Try
            Dim filename As String = source
            Dim client As New WebClient
            client.Credentials = credential
            My.Computer.FileSystem.WriteAllBytes(target, client.DownloadData(filename), False)
            Return True
        Catch ex As Exception
            _err.Add(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function ftp_delete(ByVal par_url As String, ByVal credential As Net.NetworkCredential, ByVal par_err As ArrayList) As Boolean

        Try
            'give simple file name a fully qualified locatin (based on my ftpURI contstant)
            Dim ftpfilename As String = par_url

            'create a FTPWebRequest object
            Dim ftpReq As FtpWebRequest = WebRequest.Create(ftpfilename)

            ftpReq.Credentials = credential
            'set the method to delete the file
            ftpReq.Method = WebRequestMethods.Ftp.DeleteFile

            'delete the file
            Dim ftpResp As FtpWebResponse = ftpReq.GetResponse


            Return True
        Catch ex As Exception
            par_err.Add(ex.Message)
            Return False
        End Try
    End Function

End Class