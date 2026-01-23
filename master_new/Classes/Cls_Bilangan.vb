Public Class Cls_Bilangan
    Public Function nominal_satu(ByVal param As String) As String
        nominal_satu = ""

        If param = 1 Then
            nominal_satu = "Satu"
        ElseIf param = 2 Then
            nominal_satu = "Dua"
        ElseIf param = 3 Then
            nominal_satu = "Tiga"
        ElseIf param = 4 Then
            nominal_satu = "Empat"
        ElseIf param = 5 Then
            nominal_satu = "Lima"
        ElseIf param = 6 Then
            nominal_satu = "Enam"
        ElseIf param = 7 Then
            nominal_satu = "Tujuh"
        ElseIf param = 8 Then
            nominal_satu = "Delapan"
        ElseIf param = 9 Then
            nominal_satu = "Sembilan"
        End If

        Return nominal_satu
    End Function

    Public Function nominal_dua(ByVal param As String) As String
        nominal_dua = ""

        If param = 10 Then
            nominal_dua = "Sepuluh"
        ElseIf param = 11 Then
            nominal_dua = "Sebelas"
        ElseIf param < 20 Then
            nominal_dua = nominal_satu(param) + " Belas"
        ElseIf param < 100 Then
            nominal_dua = nominal_satu(param.Substring(0, 1)) + " Puluh " + nominal_satu(param.Substring(1, 1))
        End If

        Return nominal_dua
    End Function

    Public Function nominal_tiga(ByVal param As String) As String
        Dim param_int As Integer

        If param < 200 Then
            nominal_tiga = "Seratus "

            param_int = CDbl(param.Substring(1, 2))
            param = param_int

            If Len(param) = 1 Then
                nominal_tiga = nominal_tiga + nominal_satu(param)
            Else
                nominal_tiga = nominal_tiga + nominal_dua(param)
            End If
        Else
            nominal_tiga = nominal_satu(param.Substring(0, 1)) + " Ratus "

            param_int = CDbl(param.Substring(1, 2))
            param = param_int

            If Len(param) = 1 Then
                nominal_tiga = nominal_tiga + nominal_satu(param)
            Else
                nominal_tiga = nominal_tiga + nominal_dua(param)
            End If
        End If

        Return nominal_tiga
    End Function

    Public Function nominal_empat(ByVal param As String) As String
        Dim param_int As Integer

        If param < 2000 Then
            nominal_empat = "Seribu "

            param_int = CDbl(param.Substring(1, 3))
            param = param_int

            If Len(param) = 1 Then
                nominal_empat = nominal_empat + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_empat = nominal_empat + nominal_dua(param)
            Else
                nominal_empat = nominal_empat + nominal_tiga(param)
            End If
        Else
            nominal_empat = nominal_satu(param.Substring(0, 1)) + " Ribu "

            param_int = CDbl(param.Substring(1, 3))
            param = param_int

            If Len(param) = 1 Then
                nominal_empat = nominal_empat + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_empat = nominal_empat + nominal_dua(param)
            Else
                nominal_empat = nominal_empat + nominal_tiga(param)
            End If
        End If

        Return nominal_empat
    End Function

    Public Function nominal_lima(ByVal param As String) As String
        Dim param_int As Integer

        If param < 20000 Then
            nominal_lima = nominal_dua(param.Substring(0, 2)) + " Ribu "

            param_int = CDbl(param.Substring(2, 3))
            param = param_int

            If Len(param) = 1 Then
                nominal_lima = nominal_lima + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_lima = nominal_lima + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_lima = nominal_lima + nominal_tiga(param)
            Else
                nominal_lima = nominal_lima + nominal_tiga(param)
            End If
        Else
            nominal_lima = nominal_dua(param.Substring(0, 2)) + " Ribu "

            param_int = CDbl(param.Substring(2, 3))
            param = param_int

            If Len(param) = 1 Then
                nominal_lima = nominal_lima + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_lima = nominal_lima + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_lima = nominal_lima + nominal_tiga(param)
            Else
                nominal_lima = nominal_lima + nominal_tiga(param)
            End If
        End If

        Return nominal_lima
    End Function

    Public Function nominal_enam(ByVal param As String) As String
        Dim param_int As Integer

        If param < 200000 Then
            nominal_enam = nominal_tiga(param.Substring(0, 3)) + " Ribu "

            param_int = CDbl(param.Substring(3, 3))
            param = param_int

            If Len(param) = 1 Then
                nominal_enam = nominal_enam + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_enam = nominal_enam + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_enam = nominal_enam + nominal_tiga(param)
            Else
                nominal_enam = nominal_enam + nominal_tiga(param)
            End If
        Else
            nominal_enam = nominal_tiga(param.Substring(0, 3)) + " Ribu "

            param_int = CDbl(param.Substring(3, 3))
            param = param_int

            If Len(param) = 1 Then
                nominal_enam = nominal_enam + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_enam = nominal_enam + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_enam = nominal_enam + nominal_tiga(param)
            Else
                nominal_enam = nominal_enam + nominal_tiga(param)
            End If
        End If

        Return nominal_enam
    End Function

    Public Function nominal_tujuh(ByVal param As String) As String
        Dim param_int As Integer

        If param < 2000000 Then
            nominal_tujuh = nominal_satu(param.Substring(0, 1)) + " Juta "

            param_int = CDbl(param.Substring(1, 6))
            param = param_int

            If Len(param) = 1 Then
                nominal_tujuh = nominal_tujuh + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_tujuh = nominal_tujuh + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_tujuh = nominal_tujuh + nominal_tiga(param)
            ElseIf Len(param) = 4 Then
                nominal_tujuh = nominal_tujuh + nominal_empat(param)
            ElseIf Len(param) = 5 Then
                nominal_tujuh = nominal_tujuh + nominal_lima(param)
            ElseIf Len(param) = 6 Then
                nominal_tujuh = nominal_tujuh + nominal_enam(param)
            Else
                nominal_tujuh = nominal_tujuh + nominal_tiga(param)
            End If
        Else
            nominal_tujuh = nominal_satu(param.Substring(0, 1)) + " Juta "

            param_int = CDbl(param.Substring(1, 6))
            param = param_int

            If Len(param) = 1 Then
                nominal_tujuh = nominal_tujuh + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_tujuh = nominal_tujuh + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_tujuh = nominal_tujuh + nominal_tiga(param)
            ElseIf Len(param) = 4 Then
                nominal_tujuh = nominal_tujuh + nominal_empat(param)
            ElseIf Len(param) = 5 Then
                nominal_tujuh = nominal_tujuh + nominal_lima(param)
            ElseIf Len(param) = 6 Then
                nominal_tujuh = nominal_tujuh + nominal_enam(param)
            Else
                nominal_tujuh = nominal_tujuh + nominal_tiga(param)
            End If
        End If

        Return nominal_tujuh
    End Function

    Public Function nominal_delapan(ByVal param As String) As String
        Dim param_int As Integer

        If param < 20000000 Then
            nominal_delapan = nominal_dua(param.Substring(0, 2)) + " Juta "

            param_int = CDbl(param.Substring(2, 6))
            param = param_int

            If Len(param) = 1 Then
                nominal_delapan = nominal_delapan + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_delapan = nominal_delapan + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_delapan = nominal_delapan + nominal_tiga(param)
            ElseIf Len(param) = 4 Then
                nominal_delapan = nominal_delapan + nominal_empat(param)
            ElseIf Len(param) = 5 Then
                nominal_delapan = nominal_delapan + nominal_lima(param)
            ElseIf Len(param) = 6 Then
                nominal_delapan = nominal_delapan + nominal_enam(param)
            Else
                nominal_delapan = nominal_delapan + nominal_tiga(param)
            End If
        Else
            nominal_delapan = nominal_dua(param.Substring(0, 2)) + " Juta "

            param_int = CDbl(param.Substring(2, 6))
            param = param_int

            If Len(param) = 1 Then
                nominal_delapan = nominal_delapan + nominal_satu(param)
            ElseIf Len(param) = 2 Then
                nominal_delapan = nominal_delapan + nominal_dua(param)
            ElseIf Len(param) = 3 Then
                nominal_delapan = nominal_delapan + nominal_tiga(param)
            ElseIf Len(param) = 4 Then
                nominal_delapan = nominal_delapan + nominal_empat(param)
            ElseIf Len(param) = 5 Then
                nominal_delapan = nominal_delapan + nominal_lima(param)
            ElseIf Len(param) = 6 Then
                nominal_delapan = nominal_delapan + nominal_enam(param)
            Else
                nominal_delapan = nominal_delapan + nominal_tiga(param)
            End If
        End If

        Return nominal_delapan
    End Function

    Public Function nominal_sembilan(ByVal param As String) As String
        Dim param_int As Integer

        nominal_sembilan = nominal_tiga(param.Substring(0, 3)) + " Juta "

        param_int = CDbl(param.Substring(3, 6))
        param = param_int

        If Len(param) = 1 Then
            nominal_sembilan = nominal_sembilan + nominal_satu(param)
        ElseIf Len(param) = 2 Then
            nominal_sembilan = nominal_sembilan + nominal_dua(param)
        ElseIf Len(param) = 3 Then
            nominal_sembilan = nominal_sembilan + nominal_tiga(param)
        ElseIf Len(param) = 4 Then
            nominal_sembilan = nominal_sembilan + nominal_empat(param)
        ElseIf Len(param) = 5 Then
            nominal_sembilan = nominal_sembilan + nominal_lima(param)
        ElseIf Len(param) = 6 Then
            nominal_sembilan = nominal_sembilan + nominal_enam(param)
        Else
            nominal_sembilan = nominal_sembilan + nominal_tiga(param)
        End If

        Return nominal_sembilan
    End Function

    Public Function nominal(ByVal total_gaji As String) As String
        nominal = ""

        If Len(total_gaji) = 1 Then
            nominal = nominal_satu(total_gaji)
        ElseIf Len(total_gaji) = 2 Then
            nominal = nominal_dua(total_gaji)
        ElseIf Len(total_gaji) = 3 Then
            nominal = nominal_tiga(total_gaji)
        ElseIf Len(total_gaji) = 4 Then
            nominal = nominal_empat(total_gaji)
        ElseIf Len(total_gaji) = 5 Then
            nominal = nominal_lima(total_gaji)
        ElseIf Len(total_gaji) = 6 Then
            nominal = nominal_enam(total_gaji)
        ElseIf Len(total_gaji) = 7 Then
            nominal = nominal_tujuh(total_gaji)
        ElseIf Len(total_gaji) = 8 Then
            nominal = nominal_delapan(total_gaji)
        ElseIf Len(total_gaji) = 9 Then
            nominal = nominal_sembilan(total_gaji)
        End If

        Return nominal ' + " Rupiah"
    End Function
End Class
