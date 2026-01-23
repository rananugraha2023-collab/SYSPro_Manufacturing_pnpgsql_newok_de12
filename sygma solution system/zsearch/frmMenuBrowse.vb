Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList.Nodes
Imports System.Collections.Generic


Public Class frmMenuBrowse
    Public par_user_id As String
    Public searchString As String
    Public searchIndex As Integer
    Public searchArray As New List(Of TreeListNode)
    Public _obj As Object
    Public fobject As Object
    Private Sub init_menu_tree()
        Dim sSQL As String
        Try

            sSQL = "SELECT  " _
              & "a.menuid, " _
              & "a.menuname, " _
              & "a.menuid_parent,a.menudesc,a.menuseq " _
              & "FROM " _
              & "  public.tconfmenucollection a "

            TreeList1.DataSource = GetTableData(sSQL)
            TreeList1.BestFitColumns()
            TreeList1.Columns("menuseq").SortOrder = SortOrder.Ascending
            TreeList1.CollapseAll()
            'ceExpandCollaps.EditValue = False
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub frmTestMenu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        init_menu_tree()
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

    'Private Sub TreeList1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeList1.DoubleClick
    '    If fobject.name = "FAddMenu" Then
    '        _obj.editvalue = TreeList1.FocusedNode("menuid")
    '    End If
    'End Sub

    Private Sub BtCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCancel.Click
        Me.Close()
    End Sub

    Private Sub BtOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOK.Click
        If fobject.name = FSortMenu.Name Then
            _obj.editvalue = TreeList1.FocusedNode("menuid")
        End If
        Me.Close()
    End Sub
End Class

'Class MyNodeOperation_Search

'    Inherits TreeListOperation
'    Private search_string As String
'    Private search_count As Integer
'    Private search_lst As New List(Of TreeListNode)


'    Public Sub New(ByVal searchStr As String)
'        Me.search_string = searchStr.ToUpper.Trim
'        Me.search_count = 0
'        Me.search_lst.Clear()
'    End Sub

'    Public Overrides Sub Execute(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
'        'If node.Item("partNumber").ToString.Contains(search_string) Or node.Item("description").ToString.Contains(search_string) Then
'        If node.Item("menudesc").ToString.ToLower.Contains(search_string.ToLower) Then
'            search_count += 1
'            search_lst.Add(node)
'        End If
'    End Sub

'    Public ReadOnly Property SearchCount() As Integer
'        Get
'            Return search_count
'        End Get
'    End Property

'    Public ReadOnly Property SearchList() As List(Of TreeListNode)
'        Get
'            Return search_lst
'        End Get
'    End Property

'End Class