Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FPersonSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String
    Public _filter As String

    Public _site_id As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "Select", "pilih", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Person Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)
       
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT false as pilih, " _
                    & "  a.lbrfp_id, " _
                    & "  a.lbrfp_name, " _
                    & "  a.lbrfp_group " _
                    & "FROM " _
                    & "  public.lbrfp_person a " _
                    & " where lbrfp_name ~~* '%" + Trim(te_search.Text) + "%' "

        If _filter <> "" Then
            get_sequel += _filter
        End If

        get_sequel += "  order by a.lbrfp_name"

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Public Overrides Sub fill_data()
        Dim sSQL As String
        Try
            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position
            ds.AcceptChanges()
            If fobject.name = FWOLaborFeedback.Name Then

                Dim x As Integer = 0
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr("pilih") = True Then
                        If x = 0 Then
                            fobject.gv_person_edit.SetRowCellValue(_row, "lbrfd_lbrfp_id", ds.Tables(0).Rows(_row_gv).Item("lbrfp_id"))
                            fobject.gv_person_edit.SetRowCellValue(_row, "lbrfp_name", ds.Tables(0).Rows(_row_gv).Item("lbrfp_name"))
                            fobject.gv_person_edit.SetRowCellValue(_row, "lbrfp_group", ds.Tables(0).Rows(_row_gv).Item("lbrfp_group"))
                            fobject.gv_person_edit.BestFitColumns()
                            fobject.dt_edit.AcceptChanges()
                        Else
                            Dim _dtrow As DataRow
                            _dtrow = fobject.dt_edit.NewRow

                            _dtrow("lbrfd_lbrfp_id") = dr("lbrfp_id")
                            _dtrow("lbrfp_name") = SetString(dr("lbrfp_name"))
                            _dtrow("lbrfp_group") = SetString(dr("lbrfp_group"))

                            fobject.dt_edit.Rows.Add(_dtrow)
                            fobject.dt_edit.AcceptChanges()
                        End If
                       
                        x = x + 1
                    End If

                Next

                fobject.gv_person_edit.BestFitColumns()

            End If

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

End Class
