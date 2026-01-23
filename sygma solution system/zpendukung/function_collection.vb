Imports System.Net.Mail
Imports master_new.ModFunction
Imports npgsql

Public Class function_collection

    Dim func_data As New function_data
    'Dim func_coll As New function_collection


#Region "SetID"
    Public Function GetID(ByVal par_table As String, ByVal par_colom As String) As Integer
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
                        GetID = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetID
    End Function

    Public Function GetID(ByVal par_table As String, ByVal par_colom As String, ByVal par_colom_criteria As String, ByVal criteria As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(cast(substring(cast(" + par_colom + " as varchar),3,100) as integer)),0) as max_col  from " + par_table + _
                                           " where " + par_colom_criteria + " = '" + criteria + "'" + _
                                           " and substring(cast(" + par_colom + " as varchar),3,100) <> ''"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetID = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetID
    End Function

    'Public Function GetID(ByVal par_table As String, ByVal par_en_code As String, ByVal par_colom As String, ByVal par_colom_criteria As String, ByVal criteria As String) As Integer
    '    Try
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select coalesce(max(cast(substring(cast(" + par_colom + " as varchar),3,100) as integer)),0) as max_col  from " + par_table + _
    '                                       " where " + par_colom_criteria + " = '" + criteria + "'" + _
    '                                       " and substring(cast(" + par_colom + " as varchar),3,100) <> ''"

    '                .InitializeCommand()

    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read

    '                    GetID = .DataReader("max_col") + 1
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    If par_en_code = "0" Then
    '        par_en_code = "99"
    '    End If

    '    GetID = CInt(par_en_code + GetID.ToString)

    '    Return GetID
    'End Function

    Public Function GetID(ByVal par_table As String, ByVal par_en_code As String, ByVal par_colom As String, ByVal par_colom_criteria As String, ByVal criteria As String) As Long
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    Dim _len As Integer = par_en_code.Length + master_new.ClsVar.sServerCode.Length + 1

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(" + par_colom + " as varchar)," & _len.ToString & ",100) as integer)),0) as max_col  from " + par_table + _
                                           " where " + par_colom_criteria + " = '" + criteria + "'" + _
                                           " and substring(cast(" + par_colom + " as varchar)," & _len.ToString & ",100) <> ''"

                    .InitializeCommand()

                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetID = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        'GetID = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID.ToString)
        GetID = (par_en_code + master_new.ClsVar.sServerCode + GetID.ToString)

        Return GetID
    End Function

#End Region

