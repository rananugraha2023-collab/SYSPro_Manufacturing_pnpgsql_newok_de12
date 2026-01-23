Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList.Nodes

Public Class frmTestMenu
    Public par_user_id As String
    Public searchString As String
    Public searchIndex As Integer
    Public searchArray As New List(Of TreeListNode)
    Private Sub init_menu_tree()
        Dim sSQL As String
        Try

            sSQL = "SELECT  " _
              & "a.menuid, " _
              & "a.menuname, " _
              & "a.menuid_parent,a.menudesc,a.menuseq,'' as menu_access " _
              & "FROM " _
              & "  public.tconfmenucollection a " _
              & "WHERE menuid in (SELECT menu_id as menuid " _
              & "FROM " _
              & "  public.get_all_menu_group(" & par_user_id & ") where menu_id is not null " & _
              " UNION  " _
              & "SELECT menu_id as menuid " _
              & "FROM " _
              & "  public.get_all_menu_user(" & par_user_id & ") where menu_id is not null) "


            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            For Each dr As DataRow In dt.Rows
                dr("menu_access") = cek_akses(par_user_id, dr("menuname"))
            Next
            dt.AcceptChanges()

            TreeList1.DataSource = dt
            'TreeList1.BestFitColumns()
            TreeList1.Columns("menuseq").SortOrder = SortOrder.Ascending
            TreeList1.CollapseAll()
            TreeList1.BestFitColumns()
            'ceExpandCollaps.EditValue = False
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Function cek_akses(ByVal par_id As Integer, ByVal par_form As String) As String
        Try
            Dim sSQL As String
            sSQL = "select editablestatus,deleteablestatus,insertablestatus " & _
                   " from tconfusergroup ug " & _
                   " inner join tconfmenu m on m.groupid = ug.groupid " & _
                   " inner join tconfmenucollection mc on mc.menuid = m.menuid " & _
                   " where enablestatus = true " & _
                   " and userid = " & par_id & _
                   " and menuname ~~* '" & par_form & "'" & _
                   "UNION  " _
                    & "SELECT  " _
                    & "  " _
                    & "  a.editablestatus, " _
                    & "  a.deleteablestatus,a.insertablestatus " _
                    & "FROM " _
                    & "  public.tconfmenuuser a " _
                    & "  INNER JOIN public.tconfmenucollection b ON (a.menuid = b.menuid) " _
                    & "WHERE " _
                    & "  a.userid = " & par_id & " " _
                    & " and menuname ~~* '" & par_form & "' " _
                    & " order by insertablestatus,editablestatus,deleteablestatus"

            Dim dt As New DataTable
            dt = GetTableData(sSQL)

            Dim _hasil As String = ""

            For Each dr As DataRow In dt.Rows
                _hasil = "Edit:" & IIf(dr("editablestatus").ToString = "True", "Y", "N") & ", Del:" & IIf(dr("deleteablestatus").ToString = "True", "Y", "N") & ",Ins:" & IIf(dr("insertablestatus").ToString = "True", "Y", "N")
            Next

            Return _hasil
        Catch ex As Exception
            Return ""
        End Try
    End Function

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


    Private Sub frmTestMenu_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        TreeList1.BestFitColumns()
    End Sub
End Class
