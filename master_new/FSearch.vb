Public Class FSearch

    Public fobject As Object

    Public Overridable Sub set_win(ByVal arg As Object)
        fobject = arg
    End Sub

    Public Overrides Sub form_first_load()
        help_load_data(False)
        format_grid()
        add_groupsummary()
        AllowIncrementalSearch()
        load_Columns()

        gc = get_gc()

        gc.UseEmbeddedNavigator = True
        format_embeddednavigator(gc)

        gv = gc.MainView
        gv.OptionsView.ColumnAutoWidth = False
        gv.OptionsView.ShowAutoFilterRow = True
        gv.OptionsView.ShowFooter = True
        gv.OptionsView.ShowGroupedColumns = True
        Me.CenterToScreen()
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        load_data(par, get_gc())
    End Sub

    Public Overrides Sub load_data(ByVal arg As Boolean, ByVal gc As DevExpress.XtraGrid.GridControl)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            set_param_textbox()
            '================================================================
            Try
                'row = BindingContext(ds.Tables(0)).Position
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload

                        .SQL = get_sequel()
                        .InitializeCommand()
                        .FillDataSet(ds, Me.Name + "_select")
                        gc.DataSource = ds.Tables(0)
                        BindingContext(ds.Tables(0)).Position = row
                        bestfit_column()
                        load_data_grid_detail()
                        ConditionsAdjustment()
                    End With

                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub add_groupsummary()
        gv = get_gc().MainView
        gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "groupsummary")
    End Sub

    Public Overrides Sub AllowIncrementalSearch()
        gv = get_gc().MainView
        gv.OptionsBehavior.AllowIncrementalSearch = True
    End Sub

    Public Overrides Sub bestfit_column()
        gv = get_gc().MainView
        gv.BestFitColumns()
    End Sub

    Public Function get_gc() As Object
        get_gc = DBNull.Value
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        If ctrl_xtc.name = "xtp_data" Then
                                            xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                            For Each ctrl_xtp In xtp.Controls
                                                If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                    get_gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        Return get_gc
    End Function

    Public Overrides Function get_gv() As Object
        get_gv = DBNull.Value
        gv = get_gc().MainView
        get_gv = gv
        Return get_gv
    End Function

    Public Overrides Sub Master_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.Handled = True
            Return
            'ElseIf e.KeyCode = Keys.Escape Then
            '    Close()
        End If
    End Sub
End Class
