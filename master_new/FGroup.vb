Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList
Imports System.Collections.Generic


Public Class FGroup
    Public startNode As TreeListNode
    Public currentNode As TreeListNode
    Dim _groupid_mstr As Integer


    Dim _number_find As Integer = 0
    Public searchString As String
    Public searchIndex As Integer
    Public searchArray As New List(Of TreeListNode)

    Private Sub FGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        Dim ds_cb As New DataSet
        Using objcb As New master_new.WDABasepgsql("", "")
            With objcb
                .SQL = "select groupid , groupnama from tconfgroup order by groupnama"
                .InitializeCommand()
                .FillDataSet(ds_cb, "HelpGroup")
                cb_group.Properties.DataSource = ds_cb.Tables("HelpGroup")
                cb_group.Properties.DisplayMember = ds_cb.Tables("HelpGroup").Columns("groupnama").ToString
                cb_group.Properties.ValueMember = ds_cb.Tables("HelpGroup").Columns("groupid").ToString
                cb_group.EditValue = ds_cb.Tables("HelpGroup").Rows(0).Item("groupid")

            End With
        End Using

    End Sub

    Public Overrides Sub format_grid()

        Dim test As Guid = Guid.NewGuid

        add_column(gv_master, "Kode", "groupkode", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Group", "groupnama", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Default", "groupdefault", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Active", "groupactive", DevExpress.Utils.HorzAlignment.Center)

        add_col_treelist(TreeList1, "Menu", "menudesc")
        add_col_treelist(TreeList1, "Enable", "enablestatus")
        add_col_treelist(TreeList1, "Visible", "visiblestatus")
        add_col_treelist(TreeList1, "Insertable", "insertablestatus")
        add_col_treelist(TreeList1, "Editable", "editablestatus")
        add_col_treelist(TreeList1, "Deleteable", "deleteablestatus")
        add_col_treelist(TreeList1, "Cancelable", "cancelablestatus")

        add_column(gv_detail, "userid", False)
        add_column(gv_detail, "User Name", "usernama", DevExpress.Utils.HorzAlignment.Default)

    End Sub


    Public Overrides Function get_sequel() As String
        get_sequel = "select groupid, groupkode, groupnama, groupdefault, groupactive" + _
                     " from tconfgroup " + _
                     " order by groupid"
        Return get_sequel
    End Function

    Public Overrides Function insert_data() As Boolean

        sc_txtkode_1.Text = ""
        sc_txtgroup.Text = ""
        groupactive.EditValue = True
        sc_cbdefault.EditValue = False

        Dim ssql As String
        ssql = "SELECT  " _
            & "a.menuid, " _
            & "a.menudesc , " _
            & "a.menuid_parent, " _
            & "false as enablestatus, " _
            & "false as visiblestatus, " _
            & "false as deleteablestatus, " _
               & "false as insertablestatus, " _
            & "false as editablestatus,false as cancelablestatus " _
            & "FROM " _
            & "  public.tconfmenucollection a "


        Dim dt As New DataTable
        dt = GetTableData(ssql)

        TreeList1.DataSource = dt

        TreeList1.ExpandAll()
        TreeList1.BestFitColumns()

        TreeList1.Columns("menudesc").SortOrder = SortOrder.Ascending

        Return MyBase.insert_data()
    End Function
    Public Overrides Function insert() As Boolean
        Dim ds_bantu As New DataSet
        Dim i As Integer = 0

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .SQL = "select coalesce(max(groupid),0)+1 as max_id from tconfgroup "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "max_id")

                    .SQL = "select menuid from tconfmenucollection "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "menu")
                End With
            End Using
        Catch ex As NpgsqlException
            MessageBox.Show(ex.Message)
        End Try

        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction
                    Try
                        _groupid_mstr = ds_bantu.Tables("max_id").Rows(0).Item("max_id").ToString
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfgroup " _
                                            & "( " _
                                            & "  groupid, " _
                                            & "  groupkode, " _
                                            & "  groupnama, " _
                                            & "  groupdefault, " _
                                            & "  groupactive " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & _groupid_mstr & ",  '" _
                                            & Trim(sc_txtkode_1.Text) & "',  '" _
                                            & Trim(sc_txtgroup.Text) & "',  " _
                                            & sc_cbdefault.Text & ",  '" _
                                            & IIf(groupactive.EditValue = True, "Y", "N") & "'  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'For i = 0 To ds_bantu.Tables("menu").Rows.Count - 1
                        '    .Command.CommandText = "insert into tconfmenu (groupid, menuid, enablestatus) " _
                        '         + " values (" + ds_bantu.Tables("max_id").Rows(0).Item("max_id").ToString + "," + ds_bantu.Tables("menu").Rows(i).Item("menuid").ToString + ",false)"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        'Next

                        startNode = TreeList1.Nodes.FirstNode
                        currentNode = startNode
                        TreeList1.FocusedNode = Nothing

                        While currentNode IsNot Nothing

                            If currentNode.Item("enablestatus") = True Or _
                            currentNode.Item("visiblestatus") = True Or _
                            currentNode.Item("editablestatus") = True Or _
                              currentNode.Item("insertablestatus") = True Or _
                            currentNode.Item("deleteablestatus") = True Or _
                            setBoolean(currentNode.Item("cancelablestatus")) = True Then

                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfmenu " _
                                            & "( " _
                                            & "  groupid, " _
                                            & "  menuid, " _
                                            & "  enablestatus, " _
                                            & "  visiblestatus, " _
                                            & "  deleteablestatus, " _
                                              & "  insertablestatus, " _
                                            & "  editablestatus,cancelablestatus " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & _groupid_mstr & ",  " _
                                            & currentNode.Item("menuid") & ",  " _
                                            & currentNode.Item("enablestatus").ToString & ",  " _
                                            & currentNode.Item("visiblestatus").ToString & ",  " _
                                            & currentNode.Item("deleteablestatus").ToString & ",  " _
                                            & currentNode.Item("insertablestatus").ToString & ",  " _
                                            & currentNode.Item("editablestatus").ToString & ",  " _
                                             & setBoolean(currentNode.Item("cancelablestatus")).ToString & "  " _
                                            & ")"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            End If

                            currentNode = GetNextNode(currentNode, TreeList1.Nodes)
                            'If currentNode Is Nothing Then
                            '    currentNode = startNode
                            'End If
                        End While


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

                        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

                        insert = True
                    Catch ex As NpgsqlException
                        insert = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As NpgsqlException
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _groupid_mstr = .Item("groupid")
                sc_txtkode_1.Text = .Item("groupkode")
                sc_txtgroup.Text = .Item("groupnama")
                sc_cbdefault.SelectedIndex = IIf(.Item("groupdefault") = True, 1, 2)
                groupactive.EditValue = IIf(.Item("groupactive") = "Y", True, False)
            End With

            Dim ssql As String
            ssql = "SELECT  " _
                & "  a.menuid, " _
                & "  a.menudesc , " _
                & "  a.menuid_parent, " _
                & "  b.enablestatus, " _
                & "  b.visiblestatus, " _
                & "  b.deleteablestatus, " _
                & "  b.editablestatus,b.insertablestatus,b.cancelablestatus " _
                & "FROM " _
                & "  public.tconfmenucollection a " _
                & "  INNER JOIN public.tconfmenu b ON (a.menuid = b.menuid) " _
                & "WHERE " _
                & "  b.groupid = " & _groupid_mstr _
                & "union " _
                & "SELECT  " _
                & "a.menuid, " _
                & "a.menudesc, " _
                & "a.menuid_parent, " _
                & "false as enablestatus, " _
                & "false as visiblestatus, " _
                & "false as deleteablestatus, " _
                & "false as editablestatus, " _
                & "false as insertablestatus,false as cancelablestatus " _
                & "FROM " _
                & "  public.tconfmenucollection a " _
                & "Where a.menuid not in  " _
                & "(SELECT  " _
                & "  a.menuid " _
                & "FROM " _
                & "  public.tconfmenucollection a " _
                & "  INNER JOIN public.tconfmenu b ON (a.menuid = b.menuid) " _
                & "WHERE " _
                & "  b.groupid = " & _groupid_mstr & ")"


            Dim dt As New DataTable
            dt = GetTableData(ssql)

            TreeList1.DataSource = dt

            TreeList1.ExpandAll()
            TreeList1.BestFitColumns()

            TreeList1.Columns("menudesc").SortOrder = SortOrder.Ascending

            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden

            edit_data = True
        End If
    End Function
    'Private Sub treeList1_AfterCheckNode(ByVal sender As Object, ByVal e As NodeEventArgs) Handles TreeList1.AfterCheckNode
    '    SetCheckedChildNodes(e.Node, e.Node.CheckState)
    '    'SetCheckedParentNodes(e.Node, e.Node.CheckState)
    'End Sub
    'Private Sub TreeList1_AfterCheckNode(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs) Handles TreeList1.AfterCheckNode
    '    SetCheckedChildNodes(e.Node, e.Node.Item("enablestatus"))
    'End Sub

    Private Sub SetCheckedChildNodes(ByVal node As TreeListNode, ByVal check As CheckState)
        For i As Integer = 0 To node.Nodes.Count - 1
            'node.Nodes(i).CheckState = check
            node.Nodes(i).Item("enablestatus") = check
            node.Nodes(i).Item("visiblestatus") = check
            node.Nodes(i).Item("insertablestatus") = check
            node.Nodes(i).Item("enablestatus") = check
            node.Nodes(i).Item("deleteablestatus") = check
            node.Nodes(i).Item("cancelablestatus") = check
            SetCheckedChildNodes(node.Nodes(i), check)
        Next i
    End Sub

    Private Function GetNextNode(ByVal node As TreeListNode, ByVal rootNodes As TreeListNodes) As TreeListNode
        If node Is Nothing Then
            Return Nothing
        End If
        If node.Nodes.Count > 0 Then
            Return node.Nodes(0)
        End If

        Dim treeList As DevExpress.XtraTreeList.TreeList = node.TreeList

        If node.ParentNode IsNot Nothing Then
            Dim owner As TreeListNodes = node.ParentNode.Nodes
            While node Is owner.LastNode
                If owner Is treeList.Nodes Then
                    Return Nothing
                End If
                If node.ParentNode Is Nothing Then
                    Return Nothing
                End If
                node = node.ParentNode
                If node.ParentNode Is Nothing Then
                    owner = rootNodes
                Else
                    owner = node.ParentNode.Nodes
                End If
            End While
            Dim index As Integer = owner.IndexOf(node)
            Return owner(index + 1)

        Else
            If treeList.Nodes.LastNode Is node Then
                Return Nothing
            Else
                Dim index As Integer = treeList.Nodes.IndexOf(node)
                Return treeList.Nodes(index + 1)
            End If
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
                                            & "  public.tconfgroup   " _
                                            & "SET  " _
                                            & "  groupkode = '" & Trim(sc_txtkode_1.Text) & "',  " _
                                            & "  groupnama = '" & Trim(sc_txtgroup.Text) & "',  " _
                                            & "  groupdefault = " & IIf(sc_cbdefault.SelectedIndex = 1, True, False) & ",  " _
                                            & "  groupactive = '" & IIf(groupactive.EditValue = True, "Y", "N") & "'  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  groupid = " & _groupid_mstr.ToString
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "  DELETE FROM   public.tconfmenu " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  groupid = " & _groupid_mstr.ToString

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        startNode = TreeList1.Nodes.FirstNode
                        currentNode = startNode
                        TreeList1.FocusedNode = Nothing

                        While currentNode IsNot Nothing

                            If currentNode.Item("enablestatus") = True Or _
                            currentNode.Item("visiblestatus") = True Or _
                            currentNode.Item("deleteablestatus") = True Or _
                             currentNode.Item("insertablestatus") = True Or _
                            currentNode.Item("editablestatus") = True Or _
                            setBoolean(currentNode.Item("cancelablestatus")) = True Then

                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfmenu " _
                                            & "( " _
                                            & "  groupid, " _
                                            & "  menuid, " _
                                            & "  enablestatus, " _
                                            & "  visiblestatus, " _
                                            & "  deleteablestatus, " _
                                              & "  insertablestatus, " _
                                            & "  editablestatus,cancelablestatus " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & _groupid_mstr & ",  " _
                                            & currentNode.Item("menuid") & ",  " _
                                            & currentNode.Item("enablestatus").ToString & ",  " _
                                            & currentNode.Item("visiblestatus").ToString & ",  " _
                                             & currentNode.Item("deleteablestatus").ToString & ",  " _
                                              & currentNode.Item("insertablestatus").ToString & ",  " _
                                            & currentNode.Item("editablestatus").ToString & ",  " _
                                              & setBoolean(currentNode.Item("cancelablestatus")).ToString & "  " _
                                            & ")"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            End If

                            currentNode = GetNextNode(currentNode, TreeList1.Nodes)
                            'If currentNode Is Nothing Then
                            '    currentNode = startNode
                            'End If
                        End While

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

                        sqlTran.Commit()
                        after_success()
                        edit = True
                    Catch ex As nPgSqlException
                        edit = False
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
                            .Command.CommandText = "delete from tconfgroup where groupid = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("groupid").ToString
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

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        show_detail()
    End Sub

    Private Sub gv_master_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_master.FocusedRowChanged
        show_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        show_detail()
    End Sub
    Private Sub show_detail()
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String

            Try
                Try
                    ds.Tables("detail").Clear()
                Catch ex As Exception
                End Try


                sql = "SELECT  " _
                & "  b.userid, " _
                & "  a.usernama " _
                & "FROM " _
                & "  public.tconfusergroup b " _
                & "  INNER JOIN public.tconfuser a ON (b.userid = a.userid) " _
                & " Where b.groupid= " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("groupid").ToString & " order by usernama"


                load_data_detail(sql, gc_detail, "detail")

            Catch ex As Exception
                Pesan(Err)
            End Try
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function


    Private Sub gv_detail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_detail.DoubleClick
        Try
            Dim frm As New frmTestMenu

            frm.par_user_id = ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("userid")
            frm.Show()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_copy.Click
        Dim ssql As String
        Dim sSqls As New ArrayList

        ssql = "SELECT  " _
               & "  a.menuid, " _
               & "  a.menudesc , " _
               & "  a.menuid_parent, " _
               & "  b.enablestatus, " _
               & "  b.visiblestatus, " _
               & "  b.deleteablestatus, " _
               & "  b.editablestatus,b.insertablestatus,b.cancelablestatus " _
               & "FROM " _
               & "  public.tconfmenucollection a " _
               & "  INNER JOIN public.tconfmenu b ON (a.menuid = b.menuid) " _
               & "WHERE " _
               & "  b.groupid = " & cb_group.EditValue

        Dim dt As New DataTable
        dt = GetTableData(ssql)

        Using objinsert As New master_new.WDABasepgsql("", "")
            With objinsert
                .Connection.Open()
                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                Try
                    .Command = .Connection.CreateCommand
                    .Command.Transaction = sqlTran

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "  DELETE FROM   public.tconfmenu " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  groupid = " & _groupid_mstr.ToString

                    sSqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    For Each dr As DataRow In dt.Rows


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                    & "  public.tconfmenu " _
                                    & "( " _
                                    & "  groupid, " _
                                    & "  menuid, " _
                                    & "  enablestatus, " _
                                    & "  visiblestatus, " _
                                    & "  deleteablestatus, " _
                                      & "  insertablestatus, " _
                                    & "  editablestatus,cancelablestatus " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & _groupid_mstr & ",  " _
                                    & dr("menuid") & ",  " _
                                    & dr("enablestatus").ToString & ",  " _
                                    & dr("visiblestatus").ToString & ",  " _
                                     & dr("deleteablestatus").ToString & ",  " _
                                      & dr("insertablestatus").ToString & ",  " _
                                    & dr("editablestatus").ToString & ",  " _
                                      & setBoolean(dr("cancelablestatus")).ToString & "  " _
                                    & ")"

                        sSqls.Add(.Command.CommandText)
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
                    Box("Copy success")

                    'Dim ssql As String
                    ssql = "SELECT  " _
                        & "  a.menuid, " _
                        & "  a.menudesc , " _
                        & "  a.menuid_parent, " _
                        & "  b.enablestatus, " _
                        & "  b.visiblestatus, " _
                        & "  b.deleteablestatus, " _
                        & "  b.editablestatus,b.insertablestatus,b.cancelablestatus " _
                        & "FROM " _
                        & "  public.tconfmenucollection a " _
                        & "  INNER JOIN public.tconfmenu b ON (a.menuid = b.menuid) " _
                        & "WHERE " _
                        & "  b.groupid = " & _groupid_mstr _
                        & "union " _
                        & "SELECT  " _
                        & "a.menuid, " _
                        & "a.menudesc , " _
                        & "a.menuid_parent, " _
                        & "false as enablestatus, " _
                        & "false as visiblestatus, " _
                          & "false as deleteablestatus, " _
                        & "false as editablestatus, " _
                         & "false as insertablestatus,false as cancelablestatus " _
                        & "FROM " _
                        & "  public.tconfmenucollection a " _
                        & "Where a.menuid not in  " _
                        & "(SELECT  " _
                        & "  a.menuid " _
                        & "FROM " _
                        & "  public.tconfmenucollection a " _
                        & "  INNER JOIN public.tconfmenu b ON (a.menuid = b.menuid) " _
                        & "WHERE " _
                        & "  b.groupid = " & _groupid_mstr & ")"


                    'Dim dt_menu As New DataTable
                    dt = GetTableData(ssql)


                    TreeList1.DataSource = dt

                    TreeList1.ExpandAll()
                    TreeList1.BestFitColumns()

                    TreeList1.Columns("menudesc").SortOrder = SortOrder.Ascending

                    DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden


                Catch ex As nPgSqlException
                    sqlTran.Rollback()
                    MessageBox.Show(ex.Message)
                End Try
            End With
        End Using

    End Sub

    Private Sub btSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSearch.Click
        If txtSearchMenu.Text <> "" Then
            searchString = txtSearchMenu.Text
            searchArray.Clear()
            searchIndex = -1

            Dim opS As MyNodeOperation_Search = New MyNodeOperation_Search(searchString)
            TreeList1.NodesIterator.DoLocalOperation(opS, TreeList1.Nodes)
            searchArray = opS.SearchList

            If searchArray.Count > 0 Then
                searchIndex = 0
                TreeList1.FocusedNode = searchArray(searchIndex)
                'staSearch.Text = (searchIndex + 1).ToString & " / " & searchArray.Count.ToString
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("No result found for [" & searchString & "] !", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'staSearch.Text = ""
            End If
        End If

    End Sub

    Private Sub btFindNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFindNext.Click
        If txtSearchMenu.Text <> "" Then
            If searchArray.Count > 0 Then
                searchIndex += 1
                If searchIndex > (searchArray.Count - 1) Then
                    searchIndex = 0
                End If
                TreeList1.FocusedNode = searchArray(searchIndex)
                'staSearch.Text = (searchIndex + 1).ToString & " / " & searchArray.Count.ToString
            End If
        End If

    End Sub
End Class

Class MyNodeOperation_Search

    Inherits TreeListOperation
    Private search_string As String
    Private search_count As Integer
    Private search_lst As New List(Of TreeListNode)


    Public Sub New(ByVal searchStr As String)
        Me.search_string = searchStr.ToUpper.Trim
        Me.search_count = 0
        Me.search_lst.Clear()
    End Sub

    Public Overrides Sub Execute(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
        If node.Item("menudesc").ToString.ToLower.Contains(search_string.ToLower) Then
            search_count += 1
            search_lst.Add(node)
        End If
    End Sub

    Public ReadOnly Property SearchCount() As Integer
        Get
            Return search_count
        End Get
    End Property

    Public ReadOnly Property SearchList() As List(Of TreeListNode)
        Get
            Return search_lst
        End Get
    End Property

End Class
