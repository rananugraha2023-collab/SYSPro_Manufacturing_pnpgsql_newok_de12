Imports master_new.ModFunction

Public Class function_data
    Public Function load_dpt_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select dpt_id, dpt_code, dpt_desc from dpt_mstr where dpt_active ~~* 'Y' " + _
                           " and dpt_en_id = " + par_en_id.ToString + _
                           " order by dpt_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "dpt_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_user_approval() As String
        get_user_approval = "'" + master_new.ClsVar.sNama + "',"
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupnama from tconfusergroup ug " + _
                           " inner join tconfgroup g on g.groupid = ug.groupid " + _
                           " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wfs_status")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            get_user_approval = get_user_approval + "'" + ds_bantu.Tables(0).Rows(i).Item("groupnama") + "',"
        Next

        get_user_approval = get_user_approval.Substring(0, Len(get_user_approval) - 1)
        Return get_user_approval
    End Function

    Public Function load_wfs_status() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select wfs_id, wfs_desc from wfs_status order by wfs_id "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "wfs_status")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_purchase_order(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select po_code, ptnr_name from po_mstr  " + _
                           " inner join ptnr_mstr on ptnr_id = po_ptnr_id " + _
                           " where coalesce(po_close_date,'01/01/1999') = '01/01/1999' " + _
                           " and po_en_id = " + par_en_id.ToString + _
                           " order by po_date desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "po_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    'for sales quotation'
    Public Function load_sq_type() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'R' as value, 'Regular' as display " _
                            & "union " _
                            & "select 'P' as value, 'Personal Selling' as display " _
                            & "union " _
                            & "select 'D' as value, 'Direct Selling' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "so_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function load_pt_mstr(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pt_id, pt_code, pt_desc1, pt_desc2, pt_um from pt_mstr " _
                                & " inner join en_mstr on en_id = pt_en_id " _
                                & " left outer join eng_group on eng_id = pt_eng_id " _
                                & " left outer join engd_det on engd_eng_oid = eng_oid " _
                                & " where en_active ~~* 'Y' " _
                                & " and engd_en_id in (" + par_en_id.ToString + ") and pt_type ~~* 'I' " _
                                & " order by pt_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    'har 20110715
    Public Function load_unit_measure(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "SELECT code_id as unit_id, code_name as unit_desc from code_mstr where code_field ~~* 'unitmeasure' and code_active ~~* 'Y'" _
                         & " AND code_en_id =" & par_en_id & " " _
                         & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                         & " order by code_default desc, code_name "

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "unitmeasure")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    'har 20110715
    Public Function load_wc_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select wc_id, wc_desc from wc_mstr" _
                         & " where wc_dom_id = " & master_new.ClsVar.sdom_id _
                         & " and wc_en_id in (0," + par_en_id.ToString + ")" _
                         & " and wc_active ~~* 'y' order by wc_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wc_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_pt_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pt_id, pt_code, pt_desc1, pt_desc2, pt_um from pt_mstr " + _
                               " where pt_en_id in (select user_en_id from tconfuserentity " + _
                                             " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                               " and pt_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by pt_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_si_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select si_id, si_desc from si_mstr where si_active = 'Y' " + _
                            " and si_en_id in (select user_en_id from tconfuserentity " + _
                                             " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                           " and si_dom_id = " + master_new.ClsVar.sdom_id + _
                           " and si_desc <> '-' " + _
                           " order by si_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "si_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_si_mstr(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select si_id, si_desc from si_mstr where si_active = 'Y' " + _
                           " and si_en_id in(0," + par_en_id.ToString + ")" + _
                           " and si_dom_id = " + master_new.ClsVar.sdom_id + _
                           " and si_desc <> '-' " + _
                           " order by si_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "si_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_si_mstr_data(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select si_id, si_desc from si_mstr where si_active = 'Y' " + _
                           " and si_en_id in(0," + par_en_id.ToString + ")" + _
                           " and si_dom_id = " + master_new.ClsVar.sdom_id + _
                           " order by si_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "si_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_dom_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select dom_id, dom_code, dom_desc from dom_mstr where dom_id > 0 " _
                             & " and dom_active ~~* 'Y' "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "dom_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_pay_type(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_code, code_name, code_usr_1 from code_mstr where code_field ~~* 'payment_type' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and code_en_id in (0," + par_en_id.ToString + ")" _
                             & " order by code_default desc, code_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "code_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_code_mstr(ByVal par_en_id As Integer, ByVal par_code_field As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_code, code_name from code_mstr where code_field ~~* '" + par_code_field + "' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and code_en_id in (0," + par_en_id.ToString + ")" _
                             & " order by code_default desc, code_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "code_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_code_mstr(ByVal par_code_field As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name from code_mstr where code_field ~~* '" + par_code_field + "' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " AND (code_en_id = 0 or code_en_id in (select user_en_id from tconfuserentity " _
                                            & " where userid = " + master_new.ClsVar.sUserID.ToString + "))" _
                             & " order by code_default desc, code_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "code_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_pi_mstr(ByVal par_en_id As Integer, ByVal par_so_type As String, ByVal par_cu_id As Integer, ByVal par_date As Date) As DataTable
        Dim _en_id_coll As String = entity_parent(par_en_id)

        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
                             & " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
                             & " AND pi_en_id in (" & _en_id_coll + ")" _
                             & " AND pi_cu_id = " & par_cu_id.ToString _
                             & " AND pi_so_type ~~* '" & par_so_type + "'" _
                             & " AND pi_start_date <= " + SetDate(par_date) + " and pi_end_date >= " + SetDate(par_date) _
                             & " AND pi_active ~~* 'Y' " _
                             & " order by pi_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pi_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_pi_mstr(ByVal par_en_id As Integer, ByVal par_so_type As String, ByVal par_cu_id As Integer, ByVal par_date As Date, ByVal par_ptnrg_id As Integer) As DataTable
        Dim _en_id_coll As String = entity_parent(par_en_id)

        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
                             & " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
                             & " AND pi_en_id in (" & _en_id_coll + ")" _
                             & " AND pi_cu_id = " & par_cu_id.ToString _
                             & " AND pi_so_type ~~* '" & par_so_type + "'" _
                             & " AND pi_ptnrg_id = " & par_ptnrg_id.ToString + " " _
                             & " AND pi_start_date <= " + SetDate(par_date) + " and pi_end_date >= " + SetDate(par_date) _
                             & " AND pi_active ~~* 'Y' " _
                             & " order by pi_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pi_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_pi_mstrs(ByVal par_en_id As Integer, ByVal par_so_type As String, ByVal par_cu_id As Integer, ByVal par_date As Date, ByVal par_sales_program As String) As DataTable
        Dim _en_id_coll As String = entity_parent(par_en_id)

        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
                             & " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
                             & " AND pi_en_id in (" & _en_id_coll + ")" _
                             & " AND pi_cu_id = " & par_cu_id.ToString _
                             & " AND pi_so_type ~~* '" & par_so_type + "'" _
                             & " AND pi_sales_program = " & par_sales_program.ToString + " " _
                             & " AND pi_start_date <= " + SetDate(par_date) + " and pi_end_date >= " + SetDate(par_date) _
                             & " AND pi_active ~~* 'Y' " _
                             & " order by pi_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pi_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_pint_mstr(ByVal par_en_id As Integer, ByVal par_so_type As String, ByVal par_cu_id As Integer, ByVal par_date As Date) As DataTable
        Dim _en_id_coll As String = entity_parent(par_en_id)

        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pi_id, pi_desc from pi_mstr where pi_active ~~* 'Y'" _
                             & " AND pi_dom_id = " & master_new.ClsVar.sdom_id _
                             & " AND pi_en_id in (" & _en_id_coll + ")" _
                             & " AND pi_cu_id = " & par_cu_id.ToString _
                             & " AND pi_so_type ~~* '" & par_so_type + "'" _
                             & " AND pi_start_date <= " + SetDate(par_date) + " and pi_end_date >= " + SetDate(par_date) _
                             & " AND pi_is_internal ~~* 'Y' " _
                             & " AND pi_active ~~* 'Y' " _
                             & " order by pi_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pi_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function load_pl_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pl_id, pl_desc from pl_mstr where pl_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and pl_active ~~* 'Y' order by pl_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pl_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_sales_program(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'sales_program' and code_active ~~* 'Y'" _
                             & " and code_en_id in (0," + par_en_id.ToString + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_promo_mstr(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select promo_id, promo_desc from promo_mstr" _
                            & " where promo_dom_id = " & master_new.ClsVar.sdom_id _
                            & " and promo_en_id in (0," + par_en_id.ToString + ")" _
                            & " and promo_active ~~* 'y' order by promo_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "promo_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function get_gcald_det_status(ByVal par_en_id As Integer, ByVal par_type As String, ByVal par_date As Date) As String
        get_gcald_det_status = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_type + " as status " + _
                                           " from gcald_det " + _
                                           " inner join gcal_mstr on gcal_oid = gcald_gcal_oid " + _
                                           " where gcal_start_date <= " + SetDate(par_date) + _
                                           " and gcal_end_date >= " + SetDate(par_date) + _
                                           " and gcald_en_id = " + par_en_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_gcald_det_status = .DataReader("status")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try

        Return get_gcald_det_status
    End Function

    Public Function load_loc_mstr(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select loc_id, loc_desc, code_name from loc_mstr" _
                             & " inner join code_mstr on code_id = loc_type " _
                             & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and loc_en_id in (0," + par_en_id.ToString & ") and loc_active ~~* 'y' order by loc_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "loc_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function load_loc_mstr_to(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select loc_id, loc_desc, code_name from loc_mstr" _
                             & " inner join code_mstr on code_id = loc_type " _
                             & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and loc_en_id in (0," + par_en_id.ToString & ") and loc_active ~~* 'y' and loc_git='N' order by loc_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "loc_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_loc_mstr_ptnr(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select loc_id, loc_desc, code_name from loc_mstr" _
                             & " inner join code_mstr on code_id = loc_type " _
                             & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and loc_en_id in (0," + par_en_id.ToString & ") and loc_active ~~* 'y' " _
                             & " and loc_type = '99298' and loc_git='N' order by loc_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "loc_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_orgs_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select orgs_id, orgs_desc from orgs_mstr where orgs_active ~~* 'y' " + _
                               " and orgs_en_id in (select user_en_id from tconfuserentity " + _
                               " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                               " order by orgs_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "orgs_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_loc_mstr() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select loc_id, loc_desc from loc_mstr" _
                             & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and loc_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                             & " and loc_active ~~* 'y' order by loc_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "loc_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_partner_type() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'A' as value, 'All Data' as display " _
                            & "union " _
                            & "select 'V' as value, 'Is Vendor' as display " _
                            & "union " _
                            & "select 'C' as value, 'Is Customer' as display " _
                            & "union " _
                            & "select 'M' as value, 'Is Member' as display " _
                            & "union " _
                            & "select 'E' as value, 'Is Employee' as display " _
                            & "union " _
                            & "select 'W' as value, 'Is Writer' as display order by value"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "partner_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_ac_type() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'A' as value, 'Asset' as display " _
                            & "union " _
                            & "select 'L' as value, 'Liabilities' as display " _
                            & "union " _
                            & "select 'R' as value, 'Revenue' as display " _
                            & "union " _
                            & "select 'C' as value, 'Capital' as display " _
                            & "union " _
                            & "select 'E' as value, 'Expense' as display"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ac_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_fp_status() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 0 as value, 'Normal' as display " + _
                              " union " + _
                              " select 1 as value, 'Penggantian' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "fp_status")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_pt_class() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select '' as value, '' as display " _
                            & "union " _
                            & "select 'A' as value, 'A' as display " _
                            & "union " _
                            & "select 'B' as value, 'B' as display " _
                            & "union " _
                            & "select 'C' as value, 'C' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_class")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_gender() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'P' as value, 'Pria' as display " _
                            & "union " _
                            & "select 'W' as value, 'Wanita' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ac_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_ccre_type() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'I' as value, 'Initial' as display " _
                            & "union " _
                            & "select 'R' as value, 'Recount' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ccre_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_po_status_cash() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select '-' as value, '-' as display " _
                            & "union " _
                            & "select 'C' as value, 'Cash' as display " _
                            & "union " _
                            & "select 'R' as value, 'Credit' as display order by value"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ac_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_so_type() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'R' as value, 'Regular' as display " _
                            & "union " _
                            & "select 'P' as value, 'Personal Selling' as display " _
                            & "union " _
                            & "select 'D' as value, 'Direct Selling' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "so_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_ac_sign() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select 'D' as value, 'Debet' as display " _
                            & "union " _
                            & "select 'C' as value, 'Credit' as display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ac_sign")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_wh_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select wh_id, wh_desc from wh_mstr" _
                         & " where wh_dom_id = " & master_new.ClsVar.sdom_id _
                         & " and wh_en_id in (0," + par_en_id.ToString + ")" _
                         & " and wh_active ~~* 'y' order by wh_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wh_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_taxtype_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'taxtype_mstr' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "taxtype_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_taxtype_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'taxtype_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "taxtype_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_taxclass_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'taxclass_mstr' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "taxclass_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_methode_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'ptnr_man_methode_mstr' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_man_methode_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_taxclass_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'taxclass_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "taxclass_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ap_type(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    '.SQL = "select code_id, code_name from code_mstr where code_field ~~* 'ap_type' and code_active ~~* 'Y'" _
                    '         & " AND code_en_id (0," & par_en_id + ")" _
                    '         & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                    '         & " order by code_default desc, code_desc"
                    .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'ap_type' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ap_type")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_creditterms_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name, code_usr_1 from code_mstr where code_field ~~* 'creditterms_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "creditterms_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_years_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_name, code_usr_1 from code_mstr where code_field ~~* 'year' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "creditterms_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_cu_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select cu_id, cu_name from cu_mstr where cu_active ~~* 'Y' order by cu_id"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "cu_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_cu_mstr(ByVal par_cu_name As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select cu_id, cu_name from cu_mstr where cu_active ~~* 'Y' and cu_name ~~* '" + par_cu_name + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "cu_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_tran_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select tran_id, tran_name from tran_mstr " + _
                               " inner join tranu_usr on tranu_tran_id = tran_id order by tran_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "tran_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_tran_mstr(ByVal par_type As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select tran_id, tran_name from tran_mstr " + _
                               " inner join tranu_usr on tranu_tran_id = tran_id " + _
                               " where tran_table ~~* '" + par_type + "' order by tran_name "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "tran_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_sb_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.sb_id, a.sb_code, a.sb_desc " _
                            & " FROM public.sb_mstr a " _
                            & " WHERE a.sb_active ~~* 'Y' And " _
                            & " a.sb_dom_id = " + master_new.ClsVar.sdom_id.ToString _
                            & " AND a.sb_en_id in (0," + par_en_id.ToString + ")" _
                            & " order by sb_desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sb_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_sb_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.sb_id, a.sb_code, a.sb_desc " _
                            & " FROM public.sb_mstr a " _
                            & " WHERE a.sb_active ~~* 'Y' And " _
                            & " a.sb_dom_id = " + master_new.ClsVar.sdom_id.ToString _
                            & " AND a.sb_en_id in (select user_en_id from tconfuserentity " _
                                            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " order by sb_desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sb_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ccr_restrc(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select cc_id, cc_code, cc_desc from ccr_restrc cu inner join cc_mstr cm on cm.cc_id = cu.ccr_cc_id " + _
                           " where ccr_user_id = " + master_new.ClsVar.sUserID.ToString + _
                           " and cc_active ~~* 'y' " + _
                           " and cc_en_id  in (0," + par_en_id.ToString + ")" + _
                           " order by cc_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "cc_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_cc_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.cc_id,  a.cc_desc " _
                                & "FROM public.cc_mstr a " _
                                & "WHERE " _
                                & "  a.cc_dom_id  = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.cc_active ~~* 'Y' " _
                                & " AND a.cc_en_id in (0," + par_en_id.ToString + ")" _
                                & " order by cc_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "cc_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_cc_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.cc_id,  a.cc_desc " _
                                & "FROM public.cc_mstr a " _
                                & "WHERE " _
                                & "  a.cc_dom_id  = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.cc_active ~~* 'Y' AND a.cc_en_id in (select user_en_id from tconfuserentity " _
                                            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " order by cc_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "cc_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function


    Public Function load_dpt_group() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.dptg_kode,  a.dptg_desc " _
                                & "FROM public.dptg_group a " _
                                & "WHERE " _
                                & "  " _
                                & "  a.dptg_active ~~* 'Y' " _
                                & " order by dptg_kode "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "dpt_group")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function


    Public Function load_ac_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.ac_code, a.ac_name, a.ac_id " _
                                & "FROM public.ac_mstr a " _
                                & "WHERE a.ac_dom_id = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.ac_active ~~* 'Y' and ac_is_sumlevel ~~* 'N' order by ac_code"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ac_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ac_mstr(ByVal par_filter As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.ac_code, a.ac_name, a.ac_id " _
                                & "FROM public.ac_mstr a " _
                                & "WHERE a.ac_dom_id = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.ac_active ~~* 'Y' and ac_is_sumlevel ~~* 'N' " & par_filter & " order by ac_code"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ac_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_area_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select -1 as area_id, '' as area_code, '' as area_name " + _
                           " union " + _
                           " select area_id, area_code, area_name  " + _
                           " from area_mstr order by area_id"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "area_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_gcal_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select gcal_start_date, gcal_end_date, gcal_oid from gcal_mstr" + _
                           " order by gcal_year desc, gcal_periode desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "gcal_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_periode_royalti() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_desc, rms_code, rms_date, rms_generate, rms_oid, rms_en_id from rms_mstr inner join en_mstr on en_id = rms_en_id" + _
                               " where rms_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and rms_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                            " order by rms_date desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "rms_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_periode_bonus_ds() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_desc, bds_code, bds_date, bds_generate, bds_oid, bds_en_id,bds_start_date,bds_end_date,bds_end_date2,bds_generate2 from bds_mstr inner join en_mstr on en_id = bds_en_id" + _
                               " where bds_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and bds_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                            " order by bds_date desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bds_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_periode_bonus_rs() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_desc, brs_code, brs_date, brs_generate, brs_oid, brs_en_id from brs_mstr inner join en_mstr on en_id = brs_en_id" + _
                               " where brs_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and brs_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                            " order by brs_date desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "brs_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function entity_user() As String
        entity_user = ""

        Try
            Dim dr As DataRow
            Dim ssql As String
            ssql = "select en_id from tconfuser where userid = " + master_new.ClsVar.sUserID.ToString
            dr = master_new.PGSqlConn.GetRowInfo(ssql)
            If dr(0) Is System.DBNull.Value Then
                Box("Sorry this user have no entity default")
            Else
                entity_user = dr(0).ToString
            End If
            'Using objcek As New master_new.WDABasepgsql("", "")
            '    With objcek
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "select en_id from tconfuser where userid = " + master_new.ClsVar.sUserID.ToString
            '        .InitializeCommand()
            '        .DataReader = .Command.ExecuteReader
            '        While .DataReader.Read
            '            entity_user = .DataReader("en_id")
            '        End While
            '    End With
            'End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
        Return entity_user
    End Function

    Public Function entity_parent(ByVal par_en_id As String) As String
        entity_parent = ""
        Dim i As Integer
        Dim _en_id_coll As String = par_en_id.ToString + ","

        For i = 0 To 100
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select en_parent from en_mstr where en_id = " + par_en_id.ToString
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            If IsDBNull(.DataReader("en_parent")) = True Then
                                _en_id_coll = _en_id_coll.Substring(0, Len(_en_id_coll) - 1)
                                Exit For
                                ' Return _en_id_coll
                            Else
                                _en_id_coll = _en_id_coll + .DataReader("en_parent").ToString + ","
                                par_en_id = .DataReader("en_parent")
                            End If
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Function
            End Try
        Next

        Dim sSQL As String
        sSQL = "SELECT  " _
            & "  user_en_id " _
            & "FROM  " _
            & "  public.tconfuserentityview  " _
            & "  where userid=" & master_new.ClsVar.sUserID.ToString

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(sSQL)

        For Each dr As DataRow In dt.Rows
            _en_id_coll = _en_id_coll & "," & dr(0)
        Next

        entity_parent = _en_id_coll
        Return entity_parent
    End Function

    Public Function load_en_mstr_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                            " order by en_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_group_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.gr_id, " _
                        & "  a.gr_name " _
                        & "FROM " _
                        & "  public.gr_mstr a " _
                        & "WHERE " _
                        & "  a.gr_active = 'Y'"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "gr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_quarter_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.qrtr_code, " _
                        & "  a.qrtr_name, " _
                        & "  a.qrtr_id " _
                        & "FROM " _
                        & "  public.qrtr_mstr a " _
                        & "ORDER BY " _
                        & "  a.qrtr_code"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "qrtr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_qrtr_code_id() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT DISTINCT " _
                        & "  a.qrtr_code_id, " _
                        & "  a.qrtr_code_name " _
                        & "FROM " _
                        & "  public.qrtr_mstr a " _
                        & "ORDER BY " _
                        & "  a.qrtr_code_id"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "qrtr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_period_type_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT DISTINCT " _
                        & "  a.period_type_id, " _
                        & "  a.period_type_name, " _
                        & "  a.period_type_month_count " _
                        & "FROM " _
                        & "  public.period_type_mstr a " _
                        & "ORDER BY " _
                        & "  a.period_type_id"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "qrtr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_periode_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  a.periode_code, " _
                    & "  a.periode_start_date, " _
                    & "  a.periode_end_date " _
                    & "FROM " _
                    & "  public.psperiode_mstr a " _
                    & "WHERE " _
                    & "  a.periode_active = 'Y' order by periode_code desc"


                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "qrtr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_periode_mstr_se() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  a.seperiode_code, " _
                    & "  a.seperiode_start_date, " _
                    & "  a.seperiode_end_date, " _
                    & "  a.seperiode_bonus_gen, " _
                    & "  a.seperiode_payment_date, " _
                    & "  a.seperiode_status_gen,seperiode_remarks " _
                    & "FROM " _
                    & "  public.seperiode_mstr a " _
                    & "ORDER BY " _
                    & "  a.seperiode_code"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "qrtr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_eng_group() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select eng_id, eng_code, eng_name from eng_group"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "eng_group")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_en_mstr_tran() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and en_id <> 0 " + _
                               " and en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ") " + _
                            " order by en_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_pb_type() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.pbt_code, " _
                        & "  a.pbt_desc " _
                        & "FROM " _
                        & "  public.pbt_type a " _
                        & "WHERE " _
                        & "  a.pbt_active = 'Y' " _
                        & "ORDER BY " _
                        & "  a.pbt_code"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    'Public Function load_transaction() As DataTable
    '    Dim ds_bantu As New DataSet
    '    Dim dt_bantu As New DataTable
    '    Try
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .SQL = "select tranu_tran_id, tran_name " + _
    '                       " from tranu_usr " + _
    '                       " inner join tran_mstr on tran_id = tranu_tran_id " + _
    '                       " where tranu_user_id = " + master_new.ClsVar.sUserID.ToString()
    '                .InitializeCommand()
    '                .FillDataSet(ds_bantu, "approvaltype")
    '                dt_bantu = ds_bantu.Tables(0)
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return dt_bantu
    'End Function

    Public Function load_transaction() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select tranu_tran_id, tran_name " + _
                           " from tranu_usr " + _
                           " inner join tran_mstr on tran_id = tranu_tran_id " + _
                           " where tranu_user_id = " + master_new.ClsVar.sUserID.ToString() + _
                           " and tran_active ~~* 'Y' "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "approvaltype")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_aprv_mstr(ByVal par_tran_id As Integer) As DataSet
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  aprv_oid, " _
                        & "  aprv_dom_id, " _
                        & "  aprv_en_id, " _
                        & "  aprv_tran_id, " _
                        & "  aprv_user_id, " _
                        & "  aprv_dt, " _
                        & "  aprv_seq, " _
                        & "  aprv_type " _
                        & "FROM  " _
                        & "  public.aprv_mstr " _
                        & "  where aprv_tran_id = " + par_tran_id.ToString _
                        & "  order by aprv_seq "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "aprv_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return ds_bantu
    End Function

    Public Function load_list_aprvd_dok(ByVal par_en_id As Integer, ByVal par_type As Integer, ByVal par_date As Date) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  aprvd_name_1, " _
                        & "  aprvd_pos_1, " _
                        & "  aprvd_name_2, " _
                        & "  aprvd_pos_2, " _
                        & "  aprvd_name_3, " _
                        & "  aprvd_pos_3, " _
                        & "  aprvd_name_4, " _
                        & "  aprvd_pos_4 " _
                        & "FROM  " _
                        & "  public.aprvd_dok " _
                        & "  where aprvd_type = " + par_type.ToString _
                        & "  and aprvd_active ~~* 'Y' " _
                        & "  and aprvd_start_eff <= " + SetDate(par_date) _
                        & "  and aprvd_end_eff >= " + SetDate(par_date) _
                        & "  and aprvd_en_id = " + par_en_id.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_list_aprvd_data(ByVal par_en_id As Integer, ByVal par_awal As String, ByVal par_akhir As String, ByVal par_initial As String, ByVal par_table As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & par_initial & "_oid as data_oid, " _
                        & par_initial & "_code as data_code " _
                        & "FROM  " _
                        & "  public." + par_table + " where " _
                        & par_initial & "_code >= '" + par_awal + "' and " _
                        & par_initial & "_code <= '" + par_akhir + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_en_mstr_tran_global() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and en_id <> 0 " + _
                               " order by en_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_en_mstr_all() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " order by en_code "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "en_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    'Public Function load_ptnr_mstr() As DataTable
    '    Dim ds_bantu As New DataSet
    '    Dim dt_bantu As New DataTable
    '    Try
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .SQL = "select ptnr_name, ptnr_oid from ptnr_mstr where ptnr_active ~~* 'Y'" + _
    '                       " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
    '                       " and ptnr_en_id in (select user_en_id from tconfuserentity " + _
    '                                          " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
    '                       " order by ptnr_name "
    '                .InitializeCommand()
    '                .FillDataSet(ds_bantu, "ptnr_mstr")
    '                dt_bantu = ds_bantu.Tables(0)
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return dt_bantu
    'End Function

    Public Function load_ptnr_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_name, ptnr_oid from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (0," + par_en_id + ")" + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ptnrc_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnrc_name, ptnrc_oid, ptnrc_id from ptnrc_cat where ptnrc_active ~~* 'Y'" + _
                           " and ptnrc_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnrc_en_id in (0," + par_en_id + ")" + _
                           " order by ptnrc_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnrc_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_type_document() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select 1 as value, 'Requisition' as display " _
                            & " union " _
                            & " select 2 as value, 'Purchase Order' as display " _
                            & " union " _
                            & " select 3 as value, 'Req. Transfer Issue' as display " _
                            & " union " _
                            & " select 4 as value, 'Req. Transfer Receipt' as display " _
                            & " union " _
                            & " select 5 as value, 'Purchase Receipt' as display " _
                            & " union " _
                            & " select 6 as value, 'Purchase Return' as display " _
                            & " union " _
                            & " select 7 as value, 'Inventory Request' as display " _
                            & " union " _
                            & " select 8 as value, 'Transfer Issue' as display " _
                            & " union " _
                            & " select 9 as value, 'Transfer Receipt' as display " _
                            & " union " _
                            & " select 10 as value, 'Sales Order' as display " _
                            & " union " _
                            & " select 11 as value, 'Sales Order Shipment' as display " _
                            & " union " _
                            & " select 12 as value, 'Sales Order Print Entity Shipment' as display " _
                            & " union " _
                            & " select 13 as value, 'Sales Order Return' as display " _
                            & " union " _
                            & " select 14 as value, 'Invoice' as display " _
                            & " union " _
                            & " select 15 as value, 'Consignment Invoice' as display " _
                            & " union " _
                            & " select 16 as value, 'Inventory Receipt' as display " _
                            & " union " _
                            & " select 17 as value, 'Inventory Issue' as display " _
                             & " union " _
                            & " select 18 as value, 'Routing' as display " _
                            & " order by display "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "type_document")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_pt_size() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.kode_id, " _
                        & "  a.kode " _
                        & "FROM " _
                        & "  public.cetak_bahan a " _
                        & "ORDER BY " _
                        & "  a.kode"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "pt_size")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ppn_type() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select 'E' as value, 'PPN Bebas' as display " _
                            & "union " _
                            & "select 'A' as value, 'PPN Bayar' as display " _
                            & "union " _
                            & "select 'N' as value, 'None' as display "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ppn_type")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_sales_program() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.sls_id, " _
                        & "  a.sls_code, " _
                        & "  a.sls_name " _
                        & "FROM " _
                        & "  public.sls_program a " _
                        & "WHERE " _
                        & "  a.sls_active = 'Y' " _
                        & "ORDER BY " _
                        & "  a.sls_name"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ppn_type")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_is_writer(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name  from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (0," + par_en_id + ")" + _
                           " and ptnr_is_writer ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_is_employee_id() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name  from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                           " and ptnr_is_emp ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_employee() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select -1 as emp_id, '' as emp_fname " + _
                           " union " + _
                           "select emp_id, emp_fname from emp_mstr order by emp_fname "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "emp_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_is_employee_oid() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_oid, ptnr_name  from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                           " and ptnr_is_emp ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_supplier(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name, ptnr_ac_ap_id from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (0," + par_en_id + ")" + _
                           " and ptnr_is_vend ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_customer() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, en_desc, ptnr_name from ptnr_mstr " + _
                           " inner join en_mstr on en_id = ptnr_en_id " + _
                           " where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                           " and ptnr_is_cust ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_customer(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Dim _en_id_coll As String = entity_parent(par_en_id)


            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name, ptnr_ac_ar_id,en_desc, ptnr_oid from ptnr_mstr inner join en_mstr on (ptnr_en_id=en_id) where ptnr_active ~~* 'Y'" + _
                           " and (ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + " or ptnr_dom_id is null ) " & _
                           " and ptnr_en_id in (0," + _en_id_coll + ")" + _
                           " and ptnr_is_cust ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_sales(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Dim _en_id_coll As String = entity_parent(par_en_id)

            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name, ptnr_ac_ap_id from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (0," + par_en_id + ")" + _
                           " and ptnr_is_member ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_level() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.lvl_id, " _
                        & "  a.lvl_name " _
                        & "FROM " _
                        & "  public.pslvl_mstr a " _
                        & "union select null as lvl_id, '-' as lvl_name " _
                        & "ORDER BY " _
                        & "  lvl_id desc"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function ptnr_sls_emp_pos() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.emp_pos_id, " _
                        & "  a.emp_pos_name " _
                        & "FROM " _
                        & "  public.sls_emp_pos a " _
                        & "union select null as emp_pos_id, '-' as emp_pos_name " _
                        & "ORDER BY " _
                        & "  emp_pos_id"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function ptnr_sls_emp_cat() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.emp_cat_id, " _
                        & "  a.emp_cat_name " _
                        & "FROM " _
                        & "  public.sls_emp_cat a " _
                        & "union select null as emp_cat_id, '-' as emp_cat_name " _
                        & "ORDER BY " _
                        & "  emp_cat_id"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function ptnr_sls_emp_lvl() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.emp_lvl_id, " _
                        & "  a.emp_lvl_name " _
                        & "FROM " _
                        & "  public.sls_emp_lvl a " _
                        & "union select null as emp_lvl_id, '-' as emp_lvl_name " _
                        & "ORDER BY " _
                        & "  emp_lvl_id"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "data")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_bk_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select bk_name, bk_id from bk_mstr where bk_active ~~* 'Y'" + _
                           " and bk_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and bk_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                           " order by bk_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bk_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_bk_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select bk_id, bk_code, bk_name from bk_mstr where bk_active ~~* 'Y'" + _
                           " and bk_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and bk_en_id in (0," + par_en_id.ToString + ")" + _
                           " order by bk_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bk_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_bk_mstr(ByVal par_en_id As Integer, ByVal par_filter As String, ByVal par_status As Boolean) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select bk_id, bk_code, bk_name from bk_mstr where bk_active ~~* 'Y'" + _
                           " and bk_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and bk_en_id in (0," + par_en_id.ToString + ") " & par_filter + _
                           " order by bk_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bk_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_bk_mstr(ByVal par_en_id As Integer, ByVal par_cu_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select bk_id, bk_code, bk_name from bk_mstr where bk_active ~~* 'Y'" + _
                           " and bk_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and bk_en_id in (0," + par_en_id.ToString + ")" + _
                           " and bk_cu_id = " + par_cu_id.ToString + _
                           " order by bk_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bk_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_addr_type_mstr(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'addr_type_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "addr_type_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ptnra_addr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_name, code_name, ptnra_oid  from ptnra_addr" _
                             & " inner join ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                             & " inner join code_mstr on code_id = ptnra_addr_type "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnra_addr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function load_ptnrac_function() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'ptnrac_function' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnrac_function")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function


    Public Function load_status_rumah() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('resident_status_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_lama_tinggal() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('resident_stay_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function masa_kerja() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('work_age_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function income() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('income_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function kepribadian() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select coalesce(code_id,0)as code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('morality_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function tanggungan() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('mortage_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function jaminan() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc from code_mstr where code_field in " _
                             & " ('support_value_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    Public Function load_ver_category() As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_name, code_desc,code_usr_1,code_field from code_mstr where code_field in " _
                             & " ('income_mstr','morality_mstr','mortage_mstr','resident_status_mstr', " _
                             & " 'resident_stay_mstr','support_value_mstr','work_age_mstr') " _
                             & " and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_field, code_usr_1"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sales_program")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function get_value(ByVal par_id As Integer) As Double
        get_value = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select sqr_value from sqr_rate " + _
                                           " where sqr_code_id = " + par_id.ToString + " "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_value = .DataReader("sqr_value")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
        Return get_value
    End Function

    Public Function load_bank_mstr(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select bk_id, bk_code, bk_name from bk_mstr where bk_active ~~* 'Y'" + _
                           " and bk_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and bk_en_id in (0," + par_en_id.ToString + ")" + _
                           " order by bk_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bk_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_va_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT a.va_code, a.va_name, a.va_id " _
                                & "FROM public.va_mstr a " _
                                & " inner join ptnr_mstr on ptnr_id=va_ptnr_id " _
                                & "WHERE a.va_dom_id = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.va_active ~~* 'Y' and ptnr_name='-' order by va_name"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "va_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

#Region "Get Data"
    Public Function get_exchange_rate(ByVal par_cu_id As Integer) As Double
        get_exchange_rate = 1
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select exr_cu_rate_1 from exr_rate " + _
                                           " where exr_cu_id_1 = " + master_new.ClsVar.ibase_cur_id.ToString + _
                                           " and exr_cu_id_2 = " + par_cu_id.ToString + _
                                           " and exr_start_date <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " + _
                                           " and exr_end_date >= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_exchange_rate = .DataReader("exr_cu_rate_1")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
        Return get_exchange_rate
    End Function

    Public Function get_exchange_rate(ByVal par_cu_id As Integer, ByVal par_date As DateTime) As Double
        get_exchange_rate = 1
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select exr_cu_rate_1 from exr_rate " + _
                                           " where exr_cu_id_1 = " + master_new.ClsVar.ibase_cur_id.ToString + _
                                           " and exr_cu_id_2 = " + par_cu_id.ToString + _
                                           " and exr_start_date <= " + SetDate(par_date) + _
                                           " and exr_end_date >= " + SetDate(par_date)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            get_exchange_rate = .DataReader("exr_cu_rate_1")
                        End While
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
        Return get_exchange_rate
    End Function

    Public Function get_taxrate_ap_account(ByVal par_tax_class_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_name as tax_type_name, taxr_ac_ap_id, ac_code, ac_name, taxr_sb_ap_id, taxr_cc_ap_id, taxr_rate from taxr_mstr " _
                         & " inner join code_mstr on code_id = taxr_tax_type " _
                         & " inner join ac_mstr on ac_id = taxr_ac_ap_id " _
                         & " where taxr_tax_class = " + par_tax_class_id.ToString _
                         & " order by tax_type_name desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "taxr_mstr")


                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_taxrate_ar_account(ByVal par_tax_class_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_name as tax_type_name, taxr_ac_sales_id, ac_code, ac_name, taxr_sb_sales_id, taxr_cc_sales_id, taxr_rate from taxr_mstr " _
                         & " inner join code_mstr on code_id = taxr_tax_type " _
                         & " inner join ac_mstr on ac_id = taxr_ac_sales_id " _
                         & " where taxr_tax_class = " + par_tax_class_id.ToString _
                         & " order by tax_type_name desc "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "taxr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_real_account(ByVal par_cu_id As Integer, ByVal par_type As String) As DataTable
        Dim dt_bantu As New DataTable
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    If par_type = "gain" Then
                        .SQL = "select cu_ac_real_exc_gain_id, ac_id, ac_code, ac_name from cu_mstr " + _
                               " inner join ac_mstr on ac_id = cu_ac_real_exc_gain_id " + _
                               " where cu_id = " + par_cu_id.ToString
                    ElseIf par_type = "loss" Then
                        .SQL = "select cu_ac_real_exc_lost_id, ac_id, ac_code, ac_name from cu_mstr " + _
                               " inner join ac_mstr on ac_id = cu_ac_real_exc_lost_id " + _
                               " where cu_id = " + par_cu_id.ToString
                    End If

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "real")
                    If ds_bantu.Tables(0).Rows(0).Item("ac_id") = 0 Then

                        Box("Setingan untuk Real excange " & par_type & " masih - di master currency")
                    End If
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_unreal_account(ByVal par_cu_id As Integer, ByVal par_type As String) As DataTable
        Dim dt_bantu As New DataTable
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    If par_type = "gain" Then
                        .SQL = "select cu_ac_unreal_exc_gain_id, ac_id, ac_code, ac_name from cu_mstr " + _
                               " inner join ac_mstr on ac_id = cu_ac_unreal_exc_gain_id " + _
                               " where cu_id = " + par_cu_id.ToString
                    ElseIf par_type = "loss" Then
                        .SQL = "select cu_ac_unreal_exc_lost_id, ac_id, ac_code, ac_name from cu_mstr " + _
                               " inner join ac_mstr on ac_id = cu_ac_unreal_exc_lost_id " + _
                               " where cu_id = " + par_cu_id.ToString
                    End If

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "unreal")
                    If ds_bantu.Tables(0).Rows(0).Item("ac_id") = 0 Then
                        Box("Setingan untuk Unreal excange " & par_type & " masih - di master currency")
                    End If
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_prodline_account_ap(ByVal par_pt_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Dim _pl_id As Integer

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_pl_id from pt_mstr" _
                                         & " where pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pl_id = .DataReader.Item("pt_pl_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ac_id, ac_code, ac_name from pla_mstr " _
                         & " inner join ac_mstr on ac_id = pla_ac_id " _
                         & " where pla_pl_id = " + _pl_id.ToString _
                         & " and pla_code ~~* 'PRC_PORACC'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prod_acct")

                    If ds_bantu.Tables(0).Rows.Count = 0 Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "PRC_PORACC" & " kosong")
                    ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "PRC_PORACC" & " belum di setting product line nya")
                    ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "PRC_PORACC" & " masih - di setting product line nya")
                    End If

                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_prodline_account_ap_rate(ByVal par_pt_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Dim _pl_id As Integer

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_pl_id from pt_mstr" _
                                         & " where pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pl_id = .DataReader.Item("pt_pl_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ac_id, ac_code, ac_name from pla_mstr " _
                         & " inner join ac_mstr on ac_id = pla_ac_id " _
                         & " where pla_pl_id = " + _pl_id.ToString _
                         & " and pla_code ~~* 'PRC_APRVACC'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prod_acct")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_prodline_account_ar_discount(ByVal par_pt_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Dim _pl_id As Integer

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_pl_id from pt_mstr" _
                                         & " where pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pl_id = .DataReader.Item("pt_pl_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ac_id, ac_code, ac_name from pla_mstr " _
                         & " inner join ac_mstr on ac_id = pla_ac_id " _
                         & " where pla_pl_id = " + _pl_id.ToString _
                         & " and pla_code ~~* 'SL_SLDACC'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prod_acct")

                    If ds_bantu.Tables(0).Rows.Count = 0 Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "SL_SLDACC" & " kosong")
                    ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "SL_SLDACC" & " belum di setting product line nya")
                    ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "SL_SLDACC" & " masih - di setting product line nya")
                    End If
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_prodline_account_ar(ByVal par_pt_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Dim _pl_id As Integer

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_pl_id from pt_mstr" _
                                         & " where pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pl_id = .DataReader.Item("pt_pl_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ac_id, ac_code, ac_name from pla_mstr " _
                         & " inner join ac_mstr on ac_id = pla_ac_id " _
                         & " where pla_pl_id = " + _pl_id.ToString _
                         & " and pla_code ~~* 'SL_SLACC'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prod_acct")

                    If ds_bantu.Tables(0).Rows.Count = 0 Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "SL_SLACC" & " kosong")
                    ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "SL_SLACC" & " belum di setting product line nya")
                    ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                        Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _pl_id) & ", baris = " & "SL_SLACC" & " masih - di setting product line nya")
                    End If

                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
