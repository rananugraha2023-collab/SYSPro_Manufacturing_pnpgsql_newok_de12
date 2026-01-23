Imports npgsql
Imports master_new.ModFunction

Public Class FPSTreeReport
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ds_detail As DataSet

    Private Sub FSOARFPReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        'pr_txttglawal.DateTime = _now
        'pr_txttglakhir.DateTime = _now

        'AddHandler gv_view1.FocusedRowChanged, AddressOf relation_detail
        'AddHandler gv_view1.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub format_grid()
      
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                       
                        Dim sql As String

                      

                        sql = "select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active,ptnr_code, a.ptnr_name ,b.lvl_name,en_desc  from ptnr_mstr a " _
                        & " inner join ptnrg_grp on ptnr_ptnrg_id=ptnrg_id " _
                         & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
                         & "  left outer join public.en_mstr c ON ptnr_en_id = en_id " _
                         & " where  a.ptnr_is_ps = 'Y'  AND a.ptnr_active = 'Y' and ptnrg_code='KLGSYAAMIL' order by en_desc, ptnr_name "


                        Dim dt_tree As New DataTable
                        dt_tree = master_new.PGSqlConn.GetTableData(sql)

                        TreeList1.DataSource = dt_tree
                        TreeList1.ExpandAll()
                        TreeList1.BestFitColumns()

                        'bestfit_column()
                        'load_data_grid_detail()
                        'ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Function export_data() As Boolean

        '  Dim ssql As String
        Try
            Dim _path As String

            Dim _filter As String
            _filter = "Excel Files | *.xlsx"
            _path = master_new.ModFunction.AskSaveAsFile(_filter)
            If _path <> "" Then
                TreeList1.ExportToXlsx(_path)

                If ask("Do you want to open this file?", "Export To...") = True Then
                    Try
                        Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                        process.StartInfo.FileName = _path
                        process.StartInfo.Verb = "Open"
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                        process.Start()
                    Catch
                        'MessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Pesan(Err)
                    End Try
                End If
            End If


        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function
End Class
