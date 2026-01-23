Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid

Public Class FSortMenu
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim dt As New DataTable
    Dim ssql As String
    Private downHitInfo As GridHitInfo = Nothing
    Private Const OrderFieldName As String = "menuseq"

    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dt2 As New DataTable
            Dim ssql As String

            ssql = "select null as menuid, '-' as menudesc,null as menuid_parent,'' as menudesc_parent UNION select a.menuid ,a.menudesc ,a.menuid_parent, b.menudesc as menudesc_parent  from tconfmenucollection a left outer join tconfmenucollection b on b.menuid=a.menuid_parent   " _
                    & "order by menudesc"

            dt2 = master_new.PGSqlConn.GetTableData(ssql)
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

            format_grid()

            gv_master.Columns(OrderFieldName).SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            gv_master.OptionsCustomization.AllowSort = False
            gv_master.OptionsView.ShowGroupPanel = False

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub format_grid()
        add_column(gv_master, "ID", "menuid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "menuname", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Parent", "menuid_parent_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Menu Desc", "menudesc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Menu Number", "menuseq", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Private Sub btMenuTree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btMenuTree.Click
        Try
            Dim frm As New frmMenuBrowse
            frm.fobject = Me
            frm._obj = menuid_parent
            frm.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub BtRetreive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRetreive.Click
        Try
            ssql = "select menuid, menuname,menuid_parent," _
                    & "(select b.menudesc from tconfmenucollection b where b.menuid=a.menuid_parent) as menuid_parent_desc,menudesc,menuseq " + _
                     " from  tconfmenucollection a where menuid_parent " & IIf(menuid_parent.EditValue Is System.DBNull.Value, "is null", "=" & menuid_parent.EditValue) & "  order by menuseq"

            dt = master_new.PGSqlConn.GetTableData(ssql)
            gc_master.DataSource = dt
            gv_master.BestFitColumns()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSimpan.Click
        Try
            Dim ssqls As New ArrayList

            For i As Integer = 0 To gv_master.RowCount - 1
                ssql = "UPDATE  " _
                        & "  public.tconfmenucollection   " _
                        & "SET  " _
                        & "  menuseq = " & SetInteger(i) & "  " _
                        & "WHERE  " _
                        & "  menuid = " & SetInteger(gv_master.GetRowCellValue(i, "menuid")) & " "

                ssqls.Add(ssql)

            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Data Saved")

            BtRetreive_Click(sender, e)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    

    Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gv_master.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        downHitInfo = Nothing

        Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
        If Control.ModifierKeys <> Keys.None Then
            Return
        End If
        If e.Button = MouseButtons.Left AndAlso hitInfo.InRow AndAlso hitInfo.RowHandle <> GridControl.NewItemRowHandle Then
            downHitInfo = hitInfo
        End If
    End Sub

    Private Sub gridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gv_master.MouseMove
        Dim view As GridView = TryCast(sender, GridView)
        If e.Button = MouseButtons.Left AndAlso downHitInfo IsNot Nothing Then
            Dim dragSize As Size = SystemInformation.DragSize
            Dim dragRect As New Rectangle(New Point(downHitInfo.HitPoint.X - dragSize.Width \ 2, downHitInfo.HitPoint.Y - dragSize.Height \ 2), dragSize)

            If (Not dragRect.Contains(New Point(e.X, e.Y))) Then
                view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All)
                downHitInfo = Nothing
            End If
        End If
    End Sub

    Private Sub gridControl1_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles gc_master.DragOver
        If e.Data.GetDataPresent(GetType(GridHitInfo)) Then
            Dim downHitInfo As GridHitInfo = TryCast(e.Data.GetData(GetType(GridHitInfo)), GridHitInfo)
            If downHitInfo Is Nothing Then
                Return
            End If

            Dim grid As GridControl = TryCast(sender, GridControl)
            Dim view As GridView = TryCast(grid.MainView, GridView)
            Dim hitInfo As GridHitInfo = view.CalcHitInfo(grid.PointToClient(New Point(e.X, e.Y)))
            If hitInfo.InRow AndAlso hitInfo.RowHandle <> downHitInfo.RowHandle AndAlso hitInfo.RowHandle <> GridControl.NewItemRowHandle Then
                e.Effect = DragDropEffects.Move
            Else
                e.Effect = DragDropEffects.None
            End If
        End If
    End Sub

    Private Sub gridControl1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles gc_master.DragDrop
        Dim grid As GridControl = TryCast(sender, GridControl)
        Dim view As GridView = TryCast(grid.MainView, GridView)
        Dim srcHitInfo As GridHitInfo = TryCast(e.Data.GetData(GetType(GridHitInfo)), GridHitInfo)
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(grid.PointToClient(New Point(e.X, e.Y)))
        Dim sourceRow As Integer = srcHitInfo.RowHandle
        Dim targetRow As Integer = hitInfo.RowHandle
        MoveRow(sourceRow, targetRow)
    End Sub

    Private Sub MoveRow(ByVal sourceRow As Integer, ByVal targetRow As Integer)
        If sourceRow = targetRow OrElse sourceRow = targetRow + 1 Then
            Return
        End If

        Dim view As GridView = gv_master
        Dim row1 As DataRow = view.GetDataRow(targetRow)
        Dim row2 As DataRow = view.GetDataRow(targetRow + 1)
        Dim dragRow As DataRow = view.GetDataRow(sourceRow)
        Dim val1 As Decimal = CDec(row1(OrderFieldName))
        If row2 Is Nothing Then
            dragRow(OrderFieldName) = val1 + 1
        Else
            Dim val2 As Decimal = CDec(row2(OrderFieldName))
            dragRow(OrderFieldName) = (val1 + val2) / 2
        End If
    End Sub
End Class