#End Region

#Region "Format Lama"
    Public Function load_data(ByVal par_type As String, ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        If par_type = "entity" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "entity")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "ptnr_mstr_cust" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnr_id, ptnr_name from ptnr_mstr  where ptnr_active = 'Y' " + _
                               " and ptnr_is_cust = 'Y' and ptnr_en_id in (0," + par_en_id + ")" + _
                               " order by ptnr_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptnr_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "ptnr_mstr_sal" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnr_id, ptnr_name from ptnr_mstr  where ptnr_active = 'Y' " + _
                               " and ptnr_is_emp = 'Y' and ptnr_en_id in (0," + par_en_id + ")" + _
                               " order by ptnr_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptnr_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "currency" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select cu_id, cu_code from cu_mstr where cu_active ~~* 'Y' order by cu_code"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "cu_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "pt_type" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT 'I' as value, 'Inventory' as Display " _
                            & " union " _
                            & " SELECT 'A' as value, 'Asset' as Display " _
                            & "union " _
                            & " SELECT 'E' as value, 'Expense' as Display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_type")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "pt_cost_method" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT 'A' as value, 'AVERAGE' as Display " _
                            & " union " _
                            & " SELECT 'F' as value, 'FIFO' as Display " _
                            & "union " _
                            & " SELECT 'L' as value, 'LIFO' as Display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_cost_method")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "pt_ls" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT 'N' as value, 'Non' as Display " _
                            & " union " _
                            & " SELECT 'L' as value, 'Lot' as Display " _
                            & "union " _
                            & " SELECT 'S' as value, 'Serial' as Display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_ls")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "pt_pm_code" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT 'P' as value, 'Purchase' as Display " _
                            & " union " _
                            & " SELECT 'M' as value, 'Manufacture' as Display "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_pm_code")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "cc_id" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  a.cc_id, " _
                                & "  a.cc_code " _
                                & "FROM " _
                                & "  public.cc_mstr a " _
                                & "WHERE " _
                                & "  a.cc_dom_id  = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.cc_active ~~* 'Y' AND a.cc_en_id in (0," + par_en_id + ")" _
                                & " order by cc_code "

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "cc_id")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "ac_id" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  a.ac_code, " _
                                & "  a.ac_name, " _
                                & "  a.ac_id " _
                                & "FROM " _
                                & "  public.ac_mstr a " _
                                & "WHERE " _
                                & "  a.ac_dom_id = " + master_new.ClsVar.sdom_id.ToString & " AND  " _
                                & "  a.ac_active ~~* 'Y' order by ac_name"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ac_id")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "sb_id" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "SELECT  " _
                            & "  a.sb_id, " _
                            & "  a.sb_code, " _
                            & "  a.sb_desc " _
                            & "FROM " _
                            & "  public.sb_mstr a " _
                            & "WHERE " _
                            & "  a.sb_active ~~* 'Y' And " _
                            & "  a.sb_dom_id = " + master_new.ClsVar.sdom_id.ToString _
                            & " AND a.sb_en_id in (0," + par_en_id + ")" _
                            & " order by sb_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sb_id")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "org_type_id" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'org_type_id' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "org_type_id")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "org_type_id_non" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'org_type_id' " _
                             & " and code_active ~~* 'Y' " _
                             & " and code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "org_type_id_non")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "org_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select org_id, org_name, code_name from org_mstr " + _
                               " inner join code_mstr on code_id = org_type_id " + _
                               " where org_active = 'Y' order by org_id"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "org_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "orgs_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select orgs_desc, orgs_oid from orgs_mstr where orgs_active ~~* 'y' order by orgs_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "orgs_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "orgs_mstr2" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select orgs_id, orgs_desc from orgs_mstr where orgs_active ~~* 'y' " + _
                               " and orgs_en_id = " + par_en_id + _
                               "order by orgs_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "orgs_mstr2")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "user_login" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select userid, usernama from tconfuser"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "user_login")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "transaction_id" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  b.tran_name, " _
                            & "  b.tran_id " _
                            & "FROM " _
                            & "  public.tran_mstr b"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "transaction_id")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "taxclass" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_code from code_mstr where code_active ~~* 'Y'" + _
                               " and code_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " order by code_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "taxclass_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "wh_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select wh_id, wh_desc from wh_mstr where wh_active ~~* 'Y'" + _
                               " and wh_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and wh_en_id in (0," + par_en_id + ")" + _
                               " order by wh_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "wh_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "is_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select is_id, is_code from is_mstr where is_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                               " and is_en_id in (0," + par_en_id + ")" + _
                               " order by is_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "is_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "ptnr_mstr_vend" Then
            Dim _en_id_coll As String = entity_parent(par_en_id)
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnr_id,en_desc, ptnr_name from ptnr_mstr inner join en_mstr on ptnr_en_id = en_id where ptnr_active = 'Y' " + _
                               " and ptnr_is_vend = 'Y' and ptnr_en_id in (0," + _en_id_coll + ")" + _
                               " order by ptnr_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptnr_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "ptnr_mstr_vend_internal" Then
            Dim _en_id_coll As String = entity_parent(par_en_id)
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnr_id,en_desc, ptnr_name from ptnr_mstr inner join en_mstr on ptnr_en_id = en_id where ptnr_active = 'Y' " + _
                               " and ptnr_is_vend = 'Y' and ptnr_is_internal = 'Y' and ptnr_en_id in (0," + _en_id_coll + ")" + _
                               " order by ptnr_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptnr_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "ptnr_mstr_cust_int" Then
            Dim _en_id_coll As String = entity_parent(par_en_id)
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnr_id,en_desc, ptnr_name from ptnr_mstr inner join en_mstr on ptnr_en_id = en_id where ptnr_active = 'Y' " + _
                               " and ptnr_is_cust = 'Y' and ptnr_is_internal = 'Y' and ptnr_en_id in (0," + _en_id_coll + ")" + _
                               " order by ptnr_name"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "ptnr_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "cmaddr_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select cmaddr_id, cmaddr_name from cmaddr_mstr " + _
                               " where cmaddr_en_id in (0," + par_en_id + ")" + _
                               " order by cmaddr_name "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "cmaddr_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "sb_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select sb_id, sb_desc from sb_mstr where sb_active = 'Y' " + _
                               " and sb_en_id in (0," + par_en_id + ")" + _
                               " and sb_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by sb_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sb_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "cc_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select cc_id, cc_desc from cc_mstr where cc_active = 'Y' " + _
                               " and cc_en_id in (0," + par_en_id + ")" + _
                               " and cc_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by cc_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "cc_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "si_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select si_id, si_desc from si_mstr where si_active = 'Y' " + _
                               " and si_en_id in (0," + par_en_id + ")" + _
                               " and si_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by si_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "si_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "pjc_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pjc_id, pjc_desc from pjc_mstr where pjc_active = 'Y' " + _
                               " and pjc_en_id in (0, " + par_en_id + ")" + _
                               " and pjc_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by pjc_desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pjc_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "tran_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select tran_id, tran_name from tran_mstr " + _
                               " inner join tranu_usr on tranu_tran_id = tran_id order by tran_name "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "tran_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "pt_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pt_id, pt_code, pt_desc1, pt_desc2 from pt_mstr " + _
                               " where pt_en_id in (0," + par_en_id + ")" + _
                               " and pt_dom_id = " + master_new.ClsVar.sdom_id + _
                               " order by pt_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "unitmeasure" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_name from code_mstr where code_field ~~* 'unitmeasure' and code_active ~~* 'Y'" _
                             & " AND code_en_id in(0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_name "

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "unitmeasure")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "ptnrg_grp" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnrg_id, ptnrg_name from ptnrg_grp" _
                             & " where ptnrg_active = 'Y' " _
                             & " AND ptnrg_en_id in (0," & par_en_id + ")" _
                             & " AND ptnrg_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by ptnrg_name"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "unitmeasure")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "ptnrc_cat" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ptnrc_id, ptnrc_name from ptnrc_cat" _
                             & " where ptnrc_active = 'Y' " _
                             & " AND ptnrc_en_id in (0," & par_en_id + ")" _
                             & " AND ptnrc_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by ptnrc_name"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "category")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "loc_cat_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'loc_cat_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "loc_cat_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "loc_type_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'loc_type_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in(0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "loc_type_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "wh_cat_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'wh_cat_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "wh_cat_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "wh_type_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'wh_type_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "wh_type_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "pl_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select pl_id, pl_code, pl_desc from pl_mstr where pl_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and pl_dom_id = " + master_new.ClsVar.sdom_id _
                             & " and pl_active ~~* 'Y' order by pl_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pl_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "its_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select its_id, its_desc from its_mstr where its_dom_id = " & master_new.ClsVar.sdom_id _
                             & " and its_dom_id = " + master_new.ClsVar.sdom_id _
                             & " order by its_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "its_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'ElseIf par_type = "size_mstr" Then
            '    Try
            '        Using objcb As New master_new.WDABasepgsql("", "")
            '            With objcb

            '                .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'pt_size_mstr' and code_active ~~* 'Y'" _
            '                     & " AND code_en_id in (0," & par_en_id + ")" _
            '                     & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
            '                     & " order by code_default desc, code_desc"

            '                .InitializeCommand()
            '                .FillDataSet(ds_bantu, "pt_size_mstr")
            '                dt_bantu = ds_bantu.Tables(0)
            '            End With
            '        End Using
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try

            'ElseIf par_type = "size_mstr" Then
            '    Try
            '        Using objcb As New master_new.WDABasepgsql("", "")
            '            With objcb

            '                .SQL = "SELECT " _
            '                    & "  public.cetak_bahan.kode_id " _
            '                    & "  public.cetak_bahan.kode " _
            '                    & "FROM " _
            '                    & "  public.cetak_bahan"

            '                .InitializeCommand()
            '                .FillDataSet(ds_bantu, "pt_size_mstr")
            '                dt_bantu = ds_bantu.Tables(0)
            '            End With
            '        End Using
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try

        ElseIf par_type = "group_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'group_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "group_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "pt_subgroup" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'sub_group_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sub_group_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "pt_category_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'pt_category_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_category_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        ElseIf par_type = "pt_subcategory_mstr" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'pt_subcategory_mstr' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_subcategory_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        
        ElseIf par_type = "taxclass_mstr_non" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_id, code_desc from code_mstr where code_field ~~* 'taxclass_mstr' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "taxclass_mstr_non")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "trans_status" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select trans_id, trans_desc, trans_wf_start" _
                             & " from trans_status "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "trans_status")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf par_type = "fp_sign_user" Then
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "select code_name from code_mstr where code_field ~~* 'fp_sign_user' and code_active ~~* 'Y'" _
                             & " AND code_en_id in (0," & par_en_id + ")" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "fpsign_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Return dt_bantu
    End Function



    'new func merge asset
    Public Function load_fix_asset_method() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select code_code,code_name from code_mstr where code_field ~~* 'fix_asset_method' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " order by code_default desc, code_desc"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "fix_asset_method")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Function get_ass_code(ByVal par_pt_code As String, ByVal par_type As String, ByVal par_table As String, ByVal par_colom As String, ByVal par_i As Integer) As String
        get_ass_code = ""

        Dim tahun, bulan, no_urut_format As String
        Dim tanggal As Date
        tanggal = master_new.PGSqlConn.CekTanggal
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

    Public Function load_pt_mstr(ByVal par_en_id As Integer, ByVal _type As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pt_id, pt_code, pt_desc1, pt_desc2, pt_um from pt_mstr " + _
                               " where pt_en_id in (0," + par_en_id.ToString + ")" + _
                               " and pt_dom_id = " + master_new.ClsVar.sdom_id.ToString() + _
                               " and pt_type = " + SetSetring(_type) + _
                               " order by pt_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Function load_ptnr_mstr() As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_name, ptnr_id, ptnr_oid from ptnr_mstr where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_en_id in (select user_en_id from tconfuserentity " + _
                                              " where userid = " + master_new.ClsVar.sUserID.ToString + ")" + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    

    'Public Function load_pt_mstr(ByVal par_en_id As Integer) As DataTable
    '    Using ds_bantu As New DataSet()
    '        Dim dt_bantu As New DataTable()
    '        Try
    '            Using objcb As New master_new.WDABasepgsql("", "")
    '                With objcb
    '                    .SQL = "select pt_id, pt_code, pt_desc1, pt_desc2, pt_um from pt_mstr " _
    '                            & " inner join en_mstr on en_id = pt_en_id " _
    '                            & " left outer join eng_group on eng_id = pt_eng_id " _
    '                            & " left outer join engd_det on engd_eng_oid = eng_oid " _
    '                            & " where en_active ~~* 'Y' " _
    '                            & " and pt_en_id in (" + par_en_id.ToString + ") and pt_type in('A','I') " _
    '                            & " order by pt_code "
    '                    .InitializeCommand()
    '                    .FillDataSet(ds_bantu, "pt_mstr")
    '                    dt_bantu = ds_bantu.Tables(0)
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '        Return dt_bantu
    '    End Using
    'End Function
    Public Function load_end_user(ByVal par_en_id As Integer) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select xemp_id, xemp_code, xemp_name from xemp_mstr " + _
                           " where xemp_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and xemp_en_id = " + par_en_id.ToString() + _
                           " and xemp_is_active ~~* 'Y' " + _
                           " order by xemp_id "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "end_user")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function
    Public Function load_pt_class(ByVal par_type As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select code_id, code_code,code_name from code_mstr where code_field ~~* 'pt_class' and code_active ~~* 'Y'" _
                             & " AND code_dom_id = " & master_new.ClsVar.sdom_id _
                             & " AND code_code in ('" & par_type & "' )" _
                             & " order by code_default desc, code_desc"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_class")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function
    'Public Function get_ass_code(ByVal par_pt_code As String, ByVal par_type As String, ByVal par_table As String, ByVal par_colom As String, ByVal par_i As Integer) As String
    '    get_ass_code = ""

    '    Dim tahun, bulan, no_urut_format As String
    '    Dim tanggal As Date
    '    tanggal = master_new.PGSqlConn.CekTanggal
    '    tahun = tanggal.Year.ToString.Substring(2, 2)
    '    bulan = tanggal.Month.ToString
    '    no_urut_format = ""

    '    If Len(bulan) = 1 Then
    '        bulan = "0" + bulan
    '    End If

    '    Try
    '        Dim ds_bantu As New DataSet
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .SQL = "select coalesce(max(cast(substring(" + par_colom + ",13,5) as integer)),0) as no_urut " + _
    '                       " from " + par_table + _
    '                       " where substring(" + par_colom + ",3,2) = '" + tahun + "'" + _
    '                       " and substring(" + par_colom + ",6,2) = '" + bulan + "'" + _
    '                       " and length(" + par_colom + ") = 16 " + _
    '                       " limit 1"
    '                .InitializeCommand()
    '                .FillDataSet(ds_bantu, "transactionnumber")
    '                no_urut_format = ds_bantu.Tables(0).Rows(0).Item("no_urut") + par_i
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    If Len(no_urut_format) = 1 Then
    '        no_urut_format = "000" + no_urut_format.ToString
    '    ElseIf Len(no_urut_format) = 2 Then
    '        no_urut_format = "00" + no_urut_format.ToString
    '    ElseIf Len(no_urut_format) = 3 Then
    '        no_urut_format = "0" + no_urut_format.ToString
    '    ElseIf Len(no_urut_format) = 4 Then
    '        no_urut_format = no_urut_format.ToString
    '    End If

    '    get_ass_code = par_type + "-" + tahun + "-" + bulan + "-" + par_pt_code + "-" + no_urut_format

    '    Return get_ass_code
    'End Function
#End Region
End Class