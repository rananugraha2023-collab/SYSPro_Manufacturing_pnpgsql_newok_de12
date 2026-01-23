Imports master_new.ModFunction
Imports npgsql

Public Class FViewDepresiasi
    Dim ds_depr As New DataSet
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Dim func_data As New function_data

    Public _famt_oid As String
    Public _famt_method As String
    Public _exp_life As Integer
    Public _cost, _salv_cost As Double
    Public _year, _month As Integer
    Public _fabk_code, _famt_code As String

    Public _part_number, _serial As String
    '================================
    Public _assbk_oid As String
    '================================
    Public _start_date As Date

    Private Sub FViewDepresiasi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        v_part_number.Text = _part_number
        v_serial.Text = _serial
        v_asbk.Text = _fabk_code
        v_asmt.Text = _famt_code
        sc_txt_cost.Text = SetDbl(_cost)
        sc_tct_salv_cost.Text = SetDbl(_salv_cost)

        GetDepresiasi()
        view_depresiasi()
        gv_master.Focus()
        delete()

    End Sub

    Public Sub GetDepresiasi()
        ds_depr = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  v_part_number, " _
                            & "  v_serial, " _
                            & "  v_asbk, " _
                            & "  v_asmt, " _
                            & "  v_year, " _
                            & "  v_month, " _
                            & "  v_periode, " _
                            & "  v_depr, " _
                            & "  v_sisa " _
                            & "FROM  " _
                            & "  public.v_depr " _
                            & "  order by v_year,v_month asc "
                    .InitializeCommand()
                    .FillDataSet(ds_depr, "depr")
                    gc_master.DataSource = ds_depr.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub view_depresiasi()
        If _famt_method = "T" Then
            view_custom_table()
        ElseIf _famt_method = "S" Then
            view_stright_line()
        ElseIf _famt_method = "D" Then
            view_double_declining()
        ElseIf _famt_method = "B" Then
            view_double_declining2()
        End If
    End Sub

    Public Sub view_custom_table()
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  famtd_oid, " _
                            & "  famtd_famt_oid, " _
                            & "  famtd_add_by, " _
                            & "  famtd_add_date, " _
                            & "  famtd_upd_by, " _
                            & "  famtd_upd_date, " _
                            & "  famtd_year, " _
                            & "  famtd_per_1, " _
                            & "  famtd_per_2, " _
                            & "  famtd_per_3, " _
                            & "  famtd_per_4, " _
                            & "  famtd_per_5, " _
                            & "  famtd_per_6, " _
                            & "  famtd_per_7, " _
                            & "  famtd_per_8, " _
                            & "  famtd_per_9, " _
                            & "  famtd_per_10, " _
                            & "  famtd_per_11, " _
                            & "  famtd_per_12, " _
                            & "  famtd_dt " _
                            & "FROM  " _
                            & "  public.famtd_det " _
                            & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
                            & "  where famt_oid = " + SetSetring(_famt_oid)
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "req")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ds_depr.Tables(0).Clear()

        Dim _sisa, _cost_after_salv As Double
        _cost_after_salv = _cost - _salv_cost
        _sisa = _cost_after_salv


        Dim a As Integer
        Dim _start_month As Integer

        If _month = 1 Then
            _start_month = 1
        ElseIf _month = 2 Then
            _start_month = 2
        ElseIf _month = 3 Then
            _start_month = 3
        ElseIf _month = 4 Then
            _start_month = 4
        ElseIf _month = 5 Then
            _start_month = 5
        ElseIf _month = 6 Then
            _start_month = 6
        ElseIf _month = 7 Then
            _start_month = 7
        ElseIf _month = 8 Then
            _start_month = 8
        ElseIf _month = 9 Then
            _start_month = 9
        ElseIf _month = 10 Then
            _start_month = 10
        ElseIf _month = 11 Then
            _start_month = 11
        ElseIf _month = 12 Then
            _start_month = 12
        End If

        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        _end_date_life = _start_date.AddYears(_exp_life)

        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            Dim _per_1, _per_2, _per_3, _per_4, _per_5, _per_6, _per_7, _per_8, _per_9, _per_10, _per_11, _per_12 As Double
            _per_1 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_1")
            _per_2 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_2")
            _per_3 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_3")
            _per_4 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_4")
            _per_5 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_5")
            _per_6 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_6")
            _per_7 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_7")
            _per_8 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_8")
            _per_9 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_9")
            _per_10 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_10")
            _per_11 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_11")
            _per_12 = ds_bantu.Tables(0).Rows(i).Item("famtd_per_12")

            Dim _periode As Integer
            _periode = 0

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = _exp_life Then
                _end_month_life = Month(_end_date_life) - 1
            Else
                _end_month_life = 12
            End If

            For a = _start_month To _end_month_life
                _dtrow = ds_depr.Tables(0).NewRow
                _dtrow("v_part_number") = _part_number
                _dtrow("v_serial") = _serial
                _dtrow("v_asbk") = _fabk_code
                _dtrow("v_year") = _year
                _dtrow("v_month") = MonthName(a).ToString()
                _dtrow("v_periode") = _periode + 1
                _periode = _periode + 1
                If a = 1 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_1)
                    _sisa = _sisa - (_cost_after_salv * _per_1)
                ElseIf a = 2 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_2)
                    _sisa = _sisa - (_cost_after_salv * _per_2)
                ElseIf a = 3 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_3)
                    _sisa = _sisa - (_cost_after_salv * _per_3)
                ElseIf a = 4 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_4)
                    _sisa = _sisa - (_cost_after_salv * _per_4)
                ElseIf a = 5 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_5)
                    _sisa = _sisa - (_cost_after_salv * _per_5)
                ElseIf a = 6 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_6)
                    _sisa = _sisa - (_cost_after_salv * _per_6)
                ElseIf a = 7 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_7)
                    _sisa = _sisa - (_cost_after_salv * _per_7)
                ElseIf a = 8 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_8)
                    _sisa = _sisa - (_cost_after_salv * _per_8)
                ElseIf a = 9 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_9)
                    _sisa = _sisa - (_cost_after_salv * _per_9)
                ElseIf a = 10 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_10)
                    _sisa = _sisa - (_cost_after_salv * _per_10)
                ElseIf a = 11 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_11)
                    _sisa = _sisa - (_cost_after_salv * _per_11)
                ElseIf a = 12 Then
                    _dtrow("v_depr") = _cost_after_salv * (_per_12)
                    _sisa = _sisa - (_cost_after_salv * _per_12)
                End If

                If _sisa <= 0 Then
                    _dtrow("v_sisa") = 0
                    _dtrow("v_depr") = _sisa + _dtrow("v_depr")  '(_sisa + _dtrow("v_depr")) - _salv_cost
                Else
                    _dtrow("v_sisa") = _sisa
                End If

                ds_depr.Tables(0).Rows.Add(_dtrow)

                If _sisa <= 0 Then
                    Exit Sub
                End If
            Next

            _year = _year + 1

        Next
        ds_depr.Tables(0).AcceptChanges()
        gv_master.BestFitColumns()
    End Sub

    Public Sub view_double_declining()
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  famtd_oid, " _
                            & "  famtd_famt_oid, " _
                            & "  famtd_add_by, " _
                            & "  famtd_add_date, " _
                            & "  famtd_upd_by, " _
                            & "  famtd_upd_date, " _
                            & "  famtd_year, " _
                            & "  famtd_per_1, " _
                            & "  famtd_per_2, " _
                            & "  famtd_per_3, " _
                            & "  famtd_per_4, " _
                            & "  famtd_per_5, " _
                            & "  famtd_per_6, " _
                            & "  famtd_per_7, " _
                            & "  famtd_per_8, " _
                            & "  famtd_per_9, " _
                            & "  famtd_per_10, " _
                            & "  famtd_per_11, " _
                            & "  famtd_per_12, " _
                            & "  famtd_dt " _
                            & "FROM  " _
                            & "  public.famtd_det " _
                            & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
                            & "  where famt_oid = " + SetSetring(_famt_oid)
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "req")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ds_depr.Tables(0).Clear()

        Dim _sisa, _cost_after_salv As Double
        _cost_after_salv = _cost - _salv_cost
        _sisa = _cost_after_salv


        Dim a As Integer
        Dim _start_month As Integer

        If _month = 1 Then
            _start_month = 1
        ElseIf _month = 2 Then
            _start_month = 2
        ElseIf _month = 3 Then
            _start_month = 3
        ElseIf _month = 4 Then
            _start_month = 4
        ElseIf _month = 5 Then
            _start_month = 5
        ElseIf _month = 6 Then
            _start_month = 6
        ElseIf _month = 7 Then
            _start_month = 7
        ElseIf _month = 8 Then
            _start_month = 8
        ElseIf _month = 9 Then
            _start_month = 9
        ElseIf _month = 10 Then
            _start_month = 10
        ElseIf _month = 11 Then
            _start_month = 11
        ElseIf _month = 12 Then
            _start_month = 12
        End If

        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        Dim _depr_per_month As Double
        _end_date_life = _start_date.AddYears(_exp_life)

        Dim _dtrow As DataRow
        For i = 0 To _exp_life
            _depr_per_month = (_sisa * 0.5) / 12

            Dim _periode As Integer
            _periode = 0

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = _exp_life Then
                _end_month_life = Month(_end_date_life) - 1
                _depr_per_month = _sisa / _end_month_life
            Else
                _end_month_life = 12
            End If

            For a = _start_month To _end_month_life
                _dtrow = ds_depr.Tables(0).NewRow
                _dtrow("v_part_number") = _part_number
                _dtrow("v_serial") = _serial
                _dtrow("v_asbk") = _fabk_code
                _dtrow("v_year") = _year
                _dtrow("v_month") = MonthName(a).ToString()
                _dtrow("v_periode") = _periode + 1
                _periode = _periode + 1
                If a = 1 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 2 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 3 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 4 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 5 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 6 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 7 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 8 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 9 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 10 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 11 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 12 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                End If

                If _sisa <= 0 Then
                    _dtrow("v_sisa") = 0
                    _dtrow("v_depr") = _sisa + _dtrow("v_depr")
                Else
                    _dtrow("v_sisa") = _sisa
                End If

                ds_depr.Tables(0).Rows.Add(_dtrow)

                If _sisa <= 0 Then
                    Exit Sub
                End If
            Next

            _year = _year + 1

        Next
        ds_depr.Tables(0).AcceptChanges()
        gv_master.BestFitColumns()
    End Sub

    Public Sub view_double_declining2()
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  famtd_oid, " _
                            & "  famtd_famt_oid, " _
                            & "  famtd_add_by, " _
                            & "  famtd_add_date, " _
                            & "  famtd_upd_by, " _
                            & "  famtd_upd_date, " _
                            & "  famtd_year, " _
                            & "  famtd_per_1, " _
                            & "  famtd_per_2, " _
                            & "  famtd_per_3, " _
                            & "  famtd_per_4, " _
                            & "  famtd_per_5, " _
                            & "  famtd_per_6, " _
                            & "  famtd_per_7, " _
                            & "  famtd_per_8, " _
                            & "  famtd_per_9, " _
                            & "  famtd_per_10, " _
                            & "  famtd_per_11, " _
                            & "  famtd_per_12, " _
                            & "  famtd_dt " _
                            & "FROM  " _
                            & "  public.famtd_det " _
                            & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
                            & "  where famt_oid = " + SetSetring(_famt_oid)
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "req")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ds_depr.Tables(0).Clear()

        Dim _sisa, _cost_after_salv As Double
        _cost_after_salv = _cost - _salv_cost
        _sisa = _cost_after_salv


        Dim a As Integer
        Dim _start_month As Integer

        If _month = 1 Then
            _start_month = 1
        ElseIf _month = 2 Then
            _start_month = 2
        ElseIf _month = 3 Then
            _start_month = 3
        ElseIf _month = 4 Then
            _start_month = 4
        ElseIf _month = 5 Then
            _start_month = 5
        ElseIf _month = 6 Then
            _start_month = 6
        ElseIf _month = 7 Then
            _start_month = 7
        ElseIf _month = 8 Then
            _start_month = 8
        ElseIf _month = 9 Then
            _start_month = 9
        ElseIf _month = 10 Then
            _start_month = 10
        ElseIf _month = 11 Then
            _start_month = 11
        ElseIf _month = 12 Then
            _start_month = 12
        End If

        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        Dim _depr_per_month As Double
        _end_date_life = _start_date.AddYears(_exp_life)

        Dim _dtrow As DataRow
        Dim _tarif As Double = 0
        _tarif = ((100 / 100) / _exp_life) * 2

        For i = 0 To _exp_life
            '_depr_per_month = (_sisa * _tarif) / 12

            Dim _periode As Integer
            _periode = 0

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = _exp_life Then
                _end_month_life = Month(_end_date_life) - 1
                '_depr_per_month = _sisa / _end_month_life
            Else
                _end_month_life = 12
            End If

            For a = _start_month To _end_month_life
                _depr_per_month = (_sisa * _tarif) / 12

                _dtrow = ds_depr.Tables(0).NewRow
                _dtrow("v_part_number") = _part_number
                _dtrow("v_serial") = _serial
                _dtrow("v_asbk") = _fabk_code
                _dtrow("v_year") = _year
                _dtrow("v_month") = MonthName(a).ToString()
                _dtrow("v_periode") = _periode + 1
                _periode = _periode + 1
                If a = 1 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 2 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 3 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 4 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 5 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 6 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 7 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 8 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 9 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 10 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 11 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 12 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                End If

                If _sisa <= 0 Then
                    _dtrow("v_sisa") = 0
                    _dtrow("v_depr") = _sisa + _dtrow("v_depr")
                Else
                    _dtrow("v_sisa") = _sisa
                End If

                ds_depr.Tables(0).Rows.Add(_dtrow)

                If _sisa <= 0 Then
                    Exit Sub
                End If
            Next

            _year = _year + 1

        Next
        ds_depr.Tables(0).AcceptChanges()
        gv_master.BestFitColumns()
    End Sub

    Public Sub view_stright_line()
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  famtd_oid, " _
                            & "  famtd_famt_oid, " _
                            & "  famtd_add_by, " _
                            & "  famtd_add_date, " _
                            & "  famtd_upd_by, " _
                            & "  famtd_upd_date, " _
                            & "  famtd_year, " _
                            & "  famtd_per_1, " _
                            & "  famtd_per_2, " _
                            & "  famtd_per_3, " _
                            & "  famtd_per_4, " _
                            & "  famtd_per_5, " _
                            & "  famtd_per_6, " _
                            & "  famtd_per_7, " _
                            & "  famtd_per_8, " _
                            & "  famtd_per_9, " _
                            & "  famtd_per_10, " _
                            & "  famtd_per_11, " _
                            & "  famtd_per_12, " _
                            & "  famtd_dt " _
                            & "FROM  " _
                            & "  public.famtd_det " _
                            & "  inner join famt_mstr on famt_oid = famtd_famt_oid " _
                            & "  where famt_oid = " + SetSetring(_famt_oid)
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "req")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ds_depr.Tables(0).Clear()

        Dim _sisa, _cost_after_salv As Double
        _cost_after_salv = _cost - _salv_cost
        _sisa = _cost_after_salv


        Dim a As Integer
        Dim _start_month As Integer

        If _month = 1 Then
            _start_month = 1
        ElseIf _month = 2 Then
            _start_month = 2
        ElseIf _month = 3 Then
            _start_month = 3
        ElseIf _month = 4 Then
            _start_month = 4
        ElseIf _month = 5 Then
            _start_month = 5
        ElseIf _month = 6 Then
            _start_month = 6
        ElseIf _month = 7 Then
            _start_month = 7
        ElseIf _month = 8 Then
            _start_month = 8
        ElseIf _month = 9 Then
            _start_month = 9
        ElseIf _month = 10 Then
            _start_month = 10
        ElseIf _month = 11 Then
            _start_month = 11
        ElseIf _month = 12 Then
            _start_month = 12
        End If

        Dim _end_date_life As Date
        Dim _end_month_life As Integer
        Dim _depr_per_month As Double
        _end_date_life = _start_date.AddYears(_exp_life)
        _depr_per_month = _cost_after_salv / (_exp_life * 12)


        Dim _dtrow As DataRow
        For i = 0 To _exp_life
            Dim _periode As Integer
            _periode = 0

            If i = 0 Then
                _start_month = _start_month
            Else
                _start_month = 1
            End If

            If i = _exp_life Then
                _end_month_life = Month(_end_date_life) - 1
            Else
                _end_month_life = 12
            End If

            For a = _start_month To _end_month_life
                _dtrow = ds_depr.Tables(0).NewRow
                _dtrow("v_part_number") = _part_number
                _dtrow("v_serial") = _serial
                _dtrow("v_asbk") = _fabk_code
                _dtrow("v_year") = _year
                _dtrow("v_month") = MonthName(a).ToString()
                _dtrow("v_periode") = _periode + 1
                _periode = _periode + 1
                If a = 1 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 2 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 3 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 4 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 5 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 6 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 7 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 8 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 9 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 10 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 11 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                ElseIf a = 12 Then
                    _dtrow("v_depr") = _depr_per_month
                    _sisa = _sisa - (_depr_per_month)
                End If

                If _sisa <= 0 Then
                    _dtrow("v_sisa") = 0
                    _dtrow("v_depr") = _sisa + _dtrow("v_depr")
                Else
                    _dtrow("v_sisa") = _sisa
                End If

                ds_depr.Tables(0).Rows.Add(_dtrow)

                If _sisa <= 0 Then
                    Exit Sub
                End If
            Next

            _year = _year + 1

        Next
        ds_depr.Tables(0).AcceptChanges()
        gv_master.BestFitColumns()
    End Sub

    Public Sub delete()
        Dim _line_del As Integer
        For Each _dr As DataRow In ds_depr.Tables(0).Rows
            If _dr("v_depr") = 0 Then
                _line_del = _line_del + 1
            End If
        Next

        Dim i As Integer
        i = 0
        For i = 0 To (ds_depr.Tables(0).Rows.Count - 1) - _line_del
            If ds_depr.Tables(0).Rows(i).Item("v_depr") = 0 Then
                ds_depr.Tables(0).Rows(i).Delete()
                ds_depr.Tables(0).AcceptChanges()
                i = i - 1
            End If
        Next
    End Sub

End Class