#Region "Update Inventory"
    Public Function update_item_cost_begining_balance(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_pt_id As Integer, ByVal par_cost As Double) As Boolean
        update_item_cost_begining_balance = True
        'ini tidak dipake...karena untuk cycle count yang initial dia tidak update cost kecuali cycle count yang recount + atau recount -

        'With par_obj
        '    Try
        '        .Command.CommandType = CommandType.Text
        '        .Command.CommandText = "update pt_mstr set pt_cost = " + par_cost.ToString + _
        '                               " where pt_id = " + par_pt_id.ToString + _
        '                               " and pt_en_id = " + par_en_id.ToString
        '        par_ssqls.Add(.Command.CommandText)
        '        .Command.ExecuteNonQuery()
        '        .Command.Parameters.Clear()
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try
        'End With
    End Function

    Public Function get_status_wf(ByVal par_pd_code As String) As String
        get_status_wf = "-1"
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_wfs_id from wf_mstr  " + _
                                           " where wf_ref_code = '" + par_pd_code + "'" + _
                                           " and wf_seq = 0"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_status_wf = .DataReader.Item("wf_wfs_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_status_wf
    End Function

    Public Function get_cost_average(ByVal par_type As String, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_pt_id As Integer, ByVal par_qty As Double, ByVal par_cost As Double) As Double
        Dim _invc_qty_sum, _pt_cost, _avg_cost As Double

        'Mencari sum dari inventory dari item yang bersangkutan
        _invc_qty_sum = 0
        _pt_cost = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(sum(invc_qty),0) as invc_qty_sum From invc_mstr where invc_pt_id = " + par_pt_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_en_id in (SELECT   a.en_id FROM  public.en_mstr a WHERE  a.en_active = 'Y')"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _invc_qty_sum = .DataReader("invc_qty_sum").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'Mencari cost dari item bersangkutan
        _pt_cost = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(invct_cost,0) as invct_cost from invct_table where invct_pt_id = " + par_pt_id.ToString + _
                                           " and invct_si_id = " + par_si_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pt_cost = .DataReader("invct_cost").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        If _invc_qty_sum < 0 Then
            _invc_qty_sum = 0
        End If
        'rumus ((qty stock x cost average) + (po_cost * po_qty)) / (qty stock + po_qty)
        Dim par1, par2 As Double
        If par_type = "+" Then
            _avg_cost = ((_invc_qty_sum * _pt_cost) + (par_cost * par_qty)) / (_invc_qty_sum + par_qty)

            'If _pt_cost > 0 Then
            '    _avg_cost = (_pt_cost + par_cost) / 2
            'Else
            '    _avg_cost = par_cost
            'End If

        ElseIf par_type = "-" Then
            par1 = ((_invc_qty_sum * _pt_cost) - (par_cost * par_qty))
            par2 = (_invc_qty_sum - par_qty)
            If par1 = 0 And par2 = 0 Then
                _avg_cost = 0
            Else
                If par2 = 0 And par1 <> 0 Then
                    _avg_cost = par1
                Else
                    _avg_cost = par1 / par2
                End If
            End If
            _avg_cost = ((_invc_qty_sum * _pt_cost) - (par_cost * par_qty)) / (_invc_qty_sum - par_qty)
        End If

        If _avg_cost.ToString = "NaN" Then
            get_cost_average = 0
        Else
            get_cost_average = _avg_cost
        End If

        Return get_cost_average
    End Function

    Public Function update_item_cost_avg(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_si_id As Integer, ByVal par_pt_id As Integer, ByVal par_avg_cost As Double) As Boolean
        update_item_cost_avg = True

        Dim _jum As Integer = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT count(invct_pt_id) as jum FROM public.invct_table where invct_pt_id = " + par_pt_id.ToString + _
                                           " and invct_si_id = " + par_si_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _jum = .DataReader("jum").ToString
                    End While
                End With
            End Using
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


        If _jum = 0 Then
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invct_table " _
                                        & "( " _
                                        & "  invct_oid, " _
                                        & "  invct_dom_id, " _
                                        & "  invct_pt_id, " _
                                        & "  invct_cost, " _
                                        & "  invct_si_id " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_avg_cost) & ",  " _
                                        & SetInteger(par_si_id) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "update invct_table set invct_cost = " + SetDbl(par_avg_cost) + _
                                           " where invct_pt_id = " + SetInteger(par_pt_id) + _
                                           " and invct_si_id = " + SetInteger(par_si_id)
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If

    End Function

    Public Function update_invct_table_minus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_pt_id As Integer, ByVal par_qty As Double, _
                                        ByVal par_type As String) As Boolean
        update_invct_table_minus = True

        Dim ds_bantu As New DataSet
        Dim i As Integer
        Dim _qty As Double

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from invct_table" _
                         & " where invct_dom_id = " & master_new.ClsVar.sdom_id _
                         & " and invct_en_id = " + par_en_id.ToString _
                         & " and invct_pt_id =  " + par_pt_id.ToString
                    If par_type = "F" Then
                        .SQL = .SQL + " order by invct_date asc"
                    ElseIf par_type = "L" Then
                        .SQL = .SQL + " order by invct_date desc"
                    End If

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "invct_table")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        _qty = par_qty
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If _qty >= ds_bantu.Tables(0).Rows(i).Item("invct_qty") Then
                With par_obj
                    Try
                        _qty = _qty - ds_bantu.Tables(0).Rows(i).Item("invct_qty")
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from invct_table where invct_oid = '" + ds_bantu.Tables(0).Rows(i).Item("invct_oid").ToString + "'"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            ElseIf _qty < ds_bantu.Tables(0).Rows(i).Item("invct_qty") Then
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update invct_table " + _
                                               " set invct_qty = invct_qty - " + _qty.ToString + _
                                               " where invct_oid = '" + ds_bantu.Tables(0).Rows(i).Item("invct_oid").ToString + "'"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        Exit For
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        Next

        'With par_obj
        '    Try
        '        .Command.CommandType = CommandType.Text
        '        .Command.CommandText = "delete from invct_table where invct_qty = 0"
        '        par_ssqls.Add(.Command.CommandText)
        '        .Command.ExecuteNonQuery()
        '        .Command.Parameters.Clear()
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try
        'End With

    End Function

    Public Function update_invct_table_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_pt_id As Integer, ByVal par_qty As Double, _
                                        ByVal par_cost As Double) As Boolean
        update_invct_table_plus = True
        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.invct_table " _
                                    & "( " _
                                    & "  invct_oid, " _
                                    & "  invct_dom_id, " _
                                    & "  invct_pt_id, " _
                                    & "  invct_date, " _
                                    & "  invct_qty, " _
                                    & "  invct_cost, " _
                                    & "  invct_en_id " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(par_pt_id) & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetDbl(par_qty) & ",  " _
                                    & SetDbl(par_cost) & ",  " _
                                    & SetInteger(par_en_id) & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Public Function update_invc_mstr_adj(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean
        update_invc_mstr_adj = True
        Dim _invc_oid As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select invc_oid from invc_mstr " + _
                                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                           " and invc_en_id = " + par_en_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_loc_id = " + par_loc_id.ToString + _
                                           " and invc_pt_id = " + par_pt_id.ToString + _
                                           " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _invc_oid = .DataReader("invc_oid").ToString
                        End While
                    Else
                        _invc_oid = ""
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _invc_oid = "" Then
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invc_mstr " _
                                        & "( " _
                                        & "  invc_oid, " _
                                        & "  invc_dom_id, " _
                                        & "  invc_en_id, " _
                                        & "  invc_si_id, " _
                                        & "  invc_loc_id, " _
                                        & "  invc_pt_id, " _
                                        & "  invc_qty, " _
                                        & "  invc_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try

            End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & "  invc_qty = " & SetDbl(par_qty) _
                                        & "WHERE  " _
                                        & "  invc_oid = " & SetSetring(_invc_oid) & " "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function

    Public Function update_invc_mstr_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean

        'ini yg dipake umum

        update_invc_mstr_plus = True
        Dim _invc_oid As String = ""
        Dim sSQL As String
        Try
            sSQL = "select invc_oid from invc_mstr " + _
                   " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                   " and invc_en_id = " + par_en_id.ToString + _
                   " and invc_si_id = " + par_si_id.ToString + _
                   " and invc_loc_id = " + par_loc_id.ToString + _
                   " and invc_pt_id = " + par_pt_id.ToString + _
                   " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            If dt.Rows.Count > 1 Then
                MessageBox.Show("Inventory " + master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", par_pt_id) + " duplicate in this location " & master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If dt.Rows.Count > 0 Then
                _invc_oid = dt.Rows(0).Item("invc_oid").ToString '.DataReader("invc_oid").ToString
            Else
                _invc_oid = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _invc_oid = "" Then
            With par_obj
                Try

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invc_mstr " _
                                        & "( " _
                                        & "  invc_oid, " _
                                        & "  invc_dom_id, " _
                                        & "  invc_en_id, " _
                                        & "  invc_si_id, " _
                                        & "  invc_loc_id, " _
                                        & "  invc_pt_id, " _
                                        & "  invc_qty, " _
                                        & "  invc_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try

            End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & " invc_qty_old=invc_qty, invc_qty = coalesce(invc_qty,0) + " & SetDbl(par_qty) _
                                        & " WHERE  " _
                                        & " invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                        " and invc_en_id = " + par_en_id.ToString + _
                                        " and invc_si_id = " + par_si_id.ToString + _
                                        " and invc_loc_id = " + par_loc_id.ToString + _
                                        " and invc_pt_id = " + par_pt_id.ToString + _
                                        " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function

    Public Function update_invc_mstr_plus_minus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean

        'ini yg dipake umum

        update_invc_mstr_plus_minus = True
        Dim _invc_oid As String = ""
        Dim sSQL As String
        Try
            sSQL = "select invc_oid from invc_mstr " + _
                   " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                   " and invc_en_id = " + par_en_id.ToString + _
                   " and invc_si_id = " + par_si_id.ToString + _
                   " and invc_loc_id = " + par_loc_id.ToString + _
                   " and invc_pt_id = " + par_pt_id.ToString + _
                   " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            If dt.Rows.Count > 1 Then
                MessageBox.Show("Inventory " + master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", par_pt_id) + " duplicate in this location " & master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If dt.Rows.Count > 0 Then
                _invc_oid = dt.Rows(0).Item("invc_oid").ToString '.DataReader("invc_oid").ToString
            Else
                _invc_oid = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _invc_oid = "" Then
            With par_obj
                Try

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invc_mstr " _
                                        & "( " _
                                        & "  invc_oid, " _
                                        & "  invc_dom_id, " _
                                        & "  invc_en_id, " _
                                        & "  invc_si_id, " _
                                        & "  invc_loc_id, " _
                                        & "  invc_pt_id, " _
                                        & "  invc_qty, " _
                                        & "  invc_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try

            End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & " invc_qty_old=invc_qty, invc_qty = coalesce(invc_qty,0) + " & SetDbl(par_qty) _
                                        & " WHERE  " _
                                        & " invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                        " and invc_en_id = " + par_en_id.ToString + _
                                        " and invc_si_id = " + par_si_id.ToString + _
                                        " and invc_loc_id = " + par_loc_id.ToString + _
                                        " and invc_pt_id = " + par_pt_id.ToString + _
                                        " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function
    'Public Function update_pspt_mstr_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean

    '    'ini yg dipake umum

    '    'update_pspt_mstr_plus = True
    '    Dim _invc_oid As String = ""
    '    Dim sSQL As String
    '    Try
    '        sSQL = "select invc_oid from invc_mstr " + _
    '               " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
    '               " and invc_en_id = " + par_en_id.ToString + _
    '               " and invc_si_id = " + par_si_id.ToString + _
    '               " and invc_loc_id = " + par_loc_id.ToString + _
    '               " and invc_pt_id = " + par_pt_id.ToString + _
    '               " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
    '        Dim dt As New DataTable
    '        dt = master_new.PGSqlConn.GetTableData(sSQL)

    '        If dt.Rows.Count > 1 Then
    '            MessageBox.Show("Inventory " + master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", par_pt_id) + " duplicate in this location " & master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Return False
    '        End If

    '        If dt.Rows.Count > 0 Then
    '            _invc_oid = dt.Rows(0).Item("invc_oid").ToString '.DataReader("invc_oid").ToString
    '        Else
    '            _invc_oid = ""
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try

    '    If _invc_oid = "" Then
    '        With par_obj
    '            Try

    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "INSERT INTO  " _
    '                                    & "  public.pspt_rpt " _
    '                                    & "( " _
    '                                    & "  pspt_oid, " _
    '                                    & "  pspt_en_id, " _
    '                                    & "  pspt_ptnr_id, " _
    '                                    & "  pspt_periode_code, " _
    '                                    & "  pspt_point, " _
    '                                    & "  pspt_per_ttl, " _
    '                                    & "  pspt_com_ammount_ttl, " _
    '                                    & "  pspt_per_com_ammount, " _
    '                                    & ")  " _
    '                                    & "VALUES ( " _
    '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                    & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
    '                                    & SetInteger(par_en_id) & ",  " _
    '                                    & SetInteger(par_ptnr_id) & ",  " _
    '                                    & SetInteger(par_loc_id) & ",  " _
    '                                    & SetInteger(par_pt_id) & ",  " _
    '                                    & SetDbl(par_qty) & ",  " _
    '                                    & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
    '                                    & ")"
    '                par_ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                .Command.Parameters.Clear()
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '                Return False
    '            End Try

    '        End With
    '    Else
    '        With par_obj
    '            Try
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.invc_mstr   " _
    '                                    & "SET  " _
    '                                    & " invc_qty_old=invc_qty, invc_qty = coalesce(invc_qty,0) + " & SetDbl(par_qty) _
    '                                    & "WHERE  " _
    '                                    & " invc_dom_id = " + master_new.ClsVar.sdom_id + _
    '                                    " and invc_en_id = " + par_en_id.ToString + _
    '                                    " and invc_si_id = " + par_si_id.ToString + _
    '                                    " and invc_loc_id = " + par_loc_id.ToString + _
    '                                    " and invc_pt_id = " + par_pt_id.ToString + _
    '                                    " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
    '                par_ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                .Command.Parameters.Clear()
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '                Return False
    '            End Try
    '        End With
    '    End If
    'End Function

    Public Function update_invc_mstr_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, _
                                          ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pjc_id As Integer, ByVal par_pt_id As Integer, _
                                          ByVal par_serial As String, ByVal par_qty As Double) As Boolean
        update_invc_mstr_plus = True
        Dim _invc_oid As String = ""
        ''plus
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select invc_oid from invc_mstr " + _
                                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                           " and invc_en_id = " + par_en_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_loc_id = " + par_loc_id.ToString + _
                                           " and invc_pt_id = " + par_pt_id.ToString + _
                                           " and invc_pjc_id = " + par_pjc_id.ToString + _
                                           " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _invc_oid = .DataReader("invc_oid").ToString
                        End While
                    Else
                        _invc_oid = ""
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _invc_oid = "" Then
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invc_mstr " _
                                        & "( " _
                                        & "  invc_oid, " _
                                        & "  invc_dom_id, " _
                                        & "  invc_en_id, " _
                                        & "  invc_si_id, " _
                                        & "  invc_loc_id, " _
                                        & "  invc_pjc_id, " _
                                        & "  invc_pt_id, " _
                                        & "  invc_qty, " _
                                        & "  invc_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pjc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try

            End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & "  invc_qty = coalesce(invc_qty,0) + " & SetDbl(par_qty) _
                                        & "WHERE  " _
                                        & "  invc_oid = " & SetSetring(_invc_oid) & " "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function

    Public Function cek_inventory_allocation(ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_pt_code As String, ByVal par_qty As Double, ByVal par_serial As String) As Boolean
        cek_inventory_allocation = True

        'Dim _invc_qty As Integer
        'Dim _invc_oid As String = ""

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select invc_oid, coalesce(public.invc_mstr.invc_qty,0) - coalesce(public.invc_mstr.invc_qty_alloc,0) as invc_qty from invc_mstr " + _
        '                                   " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
        '                                   " and invc_en_id = " + par_en_id.ToString + _
        '                                   " and invc_si_id = " + par_si_id.ToString + _
        '                                   " and invc_loc_id = " + par_loc_id.ToString + _
        '                                   " and invc_pt_id = " + par_pt_id.ToString + _
        '                                   " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _invc_oid = .DataReader("invc_oid").ToString
        '                _invc_qty = .DataReader("invc_qty").ToString
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        'If _invc_oid = "" Then
        '    MessageBox.Show("Inventory " + par_pt_code + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If _invc_qty < par_qty Then
        '    MessageBox.Show("Inventory " + par_pt_code + " Lower Than Qty Process...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If
    End Function

    'Public Function update_pwgend_det_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean

    '    'ini yg dipake umum

    '    'update_pwgend_det_plus = True
    '    Dim _invc_oid As String = ""
    '    Dim sSQL As String
    '    Try
    '        sSQL = "select pspt_oid from pspt_rpt" + _
    '               " and pspt_en_id = " + par_en_id.ToString + _
    '               " and pspt_ptnr_id = " + par_pt_id.ToString + _
    '               " and pspt_periode_code= " + par_pt_id.ToString + _
    '               " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
    '        Dim dt As New DataTable
    '        dt = master_new.PGSqlConn.GetTableData(sSQL)

    '        'If dt.Rows.Count > 1 Then
    '        '    MessageBox.Show("Inventory " + master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", par_pt_id) + " duplicate in this location " & master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '    Return False
    '        End If

    '        If dt.Rows.Count > 0 Then
    '            _invc_oid = dt.Rows(0).Item("invc_oid").ToString '.DataReader("invc_oid").ToString
    '        Else
    '            _invc_oid = ""
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try

    '    If _invc_oid = "" Then
    '        With par_obj
    '            Try

    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "INSERT INTO  " _
    '                                    & "  public.invc_mstr " _
    '                                    & "( " _
    '                                    & "  invc_oid, " _
    '                                    & "  invc_dom_id, " _
    '                                    & "  invc_en_id, " _
    '                                    & "  invc_si_id, " _
    '                                    & "  invc_loc_id, " _
    '                                    & "  invc_pt_id, " _
    '                                    & "  invc_qty, " _
    '                                    & "  invc_serial " _
    '                                    & ")  " _
    '                                    & "VALUES ( " _
    '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                    & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
    '                                    & SetInteger(par_en_id) & ",  " _
    '                                    & SetInteger(par_si_id) & ",  " _
    '                                    & SetInteger(par_loc_id) & ",  " _
    '                                    & SetInteger(par_pt_id) & ",  " _
    '                                    & SetDbl(par_qty) & ",  " _
    '                                    & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
    '                                    & ")"
    '                par_ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                .Command.Parameters.Clear()
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '                Return False
    '            End Try

    '        End With
    '    Else
    '        With par_obj
    '            Try
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.invc_mstr   " _
    '                                    & "SET  " _
    '                                    & " invc_qty_old=invc_qty, invc_qty = coalesce(invc_qty,0) + " & SetDbl(par_qty) _
    '                                    & "WHERE  " _
    '                                    & " invc_dom_id = " + master_new.ClsVar.sdom_id + _
    '                                    " and invc_en_id = " + par_en_id.ToString + _
    '                                    " and invc_si_id = " + par_si_id.ToString + _
    '                                    " and invc_loc_id = " + par_loc_id.ToString + _
    '                                    " and invc_pt_id = " + par_pt_id.ToString + _
    '                                    " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
    '                par_ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                .Command.Parameters.Clear()
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '                Return False
    '            End Try
    '        End With
    '    End If
    'End Function


    Public Function update_invc_mstr_minus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, _
                                           ByVal par_en_id As Integer, ByVal par_si_id As Integer, _
                                           ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, _
                                           ByVal par_pt_code As String, ByVal par_serial As String, _
                                           ByVal par_qty As Double) As Boolean
        update_invc_mstr_minus = True
        Dim _invc_oid As String = ""
        Dim _invc_qty As Double = 0
        Dim ssql As String

        Try
            'Using objcb As New master_new.WDABasepgsql("", "")
            '    With objcb
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '.Command.CommandText = "select invc_oid, invc_qty from invc_mstr " + _
            '                       " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
            '                       " and invc_en_id = " + par_en_id.ToString + _
            '                       " and invc_si_id = " + par_si_id.ToString + _
            '                       " and invc_loc_id = " + par_loc_id.ToString + _
            '                       " and invc_pt_id = " + par_pt_id.ToString + _
            '                       " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))

            ssql = "select invc_oid, invc_qty from invc_mstr " + _
                       " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                       " and invc_en_id = " + par_en_id.ToString + _
                       " and invc_si_id = " + par_si_id.ToString + _
                       " and invc_loc_id = " + par_loc_id.ToString + _
                       " and invc_pt_id = " + par_pt_id.ToString + _
                       " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)

            If dt.Rows.Count > 1 Then
                MessageBox.Show("Inventory " + "Inventory " + master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", par_pt_id) + " duplicate in this location " & master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            If dt.Rows.Count > 0 Then
                _invc_oid = dt.Rows(0).Item("invc_oid").ToString
                _invc_qty = dt.Rows(0).Item("invc_qty").ToString
            Else
                _invc_oid = ""
            End If

            '"select invc_oid, (invc_qty - coalesce(invc_qty_alloc,0)) as invc_qty from invc_mstr " + _
            '.InitializeCommand()
            '.DataReader = .Command.ExecuteReader
            'If .DataReader.HasRows Then
            '    While .DataReader.Read
            '        _invc_oid = .DataReader("invc_oid").ToString
            '        _invc_qty = .DataReader("invc_qty").ToString
            '    End While
            'Else
            '    _invc_oid = ""
            'End If

            '    End With
            'End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'If _invc_oid = "" Then
        '    If par_serial = "''" Then
        '        MessageBox.Show("Inventory " + par_pt_code + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    Else
        '        MessageBox.Show("Inventory " + par_pt_code + " With Serial Number : " + par_serial + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If _invc_qty < par_qty Then
        '    MessageBox.Show("Inventory " + par_pt_code + " Lower Than Qty Process...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'Else
        '    With par_obj
        '        Try
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "UPDATE  " _
        '                                & "  public.invc_mstr   " _
        '                                & "SET  " _
        '                                & "  invc_qty = invc_qty - " & SetDbl(par_qty) _
        '                                & " WHERE  " _
        '                                & "  invc_oid = " & SetSetring(_invc_oid) & " "
        '            par_ssqls.Add(.Command.CommandText)
        '            .Command.ExecuteNonQuery()
        '            .Command.Parameters.Clear()
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Return False
        '        End Try
        '    End With
        'End If


        If _invc_oid = "" Then
            If par_serial = "''" Then
                'MessageBox.Show("Inventory " + par_pt_code + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Return False
            Else
                'MessageBox.Show("Inventory " + par_pt_code + " With Serial Number : " + par_serial + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Return False
            End If
        End If

        If _invc_qty < par_qty Then
            Dim pt_desc As String = master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_code", par_pt_code)
            Dim loc_desc As String = master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id) '"par_loc_id"

            MessageBox.Show("Inventory " + par_pt_code + " " + pt_desc + " in location " + loc_desc + " = " & _invc_qty & ", Lower Than Qty Process... (" & par_qty & ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & "  invc_qty_old=invc_qty,invc_qty = invc_qty - " & SetDbl(par_qty) _
                                        & " WHERE  " _
                                        & "  invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                       " and invc_en_id = " + par_en_id.ToString + _
                                       " and invc_si_id = " + par_si_id.ToString + _
                                       " and invc_loc_id = " + par_loc_id.ToString + _
                                       " and invc_pt_id = " + par_pt_id.ToString + _
                                       " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    par_ssqls.Add(.Command.CommandText)


                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If

        'With par_obj
        '    Try
        '        .Command.CommandType = CommandType.Text
        '        .Command.CommandText = "delete from invc_mstr where invc_qty = 0"
        '        par_ssqls.Add(.Command.CommandText)
        '        .Command.ExecuteNonQuery()
        '        .Command.Parameters.Clear()

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try
        'End With
    End Function

    Public Function update_invc_mstr_minus_return(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, _
                                           ByVal par_en_id As Integer, ByVal par_si_id As Integer, _
                                           ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, _
                                           ByVal par_pt_code As String, ByVal par_serial As String, _
                                           ByVal par_qty As Double) As Boolean
        update_invc_mstr_minus_return = True
        Dim _invc_oid As String = ""
        Dim _invc_qty As Double = 0
        Dim _invc_qty_old As Double = 0
        Dim ssql As String

        Try
            'Using objcb As New master_new.WDABasepgsql("", "")
            '    With objcb
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '.Command.CommandText = "select invc_oid, invc_qty from invc_mstr " + _
            '                       " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
            '                       " and invc_en_id = " + par_en_id.ToString + _
            '                       " and invc_si_id = " + par_si_id.ToString + _
            '                       " and invc_loc_id = " + par_loc_id.ToString + _
            '                       " and invc_pt_id = " + par_pt_id.ToString + _
            '                       " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))

            ssql = "select invc_oid, invc_qty from invc_mstr " + _
                       " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                       " and invc_en_id = " + par_en_id.ToString + _
                       " and invc_si_id = " + par_si_id.ToString + _
                       " and invc_loc_id = " + par_loc_id.ToString + _
                       " and invc_pt_id = " + par_pt_id.ToString + _
                       " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)

            If dt.Rows.Count > 1 Then
                MessageBox.Show("Inventory " + "Inventory " + master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_id", par_pt_id) + " duplicate in this location " & master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            If dt.Rows.Count > 0 Then
                _invc_oid = dt.Rows(0).Item("invc_oid").ToString
                _invc_qty = dt.Rows(0).Item("invc_qty").ToString
                '_invc_qty_old = dt.Rows(0).Item("invc_qty_old").ToString
            Else
                _invc_oid = ""
            End If

            '"select invc_oid, (invc_qty - coalesce(invc_qty_alloc,0)) as invc_qty from invc_mstr " + _
            '.InitializeCommand()
            '.DataReader = .Command.ExecuteReader
            'If .DataReader.HasRows Then
            '    While .DataReader.Read
            '        _invc_oid = .DataReader("invc_oid").ToString
            '        _invc_qty = .DataReader("invc_qty").ToString
            '    End While
            'Else
            '    _invc_oid = ""
            'End If

            '    End With
            'End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'If _invc_oid = "" Then
        '    If par_serial = "''" Then
        '        MessageBox.Show("Inventory " + par_pt_code + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    Else
        '        MessageBox.Show("Inventory " + par_pt_code + " With Serial Number : " + par_serial + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If _invc_qty < par_qty Then
        '    MessageBox.Show("Inventory " + par_pt_code + " Lower Than Qty Process...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'Else
        '    With par_obj
        '        Try
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "UPDATE  " _
        '                                & "  public.invc_mstr   " _
        '                                & "SET  " _
        '                                & "  invc_qty = invc_qty - " & SetDbl(par_qty) _
        '                                & " WHERE  " _
        '                                & "  invc_oid = " & SetSetring(_invc_oid) & " "
        '            par_ssqls.Add(.Command.CommandText)
        '            .Command.ExecuteNonQuery()
        '            .Command.Parameters.Clear()
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Return False
        '        End Try
        '    End With
        'End If


        If _invc_oid = "" Then
            If par_serial = "''" Then
                'MessageBox.Show("Inventory " + par_pt_code + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Return False
            Else
                'MessageBox.Show("Inventory " + par_pt_code + " With Serial Number : " + par_serial + " Does Not Exist At This Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Return False
            End If
        End If

        If _invc_qty < par_qty Then
            Dim pt_desc As String = master_new.PGSqlConn.GetIDByName("pt_mstr", "pt_desc1", "pt_code", par_pt_code)
            Dim loc_desc As String = master_new.PGSqlConn.GetIDByName("loc_mstr", "loc_desc", "loc_id", par_loc_id) '"par_loc_id"

            MessageBox.Show("Inventory " + par_pt_code + " " + pt_desc + " in location " + loc_desc + " = " & _invc_qty & ", Lower Than Qty Process... (" & par_qty & ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & "  invc_qty_old=invc_qty," _
                                        & "  invc_qty = invc_qty_old - " & SetDbl(par_qty) _
                                        & " WHERE  " _
                                        & "  invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                       " and invc_en_id = " + par_en_id.ToString + _
                                       " and invc_si_id = " + par_si_id.ToString + _
                                       " and invc_loc_id = " + par_loc_id.ToString + _
                                       " and invc_pt_id = " + par_pt_id.ToString + _
                                       " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    par_ssqls.Add(.Command.CommandText)


                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If

        'With par_obj
        '    Try
        '        .Command.CommandType = CommandType.Text
        '        .Command.CommandText = "delete from invc_mstr where invc_qty = 0"
        '        par_ssqls.Add(.Command.CommandText)
        '        .Command.ExecuteNonQuery()
        '        .Command.Parameters.Clear()

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try
        'End With
    End Function

    Public Function update_invc_mstr_minus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, _
                                           ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pjc_id As Integer, ByVal par_pt_id As Integer, _
                                           ByVal par_pt_code As String, ByVal par_serial As String, ByVal par_qty As Double) As Boolean
        update_invc_mstr_minus = True
        Dim _invc_oid As String = ""
        Dim _invc_qty As Double = 0

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select invc_oid, invc_qty from invc_mstr " + _
                                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                           " and invc_en_id = " + par_en_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_loc_id = " + par_loc_id.ToString + _
                                           " and invc_pt_id = " + par_pt_id.ToString + _
                                           " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    '"select invc_oid, (invc_qty - coalesce(invc_qty_alloc,0)) as invc_qty from invc_mstr " + _
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _invc_oid = .DataReader("invc_oid").ToString
                            _invc_qty = .DataReader("invc_qty").ToString
                        End While
                    Else
                        _invc_oid = ""
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _invc_oid = "" Then
            If par_serial = "''" Then
                MessageBox.Show("Inventory " + par_pt_code + " Does Not Exist At This Project & Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                MessageBox.Show("Inventory " + par_pt_code + " With Serial Number : " + par_serial + " Does Not Exist At This Project & Location....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If _invc_qty < par_qty Then
            MessageBox.Show("Inventory " + par_pt_code + " Lower Than Qty Process...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & "  invc_qty = invc_qty - " & SetDbl(par_qty) _
                                        & " WHERE  " _
                                        & "  invc_oid = " & SetSetring(_invc_oid) & " "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If

        'tidak usah dilakukan terjadi bugs, karena datanya 0 lalu di insert tapi malah kedelete duluan dan tidak pernah terinsert lagi
        'by sys 20110420
        'With par_obj
        '    Try
        '        .Command.CommandType = CommandType.Text
        '        .Command.CommandText = "delete from invc_mstr where invc_qty = 0"
        '        par_ssqls.Add(.Command.CommandText)
        '        .Command.ExecuteNonQuery()
        '        .Command.Parameters.Clear()
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try
        'End With
    End Function

    Public Function boq_trans_id(ByVal par_pjc_id As Integer) As String
        boq_trans_id = ""

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT  boq_trans_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join boq_mstr on prj_oid = boq_sopj_oid " _
                        & "  inner join pjc_mstr on prj_code = pjc_code " _
                        & " where pjc_id = " + SetInteger(par_pjc_id)

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        boq_trans_id = .DataReader.Item("boq_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return boq_trans_id
    End Function
    Public Function get_invc_oid(ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, _
                            ByVal par_pt_id As Integer, ByVal par_pjc_id As Integer, ByVal par_serial As String) As String

        Dim _invc_oid As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select invc_oid from invc_mstr " + _
                                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                           " and invc_en_id = " + par_en_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_loc_id = " + par_loc_id.ToString + _
                                           " and invc_pjc_id = " + par_pjc_id.ToString + _
                                           " and invc_pt_id = " + par_pt_id.ToString + _
                                           " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _invc_oid = .DataReader("invc_oid").ToString
                        End While
                    Else
                        _invc_oid = ""
                    End If
                End With
            End Using

            Return _invc_oid

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function

    Public Function bdgt_trans_id(ByVal par_pjc_id As Integer) As String
        bdgt_trans_id = ""

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bdgt_trans_id " _
                                            & " from bdgt_mstr " _
                                            & " WHERE bdgt_pjc_id = " & SetInteger(par_pjc_id) & "  " _
                                            & " and bdgt_active = 'Y' "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        bdgt_trans_id = .DataReader.Item("bdgt_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return bdgt_trans_id
    End Function

    'Public Function update_budget_dr_wf(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ac_id As Integer, ByVal par_amount As Double _
    '                                   , ByVal par_cc_id As Integer, ByVal par_pjc_id As Integer, ByVal par_date As Date, ByVal par_type As String) As Boolean
    '    update_budget_dr_wf = True
    '    Dim _sisa_budget As Double = 0
    '    Dim _bdgt_oid As String = ""
    '    Dim _ac_code As String = ""
    '    Dim _pjc_code As String = ""
    '    Dim _cc_code As String = ""
    '    With par_obj
    '        Try
    '            _ac_code = get_account_code(par_ac_id)
    '            _cc_code = get_cost_center_code(par_cc_id)
    '            _pjc_code = get_project_code(par_pjc_id)

    '            If par_pjc_id = 0 Then                     'Jika Bukan Project
    '                _bdgt_oid = get_bdgt_oid(par_cc_id)
    '            Else                                       'Jika Project
    '                _bdgt_oid = get_bdgt_oid(SetInteger(0), par_pjc_id)
    '            End If

    '            Dim _acc_cek_budget As String
    '            _acc_cek_budget = acc_cek_budget(par_ac_id.ToString())

    '            If _acc_cek_budget = "Y" Then
    '                If get_budget(_bdgt_oid, par_ac_id, par_date) = False Then
    '                    'MessageBox.Show("Budget Untuk Account Kode : " + _ac_code + " Tidak Tersedia...!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    MessageBox.Show("Get Budget Error For Project : " + _pjc_code + ", Cost Center : " + _cc_code + ", Account : " + _ac_code, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    update_budget_dr_wf = False
    '                    Exit Function
    '                End If
    '            End If

    '            'Cek status close periode (apakah masih aktif atau sudah close)
    '            If status_close_periode(par_date) = "Y" Then
    '                MessageBox.Show("Periode Budget :  " + par_date.ToString("dd/MM/yyyy") + " Is Closed..! Transaction Failed..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                update_budget_dr_wf = False
    '                Exit Function
    '            End If

    '            If par_type = "DRV" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "KPREEDIT" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "DZMIN" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "DRREALIZATIONAPPROVE" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "PPREEDIT" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "DVDELETE" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "DZPLUS" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "CANCELREALISASI" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "CANCELK" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            ElseIf par_type = "CANCELP" Then
    '                _sisa_budget = get_remaining_budget(_bdgt_oid, par_ac_id, par_date)
    '            Else
    '                _sisa_budget = get_sisa_budget(_bdgt_oid, par_ac_id, par_date)
    '            End If

    '            'Dim _acc_cek_budget As String
    '            '_acc_cek_budget = acc_cek_budget(par_ac_id.ToString())
    '            If _acc_cek_budget = "Y" Then
    '                If _sisa_budget < par_amount Then
    '                    'MessageBox.Show("Nilai Pengajuan Lebih Besar Dari Budget Yang Tersedia,,! Silahkan Lakukan Cross Budget Terlebih Dahulu,,!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    MessageBox.Show("Over Budget For Project : " + _pjc_code + ", Cost Center : " + _cc_code + ", Account : " + _ac_code, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    update_budget_dr_wf = False
    '                    Exit Function
    '                End If
    '            Else
    '                Exit Function
    '            End If

    '            If par_type = "P" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "DVDELETE" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "PPREEDIT" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_realisasi = bdgtd_realisasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "K" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "KPREEDIT" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "DRI" Then 'Ini jika dari disbursment realisasi (insert)
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "DRREALIZATIONAPPROVE" Then 'Ini jika dari disbursment realisasi (Approve terakhir)
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_realisasi = bdgtd_realisasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "DRV" Then 'Ini jika dari disbursment Voucer
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_realisasi = bdgtd_realisasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "DRREALIZATIONDELETE" Then 'Ini jika dari disbursment realisasi (delete)
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_realisasi = bdgtd_realisasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "DRD" Then 'Ini jika dari disbursment realisasi (delete)
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "DZMIN" Then 'Ini jika dari disbursment realisasi (insert/hapus dulu)
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "DZPLUS" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "DRAPPROVE" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_realisasi = bdgtd_realisasi + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp" _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "CANCELP" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp " _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)

    '            ElseIf par_type = "CANCELK" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp " _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "CANCELREALISASI" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_realisasi = bdgtd_realisasi - " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp " _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            ElseIf par_type = "UPDATERELOKASI" Then
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "UPDATE  " _
    '                                    & "  public.bdgtd_det   " _
    '                                    & "SET  " _
    '                                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                    & "  bdgtd_upd_date = current_timestamp, " _
    '                                    & "  bdgtd_relokasi = coalesce(bdgtd_relokasi,0) + " & SetDbl(par_amount) & ",  " _
    '                                    & "  bdgtd_dt = current_timestamp " _
    '                                    & "  " _
    '                                    & "WHERE  " _
    '                                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
    '                                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
    '                                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
    '                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
    '                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
    '                                                              & " ) " _
    '                                    & ";"
    '                par_ssqls.Add(.Command.CommandText)
    '            End If


    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()

    '        Catch ex As Exception
    '            'If par_type = "P" Or par_type = "K" Then
    '            '    If Err.Number = 5 Then
    '            '        MessageBox.Show("Over Budget For Account : " + _ac_code, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            '    Else : MessageBox.Show(ex.Message)
    '            '    End If
    '            'Else : MessageBox.Show(ex.Message)
    '            'End If
    '            MessageBox.Show("Error Update Budget For Project : " + _pjc_code + ", Cost Center : " + _cc_code + ", Account : " + _ac_code, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Return False
    '        End Try
    '    End With
    'End Function

    Public Function cek_inv(ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, _
                           ByVal par_pt_id As Integer, ByVal par_pjc_id As Integer, ByVal par_serial As String) As Boolean

        Dim _invc_oid As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select invc_oid from invc_mstr " + _
                                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                           " and invc_en_id = " + par_en_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_loc_id = " + par_loc_id.ToString + _
                                           " and invc_pt_id = " + par_pt_id.ToString + _
                                           " and invc_pjc_id = " + par_pjc_id.ToString + _
                                           " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _invc_oid = .DataReader("invc_oid").ToString
                        End While
                    Else
                        _invc_oid = ""
                    End If
                End With
            End Using

            If _invc_oid = "" Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Public Function update_invh_mstr(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_tran_id As Integer, ByVal par_seq As Integer, ByVal par_en_id As Integer, _
                                      ByVal par_trn_code As String, ByVal par_trn_oid As String, ByVal par_desc As String, ByVal par_opn_type As String, _
                                      ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, _
                                      ByVal par_qty As Double, ByVal par_cost As Double, ByVal par_avg_cost As Double, ByVal par_serial As String, ByVal par_date As Date) As Boolean
        'Insert History Inventory
        update_invh_mstr = True
        Dim sSQL As String
        Dim _qty_old As Double = 0
        With par_obj
            Try
                If cek_trx_inv() = False Then
                    Return False
                    Exit Function
                End If

                sSQL = "select invc_oid, invc_qty,coalesce(invc_qty_old,0) as invc_qty_old from invc_mstr " + _
                      " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                      " and invc_en_id = " + par_en_id.ToString + _
                      " and invc_si_id = " + par_si_id.ToString + _
                      " and invc_loc_id = " + par_loc_id.ToString + _
                      " and invc_pt_id = " + par_pt_id.ToString + _
                      " and coalesce(invc_serial,'') = " + IIf(par_serial = "''" Or par_serial = "", "''", SetSetring(par_serial))

                Dim dt As New DataTable
                dt = master_new.PGSqlConn.GetTableData(sSQL)
                If dt.Rows.Count > 0 Then
                    _qty_old = dt.Rows(0).Item("invc_qty")
                Else
                    _qty_old = 0
                End If


                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invh_mstr " _
                                        & "( " _
                                        & "  invh_oid, " _
                                        & "  invh_tran_id, " _
                                        & "  invh_seq, " _
                                        & "  invh_dom_id, " _
                                        & "  invh_en_id, " _
                                        & "  invh_trn_code, " _
                                        & "  invh_trn_oid, " _
                                        & "  invh_date, " _
                                        & "  invh_desc, " _
                                        & "  invh_opn_type, " _
                                        & "  invh_si_id, " _
                                        & "  invh_loc_id, " _
                                        & "  invh_pt_id, " _
                                        & "  invh_qty, " _
                                        & "  invh_cost, " _
                                        & "  invh_avg_cost, " _
                                        & "  invh_serial,invh_qty_old " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(par_tran_id) & ",  " _
                                        & SetInteger(par_seq) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetSetring(par_en_id) & ",  " _
                                        & SetSetring(par_trn_code) & ",  " _
                                        & SetSetring(par_trn_oid) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_desc) & ",  " _
                                        & SetSetring(par_opn_type) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetDbl(par_cost) & ",  " _
                                        & SetDbl(par_avg_cost) & ",  " _
                                        & SetSetring(par_serial) & ",  " _
                                        & SetDbl(_qty_old) & "  " _
                                        & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Public Function update_invh_mstr(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_tran_id As Integer, ByVal par_seq As Integer, ByVal par_en_id As Integer, _
                                     ByVal par_trn_code As String, ByVal par_trn_oid As String, ByVal par_desc As String, ByVal par_opn_type As String, _
                                     ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pjc_id As Integer, ByVal par_pt_id As Integer, _
                                     ByVal par_qty As Double, ByVal par_cost As Double, ByVal par_avg_cost As Double, ByVal par_serial As String, ByVal par_date As Date) As Boolean
        'Insert History Inventory
        update_invh_mstr = True
        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invh_mstr " _
                                        & "( " _
                                        & "  invh_oid, " _
                                        & "  invh_tran_id, " _
                                        & "  invh_seq, " _
                                        & "  invh_dom_id, " _
                                        & "  invh_en_id, " _
                                        & "  invh_trn_code, " _
                                        & "  invh_trn_oid, " _
                                        & "  invh_date, " _
                                        & "  invh_desc, " _
                                        & "  invh_opn_type, " _
                                        & "  invh_si_id, " _
                                        & "  invh_loc_id, " _
                                        & "  invh_pjc_id, " _
                                        & "  invh_pt_id, " _
                                        & "  invh_qty, " _
                                        & "  invh_cost, " _
                                        & "  invh_avg_cost, " _
                                        & "  invh_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(par_tran_id) & ",  " _
                                        & SetInteger(par_seq) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetSetring(par_en_id) & ",  " _
                                        & SetSetring(par_trn_code) & ",  " _
                                        & SetSetring(par_trn_oid) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_desc) & ",  " _
                                        & SetSetring(par_opn_type) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                         & SetInteger(par_pjc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetDbl(par_cost) & ",  " _
                                        & SetDbl(par_avg_cost) & ",  " _
                                        & SetSetring(par_serial) & "  " _
                                        & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Public Function update_pspt_rpt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_tran_id As Integer, ByVal par_seq As Integer, ByVal par_en_id As Integer, _
                                      ByVal par_trn_code As String, ByVal par_trn_oid As String, ByVal par_desc As String, ByVal par_opn_type As String, _
                                      ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, _
                                      ByVal par_qty As Double, ByVal par_cost As Double, ByVal par_avg_cost As Double, ByVal par_serial As String, ByVal par_date As Date) As Boolean
        'Insert History Point
        'update_invh_mstr = True
        Dim sSQL As String
        Dim _qty_old As Double = 0
        With par_obj
            Try
                If cek_trx_inv() = False Then
                    Return False
                    Exit Function
                End If

                sSQL = "select pspt_oid, pspt_point, pspt_per_ttl, coalesce(pspt_per_ttl_old,0) as pspt_per_ttl_old from pspt_rpt " + _
                      " where pspt_en_id = " + master_new.ClsVar.sdom_id + _
                      " and pspt_en_id = " + par_en_id.ToString + _
                      " and pspt_ptnr_id = " + par_si_id.ToString + _
                      " and pspt_periode_code = " + par_loc_id.ToString + _
                      " and invc_pt_id = " + par_pt_id.ToString + _
                      " and coalesce(invc_serial,'') = " + IIf(par_serial = "''" Or par_serial = "", "''", SetSetring(par_serial))

                Dim dt As New DataTable
                dt = master_new.PGSqlConn.GetTableData(sSQL)
                If dt.Rows.Count > 0 Then
                    _qty_old = dt.Rows(0).Item("invc_qty")
                Else
                    _qty_old = 0
                End If


                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invh_mstr " _
                                        & "( " _
                                        & "  invh_oid, " _
                                        & "  invh_tran_id, " _
                                        & "  invh_seq, " _
                                        & "  invh_dom_id, " _
                                        & "  invh_en_id, " _
                                        & "  invh_trn_code, " _
                                        & "  invh_trn_oid, " _
                                        & "  invh_date, " _
                                        & "  invh_desc, " _
                                        & "  invh_opn_type, " _
                                        & "  invh_si_id, " _
                                        & "  invh_loc_id, " _
                                        & "  invh_pt_id, " _
                                        & "  invh_qty, " _
                                        & "  invh_cost, " _
                                        & "  invh_avg_cost, " _
                                        & "  invh_serial,invh_qty_old " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(par_tran_id) & ",  " _
                                        & SetInteger(par_seq) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetSetring(par_en_id) & ",  " _
                                        & SetSetring(par_trn_code) & ",  " _
                                        & SetSetring(par_trn_oid) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_desc) & ",  " _
                                        & SetSetring(par_opn_type) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetDbl(par_cost) & ",  " _
                                        & SetDbl(par_avg_cost) & ",  " _
                                        & SetSetring(par_serial) & ",  " _
                                        & SetDbl(_qty_old) & "  " _
                                        & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function
#End Region

#Region "Cost"
    Public Function get_cost_sod_det(ByVal par_sod_oid As String) As Double
        'Mengambil Nilai Cost Pada Saat PO Receive
        'Dikarenakan terdapat nilai discount
        'Untuk PPN, PPH tidak diambil nilainya hanya sod_cost - sod_disc saja
        'Kalau po nya non idr harus dikali exchange rate dulu.....
        Dim _sod_cost_real As Double
        get_cost_sod_det = -1

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select sod_cost  as sod_cost_real " _
                                         & " from sod_det " _
                                         & " where sod_oid = '" + par_sod_oid.ToString + "'"
                    '.Command.CommandText = "select sod_cost - ((sod_cost * coalesce(sod_disc,0))) as sod_cost_real " _
                    '                    & " from sod_det " _
                    '                    & " where sod_oid = '" + par_sod_oid.ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _sod_cost_real = .DataReader.Item("sod_cost_real")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_cost_sod_det = _sod_cost_real

        Return get_cost_sod_det
    End Function

    Public Function get_inv_cost(ByVal par_po_oid As String, ByVal par_pt_id As Integer) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT  " _
                                    & "  apr_invoice_cost " _
                                    & "FROM  " _
                                    & "  public.apr_rcv " _
                                    & "  inner join rcvd_det on rcvd_oid = apr_rcvd_oid " _
                                    & "  inner join pod_det on pod_oid = rcvd_pod_oid " _
                                    & "  inner join pt_mstr on pt_id = pod_pt_id " _
                                    & "  where pod_po_oid = " + SetSetring(par_po_oid.ToString()) _
                                    & "  and pt_id = " + SetInteger(par_pt_id)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_inv_cost = .DataReader("apr_invoice_cost")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_inv_cost
    End Function

    Public Function get_cost_pod_det(ByVal par_pod_oid As String) As Double
        'Mengambil Nilai Cost Pada Saat PO Receive
        'Dikarenakan terdapat nilai discount
        'Untuk PPN, PPH tidak diambil nilainya hanya pod_cost - pod_disc saja
        'Kalau po nya non idr harus dikali exchange rate dulu.....

        'Dim _pod_cost_real As Double
        Dim _pod_cost As Double
        get_cost_pod_det = -1
        Dim sSQL As String
        Try
            'Using objcek As New master_new.WDABasepgsql("", "")
            '    With objcek
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text

            '        .Command.CommandText = "select pod_cost - ((pod_cost * coalesce(pod_disc,0))) as pod_cost_real " _
            '                             & " from pod_det " _
            '                             & " where pod_oid = '" + par_pod_oid.ToString + "'"
            '        .InitializeCommand()
            '        .DataReader = .Command.ExecuteReader
            '        While .DataReader.Read
            '            _pod_cost_real = .DataReader.Item("pod_cost_real")
            '        End While
            '    End With
            'End Using
            ' sSQL = "select pod_tax_inc,pod_tax_class,pod_cost - ((pod_cost * coalesce(pod_disc,0))) as pod_cost_real from pod_det where pod_oid='" & par_pod_oid.ToString & "'"

            '            sSQL = "select pod_tax_inc,pod_tax_class,pod_cost  as pod_cost_real from pod_det where pod_oid='" & par_pod_oid.ToString & "'"
            sSQL = "select pod_tax_inc,pod_tax_class,pod_cost - ((pod_cost * coalesce(pod_disc,0)))  as pod_cost_real from pod_det where pod_oid='" & par_pod_oid.ToString & "'"

            Dim dr As DataRow
            dr = master_new.PGSqlConn.GetRowInfo(sSQL)

            'berarti tax include
            Dim _tax_rate As Double

            If dr(0) = "Y" Then
                _tax_rate = get_ppn(dr("pod_tax_class"))
                _pod_cost = dr("pod_cost_real") - (_tax_rate * (dr("pod_cost_real") / (1 + _tax_rate)))
            Else
                _pod_cost = dr("pod_cost_real")
            End If

            '_tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
            '_sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_cost_pod_det = _pod_cost

        Return get_cost_pod_det
    End Function

    Public Function get_pt_cost_method(ByVal par_pt_id As Integer) As String
        'Mengambil type cost_method dari suatu part number (A, F, L)
        get_pt_cost_method = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_cost_method from pt_mstr  " _
                                         & " where pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_pt_cost_method = .DataReader.Item("pt_cost_method")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_pt_cost_method
    End Function
#End Region

#Region "Taxes"
    Public Function get_id_tax_class(ByVal par_tax_class_name As String) As Integer
        'Mengambil nilai id dari suatu taxclass
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_id from code_mstr where code_field ~~* 'taxclass_mstr' and code_active ~~* 'Y'" _
                                         & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                                         & " and code_name ~~* '%" + par_tax_class_name + "%'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_id_tax_class = .DataReader.Item("code_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_id_tax_class
    End Function

    Public Function get_ppn(ByVal par_tax_class As Integer) As Double
        'Mengambil tax rate untuk tax type PPN
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_name, taxr_rate  " + _
                                           " from taxr_mstr  " + _
                                           " inner join code_mstr on code_id = taxr_tax_type " + _
                                           " where taxr_tax_class = " + par_tax_class.ToString + _
                                           " and code_name ~~* 'PPN'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_ppn = .DataReader.Item("taxr_rate") / 100
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_ppn
    End Function

    Public Function get_pph(ByVal par_tax_class As Integer) As Double
        'Mengambil nilai tax rate dari tax type PPH
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_name, taxr_rate  " + _
                                           " from taxr_mstr  " + _
                                           " inner join code_mstr on code_id = taxr_tax_type " + _
                                           " where taxr_tax_class = " + par_tax_class.ToString + _
                                           " and code_name ~~* 'PPH'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_pph = .DataReader.Item("taxr_rate") / 100
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_pph
    End Function
#End Region

#Region "Update GL"

    Public Function get_prodline_account(ByVal par_pl_id As Integer, ByVal par_code As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pla_ac_id, pla_sb_id, pla_cc_id, ac_code,pla_param,pla_desc, ac_name || ' (' || pl_desc || ')' as ac_name from pla_mstr " + _
                               " inner join ac_mstr on ac_id = pla_ac_id " + _
                                " inner join pl_mstr on pla_pl_id = pl_id " + _
                               " where pla_pl_id = " + par_pl_id.ToString + _
                               " and pla_code ~~* '" + par_code + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pla")
                        If ds_bantu.Tables(0).Rows.Count = 0 Then
                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", par_pl_id) & ", baris = " & par_code & " kosong")
                            Return Nothing
                            Exit Function
                        ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", par_pl_id) & ", baris = " & par_code & " belum di setting product line nya")
                            Return Nothing
                            Exit Function
                        ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", par_pl_id) & ", baris = " & par_code & " masih - di setting product line nya")
                            Return Nothing
                            Exit Function
                        End If
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function get_prodline(ByVal par_pt_id As Integer) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_pl_id from pt_mstr where pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_prodline = .DataReader("pt_pl_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_prodline
    End Function

    Public Function insert_glt_det_ic(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_exc_rate As Double, _
                                   ByVal par_type As String, ByVal par_daybook As String) As Boolean
        insert_glt_det_ic = True
        Dim i, _pl_id As Integer
        Dim _glt_code, _glt_desc As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        _glt_desc = ""
        _glt_code = get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id"))

                        If par_daybook.ToUpper = "IC-RPO" Then
                            'MessageBox.Show(par_ds.Tables(0).Rows(i).Item("pod_memo"))
                            If Trim(par_ds.Tables(0).Rows(i).Item("pod_memo")) = "" Then
                                dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                            Else
                                dt_bantu = (get_prodline_account(_pl_id, "PRC_PACC"))
                            End If
                            _glt_desc = "PO Receipt"
                        ElseIf par_daybook.ToUpper = "IC-PRS" Then
                            dt_bantu = (get_prodline_account(_pl_id, "PRC_PORACC"))
                            _glt_desc = "PO Return"
                        End If

                        _cost = get_cost_pod_det(par_ds.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString) * par_ds.Tables(0).Rows(i).Item("rcvd_qty")
                        '_cost = CDbl(_cost.ToString("n")) 'biar 2 digit dibelakang koma

                        'Insert Untuk Yang Debet Dulu
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring(_glt_desc) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If par_daybook.ToUpper = "IC-PRS" Then
                        '    _cost = _cost * -1 'karena retur maka dikali -1 untuk membalikan nilai di glbal_balance
                        'End If

                        If update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, par_cu_id, _
                                                         par_exc_rate, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                        '********************** finish untuk yang debet

                        dt_bantu = New DataTable

                        If par_daybook.ToUpper = "IC-RPO" Then
                            dt_bantu = (get_prodline_account(_pl_id, "PRC_PORACC"))
                        ElseIf par_daybook.ToUpper = "IC-PRS" Then
                            If Trim(par_ds.Tables(0).Rows(i).Item("pod_memo")) = "" Then
                                dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                            Else
                                dt_bantu = (get_prodline_account(_pl_id, "PRC_PACC"))
                            End If
                        End If

                        'dt_bantu = (get_prodline_account(_pl_id, ""))

                        'Insert Untuk Yang Credit

                        'If par_daybook.ToUpper = "IC-PRS" Then
                        '    _cost = _cost * -1 'harus dikali -1 lagi karena walaupun retur di table glt_det tetep harus nilai positif bukan negatif
                        'End If                 'karena diatas sudah dikali -1, jadi disini harus dikali -1 lagi agar jadi positif

                        _seq = _seq + 1

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB(_glt_desc) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If par_daybook.ToUpper = "IC-PRS" Then
                        '    _cost = _cost * -1 'karena retur maka dikali -1 untuk membalikan nilai di glbal_balance
                        'End If

                        If update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, par_cu_id, _
                                                         par_exc_rate, _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Public Function delete_glt_det(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_glt_code As String) As Boolean
        delete_glt_det = True
        Using ds_bantu As New DataSet()
            Dim i As Integer
            Dim _glt_value, _glt_exc_rate As Double
            Dim _ac_sign As String = ""
            Dim _ac_id, _sb_id, _cc_id, _glt_en_id, _glt_cu_id As Integer
            Dim _glt_date As Date

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select * from glt_det where glt_code = '" + par_glt_code + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "glt_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try

            With par_obj
                Try
                    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                        If ds_bantu.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
                            _glt_value = ds_bantu.Tables(0).Rows(i).Item("glt_debit") * -1 'dikali -1 agar dibalik nilainya
                            _ac_sign = "D"
                        ElseIf ds_bantu.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
                            _glt_value = ds_bantu.Tables(0).Rows(i).Item("glt_credit") * -1 'dikali -1 agar dibalik nilainya
                            _ac_sign = "C"
                        End If

                        _ac_id = ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")
                        _sb_id = SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id"))
                        _cc_id = SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id"))
                        _glt_date = ds_bantu.Tables(0).Rows(i).Item("glt_date")
                        _glt_en_id = ds_bantu.Tables(0).Rows(i).Item("glt_en_id")
                        _glt_cu_id = ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")
                        _glt_exc_rate = ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")

                        If update_unposted_glbal_balance(par_ssqls, par_obj, _glt_date, _
                                                                   _ac_id, _sb_id, _cc_id, _
                                                                   _glt_en_id, _
                                                                   _glt_cu_id, _glt_exc_rate, _
                                                                   _glt_value, _ac_sign) = False Then
                            Return False
                            Exit Function
                        End If
                    Next

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "delete from glt_det where glt_code = '" + par_glt_code + "'" + _
                                           " and glt_posted = 'N' "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End Using
    End Function

    Public Function delete_glt_det_ap(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ap_oid As String, ByVal par_ap_code As String) As Boolean
        delete_glt_det_ap = True
        Dim _glt_code As String = ""
        Dim _glt_posted As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select glt_code, glt_posted from glt_det " + _
                                           " where glt_ref_oid = '" + par_ap_oid + "'" + _
                                           " and glt_ref_trans_code = '" + par_ap_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _glt_code = .DataReader("glt_code")
                        _glt_posted = .DataReader("glt_posted")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _glt_posted.ToUpper = "N" Then
            If delete_glt_det(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If

            ''by sys 20110425
            ''tetap melakukan jurnal balik tanpa mendelete jurnal sebelumnya
            'If insert_glt_det_ap_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
            '    Return False
            '    Exit Function
            'End If
        Else
            If insert_glt_det_ap_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If
        End If

        'delete_glt_det_ap = True
        'Dim _glt_code As String = ""
        'Dim _glt_posted As String = ""

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select glt_code, glt_posted from glt_det " + _
        '                                   " where glt_ref_oid = '" + par_ap_oid + "'" + _
        '                                   " and glt_ref_trans_code = '" + par_ap_code + "'"
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _glt_code = .DataReader("glt_code")
        '                _glt_posted = .DataReader("glt_posted")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        'If _glt_posted.ToUpper = "N" Then
        '    'If delete_glt_det(par_ssqls, par_obj, _glt_code) = False Then
        '    '    Return False
        '    '    Exit Function
        '    'End If

        '    'by sys 20110425
        '    'tetap melakukan jurnal balik tanpa mendelete jurnal sebelumnya
        '    If insert_glt_det_ap_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
        '        Return False
        '        Exit Function
        '    End If
        'Else
        '    If insert_glt_det_ap_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
        '        Return False
        '        Exit Function
        '    End If
        'End If
    End Function

    Public Function delete_glt_det_ar(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ap_oid As String, ByVal par_ap_code As String) As Boolean
        delete_glt_det_ar = True
        Dim _glt_code As String = ""
        Dim _glt_posted As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select glt_code, glt_posted from glt_det " + _
                                           " where glt_ref_oid = '" + par_ap_oid + "'" + _
                                           " and glt_ref_trans_code = '" + par_ap_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _glt_code = .DataReader("glt_code")
                        _glt_posted = .DataReader("glt_posted")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


        If _glt_posted.ToUpper = "N" Then
            If delete_glt_det(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If
        ElseIf _glt_posted.ToUpper = "Y" Then
            If insert_glt_det_ar_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
                Return False
                Exit Function
            End If
        End If

        'delete_glt_det_ar = True
        'Dim _glt_code As String = ""
        'Dim _glt_posted As String = ""

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select glt_code, glt_posted from glt_det " + _
        '                                   " where glt_ref_oid = '" + par_ap_oid + "'" + _
        '                                   " and glt_ref_trans_code = '" + par_ap_code + "'"
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _glt_code = .DataReader("glt_code")
        '                _glt_posted = .DataReader("glt_posted")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try


        'If _glt_posted.ToUpper = "N" Then
        '    'If delete_glt_det(par_ssqls, par_obj, _glt_code) = False Then
        '    '    Return False
        '    '    Exit Function
        '    'End If

        '    'by sys 20110425
        '    'tetap melakukan jurnal balik tanpa mendelete jurnal sebelumnya
        '    If insert_glt_det_ar_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
        '        Return False
        '        Exit Function
        '    End If
        'ElseIf _glt_posted.ToUpper = "Y" Then
        '    If insert_glt_det_ar_jurnal_balik(par_ssqls, par_obj, _glt_code) = False Then
        '        Return False
        '        Exit Function
        '    End If
        'End If
    End Function

    Public Function insert_glt_det_ar_jurnal_balik(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_glt_code As String) As Boolean

        'ini prosedur untuk jurnal balik jurnal ar karena didelete tetapi sudah di posting...
        insert_glt_det_ar_jurnal_balik = True
        Dim i As Integer
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT glt_oid, glt_dom_id, glt_en_id, glt_gl_oid, glt_code, glt_date, " _
                         & " glt_type, glt_cu_id, glt_exc_rate, glt_seq, glt_ac_id, glt_sb_id, glt_cc_id, " _
                         & " glt_desc, glt_debit, glt_credit, glt_ref_tran_id, glt_ref_trans_code, glt_posted, " _
                         & " glt_daybook, glt_ref_oid, en_code " _
                         & " FROM  " _
                         & " public.glt_det " _
                         & " inner join en_mstr on en_id = glt_en_id" _
                         & " where glt_code = '" + par_glt_code + "' order by glt_seq"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "glt_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        Dim _en_code As String = ds_bantu.Tables(0).Rows(0).Item("en_code")
        Dim _glt_code As String = get_transaction_number("AR", _en_code, "glt_det", "glt_code")

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If ds_bantu.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
                        'debet dibalik jadi credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_is_reverse, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring("AR") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_debit")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
                                            & ")"
                        'debet di set 0 dan credit diset nilainya dari debet agar menjadi jurnal balik
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If update_unposted_glbal_balance(par_ssqls, par_obj, master_new.PGSqlConn.CekTanggal, _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_debit"), _
                                                         "C") = False Then
                            Return False
                            Exit Function
                        End If
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
                        'debet dibalik jadi credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_is_reverse, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring("AR") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                            & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_credit")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
                                            & ")"
                        'credit di set 0 dan debet diset nilainya dari credit agar menjadi jurnal balik
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If update_unposted_glbal_balance(par_ssqls, par_obj, master_new.PGSqlConn.CekTanggal, _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_credit"), _
                                                         "D") = False Then
                            Return False
                            Exit Function
                        End If
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

        ''ini prosedur untuk jurnal balik jurnal ar karena didelete tetapi sudah di posting...
        'insert_glt_det_ar_jurnal_balik = True
        'Dim i As Integer
        'Dim ds_bantu As New DataSet

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT glt_oid, glt_dom_id, glt_en_id, glt_gl_oid, glt_code, glt_date, " _
        '                 & " glt_type, glt_cu_id, glt_exc_rate, glt_seq, glt_ac_id, glt_sb_id, glt_cc_id, " _
        '                 & " glt_desc, glt_debit, glt_credit, glt_ref_tran_id, glt_ref_trans_code, glt_posted, " _
        '                 & " glt_daybook, glt_ref_oid, en_code " _
        '                 & " FROM  " _
        '                 & " public.glt_det " _
        '                 & " inner join en_mstr on en_id = glt_en_id" _
        '                 & " where glt_code = '" + par_glt_code + "' order by glt_seq"
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "glt_det")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try
        'Dim _en_code As String = ds_bantu.Tables(0).Rows(0).Item("en_code")
        'Dim _glt_code As String = get_transaction_number("AR", _en_code, "glt_det", "glt_code")

        'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
        '    With par_obj
        '        Try
        '            If ds_bantu.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
        '                'debet dibalik jadi credit 
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "INSERT INTO  " _
        '                                    & "  public.glt_det " _
        '                                    & "( " _
        '                                    & "  glt_oid, " _
        '                                    & "  glt_dom_id, " _
        '                                    & "  glt_en_id, " _
        '                                    & "  glt_add_by, " _
        '                                    & "  glt_add_date, " _
        '                                    & "  glt_code, " _
        '                                    & "  glt_date, " _
        '                                    & "  glt_type, " _
        '                                    & "  glt_cu_id, " _
        '                                    & "  glt_exc_rate, " _
        '                                    & "  glt_seq, " _
        '                                    & "  glt_ac_id, " _
        '                                    & "  glt_sb_id, " _
        '                                    & "  glt_cc_id, " _
        '                                    & "  glt_desc, " _
        '                                    & "  glt_debit, " _
        '                                    & "  glt_credit, " _
        '                                    & "  glt_ref_oid, " _
        '                                    & "  glt_ref_trans_code, " _
        '                                    & "  glt_posted, " _
        '                                    & "  glt_dt, " _
        '                                    & "  glt_daybook " _
        '                                    & ")  " _
        '                                    & "VALUES ( " _
        '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
        '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
        '                                    & SetSetring(_glt_code) & ",  " _
        '                                    & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
        '                                    & SetSetring("AR") & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
        '                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
        '                                    & SetDblDB(0) & ",  " _
        '                                    & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_debit")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
        '                                    & SetSetring("N") & ",  " _
        '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
        '                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
        '                                    & ")"
        '                'debet di set 0 dan credit diset nilainya dari debet agar menjadi jurnal balik
        '                par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                If update_unposted_glbal_balance(par_ssqls, par_obj, ds_bantu.Tables(0).Rows(i).Item("glt_date"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_debit"), _
        '                                                 "C") = False Then
        '                    Return False
        '                    Exit Function
        '                End If
        '            ElseIf ds_bantu.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
        '                'debet dibalik jadi credit 
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "INSERT INTO  " _
        '                                    & "  public.glt_det " _
        '                                    & "( " _
        '                                    & "  glt_oid, " _
        '                                    & "  glt_dom_id, " _
        '                                    & "  glt_en_id, " _
        '                                    & "  glt_add_by, " _
        '                                    & "  glt_add_date, " _
        '                                    & "  glt_code, " _
        '                                    & "  glt_date, " _
        '                                    & "  glt_type, " _
        '                                    & "  glt_cu_id, " _
        '                                    & "  glt_exc_rate, " _
        '                                    & "  glt_seq, " _
        '                                    & "  glt_ac_id, " _
        '                                    & "  glt_sb_id, " _
        '                                    & "  glt_cc_id, " _
        '                                    & "  glt_desc, " _
        '                                    & "  glt_debit, " _
        '                                    & "  glt_credit, " _
        '                                    & "  glt_ref_oid, " _
        '                                    & "  glt_ref_trans_code, " _
        '                                    & "  glt_posted, " _
        '                                    & "  glt_dt, " _
        '                                    & "  glt_daybook " _
        '                                    & ")  " _
        '                                    & "VALUES ( " _
        '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
        '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
        '                                    & SetSetring(_glt_code) & ",  " _
        '                                    & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
        '                                    & SetSetring("AR") & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
        '                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
        '                                    & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_credit")) & ",  " _
        '                                    & SetDblDB(0) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
        '                                    & SetSetring("N") & ",  " _
        '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
        '                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
        '                                    & ")"
        '                'credit di set 0 dan debet diset nilainya dari credit agar menjadi jurnal balik
        '                par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                If update_unposted_glbal_balance(par_ssqls, par_obj, ds_bantu.Tables(0).Rows(i).Item("glt_date"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_credit"), _
        '                                                 "D") = False Then
        '                    Return False
        '                    Exit Function
        '                End If
        '            End If

        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Return False
        '        End Try
        '    End With
        'Next
    End Function

    Public Function insert_glt_det_ap_jurnal_balik(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_glt_code As String) As Boolean
        'ini prosedur untuk jurnal balik jurnal ap karena didelete tetapi sudah di posting...
        insert_glt_det_ap_jurnal_balik = True
        Dim i As Integer
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT glt_oid, glt_dom_id, glt_en_id, glt_gl_oid, glt_code, glt_date, " _
                         & " glt_type, glt_cu_id, glt_exc_rate, glt_seq, glt_ac_id, glt_sb_id, glt_cc_id, " _
                         & " glt_desc, glt_debit, glt_credit, glt_ref_tran_id, glt_ref_trans_code, glt_posted, " _
                         & " glt_daybook, glt_ref_oid, en_code " _
                         & " FROM  " _
                         & " public.glt_det " _
                         & " inner join en_mstr on en_id = glt_en_id" _
                         & " where glt_code = '" + par_glt_code + "' order by glt_seq"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "glt_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Dim _en_code As String = ds_bantu.Tables(0).Rows(0).Item("en_code")
        Dim _glt_code As String = get_transaction_number("AP", _en_code, "glt_det", "glt_code")
        Dim _date As Date = get_tanggal_sistem()

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If ds_bantu.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
                        'debet dibalik jadi credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_is_reverse, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("AP") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_debit")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
                                            & ")"
                        'debet di set 0 dan credit diset nilainya dari debet agar menjadi jurnal balik
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If update_unposted_glbal_balance(par_ssqls, par_obj, master_new.PGSqlConn.CekTanggal, _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_debit"), _
                                                         "C") = False Then
                            Return False
                            Exit Function
                        End If
                    ElseIf ds_bantu.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
                        'debet dibalik jadi credit 
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_is_reverse, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("AP") & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
                                            & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
                                            & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
                                            & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                            & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_credit")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
                                            & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
                                            & ")"
                        'credit di set 0 dan debet diset nilainya dari credit agar menjadi jurnal balik
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If update_unposted_glbal_balance(par_ssqls, par_obj, master_new.PGSqlConn.CekTanggal, _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
                                                         SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
                                                         ds_bantu.Tables(0).Rows(i).Item("glt_credit"), _
                                                         "D") = False Then
                            Return False
                            Exit Function
                        End If
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

        ''ini prosedur untuk jurnal balik jurnal ap karena didelete tetapi sudah di posting...
        'insert_glt_det_ap_jurnal_balik = True
        'Dim i As Integer
        'Dim ds_bantu As New DataSet

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT glt_oid, glt_dom_id, glt_en_id, glt_gl_oid, glt_code, glt_date, " _
        '                 & " glt_type, glt_cu_id, glt_exc_rate, glt_seq, glt_ac_id, glt_sb_id, glt_cc_id, " _
        '                 & " glt_desc, glt_debit, glt_credit, glt_ref_tran_id, glt_ref_trans_code, glt_posted, " _
        '                 & " glt_daybook, glt_ref_oid, en_code " _
        '                 & " FROM  " _
        '                 & " public.glt_det " _
        '                 & " inner join en_mstr on en_id = glt_en_id" _
        '                 & " where glt_code = '" + par_glt_code + "' order by glt_seq"
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "glt_det")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try
        'Dim _en_code As String = ds_bantu.Tables(0).Rows(0).Item("en_code")
        'Dim _glt_code As String = get_transaction_number("AP", _en_code, "glt_det", "glt_code")
        'Dim _date As Date = get_tanggal_sistem()

        'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
        '    With par_obj
        '        Try
        '            If ds_bantu.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
        '                'debet dibalik jadi credit 
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "INSERT INTO  " _
        '                                    & "  public.glt_det " _
        '                                    & "( " _
        '                                    & "  glt_oid, " _
        '                                    & "  glt_dom_id, " _
        '                                    & "  glt_en_id, " _
        '                                    & "  glt_add_by, " _
        '                                    & "  glt_add_date, " _
        '                                    & "  glt_code, " _
        '                                    & "  glt_date, " _
        '                                    & "  glt_type, " _
        '                                    & "  glt_cu_id, " _
        '                                    & "  glt_exc_rate, " _
        '                                    & "  glt_seq, " _
        '                                    & "  glt_ac_id, " _
        '                                    & "  glt_sb_id, " _
        '                                    & "  glt_cc_id, " _
        '                                    & "  glt_desc, " _
        '                                    & "  glt_debit, " _
        '                                    & "  glt_credit, " _
        '                                    & "  glt_ref_oid, " _
        '                                    & "  glt_ref_trans_code, " _
        '                                    & "  glt_posted, " _
        '                                    & "  glt_dt, " _
        '                                    & "  glt_is_reverse, " _
        '                                    & "  glt_daybook " _
        '                                    & ")  " _
        '                                    & "VALUES ( " _
        '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
        '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
        '                                    & SetSetring(_glt_code) & ",  " _
        '                                    & SetDate(_date) & ",  " _
        '                                    & SetSetring("AP") & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
        '                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
        '                                    & SetDblDB(0) & ",  " _
        '                                    & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_debit")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
        '                                    & SetSetring("N") & ",  " _
        '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
        '                                    & SetSetring("Y") & ",  " _
        '                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
        '                                    & ")"
        '                'debet di set 0 dan credit diset nilainya dari debet agar menjadi jurnal balik
        '                par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                If update_unposted_glbal_balance(par_ssqls, par_obj, ds_bantu.Tables(0).Rows(i).Item("glt_date"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_debit"), _
        '                                                 "C") = False Then
        '                    Return False
        '                    Exit Function
        '                End If
        '            ElseIf ds_bantu.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
        '                'debet dibalik jadi credit 
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "INSERT INTO  " _
        '                                    & "  public.glt_det " _
        '                                    & "( " _
        '                                    & "  glt_oid, " _
        '                                    & "  glt_dom_id, " _
        '                                    & "  glt_en_id, " _
        '                                    & "  glt_add_by, " _
        '                                    & "  glt_add_date, " _
        '                                    & "  glt_code, " _
        '                                    & "  glt_date, " _
        '                                    & "  glt_type, " _
        '                                    & "  glt_cu_id, " _
        '                                    & "  glt_exc_rate, " _
        '                                    & "  glt_seq, " _
        '                                    & "  glt_ac_id, " _
        '                                    & "  glt_sb_id, " _
        '                                    & "  glt_cc_id, " _
        '                                    & "  glt_desc, " _
        '                                    & "  glt_debit, " _
        '                                    & "  glt_credit, " _
        '                                    & "  glt_ref_oid, " _
        '                                    & "  glt_ref_trans_code, " _
        '                                    & "  glt_posted, " _
        '                                    & "  glt_dt, " _
        '                                    & "  glt_is_reverse, " _
        '                                    & "  glt_daybook " _
        '                                    & ")  " _
        '                                    & "VALUES ( " _
        '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_en_id")) & ",  " _
        '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
        '                                    & SetSetring(_glt_code) & ",  " _
        '                                    & SetDate(_date) & ",  " _
        '                                    & SetSetring("AP") & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_cu_id")) & ",  " _
        '                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_seq")) & ",  " _
        '                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")) & ",  " _
        '                                    & SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
        '                                    & SetDblDB(ds_bantu.Tables(0).Rows(i).Item("glt_credit")) & ",  " _
        '                                    & SetDblDB(0) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_oid")) & ",  " _
        '                                    & SetSetringDB(ds_bantu.Tables(0).Rows(i).Item("glt_ref_trans_code")) & ",  " _
        '                                    & SetSetring("N") & ",  " _
        '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
        '                                    & SetSetring("Y") & ",  " _
        '                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("glt_daybook")) & "  " _
        '                                    & ")"
        '                'credit di set 0 dan debet diset nilainya dari credit agar menjadi jurnal balik
        '                par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                If update_unposted_glbal_balance(par_ssqls, par_obj, ds_bantu.Tables(0).Rows(i).Item("glt_date"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_ac_id"), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_sb_id")), _
        '                                                 SetIntegerDB(ds_bantu.Tables(0).Rows(i).Item("glt_cc_id")), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_en_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_cu_id"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_exc_rate"), _
        '                                                 ds_bantu.Tables(0).Rows(i).Item("glt_credit"), _
        '                                                 "D") = False Then
        '                    Return False
        '                    Exit Function
        '                End If
        '            End If

        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Return False
        '        End Try
        '    End With
        'Next
    End Function

    Public Function update_unposted_glbal_balance_tran(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_date As Date, ByVal par_ac_id As Integer, ByVal par_sb_id As String, ByVal par_cc_id As String, ByVal par_en_id As Integer, ByVal par_cu_id As Integer, ByVal par_exc_rate As Double, ByVal par_glt_value As Double, ByVal par_sign As String) As Boolean
        update_unposted_glbal_balance_tran = True
        Dim _gcal_oid As String = ""
        Dim _glbal_oid As String = ""
        Dim _ac_sign As String = ""
        Dim _ac_cu_id As Integer

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_sign, ac_cu_id from ac_mstr where ac_id = " + par_ac_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ac_sign = .DataReader("ac_sign")
                        _ac_cu_id = .DataReader("ac_cu_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _ac_sign.ToUpper <> par_sign.ToUpper Then
            par_glt_value = par_glt_value * -1
        End If

        If par_cu_id = master_new.ClsVar.ibase_cur_id Then
            If _ac_cu_id <> par_cu_id Then
                par_glt_value = par_glt_value / func_data.get_exchange_rate(_ac_cu_id)
            End If
        Else
            If _ac_cu_id <> par_cu_id Then
                par_glt_value = par_glt_value * par_exc_rate
            End If
        End If

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_oid from gcal_mstr " + _
                                           " where gcal_start_date <= " + SetDate(par_date) + _
                                           " and gcal_end_date >= " + SetDate(par_date)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _gcal_oid = .DataReader("gcal_oid").ToString
                        End While
                    Else
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + par_date.ToString("dd/MM/yyyy") _
                                        & SetString(master_new.PGSqlConn.GetIDByName("sb_mstr", "sb_desc", "sb_id", par_sb_id)) _
                                        & SetString(master_new.PGSqlConn.GetIDByName("cc_mstr", "cc_desc", "cc_id", par_cc_id)), "Error", _
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                           " where glbal_en_id = " + par_en_id.ToString + _
                                           " and glbal_ac_id = " + par_ac_id.ToString + _
                                           " and coalesce(glbal_sb_id,-1) = " + IIf(par_sb_id.ToUpper = "NULL", "-1", par_sb_id) + _
                                           " and coalesce(glbal_cc_id,-1) = " + IIf(par_cc_id.ToUpper = "NULL", "-1", par_cc_id) + _
                                           " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _glbal_oid = .DataReader("glbal_oid").ToString
                        End While
                    Else
                        _glbal_oid = ""
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _glbal_oid = "" Then
            Dim _account_code As String
            _account_code = get_account_code(par_ac_id)
            MessageBox.Show("Opening Balance For Account : " + _account_code + " Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            'With par_obj
            '    Try
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "INSERT INTO  " _
            '                            & "  public.glbal_balance " _
            '                            & "( " _
            '                            & "  glbal_oid, " _
            '                            & "  glbal_dom_id, " _
            '                            & "  glbal_en_id, " _
            '                            & "  glbal_add_by, " _
            '                            & "  glbal_add_date, " _
            '                            & "  glbal_gcal_oid, " _
            '                            & "  glbal_ac_id, " _
            '                            & "  glbal_sb_id, " _
            '                            & "  glbal_cc_id, " _
            '                            & "  glbal_cu_id, " _
            '                            & "  glbal_balance_open, " _
            '                            & "  glbal_balance_unposted, " _
            '                            & "  glbal_balance_posted, " _
            '                            & "  glbal_dt " _
            '                            & ")  " _
            '                            & "VALUES ( " _
            '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
            '                            & SetInteger(par_en_id) & ",  " _
            '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
            '                            & SetSetring(_gcal_oid.ToString) & ",  " _
            '                            & SetInteger(par_ac_id) & ",  " _
            '                            & par_sb_id & ",  " _
            '                            & par_cc_id & ",  " _
            '                            & SetInteger(_ac_cu_id) & ",  " _
            '                            & SetDbl(0) & ",  " _
            '                            & SetDbl(par_glt_value) & ",  " _
            '                            & SetDbl(0) & ",  " _
            '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
            '                            & ")"
            '        par_ssqls.Add(.Command.CommandText)
            '        .Command.ExecuteNonQuery()
            '        .Command.Parameters.Clear()
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '        Return False
            '    End Try
            'End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.glbal_balance   " _
                                        & "SET  " _
                                        & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                        & "  glbal_balance_unposted = coalesce(glbal_balance_unposted,0) + " & SetDbl(par_glt_value) & ",  " _
                                        & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function

    Public Function update_posted_glbal_balance_tran(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_date As Date, ByVal par_ac_id As Integer, ByVal par_sb_id As String, ByVal par_cc_id As String, ByVal par_en_id As Integer, ByVal par_cu_id As Integer, ByVal par_exc_rate As Double, ByVal par_glt_value As Double, ByVal par_sign As String) As Boolean
        update_posted_glbal_balance_tran = True
        Dim _gcal_oid As String = ""
        Dim _glbal_oid As String = ""

        Dim _ac_sign As String = ""
        Dim _ac_cu_id As Integer
        Dim _ac_type As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_sign, ac_cu_id, ac_type from ac_mstr where ac_id = " + par_ac_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ac_sign = .DataReader("ac_sign")
                        _ac_cu_id = .DataReader("ac_cu_id")
                        _ac_type = .DataReader("ac_type")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _ac_sign.ToUpper <> par_sign.ToUpper Then
            par_glt_value = par_glt_value * -1
        End If

        If par_cu_id = master_new.ClsVar.ibase_cur_id Then
            If _ac_cu_id <> par_cu_id Then
                par_glt_value = par_glt_value / func_data.get_exchange_rate(_ac_cu_id)
            End If
        Else
            If _ac_cu_id <> par_cu_id Then
                par_glt_value = par_glt_value * par_exc_rate
            End If
        End If

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_oid from gcal_mstr " + _
                                           " where gcal_start_date <='" + par_date.ToString("dd/MM/yyyy") + "'" + _
                                           " and gcal_end_date >='" + par_date.ToString("dd/MM/yyyy") + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _gcal_oid = .DataReader("gcal_oid").ToString
                        End While
                    Else
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + par_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                           " where glbal_en_id = " + par_en_id.ToString + _
                                           " and glbal_ac_id = " + par_ac_id.ToString + _
                                           " and coalesce(glbal_sb_id,-1) = " + IIf(par_sb_id.ToUpper = "NULL", "-1", par_sb_id) + _
                                           " and coalesce(glbal_cc_id,-1) = " + IIf(par_cc_id.ToUpper = "NULL", "-1", par_cc_id) + _
                                           " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _glbal_oid = .DataReader("glbal_oid").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.glbal_balance   " _
                                    & "SET  " _
                                    & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  glbal_balance_unposted = glbal_balance_unposted - " & SetDbl(par_glt_value) & ",  " _
                                    & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(par_glt_value) & ",  " _
                                    & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        'Update laba tahun berjalan, kalau pendapatan (R) bertambah, kalau expense (E) berkurang..
        If _ac_type.ToUpper = "E" Or _ac_type.ToUpper = "R" Then
            Dim _ac_pl_cu_id, _dom_pl_ac As Integer
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text

                        .Command.CommandText = "select dom_pl_ac, ac_pl.ac_code as ac_code_pl, cu_pl.cu_id as ac_pl_cu_id " + _
                                               " from dom_mstr " + _
                                               " inner join ac_mstr ac_pl on ac_pl.ac_id = dom_pl_ac " + _
                                               " inner join cu_mstr cu_pl on cu_pl.cu_id = ac_pl.ac_cu_id " + _
                                               " where dom_id = " + master_new.ClsVar.sdom_id
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _ac_pl_cu_id = .DataReader("ac_pl_cu_id")
                            _dom_pl_ac = .DataReader("dom_pl_ac")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try

            If par_cu_id = master_new.ClsVar.ibase_cur_id Then
                If _ac_pl_cu_id <> par_cu_id Then
                    par_glt_value = par_glt_value / func_data.get_exchange_rate(_ac_pl_cu_id)
                End If
            Else
                If _ac_pl_cu_id <> par_cu_id Then
                    par_glt_value = par_glt_value * par_exc_rate
                End If
            End If

            If _ac_type.ToUpper = "E" Then
                If par_glt_value > 0 Then
                    par_glt_value = par_glt_value * -1
                End If
            ElseIf _ac_type.ToUpper = "R" Then
                If par_glt_value < 0 Then
                    par_glt_value = par_glt_value * -1
                End If
            End If

            _glbal_oid = "" 'dikosongkan lagi....kan dicari lagi coy

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text

                        .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                               " where glbal_en_id = " + par_en_id.ToString + _
                                               " and glbal_ac_id = " + _dom_pl_ac.ToString + _
                                               " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _glbal_oid = .DataReader("glbal_oid").ToString
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try

            If _glbal_oid = "" Then
                Dim _account_code As String
                _account_code = get_account_code(par_ac_id)
                MessageBox.Show("Opening Balance For Account :" + _account_code + "Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
                'With par_obj
                '    Try
                '        .Command.CommandType = CommandType.Text
                '        .Command.CommandText = "INSERT INTO  " _
                '                            & "  public.glbal_balance " _
                '                            & "( " _
                '                            & "  glbal_oid, " _
                '                            & "  glbal_dom_id, " _
                '                            & "  glbal_en_id, " _
                '                            & "  glbal_add_by, " _
                '                            & "  glbal_add_date, " _
                '                            & "  glbal_gcal_oid, " _
                '                            & "  glbal_ac_id, " _
                '                            & "  glbal_sb_id, " _
                '                            & "  glbal_cc_id, " _
                '                            & "  glbal_cu_id, " _
                '                            & "  glbal_balance_open, " _
                '                            & "  glbal_balance_unposted, " _
                '                            & "  glbal_balance_posted, " _
                '                            & "  glbal_dt " _
                '                            & ")  " _
                '                            & "VALUES ( " _
                '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                            & SetInteger(par_en_id) & ",  " _
                '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                            & SetSetring(_gcal_oid.ToString) & ",  " _
                '                            & SetInteger(_dom_pl_ac) & ",  " _
                '                            & 0 & ",  " _
                '                            & 0 & ",  " _
                '                            & SetInteger(_ac_cu_id) & ",  " _
                '                            & SetDbl(0) & ",  " _
                '                            & SetDbl(0) & ",  " _
                '                            & SetDbl(par_glt_value) & ",  " _
                '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                '                            & ")"
                '        par_ssqls.Add(.Command.CommandText)
                '        .Command.ExecuteNonQuery()
                '        .Command.Parameters.Clear()
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message)
                '        Return False
                '    End Try
                'End With
            Else
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.glbal_balance   " _
                                            & "SET  " _
                                            & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(par_glt_value) & ",  " _
                                            & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        End If
    End Function

    Public Function update_unposted_glbal_balance(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_date As Date, ByVal par_ac_id As Integer, ByVal par_sb_id As String, ByVal par_cc_id As String, ByVal par_en_id As Integer, ByVal par_cu_id As Integer, ByVal par_exc_rate As Double, ByVal par_glt_value As Double, ByVal par_sign As String) As Boolean
        update_unposted_glbal_balance = True
        Dim _gcal_oid As String = ""
        Dim _glbal_oid As String = ""
        Dim _ac_sign As String = ""
        Dim _ac_cu_id As Integer

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_sign, ac_cu_id from ac_mstr where ac_id = " + par_ac_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ac_sign = .DataReader("ac_sign")
                        _ac_cu_id = .DataReader("ac_cu_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'If par_cu_id = master_new.ClsVar.ibase_cur_id Then
        '    If _ac_cu_id <> par_cu_id Then
        '        MessageBox.Show("Invalid Transaction, Contact Your Admin System...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '        'ini tidak mungkin terjadi karena currency idr tidak bisa melakukan pembayaran non idr
        '        'bingung ketika terjadi transaksinya..coba liat di menu faccount search
        '        '_kurs = func_data.get_exchange_rate(_ac_cu_id)
        '        'If _kurs = "1" Then
        '        '    MessageBox.Show("Exchange Rate Doesn't Exist...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        '    Return False
        '        'End If
        '        ''par_glt_value = par_glt_value / _kurs
        '    End If
        'Else

        'If _ac_cu_id <> master_new.ClsVar.ibase_cur_id Then
        par_glt_value = par_glt_value * par_exc_rate
        'End If

        'If _ac_cu_id <> par_cu_id Then
        '    par_glt_value = par_glt_value * par_exc_rate
        'End If

        'End If

        If _ac_sign <> par_sign Then
            par_glt_value = par_glt_value * -1
        End If

        'If par_cu_id <> master_new.ClsVar.ibase_cur_id Then
        '    If _ac_cu_id <> par_cu_id Then
        '        MessageBox.Show("Invalid Transaction, Contact Your Admin System...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        '_amount_cur = par_glt_value
        'par_glt_value = par_glt_value * par_exc_rate

        'Return False
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_oid from gcal_mstr " + _
                                           " where gcal_start_date <=" + SetDate(par_date) + "" + _
                                           " and gcal_end_date >=" + SetDate(par_date) + ""
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _gcal_oid = .DataReader("gcal_oid").ToString
                        End While
                    Else
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + par_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                           " where glbal_en_id = " + par_en_id.ToString + _
                                           " and glbal_ac_id = " + par_ac_id.ToString + _
                                           " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"

                    '.Command.CommandText = "select glbal_oid from glbal_balance" + _
                    '                      " where glbal_en_id = " + par_en_id.ToString + _
                    '                      " and glbal_ac_id = " + par_ac_id.ToString + _
                    '                      " and glbal_sb_id = " + par_sb_id + _
                    '                      " and glbal_cc_id = " + par_cc_id + _
                    '                      " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _glbal_oid = .DataReader("glbal_oid").ToString
                        End While
                    Else
                        _glbal_oid = ""
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _glbal_oid = "" Then
            Dim _account_code As String
            _account_code = get_account_code(par_ac_id)
            MessageBox.Show("Opening Balance For Account : " + _account_code + " Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            'With par_obj
            '    Try
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "INSERT INTO  " _
            '                            & "  public.glbal_balance " _
            '                            & "( " _
            '                            & "  glbal_oid, " _
            '                            & "  glbal_dom_id, " _
            '                            & "  glbal_en_id, " _
            '                            & "  glbal_add_by, " _
            '                            & "  glbal_add_date, " _
            '                            & "  glbal_gcal_oid, " _
            '                            & "  glbal_ac_id, " _
            '                            & "  glbal_sb_id, " _
            '                            & "  glbal_cc_id, " _
            '                            & "  glbal_cu_id, " _
            '                            & "  glbal_balance_open, " _
            '                            & "  glbal_balance_unposted, " _
            '                            & "  glbal_balance_posted, " _
            '                            & "  glbal_dt " _
            '                            & ")  " _
            '                            & "VALUES ( " _
            '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
            '                            & SetInteger(par_en_id) & ",  " _
            '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
            '                            & SetSetring(_gcal_oid.ToString) & ",  " _
            '                            & SetInteger(par_ac_id) & ",  " _
            '                            & par_sb_id & ",  " _
            '                            & par_cc_id & ",  " _
            '                            & SetInteger(_ac_cu_id) & ",  " _
            '                            & SetDbl(0) & ",  " _
            '                            & SetDbl(par_glt_value) & ",  " _
            '                            & SetDbl(0) & ",  " _
            '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
            '                            & ")"
            '        par_ssqls.Add(.Command.CommandText)
            '        .Command.ExecuteNonQuery()
            '        .Command.Parameters.Clear()
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '        Return False
            '    End Try
            'End With
        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.glbal_balance   " _
                                        & "SET  " _
                                        & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                        & "  glbal_balance_unposted = coalesce(glbal_balance_unposted,0) + " & SetDbl(par_glt_value) & ",  " _
                                        & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function

    Public Function update_posted_glbal_balance(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_date As Date, ByVal par_ac_id As Integer, ByVal par_sb_id As String, ByVal par_cc_id As String, ByVal par_en_id As Integer, ByVal par_cu_id As Integer, ByVal par_exc_rate As Double, ByVal par_glt_value As Double, ByVal par_sign As String) As Boolean
        update_posted_glbal_balance = True
        Dim _gcal_oid As String = ""
        Dim _glbal_oid As String = ""

        Dim _ac_sign As String = ""
        Dim _ac_cu_id As Integer
        Dim _ac_type As String = ""
        'Dim _glt_value_laba_berjalan As Double = par_glt_value

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_sign, ac_cu_id, ac_type from ac_mstr where ac_id = " + par_ac_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ac_sign = .DataReader("ac_sign")
                        _ac_cu_id = .DataReader("ac_cu_id")
                        _ac_type = .DataReader("ac_type")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _ac_sign.ToUpper <> par_sign.ToUpper Then
            par_glt_value = par_glt_value * -1
        End If

        'If par_cu_id = master_new.ClsVar.ibase_cur_id Then
        '    If _ac_cu_id <> par_cu_id Then
        '        MessageBox.Show("Invalid Transaction, Contact Your Admin System...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '        'ini tidak mungkin terjadi karena currency idr tidak bisa melakukan pembayaran non idr
        '        'bingung ketika terjadi transaksinya..coba liat di menu faccount search
        '        'par_glt_value = par_glt_value / func_data.get_exchange_rate(_ac_cu_id)
        '    End If
        'Else
        '    If _ac_cu_id <> par_cu_id Then
        '        par_glt_value = par_glt_value * par_exc_rate
        '    End If
        'End If

        'If par_cu_id = master_new.ClsVar.ibase_cur_id Then
        '    If _ac_cu_id <> par_cu_id Then
        '        MessageBox.Show("Invalid Transaction, Contact Your Admin System...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        par_glt_value = par_glt_value * par_exc_rate


        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_oid from gcal_mstr " + _
                                           " where gcal_start_date <=" + SetDate(par_date) + "" + _
                                           " and gcal_end_date >=" + SetDate(par_date) + ""
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _gcal_oid = .DataReader("gcal_oid").ToString
                        End While
                    Else
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + par_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                           " where glbal_en_id = " + par_en_id.ToString + _
                                           " and glbal_ac_id = " + par_ac_id.ToString + _
                                           " and glbal_sb_id = 0 " + _
                                           " and glbal_cc_id = 0 " + _
                                           " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _glbal_oid = .DataReader("glbal_oid").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.glbal_balance   " _
                                    & "SET  " _
                                    & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  glbal_balance_unposted = glbal_balance_unposted - " & SetDbl(par_glt_value) & ",  " _
                                    & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(par_glt_value) & ",  " _
                                    & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        ''Return False
        ''Update laba tahun berjalan, kalau pendapatan (R) bertambah, kalau expense (E) berkurang..

        ''Update laba tahun berjalan, kalau pendapatan (R) bertambah, kalau expense (E) berkurang..
        'If _ac_type.ToUpper = "E" Or _ac_type.ToUpper = "R" Then
        '    Dim _ac_pl_cu_id, _dom_pl_ac As Integer
        '    Try
        '        Using objcb As New master_new.WDABasepgsql("", "")
        '            With objcb
        '                .Connection.Open()
        '                .Command = .Connection.CreateCommand
        '                .Command.CommandType = CommandType.Text

        '                .Command.CommandText = "select dom_pl_ac, ac_pl.ac_code as ac_code_pl, cu_pl.cu_id as ac_pl_cu_id " + _
        '                                       " from dom_mstr " + _
        '                                       " inner join ac_mstr ac_pl on ac_pl.ac_id = dom_pl_ac " + _
        '                                       " inner join cu_mstr cu_pl on cu_pl.cu_id = ac_pl.ac_cu_id " + _
        '                                       " where dom_id = " + master_new.ClsVar.sdom_id
        '                .InitializeCommand()
        '                .DataReader = .Command.ExecuteReader
        '                While .DataReader.Read
        '                    _ac_pl_cu_id = .DataReader("ac_pl_cu_id")
        '                    _dom_pl_ac = .DataReader("dom_pl_ac")
        '                End While
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try

        '    'If par_cu_id = master_new.ClsVar.ibase_cur_id Then
        '    '    If _ac_pl_cu_id <> par_cu_id Then
        '    '        par_glt_value = par_glt_value / func_data.get_exchange_rate(_ac_pl_cu_id)
        '    '    End If
        '    'Else
        '    '    If _ac_pl_cu_id <> par_cu_id Then
        '    '        par_glt_value = par_glt_value * par_exc_rate
        '    '    End If
        '    'End If dikarenakan sudah dikalikan kurs diatas '2011 - 02 - 05

        '    'Jadi untuk update ke laba tahun berjalan yang Expense selalu dikalikan -1 agar mengurangi laba tahun berjalan
        '    'semisal ada biaya sebesar 500 rb maka harus dikalikan -1 agar mengurangi laba
        '    'kalau ada jurnal balik sebesar -500 rb maka harus tetep dikalikan -1 agar menambah laba adjustment jurnal sebelumnya 
        '    'yang berarti laba tersebut tidak jadi transaksinya
        '    'untuk yang revenue sesuai transaksinya nilainya
        '    'semisal ada pendapatan 500 rb ya langsung aja update laba untuk menambah laba
        '    'kalau ada jurnal balik sebesar -500 rb yang langsung aja update laba untuk mengurangi laba, adjustment jurnal sebelumnya.
        '    If _ac_type.ToUpper = "E" Then
        '        par_glt_value = par_glt_value * -1
        '        'bys sys 20110212
        '        'If par_glt_value > 0 Then
        '        '    par_glt_value = par_glt_value * -1
        '        'End If
        '    ElseIf _ac_type.ToUpper = "R" Then
        '        'bys sys 20110212
        '        'If par_glt_value < 0 Then
        '        '    par_glt_value = par_glt_value * 1
        '        'End If
        '    End If

        '    _glbal_oid = "" 'dikosongkan lagi....kan dicari lagi coy

        '    Try
        '        Using objcb As New master_new.WDABasepgsql("", "")
        '            With objcb
        '                .Connection.Open()
        '                .Command = .Connection.CreateCommand
        '                .Command.CommandType = CommandType.Text

        '                .Command.CommandText = "select glbal_oid from glbal_balance" + _
        '                                       " where glbal_en_id = " + par_en_id.ToString + _
        '                                       " and glbal_ac_id = " + _dom_pl_ac.ToString + _
        '                                       " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
        '                .InitializeCommand()
        '                .DataReader = .Command.ExecuteReader
        '                While .DataReader.Read
        '                    _glbal_oid = .DataReader("glbal_oid").ToString
        '                End While
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try

        '    If _glbal_oid = "" Then
        '        With par_obj
        '            Try
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "INSERT INTO  " _
        '                                    & "  public.glbal_balance " _
        '                                    & "( " _
        '                                    & "  glbal_oid, " _
        '                                    & "  glbal_dom_id, " _
        '                                    & "  glbal_en_id, " _
        '                                    & "  glbal_add_by, " _
        '                                    & "  glbal_add_date, " _
        '                                    & "  glbal_gcal_oid, " _
        '                                    & "  glbal_ac_id, " _
        '                                    & "  glbal_sb_id, " _
        '                                    & "  glbal_cc_id, " _
        '                                    & "  glbal_cu_id, " _
        '                                    & "  glbal_balance_open, " _
        '                                    & "  glbal_balance_unposted, " _
        '                                    & "  glbal_balance_posted, " _
        '                                    & "  glbal_dt " _
        '                                    & ")  " _
        '                                    & "VALUES ( " _
        '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
        '                                    & SetInteger(par_en_id) & ",  " _
        '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
        '                                    & SetSetring(_gcal_oid.ToString) & ",  " _
        '                                    & SetInteger(_dom_pl_ac) & ",  " _
        '                                    & 0 & ",  " _
        '                                    & 0 & ",  " _
        '                                    & SetInteger(_ac_cu_id) & ",  " _
        '                                    & SetDbl(0) & ",  " _
        '                                    & SetDbl(0) & ",  " _
        '                                    & SetDbl(par_glt_value) & ",  " _
        '                                    & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
        '                                    & ")"
        '                par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()
        '            Catch ex As Exception
        '                MessageBox.Show(ex.Message)
        '                Return False
        '            End Try
        '        End With
        '    Else
        '        With par_obj
        '            Try
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "UPDATE  " _
        '                                    & "  public.glbal_balance " _
        '                                    & "SET  " _
        '                                    & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                    & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
        '                                    & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(par_glt_value) & ",  " _
        '                                    & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
        '                                    & "  " _
        '                                    & "WHERE  " _
        '                                    & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
        '                par_ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()
        '            Catch ex As Exception
        '                MessageBox.Show(ex.Message)
        '                Return False
        '            End Try
        '        End With
        '    End If
        'End If
    End Function
#End Region

    'Public Function insert_tranaprvd_det(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_tran_oid As String, ByVal par_tran_code As String, _
    '                                      ByVal par_date As Date) As Boolean
    '    insert_tranaprvd_det = True

    '    Dim dt_bantu As DataTable = New DataTable

    '    dt_bantu = (func_data.load_list_aprvd_dok(par_en_id, par_type, par_date))

    '    With par_obj
    '        Try
    '            .Command.CommandType = CommandType.Text
    '            .Command.CommandText = "delete from tranaprvd_dok where tranaprvd_tran_oid = '" + par_tran_oid + "'"
    '            par_ssqls.Add(.Command.CommandText)
    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()

    '            .Command.CommandType = CommandType.Text
    '            .Command.CommandText = "INSERT INTO  " _
    '                                & "  public.tranaprvd_dok " _
    '                                & "( " _
    '                                & "  tranaprvd_oid, " _
    '                                & "  tranaprvd_dom_id, " _
    '                                & "  tranaprvd_en_id, " _
    '                                & "  tranaprvd_add_by, " _
    '                                & "  tranaprvd_add_date, " _
    '                                & "  tranaprvd_dt, " _
    '                                & "  tranaprvd_tran_oid, " _
    '                                & "  tranaprvd_tran_code, " _
    '                                & "  tranaprvd_name_1, " _
    '                                & "  tranaprvd_pos_1, " _
    '                                & "  tranaprvd_name_2, " _
    '                                & "  tranaprvd_pos_2, " _
    '                                & "  tranaprvd_name_3, " _
    '                                & "  tranaprvd_pos_3, " _
    '                                & "  tranaprvd_name_4, " _
    '                                & "  tranaprvd_pos_4 " _
    '                                & ")  " _
    '                                & "VALUES ( " _
    '                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                & SetInteger(par_en_id) & ",  " _
    '                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
    '                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
    '                                & SetSetring(par_tran_oid) & ",  " _
    '                                & SetSetring(par_tran_code) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_1")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_1")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_2")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_2")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_3")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_3")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_4")) & ",  " _
    '                                & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_4")) & "  " _
    '                                & ")"
    '            par_ssqls.Add(.Command.CommandText)
    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()
    '        Catch ex As Exception
    '            'MessageBox.Show(ex.Message)
    '            Return False
    '        End Try
    '    End With
    'End Function

    Public Function insert_tranaprvd_det(ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_table As String, ByVal par_initial As String, ByVal par_tran_code_awal As String, ByVal par_tran_code_akhir As String, ByVal par_date As Date) As Boolean
        insert_tranaprvd_det = True

        Dim i As Integer
        Dim dt_bantu As DataTable = New DataTable
        Dim dt_data As DataTable = New DataTable

        dt_bantu = (func_data.load_list_aprvd_dok(par_en_id, par_type, par_date))
        dt_data = (func_data.load_list_aprvd_data(par_en_id, par_tran_code_awal, par_tran_code_akhir, par_initial, par_table))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To dt_data.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from tranaprvd_dok where tranaprvd_tran_oid = '" + dt_data.Rows(i).Item("data_oid").ToString + "'"
                            'par_ssqls.Add(.Command.CommandText) gak perlu karena tidak penting..untuk disinkronisasikan
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If dt_bantu.Rows.Count > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.tranaprvd_dok " _
                                                    & "( " _
                                                    & "  tranaprvd_oid, " _
                                                    & "  tranaprvd_dom_id, " _
                                                    & "  tranaprvd_en_id, " _
                                                    & "  tranaprvd_add_by, " _
                                                    & "  tranaprvd_add_date, " _
                                                    & "  tranaprvd_dt, " _
                                                    & "  tranaprvd_tran_oid, " _
                                                    & "  tranaprvd_tran_code, " _
                                                    & "  tranaprvd_name_1, " _
                                                    & "  tranaprvd_pos_1, " _
                                                    & "  tranaprvd_name_2, " _
                                                    & "  tranaprvd_pos_2, " _
                                                    & "  tranaprvd_name_3, " _
                                                    & "  tranaprvd_pos_3, " _
                                                    & "  tranaprvd_name_4, " _
                                                    & "  tranaprvd_pos_4 " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(par_en_id) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                    & SetSetring(dt_data.Rows(i).Item("data_oid")) & ",  " _
                                                    & SetSetring(dt_data.Rows(i).Item("data_code")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_1")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_1")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_2")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_2")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_3")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_3")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_4")) & ",  " _
                                                    & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_4")) & "  " _
                                                    & ")"
                                'par_ssqls.Add(.Command.CommandText)  gak perlu karena tidak penting..untuk disinkronisasikan
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                            
                        Next

                        ' gak perlu karena tidak penting..untuk disinkronisasikan
                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'End If

                        sqlTran.Commit()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function update_tranaprvd_det(ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_table As String, ByVal par_initial As String, ByVal par_tran_code_awal As String, ByVal par_tran_code_akhir As String, ByVal par_date As Date) As Boolean
        update_tranaprvd_det = True

        Dim i As Integer
        Dim dt_bantu As DataTable = New DataTable
        Dim dt_data As DataTable = New DataTable

        dt_bantu = (func_data.load_list_aprvd_dok(par_en_id, par_type, par_date))
        dt_data = (func_data.load_list_aprvd_data(par_en_id, par_tran_code_awal, par_tran_code_akhir, par_initial, par_table))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To dt_data.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.tranaprvd_dok   " _
                                                & "SET  " _
                                                & "  tranaprvd_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & "  tranaprvd_en_id = " & SetInteger(par_en_id) & ",  " _
                                                & "  tranaprvd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  tranaprvd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                & "  tranaprvd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                & "  tranaprvd_name_5 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_1")) & ",  " _
                                                & "  tranaprvd_name_6 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_2")) & ",  " _
                                                & "  tranaprvd_name_7 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_3")) & ",  " _
                                                & "  tranaprvd_name_8 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_4")) & ",  " _
                                                & "  tranaprvd_pos_5 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_1")) & ",  " _
                                                & "  tranaprvd_pos_6 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_2")) & ",  " _
                                                & "  tranaprvd_pos_7 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_3")) & ",  " _
                                                & "  tranaprvd_pos_8 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_4")) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  tranaprvd_tran_oid = " & SetSetring(dt_data.Rows(i).Item("data_oid").ToString) & " "
                            'par_ssqls.Add(.Command.CommandText) gak perlu karena tidak penting..untuk disinkronisasikan
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        ' gak perlu karena tidak penting..untuk disinkronisasikan
                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'End If

                        sqlTran.Commit()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    'Public Function update_tranaprvd_det(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_tran_oid As String, ByVal par_tran_code As String, _
    '                                      ByVal par_date As Date) As Boolean
    '    update_tranaprvd_det = True

    '    Dim dt_bantu As DataTable = New DataTable

    '    dt_bantu = (func_data.load_list_aprvd_dok(par_en_id, par_type, par_date))

    '    With par_obj
    '        Try
    '            .Command.CommandType = CommandType.Text
    '            .Command.CommandText = "UPDATE  " _
    '                                & "  public.tranaprvd_dok   " _
    '                                & "SET  " _
    '                                & "  tranaprvd_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                & "  tranaprvd_en_id = " & SetInteger(par_en_id) & ",  " _
    '                                & "  tranaprvd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                & "  tranaprvd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
    '                                & "  tranaprvd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
    '                                & "  tranaprvd_name_5 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_1")) & ",  " _
    '                                & "  tranaprvd_name_6 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_2")) & ",  " _
    '                                & "  tranaprvd_name_7 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_3")) & ",  " _
    '                                & "  tranaprvd_name_8 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_name_4")) & ",  " _
    '                                & "  tranaprvd_pos_5 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_1")) & ",  " _
    '                                & "  tranaprvd_pos_6 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_2")) & ",  " _
    '                                & "  tranaprvd_pos_7 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_3")) & ",  " _
    '                                & "  tranaprvd_pos_8 = " & SetSetringDB(dt_bantu.Rows(0).Item("aprvd_pos_4")) & "  " _
    '                                & "  " _
    '                                & "WHERE  " _
    '                                & "  tranaprvd_tran_oid = " & SetSetring(par_tran_oid.ToString) & " "
    '            par_ssqls.Add(.Command.CommandText)
    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()
    '        Catch ex As Exception
    '            'MessageBox.Show(ex.Message)
    '            Return False
    '        End Try
    '    End With
    'End Function

    Public Function get_id_tran_mstr(ByVal par_tran_name As String) As Integer
        get_id_tran_mstr = -1
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select tran_id from tran_mstr" _
                                         & " where tran_name ~~* '" + par_tran_name + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_id_tran_mstr = .DataReader.Item("tran_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_id_tran_mstr
    End Function

    Public Function get_domain(ByVal par_entity As Integer) As Integer
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select en_dom_id from en_mstr " + _
                                           " where en_id = " + par_entity.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_domain = .DataReader.Item("en_dom_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_domain
    End Function

    Public Function get_create_jurnal_status() As Boolean
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select create_jurnal from tconfsetting "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_create_jurnal_status = .DataReader.Item("create_jurnal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_create_jurnal_status
    End Function

    Public Function get_tanggal_sistem() As Date
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select LOCALTIMESTAMP as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_tanggal_sistem = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_tanggal_sistem
    End Function

    Private Function get_account_code(ByVal par_ac_id As Integer) As String
        get_account_code = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_code, ac_name from ac_mstr where ac_id = " + par_ac_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_account_code = .DataReader.Item("ac_code") + " " + .DataReader.Item("ac_name")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_account_code
    End Function

    Public Function get_now() As DateTime
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select LOCALTIMESTAMP as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_now = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_now
    End Function

    Public Function get_now_curr() As DateTime
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select CURRENT_DATE + TIME '11:00:00' as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_now_curr = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_now_curr
    End Function

    Public Function get_then() As DateTime
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select CURRENT_DATE - interval '1 days' + TIME '11:00:00' as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_then = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_then
    End Function

    'Public Function get_before_time() As DateTime
    '    Try
    '        Using objcek As New master_new.WDABasepgsql("", "")
    '            With objcek
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select LOCALTIMESTAMP - interval '1 DAY' as tanggal"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_before_time = .DataReader.Item("tanggal")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return get_before_time()
    'End Function

    Public Function get_transaction_number(ByVal par_type As String, ByVal par_entity As String, ByVal par_table As String, ByVal par_colom As String, ByVal par_date As Date) As String
        get_transaction_number = ""

        Dim tahun, bulan, no_urut_format As String

        Dim tanggal As Date
        tanggal = par_date 'get_tanggal_sistem()
        tahun = tanggal.Year.ToString.Substring(2, 2)
        bulan = tanggal.Month.ToString
        no_urut_format = ""

        If Len(bulan) = 1 Then
            bulan = "0" + bulan
        End If

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    '.SQL = "select coalesce(max(cast(substring(" + par_colom + ",7,10) as integer)),0) + 1 as urut " + _
                    '       " from " + par_table + _
                    '       " where substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                    '       " limit 1"
                    .SQL = "select coalesce(max(cast(substring(" + par_colom + ",9,7) as integer)),0) + 1 as no_urut " + _
                           " from " + par_table + _
                           " where substring(" + par_colom + ",3,2) = '" + par_entity + "'" + _
                           " and substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                           " and substring(" + par_colom + ",7,2) = '" + bulan + "'" + _
                           " and length(" + par_colom + ") = 15 " + _
                           " limit 1"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    no_urut_format = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If Len(no_urut_format) = 1 Then
            no_urut_format = "000000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 2 Then
            no_urut_format = "00000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 3 Then
            no_urut_format = "0000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 4 Then
            no_urut_format = "000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 5 Then
            no_urut_format = "00" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 6 Then
            no_urut_format = "0" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 7 Then
            no_urut_format = no_urut_format.ToString
        End If

        get_transaction_number = par_type + par_entity + tahun + bulan + no_urut_format
        'RV501012
        Return get_transaction_number
    End Function

    Public Function get_simple_transaction_number(ByVal par_type As String, ByVal par_entity As String, ByVal par_table As String, ByVal par_colom As String, ByVal par_date As Date) As String
        get_simple_transaction_number = ""

        Dim tahun, bulan, no_urut_format As String

        Dim tanggal As Date
        tanggal = par_date 'get_tanggal_sistem()
        tahun = tanggal.Year.ToString.Substring(2, 2)
        bulan = tanggal.Month.ToString
        no_urut_format = ""

        If Len(bulan) = 1 Then
            bulan = "0" + bulan
        End If

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    '.SQL = "select coalesce(max(cast(substring(" + par_colom + ",7,10) as integer)),0) + 1 as urut " + _
                    '       " from " + par_table + _
                    '       " where substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                    '       " limit 1"
                    .SQL = "select coalesce(max(cast(substring(" + par_colom + ",9,7) as integer)),0) + 1 as no_urut " + _
                           " from " + par_table + _
                           " where substring(" + par_colom + ",3,2) = '" + par_entity + "'" + _
                           " and substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                           " and substring(" + par_colom + ",7,2) = '" + bulan + "'" + _
                           " and length(" + par_colom + ") = 15 " + _
                           " limit 1"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    no_urut_format = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If Len(no_urut_format) = 1 Then
            no_urut_format = "000000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 2 Then
            no_urut_format = "00000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 3 Then
            no_urut_format = "0000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 4 Then
            no_urut_format = "000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 5 Then
            no_urut_format = "00" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 6 Then
            no_urut_format = "0" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 7 Then
            no_urut_format = no_urut_format.ToString
        End If

        get_simple_transaction_number = par_type + par_entity + tahun + bulan + no_urut_format
        'RV501012
        Return get_simple_transaction_number
    End Function

    'by Rana 01/11/2022 untuk SQ Alocated
    Public Function get_notime() As DateTime
        Dim d As DateTime? = Nothing
        Dim boolNotSet As Boolean = d.HasValue

        'Dim dt As System.Nullable(Of DateTime)
        'dt = Nothing

        'Dim GetDate As DateTime
        'GetDate = get_now() 'put the date you want to increment it 2 business days
        'Dim numDays As Integer
        'numDays = 2
        'Dim totalDays As Integer
        'Dim businessDays As Integer
        'Dim currDate As DateTime
        'totalDays = 0
        'businessDays = 0
        'While businessDays < numDays
        '    totalDays += 1
        '    currDate = GetDate.AddDays(totalDays)
        '    If Not (currDate.DayOfWeek = DayOfWeek.Saturday) Then
        '        businessDays += 1
        '    End If
        'End While
        ''to check if the date after increment the 2 work days are less than the day of now 
        'If currDate < Date.Now Then
        '    'date is expired
        'Else
        '    'date is not expired yet
        'End If

        'Dim DOB As Nullable(Of Date) = Date.Now
        'Dim datestring As String = DOB.Value.ToString("d")
        'DOB = Nothing



        'Try
        '    Using objcek As New master_new.WDABasepgsql("", "")
        '        With objcek
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select LOCALTIMESTAMP + interval '72 hours' as tanggal"
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                get_notime = .DataReader.Item("tanggal")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        'Return get_then



    End Function

    Public Function get_transaction_number(ByVal par_type As String, ByVal par_entity As String, ByVal par_table As String, ByVal par_colom As String) As String
        get_transaction_number = ""

        Dim tahun, bulan, no_urut_format As String
        Dim _no_urut As Double

        Dim tanggal As Date
        tanggal = get_tanggal_sistem()

        tahun = tanggal.Year.ToString.Substring(2, 2)
        bulan = tanggal.Month.ToString
        no_urut_format = ""

        If Len(bulan) = 1 Then
            bulan = "0" + bulan
        End If

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "select coalesce(max(cast(substring(" + par_colom + ",11,5) as integer)),0) + 1 as no_urut " + _
                           " from " + par_table + _
                           " where substring(" + par_colom + ",3,2) = '" + par_entity + "'" + _
                           " and substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                           " and substring(" + par_colom + ",7,2) = '" + bulan + "'" + _
                           " and length(" + par_colom + ")=18 limit 1"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    _no_urut = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'SO 54 10 06 00 00010
        'SO 54 10 06 00 000 00010
        '3,2    54
        '5,2    10
        '7,2    06
        '11,7   00010   ==>> 11,3 
        '14,
        'SO 33 11 04 07 00130

        'If Len(no_urut_format) = 1 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "0000" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 2 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "000" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 3 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "00" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 4 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "0" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 5 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + no_urut_format.ToString
        'End If
        Dim _id_new As Integer = 0
        Dim Generator As System.Random = New System.Random()
        _id_new = Generator.Next(1, 999)

        Dim _no_id As String = Format(_id_new, "000")

        no_urut_format = Format(_no_urut, "00000")

        get_transaction_number = par_type + par_entity + tahun + bulan + master_new.ClsVar.sServerCode + no_urut_format + _no_id

        Return get_transaction_number
        'get_transaction_number = ""

        'Dim tahun, bulan, no_urut_format As String

        'Dim tanggal As Date
        'tanggal = get_tanggal_sistem()

        'tahun = tanggal.Year.ToString.Substring(2, 2)
        'bulan = tanggal.Month.ToString
        'no_urut_format = ""

        'If Len(bulan) = 1 Then
        '    bulan = "0" + bulan
        'End If

        'Try
        '    Dim ds_bantu As New DataSet
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb

        '            .SQL = "select coalesce(max(cast(substring(" + par_colom + ",11,7) as integer)),0) + 1 as no_urut " + _
        '                   " from " + par_table + _
        '                   " where substring(" + par_colom + ",3,2) = '" + par_entity + "'" + _
        '                   " and substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
        '                   " and substring(" + par_colom + ",7,2) = '" + bulan + "'" + _
        '                   " limit 1"
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "transactionnumber")
        '            no_urut_format = ds_bantu.Tables(0).Rows(0).Item("no_urut")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try


        'If Len(no_urut_format) = 1 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "0000" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 2 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "000" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 3 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "00" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 4 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + "0" + no_urut_format.ToString
        'ElseIf Len(no_urut_format) = 5 Then
        '    no_urut_format = master_new.ClsVar.sServerCode + no_urut_format.ToString
        'End If

        'get_transaction_number = par_type + par_entity + tahun + bulan + no_urut_format

        'Return get_transaction_number
    End Function

    Public Function get_conf_file(ByVal par_type As String) As String
        get_conf_file = ""
        Try
            Dim dr As DataRow
            Dim ssql As String
            ssql = "select conf_value from conf_file " + _
                    " where conf_name = '" + par_type + "'"

            dr = master_new.PGSqlConn.GetRowInfo(ssql)
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
            Dim url As String = get_conf_file("http_approval_online")
            If url = "" Then
                Box("http_approval_online has not been Configured")
                Return ""
            Else
                Return url
            End If
        Else
            Return get_conf_file("http_approval")
        End If
    End Function

    Public Function get_http_server_api() As String
        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
            Dim url As String = get_conf_file("server_api_online")
            If url = "" Then
                Box("http_approval_online has not been Configured")
                Return ""
            Else
                Return url
            End If
        Else
            Try
                Dim ssql As String = "select xmpp_ip from tconfsetting limit 1"
                Dim dt_serv As New DataTable
                dt_serv = master_new.PGSqlConn.GetTableData(ssql)
                Dim _serv_code As String = ""
                For Each dr_serv As DataRow In dt_serv.Rows
                    _serv_code = dr_serv(0).ToString
                Next
                If _serv_code = "192.168.1.152" Then
                    Return get_conf_file("server_api_local")
                Else
                    Return get_conf_file("server_api_online")
                End If
            Catch ex As Exception
                Return get_conf_file("server_api_local")
            End Try
            

        End If
    End Function

    Public Function get_wf_iscurrent(ByVal par_code As String) As String
        get_wf_iscurrent = ""
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_user_id from wf_mstr " + _
                                           " where wf_ref_code = '" + par_code + "'" + _
                                           " and wf_iscurrent ~~* 'Y' "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_wf_iscurrent = .DataReader.Item("wf_user_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_wf_iscurrent
    End Function

    Public Function update_inv_wip(ByVal par_obj As Object, ByVal par_wc_id As Integer, _
                                 ByVal par_pt_id As String, ByVal par_en_id As String, _
                                 ByVal par_wo_oid As String, ByVal par_qty As Double) As Boolean
        update_inv_wip = True
        Dim _invw_oid As String = ""
        Dim _qty As Double = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT  " _
                                    & "  invw_oid,invw_qty " _
                                    & "FROM  " _
                                    & "  public.invw_wip  " _
                                    & "WHERE " _
                                    & "  invw_wc_id = " & par_wc_id & " and " _
                                    & "  invw_pt_id = " & par_pt_id & " and " _
                                    & "  invw_en_id = " & par_en_id & " and " _
                                    & "  invw_dom_id = " & master_new.ClsVar.sdom_id & " and " _
                                    & "  invw_wo_oid = '" & par_wo_oid & "'"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _invw_oid = .DataReader("invw_oid").ToString
                        _qty = .DataReader("invw_qty")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
            Exit Function
        End Try


        If _invw_oid = "" Then
            If par_qty < 0 Then
                Box("Inventory in this work center lower than transaction")
                Return False
                Exit Function
            Else
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invw_wip " _
                                        & "( " _
                                        & "  invw_oid, " _
                                        & "  invw_dom_id, " _
                                        & "  invw_en_id, " _
                                        & "  invw_wo_oid, " _
                                        & "  invw_wc_id, " _
                                        & "  invw_pt_id, " _
                                        & "  invw_qty " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetSetring(par_wo_oid) & ",  " _
                                        & SetInteger(par_wc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDec(par_qty) & "  " _
                                        & ")"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                        Exit Function
                    End Try
                End With
            End If


        Else
            If par_qty < 0 Then
                If (par_qty * -1) > _qty Then
                    Box("Inventory in this work center lower than transaction")
                    Return False
                    Exit Function
                Else
                    With par_obj
                        Try
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                            & "  public.invw_wip   " _
                                            & "SET  " _
                                            & "  invw_qty = invw_qty + " & SetDec(par_qty) & "  " _
                                            & "WHERE  " _
                                            & "  invw_oid = '" & _invw_oid & "' "
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                            Exit Function
                        End Try
                    End With
                End If
            Else
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                        & "  public.invw_wip   " _
                                        & "SET  " _
                                        & "  invw_qty = invw_qty + " & SetDec(par_qty) & "  " _
                                        & "WHERE  " _
                                        & "  invw_oid = '" & _invw_oid & "' "
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                        Exit Function
                    End Try
                End With
            End If

        End If
        Return True
    End Function

    Public Function update_item_cost_avg(ByVal par_obj As Object, ByVal par_type As String, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_pt_id As Integer, ByVal par_qty As Double, ByVal par_cost As Double) As Boolean
        update_item_cost_avg = True
        Dim _invc_qty_sum, _pt_cost, _avg_cost As Double

        'Mencari sum dari inventory dari item yang bersangkutan
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(sum(invc_qty),0) as invc_qty_sum From invc_mstr where invc_pt_id = " + par_pt_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _invc_qty_sum = .DataReader("invc_qty_sum").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'Mencari cost dari item bersangkutan
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(pt_cost,0) as pt_cost from pt_mstr where pt_id = " + par_pt_id.ToString + _
                                           " and pt_en_id in (0," + par_en_id.ToString + ")"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pt_cost = .DataReader("pt_cost").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'rumus ((qty stock x cost average) + (po_cost * po_qty)) / (qty stock + po_qty)
        Dim par1, par2 As Double
        If par_type = "+" Then
            _avg_cost = ((_invc_qty_sum * _pt_cost) + (par_cost * par_qty)) / (_invc_qty_sum + par_qty)
        ElseIf par_type = "-" Then
            par1 = ((_invc_qty_sum * _pt_cost) - (par_cost * par_qty))
            par2 = (_invc_qty_sum - par_qty)
            If par1 = 0 And par2 = 0 Then
                _avg_cost = 0
            Else
                _avg_cost = par1 / par2
            End If
            '_avg_cost = ((_invc_qty_sum * _pt_cost) - (par_cost * par_qty)) / (_invc_qty_sum - par_qty)
        End If


        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                '.Command.CommandText = "update pt_mstr set pt_cost = " + par_cost.ToString + _
                '                       " where pt_id = " + par_pt_id.ToString + _
                '                       " and pt_en_id = " + par_en_id.ToString
                .Command.CommandText = "update pt_mstr set pt_cost = " + _avg_cost.ToString + _
                                       " where pt_id = " + par_pt_id.ToString + _
                                       " and pt_en_id = " + par_en_id.ToString
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Public Function update_invct_table_minus(ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_pt_id As Integer, ByVal par_qty As Double, _
                                        ByVal par_type As String) As Boolean
        update_invct_table_minus = True

        Dim ds_bantu As New DataSet
        Dim i As Integer
        Dim _qty As Double

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from invct_table" _
                         & " where invct_dom_id = " & master_new.ClsVar.sdom_id _
                         & " and invct_en_id = " + par_en_id.ToString _
                         & " and invct_pt_id =  " + par_pt_id.ToString
                    If par_type = "F" Then
                        .SQL = .SQL + " order by invct_date asc"
                    ElseIf par_type = "L" Then
                        .SQL = .SQL + " order by invct_date desc"
                    End If

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "invct_table")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        _qty = par_qty
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If _qty >= ds_bantu.Tables(0).Rows(i).Item("invct_qty") Then
                With par_obj
                    Try
                        _qty = _qty - ds_bantu.Tables(0).Rows(i).Item("invct_qty")
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from invct_table where invct_oid = '" + ds_bantu.Tables(0).Rows(i).Item("invct_oid").ToString + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            ElseIf _qty < ds_bantu.Tables(0).Rows(i).Item("invct_qty") Then
                With par_obj
                    Try
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update invct_table " + _
                                               " set invct_qty = invct_qty - " + _qty.ToString + _
                                               " where invct_oid = '" + ds_bantu.Tables(0).Rows(i).Item("invct_oid").ToString + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        Exit For
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        Next

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "delete from invct_table where invct_qty = 0"
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

    End Function

    

#Region "Budget"
    Public Function update_budget(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_date As Date) As Boolean
        update_budget = True
        Dim i, _pl_id As Integer
        Dim dt_bantu As DataTable
        Dim _cost As Double

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("pod_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = get_prodline(par_ds.Tables(0).Rows(i).Item("pod_pt_id"))

                        If IsDBNull(par_ds.Tables(0).Rows(i).Item("pod_memo")) = True Then
                            dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                        ElseIf Trim(par_ds.Tables(0).Rows(i).Item("pod_memo")) = "M" Then
                            dt_bantu = (get_prodline_account(_pl_id, "PRC_PACC"))
                        Else
                            dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                        End If

                        _cost = (par_ds.Tables(0).Rows(i).Item("pod_cost") - _
                                (par_ds.Tables(0).Rows(i).Item("pod_cost") * par_ds.Tables(0).Rows(i).Item("pod_disc"))) _
                                * par_ds.Tables(0).Rows(i).Item("pod_qty")

                        Dim _bdgt_oid As String = ""
                        _bdgt_oid = get_bdgt_oid(par_ds.Tables(0).Rows(i).Item("pod_cc_id"))

                        If get_budget(_bdgt_oid, dt_bantu.Rows(0).Item("pla_ac_id"), par_date) = False Then
                            MessageBox.Show("Budget Tidak Tersedia..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            update_budget = False
                            Exit Function
                        End If

                        Dim _sisa_budget As Double = 0
                        Try
                            Using objcb As New master_new.WDABasepgsql("", "")
                                With objcb
                                    .Connection.Open()
                                    .Command = .Connection.CreateCommand
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "select (bdgtd_budget - ((coalesce(bdgtd_alokasi,0)) + (coalesce(bdgtd_realisasi,0)))) as sisa_budget " _
                                                         & " from bdgtd_det " _
                                                         & " where bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
                                                         & "  and bdgtd_ac_id = " & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & "  " _
                                                         & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
                                                          & " where bdgtp_start_date <= " + SetDate(par_date) _
                                                          & " and bdgtp_end_date >= " + SetDate(par_date) _
                                                          & " ) " _
                                                         & ";"
                                    .InitializeCommand()
                                    .DataReader = .Command.ExecuteReader
                                    While .DataReader.Read
                                        _sisa_budget = .DataReader("sisa_budget")
                                    End While
                                End With
                            End Using
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        '=========================================================
                        Dim _acc_cek_budget As String
                        _acc_cek_budget = acc_cek_budget(dt_bantu.Rows(0).Item("pla_ac_id"))
                        If _acc_cek_budget = "Y" Then
                            If _sisa_budget < _cost Then
                                MessageBox.Show("Cost PO Lebih Besar Dari Budget Yang Tersedia,,! Silahkan Lakukan Cross Budget Terlebih Dahulu,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                update_budget = False
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                        '=========================================================

                        'Update bdgtd_det Set Alokasi nya
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.bdgtd_det   " _
                                            & "SET  " _
                                            & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bdgtd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                                            & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(_cost) & ",  " _
                                            & "  bdgtd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
                                            & "  and bdgtd_ac_id = " & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & "  " _
                                            & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
                                              & " ) " _
                                            & ";"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Public Function get_budget(ByVal par_bdgt_oid As String, ByVal par_ac_id As Integer, ByVal par_date As Date) As Boolean
        get_budget = False
        Dim _sisa_budget As Double = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bdgtd_budget " _
                                         & " from bdgtd_det " _
                                         & " inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                                         & " where bdgtd_bdgt_oid = " & SetSetring(par_bdgt_oid.ToString()) & "  " _
                                         & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
                                         & "  and bdgt_trans_id = 'I' and bdgt_active = 'Y' " _
                                         & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
                                                              & " where bdgtp_start_date <= " + SetDate(par_date) _
                                                              & " and bdgtp_end_date >= " + SetDate(par_date) _
                                                              & " ) " _
                                         & ";"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_budget = True
                        Return get_budget
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_budget
    End Function

    Public Function acc_cek_budget(ByVal par_ac_id As Integer) As String
        acc_cek_budget = "N"
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_is_budget from ac_mstr " + _
                                           " where ac_id = " + par_ac_id.ToString()
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        acc_cek_budget = .DataReader.Item("ac_is_budget")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return acc_cek_budget
    End Function

    Public Function get_bdgt_oid(ByVal par_cc_id As Integer) As String
        get_bdgt_oid = ""

        Dim _bdgt_oid As String = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bdgt_oid " _
                                            & " from bdgt_mstr " _
                                            & " WHERE bdgt_cc_id = " & SetInteger(par_cc_id) & "  " _
                                            & " and bdgt_active = 'Y' "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _bdgt_oid = .DataReader.Item("bdgt_oid").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_bdgt_oid = _bdgt_oid

        Return get_bdgt_oid
    End Function

    Public Function update_budget_po_receipt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                            ByVal par_en_id As Integer, ByVal par_date As Date) As Boolean
        update_budget_po_receipt = True
        Dim i, _pl_id As Integer
        Dim dt_bantu As DataTable
        Dim _cost As Double

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = get_prodline(par_ds.Tables(0).Rows(i).Item("pod_pt_id"))

                        If Trim(par_ds.Tables(0).Rows(i).Item("pod_memo")) = "" Then
                            dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                        Else
                            dt_bantu = (get_prodline_account(_pl_id, "PRC_PACC"))
                        End If

                        Dim _po_disc, _po_cost, _qty_receipt As Double
                        _po_cost = IIf(IsDBNull(par_ds.Tables(0).Rows(i).Item("pod_cost")) = True, 0, par_ds.Tables(0).Rows(i).Item("pod_cost"))
                        _po_disc = IIf(IsDBNull(par_ds.Tables(0).Rows(i).Item("pod_disc")) = True, 0, par_ds.Tables(0).Rows(i).Item("pod_disc"))
                        _qty_receipt = IIf(IsDBNull(par_ds.Tables(0).Rows(i).Item("rcvd_qty")) = True, 0, par_ds.Tables(0).Rows(i).Item("rcvd_qty"))
                        _cost = (_po_cost - (_po_cost * _po_disc)) * _qty_receipt

                        Dim _bdgt_oid As String = ""
                        _bdgt_oid = get_bdgt_oid(par_ds.Tables(0).Rows(i).Item("pod_cc_id"))

                        'Update bdgtd_det Set Realisasi nya
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.bdgtd_det   " _
                                            & "SET  " _
                                            & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bdgtd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                                            & "  bdgtd_alokasi = bdgtd_alokasi - " & SetDbl(_cost) & ",  " _
                                            & "  bdgtd_realisasi = bdgtd_realisasi + " & SetDbl(_cost) & ",  " _
                                            & "  bdgtd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
                                            & "  and bdgtd_ac_id = " & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & "  " _
                                            & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
                                                                      & " where bdgtp_start_date <= " + SetDate(par_date) _
                                                                      & " and bdgtp_end_date >= " + SetDate(par_date) _
                                                                      & " ) " _
                                            & ";"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Public Function update_budget_po_return(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                            ByVal par_en_id As Integer) As Boolean
        update_budget_po_return = True
        Dim i, _pl_id As Integer
        Dim dt_bantu, dt_bantu_2 As DataTable
        Dim _inv_cost, _pod_cost As Double
        Dim _ret_cost As Double

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("rcvd_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id"))

                        dt_bantu_2 = New DataTable
                        _pod_cost = get_cost_pod_det(par_ds.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString)
                        _inv_cost = get_inv_cost(par_ds.Tables(0).Rows(i).Item("pod_po_oid").ToString, par_ds.Tables(0).Rows(i).Item("pt_id"))

                        If _inv_cost > 0 Then
                            _pod_cost = _inv_cost
                        End If

                        If IsDBNull(par_ds.Tables(0).Rows(i).Item("pod_memo")) = True Then
                            dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                        ElseIf Trim(par_ds.Tables(0).Rows(i).Item("pod_memo")) = "" Then
                            dt_bantu = (get_prodline_account(_pl_id, "INV_ACCT"))
                        Else
                            dt_bantu = (get_prodline_account(_pl_id, "PRC_PACC"))
                        End If

                        _ret_cost = _pod_cost * par_ds.Tables(0).Rows(i).Item("rcvd_qty")

                        Dim _bdgt_oid As String = ""
                        _bdgt_oid = get_bdgt_oid(par_ds.Tables(0).Rows(i).Item("pod_cc_id"))

                        Dim _po_date As Date
                        _po_date = par_ds.Tables(0).Rows(i).Item("po_date")

                        'Update bdgtd_det Set Realisasi nya
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.bdgtd_det   " _
                                            & "SET  " _
                                            & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bdgtd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                                            & "  bdgtd_realisasi = bdgtd_realisasi - " & SetDbl(_ret_cost) & ",  " _
                                            & "  bdgtd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
                                            & "  and bdgtd_ac_id = " & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & "  " _
                                            & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
                                            & " where bdgtp_start_date <= " + SetDate(_po_date) _
                                            & " and bdgtp_end_date >= " + SetDate(_po_date) _
                                            & " ) " _
                                            & ";"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

    End Function
#End Region

#Region "Belum Dipakai"
    'Public Function get_trans_status(ByVal par_tran_id As Integer, ByVal par_trane_seq As Integer) As String
    '    get_trans_status = ""
    '    Dim tran_oid As String = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select tran_oid from tran_mstr " + _
    '                                       " where tran_id = '" + par_tran_id.ToString + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    tran_oid = .DataReader.Item("tran_oid")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select trane_trans_id from trane_entity " + _
    '                                       " where trane_tran_oid = '" + tran_oid + "'" + _
    '                                       " and trane_seq = " + par_trane_seq.ToString
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_trans_status = .DataReader.Item("trane_trans_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return get_trans_status
    'End Function

    'Public Function get_type_approval(ByVal par_account_id As Integer) As Integer
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select z_trana_tran_id from z_trana_acct " + _
    '                                       " where z_trana_ac_id =" + par_account_id.ToString
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_type_approval = .DataReader.Item("z_trana_tran_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_type_approval
    'End Function

    'Public Function get_nama_approval(ByVal par_cc_id As Integer, ByVal par_aprv_user_id As String) As String
    '    get_nama_approval = par_aprv_user_id

    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select k.nama_depan as koordinator, m.nama_depan as manager,  " + _
    '                                       " v.nama_depan as vp, d.nama_depan as director " + _
    '                                       " from z_level " + _
    '                                       " left outer join tkaryawan k on k.id_karyawan = level_koordinator " + _
    '                                       " left outer join tkaryawan m on m.id_karyawan = level_manager " + _
    '                                       " left outer join tkaryawan v on v.id_karyawan = level_vp " + _
    '                                       " left outer join tkaryawan d on d.id_karyawan = level_director " + _
    '                                       " where level_cc_id = " + par_cc_id.ToString
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    If par_aprv_user_id = "{Koordinator}" Then
    '                        get_nama_approval = .DataReader.Item("koordinator")
    '                    ElseIf par_aprv_user_id = "{Manager}" Then
    '                        get_nama_approval = .DataReader.Item("manager")
    '                    ElseIf par_aprv_user_id = "{VP}" Then
    '                        get_nama_approval = .DataReader.Item("vp")
    '                    ElseIf par_aprv_user_id = "{Director}" Then
    '                        get_nama_approval = .DataReader.Item("director")
    '                    End If
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    'End If

    '    Return get_nama_approval
    'End Function

    'Public Function get_transaction_status_pd(ByVal par_pd_code As String) As String
    '    get_transaction_status_pd = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select pd_trn_status from pd_mstr  " + _
    '                                       " where pd_code ~~* '" + par_pd_code + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_transaction_status_pd = .DataReader.Item("pd_trn_status")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_transaction_status_pd
    'End Function

    'Public Function get_transaction_status_inv(ByVal par_arinv_code As String) As String
    '    get_transaction_status_inv = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select arinv_trans_id from arinv_mstr  " + _
    '                                       " where arinv_code ~~* '" + par_arinv_code + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_transaction_status_inv = .DataReader.Item("arinv_trans_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_transaction_status_inv
    'End Function

    'Public Function get_status_wf(ByVal par_pd_code As String) As String
    '    get_status_wf = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select wf_wfs_id from wf_mstr  " + _
    '                                       " where wf_ref_code = '" + par_pd_code + "'" + _
    '                                       " and wf_seq = 1"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_status_wf = .DataReader.Item("wf_wfs_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_status_wf
    'End Function

    'Public Function get_user_wf(ByVal par_pd_code As String, ByVal par_wf_seq As Integer) As String
    '    get_user_wf = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select wf_user_id from wf_mstr  " + _
    '                                       " where wf_ref_code ~~* '" + par_pd_code + "'" + _
    '                                       " and wf_seq = " + par_wf_seq.ToString
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_user_wf = .DataReader.Item("wf_user_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_user_wf
    'End Function

    'Public Function get_email_address(ByVal par_nama As String) As String
    '    get_email_address = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select email_perusahaan From tkaryawan  " + _
    '                                       " where nama_depan ~~* '" + par_nama + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_email_address = .DataReader.Item("email_perusahaan")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_email_address
    'End Function

    'Public Function sent_email(ByVal par_to As String, ByVal par_cc As String, ByVal par_subject As String, ByVal par_body As String, ByVal par_from As String, ByVal par_attachment As String) As Boolean
    '    If par_cc = "" Then
    '        par_cc = "setiadi.sudrajat@hariff.com"
    '    End If

    '    sent_email = True
    '    Try
    '        Dim message As New MailMessage
    '        message.From = New MailAddress(par_from)
    '        message.To.Add(par_to)

    '        If par_cc <> "" Then
    '            message.CC.Add(par_cc)
    '        End If

    '        message.Subject = par_subject
    '        message.Body = par_body


    '        If par_attachment <> "" Then
    '            Dim attachment = New Attachment(par_attachment)
    '            message.Attachments.Add(attachment)
    '        End If

    '        Dim emailClient As New SmtpClient("mail.hariff.com")
    '        Dim SMTPUserInfo As New System.Net.NetworkCredential("syspro@hariff.com", "sysprohariff")
    '        emailClient.UseDefaultCredentials = False
    '        emailClient.Credentials = SMTPUserInfo
    '        emailClient.Send(message)
    '    Catch ex As Exception
    '        sent_email = False
    '        MessageBox.Show(ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '    End Try
    'End Function

    'Public Function get_nama_karyawan(ByVal id_karyawan As Integer) As String
    '    get_nama_karyawan = ""
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select nama_depan From tkaryawan  " + _
    '                                       " where id_karyawan  = " + id_karyawan.ToString
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_nama_karyawan = .DataReader.Item("nama_depan")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_nama_karyawan
    'End Function

    'Public Function get_pd_trn_status(ByVal par_no As String) As String
    '    get_pd_trn_status = ""

    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select pd_trn_status From pd_mstr  " + _
    '                                       " where pd_code  = '" + par_no + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    get_pd_trn_status = .DataReader.Item("pd_trn_status")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return get_pd_trn_status
    'End Function

    'Public Function cek_restrict_approve(ByVal par_pd_code As String, ByVal wf_user_id As String) As String
    '    cek_restrict_approve = ""

    '    Dim wf_seq As Integer
    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select wf_seq From wf_mstr  " + _
    '                                       " where wf_user_id ~~* '" + wf_user_id + "'" + _
    '                                       " and wf_ref_code ~~* '" + par_pd_code + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    wf_seq = .DataReader.Item("wf_seq")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    wf_seq += 1

    '    Try
    '        Using objkalendar As New master_new.WDABasepgsql("", "")
    '            With objkalendar
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select wf_wfs_id From wf_mstr  " + _
    '                                       " where wf_seq = " + wf_seq.ToString + _
    '                                       " and wf_ref_code ~~* '" + par_pd_code + "'"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    cek_restrict_approve = .DataReader.Item("wf_wfs_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return cek_restrict_approve
    'End Function

    'Public Function format_email(ByVal par_nama As String, ByVal par_no As String, ByVal par_type As String) As String
    '    format_email = ""
    '    Dim wf_seq As Integer = 0
    '    Dim wf_user_id As String = "", nik As String = "", pwd As String = ""
    '    If par_type = "inetpd" Then
    '        Try
    '            Using objkalendar As New master_new.WDABasepgsql("", "")
    '                With objkalendar
    '                    .Connection.Open()
    '                    .Command = .Connection.CreateCommand
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "select nik, password from tkaryawan k " + _
    '                                           " inner join tconfuser u on u.id_karyawan = k.id_karyawan  " + _
    '                                           " where nama_depan ~~* '" + par_nama + "'"
    '                    .InitializeCommand()
    '                    .DataReader = .Command.ExecuteReader
    '                    While .DataReader.Read
    '                        nik = .DataReader.Item("nik")
    '                        pwd = .DataReader.Item("password")
    '                    End While
    '                    format_email = "*" + nik + "*" + pwd + "*" + par_type + "*" + par_no + "*A#"
    '                End With
    '            End Using
    '        Catch ex As Exception
    '        End Try
    '    ElseIf par_type = "inv" Then
    '        format_email = "Invoice Number : " + par_no + " Mohon Untuk Dilakukan Approval..."
    '    End If


    '    Return format_email
    'End Function

    'Public Function title_email(ByVal par_title As String) As String
    '    title_email = ""

    '    If par_title = "pd" Then
    '        title_email = "Email Gateway - Permohonan Dana"
    '    ElseIf par_title = "inv" Then
    '        title_email = "Email Gateway - Invoice"
    '    End If

    '    Return title_email
    'End Function

    'Public Function petunjuk() As String
    '    petunjuk = ""
    '    petunjuk = vbCrLf + vbCrLf + "The approval procedure can be done by using the following methodes (choose only one): "
    '    petunjuk = petunjuk + vbCrLf + "1. By SYSPRO Application, or"
    '    petunjuk = petunjuk + vbCrLf + "2. Reply This Email"
    '    petunjuk = petunjuk + vbCrLf + vbCrLf + "A = Approve, H = Hold, X = Cancel"
    '    Return petunjuk
    'End Function
#End Region

    '13/06/2011 | Function untuk SO restriction close periode jurnal untuk semua transaksi
    Public Function get_menu_status(ByVal par_userid As Integer, ByVal form_name As String) As Boolean
        get_menu_status = False
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(cancelablestatus,false) as cancelablestatus from tconfmenu " + _
                                           " where groupid in (select groupid from tconfusergroup where userid = " + par_userid.ToString + ")" + _
                                           " and menuid in (select menuid from tconfmenucollection where menuname ~~* " + SetSetring(form_name) + ") " _
                                           & " UNION  " _
                                            & "SELECT  " _
                                            & "   coalesce(cancelablestatus,false) as cancelablestatus  " _
                                            & "FROM " _
                                            & "  public.tconfmenuuser a " _
                                            & "  INNER JOIN public.tconfmenucollection b ON (a.menuid = b.menuid) " _
                                            & " WHERE " _
                                            & "  a.userid = " & master_new.ClsVar.sUserID.ToString _
                                            & " and a.menuid in (select menuid from tconfmenucollection where menuname ~~* " + SetSetring(form_name) + ") " _
                                            & " order by cancelablestatus"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_menu_status = .DataReader("cancelablestatus")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_menu_status
    End Function
    Public Function get_ass_code(ByVal par_pt_code As String, ByVal par_type As String, ByVal par_table As String, ByVal par_colom As String, ByVal par_i As Integer) As String
        get_ass_code = ""

        Dim tahun, bulan, no_urut_format As String
        Dim tanggal As Date
        tanggal = get_tanggal_sistem()
        tahun = tanggal.Year.ToString.Substring(2, 2)
        bulan = tanggal.Month.ToString
        no_urut_format = ""

        If Len(bulan) = 1 Then
            bulan = "0" + bulan
        End If

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select coalesce(max(cast(substring(" + par_colom + ",13,5) as integer)),0) as no_urut " + _
                           " from " + par_table + _
                           " where substring(" + par_colom + ",3,2) = '" + tahun + "'" + _
                           " and substring(" + par_colom + ",6,2) = '" + bulan + "'" + _
                           " and length(" + par_colom + ") = 16 " + _
                           " limit 1"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    no_urut_format = ds_bantu.Tables(0).Rows(0).Item("no_urut") + par_i
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If Len(no_urut_format) = 1 Then
            no_urut_format = "000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 2 Then
            no_urut_format = "00" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 3 Then
            no_urut_format = "0" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 4 Then
            no_urut_format = no_urut_format.ToString
        End If

        get_ass_code = par_type + "-" + tahun + "-" + bulan + "-" + par_pt_code + "-" + no_urut_format

        Return get_ass_code
    End Function
End Class

