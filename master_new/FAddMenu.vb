Imports Npgsql
Imports master_new.ModFunction

Public Class FAddMenu

    Dim menuid_mstr As Integer

    Public Overrides Sub MasterWITwo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        'My.configurasi_menu("awal_transaksi")
        set_button_mdi()
    End Sub

    Private Sub FAddMenu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        Dim dt2 As New DataTable
        Dim ssql As String

        ssql = "select null as menuid, '-' as menudesc,null as menuid_parent,'' as menudesc_parent UNION select a.menuid ,a.menudesc ,a.menuid_parent, b.menudesc as menudesc_parent  from tconfmenucollection a left outer join tconfmenucollection b on b.menuid=a.menuid_parent   " _
                & "order by menudesc"

        dt2 = PGSqlConn.GetTableData(ssql)
        With menuid_parent
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("menuid", "ID", 10))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("menudesc", "Desc", 30))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("menuid_parent", "ID Parent", 10))
                .Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("menudesc_parent", "Desc Parent", 30))
            End If

            .Properties.DataSource = dt2
            .Properties.DisplayMember = dt2.Columns("menudesc").ToString
            .Properties.ValueMember = dt2.Columns("menuid").ToString
            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 20
            .Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            .EditValue = dt2.Rows(0).Item("menuid")
        End With

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select menuid, menuname,menuid_parent," _
                    & "(select b.menudesc from tconfmenucollection b where b.menuid=a.menuid_parent) as menuid_parent_desc,menudesc,menuseq " + _
                     " from  tconfmenucollection a order by menuid_parent_desc,menuname"
        Return get_sequel
    End Function

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "ID", "menuid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "menuname", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Parent", "menuid_parent_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Menu Desc", "menudesc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Menu Number", "menuseq", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub insert_data_awal()
        menuname.Focus()
        menuname.Text = ""
        menuid_parent.ItemIndex = 0
        menudesc.Text = ""
        menuseq.Text = ""
    End Sub

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList

        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupid from tconfgroup"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim menuid As Integer
        Try
            Using objbantu As New master_new.WDABasepgsql("", "")
                With objbantu
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(menuid),0) + 1 as max_id from tconfmenucollection"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read()
                        menuid = .DataReader.Item("max_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfmenucollection " _
                                            & "( " _
                                            & "  menuid, " _
                                            & "  menuname,menuid_parent,menudesc,menuseq " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(menuid) & " ,  " _
                                            & SetSetring(menuname.Text) & ",  " _
                                            & SetInteger(menuid_parent.EditValue) & " , " _
                                            & SetSetring(menudesc.Text) & ",  " _
                                            & SetInteger(menuseq.Text) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.tconfmenu " _
                                                & "( " _
                                                & "  groupid, " _
                                                & "  menuid, " _
                                                & "  enablestatus, " _
                                                & "  visiblestatus, " _
                                                & "  editablestatus,deleteablestatus,insertablestatus " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetInteger(ds_bantu.Tables(0).Rows(i).Item("groupid")) & ",  " _
                                                & SetInteger(menuid) & ",  False , False, False, False, False)"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        set_to_data_insert()
                        after_success()
                        set_row(menuid, "menuid")
                        insert = True

                    Catch ex As NpgsqlException
                        row = 0
                        insert = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As NpgsqlException
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                menuid_mstr = .Item("menuid")
                menuname.Text = .Item("menuname")
                menuid_parent.EditValue = .Item("menuid_parent")
                menudesc.Text = SetString(.Item("menudesc"))
                menuseq.Text = SetString(.Item("menuseq"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.tconfmenucollection   " _
                                            & "SET  " _
                                            & "  menuname = " & SetSetring(menuname.Text) & ",  " _
                                            & "  menuid_parent = " & SetInteger(menuid_parent.EditValue) & ",  " _
                                             & "  menuseq = " & SetInteger(menuseq.EditValue) & ",  " _
                                            & "  menudesc = " & SetSetring(menudesc.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  menuid = " & SetInteger(menuid_mstr) & " "
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
                        set_row(Trim(menuid_mstr), "menuid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from tconfmenucollection where menuid = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("menuid"))
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
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Private Sub BtMenuTree_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtMenuTree.Click
        Try
            Dim frm As New frmMenuBrowse
            frm.fobject = Me
            frm._obj = menuid_parent
            frm.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
